using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind1200 : InfluenceKind
    {
        private float rate = 1f;
        private int type = 0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            switch (this.type)
            {
                case 0:
                    architecture.RateIncrementOfNewBubingTroopOffence += this.rate;
                    break;

                case 1:
                    architecture.RateIncrementOfNewNubingTroopOffence += this.rate;
                    break;

                case 2:
                    architecture.RateIncrementOfNewQibingTroopOffence += this.rate;
                    break;

                case 3:
                    architecture.RateIncrementOfNewShuijunTroopOffence += this.rate;
                    break;

                case 4:
                    architecture.RateIncrementOfNewQixieTroopOffence += this.rate;
                    break;
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            switch (this.type)
            {
                case 0:
                    architecture.RateIncrementOfNewBubingTroopOffence -= this.rate;
                    break;

                case 1:
                    architecture.RateIncrementOfNewNubingTroopOffence -= this.rate;
                    break;

                case 2:
                    architecture.RateIncrementOfNewQibingTroopOffence -= this.rate;
                    break;

                case 3:
                    architecture.RateIncrementOfNewShuijunTroopOffence -= this.rate;
                    break;

                case 4:
                    architecture.RateIncrementOfNewQixieTroopOffence -= this.rate;
                    break;
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.type = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.rate = float.Parse(parameter);
            }
            catch
            {
            }
        }

        public override double AIFacilityValue(Architecture a)
        {
            return this.rate * 4 * (a.FrontLine ? 1 : 0.001) * (a.FrontLine ? 2 : 1) * (a.HostileLine ? 2 : 1) * (a.CriticalHostile ? 2 : 1);
        }
    }
}

