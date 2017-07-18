using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind388 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.BaseNoCounterAttack = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.BaseNoCounterAttack = false;
        }

    }
}

