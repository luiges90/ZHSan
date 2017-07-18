using GameGlobal;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class Information : GameObject
    {
        private GameArea area;
        public Faction BelongedFaction; // informations belonging to faction will naturally expire
        public Architecture BelongedArchitecture; // informations belonging to architecture will not expire
        private InformationLevel level;
        private bool oblique;
        private Point position;
        private int radius;
        private int dayCost;
        private int daysLeft;
        private int daysStarted = 0;

        public void Apply()
        {
            foreach (Point point in this.Area.Area)
            {
                if (this.BelongedArchitecture != null && this.BelongedArchitecture.BelongedFaction != null)
                {
                    this.BelongedArchitecture.BelongedFaction.AddPositionInformation(point, this.Level);
                }
                if (this.BelongedFaction != null)
                {
                    this.BelongedFaction.AddPositionInformation(point, this.Level);
                }
                //this.CheckAmbushTroop(point);
            }
        }

        public void CheckAmbushTroop()
        {
            foreach (Point point in this.Area.Area)
            {
                this.CheckAmbushTroop(point);
            }
        }

        private void CheckAmbushTroop(Point p)
        {
            Troop troopByPosition = base.Scenario.GetTroopByPosition(p);
            if (troopByPosition != null && troopByPosition.Status == TroopStatus.埋伏 && 
                ((this.BelongedArchitecture != null && !this.BelongedArchitecture.IsFriendly(troopByPosition.BelongedFaction)) || 
                (this.BelongedFaction != null && !this.BelongedFaction.IsFriendly(troopByPosition.BelongedFaction))))
            {
                this.DetectAmbush(troopByPosition);
            }
        }

        private void DetectAmbush(Troop troop)
        {
            int chance = 40 - troop.Leader.Calmness;
            if (this.Level <= InformationLevel.中)
            {
                if (troop.OnlyBeDetectedByHighLevelInformation)
                {
                    return;
                }
            }
            else
            {
                chance *= 3;
            }
            if (GameObject.Chance(chance))
            {
                troop.AmbushDetected(troop);
            }
        }

        public void Initialize()
        {
            foreach (Point point in this.Area.Area)
            {
                if (this.BelongedArchitecture != null && this.BelongedArchitecture.BelongedFaction != null)
                {
                    this.BelongedArchitecture.BelongedFaction.AddPositionInformation(point, this.Level);
                }
                else if (this.BelongedFaction != null)
                {
                    this.BelongedFaction.AddPositionInformation(point, this.Level);
                }
            }
        }

        public void Purify()
        {
            if (this.BelongedArchitecture != null && this.BelongedArchitecture.BelongedFaction != null)
            {
                foreach (Point point in this.Area.Area)
                {
                    this.BelongedArchitecture.BelongedFaction.RemovePositionInformation(point, this.Level);
                }
            }
            if (this.BelongedFaction != null)
            {
                foreach (Point point in this.Area.Area)
                {
                    this.BelongedFaction.RemovePositionInformation(point, this.Level);
                }
            }
        }

        public GameArea Area
        {
            get
            {
                if (this.area == null)
                {
                    this.area = GameArea.GetViewArea(this.position, this.radius, this.oblique, base.Scenario, null);
                }
                return this.area;
            }
            set
            {
                this.area = value;
            }
        }

        [DataMember]
        public InformationLevel Level
        {
            get
            {
                return this.level;
            }
            set
            {
                this.level = value;
            }
        }

        public string LevelString
        {
            get
            {
                return this.level.ToString();
            }
        }

        [DataMember]
        public bool Oblique
        {
            get
            {
                return this.oblique;
            }
            set
            {
                this.oblique = value;
            }
        }

        [DataMember]
        public int DayCost
        {
            get
            {
                return this.dayCost;
            }
            set
            {
                this.dayCost = value;
            }
        }

        [DataMember]
        public int DaysLeft
        {
            get
            {
                return this.daysLeft;
            }
            set
            {
                this.daysLeft = value;
            }
        }

        [DataMember]
        public int DaysStarted
        {
            get
            {
                return this.daysStarted;
            }
            set
            {
                this.daysStarted = value;
            }
        }

        public string ObliqueString
        {
            get
            {
                return (this.Oblique ? "○" : "×");
            }
        }

        [DataMember]
        public Point Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        public string PositionString
        {
            get
            {
                return this.position.X + ", " + this.position.Y;
            }
        }

        [DataMember]
        public int Radius
        {
            get
            {
                return this.radius;
            }
            set
            {
                this.radius = value;
            }
        }

        public string BelongedArchitectureName
        {
            get
            {
                return this.BelongedArchitecture != null ? this.BelongedArchitecture.Name : "－－－－";
            }
        }
    }
}

