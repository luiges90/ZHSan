using GameObjects;
using System;
using GameManager;

using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect1400 : EventEffectKind
    {
        private int kind;

        public override void ApplyEffectKind(Architecture a, Event e)
        {
            a.Kind = Session.Current.Scenario.GameCommonData.AllArchitectureKinds.GetArchitectureKind(kind);
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.kind = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

