using GameManager;
using GameObjects;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.TroopDetail;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class Stunt : GameObject
    {
        [DataMember]
        public string InfluencesString
        {
            get;
            set;
        }

        [DataMember]
        public string AIConditionsString
        {
            get;
            set;
        }

        [DataMember]
        public string CastConditionsString
        {
            get;
            set;
        }

        [DataMember]
        public string LearnConditionsString
        {
            get;
            set;
        }

        [DataMember]
        public string GenerateConditionsString
        {
            get;
            set;
        }

        public ConditionTable AIConditions = new ConditionTable();
        private int animation;

        public ConditionTable CastConditions = new ConditionTable();
        private int combativity;

        public InfluenceTable Influences = new InfluenceTable();

        public ConditionTable LearnConditions = new ConditionTable();
        private int period;

        public ConditionTable GenerateConditions = new ConditionTable();

        public void Init()
        {
            AIConditions = new ConditionTable();
            CastConditions = new ConditionTable();
            Influences = new InfluenceTable();
            LearnConditions = new ConditionTable();
            GenerateConditions = new ConditionTable();
        }

        private int[] generationChance = new int[10];

        [DataMember]
        public int[] GenerationChance
        {
            get
            {
                return generationChance;
            }
            set
            {
                generationChance = value;
            }
        }
        [DataMember]
        public int RelatedAbility { get; set; }

        public int GetRelatedAbility(Person p)
        {
            switch (RelatedAbility)
            {
                case 0: return p.Strength;
                case 1: return p.Command;
                case 2: return p.Intelligence;
                case 3: return p.Politics;
                case 4: return p.Glamour;
            }
            return 0;
        }

        public MilitaryType MilitaryTypeOnly
        {
            get
            {
                foreach (Influence i in this.Influences.Influences.Values)
                {
                    if (i.Kind.ID == 290)
                    {
                        return (MilitaryType)Enum.Parse(typeof(MilitaryType), i.Parameter);
                    }
                }
                return MilitaryType.其他;
            }
        }

        public void Apply(Troop troop)
        {
            troop.DecreaseCombativity(this.Combativity);
            troop.StuntDayLeft = this.Period;
            foreach (Influence influence in this.Influences.Influences.Values)
            {
                influence.ApplyInfluence(troop.Leader, Applier.Stunt, 0, false);
            }
            troop.RefreshAllData();
        }

        public GameObjectList GetAIConditionList()
        {
            return this.AIConditions.GetConditionList();
        }

        public GameObjectList GetCastConditionList()
        {
            return this.CastConditions.GetConditionList();
        }

        public GameObjectList GetInfluenceList()
        {
            return this.Influences.GetInfluenceList();
        }

        public GameObjectList GetLearnConditionList()
        {
            return this.LearnConditions.GetConditionList();
        }

        public bool IsAIable(Troop troop)
        {
            return Condition.CheckConditionList(this.AIConditions.Conditions.Values, troop);
        }

        public bool IsCastable(Troop troop)
        {
            if (this.CastConditions == null) return false;
            return Condition.CheckConditionList(this.CastConditions.Conditions.Values, troop);
        }

        public bool IsLearnable(Person person)
        {
            return this.LearnConditions.CheckCondition(person);
        }

        public bool CanBeChosenForGenerated(Person p)
        {
            foreach (Condition condition in this.LearnConditions.Conditions.Values)
            {
                if (condition.Kind.ID == 902) return false;
            }
            return this.GenerateConditions.CheckCondition(p);
        }

        public bool CanBeBorn(Person person)
        {
            foreach (Condition condition in this.LearnConditions.Conditions.Values)
            {
                if (condition.Kind.ID == 901) return false;
            }
            return this.GenerateConditions.CheckCondition(person);
        }

        public void Purify(Troop troop)
        {
            foreach (Influence influence in this.Influences.Influences.Values)
            {
                influence.PurifyInfluence(troop.Leader, Applier.Stunt, 0, false);
            }
        }

        public string AIConditionString
        {
            get
            {
                string str = "";
                foreach (Condition condition in this.AIConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                return str;
            }
        }
        [DataMember]
        public int Animation
        {
            get
            {
                return this.animation;
            }
            set
            {
                this.animation = value;
            }
        }

        public string CastConditionString
        {
            get
            {
                string str = "";
                foreach (Condition condition in this.CastConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                return str;
            }
        }
        [DataMember]
        public int Combativity
        {
            get
            {
                return this.combativity;
            }
            set
            {
                this.combativity = value;
            }
        }

        public string InfluenceString
        {
            get
            {
                string str = "";
                foreach (Influence influence in this.Influences.Influences.Values)
                {
                    str = str + "•" + influence.Description;
                }
                return str;
            }
        }

        public string LearnConditionString
        {
            get
            {
                string str = "";
                foreach (Condition condition in this.LearnConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                return str;
            }
        }
        [DataMember]
        public int Period
        {
            get
            {
                return this.period;
            }
            set
            {
                this.period = value;
            }
        }

        public bool CanBeChosenForGenerated()
        {
            foreach (Condition condition in this.LearnConditions.Conditions.Values)
            {
                if (condition.Kind.ID == 902) return false;
            }
            return true;
        }
    }
}

