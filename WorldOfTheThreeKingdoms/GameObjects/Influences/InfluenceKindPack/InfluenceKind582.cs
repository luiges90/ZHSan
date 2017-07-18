using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind582 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InevitableChaosOnWaylay = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InevitableChaosOnWaylay = false;
        }
    }
}

