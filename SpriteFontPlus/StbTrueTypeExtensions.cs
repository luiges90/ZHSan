using StbSharp;

namespace SpriteFontPlus
{
	internal static unsafe class StbTrueTypeExtensions
	{
		public static void fons__tt_getFontVMetrics(this StbTrueType.stbtt_fontinfo font, int* ascent, int* descent, int* lineGap)
		{
			StbTrueType.stbtt_GetFontVMetrics(font, ascent, descent, lineGap);
		}

		public static float fons__tt_getPixelHeightScale(this StbTrueType.stbtt_fontinfo font, float size)
		{
			return (float)(StbTrueType.stbtt_ScaleForPixelHeight(font, (float)(size)));
		}

		public static int fons__tt_getGlyphIndex(this StbTrueType.stbtt_fontinfo font, int codepoint)
		{
			return (int)(StbTrueType.stbtt_FindGlyphIndex(font, (int)(codepoint)));
		}

		public static int fons__tt_buildGlyphBitmap(this StbTrueType.stbtt_fontinfo font, int glyph, float size, float scale, int* advance, int* lsb, int* x0, int* y0, int* x1, int* y1)
		{
			StbTrueType.stbtt_GetGlyphHMetrics(font, (int)(glyph), advance, lsb);
			StbTrueType.stbtt_GetGlyphBitmapBox(font, (int)(glyph), (float)(scale), (float)(scale), x0, y0, x1, y1);
			return (int)(1);
		}

		public static void fons__tt_renderGlyphBitmap(this StbTrueType.stbtt_fontinfo font, byte* output, int outWidth, int outHeight, int outStride, float scaleX, float scaleY, int glyph)
		{
			StbTrueType.stbtt_MakeGlyphBitmap(font, output, (int)(outWidth), (int)(outHeight), (int)(outStride), (float)(scaleX), (float)(scaleY), (int)(glyph));
		}

		public static int fons__tt_getGlyphKernAdvance(this StbTrueType.stbtt_fontinfo font, int glyph1, int glyph2)
		{
			return (int)(StbTrueType.stbtt_GetGlyphKernAdvance(font, (int)(glyph1), (int)(glyph2)));
		}
	}
}
