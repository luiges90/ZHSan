using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect212 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            if (person.LocationArchitecture != null)
            {
                Captive captive = Captive.Create(base.Scenario, person, person.LocationArchitecture.BelongedFaction);
            }
        }

    }
}

