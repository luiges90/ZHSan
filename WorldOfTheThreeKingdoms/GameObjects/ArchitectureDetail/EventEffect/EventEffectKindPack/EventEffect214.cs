using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect214 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            person.ToDeath(null, person.BelongedFaction);
        }

    }
}

