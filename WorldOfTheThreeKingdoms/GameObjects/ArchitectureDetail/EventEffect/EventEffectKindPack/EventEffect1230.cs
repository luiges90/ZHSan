using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect1230 : EventEffectKind
    {
        public override void ApplyEffectKind(Architecture a, Event e)
        {
            while (a.Facilities.Count > 0)
            {
                a.DemolishFacility(a.Facilities[0] as Facility);
            }
        }
    }
}

