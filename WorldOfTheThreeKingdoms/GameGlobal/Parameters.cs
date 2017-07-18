using System;
using System.Xml;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Platforms;

namespace GameGlobal
{
    [DataContract]
    public class Parameters
    {
        [DataMember]
        public static float AIArchitectureDamageRate = 1f;
        [DataMember]
        public static float AIFoodRate = 1f;
        [DataMember]
        public static float AIFundRate = 1f;
        [DataMember]
        public static float AIRecruitmentSpeedRate = 1f;
        [DataMember]
        public static float AITrainingSpeedRate = 1f;
        [DataMember]
        public static float AITroopDefenceRate = 1f;
        [DataMember]
        public static float AITroopOffenceRate = 1f;
        [DataMember]
        public static float ArchitectureDamageRate = 1f;
        [DataMember]
        public static int AIAntiStratagem = 0;
        [DataMember]
        public static int AIAntiSurround = 0;
        [DataMember]
        public static int BuyFoodAgriculture = 500;
        [DataMember]
        public static int ChangeCapitalCost = 0x1388;
        [DataMember]
        public static int ClearFieldAgricultureCostUnit = 3;
        [DataMember]
        public static int ClearFieldFundCostUnit = 50;
        [DataMember]
        public static int ConvincePersonCost = 200;
        [DataMember]
        public static float DefaultPopulationDevelopingRate = 6E-05f;
        [DataMember]
        public static int DestroyArchitectureCost = 200;
        [DataMember]
        public static int FindTreasureChance = 10;
        [DataMember]
        public static float FireDamageScale = 0.5f;
        [DataMember]
        public static float FollowedLeaderDefenceRateIncrement = 0.2f;
        [DataMember]
        public static float FollowedLeaderOffenceRateIncrement = 0.2f;
        [DataMember]
        public static float FoodRate = 1f;
        [DataMember]
        public static int FoodToFundDivisor = 200;
        [DataMember]
        public static float FundRate = 1f;
        [DataMember]
        public static int FundToFoodMultiple = 50;
        [DataMember]
        public static int GossipArchitectureCost = 200;
        [DataMember]
        public static int JailBreakArchitectureCost = 200;
        [DataMember]
        public static int HireNoFactionPersonCost = 100;
        [DataMember]
        public static int InstigateArchitectureCost = 200;
        [DataMember]
        public static int InternalFundCost = 5;
        [DataMember]
        public static float InternalRate = 1f;
        [DataMember]
        public static int LearnSkillDays = 30;
        [DataMember]
        public static int LearnStuntDays = 60;
        [DataMember]
        public static int LearnTitleDays = 90;
        [DataMember]
        public static int SearchDays = 10;
        [DataMember]
        public static int RecruitmentDomination = 50;
        [DataMember]
        public static int RecruitmentFundCost = 20;
        [DataMember]
        public static int RecruitmentMorale = 100;
        [DataMember]
        public static float RecruitmentRate = 1f;
        [DataMember]
        public static int RewardPersonCost = 100;
        [DataMember]
        public static int SellFoodCommerce = 500;
        [DataMember]
        public static int SendSpyCost = 200;
        [DataMember]
        public static int SurroundArchitectureDominationUnit = 2;
        [DataMember]
        public static float TrainingRate = 1f;
        [DataMember]
        public static float TroopDamageRate = 1f;
        [DataMember]
        public static float AIArchitectureDamageYearIncreaseRate = 0f;
        [DataMember]
        public static float AIFoodYearIncreaseRate = 0f;
        [DataMember]
        public static float AIFundYearIncreaseRate = 0f;
        [DataMember]
        public static float AIRecruitmentSpeedYearIncreaseRate = 0f;
        [DataMember]
        public static float AITrainingSpeedYearIncreaseRate = 0f;
        [DataMember]
        public static float AITroopDefenceYearIncreaseRate = 0f;
        [DataMember]
        public static float AITroopOffenceYearIncreaseRate = 0f;
        [DataMember]
        public static float AIArmyExperienceYearIncreaseRate = 0f;
        [DataMember]
        public static float AIOfficerExperienceYearIncreaseRate = 0f;
        [DataMember]
        public static float AIAntiStratagemIncreaseRate = 0f;
        [DataMember]
        public static float AIAntiSurroundIncreaseRate = 0f;
        [DataMember]
        public static float AIOfficerExperienceRate = 1f;
        [DataMember]
        public static float AIArmyExperienceRate = 1f;

