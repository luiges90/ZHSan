using GameObjects;
using System;
using GameManager;

using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect2040 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Faction f, Event e)
        {
            f.BaseMilitaryKinds.AddMilitaryKind(Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(increment));
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
    }
}

