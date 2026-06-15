using GameGlobal;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.TroopDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail;

/// <summary>
/// 特技
/// </summary>
[DataContract]
public class Stunt : GameObject
{
    #region DataMember

    /// <summary>
    /// 消耗战意
    /// </summary>
    [DataMember]
    public int Combativity { get; set; }

    /// <summary>
    /// 延续天数
    /// </summary>
    [DataMember]
    public int Period { get; set; }

    /// <summary>
    /// 动画
    /// </summary>
    [DataMember]
    public int Animation { get; set; }

    /// <summary>
    /// 影响列表
    /// </summary>
    [DataMember]
    public string InfluencesString { get; set; }

    /// <summary>
    /// 使用条件列表
    /// </summary>
    [DataMember]
    public string CastConditionsString { get; set; }

    /// <summary>
    /// 修习条件列表
    /// </summary>
    [DataMember]
    public string LearnConditionsString { get; set; }

    /// <summary>
    /// AI触发条件
    /// </summary>
    [DataMember]
    public string AIConditionsString { get; set; }

    /// <summary>
    /// 生成武将条件
    /// </summary>
    [DataMember]
    public string GenerateConditionsString { get; set; }

    /// <summary>
    /// 不同生成武将类型获得机率
    /// </summary>
    [DataMember]
    public int[] GenerationChance { get; set; } = new int[10];
    
    /// <summary>
    /// 此技能的相关能力、0-4为武统智政魅
    /// </summary>
    [DataMember]
    public int RelatedAbility { get; set; }

    #endregion

    public InfluenceTable Influences { get; set; } = new();

    public List<Condition> CastConditions { get; set; } = new();

    public List<Condition> LearnConditions { get; set; } = new();

    public List<Condition> AIConditions { get; set; } = new();

    public List<Condition> GenerateConditions { get; set; } = new();

    public int GetRelatedAbility(Person person)
    {
        switch (RelatedAbility)
        {
            case 0: return person.Strength;
            case 1: return person.Command;
            case 2: return person.Intelligence;
            case 3: return person.Politics;
            case 4: return person.Glamour;
        }
        return 0;
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

    public bool IsAIable(Troop troop)
    {
        return Condition.CheckConditionList(AIConditions, troop);
    }

    public bool IsCastable(Troop troop)
    {
        return Condition.CheckConditionList(CastConditions, troop);
    }

    public bool IsLearnable(Person person)
    {
        return Condition.CheckConditionList(LearnConditions, person);
    }

    public bool CanBeChosenForGenerated(Person person)
    {
        if (LearnConditions.Any(x => x.Kind.ID == 902)) return false;

        return Condition.CheckConditionList(GenerateConditions, person);
    }

    public bool CanBeBorn(Person person)
    {
        if (LearnConditions.Any(x => x.Kind.ID == 901)) return false;

        return Condition.CheckConditionList(GenerateConditions, person);
    }

    public bool CanBeChosenForGenerated() => !LearnConditions.Any(x => x.Kind.ID == 902);

    public void Apply(Troop troop)
    {
        troop.DecreaseCombativity(this.Combativity);
        troop.StuntDayLeft = this.Period;
        foreach (var influence in Influences.Values)
        {
            influence.ApplyInfluence(troop.Leader, Applier.Stunt, 0);
        }
        troop.RefreshAllData();
    }

    public void Purify(Troop troop)
    {
        foreach (var influence in Influences.Values)
        {
            influence.PurifyInfluence(troop.Leader, Applier.Stunt, 0);
        }
    }

    public string AIConditionString => StaticMethods.SaveNameToString(AIConditions);
    
    public string CastConditionString => StaticMethods.SaveNameToString(CastConditions);

    public string LearnConditionString => StaticMethods.SaveNameToString(LearnConditions);

    public string InfluenceString => string.Join("•", Influences.Values.Select(x => x.Description));
}