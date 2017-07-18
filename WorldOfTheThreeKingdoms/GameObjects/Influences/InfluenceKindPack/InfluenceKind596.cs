using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind596 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InvincibleAttract = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InvincibleAttract = false;
        }
    }
}

