using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind600 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.ChaosLastOneDay = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.ChaosLastOneDay = false;
        }
    }
}

