using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{
    
    [DataContract]public class EventEffect1000 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Architecture a, Event e)
        {
            a.IncreaseAgriculture(increment);
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

