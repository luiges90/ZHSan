using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind30 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Person person)
        {
            person.InternalNoFundNeeded = true;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.InternalNoFundNeeded = false;
        }
    }
}

