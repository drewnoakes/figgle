using Figgle;

Console.WriteLine(MyAsciiBanners.Greeting);

[GenerateFiggleText(memberName: "Greeting", fontName: "blocks", sourceText: "Hello, World!")]
internal static partial class MyAsciiBanners
{
}
