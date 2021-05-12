using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind610 : InfluenceKind
    {

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.BaseNoAccidentalInjury)
            {
                troop.NoAccidentalInjury = true;
            }
            troop.BaseNoAccidentalInjury = true;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.BaseNoAccidentalInjury = false;
        }
    }
}

