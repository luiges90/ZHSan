using Microsoft.Xna.Framework;
using System;


namespace GameObjects.MapDetail
{

    public class RoutewaySquare
    {
        private float consumptionRate;
        private int g;
        private int h;
        public int Index = -1;
        public RoutewaySquare Parent = null;
        public int PenalizedCost;
        public Point Position;

        public override string ToString()
        {
            return string.Concat(new object[] { "Position=(", this.Position.X, ",", this.Position.Y, ") F=", this.F });
        }

        public float ConsumptionRate
        {
            get
            {
                return this.consumptionRate;
            }
            set
            {
                this.consumptionRate = value;
            }
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

