using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect217 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            if (person.LocationArchitecture != null && person.Status == GameObjects.PersonDetail.PersonStatus.Princess)
            {
                Architecture originalLocationArch = person.LocationArchitecture;
                person.Status = GameObjects.PersonDetail.PersonStatus.Normal;
            }
        }

    }
}

