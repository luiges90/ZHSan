using GameGlobal;
using GameManager;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.TroopDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail;

/// <summary>
/// 称号
/// </summary>
[DataContract]
public class Title : GameObject
{
    #region DataMember

    /// <summary>
    /// 类别
    /// </summary>
    [DataMember]
    public TitleKind Kind { get; set; }

    /// <summary>
    /// 等级
    /// </summary>
    [DataMember]
    public int Level { get; set; }

    /// <summary>
    /// 战斗
    /// </summary>
    [DataMember]
    public bool Combat { get; set; }

    /// <summary>
    /// 手动授予
    /// </summary>
    [DataMember]
    public bool ManualAward { get; set; }

    /// <summary>
    /// 薪金
    /// </summary>
    [DataMember]
    public int FundForHolder { get; set; }

    /// <summary>
    /// 影响列表
    /// </summary>
    [DataMember]
    public string InfluencesString { get; set; }

    /// <summary>
    /// 条件列表
    /// </summary>
    [DataMember]
    public string ConditionTableString { get; set; }

    /// <summary>
    /// 生成武將条件
    /// </summary>
    [DataMember]
    public string GenerateConditionsString { get; set; }

    /// <summary>
    /// 建筑条件
    /// </summary>
    [DataMember]
    public string ArchitectureConditionsString { get; set; }

    /// <summary>
    /// 势力条件
    /// </summary>
    [DataMember]
    public string FactionConditionsString { get; set; }

    /// <summary>
    /// 失去条件
    /// </summary>
    [DataMember]
    public string LoseConditionsString { get; set; }

    /// <summary>
    /// 自动习得机率：每天有1除以此数的机率自动习得这个称号。0为不会自动习得
    /// </summary>
    [DataMember]
    public int AutoLearn { get; set; }

    /// <summary>
    /// 习得对话
    /// </summary>
    [DataMember]
    public string AutoLearnText { get; set; }

    /// <summary>
    /// 习得传令官对话
    /// </summary>
    [DataMember]
    public string AutoLearnTextByCourier { get; set; }

    /// <summary>
    /// 全地图数目上限
    /// </summary>
    [DataMember]
    public int MapLimit { get; set; }

    /// <summary>
    /// 势力数目上限
    /// </summary>
    [DataMember]
    public int FactionLimit { get; set; }

    /// <summary>
    /// 继承机率
    /// </summary>
    [DataMember]
    public int InheritChance { get; set; }

    /// <summary>
    /// 不同生成武将类型获得机率
    /// </summary>
    [DataMember]
    public int[] GenerationChance { get; set; } = new int[10];

    #endregion

    //public ConditionTable LoseArchitectureConditions = new ConditionTable(); //失去建筑条件
    // public ConditionTable LoseFactionConditions = new ConditionTable(); //失去势力条件

    public InfluenceTable Influences { get; set; } = new();

    public List<Condition> Conditions { get; set; } = new();

    public List<Condition> GenerateConditions { get; set; } = new();

    public List<Condition> ArchitectureConditions { get; set; } = new();

    public List<Condition> FactionConditions { get; set; } = new();

    public List<Condition> LoseConditions { get; set; } = new();

    public PersonList Persons = new PersonList();

    public void Init()
    {
        Persons = new PersonList();
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
            foreach (var influence in Influences.Values)
            {
                if (influence.Kind.ID == 281)
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
            foreach (var influence in Influences.Values)
            {
                if (influence.Kind.ID == 290)
                {
                    return (MilitaryType)Enum.Parse(typeof(MilitaryType), influence.Parameter);
                }
            }
            return MilitaryType.其他;
        }
    }


    public int MilitaryKindOnly
    {
        get
        {
            foreach (var influence in Influences.Values)
            {
                if (influence.Kind.ID == 300) return influence.GetIntParam();
            }

            return -1;
        }
    }

    public bool CanLearn(Person person)
    {
        return CanLearn(person, false);
    }

    public bool WillLose(Person person) //失去条件
    {
        return Condition.CheckConditionList(LoseConditions, person);
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
        if (!Condition.CheckConditionList(Conditions, person)) return false;
        if (!Condition.CheckConditionList(ArchitectureConditions, person.LocationArchitecture)) return false;
        if (!Condition.CheckConditionList(FactionConditions, person.BelongedFaction)) return false;
        return CheckLimit(person);
    }

    public bool CanBeChosenForGenerated(Person person)
    {
        if (Conditions.Any(x => x.Kind.ID == 902)) return false;

        return Condition.CheckConditionList(GenerateConditions, person);
    }

    public bool CanBeBorn()
    {
        if (Conditions.Any(x => x.Kind.ID == 901)) return false;

        return true;
    }

    public bool CanBeBorn(Person person)
    {
        if (Conditions.Any(x => x.Kind.ID == 901)) return false;

        return Condition.CheckConditionList(GenerateConditions, person);
    }

    public int ConditionCount => Conditions.Count;

    public string Description => string.Join("•", Influences.Values.Select(x => x.Description));

    public int InfluenceCount => Influences.Count;

    public string KindName => Kind.Name;

    public int Merit => (int)AIPersonValue;

    public int FightingMerit => (int)AIFightingPersonValue;

    public int SubOfficerMerit => (int)AISubOfficerPersonValue;

    public string Prerequisite
    {
        get
        {
            var str = StaticMethods.SaveNameToString(Conditions) + 
                      StaticMethods.SaveNameToString(ArchitectureConditions) +
                      StaticMethods.SaveNameToString(FactionConditions);

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
        foreach (var influence in Influences.Values)
        {
            var kind = influence.Kind;

            switch (kind.ID)
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

            var personValue = influence.AIPersonValue;

            aiPersonValue += personValue * d;
            if (kind.Combat)
            {
                aiFightingPersonValue += personValue * d;
            }

            if (!leaderEffective && kind.Combat)
            {
                aiSubofficerPersonValue += personValue * d;
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