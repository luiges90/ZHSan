using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind5050 : ConditionKind
    {
        private int val;

        public override bool CheckConditionKind(Faction faction)
        {
            return Session.Current.Scenario.Troops.Count >= val;
        }

        public override bool CheckConditionKind(Architecture architecture)
        {
            return Session.Current.Scenario.Troops.Count >= val;
        }

        public override bool CheckConditionKind(Person person)
        {
            return Session.Current.Scenario.Troops.Count >= val;
        }

        public override bool CheckConditionKind(Troop troop)
        {
            return Session.Current.Scenario.Troops.Count >= val;
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

