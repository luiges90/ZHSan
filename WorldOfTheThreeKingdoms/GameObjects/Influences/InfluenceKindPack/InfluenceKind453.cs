using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind453 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.OnlyBeDetectedByHighLevelInformation = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.OnlyBeDetectedByHighLevelInformation = false;
        }
    }
}

