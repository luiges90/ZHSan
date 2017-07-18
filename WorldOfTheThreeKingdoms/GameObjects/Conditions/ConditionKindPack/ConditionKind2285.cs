using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind2285 : ConditionKind
    {
        private int val = 0;

        public override bool CheckConditionKind(Architecture a)
        {
            return a.Food < val;
        }

        public override bool CheckConditionKind(Faction faction)
        {
            return faction.Food < val;
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

