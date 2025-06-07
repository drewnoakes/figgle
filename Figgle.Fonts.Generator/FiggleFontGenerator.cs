// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Immutable;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;

namespace Figgle.Fonts.Generator;

[Generator(LanguageNames.CSharp)]
public class FiggleFontGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var fontsProvider = context.AdditionalTextsProvider
            .Where(file => Path.GetFileName(file.Path).Equals("Aliases.csv", StringComparison.OrdinalIgnoreCase))
            .Select(static (file, cancellationToken) =>
            {
                var csvFileContent = file.GetText(cancellationToken)?.ToString();
                if (csvFileContent is null)
                {
                    return [];
                }

                var fontInfos = ImmutableArray.CreateBuilder<FontInfo>();
                var entries = csvFileContent.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries);
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

        context.RegisterSourceOutput(fontsProvider, (context, fontInfos) =>
        {
            var source = $$"""
                namespace Figgle.Fonts;

                static partial class FiggleFonts
                {
                {{RenderFiggleFontProperties(fontInfos)}}
                }
                """;

            context.AddSource("FiggleFonts.g.cs", source);

            static string RenderFiggleFontProperties(ImmutableArray<FontInfo> fontInfos)
            {
                var builder = new StringBuilder(capacity: 4096);
                var indentation = new string(' ', 4);
                foreach (var fontInfo in fontInfos)
                {
                    builder.AppendLine($$"""
                        {{indentation}}/// <summary>
                        {{indentation}}/// Gets the FIGlet font with the name "{{fontInfo.FontName}}".
                        {{indentation}}/// </summary>
                        {{indentation}}public static FiggleFont {{fontInfo.MemberName}} => GetByName("{{fontInfo.FontName}}");
                        """);
                }

                return builder.ToString();
            }
        });
    }

    private sealed record FontInfo(string FontName, string MemberName);
}
