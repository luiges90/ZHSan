using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect221 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            person.Spouse.Spouse = null;
            person.Spouse = null;
        }

    }
}

