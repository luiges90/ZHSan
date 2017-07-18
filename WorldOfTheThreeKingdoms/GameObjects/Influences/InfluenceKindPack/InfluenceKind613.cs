using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind613 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.CanAttackAfterRout = true;
        }


        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.CanAttackAfterRout = false;
        }
    }
}

