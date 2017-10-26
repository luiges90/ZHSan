using System;
using System.Xml;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Platforms;
using GameManager;

namespace GameGlobal
{
    [DataContract]
    public class Parameters
    {
        [DataMember]
        public float AIArchitectureDamageRate = 1f;
        [DataMember]
        public float AIFoodRate = 1f;
        [DataMember]
        public float AIFundRate = 1f;
        [DataMember]
        public float AIRecruitmentSpeedRate = 1f;
        [DataMember]
        public float AITrainingSpeedRate = 1f;
        [DataMember]
        public float AITroopDefenceRate = 1f;
        [DataMember]
        public float AITroopOffenceRate = 1f;
        [DataMember]
        public float ArchitectureDamageRate = 1f;
        [DataMember]
        public int AIAntiStratagem = 0;
        [DataMember]
        public int AIAntiSurround = 0;
        [DataMember]
        public int BuyFoodAgriculture = 500;
        [DataMember]
        public int ChangeCapitalCost = 0x1388;
        [DataMember]
        public int ClearFieldAgricultureCostUnit = 3;
        [DataMember]
        public int ClearFieldFundCostUnit = 50;
        [DataMember]
        public int ConvincePersonCost = 200;
        [DataMember]
        public float DefaultPopulationDevelopingRate = 6E-05f;
        [DataMember]
        public int DestroyArchitectureCost = 200;
        [DataMember]
        public int FindTreasureChance = 10;
        [DataMember]
        public float FireDamageScale = 0.5f;
        [DataMember]
        public float FollowedLeaderDefenceRateIncrement = 0.2f;
        [DataMember]
        public float FollowedLeaderOffenceRateIncrement = 0.2f;
        [DataMember]
        public float FoodRate = 1f;
        [DataMember]
        public int FoodToFundDivisor = 200;
        [DataMember]
        public float FundRate = 1f;
        [DataMember]
        public int FundToFoodMultiple = 50;
        [DataMember]
        public int GossipArchitectureCost = 200;
        [DataMember]
        public int JailBreakArchitectureCost = 200;
        [DataMember]
        public int HireNoFactionPersonCost = 100;
        [DataMember]
        public int InstigateArchitectureCost = 200;
        [DataMember]
        public int InternalFundCost = 5;
        [DataMember]
        public float InternalRate = 1f;
        [DataMember]
        public int LearnSkillDays = 30;
        [DataMember]
        public int LearnStuntDays = 60;
        [DataMember]
        public int LearnTitleDays = 90;
        [DataMember]
        public int SearchDays = 10;
        [DataMember]
        public int RecruitmentDomination = 50;
        [DataMember]
        public int RecruitmentFundCost = 20;
        [DataMember]
        public int RecruitmentMorale = 100;
        [DataMember]
        public float RecruitmentRate = 1f;
        [DataMember]
        public int RewardPersonCost = 100;
        [DataMember]
        public int SellFoodCommerce = 500;
        [DataMember]
        public int SendSpyCost = 200;
        [DataMember]
        public int SurroundArchitectureDominationUnit = 2;
        [DataMember]
        public float TrainingRate = 1f;
        [DataMember]
        public float TroopDamageRate = 1f;
        [DataMember]
        public float AIArchitectureDamageYearIncreaseRate = 0f;
        [DataMember]
        public float AIFoodYearIncreaseRate = 0f;
        [DataMember]
        public float AIFundYearIncreaseRate = 0f;
        [DataMember]
        public float AIRecruitmentSpeedYearIncreaseRate = 0f;
        [DataMember]
        public float AITrainingSpeedYearIncreaseRate = 0f;
        [DataMember]
        public float AITroopDefenceYearIncreaseRate = 0f;
        [DataMember]
        public float AITroopOffenceYearIncreaseRate = 0f;
        [DataMember]
        public float AIArmyExperienceYearIncreaseRate = 0f;
        [DataMember]
        public float AIOfficerExperienceYearIncreaseRate = 0f;
        [DataMember]
        public float AIAntiStratagemIncreaseRate = 0f;
        [DataMember]
        public float AIAntiSurroundIncreaseRate = 0f;
        [DataMember]
        public float AIOfficerExperienceRate = 1f;
        [DataMember]
        public float AIArmyExperienceRate = 1f;

