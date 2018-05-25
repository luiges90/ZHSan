using GameManager;
using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.FactionDetail
{
    [DataContract]
    public class DiplomaticRelation : GameObject
    {
        private int relation;
        private Faction relationFaction1;
        private int relationFaction1ID;
        private Faction relationFaction2;
        private int relationFaction2ID;
        private int truce;

        public DiplomaticRelation()
        {
            this.relationFaction1ID = -1;
            this.relationFaction2ID = -1;
        }

        public DiplomaticRelation(int faction1ID, int faction2ID, int relation)
        {
            this.relationFaction1ID = -1;
            this.relationFaction2ID = -1;
            this.RelationFaction1ID = faction1ID;
            this.RelationFaction2ID = faction2ID;
            this.Relation = relation;
        }

        public Faction GetDiplomaticFaction(int factionID)
        {
            if (factionID == this.RelationFaction1ID)
            {
                return this.RelationFaction2;
            }
            if (factionID == this.RelationFaction2ID)
            {
                return this.RelationFaction1;
            }
            return null;
        }

        public int GetTheOtherFactionID(int factionID)
        {
            if (factionID == this.RelationFaction1ID)
            {
                return this.RelationFaction2ID;
            }
            return this.RelationFaction1ID;
        }
        [DataMember]
        public int Relation
        {
            get
            {
                return this.relation;
            }
            set
            {
                this.relation = value;
            }
        }
        [DataMember]
        public int Truce
        {
            get
            {
                return this.truce;
            }
            set
            {
                this.truce = value;
            }
        }

        public Faction RelationFaction1
        {
            get
            {
                if (this.relationFaction1 == null)
                {
                    this.relationFaction1 = Session.Current.Scenario.Factions.GetGameObject(this.relationFaction1ID) as Faction;
                }
                return this.relationFaction1;
            }
        }

        [DataMember]
        public int RelationFaction1ID
        {
            get
            {
                return this.relationFaction1ID;
            }
            set
            {
                this.relationFaction1ID = value;
            }
        }

        public string RelationFaction1String
        {
            get
            {
                if (this.RelationFaction1 != null)
                {
                    return this.RelationFaction1.Name;
                }
                return "----";
            }
        }

        public Faction RelationFaction2
        {
            get
            {
                if (this.relationFaction2 == null)
                {
                    this.relationFaction2 = Session.Current.Scenario.Factions.GetGameObject(this.relationFaction2ID) as Faction;
                }
                return this.relationFaction2;
            }
        }

        [DataMember]
        public int RelationFaction2ID
        {
            get
            {
                return this.relationFaction2ID;
            }
            set
            {
                this.relationFaction2ID = value;
            }
        }

        public string RelationFaction2String
        {
            get
            {
                if (this.RelationFaction2 != null)
                {
                    return this.RelationFaction2.Name;
                }
                return "----";
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is DiplomaticRelation)) return false;
            DiplomaticRelation other = (DiplomaticRelation)obj;
            return this.RelationFaction1ID == other.RelationFaction1ID && this.RelationFaction2ID == other.RelationFaction2ID;
        }

        public override int GetHashCode()
        {
            return this.RelationFaction1ID * 31 + this.RelationFaction2ID;
        }
    }
}

