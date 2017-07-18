using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect1220 : EventEffectKind
    {
        private int id;

        public override void ApplyEffectKind(Architecture a, Event e)
        {
            foreach (Facility f in a.Facilities)
            {
                if (f.KindID == id)
                {
                    a.DemolishFacility(f);
                    return;
                }
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.id = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

