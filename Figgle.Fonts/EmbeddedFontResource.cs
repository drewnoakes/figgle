// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;

namespace Figgle.Fonts;

/// <summary>
/// The resource for all of the embedded FIGlet font files.
/// </summary>
internal static class EmbeddedFontResource
{
    /// <summary>
    /// Gets the description of a FIGlet font by its name.
    /// </summary>
    /// <param name="fontName">the font's name</param>
    /// <returns>the font description string if font name is found, otherwise null</returns>
    /// <exception cref="InvalidOperationException">The stream contained an error and could not be parsed.</exception>
    public static string? GetFontDescription(string fontName)
    {
        using var stream = GetFontArchiveStream();

        if (stream is null)
            throw new FiggleException("Unable to open embedded font archive.");

        using var zip = new ZipArchive(stream, ZipArchiveMode.Read);

        var entry = zip.GetEntry(fontName + ".flf");

        if (entry == null)
            return null;

        using var entryStream = entry.Open();
        return new StreamReader(entryStream).ReadToEnd();
    }

    internal static Stream GetFontArchiveStream()
    {
        return Assembly.GetAssembly(typeof(EmbeddedFontResource))
            .GetManifestResourceStream("Figgle.Fonts.Fonts.zip");
    }
}
