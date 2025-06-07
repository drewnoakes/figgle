using Figgle;

while (true)
{
    Console.Write("Enter text to print: ");

    string? message = Console.ReadLine();

    if (message is null)
        break;

    Console.WriteLine(MyFonts.Cosmic.Render(message));
}

[EmbedFiggleFont(memberName: "Cosmic", fontName: "cosmic")]
internal static partial class MyFonts
{
}