        private static float BasicAIArchitectureDamageRate = 1f;
        private static float BasicAIFoodRate = 1f;
        private static float BasicAIFundRate = 1f;
        private static float BasicAIRecruitmentSpeedRate = 1f;
        private static float BasicAITrainingSpeedRate = 1f;
        private static float BasicAITroopDefenceRate = 1f;
        private static float BasicAITroopOffenceRate = 1f;
        private static float BasicAIArmyExperienceRate = 1f;
        private static float BasicAIOfficerExperienceRate = 1f;
        private static int BasicAIAntiStratagem = 0;
        private static int BasicAIAntiSurround = 0;

        [DataMember]
        public static float AIBackendArmyReserveCalmBraveDifferenceMultiply = 5;
        [DataMember]
        public static float AIBackendArmyReserveAmbitionMultiply = 10;
        [DataMember]
        public static float AIBackendArmyReserveAdd = 50;
        [DataMember]
        public static float AIBackendArmyReserveMultiply = 1;
        [DataMember]
        public static int AITradePeriod = 10;
        [DataMember]
        public static int AITreasureChance = 10;
        [DataMember]
        public static int AITreasureCountMax = 2;
        [DataMember]
        public static float AITreasureCountCappedTitleLevelAdd = 0;
        [DataMember]
        public static float AITreasureCountCappedTitleLevelMultiply = 1;
        [DataMember]
        public static int AIGiveTreasureMaxWorth = 40;
        [DataMember]
        public static float AIFacilityFundMonthWaitParam = 8;
        [DataMember]
        public static float AIFacilityDestroyValueRate = 2;
        [DataMember]
        public static float AIBuildHougongUnambitionProbWeight = 10;
        [DataMember]
        public static float AIBuildHougongSpaceBuiltProbWeight = 5;
        [DataMember]
        public static int AIBuildHougongMaxSizeAdd = 0;
        [DataMember]
        public static int AIBuildHougongSkipSizeChance = 80;
        [DataMember]
        public static int AINafeiUncreultyProbAdd = -1;
        [DataMember]
        public static float AINafeiAbilityThresholdRate = 30000;
        [DataMember]
        public static float AINafeiStealSpouseThresholdRateAdd = 0.5f;
        [DataMember]
        public static float AINafeiStealSpouseThresholdRateMultiply = 1;
        [DataMember]
        public static int AINafeiMaxAgeThresholdAdd = 30;
        [DataMember]
        public static float AINafeiMaxAgeThresholdMultiply = 1;
        [DataMember]
        public static float AINafeiSkipChanceAdd = 25;
        [DataMember]
        public static float AINafeiSkipChanceMultiply = 15;
        [DataMember]
        public static float AIChongxingChanceAdd = 10;
        [DataMember]
        public static float AIChongxingChanceMultiply = 20;
        [DataMember]
        public static float AIRecruitPopulationCapMultiply = 90;
        [DataMember]
        public static float AIRecruitPopulationCapBackendMultiply = 0.5f;
        [DataMember]
        public static float AIRecruitPopulationCapHostilelineMultiply = 1.2f;
        [DataMember]
        public static float AIRecruitPopulationCapStrategyTendencyMulitply = 0.2f;
        [DataMember]
        public static float AIRecruitPopulationCapStrategyTendencyAdd = 0.2f;
        [DataMember]
        public static int AINewMilitaryPopulationThresholdDivide = 30000;
        [DataMember]
        public static int AINewMilitaryPersonThresholdDivide = 5;
        [DataMember]
        public static int AIExecuteMaxUncreulty = 4;
        [DataMember]
        public static float AIExecutePersonIdealToleranceMultiply = 15;
        [DataMember]
        public static float AIHougongArchitectureCountProbMultiply = 10;
        [DataMember]
        public static float AIHougongArchitectureCountProbPower = 0.5f;
        [DataMember]
        public static int FireStayProb = 20;
        [DataMember]
        public static float FireSpreadProbMultiply = 1f;
        [DataMember]
        public static int MinPregnantProb = 0;
        [DataMember]
        public static float InternalExperienceRate = 1f;
        [DataMember]
        public static float AbilityExperienceRate = 1f;
        [DataMember]
        public static float ArmyExperienceRate = 1f;
        [DataMember]
        public static float AIAttackChanceIfUnfull = 5;
        [DataMember]
        public static int AIObeyStrategyTendencyChance = 90;
        [DataMember]
        public static int AIOffendMaxDiplomaticRelationMultiply = 20;
        [DataMember]
        public static float AIOffendReserveAdd = 0.8f;
        [DataMember]
        public static float AIOffendReserveBCDiffMultiply = 0.1f;
        [DataMember]
        public static float AIOffendDefendingTroopRate = 0.75f;
        [DataMember]
        public static float AIOffendDefendTroopAdd = 1.2f;
        [DataMember]
        public static float AIOffendDefendTroopMultiply = 0.1f;
        [DataMember]
        public static int AIOffendIgnoreReserveProbAmbitionMultiply = 5;
        [DataMember]
        public static int AIOffendIgnoreReserveProbAmbitionAdd = -2;
        [DataMember]
        public static int AIOffendIgnoreReserveProbBCDiffMultiply = 2;
        [DataMember]
        public static int AIOffendIgnoreReserveProbBCDiffAdd = 10;
        [DataMember]
        public static float AIOffendIgnoreReserveChanceTroopRatioAdd = -0.8f;
        [DataMember]
        public static float AIOffendIgnoreReserveChanceTroopRatioMultiply = 100.0f;
        [DataMember]
        public static int PrincessMaintainenceCost = 50;
        [DataMember]
        public static int AIUniqueTroopFightingForceThreshold = 60000;
        [DataMember]
        public static int LearnSkillSuccessRate = 0;
        [DataMember]
        public static int LearnStuntSuccessRate = 75;
        [DataMember]
        public static int LearnTitleSuccessRate = 0;
        [DataMember]
        public static int AutoLearnSkillSuccessRate = 0;
        [DataMember]
        public static int AutoLearnStuntSuccessRate = 0;
        [DataMember]
        public static float MilitaryPopulationCap = 0.1f;
        [DataMember]
        public static float MilitaryPopulationReloadQuantity = 1.0f;
        [DataMember]
        public static int CloseThreshold = 500;
        [DataMember]
        public static int HateThreshold = -500;
        [DataMember]
        public static int VeryCloseThreshold = 2000;
        [DataMember]
        public static int MaxAITroopCountCandidates = 1000;
        [DataMember]
        public static float PopulationDevelopingRate = 1;
        [DataMember]
        public static float CloseAbilityRate = 1.1F;
        [DataMember]
        public static float VeryCloseAbilityRate = 1.2F;
        [DataMember]
        public static int RetainFeiziPersonalLoyalty = 0;
        [DataMember]
        public static int AIEncirclePlayerRate = 0;
        [DataMember]
        public static float BasicAIExtraPerson = 0;
        [DataMember]
        public static float AIExtraPerson = 0;
        [DataMember]
        public static float AIExtraPersonIncreaseRate = 0;
        [DataMember]
        public static int AITirednessDecrease = 0;
        [DataMember]
        public static int InternalSurplusFactor = 10000000;
        [DataMember]
        public static int MakeMarrigeIdealLimit = 5;
        [DataMember]
        public static int MakeMarriageCost = 80000;
        [DataMember]
        public static int NafeiCost = 50000;
        [DataMember]
        public static int SelectPrinceCost = 50000;
        [DataMember]
        public static int TransferCostPerMilitary = 2000;
        [DataMember]
        public static int TransferFoodPerMilitary = 2000;
        [DataMember]
        public static int AIEncircleRank = 0;
        [DataMember]
        public static int AIEncircleVar = 0;
        [DataMember]
        public static float RansomRate = 1.0f;
        [DataMember]
        public static List<int> ExpandConditions = new List<int>();
        [DataMember]
        public static float SearchPersonArchitectureCountPower = 0;

