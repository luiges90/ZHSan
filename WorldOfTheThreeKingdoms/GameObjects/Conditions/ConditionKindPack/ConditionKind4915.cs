using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind4915 : ConditionKind
    {
        
        public override bool CheckConditionKind(Person person)
        {
            return person.HasCloseStrainTo(ConditionKind.markedPerson);
        }

    }
}

