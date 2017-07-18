using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6470 : InfluenceKind
    {
        private float increment;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.enduranceDecreaseRateDrop += this.increment;
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.enduranceDecreaseRateDrop -= this.increment;
        }

        public override void InitializeParameter(string parameter)
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
            return this.increment * (a.HostileLine ? 2 : 1);
        }
    }
}

