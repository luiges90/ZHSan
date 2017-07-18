using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind3050 : ConditionKind
    {

        public override bool CheckConditionKind(Faction faction)
        {
            return faction.IsAlien;
        }

    }
}

