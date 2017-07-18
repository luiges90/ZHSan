using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind570 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InevitableStratagemOnLowerIntelligence = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InevitableStratagemOnLowerIntelligence = false;
        }
    }
}

