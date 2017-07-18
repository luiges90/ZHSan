using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind474 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfGossip = true;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfGossip = false;
        }
    }
}

