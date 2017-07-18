using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind282 : InfluenceKind
    {
        public override bool IsVaild(Person person)
        {
            return ((person.BelongedArchitecture != null) && (person.BelongedArchitecture.Mayor == person));
        }
    }
}

