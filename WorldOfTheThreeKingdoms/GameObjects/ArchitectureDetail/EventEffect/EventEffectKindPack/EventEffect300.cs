using GameManager;
using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect300 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Person person, Event e)
        {
            GameObjects.PersonDetail.Skill skill = Session.Current.Scenario.GameCommonData.AllSkills.GetSkill(increment);
            person.Skills.AddSkill(skill);
            skill.Influences.ApplyInfluence(person, GameObjects.Influences.Applier.Skill, increment, false);
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

