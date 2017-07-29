using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind394 : InfluenceKind
    {
        private float scale;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.GetCurrentStratagemSuccess(troop.OrientationTroop, troop.InevitableHuogongOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence, troop.OrientationTroop.InvincibleHuogong, troop.OrientationTroop.InvincibleStratagemFromLowerIntelligence))
            {
                troop.OrientationTroop.SetOnFire(troop.GenerateFireDamageScale(this.scale, Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(troop.OrientationTroop.Position)));
            }
            foreach (Troop troop2 in troop.AreaStratagemTroops)
            {
                if (troop.GetCurrentStratagemSuccess(troop2, troop.InevitableHuogongOnLowerIntelligence || troop.InevitableStratagemOnLowerIntelligence, troop2.InvincibleHuogong, troop2.InvincibleStratagemFromLowerIntelligence))
                {
                    troop2.SetOnFire(troop.GenerateFireDamageScale(this.scale, Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(troop2.Position)));
                }
            }
        }

        public override int GetCredit(Troop source, Troop destination)
        {
            if (!this.IsVaild(destination))
            {
                return 0;
            }
            int num = 0;
            int pureFightingForce = source.PureFightingForce;
            foreach (Troop troop in source.GetAreaStratagemTroops(destination, false))
            {
                int num3 = source.GetStratagemSuccessChanceCredit(troop, source.InevitableHuogongOnLowerIntelligence || source.InevitableStratagemOnLowerIntelligence, troop.InvincibleHuogong, troop.InvincibleStratagemFromLowerIntelligence);
                if (num3 > 0)
                {
                    num3 = (int)(((num3 + (((5 - troop.Army.Scales) * 5))) * (troop.FireDamageRate - 1)) * troop.PureFightingForce) / pureFightingForce;
                    if (troop.RateOfFireProtection > 0f)
                    {
                        num += (int) (num3 * troop.RateOfFireProtection);
                    }
                    else
                    {
                        num += num3;
                    }
                }
            }
            return (int) (num * source.RateOfFireDamage);
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.scale = float.Parse(parameter);
            }
            catch
            {
            }
        }

        public override bool IsVaild(Troop troop)
        {
            return Session.Current.Scenario.IsFireVaild(troop.Position, true, troop.Army.Kind.Type);
        }
    }
}

