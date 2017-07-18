using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind399 : InfluenceKind
    {
        private float rate;

        public override void ApplyInfluenceKind(Troop troop)
        {
            int recoverQuantity;
            if (troop.GetCurrentStratagemSuccess(troop.OrientationTroop, false, false, false))
            {
                troop.OrientationTroop.PreAction = TroopPreAction.恢复;
                recoverQuantity = (int)(this.rate * troop.OrientationTroop.Army.Kind.MinScale);
                if (recoverQuantity > troop.OrientationTroop.InjuryQuantity)
                {
                    recoverQuantity = troop.OrientationTroop.InjuryQuantity;
                }
                recoverQuantity = troop.OrientationTroop.IncreaseQuantity(recoverQuantity);
                troop.OrientationTroop.InjuryQuantity -= recoverQuantity;
            }
            foreach (Troop troop2 in troop.AreaStratagemTroops)
            {
                if (troop.GetCurrentStratagemSuccess(troop2, false, false, false))
                {
                    troop2.PreAction = TroopPreAction.恢复;
                    recoverQuantity = (int)(this.rate * troop2.Army.Kind.MinScale);
                    if (recoverQuantity > troop2.InjuryQuantity)
                    {
                        recoverQuantity = troop2.InjuryQuantity;
                    }
                    recoverQuantity = troop2.IncreaseQuantity(recoverQuantity);
                    troop2.OrientationTroop.InjuryQuantity -= recoverQuantity;
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
            int fightingForce = source.FightingForce;
            foreach (Troop troop in source.GetAreaStratagemTroops(destination, true))
            {
                float rate = ((float) troop.InjuryQuantity) / ((float) troop.Army.Kind.MinScale);
                int num4 = source.GetStratagemSuccessChanceCredit(troop, false, false, false);
                if (num4 > 0)
                {
                    if (rate > this.rate)
                    {
                        rate = this.rate;
                    }
                    num += (int) ((((num4 * rate) / this.rate) * troop.FightingForce) / ((float) fightingForce));
                }
            }
            return num;
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

        public override bool IsVaild(Troop troop)
        {
            return (troop.InjuryQuantity > 0);
        }
    }
}

