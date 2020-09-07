using System;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Figgle.Tests
{
    public class FiggleFontTest
    {
        private readonly ITestOutputHelper _output;
        public FiggleFontTest(ITestOutputHelper output) => _output = output;

        [Fact]
        public void Render()
        {
            Test(FiggleFonts.Standard, "Hello, World!", null,
                @"  _   _      _ _         __        __         _     _ _ ",
                @" | | | | ___| | | ___    \ \      / /__  _ __| | __| | |",
                @" | |_| |/ _ \ | |/ _ \    \ \ /\ / / _ \| '__| |/ _` | |",
                @" |  _  |  __/ | | (_) |    \ V  V / (_) | |  | | (_| |_|",
                @" |_| |_|\___|_|_|\___( )    \_/\_/ \___/|_|  |_|\__,_(_)",
                @"                     |/                                 ");

            Test(FiggleFonts.ThreePoint, "Hello, World!", null,
                @"|_| _ || _    \    / _  _| _||",
                @"| |(/_||(_),   \/\/ (_)| |(_|.",
                @"                              ");

            Test(FiggleFonts.Ogre, "Hello, World!", null,
                @"            _ _          __    __           _     _   _ ",
                @"  /\  /\___| | | ___    / / /\ \ \___  _ __| | __| | / \",
                @" / /_/ / _ \ | |/ _ \   \ \/  \/ / _ \| '__| |/ _` |/  /",
                @"/ __  /  __/ | | (_) |   \  /\  / (_) | |  | | (_| /\_/ ",
                @"\/ /_/ \___|_|_|\___( )   \/  \/ \___/|_|  |_|\__,_\/   ",
                @"                    |/                                  ");

            Test(FiggleFonts.Rectangles, "Hello, World!", null,
                @"                                            __ ",
                @" _____     _ _          _ _ _         _   _|  |",
                @"|  |  |___| | |___     | | | |___ ___| |_| |  |",
                @"|     | -_| | | . |_   | | | | . |  _| | . |__|",
                @"|__|__|___|_|_|___| |  |_____|___|_| |_|___|__|",
                @"                  |_|                          ");

            Test(FiggleFonts.Slant, "Hello, World!", null,
                @"    __  __     ____           _       __           __    ____",
                @"   / / / /__  / / /___       | |     / /___  _____/ /___/ / /",
                @"  / /_/ / _ \/ / / __ \      | | /| / / __ \/ ___/ / __  / / ",
                @" / __  /  __/ / / /_/ /      | |/ |/ / /_/ / /  / / /_/ /_/  ",
                @"/_/ /_/\___/_/_/\____( )     |__/|__/\____/_/  /_/\__,_(_)   ",
                @"                     |/                                      ");

            Test(FiggleFonts.Slant, "H.W", null,
                @"    __  ___       __",
                @"   / / / / |     / /",
                @"  / /_/ /| | /| / / ",
                @" / __  /_| |/ |/ /  ",
                @"/_/ /_/(_)__/|__/   ",
                @"                    ");

            Test(FiggleFonts.Impossible, "Figgle", null,
                @"         _        _          _              _              _             _      ",
                @"        /\ \     /\ \       /\ \           /\ \           _\ \          /\ \    ",
                @"       /  \ \    \ \ \     /  \ \         /  \ \         /\__ \        /  \ \   ",
                @"      / /\ \ \   /\ \_\   / /\ \_\       / /\ \_\       / /_ \_\      / /\ \ \  ",
                @"     / / /\ \_\ / /\/_/  / / /\/_/      / / /\/_/      / / /\/_/     / / /\ \_\ ",
                @"    / /_/_ \/_// / /    / / / ______   / / / ______   / / /         / /_/_ \/_/ ",
                @"   / /____/\  / / /    / / / /\_____\ / / / /\_____\ / / /         / /____/\    ",
                @"  / /\____\/ / / /    / / /  \/____ // / /  \/____ // / / ____    / /\____\/    ",
                @" / / /   ___/ / /__  / / /_____/ / // / /_____/ / // /_/_/ ___/\ / / /______    ",
                @"/ / /   /\__\/_/___\/ / /______\/ // / /______\/ //_______/\__\// / /_______\   ",
                @"\/_/    \/_________/\/___________/ \/___________/ \_______\/    \/__________/   ",
                @"                                                                                ");
            
            Test(FiggleFonts.Graffiti, "Hello, World!", null,
                @"  ___ ___         .__  .__               __      __            .__       .___._.",
                @" /   |   \   ____ |  | |  |   ____      /  \    /  \___________|  |    __| _/| |",
                @"/    ~    \_/ __ \|  | |  |  /  _ \     \   \/\/   /  _ \_  __ \  |   / __ | | |",
                @"\    Y    /\  ___/|  |_|  |_(  <_> )     \        (  <_> )  | \/  |__/ /_/ |  \|",
                @" \___|_  /  \___  >____/____/\____/  /\   \__/\  / \____/|__|  |____/\____ |  __",
                @"       \/       \/                   )/        \/                         \/  \/");

            // ReSharper disable once UnusedParameter.Local
            void Test(FiggleFont font, string s, int? smushOverride = null, params string[] expected)
            {
                var output = font.Render(s, smushOverride);
                var actual = output.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                actual = actual.Take(Math.Max(0, actual.Length - 1)).ToArray();
                Assert.Equal(expected.Length, actual.Length);
                for (var i = 0; i < expected.Length; i++)
                {
                    if (expected[i] == actual[i])
                        continue;
                    if (expected[i].Length != actual[i].Length)
                    {
                        _output.WriteLine("Expected:\n" + string.Join(Environment.NewLine, expected));
                        _output.WriteLine("Actual:\n" + output);
                        Assert.True(false, $"Mismatched lengths row {i}. Expecting '{expected[i].Length}' but got '{actual[i].Length}'.");
                    }
                    
                    for (var x = 0; x < expected[i].Length; x++)
                    {
                        if (expected[i][x] != actual[i][x])
                        {
                            _output.WriteLine("Expected:\n" + string.Join(Environment.NewLine, expected));
                            _output.WriteLine("Actual:\n" + output);
                            Assert.True(false, $"Mismatch at row {i} col {x}. Expecting '{expected[i][x]}' but got '{actual[i][x]}'.");
                        }
                    }
                }
            }
        }
    }
}
