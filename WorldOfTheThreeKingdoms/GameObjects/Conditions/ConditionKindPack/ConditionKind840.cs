using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;
using System.Collections.Generic;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind840 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            HashSet<Person> relatedPersons = new HashSet<Person>();
            foreach (Person p in Session.Current.Scenario.Persons)
            {
                if (p.Father == person)
                {
                    relatedPersons.Add(p.Mother);   
                }
                if (p.Mother == person)
                {
                    relatedPersons.Add(p.Father);
                }
            }
            return relatedPersons.Count < number;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.number = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

