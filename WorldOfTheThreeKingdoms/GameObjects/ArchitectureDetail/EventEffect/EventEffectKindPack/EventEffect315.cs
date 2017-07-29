using GameObjects;
using System;
using GameManager;

using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect315 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Person person, Event e)
        {
            GameObjects.PersonDetail.Title title = Session.Current.Scenario.GameCommonData.AllTitles.GetTitle(increment);

            if (person.RealTitles.Contains(title))
            {
                title.Influences.PurifyInfluence(person, GameObjects.Influences.Applier.Title, title.ID, false);
                person.RealTitles.Remove(title);
            }
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

