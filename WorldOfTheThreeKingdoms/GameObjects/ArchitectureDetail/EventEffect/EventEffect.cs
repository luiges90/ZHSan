using GameObjects;
using System;

using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{
    [DataContract]
    public class EventEffect : GameObject
    {
        [DataMember]
        public EventEffectKind Kind;
        private string parameter;
        private string parameter2;

        public EventEffect Clone()
        {
            return this.MemberwiseClone() as EventEffect;
        }

        public void ApplyEffect(Person person, Event e)
        {
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            this.Kind.ApplyEffectKind(person, e);
        }

        public void ApplyEffect(Architecture a, Event e)
        {
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            this.Kind.ApplyEffectKind(a, e);
        }

        public void ApplyEffect(Faction f, Event e)
        {
            this.Kind.InitializeParameter(this.Parameter);
            this.Kind.InitializeParameter2(this.Parameter2);
            this.Kind.ApplyEffectKind(f, e);
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
        [DataMember]
        public string Parameter2
        {
            get
            {
                return this.parameter2;
            }
            set
            {
                this.parameter2 = value;
            }
        }
    }
}

