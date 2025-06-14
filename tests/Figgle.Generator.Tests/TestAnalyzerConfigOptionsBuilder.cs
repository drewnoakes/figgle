// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.Collections.Immutable;

namespace Figgle.Generator.Tests;

public sealed class TestAnalyzerConfigOptionsBuilder
{
    private readonly ImmutableDictionary<string, string>.Builder _optionsBuilder
        = ImmutableDictionary.CreateBuilder<string, string>();

    public TestAnalyzerConfigOptionsBuilder AddOption(
        string key,
        string value)
    {
        _optionsBuilder.Add(key, value);
        return this;
    }

    public TestAnalyzerConfigOptions Build()
    {
        return new TestAnalyzerConfigOptions(
            _optionsBuilder.ToImmutable());
    }
}
