using GameManager;
using System.Runtime.Serialization;

namespace GameObjects.Conditions.ConditionKindPack;

[DataContract]
class ConditionKind2910 : ConditionKind
{
    public override bool CheckConditionKind(Condition condition, Architecture arch)
    {
        Condition c = Session.Current.Scenario.GameCommonData.AllConditions.Get(condition.GetIntParam());

        if (c != null && arch.Mayor != null)
        {
            return c.CheckCondition(arch.Mayor);
        }

        return false;
    }
}