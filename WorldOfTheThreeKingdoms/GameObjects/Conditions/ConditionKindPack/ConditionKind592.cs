using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind592 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return (person.TroopsDefeatedCount == person.RoutedCount);
        }
    }
}

