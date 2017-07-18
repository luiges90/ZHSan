using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3010 : InfluenceKind
    {
        private int increment = 0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.IncrementOfMonthFood += this.increment;
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
            architecture.IncrementOfMonthFood -= this.increment;
        }

        public override double AIFacilityValue(Architecture a)
        {
            return (1 - Math.Pow((double)a.Food / a.FoodCeiling, 0.5) + (a.IsFoodEnough ? 0 : 0.5) + (a.IsFoodAbundant ? 0 : 0.5) + (a.IsFoodIncomeEnough ? 0: 1000)) * this.increment / 200000.0;
        }
    }
}

