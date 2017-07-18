using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2350 : InfluenceKind
    {
        private float rate = 0f;

        public override void ApplyInfluenceKind(Faction faction)
        {
            faction.RateOfCombativityRecoveryAfterAttacked += this.rate;
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

        public override void PurifyInfluenceKind(Faction faction)
        {
            faction.RateOfCombativityRecoveryAfterAttacked -= this.rate;
        }
    }
}

