using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind1300 : InfluenceKind
    {
        private float rate = 1f;
        private int type = 0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            switch (this.type)
            {
                case 0:
                    architecture.RateIncrementOfNewBubingTroopDefence += this.rate;
                    break;

                case 1:
                    architecture.RateIncrementOfNewNubingTroopDefence += this.rate;
                    break;

                case 2:
                    architecture.RateIncrementOfNewQibingTroopDefence += this.rate;
                    break;

                case 3:
                    architecture.RateIncrementOfNewShuijunTroopDefence += this.rate;
                    break;

                case 4:
                    architecture.RateIncrementOfNewQixieTroopDefence += this.rate;
                    break;
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            switch (this.type)
            {
                case 0:
                    architecture.RateIncrementOfNewBubingTroopDefence -= this.rate;
                    break;

                case 1:
                    architecture.RateIncrementOfNewNubingTroopDefence -= this.rate;
                    break;

                case 2:
                    architecture.RateIncrementOfNewQibingTroopDefence -= this.rate;
                    break;

                case 3:
                    architecture.RateIncrementOfNewShuijunTroopDefence -= this.rate;
                    break;

                case 4:
                    architecture.RateIncrementOfNewQixieTroopDefence -= this.rate;
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
    }
}

