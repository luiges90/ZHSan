using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind591 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return (person.TroopsDefeatedCount > person.RoutedCount);
        }
    }
}

