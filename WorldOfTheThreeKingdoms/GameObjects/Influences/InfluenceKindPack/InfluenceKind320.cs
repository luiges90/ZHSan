using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind320 : InfluenceKind
    {
        private int combatMethodID;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.CombatMethods.AddCombatMethod(Session.Current.Scenario.GameCommonData.AllCombatMethods.GetCombatMethod(this.combatMethodID));
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.combatMethodID = int.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.CombatMethods.RemoveCombatMethod(Session.Current.Scenario.GameCommonData.AllCombatMethods.GetCombatMethod(this.combatMethodID));
        }
    }
}

