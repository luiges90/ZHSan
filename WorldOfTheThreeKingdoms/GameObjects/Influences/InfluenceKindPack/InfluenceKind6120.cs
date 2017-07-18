using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6120 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Architecture a)
        {
            a.noEscapeChance += this.increment;
        }

        public override void PurifyInfluenceKind(Architecture a)
        {
            a.noEscapeChance -= this.increment;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override double AIFacilityValue(Architecture a)
        {
            return this.increment * 10 / (double)a.BelongedFaction.PersonCount * (a.FrontLine ? 1 : 0.2);
        }
    }
}

