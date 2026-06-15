using GameManager;
using System.Runtime.Serialization;

namespace GameObjects.Conditions.ConditionKindPack;

[DataContract]
public class ConditionKind1900 : ConditionKind
{
    public override bool CheckConditionKind(Condition condition, Troop troop)
    {
        Condition c = Session.Current.Scenario.GameCommonData.AllConditions.Get(condition.GetIntParam());
        if (c != null)
        {
            return c.CheckCondition(troop.Leader);
        }

        return false;
    }
}