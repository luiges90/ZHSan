using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind405 : InfluenceKind
    {
        private int decrement;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.ChanceDecrementOfCriticalStrike += this.decrement;
        }

        public override void ApplyInfluenceKind(Architecture a)
        {
            a.ChanceDecrementOfCriticalStrike += this.decrement;
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
            troop.ChanceDecrementOfCriticalStrike -= this.decrement;
        }

        public override void PurifyInfluenceKind(Architecture a)
        {
            a.ChanceDecrementOfCriticalStrike -= this.decrement;
        }
    }
}

