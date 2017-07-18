using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3260 : InfluenceKind
    {
        private double rate = 0.0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.RateIncrementOfPopulationDevelop += this.rate;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.rate = double.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.RateIncrementOfPopulationDevelop -= this.rate;
        }

        public override double AIFacilityValue(Architecture a)
        {
            if (!a.Kind.HasPopulation) return -1;
            return (1 - Math.Pow((double) a.Population / a.PopulationCeiling, 0.5)) * (0.001 / a.PopulationDevelopingRate) * (this.rate * 10000.0);
        }
    }
}

