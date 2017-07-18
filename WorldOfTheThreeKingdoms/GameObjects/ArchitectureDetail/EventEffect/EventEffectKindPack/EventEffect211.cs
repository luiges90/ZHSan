using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect211 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            if (person.BelongedFaction != null && person.LocationArchitecture != null)
            {
                person.LeaveToNoFaction();
            }
            else if (person.LocationArchitecture != null && person.BelongedCaptive != null)
            {
                person.BelongedCaptive.CaptiveDirectEscape();
            }
        }

    }
}

