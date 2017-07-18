using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6535 : InfluenceKind
    {
        private float rate = 0;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.reduceInjuredOnCritical += this.rate;
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

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.reduceInjuredOnCritical -= this.rate;
        }
    }
}

