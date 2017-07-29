using GameObjects;
using GameObjects.Conditions;
using System;
using GameManager;

using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind520 : ConditionKind
    {
        private int personID;

        public override bool CheckConditionKind(Person person)
        {
            Person p1 = Session.Current.Scenario.Persons.GetGameObject(personID) as Person;
            return ( person.LocationArchitecture != null && p1.LocationArchitecture  != null && person.LocationArchitecture == p1.LocationArchitecture );
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
