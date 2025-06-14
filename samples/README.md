# Figgle Samples

This `samples` folder contains tiny console applications that demonstate
the ways in which you can use Figgle in your own applications.

> [!IMPORTANT]
> For each, be sure to check both:
> 
> - **The `Program.cs` file**, for the C# code.
> - **The `csproj` file**, for `<PackageReference>` item(s) and any additional configuration.

Check this table 

| Sample | Description |
| :----- | :---------- |
| [Basics](1-basics) | The easiest option, if you don't care about application size or memory use. |
| [Static text generation](2-static-text) | **For statically-known text**, have a source generator embed the rendered text directly into your assembly. Uses the `Figgle.Generator` package, and uses a single attribute to render the text at compile time. If all Figgle text is rendered this way, you don't have to ship any `Figgle` assembly with your app. |
| [Embed font from package](3-embed-font-from-package) | **For dynamic text, using a font from the `Figgle.Fonts` package** via an attribute. The font is embedded directly into your assembly. With this approach, you only need the lightweight `Figgle` package at runtime. |
| [Embed font from `.flf` file](4-embed-font-from-file) | **For dynamic text, using a `.flf` font file** via an attribute and `<AdditionalFiles>` project item in the `.csproj`. The font is embedded directly into your assembly. With this approach, you only need the lightweight `Figgle` package at runtime. |
