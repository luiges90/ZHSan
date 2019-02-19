using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect2100 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Faction f, Event e)
        {
            f.ContributionToEmperor += increment;
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

