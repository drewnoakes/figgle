using Figgle;

while (true)
{
    Console.Write("Enter text to print: ");

    string? message = Console.ReadLine();

    if (message is null)
        break;

    Console.WriteLine($"Rendered with {nameof(MyFonts.ThreeDDiagonal)}");
    Console.WriteLine();
    Console.WriteLine(MyFonts.ThreeDDiagonal.Render(message));
    Console.WriteLine();
    Console.WriteLine($"Rendered with {nameof(MyFonts.Cosmic)}");
    Console.WriteLine();
    Console.WriteLine(MyFonts.Cosmic.Render(message));
    Console.WriteLine();
}

[EmbedFiggleFont(memberName: "ThreeDDiagonal", fontName: "3d_diagonal")]
[EmbedFiggleFont(memberName: "Cosmic", fontName: "cosmic")]
internal static partial class MyFonts
{
}
