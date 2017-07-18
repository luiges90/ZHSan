using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind440 : InfluenceKind
    {
        private int increment = 0;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if ((troop != null) && (troop.IncrementOfInjuryRate < this.increment))
            {
                troop.IncrementOfInjuryRate = this.increment;
            }
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

        public override void PurifyInfluenceKind(Troop troop)
        {
            if ((troop != null) && (troop.IncrementOfInjuryRate >= this.increment))
            {
                troop.IncrementOfInjuryRate = 0;
            }
        }
    }
}

