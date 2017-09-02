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
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace Figgle
{
    internal sealed class FiggleCharacter
    {
        public IReadOnlyList<string> Lines { get; }
        public FiggleCharacter(IReadOnlyList<string> lines) => Lines = lines;
    }

    public sealed class FiggleFont
    {
        private static readonly Regex _firstLinePattern = new Regex(@"^flf2a(?<hardblank>.) (?<height>\d+) \d+ \d+ \d+ (?<commentlinecount>\d+)");

        private readonly IReadOnlyList<FiggleCharacter> _requiredCharacters;
        private readonly IReadOnlyDictionary<int, FiggleCharacter> _sparseCharacters;
        private readonly char _hardBlank;
        private readonly int _characterHeight;

        public FiggleFont(Stream stream, StringPool pool = null)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));
            
            // TODO allow specifying encoding
            var reader = new StreamReader(stream);

            var firstLine = reader.ReadLine();

            if (firstLine == null)
                throw new FiggleException("Font file is empty.");

            var match = _firstLinePattern.Match(firstLine);
            
            if (!match.Success)
                throw new FiggleException("Font file has invalid first line.");

            _hardBlank = match.Groups["hardblank"].Value[0];
            _characterHeight = int.Parse(match.Groups["height"].Value);
            var commentLineCount = int.Parse(match.Groups["commentlinecount"].Value);
            
            // skip comment lines
            for (var i = 0; i < commentLineCount; i++)
                reader.ReadLine();

            if (pool == null)
                pool = new StringPool();
            
            /*
            Characters 0-31 are control characters.
            
            Characters 32-126 appear in order:

            32 (blank/space) 64 @             96  `
            33 !             65 A             97  a
            34 "             66 B             98  b
            35 #             67 C             99  c
            36 $             68 D             100 d
            37 %             69 E             101 e
            38 &             70 F             102 f
            39 '             71 G             103 g
            40 (             72 H             104 h
            41 )             73 I             105 i
            42 *             74 J             106 j
            43 +             75 K             107 k
            44 ,             76 L             108 l
            45 -             77 M             109 m
            46 .             78 N             110 n
            47 /             79 O             111 o
            48 0             80 P             112 p
            49 1             81 Q             113 q
            50 2             82 R             114 r
            51 3             83 S             115 s
            52 4             84 T             116 t
            53 5             85 U             117 u
            54 6             86 V             118 v
            55 7             87 W             119 w
            56 8             88 X             120 x
            57 9             89 Y             121 y
            58 :             90 Z             122 z
            59 ;             91 [             123 {
            60 <             92 \             124 |
            61 =             93 ]             125 }
            62 >             94 ^             126 ~
            63 ?             95 _
        
            Then codes:
            
            196 Ä
            214 Ö
            220 Ü
            228 ä
            246 ö
            252 ü
            223 ß

            If some of these characters are not desired, empty characters may be used, having endmarks flush with the margin.
            
            After the required characters come "code tagged characters" in range -2147483648 to +2147483647, excluding -1. The assumed mapping is to ASCII/Latin-1/Unicode.
            
            A zero character is treated as the character to be used whenever a required character is missing. If no zero character is available, nothing will be printed.
            */

            FiggleCharacter ReadCharacter()
            {
                var lines = new string[_characterHeight];
                
                for (var i = 0; i < _characterHeight; i++)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        throw new FiggleException("Unexpected EOF in Font file.");
                    lines[i] = pool.Pool(line);
                }
                
                return new FiggleCharacter(lines);
            }

            var requiredCharacters = new FiggleCharacter[256];

            for (var i = 32; i < 127; i++)
                requiredCharacters[i] = ReadCharacter();

            requiredCharacters[196] = ReadCharacter();
            requiredCharacters[214] = ReadCharacter();
            requiredCharacters[220] = ReadCharacter();
            requiredCharacters[228] = ReadCharacter();
            requiredCharacters[246] = ReadCharacter();
            requiredCharacters[252] = ReadCharacter();
            requiredCharacters[223] = ReadCharacter();

            // support code-tagged characters
            var sparseCharacters = new Dictionary<int, FiggleCharacter>();

            while (true)
            {
                var line = reader.ReadLine();

                if (line == null)
                    break;

                if (!ParseUtil.TryParse(line, out int code))
                    throw new FiggleException($"Unsupported code-tagged character code string: {line}");
                
                if (code >= 0 && code < 256)
                    requiredCharacters[code] = ReadCharacter();
                else
                    sparseCharacters[code] = ReadCharacter();
            }

            _requiredCharacters = requiredCharacters;
            _sparseCharacters = sparseCharacters;
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

        public string Format(string message)
        {
            var res = new StringBuilder();
            
            // TODO support fitting and smushing
            
            for (var row = 0; row < _characterHeight; row++)
            {
                foreach (var c in message)
                {
                    var ch = GetCharacter(c);

                    if (ch == null)
                        continue;

                    var line = ch.Lines[row];
                    
                    var lastChar = line[line.Length - 1];

                    line = line.TrimEnd(lastChar).Replace(_hardBlank, ' ');
                    
                    res.Append(line);
                }
                
                res.AppendLine();
            }
            
            return res.ToString();
        }
    }
}