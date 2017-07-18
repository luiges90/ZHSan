using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3140 : InfluenceKind
    {
        private float rate = 1f;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.RateOfFacilityEnduranceDown -= 1 - this.rate;
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
            architecture.RateOfFacilityEnduranceDown += 1 - this.rate;
        }

        public override double AIFacilityValue(Architecture a)
        {
            if (a.FacilityCount <= 0) return -1;
            return (a.FrontLine ? (1 - this.rate) * 2 : 0.01) * (a.FrontLine ? 2 : 1) * (a.HostileLine ? 2 : 1) * (a.CriticalHostile ? 2 : 1);
        }
    }
}

