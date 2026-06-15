using System.Runtime.Serialization;
using GameObjects.PersonDetail;

namespace GameObjects.Conditions.ConditionKindPack;

[DataContract]
public class ConditionKind650 : ConditionKind
{
    public override bool CheckConditionKind(Condition condition, Person person)
    {
        var influenceId = condition.GetIntParam();

        foreach (Skill skill in person.Skills.Skills.Values)
        {
            if (skill.Influences.HasInfluence(influenceId)) return true;
        }

        foreach (Title title in person.Titles)
        {
            if (title.Influences.HasInfluence(influenceId)) return true;
        }

        foreach (Stunt stunt in person.Stunts.Stunts.Values)
        {
            if (stunt.Influences.HasInfluence(influenceId)) return true;
        }

        return false;
    }
}