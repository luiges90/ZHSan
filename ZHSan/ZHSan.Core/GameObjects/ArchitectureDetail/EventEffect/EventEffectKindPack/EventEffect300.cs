using GameManager;
using System.Runtime.Serialization;
using GameObjects.Influences;

namespace GameObjects.ArchitectureDetail.EventEffect;

[DataContract]
public class EventEffect300 : EventEffectKind
{
    public override void ApplyEffectKind(EventEffect eventEffect, Person person, Event e)
    {
        var skillId = eventEffect.GetIntParam();
        var skill = Session.Current.Scenario.GameCommonData.AllSkills.GetSkill(skillId);

        person.Skills.AddSkill(skill);
        skill.Influences.ApplyInfluence(person, Applier.Skill, skillId);
    }
}