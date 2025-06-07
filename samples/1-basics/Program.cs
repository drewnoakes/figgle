using Figgle;
using Figgle.Fonts;

while (true)
{
    Console.Write("Enter text to print: ");

    string? message = Console.ReadLine();

    if (message is null)
        break;

    FiggleFont font = FiggleFonts.Standard;

    Console.WriteLine(font.Render(message));
}
