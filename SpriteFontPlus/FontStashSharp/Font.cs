using StbSharp;

namespace FontStashSharp
{
	internal unsafe class Font
	{
		public StbTrueType.stbtt_fontinfo _font = new StbTrueType.stbtt_fontinfo();
		public string Name;
		public byte[] Data;
		public float Ascent;
		public float Ascender;
		public float Descender;
		public float LineHeight;
		public FontGlyph[] Glyphs;
		public int GlyphsNumber;
		public int[] Lut = new int[256];
		public int[] Fallbacks = new int[20];
		public int FallbacksCount;
	}
}
