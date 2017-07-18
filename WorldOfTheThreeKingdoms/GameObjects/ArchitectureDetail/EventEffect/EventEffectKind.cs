using GameObjects;
using GameObjects.TroopDetail.EventEffect.EventEffectKindPack;
using System;

using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{
[DataContract]
//[KnownType(typeof(EventEffect0))]
//[KnownType(typeof(EventEffect5))]
//[KnownType(typeof(EventEffect10))]
//[KnownType(typeof(EventEffect15))]
//[KnownType(typeof(EventEffect20))]
//[KnownType(typeof(EventEffect25))]
//[KnownType(typeof(EventEffect30))]
//[KnownType(typeof(EventEffect35))]
//[KnownType(typeof(EventEffect40))]
//[KnownType(typeof(EventEffect45))]
//[KnownType(typeof(EventEffect50))]
//[KnownType(typeof(EventEffect100))]
//[KnownType(typeof(EventEffect110))]
//[KnownType(typeof(EventEffect120))]
//[KnownType(typeof(EventEffect130))]
//[KnownType(typeof(EventEffect140))]
//[KnownType(typeof(EventEffect150))]
//[KnownType(typeof(EventEffect160))]
//[KnownType(typeof(EventEffect170))]
//[KnownType(typeof(EventEffect200))]
//[KnownType(typeof(EventEffect210))]
//[KnownType(typeof(EventEffect211))]
//[KnownType(typeof(EventEffect212))]
//[KnownType(typeof(EventEffect213))]
//[KnownType(typeof(EventEffect214))]
//[KnownType(typeof(EventEffect215))]
//[KnownType(typeof(EventEffect216))]
//[KnownType(typeof(EventEffect220))]
//[KnownType(typeof(EventEffect221))]
//[KnownType(typeof(EventEffect222))]
//[KnownType(typeof(EventEffect223))]
//[KnownType(typeof(EventEffect224))]
//[KnownType(typeof(EventEffect225))]
//[KnownType(typeof(EventEffect226))]
//[KnownType(typeof(EventEffect227))]
//[KnownType(typeof(EventEffect230))]
//[KnownType(typeof(EventEffect235))]
//[KnownType(typeof(EventEffect250))]
//[KnownType(typeof(EventEffect280))]
//[KnownType(typeof(EventEffect290))]
//[KnownType(typeof(EventEffect300))]
//[KnownType(typeof(EventEffect305))]
//[KnownType(typeof(EventEffect310))]
//[KnownType(typeof(EventEffect315))]
//[KnownType(typeof(EventEffect320))]
//[KnownType(typeof(EventEffect325))]
//[KnownType(typeof(EventEffect330))]
//[KnownType(typeof(EventEffect400))]
//[KnownType(typeof(EventEffect410))]
//[KnownType(typeof(EventEffect420))]
//[KnownType(typeof(EventEffect430))]
//[KnownType(typeof(EventEffect440))]
//[KnownType(typeof(EventEffect450))]
//[KnownType(typeof(EventEffect460))]
//[KnownType(typeof(EventEffect465))]
//[KnownType(typeof(EventEffect466))]
//[KnownType(typeof(EventEffect470))]
//[KnownType(typeof(EventEffect480))]
//[KnownType(typeof(EventEffect500))]
//[KnownType(typeof(EventEffect510))]
//[KnownType(typeof(EventEffect600))]
//[KnownType(typeof(EventEffect650))]
//[KnownType(typeof(EventEffect700))]
//[KnownType(typeof(EventEffect1000))]
//[KnownType(typeof(EventEffect1010))]
//[KnownType(typeof(EventEffect1020))]
//[KnownType(typeof(EventEffect1030))]
//[KnownType(typeof(EventEffect1040))]
//[KnownType(typeof(EventEffect1050))]
//[KnownType(typeof(EventEffect1060))]
//[KnownType(typeof(EventEffect1070))]
//[KnownType(typeof(EventEffect1080))]
//[KnownType(typeof(EventEffect1090))]
//[KnownType(typeof(EventEffect1100))]
//[KnownType(typeof(EventEffect1110))]
//[KnownType(typeof(EventEffect1120))]
//[KnownType(typeof(EventEffect1130))]
//[KnownType(typeof(EventEffect1140))]
//[KnownType(typeof(EventEffect1150))]
//[KnownType(typeof(EventEffect1200))]
//[KnownType(typeof(EventEffect1210))]
//[KnownType(typeof(EventEffect1220))]
//[KnownType(typeof(EventEffect1230))]
//[KnownType(typeof(EventEffect1240))]
//[KnownType(typeof(EventEffect1250))]
//[KnownType(typeof(EventEffect1300))]
//[KnownType(typeof(EventEffect1310))]
//[KnownType(typeof(EventEffect1400))]
//[KnownType(typeof(EventEffect2000))]
//[KnownType(typeof(EventEffect2010))]
//[KnownType(typeof(EventEffect2020))]
//[KnownType(typeof(EventEffect2030))]
//[KnownType(typeof(EventEffect2040))]
//[KnownType(typeof(EventEffect2050))]
//[KnownType(typeof(EventEffect2100))]
//[KnownType(typeof(EventEffect2110))]
//[KnownType(typeof(EventEffect2120))]
//[KnownType(typeof(EventEffect2130))]
//[KnownType(typeof(EventEffect2131))]
//[KnownType(typeof(EventEffect2200))]
    public class EventEffectKind : GameObject  //abstract
    {
        public virtual void ApplyEffectKind(Person person, Event e)
        {
        }
        /*
        public virtual void ApplyEffectKind(FactionList factions,Person person, Event e)
        {
        }
        */
        public virtual void ApplyEffectKind(Architecture architecture, Event e)
        {
            if (architecture.Mayor != null)
            {
                this.ApplyEffectKind(architecture.Mayor, e);
            }
        }

        public virtual void ApplyEffectKind(Faction faction, Event e)
        {
            this.ApplyEffectKind(faction.Leader, e);
        }

        public virtual void InitializeParameter(string parameter)
        {
        }

        public virtual void InitializeParameter2(string parameter2)
        {
        }
        /*
        public virtual void InitializeParameter3(FactionList factions, string parameter)
        {
        }
        */
    }
}

