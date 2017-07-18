using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind386 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.BaseAttackAllOffenceArea = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.BaseAttackAllOffenceArea = false;
        }

    }
}

