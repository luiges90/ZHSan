using Microsoft.Xna.Framework;
using System;

namespace PersonBubble
{

    internal class PositionCount
    {
        internal int Count;
        internal Point Position;

        public PositionCount(Point position, int count)
        {
            this.Position = position;
            this.Count = count;
        }
    }
}

