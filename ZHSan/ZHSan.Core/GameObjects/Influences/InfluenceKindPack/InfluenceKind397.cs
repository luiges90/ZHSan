using System.Runtime.Serialization;

namespace GameObjects.Influences.InfluenceKindPack;

[DataContract]
public class InfluenceKind397 : InfluenceKind
{
    public override void ApplyInfluenceKind(Influence influence, Troop troop)
    {
        var baseIncrement = influence.GetIntParam();

        var friendly = troop.OrientationTroop;

        if (troop.GetCurrentStratagemSuccess(friendly, false, false, false))
        {
            friendly.PreAction = TroopPreAction.鼓舞;
            friendly.IncreaseMorale(troop.GenerateBoostIncrement(baseIncrement));
        }
        foreach (Troop troop2 in troop.AreaStratagemTroops)
        {
            if (troop.GetCurrentStratagemSuccess(troop2, false, false, false))
            {
                troop2.PreAction = TroopPreAction.鼓舞;
                troop2.IncreaseMorale(troop.GenerateBoostIncrement(baseIncrement));
            }
        }
    }

    public override int GetCredit(Influence influence, Troop source, Troop destination)
    {
        if (!IsVaild(influence, destination)) return 0;

        var baseIncrement = influence.GetIntParam();

        var sum = 0;
        int fightingForce = source.FightingForce;
        foreach (Troop troop in source.GetAreaStratagemTroops(destination, true))
        {
            int num3 = troop.Army.MoraleCeiling - troop.Army.Morale;
            if (num3 >= 5 || !GetChance(0x5f))
            {
                int num4 = source.GetStratagemSuccessChanceCredit(troop, false, false, false);
                if (num4 > 0)
                {
                    int num5 = source.GenerateBoostIncrement(baseIncrement);

                    num4 = (num4 + (100 - troop.Morale) / 2) * troop.FightingForce / fightingForce;

                    if (num3 < num5)
                    {
                        num4 *= num3 / num5;
                    }
                   
                    sum += num4;
                }
            }
        }
        return sum;
    }

    public override bool IsVaild(Influence influence, Troop troop)
    {
        return troop.Morale < troop.Army.EncourageMoraleCeiling;
    }
}