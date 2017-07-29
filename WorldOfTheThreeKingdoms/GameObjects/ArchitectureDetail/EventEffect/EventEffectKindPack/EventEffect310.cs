using GameManager;
using GameObjects;
using GameObjects.PersonDetail;
using System;
using System.Collections.Generic;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect310 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Person person, Event e)
        {
            Title title = Session.Current.Scenario.GameCommonData.AllTitles.GetTitle(increment);
            if (title == null) return;

            person.LearnTitle(title);
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

