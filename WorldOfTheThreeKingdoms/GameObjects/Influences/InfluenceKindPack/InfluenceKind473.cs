using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind473 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfInstigate = true;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfInstigate = false;
        }
    }
}

