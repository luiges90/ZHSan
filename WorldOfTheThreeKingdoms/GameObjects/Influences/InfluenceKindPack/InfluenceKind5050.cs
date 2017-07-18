using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind5050 : InfluenceKind
    {
        private int chance = 0;

        public override void ApplyInfluenceKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                person.LocationTroop.ChanceOfDoubleDamage += this.chance;
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.chance = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                person.LocationTroop.ChanceOfDoubleDamage -= this.chance;
            }
        }
    }
}

