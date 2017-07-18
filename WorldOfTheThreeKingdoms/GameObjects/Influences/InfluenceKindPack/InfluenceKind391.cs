using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind391 : InfluenceKind
    {
        private int maxDays;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.GetCurrentStratagemSuccess(troop.OrientationTroop, troop.InevitableRaoluanOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence, (troop.OrientationTroop.NeverBeIntoChaos || troop.OrientationTroop.OutburstNeverBeIntoChaos) || troop.OrientationTroop.InvincibleRaoluan, troop.OrientationTroop.InvincibleStratagemFromLowerIntelligence))
            {
                troop.OrientationTroop.SetChaos(troop.GenerateCastChaosDay(this.maxDays));
            }
            foreach (Troop troop2 in troop.AreaStratagemTroops)
            {
                if (troop.GetCurrentStratagemSuccess(troop2, troop.InevitableRaoluanOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence, (troop2.NeverBeIntoChaos || troop2.OutburstNeverBeIntoChaos) || troop2.InvincibleRaoluan, troop2.InvincibleStratagemFromLowerIntelligence))
                {
                    troop2.SetChaos(troop.GenerateCastChaosDay(this.maxDays));
                }
            }
        }

        public override int GetCredit(Troop source, Troop destination)
        {
            int num = 0;
            int pureFightingForce = source.PureFightingForce;
            foreach (Troop troop in source.GetAreaStratagemTroops(destination, false))
            {
                int num3 = source.GetStratagemSuccessChanceCredit(troop, source.InevitableRaoluanOnLowerIntelligence || source.InevitableStratagemOnLowerIntelligence, (troop.NeverBeIntoChaos || troop.OutburstNeverBeIntoChaos) || troop.InvincibleRaoluan, troop.InvincibleStratagemFromLowerIntelligence); 
                if (destination.ChaosDayLeft > 2)
                {
                    num3 /= (destination.ChaosDayLeft - 1);
                }
                num3 *= (1 - troop.ChaosRecoverByViewArea / 100);
                num3 *= (1 - troop.ChaosRecoverInViewArea / 100);
                if (troop.ChaosLastOneDay)
                {
                    num3 /= 5;
                }
                if (num3 > 0)
                {
                    num3 = (((num3 + ((120 - troop.Morale) / 2)) + ((troop.Army.Scales - 5) * 5)) * troop.PureFightingForce) / pureFightingForce;
                    num += num3;
                }
            }
            return ((num * (2 + (source.IncrementOfChaosDay * 3))) / 2);
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

