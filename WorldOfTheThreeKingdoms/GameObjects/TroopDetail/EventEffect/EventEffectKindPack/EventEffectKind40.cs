using GameObjects;
using GameObjects.Animations;
using GameObjects.TroopDetail.EventEffect;
using System;


using System.Runtime.Serialization;namespace GameObjects.TroopDetail.EventEffect.EventEffectKindPack
{

    [DataContract]public class EventEffectKind40 : EventEffectKind
    {
        private int change;

        public override void ApplyEffectKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                person.LocationTroop.Food += change;
                if (person.LocationTroop.Food < 0) person.LocationTroop.Food = 0;
                if (person.LocationTroop.Food > person.LocationTroop.FoodMax) person.LocationTroop.Food = person.LocationTroop.FoodMax;
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.change = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

