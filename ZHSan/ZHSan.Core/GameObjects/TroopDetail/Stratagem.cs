using GameObjects.Animations;
using GameObjects.Influences;
using GameObjects.Conditions;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;
using GameGlobal;

namespace GameObjects.TroopDetail;

/// <summary>
/// 计略
/// </summary>
[DataContract]
public class Stratagem : GameObject
{
    #region DataMember

    /// <summary>
    /// 消耗战意
    /// </summary>
    [DataMember]
    public int Combativity { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>
    /// 动画
    /// </summary>
    [DataMember]
    public TileAnimationKind AnimationKind { get; set; }

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

    [DataMember]
    public string AIConditionWeightSelfString { get; set; }

    [DataMember]
    public string AIConditionWeightEnemyString { get; set; }

    
    [DataMember]
    public bool ArchitectureTarget { get; set; }

    [DataMember]
    public int CastDefaultString { get; set; }

    [DataMember]
    public int CastTargetString { get; set; }

    public CastDefaultKind CastDefault { get; set; }

    public CastTargetKind CastTarget { get; set; }

    [DataMember]
    public int Chance { get; set; }

    [DataMember]
    public bool Friendly { get; set; }

    [DataMember]
    public bool Self { get; set; }

    [DataMember]
    public int TechniquePoint { get; set; }

    [DataMember]
    public bool RequireInfluenceToUse { get; set; }

    #endregion

    public InfluenceTable Influences { get; set; } = new();

    public List<Condition> CastConditions { get; set; } = new();
    
    public Dictionary<Condition, float> AIConditionWeightSelf = new();

    public Dictionary<Condition, float> AIConditionWeightEnemy = new();

    public void Apply(Troop troop)
    {
        foreach (var influence in Influences.Values)
        {
            influence.ApplyInfluence(troop, Applier.Stratagem, 0);
        }
    }

    public int GetCredit(Troop source, Troop destination)
    {
        if (!source.HasStratagem(ID)) return 0;

        int num = 0;
        foreach (var influence in Influences.Values)
        {
            num += influence.GetCredit(source, destination);
        }
        return num;
    }

    public bool IsCastable(Troop troop)
    {
        return Condition.CheckConditionList(CastConditions, troop);
    }

    public string CastConditionString => StaticMethods.SaveNameToString(CastConditions);

    public int GetCreditWithPosition(Troop source, out Point? position)
    {
        position = new Point(0, 0);

        int num = 0;
        List<Point?> list = new List<Point?>();
        foreach (var influence in Influences.Values)
        {
            Point? nullable = null;
            num += influence.GetCreditWithPosition(source, out nullable);
            list.Add(nullable);
        }
        if (list.Count > 0)
        {
            position = list[0];
        }
        return num;
    }

    public bool IsValid(Troop troop)
    {
        foreach (var influence in Influences.Values)
        {
            if (!influence.IsVaild(troop)) return false;
        }

        return true;
    }
}