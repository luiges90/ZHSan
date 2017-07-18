using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1404 : ConditionKind
    {
        public override bool CheckConditionKind(Troop troop)
        {
            return troop.Scenario.GetArchitectureByPosition(troop.Position) != null &&
                troop.Scenario.GetArchitectureByPosition(troop.Position).BelongedFaction != troop.BelongedFaction;
        }
    }
}

