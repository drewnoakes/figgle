```
 _____ _         _     
|   __|_|___ ___| |___ 
|   __| | . | . | | -_|
|__|  |_|_  |_  |_|___|
        |___|___|      
```

# Source

This folder contains the source code for all of Figgle's NuGet packages.

| Project | Badges | Description |
| :------ | :----- | :---------- |
| [Figgle][figgle] | [![v][figgle-v]][figgle-nuget] [![dl][figgle-dl]][figgle-nuget] | The core library. Supports parsing font files and rendering text. |
| [Figgle.Fonts][fonts] | [![v][fonts-v]][fonts-nuget] [![dl][fonts-dl]][fonts-nuget] | A collection of 250+ FIGlet fonts, for use with Figgle. |
| [Figgle.Generator][generator] | [![v][gen-v]][gen-nuget] [![dl][gen-dl]][gen-nuget] | A source generator to embedding fonts and render static text at compile-time. |

## History

Figgle was created in 2017 as a way to render ASCII text banners, for use in .NET applications.
It has evolved to include a variety of features, including support for rendering statically-known
text at compile time, and embedding fonts into an assembly via source generator.

- `Figgle.Fonts` was introduced in version 0.6, where it was extracted from the `Figgle` package to allow leaner distribution, by embedding fonts with `Figgle.Generator`.
- `Figgle.Generator` was introduced in version 0.5, allowing compile-time rendering of text. The ability to embed fonts was added in 0.6, along with the introduction of a separate `Figgle.Fonts` package.
- `Figgle` was updated to require `netstandard2.0` in 0.4.2. This runs pretty much everywhere these days. If you require `net452;netstandard1.3`, use 0.4.1.

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

## See also

- [samples](../samples) &mdash; Sample applications demonstrating Figgle usage.
- [tests](../tests) &mdash; Automated tests for Figgle.
- [fonts](../fonts) &mdash; A collection of FIGlet font files.
- [images](../images) &mdash; Project logo and other media.
