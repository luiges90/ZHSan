using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2400 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Faction faction)
        {
            faction.IncrementOfRoutewayWorkforce += this.increment;
            if (Session.Current.Scenario.NewInfluence)
            {
                faction.ClosedRouteways.Clear();
                foreach (Routeway routeway in faction.Routeways)
                {
                    routeway.RemoveAfterClose = true;
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

        public override void PurifyInfluenceKind(Faction faction)
        {
            faction.IncrementOfRoutewayWorkforce -= this.increment;
            if (Session.Current.Scenario.NewInfluence)
            {
                faction.ClosedRouteways.Clear();
                foreach (Routeway routeway in faction.Routeways)
                {
                    routeway.RemoveAfterClose = true;
                }
            }
        }
    }
}

