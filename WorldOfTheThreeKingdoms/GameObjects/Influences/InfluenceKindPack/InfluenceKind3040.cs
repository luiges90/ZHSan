using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3040 : InfluenceKind
    {
        private float rate = 0f;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.RateOfpublic += this.rate;
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
            architecture.RateOfpublic -= this.rate;
        }

        public override double AIFacilityValue(Architecture a)
        {
            return (a.CompletelyDeveloped ? 2 : 10) * this.rate;
        }
    }
}

