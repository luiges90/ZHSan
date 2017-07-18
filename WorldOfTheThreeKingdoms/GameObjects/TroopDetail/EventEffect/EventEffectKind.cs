using GameObjects;
using GameObjects.TroopDetail.EventEffect.EventEffectKindPack;
using System;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail.EventEffect
{
    [DataContract]
    //[KnownType(typeof(EventEffectKind0))]
    //[KnownType(typeof(EventEffectKind1))]
    //[KnownType(typeof(EventEffectKind10))]
    //[KnownType(typeof(EventEffectKind15))]
    //[KnownType(typeof(EventEffectKind20))]
    //[KnownType(typeof(EventEffectKind25))]
    //[KnownType(typeof(EventEffectKind30))]
    //[KnownType(typeof(EventEffectKind35))]
    //[KnownType(typeof(EventEffectKind40))]
    //[KnownType(typeof(EventEffectKind45))]
    //[KnownType(typeof(EventEffectKind50))]
    //[KnownType(typeof(EventEffectKind60))]
    //[KnownType(typeof(EventEffectKind80))]
    //[KnownType(typeof(EventEffectKind100))]
    
    public class EventEffectKind : GameObject  //abstract
    {
        public virtual void ApplyEffectKind(Person person)
        {
        }

        public virtual void ApplyEffectKind(Troop troop)
        {
        }

        public virtual void InitializeParameter(string parameter)
        {
        }
    }
}

