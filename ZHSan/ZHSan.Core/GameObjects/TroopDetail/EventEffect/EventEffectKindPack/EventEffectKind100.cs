using GameManager;
using GameObjects.Influences;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail.EventEffect.EventEffectKindPack;

[DataContract]
public class EventEffectKind100 : EventEffectKind
{
    public override void ApplyEffectKind(EventEffect eventEffect, Person person)
    {
        var troop = person.LocationTroop;

        if (troop != null)
        {
            var influence = Session.Current.Scenario.GameCommonData.AllInfluences.Get(eventEffect.GetIntParam());
            if (influence != null)
            {
                troop.EventInfluences.Add(influence);
                influence.ApplyInfluence(troop, Applier.Event, 0);
            }
        }
    }

    public override void ApplyEffectKind(EventEffect eventEffect, Troop troop)
    {
        var influence = Session.Current.Scenario.GameCommonData.AllInfluences.Get(eventEffect.GetIntParam());
        if (influence != null)
        {
            troop.EventInfluences.Add(influence);
            influence.ApplyInfluence(troop, Applier.Event, 0);
        }
    }
}