using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class FactionTab : BaseTab<Faction>
    {
		public string Lang_FactionID_CHS = "编号";
		public string Lang_FactionID_Desc_CHS = "势力的编号。";
		public string Lang_FactionName_CHS = "名称";
		public string Lang_FactionName_Desc_CHS = "势力的名称。";
		public string Lang_FactionLeaderID_CHS = "君主";
		public string Lang_FactionLeaderID_Desc_CHS = "势力君主的人物编号。";
		public string Lang_FactionPrinceID_CHS = "储君";
		public string Lang_FactionPrinceID_Desc_CHS = "势力储君的人物编号。";
		public string Lang_FactionColorIndex_CHS = "颜色";
		public string Lang_FactionColorIndex_Desc_CHS = "势力的颜色编号。";
		public string Lang_FactionCapitalID_CHS = "首都";
		public string Lang_FactionCapitalID_Desc_CHS = "设定为势力首都的城池编号。";
		public string Lang_FactionReputation_CHS = "声望";
		public string Lang_FactionReputation_Desc_CHS = "势力的声望值。";
		public string Lang_FactionCitiesControlledString_CHS = "城池列表";
		public string Lang_FactionCitiesControlledString_Desc_CHS = "势力所控制的各城池编号。";
		public string Lang_FactionInitialTroopTypesAllowedString_CHS = "初始兵种";
		public string Lang_FactionInitialTroopTypesAllowedString_Desc_CHS = "势力于剧本开始时，能征召的部队类型。";
		public string Lang_FactionTechniquePoint_CHS = "技巧点数";
		public string Lang_FactionTechniquePoint_Desc_CHS = "势力目前拥有的技巧点数。";
		public string Lang_FactionTechniquePointForTechnique_CHS = "升级保留";
		public string Lang_FactionTechniquePointForTechnique_Desc_CHS = "为升级技巧，保留的技巧点数。";
		public string Lang_FactionTechniquePointForFacility_CHS = "建造保留";
		public string Lang_FactionTechniquePointForFacility_Desc_CHS = "为建造设施，保留的技巧点数。";
		public string Lang_FactionMilitaryDistrictsString_CHS = "军区列表";
		public string Lang_FactionMilitaryDistrictsString_Desc_CHS = "势力军区的编号列表。";
		public string Lang_FactionIntelsAcquiredString_CHS = "情报列表";
		public string Lang_FactionIntelsAcquiredString_Desc_CHS = "势力情报的编号列表。";
		public string Lang_FactionTroopsString_CHS = "部队列表";
		public string Lang_FactionTroopsString_Desc_CHS = "势力部队的编号列表。";
		public string Lang_FactionSupplyRoutesString_CHS = "粮道列表";
		public string Lang_FactionSupplyRoutesString_Desc_CHS = "势力粮道的编号列表。";
		public string Lang_FactionContributionToEmperor_CHS = "朝廷贡献度";
		public string Lang_FactionContributionToEmperor_Desc_CHS = "势力的朝廷贡献度。";
		public string Lang_FactionNobleRankID_CHS = "官爵";
		public string Lang_FactionNobleRankID_Desc_CHS = "势力的官爵编号。";
		public string Lang_FactionUpgradingTechnique_CHS = "升级中技巧";
		public string Lang_FactionUpgradingTechnique_Desc_CHS = "指定势力目前升级中的技巧编号。";
		public string Lang_FactionUpgradingDaysLeft_CHS = "剩余天数";
		public string Lang_FactionUpgradingDaysLeft_Desc_CHS = "设定完成升级所剩余的天数。";
		public string Lang_FactionAvailableTechniquesString_CHS = "已有技巧";
		public string Lang_FactionAvailableTechniquesString_Desc_CHS = "设定势力已拥有的技巧编号。";
		public string Lang_FactionPreferredTechniqueKinds_CHS = "偏好技巧";
		public string Lang_FactionPreferredTechniqueKinds_Desc_CHS = "设定势力偏好的技巧类别编号。";
		public string Lang_FactionIsAlien_CHS = "异族";
		public string Lang_FactionIsAlien_Desc_CHS = "勾选后，势力会被认定为异族。";
		public string Lang_FactionNotPlayerSelectable_CHS = "玩家禁选";
		public string Lang_FactionNotPlayerSelectable_Desc_CHS = "勾选后，被设定为玩家禁止选择的势力。";
		
        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"CapitalID", "-1"},
                {"PlanTechnique", "-1"},
                {"PrinceID", "-1"},
                {"InitialTroopTypesAllowedString", "0 1 3"},
                {"UpgradingTechnique", "-1" },
                {"AvailableTechniquesString", ""}
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new string[]
            {
                "ID",
                "Name",
                "LeaderID",
                "PrinceID",
                "ColorIndex",
                "FName",
                "CapitalID",
                "Reputation",
                "CitiesControlledString",
                "InitialTroopTypesAllowedString",
                "TechniquePoint",
                "TechniquePointForTechnique",
                "TechniquePointForFacility",
                "MilitaryDistrictsString",
                "IntelsAcquiredString",
                "TroopsString",
				"MilitariesString",
				"MilitaryCount",
				"TransferingMilitariesString",
				"TransferingMilitaryCount",
                "SupplyRoutesString",
				"ZhaoxianFailureCount",
                "ContributionToEmperor",
                "NobleRankID",
                "UpgradingTechnique",
                "UpgradingDaysLeft",
                "AvailableTechniquesString",
				"PlanTechniqueString",
                "PreferredTechniqueKinds",
                "IsAlien",
                "NotPlayerSelectable"
            };
        }

         protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
				{ Lang_FactionID_CHS, Lang_FactionID_Desc_CHS },
				{ Lang_FactionName_CHS, Lang_FactionName_Desc_CHS },
				{ Lang_FactionLeaderID_CHS, Lang_FactionLeaderID_Desc_CHS },
				{ Lang_FactionPrinceID_CHS, Lang_FactionPrinceID_Desc_CHS },
				{ Lang_FactionColorIndex_CHS, Lang_FactionColorIndex_Desc_CHS },
				{ Lang_FactionCapitalID_CHS, Lang_FactionCapitalID_Desc_CHS },
				{ Lang_FactionReputation_CHS, Lang_FactionReputation_Desc_CHS },
				{ Lang_FactionCitiesControlledString_CHS, Lang_FactionCitiesControlledString_Desc_CHS },
				{ Lang_FactionInitialTroopTypesAllowedString_CHS, Lang_FactionInitialTroopTypesAllowedString_Desc_CHS },
				{ Lang_FactionTechniquePoint_CHS, Lang_FactionTechniquePoint_Desc_CHS },
				{ Lang_FactionTechniquePointForTechnique_CHS, Lang_FactionTechniquePointForTechnique_Desc_CHS },
				{ Lang_FactionTechniquePointForFacility_CHS, Lang_FactionTechniquePointForFacility_Desc_CHS },
				{ Lang_FactionMilitaryDistrictsString_CHS, Lang_FactionMilitaryDistrictsString_Desc_CHS },
				{ Lang_FactionIntelsAcquiredString_CHS, Lang_FactionIntelsAcquiredString_Desc_CHS },
				{ Lang_FactionTroopsString_CHS, Lang_FactionTroopsString_Desc_CHS },
				{ Lang_FactionSupplyRoutesString_CHS, Lang_FactionSupplyRoutesString_Desc_CHS },
				{ Lang_FactionContributionToEmperor_CHS, Lang_FactionContributionToEmperor_Desc_CHS },
				{ Lang_FactionNobleRankID_CHS, Lang_FactionNobleRankID_Desc_CHS },
				{ Lang_FactionUpgradingTechnique_CHS, Lang_FactionUpgradingTechnique_Desc_CHS },
				{ Lang_FactionUpgradingDaysLeft_CHS, Lang_FactionUpgradingDaysLeft_Desc_CHS },
				{ Lang_FactionAvailableTechniquesString_CHS, Lang_FactionAvailableTechniquesString_Desc_CHS },
				{ Lang_FactionPreferredTechniqueKinds_CHS, Lang_FactionPreferredTechniqueKinds_Desc_CHS },
				{ Lang_FactionIsAlien_CHS, Lang_FactionIsAlien_Desc_CHS },
				{ Lang_FactionNotPlayerSelectable_CHS, Lang_FactionNotPlayerSelectable_Desc_CHS }
            };
        }

        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Factions);
        }

        public FactionTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
