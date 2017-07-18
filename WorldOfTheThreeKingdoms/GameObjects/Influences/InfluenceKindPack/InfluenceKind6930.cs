using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6930 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.stratagemInjuryDecrease += this.increment;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.stratagemInjuryDecrease -= this.increment;
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
    }
}

