using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind915 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return !(person.BelongedFactionWithPrincess != null && (person.Father == person.BelongedFactionWithPrincess.Leader || person.Mother == person.BelongedFactionWithPrincess.Leader));
        }
    }
}

