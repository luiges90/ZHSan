using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind5030 : ConditionKind
    {
        private int val;

        public override bool CheckConditionKind(Faction faction)
        {
            return faction.Scenario.Date.Day >= val;
        }

        public override bool CheckConditionKind(Architecture architecture)
        {
            return architecture.Scenario.Date.Day >= val;
        }

        public override bool CheckConditionKind(Person person)
        {
            return person.Scenario.Date.Day >= val;
        }

        public override bool CheckConditionKind(Troop troop)
        {
            return troop.Scenario.Date.Day >= val;
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

