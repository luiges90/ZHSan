using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6140 : InfluenceKind
    {
        private int increment;
        private int threshold;

        public override void ApplyInfluenceKind(Architecture a)
        {
            a.captiveLoyaltyFall.Add(new System.Collections.Generic.KeyValuePair<int, int>(this.threshold, this.increment));
        }

        public override void PurifyInfluenceKind(Architecture a)
        {
            a.captiveLoyaltyFall.Remove(new System.Collections.Generic.KeyValuePair<int, int>(this.threshold, this.increment));
        }

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.increment = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.threshold = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override double AIFacilityValue(Architecture a)
        {
            if (this.threshold > 110 && a.FrontLine) return 100;
            return this.increment * this.threshold * 2 / (double)a.BelongedFaction.PersonCount * (a.FrontLine ? 1 : 0.2);
        }
    }
}

