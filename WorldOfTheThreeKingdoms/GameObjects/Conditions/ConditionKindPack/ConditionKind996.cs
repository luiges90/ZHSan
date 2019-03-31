using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind996 : ConditionKind
    {
        public override bool CheckConditionKind(Person person)
        {
            return true;
        }

        public override bool CheckConditionKind(Architecture architecture)
        {
            return true;
        }

        public override bool CheckConditionKind(Faction faction)
        {
            return true;
        }

        public override bool CheckConditionKind(Troop troop)
        {
            return true;
        }
    }
}

