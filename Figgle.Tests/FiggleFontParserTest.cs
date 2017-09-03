using System.IO.Compression;
using System.Reflection;
using Xunit;
using Xunit.Abstractions;

namespace Figgle.Tests
{
    public sealed class FiggleFontParserTest
    {
        private readonly ITestOutputHelper _output;

        public FiggleFontParserTest(ITestOutputHelper output) => _output = output;

        [Fact]
        public void ParseAllEmbeddedFonts()
        {
            using (var stream = typeof(FiggleFonts).GetTypeInfo().Assembly.GetManifestResourceStream("Figgle.Fonts.zip"))
            using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                foreach (var entry in zip.Entries)
                {
                    _output.WriteLine($"Parsing: {entry.Name}");

                    using (var entryStream = entry.Open())
                        FiggleFontParser.Parse(entryStream);
                }
            }
        }
    }
}