using GameObjects;
using GameObjects.Influences;
using GameObjects.Conditions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GameManager;

namespace GameObjects.ArchitectureDetail
{
    [DataContract]
    public class FacilityKind : GameObject
    {
        private int days;
        private int endurance;
        private int fundCost;

        public InfluenceTable Influences = new InfluenceTable();

        public ConditionTable Conditions = new ConditionTable();

        private int maintenanceCost;
        private int pointCost;
        private bool populationRelated;
        private int positionOccupied;
        private int technologyNeeded;
        private int architectureLimit;
        private int factionLimit;

        public Dictionary<Condition, float> AIBuildConditionWeight = new Dictionary<Condition, float>();

        public void Init()
        {
            Influences = new InfluenceTable();
            Conditions = new ConditionTable();
            AIBuildConditionWeight = new Dictionary<Condition, float>();
        }

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
        public string AIBuildConditionWeightString
        {
            get;
            set;
        }

        [DataMember]
        public float AILevel
        {
            get;
            set;
        }
        [DataMember]
        public bool bukechaichu
        {
            get;
            set;
        }
        [DataMember]
        public int rongna
        {
            get;
            set;
        }

        public GameObjectList GetInfluenceList()
        {
            return this.Influences.GetInfluenceList();
        }

        public GameObjectList GetConditionList()
        {
            return this.Conditions.GetConditionList();
        }

        public double AIValue(Architecture a)
        {
            double influenceValue = double.MinValue;
            foreach (Influence i in this.Influences.GetInfluenceList())
            {
                double weight = i.AIFacilityValue(a);
                if (weight > influenceValue)
                {
                    influenceValue = weight;
                }
            }
            if (influenceValue < 0) return influenceValue;
            return (influenceValue - ((double) this.MaintenanceCost / a.ExpectedFund) * 30.0) * this.AILevel / this.PositionOccupied;
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

        public int DaysText
        {
            get
            {
                return this.days * Session.Parameters.DayInTurn;
            }
        }

        public string Description
        {
            get
            {
                string str = "";
                if (this.rongna > 0)
                {
                    str += "•可以容纳" + this.rongna + "名美女";
                }
                foreach (Influence influence in this.Influences.Influences.Values)
                {
                    str = str + "•" + influence.Description;
                }
                return str;
            }
        }

        public string ConditionString
        {
            get
            {
                string str = "";
                foreach (Condition i in this.Conditions.Conditions.Values)
                {
                    str = str + "•" + i.Name;
                }
                return str;
            }
        }

        [DataMember]
        public int Endurance
        {
            get
            {
                return this.endurance;
            }
            set
            {
                this.endurance = value;
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
        public int MaintenanceCost
        {
            get
            {
                return this.maintenanceCost;
            }
            set
            {
                this.maintenanceCost = value;
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
        public bool PopulationRelated
        {
            get
            {
                return this.populationRelated;
            }
            set
            {
                this.populationRelated = value;
            }
        }

        public string PopulationRelatedString
        {
            get
            {
                return (this.PopulationRelated ? "○" : "×");
            }
        }
        [DataMember]
        public int PositionOccupied
        {
            get
            {
                return this.positionOccupied;
            }
            set
            {
                this.positionOccupied = value;
            }
        }
        [DataMember]
        public int TechnologyNeeded
        {
            get
            {
                return this.technologyNeeded;
            }
            set
            {
                this.technologyNeeded = value;
            }
        }
        [DataMember]
        public int ArchitectureLimit
        {
            get
            {
                return this.architectureLimit;
            }
            set
            {
                this.architectureLimit = value;
            }
        }
        [DataMember]
        public int FactionLimit
        {
            get
            {
                return this.factionLimit;
            }
            set
            {
                this.factionLimit = value;
            }
        }

        public int NetFundIncrease
        {
            get
            {
                int fundIncrease = 0;
                foreach (Influence i in this.Influences.Influences.Values)
                {
                    if (i.Kind.ID == 3000)
                    {
                        try
                        {
                            fundIncrease += int.Parse(i.Parameter);
                        }
                        catch
                        {
                        }
                    }
                    else if (i.Kind.ID == 3020)
                    {
                        return 1;
                    }
                    else if (i.Kind.ID == 3210)
                    {
                        return 1;
                    }
                }
                return fundIncrease - this.MaintenanceCost * 30;
            }
        }

        public bool IsExtension
        {
            get
            {
                if (this.PositionOccupied > 0) return false;
                bool isExtension = false;
                foreach (Influence i in this.Influences.Influences.Values)
                {
                    if (i.Kind.ID == 1000 || i.Kind.ID == 1001 || i.Kind.ID == 1002 || i.Kind.ID == 1003 || i.Kind.ID == 1020 || i.Kind.ID == 1050)
                    {
                        isExtension = true;
                        break;
                    }
                }
                return isExtension;
            }
        }

        public bool CanBuild(Architecture a)
        {
            if (this.PositionOccupied > 0 && a.FacilityPositionCount == 0) return false;
            if (!(!this.PopulationRelated || a.Kind.HasPopulation))
            {
                return false;
            }
            if (a.BelongedFaction != null &&
                a.BelongedFaction.TechniquePoint + a.BelongedFaction.TechniquePointForFacility + a.BelongedFaction.TechniquePointForTechnique < this.PointCost)
            {
                return false;
            }
            if (this.TechnologyNeeded > a.Technology)
            {
                return false;
            }
            if (!Condition.CheckConditionList(this.Conditions.Conditions.Values, a)) return false;
            if (this.ArchitectureLimit < 9999 && a.GetFacilityKindCount(this.ID) >= this.ArchitectureLimit)
            {
                return false;
            }
            if (a.BelongedFaction != null && this.FactionLimit < 9999 && a.BelongedFaction.GetFacilityKindCount(this.ID) >= this.FactionLimit)
            {
                return false;
            }
            
            return true;
        }
    }
}

