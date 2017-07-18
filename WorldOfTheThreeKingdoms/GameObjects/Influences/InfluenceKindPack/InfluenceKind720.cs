using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind720 : InfluenceKind
    {
        private int maxDays;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.GetCurrentStratagemSuccess(troop.OrientationTroop, 
                troop.InevitableRumourOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence,
                (troop.OrientationTroop.NeverBeIntoChaos || troop.OrientationTroop.OutburstNeverBeIntoChaos) || troop.OrientationTroop.InvincibleRumour, 
                troop.OrientationTroop.InvincibleStratagemFromLowerIntelligence))
            {
                troop.OrientationTroop.SetRumour(troop.GenerateCastRumourDay(this.maxDays));
            }
            foreach (Troop troop2 in troop.AreaStratagemTroops)
            {
                if (troop.GetCurrentStratagemSuccess(troop2,
                    troop.InevitableRumourOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence,
                    (troop2.NeverBeIntoChaos || troop2.OutburstNeverBeIntoChaos) || troop2.InvincibleRumour, 
                    troop2.InvincibleStratagemFromLowerIntelligence))
                {
                    troop2.SetRumour(troop.GenerateCastRumourDay(this.maxDays));
                }
            }
        }

        public override int GetCredit(Troop source, Troop destination)
        {
            int num = 0;
            int pureFightingForce = source.PureFightingForce;
            foreach (Troop troop in source.GetAreaStratagemTroops(destination, false))
            {
                int num3 = source.GetStratagemSuccessChanceCredit(troop, 
                    source.InevitableRumourOnLowerIntelligence || source.InevitableStratagemOnLowerIntelligence, 
                    troop.InvincibleAttract, troop.InvincibleStratagemFromLowerIntelligence);
                if (num3 > 0)
                {
                    num3 = ((num3 + ((troop.Army.Scales - 5) * 10)) * troop.PureFightingForce) / pureFightingForce;
                    num += num3;
                }
            }
            return ((num * (2 + (source.IncrementOfRumourDay * 3))) / 2);
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.maxDays = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

