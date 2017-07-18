using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind593 : InfluenceKind
    {


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.LowerLevelInformationWhileInvestigated = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.LowerLevelInformationWhileInvestigated = false;
        }
    }
}

