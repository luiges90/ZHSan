using System.Runtime.InteropServices;

namespace FontStashSharp
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct FontAtlasNode
	{
		public short X;
		public short Y;
		public short Width;
	}
}
