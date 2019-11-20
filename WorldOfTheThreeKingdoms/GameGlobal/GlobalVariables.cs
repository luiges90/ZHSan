using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Platforms;
using GameManager;

namespace GameGlobal
{
    [DataContract]
    public class GlobalVariables
    {
        [DataMember]
        public bool WujiangYoukenengDuli = true;
        [DataMember]
        public bool LiangdaoXitong = false;
        [DataMember]
        public bool ShowGrid = false;
        [DataMember]
        public bool AdditionalPersonAvailable = false;
        [DataMember]
        public float ArchitectureLayerDepth = 0.8f;
        [DataMember]
        public const float BackgroundDepthOffset = -1E-05f;
        [DataMember]
        public const float BackTileAnimationLayerDepth = 0.75f;
        [DataMember]
        public bool CalculateAverageCostOfTiers = false;
        [DataMember]
        public bool CommonPersonAvailable = true;
        [DataMember]
        public const float ConmentTextDepth = 0.15f;
        [DataMember]
        public const float ContextMenuDepth = 0.1f;
        [DataMember]
        public const float ControlDepthOffset = -0.001f;
        [DataMember]
        public MapLayerKind CurrentMapLayer;
        [DataMember]
        public const float DialogDepth = 0.2f;
        [DataMember]
        public bool DrawMapVeil = true;
        [DataMember]
        public bool DrawTroopAnimation = true;
        [DataMember]
        public long FactionRunningTicksLimitInOneFrame = 0x186a0;
        [DataMember]
        public int FastBattleSpeed = 1;
        [DataMember]
        public const float FloatingPartDepth = 0.25f;
        [DataMember]
        public const float FrameContentDepth = 0.35f;
        [DataMember]
        public const float FrontTileAnimationLayerDepth = 0.65f;
        [DataMember]
        public string GameDifficulty;
        [DataMember]
        public const float GameFrameDepth = 0.4f;
        [DataMember]
        public bool HintPopulation = true;
        [DataMember]
        public bool HintPopulationUnder1000 = true;
        [DataMember]
        public bool IdealTendencyValid = true;
        [DataMember]
        public const float LayerDepthOffset = -0.01f;
        [DataMember]
        public bool LoadBackGroundMapTexture = false;
        [DataMember]
        public const float MapLayerDepth = 0.9f;
        [DataMember]
        public float MapScrollSpeed = 0.8f;
        [DataMember]
        public const float MapVeilLayerDepth = 0.6f;
        [DataMember]
        public const float MapViewSelectorDepth = 0.18f;
        [DataMember]
        public int MaxCountOfKnownPaths = 0x3e8;
        [DataMember]
        public const float MaxDepth = 1f;
        [DataMember]
        public int MaxTimeOfAnimationFrame = 0x19;
        [DataMember]
        public bool MilitaryKindSpeedValid = true;
        [DataMember]
        public const float MinDepth = 0f;
        [DataMember]
        public const float MinDepthOffset = -1E-06f;
        [DataMember]
        public const float MovableControlDepthOffset = -0.0002f;
        [DataMember]
        public bool MultipleResource = false;
        [DataMember]
        public bool NoHintOnSmallFacility = true;
        [DataMember]
        public const float PersonBubbleDepth = 0.45f;
        [DataMember]
        public bool? PersonNaturalDeath = true;
        [DataMember]
        public bool PlayBattleSound = true;
        [DataMember]
        public bool PlayerPersonAvailable = true;
        [DataMember]
        public bool PlayMusic = true;
        [DataMember]
        public bool PlayNormalSound = true;
        [DataMember]
        public bool PopulationRecruitmentLimit = true;
        [DataMember]
        public InformationLevel RoutewayInformationLevel = InformationLevel.低;
        [DataMember]
        public const float RoutewayLayerDepth = 0.85f;
        [DataMember]
        public bool RunWhileNotFocused = true;
        [DataMember]
        public InformationLevel ScoutRoutewayInformationLevel = InformationLevel.高;
        [DataMember]
        public const float ScreenBlindDepth = 0.43f;
        [DataMember]
        public const float SelectingLayerDepth = 0.5f;
        [DataMember]
        public const float SelectorDepth = 0.72f;
        [DataMember]
        public bool SingleSelectionOneClick = true;
        [DataMember]
        public bool SkyEye = false;
        [DataMember]
        public const float SurveyDepth = 0.05f;
        [DataMember]
        public const float TextDepthOffset = -0.0001f;
        [DataMember]
        public const float ToolBarDepth = 0.1f;
        [DataMember]
        public const float TroopLayerDepth = 0.7f;
        [DataMember]
        public int TroopMoveFrameCount = 10;
        [DataMember]
        public int TroopMoveLimitOnce = 5;
        [DataMember]
        public int TroopMoveSpeed = 1;
        [DataMember]
        public const float TroopTitleDepth = 0.47f;
        [DataMember]
        public bool PinPointAtPlayer = false;
        [DataMember]
        public bool IgnoreStrategyTendency = false;
        [DataMember]
        public bool createChildren = true;
        [DataMember]
        public int zainanfashengjilv = 3000;
        [DataMember]
        public bool doAutoSave = true;
        [DataMember]
        public bool createChildrenIgnoreLimit = true;
        [DataMember]
        public bool internalSurplusRateForPlayer = true;
        [DataMember]
        public bool internalSurplusRateForAI = false;
        [DataMember]
        public int getChildrenRate = 90;
        [DataMember]
        public int hougongGetChildrenRate = 90;
        [DataMember]
        public bool hougongAlienOnly = false;
        [DataMember]
        public int getRaisedSoliderRate = 90;
        [DataMember]
        public int AIExecutionRate = 500;
        [DataMember]
        public bool AIExecuteBetterOfficer = false;
        [DataMember]
        public int maxExperience = 10000;
        [DataMember]
        public bool lockChildrenLoyalty = true;
        [DataMember]
        public bool AIAutoTakeNoFactionCaptives = false;
        [DataMember]
        public bool AIAutoTakeNoFactionPerson = false;
        [DataMember]
        public bool AIAutoTakePlayerCaptives = false;
        [DataMember]
        public bool AIAutoTakePlayerCaptiveOnlyUnfull = false;
        [DataMember]
        public float TechniquePointMultiple = 1.0f;
        [DataMember]
        public bool PermitFactionMerge = true;
        [DataMember]
        public float LeadershipOffenceRate = 0.0f;
        [DataMember]
        public int DialogShowTime = 10;
        [DataMember]
        public bool LandArmyCanGoDownWater = true;
        [DataMember]
        public bool EnableResposiveThreading = false;
        [DataMember]
        public bool EnableCheat = false;
        [DataMember]
        public bool HardcoreMode = false;
        [DataMember]
        public int MaxAbility = 150;
        [DataMember]
        public int TirednessIncrease = 1;
        [DataMember]
        public int TirednessDecrease = 1;
        [DataMember]
        public bool EnableAgeAbilityFactor = true;
        [DataMember]
        public int TabListDetailLevel = 3;
        [DataMember]
        public bool EnableExtensions = false;
        [DataMember]
        public bool EncryptSave = false;
        [DataMember]
        public int AutoSaveFrequency = 30;
        [DataMember]
        public bool ShowChallengeAnimation = true;
        [DataMember]
        public bool PersonDieInChallenge = true;
        [DataMember]
        public int OfficerDieInBattleRate = 10;
        [DataMember]
        public int OfficerChildrenLimit = 20;
        [DataMember]
        public bool StopToControlOnAttack = true;
        [DataMember]
        public int MaxMilitaryExperience = 3000;
        [DataMember]
        public int FactionMilitaryLimt = 9000;
        [DataMember]
        public float ZhaoXianSuccessRate = 30;
        [DataMember]
        public int TroopTirednessDecrease = 10;
        [DataMember]
        public float CreateRandomOfficerChance = 5;
        [DataMember]
        public int ChildrenAvailableAge = 12;
        [DataMember]
        public float CreatedOfficerAbilityFactor = 0.8f;
        [DataMember]
        public float ChildrenAbilityFactor = 1.0f;
        [DataMember]
        public bool EnablePersonRelations = true;
        [DataMember]
        public int FriendlyDiplomacyThreshold = 300;
        [DataMember]
        public int SurroundFactor = 5;
        [DataMember]
        public bool FullScreen = false;
        [DataMember]
        public bool PermitQuanXiang = true;
        [DataMember]
        public bool PermitManualAwardTitleAutoLearn = false;
        [DataMember]
        public int zhaoxianOfficerMax = 500;
        [DataMember]
        public bool AIZhaoxianFixIdeal = false;
        [DataMember]
        public bool PlayerZhaoxianFixIdeal = false;
        [DataMember]
        public int FixedUnnaturalDeathAge = 80;
        [DataMember]
        public bool AIQuickBattle = false;
        [DataMember]
        public bool PlayerAutoSectionHasAIResourceBonus = false;
        [DataMember]
        public float ProhibitFactionAgainstDestroyer = 1.0f;
        [DataMember]
        public float AIMergeAgainstPlayer = -1f;
        [DataMember]
        public bool RemoveSpouseIfNotAvailable = false;
        [DataMember]
        public bool SkyEyeSimpleNotification = false;
        [DataMember]
        public bool AutoMultipleMarriage = false;
        [DataMember]
        public bool BornHistoricalChildren = false;
        [DataMember]
        public float StartCircleTime = 30f;
        [DataMember]
        public float ScenarioMapPerTime = 3f;
        [DataMember]
        public int KeepSpousePersonalLoyalty = 4;
        [DataMember]
        public bool TroopVoice = true;

