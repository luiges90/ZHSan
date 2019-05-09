using GameObjects;
using GameObjects.PersonDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    public class PersonTab : BaseTab<Person>
    {
        protected override String[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Available",
                "Alive",
                "SurName",
                "GivenName",
                "CalledName",
                "Sex",
                "PictureIndex",
                "Ideal",
                "IdealTendencyIDString",
                "LeaderPossibility",
                "PCharacter",
                "YearAvailable",
                "YearBorn",
                "YearDead",
                "YearJoin",
                "DeadReason",
                "BaseStrength",
                "BaseCommand",
                "BaseIntelligence",
                "BasePolitics",
                "BaseGlamour",
                "Tiredness",
                "Reputation",
                "Karma",
                "BaseBraveness",
                "BaseCalmness",
                "SkillsString",
                "RealTitlesString",
                "StudyingTitleString",
                "StuntsString",
                "StudyingStuntString",
                "UniqueTitlesString",
                "UniqueMilitaryKindsString",
                "Generation",
                "Strain",
                "huaiyun",
                "faxianhuaiyun",
                "huaiyuntianshu",
                "shoshurenwu",
                "suoshurenwuList",
                "MarriageGranter",
                "TempLoyaltyChange",
                "BornRegion",
                "AvailableLocation",
                "PersonalLoyalty",
                "Ambition",
                "Qualification",
                "ValuationOnGovernment",
                "StrategyTendency",
                "OldFactionID",
                "ProhibitedFactionID",
                "IsGeneratedChildren",
                "StrengthPotential",
                "CommandPotential",
                "IntelligencePotential",
                "PoliticsPotential",
                "GlamourPotential",
                "TrainPolicy"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Available", "已出场" },
                { "Alive", "是否活着" },
                { "Ambition", "野心" },
                { "ArchitectureDamageDealt", "总建筑伤害" },
                { "AvailableLocation", "出场地点" },
                { "ArrivingDays", "到达天数" },
                { "BaseBraveness", "勇猛度" },
                { "BaseCalmness", "冷静度" },
                { "BaseCommand", "将略" },
                { "BaseGlamour", "风度" },
                { "BaseIntelligence", "谋略" },
                { "BasePolitics", "政理" },
                { "BaseStrength", "武学" },
                { "BattleSelfDamage", "BattleSelfDamage" },
                { "BelongedPersonName", "所属人物名称" },
                { "BornRegion", "出生地区" },
                { "BubingExperience", "步兵经验" },
                { "ConvincingPersonID", "正说服武将ID" },
                { "CalledName", "字" },
                { "CaptiveCount", "俘获将领次数" },
                { "CombatTitleString", "战斗称号" },
                { "CommandExperience", "将略经验" },
                { "CommandPotential", "将略潜质" },
                { "DeadReason", "死亡原因" },
                { "ExecuteCount", "处斩次数" },
                { "faxianhuaiyun", "发现怀孕" },
                { "FleeCount", "逃跑数" },
                { "firstPreferred", "firstPreferred" },
                { "Generation", "世代" },
                { "GivenName", "名字" },
                { "GlamourExperience", "风度经验" },
                { "GlamourPotential", "风度潜质" },
                { "HeldCaptiveCount", "被俘次数" },
                { "huaiyun", "怀孕" },
                { "huaiyuntianshu", "怀孕天数" },
                { "Ideal", "志向" },
                { "IdealTendencyIDString", "出仕志向考虑" },
                { "lmmortal", "不死" },
                { "InformationKindID", "情报种类" },
                { "InjureRate", "负伤" },
                { "InternalExperience", "内政经验" },
                { "IntelligenceExperience", "谋略经验" },
                { "IntelligencePotential", "谋略潜质" },
                { "IsGeneratedChildren", "为生成子女" },
                { "JoinFactionID", "加入势力ID" },
                { "Karma", "善名" },
                { "LastOutsideTask", "上次外出工作类型" },
                { "LeaderPossibility", "新建势力可能性" },
                { "ManualStudy", "ManualStudy" },
                { "MarriageGranter", "赐婚者" },
                { "NubingExperience", "弩兵经验" },
                { "NumberOfChildren", "子女数" },
                { "OfficerMerit", "OfficerMerit" },
                { "OldFactionID", "出仕势力" },
                { "OldWorkKind", "旧工作类型" },
                { "OfficerKillCount", "致武将战死数" },
                { "OutsideDestination", "外出工作地点" },
                { "OutsideTask", "外出工作类型" },
                { "PCharacter", "性格" },
                { "PersonalTitleString", "私有称号" },
                { "PersonalLoyalty", "义理" },
                { "PictureIndex", "头像序号" },
                { "PoliticsExperience", "政理经验" },
                { "PoliticsPotential", "政理潜质" },
                { "preferredTroopPersonsString", "副将" },
                { "princessTakerID", "princessTakerID" },
                { "ProhibitedFactionID", "禁止出仕势力" },
                { "QibingExperience", "骑兵经验" },
                { "QixieExperience", "器械经验" },
                { "Qualification", "人才起用" },
                { "ReturnedDaySince", "ReturnedDaySince" },
                { "RewardFinished", "已褒赏完毕" },
                { "RealTitlesString", "称号" },
                { "Reputation", "名声" },
                { "RebelCount", "反叛次数" },
                { "RoutCount", "击破次数" },
                { "RoutedCount", "被击破次数" },
                { "Sex", "性别" },
                { "ShuijunExperience", "水战经验" },
                { "suoshurenwu", "所属人物" },
                { "SkillsString", "技能" },
                { "Strain", "血缘" },
                { "StratagemExperience", "计略经验" },
                { "StratagemSuccessCount", "计略成功次数" },
                { "StratagemFailCount", "计略失败次数" },
                { "StratagemBeSuccessCount", "中计次数" },
                { "StratagemBeFailCount", "计略阻挡次数" },
                { "StrategyTendency", "战略倾向" },
                { "StrengthExperience", "武学经验" },
                { "StrengthPotential", "武学潜质" },
                { "StudyingStuntString", "学习中特技" },
                { "StudyingTitleString", "获取中称号" },
                { "StuntsString", "特技" },
                { "shoudongsousuo", "手动搜索" },
                { "suoshurenwuList", "所属人物表" },
                { "SurName", "姓氏" },
                { "Tags", "标签" },
                { "TaskDays", "外出时间" },
                { "TacticsExperience", "策略经验" },
                { "TempLoyaltyChange", "忠诚调整" },
                { "TroopBeDamageDealt", "总受士兵伤害" },
                { "TroopDamageDealt", "总士兵伤害" },
                { "Tiredness", "疲累度" },
                { "TrainPolicyIDString", "培育方针" },
                { "UniqueMilitaryKindsString", "独有兵种" },
                { "UniqueTitlesString", "独有称号" },
                { "ValuationOnGovernment", "汉室" },
                { "WaitForFeiZiPeriod", "等待妃子时间" },
                { "waitForFeiziId", "等待妃子ID" },
                { "wasMayor", "是县令" },
                { "WorkKind", "工作类型" },
                { "YearAvailable", "出场年份" },
                { "YearBorn", "出生年份" },
                { "YearDead", "死亡年份" },
                { "YearJoin", "仕官年" },

            };
        }

        protected override Dictionary<String, String> GetDefaultValues()
        {
            return new Dictionary<string, string>
            {
                { "StudyingTitleString", "-1" },
                { "StudyingStuntString", "-1" },
                { "huaiyuntianshu", "-1" },
                { "suoshurenwu", "-1" },
                { "ConvincingPersonID", "-1" },
                { "waitForFeiZiPeriod", "30" },
                { "waitForFeiziId", "-1" },
                { "InjureRate", "1" },
                { "Generation", "1" },
                { "InformationKindID", "-1" }
            };
        }

        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Persons);
        }

        public PersonTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }



    }
}
