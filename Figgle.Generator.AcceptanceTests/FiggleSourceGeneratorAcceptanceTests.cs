using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System;
using Xunit;

namespace Figgle.Generator.AcceptanceTests;

[GenerateFiggleText("HelloWorld", "My External Font", "Hello World!")]
public partial class FiggleSourceGeneratorAcceptanceTests
{
    [Fact]
    public void HelloWorldTextWithExternalFontIsSourceGenerated_RenderedTextMatches()
    {
        string expectedText = """
            ██╗  ██╗███████╗██╗     ██╗      ██████╗     ██╗    ██╗ ██████╗ ██████╗ ██╗     ██████╗ ██╗
            ██║  ██║██╔════╝██║     ██║     ██╔═══██╗    ██║    ██║██╔═══██╗██╔══██╗██║     ██╔══██╗██║
            ███████║█████╗  ██║     ██║     ██║   ██║    ██║ █╗ ██║██║   ██║██████╔╝██║     ██║  ██║██║
            ██╔══██║██╔══╝  ██║     ██║     ██║   ██║    ██║███╗██║██║   ██║██╔══██╗██║     ██║  ██║╚═╝
            ██║  ██║███████╗███████╗███████╗╚██████╔╝    ╚███╔███╔╝╚██████╔╝██║  ██║███████╗██████╔╝██╗
            ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝ ╚═════╝      ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝
            """;

        Assert.Equal(expectedText, HelloWorld.Trim(), NewlineIgnoreComparer.Instance);
    }

    private sealed class NewlineIgnoreComparer : IEqualityComparer<string>
    {
        public static NewlineIgnoreComparer Instance { get; } = new();

        public bool Equals(string? x, string? y)
        {
            return StringComparer.Ordinal.Equals(Normalize(x), Normalize(y));
        }

        public int GetHashCode(string obj)
        {
            return StringComparer.Ordinal.GetHashCode(Normalize(obj));
        }

        [return: NotNullIfNotNull("s")]
        private static string? Normalize(string? s) => s?.Replace("\r", "");
    }
}
