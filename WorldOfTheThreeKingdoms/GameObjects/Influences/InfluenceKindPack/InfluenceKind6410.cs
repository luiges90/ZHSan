using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6410 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Architecture a)
        {
            a.facilityEnduranceIncrease += this.increment;
        }

        public override void PurifyInfluenceKind(Architecture a)
        {
            a.facilityEnduranceIncrease -= this.increment;
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

        public override double AIFacilityValue(Architecture a)
        {
            if (a.FacilityCount <= 0) return -1;
            return (a.FrontLine ? (1 - this.increment) * 2 : 0.01) * (a.FrontLine ? 2 : 1) * (a.HostileLine ? 2 : 1) * (a.CriticalHostile ? 2 : 1);
        }
    }
}

