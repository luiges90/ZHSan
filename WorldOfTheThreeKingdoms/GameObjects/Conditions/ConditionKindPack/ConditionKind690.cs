using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind690 : ConditionKind
    {
        
        public override bool CheckConditionKind(Person person)
        {
            if (person.ID == -1)
            {
                person = person.Scenario.Persons[GameObject.Random(person.Scenario.Persons.Count)] as Person;
            }


            if (person.LocationArchitecture != null)
            {
                Faction faction = person.LocationArchitecture.BelongedFaction as Faction;

                if (person.BelongedFaction == faction )
                {
                    return true;
                }
            }
            return false; ;
        }

        
    }
}
