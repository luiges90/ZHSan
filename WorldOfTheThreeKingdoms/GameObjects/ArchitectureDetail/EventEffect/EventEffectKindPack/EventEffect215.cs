using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect215 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            person.IsPregnant = true;
            person.PregnancyDayCount = 0;
            if (person.BelongedFactionWithPrincess != null)
            {
                person.suoshurenwu = person.BelongedFactionWithPrincess.LeaderID;
                person.BelongedFactionWithPrincess.Leader.suoshurenwu = person.ID;

                if (!person.PartnersList.GameObjects.Contains(person.BelongedFactionWithPrincess.Leader))
                {
                    person.PartnersList.Add(person.BelongedFactionWithPrincess.Leader);
                }
                if (!person.BelongedFactionWithPrincess.Leader.PartnersList.GameObjects.Contains(person))
                {
                    person.BelongedFactionWithPrincess.Leader.PartnersList.Add(person);
                }

            }
            else
            {
                person.suoshurenwu = person.ID;
            }
        }

    }
}

