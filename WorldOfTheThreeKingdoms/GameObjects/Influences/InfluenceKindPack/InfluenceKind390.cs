using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind390 : InfluenceKind
    {
        private int baseDecrement;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.GetCurrentStratagemSuccess(troop.OrientationTroop, troop.InevitableGongxinOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence, troop.OrientationTroop.InvincibleGongxin, troop.OrientationTroop.InvincibleStratagemFromLowerIntelligence))
            {
                troop.ApplyGongxin(troop.OrientationTroop, this.baseDecrement);
            }
            foreach (Troop troop2 in troop.AreaStratagemTroops)
            {
                if (troop.GetCurrentStratagemSuccess(troop2, troop.InevitableGongxinOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence, troop2.InvincibleGongxin, troop2.InvincibleStratagemFromLowerIntelligence))
                {
                    troop.ApplyGongxin(troop2, this.baseDecrement);
                }
            }
        }

        public override int GetCredit(Troop source, Troop destination)
        {
            int num = 0;
            int pureFightingForce = source.PureFightingForce;
            foreach (Troop troop in source.GetAreaStratagemTroops(destination, false))
            {
                int num3 = source.GetStratagemSuccessChanceCredit(troop, source.InevitableGongxinOnLowerIntelligence || source.InevitableStratagemOnLowerIntelligence, troop.InvincibleGongxin, troop.InvincibleStratagemFromLowerIntelligence);
                num3 -= destination.MoraleIncreaseByViewArea * 50;
                num3 -= destination.MoraleIncreaseInViewArea * 50;
                num3 += destination.MoraleDecreaseByViewArea * 50;
                num3 += destination.MoraleDecreaseInViewArea * 50;
                num3 -= destination.IncrementPerDayOfMorale * 50;
                if (num3 > 0)
                {
                    num3 = (((num3 + ((120 - troop.Morale) / 2)) + ((troop.Army.Scales - 5) * 5)) * troop.PureFightingForce) / pureFightingForce;
                    num += num3;
                }
            }
            return (int) (num * source.RateOfGongxin);
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.baseDecrement = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

