#region License

// Copyright 2017 Drew Noakes
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

// TODO support right to left fonts
// TODO include comments/sample output in XML docs of FiggleFonts
// TODO have a utility that produces example output for each font in a markdown document
// TODO have a means of enumerating all fonts in FiggleFonts

namespace Figgle
{
    internal struct Line
    {
        public string Content { get; }
        public byte SpaceBefore { get; }
        public byte SpaceAfter { get; }

        public char FrontChar => Content.Length == SpaceBefore ? ' ' : Content[SpaceBefore];
        public char BackChar  => Content.Length == SpaceAfter  ? ' ' : Content[Content.Length - SpaceAfter - 1];

        public Line(string content, byte spaceBefore, byte spaceAfter)
        {
            Content = content;
            SpaceBefore = spaceBefore;
            SpaceAfter = spaceAfter;
        }
    }

    internal sealed class FiggleCharacter
    {
        public IReadOnlyList<Line> Lines { get; }
        public FiggleCharacter(IReadOnlyList<Line> lines) => Lines = lines;
    }

    /// <summary>Enumeration of possible text directions.</summary>
    public enum FiggleTextDirection
    {
        /// <summary>Text flows from the left to the right.</summary>
        LeftToRight = 0,
        /// <summary>Text flows from the right to the left.</summary>
        RightToLeft = 1
    }

    /// <summary>
    /// Holds metadata and glyphs for rendering characters in this font.
    /// </summary>
    public sealed class FiggleFont
    {
        private readonly IReadOnlyList<FiggleCharacter> _requiredCharacters;
        private readonly IReadOnlyDictionary<int, FiggleCharacter> _sparseCharacters;
        private readonly char _hardBlank;
        private readonly int _smushMode;

        private const int SM_SMUSH     = 0b10000000;
        private const int SM_KERN      = 0b01000000;
        private const int SM_HARDBLANK = 0b00100000;
        private const int SM_BIGX      = 0b00010000;
        private const int SM_PAIR      = 0b00001000;
        private const int SM_HIERARCHY = 0b00000100;
        private const int SM_LOWLINE   = 0b00000010;
        private const int SM_EQUAL     = 0b00000001;
        private const int SM_FULLWIDTH = 0;

        /// <summary>The height of each character, in rows.</summary>
        public int Height { get; }

        /// <summary>The number of rows from the top of the font to the baseline, excluding descenders.</summary>
        /// <remarks>Must be less than or equal to <see cref="Height"/>.</remarks>
        public int Baseline { get; }

        /// <summary>The direction that text reads when rendered with this font.</summary>
        public FiggleTextDirection Direction { get; }

        internal FiggleFont(IReadOnlyList<FiggleCharacter> requiredCharacters, IReadOnlyDictionary<int, FiggleCharacter> sparseCharacters, char hardBlank, int height, int baseline, FiggleTextDirection direction, int smushMode)
        {
            _requiredCharacters = requiredCharacters;
            _sparseCharacters = sparseCharacters;
            _hardBlank = hardBlank;
            _smushMode = smushMode;
            Height = height;
            Baseline = baseline;
            Direction = direction;
        }

        private FiggleCharacter GetCharacter(char c)
        {
            var i = (int)c;

            if (i < 0 || i > 255)
            {
                _sparseCharacters.TryGetValue(i, out var ch);
                return ch ?? _requiredCharacters[0];
            }

            return _requiredCharacters[i] ?? _requiredCharacters[0];
        }

        /// <summary>Gets whether this font contains the specified character.</summary>
        /// <remarks>Note that during rendering, if a character is not found then a character with value zero will be used instead, if present.</remarks>
        /// <param name="c">The character to test for.</param>
        /// <returns><c>true</c> if the character is present, otherwise <c>false</c>.</returns>
        public bool Contains(char c)
        {
            var i = (int)c;
            return i >= 0 && i <= 255
                ? _requiredCharacters[i] != null
                : _sparseCharacters.ContainsKey(i);
        }

