using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6775 : InfluenceKind
    {
        private int rate;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.StrengthExperienceIncrease += rate;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.StrengthExperienceIncrease -= rate;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.rate = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

