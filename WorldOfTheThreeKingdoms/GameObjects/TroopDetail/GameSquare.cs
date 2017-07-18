using Microsoft.Xna.Framework;
using System;


namespace GameObjects.TroopDetail
{

    public class GameSquare
    {
        private int g;
        private int h;
        public int Index = -1;
        public GameSquare Parent = null;
        public int PenalizedCost;
        public Point Position;

        public override string ToString()
        {
            return string.Concat(new object[] { "Position=(", this.Position.X, ",", this.Position.Y, ") F=", this.F, " G=", this.G, " H=", this.H });
        }

        public int F
        {
            get
            {
                return (this.G + this.h);
            }
        }

        public int G
        {
            get
            {
                return (this.g + this.PenalizedCost);
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

        public int RealG
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
    }
}

