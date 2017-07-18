using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind272 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop != null)
            {
                troop.YesOrNoOfObliqueStratagem = true;
            }
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            if (troop != null)
            {
                troop.YesOrNoOfObliqueStratagem = false;
            }
        }
    }
}

