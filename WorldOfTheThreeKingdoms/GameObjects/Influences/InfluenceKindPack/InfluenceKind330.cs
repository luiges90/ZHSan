using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind330 : InfluenceKind
    {
        private int combatMethodID;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.Stunts.AddStunt(Session.Current.Scenario.GameCommonData.AllStunts.GetStunt(this.combatMethodID));
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
            troop.Stunts.RemoveStunt(Session.Current.Scenario.GameCommonData.AllStunts.GetStunt(this.combatMethodID));
        }
    }
}

