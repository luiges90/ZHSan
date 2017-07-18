using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind665 : InfluenceKind
    {
        private int decrement;


        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.CombativityDecrementPerDayInViewArea += this.decrement;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.CombativityDecrementPerDayInViewArea -= this.decrement;
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
                Troop troopByPosition = base.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && !architecture.IsFriendly(troopByPosition.BelongedFaction))
                {
                    troopByPosition.DecreaseCombativity(this.decrement);
                }
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            foreach (Microsoft.Xna.Framework.Point point in architecture.ViewArea.Area)
            {
                Troop troopByPosition = base.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && !architecture.IsFriendly(troopByPosition.BelongedFaction))
                {
                    troopByPosition.DecreaseCombativity(this.decrement);
                }
            }
        }
    }
}