        public void InitializeGameParameters()
        {
            XmlDocument document = new XmlDocument();

            string xml = Platform.Current.LoadText("Content/Data/GameParameters.xml");
            document.LoadXml(xml);

            XmlNode nextSibling = document.FirstChild.NextSibling;
            FindTreasureChance = int.Parse(nextSibling.Attributes.GetNamedItem("FindTreasureChance").Value);
            LearnSkillDays = int.Parse(nextSibling.Attributes.GetNamedItem("LearnSkillDays").Value);
            LearnStuntDays = int.Parse(nextSibling.Attributes.GetNamedItem("LearnStuntDays").Value);
            LearnTitleDays = int.Parse(nextSibling.Attributes.GetNamedItem("LearnTitleDays").Value);
            SearchDays = int.Parse(nextSibling.Attributes.GetNamedItem("SearchDays").Value);
            FollowedLeaderOffenceRateIncrement = float.Parse(nextSibling.Attributes.GetNamedItem("FollowedLeaderOffenceRateIncrement").Value);
            FollowedLeaderDefenceRateIncrement = float.Parse(nextSibling.Attributes.GetNamedItem("FollowedLeaderDefenceRateIncrement").Value);
            InternalRate = float.Parse(nextSibling.Attributes.GetNamedItem("InternalRate").Value);
            TrainingRate = float.Parse(nextSibling.Attributes.GetNamedItem("TrainingRate").Value);
            RecruitmentRate = float.Parse(nextSibling.Attributes.GetNamedItem("RecruitmentRate").Value);
            FundRate = float.Parse(nextSibling.Attributes.GetNamedItem("FundRate").Value);
            FoodRate = float.Parse(nextSibling.Attributes.GetNamedItem("FoodRate").Value);
            TroopDamageRate = float.Parse(nextSibling.Attributes.GetNamedItem("TroopDamageRate").Value);
            ArchitectureDamageRate = float.Parse(nextSibling.Attributes.GetNamedItem("ArchitectureDamageRate").Value);
            DefaultPopulationDevelopingRate = float.Parse(nextSibling.Attributes.GetNamedItem("DefaultPopulationDevelopingRate").Value);
            BuyFoodAgriculture = int.Parse(nextSibling.Attributes.GetNamedItem("BuyFoodAgriculture").Value);
            SellFoodCommerce = int.Parse(nextSibling.Attributes.GetNamedItem("SellFoodCommerce").Value);
            FundToFoodMultiple = int.Parse(nextSibling.Attributes.GetNamedItem("FundToFoodMultiple").Value);
            FoodToFundDivisor = int.Parse(nextSibling.Attributes.GetNamedItem("FoodToFundDivisor").Value);
            InternalFundCost = int.Parse(nextSibling.Attributes.GetNamedItem("InternalFundCost").Value);
            RecruitmentFundCost = int.Parse(nextSibling.Attributes.GetNamedItem("RecruitmentFundCost").Value);
            RecruitmentDomination = int.Parse(nextSibling.Attributes.GetNamedItem("RecruitmentDomination").Value);
            RecruitmentMorale = int.Parse(nextSibling.Attributes.GetNamedItem("RecruitmentMorale").Value);
            ChangeCapitalCost = int.Parse(nextSibling.Attributes.GetNamedItem("ChangeCapitalCost").Value);
            SelectPrinceCost = int.Parse(nextSibling.Attributes.GetNamedItem("SelectPrinceCost").Value);
            TransferCostPerMilitary = int.Parse(nextSibling.Attributes.GetNamedItem("TransferCostPerMilitary").Value); //运兵耗钱
            TransferFoodPerMilitary = int.Parse(nextSibling.Attributes.GetNamedItem("TransferFoodPerMilitary").Value);  //运兵耗粮
            HireNoFactionPersonCost = int.Parse(nextSibling.Attributes.GetNamedItem("HireNoFactionPersonCost").Value);
            ConvincePersonCost = int.Parse(nextSibling.Attributes.GetNamedItem("ConvincePersonCost").Value);
            RewardPersonCost = int.Parse(nextSibling.Attributes.GetNamedItem("RewardPersonCost").Value);
            SendSpyCost = int.Parse(nextSibling.Attributes.GetNamedItem("SendSpyCost").Value);
            DestroyArchitectureCost = int.Parse(nextSibling.Attributes.GetNamedItem("DestroyArchitectureCost").Value);
            InstigateArchitectureCost = int.Parse(nextSibling.Attributes.GetNamedItem("InstigateArchitectureCost").Value);
            GossipArchitectureCost = int.Parse(nextSibling.Attributes.GetNamedItem("GossipArchitectureCost").Value);
            JailBreakArchitectureCost = int.Parse(nextSibling.Attributes.GetNamedItem("JailBreakArchitectureCost").Value);
            ClearFieldFundCostUnit = int.Parse(nextSibling.Attributes.GetNamedItem("ClearFieldFundCostUnit").Value);
            ClearFieldAgricultureCostUnit = int.Parse(nextSibling.Attributes.GetNamedItem("ClearFieldAgricultureCostUnit").Value);
            SurroundArchitectureDominationUnit = int.Parse(nextSibling.Attributes.GetNamedItem("SurroundArchitectureDominationUnit").Value);
            FireDamageScale = float.Parse(nextSibling.Attributes.GetNamedItem("FireDamageScale").Value);
            AIFundRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIFundRate").Value);
            AIFoodRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIFoodRate").Value);
            AITroopOffenceRate = float.Parse(nextSibling.Attributes.GetNamedItem("AITroopOffenceRate").Value);
            AITroopDefenceRate = float.Parse(nextSibling.Attributes.GetNamedItem("AITroopDefenceRate").Value);
            AIArchitectureDamageRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIArchitectureDamageRate").Value);
            AITrainingSpeedRate = float.Parse(nextSibling.Attributes.GetNamedItem("AITrainingSpeedRate").Value);
            AIRecruitmentSpeedRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIRecruitmentSpeedRate").Value);
            AIOfficerExperienceRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIOfficerExperienceRate").Value);
            AIArmyExperienceRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIArmyExperienceRate").Value);
            AIAntiStratagem = int.Parse(nextSibling.Attributes.GetNamedItem("AIAntiStratagem").Value);
            AIAntiSurround = int.Parse(nextSibling.Attributes.GetNamedItem("AIAntiSurround").Value);

            AIFundYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIFundYearIncreaseRate").Value);
            AIFoodYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIFoodYearIncreaseRate").Value);
            AITroopOffenceYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AITroopOffenceYearIncreaseRate").Value);
            AITroopDefenceYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AITroopDefenceYearIncreaseRate").Value);
            AIArchitectureDamageYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIArchitectureDamageYearIncreaseRate").Value);
            AITrainingSpeedYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AITrainingSpeedYearIncreaseRate").Value);
            AIRecruitmentSpeedYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIRecruitmentSpeedYearIncreaseRate").Value);
            AIOfficerExperienceYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIOfficerExperienceYearIncreaseRate").Value);
            AIArmyExperienceYearIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIArmyExperienceYearIncreaseRate").Value);
            AIAntiStratagemIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIAntiStratagemIncreaseRate").Value);
            AIAntiSurroundIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIAntiSurroundIncreaseRate").Value);

            AIBackendArmyReserveCalmBraveDifferenceMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIBackendArmyReserveCalmBraveDifferenceMultiply").Value);
            AIBackendArmyReserveAmbitionMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIBackendArmyReserveAmbitionMultiply").Value);
            AIBackendArmyReserveAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AIBackendArmyReserveAdd").Value);
            AIBackendArmyReserveMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIBackendArmyReserveMultiply").Value);
            AITradePeriod = int.Parse(nextSibling.Attributes.GetNamedItem("AITradePeriod").Value);
            AITreasureChance = int.Parse(nextSibling.Attributes.GetNamedItem("AITreasureChance").Value);
            AITreasureCountMax = int.Parse(nextSibling.Attributes.GetNamedItem("AITreasureCountMax").Value);
            AITreasureCountCappedTitleLevelAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AITreasureCountCappedTitleLevelAdd").Value);
            AITreasureCountCappedTitleLevelMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AITreasureCountCappedTitleLevelMultiply").Value);
            AIGiveTreasureMaxWorth = int.Parse(nextSibling.Attributes.GetNamedItem("AIGiveTreasureMaxWorth").Value);
            AIFacilityFundMonthWaitParam = float.Parse(nextSibling.Attributes.GetNamedItem("AIFacilityFundMonthWaitParam").Value);
            AIFacilityDestroyValueRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIFacilityDestroyValueRate").Value);
            AIBuildHougongUnambitionProbWeight = float.Parse(nextSibling.Attributes.GetNamedItem("AIBuildHougongUnambitionProbWeight").Value);
            AIBuildHougongSpaceBuiltProbWeight = float.Parse(nextSibling.Attributes.GetNamedItem("AIBuildHougongSpaceBuiltProbWeight").Value);
            AIBuildHougongMaxSizeAdd = int.Parse(nextSibling.Attributes.GetNamedItem("AIBuildHougongMaxSizeAdd").Value);
            AIBuildHougongSkipSizeChance = int.Parse(nextSibling.Attributes.GetNamedItem("AIBuildHougongSkipSizeChance").Value);
            AINafeiUncreultyProbAdd = int.Parse(nextSibling.Attributes.GetNamedItem("AINafeiUncreultyProbAdd").Value);
            AINafeiAbilityThresholdRate = float.Parse(nextSibling.Attributes.GetNamedItem("AINafeiAbilityThresholdRate").Value);
            AINafeiStealSpouseThresholdRateAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AINafeiStealSpouseThresholdRateAdd").Value);
            AINafeiStealSpouseThresholdRateMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AINafeiStealSpouseThresholdRateMultiply").Value);
            AINafeiMaxAgeThresholdAdd = int.Parse(nextSibling.Attributes.GetNamedItem("AINafeiMaxAgeThresholdAdd").Value);
            AINafeiMaxAgeThresholdMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AINafeiMaxAgeThresholdMultiply").Value);
            AINafeiSkipChanceAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AINafeiSkipChanceAdd").Value);
            AINafeiSkipChanceMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AINafeiSkipChanceMultiply").Value);
            AIChongxingChanceAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AIChongxingChanceAdd").Value);
            AIChongxingChanceMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIChongxingChanceMultiply").Value);
            AIRecruitPopulationCapMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIRecruitPopulationCapMultiply").Value);
            AIRecruitPopulationCapBackendMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIRecruitPopulationCapBackendMultiply").Value);
            AIRecruitPopulationCapHostilelineMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIRecruitPopulationCapHostilelineMultiply").Value);
            AIRecruitPopulationCapStrategyTendencyMulitply = float.Parse(nextSibling.Attributes.GetNamedItem("AIRecruitPopulationCapStrategyTendencyMulitply").Value);
            AIRecruitPopulationCapStrategyTendencyAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AIRecruitPopulationCapStrategyTendencyAdd").Value);
            AINewMilitaryPopulationThresholdDivide = int.Parse(nextSibling.Attributes.GetNamedItem("AINewMilitaryPopulationThresholdDivide").Value);
            AINewMilitaryPersonThresholdDivide = int.Parse(nextSibling.Attributes.GetNamedItem("AINewMilitaryPersonThresholdDivide").Value);
            AIExecuteMaxUncreulty = int.Parse(nextSibling.Attributes.GetNamedItem("AIExecuteMaxUncreulty").Value);
            AIExecutePersonIdealToleranceMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIExecutePersonIdealToleranceMultiply").Value);

            AIHougongArchitectureCountProbMultiply = int.Parse(nextSibling.Attributes.GetNamedItem("AIHougongArchitectureCountProbMultiply").Value);
            AIHougongArchitectureCountProbPower = float.Parse(nextSibling.Attributes.GetNamedItem("AIHougongArchitectureCountProbPower").Value);

            FireStayProb = int.Parse(nextSibling.Attributes.GetNamedItem("FireStayProb").Value);
            FireSpreadProbMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("FireSpreadProbMultiply").Value);

