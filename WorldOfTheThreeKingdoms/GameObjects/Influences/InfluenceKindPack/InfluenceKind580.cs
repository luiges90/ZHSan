using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind580 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InevitableGongxinOnLowerIntelligence = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InevitableGongxinOnLowerIntelligence = false;
        }
    }
}

