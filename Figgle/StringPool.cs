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

namespace Figgle
{
    /// <summary>
    /// An object pool for merging references to identical strings.
    /// </summary>
    /// <remarks>
    /// Unlike string interning (which is not available in <c>netstandard1.3</c> anyway)
    /// the pool may be released and garbage collected.
    /// </remarks>
    public sealed class StringPool
    {
        private readonly Dictionary<string, string> _pool = new Dictionary<string, string>(StringComparer.Ordinal);

        /// <summary>
        /// Returns a reference to a string equal to <paramref name="s"/> from the pool.
        /// If no such string exists within the pool, it is added, and <paramref name="s"/> is returned.
        /// </summary>
        /// <param name="s">The string to pool.</param>
        /// <returns>A reference to the pooled string.</returns>
        public string Pool(string s)
        {
            if (_pool.TryGetValue(s, out var pooled))
                return pooled;

            _pool[s] = s;
            return s;
        }
    }
}