            MinPregnantProb = int.Parse(nextSibling.Attributes.GetNamedItem("MinPregnantProb").Value);

            InternalExperienceRate = float.Parse(nextSibling.Attributes.GetNamedItem("InternalExperienceRate").Value);
            AbilityExperienceRate = float.Parse(nextSibling.Attributes.GetNamedItem("AbilityExperienceRate").Value);
            ArmyExperienceRate = float.Parse(nextSibling.Attributes.GetNamedItem("ArmyExperienceRate").Value);

            AIAttackChanceIfUnfull = float.Parse(nextSibling.Attributes.GetNamedItem("AIAttackChanceIfUnfull").Value);
            AIObeyStrategyTendencyChance = int.Parse(nextSibling.Attributes.GetNamedItem("AIObeyStrategyTendencyChance").Value);
            AIOffendMaxDiplomaticRelationMultiply = int.Parse(nextSibling.Attributes.GetNamedItem("AIOffendMaxDiplomaticRelationMultiply").Value);
            AIOffendReserveAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AIOffendReserveAdd").Value);
            AIOffendReserveBCDiffMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIOffendReserveBCDiffMultiply").Value);
            AIOffendDefendingTroopRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIOffendDefendingTroopRate").Value);
            AIOffendDefendTroopAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AIOffendDefendTroopAdd").Value);
            AIOffendDefendTroopMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIOffendDefendTroopMultiply").Value);
            AIOffendIgnoreReserveProbAmbitionMultiply = int.Parse(nextSibling.Attributes.GetNamedItem("AIOffendIgnoreReserveProbAmbitionMultiply").Value);
            AIOffendIgnoreReserveProbAmbitionAdd = int.Parse(nextSibling.Attributes.GetNamedItem("AIOffendIgnoreReserveProbAmbitionAdd").Value);
            AIOffendIgnoreReserveProbBCDiffMultiply = int.Parse(nextSibling.Attributes.GetNamedItem("AIOffendIgnoreReserveProbBCDiffMultiply").Value);
            AIOffendIgnoreReserveProbBCDiffAdd = int.Parse(nextSibling.Attributes.GetNamedItem("AIOffendIgnoreReserveProbBCDiffAdd").Value);
            AIOffendIgnoreReserveChanceTroopRatioAdd = float.Parse(nextSibling.Attributes.GetNamedItem("AIOffendIgnoreReserveChanceTroopRatioAdd").Value);
            AIOffendIgnoreReserveChanceTroopRatioMultiply = float.Parse(nextSibling.Attributes.GetNamedItem("AIOffendIgnoreReserveChanceTroopRatioMultiply").Value);
            PrincessMaintainenceCost = int.Parse(nextSibling.Attributes.GetNamedItem("PrincessMaintainenceCost").Value);

