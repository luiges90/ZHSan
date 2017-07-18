using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect1250 : EventEffectKind
    {
        private int id;

        public override void ApplyEffectKind(Architecture a, Event e)
        {
            a.Characteristics.PurifyInfluence(a, GameObjects.Influences.Applier.Characteristics, 0);
            a.Characteristics.Influences.Remove(id);
            a.Characteristics.ApplyInfluence(a, GameObjects.Influences.Applier.Characteristics, 0);
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

