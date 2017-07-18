using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind817 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return !person.Alive;
        }
    }
}
