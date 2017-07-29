using GameManager;
using GameObjects;
using GameObjects.Conditions;
using System;


using System.Runtime.Serialization;namespace GameObjects.Conditions.ConditionKindPack
{

    [DataContract]public class ConditionKind2750 : ConditionKind
    {
        private int val;

        public override bool CheckConditionKind(Architecture a)
        {
            int hostile = 0;
            int friendly = 0;
            foreach (Microsoft.Xna.Framework.Point point in a.ArchitectureArea.Area)
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
            return friendly < val;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.val = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

