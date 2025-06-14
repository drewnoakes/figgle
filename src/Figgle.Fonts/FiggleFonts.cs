// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Concurrent;

namespace Figgle.Fonts;

/// <summary>
/// Collection of bundled fonts, ready for use.
/// </summary>
/// <remarks>
/// Fonts are lazily loaded upon property access. Only the fonts you use will be loaded.
/// <para />
/// Fonts are stored as an embedded ZIP archive within the assembly.
/// </remarks>
public static partial class FiggleFonts
{
    private static readonly ConcurrentDictionary<string, FiggleFont> _fontByName = new(StringComparer.Ordinal);
    private static readonly StringPool _stringPool = new();

    private static FiggleFont GetByName(string name)
    {
        return _fontByName.GetOrAdd(name, FontFactory);

        static FiggleFont FontFactory(string name)
        {
            var font = ParseEmbeddedFont(name);

            if (font is null)
                throw new FiggleException($"No embedded font exists with name \"{name}\".");

            return font;
        }
    }

    /// <summary>
    /// Attempts to load the font with specified name.
    /// </summary>
    /// <param name="name">the name of the font. Case-sensitive.</param>
    /// <returns>The font if found, otherwise <see langword="null"/>.</returns>
    public static FiggleFont? TryGetByName(string name)
    {
        if (_fontByName.TryGetValue(name, out var font))
            return font;

        font = ParseEmbeddedFont(name);

        if (font is not null)
            _fontByName.TryAdd(name, font);

        return font;
    }

    private static FiggleFont? ParseEmbeddedFont(string name)
    {
        var fontDescriptionString = EmbeddedFontResource.GetFontDescription(name);
        if (fontDescriptionString is null)
            return null;

        return FiggleFontParser.ParseString(fontDescriptionString, _stringPool);
    }
}
