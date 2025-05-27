// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Figgle.Generator.AcceptanceTests;

internal sealed class NewlineIgnoreComparer : IEqualityComparer<string>
{
    public static NewlineIgnoreComparer Instance { get; } = new();

    public bool Equals(string? x, string? y)
    {
        return StringComparer.Ordinal.Equals(Normalize(x), Normalize(y));
    }

    public int GetHashCode(string obj)
    {
        return StringComparer.Ordinal.GetHashCode(Normalize(obj));
    }

    [return: NotNullIfNotNull("s")]
    private static string? Normalize(string? s) => s?.Replace("\r", "");
}
