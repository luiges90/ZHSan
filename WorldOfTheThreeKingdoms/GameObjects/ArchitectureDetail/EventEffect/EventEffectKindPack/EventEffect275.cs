using GameObjects;
using GameObjects.PersonDetail;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{


    [DataContract]public class EventEffect275 : EventEffectKind
    {
        
        public override void ApplyEffectKind(Person person, Event e)
        {
            if (person.BelongedFaction != null && person.LocationArchitecture != null && person.BelongedCaptive == null)
            {
                PersonGeneratorType type = person.Scenario.GameCommonData.AllPersonGeneratorTypes[GameObject.Random(person.Scenario.GameCommonData.AllPersonGeneratorTypes.Count)] as PersonGeneratorType;
                person.LocationArchitecture.GenerateOfficer(type, true);
            }
        }

    }
}
