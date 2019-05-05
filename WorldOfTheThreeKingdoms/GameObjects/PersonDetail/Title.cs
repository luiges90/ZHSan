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
    public class Title : GameObject
    {
        private bool combat;

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
        public string ArchitectureConditionsString
        {
            get;
            set;
        }

        [DataMember]
        public string FactionConditionsString
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

        [DataMember]
        public string LoseConditionsString
        {
            get;
            set;
        }

        public ConditionTable Conditions = new ConditionTable();
        
        public ConditionTable LoseConditions = new ConditionTable(); //失去条件

        //public ConditionTable LoseArchitectureConditions = new ConditionTable(); //失去建筑条件
        // public ConditionTable LoseFactionConditions = new ConditionTable(); //失去势力条件
        
        public InfluenceTable Influences = new InfluenceTable();
        private TitleKind kind;
        private int level;

        
        public ConditionTable ArchitectureConditions = new ConditionTable();
        
        public ConditionTable FactionConditions = new ConditionTable();
        
        public ConditionTable GenerateConditions = new ConditionTable();

        public PersonList Persons = new PersonList();

        public void Init()
        {
            Conditions = new ConditionTable();
            LoseConditions = new ConditionTable();
            Influences = new InfluenceTable();
            ArchitectureConditions = new ConditionTable();
            FactionConditions = new ConditionTable();
            GenerateConditions = new ConditionTable();
            Persons = new PersonList();
        }

        [DataMember]
        public bool ManualAward
        {
            get;
            set;
        }
        [DataMember]
        public int AutoLearn
        {
            get;
            set;
        }
        [DataMember]
        public string AutoLearnText
        {
            get;
            set;
        }
        [DataMember]
        public string AutoLearnTextByCourier
        {
            get;
            set;
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
        public int MapLimit
        {
            get;
            set;
        }
        [DataMember]
        public int FactionLimit
        {
            get;
            set;
        }
        [DataMember]
        public int InheritChance
        {
            get;
            set;
        }

        private bool? containsLeaderOnlyCache = null;
        public bool ContainsLeaderOnly
        {
            get
            {
                if (containsLeaderOnlyCache != null)
                {
                    return containsLeaderOnlyCache.Value;
                }
                foreach (Influence i in this.Influences.Influences.Values)
                {
                    if (i.Kind.ID == 281)
                    {
                        containsLeaderOnlyCache = true;
                        return true;
                    }
                }
                containsLeaderOnlyCache = false;
                return false;
            }
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


        public int MilitaryKindOnly
        {
            get
            {
                foreach (Influence i in this.Influences.Influences.Values)
                {
                    if (i.Kind.ID == 300)
                    {
                        return int.Parse(i.Parameter);
                    }
                }
                return -1;
            }
        }

        public void AddInfluence(Influence influence)
        {
            this.Influences.AddInfluence(influence);
        }

        public bool CanLearn(Person person)
        {
            return CanLearn(person, false);
        }

        public bool WillLose(Person person) //失去条件
        {
            return this.LoseConditions.CheckCondition(person);
        }

        public bool CheckLimit(Person person)
        {
             if (person.BelongedFaction != null && person.BelongedFaction.PersonCount > this.FactionLimit)
            {
                int cnt = 0;
                foreach (Person p in person.BelongedFaction.Persons)
                {
                    if (p.Titles.Contains(this))
                    {
                        cnt++;
                    }
                }
                if (cnt >= this.FactionLimit) return false;
            }
            if (Session.Current.Scenario.Persons.Count > this.MapLimit)
            {
                int cnt = 0;
                foreach (Person p in Session.Current.Scenario.Persons)
                {
                    if ((p.Alive || p.Available) && p.Titles.Contains(this))
                    {
                        cnt++;
                    }
                }
                if (cnt >= this.MapLimit) return false;
            }
            return true;
        }

        public bool CanLearn(Person person, bool ignoreAutoLearn)
        {
            if (AutoLearn > 0 && !ignoreAutoLearn) return false;
            if (this.ManualAward && !ignoreAutoLearn) return false;
            if (!Condition.CheckConditionList(this.Conditions.Conditions.Values, person)) return false;
            if (!Condition.CheckConditionList(this.ArchitectureConditions.Conditions.Values, person.LocationArchitecture)) return false;
            if (!Condition.CheckConditionList(this.FactionConditions.Conditions.Values, person.BelongedFaction)) return false;
            return CheckLimit(person);
        }

        public bool CanBeChosenForGenerated(Person p)
        {
            foreach (Condition condition in this.Conditions.Conditions.Values)
            {
                if (condition.Kind.ID == 902) return false;
            }
            return this.GenerateConditions.CheckCondition(p);
        }

        public bool CanBeBorn()
        {
            foreach (Condition condition in this.Conditions.Conditions.Values)
            {
                if (condition.Kind.ID == 901) return false;
            }
            return true;
        }

        public bool CanBeBorn(Person person)
        {
            foreach (Condition condition in this.Conditions.Conditions.Values)
            {
                if (condition.Kind.ID == 901) return false;
            }
            return this.GenerateConditions.CheckCondition(person);
        }

        public GameObjectList GetConditionList()
        {
            return this.Conditions.GetConditionList();
        }

        public GameObjectList GetInfluenceList()
        {
            return this.Influences.GetInfluenceList();
        }
        [DataMember]
        public bool Combat
        {
            get
            {
                return this.combat;
            }
            set
            {
                this.combat = value;
            }
        }

        public int ConditionCount
        {
            get
            {
                return this.Conditions.Count;
            }
        }

        private string DescriptionCache;
        public string Description
        {
            get
            {
                if (DescriptionCache == null)
                {
                    string str = "";
                    foreach (Influence influence in this.Influences.Influences.Values)
                    {
                        str = str + "•" + influence.Description;
                    }
                    DescriptionCache = str;
                }
                return DescriptionCache;
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
        public TitleKind Kind
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

        public string KindName
        {
            get
            {
                return this.Kind.Name;
            }
        }
        [DataMember]
        public int Level
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
        
        public int Merit
        {
            get
            {
                return (int) AIPersonValue;
            }
        }

        private int? fightingMerit = null;
        public int FightingMerit
        {
            get
            {
                return (int) AIFightingPersonValue;
            }
        }

        public int SubOfficerMerit
        {
            get
            {
                return (int) AISubOfficerPersonValue;
            }
        }

        public string Prerequisite
        {
            get
            {
                string str = "";
                foreach (Condition condition in this.Conditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                foreach (Condition condition in this.ArchitectureConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                foreach (Condition condition in this.FactionConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                /*
                foreach (Condition condition in this.LoseConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                */
              
                return str;
            }
        }

        public string DetailedName
        {
            get
            {
                return this.Level + "级" + this.KindName + "「" + this.Name + "」";
            }
        }

        private double? aiPersonValue = null;
        public double AIPersonValue
        {
            get
            {
                if (aiPersonValue != null)
                {
                    return aiPersonValue.Value;
                }

                calculatePersonValues();
                return aiPersonValue.Value;
            }
        }

        private double? aiFightingPersonValue = null;
        public double AIFightingPersonValue
        {
            get
            {
                if (aiFightingPersonValue != null)
                {
                    return aiFightingPersonValue.Value;
                }

                calculatePersonValues();
                return aiFightingPersonValue.Value;
            }
        }

        private double? aiSubofficerPersonValue = null;
        public double AISubOfficerPersonValue
        {
            get
            {
                if (aiSubofficerPersonValue != null)
                {
                    return aiSubofficerPersonValue.Value;
                }

                calculatePersonValues();
                return aiSubofficerPersonValue.Value;
            }
        }

        private void calculatePersonValues()
        {
            double d = 1;
            bool hasKind = false;
            bool hasType = false;

            aiPersonValue = 0;
            aiFightingPersonValue = 0;
            aiSubofficerPersonValue = 0;
            bool leaderEffective = false;
            foreach (Influence i in this.Influences.GetInfluenceList())
            {
                switch (i.Kind.ID)
                {
                    case 281:
                        d *= 0.8;
                        leaderEffective = true;
                        break;
                    case 290:
                        if (hasKind)
                        {
                            d *= 1.2;
                        }
                        else
                        {
                            hasKind = true;
                            d *= 0.4;
                        }
                        break;
                    case 300:
                        if (hasType)
                        {
                            d *= 1.1;
                            if (d > 1)
                            {
                                d = 1;
                            }
                        }
                        else
                        {
                            hasKind = true;
                            d *= 0.2;
                        }
                        break;
                }
                aiPersonValue += i.AIPersonValue * d;
                if (i.Kind.Combat)
                {
                    aiFightingPersonValue += i.AIPersonValue * d;
                }
                if (!leaderEffective && i.Kind.Combat)
                {
                    aiSubofficerPersonValue += i.AIPersonValue * d;
                }
            }
        }

        private int? aiPersonLevel = null;
        public int AIPersonLevel
        {
            get
            {
                if (aiPersonLevel != null)
                {
                    return aiPersonLevel.Value;
                }
                if (AIPersonValue < 14)
                {
                    aiPersonLevel = 1;
                }
                else
                {
                    double a = 35.0 / 11.0;
                    float b = 5;
                    double c = 14 - AIPersonValue;
                    aiPersonLevel = (int)Math.Ceiling((-b + Math.Sqrt(b * b - 4 * a * c)) / (2 * a));
                }
                return aiPersonLevel.Value;
            }
        }

        public static Dictionary<TitleKind, List<Title>> GetKindTitleDictionary()
        {
            GameObjectList rawTitles = Session.Current.Scenario.GameCommonData.AllTitles.GetTitleList().GetRandomList();
            Dictionary<TitleKind, List<Title>> titles = new Dictionary<TitleKind, List<Title>>();
            foreach (Title t in rawTitles)
            {
                if (!titles.ContainsKey(t.Kind))
                {
                    titles[t.Kind] = new List<Title>();
                }
                titles[t.Kind].Add(t);
            }
            return titles;
        }
    }
}

