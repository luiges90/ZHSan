using System;


namespace GameObjects.Influences
{

    public enum AreaInfluenceKind
    {
        友军攻击力增加,
        敌军攻击力减少,
        友军防御力增加,
        敌军防御力减少,
        友军暴击几率增加,
        友军暴击抵抗几率增加,
        友军计略成功几率增加,
        友军计略抵抗几率增加,
        友军战意每天增加,
        敌军战意每天减少,
        friendlyTirednessDecrease,
        hostileTirednessIncrease,
        friendlyRecoverInjury,
        hostileLoseInjury,
        friendlyTroopIncrease,
        hostileTroopDecrease,
        friendlyMoraleIncrease,
        hostileMoraleDecrease,
        friendlyRecoverChaos,
        hostileChaos,
        friendlyMovabilityIncrease,
        hostileMovabilityDecrease,
        friendlySpeedIncrease,
        hostileSpeedDecrease
    }
}

