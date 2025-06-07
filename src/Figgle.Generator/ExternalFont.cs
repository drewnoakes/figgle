// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Generic;
using System.Text;

namespace Figgle.Generator;

internal sealed record ExternalFont(
    string FontName,
    string? FontDescriptionString);
