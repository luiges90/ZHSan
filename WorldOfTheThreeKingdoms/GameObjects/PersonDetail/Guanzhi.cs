using GameObjects;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.TroopDetail;
using System;
using System.Collections.Generic;


namespace GameObjects.PersonDetail
{
    /*
    public class Guanzhi : GameObject
    {
        
        private bool combat;

        public ConditionTable Conditions = new ConditionTable();
        public ConditionTable LoseConditions = new ConditionTable(); //失去条件

        public InfluenceTable Influences = new InfluenceTable();
        private int level;
        private GuanzhiKind kind;

        public ConditionTable FactionConditions = new ConditionTable();

        public bool AutoAward
        {
            get;
            set;
        }

        public PersonList Persons = new PersonList();


         public string AutoLearnText
        {
            get;
            set;
        }

        public string AutoLearnTextByCourier
        {
            get;
            set;
        }

         public int MapLimit
        {
            get;
            set;
        }

        public int FactionLimit
        {
            get;
            set;
        }

        public void AddInfluence(Influence influence)
        {
            this.Influences.AddInfluence(influence);
        }

        
        public bool WillLose(Person person) //失去条件
        {
            foreach (Condition condition in this.LoseConditions.Conditions.Values)
            {
                if (!condition.CheckCondition(person))
                {
                    return false;
                }
            }

            return true;
        }
        /*
        public bool CheckLimit(Person person)
        {
             if (person.BelongedFaction != null && person.BelongedFaction.PersonCount > this.FactionLimit)
            {
                int cnt = 0;
                foreach (Person p in person.BelongedFaction.Persons)
                {
                    if (p.Guanzhis.Contains(this))
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
                    if ((p.Alive || p.Available) && p.Guanzhis.Contains(this))
                    {
                        cnt++;
                    }
                }
                if (cnt >= this.MapLimit) return false;
            }
            return true;
        }

        public bool CanAward(Person person)
        {
            foreach (Condition condition in this.Conditions.Conditions.Values)
            {
                if (!condition.CheckCondition(person))
                {
                    return false;
                }
            }
            
            foreach (Condition condition in this.FactionConditions.Conditions.Values)
            {
                if (person.BelongedFaction == null) return false;
                if (!condition.CheckCondition(person.BelongedFaction)) return false;
            }
           
            return CheckLimit(person);
        }
        
        public GameObjectList GetConditionList()
        {
            return this.Conditions.GetConditionList();
        }

        public GameObjectList GetInfluenceList()
        {
            return this.Influences.GetInfluenceList();
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

        public GuanzhiKind Kind
        {
            get
            {
                return this.kind ;
            }
            set 
            {
                this.kind = value ;
            }
        }

        public string KindName
        {
            get
            {
                return this.Kind.Name;
            }
        }

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

        public string Prerequisite
        {
            get
            {
                string str = "";
                foreach (Condition condition in this.Conditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                
                foreach (Condition condition in this.FactionConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
                foreach (Condition condition in this.LoseConditions.Conditions.Values)
                {
                    str = str + "•" + condition.Name;
                }
              
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

        public static Dictionary<GuanzhiKind, List<Guanzhi>> GetKindGuanzhiDictionary()
        {
            GameObjectList rawGuanzhis = scen.GameCommonData.AllGuanzhis.GetGuanzhiList().GetRandomList();
            Dictionary<GuanzhiKind, List<Guanzhi>> guanzhis = new Dictionary<GuanzhiKind, List<Guanzhi>>();
            foreach (Guanzhi g in rawGuanzhis)
            {
                if (!guanzhis.ContainsKey(g.Kind))
                {
                    guanzhis[g.Kind] = new List<Guanzhi>();
                }
                guanzhis[g.Kind].Add(g);
            }
            return guanzhis;
        }
    }
    */
}
