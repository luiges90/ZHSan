using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect215 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            person.huaiyun = true;
            person.huaiyuntianshu = 0;
            if (person.BelongedFactionWithPrincess != null)
            {
                person.suoshurenwu = person.BelongedFactionWithPrincess.LeaderID;
                person.BelongedFactionWithPrincess.Leader.suoshurenwu = person.ID;

                if (!person.suoshurenwuList.GameObjects.Contains(person.BelongedFactionWithPrincess.Leader))
                {
                    person.suoshurenwuList.Add(person.BelongedFactionWithPrincess.Leader);
                }
                if (!person.BelongedFactionWithPrincess.Leader.suoshurenwuList.GameObjects.Contains(person))
                {
                    person.BelongedFactionWithPrincess.Leader.suoshurenwuList.Add(person);
                }

            }
            else
            {
                person.suoshurenwu = person.ID;
            }
        }

    }
}

