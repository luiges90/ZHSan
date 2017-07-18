using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind360 : InfluenceKind
    {
        private int decrement;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.MoraleDecrementOfCriticalStrike += this.decrement;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.decrement = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            if (troop != null)
            {
                troop.MoraleDecrementOfCriticalStrike -= this.decrement;
            }
        }
    }
}

