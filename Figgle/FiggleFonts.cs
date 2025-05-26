// Copyright Drew Noakes. Licensed under the Apache-2.0 license. See the LICENSE file for more details.

using System;
using System.Collections.Concurrent;
using Figgle.Fonts;

namespace Figgle;

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
    private static readonly ConcurrentDictionary<string, FiggleFont> _fontByName = new(StringComparer.Ordinal);
    private static readonly StringPool _stringPool = new();

#pragma warning disable CS1591
    public static FiggleFont OneRow => GetByName("1row");
    public static FiggleFont ThreeD => GetByName("3-d");
    public static FiggleFont ThreeDDiagonal => GetByName("3d_diagonal");
    public static FiggleFont ThreeByFive => GetByName("3x5");
    public static FiggleFont FourMax => GetByName("4max");
    public static FiggleFont FiveLineOblique => GetByName("5lineoblique");
    public static FiggleFont Acrobatic => GetByName("acrobatic");
    public static FiggleFont Alligator => GetByName("alligator");
    public static FiggleFont Alligator2 => GetByName("alligator2");
    public static FiggleFont Alligator3 => GetByName("alligator3");
    public static FiggleFont Alpha => GetByName("alpha");
    public static FiggleFont Alphabet => GetByName("alphabet");
    public static FiggleFont Amc3Line => GetByName("amc3line");
    public static FiggleFont Amc3Liv1 => GetByName("amc3liv1");
    public static FiggleFont AmcAaa01 => GetByName("amcaaa01");
    public static FiggleFont AmcNeko => GetByName("amcneko");
    public static FiggleFont AmcRazor2 => GetByName("amcrazo2");
    public static FiggleFont AmcRazor => GetByName("amcrazor");
    public static FiggleFont AmcSlash => GetByName("amcslash");
    public static FiggleFont AmcSlder => GetByName("amcslder");
    public static FiggleFont AmcThin => GetByName("amcthin");
    public static FiggleFont AmcTubes => GetByName("amctubes");
    public static FiggleFont AmcUn1 => GetByName("amcun1");
    public static FiggleFont Arrows => GetByName("arrows");
    public static FiggleFont AsciiNewroman => GetByName("ascii_new_roman");
    public static FiggleFont Avatar => GetByName("avatar");
    public static FiggleFont B1FF => GetByName("B1FF");
    public static FiggleFont Banner => GetByName("banner");
    public static FiggleFont Banner3 => GetByName("banner3");
    public static FiggleFont Banner3D => GetByName("banner3-D");
    public static FiggleFont Banner4 => GetByName("banner4");
    public static FiggleFont BarbWire => GetByName("barbwire");
    public static FiggleFont Basic => GetByName("basic");
    public static FiggleFont Bear => GetByName("bear");
    public static FiggleFont Bell => GetByName("bell");
    public static FiggleFont Benjamin => GetByName("benjamin");
    public static FiggleFont Big => GetByName("big");
    public static FiggleFont BigChief => GetByName("bigchief");
    public static FiggleFont BigFig => GetByName("bigfig");
    public static FiggleFont Binary => GetByName("binary");
    public static FiggleFont Block => GetByName("block");
    public static FiggleFont Blocks => GetByName("blocks");
    public static FiggleFont Bolger => GetByName("bolger");
    public static FiggleFont Braced => GetByName("braced");
    public static FiggleFont Bright => GetByName("bright");
    public static FiggleFont Broadway => GetByName("broadway");
    public static FiggleFont BroadwayKB => GetByName("broadway_kb");
    public static FiggleFont Bubble => GetByName("bubble");
    public static FiggleFont Bulbhead => GetByName("bulbhead");
    public static FiggleFont Caligraphy2 => GetByName("calgphy2");
    public static FiggleFont Caligraphy => GetByName("caligraphy");
    public static FiggleFont Cards => GetByName("cards");
    public static FiggleFont CatWalk => GetByName("catwalk");
    public static FiggleFont Chiseled => GetByName("chiseled");
    public static FiggleFont Chunky => GetByName("chunky");
    public static FiggleFont Coinstak => GetByName("coinstak");
    public static FiggleFont Cola => GetByName("cola");
    public static FiggleFont Colossal => GetByName("colossal");
    public static FiggleFont Computer => GetByName("computer");
    public static FiggleFont Contessa => GetByName("contessa");
    public static FiggleFont Contrast => GetByName("contrast");
    public static FiggleFont Cosmic => GetByName("cosmic");
    public static FiggleFont Cosmike => GetByName("cosmike");
    public static FiggleFont Crawford => GetByName("crawford");
    public static FiggleFont Crazy => GetByName("crazy");
    public static FiggleFont Cricket => GetByName("cricket");
    public static FiggleFont Cursive => GetByName("cursive");
    public static FiggleFont CyberLarge => GetByName("cyberlarge");
    public static FiggleFont CyberMedium => GetByName("cybermedium");
    public static FiggleFont CyberSmall => GetByName("cybersmall");
    public static FiggleFont Cygnet => GetByName("cygnet");
    public static FiggleFont DANC4 => GetByName("DANC4");
    public static FiggleFont DancingFont => GetByName("dancingfont");
    public static FiggleFont Decimal => GetByName("decimal");
    public static FiggleFont DefLeppard => GetByName("defleppard");
    public static FiggleFont Diamond => GetByName("diamond");
    public static FiggleFont DietCola => GetByName("dietcola");
    public static FiggleFont Digital => GetByName("digital");
    public static FiggleFont Doh => GetByName("doh");
    public static FiggleFont Doom => GetByName("doom");
    public static FiggleFont DosRebel => GetByName("dosrebel");
    public static FiggleFont DotMatrix => GetByName("dotmatrix");
    public static FiggleFont Double => GetByName("double");
    public static FiggleFont DoubleShorts => GetByName("doubleshorts");
    public static FiggleFont DRPepper => GetByName("drpepper");
    public static FiggleFont DWhistled => GetByName("dwhistled");
    public static FiggleFont EftiChess => GetByName("eftichess");
    public static FiggleFont EftiFont => GetByName("eftifont");
    public static FiggleFont EftiPiti => GetByName("eftipiti");
    public static FiggleFont EftiRobot => GetByName("eftirobot");
    public static FiggleFont EftiItalic => GetByName("eftitalic");
    public static FiggleFont EftiWall => GetByName("eftiwall");
    public static FiggleFont EftiWater => GetByName("eftiwater");
    public static FiggleFont Epic => GetByName("epic");
    public static FiggleFont Fender => GetByName("fender");
    public static FiggleFont Filter => GetByName("filter");
    public static FiggleFont FireFontK => GetByName("fire_font-k");
    public static FiggleFont FireFontS => GetByName("fire_font-s");
    public static FiggleFont Flipped => GetByName("flipped");
    public static FiggleFont FlowerPower => GetByName("flowerpower");
    public static FiggleFont FourTops => GetByName("fourtops");
    public static FiggleFont Fraktur => GetByName("fraktur");
    public static FiggleFont FunFace => GetByName("funface");
    public static FiggleFont FunFaces => GetByName("funfaces");
    public static FiggleFont Fuzzy => GetByName("fuzzy");
    public static FiggleFont Georgia16 => GetByName("georgi16");
    public static FiggleFont Georgia11 => GetByName("Georgia11");
    public static FiggleFont Ghost => GetByName("ghost");
    public static FiggleFont Ghoulish => GetByName("ghoulish");
    public static FiggleFont Glenyn => GetByName("glenyn");
    public static FiggleFont Goofy => GetByName("goofy");
    public static FiggleFont Gothic => GetByName("gothic");
    public static FiggleFont Graceful => GetByName("graceful");
    public static FiggleFont Gradient => GetByName("gradient");
    public static FiggleFont Graffiti => GetByName("graffiti");
    public static FiggleFont Greek => GetByName("greek");
    public static FiggleFont HeartLeft => GetByName("heart_left");
    public static FiggleFont HeartRight => GetByName("heart_right");
    public static FiggleFont Henry3d => GetByName("henry3d");
    public static FiggleFont Hex => GetByName("hex");
    public static FiggleFont Hieroglyphs => GetByName("hieroglyphs");
    public static FiggleFont Hollywood => GetByName("hollywood");
    public static FiggleFont HorizontalLeft => GetByName("horizontalleft");
    public static FiggleFont HorizontalRight => GetByName("horizontalright");
    public static FiggleFont ICL1900 => GetByName("ICL-1900");
    public static FiggleFont Impossible => GetByName("impossible");
    public static FiggleFont Invita => GetByName("invita");
    public static FiggleFont Isometric1 => GetByName("isometric1");
    public static FiggleFont Isometric2 => GetByName("isometric2");
    public static FiggleFont Isometric3 => GetByName("isometric3");
    public static FiggleFont Isometric4 => GetByName("isometric4");
    public static FiggleFont Italic => GetByName("italic");
    public static FiggleFont Ivrit => GetByName("ivrit");
    public static FiggleFont Jacky => GetByName("jacky");
    public static FiggleFont Jazmine => GetByName("jazmine");
    public static FiggleFont Jerusalem => GetByName("jerusalem");
    public static FiggleFont Katakana => GetByName("katakana");
    public static FiggleFont Kban => GetByName("kban");
    public static FiggleFont Keyboard => GetByName("keyboard");
    public static FiggleFont Knob => GetByName("knob");
    public static FiggleFont Konto => GetByName("konto");
    public static FiggleFont KontoSlant => GetByName("kontoslant");
    public static FiggleFont Larry3d => GetByName("larry3d");
    public static FiggleFont Lcd => GetByName("lcd");
    public static FiggleFont Lean => GetByName("lean");
    public static FiggleFont Letters => GetByName("letters");
    public static FiggleFont LilDevil => GetByName("lildevil");
    public static FiggleFont LineBlocks => GetByName("lineblocks");
    public static FiggleFont Linux => GetByName("linux");
    public static FiggleFont LockerGnome => GetByName("lockergnome");
    public static FiggleFont Madrid => GetByName("madrid");
    public static FiggleFont Marquee => GetByName("marquee");
    public static FiggleFont MaxFour => GetByName("maxfour");
    public static FiggleFont Merlin1 => GetByName("merlin1");
    public static FiggleFont Merlin2 => GetByName("merlin2");
    public static FiggleFont Mike => GetByName("mike");
    public static FiggleFont Mini => GetByName("mini");
    public static FiggleFont Mirror => GetByName("mirror");
    public static FiggleFont Mnemonic => GetByName("mnemonic");
    public static FiggleFont Modular => GetByName("modular");
    public static FiggleFont Morse => GetByName("morse");
    public static FiggleFont Morse2 => GetByName("morse2");
    public static FiggleFont Moscow => GetByName("moscow");
    public static FiggleFont Mshebrew210 => GetByName("mshebrew210");
    public static FiggleFont Muzzle => GetByName("muzzle");
    public static FiggleFont NancyJ => GetByName("nancyj");
    public static FiggleFont NancyJFancy => GetByName("nancyj-fancy");
    public static FiggleFont NancyJImproved => GetByName("nancyj-improved");
    public static FiggleFont NancyJUnderlined => GetByName("nancyj-underlined");
    public static FiggleFont Nipples => GetByName("nipples");
    public static FiggleFont NScript => GetByName("nscript");
    public static FiggleFont NTGreek => GetByName("ntgreek");
    public static FiggleFont NVScript => GetByName("nvscript");
    public static FiggleFont O8 => GetByName("o8");
    public static FiggleFont Octal => GetByName("octal");
    public static FiggleFont Ogre => GetByName("ogre");
    public static FiggleFont OldBanner => GetByName("oldbanner");
    public static FiggleFont OS2 => GetByName("os2");
    public static FiggleFont Pawp => GetByName("pawp");
    public static FiggleFont Peaks => GetByName("peaks");
    public static FiggleFont PeaksSlant => GetByName("peaksslant");
    public static FiggleFont Pebbles => GetByName("pebbles");
    public static FiggleFont Pepper => GetByName("pepper");
    public static FiggleFont Poison => GetByName("poison");
    public static FiggleFont Puffy => GetByName("puffy");
    public static FiggleFont Puzzle => GetByName("puzzle");
    public static FiggleFont Pyramid => GetByName("pyramid");
    public static FiggleFont Rammstein => GetByName("rammstein");
    public static FiggleFont Rectangles => GetByName("rectangles");
    public static FiggleFont RedPhoenix => GetByName("red_phoenix");
    public static FiggleFont Relief => GetByName("relief");
    public static FiggleFont Relief2 => GetByName("relief2");
    public static FiggleFont Rev => GetByName("rev");
    public static FiggleFont Reverse => GetByName("reverse");
    public static FiggleFont Roman => GetByName("roman");
    public static FiggleFont Rot13 => GetByName("rot13");
    public static FiggleFont Rotated => GetByName("rotated");
    public static FiggleFont Rounded => GetByName("rounded");
    public static FiggleFont RowanCap => GetByName("rowancap");
    public static FiggleFont Rozzo => GetByName("rozzo");
    public static FiggleFont Runic => GetByName("runic");
    public static FiggleFont Runyc => GetByName("runyc");
    public static FiggleFont SantaClara => GetByName("santaclara");
    public static FiggleFont SBlood => GetByName("sblood");
    public static FiggleFont Script => GetByName("script");
    public static FiggleFont ScriptSlant => GetByName("slscript");
    public static FiggleFont SerifCap => GetByName("serifcap");
    public static FiggleFont Shadow => GetByName("shadow");
    public static FiggleFont Shimrod => GetByName("shimrod");
    public static FiggleFont Short => GetByName("short");
    public static FiggleFont Slant => GetByName("slant");
    public static FiggleFont Slide => GetByName("slide");
    public static FiggleFont Small => GetByName("small");
    public static FiggleFont SmallCaps => GetByName("smallcaps");
    public static FiggleFont IsometricSmall => GetByName("smisome1");
    public static FiggleFont KeyboardSmall => GetByName("smkeyboard");
    public static FiggleFont PoisonSmall => GetByName("smpoison");
    public static FiggleFont ScriptSmall => GetByName("smscript");
    public static FiggleFont ShadowSmall => GetByName("smshadow");
    public static FiggleFont SlantSmall => GetByName("smslant");
    public static FiggleFont TengwarSmall => GetByName("smtengwar");
    public static FiggleFont Soft => GetByName("soft");
    public static FiggleFont Speed => GetByName("speed");
    public static FiggleFont Spliff => GetByName("spliff");
    public static FiggleFont SRelief => GetByName("s-relief");
    public static FiggleFont Stacey => GetByName("stacey");
    public static FiggleFont Stampate => GetByName("stampate");
    public static FiggleFont Stampatello => GetByName("stampatello");
    public static FiggleFont Standard => GetByName("standard");
    public static FiggleFont Starstrips => GetByName("starstrips");
    public static FiggleFont Starwars => GetByName("starwars");
    public static FiggleFont Stellar => GetByName("stellar");
    public static FiggleFont Stforek => GetByName("stforek");
    public static FiggleFont Stop => GetByName("stop");
    public static FiggleFont Straight => GetByName("straight");
    public static FiggleFont SubZero => GetByName("sub-zero");
    public static FiggleFont Swampland => GetByName("swampland");
    public static FiggleFont Swan => GetByName("swan");
    public static FiggleFont Sweet => GetByName("sweet");
    public static FiggleFont Tanja => GetByName("tanja");
    public static FiggleFont Tengwar => GetByName("tengwar");
    public static FiggleFont Term => GetByName("term");
    public static FiggleFont Test1 => GetByName("test1");
    public static FiggleFont Thick => GetByName("thick");
    public static FiggleFont Thin => GetByName("thin");
    public static FiggleFont ThreePoint => GetByName("threepoint");
    public static FiggleFont Ticks => GetByName("ticks");
    public static FiggleFont TicksSlant => GetByName("ticksslant");
    public static FiggleFont Tiles => GetByName("tiles");
    public static FiggleFont TinkerToy => GetByName("tinker-toy");
    public static FiggleFont Tombstone => GetByName("tombstone");
    public static FiggleFont Train => GetByName("train");
    public static FiggleFont Trek => GetByName("trek");
    public static FiggleFont Tsalagi => GetByName("tsalagi");
    public static FiggleFont Tubular => GetByName("tubular");
    public static FiggleFont Twisted => GetByName("twisted");
    public static FiggleFont TwoPoint => GetByName("twopoint");
    public static FiggleFont Univers => GetByName("univers");
    public static FiggleFont UsaFlag => GetByName("usaflag");
    public static FiggleFont Varsity => GetByName("varsity");
    public static FiggleFont Wavy => GetByName("wavy");
    public static FiggleFont Weird => GetByName("weird");
    public static FiggleFont WetLetter => GetByName("wetletter");
    public static FiggleFont Whimsy => GetByName("whimsy");
    public static FiggleFont Wow => GetByName("wow");
