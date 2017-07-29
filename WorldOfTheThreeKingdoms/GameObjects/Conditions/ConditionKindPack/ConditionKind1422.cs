using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1422 : ConditionKind
    {
        public override bool CheckConditionKind(Troop troop)
        {
            Architecture architectureByPositionNoCheck = Session.Current.Scenario.GetArchitectureByPositionNoCheck(troop.Position);
            return ((architectureByPositionNoCheck != null) && !troop.IsFriendly(architectureByPositionNoCheck.BelongedFaction));
        }
    }
}

