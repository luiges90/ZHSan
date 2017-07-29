using GameManager;
using GameObjects;
using GameObjects.PersonDetail;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{


    [DataContract]public class EventEffect270 : EventEffectKind
    {
        private int preferredType;

        public override void ApplyEffectKind(Person person, Event e)
        {
            if (person.BelongedFaction != null && person.LocationArchitecture != null && person.BelongedCaptive == null)
            {
                PersonGeneratorType type = Session.Current.Scenario.GameCommonData.AllPersonGeneratorTypes[preferredType] as PersonGeneratorType;
                person.LocationArchitecture.GenerateOfficer(type,true);
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.preferredType = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}
