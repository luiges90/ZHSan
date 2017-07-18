using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect210 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            if (person.BelongedFaction == null && person.LocationArchitecture != null)
            {
                person.Status = GameObjects.PersonDetail.PersonStatus.Normal;
                person.ChangeFaction(person.LocationArchitecture.BelongedFaction);
            }
            else if (person.LocationArchitecture != null && person.BelongedCaptive != null)
            {
                Faction f = person.BelongedCaptive.LocationArchitecture.BelongedFaction;
                person.SetBelongedCaptive(null, GameObjects.PersonDetail.PersonStatus.Normal);
                person.ChangeFaction(f);
            }
        }

    }
}

