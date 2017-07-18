using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind606 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.NotAfraidOfFire = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.NotAfraidOfFire = false;
        }
    }
}

