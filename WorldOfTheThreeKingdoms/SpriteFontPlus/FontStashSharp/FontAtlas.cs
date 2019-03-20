using System;
using System.Runtime.InteropServices;
using StbSharp;

namespace FontStashSharp
{
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe class FontAtlas
	{
		public int NodesCount;
		public int Height;
		public int NodesNumber;
		public FontAtlasNode* Nodes;
		public int Width;

		public FontAtlas(int w, int h, int count)
		{
			Width = w;
			Height = h;
			Nodes = (FontAtlasNode*)CRuntime.malloc((ulong)(sizeof(FontAtlasNode) * count));
			CRuntime.memset(Nodes, 0, (ulong)(sizeof(FontAtlasNode) * count));
			count = 0;
			NodesCount = count;
			Nodes[0].X = 0;
			Nodes[0].Y = 0;
			Nodes[0].Width = (short)w;
			NodesNumber++;
		}

		public int InsertNode(int idx, int x, int y, int w)
		{
			if (NodesNumber + 1 > NodesCount)
			{
				NodesCount = NodesCount == 0 ? 8 : NodesCount * 2;
				Nodes = (FontAtlasNode*)CRuntime.realloc(Nodes, (ulong)(sizeof(FontAtlasNode) * NodesCount));
				if (Nodes == null)
					return 0;
			}

			for (var i = NodesNumber; i > idx; i--) Nodes[i] = Nodes[i - 1];
			Nodes[idx].X = (short)x;
			Nodes[idx].Y = (short)y;
			Nodes[idx].Width = (short)w;
			NodesNumber++;
			return 1;
		}

		public void RemoveNode(int idx)
		{
			if (NodesNumber == 0)
				return;
			for (var i = idx; i < NodesNumber - 1; i++) Nodes[i] = Nodes[i + 1];
			NodesNumber--;
		}

		public void Expand(int w, int h)
		{
			if (w > Width)
				InsertNode(NodesNumber, Width, 0, w - Width);
			Width = w;
			Height = h;
		}

		public void Reset(int w, int h)
		{
			Width = w;
			Height = h;
			NodesNumber = 0;
			Nodes[0].X = 0;
			Nodes[0].Y = 0;
			Nodes[0].Width = (short)w;
			NodesNumber++;
		}

		public int AddSkylineLevel(int idx, int x, int y, int w, int h)
		{
			if (InsertNode(idx, x, y + h, w) == 0)
				return 0;
			for (var i = idx + 1; i < NodesNumber; i++)
				if (Nodes[i].X < Nodes[i - 1].X + Nodes[i - 1].Width)
				{
					var shrink = Nodes[i - 1].X + Nodes[i - 1].Width - Nodes[i].X;
					Nodes[i].X += (short)shrink;
					Nodes[i].Width -= (short)shrink;
					if (Nodes[i].Width <= 0)
					{
						RemoveNode(i);
						i--;
					}
					else
					{
						break;
					}
				}
				else
				{
					break;
				}

			for (var i = 0; i < NodesNumber - 1; i++)
				if (Nodes[i].Y == Nodes[i + 1].Y)
				{
					Nodes[i].Width += Nodes[i + 1].Width;
					RemoveNode(i + 1);
					i--;
				}

			return 1;
		}

		public int RectFits(int i, int w, int h)
		{
			var x = (int)Nodes[i].X;
			var y = (int)Nodes[i].Y;
			if (x + w > Width)
				return -1;
			var spaceLeft = w;
			while (spaceLeft > 0)
			{
				if (i == NodesNumber)
					return -1;
				y = Math.Max(y, Nodes[i].Y);
				if (y + h > Height)
					return -1;
				spaceLeft -= Nodes[i].Width;
				++i;
			}

			return y;
		}

		public int AddRect(int rw, int rh, int* rx, int* ry)
		{
			var besth = Height;
			var bestw = Width;
			var besti = -1;
			var bestx = -1;
			var besty = -1;
			for (var i = 0; i < NodesNumber; i++)
			{
				var y = RectFits(i, rw, rh);
				if (y != -1)
					if (y + rh < besth || y + rh == besth && Nodes[i].Width < bestw)
					{
						besti = i;
						bestw = Nodes[i].Width;
						besth = y + rh;
						bestx = Nodes[i].X;
						besty = y;
					}
			}

			if (besti == -1)
				return 0;
			if (AddSkylineLevel(besti, bestx, besty, rw, rh) == 0)
				return 0;
			*rx = bestx;
			*ry = besty;
			return 1;
		}
	}
}