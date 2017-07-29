using GameObjects;
using GameObjects.Conditions;
using System;
using GameManager;

using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind500 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            Treasure t = Session.Current.Scenario.Treasures.GetGameObject(number) as Treasure;
            return person.Treasures.GameObjects.Contains(t);
        }

        public override bool CheckConditionKind(Architecture architecture)
        {
            foreach (Person p in architecture.Persons)
            {
                if (this.CheckConditionKind(p))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool CheckConditionKind(Faction faction)
        {
            foreach (Person p in faction.Persons)
            {
                if (this.CheckConditionKind(p))
                {
                    return true;
                }
            }
            return false;
        }

        public override bool CheckConditionKind(Troop troop)
        {
            foreach (Person p in troop.Persons)
            {
                if (this.CheckConditionKind(p))
                {
                    return true;
                }
            }
            return false;
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

