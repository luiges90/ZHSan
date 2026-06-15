using GameObjects.Influences;
using GameObjects.Conditions;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GameManager;
using Microsoft.Xna.Framework;
using GameGlobal;

namespace GameObjects.TroopDetail;

/// <summary>
/// 兵种类型
/// </summary>
[DataContract]
public class MilitaryKind : GameObject
{
    #region DataMember

    /// <summary>
    /// 类别
    /// </summary>
    [DataMember]
    public MilitaryType Type { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>
    /// 强度（AI）
    /// </summary>
    [DataMember]
    public int Merit { get; set; }

    /// <summary>
    /// 较强兵种ID：如果AI准备征召这个兵种的话，会考虑征召在这列表中的兵种，而这列表的兵种是绝对强于这个兵种
    /// </summary>
    [DataMember]
    public string SuccessorString { get; set; }

    /// <summary>
    /// 行动速率：行动速率高的部队将优先行动。行动速率＝兵种本身的行动速率×士气÷士气上限
    /// </summary>
    [DataMember]
    public int Speed { get; set; }

    /// <summary>
    /// 获得机率：每天有1除以此数的机率，拥有武将的势力可获得这个兵种
    /// </summary>
    [DataMember]
    public int ObtainProb { get; set; }

    /// <summary>
    /// 出兵称号影响
    /// </summary>
    [DataMember]
    public int TitleInfluence { get; set; } = -1;

    /// <summary>
    /// 新建资金
    /// </summary>
    [DataMember]
    public int CreateCost { get; set; }

    /// <summary>
    /// 新建所需技术
    /// </summary>
    [DataMember]
    public int CreateTechnology { get; set; }

    /// <summary>
    /// 水边新建
    /// </summary>
    [DataMember]
    public bool CreateBesideWater { get; set; }

    /// <summary>
    /// 攻击
    /// </summary>
    [DataMember]
    public int Offence { get; set; }

    /// <summary>
    /// 防御
    /// </summary>
    [DataMember]
    public int Defence { get; set; }

    /// <summary>
    /// 攻击半径
    /// </summary>
    [DataMember]
    public int OffenceRadius { get; set; }

    /// <summary>
    /// 能否反击
    /// </summary>
    [DataMember]
    public bool CounterOffence { get; set; }

    /// <summary>
    /// 能否被反击
    /// </summary>
    [DataMember]
    public bool BeCountered { get; set; }

    /// <summary>
    /// 斜向攻击
    /// </summary>
    [DataMember]
    public bool ObliqueOffence { get; set; }

    /// <summary>
    /// 箭矢攻击：弓箭攻击，投石车等部队不属于弓箭攻击
    /// </summary>
    [DataMember]
    public bool ArrowOffence { get; set; }

    /// <summary>
    /// 凌空攻击：是否可以攻击建筑内的部队
    /// </summary>
    [DataMember]
    public bool AirOffence { get; set; }

    /// <summary>
    /// 近身攻击
    /// </summary>
    [DataMember]
    public bool ContactOffence { get; set; }

    /// <summary>
    /// 建筑伤害系数
    /// </summary>
    [DataMember]
    public float ArchitectureDamageRate { get; set; }

    /// <summary>
    /// 建筑反击承受率
    /// </summary>
    [DataMember]
    public float ArchitectureCounterDamageRate { get; set; }

    /// <summary>
    /// 计略范围
    /// </summary>
    [DataMember]
    public int StratagemRadius { get; set; }

    /// <summary>
    /// 斜向计略
    /// </summary>
    [DataMember]
    public bool ObliqueStratagem { get; set; }

    /// <summary>
    /// 视野半径
    /// </summary>
    [DataMember]
    public int ViewRadius { get; set; }

    /// <summary>
    /// 斜向视野
    /// </summary>
    [DataMember]
    public bool ObliqueView { get; set; }

    /// <summary>
    /// 伤兵概率
    /// </summary>
    [DataMember]
    public int InjuryChance { get; set; }

    /// <summary>
    /// 行动力
    /// </summary>
    [DataMember]
    public int Movability { get; set; }

    /// <summary>
    /// 单一适性种类
    /// </summary>
    [DataMember]
    public int OneAdaptabilityKind { get; set; }

    /// <summary>
    /// 平原适性
    /// </summary>
    [DataMember]
    public int PlainAdaptability { get; set; }

    /// <summary>
    /// 草地适性
    /// </summary>
    [DataMember]
    public int GrasslandAdaptability { get; set; }

    /// <summary>
    /// 森林适性
    /// </summary>
    [DataMember]
    public int ForrestAdaptability { get; set; }

    /// <summary>
    /// 湿地适性
    /// </summary>
    [DataMember]
    public int MarshAdaptability { get; set; }

    /// <summary>
    /// 山地适性
    /// </summary>
    [DataMember]
    public int MountainAdaptability { get; set; }

    /// <summary>
    /// 水域适性
    /// </summary>
    [DataMember]
    public int WaterAdaptability { get; set; }

    /// <summary>
    /// 峻岭适性
    /// </summary>
    [DataMember]
    public int RidgeAdaptability { get; set; }

    /// <summary>
    /// 荒地适性
    /// </summary>
    [DataMember]
    public int WastelandAdaptability { get; set; }

    /// <summary>
    /// 沙漠适性
    /// </summary>
    [DataMember]
    public int DesertAdaptability { get; set; }

    /// <summary>
    /// 棧道适性
    /// </summary>
    [DataMember]
    public int CliffAdaptability { get; set; }

    /// <summary>
    /// 平原乘数
    /// </summary>
    [DataMember]
    public float PlainRate { get; set; }

    /// <summary>
    /// 草地乘数
    /// </summary>
    [DataMember]
    public float GrasslandRate { get; set; }

    /// <summary>
    /// 森林乘数
    /// </summary>
    [DataMember]
    public float ForrestRate { get; set; }

    /// <summary>
    /// 湿地乘数
    /// </summary>
    [DataMember]
    public float MarshRate { get; set; }

    /// <summary>
    /// 山地乘数
    /// </summary>
    [DataMember]
    public float MountainRate { get; set; }

    /// <summary>
    /// 水域乘数
    /// </summary>
    [DataMember]
    public float WaterRate { get; set; }

    /// <summary>
    /// 峻岭乘数
    /// </summary>
    [DataMember]
    public float RidgeRate { get; set; }

    /// <summary>
    /// 荒地乘数
    /// </summary>
    [DataMember]
    public float WastelandRate { get; set; }

    /// <summary>
    /// 沙漠乘数
    /// </summary>
    [DataMember]
    public float DesertRate { get; set; }

    /// <summary>
    /// 棧道乘数
    /// </summary>
    [DataMember]
    public float CliffRate { get; set; }

    /// <summary>
    /// 受火伤率
    /// </summary>
    [DataMember]
    public float FireDamageRate { get; set; }

    /// <summary>
    /// 势力编队上限
    /// </summary>
    [DataMember]
    public int RecruitLimit { get; set; }

    /// <summary>
    /// 每个士兵每天消耗的粮草数
    /// </summary>
    [DataMember]
    public int FoodPerSoldier { get; set; }

    /// <summary>
    /// 口粮天数
    /// </summary>
    [DataMember]
    public int RationDays { get; set; }

    /// <summary>
    /// 每补充1人所需的技巧点数
    /// </summary>
    [DataMember]
    public int PointsPerSoldier { get; set; }

    /// <summary>
    /// 成军最小规模
    /// </summary>
    [DataMember]
    public int MinScale { get; set; }

    /// <summary>
    /// 一个单位规模所增加的攻击力
    /// </summary>
    [DataMember]
    public int OffencePerScale { get; set; }

    /// <summary>
    /// 一个单位规模所增加的防御力
    /// </summary>
    [DataMember]
    public int DefencePerScale { get; set; }

    /// <summary>
    /// 最大规模
    /// </summary>
    [DataMember]
    public int MaxScale { get; set; }

    /// <summary>
    /// 能否升级
    /// </summary>
    [DataMember]
    public bool CanLevelUp { get; set; }

    /// <summary>
    /// 升级成的兵种ID
    /// </summary>
    [DataMember]
    public List<int> LevelUpKindID { get; set; } = new();

    /// <summary>
    /// 升级经验
    /// </summary>
    [DataMember]
    public int LevelUpExperience { get; set; }

    /// <summary>
    /// 每一百经验增加的攻击力
    /// </summary>
    [DataMember]
    public int OffencePer100Experience { get; set; }

    /// <summary>
    /// 每一百经验增加的防御力
    /// </summary>
    [DataMember]
    public int DefencePer100Experience { get; set; }

    /// <summary>
    /// 影响列表
    /// </summary>
    [DataMember]
    public string InfluencesString { get; set; }

    /// <summary>
    /// 最低统率(AI)
    /// </summary>
    [DataMember]
    public int MinCommand { get; set; }

    /// <summary>
    /// 新编条件：编队所在建筑条件
    /// </summary>
    [DataMember]
    public string CreateConditionsString { get; set; }

    /// <summary>
    /// 资金上限
    /// </summary>
    [DataMember]
    public int zijinshangxian { get; set; }

    /// <summary>
    /// 攻击默认类型
    /// </summary>
    [DataMember]
    public TroopAttackDefaultKind AttackDefaultKind { get; set; }

    /// <summary>
    /// 攻击目标类型
    /// </summary>
    [DataMember]
    public TroopAttackTargetKind AttackTargetKind { get; set; }

    /// <summary>
    /// 施展默认类型
    /// </summary>
    [DataMember]
    public TroopCastDefaultKind CastDefaultKind { get; set; }

    /// <summary>
    /// 施展目标类型
    /// </summary>
    [DataMember]
    public TroopCastTargetKind CastTargetKind { get; set; }

    /// <summary>
    /// 是否外壳
    /// </summary>
    [DataMember]
    public bool IsShell { get; set; }

    /// <summary>
    /// 只能在移动前攻击
    /// </summary>
    [DataMember]
    public bool OffenceOnlyBeforeMove { get; set; }

    /// <summary>
    /// 变换至兵种
    /// </summary>
    [DataMember]
    public int MorphToKindId { get; set; }

    [DataMember]
    public string AICreateArchitectureConditionWeightString { get; set; }

    [DataMember]
    public string AIUpgradeArchitectureConditionWeightString { get; set; }

    [DataMember]
    public string AIUpgradeLeaderConditionWeightString { get; set; }

    [DataMember]
    public string AILeaderConditionWeightString { get; set; }

    #endregion

    //[DataMember]
    public TroopSounds Sounds;

    public TroopTextures Textures;

    public InfluenceTable Influences { get; set; } = new();

    public List<Condition> CreateConditions { get; set; } = new();

    public Dictionary<Condition, float> AICreateArchitectureConditionWeight = new();

    public Dictionary<Condition, float> AIUpgradeArchitectureConditionWeight = new();
    
    public Dictionary<Condition, float> AIUpgradeLeaderConditionWeight = new();

    public Dictionary<Condition, float> AILeaderConditionWeight = new();

    public PersonList Persons = new PersonList();

    public void Init()
    {
        Persons = new PersonList();
    }

    public MilitaryKindTable successor;
    private bool findSuccessor_visited;

    public bool LevelUpAvail(Architecture arch)
    {
        return CheckConditions(arch) && GetLevelUpKinds(arch).Count > 0;
    }

    public bool CreateAvail(Architecture arch)
    {
        if (IsShell) return false;
        
        if (arch.Fund < CreateCost * GetRateOfNewMilitary(arch) || arch.Technology < CreateTechnology)
        {
            return false;
        }

        if (arch.BelongedFaction.IsMilitaryKindOverLimit(ID))
        {
            return false;
        }

        if (CreateBesideWater && arch.IsBesideWater)
        {
            return false;
        }

        if (!CheckConditions(arch))
        {
            return false;
        }

        return true;
    }

    public bool IsTransport => ID == 29;

    public MilitaryKind findSuccessorCreatable(MilitaryKindList allMilitaryKinds, Architecture recruiter)
    {
        foreach (MilitaryKind i in allMilitaryKinds)
        {
            i.findSuccessor_visited = false;
        }
        return findSuccessorRecruitable_r(allMilitaryKinds, recruiter, this);
    }

    private MilitaryKind findSuccessorRecruitable_r(MilitaryKindList allMilitaryKinds, Architecture recruiter, MilitaryKind prev)
    {
        if (prev.successor.GetMilitaryKindList().Count == 0)
        {
            return prev;
        }
        prev.findSuccessor_visited = true;
        MilitaryKindList toVisit = new MilitaryKindList();
        foreach (MilitaryKind i in prev.successor.GetMilitaryKindList())
        {
            if (!i.findSuccessor_visited && recruiter.GetNewMilitaryKindList().GameObjects.Contains(i) && allMilitaryKinds.GetList().GameObjects.Contains(i))
            {
                toVisit.Add(i);
            }
        }
        if (toVisit.Count == 0)
        {
            return prev;
        }
        return findSuccessorRecruitable_r(allMilitaryKinds, recruiter, toVisit[GameObject.Random(toVisit.Count)] as MilitaryKind);
    }

    public float GetRateOfNewMilitary(Architecture arch)
    {
        switch (Type)
        {
            case MilitaryType.步兵:
                return arch.RateOfNewBubingMilitaryFundCost;

            case MilitaryType.弩兵:
                return arch.RateOfNewNubingMilitaryFundCost;

            case MilitaryType.骑兵:
                return arch.RateOfNewQibingMilitaryFundCost;

            case MilitaryType.水军:
                return arch.RateOfNewShuijunMilitaryFundCost;

            case MilitaryType.器械:
                return arch.RateOfNewQixieMilitaryFundCost;
        }
        
        return 1f;
    }

    public int[] Adaptabilities
    {
        get
        {
            return [PlainAdaptability, GrasslandAdaptability, ForrestAdaptability, WastelandAdaptability, MarshAdaptability,
                    MountainAdaptability, CliffAdaptability, RidgeAdaptability, WaterAdaptability];
        }
    }

    public bool Movable
    {
        get
        {
            foreach (int i in Adaptabilities)
            {
                if (Movability >= i)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public int GetTerrainAdaptability(TerrainKind terrain)
    {
        switch (terrain)
        {
            case TerrainKind.无:
                return 0xdac;

            case TerrainKind.平原:
                return PlainAdaptability;

            case TerrainKind.草原:
                return GrasslandAdaptability;

            case TerrainKind.森林:
                return ForrestAdaptability;

            case TerrainKind.湿地:
                return MarshAdaptability;

            case TerrainKind.山地:
                return MountainAdaptability;

            case TerrainKind.水域:
                return WaterAdaptability;

            case TerrainKind.峻岭:
                return RidgeAdaptability;

            case TerrainKind.荒地:
                return WastelandAdaptability;

            case TerrainKind.沙漠:
                return DesertAdaptability;

            case TerrainKind.栈道:
                return CliffAdaptability;
        }
        return 0xdac;
    }

    public bool IsMovableOnPosition(Point position)
    {
        return GetTerrainAdaptability(Session.Current.Scenario.GetTerrainKindByPosition(position)) <= Movability;
    }

    public override string ToString() => $"{Name} {Type}";
    
    public string ArrowOffenceString => StaticMethods.ToMark(ArrowOffence);
    
    public string BeCounteredString => StaticMethods.ToMark(BeCountered);
    
    public string CanLevelUpString => StaticMethods.ToMark(CanLevelUp);
    
    public string ContactOffenceString => StaticMethods.ToMark(ContactOffence);

    public string CounterOffenceString => StaticMethods.ToMark(CounterOffence);

    public string CreateBesideWaterString => StaticMethods.ToMark(CreateBesideWater);

    public string IsShellString => StaticMethods.ToMark(IsShell);

    public string ObliqueOffenceString => StaticMethods.ToMark(ObliqueOffence);
    
    public string ObliqueStratagemString => StaticMethods.ToMark(ObliqueStratagem);

    public string OffenceOnlyBeforeMoveString => StaticMethods.ToMark(OffenceOnlyBeforeMove);
    
    public int InfluenceCount => Influences.Count;
    
    public List<MilitaryKind> GetLevelUpKinds(Architecture arch)
    {
        var militaryKinds = Session.Current.Scenario.GameCommonData.AllMilitaryKinds;

        List<MilitaryKind> result = new List<MilitaryKind>();
        foreach (int id in LevelUpKindID)
        {
            if (!arch.BelongedFaction.IsMilitaryKindOverLimit(id))
            {
                result.Add(militaryKinds.GetMilitaryKind(id));
            }
        }

        return result;
    }
    
    public MilitaryKind MorphTo
    {
        get
        {
            var militaryKinds = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.MilitaryKinds;

            if (!militaryKinds.ContainsKey(MorphToKindId)) return null;

            return militaryKinds[MorphToKindId];
        }
    }

    public bool CheckConditions(Architecture arch)
    {
        return Condition.CheckConditionList(CreateConditions, arch);
    }

    /*
    public int EachMilitaryKindCount(Faction f)
    {
        int count = 0;
       // MilitaryKind mk = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(id);
        if (f != null)
        {
            foreach (Military military in f.Militaries)
            {
                if (military.RealKindID == this.ID )
                {
                    count++;
                }
            }
        }

        return count;
    }
    */
}