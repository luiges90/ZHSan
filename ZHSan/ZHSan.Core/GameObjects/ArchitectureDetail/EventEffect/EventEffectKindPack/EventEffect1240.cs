using GameManager;
using System.Runtime.Serialization;
using GameObjects.Influences;

namespace GameObjects.ArchitectureDetail.EventEffect;

[DataContract]
public class EventEffect1240 : EventEffectKind
{
    public override void ApplyEffectKind(EventEffect eventEffect, Architecture arch, Event e)
    {
        var influence = Session.Current.Scenario.GameCommonData.AllInfluences.Get(eventEffect.GetIntParam());

        arch.Characteristics.PurifyInfluence(arch, Applier.Characteristics, 0);
        arch.Characteristics.Add(influence);
        arch.Characteristics.ApplyInfluence(arch, Applier.Characteristics, 0);
    }
}