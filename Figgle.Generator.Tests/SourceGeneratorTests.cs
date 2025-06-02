// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;
using Xunit;
using Xunit.Sdk;

namespace Figgle.Generator.Tests;

public abstract class SourceGeneratorTests
{
    private readonly ImmutableArray<MetadataReference> _references;

    public SourceGeneratorTests()
    {
        _references = GetReferences().ToImmutableArray();

        static IEnumerable<MetadataReference> GetReferences()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!assembly.IsDynamic && !string.IsNullOrWhiteSpace(assembly.Location))
                {
                    yield return MetadataReference.CreateFromFile(assembly.Location);
                }
            }
        }
    }

    protected abstract ISourceGenerator CreateSourceGenerator();

    protected void ValidateOutput(string source, params string[] outputs)
    {
        ValidateOutput(
            source,
            ImmutableArray<ExternalFontAdditionalText>.Empty,
            optionsProvider: null,
            outputs);
    }

    protected void ValidateOutput(
        string source,
        ImmutableArray<ExternalFontAdditionalText> additionalFonts,
        TestAnalyzerConfigOptionsProvider? optionsProvider,
        params string[] outputs)
    {
        var (compilation, diagnostics) = RunGenerator(source, additionalFonts, optionsProvider);

        ValidateNoErrors(diagnostics);

        Assert.Equal(
            new[] { source, RenderTextSourceGenerator.AttributeSource }.Concat(outputs),
            compilation.SyntaxTrees.Select(tree => tree.ToString()),
            NewlineIgnoreComparer.Instance);
    }

    protected (Compilation Compilation, ImmutableArray<Diagnostic> Diagnostics) RunGenerator(
        string source,
        ImmutableArray<ExternalFontAdditionalText>? additionalFonts = null,
        TestAnalyzerConfigOptionsProvider? optionsProvider = null)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        var compilation = CSharpCompilation.Create(
            "testAssembly",
            [syntaxTree],
            _references,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        ISourceGenerator generator = CreateSourceGenerator();

        var driver = CSharpGeneratorDriver.Create(
            [generator],
            additionalTexts: additionalFonts,
            optionsProvider: optionsProvider);

        driver.RunGeneratorsAndUpdateCompilation(
            compilation,
            out var outputCompilation,
            out var generateDiagnostics);

        return (outputCompilation, generateDiagnostics);
    }

    protected static TestAnalyzerConfigOptionsProvider CreateOptionsProvider(
        string fontFileName,
        string fontNameProperty)
    {
        var additionalFontOption = new TestAnalyzerConfigOptionsBuilder()
            .AddOption("build_metadata.AdditionalFiles.FontName", fontNameProperty)
            .Build();

        return new TestAnalyzerConfigOptionsProvider(
            getAdditionalFileOptions: f => f.Path == fontFileName
                ? additionalFontOption
                : null);
    }

    private static void ValidateNoErrors(ImmutableArray<Diagnostic> diagnostics)
    {
        var errors = diagnostics.Where(d => d.Severity == DiagnosticSeverity.Error).ToList();

        if (errors.Any())
        {
            throw new XunitException(
                string.Join(
                    Environment.NewLine,
                    errors.Select(error => error.GetMessage())));
        }
    }

    protected sealed class NewlineIgnoreComparer : IEqualityComparer<string>
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

    protected sealed class ExternalFontAdditionalText : AdditionalText
    {
        private readonly SourceText _sourceText;

        public static ExternalFontAdditionalText Create(string externalFontPath)
        {
            using var fontStream = File.OpenRead(externalFontPath);

            return new ExternalFontAdditionalText(externalFontPath, fontStream);
        }

        public ExternalFontAdditionalText(string path, Stream externalFont)
        {
            Path = path;
            _sourceText = SourceText.From(externalFont);
        }

        public override string Path { get; }

        public override SourceText? GetText(CancellationToken cancellationToken = default)
        {
            return _sourceText;
        }
    }
}
