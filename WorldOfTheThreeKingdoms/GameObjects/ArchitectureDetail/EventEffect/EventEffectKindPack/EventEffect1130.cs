using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect1130 : EventEffectKind
    {
        private int increment;
        private int mType;

        public override void ApplyEffectKind(Architecture a, Event e)
        {
            foreach (Military i in a.Militaries)
            {
                if (i.KindID == mType)
                {
                    i.IncreaseQuantity(increment);
                }
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.mType = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

