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
    class ConditionKind1910 : ConditionKind
    {
        private int number = 0;

        public override bool CheckConditionKind(Troop troop)
        {
            Condition c = Session.Current.Scenario.GameCommonData.AllConditions.GetCondition(number);
            if (c != null)
            {
                foreach (Person p in troop.Persons)
                {
                    if (c.CheckCondition(p))
                    {
                        return true;
                    }
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
