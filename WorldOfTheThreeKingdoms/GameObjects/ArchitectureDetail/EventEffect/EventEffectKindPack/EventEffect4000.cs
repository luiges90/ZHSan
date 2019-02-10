using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect4000 : EventEffectKind
    {
        private int type;

        public override void ApplyEffectKind(Person person, Event e)
        {
            person.AvatarIndex = type;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.type = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}
