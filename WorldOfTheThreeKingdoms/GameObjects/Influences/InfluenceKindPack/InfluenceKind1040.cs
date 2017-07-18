using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind1040 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.TroopershipAvailable = true;
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.TroopershipAvailable = false;
        }

    }
}

