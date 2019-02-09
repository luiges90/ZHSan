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
                "LastName",
                "FirstName",
                "CalledName",
                "Sex",
                "AvatarIndex",
                "Ideal",
                "IdealTendencyIDString",
                "LeaderPossibility",
                "PCharacter",
                "YearAvailable",
                "YearBorn",
                "YearDead",
                "YearJoin",
                "DeathReason",
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
                "LearningTitleString",
                "StuntsString",
                "LearningStuntString",
                "UniqueTitlesString",
                "UniqueTroopTypesString",
                "Generation",
                "Strain",
                "IsPregnant",
                "PregnancyDiscovered",
                "PregnancyDayCount",
                "DefinedPartner",
                "PartnersList",
                "MarriageGranter",
                "TempLoyaltyChange",
                "BirthRegion",
                "AvailableLocation",
                "PersonalLoyalty",
                "Ambition",
                "Qualification",
                "ValuationOnGovernment",
                "StrategyTendency",
                "OldFactionID",
                "ProhibitedFactionID",
                "IsGeneratedChild",
                "StrengthPotential",
                "CommandPotential",
                "IntelligencePotential",
                "PoliticsPotential",
                "GlamourPotential",
                "EducationPolicy"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Available", "登场。勾选后，角色在剧本起始时登场。" },
                { "Alive", "活著。勾选后，角色在剧本起始时存活。" },
                { "LastName", "姓氏" },
                { "FirstName", "名称" },
                { "CalledName", "表字" },
                { "Sex", "性别。勾选后，角色在剧本中的性别为女性。" },
                { "AvatarIndex", "头像编号" },
                { "Ideal", "相性" },
                { "IdealTendencyIDString", "出仕相性考虑" },
                { "LeaderPossibility", "独立倾向。勾选后，角色在剧本中有机会独立。" },
                { "PCharacter", "性格" },
                { "YearAvailable", "登场年份" },
                { "YearBorn", "出生年份" },
                { "YearDead", "死亡年份" },
                { "YearJoin", "出仕年份" },
                { "DeathReason", "死亡原因" },
                { "BaseStrength", "武勇" },
                { "BaseCommand", "统率" },
                { "BaseIntelligence", "智谋" },
                { "BasePolitics", "政治" },
                { "BaseGlamour", "魅力" },
                { "Tiredness", "疲劳度" },
                { "Reputation", "名声" },
                { "Karma", "善名" },
                { "BaseBraveness", "勇猛" },
                { "BaseCalmness", "冷静" },
                { "SkillsString", "技能" },
                { "RealTitlesString", "称号" },
                { "LearningTitleString", "研修中称号" },
                { "StuntsString", "特技" },
                { "LearningStuntString", "研修中特技" },
                { "UniqueTitlesString", "专属称号" },
                { "UniqueTroopTypesString", "专属兵种" },
                { "Generation", "世代" },
                { "Strain", "血缘" },
                { "IsPregnant", "怀孕" },
                { "PregnancyDiscovered", "发现怀孕" },
                { "PregnancyDayCount", "怀孕天数" },
                { "DefinedPartner", "目前伴侣" },
                { "PartnersList", "伴侣列表" },
                { "MarriageGranter", "赐婚者" },
                { "TempLoyaltyChange", "忠诚调整" },
                { "BirthRegion", "出生地区" },
                { "AvailableLocation", "登场地点" },
                { "PersonalLoyalty", "义理" },
                { "Ambition", "野心" },
                { "Qualification", "仕官倾向" },
                { "ValuationOnGovernment", "汉室拥护度" },
                { "StrategyTendency", "战略倾向" },
                { "OldFactionID", "所属势力" },
                { "ProhibitedFactionID", "禁止出仕势力" },
                { "IsGeneratedChild", "为生成子女" },
                { "StrengthPotential", "武勇潜质" },
                { "CommandPotential", "统率潜质" },
                { "IntelligencePotential", "智谋潜质" },
                { "PoliticsPotential", "政治潜质" },
                { "GlamourPotential", "魅力潜质" },
                { "EducationPolicy", "培育方针" },
            };
        }

        protected override Dictionary<String, String> GetDefaultValues()
        {
            return new Dictionary<string, string>
            {
                { "LearningTitleString", "-1" },
                { "LearningStuntString", "-1" },
                { "PregnancyDayCount", "-1" },
                { "DefinedPartner", "-1" },
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
