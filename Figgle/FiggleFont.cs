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

namespace Figgle
{
    internal struct Line
    {
        public string Content { get; }
        public byte SpaceBefore { get; }
        public byte SpaceAfter { get; }

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

        /// <summary>The height of each character, in rows.</summary>
        public int Height { get; }
        
        /// <summary>The number of rows from the top of the font to the baseline, excluding descenders.</summary>
        /// <remarks>Must be less than or equal to <see cref="Height"/>.</remarks>
        public int Baseline { get; }
        
        /// <summary>The direction that text reads when rendered with this font.</summary>
        public FiggleTextDirection Direction { get; }

        internal FiggleFont(IReadOnlyList<FiggleCharacter> requiredCharacters, IReadOnlyDictionary<int, FiggleCharacter> sparseCharacters, char hardBlank, int height, int baseline, FiggleTextDirection direction)
        {
            _requiredCharacters = requiredCharacters;
            _sparseCharacters = sparseCharacters;
            _hardBlank = hardBlank;
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
        /// <param name="fitCharacters">Whether fitting should be applied. Defaults to <c>true</c>.</param>
        /// <returns></returns>
        public string Format(string message, bool fitCharacters = true)
        {
            var outputLines = Enumerable.Range(0, Height).Select(_ => new StringBuilder()).ToList();

            // TODO support smushing

            FiggleCharacter lastCh = null;

            foreach (var c in message)
            {
                var ch = GetCharacter(c);

                if (ch == null)
                    continue;

                var fitMove = fitCharacters ? CalculateFitMove(lastCh, ch) : 0;

                for (var row = 0; row < Height; row++)
                {
                    var charLine = ch.Lines[row];
                    var outputLine = outputLines[row];

                    if (fitCharacters && fitMove != 0)
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
                        outputLine.Append(toMove == 0 ? charLine.Content : charLine.Content.Substring(toMove));
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

            int CalculateFitMove(FiggleCharacter a, FiggleCharacter b)
            {
                if (a == null)
                    return 0; // TODO could still shift b if it had whitespace in the first column

                var minMove = int.MaxValue;

                for (var row = 0; row < Height; row++)
                {
                    var after  = a.Lines[row].SpaceAfter;
                    var before = b.Lines[row].SpaceBefore;

                    var move = after + before;
                    if (move < minMove)
                        minMove = move;
                }

                Debug.Assert(minMove >= 0, "minMove >= 0");

                return minMove;
            }
        }
    }
}