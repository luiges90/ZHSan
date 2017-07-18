using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind591 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InvincibleRaoluan = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InvincibleRaoluan = false;
        }
    }
}

