using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6480 : InfluenceKind
    {
        private float rate;

        public override void ApplyInfluenceKind(Architecture p)
        {
            p.TroopTransportDayRate += rate;
        }

        public override void PurifyInfluenceKind(Architecture p)
        {
            p.TroopTransportDayRate -= rate;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.rate = float.Parse(parameter);
            }
            catch
            {
            }
        }

        public override double AIFacilityValue(Architecture a)
        {
            return this.rate * 160;
        }
    }
}

