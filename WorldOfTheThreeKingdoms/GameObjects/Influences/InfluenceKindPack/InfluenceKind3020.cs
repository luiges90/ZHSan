using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3020 : InfluenceKind
    {
        private float rate = 0f;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.RateIncrementOfMonthFund += this.rate;
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

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.RateIncrementOfMonthFund -= this.rate;
        }

        public override double AIFacilityValue(Architecture a)
        {
            return (1 - Math.Pow((double)a.Fund / a.FundCeiling, 0.5) + (a.IsFundEnough ? 0 : 0.5) + (a.IsFundAbundant ? 0 : 0.5) + (a.IsFundIncomeEnough ? 0 : 1000))
                * (a.ExpectedFund * this.rate / 200.0);
        }
    }
}

