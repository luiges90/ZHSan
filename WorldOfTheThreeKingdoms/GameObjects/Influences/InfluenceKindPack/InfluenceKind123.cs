using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind123 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Architecture person)
        {
            person.DayAvoidPopulationEscape = true;
        }

        public override void PurifyInfluenceKind(Architecture person)
        {
            person.DayAvoidPopulationEscape = false;
        }
    }
}

