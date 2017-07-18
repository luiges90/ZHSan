using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind590 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InvincibleGongxin = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InvincibleGongxin = false;
        }
    }
}

