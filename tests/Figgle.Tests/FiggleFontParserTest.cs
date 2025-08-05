// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.IO.Compression;
using Figgle.Fonts;
using Xunit.Abstractions;

namespace Figgle.Tests;

public sealed class FiggleFontParserTest(ITestOutputHelper output)
{
    [Fact]
    public void ParseAllEmbeddedFonts()
    {
        using var stream = EmbeddedFontResource.GetFontArchiveStream();

        using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        foreach (var entry in zip.Entries)
        {
            output.WriteLine($"Parsing: {entry.Name}");

            using var entryStream = entry.Open();

            FiggleFontParser.Parse(entryStream);
        }
    }
}
