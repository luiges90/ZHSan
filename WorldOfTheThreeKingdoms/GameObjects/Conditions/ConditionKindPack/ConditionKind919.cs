using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind919 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return !(person.BelongedFactionWithPrincess != null && (person.Father != null || person.Mother != null) &&
                (person.BelongedFactionWithPrincess.Leader.Father == person.Father || person.BelongedFactionWithPrincess.Leader.Mother == person.Mother));
        }
    }
}

