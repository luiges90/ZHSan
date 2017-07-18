using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail.EventEffect
{
    [DataContract]
    public class EventEffect : GameObject
    {
        [DataMember]
        public EventEffectKind Kind;
        private string parameter;

        public EventEffect Clone()
        {
            return this.MemberwiseClone() as EventEffect;
        }

        public void ApplyEffect(Person person)
        {
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.ApplyEffectKind(person);
        }

        [DataMember]
        public string Parameter
        {
            get
            {
                return this.parameter;
            }
            set
            {
                this.parameter = value;
            }
        }
    }
}

