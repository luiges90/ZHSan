using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3514 : InfluenceKind
    {
        private float rate = 1f;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.SiegeTrainingFacilityRate += rate;
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
            architecture.SiegeTrainingFacilityRate-= rate;
        }

        public override double AIFacilityValue(Architecture a)
        {
            return 1;
        }
    }
}

