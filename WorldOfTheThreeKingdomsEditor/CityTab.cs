using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class CityTab : BaseTab<Architecture>
    {
		public string Lang_CityID_CHS = "编号";
		public string Lang_CityID_CHS_Desc_CHS = "城池的编号。";
		public string Lang_CityName_CHS = "名称";
		public string Lang_CityName_CHS_Desc_CHS = "城池的名称。";
		public string Lang_CityKindId_CHS = "类型";
		public string Lang_CityKindId_CHS_Desc_CHS = "城池的类型编号。请参照城池类型列表里的编号修改。";
		public string Lang_CityCaptionID_CHS = "名称图片";
		public string Lang_CityCaptionID_CHS_Desc_CHS = "城池的名称图片编号。";
		public string Lang_CityIsStrategicCenter_CHS = "战略中心";
		public string Lang_CityIsStrategicCenter_CHS_Desc_CHS = "勾选后，认定该城池为战略中心。";
		public string Lang_CityStateID_CHS = "所属州";
		public string Lang_CityStateID_CHS_Desc_CHS = "设定该城池所属州的编号。";
		public string Lang_CityArea_CHS = "地方";
		public string Lang_CityArea_CHS_Desc_CHS = "设定该城池所属地方的编号。";
		public string Lang_CityMayorID_CHS = "县令";
		public string Lang_CityMayorID_CHS_Desc_CHS = "设定成为县令的人物编号。";
		public string Lang_CityCharacteristicsString_CHS = "特色";
		public string Lang_CityCharacteristicsString_CHS_Desc_CHS = "设定该城池拥有的特色编号。请参照效果列表里的编号修改";
		public string Lang_CityPersonsString_CHS = "人物";
		public string Lang_CityPersonsString_CHS_Desc_CHS = "设定归属该城池的人物编号。";
		public string Lang_CityCharactersTravellingString_CHS = "移动中人物";
		public string Lang_CityCharactersTravellingString_CHS_Desc_CHS = "设定处于移动状态的人物编号。";
		public string Lang_CityUnemployedString_CHS = "在野人物";
		public string Lang_CityUnemployedString_CHS_Desc_CHS = "设定身处该城池的在野人物编号。";
		public string Lang_CityUnemployedTravellingString_CHS = "移动中在野";
		public string Lang_CityUnemployedTravellingString_CHS_Desc_CHS = "设定处于移动状态的在野人物编号。";
		public string Lang_CityConsortListString_CHS = "妃子列表";
		public string Lang_CityConsortListString_CHS_Desc_CHS = "指定归属于该城池的妃子人物编号。";
		public string Lang_CityCaptivesString_CHS = "俘虏列表";
		public string Lang_CityCaptivesString_CHS_Desc_CHS = "指定归属于该城池的俘虏人物编号。";
		public string Lang_CityPopulation_CHS = "人口";
		public string Lang_CityPopulation_CHS_Desc_CHS = "城池人口数量。";
		public string Lang_CityServicePopulation_CHS = "兵役人口";
		public string Lang_CityServicePopulation_CHS_Desc_CHS = "城池里可征召为士兵的人口数量。";
		public string Lang_CityFund_CHS = "资金";
		public string Lang_CityFund_CHS_Desc_CHS = "城池拥有的资金数量。";
		public string Lang_CityFood_CHS = "粮草";
		public string Lang_CityFood_CHS_Desc_CHS = "城池拥有的粮草。";
		public string Lang_CityAgriculture_CHS = "名称";
		public string Lang_CityAgriculture_CHS_Desc_CHS = "称号类型的名称。";
		public string Lang_CityCommerce_CHS = "战斗用";
		public string Lang_CityCommerce_CHS_Desc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string Lang_CityTechnology_CHS = "随机教授";
		public string Lang_CityTechnology_CHS_Desc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string Lang_CityDomination_CHS = "可回收";
		public string Lang_CityDomination_CHS_Desc_CHS = "勾选后，称号可被回收。";
		public string Lang_CityMorale_CHS = "研修时间";
		public string Lang_CityMorale_CHS_Desc_CHS = "设定修得称号所需要的时间。";
		public string Lang_CityEndurance_CHS = "成功率";
		public string Lang_CityEndurance_CHS_Desc_CHS = "设定修得称号的机率。";
		public string Lang_CityFormationsString_CHS = "名称";
		public string Lang_CityFormationsString_CHS_Desc_CHS = "称号类型的名称。";
		public string Lang_CityFacilitiesString_CHS = "战斗用";
		public string Lang_CityFacilitiesString_CHS_Desc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string Lang_CityBuildingFacility_CHS = "随机教授";
		public string Lang_CityBuildingFacility_CHS_Desc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string Lang_CityBuildingDaysLeft_CHS = "可回收";
		public string Lang_CityBuildingDaysLeft_CHS_Desc_CHS = "勾选后，称号可被回收。";
		public string Lang_CityIntelsAcquiredString_CHS = "研修时间";
		public string Lang_CityIntelsAcquiredString_CHS_Desc_CHS = "设定修得称号所需要的时间。";
		public string Lang_CityEmperorResidesHere_CHS = "成功率";
		public string Lang_CityEmperorResidesHere_CHS_Desc_CHS = "设定修得称号的机率。";
		public string Lang_CityHasDisaster_CHS = "名称";
		public string Lang_CityHasDisaster_CHS_Desc_CHS = "称号类型的名称。";
		public string Lang_CityDisasterType_CHS = "战斗用";
		public string Lang_CityDisasterType_CHS_Desc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string Lang_CityDisasterDaysLeft_CHS = "随机教授";
		public string Lang_CityDisasterDaysLeft_CHS_Desc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string Lang_CityTodayPersonArriveNote_CHS = "可回收";
		public string Lang_CityTodayPersonArriveNote_CHS_Desc_CHS = "勾选后，称号可被回收。";
		public string Lang_CityHasManualHire_CHS = "研修时间";
		public string Lang_CityHasManualHire_CHS_Desc_CHS = "设定修得称号所需要的时间。";
		public string Lang_CityAILandLinksString_CHS = "成功率";
		public string Lang_CityAILandLinksString_CHS_Desc_CHS = "设定修得称号的机率。";
		public string Lang_CityAIWaterLinksString_CHS = "名称";
		public string Lang_CityAIWaterLinksString_CHS_Desc_CHS = "称号类型的名称。";
		public string Lang_CityDefensiveLegionID_CHS = "战斗用";
		public string Lang_CityDefensiveLegionID_CHS_Desc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string Lang_CityPlanArchitectureID_CHS = "随机教授";
		public string Lang_CityPlanArchitectureID_CHS_Desc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string Lang_CityPlanFacilityKindID_CHS = "可回收";
		public string Lang_CityPlanFacilityKindID_CHS_Desc_CHS = "勾选后，称号可被回收。";
		public string Lang_CityRecentlyAttacked_CHS = "研修时间";
		public string Lang_CityRecentlyAttacked_CHS_Desc_CHS = "设定修得称号所需要的时间。";
		public string Lang_CityRecentlyHit_CHS = "成功率";
		public string Lang_CityRecentlyHit_CHS_Desc_CHS = "设定修得称号的机率。";
		public string Lang_CityRecentlyBreaked_CHS = "名称";
		public string Lang_CityRecentlyBreaked_CHS_Desc_CHS = "称号类型的名称。";
		public string Lang_CityRobberTroopID_CHS = "战斗用";
		public string Lang_CityRobberTroopID_CHS_Desc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string Lang_CityTransferFunArchitectureID_CHS = "随机教授";
		public string Lang_CityTransferFunArchitectureID_CHS_Desc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string Lang_CityTransferFoodArchitectureID_CHS = "可回收";
		public string Lang_CityTransferFoodArchitectureID_CHS_Desc_CHS = "勾选后，称号可被回收。";
		public string Lang_CityTroopershipAvailable_CHS = "研修时间";
		public string Lang_CityTroopershipAvailable_CHS_Desc_CHS = "设定修得称号所需要的时间。";
		public string Lang_CitynoFundToSustainFacility_CHS = "成功率";
		public string Lang_CitynoFundToSustainFacility_CHS_Desc_CHS = "设定修得称号的机率。";
		public string Lang_CitySuspendTroopTransfer_CHS = "名称";
		public string Lang_CitySuspendTroopTransfer_CHS_Desc_CHS = "称号类型的名称。";
		public string Lang_CityArchitectureAreaString_CHS = "战斗用";
		public string Lang_CityArchitectureAreaString_CHS_Desc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string Lang_CityFundPacksString_CHS = "随机教授";
		public string Lang_CityFundPacksString_CHS_Desc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string Lang_CityFoodPacksString_CHS = "可回收";
		public string Lang_CityFoodPacksString_CHS_Desc_CHS = "勾选后，称号可被回收。";
		public string Lang_CityPopulationPacksString_CHS = "研修时间";
		public string Lang_CityPopulationPacksString_CHS_Desc_CHS = "设定修得称号所需要的时间。";
		public string Lang_CityMilitaryPopulationPacksString_CHS = "成功率";
		public string Lang_CityMilitaryPopulationPacksString_CHS_Desc_CHS = "设定修得称号的机率。";
		public string Lang_CityMayorOnDutyDays_CHS = "名称";
		public string Lang_CityMayorOnDutyDays_CHS_Desc_CHS = "称号类型的名称。";
		public string Lang_CityOldFactionName_CHS = "战斗用";
		public string Lang_CityOldFactionName_CHS_Desc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string Lang_CityAutoHiring_CHS = "随机教授";
		public string Lang_CityAutoHiring_CHS_Desc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string Lang_CityAutoRewarding_CHS = "可回收";
		public string Lang_CityAutoRewarding_CHS_Desc_CHS = "勾选后，称号可被回收。";
		public string Lang_CityAutoSearching_CHS = "研修时间";
		public string Lang_CityAutoSearching_CHS_Desc_CHS = "设定修得称号所需要的时间。";
		public string Lang_CityAutoZhaoXian_CHS = "成功率";
		public string Lang_CityAutoZhaoXian_CHS_Desc_CHS = "设定修得称号的机率。";
		public string Lang_CityAutoWorking_CHS = "名称";
		public string Lang_CityAutoWorking_CHS_Desc_CHS = "称号类型的名称。";
		public string Lang_CityAutoRecruiting_CHS = "战斗用";
		public string Lang_CityAutoRecruiting_CHS_Desc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string Lang_CityFacilityEnabled_CHS = "随机教授";
		public string Lang_CityFacilityEnabled_CHS_Desc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string Lang_CityHireFinished_CHS = "可回收";
		public string Lang_CityHireFinished_CHS_Desc_CHS = "勾选后，称号可被回收。";
		
        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"MayorID", "-1"},
                {"BuildingFacility", "-1"},
                {"PlanFacilityKind", "-1"},
                {"PlanArchitecture", "-1"},
                {"TransferFundArchitecture", "-1"},
                {"TransferFoodArchitecture", "-1"},
                {"DefensiveLegion", "-1"},
                {"RobberTroop", "-1"}
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new string[]
            {
                "ID",
                "Name",
                "KindId",
                "CaptionID",
                "IsStrategicCenter",
                "StateID",
                "Area",
                "MayorID",
                "CharacteristicsString",
                "PersonsString",
                "CharactersTravellingString",
                "UnemployedString",
                "UnemployedTravellingString",
                "ConsortListString",
                "CaptivesString",
                "Population",
                "ServicePopulation",
                "Fund",
                "Food",
                "Agriculture",
                "Commerce",
                "Technology",
                "Domination",
                "Morale",
                "Endurance",
                "FormationsString",
                "FacilitiesString",
                "BuildingFacility",
                "BuildingDaysLeft",
                "IntelsAcquiredString",
                "EmperorResidesHere",
                "HasDisaster",
                "DisasterType",
                "DisasterDaysLeft",
                "TodayPersonArriveNote",
                "HasManualHire",
                "AILandLinksString",
                "AIWaterLinksString",
                "DefensiveLegionID",
                "PlanArchitectureID",
                "PlanFacilityKindID",
                "RecentlyAttacked",
                "RecentlyHit",
                "RecentlyBreaked",
                "RobberTroopID",
                "TransferFunArchitectureID",
                "TransferFoodArchitectureID",
                "TroopershipAvailable",
                "noFundToSustainFacility",
                "SuspendTroopTransfer",
                "ArchitectureAreaString",
                "FundPacksString",
                "FoodPacksString",
                "PopulationPacksString",
                "MilitaryPopulationPacksString",
                "MayorOnDutyDays",
                "OldFactionName",
                "AutoHiring",
                "AutoRewarding",
                "AutoSearching",
                "AutoZhaoXian",
                "AutoWorking",
                "AutoRecruiting",
                "FacilityEnabled",
                "HireFinished"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { Lang_CityID_CHS, Lang_CityID_CHS_Desc_CHS },
                { Lang_CityName_CHS, Lang_CityName_CHS_Desc_CHS },
                { Lang_CityKindId_CHS, Lang_CityKindId_CHS_Desc_CHS },
                { Lang_CityCaptionID_CHS, Lang_CityCaptionID_CHS_Desc_CHS },
                { Lang_CityIsStrategicCenter_CHS, Lang_CityIsStrategicCenter_CHS_Desc_CHS },
                { Lang_CityStateID_CHS, Lang_CityStateID_CHS_Desc_CHS },
                { Lang_CityArea_CHS, Lang_CityArea_CHS_Desc_CHS }
            };
        }

        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Architectures);
        }

        public CityTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
