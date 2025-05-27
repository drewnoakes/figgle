// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using Figgle.Fonts;

while (true)
{
    Console.Write("Message: ");

    var message = Console.ReadLine();

    if (message is null)
        break;

    try
    {
        Console.WriteLine(FiggleFonts.Standard.Render(message));
    }
    catch (Exception e)
    {
        Console.Error.WriteLine($"Exception: {e}");
    }
}
