using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind700 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Person person)
        {
            return GameObject.Chance(this.number);
        }

        public override bool CheckConditionKind(Architecture architecture)
        {
            return GameObject.Chance(this.number);
        }

        public override bool CheckConditionKind(Faction faction)
        {
            return GameObject.Chance(this.number);
        }

        public override bool CheckConditionKind(Troop troop)
        {
            return GameObject.Chance(this.number);
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

