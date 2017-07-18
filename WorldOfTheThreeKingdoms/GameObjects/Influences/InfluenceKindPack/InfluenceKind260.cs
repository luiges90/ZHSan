using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind260 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Person person)
        {
            person.ImmunityOfCaptive = true;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.ImmunityOfCaptive = false;
        }
    }
}

