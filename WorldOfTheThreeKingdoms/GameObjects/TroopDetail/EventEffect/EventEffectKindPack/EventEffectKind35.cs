using GameObjects;
using GameObjects.Animations;
using GameObjects.TroopDetail.EventEffect;
using System;


using System.Runtime.Serialization;namespace GameObjects.TroopDetail.EventEffect.EventEffectKindPack
{

    [DataContract]public class EventEffectKind35 : EventEffectKind
    {
        private float lostCount;

        public override void ApplyEffectKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                int number = person.LocationTroop.Army.LoseInjuredTroop(lostCount);
                /*if (number != 0)
                {
                    person.LocationTroop.RefreshOffence();
                    person.LocationTroop.RefreshDefence();
                    person.LocationTroop.IncrementNumberList.AddNumber(number, CombatNumberKind.人数, person.LocationTroop.Position);
                    person.LocationTroop.ShowNumber = true;
                }*/
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.lostCount = float.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

