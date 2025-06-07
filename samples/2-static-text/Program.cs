using Figgle;

Console.WriteLine(MyAsciiBanners.Greeting);

[GenerateFiggleText(memberName: "Greeting", fontName: "cosmic", sourceText: "Hello, World!")]
internal static partial class MyAsciiBanners
{
}
