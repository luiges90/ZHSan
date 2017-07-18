using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6430 : InfluenceKind
    {
        private float increment;
        private int disasterId;


        public override void ApplyInfluenceKind(Architecture a)
        {
            if (a.disasterDamageRateDecrease.ContainsKey(disasterId))
            {
                a.disasterDamageRateDecrease[disasterId] += this.increment;
            }
            else
            {
                a.disasterDamageRateDecrease[disasterId] = this.increment;
            }
        }


        public override void PurifyInfluenceKind(Architecture a)
        {
            if (a.disasterDamageRateDecrease.ContainsKey(disasterId))
            {
                a.disasterDamageRateDecrease[disasterId] -= this.increment;
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.disasterId = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.increment = float.Parse(parameter);
            }
            catch
            {
            }
        }

        public override double AIFacilityValue(Architecture a)
        {
            return this.increment / 10;
        }
    }
}

