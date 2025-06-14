// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;

namespace Figgle.Generator;

internal readonly record struct GenerationInfo<TAttributeInfo>(
    ITypeSymbol TargetType,
    HashSet<TAttributeInfo> AttributeInfos);
