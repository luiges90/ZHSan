using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Runtime.InteropServices;

namespace NumberInputerPlugin
{

    [StructLayout(LayoutKind.Sequential)]
    internal struct Number
    {
        internal int Num;
        internal Texture2D Texture;
        internal Rectangle Position;
        internal Rectangle GetDisplayPosition(Point offset)
        {
            return new Rectangle(offset.X + this.Position.X, offset.Y + this.Position.Y, this.Position.Width, this.Position.Height);
        }
    }
}

