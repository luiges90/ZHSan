using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3200 : InfluenceKind
    {
        private int increment = 0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.IncrementOfAgriculturePerDay += this.increment;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.IncrementOfAgriculturePerDay -= this.increment;
        }

        public override double AIFacilityValue(Architecture a)
        {
            if (!a.Kind.HasAgriculture) return -1;
            return (a.AgricultureCeiling - a.Agriculture) * this.increment + 1;
        }
    }
}

