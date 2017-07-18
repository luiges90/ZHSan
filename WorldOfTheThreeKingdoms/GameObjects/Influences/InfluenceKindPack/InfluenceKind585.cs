using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind585 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InevitableRumourOnLowerIntelligence = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InevitableRumourOnLowerIntelligence = false;
        }
    }
}

