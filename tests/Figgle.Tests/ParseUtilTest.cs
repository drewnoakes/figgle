// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

namespace Figgle.Tests;

public sealed class ParseUtilTest
{
    [Theory]
    [InlineData("1234", 1234)]
    [InlineData("1234 ", 1234)]
    [InlineData("1234  ", 1234)]
    [InlineData("0X4D2", 1234)]
    [InlineData("0h4D2", 1234)]
    [InlineData("0x4d2", 1234)]
    [InlineData("0x4D2  ", 1234)]
    [InlineData("02322", 1234)]
    [InlineData("02322  ", 1234)]
    [InlineData("002322  ", 1234)]
    [InlineData("0002322  ", 1234)]
    [InlineData("-1234", -1234)]
    [InlineData("-1234 ", -1234)]
    [InlineData("-1234  ", -1234)]
    [InlineData("-0X4D2", -1234)]
    [InlineData("-0h4D2", -1234)]
    [InlineData("-0x4d2", -1234)]
    [InlineData("-0x4D2  ", -1234)]
    [InlineData("-02322", -1234)]
    [InlineData("-02322  ", -1234)]
    [InlineData("-002322  ", -1234)]
    [InlineData("-0002322  ", -1234)]
    [InlineData(" 1234", 1234)]
    [InlineData(" 1234 ", 1234)]
    [InlineData("  1234  ", 1234)]
    [InlineData(" 0X4D2", 1234)]
    [InlineData(" 0h4D2", 1234)]
    [InlineData(" 0x4d2", 1234)]
    [InlineData(" 0x4D2  ", 1234)]
    [InlineData(" 02322", 1234)]
    [InlineData(" 02322  ", 1234)]
    [InlineData(" 002322  ", 1234)]
    [InlineData(" 0002322  ", 1234)]
    [InlineData("0", 0)]
    [InlineData("00", 0)]
    [InlineData("000", 0)]
    [InlineData("0x0", 0)]
    [InlineData(" 0 ", 0)]
    [InlineData(" 00 ", 0)]
    [InlineData(" 000 ", 0)]
    [InlineData(" 0x0 ", 0)]
    [InlineData("-0", 0)]
    public void Parse_ValidInputs(string input, int expected)
    {
        var result = ParseUtil.TryParse(input, out var actual);
        Assert.True(result);
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("Hello")]
    [InlineData("0Hello")]
    [InlineData("0xx1234")]
    [InlineData("04D2")]
    [InlineData("4D2")]
    [InlineData("098LKJ")]
    [InlineData("0x")]
    [InlineData("0x ")]
    [InlineData(" 0x ")]
    [InlineData(" 0x0x ")]
    [InlineData(" 0x0x0 ")]
    [InlineData("- 123")]
    [InlineData("--123")]
    public void Parse_InvalidInputs(string input)
    {
        var result = ParseUtil.TryParse(input, out var actual);
        Assert.False(result);
        Assert.Equal(0, actual);
    }
}
