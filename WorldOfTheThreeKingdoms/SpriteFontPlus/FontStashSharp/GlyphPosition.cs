using System.Runtime.InteropServices;

namespace FontStashSharp
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct GlyphPosition
	{
		public StringSegment Str;
		public float X;
		public float MinX;
		public float MaxX;
	}
}