            AIUniqueTroopFightingForceThreshold = int.Parse(nextSibling.Attributes.GetNamedItem("AIUniqueTroopFightingForceThreshold").Value);
            LearnSkillSuccessRate = int.Parse(nextSibling.Attributes.GetNamedItem("LearnSkillSuccessRate").Value);
            LearnStuntSuccessRate = int.Parse(nextSibling.Attributes.GetNamedItem("LearnStuntSuccessRate").Value);
            LearnTitleSuccessRate = int.Parse(nextSibling.Attributes.GetNamedItem("LearnTitleSuccessRate").Value);

            AutoLearnSkillSuccessRate = int.Parse(nextSibling.Attributes.GetNamedItem("AutoLearnSkillSuccessRate").Value);
            AutoLearnStuntSuccessRate = int.Parse(nextSibling.Attributes.GetNamedItem("AutoLearnStuntSuccessRate").Value);

            MilitaryPopulationCap = float.Parse(nextSibling.Attributes.GetNamedItem("MilitaryPopulationCap").Value);
            MilitaryPopulationReloadQuantity = float.Parse(nextSibling.Attributes.GetNamedItem("MilitaryPopulationReloadQuantity").Value);

            CloseThreshold = int.Parse(nextSibling.Attributes.GetNamedItem("CloseThreshold").Value);
            HateThreshold = int.Parse(nextSibling.Attributes.GetNamedItem("HateThreshold").Value);
            VeryCloseThreshold = int.Parse(nextSibling.Attributes.GetNamedItem("VeryCloseThreshold").Value);

