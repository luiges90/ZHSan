using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2340 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Faction faction)
        {
            faction.IncrementOfChaosDaysAfterPhisicalAttack += this.increment;
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

        public override void PurifyInfluenceKind(Faction faction)
        {
            faction.IncrementOfChaosDaysAfterPhisicalAttack -= this.increment;
        }
    }
}

