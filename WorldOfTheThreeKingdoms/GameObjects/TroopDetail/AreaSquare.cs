using Microsoft.Xna.Framework;
using System;



namespace GameObjects.TroopDetail
{

    public class AreaSquare
    {
        private int g;
        private int h;
        public int Index = -1;
        public AreaSquare Parent = null;
        public Point Position;

        public override string ToString()
        {
            return string.Concat(new object[] { "Position=(", this.Position.X, ",", this.Position.Y, ") F=", this.F });
        }

        public int F
        {
            get
            {
                return (this.G + this.H);
            }
        }

        public int G
        {
            get
            {
                return this.g;
            }
            set
            {
                this.g = value;
            }
        }

        public int H
        {
            get
            {
                return this.h;
            }
            set
            {
                this.h = value;
            }
        }
    }
}

