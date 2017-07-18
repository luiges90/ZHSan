using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind471 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfJailBreak = true;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfJailBreak = false;
        }
    }
}

