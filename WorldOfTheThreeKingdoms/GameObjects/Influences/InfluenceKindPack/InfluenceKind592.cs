using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind592 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.NeverBeIntoChaosWhileWaylay = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.NeverBeIntoChaosWhileWaylay = false;
        }
    }
}

