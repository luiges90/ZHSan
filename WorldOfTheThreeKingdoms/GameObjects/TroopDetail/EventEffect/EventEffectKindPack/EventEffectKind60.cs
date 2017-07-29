using GameGlobal;
using GameManager;
using GameObjects;
using GameObjects.TroopDetail.EventEffect;
using System;


using System.Runtime.Serialization;namespace GameObjects.TroopDetail.EventEffect.EventEffectKindPack
{

    [DataContract]public class EventEffectKind60 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person)
        {
            if (person.LocationTroop != null)
            {
                person.LocationTroop.SetOnFire(Session.Parameters.FireDamageScale * Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(person.LocationTroop.Position).FireDamageRate);
            }
        }
    }
}