            MaxAITroopCountCandidates = int.Parse(nextSibling.Attributes.GetNamedItem("MaxAITroopCountCandidates").Value);
            PopulationDevelopingRate = float.Parse(nextSibling.Attributes.GetNamedItem("PopulationDevelopingRate").Value);

            CloseAbilityRate = float.Parse(nextSibling.Attributes.GetNamedItem("CloseAbilityRate").Value);
            VeryCloseAbilityRate = float.Parse(nextSibling.Attributes.GetNamedItem("VeryCloseAbilityRate").Value);

            RetainFeiziPersonalLoyalty = int.Parse(nextSibling.Attributes.GetNamedItem("RetainFeiziPersonalLoyalty").Value);

            AIEncirclePlayerRate = int.Parse(nextSibling.Attributes.GetNamedItem("AIEncirclePlayerRate").Value);

            InternalSurplusFactor = int.Parse(nextSibling.Attributes.GetNamedItem("InternalSurplusFactor").Value);
            AIExtraPerson = float.Parse(nextSibling.Attributes.GetNamedItem("AIExtraPerson").Value);
            AIExtraPersonIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIExtraPersonIncreaseRate").Value);

            StaticMethods.LoadFromString(ExpandConditions, nextSibling.Attributes.GetNamedItem("ExpandConditions").Value);

