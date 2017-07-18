using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind6300 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Person person)
        {
             person.pregnantChance += this.increment;
        }


        public override void PurifyInfluenceKind(Person person)
        {
            person.pregnantChance -= this.increment;
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
            return Math.Max(a.Meinvkongjian, a.Feiziliebiao.Count) * (this.increment / 100.0) * 10;
        }
    }
}

