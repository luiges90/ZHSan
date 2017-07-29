using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind2713 : ConditionKind
    {
        public override bool CheckConditionKind(Architecture a)
        {
            int hostile = 0;
            int friendly = 0;
            foreach (Microsoft.Xna.Framework.Point point in a.LongViewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if (troopByPosition != null)
                {
                    if (troopByPosition.IsFriendly(a.BelongedFaction))
                    {
                        friendly++;
                    }
                    else
                    {
                        hostile++;
                    }
                }
            }
            return friendly > 0 && hostile > 0 && hostile >= friendly;
        }

    }
}

