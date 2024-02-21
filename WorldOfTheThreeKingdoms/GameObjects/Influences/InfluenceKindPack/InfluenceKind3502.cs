using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3502 : InfluenceKind
    {
        private float rate = 1f;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            
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

        }

        public override double AIFacilityValue(Architecture a)
        {
            return 1;
        }
    }
}