        [DataMember]
        private float BasicAIArchitectureDamageRate = 1f;
        [DataMember]
        private float BasicAIFoodRate = 1f;
        [DataMember]
        private float BasicAIFundRate = 1f;
        [DataMember]
        private float BasicAIRecruitmentSpeedRate = 1f;
        [DataMember]
        private float BasicAITrainingSpeedRate = 1f;
        [DataMember]
        private float BasicAITroopDefenceRate = 1f;
        [DataMember]
        private float BasicAITroopOffenceRate = 1f;
        [DataMember]
        private float BasicAIArmyExperienceRate = 1f;
        [DataMember]
        private float BasicAIOfficerExperienceRate = 1f;
        [DataMember]
        private int BasicAIAntiStratagem = 0;
        [DataMember]
        private int BasicAIAntiSurround = 0;

        [DataMember]
        public float AIBackendArmyReserveCalmBraveDifferenceMultiply = 5;
        [DataMember]
        public float AIBackendArmyReserveAmbitionMultiply = 10;
        [DataMember]
        public float AIBackendArmyReserveAdd = 50;
        [DataMember]
        public float AIBackendArmyReserveMultiply = 1;
        [DataMember]
        public int AITradePeriod = 10;
        [DataMember]
        public int AITreasureChance = 10;
        [DataMember]
        public int AITreasureCountMax = 2;
        [DataMember]
        public float AITreasureCountCappedTitleLevelAdd = 0;
        [DataMember]
        public float AITreasureCountCappedTitleLevelMultiply = 1;
        [DataMember]
        public int AIGiveTreasureMaxWorth = 40;
        [DataMember]
        public float AIFacilityFundMonthWaitParam = 8;
        [DataMember]
        public float AIFacilityDestroyValueRate = 2;
        [DataMember]
        public float AIBuildHougongUnambitionProbWeight = 10;
        [DataMember]
        public float AIBuildHougongSpaceBuiltProbWeight = 5;
        [DataMember]
        public int AIBuildHougongMaxSizeAdd = 0;
        [DataMember]
        public int AIBuildHougongSkipSizeChance = 80;
        [DataMember]
        public int AINafeiUncreultyProbAdd = -1;
        [DataMember]
        public float AINafeiAbilityThresholdRate = 30000;
        [DataMember]
        public float AINafeiStealSpouseThresholdRateAdd = 0.5f;
        [DataMember]
        public float AINafeiStealSpouseThresholdRateMultiply = 1;
        [DataMember]
        public int AINafeiMaxAgeThresholdAdd = 30;
        [DataMember]
        public float AINafeiMaxAgeThresholdMultiply = 1;
        [DataMember]
        public float AINafeiSkipChanceAdd = 25;
        [DataMember]
        public float AINafeiSkipChanceMultiply = 15;
        [DataMember]
        public float AIChongxingChanceAdd = 10;
        [DataMember]
        public float AIChongxingChanceMultiply = 20;
        [DataMember]
        public float AIRecruitPopulationCapMultiply = 90;
        [DataMember]
        public float AIRecruitPopulationCapBackendMultiply = 0.5f;
        [DataMember]
        public float AIRecruitPopulationCapHostilelineMultiply = 1.2f;
        [DataMember]
        public float AIRecruitPopulationCapStrategyTendencyMulitply = 0.2f;
        [DataMember]
        public float AIRecruitPopulationCapStrategyTendencyAdd = 0.2f;
        [DataMember]
        public int AINewMilitaryPopulationThresholdDivide = 30000;
        [DataMember]
        public int AINewMilitaryPersonThresholdDivide = 5;
        [DataMember]
        public int AIExecuteMaxUncreulty = 4;
        [DataMember]
        public float AIExecutePersonIdealToleranceMultiply = 15;
        [DataMember]
        public float AIHougongArchitectureCountProbMultiply = 10;
        [DataMember]
        public float AIHougongArchitectureCountProbPower = 0.5f;
        [DataMember]
        public int FireStayProb = 20;
        [DataMember]
        public float FireSpreadProbMultiply = 1f;
        [DataMember]
        public int MinPregnantProb = 0;
        [DataMember]
        public float InternalExperienceRate = 1f;
        [DataMember]
        public float AbilityExperienceRate = 1f;
        [DataMember]
        public float ArmyExperienceRate = 1f;
        [DataMember]
        public float AIAttackChanceIfUnfull = 5;
        [DataMember]
        public int AIObeyStrategyTendencyChance = 90;
        [DataMember]
        public int AIOffendMaxDiplomaticRelationMultiply = 20;
        [DataMember]
        public float AIOffendReserveAdd = 0.8f;
        [DataMember]
        public float AIOffendReserveBCDiffMultiply = 0.1f;
        [DataMember]
        public float AIOffendDefendingTroopRate = 0.75f;
        [DataMember]
        public float AIOffendDefendTroopAdd = 1.2f;
        [DataMember]
        public float AIOffendDefendTroopMultiply = 0.1f;
        [DataMember]
        public int AIOffendIgnoreReserveProbAmbitionMultiply = 5;
        [DataMember]
        public int AIOffendIgnoreReserveProbAmbitionAdd = -2;
        [DataMember]
        public int AIOffendIgnoreReserveProbBCDiffMultiply = 2;
        [DataMember]
        public int AIOffendIgnoreReserveProbBCDiffAdd = 10;
        [DataMember]
        public float AIOffendIgnoreReserveChanceTroopRatioAdd = -0.8f;
        [DataMember]
        public float AIOffendIgnoreReserveChanceTroopRatioMultiply = 100.0f;
        [DataMember]
        public int PrincessMaintainenceCost = 50;
        [DataMember]
        public int AIUniqueTroopFightingForceThreshold = 60000;
        [DataMember]
        public int LearnSkillSuccessRate = 0;
        [DataMember]
        public int LearnStuntSuccessRate = 75;
        [DataMember]
        public int LearnTitleSuccessRate = 0;
        [DataMember]
        public int AutoLearnSkillSuccessRate = 0;
        [DataMember]
        public int AutoLearnStuntSuccessRate = 0;
        [DataMember]
        public float MilitaryPopulationCap = 0.1f;
        [DataMember]
        public float MilitaryPopulationReloadQuantity = 1.0f;
        [DataMember]
        public int CloseThreshold = 500;
        [DataMember]
        public int HateThreshold = -500;
        [DataMember]
        public int VeryCloseThreshold = 2000;
        [DataMember]
        public int MaxAITroopCountCandidates = 1000;
        [DataMember]
        public float PopulationDevelopingRate = 1;
        [DataMember]
        public float CloseAbilityRate = 1.1F;
        [DataMember]
        public float VeryCloseAbilityRate = 1.2F;
        [DataMember]
        public int RetainFeiziPersonalLoyalty = 0;
        [DataMember]
        public int AIEncirclePlayerRate = 0;
        [DataMember]
        public float BasicAIExtraPerson = 0;
        [DataMember]
        public float AIExtraPerson = 0;
        [DataMember]
        public float AIExtraPersonIncreaseRate = 0;
        [DataMember]
        public int AITirednessDecrease = 0;
        [DataMember]
        public int InternalSurplusFactor = 10000000;
        [DataMember]
        public int MakeMarrigeIdealLimit = 5;
        [DataMember]
        public int MakeMarriageCost = 80000;
        [DataMember]
        public int NafeiCost = 50000;
        [DataMember]
        public int SelectPrinceCost = 50000;
        [DataMember]
        public int TransferCostPerMilitary = 2000;
        [DataMember]
        public int TransferFoodPerMilitary = 2000;
        [DataMember]
        public int AIEncircleRank = 0;
        [DataMember]
        public int AIEncircleVar = 0;
        [DataMember]
        public float RansomRate = 1.0f;
        [DataMember]
        public List<int> ExpandConditions = new List<int>();
        [DataMember]
        public float SearchPersonArchitectureCountPower = 0;
        [DataMember]
        public float InternalSurplusMinEffect = 0.2f;
        [DataMember]
        public int DayInTurn = 1;

        public Parameters Clone()
        {
            return this.MemberwiseClone() as Parameters;
        }

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
            InternalSurplusMinEffect = float.Parse(nextSibling.Attributes.GetNamedItem("InternalSurplusMinEffect").Value);

            DayInTurn = int.Parse(nextSibling.Attributes.GetNamedItem("DayInTurn").Value);

            InitBaseRates();
        }

        public void InitBaseRates()
        {
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

        public void DayEvent(int year)
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

