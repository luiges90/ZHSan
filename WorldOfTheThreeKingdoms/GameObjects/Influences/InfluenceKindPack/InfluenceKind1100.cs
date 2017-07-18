using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind1100 : InfluenceKind
    {
        private float rate = 1f;
        private int type = 0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            switch (this.type)
            {
                case 0:
                    architecture.RateOfNewBubingMilitaryFundCost -= 1 - this.rate;
                    break;

                case 1:
                    architecture.RateOfNewNubingMilitaryFundCost -= 1 - this.rate;
                    break;

                case 2:
                    architecture.RateOfNewQibingMilitaryFundCost -= 1 - this.rate;
                    break;

                case 3:
                    architecture.RateOfNewShuijunMilitaryFundCost -= 1 - this.rate;
                    break;

                case 4:
                    architecture.RateOfNewQixieMilitaryFundCost -= 1 - this.rate;
                    break;
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            switch (this.type)
            {
                case 0:
                    architecture.RateOfNewBubingMilitaryFundCost += 1 - this.rate;
                    break;

                case 1:
                    architecture.RateOfNewNubingMilitaryFundCost += 1 - this.rate;
                    break;

                case 2:
                    architecture.RateOfNewQibingMilitaryFundCost += 1 - this.rate;
                    break;

                case 3:
                    architecture.RateOfNewShuijunMilitaryFundCost += 1 - this.rate;
                    break;

                case 4:
                    architecture.RateOfNewQixieMilitaryFundCost += 1 - this.rate;
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
            return this.rate * (a.FrontLine ? 2 : 1) * (a.HostileLine ? 2 : 1) * (a.CriticalHostile ? 2 : 1);
        }
    }
}

