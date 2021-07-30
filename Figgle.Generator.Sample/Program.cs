// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;

namespace Figgle.Generator.Sample
{
    [GenerateFiggleText("BannerText", "contessa", "Figgle!")]
    internal static partial class Program
    {
        private static void Main()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(BannerText);
            Console.ResetColor();
        }
    }
}
