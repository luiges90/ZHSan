using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect650 : EventEffectKind
    {
        private String tag;

        public override void ApplyEffectKind(Person person, Event e)
        {
            int index = person.Tags.IndexOf(tag + ",");
            if (index >= 0) {
                person.Tags.Remove(index, tag.Length + 2);
            }
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
