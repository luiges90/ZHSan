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
        public float AIRecruitPopulationCapHostilelineMultiply = 2.0f;
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
        public int MakeMarriageCost = 8000;
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
        public int DayInTurn = 1;
        [DataMember]
        public int MaxRelation = 10000;
        [DataMember]
        public float HougongRelationHateFactor = 3.0f;
        [DataMember]
        public int AIMaxFeizi = 3000;
        [DataMember]
        public int MaxReputationForRecruit = 3000000;
        [DataMember]
        public float TroopMoraleChange = 1.0f;
        [DataMember]
        public float RecruitPopualationDecreaseRate = 0.25f;
        [DataMember]
        public float AIOffensiveCampaignRequiredScaleFactor = 1.0f;

        public Parameters Clone()
        {
            return this.MemberwiseClone() as Parameters;
        }

        public void InitializeGameParameters(string str)
        {
            XmlDocument document = new XmlDocument();

            string xml = Platform.Current.LoadText("Content/Data/GameParameters.xml");
            document.LoadXml(xml);

            XmlNode nextSibling = document.FirstChild.NextSibling;
            if (str == "")
            {
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
                ConvincePersonCost = int.Parse(nextSibling.Attributes.GetNamedItem("ConvincePersonCost").Value);
                RewardPersonCost = int.Parse(nextSibling.Attributes.GetNamedItem("RewardPersonCost").Value);
                DestroyArchitectureCost = int.Parse(nextSibling.Attributes.GetNamedItem("DestroyArchitectureCost").Value);
                InstigateArchitectureCost = int.Parse(nextSibling.Attributes.GetNamedItem("InstigateArchitectureCost").Value);
                GossipArchitectureCost = int.Parse(nextSibling.Attributes.GetNamedItem("GossipArchitectureCost").Value);
                JailBreakArchitectureCost = int.Parse(nextSibling.Attributes.GetNamedItem("JailBreakArchitectureCost").Value);
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

                AIEncirclePlayerRate = int.Parse(nextSibling.Attributes.GetNamedItem("AIEncirclePlayerRate").Value);

                InternalSurplusFactor = int.Parse(nextSibling.Attributes.GetNamedItem("InternalSurplusFactor").Value);
                AIExtraPerson = float.Parse(nextSibling.Attributes.GetNamedItem("AIExtraPerson").Value);
                AIExtraPersonIncreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("AIExtraPersonIncreaseRate").Value);

                StaticMethods.LoadFromString(ExpandConditions, nextSibling.Attributes.GetNamedItem("ExpandConditions").Value);

                SearchPersonArchitectureCountPower = float.Parse(nextSibling.Attributes.GetNamedItem("SearchPersonArchitectureCountPower").Value);
                AIEncircleRank = int.Parse(nextSibling.Attributes.GetNamedItem("AIEncircleRank").Value);
                AIEncircleVar = int.Parse(nextSibling.Attributes.GetNamedItem("AIEncircleVar").Value);

                RansomRate = float.Parse(nextSibling.Attributes.GetNamedItem("RansomRate").Value);

                DayInTurn = int.Parse(nextSibling.Attributes.GetNamedItem("DayInTurn").Value);
                MaxRelation = int.Parse(nextSibling.Attributes.GetNamedItem("MaxRelation").Value);
                AIMaxFeizi = int.Parse(nextSibling.Attributes.GetNamedItem("AIMaxFeizi").Value);

                HougongRelationHateFactor = float.Parse(nextSibling.Attributes.GetNamedItem("HougongRelationHateFactor").Value);
                MaxReputationForRecruit = int.Parse(nextSibling.Attributes.GetNamedItem("MaxReputationForRecruit").Value);

                TroopMoraleChange = float.Parse(nextSibling.Attributes.GetNamedItem("TroopMoraleChange").Value);
                RecruitPopualationDecreaseRate = float.Parse(nextSibling.Attributes.GetNamedItem("RecruitPopualationDecreaseRate").Value);

                MakeMarriageCost = int.Parse(nextSibling.Attributes.GetNamedItem("MakeMarriageCost").Value);
                NafeiCost = int.Parse(nextSibling.Attributes.GetNamedItem("NafeiCost").Value);
                MakeMarrigeIdealLimit = int.Parse(nextSibling.Attributes.GetNamedItem("MakeMarrigeIdealLimit").Value);
            }

            else
            {
                document = new XmlDocument();
                xml = Platform.Current.LoadText(str);
                document.LoadXml(xml);
                nextSibling = document.FirstChild.NextSibling;
                for (int i = 0; i < nextSibling.Attributes.Count; i++)
                {
                    System.Reflection.FieldInfo[] 非getset字段表 = typeof(Parameters).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == nextSibling.Attributes[i].Name)
                        {
                            if (v.Name == "ExpandConditions")
                            {
                                List<int> listii = new List<int>();
                                GameGlobal.StaticMethods.LoadFromString(listii, nextSibling.Attributes[i].Value.ToString());
                                v.SetValue(Session.Current.Scenario.Parameters, listii);
                            }
                            if (v.FieldType.ToString() == "System.Int32")
                            {
                                string temp = nextSibling.Attributes[i].Value.ToString();
                                if (temp.Contains("E"))
                                {
                                    Decimal de;
                                    Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                    v.SetValue(Session.Current.Scenario.Parameters, (int)de);
                                }
                                else
                                {
                                    v.SetValue(Session.Current.Scenario.Parameters, int.Parse(nextSibling.Attributes[i].Value.ToString()));
                                }
                            }
                            else if (v.FieldType.ToString() == "System.Single")
                            {
                                string temp = nextSibling.Attributes[i].Value.ToString();
                                if (temp.Contains("E"))
                                {
                                    Decimal de;
                                    Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                    v.SetValue(Session.Current.Scenario.Parameters, (float)de);
                                }
                                else
                                {
                                    v.SetValue(Session.Current.Scenario.Parameters, float.Parse(nextSibling.Attributes[i].Value.ToString()));
                                }
                            }
                            else if (v.FieldType.ToString() == "System.Boolean")
                            {
                                v.SetValue(Session.Current.Scenario.Parameters, bool.Parse(nextSibling.Attributes[i].Value.ToString()));
                            }
                            else if (v.FieldType.ToString() == "System.String")
                            {
                                v.SetValue(Session.Current.Scenario.Parameters, nextSibling.Attributes[i].Value.ToString());
                            }
                        }
                    }
                }
            }
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

        public void SaveToXml()
        {
            XmlDocument document = new XmlDocument();

            XmlNode docNode = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(docNode);

            XmlElement element = document.CreateElement("GameParameters");
            element.SetAttribute("FindTreasureChance", FindTreasureChance.ToString());
            element.SetAttribute("LearnSkillDays", LearnSkillDays.ToString());
            element.SetAttribute("LearnStuntDays", LearnStuntDays.ToString());
            element.SetAttribute("LearnTitleDays", LearnTitleDays.ToString());
            element.SetAttribute("SearchDays", SearchDays.ToString());
            element.SetAttribute("LearnSkillSuccessRate", LearnSkillSuccessRate.ToString());
            element.SetAttribute("LearnStuntSuccessRate", LearnStuntSuccessRate.ToString());
            element.SetAttribute("LearnTitleSuccessRate", LearnTitleSuccessRate.ToString());
            element.SetAttribute("FollowedLeaderOffenceRateIncrement", FollowedLeaderOffenceRateIncrement.ToString());
            element.SetAttribute("FollowedLeaderDefenceRateIncrement", FollowedLeaderDefenceRateIncrement.ToString());
            element.SetAttribute("InternalRate", InternalRate.ToString());
            element.SetAttribute("TrainingRate", TrainingRate.ToString());
            element.SetAttribute("RecruitmentRate", RecruitmentRate.ToString());
            element.SetAttribute("FundRate", FundRate.ToString());
            element.SetAttribute("FoodRate", FoodRate.ToString());
            element.SetAttribute("TroopDamageRate", TroopDamageRate.ToString());
            element.SetAttribute("ArchitectureDamageRate", ArchitectureDamageRate.ToString());
            element.SetAttribute("DefaultPopulationDevelopingRate", DefaultPopulationDevelopingRate.ToString());
            element.SetAttribute("BuyFoodAgriculture", BuyFoodAgriculture.ToString());
            element.SetAttribute("SellFoodCommerce", SellFoodCommerce.ToString());
            element.SetAttribute("FundToFoodMultiple", FundToFoodMultiple.ToString());
            element.SetAttribute("FoodToFundDivisor", FoodToFundDivisor.ToString());
            element.SetAttribute("InternalFundCost", InternalFundCost.ToString());
            element.SetAttribute("RecruitmentFundCost", RecruitmentFundCost.ToString());
            element.SetAttribute("RecruitmentDomination", RecruitmentDomination.ToString());
            element.SetAttribute("RecruitmentMorale", RecruitmentMorale.ToString());
            element.SetAttribute("ChangeCapitalCost", ChangeCapitalCost.ToString());
            element.SetAttribute("ConvincePersonCost", ConvincePersonCost.ToString());
            element.SetAttribute("RewardPersonCost", RewardPersonCost.ToString());
            element.SetAttribute("DestroyArchitectureCost", DestroyArchitectureCost.ToString());
            element.SetAttribute("InstigateArchitectureCost", InstigateArchitectureCost.ToString());
            element.SetAttribute("GossipArchitectureCost", GossipArchitectureCost.ToString());
            element.SetAttribute("JailBreakArchitectureCost", JailBreakArchitectureCost.ToString());
            element.SetAttribute("SurroundArchitectureDominationUnit", SurroundArchitectureDominationUnit.ToString());
            element.SetAttribute("FireDamageScale", FireDamageScale.ToString());
            element.SetAttribute("AIFundRate", AIFundRate.ToString());
            element.SetAttribute("AIFoodRate", AIFoodRate.ToString());
            element.SetAttribute("AITroopOffenceRate", AITroopOffenceRate.ToString());
            element.SetAttribute("AITroopDefenceRate", AITroopDefenceRate.ToString());
            element.SetAttribute("AIArchitectureDamageRate", AIArchitectureDamageRate.ToString());
            element.SetAttribute("AITrainingSpeedRate", AITrainingSpeedRate.ToString());
            element.SetAttribute("AIRecruitmentSpeedRate", AIRecruitmentSpeedRate.ToString());
            element.SetAttribute("AIFundYearIncreaseRate", AIFundYearIncreaseRate.ToString());
            element.SetAttribute("AIFoodYearIncreaseRate", AIFoodYearIncreaseRate.ToString());
            element.SetAttribute("AITroopOffenceYearIncreaseRate", AITroopOffenceYearIncreaseRate.ToString());
            element.SetAttribute("AITroopDefenceYearIncreaseRate", AITroopDefenceYearIncreaseRate.ToString());
            element.SetAttribute("AIArchitectureDamageYearIncreaseRate", AIArchitectureDamageYearIncreaseRate.ToString());
            element.SetAttribute("AITrainingSpeedYearIncreaseRate", AITrainingSpeedYearIncreaseRate.ToString());
            element.SetAttribute("AIRecruitmentSpeedYearIncreaseRate", AIRecruitmentSpeedYearIncreaseRate.ToString());
            element.SetAttribute("AIArmyExperienceYearIncreaseRate", AIArmyExperienceYearIncreaseRate.ToString());
            element.SetAttribute("AIOfficerExperienceYearIncreaseRate", AIOfficerExperienceYearIncreaseRate.ToString());
            element.SetAttribute("AIOfficerExperienceRate", AIOfficerExperienceRate.ToString());
            element.SetAttribute("AIArmyExperienceRate", AIArmyExperienceRate.ToString());
            element.SetAttribute("AIBackendArmyReserveCalmBraveDifferenceMultiply", AIBackendArmyReserveCalmBraveDifferenceMultiply.ToString());
            element.SetAttribute("AIBackendArmyReserveAmbitionMultiply", AIBackendArmyReserveAmbitionMultiply.ToString());
            element.SetAttribute("AIBackendArmyReserveAdd", AIBackendArmyReserveAdd.ToString());
            element.SetAttribute("AIBackendArmyReserveMultiply", AIBackendArmyReserveMultiply.ToString());
            element.SetAttribute("AITradePeriod", AITradePeriod.ToString());
            element.SetAttribute("AITreasureChance", AITreasureChance.ToString());
            element.SetAttribute("AITreasureCountMax", AITreasureCountMax.ToString());
            element.SetAttribute("AITreasureCountCappedTitleLevelAdd", AITreasureCountCappedTitleLevelAdd.ToString());
            element.SetAttribute("AITreasureCountCappedTitleLevelMultiply", AITreasureCountCappedTitleLevelMultiply.ToString());
            element.SetAttribute("AIGiveTreasureMaxWorth", AIGiveTreasureMaxWorth.ToString());
            element.SetAttribute("AIFacilityFundMonthWaitParam", AIFacilityFundMonthWaitParam.ToString());
            element.SetAttribute("AIFacilityDestroyValueRate", AIFacilityDestroyValueRate.ToString());
            element.SetAttribute("AIBuildHougongUnambitionProbWeight", AIBuildHougongUnambitionProbWeight.ToString());
            element.SetAttribute("AIBuildHougongSpaceBuiltProbWeight", AIBuildHougongSpaceBuiltProbWeight.ToString());
            element.SetAttribute("AIBuildHougongMaxSizeAdd", AIBuildHougongMaxSizeAdd.ToString());
            element.SetAttribute("AIBuildHougongSkipSizeChance", AIBuildHougongSkipSizeChance.ToString());
            element.SetAttribute("AINafeiUncreultyProbAdd", AINafeiUncreultyProbAdd.ToString());
            element.SetAttribute("AIHougongArchitectureCountProbMultiply", AIHougongArchitectureCountProbMultiply.ToString());
            element.SetAttribute("AIHougongArchitectureCountProbPower", AIHougongArchitectureCountProbPower.ToString());
            element.SetAttribute("AINafeiAbilityThresholdRate", AINafeiAbilityThresholdRate.ToString());
            element.SetAttribute("AINafeiStealSpouseThresholdRateAdd", AINafeiStealSpouseThresholdRateAdd.ToString());
            element.SetAttribute("AINafeiStealSpouseThresholdRateMultiply", AINafeiStealSpouseThresholdRateMultiply.ToString());
            element.SetAttribute("AINafeiMaxAgeThresholdAdd", AINafeiMaxAgeThresholdAdd.ToString());
            element.SetAttribute("AINafeiMaxAgeThresholdMultiply", AINafeiMaxAgeThresholdMultiply.ToString());
            element.SetAttribute("AINafeiSkipChanceAdd", AINafeiSkipChanceAdd.ToString());
            element.SetAttribute("AINafeiSkipChanceMultiply", AINafeiSkipChanceMultiply.ToString());
            element.SetAttribute("AIChongxingChanceAdd", AIChongxingChanceAdd.ToString());
            element.SetAttribute("AIChongxingChanceMultiply", AIChongxingChanceMultiply.ToString());
            element.SetAttribute("AIRecruitPopulationCapMultiply", AIRecruitPopulationCapMultiply.ToString());
            element.SetAttribute("AIRecruitPopulationCapBackendMultiply", AIRecruitPopulationCapBackendMultiply.ToString());
            element.SetAttribute("AIRecruitPopulationCapHostilelineMultiply", AIRecruitPopulationCapHostilelineMultiply.ToString());
            element.SetAttribute("AIRecruitPopulationCapStrategyTendencyMulitply", AIRecruitPopulationCapStrategyTendencyMulitply.ToString());
            element.SetAttribute("AIRecruitPopulationCapStrategyTendencyAdd", AIRecruitPopulationCapStrategyTendencyAdd.ToString());
            element.SetAttribute("AINewMilitaryPopulationThresholdDivide", AINewMilitaryPopulationThresholdDivide.ToString());
            element.SetAttribute("AINewMilitaryPersonThresholdDivide", AINewMilitaryPersonThresholdDivide.ToString());
            element.SetAttribute("AIExecuteMaxUncreulty", AIExecuteMaxUncreulty.ToString());
            element.SetAttribute("AIExecutePersonIdealToleranceMultiply", AIExecutePersonIdealToleranceMultiply.ToString());
            element.SetAttribute("FireStayProb", FireStayProb.ToString());
            element.SetAttribute("FireSpreadProbMultiply", FireSpreadProbMultiply.ToString());
            element.SetAttribute("MinPregnantProb", MinPregnantProb.ToString());
            element.SetAttribute("InternalExperienceRate", InternalExperienceRate.ToString());
            element.SetAttribute("AbilityExperienceRate", AbilityExperienceRate.ToString());
            element.SetAttribute("ArmyExperienceRate", ArmyExperienceRate.ToString());
            element.SetAttribute("AIAttackChanceIfUnfull", AIAttackChanceIfUnfull.ToString());
            element.SetAttribute("AIObeyStrategyTendencyChance", AIObeyStrategyTendencyChance.ToString());
            element.SetAttribute("AIOffendMaxDiplomaticRelationMultiply", AIOffendMaxDiplomaticRelationMultiply.ToString());
            element.SetAttribute("AIOffendDefendTroopAdd", AIOffendDefendTroopAdd.ToString());
            element.SetAttribute("AIOffendDefendTroopMultiply", AIOffendDefendTroopMultiply.ToString());
            element.SetAttribute("AIOffendIgnoreReserveProbAmbitionMultiply", AIOffendIgnoreReserveProbAmbitionMultiply.ToString());
            element.SetAttribute("AIOffendIgnoreReserveProbAmbitionAdd", AIOffendIgnoreReserveProbAmbitionAdd.ToString());
            element.SetAttribute("AIOffendIgnoreReserveProbBCDiffMultiply", AIOffendIgnoreReserveProbBCDiffMultiply.ToString());
            element.SetAttribute("AIOffendIgnoreReserveProbBCDiffAdd", AIOffendIgnoreReserveProbBCDiffAdd.ToString());
            element.SetAttribute("AIOffendIgnoreReserveChanceTroopRatioAdd", AIOffendIgnoreReserveChanceTroopRatioAdd.ToString());
            element.SetAttribute("AIOffendIgnoreReserveChanceTroopRatioMultiply", AIOffendIgnoreReserveChanceTroopRatioMultiply.ToString());
            element.SetAttribute("PrincessMaintainenceCost", PrincessMaintainenceCost.ToString());
            element.SetAttribute("AIUniqueTroopFightingForceThreshold", AIUniqueTroopFightingForceThreshold.ToString());
            element.SetAttribute("MilitaryPopulationCap", MilitaryPopulationCap.ToString());
            element.SetAttribute("MilitaryPopulationReloadQuantity", MilitaryPopulationReloadQuantity.ToString());
            element.SetAttribute("CloseThreshold", CloseThreshold.ToString());
            element.SetAttribute("HateThreshold", HateThreshold.ToString());
            element.SetAttribute("VeryCloseThreshold", VeryCloseThreshold.ToString());
            element.SetAttribute("MaxAITroopCountCandidates", MaxAITroopCountCandidates.ToString());
            element.SetAttribute("PopulationDevelopingRate", PopulationDevelopingRate.ToString());
            element.SetAttribute("AIAntiStratagem", AIAntiStratagem.ToString());
            element.SetAttribute("AIAntiSurround", AIAntiSurround.ToString());
            element.SetAttribute("AIAntiSurroundIncreaseRate", AIAntiSurroundIncreaseRate.ToString());
            element.SetAttribute("AIAntiStratagemIncreaseRate", AIAntiStratagemIncreaseRate.ToString());
            element.SetAttribute("CloseAbilityRate", CloseAbilityRate.ToString());
            element.SetAttribute("VeryCloseAbilityRate", VeryCloseAbilityRate.ToString());
            element.SetAttribute("AIEncirclePlayerRate", AIEncirclePlayerRate.ToString());
            element.SetAttribute("InternalSurplusFactor", InternalSurplusFactor.ToString());
            element.SetAttribute("AIExtraPerson", AIExtraPerson.ToString());
            element.SetAttribute("AIExtraPersonIncreaseRate", AIExtraPersonIncreaseRate.ToString());
            element.SetAttribute("ExpandConditions", StaticMethods.SaveToString(ExpandConditions));
            element.SetAttribute("SearchPersonArchitectureCountPower", SearchPersonArchitectureCountPower.ToString());
            element.SetAttribute("AIEncircleRank", AIEncircleRank.ToString());
            element.SetAttribute("AIEncircleVar", AIEncircleVar.ToString());
            element.SetAttribute("SelectPrinceCost", SelectPrinceCost.ToString());
            element.SetAttribute("TransferCostPerMilitary", TransferCostPerMilitary.ToString());
            element.SetAttribute("TransferFoodPerMilitary", TransferFoodPerMilitary.ToString());
            element.SetAttribute("AutoLearnSkillSuccessRate", AutoLearnSkillSuccessRate.ToString());
            element.SetAttribute("AutoLearnStuntSuccessRate", AutoLearnStuntSuccessRate.ToString());
            element.SetAttribute("RansomRate", RansomRate.ToString());
            element.SetAttribute("DayInTurn", DayInTurn.ToString());
            element.SetAttribute("MaxRelation", MaxRelation.ToString());
            element.SetAttribute("HougongRelationHateFactor", HougongRelationHateFactor.ToString());
            element.SetAttribute("AIMaxFeizi", AIMaxFeizi.ToString());
            element.SetAttribute("MaxReputationForRecruit", MaxReputationForRecruit.ToString());
            element.SetAttribute("TroopMoraleChange", TroopMoraleChange.ToString());
            element.SetAttribute("RecruitPopualationDecreaseRate", RecruitPopualationDecreaseRate.ToString());
            element.SetAttribute("MakeMarriageCost", MakeMarriageCost.ToString());
            element.SetAttribute("NafeiCost", NafeiCost.ToString());
            element.SetAttribute("MakeMarrigeIdealLimit", MakeMarrigeIdealLimit.ToString());

            document.AppendChild(element);

            Platform.Current.SaveUserFile("Content/Data/GameParameters.xml", document.OuterXml, true);
        }

        public void MigrateData()
        {
            if (MaxRelation == 0)
            {
                MaxRelation = 10000;
            }
            if (MaxReputationForRecruit == 0)
            {
                MaxReputationForRecruit = 3000000;
            }
            if (TroopMoraleChange == 0)
            {
                TroopMoraleChange = 1;
            }
            if (Session.Parameters.AIOffensiveCampaignRequiredScaleFactor == 0)
            {
                Session.Parameters.AIOffensiveCampaignRequiredScaleFactor = 1.0f;
            }
        }
    }
}

