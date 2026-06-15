using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.Animations;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail;

/// <summary>
/// 战法
/// </summary>
[DataContract]
public class CombatMethod : GameObject
{
    #region DataMember

    /// <summary>
    /// 所需战意
    /// </summary>
    [DataMember]
    public int Combativity { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>
    /// 影响列表
    /// </summary>
    [DataMember]
    public string InfluencesString { get; set; }

    /// <summary>
    /// 目标可能为建筑
    /// </summary>
    [DataMember]
    public bool ArchitectureTarget { get; set; }

    /// <summary>
    /// 使用条件
    /// </summary>
    [DataMember]
    public string CastConditionsString { get; set; }

    /// <summary>
    /// 视野内敌军越多越有可能使用
    /// </summary>
    [DataMember]
    public bool ViewingHostile { get; set; }

    /// <summary>
    /// 动画
    /// </summary>
    [DataMember]
    public TileAnimationKind AnimationKind { get; set; }

    /// <summary>
    /// 攻击默认类型
    /// </summary>
    [DataMember]
    public int AttackDefaultString { get; set; }

    /// <summary>
    /// 攻击目标类型
    /// </summary>
    [DataMember]
    public int AttackTargetString { get; set; }

    [DataMember]
    public string AIConditionWeightSelfString { get; set; }

    [DataMember]
    public string AIConditionWeightEnemyString { get; set; }

    #endregion

    public AttackDefaultKind AttackDefault { get; set; }

    public AttackTargetKind AttackTarget { get; set; }

    public Influence AI;
    public List<Condition> CastConditions { get; set; } = new();

    public InfluenceTable Influences { get; set; } = new();

    public Dictionary<Condition, float> AIConditionWeightSelf = new();

    public Dictionary<Condition, float> AIConditionWeightEnemy = new();

    public void Apply(Troop troop)
    {
        if ((troop.Combativity + troop.DecrementOfCombatMethodCombativityConsuming) >= this.Combativity)
        {
            troop.CombatMethodApplied = true;
            troop.DecreaseCombativity(this.Combativity - troop.DecrementOfCombatMethodCombativityConsuming);
            troop.ShowNumber = true;
            foreach (var influence in Influences.Values)
            {
                influence.ApplyInfluence(troop.Leader, Applier.CombatMethod, 0);
            }
        }
    }

    public bool IsCastable(Troop troop)
    {
        return Condition.CheckConditionList(CastConditions, troop);
    }

    public void Purify(Troop troop)
    {
        if (troop.CombatMethodApplied)
        {
            troop.CombatMethodApplied = false;
            foreach (var influence in Influences.Values)
            {
                influence.PurifyInfluence(troop.Leader, Applier.CombatMethod, 0);
            }
        }
    }

    public bool SimulateApply(Troop troop)
    {
        foreach (var influence in Influences.Values)
        {
            influence.ApplyInfluence(troop.Leader, Applier.CombatMethod, 0);
        }
        return true;
    }

    public void SimulatePurify(Troop troop)
    {
        foreach (var influence in Influences.Values)
        {
            influence.PurifyInfluence(troop.Leader, Applier.CombatMethod, 0);
        }
    }
}