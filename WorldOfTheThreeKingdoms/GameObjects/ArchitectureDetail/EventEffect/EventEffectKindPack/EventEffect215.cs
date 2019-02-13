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
                person.DefinedPartner = person.BelongedFactionWithPrincess.LeaderID;
                person.BelongedFactionWithPrincess.Leader.DefinedPartner = person.ID;

                if (!person.DefinedPartnersList.GameObjects.Contains(person.BelongedFactionWithPrincess.Leader))
                {
                    person.DefinedPartnersList.Add(person.BelongedFactionWithPrincess.Leader);
                }
                if (!person.BelongedFactionWithPrincess.Leader.DefinedPartnersList.GameObjects.Contains(person))
                {
                    person.BelongedFactionWithPrincess.Leader.DefinedPartnersList.Add(person);
                }

            }
            else
            {
                person.DefinedPartner = person.ID;
            }
        }

    }
}