            SearchPersonArchitectureCountPower = float.Parse(nextSibling.Attributes.GetNamedItem("SearchPersonArchitectureCountPower").Value);
            AIEncircleRank = int.Parse(nextSibling.Attributes.GetNamedItem("AIEncircleRank").Value);
            AIEncircleVar = int.Parse(nextSibling.Attributes.GetNamedItem("AIEncircleVar").Value);

            RansomRate = float.Parse(nextSibling.Attributes.GetNamedItem("RansomRate").Value);

            BasicAIFundRate = AIFundRate;
            BasicAIFoodRate = AIFoodRate;
            BasicAITroopOffenceRate = AITroopOffenceRate;
            BasicAITroopDefenceRate = AITroopDefenceRate;
            BasicAIArchitectureDamageRate = AIArchitectureDamageRate;
            BasicAITrainingSpeedRate = AITrainingSpeedRate;
            BasicAIRecruitmentSpeedRate = AIRecruitmentSpeedRate;
            BasicAIArmyExperienceRate = AIArmyExperienceRate;
            BasicAIOfficerExperienceRate = AIOfficerExperienceRate;
            BasicAIAntiStratagem = AIAntiStratagem;
            BasicAIAntiSurround = AIAntiSurround;
            BasicAIExtraPerson = AIExtraPerson;
        }

