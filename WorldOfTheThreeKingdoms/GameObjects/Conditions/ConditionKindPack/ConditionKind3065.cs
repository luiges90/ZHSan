using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind3065 : ConditionKind
    {
        private int val;

        public override bool CheckConditionKind(Faction faction)
        {
            return !faction.BaseMilitaryKinds.MilitaryKinds.ContainsKey(val);
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

