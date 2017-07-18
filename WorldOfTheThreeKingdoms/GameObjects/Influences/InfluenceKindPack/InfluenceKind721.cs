using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind721 : InfluenceKind
    {
        private int maxDays;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.GetCurrentStratagemSuccess(troop.OrientationTroop, 
                troop.InevitableAttractOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence, 
                troop.OrientationTroop.InvincibleAttract, troop.OrientationTroop.InvincibleStratagemFromLowerIntelligence))
            {
                troop.OrientationTroop.SetAttract(troop, troop.GenerateCastAttractDay(this.maxDays));
            }
            foreach (Troop troop2 in troop.AreaStratagemTroops)
            {
                if (troop.GetCurrentStratagemSuccess(troop2,
                    troop.InevitableAttractOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence,
                    troop2.InvincibleAttract, troop2.InvincibleStratagemFromLowerIntelligence))
                {
                    troop2.SetAttract(troop, troop.GenerateCastAttractDay(this.maxDays));
                }
            }
        }

        public override int GetCredit(Troop source, Troop destination)
        {
            int num = 0;
            int pureFightingForce = source.PureFightingForce;
            foreach (Troop troop in source.GetAreaStratagemTroops(destination, false))
            {
                if (troop.FightingForce > source.FightingForce)
                {
                    return -100;
                }

                int num3 = source.GetStratagemSuccessChanceCredit(troop,
                    source.InevitableAttractOnLowerIntelligence || source.InevitableStratagemOnLowerIntelligence, 
                    troop.InvincibleAttract, troop.InvincibleStratagemFromLowerIntelligence);
                if (num3 > 0)
                {
                    num3 = (((num3 + ((100 - troop.Morale) / 2)) + ((40 - troop.Army.Scales) * 5)) * troop.PureFightingForce) / pureFightingForce;
                    num += num3;
                }
            }
            return ((num * (2 + (source.IncrementOfAttractDay * 3))) / 2);
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

