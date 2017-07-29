using GameManager;
using GameObjects;
using GameObjects.Influences;
using System;


using System.Runtime.Serialization;namespace GameObjects.Influences.InfluenceKindPack
{

    [DataContract]public class InfluenceKind7000 : InfluenceKind
    {
        private float rate;
        private int militaryKindID;

        public override void ApplyInfluenceKind(Troop troop)
        {
            troop.DefenceRateIncrementInViewArea += this.rate;
        }

        public override void PurifyInfluenceKind(Troop troop)
        {
            troop.DefenceRateIncrementInViewArea -= this.rate;
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.rate = float.Parse(parameter);
            }
            catch
            {
            }
        }

        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.militaryKindID = int.Parse(parameter);
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
                if ((troopByPosition != null) && (troopByPosition.Army != null) && (troopByPosition.Army.KindID == this.militaryKindID)) 
                {
                    troopByPosition.RateOfDefence += this.rate;
                }
            }
        }

        public override void PurifyInfluenceKind(Architecture architecture)
        {
            foreach (Microsoft.Xna.Framework.Point point in architecture.ViewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && (troopByPosition.Army != null) && (troopByPosition.Army.KindID == this.militaryKindID)) 
                {
                    troopByPosition.RateOfDefence -= this.rate;
                }
            }
        }
    }
}

