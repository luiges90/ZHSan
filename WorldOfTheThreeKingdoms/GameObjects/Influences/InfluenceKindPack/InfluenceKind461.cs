using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind461 : InfluenceKind
    {
        private float increment;

        public override void ApplyInfluenceKind(Person person)
        {
            person.RateIncrementOfJailBreakAbility += this.increment;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = float.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

