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
                { "SurName", "姓氏" },
                { "GivenName", "名字" },
                { "CalledName", "字" },
                { "Sex", "性别" },
                { "PictureIndex", "头像序号" },
                { "Ideal", "志向" },
                { "IdealTendencyIDString", "出仕志向考虑" },
                { "LeaderPossibility", "新建势力可能性" },
                { "PCharacter", "性格" },
                { "YearAvailable", "出场年份" },
                { "YearBorn", "出生年份" },
                { "YearDead", "死亡年份" },
                { "YearJoin", "仕官年" },
                { "DeadReason", "死亡原因" },
                { "BaseStrength", "武学" },
                { "BaseCommand", "将略" },
                { "BaseIntelligence", "谋略" },
                { "BasePolitics", "政理" },
                { "BaseGlamour", "风度" },
                { "Tiredness", "疲累度" },
                { "Reputation", "名声" },
                { "Karma", "善名" },
                { "BaseBraveness", "勇猛度" },
                { "BaseCalmness", "冷静度" },
                { "SkillsString", "技能" },
                { "RealTitlesString", "称号" },
                { "StudyingTitleString", "获取中称号" },
                { "StuntsString", "特技" },
                { "StudyingStuntString", "学习中特技" },
                { "UniqueTitlesString", "独有称号" },
                { "UniqueMilitaryKindsString", "独有兵种" },
                { "Generation", "世代" },
                { "Strain", "血缘" },
                { "huaiyun", "怀孕" },
                { "faxianhuaiyun", "发现怀孕" },
                { "huaiyuntianshu", "怀孕天数" },
                { "shoshurenwu", "所属人物" },
                { "suoshurenwuList", "所属人物表" },
                { "MarriageGranter", "赐婚者" },
                { "TempLoyaltyChange", "忠诚调整" },
                { "BornRegion", "出生地区" },
                { "AvailableLocation", "出场地点" },
                { "PersonalLoyalty", "义理" },
                { "Ambition", "野心" },
                { "Qualification", "人才起用" },
                { "ValuationOnGovernment", "汉室" },
                { "StrategyTendency", "战略倾向" },
                { "OldFactionID", "出仕势力" },
                { "ProhibitedFactionID", "禁止出仕势力" },
                { "IsGeneratedChildren", "为生成子女" },
                { "CommandPotential", "将略潜质" },
                { "StrengthPotential", "武学潜质" },
                { "IntelligencePotential", "谋略潜质" },
                { "PoliticsPotential", "政理潜质" },
                { "GlamourPotential", "风度潜质" },
                { "TrainPolicy", "培育方针" },
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