        public static void DayEvent(int year)
        {
            AIFundRate = year * AIFundYearIncreaseRate + BasicAIFundRate;
            AIFoodRate = year * AIFoodYearIncreaseRate + BasicAIFoodRate;
            AITroopOffenceRate = year * AITroopOffenceYearIncreaseRate + BasicAITroopOffenceRate;
            AITroopDefenceRate = year * AITroopDefenceYearIncreaseRate + BasicAITroopDefenceRate;
            AIArchitectureDamageRate = year * AIArchitectureDamageYearIncreaseRate + BasicAIArchitectureDamageRate;
            AITrainingSpeedRate = year * AITrainingSpeedYearIncreaseRate + BasicAITrainingSpeedRate;
            AIRecruitmentSpeedRate = year * AIRecruitmentSpeedYearIncreaseRate + BasicAIRecruitmentSpeedRate;
            AIOfficerExperienceRate = year * AIOfficerExperienceYearIncreaseRate + BasicAIOfficerExperienceRate;
            AIArmyExperienceRate = year * AIArmyExperienceYearIncreaseRate + BasicAIArmyExperienceRate;
            AIAntiSurround = (int) (year * AIAntiSurroundIncreaseRate + BasicAIAntiSurround);
            AIAntiStratagem = (int) (year * AIAntiStratagemIncreaseRate + BasicAIAntiStratagem);
            AIExtraPerson = year * AIExtraPersonIncreaseRate + BasicAIExtraPerson;
        }
    }
}

