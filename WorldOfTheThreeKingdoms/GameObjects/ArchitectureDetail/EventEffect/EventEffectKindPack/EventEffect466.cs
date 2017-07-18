using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect466 : EventEffectKind
    {
        private int decrement;

        public override void ApplyEffectKind(Person person, Event e)
        {
            person.Ideal -= decrement;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.decrement = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}
