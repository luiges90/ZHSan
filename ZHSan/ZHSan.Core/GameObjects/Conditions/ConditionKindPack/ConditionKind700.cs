using System.Runtime.Serialization;

namespace GameObjects.Conditions.ConditionKindPack;

[DataContract]
public class ConditionKind700 : ConditionKind
{
    public override bool CheckConditionKind(Condition condition, Person person)
    {
        return GameObject.GetChance(condition.GetIntParam());
    }

    public override bool CheckConditionKind(Condition condition, Architecture architecture)
    {
        return GameObject.GetChance(condition.GetIntParam());
    }

    public override bool CheckConditionKind(Condition condition, Faction faction)
    {
        return GameObject.GetChance(condition.GetIntParam());
    }

    public override bool CheckConditionKind(Condition condition, Troop troop)
    {
        return GameObject.GetChance(condition.GetIntParam());
    }
}