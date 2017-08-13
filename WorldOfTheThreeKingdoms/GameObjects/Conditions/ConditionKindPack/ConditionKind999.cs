using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind999 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return !Session.Current.Scenario.IsPlayer(person.BelongedFactionWithPrincess);
        }

        public override bool CheckConditionKind(Architecture architecture)
        {
            return !Session.Current.Scenario.IsPlayer(architecture.BelongedFaction);
        }

        public override bool CheckConditionKind(Faction faction)
        {
            return !Session.Current.Scenario.IsPlayer(faction);
        }

        public override bool CheckConditionKind(Troop troop)
        {
            return !Session.Current.Scenario.IsPlayer(troop.BelongedFaction);
        }

    }
}

