using GameObjects.Influences;
using GameObjects.Conditions;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GameManager;
using System.Linq;

namespace GameObjects.ArchitectureDetail
{
    /// <summary>
    /// 设施种类
    /// </summary>
    [DataContract]
    public class FacilityKind : GameObject
    {
        # region DataMember

        /// <summary>
        /// AI强度
        /// </summary>
        [DataMember]
        public float AILevel { get; set; }

        /// <summary>
        /// 占用位置
        /// </summary>
        [DataMember]
        public int PositionOccupied { get; set; }

        /// <summary>
        /// 新建所需技术
        /// </summary>
        [DataMember]
        public int TechnologyNeeded { get; set; }

        /// <summary>
        /// 新建所需技巧
        /// </summary>
        [DataMember]
        public int PointCost { get; set; }

        /// <summary>
        /// 新建所需资金
        /// </summary>
        [DataMember]
        public int FundCost { get; set; }

        /// <summary>
        /// 维持费用
        /// </summary>
        [DataMember]
        public int MaintenanceCost { get; set; }

        /// <summary>
        /// 建造所需时间
        /// </summary>
        [DataMember]
        public int Days { get; set; }

        /// <summary>
        /// 耐久度
        /// </summary>
        [DataMember]
        public int Endurance { get; set; }

        /// <summary>
        /// 建筑上限
        /// </summary>
        [DataMember]
        public int ArchitectureLimit { get; set; }

        /// <summary>
        /// 势力上限
        /// </summary>
        [DataMember]
        public int FactionLimit { get; set; }

        /// <summary>
        /// 人口相关
        /// </summary>
        [DataMember]
        public bool PopulationRelated { get; set; }

        /// <summary>
        /// 影响
        /// </summary>
        [DataMember]
        public string InfluencesString { get; set;}

        /// <summary>
        /// 兴建条件
        /// </summary>
        [DataMember]
        public string ConditionTableString { get; set; }

        /// <summary>
        /// 可容纳妃子数
        /// </summary>
        [DataMember]
        public int rongna { get; set; }

        /// <summary>
        /// 不可拆除
        /// </summary>
        [DataMember]
        public bool bukechaichu { get; set; }

        /// <summary>
        /// AI兴建条件权重
        /// </summary>
        [DataMember]
        public string AIBuildConditionWeightString { get; set; }
        
        # endregion

        public InfluenceTable Influences { get; set; } = new();

        public ConditionTable Conditions { get; set; } = new();

        public Dictionary<Condition, float> AIBuildConditionWeight { get; set; } = new();

        /// <summary>
        /// 获取AI值
        /// </summary>
        /// <param name="architecture"></param>
        /// <returns></returns>
        public double AIValue(Architecture architecture)
        {
            // TODO:影响为空时返回一个很小的负数，是否调整为0
            var value = Influences.Values.Any() ? Influences.Values.Max(x => x.AIFacilityValue(architecture)) : double.MinValue;

            if (value >= 0)
            {
                value = (value - (double)MaintenanceCost / architecture.ExpectedFund * 30.0) * AILevel / PositionOccupied;
            }

            return value;
        }

        public int DaysText => Days * Session.Parameters.DayInTurn;

        public string Description
        {
            get
            {
                var str = rongna > 0 ? $"•可以容纳{rongna}名美女" : "";

                str += string.Join("•", Influences.Values.Select(x => x.Description));

                return str;
            }
        }

        public string PopulationRelatedString => PopulationRelated ? "○" : "×";

        /// <summary>
        /// 是否盈利
        /// </summary>
        public bool IsProfitable
        {
            get
            {
                int fundIncrease = 0;
                foreach (var influence in Influences.Values)
                {
                    var influenceKindId = influence.Kind.ID;

                    if (influenceKindId == 3000 && int.TryParse(influence.Parameter, out var value))
                    {
                        fundIncrease += value;
                    }
                    else if (influenceKindId == 3020 || influenceKindId == 3210)
                    {
                        // 资金加成 & 商业值增长默认盈利
                        return true;
                    }
                }

                // 资金增长扣除成本
                var isProfitable = fundIncrease - MaintenanceCost * 30 > 0;

                return isProfitable;
            }
        }

        /// <summary>
        /// 是否可增筑
        /// </summary>
        public bool IsExtension
        {
            get
            {
                // TODO: 用占用位置和影响种类判断增筑，是否用增筑Id直接判断

                // 增筑 & 治所占用位置为0
                if (PositionOccupied > 0) return false;

                var influenceKindIds = Influences.Values.Select(x => x.Kind.ID);

                // 农业、商业、技术、耐久、设施空间、人口上限
                var targetKindIds = new [] { 1000, 1001, 1002, 1003, 1020, 1050 };

                var isExtension = influenceKindIds.Any(x => targetKindIds.Contains(x)) ? true : false;

                return isExtension;
            }
        }

        /// <summary>
        /// 可建造
        /// </summary>
        /// <param name="architecture">建筑</param>
        /// <returns></returns>
        public bool CanBuild(Architecture architecture)
        {
            // 9999表示无上限
            var noLimit = 9999;

            // 建筑总空间不足
            if (PositionOccupied > 0 && architecture.FacilityPositionCount == 0)
                return false;

            // 设施人口相关不匹配
            if (PopulationRelated && !architecture.Kind.HasPopulation)
                return false;

            // 建筑技术不足
            if (TechnologyNeeded > architecture.Technology)
                return false;

            if (!Condition.CheckConditionList(Conditions.Values, architecture)) 
                return false;

            // 已达建筑上限
            if (ArchitectureLimit < noLimit && ArchitectureLimit <= architecture.GetFacilityKindCount(ID))
                return false;


            var faction = architecture.BelongedFaction;

            // 势力技巧不足
            var factionPoint = faction != null ? faction.TechniquePoint + faction.TechniquePointForFacility + faction.TechniquePointForTechnique : 0;
            if (PointCost > factionPoint)
                return false;

            // 已达势力上限
            var factionLimit = faction?.GetFacilityKindCount(ID) ?? 0;
            if (FactionLimit < noLimit && FactionLimit <= factionLimit)
                return false;

            return true;
        }
    }
}