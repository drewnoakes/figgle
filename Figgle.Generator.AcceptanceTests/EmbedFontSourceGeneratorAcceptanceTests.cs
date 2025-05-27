// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Xunit;

namespace Figgle.Generator.AcceptanceTests;

public class EmbedFontSourceGeneratorAcceptanceTests
{
    [Fact]
    public void EmbedThreeDDiagonalFiggleFont_UseGeneratedFiggleFontToRenderText_TextMatches()
    {
        string expectedText = """
                                                                                                                                                    ,---,  
                                                                                                                                                 ,`--.' |  
                ,---,.          ____                                                                      ,---,.                          ___    |   :  :  
              ,'  .' |        ,'  , `.  ,---,                   ,---,      ,---,                ,---,   ,'  .' |                        ,--.'|_  '   '  ;  
            ,---.'   |     ,-+-,.' _ |,---.'|                 ,---.'|    ,---.'|              ,---.'| ,---.'   |   ,---.        ,---,   |  | :,' |   |  |  
            |   |   .'  ,-+-. ;   , |||   | :                 |   | :    |   | :              |   | : |   |   .'  '   ,'\   ,-+-. /  |  :  : ' : '   :  ;  
            :   :  |-, ,--.'|'   |  ||:   : :      ,---.      |   | |    |   | |   ,---.      |   | | :   :  :   /   /   | ,--.'|'   |.;__,'  /  |   |  '  
            :   |  ;/||   |  ,', |  |,:     |,-.  /     \   ,--.__| |  ,--.__| |  /     \   ,--.__| | :   |  |-,.   ; ,. :|   |  ,"' ||  |   |   '   :  |  
            |   :   .'|   | /  | |--' |   : '  | /    /  | /   ,'   | /   ,'   | /    /  | /   ,'   | |   :  ;/|'   | |: :|   | /  | |:__,'| :   ;   |  ;  
            |   |  |-,|   : |  | ,    |   |  / :.    ' / |.   '  /  |.   '  /  |.    ' / |.   '  /  | |   |   .''   | .; :|   | |  | |  '  : |__ `---'. |  
            '   :  ;/||   : |  |/     '   : |: |'   ;   /|'   ; |:  |'   ; |:  |'   ;   /|'   ; |:  | '   :  '  |   :    ||   | |  |/   |  | '.'| `--..`;  
            |   |    \|   | |`-'      |   | '/ :'   |  / ||   | '/  '|   | '/  ''   |  / ||   | '/  ' |   |  |   \   \  / |   | |--'    ;  :    ;.--,_     
            |   :   .'|   ;/          |   :    ||   :    ||   :    :||   :    :||   :    ||   :    :| |   :  \    `----'  |   |/        |  ,   / |    |`.  
            |   | ,'  '---'           /    \  /  \   \  /  \   \  /   \   \  /   \   \  /  \   \  /   |   | ,'            '---'          ---`-'  `-- -`, ; 
            `----'                    `-'----'    `----'    `----'     `----'     `----'    `----'    `----'                                       '---`"  
            """;

        var renderedFont = FiggleFonts.ThreeDDiagonal.Render("Embedded Font!");
        Assert.Equal(expectedText.Trim(), renderedFont.Trim(), NewlineIgnoreComparer.Instance);
    }

    [Fact]
    public void EmbedExternalFiggleFont_UseGeneratedFiggleFontToRenderText_TextMatches()
    {
        string expectedText = """
            ███████╗███╗   ███╗██████╗ ███████╗██████╗ ██████╗ ███████╗██████╗     ███████╗ ██████╗ ███╗   ██╗████████╗██╗
            ██╔════╝████╗ ████║██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔════╝██╔══██╗    ██╔════╝██╔═══██╗████╗  ██║╚══██╔══╝██║
            █████╗  ██╔████╔██║██████╔╝█████╗  ██║  ██║██║  ██║█████╗  ██║  ██║    █████╗  ██║   ██║██╔██╗ ██║   ██║   ██║
            ██╔══╝  ██║╚██╔╝██║██╔══██╗██╔══╝  ██║  ██║██║  ██║██╔══╝  ██║  ██║    ██╔══╝  ██║   ██║██║╚██╗██║   ██║   ╚═╝
            ███████╗██║ ╚═╝ ██║██████╔╝███████╗██████╔╝██████╔╝███████╗██████╔╝    ██║     ╚██████╔╝██║ ╚████║   ██║   ██╗
            ╚══════╝╚═╝     ╚═╝╚═════╝ ╚══════╝╚═════╝ ╚═════╝ ╚══════╝╚═════╝     ╚═╝      ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚═╝
            """;

        var renderedFont = FiggleFonts.ANSIShadow.Render("Embedded Font!");
        Assert.Equal(expectedText.Trim(), renderedFont.Trim(), NewlineIgnoreComparer.Instance);
    }
}

[EmbedFiggleFont("ThreeDDiagonal", "3d_diagonal")]
[EmbedFiggleFont("ANSIShadow", "My External Font")]
internal static partial class FiggleFonts
{
}
