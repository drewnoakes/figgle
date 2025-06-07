```
 _____ _         _       _____         _       
|   __|_|___ ___| |___  |   __|___ ___| |_ ___ 
|   __| | . | . | | -_|_|   __| . |   |  _|_ -|
|__|  |_|_  |_  |_|___|_|__|  |___|_|_|_| |___|
        |___|___|                              
```

[![GitHub project](https://img.shields.io/badge/GitHub-drewnoakes/figgle-blue?logo=github)](https://github.com/drewnoakes/figgle)
[![NuGet package](https://img.shields.io/badge/NuGet-Figgle.Fonts-orange?logo=nuget)](https://www.nuget.org/packages/Figgle.Fonts/)
[![Figgle NuGet download count](https://img.shields.io/nuget/dt/Figgle)](https://www.nuget.org/packages/Figgle/)

## Mega font bundle

This package bundles 250+ [FIGlet](http://www.figlet.org/) fonts, for use
with the [Figgle](https://github.com/drewnoakes/figgle) package.

## Using this package

Due to the large number of fonts included in this package, you should consider
using the `Figgle.Generator` package to embed fonts or generated text, to reduce
memory usage and application footprint.

## Example usage

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

Or with a source generator, for static text ([sample project](https://github.com/drewnoakes/figgle/tree/master/samples/2-static-text)):

```c#
Console.WriteLine(MyAsciiBanners.Greeting);

[GenerateFiggleText(memberName: "Greeting", fontName: "cosmic", sourceText: "Hello, World!")]
internal static partial class MyAsciiBanners
{
    // The source generator adds the Greeting property.
}
```

Or with a source generator, for dynamic text ([sample project](https://github.com/drewnoakes/figgle/tree/master/samples/3-embed-font-from-package)):

```c#
Console.WriteLine(MyFonts.CosmicFont.Render("Hello, World!"));

[EmbedFiggleFont(memberName: "CosmicFont", fontName: "cosmic")]
internal static partial class MyFonts
{
    // The source generator adds the CosmicFont property.
}
```

---

## A few fonts

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


