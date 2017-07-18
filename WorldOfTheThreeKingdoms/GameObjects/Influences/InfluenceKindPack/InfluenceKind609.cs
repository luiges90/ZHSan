using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind609 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.DefenceNoChangeOnChaos = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.DefenceNoChangeOnChaos = false;
        }
    }
}

