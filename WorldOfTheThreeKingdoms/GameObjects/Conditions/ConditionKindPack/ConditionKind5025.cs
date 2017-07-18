using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind5025 : ConditionKind
    {
        private int val;

        public override bool CheckConditionKind(Faction faction)
        {
            return faction.Scenario.Date.Month < val;
        }

        public override bool CheckConditionKind(Architecture architecture)
        {
            return architecture.Scenario.Date.Month < val;
        }

        public override bool CheckConditionKind(Person person)
        {
            return person.Scenario.Date.Month < val;
        }

        public override bool CheckConditionKind(Troop troop)
        {
            return troop.Scenario.Date.Month < val;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.val = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

