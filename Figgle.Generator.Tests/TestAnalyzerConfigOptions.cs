// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Figgle.Generator.Tests;

internal sealed class TestAnalyzerConfigOptions : AnalyzerConfigOptions
{
    public static readonly TestAnalyzerConfigOptions Empty
        = new(ImmutableDictionary<string, string>.Empty);

    private readonly ImmutableDictionary<string, string> _options;

    public TestAnalyzerConfigOptions(
        ImmutableDictionary<string, string> options)
    {
        _options = options;
    }

    public override IEnumerable<string> Keys => _options.Keys;

    public override bool TryGetValue(
        string key,
        [NotNullWhen(true)] out string? value)
    {
        return _options.TryGetValue(key, out value);
    }
}
