using GameManager;
using System.Runtime.Serialization;
using GameObjects.Influences;

namespace GameObjects.ArchitectureDetail.EventEffect;

[DataContract]
public class EventEffect305 : EventEffectKind
{
    public override void ApplyEffectKind(EventEffect eventEffect, Person person, Event e)
    {
        var skillId = eventEffect.GetIntParam();
        var skill = Session.Current.Scenario.GameCommonData.AllSkills.GetSkill(skillId);

        person.Skills.Skills.Remove(skillId);
        skill.Influences.PurifyInfluence(person, Applier.Skill, skillId);
    }
}