using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect600 : EventEffectKind
    {
        private String tag;

        public override void ApplyEffectKind(Person person, Event e)
        {
            person.Tags += tag + ",";
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.tag = parameter;
            }
            catch
            {
            }
        }
    }
}


 
