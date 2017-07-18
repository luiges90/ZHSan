using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind595 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.InvincibleRumour = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.InvincibleRumour = false;
        }
    }
}

