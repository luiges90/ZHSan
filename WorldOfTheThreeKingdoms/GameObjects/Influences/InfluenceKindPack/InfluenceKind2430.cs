using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2430 : InfluenceKind
    {
        private float rate = 1f;

        public override void ApplyInfluenceKind(Faction faction)
        {
            faction.RateOfRoutewayConsumption += 1 - this.rate;
            if (Session.Current.Scenario.NewInfluence)
            {
                faction.ClosedRouteways.Clear();
                foreach (Routeway routeway in faction.Routeways)
                {
                    routeway.ResetRoutePointConsumptionRate(this.rate);
                }
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.rate = float.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Faction faction)
        {
            faction.RateOfRoutewayConsumption -= 1 - this.rate;
            if (Session.Current.Scenario.NewInfluence)
            {
                faction.ClosedRouteways.Clear();
                foreach (Routeway routeway in faction.Routeways)
                {
                    routeway.ResetRoutePointConsumptionRate(this.rate);
                }
            }
        }
    }
}