        /// <summary>
        /// Renders <paramref name="message"/> using this font.
        /// </summary>
        /// <param name="message">The text to render.</param>
        /// <param name="smushOverride">Optional override for the smush settings. Defaults to <c>null</c>, meaning the font's default setting is used.</param>
        /// <returns></returns>
        public string Render(string message, int? smushOverride = null)
        {
            var smush = smushOverride ?? _smushMode;

            var outputLines = Enumerable.Range(0, Height).Select(_ => new StringBuilder()).ToList();

            FiggleCharacter lastCh = null;

            foreach (var c in message)
            {
                var ch = GetCharacter(c);

                if (ch == null)
                    continue;

                var fitMove = CalculateFitMove(lastCh, ch);

                for (var row = 0; row < Height; row++)
                {
                    var charLine = ch.Lines[row];
                    var outputLine = outputLines[row];

                    if (fitMove != 0)
                    {
                        var toMove = fitMove;
                        if (lastCh != null)
                        {
                            var lineSpace = lastCh.Lines[row].SpaceAfter;
                            if (lineSpace != 0)
                            {
                                var lineSpaceTrim = Math.Min(lineSpace, toMove);
                                toMove -= lineSpaceTrim;
                                outputLine.Length -= lineSpaceTrim;
                            }
                        }

                        var smushCharIndex = outputLine.Length - 1;
                        var cl = outputLine[smushCharIndex];

                        outputLine.Append(toMove == 0 ? charLine.Content : charLine.Content.Substring(toMove));

                        if (toMove != 0 && outputLine.Length != 0 && ch.Lines[row].Content.Length != 0)
                        {
                            var cr = ch.Lines[row].Content[toMove - 1];
                            var sc = TrySmush(cl, cr);
                            if (sc != '\0' && smushCharIndex >= 0)
                                outputLine[smushCharIndex] = sc;
                        }
                    }
                    else
                    {
                        outputLine.Append(charLine.Content);
                    }
                }

                lastCh = ch;
            }

            var res = new StringBuilder();

            foreach (var outputLine in outputLines)
                res.AppendLine(outputLine.Replace(_hardBlank, ' ').ToString());

            return res.ToString();

            int CalculateFitMove(FiggleCharacter l, FiggleCharacter r)
            {
                if (smush == SM_FULLWIDTH)
                    return 0;

                if (l == null)
                    return 0; // TODO could still shift b if it had whitespace in the first column

                var minMove = int.MaxValue;

                for (var row = 0; row < Height; row++)
                {
                    var ll = l.Lines[row];
                    var rl = r.Lines[row];

                    var move = ll.SpaceAfter + rl.SpaceBefore;

                    if (TrySmush(ll.BackChar, rl.FrontChar) != '\0')
                        move++;

                    if (move < minMove)
                        minMove = move;
                }

                Debug.Assert(minMove >= 0, "minMove >= 0");

                return minMove;
            }

            // TODO disallow smushing if either char's line has a length < 2
            char TrySmush(char l, char r)
            {
                if (l == ' ') return r;
                if (r == ' ') return l;

                // kerning
                if ((_smushMode & SM_SMUSH) == 0)
                    return '\0';

                // universal smushing
                if ((_smushMode & 0b00111111) == 0)
                {
                    // prefer visible character in case of hard blanks
                    if (l == _hardBlank) return r;
                    if (r == _hardBlank) return l;

                    // prefer overlapping character depending upon text direction
                    return Direction == FiggleTextDirection.LeftToRight ? r : l;
                }

                if ((_smushMode & SM_HARDBLANK) != 0 && l == _hardBlank && r == _hardBlank)
                    return l;

                if (l == _hardBlank && r == _hardBlank)
                    return '\0';

                if ((_smushMode & SM_EQUAL) != 0 && l == r)
                    return l;

                if ((_smushMode & SM_LOWLINE) != 0)
                {
                    const string lowLineChars = @"|/\[]{}()<>";
                    if (l == '_' && lowLineChars.Contains(r)) return r;
                    if (r == '_' && lowLineChars.Contains(l)) return l;
                }

                if ((_smushMode & SM_HIERARCHY) != 0)
                {
                    if (l == '|'          && @"/\[]{}()<>".Contains(r)) return r;
                    if (r == '|'          && @"/\[]{}()<>".Contains(l)) return l;
                    if ("/\\".Contains(l) &&    "[]{}()<>".Contains(r)) return r;
                    if ("/\\".Contains(r) &&    "[]{}()<>".Contains(l)) return l;
                    if ("[]" .Contains(l) &&      "{}()<>".Contains(r)) return r;
                    if ("[]" .Contains(r) &&      "{}()<>".Contains(l)) return l;
                    if ("{}" .Contains(l) &&        "()<>".Contains(r)) return r;
                    if ("{}" .Contains(r) &&        "()<>".Contains(l)) return l;
                    if ("()" .Contains(l) &&          "<>".Contains(r)) return r;
                    if ("()" .Contains(r) &&          "<>".Contains(l)) return l;
                }

                if ((_smushMode & SM_PAIR) != 0)
                {
                    if (l == '[' && r == ']') return '|';
                    if (r == '[' && l == ']') return '|';
                    if (l == '{' && r == '}') return '|';
                    if (r == '{' && l == '}') return '|';
                    if (l == '(' && r == ')') return '|';
                    if (r == '(' && l == ')') return '|';
                }

                if ((_smushMode & SM_BIGX) != 0)
                {
                    if (l == '/' && r == '\\') return '|';
                    if (r == '/' && l == '\\') return 'Y';
                    if (l == '>' && r == '<')  return 'X';
                }

                return '\0';
            }
        }
    }
}