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
    Console.WriteLine($"Rendered with {nameof(MyFonts.Blocks)}");
    Console.WriteLine();
    Console.WriteLine(MyFonts.Blocks.Render(message));
    Console.WriteLine();
}

[EmbedFiggleFont(memberName: "ThreeDDiagonal", fontName: "3d_diagonal")]
[EmbedFiggleFont(memberName: "Blocks", fontName: "blocks")]
internal static partial class MyFonts
{
}