#pragma warning restore CS1591

    private static FiggleFont GetByName(string name)
    {
        return _fontByName.GetOrAdd(name, FontFactory);

        static FiggleFont FontFactory(string name)
        {
            var font = ParseEmbeddedFont(name);

            if (font is null)
                throw new FiggleException($"No embedded font exists with name \"{name}\".");

            return font;
        }
    }

    /// <summary>
    /// Attempts to load the font with specified name.
    /// </summary>
    /// <param name="name">the name of the font. Case-sensitive.</param>
    /// <returns>The font if found, otherwise <see langword="null"/>.</returns>
    public static FiggleFont? TryGetByName(string name)
    {
        if (_fontByName.TryGetValue(name, out var font))
            return font;

        font = ParseEmbeddedFont(name);

        if (font is not null)
            _fontByName.TryAdd(name, font);

        return font;
    }

    private static FiggleFont? ParseEmbeddedFont(string name)
    {
        try
        {
            var fontDescriptionString = EmbeddedFontResource.GetFontDescription(name);
            if (fontDescriptionString is null)
                return null;

            return FiggleFontParser.ParseString(fontDescriptionString, _stringPool);
        }
        catch (Exception ex)
        {
            throw new FiggleException("Could not parse embedded font.", ex);
        }
    }
}
