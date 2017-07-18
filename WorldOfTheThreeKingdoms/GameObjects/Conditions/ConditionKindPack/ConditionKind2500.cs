using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind2500 : ConditionKind
    {
        public override bool CheckConditionKind(Architecture a)
        {
            return a.FrontLine;
        }

    }
}

