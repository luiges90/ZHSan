using System.Runtime.Serialization;

namespace GameObjects.Conditions.ConditionKindPack;

[DataContract]
public class ConditionKind655 : ConditionKind
{
    public override bool CheckConditionKind(Condition condition, Person person)
    {
        var influenceId = condition.GetIntParam();

        foreach (var skill in person.Skills.Skills.Values)
        {
            if (skill.Influences.HasInfluence(influenceId)) return false;
        }
        foreach (var title in person.Titles)
        {
            if (title.Influences.HasInfluence(influenceId)) return false;
        }
        foreach (var stunt in person.Stunts.Stunts.Values)
        {
            if (stunt.Influences.HasInfluence(influenceId)) return false;
        }
        return true;
    }
}