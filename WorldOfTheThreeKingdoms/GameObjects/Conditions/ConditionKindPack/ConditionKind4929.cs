using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind4929 : ConditionKind
    {
        
        public override bool CheckConditionKind(Person person)
        {
            return !(ConditionKind.markedPerson.Father == person.Father || ConditionKind.markedPerson.Mother == person.Mother) || (person.Father == null || person.Mother == null);
        }

    }
}

