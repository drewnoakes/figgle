// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.Linq;
using Microsoft.CodeAnalysis;
using Xunit;

namespace Figgle.Generator.Tests;

public class EmbedFontSourceGeneratorTests : SourceGeneratorTests
{
    protected override IIncrementalGenerator CreateIncrementalSourceGenerator()
    {
        return new EmbedFontSourceGenerator();
    }

    [Fact]
    public void InvalidFontName()
    {
        string source =
            """
            using Figgle;

            namespace Test.Namespace
            {
                [EmbedFiggleFont("Foo", "unknown-font")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(EmbedFontSourceGenerator.UnknownFontNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("A font with name 'unknown-font' was not found", diagnostic.GetMessage());
    }

    [Fact]
    public void InvalidMemberName()
    {
        string source =
            """
            using Figgle;

            namespace Test.Namespace
            {
                [EmbedFiggleFont("With Space", "stacey")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(EmbedFontSourceGenerator.InvalidMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("The string 'With Space' is not a valid member name", diagnostic.GetMessage());
    }

    [Fact]
    public void DuplicateIdenticalAttributeArguments_NoDiagnostic()
    {
        string source =
            """
            using Figgle;

            namespace Test.Namespace
            {
                [EmbedFiggleFont("Foo", "stacey")]
                [EmbedFiggleFont("Foo", "stacey")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (compilation, diagnostics) = RunGenerator(source);

        Assert.Empty(diagnostics);

        string generatedCode = compilation.SyntaxTrees.Last().ToString();
        string expectedFiggleFontProperty
            = "public static FiggleFont Foo => _fontByName.GetOrAdd(\"Foo\", _ => FiggleFontParser.ParseString(FooFontDescription, _stringPool));";
        string expectedFontDescription
            = "private static readonly string FooFontDescription = @\"";

        Assert.Contains(expectedFiggleFontProperty, generatedCode);
        Assert.Contains(expectedFontDescription, generatedCode);
    }

    [Fact]
    public void DuplicateMemberNameWithDifferentFontName_DuplicateMemberDiagnostic()
    {
        string source =
            """
            using Figgle;

            namespace Test.Namespace
            {
                [EmbedFiggleFont("Foo", "stacey")]
                [EmbedFiggleFont("Foo", "3d_diagonal")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var (compilation, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(EmbedFontSourceGenerator.DuplicateMemberNameDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Member 'Foo' has already been declared", diagnostic.GetMessage());
    }

    [Fact]
    public void TypeIsNotPartial()
    {
        string source =
            """
            using Figgle;

            namespace Test.Namespace
            {
                [EmbedFiggleFont("Foo", "stacey")]
                internal static class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(EmbedFontSourceGenerator.TypeIsNotPartialDiagnostic, diagnostic.Descriptor);
        Assert.Equal("Type 'Test.Namespace.DemoUsage' must be partial", diagnostic.GetMessage());
    }

    [Fact]
    public void TypeIsNotStatic()
    {
        string source =
            """
            using Figgle;

            namespace Test.Namespace
            {
                [EmbedFiggleFont("Foo", "stacey")]
                internal partial class DemoUsage
                {
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(EmbedFontSourceGenerator.TypeMustBeStaticDiagnostic, diagnostic.Descriptor);
        Assert.Equal("The type 'Test.Namespace.DemoUsage' must be a static class", diagnostic.GetMessage());
    }

    [Fact]
    public void NestedTypeIsNotSupported()
    {
        string source =
            """
            using Figgle;

            namespace Test.Namespace
            {
                internal static partial class Outer
                {
                    [EmbedFiggleFont("Foo", "stacey")]
                    internal static partial class Inner
                    {
                    }
                }
            }
            """;

        var (_, diagnostics) = RunGenerator(source);

        var diagnostic = Assert.Single(diagnostics);

        Assert.Same(EmbedFontSourceGenerator.NestedTypeIsNotSupportedDiagnostic, diagnostic.Descriptor);
        Assert.Equal(
            "Unable to generate Figgle text for nested type 'Test.Namespace.Outer.Inner'. Generation is only supported for non-nested types.",
            diagnostic.GetMessage());
    }

    [Fact]
    public void ExternalFontInAdditionalFiles_EmbedsFont()
    {
        string source =
            """
            using Figgle;

            namespace Test.Namespace
            {
                [EmbedFiggleFont("Foo", "ANSI Shadow")]
                internal static partial class DemoUsage
                {
                }
            }
            """;

        var additionalFont = ExternalFontAdditionalText.Create("ANSI Shadow.flf");
        var optionsProvider = CreateOptionsProvider("ANSI Shadow.flf", "ANSI Shadow");

        var (compilation, diagnostics) = RunGenerator(source, [additionalFont], optionsProvider);
        Assert.Empty(diagnostics);

        string generatedCode = compilation.SyntaxTrees.Last().ToString();
        Assert.Contains(
            "namespace Test.Namespace",
            generatedCode);
        Assert.Contains(
            "public static FiggleFont Foo => _fontByName.GetOrAdd(\"Foo\", _ => FiggleFontParser.ParseString(FooFontDescription, _stringPool));",
            generatedCode);
        Assert.Contains(
            "private static readonly string FooFontDescription = @\"",
            generatedCode);
    }

    [Fact]
    public void ExternalFontInAdditionalFiles_NoNamespace_EmbedsFont()
    {
        string source =
            """
            using Figgle;

            [EmbedFiggleFont("Foo", "ANSI Shadow")]
            internal static partial class DemoUsage
            {
            }
            """;

        var additionalFont = ExternalFontAdditionalText.Create("ANSI Shadow.flf");
        var optionsProvider = CreateOptionsProvider("ANSI Shadow.flf", "ANSI Shadow");

        var (compilation, diagnostics) = RunGenerator(source, [additionalFont], optionsProvider);
        Assert.Empty(diagnostics);

        string generatedCode = compilation.SyntaxTrees.Last().ToString();
        Assert.DoesNotContain(
            "namespace",
            generatedCode);
        Assert.Contains(
            "public static FiggleFont Foo => _fontByName.GetOrAdd(\"Foo\", _ => FiggleFontParser.ParseString(FooFontDescription, _stringPool));",
            generatedCode);
        Assert.Contains(
            "private static readonly string FooFontDescription = @\"",
            generatedCode);
    }
}
