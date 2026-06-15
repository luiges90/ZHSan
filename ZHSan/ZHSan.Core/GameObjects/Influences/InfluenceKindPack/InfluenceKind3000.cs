using System;
using System.Runtime.Serialization;

namespace GameObjects.Influences.InfluenceKindPack;

[DataContract]
public class InfluenceKind3000 : InfluenceKind
{
    public override void ApplyInfluenceKind(Influence influence, Architecture arch)
    {
        arch.IncrementOfMonthFund += influence.GetIntParam();
    }

    public override void PurifyInfluenceKind(Influence influence, Architecture arch)
    {
        arch.IncrementOfMonthFund -= influence.GetIntParam();
    }

    public override double AIFacilityValue(Influence influence, Architecture arch)
    {
        var fundEnoughRate = arch.IsFundEnough ? 0 : 0.5;
        var fundAbundantRate = arch.IsFundAbundant ? 0 : 0.5;
        var fundIncomeEnoughRate = arch.IsFundIncomeEnough ? 0 : 1000;

        return (1 - Math.Pow((double)arch.Fund / arch.FundCeiling, 0.5) + fundEnoughRate + fundAbundantRate + fundIncomeEnoughRate) * influence.GetIntParam() / 1000.0;
    }
}