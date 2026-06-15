using GameObjects.Influences;
using GameObjects.Conditions;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.FactionDetail;

/// <summary>
/// 科技
/// </summary>
[DataContract]
public class Technique : GameObject
{
    #region DataMember

    /// <summary>
    /// 种类
    /// </summary>
    [DataMember]
    public int Kind { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>
    /// 升级时间
    /// </summary>
    [DataMember]
    public int Days { get; set; }

    /// <summary>
    /// 资金消耗
    /// </summary>
    [DataMember]
    public int FundCost { get; set; }

    /// <summary>
    /// 技巧点数消耗
    /// </summary>
    [DataMember]
    public int PointCost { get; set; }

    /// <summary>
    /// 需要声望
    /// </summary>
    [DataMember]
    public int Reputation { get; set; }

    /// <summary>
    /// 影响列表
    /// </summary>
    [DataMember]
    public string InfluencesString { get; set; }

    /// <summary>
    /// 前置所需科技ID
    /// </summary>
    [DataMember]
    public int PreID { get; set; }

    /// <summary>
    /// 后置可学科技ID
    /// </summary>
    [DataMember]
    public int PostID { get; set; }

    /// <summary>
    /// 显示列
    /// </summary>
    [DataMember]
    public int DisplayCol { get; set; }

    /// <summary>
    /// 显示行
    /// </summary>
    [DataMember]
    public int DisplayRow { get; set; }

    /// <summary>
    /// AI条件列表
    /// </summary>
    [DataMember]
    public string AIConditionWeightString { get; set; }

    /// <summary>
    /// 条件列表
    /// </summary>
    [DataMember]
    public string ConditionTableString { get; set; }

    #endregion

    public InfluenceTable Influences { get; set; } = new();

    public List<Condition> Conditions { get; set; } = new();

    public Dictionary<Condition, float> AIConditionWeight = new();

    public bool CanResearch(Faction faction)
    {
        return Condition.CheckConditionList(Conditions, faction);
    }
}