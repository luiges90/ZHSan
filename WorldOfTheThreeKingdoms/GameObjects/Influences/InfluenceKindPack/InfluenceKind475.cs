using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind475 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfSearch = true;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfSearch = false;
        }
    }
}

