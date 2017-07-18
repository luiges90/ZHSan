using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3080 : InfluenceKind
    {
        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.NoCounterStrikeInArchitecture = true;
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.NoCounterStrikeInArchitecture = false;
        }

        public override double AIFacilityValue(Architecture a)
        {
            if (!a.FrontLine) return -1;
            int counterStrikeMilitaryCount = 0;
            foreach (Military m in a.Militaries)
            {
                if (m.Kind.BeCountered)
                {
                    counterStrikeMilitaryCount++;
                }
            }
            return (double)counterStrikeMilitaryCount / a.EffectiveMilitaryCount * 2 * (a.FrontLine ? 2 : 1) * (a.HostileLine ? 2 : 1) * (a.CriticalHostile ? 2 : 1);
        }
    }
}

