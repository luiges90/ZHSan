using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect1310 : EventEffectKind
    {
        private int id;

        public override void ApplyEffectKind(Architecture a, Event e)
        {
            a.zainan.shengyutianshu += id;
            if (a.zainan.shengyutianshu <= 0)
            {
                a.youzainan = false;
                a.tingzhizhenzai();
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

