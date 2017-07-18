using GameObjects;
using GameObjects.TroopDetail.EventEffect;
using System;


using System.Runtime.Serialization;namespace GameObjects.TroopDetail.EventEffect.EventEffectKindPack
{

    [DataContract]public class EventEffectKind50 : EventEffectKind
    {
        private float rate = 1f;

        public override void ApplyEffectKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                float num = this.rate - 1f;
                if (num > 0f)
                {
                    person.LocationTroop.IncreaseQuantity((int) (person.LocationTroop.Quantity * num));
                }
                else if (num < 0f)
                {
                    person.LocationTroop.DecreaseQuantity(Math.Abs((int) (person.LocationTroop.Quantity * num)));
                }
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.rate = float.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

