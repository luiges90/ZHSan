using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind1411 : ConditionKind
    {
        public override bool CheckConditionKind(Troop troop)
        {
            return (troop.Leader.Braveness == troop.Leader.Calmness);
        }
    }
}

