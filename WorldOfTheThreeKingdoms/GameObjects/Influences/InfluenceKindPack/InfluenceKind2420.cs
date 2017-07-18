using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind2420 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Faction faction)
        {
            faction.IncrementOfRoutewayRadius += this.increment;
            if (faction.Scenario.NewInfluence)
            {
                foreach (Routeway routeway in faction.Routeways)
                {
                    routeway.ReGenerateRoutePointArea();
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
            faction.IncrementOfRoutewayRadius -= this.increment;
            if (faction.Scenario.NewInfluence)
            {
                foreach (Routeway routeway in faction.Routeways)
                {
                    routeway.ReGenerateRoutePointArea();
                }
            }
        }
    }
}

