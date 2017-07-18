using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind515 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            return person.TreasureCount < number;
        }

        public override bool CheckConditionKind(Architecture architecture)
        {
            int result = 0;
            foreach (Person p in architecture.Persons)
            {
                result += p.TreasureCount;
            }
            return result < number;
        }

        public override bool CheckConditionKind(Faction faction)
        {
            int result = 0;
            foreach (Person p in faction.Persons)
            {
                result += p.TreasureCount;
            }
            return result < number;
        }

        public override bool CheckConditionKind(Troop troop)
        {
            int result = 0;
            foreach (Person p in troop.Persons)
            {
                result += p.TreasureCount;
            }
            return result < number;
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

