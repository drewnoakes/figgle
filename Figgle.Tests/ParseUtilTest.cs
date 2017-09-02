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
    public class ParseUtilTest
    {
        [Fact]
        public void Parse()
        {
            void Test(string s, int expected)
            {
                Assert.True(ParseUtil.TryParse(s, out var actual));
                Assert.Equal(expected, actual);
            }
            
            void TestFails(string s) => Assert.False(ParseUtil.TryParse(s, out var _));

            Test("1234", 1234);
            Test("1234 ", 1234);
            Test("1234  ", 1234);
            Test("0X4D2", 1234);
            Test("0h4D2", 1234);
            Test("0x4d2", 1234);
            Test("0x4D2  ", 1234);
            Test("02322", 1234);
            Test("02322  ", 1234);
            Test("002322  ", 1234);
            Test("0002322  ", 1234);

            Test(" 1234", 1234);
            Test(" 1234 ", 1234);
            Test("  1234  ", 1234);
            Test(" 0X4D2", 1234);
            Test(" 0h4D2", 1234);
            Test(" 0x4d2", 1234);
            Test(" 0x4D2  ", 1234);
            Test(" 02322", 1234);
            Test(" 02322  ", 1234);
            Test(" 002322  ", 1234);
            Test(" 0002322  ", 1234);

            TestFails("Hello");
            TestFails("0Hello");
            TestFails("0xx1234");
            TestFails("04D2");
            TestFails("4D2");
            TestFails("098LKJ");
            TestFails("0x");
            TestFails("0x ");
            TestFails(" 0x ");
        }
    }
}