        public const string cryptKey = "A3g0c3%2";
        [DataMember]
        public int ShowNumberAddTime = 0;

        public GlobalVariables Clone()
        {
            return this.MemberwiseClone() as GlobalVariables;
        }

        public List<String> getFieldsExcludedFromSave()
        {
            List<String> s = new List<string>();
            s.Add("MapScrollSpeed");
            s.Add("TroopMoveSpeed");
            s.Add("RunWhileNotFocused");
            s.Add("PlayMusic");
            s.Add("PlayNormalSound");
            s.Add("PlayBattleSound");
            s.Add("DrawMapVeil");
            s.Add("DrawTroopAnimation");
            s.Add("SingleSelectionOneClick");
            s.Add("NoHintOnSmallFacility");
            s.Add("HintPopulation");
            s.Add("HintPopulationUnder1000");
            s.Add("doAutoSave");
            s.Add("DialogShowTime");
            s.Add("FastBattleSpeed");
            s.Add("AutoSaveFrequency");
            s.Add("StopToControlOnAttack");

            return s;
        }

        public bool InitialGlobalVariables(string str)
        {
            Exception exception;

            XmlDocument document = new XmlDocument();

            string xml = Platform.Current.LoadText("Content/Data/GlobalVariables.xml");

            document.LoadXml(xml);

            XmlNode nextSibling = document.FirstChild.NextSibling;
            if (str == "")
            {
                try
                {
                    GameDifficulty = nextSibling.Attributes.GetNamedItem("GameDifficulty").Value;
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    throw new Exception("GameDifficulty:\n" + exception.ToString());
                }
                try
                {
                    MapScrollSpeed = float.Parse(nextSibling.Attributes.GetNamedItem("MapScrollSpeed").Value);
                }
                catch (Exception exception1)
                {
                    exception = exception1;
                    throw new Exception("MapScrollSpeed:\n" + exception.ToString());
                }
                try
                {
                    TroopMoveSpeed = int.Parse(nextSibling.Attributes.GetNamedItem("TroopMoveSpeed").Value);
                }
                catch (Exception exception2)
                {
                    exception = exception2;
                    throw new Exception("TroopMoveSpeed:\n" + exception.ToString());
                }
                try
                {
                    RunWhileNotFocused = bool.Parse(nextSibling.Attributes.GetNamedItem("RunWhileNotFocused").Value);
                }
                catch (Exception exception3)
                {
                    exception = exception3;
                    throw new Exception("RunWhileNotFocused:\n" + exception.ToString());
                }
                try
                {
                    PlayMusic = bool.Parse(nextSibling.Attributes.GetNamedItem("PlayMusic").Value);
                }
                catch (Exception exception4)
                {
                    exception = exception4;
                    throw new Exception("PlayMusic:\n" + exception.ToString());
                }
                try
                {
                    PlayNormalSound = bool.Parse(nextSibling.Attributes.GetNamedItem("PlayNormalSound").Value);
                }
                catch (Exception exception5)
                {
                    exception = exception5;
                    throw new Exception("PlayNormalSound:\n" + exception.ToString());
                }
                try
                {
                    PlayBattleSound = bool.Parse(nextSibling.Attributes.GetNamedItem("PlayBattleSound").Value);
                }
                catch (Exception exception6)
                {
                    exception = exception6;
                    throw new Exception("PlayBattleSound:\n" + exception.ToString());
                }
                try
                {
                    DrawMapVeil = bool.Parse(nextSibling.Attributes.GetNamedItem("DrawMapVeil").Value);
                }
                catch (Exception exception7)
                {
                    exception = exception7;
                    throw new Exception("DrawMapVeil:\n" + exception.ToString());
                }
                try
                {
                    DrawTroopAnimation = bool.Parse(nextSibling.Attributes.GetNamedItem("DrawTroopAnimation").Value);
                }
                catch (Exception exception8)
                {
                    exception = exception8;
                    throw new Exception("DrawTroopAnimation:\n" + exception.ToString());
                }
                try
                {
                    SkyEye = bool.Parse(nextSibling.Attributes.GetNamedItem("SkyEye").Value);
                }
                catch (Exception exception9)
                {
                    exception = exception9;
                    throw new Exception("SkyEye:\n" + exception.ToString());
                }
                try
                {
                    MultipleResource = bool.Parse(nextSibling.Attributes.GetNamedItem("MultipleResource").Value);
                }
                catch (Exception exception10)
                {
                    exception = exception10;
                    throw new Exception("MultipleResource:\n" + exception.ToString());
                }
                try
                {
                    SingleSelectionOneClick = bool.Parse(nextSibling.Attributes.GetNamedItem("SingleSelectionOneClick").Value);
                }
                catch (Exception exception11)
                {
                    exception = exception11;
                    throw new Exception("SingleSelectionOneClick:\n" + exception.ToString());
                }
                try
                {
                    NoHintOnSmallFacility = bool.Parse(nextSibling.Attributes.GetNamedItem("NoHintOnSmallFacility").Value);
                }
                catch (Exception exception12)
                {
                    exception = exception12;
                    throw new Exception("NoHintOnSmallFacility:\n" + exception.ToString());
                }
                try
                {
                    HintPopulation = bool.Parse(nextSibling.Attributes.GetNamedItem("HintPopulation").Value);
                }
                catch (Exception exception13)
                {
                    exception = exception13;
                    throw new Exception("HintPopulation:\n" + exception.ToString());
                }
                try
                {
                    HintPopulationUnder1000 = bool.Parse(nextSibling.Attributes.GetNamedItem("HintPopulationUnder1000").Value);
                }
                catch (Exception exception14)
                {
                    exception = exception14;
                    throw new Exception("HintPopulationUnder1000:\n" + exception.ToString());
                }
                try
                {
                    PopulationRecruitmentLimit = bool.Parse(nextSibling.Attributes.GetNamedItem("PopulationRecruitmentLimit").Value);
                }
                catch (Exception exception15)
                {
                    exception = exception15;
                    throw new Exception("PopulationRecruitmentLimit:\n" + exception.ToString());
                }
                try
                {
                    MilitaryKindSpeedValid = bool.Parse(nextSibling.Attributes.GetNamedItem("MilitaryKindSpeedValid").Value);
                }
                catch (Exception exception16)
                {
                    exception = exception16;
                    throw new Exception("MilitaryKindSpeedValid:\n" + exception.ToString());
                }
                try
                {
                    CommonPersonAvailable = bool.Parse(nextSibling.Attributes.GetNamedItem("CommonPersonAvailable").Value);
                }
                catch (Exception exception17)
                {
                    exception = exception17;
                    throw new Exception("CommonPersonAvailable:\n" + exception.ToString());
                }
                try
                {
                    AdditionalPersonAvailable = bool.Parse(nextSibling.Attributes.GetNamedItem("AdditionalPersonAvailable").Value);
                }
                catch (Exception exception18)
                {
                    exception = exception18;
                    throw new Exception("AdditionalPersonAvailable:\n" + exception.ToString());
                }
                try
                {
                    PlayerPersonAvailable = bool.Parse(nextSibling.Attributes.GetNamedItem("PlayerPersonAvailable").Value);
                }
                catch (Exception exception19)
                {
                    exception = exception19;
                    throw new Exception("PlayerPersonAvailable:\n" + exception.ToString());
                }
                try
                {
                    PersonNaturalDeath = bool.Parse(nextSibling.Attributes.GetNamedItem("PersonNaturalDeath").Value);
                }
                catch (Exception exception20)
                {
                    exception = exception20;
                    throw new Exception("PersonNaturalDeath:\n" + exception.ToString());
                }
                try
                {
                    IdealTendencyValid = bool.Parse(nextSibling.Attributes.GetNamedItem("IdealTendencyValid").Value);
                }
                catch (Exception exception21)
                {
                    exception = exception21;
                    throw new Exception("IdealTendencyValid:\n" + exception.ToString());
                }
                try
                {
                    PinPointAtPlayer = bool.Parse(nextSibling.Attributes.GetNamedItem("PinPointAtPlayer").Value);
                }
                catch (Exception exception22)
                {
                    exception = exception22;
                    throw new Exception("PinPointAtPlayer:\n" + exception.ToString());
                }
                try
                {
                    IgnoreStrategyTendency = bool.Parse(nextSibling.Attributes.GetNamedItem("IgnoreStrategyTendency").Value);
                }
                catch (Exception exception23)
                {
                    exception = exception23;
                    throw new Exception("IgnoreStrategyTendency:\n" + exception.ToString());
                }
                try
                {
                    createChildren = bool.Parse(nextSibling.Attributes.GetNamedItem("createChildren").Value);
                }
                catch (Exception exception23)
                {
                    exception = exception23;
                    throw new Exception("createChildren:\n" + exception.ToString());
                }
                try
                {
                    zainanfashengjilv = int.Parse(nextSibling.Attributes.GetNamedItem("zainanfashengjilv").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("zainanfashengjilv:\n" + exception.ToString());
                }
                try
                {
                    doAutoSave = bool.Parse(nextSibling.Attributes.GetNamedItem("doAutoSave").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("doAutoSave:\n" + exception.ToString());
                }
                try
                {
                    createChildrenIgnoreLimit = bool.Parse(nextSibling.Attributes.GetNamedItem("createChildrenIgnoreLimit").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("createChildrenIgnoreLimit:\n" + exception.ToString());
                }
                try
                {
                    internalSurplusRateForPlayer = bool.Parse(nextSibling.Attributes.GetNamedItem("internalSurplusRateForPlayer").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("internalSurplusRateForPlayer:\n" + exception.ToString());
                }
                try
                {
                    internalSurplusRateForAI = bool.Parse(nextSibling.Attributes.GetNamedItem("internalSurplusRateForAI").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("internalSurplusRateForAI:\n" + exception.ToString());
                }
                try
                {
                    getChildrenRate = int.Parse(nextSibling.Attributes.GetNamedItem("getChildrenRate").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("getChildrenRate:\n" + exception.ToString());
                }
                try
                {
                    hougongGetChildrenRate = int.Parse(nextSibling.Attributes.GetNamedItem("hougongGetChildrenRate").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("hougongGetChildrenRate:\n" + exception.ToString());
                }
                try
                {
                    AIExecutionRate = int.Parse(nextSibling.Attributes.GetNamedItem("AIExecutionRate").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AIExecutionRate:\n" + exception.ToString());
                }
                try
                {
                    AIExecuteBetterOfficer = bool.Parse(nextSibling.Attributes.GetNamedItem("AIExecuteBetterOfficer").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AIExecuteBetterOfficer:\n" + exception.ToString());
                }
                try
                {
                    maxExperience = int.Parse(nextSibling.Attributes.GetNamedItem("maxExperience").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("maxExperience:\n" + exception.ToString());
                }
                try
                {
                    lockChildrenLoyalty = bool.Parse(nextSibling.Attributes.GetNamedItem("lockChildrenLoyalty").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("lockChildrenLoyalty:\n" + exception.ToString());
                }
                try
                {
                    AIAutoTakeNoFactionCaptives = bool.Parse(nextSibling.Attributes.GetNamedItem("AIAutoTakeNoFactionCaptives").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AIAutoTakeNoFactionCaptives:\n" + exception.ToString());
                }
                try
                {
                    AIAutoTakeNoFactionPerson = bool.Parse(nextSibling.Attributes.GetNamedItem("AIAutoTakeNoFactionPerson").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AIAutoTakeNoFactionPerson:\n" + exception.ToString());
                }
                try
                {
                    AIAutoTakePlayerCaptives = bool.Parse(nextSibling.Attributes.GetNamedItem("AIAutoTakePlayerCaptives").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AIAutoTakePlayerCaptives:\n" + exception.ToString());
                }
                try
                {
                    AIAutoTakePlayerCaptiveOnlyUnfull = bool.Parse(nextSibling.Attributes.GetNamedItem("AIAutoTakePlayerCaptiveOnlyUnfull").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AIAutoTakePlayerCaptiveOnlyUnfull:\n" + exception.ToString());
                }
                try
                {
                    DialogShowTime = int.Parse(nextSibling.Attributes.GetNamedItem("DialogShowTime").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("DialogShowTime:\n" + exception.ToString());
                }
                try
                {
                    TechniquePointMultiple = float.Parse(nextSibling.Attributes.GetNamedItem("TechniquePointMultiple").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("TechniquePointMultiple:\n" + exception.ToString());
                }
                try
                {
                    PermitFactionMerge = bool.Parse(nextSibling.Attributes.GetNamedItem("PermitFactionMerge").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("PermitFactionMerge:\n" + exception.ToString());
                }
                try
                {
                    LeadershipOffenceRate = float.Parse(nextSibling.Attributes.GetNamedItem("LeadershipOffenceRate").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("LeadershipOffenceRate:\n" + exception.ToString());
                }
                try
                {
                    LiangdaoXitong = bool.Parse(nextSibling.Attributes.GetNamedItem("LiangdaoXitong").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("LiangdaoXitong:\n" + exception.ToString());
                }
                try
                {
                    WujiangYoukenengDuli = bool.Parse(nextSibling.Attributes.GetNamedItem("WujiangYoukenengDuli").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("WujiangYoukenengDuli:\n" + exception.ToString());
                }
                try
                {
                    FastBattleSpeed = int.Parse(nextSibling.Attributes.GetNamedItem("FastBattleSpeed").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("FastBattleSpeed:\n" + exception.ToString());
                }
                try
                {
                    EnableCheat = bool.Parse(nextSibling.Attributes.GetNamedItem("EnableCheat").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("EnableCheat:\n" + exception.ToString());
                }
                try
                {
                    HardcoreMode = bool.Parse(nextSibling.Attributes.GetNamedItem("HardcoreMode").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("HardcoreMode:\n" + exception.ToString());
                }
                try
                {
                    LandArmyCanGoDownWater = bool.Parse(nextSibling.Attributes.GetNamedItem("LandArmyCanGoDownWater").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("LandArmyCanGoDownWater:\n" + exception.ToString());
                }
                try
                {
                    MaxAbility = int.Parse(nextSibling.Attributes.GetNamedItem("MaxAbility").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("MaxAbility:\n" + exception.ToString());
                }
                try
                {
                    TirednessIncrease = int.Parse(nextSibling.Attributes.GetNamedItem("TirednessIncrease").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("TirednessIncrease:\n" + exception.ToString());
                }
                try
                {
                    TirednessDecrease = int.Parse(nextSibling.Attributes.GetNamedItem("TirednessDecrease").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("TirednessDecrease:\n" + exception.ToString());
                }
                try
                {
                    EnableAgeAbilityFactor = bool.Parse(nextSibling.Attributes.GetNamedItem("EnableAgeAbilityFactor").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("EnableAgeAbilityFactor:\n" + exception.ToString());
                }
                try
                {
                    TabListDetailLevel = int.Parse(nextSibling.Attributes.GetNamedItem("TabListDetailLevel").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("TabListDetailLevel:\n" + exception.ToString());
                }
                try
                {
                    EnableExtensions = bool.Parse(nextSibling.Attributes.GetNamedItem("EnableExtensions").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("EnableExtensions:\n" + exception.ToString());
                }
                try
                {
                    EncryptSave = bool.Parse(nextSibling.Attributes.GetNamedItem("EncryptSave").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("EncryptSave:\n" + exception.ToString());
                }
                try
                {
                    AutoSaveFrequency = int.Parse(nextSibling.Attributes.GetNamedItem("AutoSaveFrequency").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AutoSaveFrequency:\n" + exception.ToString());
                }
                try
                {
                    ShowChallengeAnimation = bool.Parse(nextSibling.Attributes.GetNamedItem("ShowChallengeAnimation").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("ShowChallengeAnimation:\n" + exception.ToString());
                }
                try
                {
                    PersonDieInChallenge = bool.Parse(nextSibling.Attributes.GetNamedItem("PersonDieInChallenge").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("PersonDieInChallenge:\n" + exception.ToString());
                }
                try
                {
                    OfficerDieInBattleRate = int.Parse(nextSibling.Attributes.GetNamedItem("OfficerDieInBattleRate").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("OfficerDieInBattleRate:\n" + exception.ToString());
                }
                try
                {
                    OfficerChildrenLimit = int.Parse(nextSibling.Attributes.GetNamedItem("OfficerChildrenLimit").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("OfficerChildrenLimit:\n" + exception.ToString());
                }
                try
                {
                    StopToControlOnAttack = bool.Parse(nextSibling.Attributes.GetNamedItem("StopToControlOnAttack").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("StopToControlOnAttack:\n" + exception.ToString());
                }
                try
                {
                    MaxMilitaryExperience = int.Parse(nextSibling.Attributes.GetNamedItem("MaxMilitaryExperience").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("MaxMilitaryExperience:\n" + exception.ToString());
                }
                try
                {
                    CreateRandomOfficerChance = float.Parse(nextSibling.Attributes.GetNamedItem("CreateRandomOfficerChance").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("CreateRandomOfficerChance:\n" + exception.ToString());
                }
                try
                {
                    ZhaoXianSuccessRate = float.Parse(nextSibling.Attributes.GetNamedItem("ZhaoXianSuccessRate").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("ZhaoXianSuccessRate:\n" + exception.ToString());
                }

                try
                {
                    CreatedOfficerAbilityFactor = float.Parse(nextSibling.Attributes.GetNamedItem("CreatedOfficerAbilityFactor").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("CreatedOfficerAbilityFactor:\n" + exception.ToString());
                }
                try
                {
                    EnablePersonRelations = bool.Parse(nextSibling.Attributes.GetNamedItem("EnablePersonRelations").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("EnablePersonRelations:\n" + exception.ToString());
                }
                try
                {
                    ChildrenAvailableAge = int.Parse(nextSibling.Attributes.GetNamedItem("ChildrenAvailableAge").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("ChildrenAvailableAge:\n" + exception.ToString());
                }
                try
                {
                    FullScreen = bool.Parse(nextSibling.Attributes.GetNamedItem("FullScreen").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("FullScreen:\n" + exception.ToString());
                }
                try
                {
                    FriendlyDiplomacyThreshold = int.Parse(nextSibling.Attributes.GetNamedItem("FriendlyDiplomacyThreshold").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("FriendlyDiplomacyThreshold:\n" + exception.ToString());
                }
                try
                {
                    SurroundFactor = int.Parse(nextSibling.Attributes.GetNamedItem("SurroundFactor").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("SurroundFactor:\n" + exception.ToString());
                }
                try
                {
                    PermitQuanXiang = bool.Parse(nextSibling.Attributes.GetNamedItem("PermitQuanXiang").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("PermitQuanXiang:\n" + exception.ToString());
                }
                try
                {
                    PermitManualAwardTitleAutoLearn = bool.Parse(nextSibling.Attributes.GetNamedItem("PermitManualAwardTitleAutoLearn").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("PermitManualAwardTitleAutoLearn:\n" + exception.ToString());
                }

                try
                {
                    zhaoxianOfficerMax = int.Parse(nextSibling.Attributes.GetNamedItem("zhaoxianOfficerMax").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("zhaoxianOfficerMax:\n" + exception.ToString());
                }

                try
                {

                    FactionMilitaryLimt = int.Parse(nextSibling.Attributes.GetNamedItem("FactionMilitaryLimt").Value);

                    FixedUnnaturalDeathAge = int.Parse(nextSibling.Attributes.GetNamedItem("FixedUnnaturalDeathAge").Value);

                }
                catch (Exception exception24)
                {
                    exception = exception24;

                    throw new Exception("FactionMilitaryLimt:\n" + exception.ToString());

                    throw new Exception("FixedUnnaturalDeathAge:\n" + exception.ToString());

                }
                try
                {
                    AIQuickBattle = bool.Parse(nextSibling.Attributes.GetNamedItem("AIQuickBattle").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AIQuickBattle:\n" + exception.ToString());
                }
                try
                {
                    PlayerAutoSectionHasAIResourceBonus = bool.Parse(nextSibling.Attributes.GetNamedItem("PlayerAutoSectionHasAIResourceBonus").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("PlayerAutoSectionHasAIResourceBonus:\n" + exception.ToString());
                }
                try
                {
                    ProhibitFactionAgainstDestroyer = float.Parse(nextSibling.Attributes.GetNamedItem("ProhibitFactionAgainstDestroyer").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("ProhibitFactionAgainstDestroyer:\n" + exception.ToString());
                }
                try
                {
                    AIMergeAgainstPlayer = float.Parse(nextSibling.Attributes.GetNamedItem("AIMergeAgainstPlayer").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AIMergeAgainstPlayer:\n" + exception.ToString());
                }
                try
                {
                    RemoveSpouseIfNotAvailable = bool.Parse(nextSibling.Attributes.GetNamedItem("RemoveSpouseIfNotAvailable").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("RemoveSpouseIfNotAvailable:\n" + exception.ToString());
                }
                try
                {
                    SkyEyeSimpleNotification = bool.Parse(nextSibling.Attributes.GetNamedItem("SkyEyeSimpleNotification").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("SkyEyeSimpleNotification:\n" + exception.ToString());
                }
                try
                {
                    AutoMultipleMarriage = bool.Parse(nextSibling.Attributes.GetNamedItem("AutoMultipleMarriage").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("AutoMultipleMarriage:\n" + exception.ToString());
                }
                try
                {
                    BornHistoricalChildren = bool.Parse(nextSibling.Attributes.GetNamedItem("BornHistoricalChildren").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("BornHistoricalChildren:\n" + exception.ToString());
                }
                try
                {
                    hougongAlienOnly = bool.Parse(nextSibling.Attributes.GetNamedItem("hougongAlienOnly").Value);
                }
                catch (Exception exception24)
                {
                    exception = exception24;
                    throw new Exception("hougongAlienOnly:\n" + exception.ToString());
                }
                try
                {
                    StartCircleTime = float.Parse(nextSibling.Attributes.GetNamedItem("StartCircleTime").Value);
                }
                catch (Exception exception24)
                {

                }
                try
                {
                    ScenarioMapPerTime = float.Parse(nextSibling.Attributes.GetNamedItem("ScenarioMapPerTime").Value);
                }
                catch (Exception exception24)
                {

                }
                try
                {
                    KeepSpousePersonalLoyalty = int.Parse(nextSibling.Attributes.GetNamedItem("KeepSpousePersonalLoyalty").Value);
                }
                catch (Exception exception24)
                {

                }
                try
                {
                    ShowNumberAddTime = int.Parse(nextSibling.Attributes.GetNamedItem("ShowNumberAddTime").Value);
                }
                catch (Exception exception24)
                {

                }
                try
                {
                    TroopVoice = bool.Parse(nextSibling.Attributes.GetNamedItem("TroopVoice").Value);
                }
                catch (Exception exception24)
                {

                }
            }
            else
            {
                document = new XmlDocument();
                xml = Platform.Current.LoadText(str);
                document.LoadXml(xml);
                nextSibling = document.FirstChild.NextSibling;
                for (int i = 0; i < nextSibling.Attributes.Count; i++)
                {
                    System.Reflection.FieldInfo[] fieldInfos = typeof(GlobalVariables).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
                    foreach (var v in fieldInfos)
                    {
                        if (v.Name == nextSibling.Attributes[i].Name)
                        {
                            if (v.FieldType.ToString() == "System.Int32")
                            {
                                string temp = nextSibling.Attributes[i].Value.ToString();
                                if (temp.Contains("E"))
                                {
                                    Decimal de;
                                    Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                    v.SetValue(Session.Current.Scenario.GlobalVariables, (int)de);
                                }
                                else
                                {
                                    v.SetValue(Session.Current.Scenario.GlobalVariables, int.Parse(nextSibling.Attributes[i].Value.ToString()));
                                }
                            }
                            else if (v.FieldType.ToString() == "System.Single")
                            {
                                string temp = nextSibling.Attributes[i].Value.ToString();
                                if (temp.Contains("E"))
                                {
                                    Decimal de;
                                    Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                    v.SetValue(Session.Current.Scenario.GlobalVariables, (float)de);
                                }
                                else
                                {
                                    v.SetValue(Session.Current.Scenario.GlobalVariables, float.Parse(nextSibling.Attributes[i].Value.ToString()));
                                }
                            }
                            else if (v.FieldType.ToString() == "System.Boolean")
                            {
                                v.SetValue(Session.Current.Scenario.GlobalVariables, bool.Parse(nextSibling.Attributes[i].Value.ToString()));
                            }
                            else if (v.FieldType.ToString() == "System.String")
                            {
                                v.SetValue(Session.Current.Scenario.GlobalVariables, nextSibling.Attributes[i].Value.ToString());
                            }
                            if (v.Name == "PersonNaturalDeath")
                            {
                                Session.Current.Scenario.GlobalVariables.PersonNaturalDeath = bool.Parse(nextSibling.Attributes[i].Value.ToString());
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void SaveToXml()
        {
            XmlDocument document = new XmlDocument();

            XmlNode docNode = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(docNode);

            XmlElement element = document.CreateElement("GlobalVariables");
            element.SetAttribute("MapScrollSpeed", MapScrollSpeed.ToString());
            element.SetAttribute("TroopMoveSpeed", TroopMoveSpeed.ToString());
            element.SetAttribute("RunWhileNotFocused", RunWhileNotFocused.ToString());
            element.SetAttribute("PlayMusic", PlayMusic.ToString());
            element.SetAttribute("PlayNormalSound", PlayNormalSound.ToString());
            element.SetAttribute("PlayBattleSound", PlayBattleSound.ToString());
            element.SetAttribute("DrawMapVeil", DrawMapVeil.ToString());
            element.SetAttribute("DrawTroopAnimation", DrawTroopAnimation.ToString());
            element.SetAttribute("SkyEye", SkyEye.ToString());
            element.SetAttribute("MultipleResource", MultipleResource.ToString());
            element.SetAttribute("SingleSelectionOneClick", SingleSelectionOneClick.ToString());
            element.SetAttribute("NoHintOnSmallFacility", NoHintOnSmallFacility.ToString());
            element.SetAttribute("HintPopulation", HintPopulation.ToString());
            element.SetAttribute("HintPopulationUnder1000", HintPopulationUnder1000.ToString());
            element.SetAttribute("PopulationRecruitmentLimit", PopulationRecruitmentLimit.ToString());
            element.SetAttribute("MilitaryKindSpeedValid", MilitaryKindSpeedValid.ToString());
            element.SetAttribute("CommonPersonAvailable", CommonPersonAvailable.ToString());
            element.SetAttribute("AdditionalPersonAvailable", AdditionalPersonAvailable.ToString());
            element.SetAttribute("PlayerPersonAvailable", PlayerPersonAvailable.ToString());
            element.SetAttribute("PersonNaturalDeath", PersonNaturalDeath.ToString());
            element.SetAttribute("IdealTendencyValid", IdealTendencyValid.ToString());
            element.SetAttribute("PinPointAtPlayer", PinPointAtPlayer.ToString());
            element.SetAttribute("IgnoreStrategyTendency", IgnoreStrategyTendency.ToString());
            element.SetAttribute("createChildren", createChildren.ToString());
            element.SetAttribute("zainanfashengjilv", zainanfashengjilv.ToString());
            element.SetAttribute("doAutoSave", doAutoSave.ToString());
            element.SetAttribute("createChildrenIgnoreLimit", createChildrenIgnoreLimit.ToString());
            element.SetAttribute("internalSurplusRateForPlayer", internalSurplusRateForPlayer.ToString());
            element.SetAttribute("internalSurplusRateForAI", internalSurplusRateForAI.ToString());
            element.SetAttribute("getChildrenRate", getChildrenRate.ToString());
            element.SetAttribute("hougongGetChildrenRate", hougongGetChildrenRate.ToString());
            element.SetAttribute("AIExecutionRate", AIExecutionRate.ToString());
            element.SetAttribute("AIExecuteBetterOfficer", AIExecuteBetterOfficer.ToString());
            element.SetAttribute("maxExperience", maxExperience.ToString());
            element.SetAttribute("lockChildrenLoyalty", lockChildrenLoyalty.ToString());
            element.SetAttribute("AIAutoTakeNoFactionCaptives", AIAutoTakeNoFactionCaptives.ToString());
            element.SetAttribute("AIAutoTakeNoFactionPerson", AIAutoTakeNoFactionPerson.ToString());
            element.SetAttribute("AIAutoTakePlayerCaptives", AIAutoTakePlayerCaptives.ToString());
            element.SetAttribute("AIAutoTakePlayerCaptiveOnlyUnfull", AIAutoTakePlayerCaptiveOnlyUnfull.ToString());
            element.SetAttribute("DialogShowTime", DialogShowTime.ToString());
            element.SetAttribute("TechniquePointMultiple", TechniquePointMultiple.ToString());
            element.SetAttribute("PermitFactionMerge", PermitFactionMerge.ToString());
            element.SetAttribute("GameDifficulty", GameDifficulty.ToString());
            element.SetAttribute("LeadershipOffenceRate", LeadershipOffenceRate.ToString());
            element.SetAttribute("LiangdaoXitong", LiangdaoXitong.ToString());
            element.SetAttribute("WujiangYoukenengDuli", WujiangYoukenengDuli.ToString());
            element.SetAttribute("FastBattleSpeed", FastBattleSpeed.ToString());
            element.SetAttribute("EnableCheat", EnableCheat.ToString());
            element.SetAttribute("HardcoreMode", HardcoreMode.ToString());
            element.SetAttribute("LandArmyCanGoDownWater", LandArmyCanGoDownWater.ToString());
            element.SetAttribute("MaxAbility", MaxAbility.ToString());
            element.SetAttribute("TirednessIncrease", TirednessIncrease.ToString());
            element.SetAttribute("TirednessDecrease", TirednessDecrease.ToString());
            element.SetAttribute("EnableAgeAbilityFactor", EnableAgeAbilityFactor.ToString());
            element.SetAttribute("TabListDetailLevel", TabListDetailLevel.ToString());
            element.SetAttribute("EnableExtensions", EnableExtensions.ToString());
            element.SetAttribute("EncryptSave", EncryptSave.ToString());
            element.SetAttribute("AutoSaveFrequency", AutoSaveFrequency.ToString());
            element.SetAttribute("ShowChallengeAnimation", ShowChallengeAnimation.ToString());
            element.SetAttribute("PersonDieInChallenge", PersonDieInChallenge.ToString());
            element.SetAttribute("OfficerDieInBattleRate", OfficerDieInBattleRate.ToString());
            element.SetAttribute("OfficerChildrenLimit", OfficerChildrenLimit.ToString());
            element.SetAttribute("StopToControlOnAttack", StopToControlOnAttack.ToString());
            element.SetAttribute("MaxMilitaryExperience", MaxMilitaryExperience.ToString());
            element.SetAttribute("CreateRandomOfficerChance", CreateRandomOfficerChance.ToString());
            element.SetAttribute("ZhaoXianSuccessRate", ZhaoXianSuccessRate.ToString());
            element.SetAttribute("CreatedOfficerAbilityFactor", CreatedOfficerAbilityFactor.ToString());
            element.SetAttribute("EnablePersonRelations", EnablePersonRelations.ToString());
            element.SetAttribute("ChildrenAvailableAge", ChildrenAvailableAge.ToString());
            element.SetAttribute("FullScreen", FullScreen.ToString());
            element.SetAttribute("FriendlyDiplomacyThreshold", FriendlyDiplomacyThreshold.ToString());
            element.SetAttribute("SurroundFactor", SurroundFactor.ToString());
            element.SetAttribute("PermitQuanXiang", PermitQuanXiang.ToString());
            element.SetAttribute("PermitManualAwardTitleAutoLearn", PermitManualAwardTitleAutoLearn.ToString());
            element.SetAttribute("zhaoxianOfficerMax", zhaoxianOfficerMax.ToString());
            element.SetAttribute("FactionMilitaryLimt", FactionMilitaryLimt.ToString());
            element.SetAttribute("FixedUnnaturalDeathAge", FixedUnnaturalDeathAge.ToString());
            element.SetAttribute("AIQuickBattle", AIQuickBattle.ToString());
            element.SetAttribute("PlayerAutoSectionHasAIResourceBonus", PlayerAutoSectionHasAIResourceBonus.ToString());
            element.SetAttribute("ChildrenAbilityFactor", ChildrenAbilityFactor.ToString());
            element.SetAttribute("ProhibitFactionAgainstDestroyer", ProhibitFactionAgainstDestroyer.ToString());
            element.SetAttribute("AIMergeAgainstPlayer", AIMergeAgainstPlayer.ToString());
            element.SetAttribute("RemoveSpouseIfNotAvailable", RemoveSpouseIfNotAvailable.ToString());
            element.SetAttribute("SkyEyeSimpleNotification", SkyEyeSimpleNotification.ToString());
            element.SetAttribute("AutoMultipleMarriage", AutoMultipleMarriage.ToString());
            element.SetAttribute("BornHistoricalChildren", BornHistoricalChildren.ToString());
            element.SetAttribute("hougongAlienOnly", hougongAlienOnly.ToString());
            element.SetAttribute("StartCircleTime", StartCircleTime.ToString());
            element.SetAttribute("ScenarioMapPerTime", ScenarioMapPerTime.ToString());
            element.SetAttribute("KeepSpousePersonalLoyalty", KeepSpousePersonalLoyalty.ToString());
            element.SetAttribute("ShowNumberAddTime", ShowNumberAddTime.ToString());
            element.SetAttribute("TroopVoice", TroopVoice.ToString());

            document.AppendChild(element);
        
            Platform.Current.SaveUserFile("Content/Data/GlobalVariables.xml", document.OuterXml, true);
        }
    }
}

