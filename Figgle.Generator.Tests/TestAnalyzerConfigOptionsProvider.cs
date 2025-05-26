// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Figgle.Generator.Tests;

internal sealed class TestAnalyzerConfigOptionsProvider
    : AnalyzerConfigOptionsProvider
{
    private readonly Func<SyntaxTree, AnalyzerConfigOptions?> _getSyntaxTreeOptions;
    private readonly Func<AdditionalText, AnalyzerConfigOptions?> _getAdditionalFileOptions;

    public TestAnalyzerConfigOptionsProvider(
        AnalyzerConfigOptions? globalOptions = null,
        Func<SyntaxTree, AnalyzerConfigOptions?>? getSyntaxTreeOptions = null,
        Func<AdditionalText, AnalyzerConfigOptions?>? getAdditionalFileOptions = null)
    {
        GlobalOptions = globalOptions
            ?? TestAnalyzerConfigOptions.Empty;

        _getSyntaxTreeOptions = getSyntaxTreeOptions
            ?? (_ => TestAnalyzerConfigOptions.Empty);

        _getAdditionalFileOptions = getAdditionalFileOptions
            ?? (_ => TestAnalyzerConfigOptions.Empty);
    }

    public override AnalyzerConfigOptions GlobalOptions { get; }

    public override AnalyzerConfigOptions GetOptions(SyntaxTree tree)
    {
        return _getSyntaxTreeOptions(tree)
            ?? TestAnalyzerConfigOptions.Empty;
    }

    public override AnalyzerConfigOptions GetOptions(AdditionalText textFile)
    {
        return _getAdditionalFileOptions(textFile)
            ?? TestAnalyzerConfigOptions.Empty;
    }
}
