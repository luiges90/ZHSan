using System.Runtime.InteropServices;

namespace FontStashSharp
{
	[StructLayout(LayoutKind.Sequential)]
	internal class FontGlyph
	{
		public int Codepoint;
		public int Index;
		public int Next;
		public int Size;
		public int Blur;
		public int X0;
		public int Y0;
		public int X1;
		public int Y1;
		public int XAdvance;
		public int XOffset;
		public int YOffset;
	}
}
