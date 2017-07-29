using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind675 : InfluenceKind
    {
        private int decrement;


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.ChanceDecrementOfStratagemInViewArea += this.decrement;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.ChanceDecrementOfStratagemInViewArea -= this.decrement;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.decrement = int.Parse(parameter);
            }
            catch
            {
            }
        }


        public override void ApplyInfluenceKind(Architecture architecture)
        {
            foreach (Microsoft.Xna.Framework.Point point in architecture.ViewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && !architecture.IsFriendly(troopByPosition.BelongedFaction))
                {
                    troopByPosition.ChanceDecrementOfStratagem += this.decrement;
                }
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            foreach (Microsoft.Xna.Framework.Point point in architecture.ViewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && !architecture.IsFriendly(troopByPosition.BelongedFaction))
                {
                    troopByPosition.ChanceDecrementOfStratagem -= this.decrement;
                }
            }
        }
    }
}

