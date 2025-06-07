```
 _____ _         _       _____                     _           
|   __|_|___ ___| |___  |   __|___ ___ ___ ___ ___| |_ ___ ___ 
|   __| | . | . | | -_|_|  |  | -_|   | -_|  _| .'|  _| . |  _|
|__|  |_|_  |_  |_|___|_|_____|___|_|_|___|_| |__,|_| |___|_|  
        |___|___|                                              
```

[![GitHub project](https://img.shields.io/badge/GitHub-drewnoakes/figgle-blue?logo=github)](https://github.com/drewnoakes/figgle)
[![NuGet package](https://img.shields.io/badge/NuGet-Figgle.Generator-orange?logo=nuget)](https://www.nuget.org/packages/Figgle.Generator/)
[![Figgle NuGet download count](https://img.shields.io/nuget/dt/Figgle)](https://www.nuget.org/packages/Figgle/)

## Figgle's source generator

This package contains source generators that reduce the deployment footprint
and memory usage of applications that use Figgle.

Figgle has two source generators: one renders static text at compile time,
the other embeds the font into your assembly.

### Sample code

If you just want to see some code (it's not that complex) check out one of the following sample projects:

| Sample | Description |
| :----- | :---------- |
| [Static text generation](https://github.com/drewnoakes/figgle/tree/master/samples/2-static-text) | **For statically-known text**, have a source generator embed the rendered text directly into your assembly. Uses the `Figgle.Generator` package, and uses a single attribute to render the text at compile time. If all Figgle text is rendered this way, you don't have to ship any `Figgle` assembly with your app. |
| [Embed font from package](https://github.com/drewnoakes/figgle/tree/master/samples/3-embed-font-from-package) | **For dynamic text, using a font from the `Figgle.Fonts` package** via abn attribute. The font is embedded directly into your assembly. With this approach, you only need the lightweight `Figgle` package at runtime. |
| [Embed font from `.flf` file](https://github.com/drewnoakes/figgle/tree/master/samples/4-embed-font-from-file) | **For dynamic text, using a `.flf` font file** via an attribute and `<AdditionalFiles>` project item in the `.csproj`. The font is embedded directly into your assembly. With this approach, you only need the lightweight `Figgle` package at runtime. |

### Static text rendering

If the text you want Figgle to render is known at compile time, this section is for you.

Instead of having Figgle render text at runtime, you can use Figgle's _source generator_ to
do the rendering at compile time. This has two benefits:

- Faster runtime performance, as no Figgle code is executed
- Less memory consumption, as no Figgle code is loaded
- Smaller application footprint, as you don't need to ship Figgle binaries

In your code:

```c#
using Figgle;

[GenerateFiggleText("HelloWorldString", "blocks", "Hello world")]
internal partial class MyClass
{
}
```

By adding this attribute to a partial class, Figgle will automatically generate the corresponding
member in another part of the partial class, resembling:

```c#
partial class MyClass
{
    public static string HelloWorldString { get; } = "...rendered text here...";
}
```

If you want to use an external font, include the external font file as an additional file in your csproj:

```xml
<ItemGroup>
    <AdditionalFiles Include="myfont.flf" />
</ItemGroup>
```

Then specify the font's file name (excluding the extension) as the font name in the attribute:

```c#
[GenerateFiggleText("HelloWorldString", "myfont", "Hello world")]
```

Alternatively, you can specify a custom font name using the `FontName` item metadata:

```xml
<ItemGroup>
    <AdditionalFiles Include="myfont.flf" FontName="MyCustomFontName" />
</ItemGroup>
```

```C#
[GenerateFiggleText("HelloWorldString", "MyCustomFontName", "Hello world")]
internal partial class MyClass
{
}
```

Note the font name specified in the `GenerateFiggleText` attribute is case-insensitive so `mycustomfontname` works too.

### Embedding fonts

If you know the font you want to use to render, but the text to render is dynamic during runtime (e.g. user input),
you can use the source generator to embed the font into your assembly.

By embedding only the fonts you want into your assembly, you can avoid having to ship all of built-in figgle fonts with your application, reducing
the deploy size and memory usage of your application.

In your code:

```c#
using Figgle;

namespace MyNamespace;

[EmbedFiggleFont(memberName: "MyFont", fontName: "blocks")]
internal static partial class MyClass
{
}
```

This will cause the source generator to generate a static property of type `FiggleFont` in the `MyClass` type, which you can use to render text:

```c#
using Figgle;

namespace MyNamespace;

internal partial class MyClass
{
    public static FiggleFont MyFont { get; }
}
```

You can then use the font the same way you use a built-in font:

```c#
using Figgle;

namespace MyNamespace;

string text = MyClass.MyFont.Render("Hello, World!");

Console.WriteLine(text);
```

Similar to `GenerateFiggleText`, external fonts are also supported.  Include the external font file as an additional file in your csproj
the same way as before:

```xml
<ItemGroup>
    <AdditionalFiles Include="myfont.flf" FontName="MyExternalFont" />
</ItemGroup>
```

Then specify the font's font name as specified in `AdditionalFiles` 
(filename without the extension is also recognized if `FontName` property is not defined):

```c#
[EmbedFiggleFont(memberName: "MyFont", fontName: "MyExternalFont")]
```

## Source generator diagnostics

Rule ID | Category | Severity | Notes
--------|----------|----------|--------------------
FGL0001 | Figgle   |  Error   | The specified font name was not found.
FGL0002 | Figgle   |  Error   | The attribute specified an invalid member name.
FGL0003 | Figgle   |  Error   | The member specified by the attribute has already been declared.
FGL0004 | Figgle   |  Error   | The type must be `partial`.
FGL0005 | Figgle   |  Error   | Figgle generation does not support nested types.
FGL0006 | Figgle   |  Error   | There were errors when trying to read the external font.
FGL0007 | Figgle   |  Error   | Type must `static`.
