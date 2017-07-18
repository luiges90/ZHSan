using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind916 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return person.BelongedFactionWithPrincess != null && (person.BelongedFactionWithPrincess.Leader.Father == person || person.BelongedFactionWithPrincess.Leader.Mother == person);
        }
    }
}

