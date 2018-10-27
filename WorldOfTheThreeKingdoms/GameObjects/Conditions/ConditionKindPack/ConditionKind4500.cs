using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind4500 : ConditionKind
    {
 
        public override bool CheckConditionKind(Person person)
        {
            ConditionKind.markedPerson = person;
            return true;
        }
    }
}

