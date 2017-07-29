using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind660 : InfluenceKind
    {
        private int increment;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.CombativityIncrementPerDayInViewArea += this.increment;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.CombativityIncrementPerDayInViewArea -= this.increment;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = int.Parse(parameter);
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
                if ((troopByPosition != null) && architecture.IsFriendly(troopByPosition.BelongedFaction))
                {
                    troopByPosition.IncreaseCombativity(this.increment);
                }
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            foreach (Microsoft.Xna.Framework.Point point in architecture.ViewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && architecture.IsFriendly(troopByPosition.BelongedFaction))
                {
                    troopByPosition.IncreaseCombativity(this.increment);
                }
            }
        }
    }
}

