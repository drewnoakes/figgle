// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Figgle.Generator;

internal static class IncrementalGeneratorExtensions
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

    public static IncrementalValueProvider<ImmutableDictionary<ISymbol, ImmutableHashSet<TAttributeInfo>>> ConsolidateAttributeInfosByTypeSymbol<TAttributeInfo>(
        this IncrementalValuesProvider<GenerationInfo<TAttributeInfo>> attributeInfosProvider,
        IEqualityComparer<TAttributeInfo>? comparer = null)
    {
        return attributeInfosProvider
            .Collect()
            .Select((infos, cancellationToken) =>
            {
                var typeToGenerateGroup = infos.GroupBy(
                keySelector: info => info.TargetType,
                elementSelector: info => info.AttributeInfos,
                comparer: SymbolEqualityComparer.Default);

                return typeToGenerateGroup.ToImmutableDictionary(
                    keySelector: group => group.Key!,
                    elementSelector: group => group.SelectMany(info => info).ToImmutableHashSet(comparer),
                    keyComparer: SymbolEqualityComparer.Default);
            });
    }

    public static IncrementalValuesProvider<GenerationInfo<TAttributeInfo>> ForFiggleAttributeWithMetadataName<TAttributeInfo>(
        this SyntaxValueProvider syntaxValueProvider,
        string fullyQualifiedMetadataName,
        Func<AttributeData, CancellationToken, TAttributeInfo> createAttributeInfo,
        IEqualityComparer<TAttributeInfo>? equalityComparer = null)
    {
        return syntaxValueProvider.ForAttributeWithMetadataName(
            fullyQualifiedMetadataName,
            predicate: static (syntaxNode, cancellationToken) => syntaxNode is ClassDeclarationSyntax,
            transform: (context, cancellationToken) =>
            {
                // use hash set to de-dup attributes that are identical.  If an attribute specifies
                // the same member name multiple times with different arguments, we will report a diagnostic
                // later in RegisterSourceOutput since we can't report diagnostics from here.
                var attributeInfos = new HashSet<TAttributeInfo>(
                    equalityComparer ?? EqualityComparer<TAttributeInfo>.Default);

                foreach (var matchingAttributeData in context.Attributes)
                {
                    attributeInfos.Add(createAttributeInfo(matchingAttributeData, cancellationToken));
                }

                return new GenerationInfo<TAttributeInfo>(
                    (ITypeSymbol)context.TargetSymbol,
                    attributeInfos);
            });
    }
}
