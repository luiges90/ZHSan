using GameObjects;
using GameObjects.Influences;
using GameObjects.Conditions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.FactionDetail
{
    [DataContract]
    public class Technique : GameObject
    {
        private int days;
        private string description;
        private int displayCol;
        private int displayRow;
        private int fundCost;

        [DataMember]
        public string InfluencesString
        {
            get;
            set;
        }

        [DataMember]
        public string ConditionTableString
        {
            get;
            set;
        }

        [DataMember]
        public string AIConditionWeightString
        {
            get;
            set;
        }        

        public InfluenceTable Influences = new InfluenceTable();
        
        public ConditionTable Conditions = new ConditionTable();
        private int kind;
        private int pointCost;
        private int postID;
        private int preID;
        private int reputation;
        
        public Dictionary<Condition, float> AIConditionWeight = new Dictionary<Condition, float>();

        public void Init()
        {
            Influences = new InfluenceTable();
            Conditions = new ConditionTable();
            AIConditionWeight = new Dictionary<Condition, float>();
        }

        public GameObjectList GetInfluenceList()
        {
            return this.Influences.GetInfluenceList();
        }
        [DataMember]
        public int Days
        {
            get
            {
                return this.days;
            }
            set
            {
                this.days = value;
            }
        }
        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        [DataMember]
        public int DisplayCol
        {
            get
            {
                return this.displayCol;
            }
            set
            {
                this.displayCol = value;
            }
        }
        [DataMember]
        public int DisplayRow
        {
            get
            {
                return this.displayRow;
            }
            set
            {
                this.displayRow = value;
            }
        }
        [DataMember]
        public int FundCost
        {
            get
            {
                return this.fundCost;
            }
            set
            {
                this.fundCost = value;
            }
        }

        public int InfluenceCount
        {
            get
            {
                return this.Influences.Count;
            }
        }
        [DataMember]
        public int Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }
        [DataMember]
        public int PointCost
        {
            get
            {
                return this.pointCost;
            }
            set
            {
                this.pointCost = value;
            }
        }
        [DataMember]
        public int PostID
        {
            get
            {
                return this.postID;
            }
            set
            {
                this.postID = value;
            }
        }
        [DataMember]
        public int PreID
        {
            get
            {
                return this.preID;
            }
            set
            {
                this.preID = value;
            }
        }
        [DataMember]
        public int Reputation
        {
            get
            {
                return this.reputation;
            }
            set
            {
                this.reputation = value;
            }
        }

        public bool CanResearch(Faction f)
        {
            return Condition.CheckConditionList(this.Conditions.Conditions.Values, f);
        }
    }
}

