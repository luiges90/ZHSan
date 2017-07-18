using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind2540 : ConditionKind
    {
        private int val;
        private int type;

        public override bool CheckConditionKind(Architecture a)
        {
            int result = 0;
            foreach (Military m in a.Militaries)
            {
                if (m.KindID == type)
                {
                    result++;
                }
            }
            return result >= val;
        }

        public override bool CheckConditionKind(Faction faction)
        {
            int result = 0;
            foreach (Military m in faction.Militaries)
            {
                if (m.KindID == type)
                {
                    result++;
                }
            }
            return result >= val;
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

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.type = int.Parse(parameter);
            }
            catch
            {
            }
        }

    }
}

