namespace SpriteFontPlus
{
    public struct CharacterRange
    {
        public static readonly CharacterRange BasicLatin = new CharacterRange((char) 0x0020, (char) 0x007F);

        public static readonly CharacterRange Latin1Supplement =
            new CharacterRange((char) 0x00A0, (char) 0x00FF);

        public static readonly CharacterRange LatinExtendedA =
            new CharacterRange((char) 0x0100, (char) 0x017F);

        public static readonly CharacterRange LatinExtendedB =
            new CharacterRange((char) 0x0180, (char) 0x024F);

        public static readonly CharacterRange Cyrillic = new CharacterRange((char) 0x0400, (char) 0x04FF);

        public static readonly CharacterRange CyrillicSupplement =
            new CharacterRange((char) 0x0500, (char) 0x052F);

        public static readonly CharacterRange Hiragana =
            new CharacterRange((char) 0x3040, (char) 0x309F);

        public static readonly CharacterRange Katakana =
            new CharacterRange((char) 0x30A0, (char) 0x30FF);

        public char Start { get; private set; }
        public char End { get; private set; }

        public CharacterRange(char start, char end)
        {
            Start = start;
            End = end;
        }
        
        public CharacterRange(char single): this(single, single)
        {
        }
    }
}