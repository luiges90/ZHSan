using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind397 : InfluenceKind
    {
        private int baseIncrement;

        public override void ApplyInfluenceKind(Troop troop)
        {
            if (troop.GetCurrentStratagemSuccess(troop.OrientationTroop, false, false, false))
            {
                troop.OrientationTroop.PreAction = TroopPreAction.鼓舞;
                troop.OrientationTroop.IncreaseMorale(troop.GenerateBoostIncrement(this.baseIncrement));
            }
            foreach (Troop troop2 in troop.AreaStratagemTroops)
            {
                if (troop.GetCurrentStratagemSuccess(troop2, false, false, false))
                {
                    troop2.PreAction = TroopPreAction.鼓舞;
                    troop2.IncreaseMorale(troop.GenerateBoostIncrement(this.baseIncrement));
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
                int num3 = troop.Army.MoraleCeiling - troop.Army.Morale;
                if ((num3 >= 5) || !GameObject.Chance(0x5f))
                {
                    int num4 = source.GetStratagemSuccessChanceCredit(troop, false, false, false);
                    if (num4 > 0)
                    {
                        int num5 = source.GenerateBoostIncrement(this.baseIncrement);
                        if (num3 < num5)
                        {
                            num4 = ((((num4 + ((100 - troop.Morale) / 2)) * troop.FightingForce) * num3) / num5) / fightingForce;
                        }
                        else
                        {
                            num4 = ((num4 + ((100 - troop.Morale) / 2)) * troop.FightingForce) / fightingForce;
                        }
                        num += num4;
                    }
                }
            }
            return num;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.baseIncrement = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override bool IsVaild(Troop troop)
        {
            return (troop.Morale < troop.Army.EncourageMoraleCeiling);
        }
    }
}

