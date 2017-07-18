using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6840 : InfluenceKind
    {
        private int rate;

        public override void ApplyInfluenceKind(Person person)
        {
            person.IntelligenceExperienceIncrease += this.rate;
        }

        public override void PurifyInfluenceKind(Person person)
        {
            person.IntelligenceExperienceIncrease -= this.rate;
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

