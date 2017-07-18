using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind571 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InvincibleStratagemFromLowerIntelligence = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InvincibleStratagemFromLowerIntelligence = false;
        }
    }
}

