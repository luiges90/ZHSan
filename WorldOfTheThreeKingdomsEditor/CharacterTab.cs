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
    public class CharacterTab : BaseTab<Person>
    {
		public string CharID_CHS = "编号";
		public string CharIDDesc_CHS = "角色编号";
		public string CharAvailable_CHS = "登场";
		public string CharAvailableDesc_CHS = "勾选后，角色会在剧本起始时登场。";
		public string CharAlive_CHS = "活著";
		public string CharAliveDesc_CHS = "勾选后，角色会在剧本起始时存活。";
		public string CharLastName_CHS = "姓氏";
		public string CharLastNameDesc_CHS = "姓氏";
		public string CharFirstName_CHS = "名称";
		public string CharFirstNameDesc_CHS = "名称";
		public string CharCalledName_CHS = "表字";
		public string CharCalledNameDesc_CHS = "表字";
		public string CharSex_CHS = "性别";
		public string CharSexDesc_CHS = "勾选后，角色在剧本中的性别认定为女性。";
		public string CharAvatarIndex_CHS = "头像";
		public string CharAvatarIndexDesc_CHS = "头像编号";
		public string CharIdeal_CHS = "相性";
		public string CharIdealDesc_CHS = "相性";
		public string CharIdealTendencyIDString_CHS = "出仕相性考虑";
		public string CharIdealTendencyIDStringDesc_CHS = "出仕相性考虑";
		public string CharLeaderPossibility_CHS = "自立倾向";
		public string CharLeaderPossibilityDesc_CHS = "勾选后，角色在剧本中有机会自建势力。";
		public string CharPCharacter_CHS = "性格";
		public string CharPCharacterDesc_CHS = "性格";
		public string CharYearAvailable_CHS = "登场年份";
		public string CharYearAvailableDesc_CHS = "登场年份";
		public string CharYearBorn_CHS = "出生年份";
		public string CharYearBornDesc_CHS = "出生年份";
		public string CharYearDead_CHS = "死亡年份";
		public string CharYearDeadDesc_CHS = "死亡年份";
		public string CharYearJoin_CHS = "出仕年份";
		public string CharYearJoinDesc_CHS = "出仕年份";
		public string CharDeathReason_CHS = "死亡原因";
		public string CharDeathReasonDesc_CHS = "0为自然死亡，1为被杀死亡，2为抑郁而终，3为操劳致死。";
		public string CharBaseStrength_CHS = "武勇";
		public string CharBaseStrengthDesc_CHS = "武勇初始值。";
		public string CharBaseCommand_CHS = "统率";
		public string CharBaseCommandDesc_CHS = "统率初始值。";
		public string CharBaseIntelligence_CHS = "智谋";
		public string CharBaseIntelligenceDesc_CHS = "智谋初始值。";
		public string CharBasePolitics_CHS = "政治";
		public string CharBasePoliticsDesc_CHS = "政治初始值。";
		public string CharBaseGlamour_CHS = "魅力";
		public string CharBaseGlamourDesc_CHS = "魅力初始值。";
		public string CharTiredness_CHS = "疲劳度";
		public string CharTirednessDesc_CHS = "疲劳度";
		public string CharReputation_CHS = "名声";
		public string CharReputationDesc_CHS = "名声初始值。";
		public string CharKarma_CHS = "善名";
		public string CharKarmaDesc_CHS = "善名";
		public string CharBaseBraveness_CHS = "勇猛";
		public string CharBaseBravenessDesc_CHS = "勇猛";
		public string CharBaseCalmness_CHS = "冷静";
		public string CharBaseCalmnessDesc_CHS = "冷静";
		public string CharSkillsString_CHS = "技能";
		public string CharSkillsStringDesc_CHS = "角色拥有的技能，以空格分隔各项技能。";
		public string CharRealTitlesString_CHS = "称号";
		public string CharRealTitlesStringDesc_CHS = "角色拥有的称号，以空格分隔各项称号。";
		public string CharLearningTitleString_CHS = "研修中称号";
		public string CharLearningTitleStringDesc_CHS = "角色正在研修中的称号。";
		public string CharStuntsString_CHS = "特技";
		public string CharStuntsStringDesc_CHS = "角色拥有的特技，以空格分隔各项特技。";
		public string CharLearningStuntString_CHS = "研修中特技";
		public string CharLearningStuntStringDesc_CHS = "角色正在研修中的特技。";
		public string CharUniqueTitlesString_CHS = "专属称号";
		public string CharUniqueTitlesStringDesc_CHS = "角色拥有的专属称号，以空格分隔各项专属称号。";
		public string CharUniqueTroopTypesString_CHS = "专属兵种";
		public string CharUniqueTroopTypesStringDesc_CHS = "角色拥有的专属兵种，以空格分隔各类专属兵种";
		public string CharGeneration_CHS = "世代";
		public string CharGenerationDesc_CHS = "设定剧本中角色的亲属层级，1为2的父辈‵，以此类推。";
		public string CharStrain_CHS = "血缘";
		public string CharStrainDesc_CHS = "设定为血缘关系中，具备核心地位的角色编号。";
		public string CharIsPregnant_CHS = "怀孕";
		public string CharIsPregnantDesc_CHS = "怀孕";
		public string CharPregnancyDiscovered_CHS = "发现怀孕";
		public string CharPregnancyDiscoveredDesc_CHS = "发现怀孕";
		public string CharPregnancyDayCount_CHS = "怀孕天数";
		public string CharPregnancyDayCountDesc_CHS = "怀孕天数";
		public string CharDefinedPartner_CHS = "目前伴侣";
		public string CharDefinedPartnerDesc_CHS = "目前伴侣";
		public string CharDefinedPartnersList_CHS = "伴侣列表";
		public string CharDefinedPartnersListDesc_CHS = "伴侣列表";
		public string CharMarriageGranter_CHS = "赐婚者";
		public string CharMarriageGranterDesc_CHS = "赐婚者";
		public string CharTempLoyaltyChange_CHS = "忠诚调整";
		public string CharTempLoyaltyChangeDesc_CHS = "忠诚调整";
		public string CharBirthRegion_CHS = "出生地区";
		public string CharBirthRegionDesc_CHS = "0为幽州，1为冀州，2为青徐，3为兖豫，4为司隶，5为京兆，6为凉州，7为扬州，8为荆北，9为荆南，10为益州，11为南中，12为交州，13为夷州。";
		public string CharAvailableLocation_CHS = "登场地点";
		public string CharAvailableLocationDesc_CHS = "登场地点";
		public string CharPersonalLoyalty_CHS = "义理";
		public string CharPersonalLoyaltyDesc_CHS = "0为很低，1为较低，2为普通，3为较高，4为很高。";
		public string CharAmbition_CHS = "野心";
		public string CharAmbitionDesc_CHS = "0为很低，1为较低，2为普通，3为较高，4为很高。";
		public string CharQualification_CHS = "仕官倾向";
		public string CharQualificationDesc_CHS = "仕官倾向";
		public string CharValuationOnGovernment_CHS = "汉室拥护度";
		public string CharValuationOnGovernmentDesc_CHS = "0为无视，1为普通，2为重视。";
		public string CharStrategyTendency_CHS = "战略倾向";
		public string CharStrategyTendencyDesc_CHS = "0为统一全国，1为统一地区，2为统一州郡，3为维持现状。";
		/* Possible not in use
		public string CharOldFactionID_CHS = "所属势力";
		public string CharOldFactionIDDesc_CHS = "所属势力";
		*/
		public string CharProhibitedFactionID_CHS = "禁止出仕势力";
		public string CharProhibitedFactionIDDesc_CHS = "禁止出仕势力";
		public string CharIsGeneratedChild_CHS = "为生成子女";
		public string CharIsGeneratedChildDesc_CHS = "勾选后，角色认定为系统生成子女";
		public string CharStrengthPotential_CHS = "武勇潜质";
		public string CharStrengthPotentialDesc_CHS = "武勇潜质";
		public string CharCommandPotential_CHS = "统率潜质";
		public string CharCommandPotentialDesc_CHS = "统率潜质";
		public string CharIntelligencePotential_CHS = "智谋潜质";
		public string CharIntelligencePotentialDesc_CHS = "智谋潜质";
		public string CharPoliticsPotential_CHS = "政治潜质";
		public string CharPoliticsPotentialDesc_CHS = "政治潜质";
		public string CharGlamourPotential_CHS = "魅力潜质";
		public string CharGlamourPotentialDesc_CHS = "魅力潜质";
		public string CharEducationPolicy_CHS = "培育方针";
		public string CharEducationPolicyDesc_CHS = "培育方针";
		
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
                "DefinedPartnersList",
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
                { CharID_CHS, CharIDDesc_CHS },
                { CharAvailable_CHS, CharAvailableDesc_CHS },
                { CharAlive_CHS, CharAliveDesc_CHS },
                { CharLastName_CHS, CharLastNameDesc_CHS },
                { CharFirstName_CHS, CharFirstNameDesc_CHS },
                { CharCalledName_CHS, CharCalledNameDesc_CHS },
                { CharSex_CHS, CharSexDesc_CHS },
                { CharAvatarIndex_CHS, CharAvatarIndexDesc_CHS },
                { CharIdeal_CHS, CharIdealDesc_CHS },
                { CharIdealTendencyIDString_CHS, CharIdealTendencyIDStringDesc_CHS },
                { CharLeaderPossibility_CHS, CharLeaderPossibilityDesc_CHS },
                { CharPCharacter_CHS, CharPCharacterDesc_CHS },
                { CharYearAvailable_CHS, CharYearAvailableDesc_CHS },
                { CharYearBorn_CHS, CharYearBornDesc_CHS },
                { CharYearDead_CHS, CharYearDeadDesc_CHS },
                { CharYearJoin_CHS, CharYearJoinDesc_CHS },
                { CharDeathReason_CHS, CharDeathReasonDesc_CHS },
                { CharBaseStrength_CHS, CharBaseStrengthDesc_CHS },
                { CharBaseCommand_CHS, CharBaseCommandDesc_CHS },
                { CharBaseIntelligence_CHS, CharBaseIntelligenceDesc_CHS },
                { CharBasePolitics_CHS, CharBasePoliticsDesc_CHS },
                { CharBaseGlamour_CHS, CharBaseGlamourDesc_CHS },
                { CharTiredness_CHS, CharTirednessDesc_CHS },
                { CharReputation_CHS, CharReputationDesc_CHS },
                { CharKarma_CHS, CharKarmaDesc_CHS },
                { CharBaseBraveness_CHS, CharBaseBravenessDesc_CHS },
                { CharBaseCalmness_CHS, CharBaseCalmnessDesc_CHS },
                { CharSkillsString_CHS, CharSkillsStringDesc_CHS },
                { CharRealTitlesString_CHS, CharRealTitlesStringDesc_CHS },
                { CharLearningTitleString_CHS, CharLearningTitleStringDesc_CHS },
                { CharStuntsString_CHS, CharStuntsStringDesc_CHS },
                { CharLearningStuntString_CHS, CharLearningStuntStringDesc_CHS },
                { CharUniqueTitlesString_CHS, CharUniqueTitlesStringDesc_CHS },
                { CharUniqueTroopTypesString_CHS, CharUniqueTroopTypesStringDesc_CHS },
                { CharGeneration_CHS, CharGenerationDesc_CHS },
                { CharStrain_CHS, CharStrainDesc_CHS },
                { CharIsPregnant_CHS, CharIsPregnantDesc_CHS },
                { CharPregnancyDiscovered_CHS, CharPregnancyDiscoveredDesc_CHS },
                { CharPregnancyDayCount_CHS, CharPregnancyDayCountDesc_CHS },
                { CharDefinedPartner_CHS, CharDefinedPartnerDesc_CHS },
                { CharDefinedPartnersList_CHS, CharDefinedPartnersListDesc_CHS },
                { CharMarriageGranter_CHS, CharMarriageGranterDesc_CHS },
                { CharTempLoyaltyChange_CHS, CharTempLoyaltyChangeDesc_CHS },
                { CharBirthRegion_CHS, CharBirthRegionDesc_CHS },
                { CharAvailableLocation_CHS, CharAvailableLocationDesc_CHS },
                { CharPersonalLoyalty_CHS, CharPersonalLoyaltyDesc_CHS },
                { CharAmbition_CHS, CharAmbitionDesc_CHS },
                { CharQualification_CHS, CharQualificationDesc_CHS },
                { CharValuationOnGovernment_CHS, CharValuationOnGovernmentDesc_CHS },
                { CharStrategyTendency_CHS, CharStrategyTendencyDesc_CHS },
				/* Possible not in use
				{ CharOldFactionID_CHS, CharOldFactionIDDesc_CHS },
				*/
                { CharProhibitedFactionID_CHS, CharProhibitedFactionIDDesc_CHS },
                { CharIsGeneratedChild_CHS, CharIsGeneratedChildDesc_CHS },
                { CharStrengthPotential_CHS, CharStrengthPotentialDesc_CHS },
                { CharCommandPotential_CHS, CharCommandPotentialDesc_CHS },
                { CharIntelligencePotential_CHS, CharIntelligencePotentialDesc_CHS },
                { CharPoliticsPotential_CHS, CharPoliticsPotentialDesc_CHS },
                { CharGlamourPotential_CHS, CharGlamourPotentialDesc_CHS },
                { CharEducationPolicy_CHS, CharEducationPolicyDesc_CHS },
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

        public CharacterTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }



    }
}
