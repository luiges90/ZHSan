using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6400 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Architecture a)
        {
            a.noFundToSustainFacility = true;
        }

        public override void PurifyInfluenceKind(Architecture a)
        {
            a.noFundToSustainFacility = false;
        }

        public override double AIFacilityValue(Architecture a)
        {
            return a.FacilityMaintenanceCost / 5 - 1;
        }

    }
}

