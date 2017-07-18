using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1007 : ConditionKind
    {
        public override bool CheckConditionKind(Troop troop)
        {
            return (troop.Combativity < troop.Army.CombativityCeiling);
        }
    }
}

