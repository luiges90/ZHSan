using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind3300 : InfluenceKind
    {
        private int increment = 0;

        public override void ApplyInfluenceKind(Architecture architecture)
        {
            architecture.IncrementOfFactionReputationPerDay += this.increment;
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

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            architecture.IncrementOfFactionReputationPerDay -= this.increment;
        }

        public override double AIFacilityValue(Architecture a)
        {
            return (a.BelongedFaction.Reputation >= Session.Current.Scenario.Parameters.MaxReputationForRecruit ? 0.001 : this.increment / 5.0);
        }
    }
}

