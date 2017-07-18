using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind124 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Architecture person)
        {
            person.DayAvoidInternalDecrementOnBattle = true;
        }

        public override void PurifyInfluenceKind(Architecture person)
        {
            person.DayAvoidInternalDecrementOnBattle = false;
        }
    }
}

