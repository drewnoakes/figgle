// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using Xunit;

namespace Figgle.Generator.AcceptanceTests;

[GenerateFiggleText("HelloWorld", "My External Font", "Hello World!")]
public partial class RenderTextSourceGeneratorAcceptanceTests
{
    [Fact]
    public void HelloWorldTextWithExternalFontIsSourceGenerated_RenderedTextMatches()
    {
        string expectedText = """
            ██╗  ██╗███████╗██╗     ██╗      ██████╗     ██╗    ██╗ ██████╗ ██████╗ ██╗     ██████╗ ██╗
            ██║  ██║██╔════╝██║     ██║     ██╔═══██╗    ██║    ██║██╔═══██╗██╔══██╗██║     ██╔══██╗██║
            ███████║█████╗  ██║     ██║     ██║   ██║    ██║ █╗ ██║██║   ██║██████╔╝██║     ██║  ██║██║
            ██╔══██║██╔══╝  ██║     ██║     ██║   ██║    ██║███╗██║██║   ██║██╔══██╗██║     ██║  ██║╚═╝
            ██║  ██║███████╗███████╗███████╗╚██████╔╝    ╚███╔███╔╝╚██████╔╝██║  ██║███████╗██████╔╝██╗
            ╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝ ╚═════╝      ╚══╝╚══╝  ╚═════╝ ╚═╝  ╚═╝╚══════╝╚═════╝ ╚═╝
            """;

        Assert.Equal(expectedText, HelloWorld.Trim(), NewlineIgnoreComparer.Instance);
    }

    [Fact]
    public void TextInStaticClassIsSourceGenerated_RenderedTextMatches()
    {
        string expectedText = """
             .----------------.  .----------------.  .----------------.  .----------------.  .----------------.  .----------------. 
            | .--------------. || .--------------. || .--------------. || .--------------. || .--------------. || .--------------. |
            | |    _______   | || |  _________   | || |      __      | || |  _________   | || |     _____    | || |     ______   | |
            | |   /  ___  |  | || | |  _   _  |  | || |     /  \     | || | |  _   _  |  | || |    |_   _|   | || |   .' ___  |  | |
            | |  |  (__ \_|  | || | |_/ | | \_|  | || |    / /\ \    | || | |_/ | | \_|  | || |      | |     | || |  / .'   \_|  | |
            | |   '.___`-.   | || |     | |      | || |   / ____ \   | || |     | |      | || |      | |     | || |  | |         | |
            | |  |`\____) |  | || |    _| |_     | || | _/ /    \ \_ | || |    _| |_     | || |     _| |_    | || |  \ `.___.'\  | |
            | |  |_______.'  | || |   |_____|    | || ||____|  |____|| || |   |_____|    | || |    |_____|   | || |   `._____.'  | |
            | |              | || |              | || |              | || |              | || |              | || |              | |
            | '--------------' || '--------------' || '--------------' || '--------------' || '--------------' || '--------------' |
             '----------------'  '----------------'  '----------------'  '----------------'  '----------------'  '----------------' 
            
            """;

        Assert.Equal(expectedText, TestStaticClass.StaticBlocks, NewlineIgnoreComparer.Instance);
    }
}

[GenerateFiggleText("StaticBlocks", "blocks", "Static")]
public static partial class TestStaticClass
{
}
