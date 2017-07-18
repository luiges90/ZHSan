using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind1050 : InfluenceKind
    {
        private float rate;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.RateIncrementOfPopulationCeiling += this.rate;
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.RateIncrementOfPopulationCeiling -= this.rate;
        }

        public override void InitializeParameter(string parameter)
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

