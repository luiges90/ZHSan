using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind122 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Architecture person)
        {
            person.DayAvoidInfluenceByBattle = true;
        }

        public override void PurifyInfluenceKind(Architecture person)
        {
            person.DayAvoidInfluenceByBattle = false;
        }
    }
}

