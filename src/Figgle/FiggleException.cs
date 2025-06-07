// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;

namespace Figgle;

/// <summary>
/// Type for exceptions raised by Figgle.
/// </summary>
/// <param name="message">A message explaining the exception.</param>
public sealed class FiggleException(string message) : Exception(message)
{
}
