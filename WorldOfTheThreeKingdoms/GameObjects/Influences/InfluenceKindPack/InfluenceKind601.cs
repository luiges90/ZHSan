using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind601 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.NeverBeIntoChaos = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.NeverBeIntoChaos = false;
        }
    }
}

