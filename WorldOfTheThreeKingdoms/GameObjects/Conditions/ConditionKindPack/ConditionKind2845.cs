using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind2845 : ConditionKind
    {
        public override bool CheckConditionKind(Architecture a)
        {
            if (a.BelongedFaction == null) return false;
            foreach (Person p in a.Feiziliebiao)
            {
                if (p == a.BelongedFaction.Leader)
                {
                    return false;
                }
            }
            return true;
        }


    }
}

