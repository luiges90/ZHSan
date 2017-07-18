using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect213 : EventEffectKind
    {
        public override void ApplyEffectKind(Person person, Event e)
        {
            if (person.BelongedCaptive != null)
            {
                person.SetBelongedCaptive(null, GameObjects.PersonDetail.PersonStatus.Princess);
            }
            if (person.LocationArchitecture != null)
            {
                person.Status = GameObjects.PersonDetail.PersonStatus.Princess;
            }
        }

    }
}

