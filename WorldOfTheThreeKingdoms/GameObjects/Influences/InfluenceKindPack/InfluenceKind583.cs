using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind583 : InfluenceKind
    {
 
        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.HighLevelInformationOnInvestigate = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.HighLevelInformationOnInvestigate = false;
        }
    }
}

