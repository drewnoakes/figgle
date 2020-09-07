#region License

// Copyright 2017 Drew Noakes
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

#endregion

using System.Collections.Concurrent;
using System.IO.Compression;
using System.Reflection;

namespace Figgle
{
    /// <summary>
    /// Collection of bundled fonts, ready for use.
    /// </summary>
    /// <remarks>
    /// Fonts are lazily loaded upon property access. Only the fonts you use will be loaded.
    /// <para />
    /// Fonts are stored as an embedded ZIP archive within the assembly.
    /// </remarks>
    public static class FiggleFonts
    {
        private static readonly ConcurrentDictionary<string, FiggleFont> _fontByName = new ConcurrentDictionary<string, FiggleFont>();

#pragma warning disable CS1591
// ReSharper disable UnusedMember.Global
        public static FiggleFont OneRow => Lookup("1row");
        public static FiggleFont ThreeD => Lookup("3-d");
        public static FiggleFont ThreeDDiagonal => Lookup("3d_diagonal");
        public static FiggleFont ThreeByFive => Lookup("3x5");
        public static FiggleFont FourMax => Lookup("4max");
        public static FiggleFont FiveLineOblique => Lookup("5lineoblique");
        public static FiggleFont Acrobatic => Lookup("acrobatic");
        public static FiggleFont Alligator => Lookup("alligator");
        public static FiggleFont Alligator2 => Lookup("alligator2");
        public static FiggleFont Alligator3 => Lookup("alligator3");
        public static FiggleFont Alpha => Lookup("alpha");
        public static FiggleFont Alphabet => Lookup("alphabet");
        public static FiggleFont Amc3Line => Lookup("amc3line");
        public static FiggleFont Amc3Liv1 => Lookup("amc3liv1");
        public static FiggleFont AmcAaa01 => Lookup("amcaaa01");
        public static FiggleFont AmcNeko => Lookup("amcneko");
        public static FiggleFont AmcRazor2 => Lookup("amcrazo2");
        public static FiggleFont AmcRazor => Lookup("amcrazor");
        public static FiggleFont AmcSlash => Lookup("amcslash");
        public static FiggleFont AmcSlder => Lookup("amcslder");
        public static FiggleFont AmcThin => Lookup("amcthin");
        public static FiggleFont AmcTubes => Lookup("amctubes");
        public static FiggleFont AmcUn1 => Lookup("amcun1");
        public static FiggleFont Arrows => Lookup("arrows");
        public static FiggleFont AsciiNewroman => Lookup("ascii_new_roman");
        public static FiggleFont Avatar => Lookup("avatar");
        public static FiggleFont B1FF => Lookup("B1FF");
        public static FiggleFont Banner => Lookup("banner");
        public static FiggleFont Banner3 => Lookup("banner3");
        public static FiggleFont Banner3D => Lookup("banner3-D");
        public static FiggleFont Banner4 => Lookup("banner4");
        public static FiggleFont BarbWire => Lookup("barbwire");
        public static FiggleFont Basic => Lookup("basic");
        public static FiggleFont Bear => Lookup("bear");
        public static FiggleFont Bell => Lookup("bell");
        public static FiggleFont Benjamin => Lookup("benjamin");
        public static FiggleFont Big => Lookup("big");
        public static FiggleFont BigChief => Lookup("bigchief");
        public static FiggleFont BigFig => Lookup("bigfig");
        public static FiggleFont Binary => Lookup("binary");
        public static FiggleFont Block => Lookup("block");
        public static FiggleFont Blocks => Lookup("blocks");
        public static FiggleFont Bolger => Lookup("bolger");
        public static FiggleFont Braced => Lookup("braced");
        public static FiggleFont Bright => Lookup("bright");
        public static FiggleFont Broadway => Lookup("broadway");
        public static FiggleFont BroadwayKB => Lookup("broadway_kb");
        public static FiggleFont Bubble => Lookup("bubble");
        public static FiggleFont Bulbhead => Lookup("bulbhead");
        public static FiggleFont Caligraphy2 => Lookup("calgphy2");
        public static FiggleFont Caligraphy => Lookup("caligraphy");
        public static FiggleFont Cards => Lookup("cards");
        public static FiggleFont CatWalk => Lookup("catwalk");
        public static FiggleFont Chiseled => Lookup("chiseled");
        public static FiggleFont Chunky => Lookup("chunky");
        public static FiggleFont Coinstak => Lookup("coinstak");
        public static FiggleFont Cola => Lookup("cola");
        public static FiggleFont Colossal => Lookup("colossal");
        public static FiggleFont Computer => Lookup("computer");
        public static FiggleFont Contessa => Lookup("contessa");
        public static FiggleFont Contrast => Lookup("contrast");
        public static FiggleFont Cosmic => Lookup("cosmic");
        public static FiggleFont Cosmike => Lookup("cosmike");
        public static FiggleFont Crawford => Lookup("crawford");
        public static FiggleFont Crazy => Lookup("crazy");
        public static FiggleFont Cricket => Lookup("cricket");
        public static FiggleFont Cursive => Lookup("cursive");
        public static FiggleFont CyberLarge => Lookup("cyberlarge");
        public static FiggleFont CyberMedium => Lookup("cybermedium");
        public static FiggleFont CyberSmall => Lookup("cybersmall");
        public static FiggleFont Cygnet => Lookup("cygnet");
        public static FiggleFont DANC4 => Lookup("DANC4");
        public static FiggleFont DancingFont => Lookup("dancingfont");
        public static FiggleFont Decimal => Lookup("decimal");
        public static FiggleFont DefLeppard => Lookup("defleppard");
        public static FiggleFont Diamond => Lookup("diamond");
        public static FiggleFont DietCola => Lookup("dietcola");
        public static FiggleFont Digital => Lookup("digital");
        public static FiggleFont Doh => Lookup("doh");
        public static FiggleFont Doom => Lookup("doom");
        public static FiggleFont DosRebel => Lookup("dosrebel");
        public static FiggleFont DotMatrix => Lookup("dotmatrix");
        public static FiggleFont Double => Lookup("double");
        public static FiggleFont DoubleShorts => Lookup("doubleshorts");
        public static FiggleFont DRPepper => Lookup("drpepper");
        public static FiggleFont DWhistled => Lookup("dwhistled");
        public static FiggleFont EftiChess => Lookup("eftichess");
        public static FiggleFont EftiFont => Lookup("eftifont");
        public static FiggleFont EftiPiti => Lookup("eftipiti");
        public static FiggleFont EftiRobot => Lookup("eftirobot");
        public static FiggleFont EftiItalic => Lookup("eftitalic");
        public static FiggleFont EftiWall => Lookup("eftiwall");
        public static FiggleFont EftiWater => Lookup("eftiwater");
        public static FiggleFont Epic => Lookup("epic");
        public static FiggleFont Fender => Lookup("fender");
        public static FiggleFont Filter => Lookup("filter");
        public static FiggleFont FireFontK => Lookup("fire_font-k");
        public static FiggleFont FireFontS => Lookup("fire_font-s");
        public static FiggleFont Flipped => Lookup("flipped");
        public static FiggleFont FlowerPower => Lookup("flowerpower");
        public static FiggleFont FourTops => Lookup("fourtops");
        public static FiggleFont Fraktur => Lookup("fraktur");
        public static FiggleFont FunFace => Lookup("funface");
        public static FiggleFont FunFaces => Lookup("funfaces");
        public static FiggleFont Fuzzy => Lookup("fuzzy");
        public static FiggleFont Georgia16 => Lookup("georgi16");
        public static FiggleFont Georgia11 => Lookup("Georgia11");
        public static FiggleFont Ghost => Lookup("ghost");
        public static FiggleFont Ghoulish => Lookup("ghoulish");
        public static FiggleFont Glenyn => Lookup("glenyn");
        public static FiggleFont Goofy => Lookup("goofy");
        public static FiggleFont Gothic => Lookup("gothic");
        public static FiggleFont Graceful => Lookup("graceful");
        public static FiggleFont Gradient => Lookup("gradient");
        public static FiggleFont Graffiti => Lookup("graffiti");
        public static FiggleFont Greek => Lookup("greek");
        public static FiggleFont HeartLeft => Lookup("heart_left");
        public static FiggleFont HeartRight => Lookup("heart_right");
        public static FiggleFont Henry3d => Lookup("henry3d");
        public static FiggleFont Hex => Lookup("hex");
        public static FiggleFont Hieroglyphs => Lookup("hieroglyphs");
        public static FiggleFont Hollywood => Lookup("hollywood");
        public static FiggleFont HorizontalLeft => Lookup("horizontalleft");
        public static FiggleFont HorizontalRight => Lookup("horizontalright");
        public static FiggleFont ICL1900 => Lookup("ICL-1900");
        public static FiggleFont Impossible => Lookup("impossible");
        public static FiggleFont Invita => Lookup("invita");
        public static FiggleFont Isometric1 => Lookup("isometric1");
        public static FiggleFont Isometric2 => Lookup("isometric2");
        public static FiggleFont Isometric3 => Lookup("isometric3");
        public static FiggleFont Isometric4 => Lookup("isometric4");
        public static FiggleFont Italic => Lookup("italic");
        public static FiggleFont Ivrit => Lookup("ivrit");
        public static FiggleFont Jacky => Lookup("jacky");
        public static FiggleFont Jazmine => Lookup("jazmine");
        public static FiggleFont Jerusalem => Lookup("jerusalem");
        public static FiggleFont Katakana => Lookup("katakana");
        public static FiggleFont Kban => Lookup("kban");
        public static FiggleFont Keyboard => Lookup("keyboard");
        public static FiggleFont Knob => Lookup("knob");
        public static FiggleFont Konto => Lookup("konto");
        public static FiggleFont KontoSlant => Lookup("kontoslant");
        public static FiggleFont Larry3d => Lookup("larry3d");
        public static FiggleFont Lcd => Lookup("lcd");
        public static FiggleFont Lean => Lookup("lean");
        public static FiggleFont Letters => Lookup("letters");
        public static FiggleFont LilDevil => Lookup("lildevil");
        public static FiggleFont LineBlocks => Lookup("lineblocks");
        public static FiggleFont Linux => Lookup("linux");
        public static FiggleFont LockerGnome => Lookup("lockergnome");
        public static FiggleFont Madrid => Lookup("madrid");
        public static FiggleFont Marquee => Lookup("marquee");
        public static FiggleFont MaxFour => Lookup("maxfour");
        public static FiggleFont Merlin1 => Lookup("merlin1");
        public static FiggleFont Merlin2 => Lookup("merlin2");
        public static FiggleFont Mike => Lookup("mike");
        public static FiggleFont Mini => Lookup("mini");
        public static FiggleFont Mirror => Lookup("mirror");
        public static FiggleFont Mnemonic => Lookup("mnemonic");
        public static FiggleFont Modular => Lookup("modular");
        public static FiggleFont Morse => Lookup("morse");
        public static FiggleFont Morse2 => Lookup("morse2");
        public static FiggleFont Moscow => Lookup("moscow");
        public static FiggleFont Mshebrew210 => Lookup("mshebrew210");
        public static FiggleFont Muzzle => Lookup("muzzle");
        public static FiggleFont NancyJ => Lookup("nancyj");
        public static FiggleFont NancyJFancy => Lookup("nancyj-fancy");
        public static FiggleFont NancyJImproved => Lookup("nancyj-improved");
        public static FiggleFont NancyJUnderlined => Lookup("nancyj-underlined");
        public static FiggleFont Nipples => Lookup("nipples");
        public static FiggleFont NScript => Lookup("nscript");
        public static FiggleFont NTGreek => Lookup("ntgreek");
        public static FiggleFont NVScript => Lookup("nvscript");
        public static FiggleFont O8 => Lookup("o8");
        public static FiggleFont Octal => Lookup("octal");
        public static FiggleFont Ogre => Lookup("ogre");
        public static FiggleFont OldBanner => Lookup("oldbanner");
        public static FiggleFont OS2 => Lookup("os2");
        public static FiggleFont Pawp => Lookup("pawp");
        public static FiggleFont Peaks => Lookup("peaks");
        public static FiggleFont PeaksSlant => Lookup("peaksslant");
        public static FiggleFont Pebbles => Lookup("pebbles");
        public static FiggleFont Pepper => Lookup("pepper");
        public static FiggleFont Poison => Lookup("poison");
        public static FiggleFont Puffy => Lookup("puffy");
        public static FiggleFont Puzzle => Lookup("puzzle");
        public static FiggleFont Pyramid => Lookup("pyramid");
        public static FiggleFont Rammstein => Lookup("rammstein");
        public static FiggleFont Rectangles => Lookup("rectangles");
        public static FiggleFont RedPhoenix => Lookup("red_phoenix");
        public static FiggleFont Relief => Lookup("relief");
        public static FiggleFont Relief2 => Lookup("relief2");
        public static FiggleFont Rev => Lookup("rev");
        public static FiggleFont Reverse => Lookup("reverse");
        public static FiggleFont Roman => Lookup("roman");
        public static FiggleFont Rot13 => Lookup("rot13");
        public static FiggleFont Rotated => Lookup("rotated");
        public static FiggleFont Rounded => Lookup("rounded");
        public static FiggleFont RowanCap => Lookup("rowancap");
        public static FiggleFont Rozzo => Lookup("rozzo");
        public static FiggleFont Runic => Lookup("runic");
        public static FiggleFont Runyc => Lookup("runyc");
        public static FiggleFont SantaClara => Lookup("santaclara");
        public static FiggleFont SBlood => Lookup("sblood");
        public static FiggleFont Script => Lookup("script");
        public static FiggleFont ScriptSlant => Lookup("slscript");
        public static FiggleFont SerifCap => Lookup("serifcap");
        public static FiggleFont Shadow => Lookup("shadow");
        public static FiggleFont Shimrod => Lookup("shimrod");
        public static FiggleFont Short => Lookup("short");
        public static FiggleFont Slant => Lookup("slant");
        public static FiggleFont Slide => Lookup("slide");
        public static FiggleFont Small => Lookup("small");
        public static FiggleFont SmallCaps => Lookup("smallcaps");
        public static FiggleFont IsometricSmall => Lookup("smisome1");
        public static FiggleFont KeyboardSmall => Lookup("smkeyboard");
        public static FiggleFont PoisonSmall => Lookup("smpoison");
        public static FiggleFont ScriptSmall => Lookup("smscript");
        public static FiggleFont ShadowSmall => Lookup("smshadow");
        public static FiggleFont SlantSmall => Lookup("smslant");
        public static FiggleFont TengwarSmall => Lookup("smtengwar");
        public static FiggleFont Soft => Lookup("soft");
        public static FiggleFont Speed => Lookup("speed");
        public static FiggleFont Spliff => Lookup("spliff");
        public static FiggleFont SRelief => Lookup("s-relief");
        public static FiggleFont Stacey => Lookup("stacey");
        public static FiggleFont Stampate => Lookup("stampate");
        public static FiggleFont Stampatello => Lookup("stampatello");
        public static FiggleFont Standard => Lookup("standard");
        public static FiggleFont Starstrips => Lookup("starstrips");
        public static FiggleFont Starwars => Lookup("starwars");
        public static FiggleFont Stellar => Lookup("stellar");
        public static FiggleFont Stforek => Lookup("stforek");
        public static FiggleFont Stop => Lookup("stop");
        public static FiggleFont Straight => Lookup("straight");
        public static FiggleFont SubZero => Lookup("sub-zero");
        public static FiggleFont Swampland => Lookup("swampland");
        public static FiggleFont Swan => Lookup("swan");
        public static FiggleFont Sweet => Lookup("sweet");
        public static FiggleFont Tanja => Lookup("tanja");
        public static FiggleFont Tengwar => Lookup("tengwar");
        public static FiggleFont Term => Lookup("term");
        public static FiggleFont Test1 => Lookup("test1");
        public static FiggleFont Thick => Lookup("thick");
        public static FiggleFont Thin => Lookup("thin");
        public static FiggleFont ThreePoint => Lookup("threepoint");
        public static FiggleFont Ticks => Lookup("ticks");
        public static FiggleFont TicksSlant => Lookup("ticksslant");
        public static FiggleFont Tiles => Lookup("tiles");
        public static FiggleFont TinkerToy => Lookup("tinker-toy");
        public static FiggleFont Tombstone => Lookup("tombstone");
        public static FiggleFont Train => Lookup("train");
        public static FiggleFont Trek => Lookup("trek");
        public static FiggleFont Tsalagi => Lookup("tsalagi");
        public static FiggleFont Tubular => Lookup("tubular");
        public static FiggleFont Twisted => Lookup("twisted");
        public static FiggleFont TwoPoint => Lookup("twopoint");
        public static FiggleFont Univers => Lookup("univers");
        public static FiggleFont UsaFlag => Lookup("usaflag");
        public static FiggleFont Varsity => Lookup("varsity");
        public static FiggleFont Wavy => Lookup("wavy");
        public static FiggleFont Weird => Lookup("weird");
        public static FiggleFont WetLetter => Lookup("wetletter");
        public static FiggleFont Whimsy => Lookup("whimsy");
        public static FiggleFont Wow => Lookup("wow");
// ReSharper restore UnusedMember.Global
#pragma warning restore CS1591

        private static FiggleFont Lookup(string name) => _fontByName.GetOrAdd(name, FontFactory);

        private static readonly StringPool _stringPool = new StringPool();

        private static FiggleFont FontFactory(string name)
        {
            using var stream = typeof(FiggleFonts).GetTypeInfo().Assembly.GetManifestResourceStream("Figgle.Fonts.zip");
            using var zip = new ZipArchive(stream, ZipArchiveMode.Read);
         
            var entry = zip.GetEntry(name + ".flf");

            if (entry == null)
                throw new FiggleException($"No embedded font exists with name \"{name}\".");

            using var entryStream = entry.Open();
                
            return FiggleFontParser.Parse(entryStream, _stringPool);
        }
    }
}
