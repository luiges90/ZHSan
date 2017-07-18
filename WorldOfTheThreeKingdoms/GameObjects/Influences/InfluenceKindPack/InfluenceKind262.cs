using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind262 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Person person)
        {
            person.ImmunityOfDieInBattle = true;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.ImmunityOfDieInBattle = false;
        }
    }
}

