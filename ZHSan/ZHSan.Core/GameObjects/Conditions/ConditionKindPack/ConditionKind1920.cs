using GameManager;
using System.Runtime.Serialization;

namespace GameObjects.Conditions.ConditionKindPack;

[DataContract]
public class ConditionKind1920 : ConditionKind
{
    public override bool CheckConditionKind(Condition condition, Troop troop)
    {
        Condition c = Session.Current.Scenario.GameCommonData.AllConditions.Get(condition.GetIntParam());
        if (c != null)
        {
            foreach (Person p in troop.Persons)
            {
                if (!c.CheckCondition(p))
                {
                    return false;
                }
            }
            return true;
        }

        return false;
    }
}