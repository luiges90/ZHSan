using GameObjects;
using GameObjects.Animations;
using GameObjects.TroopDetail.EventEffect;
using System;


using System.Runtime.Serialization;namespace GameObjects.TroopDetail.EventEffect.EventEffectKindPack
{

    [DataContract]public class EventEffectKind45 : EventEffectKind
    {
        private int change;

        public override void ApplyEffectKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                person.LocationTroop.zijin += change;
                if (person.LocationTroop.zijin < 0) person.LocationTroop.zijin = 0;
                if (person.LocationTroop.zijin > person.LocationTroop.Army.Kind.zijinshangxian)
                    person.LocationTroop.zijin = person.LocationTroop.Army.Kind.zijinshangxian;
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

