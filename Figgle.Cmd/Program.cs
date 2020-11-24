// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using Figgle;

while (true)
{
    Console.Write("Message: ");

    var message = Console.ReadLine();

    try
    {
        Console.WriteLine(FiggleFonts.Standard.Render(message));
    }
    catch (Exception e)
    {
        Console.Error.WriteLine($"Exception: {e}");
    }
}
