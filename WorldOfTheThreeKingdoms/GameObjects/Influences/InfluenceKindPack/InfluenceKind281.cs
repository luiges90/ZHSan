using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind281 : InfluenceKind
    {
        public InfluenceKind281()
        {
            base.TroopLeaderValid = true;
        }

        public override bool IsVaild(Person person)
        {
            return ((person.LocationTroop != null) && (person.LocationTroop.Leader == person));
        }
    }
}

