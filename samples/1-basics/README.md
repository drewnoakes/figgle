# Basics

> [!NOTE]
> If performance or memory are important in your scenario, consider one of the [other samples](..). They are no more complex. You just have to chose which applies to you.

This sample shows the easiest way to use Figgle.

## Key points

Add package references:

```xml
<ItemGroup>
  <PackageReference Include="Figgle" Version="0.6.2" />
  <PackageReference Include="Figgle.Fonts" Version="0.6.2" />
</ItemGroup>
```

Use the `FiggleFonts` class to access the fonts:

```c#
FiggleFont font = FiggleFonts.Standard;

Console.WriteLine(font.Render(message));
```