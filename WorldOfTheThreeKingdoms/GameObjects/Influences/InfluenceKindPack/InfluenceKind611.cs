using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind611 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.ScatteredShootingOblique = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.ScatteredShootingOblique = false;
        }
    }
}

