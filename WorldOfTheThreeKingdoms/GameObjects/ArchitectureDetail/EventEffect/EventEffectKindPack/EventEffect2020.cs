using GameManager;
using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect2020 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Faction f, Event e)
        {
            GameObjects.FactionDetail.Technique technique = Session.Current.Scenario.GameCommonData.AllTechniques.GetTechnique(increment);
            f.AvailableTechniques.AddTechnique(technique);
            Session.Current.Scenario.NewInfluence = true;
            technique.Influences.ApplyInfluence(f, GameObjects.Influences.Applier.Technique, increment);
            Session.Current.Scenario.NewInfluence = false;
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

