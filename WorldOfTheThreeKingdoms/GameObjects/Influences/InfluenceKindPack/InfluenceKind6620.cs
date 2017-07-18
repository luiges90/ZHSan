using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6620 : InfluenceKind
    {
        private float increment;

        public override void ApplyInfluenceKind(Architecture p)
        {
            if (p.BelongedFaction != null)
            {
                p.BelongedFaction.techniqueTimeRateDecrease.Add(this.increment);
            }
        }

        public override void PurifyInfluenceKind(Architecture p)
        {
            if (p.BelongedFaction != null)
            {
                p.BelongedFaction.techniqueTimeRateDecrease.Remove(this.increment);
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = float.Parse(parameter);
            }
            catch
            {
            }
        }

        public override double AIFacilityValue(Architecture a)
        {
            return this.increment;
        }
    }
}

