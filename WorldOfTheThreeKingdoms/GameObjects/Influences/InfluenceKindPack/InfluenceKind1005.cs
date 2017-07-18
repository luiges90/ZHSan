using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind1005 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.IncrementOfMoraleCeiling += this.increment;
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.IncrementOfMoraleCeiling -= this.increment;
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
    }
}

