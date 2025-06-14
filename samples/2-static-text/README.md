# Static text sample

> [!NOTE]
> If the text you wish to render isn't known at compile time, look at of the [other samples](..) for dynamic rendering.

This sample shows how to embed rendered text directly in your assembly.

## Key points

Add package reference to your `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Figgle.Generator" Version="0.6.2" PrivateAssets="all" />
</ItemGroup>
```

> [!IMPORTANT]
> The `PrivateAssets="all"` attribute prevents your project from taking a dependency on `Figgle` assemblies.

### Source code

Use the `GenerateFiggleText` attribute to generate the text:

```c#
[GenerateFiggleText(memberName: "Greeting", fontName: "cosmic", sourceText: "Hello, World!")]
internal static partial class MyAsciiBanners
{
}
```

Use the generated text:

```c#
Console.WriteLine(MyAsciiBanners.Greeting);
```

Which prints:

```
  ::   .: .,::::::   :::      :::         ...          .::    .   .:::  ...    :::::::..    :::   :::::::-.   .:
 ,;;   ;;,;;;;''''   ;;;      ;;;      .;;;;;;;.       ';;,  ;;  ;;;'.;;;;;;;. ;;;;``;;;;   ;;;    ;;,   `';,;;;
,[[[,,,[[[ [[cccc    [[[      [[[     ,[[     \[[,      '[[, [[, [[',[[     \[[,[[[,/[[['   [[[    `[[     [['[[
"$$$"""$$$ $$""""    $$'      $$'     $$$,     $$$        Y$c$$$c$P $$$,     $$$$$$$$$c     $$'     $$,    $$ $$
 888   "88o888oo,__ o88oo,.__o88oo,.__"888,_ _,88Pd8b      "88"888  "888,_ _,88P888b "88bo,o88oo,.__888_,o8P' ""
 MMM    YMM""""YUMMM""""YUMMM""""YUMMM  "YMMMMMP" ,M"       "M "M"    "YMMMMMP" MMMM   "W" """"YUMMMMMMMP"`   MM
```