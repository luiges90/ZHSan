using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6150 : InfluenceKind
    {
        private int rate;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.CommandExperienceIncrease += rate;
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.CommandExperienceIncrease -= rate;
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

        public override double AIFacilityValue(Architecture a)
        {
            return this.rate * a.PersonCount / 200;
        }
    }
}

