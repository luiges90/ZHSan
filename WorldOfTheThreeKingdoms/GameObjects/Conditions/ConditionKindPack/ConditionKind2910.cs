using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTheThreeKingdoms.GameObjects.Conditions.ConditionKindPack
{
    [DataContract]
    class ConditionKind2910 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Architecture a)
        {
            Condition c = Session.Current.Scenario.GameCommonData.AllConditions.GetCondition(number);
            if (c != null && a.Mayor != null)
            {
                return c.CheckCondition(a.Mayor);
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
