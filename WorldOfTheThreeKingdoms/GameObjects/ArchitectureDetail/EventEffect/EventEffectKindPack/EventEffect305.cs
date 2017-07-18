using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect305 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Person person, Event e)
        {
            GameObjects.PersonDetail.Skill skill = person.Scenario.GameCommonData.AllSkills.GetSkill(increment);
            person.Skills.Skills.Remove(increment);
            skill.Influences.PurifyInfluence(person, GameObjects.Influences.Applier.Skill, increment, false);
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

