using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3000 : InfluenceKind
    {
        private int increment = 0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.IncrementOfMonthFund += this.increment;
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
            architecture.IncrementOfMonthFund -= this.increment;
        }

        public override double AIFacilityValue(Architecture a)
        {
            return (1 - Math.Pow((double)a.Fund / a.FundCeiling, 0.5) + (a.IsFundEnough ? 0 : 0.5) + (a.IsFundAbundant ? 0 : 0.5) + (a.IsFundIncomeEnough ? 0 : 1000)) * this.increment / 1000.0;
        }
    }
}

