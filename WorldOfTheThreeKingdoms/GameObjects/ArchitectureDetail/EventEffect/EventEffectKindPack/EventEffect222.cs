using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect222 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            if (person.BelongedFactionWithPrincess != null)
            {
                person.Brothers.Add(person.BelongedFaction.Leader);
                person.BelongedFaction.Leader.Brothers.Add(person);
            }
        }

    }
}

