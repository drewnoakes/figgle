# Embed font from package

> [!NOTE]
> If all text to be rendered is known statically at compile time, use the [static sample](../2-static-text) instead.

This sample shows how to embed a `FiggleFont` directly in your assembly, sourced from the `Figgle.Fonts` package. This package contains 250+ FIGlet fonts. Copying those you need into your assembly (via the `Figgle.Geneator` source generator) keeps deployed binary size down and reduces runtime memory requirements.

## Key points

### Project file changes

Add package reference to your `.csproj`:

```xml
<ItemGroup>
  <PackageReference Include="Figgle" Version="0.6.2" />
  <PackageReference Include="Figgle.Generator" Version="0.6.2" PrivateAssets="all" />
</ItemGroup>
```

> [!IMPORTANT]
> The `PrivateAssets="all"` attribute prevents your project from taking a dependency on `Figgle.Generator` and it's ~500KB dependency, `Figgle.Fonts`.

Add the font file to your project, as an `<AdditionalFiles>` item with the correct path in the `Include` attribute:

```xml
<ItemGroup>
  <AdditionalFiles Include="cosmic.flf" />
</ItemGroup>
```

### Source code

Use the `GenerateFiggleText` attribute to generate the text:

```xml
[EmbedFiggleFont(memberName: "ThreeDDiagonal", fontName: "3d_diagonal")]
internal static partial class MyFonts
{
}
```

Use the font to render some text:

```xml
Console.WriteLine(
    MyFonts.ThreeDDiagonal.Render("Hello, World!"));
```

Which prints:

```
                                                                                                               ,---,
        ,--,                                                                                                ,`--.' |
      ,--.'|            ,--,    ,--,                            .---.                     ,--,              |   :  :
   ,--,  | :          ,--.'|  ,--.'|                           /. ./|                   ,--.'|         ,---,'   '  ;
,---.'|  : '          |  | :  |  | :     ,---.             .--'.  ' ;   ,---.    __  ,-.|  | :       ,---.'||   |  |
|   | : _' |          :  : '  :  : '    '   ,'\           /__./ \ : |  '   ,'\ ,' ,'/ /|:  : '       |   | :'   :  ;
:   : |.'  |   ,---.  |  ' |  |  ' |   /   /   |      .--'.  '   \' . /   /   |'  | |' ||  ' |       |   | ||   |  '
|   ' '  ; :  /     \ '  | |  '  | |  .   ; ,. :     /___/ \ |    ' '.   ; ,. :|  |   ,''  | |     ,--.__| |'   :  |
'   |  .'. | /    /  ||  | :  |  | :  '   | |: :     ;   \  \;      :'   | |: :'  :  /  |  | :    /   ,'   |;   |  ;
|   | :  | '.    ' / |'  : |__'  : |__'   | .; :      \   ;  `      |'   | .; :|  | '   '  : |__ .   '  /  |`---'. |
'   : |  : ;'   ;   /||  | '.'|  | '.'|   :    |       .   \    .\  ;|   :    |;  : |   |  | '.'|'   ; |:  | `--..`;
|   | '  ,/ '   |  / |;  :    ;  :    ;\   \  /___      \   \   ' \ | \   \  / |  , ;   ;  :    ;|   | '/  '.--,_
;   : ;--'  |   :    ||  ,   /|  ,   /  `----'/  .\      :   '  |--"   `----'   ---'    |  ,   / |   :    :||    |`.
|   ,/       \   \  /  ---`-'  ---`-'         \_ ; |      \   \ ;                        ---`-'   \   \  /  `-- -`, ;
'---'         `----'                          /  ,"        '---"                                   `----'     '---`"
```