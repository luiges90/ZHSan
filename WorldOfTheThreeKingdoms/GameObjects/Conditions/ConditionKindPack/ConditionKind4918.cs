using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind4918 : ConditionKind
    {
        
        public override bool CheckConditionKind(Person person)
        {
            return ConditionKind.markedPerson.Father == person || ConditionKind.markedPerson.Mother == person;
        }

    }
}

