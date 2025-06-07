// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Figgle.Fonts.Generator;

[Generator(LanguageNames.CSharp)]
internal sealed class FiggleFontGenerator : IIncrementalGenerator
{
    private static readonly string[] _newLineCharacters = ["\r\n", "\n"];

    private static readonly DiagnosticDescriptor _errorParsingFontFileDiagnostic = new(
        "FIGGLE_IMPL001",
        "Font Parsing Error",
        "Failed to parse font '{0}'. Ensure it is a valid FIGlet font file.",
        "FiggleFonts",
        DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var fontMemberNamesProvider = context.AdditionalTextsProvider
            .Where(file => Path.GetFileName(file.Path).Equals("Aliases.csv", StringComparison.OrdinalIgnoreCase))
            .Select(static (file, cancellationToken) =>
            {
                var csvFileContent = file.GetText(cancellationToken)?.ToString();
                if (csvFileContent is null)
                {
                    return [];
                }

                var fontInfos = ImmutableArray.CreateBuilder<FontInfo>();
                var entries = csvFileContent.Split(_newLineCharacters, StringSplitOptions.RemoveEmptyEntries);
                foreach (var entry in entries)
                {
                    var components = entry.Split(',');
                    if (components.Length == 2)
                    {
                        var fontName = components[0].Trim();
                        var memberName = components[1].Trim();
                        fontInfos.Add(new FontInfo(fontName, memberName));
                    }
                }

                return fontInfos.ToImmutable();
            });

        var parsedFontsProvider = context.AdditionalTextsProvider
            .Where(file => file.Path.EndsWith(".flf", StringComparison.OrdinalIgnoreCase))
            .Select(static (file, cancellationToken) =>
            {
                var fontContent = file.GetText(cancellationToken)?.ToString();
                return new ParsedFont(
                    Path.GetFileNameWithoutExtension(file.Path),
                    fontContent is null ? null : FiggleFontParser.ParseString(fontContent));
            })
            .Collect();

        context.RegisterSourceOutput(fontMemberNamesProvider.Combine(parsedFontsProvider), (context, pair) =>
        {
            var fontInfos = pair.Left;
            var parsedFonts = pair.Right.ToImmutableDictionary(
                keySelector: f => f.Name,
                elementSelector: f => f.Font);

            foreach (var kvp in parsedFonts)
            {
                if (kvp.Value is null)
                {
                    context.ReportDiagnostic(Diagnostic.Create(
                        _errorParsingFontFileDiagnostic,
                        Location.None,
                        kvp.Key));
                }
            }

            var source = $$"""
                namespace Figgle.Fonts;

                partial class FiggleFonts
                {{{RenderFiggleFontProperties(fontInfos, parsedFonts)}}
                }
                """;

            context.AddSource("FiggleFonts.g.cs", source);

            static string RenderFiggleFontProperties(
                ImmutableArray<FontInfo> fontInfos,
                ImmutableDictionary<string, FiggleFont?> parsedFonts)
            {
                var builder = new StringBuilder(capacity: 4096);
                var indentation = new string(' ', 4);
                foreach (var fontInfo in fontInfos)
                {
                    builder.Append($$"""


                        {{indentation}}/// <summary>
                        {{indentation}}/// Obtains the <see cref="Figgle.FiggleFont" /> for the font name "{{fontInfo.FontName}}".
                        {{indentation}}/// <example>
                        {{indentation}}/// <code>
                        {{indentation}}{{RenderSampleText(fontInfo.FontName, parsedFonts[fontInfo.FontName], indentation)}}
                        {{indentation}}/// </code>
                        {{indentation}}/// </example>
                        {{indentation}}/// </summary>
                        {{indentation}}public static FiggleFont {{fontInfo.MemberName}} => GetByName("{{fontInfo.FontName}}");
                        """);
                }

                return builder.ToString();
            }

            static string RenderSampleText(string fontName, FiggleFont? font, string indentation)
            {
                if (font is null)
                {
                    return $"Failed to parse {fontName} into a {nameof(FiggleFont)}";
                }

                var renderedText = font.Render(fontName);

                return string.Join(
                    $"\r\n{indentation}",
                    renderedText
                        .Split(_newLineCharacters, StringSplitOptions.None)
                        .Select(line => $"/// {EscapeXmlSpecialCharacters(line)}"));
            }

            static string EscapeXmlSpecialCharacters(string text)
            {
                // SecurityElement conveniently has Escape that escapes XML special
                // characters, so we can just reuse it.
                return SecurityElement.Escape(text);
            }
        });
    }

    private sealed record FontInfo(string FontName, string MemberName);

    private sealed record ParsedFont(string Name, FiggleFont? Font);
}
