using System.Runtime.Serialization;
using GameObjects.Influences;

namespace GameObjects.ArchitectureDetail.EventEffect;

[DataContract]
public class EventEffect1250 : EventEffectKind
{
    public override void ApplyEffectKind(EventEffect eventEffect, Architecture arch, Event e)
    {
        arch.Characteristics.PurifyInfluence(arch, Applier.Characteristics, 0);
        arch.Characteristics.Remove(eventEffect.GetIntParam());
        arch.Characteristics.ApplyInfluence(arch, Applier.Characteristics, 0);
    }
}