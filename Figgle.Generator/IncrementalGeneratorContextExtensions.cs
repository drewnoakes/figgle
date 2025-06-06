// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Immutable;
using System.IO;
using Microsoft.CodeAnalysis;

namespace Figgle.Generator;

internal static class IncrementalGeneratorContextExtensions
{
    public static IncrementalValueProvider<ImmutableArray<ExternalFont>> GetExternalFontsProvider(
        this IncrementalGeneratorInitializationContext context)
    {
        return context.AdditionalTextsProvider
            .Combine(context.AnalyzerConfigOptionsProvider)
            .Where(pair => pair.Left.Path.EndsWith(".flf", StringComparison.OrdinalIgnoreCase))
            .Select(static (pair, cancellationToken) =>
            {
                var (additionalFile, optionsProvider) = pair;
                var additionalFileOptions = optionsProvider.GetOptions(additionalFile);

                // a "build_metadata" prefix is added by msbuild for CompilerVisibleItemMetadata
                additionalFileOptions.TryGetValue("build_metadata.AdditionalFiles.FontName", out var fontNameValue);
                var fontName = fontNameValue ?? Path.GetFileNameWithoutExtension(additionalFile.Path);

                return new ExternalFont(
                    fontName,
                    // GetText returns null if there are errors reading the file.
                    additionalFile.GetText(cancellationToken)?.ToString());
            })
            .Collect();
    }
}
