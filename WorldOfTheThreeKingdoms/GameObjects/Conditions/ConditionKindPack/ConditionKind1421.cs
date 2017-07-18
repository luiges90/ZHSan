using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1421 : ConditionKind
    {
        public override bool CheckConditionKind(Troop troop)
        {
            Architecture architectureByPositionNoCheck = troop.Scenario.GetArchitectureByPositionNoCheck(troop.Position);
            return ((architectureByPositionNoCheck == null) || !troop.IsFriendly(architectureByPositionNoCheck.BelongedFaction));
        }
    }
}

