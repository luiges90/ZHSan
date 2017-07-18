using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind594 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InvincibleHuogong = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InvincibleHuogong = false;
        }
    }
}

