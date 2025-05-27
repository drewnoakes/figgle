// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.IO.Compression;
using Figgle.Fonts;
using Xunit;
using Xunit.Abstractions;

namespace Figgle.Tests;

public sealed class FiggleFontParserTest
{
    private readonly ITestOutputHelper _output;

    public FiggleFontParserTest(ITestOutputHelper output) => _output = output;

    [Fact]
    public void ParseAllEmbeddedFonts()
    {
        using var stream = EmbeddedFontResource.GetFontArchiveStream();

        Assert.NotNull(stream);

        using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        foreach (var entry in zip.Entries)
        {
            _output.WriteLine($"Parsing: {entry.Name}");

            using var entryStream = entry.Open();

            FiggleFontParser.Parse(entryStream);
        }
    }
}
