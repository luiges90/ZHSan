using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect223 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            foreach (Person p in person.Scenario.Persons)
            {
                if (p.Brothers.GameObjects.Contains(p))
                {
                    p.Brothers.Remove(p);
                }
            }
            person.Brothers.Clear();
        }

    }
}

