using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect2131 : EventEffectKind
    {
        public override void ApplyEffectKind(Faction f, Event e)
        {
            f.IsAlien = false;
        }
    }
}

