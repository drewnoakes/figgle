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

using Xunit;

namespace Figgle.Tests
{
    public class StringPoolTest
    {
        [Fact]
        public void PoolsReferences()
        {
            var pool = new StringPool();

            var s1 = "s";
            var s2 = "S".ToLower();

            Assert.NotSame(s1, s2);
            Assert.Equal(s1, s2);

            Assert.Same(s1, pool.Pool(s1));
            Assert.Same(s1, pool.Pool(s1));
            Assert.Same(s1, pool.Pool(s2));
            Assert.Same(s1, pool.Pool(s2));
        }
    }
}
