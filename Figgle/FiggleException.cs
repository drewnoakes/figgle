// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;

namespace Figgle;

/// <summary>
/// Type for exceptions raised by Figgle.
/// </summary>
public sealed class FiggleException : Exception
{
    /// <summary>
    /// Constructs a new Figgle exception.
    /// </summary>
    /// <param name="message">A message explaining the exception.</param>
    public FiggleException(string message) : base(message)
    {
    }
}
