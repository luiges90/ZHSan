using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind280 : InfluenceKind
    {
        public override bool IsVaild(Person person)
        {
            return ((person.BelongedFaction != null) && (person.BelongedFaction.Leader == person));
        }
    }
}

