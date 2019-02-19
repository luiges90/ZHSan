using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1008 : ConditionKind
    {
        public override bool CheckConditionKind(Troop troop)
        {
            return (troop.InjuryAmount == 0);
        }
    }
}

