using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind530 : ConditionKind
    {
        private int personID;

        public override bool CheckConditionKind(Person person)
        {
            Person p1 = Session.Current.Scenario.Persons.GetGameObject(personID) as Person;
            return (person.BelongedFactionWithPrincess != null && p1.BelongedFactionWithPrincess != null && person.BelongedFactionWithPrincess == p1.BelongedFactionWithPrincess);
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.personID = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}
