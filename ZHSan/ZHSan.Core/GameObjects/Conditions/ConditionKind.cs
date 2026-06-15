using System.Runtime.Serialization;

namespace GameObjects.Conditions;

[DataContract]
public class ConditionKind : GameObject
{
    protected static Person markedPerson = null;

    public virtual bool CheckConditionKind(Condition condition, Architecture architecture)
    {
        if (ID < ConditionKindIds.PersonRangeStart || ConditionKindIds.IsInLeaderRange(ID))
        {
            var mayor = architecture.Mayor;

            return mayor != null && CheckConditionKind(condition, mayor);
        }

        if (ConditionKindIds.IsInFactionRange(ID))
        {
            var faction = architecture.BelongedFaction;

            return faction != null && CheckConditionKind(condition, faction);
        }

        return false;
    }

    public virtual bool CheckConditionKind(Condition condition, Faction faction)
    {
        if (ID < ConditionKindIds.PersonRangeStart || ConditionKindIds.IsInLeaderRange(ID))
        {
            var leader = faction.Leader;

            return leader != null && CheckConditionKind(condition, leader);
        }

        return false;
    }

    public virtual bool CheckConditionKind(Condition condition, Person person)
    {
        if (ConditionKindIds.IsInArchitectureRange(ID))
        {
            var arch = person.LocationArchitecture;

            return arch != null && CheckConditionKind(condition, arch);
        }

        if (ConditionKindIds.IsInFactionRange(ID))
        {
            var faction = person.BelongedFaction;

            return faction != null && CheckConditionKind(condition, faction);
        }

        return false;
    }

    public virtual bool CheckConditionKind(Condition condition, Troop troop)
    {
        if (ConditionKindIds.IsInFactionRange(ID))
        {
            var faction = troop.BelongedFaction;

            return faction != null && CheckConditionKind(condition, faction);
        }

        return false;
    }
}