```                       
 _____ _         _     
|   __|_|___ ___| |___ 
|   __| | . | . | | -_|
|__|  |_|_  |_  |_|___|
        |___|___|      
```

[![Figgle Build Status](https://github.com/drewnoakes/figgle/actions/workflows/CI.yml/badge.svg)](https://github.com/drewnoakes/figgle/actions/)
[![Figgle NuGet version](https://img.shields.io/nuget/v/Figgle)](https://www.nuget.org/packages/Figgle/)
[![Figgle NuGet download count](https://img.shields.io/nuget/dt/Figgle)](https://www.nuget.org/packages/Figgle/)

## ASCII banner generation for .NET

```c#
Console.WriteLine(
    FiggleFonts.Standard.Render("Hello, World!"));
```

Produces...

```
  _   _      _ _         __        __         _     _ _
 | | | | ___| | | ___    \ \      / /__  _ __| | __| | |
 | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |
 |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|
 |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)
                     |/
```

Alternatively, use Figgle's source generator to embed just the fonts you want into your assembly, or&mdash;if the text to render is known ahead of time&mdash;generate output during compilation, so you don't need to ship Figgle binaries with your app. See below for details.

The library bundles 265 [FIGlet](http://www.figlet.org/) [fonts](http://www.jave.de/figlet/fonts.html) in the [Figgle.Fonts](https://www.nuget.org/packages/Figgle.Fonts) NuGet package. You can add your own if that's not enough! 

## Installation

Figgle ships as NuGet packages that target .NET Standard 2.0, so run almost everywhere.

| Project | Badges | Description |
| :------ | :----- | :---------- |
| [Figgle][figgle] | [![v][figgle-v]][figgle-nuget] [![dl][figgle-dl]][figgle-nuget] | The core library. Supports parsing font files and rendering text. |
| [Figgle.Fonts][fonts] | [![v][fonts-v]][fonts-nuget] [![dl][fonts-dl]][fonts-nuget] | A collection of 250+ FIGlet fonts, for use with Figgle. |
| [Figgle.Generator][generator] | [![v][gen-v]][gen-nuget] [![dl][gen-dl]][gen-nuget] | A source generator to embedding fonts and render static text at compile-time. |

[figgle]: https://github.com/drewnoakes/figgle/tree/master/src/Figgle
[fonts]: https://github.com/drewnoakes/figgle/tree/master/src/Figgle.Fonts
[generator]: https://github.com/drewnoakes/figgle/tree/master/src/Figgle.Generator

[figgle-v]: https://img.shields.io/nuget/v/Figgle
[figgle-dl]: https://img.shields.io/nuget/dt/Figgle
[figgle-nuget]: https://www.nuget.org/packages/Figgle/

[fonts-v]: https://img.shields.io/nuget/v/Figgle.Fonts
[fonts-dl]: https://img.shields.io/nuget/dt/Figgle.Fonts
[fonts-nuget]: https://www.nuget.org/packages/Figgle.Fonts/

[gen-v]: https://img.shields.io/nuget/v/Figgle.Generator
[gen-dl]: https://img.shields.io/nuget/dt/Figgle.Generator
[gen-nuget]: https://www.nuget.org/packages/Figgle.Generator/

## Other samples

Using `FiggleFonts.Graffiti`:

```
  ___ ___         .__  .__               __      __            .__       .___._.
 /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |
/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |
\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|
 \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __
       \/       \/                   )/        \/                         \/  \/
```

Using `FiggleFonts.ThreePoint`:

```
|_| _ || _    \    / _  _| _||
| |(/_||(_),   \/\/ (_)| |(_|.
```

Using `FiggleFonts.Ogre`:

```
            _ _          __    __           _     _   _ 
  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \
 / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /
/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ 
\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   
                    |/                                  
```

Using `FiggleFonts.Rectangles`:

```
                                            __ 
 _____     _ _          _ _ _         _   _|  |
|  |  |___| | |___     | | | |___ ___| |_| |  |
|     | -_| | | . |_   | | | | . |  _| | . |__|
|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|
                  |_|                          
```

Using `FiggleFonts.Slant`:

```
    __  __     ____           _       __           __    ____
   / / / /__  / / /___       | |     / /___  _____/ /___/ / /
  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / 
 / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  
/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   
                     |/                                      
```

## Loading external fonts

Figgle ships with a bunch of fonts (in the `FiggleFonts` class). If you prefer, you can load your own.

Here's an example that loads a font from disk and renders some text with it:

```c#
using var fontStream = System.IO.File.OpenRead("myfont.flf");
var font = FiggleFontParser.Parse(fontStream);
var text = font.Render("Hello World, from My Font");
```

## Using the Source Generator

Figgle has two source generators: one renders static text at compile time, the other embeds the font into your assembly.

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

namespace MyNamespace;

[GenerateFiggleText("HelloWorldString", "blocks", "Hello world")]
internal partial class MyClass
{
}
```

By adding this attribute to a partial class, Figgle will automatically generate the corresponding
member in another part of the partial class, resembling:

```c#
namespace MyNamespace;

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
