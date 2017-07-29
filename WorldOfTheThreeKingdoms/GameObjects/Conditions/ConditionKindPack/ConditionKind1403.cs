using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1403 : ConditionKind
    {
        public override bool CheckConditionKind(Troop troop)
        {
            return Session.Current.Scenario.GetArchitectureByPosition(troop.Position) != null &&
                Session.Current.Scenario.GetArchitectureByPosition(troop.Position).BelongedFaction == troop.BelongedFaction;
        }
    }
}

