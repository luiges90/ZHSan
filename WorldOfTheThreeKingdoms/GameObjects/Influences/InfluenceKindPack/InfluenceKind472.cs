using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind472 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfDestroy = true;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.InevitableSuccessOfDestroy = false;
        }
    }
}

