```
 _____ _         _     
|   __|_|___ ___| |___ 
|   __| | . | . | | -_|
|__|  |_|_  |_  |_|___|
        |___|___|      
```

[![Figgle Build Status](https://github.com/drewnoakes/figgle/actions/workflows/CI.yml/badge.svg)](https://github.com/drewnoakes/figgle/actions/)
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

Use Figgle's source generator to embed just the fonts you want into your assembly, or&mdash;if the text to render is known ahead of time&mdash;render that text during compilation, so you don't need to ship Figgle binaries with your app.

## Installation

Figgle ships as NuGet packages that target .NET Standard 2.0, so runs almost everywhere.

| Project | Badges | Description |
| :------ | :----- | :---------- |
| [Figgle][figgle] | [![v][figgle-v]][figgle-nuget] [![dl][figgle-dl]][figgle-nuget] | The core library. Supports parsing font files and rendering text. |
| [Figgle.Fonts][fonts] | [![v][fonts-v]][fonts-nuget] [![dl][fonts-dl]][fonts-nuget] | A collection of 250+ [FIGlet](http://www.figlet.org/) fonts, for use with Figgle. |
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

## Sample apps

If you just want to see some code (it's not that complex) check out one of the following sample projects:

| Sample | Description |
| :----- | :---------- |
| [Basics](samples/1-basics) | The easiest option, if you don't care about application size or memory use. |
| [Static text generation](samples/2-static-text) | **For statically-known text**, have a source generator embed the rendered text directly into your assembly. Uses the `Figgle.Generator` package, and uses a single attribute to render the text at compile time. If all Figgle text is rendered this way, you don't have to ship any `Figgle` assembly with your app. |
| [Embed font from package](samples/3-embed-font-from-package) | **For dynamic text, using a font from the `Figgle.Fonts` package** via an attribute. The font is embedded directly into your assembly. With this approach, you only need the lightweight `Figgle` package at runtime. |
| [Embed font from `.flf` file](samples/4-embed-font-from-file) | **For dynamic text, using a `.flf` font file** via an attribute and `<AdditionalFiles>` project item in the `.csproj`. The font is embedded directly into your assembly. With this approach, you only need the lightweight `Figgle` package at runtime. |

## More output examples

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
