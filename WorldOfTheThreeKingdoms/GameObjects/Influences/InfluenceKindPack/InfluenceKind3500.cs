using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3500 : InfluenceKind
    {
        private float rate = 1f;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.CommandTrainingFacilityRate += rate;
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
            architecture.CommandTrainingFacilityRate -= rate;
        }

        public override double AIFacilityValue(Architecture a)
        {
            return 1;
        }
    }
}

