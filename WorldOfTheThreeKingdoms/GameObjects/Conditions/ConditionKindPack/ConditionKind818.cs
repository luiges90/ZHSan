using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind818 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return (person.BelongedFaction != null && person.BelongedFaction.PrinceID != person.ID) || person.BelongedFaction == null ;
        }
    }
}
