using GameGlobal;
using GameObjects.Animations;
using GameObjects.FactionDetail;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using GameObjects.Influences;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.IO;
using GameObjects.Conditions;
using System.Runtime.Serialization;
using Platforms;
using Tools;
using GameManager;

namespace GameObjects
{
    [DataContract]
    public class PersonIDRelation
    {
        [DataMember]
        public int PersonID1 { get; set; }

        [DataMember]
        public int PersonID2 { get; set; }

        [DataMember]
        public int Relation { get; set; }
    }

    //using GameObjects.PersonDetail.PersonMessages;
    [DataContract]
    public class Person : GameObject
    {
        private int maxExperience = Session.GlobalVariables.maxExperience;

        [DataMember]
        public bool huaiyun = false;
        [DataMember]
        public bool shoudongsousuo = false;
        [DataMember]
        public int huaiyuntianshu = -1;
        [DataMember]
        public bool ManualStudy = false;

        private int numberOfChildren ;

        [DataMember]
        public bool faxianhuaiyun = false;
        [DataMember]
        public int suoshurenwu = -1;

        [DataMember]
        public int princessTakerID = -1;
        
        public PersonList suoshurenwuList = new PersonList();

        private bool alive;
        private int ambition;
        private int arrivingDays;
        private bool available;
        private int availableLocation;
        //private float baseImpactRate;//已经没用了
        private PersonBornRegion bornRegion;
        private int braveness;
        private PersonList brothers = new PersonList();
        private float bubingExperience;
        private string calledName;
        private int calmness;

        public void Init()
        {
            suoshurenwuList = new PersonList();

            brothers = new PersonList();

            closePersons = new PersonList();

            hatedPersons = new PersonList();

            if (joinFactionID == null)
            {
                joinFactionID = new List<int>();
            }

            if (prohibitedFactionID == null)
            {
                prohibitedFactionID = new Dictionary<int, int>();
            }

            Skills = new SkillTable();

            StudySkillList = new GameObjectList();

            StudyStuntList = new GameObjectList();

            StudyTitleList = new GameObjectList();

            AppointableTitleList = new GameObjectList();

            Stunts = new StuntTable();

            Treasures = new TreasureList();

            RealTitles = new List<Title>();

            UniqueMilitaryKinds = new MilitaryKindTable();

            UniqueTitles = new TitleTable();

            relations = new Dictionary<Person, int>();

            preferredTroopPersons = new PersonList();

            effectiveTreasures = new Dictionary<int, Treasure>();

            oldInjuraRate = injureRate;
            //injureRate = 1.0f;

            DayLearnTitleDay = Session.Parameters.LearnTitleDays;

            InfluenceRateOfCommand = 1.0f;
            InfluenceRateOfStrength = 1.0f;
            InfluenceRateOfIntelligence = 1.0f;
            InfluenceRateOfPolitics = 1.0f;
            InfluenceRateOfGlamour = 1.0f;

            MultipleOfAgricultureReputation = 1;
            MultipleOfAgricultureTechniquePoint = 1;
            MultipleOfCommerceReputation = 1;
            MultipleOfCommerceTechniquePoint = 1;
            MultipleOfDominationReputation = 1;
            MultipleOfDominationTechniquePoint = 1;
            MultipleOfEnduranceReputation = 1;
            MultipleOfEnduranceTechniquePoint = 1;
            MultipleOfMoraleReputation = 1;
            MultipleOfMoraleTechniquePoint = 1;
            MultipleOfRecruitmentReputation = 1;
            MultipleOfRecruitmentTechniquePoint = 1;
            MultipleOfTacticsReputation = 1;
            MultipleOfTacticsTechniquePoint = 1;
            MultipleOfTechnologyReputation = 1;
            MultipleOfTechnologyTechniquePoint = 1;
            MultipleOfTrainingReputation = 1;
            MultipleOfTrainingTechniquePoint = 1;

            maxChildren = 1;

            CommandDecrease = new List<KeyValuePair<int, int>>();
            CommandIncrease = new List<KeyValuePair<int, int>>();
            StrengthDecrease = new List<KeyValuePair<int, int>>();
            StrengthIncrease = new List<KeyValuePair<int, int>>();
            IntelligenceDecrease = new List<KeyValuePair<int, int>>();
            IntelligenceIncrease = new List<KeyValuePair<int, int>>();
            PoliticsDecrease = new List<KeyValuePair<int, int>>();
            PoliticsIncrease = new List<KeyValuePair<int, int>>();
            GlamourDecrease = new List<KeyValuePair<int, int>>();
            GlamourIncrease = new List<KeyValuePair<int, int>>();
            ReputationDecrease = new List<KeyValuePair<int, int>>();
            ReputationIncrease = new List<KeyValuePair<int, int>>();
            LoseSkill = new List<KeyValuePair<int, int>>();

            maxExperience = Session.GlobalVariables.maxExperience;
        }
        
        public int ChanceOfNoCapture;

        [DataMember]
        public int PCharacter { get; set; }

        public CharacterKind Character;

        private PersonList closePersons = new PersonList();
        private int command;
        private float commandExperience;
       
        public Person ConvincingPerson;

        [DataMember]
        public int ConvincingPersonID;
        private InformationKind currentInformationKind;

        public bool DayAvoidInfluenceByBattle;
        public bool DayAvoidInternalDecrementOnBattle;
        public bool DayAvoidPopulationEscape;
        public int DayLearnTitleDay = 90;
        public bool DayLocationLoyaltyNoChange;
        public float DayRateIncrementOfpublic = 0f;

        private PersonDeadReason deadReason;
        private Person father = null;
       // private PersonForm form;//已无用
        private int generation;
        private string givenName;
        private int glamour;
        private float glamourExperience;
        private PersonList hatedPersons = new PersonList();
        private int ideal;

        
        public IdealTendencyKind IdealTendency;

        public bool ImmunityOfCaptive;
        public bool ImmunityOfDieInBattle;
#pragma warning disable CS0649 // Field 'Person.impactRateOfBadForm' is never assigned to, and will always have its default value 0
       // private float impactRateOfBadForm;//已无用
#pragma warning restore CS0649 // Field 'Person.impactRateOfBadForm' is never assigned to, and will always have its default value 0
      //  private float impactRateOfGoodForm;//已无用

        public int IncrementOfAgricultureAbility;
        public int IncrementOfChallengeWinningChance;
        public int IncrementOfCommerceAbility;
        public int IncrementOfControversyWinningChance;
        public int IncrementOfDominationAbility;
        public int IncrementOfEnduranceAbility;
        public int IncrementOfMoraleAbility;
        public int IncrementOfRecruitmentAbility;
        public int IncrementOfSpyDays;
        public int IncrementOfTechnologyAbility;
        public int IncrementOfTrainingAbility;
        public bool InevitableSuccessOfConvince;
        public bool InevitableSuccessOfDestroy;
        public bool InevitableSuccessOfGossip;
        public bool InevitableSuccessOfInstigate;
        public bool InevitableSuccessOfSearch;
        public bool InevitableSuccessOfSpy;
        public bool InevitableSuccessOfJailBreak;
        public int InfluenceIncrementOfCommand;
        public int InfluenceIncrementOfGlamour;
        public int InfluenceIncrementOfIntelligence;
        public int InfluenceIncrementOfPolitics;
        public int InfluenceIncrementOfStrength;
        public int InfluenceIncrementOfReputation;
        public int InfluenceIncrementOfLoyalty;
       // public float InfluenceRateOfBadForm;已无用
        public float InfluenceRateOfCommand = 1f;
        public float InfluenceRateOfGlamour = 1f;
        // public float InfluenceRateOfGoodForm;已无用
        public float InfluenceRateOfIntelligence = 1f;
        public float InfluenceRateOfPolitics = 1f;
        public float InfluenceRateOfStrength = 1f;
        private int informationKindID = -1;
        private int intelligence;
        private float intelligenceExperience;
        private float internalExperience;
        public bool InternalNoFundNeeded;
        private bool leaderPossibility;

        public int MonthIncrementOfFactionReputation = 0;
        public int MonthIncrementOfTechniquePoint = 0;
        private Person mother = null;

        public int MultipleOfAgricultureReputation = 1;
        public int MultipleOfAgricultureTechniquePoint = 1;
        public int MultipleOfCommerceReputation = 1;
        public int MultipleOfCommerceTechniquePoint = 1;
        public int MultipleOfDominationReputation = 1;
        public int MultipleOfDominationTechniquePoint = 1;
        public int MultipleOfEnduranceReputation = 1;
        public int MultipleOfEnduranceTechniquePoint = 1;
        public int MultipleOfMoraleReputation = 1;
        public int MultipleOfMoraleTechniquePoint = 1;
        public int MultipleOfRecruitmentReputation = 1;
        public int MultipleOfRecruitmentTechniquePoint = 1;
        public int MultipleOfTacticsReputation = 1;
        public int MultipleOfTacticsTechniquePoint = 1;
        public int MultipleOfTechnologyReputation = 1;
        public int MultipleOfTechnologyTechniquePoint = 1;
        public int MultipleOfTrainingReputation = 1;
        public int MultipleOfTrainingTechniquePoint = 1;

        private float nubingExperience;
        private List<int> joinFactionID = new List<int>();
        private Dictionary<int, int> prohibitedFactionID = new Dictionary<int, int>();

        [DataMember]
        public ArchitectureWorkKind OldWorkKind = ArchitectureWorkKind.无;

        [DataMember]
        public ArchitectureWorkKind firstPreferred = ArchitectureWorkKind.无;

        private Point? outsideDestination;
        private OutsideTaskKind outsideTask;
        private int personalLoyalty;

        public Biography PersonBiography;

        private int pictureIndex;
        private int politics;
        private float politicsExperience;
        private float qibingExperience;
        private float qixieExperience;
        private PersonQualification qualification;

        public int RadiusIncrementOfInformation;
        public float RateIncrementOfAgricultureAbility;
        public float RateIncrementOfCommerceAbility;
        public float RateIncrementOfConvince;
        public float RateIncrementOfDestroy;
        public float RateIncrementOfDominationAbility;
        public float RateIncrementOfEnduranceAbility;
        public float RateIncrementOfGossip;
        public float RateIncrementOfInstigate;
        public float RateIncrementOfJailBreakAbility;
        public float RateIncrementOfMoraleAbility;
        public float RateIncrementOfRecruitmentAbility;
        public float RateIncrementOfSearch;
        public float RateIncrementOfTechnologyAbility;
        public float RateIncrementOfTrainingAbility;

        private Military recruitmentMilitary;
        private int reputation;

        [DataMember]
        public bool RewardFinished;

        private int routCount;
        private int routedCount;
        private bool sex = false;
        private float shuijunExperience;

        [DataMember]
        public string SkillsString { get; set; }
        
        public SkillTable Skills = new SkillTable();

        private Person spouse = null;
        private int strain;
        private float stratagemExperience;
        private PersonStrategyTendency strategyTendency;
        private int strength;
        private float strengthExperience;
        
        public Stunt StudyingStunt;
        
        public Title StudyingTitle;
        
        public GameObjectList StudySkillList = new GameObjectList();
        
        public GameObjectList StudyStuntList = new GameObjectList();
        
        public GameObjectList StudyTitleList = new GameObjectList();

        public GameObjectList AppointableTitleList = new GameObjectList();//封官列表
        
        public Person marriageGranter;

        [DataMember]
        public string StuntsString { get; set; }

        [DataMember]
        public int StudyingStuntString { get; set; }        

        public StuntTable Stunts = new StuntTable();
        private string surName;
        private float tacticsExperience;

        public Architecture TargetArchitecture;
        private int taskDays;

        public TreasureList Treasures = new TreasureList();

        private PersonValuationOnGovernment valuationOnGovernment;
        private ArchitectureWorkKind workKind = ArchitectureWorkKind.无;
        private int yearAvailable;
        private int yearBorn;
        private int yearDead;
        private Dictionary<Person, int> relations = new Dictionary<Person, int>();

        [DataMember]
        public string RealTitlesString { get; set; }

        [DataMember]
        public int PersonalTitleString { get; set; }

        [DataMember]
        public int CombatTitleString { get; set; }

        [DataMember]
        public int StudyingTitleString { get; set; }

        public List<Title> RealTitles = new List<Title>();

        public MilitaryKindTable UniqueMilitaryKinds = new MilitaryKindTable();
        public TitleTable UniqueTitles = new TitleTable();

        //public List<Title> RealGuanzhis = new List<Title>();
        // public TitleTable Guanzhis = new TitleTable();

        [DataMember]
        public string UniqueMilitaryKindsString { get; set; }

        [DataMember]
        public string UniqueTitlesString { get; set; }

        private PersonStatus status;

        private Person waitForFeiZi = null;

        [DataMember]
        public int waitForFeiZiPeriod = 0;

        [DataMember]
        public int waitForFeiziId;

        public float ExperienceRate;
        public int CommandExperienceIncrease { get; set; }
        public int StrengthExperienceIncrease { get; set; }
        public int IntelligenceExperienceIncrease { get; set; }
        public int PoliticsExperienceIncrease { get; set; }
        public int GlamourExperienceIncrease { get; set; }
        public int ReputationDayIncrease { get; set; }
        public float MovementDaysBonus { get; set; }
        public int ConvinceIdealSkip { get; set; }
        public int LongetivityIncreaseByInfluence { get; set; }

        [DataMember]
        public bool Immortal { get; set; }

        //public OngoingBattle Battle { get; set; }
        [DataMember]
        public int BattleSelfDamage { get; set; }

        [DataMember]
        public bool IsGeneratedChildren { get; set; }
        [DataMember]
        public int StrengthPotential { get; set; }
        [DataMember]
        public int CommandPotential { get; set; }
        [DataMember]
        public int IntelligencePotential { get; set; }
        [DataMember]
        public int PoliticsPotential { get; set; }
        [DataMember]
        public int GlamourPotential { get; set; }

        [DataMember]
        public int TrainPolicyIDString { get; set; }

        public TrainPolicy TrainPolicy { get; set; }

        [DataMember]
        public String Tags {get; set;}
        [DataMember]
        public int TempLoyaltyChange = 0;
        [DataMember]
        public bool wasMayor = false;

        private int karma = 0;

        [DataMember]
        public int Karma
        {
            get
            {
                return karma;
            }
            set
            {
                karma = value;
            }
        }

        private Captive belongedCaptive;

        //[DataMember]
        public Captive BelongedCaptive
        {
            get
            {
                return belongedCaptive;
            }
            set
            {
                belongedCaptive = value;
            }
        }

        public void SetBelongedCaptive(Captive c, PersonStatus newState)
        {
            this.belongedCaptive = c;
            if (c == null)
            {
                this.Status = newState;
            }
            else
            {
                this.Status = PersonStatus.Captive;
            }
        }

        private float oldInjuraRate = 1.0f;
        private float injureRate = 1.0f;

        [DataMember]
        public float InjureRate
        {
            get
            {
                if (!this.Alive)
                {
                    return 1;
                }
                if (this.Identity() != 0)
                {
                    return injureRate;
                }
                return 1;
            }
            set
            {
                if (this.Identity() != 0)
                {
                    injureRate = value;
                }
                if (injureRate < 0)
                {
                    injureRate = 0;
                }
            }
        }

        public float OldInjureRate
        {
            get
            {
                return oldInjuraRate;
            }
            set
            {
                oldInjuraRate = value;
            }
        }
        public int captiveEscapeChance;
        public int pregnantChance;
        public int childrenAbilityIncrease;
        public int childrenSkillChanceIncrease;
        public int childrenStuntChanceIncrease;
        public int childrenTitleChanceIncrease;
        public int childrenReputationIncrease;
        public int childrenLoyalty;
        public int childrenLoyaltyRate;
        public int multipleChildrenRate;
        public int maxChildren = 1;
        public int chanceTirednessStopIncrease;
        public int bravenessIncrease;
        public int calmnessIncrease;

        public PersonList preferredTroopPersons = new PersonList();
        [DataMember]
        public string preferredTroopPersonsString;

        private Troop locationTroop = null;
        public Troop LocationTroop
        {
            get
            {
                return locationTroop;
            }
            set
            {
                locationTroop = value;
            }
        }

        private Architecture locationArchitecture = null;
        public Architecture LocationArchitecture
        {
            get
            {
                return locationArchitecture;
            }
            set
            {
                locationArchitecture = value;
            }
        }

        //private Dictionary<int, Treasure> effectiveTreasures = new Dictionary<int, Treasure>();
        public Dictionary<int, Treasure> effectiveTreasures = new Dictionary<int, Treasure>();

        private int officerMerit;

        [DataMember]
        public int OfficerMerit
        {
            get
            {
                return officerMerit;
            }
            set
            {
                officerMerit = value;
            }
        }

        private int tiredness;

        [DataMember]
        public int Tiredness
        {
            get
            {
                if (!this.Alive)
                {
                    return 0;
                }
                if (this.Identity() != 0)
                {
                    return tiredness;
                }
                return 0;
            }
            set
            {
                if (this.Identity() != 0)
                {
                    tiredness = value;
                }
            }
        }

        public int HasHorse()
        {
            foreach (Treasure treasure in this.Treasures)
            {
                if (treasure.Influences.HasInfluenceKind(5110))
                {
                    return treasure.ID;
                }
            }
            return -1;
        }

        private Person princessTaker;
        public Person PrincessTaker
        {
            get
            {
                if (princessTaker == null && princessTakerID >= 0)
                {
                    princessTaker = Session.Current.Scenario.Persons.GetGameObject(princessTakerID) as Person;
                }
                return princessTaker;
            }
            set
            {
                princessTaker = value;
                princessTakerID = value.ID;
            }
        }

        //以下添加20170226
        //↓获取某类的宝物中最大价值物品的ID
        public int IDforMaxTreasureGroupValue(int id)
        {
            int num = 0;
            int idnum = 0;
            foreach (Treasure t in this.Treasures)
            {
                if (t.TreasureGroup == id)
                {
                    if (num < t.Worth)
                    {
                        num = t.Worth;
                        idnum = t.ID;
                    }
                    else if ((num == t.Worth && idnum < t.ID))
                    {
                        num = t.Worth;
                        idnum = t.ID;
                    }
                }
            }
            return idnum;

        }
        //↓获取最大价值宝物名称
        public string TreasureNameforGroup(int id)
        {
            foreach (Treasure t in this.Treasures)
            {
                if (t.TreasureGroup == id)
                {
                    if (t.ID == IDforMaxTreasureGroupValue(id))
                    {
                        return t.Name;
                    }
                }
            }
            return "无";
        }
        //↓获取最大价值宝物价值
        public int TreasureWorthforGroup(int id)
        {
            foreach (Treasure t in this.Treasures)
            {
                if (t.TreasureGroup == id)
                {
                    if (t.ID == IDforMaxTreasureGroupValue(id))
                    {
                        return t.Worth;
                    }
                }
            }
            return -1;
        }
        //↓获取最大价值宝物介绍
        public string TreasureDescriptionforGroup(int id)
        {
            foreach (Treasure t in this.Treasures)
            {
                if (t.TreasureGroup == id)
                {
                    if (t.ID == IDforMaxTreasureGroupValue(id))
                    {
                        return t.Description;
                    }
                }
            }
            return "无";
        }
        //↓获取最大价值宝物图片
        public PlatformTexture TreasurePictureforGroup(int id)
        {
            foreach (Treasure t in this.Treasures)
            {
                if (t.TreasureGroup == id)
                {
                    if (t.ID == IDforMaxTreasureGroupValue(id))
                    {

                        return t.Picture;
                    }
                }
            }
            return null;
        }
        //↓判定是否拥有某类的宝物
        public bool HasTreasureforGroup(int id)
        {
            foreach (Treasure t in this.Treasures)
            {
                if (t.TreasureGroup == id)
                {
                    return true;
                }
            }
            return false;
        }

        //↓获取人物某类称号的名称
        public string TitleNameforGroup(int id)
        {

            foreach (Title t in this.Titles)
            {
                if (t.Kind.ID == id)
                {
                    return t.Name;
                }
            }
            return "无";
        }
        //↓获取人物某类称号的称号种类
        public string TitleKindforGroup(int id)
        {

            foreach (Title t in this.Titles)
            {
                if (t.Kind.ID == id)
                {
                    return t.KindName;
                }
            }
            return "无";
        }
        //↓获取人物某类称号的称号等级
        public int TitleLevelforGroup(int id)
        {

            foreach (Title t in this.Titles)
            {
                if (t.Kind.ID == id)
                {
                    return t.Level;
                }
            }
            return -1;
        }
        //↓获取人物某类称号的称号说明
        public string TitleDescriptionforGroup(int id)
        {

            foreach (Title t in this.Titles)
            {
                if (t.Kind.ID == id)
                {
                    return t.Description;
                }
            }
            return "无";
        }
        //↓判定是否拥有某类称号
        public bool HasTitleforGroup(int id)
        {
            foreach (Title t in this.Titles)
            {
                if (t.Kind.ID == id)
                {
                    return true;
                }
            }
            return false;
        }
        //↓获取人物某类特技的名称
        public string StuntNameforGroup(int id)
        {

            foreach (Stunt t in this.Stunts.Stunts.Values)
            {
                if (t.ID == id)
                {
                    return t.Name;
                }
            }
            return "无";
        }
        //↓获取人物某类特技的消耗
        public int StuntCombativityforGroup(int id)
        {

            foreach (Stunt t in this.Stunts.Stunts.Values)
            {
                if (t.ID == id)
                {
                    return t.Combativity;
                }
            }
            return -1;
        }
        //↓获取人物某类特技的持久
        public int StuntPeriodforGroup(int id)
        {

            foreach (Stunt t in this.Stunts.Stunts.Values)
            {
                if (t.ID == id)
                {
                    return t.Period;
                }
            }
            return -1;
        }
        //↓获取人物某类特技的使用说明
        public string StuntDescriptionforGroup(int id)
        {

            foreach (Stunt t in this.Stunts.Stunts.Values)
            {
                if (t.ID == id)
                {
                    return t.CastConditionString;
                }
            }
            return "无";
        }
        //↓判定是否拥有某特技
        public bool HasStuntforGroup(int id)
        {
            foreach (Stunt t in this.Stunts.Stunts.Values)
            {
                if (t.ID == id)
                {
                    return true;
                }
            }
            return false;
        }
        //↓获取人物某技能的名称
        public string SkillNameforGroup(int id)
        {

            foreach (Skill t in this.Skills.Skills.Values)
            {
                if (t.ID == id)
                {
                    return t.Name;
                }
            }
            return "无";
        }
        //↓获取人物某技能的技能种类
        public string SkillKindforGroup(int id)
        {
            foreach (Skill t in this.Skills.Skills.Values)
            {
                if (t.ID == id)
                {
                    if (t.Combat == true)
                    {
                        return "战斗";
                    }
                }
            }
            return "非战斗";
        }
        //↓获取人物某类技能的等级
        public int SkillLevelforGroup(int id)
        {
            foreach (Skill t in this.Skills.Skills.Values)
            {
                if (t.ID == id)
                {
                    return t.Level;
                }
            }
            return -1;
        }
        //↓获取人物某类技能的技能说明
        public string SkillDescriptionforGroup(int id)
        {

            foreach (Skill t in this.Skills.Skills.Values)
            {
                if (t.ID == id)
                {
                    return t.Description;
                }
            }
            return "无";
        }
        //↓判定是否拥有某技能
        public bool HasSkillforGroup(int id)
        {
            foreach (Skill t in this.Skills.Skills.Values)
            {
                if (t.ID == id)
                {
                    return true;
                }
            }
            return false;
        }
        //以上添加
        public bool CanOwnTitleByAge(Title t)
        {
            if (!Session.GlobalVariables.EnableAgeAbilityFactor) return true;
            if (t == null) return true;
	        if (this.Trainable) return true;
	        if (this.Age >= Session.GlobalVariables.ChildrenAvailableAge) return true;
            if (this.IsGeneratedChildren) return true;
	    
            return (this.ID * 953
                    + (this.Name.Length > 0 ? this.Name[0] : 753) * 866
                    + (this.Name.Length > 1 ? this.Name[1] : 125) * 539
                    + t.ID * 829
                    + (t.Description.Length > 0 ? t.Description[0] : 850) * 750
                    ) % 15 < this.Age
                    && (this.Age > t.Level * 3 || this.Age >= 15);
        }

        public List<Title> Titles
        {
            get
            {
                List<Title> result = new List<Title>();
                foreach (Title t in this.RealTitles)
                {
                    if (!Session.GlobalVariables.EnableAgeAbilityFactor || this.CanOwnTitleByAge(t))
                    {
                        result.Add(t);
                    }
                }
                return result;
            }
        }
        /*
        public List<Guanzhi> Guanzhis
        {
            get
            {
                List<Guanzhi> result = new List<Guanzhi>();
                foreach (Guanzhi g in this.RealGuanzhis)
                {
                    result.Add(g);
                }
                return result;
            }
        }
        */
         
        public int TotalTitleLevel
        {
            get
            {
                int result = 0;
                foreach (Title t in this.Titles)
                {
                    result += t.Level;
                }
                return result;
            }
        }
        /*
        public Guanzhi getGuanzhiOfKind(GuanzhiKind kind)
        {
            foreach (Guanzhi g in this.Guanzhis)
            {
                if (g.Kind == kind)
                {
                    return g;
                }
            }
            return null;
        }
        */

        public Title getTitleOfKind(TitleKind kind)
        {
            foreach (Title t in this.Titles)
            {
                if (t.Kind.Equals(kind))
                {
                    return t;
                }
            }
            return null;
        }

        [DataMember]
        public int YearJoin { get; set; }
        [DataMember]
        public int TroopDamageDealt { get; set; }
        [DataMember]
        public int TroopBeDamageDealt { get; set; }
        [DataMember]
        public int ArchitectureDamageDealt { get; set; }
        [DataMember]
        public int RebelCount { get; set; }
        [DataMember]
        public int ExecuteCount { get; set; }
        [DataMember]
        public int OfficerKillCount { get; set; }
        [DataMember]
        public int FleeCount { get; set; }
        [DataMember]
        public int HeldCaptiveCount { get; set; }
        [DataMember]
        public int CaptiveCount { get; set; }
        [DataMember]
        public int StratagemSuccessCount { get; set; }
        [DataMember]
        public int StratagemFailCount { get; set; }
        [DataMember]
        public int StratagemBeSuccessCount { get; set; }
        [DataMember]
        public int StratagemBeFailCount { get; set; }

        private int agricultureAbility = 0; // 缓存这几个变量
        private int commerceAbility = 0;
        private int technologyAbility = 0;
        private int moraleAbility = 0;
        private int dominationAbility = 0;
        private int enduranceAbility = 0;
        private int trainingAbility = 0;

        public List<KeyValuePair<int, int>> CommandDecrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> CommandIncrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> StrengthDecrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> StrengthIncrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> IntelligenceDecrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> IntelligenceIncrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> PoliticsDecrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> PoliticsIncrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> GlamourDecrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> GlamourIncrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> ReputationDecrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> ReputationIncrease = new List<KeyValuePair<int, int>>();
        public List<KeyValuePair<int, int>> LoseSkill = new List<KeyValuePair<int, int>>();

        private OutsideTaskKind lastOutsideTask = OutsideTaskKind.无;

        [DataMember]
        public OutsideTaskKind LastOutsideTask
        {
            get
            {
                return lastOutsideTask;
            }
            //private set
            set
            {
                lastOutsideTask = value;
            }
        }

        private int returnedDaySince = 0;

        [DataMember]
        public int ReturnedDaySince
        {
            get
            {
                return returnedDaySince;
            }
            //private set
            set
            {
                returnedDaySince = value;
            }
        }

        public int ServedYears
        {
            get
            {
                if (this.Status != PersonStatus.Normal && this.Status != PersonStatus.Moving) return 0;
                int year = Session.Current.Scenario.Date.Year - this.YearJoin;
                int sinceBeginning = Session.Current.Scenario.DaySince / 360;
                return Math.Min(year, sinceBeginning);
            }
        }

        public PersonStatus Status
        {
            get
            {
                return status;
            }
            set
            {
                if (value == PersonStatus.Moving && this.LocationTroop != null)
                {
                    this.LocationTroop = null;
                }
                if (value != PersonStatus.Normal)
                {
                    if (this.RecruitmentMilitary != null)
                    {
                        this.RecruitmentMilitary.StopRecruitment();
                    }
                    this.WorkKind = ArchitectureWorkKind.无;
                }
                if (value != PersonStatus.Normal && status == PersonStatus.Normal && this.OutsideTask == OutsideTaskKind.无)
                {
                    this.PurifySkills(true);
                    this.PurifyTitles(true);
                    this.PurifyAllTreasures(true);
                    this.PurifyArchitectureInfluence(true);
                    this.PurifyFactionInfluence(true);
                }
                else if (value == PersonStatus.Normal && status == PersonStatus.Moving && this.OutsideTask == OutsideTaskKind.无)
                {
                    this.ApplySkills(true);
                    this.ApplyTitles(true);
                    this.ApplyAllTreasures(true);
                    this.ApplyArchitectureInfluence(true);
                    this.ApplyFactionInfluence(true);
                }
                if (Session.Current.Scenario != null)
                {
                    Session.Current.Scenario.ClearPersonWorkCache();
                    Session.Current.Scenario.ClearPersonStatusCache();
                }
                status = value;
            }
        }

        public Faction BelongedFaction
        {
            get
            {
                if ((this.Status == PersonStatus.Normal || this.Status == PersonStatus.Moving) && this.LocationArchitecture != null)
                {
                    return this.LocationArchitecture.BelongedFaction;
                }
                else if (this.Status == PersonStatus.Normal && this.LocationTroop != null)
                {
                    return this.LocationTroop.BelongedFaction;
                }
                else if (this.Status == PersonStatus.Captive)
                {
                    return this.BelongedCaptive.CaptiveFaction;
                }
                return null;
            }
        }

        public Faction BelongedFactionWithPrincess
        {
            get
            {
                if ((this.Status == PersonStatus.Normal || this.Status == PersonStatus.Moving || this.Status == PersonStatus.Princess) && this.LocationArchitecture != null)
                {
                    return this.LocationArchitecture.BelongedFaction;
                }
                else if (this.Status == PersonStatus.Normal && this.LocationTroop != null)
                {
                    return this.LocationTroop.BelongedFaction;
                }
                else if (this.Status == PersonStatus.Captive)
                {
                    return this.BelongedCaptive.CaptiveFaction;
                }
                return null;
            }
        }

        public Person WaitForFeiZi
        {
            get
            {
                return waitForFeiZi;
            }
            set
            {
                waitForFeiZi = value;
                waitForFeiZiPeriod = 30;
                waitForFeiziId = value == null ? -1 : value.ID;
            }
        }

        public int WaitForFeiZiPeriod
        {
            get
            {
                return waitForFeiZiPeriod;
            }
            set
            {
                waitForFeiZiPeriod = value;
            }
        }


        private void updateDayCounters()
        {
            //waitForFeiZiPeriod--;
            waitForFeiZiPeriod -= Session.Parameters.DayInTurn;
            if (waitForFeiZiPeriod < 0)
            {
                WaitForFeiZi = null;
            }
            //ReturnedDaySince++;
            ReturnedDaySince += Session.Parameters.DayInTurn;
        } 

        public event BeAwardedTreasure OnBeAwardedTreasure;

        public event BeConfiscatedTreasure OnBeConfiscatedTreasure;

        public event BeKilled OnBeKilled;

        public event ChangeLeader OnChangeLeader;

        public event ConvinceFailed OnConvinceFailed;

        public event ConvinceSuccess OnConvinceSuccess;

        public event JailBreakSuccess OnJailBreakSuccess;

        public event JailBreakFailed OnJailBreakFailed;

        public event Death OnDeath;

        public event DeathChangeFaction OnDeathChangeFaction;

        public event DestroyFailed OnDestroyFailed;

        public event DestroySuccess OnDestroySuccess;

        public event GossipFailed OnGossipFailed;

        public event GossipSuccess OnGossipSuccess;

        public event InformationObtained OnInformationObtained;

        public event qingbaoshibai qingbaoshibaishijian;

        public event InstigateFailed OnInstigateFailed;

        public event InstigateSuccess OnInstigateSuccess;

        public event Leave OnLeave;

        public event SearchFinished OnSearchFinished;
        /*
        public event ShowMessage OnShowMessage;

        public event SpyFailed OnSpyFailed;

        public event SpyFound OnSpyFound;

        public event SpySuccess OnSpySuccess;
        */
        public event StudySkillFinished OnStudySkillFinished;

        public event StudyStuntFinished OnStudyStuntFinished;

        public event StudyTitleFinished OnStudyTitleFinished;

        public event TreasureFound OnTreasureFound;

        public event CapturedByArchitecture OnCapturedByArchitecture;

        public event CreateSpouse OnCreateSpouse;

        public event CreateBrother OnCreateBrother;

        public event CreateSister OnCreateSister;



        /*
        public List<string> LoadGuanzhiFromString(String s, TitleTable allTitles)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Title guanzhi = null ;
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (allTitles.Titles.TryGetValue(int.Parse(strArray[i]), out title))
                    {
                        this.RealGuanzhis.Add(guanzhi);
                    }
                    else
                    {
                        errorMsg.Add("官职ID" + strArray[i] + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("官职一栏应为半型空格分隔的官职ID");
            }
            return errorMsg;
        }
        
        public String SaveGuanzhiToString()
        {
            String s = "";
            foreach (Guanzhi  g in this.RealGuanzhis)
            {
                s += g.ID + " ";
            }
            return s;
        }
        */

        public List<string> LoadTitleFromString(String s, TitleTable allTitles)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = s.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Title title = null;
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (allTitles.Titles.TryGetValue(int.Parse(strArray[i]), out title))
                    {
                        this.RealTitles.Add(title);
                    }
                    else
                    {
                        errorMsg.Add("稱號ID" + strArray[i] + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("稱號一栏应为半型空格分隔的稱號ID");
            }
            return errorMsg;
        }

        public String SaveTitleToString()
        {
            String s = "";
            foreach (Title t in this.RealTitles)
            {
                s += t.ID + " ";
            }
            return s;
        }

        public double TirednessFactor
        {
            get
            {
                return Math.Max(0.2, Math.Min(1, ((210 - this.Tiredness) / 180.0)));
            }
        }

        public void AddBubingExperience(int increment)
        {
            this.bubingExperience += (increment * Session.Parameters.ArmyExperienceRate * (1 + ExperienceRate)
                * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
            if (this.bubingExperience > maxExperience)
            {
                this.bubingExperience = maxExperience;
            }
        }

        public void AddCommandExperience(int increment)
        {
            if (increment > 0)
            {
                this.commandExperience += (increment * Session.Parameters.AbilityExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
                if (this.commandExperience > maxExperience)
                {
                    this.commandExperience = maxExperience;
                }
            }
        }

        public void AddGlamourExperience(int increment)
        {
            if (increment > 0)
            {
                this.glamourExperience += (increment * Session.Parameters.AbilityExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
                if (this.glamourExperience > maxExperience)
                {
                    this.glamourExperience = maxExperience;
                }
            }
        }

        public void AddIntelligenceExperience(int increment)
        {
            if (increment > 0)
            {
                this.intelligenceExperience += (increment * Session.Parameters.AbilityExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
                if (this.intelligenceExperience > maxExperience)
                {
                    this.intelligenceExperience = maxExperience;
                }
            }
        }

        public void AddInternalExperience(int increment)
        {
            this.internalExperience += (increment * Session.Parameters.InternalExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
            if (this.internalExperience > maxExperience)
            {
                this.internalExperience = maxExperience;
            }
        }

        public void AddNubingExperience(int increment)
        {
            this.nubingExperience += (increment * Session.Parameters.ArmyExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
            if (this.nubingExperience > maxExperience)
            {
                this.nubingExperience = maxExperience;
            }
        }

        public void AddPoliticsExperience(int increment)
        {
            if (increment > 0)
            {
                this.politicsExperience += (increment * Session.Parameters.AbilityExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
                if (this.politicsExperience > maxExperience)
                {
                    this.politicsExperience = maxExperience;
                }
            }
        }

        public void AddQibingExperience(int increment)
        {
            this.qibingExperience += (increment * Session.Parameters.ArmyExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
            if (this.qibingExperience > maxExperience)
            {
                this.qibingExperience = maxExperience;
            }
        }

        public void AddQixieExperience(int increment)
        {
            this.qixieExperience += (increment * Session.Parameters.ArmyExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
            if (this.qixieExperience > maxExperience)
            {
                this.qixieExperience = maxExperience;
            }
        }

        public void AddShuijunExperience(int increment)
        {
            this.shuijunExperience += (increment * Session.Parameters.ArmyExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
            if (this.shuijunExperience > maxExperience)
            {
                this.shuijunExperience = maxExperience;
            }
        }

        public void AddStratagemExperience(int increment)
        {
            this.stratagemExperience += (increment * Session.Parameters.ArmyExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
            if (this.stratagemExperience > maxExperience)
            {
                this.stratagemExperience = maxExperience;
            }
        }

        public void AddStrengthExperience(int increment)
        {
            if (increment > 0)
            {
                this.strengthExperience += (int)(increment * Session.Parameters.AbilityExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
                if (this.strengthExperience > maxExperience)
                {
                    this.strengthExperience = maxExperience;
                }
            }
        }

        public bool AddTacticsExperience(int increment)
        {
            this.tacticsExperience += (increment * Session.Parameters.ArmyExperienceRate * (1 + ExperienceRate)
                    * (this.LocationArchitecture == null ? 1 : 1 + this.LocationArchitecture.ExperienceRate)
                    * (this.LocationTroop == null ? 1 : 1 + this.LocationTroop.ExperienceRate))
                * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIOfficerExperienceRate);
            if (this.tacticsExperience > maxExperience)
            {
                this.tacticsExperience = maxExperience;
            }
            return true;
        }

        public void AddRecruitmentExperience(int increment)
        {
            if (this.RecruitmentMilitary != null)
            {
                switch (this.RecruitmentMilitary.Kind.Type)
                {
                    case MilitaryType.步兵:
                        this.AddBubingExperience(increment);
                        break;

                    case MilitaryType.弩兵:
                        this.AddNubingExperience(increment);
                        break;

                    case MilitaryType.骑兵:
                        this.AddQibingExperience(increment);
                        break;

                    case MilitaryType.水军:
                        this.AddShuijunExperience(increment);
                        break;

                    case MilitaryType.器械:
                        this.AddQixieExperience(increment);
                        break;
                }
            }
        }

        public void AddTreasureToList(TreasureList list)
        {
            foreach (Treasure treasure in this.Treasures)
            {
                list.Add(treasure);
            }
        }

        public void ApplySkills(bool excludePersonal)
        {
            foreach (Skill skill in this.Skills.Skills.Values)
            {
                skill.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Skill, skill.ID, excludePersonal);
            }
        }

        public void PurifySkills(bool excludePersonal)
        {
            foreach (Skill skill in this.Skills.Skills.Values)
            {
                skill.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Skill, skill.ID, excludePersonal);
            }
        }

        public void ApplyStunts()
        {
            if ((this.LocationTroop != null) && (this.LocationTroop.Leader == this))
            {
                this.LocationTroop.Stunts.Clear();
                foreach (Stunt stunt in this.Stunts.Stunts.Values)
                {
                    this.LocationTroop.Stunts.AddStunt(stunt);
                }
            }
        }

        public void ApplyTitles(bool excludePersonal)
        {
            foreach (Title t in this.Titles)
            {
                t.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Title, t.ID, excludePersonal);
            }
        }

        public void PurifyTitles(bool excludePersonal)
        {
            foreach (Title t in this.Titles)
            {
                t.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Title, t.ID, excludePersonal);
            }
        }

        public void PurifyTreasure(Treasure treasure, bool excludePersonal)
        {
            PurifyTreasureSkipSubstitute(treasure, excludePersonal);

            Treasure substitute = null;
            foreach (Treasure t in this.Treasures)
            {
                if (t.TreasureGroup == treasure.TreasureGroup)
                {
                    if (substitute == null)
                    {
                        substitute = t;
                    } 
                    else if (t.Worth > substitute.Worth || (t.Worth == substitute.Worth && t.ID < substitute.ID))
                    {
                        substitute = t;
                    }
                }
            }
            if (substitute != null)
            {
                ApplyTreasure(substitute, excludePersonal);
            }
        }

        private void PurifyTreasureSkipSubstitute(Treasure treasure, bool excludePersonal)
        {
            treasure.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Treasure, treasure.TreasureGroup, excludePersonal);
            effectiveTreasures.Remove(treasure.TreasureGroup);
        }

        public void PurifyAllTreasures(bool excludePersonal)
        {
            // removing all treasures, do not need to care about treasure group stacking
            foreach (Treasure treasure in this.Treasures)
            {
                treasure.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Treasure, treasure.TreasureGroup, excludePersonal);
                effectiveTreasures.Remove(treasure.TreasureGroup);
            }
        }

        public void ApplyTreasure(Treasure treasure, bool excludePersonal)
        {
            if (effectiveTreasures.ContainsKey(treasure.TreasureGroup))
            {
                Treasure old = effectiveTreasures[treasure.TreasureGroup];
                if (treasure.Worth > old.Worth || (treasure.Worth == old.Worth && treasure.ID < old.ID))
                {
                    this.PurifyTreasureSkipSubstitute(effectiveTreasures[treasure.TreasureGroup], excludePersonal);
                }
                else
                {
                    return;
                }
            }
            treasure.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Treasure, treasure.TreasureGroup, excludePersonal);
            effectiveTreasures.Add(treasure.TreasureGroup, treasure);
        }

        public void ApplyAllTreasures(bool excludePersonal)
        {
            foreach (Treasure treasure in this.Treasures)
            {
                ApplyTreasure(treasure, excludePersonal);
            }
        }

        public void AwardedTreasure(Treasure t)
        {
            this.ReceiveTreasure(t);

            if (this.OnBeAwardedTreasure != null)
            {
                this.OnBeAwardedTreasure(this, t);
            }
            // this.AdjustIdealToFactionLeader(-t.Worth / 50);

        }

        public bool BeAvailable()
        {
            Architecture gameObject = Session.Current.Scenario.Architectures.GetGameObject(this.AvailableLocation) as Architecture;
            if (gameObject == null)
            {
                this.AvailableLocation = (Session.Current.Scenario.Architectures[GameObject.Random(Session.Current.Scenario.Architectures.Count)] as Architecture).ID;
                gameObject = Session.Current.Scenario.Architectures.GetGameObject(this.AvailableLocation) as Architecture;
            }
            if (gameObject != null)
            {
                if (this.IsGeneratedChildren)
                {
                    this.PersonBiography.Brief += Person.GenerateBiography(this);
                    Person father = this.Father;
                    Person mother = this.Mother;
                    this.Reputation = (int)(Math.Min(200000, father.Reputation + mother.Reputation) * (GameObject.Random(100) / 100.0 * 0.05 + 0.025)) + father.childrenReputationIncrease + mother.childrenReputationIncrease;
                    this.Karma = (int)(Math.Max(-2000, Math.Min(500, father.Karma + mother.Karma)) * (GameObject.Random(100) / 100.0 * 0.2 + 0.1));
                }

                this.IsGeneratedChildren = false;
                ExtensionInterface.call("PersonBecomeAvailable", new Object[] { Session.Current.Scenario, this });
                Session.Current.Scenario.PreparedAvailablePersons.Add(this);
                return true;
            }
            return false;
        }

        public void ChangeFaction(GameObjects.Faction faction)
        {
            this.Status = PersonStatus.Normal;
            this.YearJoin = Session.Current.Scenario.Date.Year;
            this.TempLoyaltyChange = 0;
        }

        public static int ChanlengeWinningChance(Person source, Person destination)
        {
            int num = 0;
            if (source.Strength >= destination.Strength)
            {
                num = ((50 + ((int)Math.Round(Math.Pow((double)(source.Strength - destination.Strength), 2.0), 3))) + source.IncrementOfChallengeWinningChance) - destination.IncrementOfChallengeWinningChance;
            }
            else
            {
                num = ((50 - ((int)Math.Round(Math.Pow((double)(destination.Strength - source.Strength), 2.0), 3))) + source.IncrementOfChallengeWinningChance) - destination.IncrementOfChallengeWinningChance;
            }
            if (num > 100)
            {
                return 100;
            }
            if (num < 0)
            {
                return 0;
            }
            return num;
        }

        public Architecture BelongedArchitecture
        {
            get
            {
                if (this.IsCaptive)
                {
                    if (this.BelongedCaptive.CaptiveFaction != null)
                    {
                        return this.BelongedCaptive.CaptiveFaction.Capital;
                    }
                    else
                    {
                        return this.BelongedCaptive.LocationArchitecture;
                    }
                    
                }
                    

                if (this.LocationArchitecture != null)
                {
                    return this.LocationArchitecture;
                }
                if (this.TargetArchitecture != null)
                {
                    return this.TargetArchitecture;
                }
                if (this.LocationTroop != null)
                {
                    return this.LocationTroop.StartingArchitecture;
                }
                return null;
            }
        }

        private int ClosePersonKilledReaction
        {
            get
            {
                int[] reaction = { 0, 2, 4, 3, 1 };
                if (this.PersonalLoyalty < 0) return reaction[0];
                if (this.PersonalLoyalty > 4) return reaction[4];

                return reaction[this.PersonalLoyalty];
            }
        }

        public void KilledInBattle(Troop killingTroop, Person killer)
        {
            killingTroop.Army.OfficerKillCount++;
            killer.OfficerKillCount++;
            Session.Current.Scenario.YearTable.addKilledInBattleEntry(Session.Current.Scenario.Date, killer, this);

            foreach (Person p in Session.Current.Scenario.Persons)
            {
                // person close to killed one may hate killer
                if (p == this) continue;
                if (p == killer) continue;
                if (p.HasCloseStrainTo(killer)) continue;
                if (p.IsCloseTo(killer)) continue;
                if (p.Hates(killer)) continue;
                if (p.Hates(this)) continue;
                if (p.IsVeryCloseTo(this))
                {
                    p.AddHated(killer, -500 * p.PersonalLoyalty * p.PersonalLoyalty);
                }
                if (p.HasCloseStrainTo(this))
                {
                    int hateChance = this.ClosePersonKilledReaction * 25;
                    if (GameObject.Chance(hateChance))
                    {
                        p.AddHated(killer, -500 * p.PersonalLoyalty * p.PersonalLoyalty);
                    }
                }
                if (killer.BelongedFaction != null)
                {
                    foreach (Treasure treasure in this.Treasures.GetList())
                    {
                        this.LoseTreasure(treasure);
                        killer.BelongedFaction.Leader.ReceiveTreasure(treasure);
                    }
                }
            }

            this.ToDeath(killer, this.BelongedFaction);
        }

        public void KilledInBattle(Troop killer)
        {
            Person kill;
            if (GameObject.Chance(70))
            {
                kill = killer.Leader;
            }
            else
            {
                kill = killer.Persons.GetMaxStrengthPerson();
            }

            this.KilledInBattle(killer, kill);
        }

        public void ToDeath(Person killerInBattle, Faction oldFaction)
        {
            Architecture locationArchitecture;
            Troop locationTroop = null;
            GameObjects.Faction belongedFaction = oldFaction;

            Session.Current.Scenario.YearTable.addPersonDeathEntry(Session.Current.Scenario.Date, this);

            int deathLocation = 0;
            if (this.LocationTroop != null)
            {
                locationTroop = this.LocationTroop;
                locationArchitecture = this.LocationTroop.StartingArchitecture;
                deathLocation = 2;
                if (!locationTroop.Destroyed)
                {
                    locationTroop.Persons.Remove(this);
                    this.LocationTroop = null;
                    locationTroop.RefreshAfterLosePerson();
                }

            }
            else if (this.LocationArchitecture != null)
            {
                locationArchitecture = this.LocationArchitecture;
                deathLocation = 1;
            }
            else
            {
                deathLocation = 3;
                locationArchitecture = null;
                if(this.ArrivingDays > 0) this.ArrivingDays = 0;

                if (this.TaskDays > 0) this.TaskDays = 0;

                if (this.WorkKind != ArchitectureWorkKind.无) this.WorkKind = ArchitectureWorkKind.无;

                this.RecruitmentMilitary.ID = -1;

               // throw new Exception("try to kill person onway");
            }

            if (this.Spouse != null && this.Spouse.Spouse != null)
            {
                if (!this.Spouse.Spouse.Sex || this.Spouse.Spouse.PersonalLoyalty < Session.Current.Scenario.GlobalVariables.KeepSpousePersonalLoyalty)
                {
                    if (this.Spouse.Spouse == this)
                    {
                        this.Spouse.Spouse = null;
                    }
                    this.Spouse = null;
                }
            }

            this.Alive = false;  //死亡
            this.SetBelongedCaptive(null, PersonStatus.None);
            this.LocationArchitecture = null;
            
            if (this.OnDeath != null && locationArchitecture != null && deathLocation == 1)
            {
                this.OnDeath(this, killerInBattle, locationArchitecture, null);
            }
            else if (this.OnDeath != null && locationTroop != null && deathLocation == 2)
            {
                this.OnDeath(this, killerInBattle, null, locationTroop);
            }
            else if (this.OnDeath != null && locationArchitecture == null && deathLocation == 3)
            {
                this.OnDeath(this, killerInBattle, null, null);
            }

            if (belongedFaction != null && this == belongedFaction.Leader)
            {
                string name = belongedFaction.Name;
                Session.Current.Scenario.YearTable.addKingDeathEntry(Session.Current.Scenario.Date, this, belongedFaction);
                GameObjects.Faction faction2 = belongedFaction.ChangeLeaderAfterLeaderDeath();
                if (faction2 != null)
                {
                    if (belongedFaction == faction2)
                    {
                        bool changeName = false;
                        if ((name == this.Name) && (belongedFaction.Leader.Ambition >= (int)PersonAmbition.普通))
                        {
                            belongedFaction.Name = belongedFaction.Leader.Name;
                            changeName = true;
                        }
                        if (this.OnChangeLeader != null)
                        {
                            this.OnChangeLeader(belongedFaction, belongedFaction.Leader, changeName, name);
                        }
                    }
                    else if (this.OnDeathChangeFaction != null)
                    {
                        this.OnDeathChangeFaction(this, faction2.Leader, name);
                    }
                }
                else
                {
                    foreach (Architecture architecture2 in belongedFaction.Architectures.GetList())
                    {
                        architecture2.ResetFaction(null);
                    }
                    belongedFaction.Destroy();
                }
            }
            else
            {

                Treasure baowu;

                while (this.Treasures.Count > 0)
                {
                    baowu = (Treasure)this.Treasures[0];
                    this.LoseTreasure(baowu);
                    baowu.Available = false;
                    baowu.HidePlace = locationArchitecture;
                }
            }

            ExtensionInterface.call("PersonDie", new Object[] { Session.Current.Scenario, this });
        }

        public int EstimatedLongetivity
        {
            get
            {
                return this.YearBorn + 18 + this.PersonalLoyalty * 8 - this.Ambition * 8 + this.Intelligence / 4 + this.Strength / 4;
            }
        }

        private void CheckDeath()
        {
            if (!this.Immortal && Session.GlobalVariables.PersonNaturalDeath == true && this.LocationArchitecture != null && this.Alive)
            {
                int yearDead;
                if (this.DeadReason == PersonDeadReason.自然死亡)
                {
                    yearDead = this.YearDead;
                }
                else
                {
                    if (Session.GlobalVariables.FixedUnnaturalDeathAge <= 0)
                    {
                        yearDead = Math.Max(this.YearDead, this.EstimatedLongetivity);
                    }
                    else
                    {
                        yearDead = Math.Max(this.YearDead, this.YearBorn + Session.GlobalVariables.FixedUnnaturalDeathAge);
                    }
                }
                yearDead += this.LongetivityIncreaseByInfluence;

                if (Session.Current.Scenario.Date.Year < this.YearDead && 
                    GameObject.Random((this.YearDead - Session.Current.Scenario.Date.Year) * (100 + this.Strength) / Session.Parameters.DayInTurn) == 0)
                {
                    this.InjureRate -= 0.1f;
                    Session.MainGame.mainGameScreen.OnOfficerSick(this);
                }
                else if (Session.Current.Scenario.Date.Year >= yearDead)
                {
                    //if (this.DeadReason == PersonDeadReason.被杀死 && GameObject.Random(30) == 0)
                    if (this.DeadReason == PersonDeadReason.被杀死 && GameObject.Random(30 / Session.Parameters.DayInTurn) == 0)
                    {
                        this.InjureRate -= (Session.Current.Scenario.Date.Year - yearDead + 1) * 0.1f;
                    }
                    //else if (this.DeadReason == PersonDeadReason.郁郁而终 && GameObject.Random(30) == 0 &&
                    else if (this.DeadReason == PersonDeadReason.郁郁而终 && GameObject.Random(30 / Session.Parameters.DayInTurn) == 0 &&
                        (this.BelongedFaction == null || this.Status == PersonStatus.Captive || this.BelongedFaction.ArchitectureTotalSize <= 8))
                    {
                        this.InjureRate -= (Session.Current.Scenario.Date.Year - yearDead + 1) * 0.1f;
                    }
                    //else if (this.DeadReason == PersonDeadReason.操劳过度 && GameObject.Random(30) == 0 &&
                    else if (this.DeadReason == PersonDeadReason.操劳过度 && GameObject.Random(30 / Session.Parameters.DayInTurn) == 0 &&
                        this.InternalExperience + this.StratagemExperience + this.TacticsExperience
                        + this.BubingExperience + this.QibingExperience + this.NubingExperience + this.ShuijunExperience + this.QixieExperience >= 30000)
                    {
                        this.InjureRate -= (Session.Current.Scenario.Date.Year - yearDead + 1) * 0.1f;
                    }
                    //else if (this.DeadReason == PersonDeadReason.自然死亡 && GameObject.Random(20) == 0)
                    else if (this.DeadReason == PersonDeadReason.自然死亡 && GameObject.Random(30 / Session.Parameters.DayInTurn) == 0)
                    {
                        this.InjureRate -= (Session.Current.Scenario.Date.Year - yearDead + 1) * 0.1f;
                    }
                    else if (GameObject.Random(90 / Session.Parameters.DayInTurn) == 0)
                    //else if (GameObject.Random(90) == 0)
                    {
                        this.InjureRate -= (Session.Current.Scenario.Date.Year - yearDead + 1) * 0.1f;
                    }

                    if (this.InjureRate <= 0.05)
                    {
                        this.ToDeath(null, this.BelongedFaction);
                    }
                    else
                    {
                        Session.MainGame.mainGameScreen.OnOfficerSick(this);
                    }
                }
                
            }

        }

        public Troop BelongedTroop
        {
            get
            {
                foreach (Troop t in Session.Current.Scenario.Troops.GameObjects)
                {
                    if (t.Persons.GameObjects.Contains(this))
                    {
                        return t;
                    }
                }
                return null;
            }
        }

        public void ApplyFactionInfluence(bool excludePersonal)
        {
            if (this.BelongedFaction != null)
            {
                foreach (Technique t in this.BelongedFaction.AvailableTechniques.Techniques.Values)
                {
                    foreach (Influences.Influence i in t.Influences.Influences.Values)
                    {

                        if (i.Kind.Type == GameObjects.Influences.InfluenceType.战斗 || i.Kind.Type == GameObjects.Influences.InfluenceType.建筑战斗)
                        {
                            Troop a = this.LocationTroop;
                            if (a != null && a.Leader == this)
                            {
                                i.ApplyInfluence(a, GameObjects.Influences.Applier.Technique, t.ID);
                            }
                        }

                        if (i.Kind.Type == InfluenceType.个人 || i.Kind.Type == InfluenceType.势力 || i.Kind.Type == InfluenceType.多选一)
                        {
                            i.ApplyInfluence(this, GameObjects.Influences.Applier.Technique, t.ID, excludePersonal);
                        }
                    }
                }
            }
        }

        public void PurifyFactionInfluence(bool excludePersonal)
        {
            if (this.BelongedFaction != null)
            {
                foreach (Technique t in this.BelongedFaction.AvailableTechniques.Techniques.Values)
                {
                    foreach (Influences.Influence i in t.Influences.Influences.Values)
                    {
                        if (i.Kind.Type == GameObjects.Influences.InfluenceType.战斗 || i.Kind.Type == GameObjects.Influences.InfluenceType.建筑战斗)
                        {
                            Troop a = this.LocationTroop;
                            if (a != null && a.Leader == this)
                            {
                                i.PurifyInfluence(a, GameObjects.Influences.Applier.Technique, t.ID);
                            }
                        }

                        if (i.Kind.Type == InfluenceType.个人 || i.Kind.Type == InfluenceType.势力 || i.Kind.Type == InfluenceType.多选一)
                        {
                            i.PurifyInfluence(this, GameObjects.Influences.Applier.Technique, t.ID, excludePersonal);
                        }
                    }
                }
            }
        }

        public void ApplyArchitectureInfluence(bool excludePersonal)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                foreach (Influences.Influence i in this.LocationArchitecture.Characteristics.Influences.Values)
                {
                    if (i.Kind.Type == GameObjects.Influences.InfluenceType.战斗 || i.Kind.Type == GameObjects.Influences.InfluenceType.建筑战斗)
                    {
                        Troop a = this.LocationTroop;
                        if (a != null && a.Leader == this)
                        {
                            i.ApplyInfluence(a, GameObjects.Influences.Applier.Characteristics, 0);
                        }
                    }

                    if (i.Kind.Type == InfluenceType.个人 || i.Kind.Type == InfluenceType.势力 || i.Kind.Type == InfluenceType.多选一)
                    {
                        i.ApplyInfluence(this, GameObjects.Influences.Applier.Characteristics, 0, excludePersonal);
                    }
                }
            }
        }

        public void PurifyArchitectureInfluence(bool excludePersonal)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                foreach (Influences.Influence i in this.LocationArchitecture.Characteristics.Influences.Values)
                {
                    if (i.Kind.Type == GameObjects.Influences.InfluenceType.战斗 || i.Kind.Type == GameObjects.Influences.InfluenceType.建筑战斗)
                    {
                        Troop a = this.LocationTroop;
                        if (a != null && a.Leader == this)
                        {
                            i.PurifyInfluence(a, GameObjects.Influences.Applier.Characteristics, 0);
                        }
                    }

                    if (i.Kind.Type == InfluenceType.个人 || i.Kind.Type == InfluenceType.势力 || i.Kind.Type == InfluenceType.多选一)
                    {
                        i.PurifyInfluence(this, GameObjects.Influences.Applier.Characteristics, 0, excludePersonal);
                    }
                }
            }
        }

        public void ConfiscatedTreasure(Treasure t)
        {
            this.AdjustRelation(this.BelongedFaction.Leader, -t.Worth / 3.0f, -5);
            this.LoseTreasure(t);

            if (this.OnBeConfiscatedTreasure != null)
            {
                this.OnBeConfiscatedTreasure(this, t);
            }

            ExtensionInterface.call("ConfiscatedTreasure", new Object[] { Session.Current.Scenario, this });
        }

        public static int ControversyWinningChance(Person source, Person destination)
        {
            int num = 0;
            int controversyAbility = source.ControversyAbility;
            int num3 = destination.ControversyAbility;
            num = (((50 + controversyAbility) - num3) + source.IncrementOfControversyWinningChance) - destination.IncrementOfControversyWinningChance;
            if (num > 100)
            {
                return 100;
            }
            if (num < 0)
            {
                return 0;
            }
            return num;
        }

        public void RecoverFromInjury()
        {
            //int chance = this.Strength + 10;
            int chance = this.Strength / 2 + 5;

            if (this.LocationArchitecture != null)
            {
                if (this.Spouse != null && this.Spouse.LocationArchitecture == this.LocationArchitecture)
                {
                    chance *= 2;
                }
                
                chance = (int)(chance * this.LocationArchitecture.MultipleOfRecovery);
            }

            if (this.InjureRate < 1 && GameObject.Chance(chance))
            {
                //this.InjureRate += (GameObject.Random(30) + 1) / 1000.0f;
                this.InjureRate += (GameObject.Random(30) + 1) / 1000.0f * Session.Parameters.DayInTurn;
                if (this.InjureRate > 1)
                {
                    this.InjureRate = 1;
                    Session.MainGame.mainGameScreen.OnOfficerRecovered(this);
                }
            }
        }

        public void DayEvent()
        {
            this.CheckDeath();
            if (this.Alive)
            {
                this.RecoverFromInjury();
                this.LeaveFaction();
                this.NoFactionMove();
                this.LoyaltyChange();
                this.ProgressArrivingDays();
                this.huaiyunshijian();
                this.updateDayCounters();
                this.createRelations();
                this.AutoLearnEvent();

                List<int> toRemove = new List<int>();
                foreach (KeyValuePair<int, int> i in new Dictionary<int, int>(this.ProhibitedFactionID))
                {
                    //this.ProhibitedFactionID[i.Key]--;
                    this.ProhibitedFactionID[i.Key] -= Session.Parameters.DayInTurn;
                    if (this.ProhibitedFactionID[i.Key] <= 0)
                    {
                        toRemove.Add(i.Key);
                    }
                }
                foreach (int i in toRemove) 
                {
                    this.ProhibitedFactionID.Remove(i);
                }
            }

            this.CommandExperience += this.CommandExperienceIncrease;
            this.StrengthExperience += this.StrengthExperienceIncrease;
            this.IntelligenceExperience += this.IntelligenceExperienceIncrease;
            this.PoliticsExperience += this.PoliticsExperienceIncrease;
            this.GlamourExperience += this.GlamourExperienceIncrease;
            this.Reputation += this.ReputationDayIncrease;

            agricultureAbility = 0;
            commerceAbility = 0;
            technologyAbility = 0;
            moraleAbility = 0;
            dominationAbility = 0;
            enduranceAbility = 0;
            trainingAbility = 0;
            higherLevelLearnableTitle = null;
            makeMarryableInFactionCache = null;
        }

        private void AutoLearnEvent()
        {
            if (this.BelongedFactionWithPrincess != null)
            {
                this.AutoLearnSkill();
                this.AutoLearnStunt();
            }
        }

        private void AutoLearnSkill()
        {
            string skillString = "";
            foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
            {
                if (((this.Skills.GetSkill(skill.ID) == null) && skill.CanLearn(this)) && (GameObject.Chance(Session.Parameters.AutoLearnSkillSuccessRate)))
                {
                    this.Skills.AddSkill(skill);
                    skill.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Skill, skill.ID, false);
                    skillString = skillString + "•" + skill.Name;
                }
            }
        }

        private void AutoLearnStunt()
        {
            foreach (Stunt stunt in Session.Current.Scenario.GameCommonData.AllStunts.Stunts.Values)
            {
                if ((this.Stunts.GetStunt(stunt.ID) == null) && stunt.IsLearnable(this) && (GameObject.Chance(Session.Parameters.AutoLearnStuntSuccessRate)))
                {
                    this.Stunts.AddStunt(stunt);
                    stunt.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Stunt, stunt.ID, false);
                }
            }
        }

        public PersonList MakeMarryable()
        {
            PersonList result = new PersonList();

            if (this.LocationArchitecture == null) return result;

            if (this.Spouse != null)
            {
                if ((this.Spouse.LocationArchitecture == this.LocationArchitecture) && (this.Spouse.Spouse == null))
                {
                    result.Add(this.Spouse);
                }
                return result;
            }

            foreach (Person p in this.LocationArchitecture.Persons)
            {
                if (p == this) continue;
                if (p.isLegalFeiZi(this) && this.isLegalFeiZi(p) && Person.GetIdealOffset(p, this) <= Session.Parameters.MakeMarrigeIdealLimit
                    && !p.Hates(this) && !this.Hates(p) && p.Spouse == null)
                {
                    result.Add(p);
                }
                if (p.Spouse == this)
                {
                    result.Add(p);
                }
            }

            return result;
        }

        public String MakeMarryableList
        {
            get
            {
                PersonList pl = MakeMarryableInFaction();
                pl.PropertyName = "Merit";
                pl.SmallToBig = false;
                pl.IsNumber = true;
                pl.ReSort();

                int cnt = 1;
                String s = "";
                foreach (Person p in pl)
                {
                    s += p.Name + " ";
                    cnt++;
                    if (cnt > 5) break;
                }
                return s;
            }
        }

        private PersonList makeMarryableInFactionCache = null;
        public PersonList MakeMarryableInFaction()
        {
            if (this.BelongedFaction == null || this.BelongedFaction.Leader.Status == PersonStatus.Captive) return new PersonList();

            if (this.Status != PersonStatus.Normal) return new PersonList();

            if (makeMarryableInFactionCache != null) return makeMarryableInFactionCache;

            PersonList result = new PersonList();

            if (this.Spouse != null) return result;

            if (this.BelongedFaction == null) return result;

            foreach (Person p in this.BelongedFaction.Persons)
            {
                if (p == this) continue;
                if (p.isLegalFeiZi(this) && this.isLegalFeiZi(p) && Person.GetIdealOffset(p, this) <= Session.Parameters.MakeMarrigeIdealLimit
                    && !p.Hates(this) && !this.Hates(p) && p.Spouse == null)
                {
                    result.Add(p);
                }
                if (p.Spouse != null && p.BelongedFaction == this.BelongedFaction && p.Spouse.Spouse == null)
                {
                    result.Add(p);
                }
            }

            makeMarryableInFactionCache = result;

            return result;
        }

        public void Marry(Person p, Person maker)
        {
            if (!(this.Spouse == p || p.Spouse == this))
            {
                this.LocationArchitecture.DecreaseFund(Session.Parameters.MakeMarriageCost);

                makeHateCausedByAffair(this, p, maker);
            }

            this.Spouse = p;
            p.Spouse = this;

            this.AdjustRelation(p, 15f, -5);
            p.AdjustRelation(this, 15f, -5);

            this.marriageGranter = maker;

            Session.Current.Scenario.YearTable.addCreateSpouseEntry(Session.Current.Scenario.Date, this, p);
            Session.MainGame.mainGameScreen.MakeMarriage(this, p);

            foreach (Person q in this.BelongedFaction.Persons)
            {
                q.makeMarryableInFactionCache = null;
            }
        }

        private void createRelations()
        {
            //if (this.LocationArchitecture != null && GameObject.Random(60) == 0)
            if (this.LocationArchitecture != null && GameObject.Random(60 / Session.Parameters.DayInTurn) == 0)
            {
                foreach (KeyValuePair<Person, int> i in this.relations)
                {
                    if (i.Value >= Session.Parameters.VeryCloseThreshold / 2 && i.Key.GetRelation(this) >= Session.Parameters.VeryCloseThreshold / 2 && i.Key.BelongedFactionWithPrincess == this.BelongedFactionWithPrincess
                         && !this.HasStrainTo(i.Key) && !this.IsVeryCloseTo(i.Key)
                        && (!((bool)Session.GlobalVariables.PersonNaturalDeath) || (Math.Abs(this.Age - i.Key.Age) <= 40 && this.Age <= (50 + (this.Sex ? 0 : 10)) && i.Key.Age <= (50 + (i.Key.Sex ? 0 : 10))
                            && this.Age >= 16 && i.Key.Age >= 16))
                            && this.LocationArchitecture == i.Key.LocationArchitecture)
                    {
                        if (this.Sex == i.Key.Sex)
                        {
                            bool legal = true;
                            foreach (Person p in i.Key.Brothers)
                            {
                                if (this.HasStrainTo(p) || this.IsVeryCloseTo(p))
                                {
                                    legal = false;
                                    break;
                                }
                            }
                            foreach (Person p in this.Brothers)
                            {
                                if (i.Key.HasStrainTo(p) || i.Key.IsVeryCloseTo(p))
                                {
                                    legal = false;
                                    break;
                                }
                            }
                            if (legal && this.Brothers.Count <= 2 && i.Key.Brothers.Count <= 2)
                            {
                                if (this.Brothers.Count == 0)
                                {
                                    this.Brothers.Add(this);
                                }
                                if (i.Key.Brothers.Count == 0)
                                {
                                    i.Key.Brothers.Add(i.Key);
                                }
                                this.Brothers.Add(i.Key);
                                i.Key.Brothers.Add(this);
                                if (this.Sex)
                                {
                                    Session.Current.Scenario.YearTable.addCreateSisterEntry(Session.Current.Scenario.Date, this, i.Key);
                                    if (this.OnCreateSister != null)
                                    {
                                        this.OnCreateSister(this, i.Key);
                                    }
                                }
                                else
                                {
                                    Session.Current.Scenario.YearTable.addCreateBrotherEntry(Session.Current.Scenario.Date, this, i.Key);
                                    if (this.OnCreateBrother != null)
                                    {
                                        this.OnCreateBrother(this, i.Key);
                                    }
                                }
                            }
                        }
                        else
                        {
                            bool valid = false;
                            if (this.Spouse == null && i.Key.Spouse == null)
                            {
                                valid = true;
                            }
                            if (Session.Current.Scenario.GlobalVariables.AutoMultipleMarriage || i.Key.Status == PersonStatus.Princess)
                            {
                                if (!this.Sex && this.Spouse != null && i.Key.Spouse == null && !i.Key.Hates(this.Spouse) && !this.Spouse.Hates(i.Key))
                                {
                                    valid = true;
                                }
                            }
                            if (valid)
                            {
                                if (this.isLegalFeiZi(i.Key) && i.Key.isLegalFeiZi(this))
                                {
                                    if (this.Spouse == null && i.Key.Status != PersonStatus.Princess)
                                    {
                                        this.Spouse = i.Key;
                                    }
                                    if (i.Key.Spouse == null && this.Status != PersonStatus.Princess)
                                    {
                                        i.Key.Spouse = this;
                                    }

                                    if (this.Spouse != null && !this.Spouse.suoshurenwuList.HasGameObject(i.Key))
                                    {
                                        this.Spouse.suoshurenwuList.Add(i.Key);
                                    }
                                    if (this.Spouse != null && !i.Key.suoshurenwuList.HasGameObject(this.Spouse))
                                    {
                                        i.Key.suoshurenwuList.Add(this.Spouse);
                                    }


                                    Session.Current.Scenario.YearTable.addCreateSpouseEntry(Session.Current.Scenario.Date, this, i.Key);
                                    if (this.OnCreateSpouse != null)
                                    {
                                        this.OnCreateSpouse(this, i.Key);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void huaiyunshijian()
        {
            if (this.huaiyuntianshu < -1)
            {
                //this.huaiyuntianshu++;
                this.huaiyuntianshu += Session.Parameters.DayInTurn;
            }
            else
            {
                if (this.huaiyun)
                {
                    //this.huaiyuntianshu++;
                    this.huaiyuntianshu += Session.Parameters.DayInTurn;
                    //if (this.huaiyuntianshu == 30)
                    if (GameObject.Chance((this.huaiyuntianshu - 20) * 5) && !this.faxianhuaiyun)
                    {
                        ExtensionInterface.call("FoundPregnant", new Object[] { Session.Current.Scenario, this });
                        this.faxianhuaiyun = true;
                        if (this.BelongedFaction != null && this == this.BelongedFaction.Leader)  //女君主自己怀孕
                        {
                            Session.MainGame.mainGameScreen.selfFoundPregnant(this);
                        }
                        else
                        {
                            if (this.Status == PersonStatus.Princess && (this.BelongedFactionWithPrincess != null && !this.Hates(this.BelongedFactionWithPrincess.Leader)))
                            {
                                Session.MainGame.mainGameScreen.faxianhuaiyun(this);
                            }
                            else
                            {
                                Person reporter;
                                if (this.BelongedArchitecture != null)
                                {
                                    reporter = this.BelongedArchitecture.Advisor;
                                    if (reporter != null && (reporter != this || (reporter == this && reporter.Spouse == reporter.BelongedFaction.Leader)))
                                    {
                                        Session.MainGame.mainGameScreen.coupleFoundPregnant(this, reporter);
                                    }
                                }

                            }
                        }

                        Person p = Session.Current.Scenario.Persons.GetGameObject(this.suoshurenwu) as Person;

                        if (this.Status != PersonStatus.Princess || !this.WillHateIfChongxing)
                        {
                            this.AdjustRelation(p, 1f, -10);
                            p.AdjustRelation(this, 1f, -10);

                            if (this.LocationArchitecture == p.LocationArchitecture)
                            {
                                this.AdjustRelation(p, 2f, 0);
                                p.AdjustRelation(this, 2f, 0);
                            }
                        }
                    }
                    else if (GameObject.Chance((this.huaiyuntianshu - 290) * 5))
                    {
                        Person haizifuqin = new Person();
                        Person haizi = new Person();
                        this.huaiyun = false;
                        this.faxianhuaiyun = false;
                        this.huaiyuntianshu = -GameObject.Random(20) - 50;

                        if (this.suoshurenwu == -1)
                        {
                            if (this.BelongedFaction != null)
                            {
                                this.suoshurenwu = this.BelongedFaction.LeaderID;
                            }
                            else
                            {
                                return;
                            }
                        }
                        haizifuqin = Session.Current.Scenario.Persons.GetGameObject(this.suoshurenwu) as Person;

                        if (haizifuqin != null)
                        {
                            int count = 0;
                            do
                            {
                                PersonList origChildren = haizifuqin.meichushengdehaiziliebiao();
                                if (origChildren.Count > 0 && Session.GlobalVariables.BornHistoricalChildren)
                                {
                                    haizi = origChildren[0] as Person;

                                    int ageDeath = haizi.YearDead - haizi.YearBorn;
                                    haizi.YearBorn = Session.Current.Scenario.Date.Year;
                                    haizi.YearAvailable = haizi.YearBorn + Session.GlobalVariables.ChildrenAvailableAge;
                                    haizi.YearDead = haizi.YearBorn + ageDeath;

                                    haizi.father = this.Sex ? haizifuqin : this;
                                    haizi.mother = this.Sex ? this : haizifuqin;

                                    haizi.muqinyingxiangnengli(this);
                                }
                                else
                                {
                                    haizi = Person.createChildren(Session.Current.Scenario.Persons.GetGameObject(this.suoshurenwu) as Person, this);
                                }

                                if (haizi.BaseCommand < 1) haizi.BaseCommand = 1;
                                if (haizi.BaseStrength < 1) haizi.BaseStrength = 1;
                                if (haizi.BaseIntelligence < 1) haizi.BaseIntelligence = 1;
                                if (haizi.BasePolitics < 1) haizi.BasePolitics = 1;
                                if (haizi.BaseGlamour < 1) haizi.BaseGlamour = 1;

                                Session.Current.Scenario.YearTable.addChildrenBornEntry(Session.Current.Scenario.Date, haizifuqin, this, haizi);

                                haizifuqin.TextResultString = haizi.Name;
                                //haizi.AvailableLocation = this.BelongedTroop != null ? this.BelongedTroop.StartingArchitecture.ID : this.BelongedArchitecture.ID;
                                if (this.BelongedFaction != null)
                                {
                                    haizi.AvailableLocation = this.BelongedFaction.Capital.ID;

                                }
                                else if (this.BelongedTroop != null)
                                {
                                    haizi.AvailableLocation = this.BelongedTroop.StartingArchitecture.ID;
                                }
                                else if (this.BelongedArchitecture != null)
                                {
                                    haizi.AvailableLocation = this.BelongedArchitecture.ID;
                                }

                                Session.MainGame.mainGameScreen.xiaohaichusheng(haizi.father, haizi.mother, haizi);

                                haizifuqin.NumberOfChildren++;
                                this.NumberOfChildren++;

                                haizifuqin.childrenList.Add(haizi);
                                this.childrenList.Add(haizi);

                                if (!((bool)Session.GlobalVariables.PersonNaturalDeath) || Session.GlobalVariables.ChildrenAvailableAge <= 0)
                                {
                                    Session.Current.Scenario.haizichusheng(haizi, haizifuqin, this, origChildren.Count > 0);
                                }

                                if (this.Status != PersonStatus.Princess || !this.WillHateIfChongxing)
                                {
                                    this.AdjustRelation(haizifuqin, 3f, -5);
                                    haizifuqin.AdjustRelation(this, 3f, -5);

                                    if (this.LocationArchitecture == haizifuqin.LocationArchitecture)
                                    {
                                        this.AdjustRelation(haizifuqin, 3f, 0);
                                        haizifuqin.AdjustRelation(this, 3f, 0);
                                    }
                                }

                                if (this.Father != null)
                                {
                                    this.Father.IncreaseReputation(haizi.Sex ? 150 : 200);
                                }
                                if (this.Mother != null)
                                {
                                    this.Mother.IncreaseReputation(haizi.Sex ? 200 : 300);
                                }

                                count++;
                            } while ((GameObject.Chance(haizifuqin.multipleChildrenRate) || GameObject.Chance(this.multipleChildrenRate) || GameObject.Chance(1)) && count <= haizifuqin.maxChildren + this.maxChildren);

                            haizifuqin.suoshurenwu = -1;
                        }

                        this.suoshurenwu = -1;

                    }
                }
                else if (this.Spouse != null && !this.huaiyun && !this.Spouse.huaiyun && Session.GlobalVariables.getChildrenRate > 0 &&
                    this.SameLocationAs(this.Spouse) && 
                    (this.Status == PersonStatus.Normal || this.OutsideTask == OutsideTaskKind.后宮) && 
                    (this.Spouse.Status == PersonStatus.Normal || this.Spouse.OutsideTask == OutsideTaskKind.后宮) &&
                    this.isLegalFeiZiExcludeAge(this.Spouse) && this.Spouse.isLegalFeiZiExcludeAge(this) &&
                    this.NumberOfChildren < Session.GlobalVariables.OfficerChildrenLimit &&
                    this.Spouse.NumberOfChildren < Session.GlobalVariables.OfficerChildrenLimit)
                {
                    float relationFactor = this.PregnancyRate(this.Spouse);
                    if (this.Status == PersonStatus.Princess || this.Spouse.Status == PersonStatus.Princess)
                    {
                        relationFactor *= 4;
                    }

                    if (relationFactor > 0 && GameObject.Random((int)
                        (30000.0f / Session.GlobalVariables.getChildrenRate * 20 / Session.Parameters.DayInTurn / relationFactor / (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIExtraPerson))) == 0)
                    {
                        this.suoshurenwu = this.Spouse.ID;
                        this.Spouse.suoshurenwu = this.ID;
                        if (this.Sex)
                        {
                            this.huaiyun = true;
                            this.huaiyuntianshu = 0;
                        }
                        else
                        {
                            this.Spouse.huaiyun = true;
                            this.Spouse.huaiyuntianshu = 0;
                        }
                    }
                } 
                else if (this.Status == PersonStatus.Princess && this.BelongedFactionWithPrincess != null && 
                    this.BelongedFactionWithPrincess.Leader.LocationArchitecture == this.BelongedArchitecture
                    && !this.huaiyun && !this.BelongedFactionWithPrincess.Leader.huaiyun && Session.GlobalVariables.getChildrenRate > 0 &&
                    this.isLegalFeiZiExcludeAge(this.BelongedFactionWithPrincess.Leader) && this.BelongedFactionWithPrincess.Leader.isLegalFeiZiExcludeAge(this) &&
                    this.NumberOfChildren < Session.GlobalVariables.OfficerChildrenLimit &&
                    this.BelongedFactionWithPrincess.Leader.NumberOfChildren < Session.GlobalVariables.OfficerChildrenLimit)
                {
                    if (this.suoshurenwuList.HasGameObject(this.BelongedFactionWithPrincess.Leader) &&
                        this.BelongedFactionWithPrincess.Leader.suoshurenwuList.HasGameObject(this))
                    {
                        float relationFactor = this.PregnancyRate(this.BelongedFactionWithPrincess.Leader) * 4;

                        if (relationFactor > 0 && GameObject.Random((int)
                            (30000.0f / Session.GlobalVariables.getChildrenRate * 20 / Session.Parameters.DayInTurn / relationFactor / (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIExtraPerson))) == 0)
                        {
                            this.suoshurenwu = this.BelongedFactionWithPrincess.Leader.ID;
                            this.BelongedFactionWithPrincess.Leader.suoshurenwu = this.ID;
                            if (this.Sex)
                            {
                                this.huaiyun = true;
                                this.huaiyuntianshu = 0;
                            }
                            else
                            {
                                this.BelongedFactionWithPrincess.Leader.huaiyun = true;
                                this.BelongedFactionWithPrincess.Leader.huaiyuntianshu = 0;
                            }
                        }
                    }
                }
            }
        }

        public float PregnancyRate(Person q)
        {
            float extraRate = 1;
            extraRate *= 1 + this.pregnantChance / 100.0f + q.pregnantChance / 100.0f;

            if (this.Closes(q))
            {
                extraRate += 0.1f;
            }
            if (q.Closes(this))
            {
                extraRate += 0.1f;
            }
            if (q.Ideal == this.Ideal)
            {
                extraRate += 0.2f;
            }
            if (this.Spouse == q || q.Spouse == this)
            {
                extraRate += 1.6f;
            }
            extraRate += Math.Max(Session.Parameters.HateThreshold, q.GetRelation(this)) * 0.0001f * Math.Max(1, 
                (float) Session.Parameters.MaxRelation / (Session.Parameters.MaxRelation + Session.Parameters.VeryCloseThreshold - Math.Max(0, q.GetRelation(this))));
            extraRate += Math.Max(Session.Parameters.HateThreshold, this.GetRelation(q)) * 0.0001f * Math.Max(1, 
                (float) Session.Parameters.MaxRelation / (Session.Parameters.MaxRelation + Session.Parameters.VeryCloseThreshold - Math.Max(0, this.GetRelation(q))));

            if (this.Age > 40 && this.Sex)
            {
                extraRate *= 1 - (this.Age - 40) / 10.0f;
            }
            else if (!this.Sex && this.Age > 40)
            {
                extraRate *= 1.0f / ((this.Age - 40) / 10.0f + 1);
            }
            if (q.Age > 40 && q.Sex)
            {
                extraRate *= 1 - (q.Age - 40) / 10.0f;
            }
            else if (!q.Sex && q.Age > 40)
            {
                extraRate *= 1.0f / ((q.Age - 40) / 10.0f + 1);
            }
            if (this.Age < 16)
            {
                extraRate *= 0.25f * (this.Age - 12);
            }
            if (q.Age < 16)
            {
                extraRate *= 0.25f * (q.Age - 12);
            }
            return extraRate;
        }

        public int Uncruelty
        {
            get
            {
                return Enum.GetNames(typeof(PersonAmbition)).Length - (int)this.Ambition + (int)this.PersonalLoyalty + 1;
            }
        }
        /*
        public int NumberOfChildren
        {
            get
            {
                int cnt = 0;
                foreach (Person p in Session.Current.Scenario.Persons)
                {
                    if ((p.Father == this || p.Mother == this) && (((p.Available || p.YearBorn <= Session.Current.Scenario.Date.Year) && p.Alive) || (p.Available && !p.Alive)))
                    {
                        cnt++;
                    }
                }
                return cnt;
            }
        }
        */

        private PersonList childrenList = null;

        public PersonList ChildrenList
        {
            get
            {
                if (childrenList == null)
                {
                    childrenList = new PersonList();
                    if (Session.Current.Scenario != null && Session.Current.Scenario.Persons != null)
                    {
                        foreach (Person p in Session.Current.Scenario.Persons)
                        {
                            if ((p.Father == this || p.Mother == this) && p.Age >= 0 && (((p.Available || p.YearBorn <= Session.Current.Scenario.Date.Year) && p.Alive) || (p.Available && !p.Alive)))
                            {
                                childrenList.Add(p);
                            }
                        }
                    }
                }
                return childrenList;
            }
        }

        [DataMember]
        public int NumberOfChildren
        {
            get
            {
                
                return (this.ChildrenList.Count);
            }
            set
            {
                this.numberOfChildren = value;
            }
        }

        public void DoJailBreak()
        {
            this.OutsideTask = OutsideTaskKind.无;
            if (this.BelongedFaction != null)
            {
                Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
                CaptiveList candidates = new CaptiveList();
                if ((architectureByPosition != null) && (architectureByPosition.BelongedFaction != null))
                {
                    bool success = false;
                    bool attempted = false;
                    foreach (Captive i in architectureByPosition.Captives)
                    {
                        if (i.CaptiveFaction == this.BelongedFaction)
                        {
                            attempted = true;
                            candidates.Add(i);
                        }
                    }

                    if (candidates.Count > 0) 
                    {
                        Captive c = (Captive)architectureByPosition.Captives[GameObject.Random(architectureByPosition.Captives.Count)];

                        if ((this.InevitableSuccessOfJailBreak || (
                        (GameObject.Random((architectureByPosition.Domination * 10 + architectureByPosition.Morale) * 2) + 300 <=
                                GameObject.Random(this.JailBreakAbility + c.CaptivePerson.CaptiveAbility))
                                )))
                        {
                            if (!GameObject.Chance(architectureByPosition.noEscapeChance) || GameObject.Chance(c.CaptivePerson.captiveEscapeChance))
                            {
                                c.CaptivePerson.AdjustRelation(this, 3f, 0);
                                c.CaptivePerson.AdjustRelation(this.BelongedFaction.Leader, 1f, 0);
                                architectureByPosition.BelongedFaction.Leader.AdjustRelation(this, -2f, -2);
                                architectureByPosition.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -1f, -1);

                                success = true;
                                this.AddStrengthExperience(10);
                                this.AddIntelligenceExperience(10);
                                this.AddTacticsExperience(60);
                                this.IncreaseReputation(20);
                                this.IncreaseOfficerMerit(20);
                                this.BelongedFaction.IncreaseReputation(10 * this.MultipleOfTacticsReputation);
                                this.BelongedFaction.IncreaseTechniquePoint((10 * this.MultipleOfTacticsTechniquePoint) * 100);
                                ExtensionInterface.call("DoJailBreakSuccess", new Object[] { Session.Current.Scenario, this, c });
                                if (this.OnJailBreakSuccess != null)
                                {
                                    this.OnJailBreakSuccess(this, c);
                                }
                                c.CaptiveEscapeNoHint();
                            }
                        }
                    }
                    
                    
                    if (!success)
                    {
                        if (this.OnJailBreakFailed != null)
                        {
                            this.OnJailBreakFailed(this, architectureByPosition);
                        }
                    }
                    if (attempted && architectureByPosition.BelongedFaction != this.BelongedFaction)
                    {
                        CheckCapturedByArchitecture(architectureByPosition);
                    }
                }
            }
        }

        public void DoAssassinate()
        {
            this.OutsideTask = OutsideTaskKind.无;
            if (this.ConvincingPerson != null && this.ConvincingPerson.BelongedArchitecture != null)
            {
                if (this.ConvincingPerson.BelongedFaction == this.BelongedFaction) return;

                Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
                if (architectureByPosition != null && (this.ConvincingPerson.Status == PersonStatus.Normal || this.ConvincingPerson.Status == PersonStatus.NoFaction))
                {
                    float diff;
                    if (this.ConvincingPerson.Status == PersonStatus.Normal)
                    {
                        diff = GameObject.Random(this.AssassinateAbility) - GameObject.Random(architectureByPosition.DefendAssassinateAbility) * 1.5f;
                    }
                    else
                    {
                        diff = GameObject.Random(this.AssassinateAbility) - GameObject.Random(this.ConvincingPerson.AssassinateAbility) * 1.5f;
                    }
                    if (diff > 0)
                    {
                        this.ConvincingPerson.InjureRate -= diff / 1000.0f;
                        if (this.ConvincingPerson.InjureRate < 0.05 && Session.GlobalVariables.OfficerDieInBattleRate > 0 && !this.ConvincingPerson.ImmunityOfDieInBattle)
                        {
                            architectureByPosition.BelongedFaction.Leader.AdjustRelation(this, -20f, -20);
                            architectureByPosition.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -5f, -5);

                            this.AddStrengthExperience(30);
                            this.AddIntelligenceExperience(30);
                            this.AddTacticsExperience(180);
                            this.BelongedFaction.IncreaseTechniquePoint(30 * this.MultipleOfTacticsTechniquePoint * 100);

                            this.ConvincingPerson.illegallyKilled(this.BelongedFaction, this);

                            Session.MainGame.mainGameScreen.PersonAssassinateSuccessKilled(this, this.ConvincingPerson, architectureByPosition);

                            ExtensionInterface.call("Assassinated", new Object[] { Session.Current.Scenario, this, this.ConvincingPerson });

                            Session.Current.Scenario.YearTable.addAssassinateEntry(Session.Current.Scenario.Date, this, this.ConvincingPerson);
                            this.ConvincingPerson.ToDeath(this, this.ConvincingPerson.BelongedFaction);
                        }
                        else if (this.ConvincingPerson.InjureRate < 0.009 * this.Strength && 
                            GameObject.Chance(this.Strength + this.Intelligence - this.ConvincingPerson.Strength - this.ConvincingPerson.Intelligence) && 
                            (architectureByPosition.BelongedFaction == null || GameObject.Chance(100 - (architectureByPosition.Morale / 10))) &&
                            !this.ConvincingPerson.ImmunityOfCaptive)
                        {
                            if (architectureByPosition.BelongedFaction != this.BelongedFaction)
                            {
                                architectureByPosition.BelongedFaction.Leader.AdjustRelation(this, -15f, -15);
                                architectureByPosition.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -4f, -4);
                            }

                            this.AddStrengthExperience(30);
                            this.AddIntelligenceExperience(30);
                            this.AddTacticsExperience(180);
                            this.BelongedFaction.IncreaseTechniquePoint(30 * this.MultipleOfTacticsTechniquePoint * 100);

                            Captive captive = Captive.Create(this.ConvincingPerson, this.BelongedFaction);
                            this.ConvincingPerson.Status = PersonStatus.Captive;
                            foreach (Treasure treasure in this.ConvincingPerson.Treasures.GetList())
                            {
                                this.ConvincingPerson.LoseTreasure(treasure);
                                this.BelongedFaction.Leader.ReceiveTreasure(treasure);
                            }
                            this.ConvincingPerson.LocationArchitecture = this.LocationArchitecture;

                            Session.MainGame.mainGameScreen.PersonAssassinateSuccessCaptured(this, this.ConvincingPerson, architectureByPosition);
                        }
                        else
                        {
                            this.ConvincingPerson.AdjustRelation(this, -diff / 10.0f, -10);
                            architectureByPosition.BelongedFaction.Leader.AdjustRelation(this, -5f, -5);
                            architectureByPosition.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -2f, -2);

                            this.AddStrengthExperience(10);
                            this.AddIntelligenceExperience(10);
                            this.AddTacticsExperience(60);
                            this.BelongedFaction.IncreaseTechniquePoint(10 * this.MultipleOfTacticsTechniquePoint * 100);

                            this.LoseReputationBy(0.005f * this.ConvincingPerson.PersonalLoyalty);
                            this.DecreaseKarma(Math.Max(1, this.ConvincingPerson.Karma / 5));

                            Session.MainGame.mainGameScreen.PersonAssassinateSuccess(this, this.ConvincingPerson, architectureByPosition);
                        }
                    }
                    else
                    {
                        if (diff < -200)
                        {
                            this.InjureRate -= (-diff - 200) / 1000.0f;
                            if (this.InjureRate < 0.05 && Session.GlobalVariables.OfficerDieInBattleRate > 0)
                            {
                                architectureByPosition.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -5f, -5);

                                ExtensionInterface.call("Assassinated", new Object[] { Session.Current.Scenario, this, this.ConvincingPerson });

                                Session.Current.Scenario.YearTable.addReverseAssassinateEntry(Session.Current.Scenario.Date, this, this.ConvincingPerson);
                                this.ToDeath(this, this.BelongedFaction);

                                Session.MainGame.mainGameScreen.PersonAssassinateFailedKilled(this, this.ConvincingPerson, architectureByPosition);
                            }
                        }

                        this.LoseReputationBy(0.005f * this.ConvincingPerson.PersonalLoyalty);
                        this.DecreaseKarma(Math.Max(1, this.ConvincingPerson.Karma / 5));

                        if (this.Alive)
                        {
                            Session.MainGame.mainGameScreen.PersonAssassinateFailed(this, this.ConvincingPerson, architectureByPosition);
                        }
                    }

                    if (!CheckCapturedByArchitecture(architectureByPosition))
                    {
                        if (!CheckCapturedByArchitecture(architectureByPosition))
                        {
                            CheckCapturedByArchitecture(architectureByPosition);
                        }
                    }
                }
            }
        }

        public Person VeryClosePersonInArchitecture
        {
            get
            {
                int maxRel = int.MinValue;
                Person closest = null;
                if (this.BelongedArchitecture != null)
                {
                    foreach (Person p in this.BelongedArchitecture.Persons)
                    {
                        if (this.IsVeryCloseTo(p))
                        {
                            if (this.GetRelation(p) > maxRel)
                            {
                                maxRel = this.GetRelation(p);
                                closest = p;
                            }
                        }
                    }
                    foreach (Person p in this.BelongedArchitecture.MovingPersons)
                    {
                        if (this.IsVeryCloseTo(p))
                        {
                            if (this.GetRelation(p) > maxRel)
                            {
                                maxRel = this.GetRelation(p);
                                closest = p;
                            }
                        }
                    }
                }
                return closest;
            }
        }

        public PersonList VeryClosePersons
        {
            get
            {
                PersonList result = new PersonList();
                if (this.Spouse != null)
                {
                    result.Add(this.Spouse);
                }
                foreach (Person p in this.Brothers)
                {
                    result.Add(p);
                }
                return result;
            }
        }

        public bool CanConvince(Person target)
        {
            bool ConvinceSuccess;
            Architecture architectureByPosition = target.BelongedArchitecture;
            if (target.IsCaptive)
            {
                architectureByPosition = target.BelongedCaptive.LocationArchitecture;
            }

            if (architectureByPosition == null) return false;

            int idealOffset = Person.GetIdealOffset(target, this.BelongedFaction.Leader) - this.ConvinceIdealSkip;

            
            ConvinceSuccess =
                    (
             ((target.IsCaptive && architectureByPosition.IsCaptiveInArchitecture(target.BelongedCaptive))
             || (target.LocationArchitecture == architectureByPosition))
             && (
             (
                 ((target.BelongedFaction == null) || (this.BelongedFaction != target.BelongedFaction))

             )
             && (target.BelongedFaction == null || (target != target.BelongedFaction.Leader) && (target.Loyalty < 100))))
             && (!target.Hates(this.BelongedFaction.Leader))
             && (!Session.GlobalVariables.IdealTendencyValid || (idealOffset <= target.IdealTendency.Offset + (double)this.BelongedFaction.Reputation / Session.Current.Scenario.Parameters.MaxReputationForRecruit * 75))
             && (this.ConvinceAbility - (target.Loyalty * 4) - ((int)target.PersonalLoyalty * 25) + Person.GetIdealAttraction(target, this) * 8 + Person.GetIdealAttraction(this.BelongedFaction.Leader, target) * 8 > 0);

            if (target.BelongedFaction != null)
            {
                int thr = (4 - target.PersonalLoyalty) * 25;
                if (!target.IsCaptive)
                {
                    thr -= 10;
                }
                ConvinceSuccess &= target.Loyalty < thr;
            }

            ConvinceSuccess |= !Session.Current.Scenario.IsPlayer(this.BelongedFaction) && Session.Current.Scenario.IsPlayer(target.BelongedFaction) &&
                Session.GlobalVariables.AIAutoTakePlayerCaptives && target.IsCaptive &&
                (!Session.GlobalVariables.AIAutoTakePlayerCaptiveOnlyUnfull || target.Loyalty < 100);
            
            ConvinceSuccess = ConvinceSuccess && (!this.BelongedFaction.IsAlien || (int)target.PersonalLoyalty < 2 || target.HasStrainTo(this.BelongedFaction.Leader));  //异族只能说服义理为2以下的武将。

            // prohibitedFactionID overrides all.
            if (target.ProhibitedFactionID.ContainsKey(this.BelongedFaction.ID))
            {
                ConvinceSuccess = false;
            }

            //这样配偶和义兄可以无视一切条件强登被登用武将 (当是君主的配偶或者义兄弟)
            ConvinceSuccess |= target.IsVeryCloseTo(this);

            ConvinceSuccess |= target.IsCloseTo(this) && this.BelongedFaction == null;

            return ConvinceSuccess;
        }

        public int CanConvinceChance(Person target)
        {
            if (!CanConvince(target)) return 0;
            Person closest = target.VeryClosePersonInArchitecture;

            return (int)
                (this.ConvinceAbility - (target.Loyalty * 4) - ((int)target.PersonalLoyalty * 25) +
                Person.GetIdealAttraction(this, target) * 8 + Person.GetIdealAttraction(this.BelongedFaction.Leader, target) * 8) / 3 
                + (target.GetRelation(closest) / 5)
                + 1;
        }


        public void DoConvince()
        {
            this.OutsideTask = OutsideTaskKind.无;
            if (this.ConvincingPerson != null && this.BelongedFaction != null)
            {
                Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
                if ((architectureByPosition != null) && (
                    (this.ConvincingPerson.IsCaptive || this.ConvincingPerson.Status == PersonStatus.NoFaction || (architectureByPosition.BelongedFaction != this.BelongedFaction))))
                {
                    bool ConvinceSuccess = this.CanConvince(this.ConvincingPerson);

                    if (ConvinceSuccess)
                    {
                        ConvinceSuccess = GameObject.Chance(this.CanConvinceChance(this.ConvincingPerson));
                    }

                    if (ConvinceSuccess)
                    {
                        this.ConvincePersonSuccess(this.ConvincingPerson);
                    }
                    else
                    {
                        if (this.ConvincingPerson.BelongedFaction != null && this.ConvincingPerson.TempLoyaltyChange < 10) {
                            this.ConvincingPerson.TempLoyaltyChange += GameObject.Random(1, 2);
                        }
                        ExtensionInterface.call("DoConvinceFail", new Object[] { Session.Current.Scenario, this });
                        if (this.OnConvinceFailed != null)
                        {
                            this.OnConvinceFailed(this, this.ConvincingPerson);
                        }
                    }

                    if (architectureByPosition.BelongedFaction != this.BelongedFaction)
                    {
                        CheckCapturedByArchitecture(architectureByPosition);
                    }
                }
            }
        }

        public void ConvincePersonSuccess(Person person)
        {
            if (this.BelongedFaction == null)  //盗贼不能说服武将
            {
                return;
            }
            GameObjects.Faction belongedFaction = null;
            if (person.BelongedFaction != null && this.BelongedFaction != null)
            {
                if (person.ProhibitedFactionID.ContainsKey(person.BelongedFaction.ID))
                {
                    person.ProhibitedFactionID.Remove(person.BelongedFaction.ID);
                }
                person.ProhibitedFactionID.Add(person.BelongedFaction.ID, 90);

                foreach (Person p in person.BelongedFaction.Persons)
                {
                    if (p == person.BelongedFaction.Leader)
                    {
                        p.AdjustRelation(person, -15f - p.PersonalLoyalty * 1.5f, -8);
                        person.AdjustRelation(p, -3f, -2);
                        p.AdjustRelation(this, -7.5f, -4);
                        p.AdjustRelation(this.BelongedFaction.Leader, -2f, -1);
                    }
                    else
                    {
                        p.AdjustRelation(person, -6f - p.PersonalLoyalty * 0.5f, -4);
                    }
                }

                belongedFaction = person.BelongedFaction;
                Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, person.BelongedFaction.ID, -10);
                if (person.BelongedFaction.Leader.HasStrainTo(person))
                {
                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, person.BelongedFaction.ID, -10);
                }
                if (person.BelongedFaction.Leader.HasCloseStrainTo(person))
                {
                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, person.BelongedFaction.ID, -10);
                }
                person.RebelCount++;
                person.Reputation = (int)(person.Reputation * 0.9);
                person.DecreaseKarma(3);
                if (GameObject.Chance(5))
                {
                    this.DecreaseKarma(1);
                }
            }
            if (this.BelongedFaction != null)
            {
                if (person.BelongedFaction != null)
                {
                    Session.Current.Scenario.YearTable.addChangeFactionEntry(Session.Current.Scenario.Date, person, this, belongedFaction, this.BelongedFaction);
                }
                else
                {
                    Session.Current.Scenario.YearTable.addJoinFactionEntry(Session.Current.Scenario.Date, person, this, this.BelongedFaction);
                }
            }

            person.OfficerMerit = (int)(person.OfficerMerit * 0.2);

            Architecture from = null;
            if (person.IsCaptive)
            {
                from = person.BelongedCaptive.LocationArchitecture;
                person.SetBelongedCaptive(null, PersonStatus.Normal);
            }
            else if (person.LocationTroop != null)
            {
                from = person.LocationTroop.StartingArchitecture;
            }
            else
            {
                from = person.LocationArchitecture;
            }

            person.ChangeFaction(this.BelongedFaction);

            if (person.LocationTroop != null)  //单挑中说服敌人
            {
                this.ConvincePersonInTroop(person);
            }
            else if (from == null)
            {
                person.MoveToArchitecture(this.TargetArchitecture, null);
            }
            else
            {
                person.MoveToArchitecture(this.TargetArchitecture, from.ArchitectureArea.Area[0]);
                
            }
            person.Status = PersonStatus.Moving;
            /*if (!(flag || (person.LocationArchitecture == null)))
            {
                person.LocationArchitecture.RemovePerson(person);
            }*/
            this.AddGlamourExperience(40);
            this.IncreaseReputation(40);
            this.IncreaseOfficerMerit(40);
            this.BelongedFaction.IncreaseReputation(20 * this.MultipleOfTacticsReputation);
            this.BelongedFaction.IncreaseTechniquePoint((20 * this.MultipleOfTacticsTechniquePoint) * 100);

            ExtensionInterface.call("DoConvinceSuccess", new Object[] { Session.Current.Scenario, this });
            if (this.OnConvinceSuccess != null)
            {
                this.OnConvinceSuccess(this, person, belongedFaction);
            }
        }

        private void ConvincePersonInTroop(Person person)
        {
            if (person.LocationTroop.PersonCount >= 2)
            {
                if (!person.LocationTroop.Destroyed)
                {
                    Troop locationTroop = person.LocationTroop;

                    person.LocationTroop.Persons.Remove(person);
                    person.LocationTroop = null;
                    locationTroop.RefreshAfterLosePerson();
                    person.MoveToArchitecture(this.BelongedFaction.Capital, locationTroop.Position);

                }
                else
                {
                    person.MoveToArchitecture(this.BelongedFaction.Capital, null);

                }
            }
            else
            {
                if (!person.LocationTroop.Destroyed)
                {
                    person.LocationTroop.ChangeFaction(this.BelongedFaction);
                }
            }
        }

        public void DoDestroy()
        {
            this.OutsideTask = OutsideTaskKind.无;
            Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            if (architectureByPosition != null)
            {
                if (architectureByPosition.BelongedFaction != null && architectureByPosition.BelongedFaction != this.BelongedFaction)
                {
                    architectureByPosition.BelongedFaction.Leader.AdjustRelation(this, -3f, -2);
                    architectureByPosition.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -1f, -1);
                    if ((architectureByPosition.Endurance > 0) && (this.InevitableSuccessOfDestroy || (GameObject.Random(architectureByPosition.Domination * 8) < GameObject.Random(this.DestroyAbility))))
                    {
                        int randomValue = StaticMethods.GetRandomValue(this.DestroyAbility, 12);
                        randomValue = architectureByPosition.DecreaseEndurance(randomValue);
                        int increment = randomValue / 4;
                        this.AddTacticsExperience(increment * 6);
                        this.AddIntelligenceExperience(increment);
                        this.AddStrengthExperience(increment / 2);
                        this.AddCommandExperience(increment / 2);
                        if (GameObject.Chance(10))
                        {
                            this.DecreaseKarma(1);
                        }
                        this.BelongedFaction.IncreaseTechniquePoint((increment * this.MultipleOfTacticsTechniquePoint) * 100);
                        if (architectureByPosition.BelongedFaction != null)
                        {
                            Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, architectureByPosition.BelongedFaction.ID, -5);
                        }
                        if (this.OnDestroySuccess != null)
                        {
                            this.OnDestroySuccess(this, architectureByPosition, randomValue);
                        }
                        architectureByPosition.DecrementNumberList.AddNumber(randomValue, CombatNumberKind.人数, architectureByPosition.Position);
                        ExtensionInterface.call("DoDestroySuccess", new Object[] { Session.Current.Scenario, this, randomValue });
                    }
                    else
                    {
                        ExtensionInterface.call("DoDestroyFail", new Object[] { Session.Current.Scenario, this });
                        if (this.OnDestroyFailed != null)
                        {
                            this.OnDestroyFailed(this, architectureByPosition);
                        }
                    }
                    CheckCapturedByArchitecture(architectureByPosition);
                }
                else if (this.OnDestroyFailed != null)
                {
                    ExtensionInterface.call("DoDestroyFail", new Object[] { Session.Current.Scenario, this });
                    this.OnDestroyFailed(this, architectureByPosition);
                }
            }
        }

        public void DoHouGong()
        {
            this.OutsideTask = OutsideTaskKind.无;

        }

        public void DoGossip()
        {
            this.OutsideTask = OutsideTaskKind.无;
            Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            if (architectureByPosition != null)
            {
                if ((architectureByPosition.BelongedFaction != null) && (this.BelongedFaction != architectureByPosition.BelongedFaction))
                {
                    architectureByPosition.BelongedFaction.Leader.AdjustRelation(this, -3f, -2);
                    architectureByPosition.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -1f, -1);
                    if (this.InevitableSuccessOfGossip || (GameObject.Random(architectureByPosition.Domination * 5) < GameObject.Random(this.GossipAbility)))
                    {
                        architectureByPosition.DamageByGossip(this.GossipAbility);
                        this.AddTacticsExperience(60);
                        this.AddPoliticsExperience(10);
                        this.AddGlamourExperience(10);
                        if (GameObject.Chance(10))
                        {
                            this.DecreaseKarma(1);
                        }
                        this.BelongedFaction.IncreaseTechniquePoint((10 * this.MultipleOfTacticsTechniquePoint) * 100);
                        if (architectureByPosition.BelongedFaction != null)
                        {
                            Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, architectureByPosition.BelongedFaction.ID, -5);
                        }
                        ExtensionInterface.call("DoGossipSuccess", new Object[] { Session.Current.Scenario, this });
                        if (this.OnGossipSuccess != null)
                        {
                            this.OnGossipSuccess(this, architectureByPosition);
                        }
                    }
                    else
                    {
                        ExtensionInterface.call("DoGossipFail", new Object[] { Session.Current.Scenario, this });
                        if (this.OnGossipFailed != null)
                        {
                            this.OnGossipFailed(this, architectureByPosition);
                        }
                    }
                    CheckCapturedByArchitecture(architectureByPosition);
                }
                else
                {
                    ExtensionInterface.call("DoGossipFail", new Object[] { Session.Current.Scenario, this });
                    if (this.OnGossipFailed != null)
                    {
                        this.OnGossipFailed(this, architectureByPosition);
                    }
                }
            }
        }

        public void DoInformation()
        {
            if (this.CurrentInformationKind != null && (!Session.Current.Scenario.IsPlayer(this.BelongedFaction) || (this.InformationAbility > 90 && GameObject.Random(280) < this.InformationAbility)))
            {
                Information information = new Information();
                information.ID = Session.Current.Scenario.Informations.GetFreeGameObjectID();
                information.Level = this.CurrentInformationKind.Level;
                information.Radius = this.CurrentInformationKind.Radius + this.RadiusIncrementOfInformation +
                    (this.InformationAbility + GameObject.Random(100) - 50) / 200;
                information.Position = this.OutsideDestination.Value;
                information.Oblique = this.CurrentInformationKind.Oblique;
                information.DayCost = (int)(240.0 / this.InformationAbility * this.CurrentInformationKind.CostFund *
                    Math.Max(1.0, Session.Current.Scenario.GetDistance(information.Position, this.BelongedArchitecture.Position) / 20.0));

                Session.Current.Scenario.Informations.AddInformation(information);
                this.BelongedArchitecture.AddInformation(information);

                information.Apply();
                this.BelongedArchitecture.DecreaseFund(information.DayCost);

                this.CurrentInformationKind = null;
                this.OutsideTask = OutsideTaskKind.无;

                int increment = (int)(((int)information.Level - 2) * (information.Radius + (information.Oblique ? 1 : 0)));
                this.AddTacticsExperience(increment * 6);
                this.AddIntelligenceExperience(increment);
                this.IncreaseReputation(increment * 2);
                this.IncreaseOfficerMerit(increment * 2);
                this.BelongedFaction.IncreaseReputation(increment * this.MultipleOfTacticsReputation);
                this.BelongedFaction.IncreaseTechniquePoint((increment * this.MultipleOfTacticsTechniquePoint) * 100);
                ExtensionInterface.call("DoInformationSuccess", new Object[] { Session.Current.Scenario, this, information });
                if (this.OnInformationObtained != null)
                {
                    this.OnInformationObtained(this, information);
                }

            }
            else
            {
                if (this.CurrentInformationKind != null)
                {
                    int increment = (int)(((int)this.CurrentInformationKind.Level - 2) * (this.CurrentInformationKind.Radius + (this.CurrentInformationKind.Oblique ? 1 : 0)));
                    this.AddTacticsExperience(increment * 6);
                    this.AddIntelligenceExperience(increment);
                    this.CurrentInformationKind = null;
                }
                this.OutsideTask = OutsideTaskKind.无;
                ExtensionInterface.call("DoInformationFail", new Object[] { Session.Current.Scenario, this });
                if (this.qingbaoshibaishijian != null)
                {
                    this.qingbaoshibaishijian(this);
                }
            }

        }

        public void DoInstigate()
        {
            this.OutsideTask = OutsideTaskKind.无;
            Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            if (architectureByPosition != null)
            {
                if (architectureByPosition.BelongedFaction != this.BelongedFaction)
                {
                    architectureByPosition.BelongedFaction.Leader.AdjustRelation(this, -3f, -2);
                    architectureByPosition.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -1f, -1);
                    if ((architectureByPosition.Domination > 0) && (this.InevitableSuccessOfInstigate || (GameObject.Random((architectureByPosition.Morale * 2) + 200) < GameObject.Random(this.InstigateAbility))))
                    {
                        int randomValue = StaticMethods.GetRandomValue(this.InstigateAbility, 60);
                        randomValue = architectureByPosition.DecreaseDomination(randomValue);
                        int increment = randomValue / 1;
                        this.AddTacticsExperience(increment * 6);
                        this.AddIntelligenceExperience(increment);
                        this.AddGlamourExperience(increment);
                        if (GameObject.Chance(10))
                        {
                            this.DecreaseKarma(1);
                        }
                        this.BelongedFaction.IncreaseTechniquePoint((increment * this.MultipleOfTacticsTechniquePoint) * 100);
                        if (architectureByPosition.BelongedFaction != null)
                        {
                            Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, architectureByPosition.BelongedFaction.ID, -5);
                        }
                        if (this.OnInstigateSuccess != null)
                        {
                            this.OnInstigateSuccess(this, architectureByPosition, randomValue);
                        }
                        architectureByPosition.DecrementNumberList.AddNumber(randomValue, CombatNumberKind.士气, architectureByPosition.Position);
                        ExtensionInterface.call("DoInstigateSuccess", new Object[] { Session.Current.Scenario, this, randomValue });
                    }
                    else
                    {
                        ExtensionInterface.call("DoinstigateFail", new Object[] { Session.Current.Scenario, this });
                        if (this.OnInstigateFailed != null)
                        {
                            this.OnInstigateFailed(this, architectureByPosition);
                        }
                    }
                    CheckCapturedByArchitecture(architectureByPosition);
                }
                else
                {
                    ExtensionInterface.call("DoinstigateFail", new Object[] { Session.Current.Scenario, this });
                    if (this.OnInstigateFailed != null)
                    {
                        this.OnInstigateFailed(this, architectureByPosition);
                    }
                }
            }
        }

        public void DoEnhanceDiplomatic()
        {
            /*
            亲善
            势力间友好度上升=(c*20+max(执行武将政治-执行武将和目标势力君主的相性差/2,0)+100)/10
            系数c取决于执行武将和目标势力君主的关系，两者是义兄弟、配偶或者是目标君主的亲爱武将取2，是目标君主的厌恶武将取-1，否则取1
            如果难度为上级，则效果*0.8；如果难度为超级，则效果为*0.7 (这个部分可调整)
            执行武将名声+50，政治经验+5
            */
            this.OutsideTask = OutsideTaskKind.无;
            this.TargetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            this.OutsideDestination = null;
            if ((this.BelongedFaction != null) && (this.TargetArchitecture.BelongedFaction != null))
            {
                if (!this.TargetArchitecture.BelongedFaction.Leader.Hates(this))
                {
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this, 3f, 2);
                }
                if (!this.Hates(this.TargetArchitecture.BelongedFaction.Leader))
                {
                    this.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, 3f, 2);
                }
                if (!this.TargetArchitecture.BelongedFaction.Leader.Hates(this.BelongedFaction.Leader))
                {
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, 3f, 2);
                }
                //int g = (5 + (int)(5 * this.Glamour / 100));
                int c = 2;
                if (this.TargetArchitecture.BelongedFaction.Leader.IsCloseTo(this))
                {
                    c = 3;
                }
                if (this.TargetArchitecture.BelongedFaction.Leader.Hates(this))
                {
                    c = -2;
                }
                int g = (((c * 10 + Math.Max((this.politics - GetIdealOffset(this, this.TargetArchitecture.BelongedFaction.Leader) / 2), 0)) + 100) / 10);
                int cd = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID);
                if (((cd + g) > Session.GlobalVariables.FriendlyDiplomacyThreshold * 0.95) && cd < Session.GlobalVariables.FriendlyDiplomacyThreshold)
                {
                    g = (int) (Session.GlobalVariables.FriendlyDiplomacyThreshold * 0.95 - cd);
                }
                Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID, g);
                this.TargetArchitecture.Fund += 10000;
                Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.EnhanceDiplomaticRelation, "EnhaneceDiplomaticRelation", "EnhaneceDiplomaticRelation.jpg", "EnhaneceDiplomaticRelation", this.TargetArchitecture.BelongedFaction.Name, true);
                this.TargetArchitecture = this.LocationArchitecture;
                this.AddPoliticsExperience(5);
                this.IncreaseReputation(50);
                this.IncreaseKarma(1);
                this.IncreaseOfficerMerit(50);
            }
        }

        public void DoAllyDiplomatic()
        {
            /*
            同盟
        a、判定值=（金钱/150+执行武将政治-执行武将和目标势力君主的相性差/5+c*10）*t
        b、判定
        如果执行武将没有论客特技，且为执行势力君主为目标势力君主的厌恶武将，则必失败被捕
        如果判定值> 180，则成功
        否则执行武将有论客特技或者判定值>(80-势力间友好度)*2，则失败
        否则失败被捕（注：游戏中的实际效果和失败相同）
        c、结果
        1、成功
        势力间友好度+36 (大于300)
        执行武将功绩+500，政治经验+5
        势力技术点:50
        2、失败
        势力间友好度-10
        执行武将功绩+50，政治经验+1
        3、失败被捕（注：实际效果和失败相同）
            */
            this.OutsideTask = OutsideTaskKind.无;
            this.TargetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            this.OutsideDestination = null;
            if ((this.BelongedFaction != null) && (this.TargetArchitecture.BelongedFaction != null))
            {
                int c = 2;
                if (this.TargetArchitecture.BelongedFaction.Leader.IsCloseTo(this))
                {
                    c = 3;
                }
                if (this.TargetArchitecture.BelongedFaction.Leader.Hates(this))
                {
                    c = -2;
                }
                int g = (c * 10 + (20000 / 150) + this.politics - GetIdealOffset(this, this.TargetArchitecture.BelongedFaction.Leader) / 5);
                int cd = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID);
                if (g > 180)
                {
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this, 6f, 4);
                    this.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, 6f, 4);
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, 9f, 6);
                    this.BelongedFaction.Leader.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, 9f, 6);

                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID, 36);
                    this.TargetArchitecture.Fund += 20000;
                    Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.CreateAlly, "AllyDiplomaticRelation", "AllyDiplomaticRelation.jpg", "AllyDiplomaticRelation", this.TargetArchitecture.BelongedFaction.Name, true);
                    this.TargetArchitecture = this.LocationArchitecture;
                    this.AddPoliticsExperience(50);
                    this.IncreaseKarma(10);
                    this.BelongedFaction.Leader.IncreaseKarma(10);
                    this.TargetArchitecture.BelongedFaction.Leader.IncreaseKarma(10);
                    this.IncreaseReputation(1000);
                    this.BelongedFaction.Leader.IncreaseReputation(1000);
                    this.TargetArchitecture.BelongedFaction.Leader.IncreaseReputation(1000);
                    this.IncreaseOfficerMerit(1000);
                }
                else
                {
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this, -3f, -2);
                    this.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, -3f, -2);
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -4.5f, -3);
                    this.BelongedFaction.Leader.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, -4.5f, -3);

                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID, -10);
                    this.BelongedArchitecture.Fund += 20000;
                    Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.CreateAllyFailed, "AllyDiplomaticRelationFailed", "chuzhan.jpg", "BreakDiplomaticRelation", this.TargetArchitecture.BelongedFaction.Name, true);
                    this.TargetArchitecture = this.LocationArchitecture;
                    this.AddPoliticsExperience(1);
                    this.IncreaseReputation(50);
                    this.IncreaseKarma(1);
                    this.IncreaseOfficerMerit(50);
                }
            }
        }

        private bool GeDiSuccess(Faction sourceFaction, Faction targetFaction, Person shizhe)
        {
            if (sourceFaction == null || targetFaction == null) return false;

            if (sourceFaction.ArchitectureCount < 2) return false;

            if (sourceFaction == targetFaction) return false;

            return true;

        }

        public void DoGeDiDiplomatic() //割地
        {
            this.OutsideTask = OutsideTaskKind.无;
            this.TargetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            Faction targetFaction = this.TargetArchitecture.BelongedFaction;
            this.OutsideDestination = null;

            Architecture a = this.BelongedFaction.GetGeDiArchitecture(targetFaction);

            if (a == null) return;

            if (GeDiSuccess(this.BelongedFaction, targetFaction, this))
            {
                AfterGeDi(this.BelongedFaction, targetFaction, a, this);
            }
        }

        private static void AfterGeDi(Faction sourceFaction, Faction targetFaction, Architecture a, Person shizhe)
        {
            Session.MainGame.mainGameScreen.xianshishijiantupian(shizhe, sourceFaction.Leader.Name, TextMessageKind.GeDi, "GeDiDiplomaticRelation", "GeDiDiplomaticRelation.jpg", "shilimiewang", targetFaction.Name, true);

            foreach (Person p in a.Persons)
            {
                p.MoveToArchitecture(sourceFaction.Capital);
            }
            foreach (Person p in a.MovingPersons)
            {
                p.MoveToArchitecture(sourceFaction.Capital);
            }
            foreach (Military m in a.movableMilitaries)
            {
                a.TransferMilitary(m, sourceFaction.Capital);
            }
            foreach (Military m in sourceFaction.TransferingMilitaries)
            {
                if (m.TargetArchitecture == a)
                {
                    double distance = Session.Current.Scenario.GetDistance(a.ArchitectureArea, sourceFaction.Capital.ArchitectureArea);
                    m.TargetArchitecture = sourceFaction.Capital;
                    //m.ArrivingDays += Math.Max(1, (int)(m.TransferDays(distance) * (1 - a.TroopTransportDayRate)));
                    m.ArrivingDays += Math.Max(1, (int)(m.TransferDays(distance) * (1 - a.TroopTransportDayRate)));
                }
            }
            a.ChangeFaction(targetFaction);

            //停战1年
            if (Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(sourceFaction.ID, targetFaction.ID).Truce < 360)
            {
                Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(sourceFaction.ID, targetFaction.ID).Truce = 360;
            }

            shizhe.TargetArchitecture = shizhe.LocationArchitecture;
            shizhe.AddPoliticsExperience(50);
            shizhe.IncreaseReputation(500);
            shizhe.IncreaseOfficerMerit(20);
        }

        public void DoQuanXiangDiplomatic()
        {
            /*
          劝降
          a、判定成功条件：劝降势力声望>被劝降势力声望；劝降势力兵力大于0且为被劝降势力兵力的五倍；GameObjects.Chance(100-被劝降势力君主义理*25）；与被劝降势力接壤
          GameObjects.Chance(100-被劝降势力君主义理*25），如果被劝降势力君主义理为0，则概率为百分百 ，如此类推。   

          b、判定
          如果执行武将为目标势力君主的厌恶武将，则必失败；

          g = (c * 10 + 执行武将说服能力 + （执行武将政治+智谋）* 冷静度 - (执行武将与被劝降势力君主相性差 * 20 + （被劝降势力君主智谋+政治）*2));
          g > 0，则劝降成功

          c、结果
          1、成功

          执行武将名声+500，政治经验+50

         2、失败
         势力间友好度-100
         执行武将名声+50，政治经验+10
          */
            this.OutsideTask = OutsideTaskKind.无;
            this.TargetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            Faction targetFaction = this.TargetArchitecture.BelongedFaction;
            this.OutsideDestination = null;

            if (QuanXiangChance(this.BelongedFaction, targetFaction, this))
            {
                QuanXiangSuccess(this.BelongedFaction, targetFaction, this);

            }
            else
            {
               QuanXiangFailed(this.BelongedFaction, targetFaction, this);
               return;
            }
            
        }

        private  bool QuanXiangChance(Faction sourceFaction, Faction targetFaction, Person shizhe)
        {
            if (sourceFaction == null || targetFaction == null) return false;

            if (targetFaction == sourceFaction) return false;

            //玩家不能被劝降
            if (Session.Current.Scenario.IsPlayer(targetFaction)) return false;

            if (sourceFaction.Army == 0) return false;

            if (sourceFaction.Reputation <= targetFaction.Reputation) return false;

            if (sourceFaction.Army < targetFaction.Army * 5) return false;

            if (!sourceFaction.adjacentTo(targetFaction)) return false;

            if (targetFaction.Leader.Hates(shizhe)) return false;

            //提高劝降几率
            if (!GameObject.Chance(30)) return false;

            //野心越高越不容易投降
            if (targetFaction.Leader.Ambition > -1 && !GameObject.Chance((100 / (targetFaction.Leader.Ambition + 1)))) return false;

            //城池数量越多越不容易投降
            if (GameObject.Random(targetFaction.ArchitectureCount) != 0) return false;

            int c = targetFaction.Leader.IsCloseTo(shizhe) ? 50 : 10;

            int g = (c * 10 + shizhe.ConvinceAbility + (shizhe.Politics + shizhe.Intelligence) * shizhe.Calmness - ((GetIdealOffset(shizhe, targetFaction.Leader) * 20) + (targetFaction.Leader.Intelligence + targetFaction.Leader.Politics) * targetFaction.Leader.Calmness));

            return g > 0;
        }

        private static void QuanXiangSuccess(Faction sourceFaction, Faction targetFaction, Person shizhe)
        {
            sourceFaction.Leader.AdjustRelation(targetFaction.Leader, 9f, 6);

            //势力合并
            Session.MainGame.mainGameScreen.xianshishijiantupian(shizhe, sourceFaction.Leader.Name, TextMessageKind.QuanXiang, "QuanXiangDiplomaticRelation", "QuanXiangDiplomaticRelation.jpg", "shilimiewang", targetFaction.Name, true);

            Session.Current.Scenario.YearTable.addChangeFactionEntry(Session.Current.Scenario.Date, targetFaction, sourceFaction);

            GameObjectList rebelCandidates = targetFaction.Persons.GetList();

            targetFaction.ChangeFaction(sourceFaction);
            
            targetFaction.AfterChangeLeader(sourceFaction, rebelCandidates, targetFaction.Leader, sourceFaction.Leader);
                
            foreach (Treasure treasure in targetFaction.Leader.Treasures.GetList())
            {
                targetFaction.Leader.LoseTreasure(treasure);
                sourceFaction.Leader.ReceiveTreasure(treasure);

            }
            shizhe.TargetArchitecture = shizhe.LocationArchitecture;
            shizhe.AddPoliticsExperience(50);
            shizhe.IncreaseReputation(500);
            shizhe.IncreaseOfficerMerit(500);
        }

        private static void QuanXiangFailed(Faction sourceFaction, Faction targetFaction, Person shizhe)
        {
            sourceFaction.Leader.AdjustRelation(targetFaction.Leader, -9f, -6);
            targetFaction.Leader.AdjustRelation(sourceFaction.Leader, -9f, -6);

            Session.Current.Scenario.ChangeDiplomaticRelation(sourceFaction.ID, targetFaction.ID, -100);
            shizhe.BelongedArchitecture.Fund += 20000;
            shizhe.TargetArchitecture.Fund += 30000;
            if (Session.Current.Scenario.IsPlayer(shizhe.BelongedFaction))
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(shizhe, sourceFaction.Leader.Name, TextMessageKind.QuanXiangFailed, "QuanXiangDiplomaticRelationFailed", "BreakDiplomaticRelation.jpg", "BreakDiplomaticRelation", targetFaction.Name, true);
            }
            shizhe.TargetArchitecture = shizhe.LocationArchitecture;
            shizhe.AddPoliticsExperience(10);
            shizhe.IncreaseReputation(50);
            shizhe.IncreaseOfficerMerit(50);
        }
        
      

      public void DoTruceDiplomatic()
      {
          /*
          停战协定
          a、判定值=（金钱/400+执行武将政治-执行武将和目标势力君主的相性差/5+c）*t
              系数c取决于执行武将和目标势力君主的关系，两者是义兄弟、配偶或者是目标君主的亲爱武将取20，是目标君主的厌恶武将取-15，否则取10
              t为难度系数，初级上级超级分别为1.0、0.8、0.7
          b、判定
              如果判定值>(80+停战期间/2-势力间友好度/4)，成功
              否则执行武将有论客特技或者判定值>(70+停战期间/2-势力间友好度/4)，则失败
              否则失败被捕（注：游戏中的实际效果和失败相同）
          c、结果
              1、成功
                  执行武将功绩+500，政治经验+5
                  势力技术点:+30
              2、失败
                  势力间友好度-10
                  执行武将功绩+50，政治经验+1
              3、失败被捕（注：实际效果和失败相同）
          */
            this.OutsideTask = OutsideTaskKind.无;
            this.TargetArchitecture = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            this.OutsideDestination = null;
            if ((this.BelongedFaction != null) && (this.TargetArchitecture.BelongedFaction != null))
            {
                int c = 2;
                if (this.TargetArchitecture.BelongedFaction.Leader.IsCloseTo(this))
                {
                    c = 4;
                }
                if (this.TargetArchitecture.BelongedFaction.Leader.Hates(this))
                {
                    c = -3;
                }
                int g = (c * 5 + 50000 / 400 + this.politics - GetIdealOffset(this, this.TargetArchitecture.BelongedFaction.Leader) / 5);
                int cd = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID);
                if (g > (80 - cd / 4))
                {
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this, 3f, 2);
                    this.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, 3f, 2);
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, 3f, 2);
                    this.BelongedFaction.Leader.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, 3f, 2);

                    int di = 10;
                    if (cd + di > 290)
                    {
                        di = 290 - cd;
                    }
                    if (di < 0)
                    {
                        di = 0;
                    }
                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID, di);
                    this.TargetArchitecture.Fund += 50000;
                    Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.Truce, "TruceDiplomaticRelation", "TruceDiplomaticRelation.jpg", "TruceDiplomaticRelation", this.TargetArchitecture.BelongedFaction.Name, true);
                    //设置停战
                    Session.Current.Scenario.SetDiplomaticRelationTruce(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID, 30);
                    this.TargetArchitecture = this.LocationArchitecture;
                    this.AddPoliticsExperience(5);
                    this.IncreaseReputation(500);
                    this.BelongedFaction.Leader.IncreaseReputation(500);
                    this.TargetArchitecture.BelongedFaction.Leader.IncreaseReputation(500);
                    this.IncreaseKarma(5);
                    this.BelongedFaction.Leader.IncreaseKarma(5);
                    this.TargetArchitecture.BelongedFaction.Leader.IncreaseKarma(5);
                    this.IncreaseOfficerMerit(500);
                }
                else
                {
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this, -3f, -2);
                    this.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, -3f, -2);
                    this.TargetArchitecture.BelongedFaction.Leader.AdjustRelation(this.BelongedFaction.Leader, -6f, -4);
                    this.BelongedFaction.Leader.AdjustRelation(this.TargetArchitecture.BelongedFaction.Leader, -6f, -4);

                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, this.TargetArchitecture.BelongedFaction.ID, -10);
                    this.BelongedArchitecture.Fund += 30000;
                    this.TargetArchitecture.Fund += 20000;
                    Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.TruceFailed, "TruceDiplomaticRelationFailed", "chuzhan.jpg", "BreakDiplomaticRelation", this.TargetArchitecture.BelongedFaction.Name, true);
                    this.TargetArchitecture = this.LocationArchitecture;
                    this.AddPoliticsExperience(1);
                    this.IncreaseReputation(50);
                    this.IncreaseOfficerMerit(50);
                }
            }
        }

        private void DoOutsideTask()
        {
            switch (this.OutsideTask)
            {
                case OutsideTaskKind.说服:
                    this.DoConvince();
                    break;

                case OutsideTaskKind.情报:
                    this.DoInformation();
                    break;
                    /*
                case OutsideTaskKind.间谍:
                    this.DoSpy();
                    break;
                    */
                case OutsideTaskKind.破坏:
                    this.DoDestroy();
                    break;

                case OutsideTaskKind.煽动:
                    this.DoInstigate();
                    break;

                case OutsideTaskKind.流言:
                    this.DoGossip();
                    break;

                case OutsideTaskKind.搜索:
                    this.DoSearch();
                    break;

                case OutsideTaskKind.技能:
                    this.DoStudySkill();
                    break;

                case OutsideTaskKind.称号:
                    this.DoStudyTitle();
                    break;

                case OutsideTaskKind.特技:
                    this.DoStudyStunt();
                    break;

                case OutsideTaskKind.后宮:
                    this.DoHouGong();
                    break;

                case OutsideTaskKind.亲善:
                    this.DoEnhanceDiplomatic();
                    break;

                case OutsideTaskKind.结盟:
                    this.DoAllyDiplomatic();
                    break;

                case OutsideTaskKind.停战:
                    this.DoTruceDiplomatic();
                    break;

                case OutsideTaskKind.劫狱:
                    this.DoJailBreak();
                    break;

                case OutsideTaskKind.暗杀:
                    this.DoAssassinate();
                    break;

                case OutsideTaskKind.劝降:
                    this.DoQuanXiangDiplomatic();
                    break ;

                case OutsideTaskKind.割地:
                    this.DoGeDiDiplomatic();
                    break;
            }
        }

        

        public void DoSearch()
        {
            this.OutsideTask = OutsideTaskKind.无;
            if ((this.TargetArchitecture != null) && (this.TargetArchitecture.BelongedFaction == this.BelongedFaction))
            {
                SearchResultPack pack = new SearchResultPack();
                bool flag = false;
#pragma warning disable CS0219 // The variable 'flag2' is assigned but its value is never used
                bool flag2 = false;
#pragma warning restore CS0219 // The variable 'flag2' is assigned but its value is never used
                bool flag3 = false;
                bool flag4 = false;
                if (this.InevitableSuccessOfSearch)
                {
                    pack.Result = (SearchResult)(GameObject.Random(Enum.GetNames(typeof(SearchResult)).Length - 1) + 1);
                }
                else
                {
                    pack.Result = (SearchResult)GameObject.Random(Enum.GetNames(typeof(SearchResult)).Length);
                }
                switch (pack.Result)
                {
                    case SearchResult.资金:
                        flag = this.DoSearchFund(pack);
                        break;

                    case SearchResult.粮草:
                        flag = this.DoSearchFood(pack);
                        break;

                    case SearchResult.技巧:
                        flag = this.DoSearchTechnique(pack);
                        break;
                        /*
                    case SearchResult.间谍:
                        flag = this.DoSearchSpy(pack);
                        flag2 = flag;
                        break;
                        */
                    case SearchResult.隐士:
                        flag = this.DoSearchPerson(pack);
                        flag3 = flag;
                        break;

                    case SearchResult.宝物:
                        flag = this.DoSearchTreasure(pack);
                        flag4 = flag;
                        break;
                }
                if (!flag && this.InevitableSuccessOfSearch)
                {
                    switch (GameObject.Random(3))
                    {
                        case 0:
                            pack.Result = SearchResult.资金;
                            flag = this.DoSearchFund(pack);
                            break;

                        case 1:
                            pack.Result = SearchResult.粮草;
                            flag = this.DoSearchFood(pack);
                            break;

                        case 2:
                            pack.Result = SearchResult.技巧;
                            flag = this.DoSearchTechnique(pack);
                            break;
                    }
                }
                if (flag)
                {
                    this.AddTacticsExperience(60);
                    this.AddIntelligenceExperience(5);
                    this.AddPoliticsExperience(5);
                    this.AddGlamourExperience(10);
                    this.IncreaseReputation(20);
                    this.IncreaseOfficerMerit(20);
                    this.BelongedFaction.IncreaseReputation(10 * this.MultipleOfTacticsReputation);
                    this.BelongedFaction.IncreaseTechniquePoint((10 * this.MultipleOfTacticsTechniquePoint) * 100);
                    if (this.OnSearchFinished != null)
                    {
                        this.OnSearchFinished(this, this.TargetArchitecture, pack);
                    }
                }
                else
                {
                    pack.Result = SearchResult.无;
                    if (this.OnSearchFinished != null)
                    {
                        this.OnSearchFinished(this, this.TargetArchitecture, pack);
                    }
                }
                ExtensionInterface.call("DoSearch", new Object[] { Session.Current.Scenario, this, pack });
            }
        }

        private bool DoSearchFood(SearchResultPack pack)
        {
            if (this.InevitableSuccessOfSearch || (GameObject.Random(this.TargetArchitecture.Morale + this.SearchAbility) >= GameObject.Random(0x3e8)))
            {
                pack.Number = this.SearchAbility * 20;
                pack.Number = (pack.Number / 2) + GameObject.Random(pack.Number);
                this.TargetArchitecture.IncreaseFood(pack.Number);
                return true;
            }
            return false;
        }

        private bool DoSearchFund(SearchResultPack pack)
        {
            if (this.InevitableSuccessOfSearch || (GameObject.Random(this.TargetArchitecture.Morale + this.SearchAbility) >= GameObject.Random(0x3e8)))
            {
                pack.Number = StaticMethods.GetRandomValue(this.SearchAbility, 2);
                pack.Number = (pack.Number / 2) + GameObject.Random(pack.Number);
                this.TargetArchitecture.IncreaseFund(pack.Number);
                return true;
            }
            return false;
        }

        private bool DoSearchPerson(SearchResultPack pack)
        {
            if (this.InevitableSuccessOfSearch || (GameObject.Random(this.TargetArchitecture.Morale + this.SearchAbility) >= GameObject.Random(0x3e8)))
            {
                foreach (Person person in Session.Current.Scenario.Persons)
                {
                    if (((((!person.Available && person.Alive) && (person.YearAvailable <= Session.Current.Scenario.Date.Year)) && GameObject.Chance(20)) && (person.AvailableLocation == this.TargetArchitecture.ID)) && ((((((Session.GlobalVariables.CommonPersonAvailable && (person.ID >= 0)) && (person.ID <= 0x1b57)) || ((Session.GlobalVariables.AdditionalPersonAvailable && (person.ID >= 0x1f40)) && (person.ID <= 0x2327))) || ((Session.GlobalVariables.PlayerPersonAvailable && (person.ID >= 0x2328)) && (person.ID <= 0x270f))) && !Session.Current.Scenario.PreparedAvailablePersons.HasGameObject(person)) && person.BeAvailable()))
                    {
                        pack.FoundPerson = person;
                        return true;
                    }
                }

                if (GameObject.Random((this.BelongedFaction.PersonCount - 50) / 50) == 0)
                {
                    if (GameObject.Random((int)(10000 * Math.Pow(this.BelongedFaction.PersonCount, Session.Parameters.SearchPersonArchitectureCountPower))) <
                        Session.GlobalVariables.CreateRandomOfficerChance * 100)
                    { 
                        pack.FoundPerson = Person.createPerson(this.TargetArchitecture, this, true, false);
                        pack.FoundPerson.Ideal = (this.BelongedFaction.Leader.Ideal + GameObject.Random(pack.FoundPerson.IdealTendency.Offset * 2 + 1) - pack.FoundPerson.IdealTendency.Offset) % 150;
                        return true;
                    }
                    else if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction) &&
                        GameObject.Random((int)(10000 * Math.Pow(this.BelongedFaction.PersonCount, Session.Parameters.SearchPersonArchitectureCountPower))) <
                        Session.GlobalVariables.CreateRandomOfficerChance * 100 * (Session.Parameters.AIExtraPerson - 1))
                    {
                        pack.FoundPerson = Person.createPerson(this.TargetArchitecture, this, true, true);

                        GameObjectList ideals = Session.Current.Scenario.GameCommonData.AllIdealTendencyKinds;
                        IdealTendencyKind minIdeal = null;
                        foreach (IdealTendencyKind itk in ideals)
                        {
                            if (minIdeal == null || itk.Offset < minIdeal.Offset)
                            {
                                minIdeal = itk;
                            }
                        }

                        pack.FoundPerson.IdealTendency = minIdeal;
                        pack.FoundPerson.Ideal = (this.BelongedFaction.Leader.Ideal + GameObject.Random(minIdeal.Offset * 2 + 1) - minIdeal.Offset) % 150;

                        return true;
                    }
                }
                    
            }
            return false;
        }

        /*
        private bool DoSearchSpy(SearchResultPack pack)
        {
            if (this.TargetArchitecture.HasSpy && (this.InevitableSuccessOfSearch || (GameObject.Random(this.SearchAbility + 500) > 500)))
            {
                SpyPack sp = this.TargetArchitecture.SpyPacks[GameObject.Random(this.TargetArchitecture.SpyPacks.Count)];
                if (sp.SpyPerson.BelongedFaction != this.BelongedFaction)
                {
                    if (sp.SpyPerson.BelongedFaction != null)
                    {
                        Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedFaction.ID, sp.SpyPerson.BelongedFaction.ID, -10);
                    }
                    if (this.OnSpyFound != null)
                    {
                        this.OnSpyFound(this, sp.SpyPerson);
                    }
                    pack.FoundPerson = sp.SpyPerson;
                }
                this.TargetArchitecture.RemoveSpyPack(sp);
                return true;
            }
            return false;
        }
        */

        private bool DoSearchTechnique(SearchResultPack pack)
        {
            if (this.InevitableSuccessOfSearch || (GameObject.Random(this.TargetArchitecture.Morale + this.SearchAbility) >= GameObject.Random(0x3e8)))
            {
                pack.Number = this.SearchAbility * 2;
                pack.Number = (pack.Number / 2) + GameObject.Random(pack.Number);
                return true;
            }
            return false;
        }

        private bool DoSearchTreasure(SearchResultPack pack)
        {
            if (this.InevitableSuccessOfSearch || (GameObject.Random(this.TargetArchitecture.Morale + this.SearchAbility) >= GameObject.Random(0x3e8)))
            {
                foreach (Treasure treasure in Session.Current.Scenario.Treasures.GetRandomList())
                {
                    if (((!treasure.Available && (treasure.BelongedPerson == null)) && (treasure.HidePlace == this.TargetArchitecture || treasure.HidePlace == null)) && (treasure.AppearYear <= Session.Current.Scenario.Date.Year))
                    {
                        if (GameObject.Random(treasure.Worth) <= GameObject.Random(Session.Parameters.FindTreasureChance))
                        {
                            treasure.Available = true;
                            //this.ReceiveTreasure(treasure);
                            this.BelongedFaction.Leader.ReceiveTreasure(treasure);
                            if (this.OnTreasureFound != null)
                            {
                                this.OnTreasureFound(this, treasure);
                            }
                        }
                        break;
                    }
                }
            }
            return false;
        }

        /*
        public void DoSpy()
        {
            this.OutsideTask = OutsideTaskKind.无;
            Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(this.OutsideDestination.Value);
            if (architectureByPosition != null)
            {
                if ((architectureByPosition.BelongedFaction != null) && (this.BelongedFaction != architectureByPosition.BelongedFaction))
                {
                    if (this.InevitableSuccessOfSpy || (GameObject.Random((architectureByPosition.Domination * (20 - architectureByPosition.AreaCount)) - architectureByPosition.Commerce) < this.SpyAbility))
                    {
                        architectureByPosition.AddSpyPack(this, this.SpyDays);
                        this.AddTacticsExperience(60);
                        this.AddIntelligenceExperience(10);
                        this.AddStrengthExperience(5);
                        this.AddGlamourExperience(5);
                        this.IncreaseReputation(20);
                        this.BelongedFaction.IncreaseReputation(10 * this.MultipleOfTacticsReputation);
                        this.BelongedFaction.IncreaseTechniquePoint((10 * this.MultipleOfTacticsTechniquePoint) * 100);
                        ExtensionInterface.call("DoSpySuccess", new Object[] { Session.Current.Scenario, this, this.SpyDays });
                        if (this.OnSpySuccess != null)
                        {
                            this.OnSpySuccess(this, architectureByPosition);
                        }
                    }
                    else
                    {
                        ExtensionInterface.call("DoSpyFail", new Object[] { Session.Current.Scenario, this });
                        if (this.OnSpyFailed != null)
                        {
                            this.OnSpyFailed(this, architectureByPosition);
                        }
                    }
                }
                else if (this.OnSpyFailed != null)
                {
                    ExtensionInterface.call("DoSpyFail", new Object[] { Session.Current.Scenario, this });
                    if (this.OnSpyFailed != null)
                    {
                        this.OnSpyFailed(this, architectureByPosition);
                    }
                }
            }
        }
        */

        public int zhenzaiWeighing
        {
            get
            {
                return zhenzaiAbility;
            }
        }
        public bool CheckCapturedByArchitecture(Architecture a)
        {
            bool captured = false;
            if (a.BelongedFaction != null && a.BelongedFaction != this.BelongedFaction)
            {
                this.ApplySkills(true);
                this.ApplyTitles(true);
                this.ApplyAllTreasures(true);
                if (!this.ImmunityOfCaptive && 
                    (GameObject.Random(a.Domination * 10 + a.Morale) + 200 > GameObject.Random(this.CaptiveAbility) * 60 
                    || GameObject.Chance((int) (a.captureChance * (Session.Current.Scenario.IsPlayer(a.BelongedFaction) ? 1 : Session.Parameters.AIExtraPerson)))))
                {
                    this.ArrivingDays = 0;
                    this.TargetArchitecture = null;
                    this.TaskDays = 0;
                    this.OutsideTask = OutsideTaskKind.无;
                    Captive captive = Captive.Create(this, a.BelongedFaction);
                    this.Status = PersonStatus.Captive;
                    foreach (Treasure treasure in this.Treasures.GetList())
                    {
                        this.LoseTreasure(treasure);
                        a.BelongedFaction.Leader.ReceiveTreasure(treasure);
                    }
                    this.LocationArchitecture = a;
                    ExtensionInterface.call("CapturedByArchitecture", new Object[] { Session.Current.Scenario, this, a });
                    if (this.OnCapturedByArchitecture != null)
                    {
                        this.OnCapturedByArchitecture(this, a);
                    }
                    captured = true;
                }
                this.PurifySkills(true);
                this.PurifyTitles(true);
                this.PurifyAllTreasures(true);
            }
            return captured;

        }

        public int StudyableSkillCount
        {
            get
            {
                int result = 0;
                foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
                {
                    if ((this.Skills.GetSkill(skill.ID) == null) && skill.CanLearn(this))
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        public int StudyableStuntCount
        {
            get
            {
                int result = 0;
                foreach (Stunt stunt in Session.Current.Scenario.GameCommonData.AllStunts.Stunts.Values)
                {
                    if (this.Stunts.GetStunt(stunt.ID) == null && stunt.IsLearnable(this))
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        public String TitleNames
        {
            get
            {
                String s = "";
                foreach (Title t in this.Titles)
                {
                    s += t.Name + " ";
                }
                return s;
            }
        }

        public String TitleDetailedNames
        {
            get
            {
                String s = "";
                foreach (Title t in this.Titles)
                {
                    s += t.DetailedName + " ";
                }
                return s;
            }
        }

        public int StudyableTitleCount
        {
            get
            {
                return this.GetStudyTitleList().Count;
            }
        }

        public int StudyableHigherLevelTitleCount
        {
            get
            {
                return this.HigherLevelLearnableTitle.Count;
            }
        }

        public String StudyableHigherLevelTitle
        {
            get
            {
                Dictionary<TitleKind, int> title = new Dictionary<TitleKind, int>();
                foreach (Title candidate in Session.Current.Scenario.GameCommonData.AllTitles.Titles.Values)
                {
                    HashSet<TitleKind> hasKind = new HashSet<TitleKind>();
                    foreach (Title t in this.Titles)
                    {
                        if (t.Kind.Equals(candidate.Kind) && candidate.Level > t.Level && candidate.CanLearn(this))
                        {
                            if (title.ContainsKey(candidate.Kind))
                            {
                                title[candidate.Kind]++;
                            }
                            else
                            {
                                title.Add(candidate.Kind, 1);
                            }
                        }
                        hasKind.Add(t.Kind);
                    }
                    if (!hasKind.Contains(candidate.Kind) && candidate.CanLearn(this))
                    {
                        if (title.ContainsKey(candidate.Kind))
                        {
                            title[candidate.Kind]++;
                        }
                        else
                        {
                            title.Add(candidate.Kind, 1);
                        }
                    }
                }

                String s = "";
                foreach (KeyValuePair<TitleKind, int> i in title)
                {
                    s += i.Key.Name + i.Value + "個";
                }
                return s;
            }
        }

        public void DoStudySkill()
        {
            this.OutsideTask = OutsideTaskKind.无;
            int num = 0;
            string skillString = "";
            foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
            {
                if (((this.Skills.GetSkill(skill.ID) == null) && skill.CanLearn(this)) && (GameObject.Random((skill.Level * 2) + 8) >= ((skill.Level + num) * 2 - Session.Parameters.LearnSkillSuccessRate)))
                {
                    this.Skills.AddSkill(skill);
                    skill.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Skill, skill.ID, false);
                    skillString = skillString + "•" + skill.Name;
                    num++;
                    ExtensionInterface.call("StudySkill", new Object[] { Session.Current.Scenario, this, skill });
                }
            }
            if (this.OnStudySkillFinished != null && this.ManualStudy)
            {
                this.OnStudySkillFinished(this, skillString, num > 0);

            }
            this.ManualStudy = false;
        }

        public void DoStudyStunt()
        {
            this.OutsideTask = OutsideTaskKind.无;
            if (this.StudyingStunt != null)
            {
                if (GameObject.Chance(Session.Parameters.LearnStuntSuccessRate))
                {
                    this.Stunts.AddStunt(this.StudyingStunt);
                    ExtensionInterface.call("StudyStuntSuccess", new Object[] { Session.Current.Scenario, this, this.StudyingStunt });
                    if (this.OnStudyStuntFinished != null && this.ManualStudy)
                    {
                        this.OnStudyStuntFinished(this, this.StudyingStunt, true);
                    }
                }
                else
                {
                    ExtensionInterface.call("StudyStuntFail", new Object[] { Session.Current.Scenario, this, this.StudyingStunt });
                    if (this.OnStudyStuntFinished != null && this.ManualStudy)
                    {
                        this.OnStudyStuntFinished(this, this.StudyingStunt, false);
                    }
                }
                this.StudyingStunt = null;
            }
            this.ManualStudy = false;
        }
        
        public bool HasHigherLevelTitle(Title title)
        {
            List<Title> oldTitles = new List<Title>(this.RealTitles);
            foreach (Title t in oldTitles)
            {
                if (t.Kind.Equals(title.Kind) && t.Level >= title.Level)
                {
                    return true;
                }
            }
            return false;
        }
        
        public void LearnTitle(Title title)
        {
            List<Title> oldTitles = new List<Title>(this.RealTitles);
            foreach (Title t in oldTitles)
            {
                if (t.Kind.Equals(title.Kind))
                {
                    t.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Title, t.ID, false);
                    this.RealTitles.Remove(t);
                }
            }
            this.RealTitles.Add(title);
            title.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Title, title.ID, false);
            Session.Current.Scenario.YearTable.addObtainedTitleEntry(Session.Current.Scenario.Date, this, title);
        }

        public void LoseTitle()
        {
            List<Title> temp = new List<Title>(this.RealTitles);
            foreach (Title t in temp)
            {
                if (t.LoseConditions.Count > 0  && t.WillLose(this))
                {
                    t.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Title, t.ID, false);

                    this.RealTitles.Remove(t);
                }

            }
        }

        public void AwardTitle(Title title)
        {
            List<Title> oldTitles = new List<Title>(this.RealTitles);
            foreach (Title t in oldTitles)
            {
                if (t.Kind.Equals(title.Kind))
                {
                    t.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Title, t.ID, false);
                    this.RealTitles.Remove(t);
                }
            }
            this.RealTitles.Add(title);
            title.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Title, title.ID, false);
            if (Session.Current.Scenario.IsPlayer(this.BelongedFaction))
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this.BelongedFaction.Leader, this.Name, "AwardTitle", "AwardTitle.jpg", "AwardTitle", title.Name, true);
            }
            Session.Current.Scenario.YearTable.addAwardTitleEntry(Session.Current.Scenario.Date, this, title);
        }

        public void RemoveTitle(Title title)
        {
            title.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Title, title.ID, false);
            this.RealTitles.Remove(title);
        }

        public void DoStudyTitle()
        {
            this.OutsideTask = OutsideTaskKind.无;
            if (this.StudyingTitle != null)
            {
                if (GameObject.Random((this.StudyingTitle.Level * 2) + 8) + this.StudyingTitle.Kind.SuccessRate >= (this.StudyingTitle.Level * 2 - Session.Parameters.LearnTitleSuccessRate))
                {
                    this.PurifyTitles(false);

                    foreach (Title t in this.RealTitles)
                    {
                        if (t.Kind == this.StudyingTitle.Kind)
                        {
                            t.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Title, t.ID, false);
                            this.RealTitles.Remove(t);
                            break;
                        }

                    }
                    this.RealTitles.Add(this.StudyingTitle);
                    this.StudyingTitle.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Title, this.StudyingTitle.ID, false);

                    Session.Current.Scenario.YearTable.addObtainedTitleEntry(Session.Current.Scenario.Date, this, this.StudyingTitle);

                    ExtensionInterface.call("StudyTitleSuccess", new Object[] { Session.Current.Scenario, this, this.StudyingTitle });
                    if (this.OnStudyTitleFinished != null && this.ManualStudy)
                    {
                        this.OnStudyTitleFinished(this, this.StudyingTitle, true);
                    }
                }
                else
                {
                    ExtensionInterface.call("StudyTitleFail", new Object[] { Session.Current.Scenario, this, this.StudyingTitle });
                    if (this.OnStudyTitleFinished != null && this.ManualStudy)
                    {
                        this.OnStudyTitleFinished(this, this.StudyingTitle, false);
                    }
                }
                this.StudyingTitle = null;
                this.ManualStudy = false;
            }
        }

        public GameObjectList GetHirableFactionList()
        {
            GameObjectList list = new GameObjectList();
            foreach (GameObjects.Faction faction in Session.Current.Scenario.Factions)
            {
                if (this.IsHirable(faction))
                {
                    list.Add(faction);
                }
            }
            return list;
        }

        public static float GetIdealAttraction(Person target, Person src)
        {
            return GetIdealAttraction(target, src, 1.0f);
        }

        public static float GetIdealAttraction(Person target, Person src, float idealFactor)
        {
            float v = 0;
            v += (-Person.GetIdealOffset(target, src) * 0.6f + src.IdealTendency.Offset * 0.2f + target.IdealTendency.Offset * 0.2f) * idealFactor;
            v += target.Glamour / 5.0f - 10.0f;
            v -= Math.Abs(target.Karma - src.Karma) / 2.5f;
            v += (float) (Math.Sign(target.Karma) * Math.Sqrt(Math.Abs(target.Karma)));

            if (Session.Current.Scenario.huangdisuozaijianzhu() != null)
            {
                v -= (Math.Abs(target.ValuationOnGovernment - src.ValuationOnGovernment) - 1) * 5;
            }
            switch (src.Qualification)
            {
                case PersonQualification.义理:
                    v += (target.PersonalLoyalty - 2) * 10;
                    break;
                case PersonQualification.功绩:
                    v += Math.Max(-20, Math.Min(20, (target.OfficerMerit - src.OfficerMerit) / 750.0f));
                    break;
                case PersonQualification.名声:
                    v += Math.Max(-20, Math.Min(20, (target.Reputation - src.Reputation) / 1000.0f));
                    break;
                case PersonQualification.能力:
                    v += Math.Max(-20, Math.Min(20, (target.UnalteredUntiredMerit - src.UnalteredUntiredMerit) / 7500.0f));
                    break;
                case PersonQualification.任意:
                    break;
            }

            if (src.Ideal == target.Ideal)
            {
                v += 2;
            }
            if (src.Closes(target))
            {
                v += 10;
            }
            if (src.HasStrainTo(target))
            {
                v += 5;
            }
            if (src.HasCloseStrainTo(target))
            {
                v += 20;
            }
            if (src.Spouse == target)
            {
                v += 30;
            }
            if (src.Brothers.GameObjects.Contains(target))
            {
                v += 40;
            }
            if (src.Hates(target))
            {
                v -= 50;
            }

            return v;
        }

        public static int GetIdealOffset(Person p1, Person p2)
        {
            int num = Math.Abs((int)(p1.Ideal - p2.Ideal));
            if (num > 75)
            {
                num = Math.Abs(150 - num);
            }
            return num;
        }

        public bool YoukenengChuangjianXinShili()
        {
            if (this.IsCaptive || this.LocationArchitecture == null ||
                (this.Status != PersonStatus.Normal && this.Status != PersonStatus.NoFaction && this.Status != PersonStatus.Moving))
            {
                return false;
            }

            if (this.BelongedFaction == null)
            {

                return true;
            }
            else
            {
                if (this == this.BelongedFaction.Leader) return false;

                if (this.Hates(this.BelongedFaction.Leader)) return true;

                if (this.Father == this.BelongedFaction.Leader) return false;  //隐含父亲活着，下同。
                if (this.Mother == this.BelongedFaction.Leader) return false;
                if (this.IsCloseTo(this.BelongedFaction.Leader)) return false;
                if (this.Strain == this.BelongedFaction.Leader.Strain) return false;  //同一血缘不能独立，孙子不能从爷爷手下独立。
                return true;
            }

        }

        public GameObjectList GetSkillList()
        {
            return this.Skills.GetSkillList();
        }

        public GameObjectList GetStudySkillList()
        {
            this.StudySkillList.Clear();
            foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
            {
                if ((this.Skills.GetSkill(skill.ID) == null) && skill.CanLearn(this))
                {
                    this.StudySkillList.Add(skill);
                }
            }
            return StudySkillList;
        }

        public GameObjectList GetStuntList()
        {
            return this.Stunts.GetStuntList();
        }

        public GameObjectList GetStudyStuntList()
        {
            this.StudyStuntList.Clear();
            foreach (Stunt stunt in Session.Current.Scenario.GameCommonData.AllStunts.Stunts.Values)
            {
                if ((this.Stunts.GetStunt(stunt.ID) == null) && stunt.IsLearnable(this))
                {
                    this.StudyStuntList.Add(stunt);
                }
            }
            return StudyStuntList;
        }

        public GameObjectList GetTitleList()
        {
            GameObjectList result = new GameObjectList();
            foreach (Title t in this.Titles)
            {
                result.Add(t);
            }
            return result;
        }

        public GameObjectList GetStudyTitleList()
        {
            this.StudyTitleList.Clear();
            foreach (Title title in Session.Current.Scenario.GameCommonData.AllTitles.Titles.Values)
            {
                if (!this.RealTitles.Contains(title) && title.CanLearn(this))
                {
                    this.StudyTitleList.Add(title);
                }
            }
            return StudyTitleList;
        }

        public GameObjectList GetAppointableTitleList()
        {
            this.AppointableTitleList.Clear();
            foreach (Title title in Session.Current.Scenario.GameCommonData.AllTitles.Titles.Values)
            {
                if (!this.RealTitles.Contains(title) && !this.HasHigherLevelTitle(title) && title.ManualAward && title.CanLearn(this,true))        
                {
                    this.AppointableTitleList.Add(title);
                }
            }
            return AppointableTitleList;
        }

        public GameObjectList RecallableTitleList()
        {
            GameObjectList list = new GameObjectList();
            foreach (Title title in this.Titles)
            {
                if (title.Kind.Recallable && title.AutoLearn > 0)
                {
                    list.Add(title);
                }
            }
            return list;
        }

        public PersonRelationValueList GetPersonRelationList()
        {
            PersonRelationValueList list = new PersonRelationValueList();
            foreach (KeyValuePair<Person, int> i in relations) 
            {
                if (i.Key.Alive && i.Key.Available)
                {
                    list.Add(new PersonRelationValue(this, i.Key, i.Value));
                }
            }
            return list;
        }

        public int GetWorkAbility(ArchitectureWorkKind workKind)
        {
            switch (workKind)
            {
                case ArchitectureWorkKind.无:
                    return 0;

                case ArchitectureWorkKind.赈灾:
                    return this.zhenzaiAbility;

                case ArchitectureWorkKind.农业:
                    return this.AgricultureAbility;

                case ArchitectureWorkKind.商业:
                    return this.CommerceAbility;

                case ArchitectureWorkKind.技术:
                    return this.TechnologyAbility;

                case ArchitectureWorkKind.统治:
                    return this.DominationAbility;

                case ArchitectureWorkKind.民心:
                    return this.MoraleAbility;

                case ArchitectureWorkKind.耐久:
                    return this.EnduranceAbility;

                case ArchitectureWorkKind.训练:
                    return this.TrainingAbility;
            }
            return 0;
        }

        public void GoForConvince(Person person)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal) 
            {
                this.OutsideTask = OutsideTaskKind.说服;
                this.ConvincingPerson = person;
                this.LocationArchitecture.DecreaseFund(this.LocationArchitecture.ConvincePersonFund);
                this.GoToDestinationAndReturn(this.OutsideDestination.Value);
                this.TaskDays = (this.ArrivingDays + 1) / 2;
                ExtensionInterface.call("GoForConvince", new Object[] { Session.Current.Scenario, this, person });
            }
        }
        /*
        public void GoForQxuanXiang(Person person)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal) 
            {
                this.outsideTask = OutsideTaskKind .劝降;
                this.ConvincingPerson = person;
                this.LocationArchitecture.DecreaseFund(10000);
                this.GoToDestinationAndReturn(this.OutsideDestination.Value);
                this.TaskDays = (this.ArrivingDays + 1) / 2;
                ExtensionInterface.call("GoForQuanXiang", new Object[] { Session.Current.Scenario, this, person });
            }
        }
        */

 

        public void GoForDestroy(Point position)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.破坏;
                this.OutsideDestination = new Point?(position);
                this.LocationArchitecture.DecreaseFund(this.LocationArchitecture.DestroyArchitectureFund);
                this.GoToDestinationAndReturn(position);
                this.TaskDays = (this.ArrivingDays + 1) / 2;
                ExtensionInterface.call("GoForDestroy", new Object[] { Session.Current.Scenario, this, position });
            }
        }

        public void GoForGossip(Point position)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.流言;
                this.OutsideDestination = new Point?(position);
                this.LocationArchitecture.DecreaseFund(this.LocationArchitecture.GossipArchitectureFund);
                this.GoToDestinationAndReturn(position);
                this.TaskDays = (this.ArrivingDays + 1) / 2;
                ExtensionInterface.call("GoForGossip", new Object[] { Session.Current.Scenario, this, position });
            }
        }

        public void GoForInformation(Point position)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.情报;
                this.OutsideDestination = new Point?(position);
                this.GoToDestinationAndReturn(position);
                this.TaskDays = this.ArrivingDays;
                ExtensionInterface.call("GoForInformation", new Object[] { Session.Current.Scenario, this, position });
            }
        }

        public void GoForJailBreak(Point position)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.劫狱;
                this.OutsideDestination = new Point?(position);
                this.LocationArchitecture.DecreaseFund(this.LocationArchitecture.JailBreakArchitectureFund);
                this.GoToDestinationAndReturn(position);
                this.TaskDays = (this.ArrivingDays + 1) / 2;
                ExtensionInterface.call("GoForJailBreak", new Object[] { Session.Current.Scenario, this, position });
            }
        }

        public void GoForInstigate(Point position)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.煽动;
                this.OutsideDestination = new Point?(position);
                this.LocationArchitecture.DecreaseFund(this.LocationArchitecture.InstigateArchitectureFund);
                this.GoToDestinationAndReturn(position);
                this.TaskDays = (this.ArrivingDays + 1) / 2;
                ExtensionInterface.call("GoForInstigate", new Object[] { Session.Current.Scenario, this, position });
            }
        }

        public void GoForSearch()
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.搜索;
                this.TargetArchitecture = this.LocationArchitecture;
                this.ArrivingDays = Math.Max(1, Session.Parameters.SearchDays) / Session.Parameters.DayInTurn + 1;
                this.TaskDays = this.ArrivingDays;
                this.Status = PersonStatus.Moving;
                ExtensionInterface.call("GoForSearch", new Object[] { Session.Current.Scenario, this });
            }
        }

        public void GoForAssassinate(Person person)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.暗杀;
                this.ConvincingPerson = person;
                this.GoToDestinationAndReturn(this.OutsideDestination.Value);
                this.TaskDays = (this.ArrivingDays + 1) / 2;
                ExtensionInterface.call("GoForAssassinate", new Object[] { Session.Current.Scenario, this });
            }
        }

        public void shoudongjinxingsousuo()
        {
            this.shoudongsousuo = true;
            this.GoForSearch();
        }

        /*
        public void GoForSpy(Point position)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.间谍;
                this.OutsideDestination = new Point?(position);
                this.LocationArchitecture.DecreaseFund(this.LocationArchitecture.SpyArchitectureFund);
                this.GoToDestinationAndReturn(position);
                this.TaskDays = (this.ArrivingDays + 1) / 2;
                ExtensionInterface.call("GoForSpy", new Object[] { Session.Current.Scenario, this, position });
            }
        }
        */

        public void GoForStudySkill()
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.技能;
                this.TargetArchitecture = this.LocationArchitecture;
                this.ArrivingDays = Math.Max(1, Session.Parameters.LearnSkillDays);
                this.Status = PersonStatus.Moving;
                this.TaskDays = this.ArrivingDays;
                ExtensionInterface.call("GoForStudySkill", new Object[] { Session.Current.Scenario, this });
            }
        }

        public void GoForStudyStunt(Stunt desStunt)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.特技;
                this.StudyingStunt = desStunt;
                this.TargetArchitecture = this.LocationArchitecture;
                this.ArrivingDays = Math.Max(1, Session.Parameters.LearnStuntDays);
                this.Status = PersonStatus.Moving;
                this.TaskDays = this.ArrivingDays;
                ExtensionInterface.call("GoForStudyStunt", new Object[] { Session.Current.Scenario, this });
            }
        }

        public void GoForStudyTitle(Title desTitle)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                this.OutsideTask = OutsideTaskKind.称号;
                this.StudyingTitle = desTitle;
                this.TargetArchitecture = this.LocationArchitecture;
                this.ArrivingDays = Math.Max(1,
                    Math.Min(this.LocationArchitecture.DayLearnTitleDay, desTitle.Kind.StudyDay));
                this.Status = PersonStatus.Moving;
                this.TaskDays = this.ArrivingDays;
                ExtensionInterface.call("GoForStudyTitle", new Object[] { Session.Current.Scenario, this });
            }
        }

        private void GoToDestinationAndReturn(Point destination)
        {
            this.TargetArchitecture = this.LocationArchitecture;
            this.ArrivingDays = Session.Current.Scenario.GetReturnDays(destination, this.TargetArchitecture.ArchitectureArea);
            this.Status = PersonStatus.Moving;
        }

        /*
        private void HandleSpyMessage(SpyMessage sm)
        {
            if (sm.Kind == SpyMessageKind.NewTroop)
            {
            }
        }
        */

        public void IncreaseKarma(int v)
        {
            float increase = v * ((100 - Math.Abs(this.Karma)) / 100.0f);
            this.Karma = this.Karma + (int)increase + (GameObject.Chance((int)((increase - (int)increase) * 100)) ? 1 : 0);
        }

        public void DecreaseKarma(int v)
        {
            float decrease = v * ((100 - Math.Abs(Math.Min(0, this.Karma))) / 100.0f);
            this.Karma = this.Karma - (int)decrease - (GameObject.Chance((int)((decrease - (int)decrease) * 100)) ? 1 : 0);
        }

        public void DecreaseReputation(int v)
        {
            this.reputation -= v;
            if (this.reputation <= 0)
            {
                this.reputation = 0;
            }
        }

        public bool IncreaseReputation(int increment)
        {
            this.reputation += increment;
            return true;
        }

        public bool IncreaseOfficerMerit(int x)
        {
            this.officerMerit += x;
            return true;
        }

        public bool DecreaseOfficerMerit(int x)
        {
            this.officerMerit -= x;
            if (this.officerMerit <= 0)
            {
                this.officerMerit = 0;
            }
            return true;
        }

        public bool LoseReputationBy(float rate)
        {
            if (this.BelongedFaction != null)
            {
                this.BelongedFaction.Reputation = (int)(this.BelongedFaction.Reputation * (1 - rate));
            }
            this.reputation = (int)(this.reputation * (1 - rate));
            return true;
        }

        public void IncreaseTechniquePoint(int increment)
        {
            if (this.BelongedFaction != null)
            {
                this.BelongedFaction.IncreaseTechniquePoint(increment);
            }
        }

        public bool IsHirable(GameObjects.Faction faction)
        {
            if (faction.Leader != null)
            {
                if (this.Hates(faction.Leader))
                {
                    return false;
                }
                if (Session.GlobalVariables.IdealTendencyValid && (this.IdealTendency != null))
                {
                    bool flag = GetIdealOffset(this, faction.Leader) <= this.IdealTendency.Offset;
                    if (!flag)
                    {
                        foreach (GameObjects.Faction faction2 in Session.Current.Scenario.Factions)
                        {
                            if ((faction2 != faction) && (faction2.Leader != null))
                            {
                                flag = GetIdealOffset(this, faction2.Leader) <= this.IdealTendency.Offset;
                                if (flag)
                                {
                                    return false;
                                }
                            }
                        }
                        return true;
                    }
                }
                return true;
            }
            return false;
        }

        public void Killed()   //现在用PlayerKillLeader代替，应该没有使用
        {
            if (((this.LocationArchitecture != null) && !this.IsCaptive) && (this.BelongedFaction == null))
            {
                if (this.OnBeKilled != null)
                {
                    this.OnBeKilled(this, this.LocationArchitecture);
                }
                this.Alive = false;
                this.ArrivingDays = 0;
                this.Status = PersonStatus.None;
                this.LocationArchitecture = null;
            }
            else if ((this.TargetArchitecture != null) && (this.BelongedFaction == null))
            {
                if (this.OnBeKilled != null)
                {
                    this.OnBeKilled(this, this.TargetArchitecture);
                }
                this.Alive = false;
                this.ArrivingDays = 0;
                this.status = PersonStatus.None;
                Session.Current.Scenario.AvailablePersons.Remove(this);
            }
        }

        public void PlayerKillLeader()
        {
            this.execute(Session.Current.Scenario.CurrentPlayer);
        }

        private void illegallyKilled(Faction executingFaction, Person killer)
        {
            killer.ExecuteCount++;

            if (this.BelongedCaptive != null && this.BelongedCaptive.CaptiveFaction != null && this.BelongedCaptive.CaptiveFaction != executingFaction) // 斩有势力的俘虏
            {
                Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedCaptive.CaptiveFaction.ID, executingFaction.ID, -10);
                if (this.BelongedFaction.Leader.HasStrainTo(this))
                {
                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedCaptive.CaptiveFaction.ID, executingFaction.ID, -10);
                }
                if (this.BelongedFaction.Leader.HasCloseStrainTo(this))
                {
                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedCaptive.CaptiveFaction.ID, executingFaction.ID, -10);
                    Session.Current.Scenario.SetDiplomaticRelationIfHigher(this.BelongedCaptive.CaptiveFaction.ID, executingFaction.ID, 0);
                }
                if (this == this.BelongedFaction.Leader)
                {
                    Session.Current.Scenario.ChangeDiplomaticRelation(this.BelongedCaptive.CaptiveFaction.ID, executingFaction.ID, -1000);
                    Session.Current.Scenario.SetDiplomaticRelationIfHigher(this.BelongedCaptive.CaptiveFaction.ID, executingFaction.ID, -1000);
                }
            }

            foreach (Person p in Session.Current.Scenario.Persons)
            {
                if (p == this) continue;
                if (p == killer) continue;
                if (p.IsVeryCloseTo(this))
                {
                    p.AddHated(killer, -500 * p.PersonalLoyalty * p.PersonalLoyalty);
                }
                if (p.HasCloseStrainTo(this))
                {
                    // person close to killed one hates executor
                    p.AddHated(killer, -500 * p.PersonalLoyalty * p.PersonalLoyalty);

                    // person close to killed one may also hate executor's close persons
                    foreach (Person q in Session.Current.Scenario.Persons)
                    {
                        if (p == q || q == this || q == killer) continue;
                        if (q.HasCloseStrainTo(killer))
                        {
                            p.AddHated(q, -200 * p.PersonalLoyalty * p.PersonalLoyalty);
                        }
                    }
                }
            }

            /*
            if (!executingFaction.IsAlien)
            {
                killer.LoseReputationBy(0.02f * this.PersonalLoyalty);
            }

            killer.DecreaseKarma(1 + this.PersonalLoyalty + Math.Max(0, this.Karma / 5));
            killer.BelongedFaction.Leader.DecreaseKarma(1 + this.PersonalLoyalty + Math.Max(0, this.Karma / 5));
            */
        }

        public void execute(Faction executingFaction)
        {
            Faction old = null;

            if (this.BelongedCaptive != null)
            {
                old = this.BelongedCaptive.CaptiveFaction;
                this.BelongedCaptive.Clear();
            }

            Person executor = executingFaction.Leader;

            this.illegallyKilled(executingFaction, executor);

            ExtensionInterface.call("Executed", new Object[] { Session.Current.Scenario, this, executingFaction });

            Session.Current.Scenario.YearTable.addExecuteEntry(Session.Current.Scenario.Date, executor, this, old);
            this.ToDeath(null, old);
        }
        /*
        private void LeaveFaction()
        {
            if (GameObject.Chance(20) && ((((this.LocationArchitecture != null) && (this.BelongedFaction != null)) && (this.BelongedFaction.Leader != this)) && !this.IsCaptive))
            {
                if ((this.Loyalty < 50) && (GameObject.Random(this.Loyalty * (1 + (int)this.PersonalLoyalty)) <= GameObject.Random(5)))
                {
                    this.LeaveToNoFaction();
                }
                else if (((Session.GlobalVariables.IdealTendencyValid && (this.IdealTendency != null)) && (this.IdealTendency.Offset <= 1)) && (this.BelongedFaction.Leader != null))
                {
                    int idealOffset = GetIdealOffset(this, this.BelongedFaction.Leader);
                    if (idealOffset > this.IdealTendency.Offset)
                    {
                        GameObjectList list = new GameObjectList();
                        foreach (GameObjects.Faction faction in Session.Current.Scenario.Factions)
                        {
                            if (((faction != this.BelongedFaction) && (faction.Leader != null)) && (GetIdealOffset(this, faction.Leader) <= this.IdealTendency.Offset))
                            {
                                list.Add(faction);
                            }
                        }
                        if (list.Count > 0)
                        {
                            GameObjects.Faction faction2 = list[GameObject.Random(list.Count)] as GameObjects.Faction;
                            if ((faction2.Capital != null) && ((((this.PersonalLoyalty < PersonLoyalty.很高) || ((this.Father >= 0) && (this.Father == faction2.Leader.ID))) || ((this.Brother >= 0) && (this.Brother == faction2.Leader.ID))) || (idealOffset > 10)))
                            {
                                this.LeaveToNoFaction();
                                this.MoveToArchitecture(faction2.Capital);
                                //this.LocationArchitecture.RemoveNoFactionPerson(this);
                            }
                        }
                    }
                }
            }
        }
        */

        public void LeaveFaction()
        {
            Faction oldFaction = this.BelongedFaction;
            if (GameObject.Chance(20) && this.LocationArchitecture != null && this.Status == PersonStatus.Normal && this.BelongedFaction != null && this.BelongedFaction.Leader != this && !this.IsCaptive)
            {
                if ((this.Loyalty < 50) && GameObject.Chance(100 - this.Loyalty * 2) && (GameObject.Random(this.Loyalty * (1 + (int)this.PersonalLoyalty)) <= GameObject.Random(5)))
                {
                    this.LeaveToNoFaction();
                    ArchitectureList allArch = Session.Current.Scenario.Architectures;
                    Architecture a;
                    int tries = 0;
                    do
                    {
                        a = allArch[GameObject.Random(allArch.Count)] as Architecture;
                        tries++;
                    } while (a.BelongedFaction == oldFaction && tries <= 10);
                    this.MoveToArchitecture(a);
                    ExtensionInterface.call("LeaveFaction", new Object[] { Session.Current.Scenario, this });
                }
                /*else if (((Session.GlobalVariables.IdealTendencyValid && (this.IdealTendency != null)) && (this.IdealTendency.Offset <= 1)) && (this.BelongedFaction.Leader != null))
                {
                    int idealOffset = GetIdealOffset(this, this.BelongedFaction.Leader);
                    if (idealOffset > this.IdealTendency.Offset + (double) this.BelongedFaction.Reputation / this.BelongedFaction.MaxPossibleReputation * 75)
                    {
                        GameObjectList list = new GameObjectList();
                        foreach (GameObjects.Faction faction in Session.Current.Scenario.Factions)
                        {
                            if (((faction != this.BelongedFaction) && (faction.Leader != null)) && (GetIdealOffset(this, faction.Leader) <= this.IdealTendency.Offset)
                                && !this.HatedPersons.Contains(faction.LeaderID))
                            {
                                list.Add(faction);
                            }
                        }
                        if (list.Count > 0)
                        {
                            GameObjects.Faction faction2 = list[GameObject.Random(list.Count)] as GameObjects.Faction;
                            if ((faction2.Capital != null) && ((((this.PersonalLoyalty < (int) PersonLoyalty.很高) || ((this.Father >= 0) && (this.Father == faction2.Leader.ID))) || ((this.Brother >= 0) && (this.Brother == faction2.Leader.Brother))) || (idealOffset > 10)))
                            {
                                this.LeaveToNoFaction();
                                this.MoveToArchitecture(faction2.Capital);
                                ExtensionInterface.call("LeaveFaction", new Object[] { Session.Current.Scenario, this });
                                //this.LocationArchitecture.RemoveNoFactionPerson(this);
                                //Session.Current.Scenario.detectDuplication();
                            }
                        }
                    }
                }*/
            }
        }

        public void LeaveToNoFaction() // 下野
        {
            Architecture locationArchitecture = this.LocationArchitecture;
            if (!this.ProhibitedFactionID.ContainsKey(this.BelongedFaction.ID))
            {
                this.ProhibitedFactionID.Add(this.BelongedFaction.ID, 90);
            }

            foreach (Person p in this.BelongedFaction.Persons)
            {
                if (p == this.BelongedFaction.Leader)
                {
                    p.AdjustRelation(this, -5f - p.PersonalLoyalty * 0.5f, -4);
                    this.AdjustRelation(p, -3f, -2);
                }
                else
                {
                    p.AdjustRelation(this, -3f - p.PersonalLoyalty * 0.25f, -2);
                }
            }

            this.OfficerMerit = (int)(this.OfficerMerit * 0.2);

            if (TargetArchitecture != null)
            {
                this.TargetArchitecture = null;
                this.ArrivingDays = 0;
                this.TaskDays = 0;
                this.OutsideTask = OutsideTaskKind.无;
            }
            Session.Current.Scenario.YearTable.addBecomeNoFactionEntry(Session.Current.Scenario.Date, this, this.BelongedFaction);
            this.Status = PersonStatus.NoFaction;
            if (this.OnLeave != null)
            {
                this.OnLeave(this, locationArchitecture);
            }
        }

        public void BeLeaveToNoFaction() // 流放
        {
            Architecture locationArchitecture = this.LocationArchitecture;
            if (this.ProhibitedFactionID.ContainsKey(this.BelongedFaction.ID))
            {
                this.ProhibitedFactionID.Remove(this.BelongedFaction.ID);
            }

            foreach (Person p in this.BelongedFaction.Persons)
            {
                if (p == this.BelongedFaction.Leader)
                {
                    p.AdjustRelation(this, -5f - p.PersonalLoyalty * 0.5f, -4);
                    this.AdjustRelation(p, -10f - p.PersonalLoyalty * 1f, -8);
                }
            }

            this.OfficerMerit = (int)(this.OfficerMerit * 0.2);

            this.ProhibitedFactionID.Add(this.BelongedFaction.ID, 360);
            this.Status = PersonStatus.NoFaction;
        }

        public void LoseTreasure(Treasure t)
        {
            this.Treasures.Remove(t);
            this.PurifyTreasure(t, false);
            t.BelongedPerson = null;
        }

        public void LoseTreasureList(TreasureList list)
        {
            foreach (Treasure treasure in list)
            {
                this.Treasures.Remove(treasure);
                this.PurifyTreasure(treasure, false);
                treasure.BelongedPerson = null;
            }
        }

        public bool WillLoseLoyalty
        {
            get
            {
                if (this.PersonalLoyalty >= 4) return false;
                return true;
            }
        }

        public bool WillLoseLoyaltyWhenHeldCaptive
        {
            get
            {
                return WillLoseLoyalty || (this.BelongedCaptive.LocationArchitecture.captiveLoyaltyFall.Count > 0);
            }
        }

        private void LoyaltyChange()
        {
            if (this.Status != PersonStatus.Captive)
            {
                if (TempLoyaltyChange > 0)
                {
                    if (GameObject.Chance((13 - this.Uncruelty) * 5))
                    {
                        TempLoyaltyChange--;
                    }
                }
                else if (TempLoyaltyChange < 0)
                {
                    if (GameObject.Chance(this.Uncruelty * 5))
                    {
                        TempLoyaltyChange++;
                    }
                }
            }
            else
            {
                if (this.LocationArchitecture != null)
                {
                    foreach (KeyValuePair<int, int> i in this.BelongedCaptive.LocationArchitecture.captiveLoyaltyFall)
                    {
                        if (this.Loyalty < i.Key)
                        {
                            TempLoyaltyChange -= i.Value;
                        }
                    }
                }
            }
        }

        private bool MeetAvailableCondition()
        {
            return ((((this.Alive && !this.Available) && (this.YearAvailable <= Session.Current.Scenario.Date.Year)) && ((((Session.GlobalVariables.CommonPersonAvailable && (base.ID >= 0)) && (base.ID <= 6999)) || ((Session.GlobalVariables.AdditionalPersonAvailable && (base.ID >= 8000)) && (base.ID <= 8999))) || ((Session.GlobalVariables.PlayerPersonAvailable && (base.ID >= 9000))))) && !Session.Current.Scenario.PreparedAvailablePersons.HasGameObject(this));
        }

        public void AdjustIdealToFactionLeader(int diff)
        {
            if (this.BelongedFactionWithPrincess == null) return;
            if (diff == 0) return;

            if (diff > 75)
            {
                diff = 75;
            }

            int oldDiff = Person.GetIdealOffset(this, this.BelongedFactionWithPrincess.Leader);
            int newValue = this.Ideal;

            int targetIdeal = this.BelongedFactionWithPrincess.Leader.Ideal;

            if (this.Ideal == targetIdeal)
            {
                return;
            }
            else
            {
                int opposite = (targetIdeal + 75) % 150;
                if (this.Ideal == opposite)
                {
                    newValue += diff * (GameObject.Chance(50) ? 1 : -1);
                }
                else if (opposite < targetIdeal)
                {
                    if (opposite < this.Ideal && this.Ideal < targetIdeal)
                    {
                        newValue += Math.Min(oldDiff, diff);
                    }
                    else
                    {
                        newValue -= Math.Min(oldDiff, diff);
                    }
                }
                else
                {
                    if (targetIdeal < this.Ideal && this.Ideal < opposite)
                    {
                        newValue -= Math.Min(oldDiff, diff);
                    }
                    else
                    {
                        newValue += Math.Min(oldDiff, diff);
                    }
                }
            }

            this.Ideal = (newValue + 150) % 150;
        }

        private void AdjustIdeal()
        {
            if (this.BelongedFactionWithPrincess != null)
            {
                if (this.Status == PersonStatus.Captive)
                {
                    if (GameObject.Chance((10 - this.Uncruelty) * 10))
                    {
                        this.AdjustIdealToFactionLeader(-1);
                    }
                }
                else
                {
                    if (GameObject.Chance(this.IdealTendency.Offset / 4))
                    {
                        this.AdjustIdealToFactionLeader(1);
                    }
                }

            }
        }

        public void MonthEvent()
        {
            if ((this.MonthIncrementOfTechniquePoint > 0) && (this.BelongedFaction != null))
            {
                this.BelongedFaction.IncreaseTechniquePoint(this.MonthIncrementOfTechniquePoint);
            }
            if ((this.MonthIncrementOfFactionReputation > 0) && (this.BelongedFaction != null))
            {
                this.BelongedFaction.IncreaseReputation(this.MonthIncrementOfFactionReputation);
            }
            this.AdjustIdeal();
        }

        public void resetPreferredWorkkind(bool[] need)
        {
            this.firstPreferred = ArchitectureWorkKind.无;
            int firstAbility = 0;
            int agricultureAbility = (need[0] ? this.AgricultureAbility : -2);
            int commerceAbility = (need[1] ? this.CommerceAbility : -2);
            int technologyAbility = (need[2] ? this.TechnologyAbility : -2);
            int dominationAbility = (need[3] ? this.DominationAbility : -2);
            int moraleAbility = (need[4] ? this.MoraleAbility : -2);
            int enduranceAbility = (need[5] ? this.EnduranceAbility : -2);
            int trainingAbility = (need[6] ? this.TrainingAbility : -2);

            if (agricultureAbility > firstAbility)
            {
                this.firstPreferred = ArchitectureWorkKind.农业;
                firstAbility = agricultureAbility;
            }
            if (commerceAbility > firstAbility)
            {
                this.firstPreferred = ArchitectureWorkKind.商业;
                firstAbility = commerceAbility;
            }
            if (technologyAbility > firstAbility)
            {
                this.firstPreferred = ArchitectureWorkKind.技术;
                firstAbility = technologyAbility;
            }
            if (dominationAbility > firstAbility)
            {
                this.firstPreferred = ArchitectureWorkKind.统治;
                firstAbility = dominationAbility;
            }
            if (moraleAbility > firstAbility)
            {
                this.firstPreferred = ArchitectureWorkKind.民心;
                firstAbility = moraleAbility;
            }
            if (enduranceAbility > firstAbility)
            {
                this.firstPreferred = ArchitectureWorkKind.耐久;
                firstAbility = enduranceAbility;
            }
            if (trainingAbility > firstAbility)
            {
                this.firstPreferred = ArchitectureWorkKind.训练;
                firstAbility = trainingAbility;
            }
        }

        /*
        public void MoveToArchitecture(Architecture a)
        {
            if (this.LocationArchitecture != a)
            {
                Point position = this.Position;
                Architecture targetArchitecture = this.TargetArchitecture;
                this.TargetArchitecture = a;
                if (this.LocationArchitecture != null)
                {
                    this.ArrivingDays = (int) Math.Ceiling((double) (Session.Current.Scenario.GetDistance(this.LocationArchitecture.ArchitectureArea, a.ArchitectureArea) / 10.0));
                }
                else if (targetArchitecture != null)
                {
                    this.ArrivingDays += (int) Math.Ceiling((double) (Session.Current.Scenario.GetDistance(targetArchitecture.ArchitectureArea, a.ArchitectureArea) / 10.0));
                    if ((((this.OutsideTask == OutsideTaskKind.情报) || (this.OutsideTask == OutsideTaskKind.搜索)) || (this.OutsideTask == OutsideTaskKind.技能)) || (this.OutsideTask == OutsideTaskKind.称号))
                    {
                        this.TaskDays = this.ArrivingDays;
                    }
                }
                else
                {
                    this.ArrivingDays = (int) Math.Ceiling((double) (Session.Current.Scenario.GetDistance(position, Session.Current.Scenario.GetClosestPoint(a.ArchitectureArea, position)) / 10.0));
                }
                if (this.ArrivingDays == 0)
                {
                    this.ArrivingDays = 1;
                }
            }
            else
            {
                this.TargetArchitecture = a;
                this.ArrivingDays = 1;
            }
            if (this.TargetArchitecture != null)
            {
                if (this.BelongedFaction != null)
                {
                    if (this.LocationArchitecture != null)
                    {
                        this.LocationArchitecture.RemovePerson(this);
                    }
                    this.TargetArchitecture.MovingPersons.Add(this);

                }
                else
                {
                    if (this.LocationArchitecture != null)
                    {
                        this.LocationArchitecture.RemoveNoFactionPerson(this);
                    }
                    this.TargetArchitecture.NoFactionMovingPersons.Add(this);
                }

            }
        }
        */

        public void MoveToArchitecture(Architecture a, Point? startingPoint)
        {
            this.MoveToArchitecture(a, startingPoint, false);
        }

        public void MoveToArchitecture(Architecture a, Point? startingPoint, bool removeFromTroop)
        {
            Architecture targetArchitecture = this.TargetArchitecture;

            if (a == null) return;

            // if (this.Status != PersonStatus.Normal) return;

            if (this.LocationTroop != null && !this.LocationTroop.Destroyed && !removeFromTroop) return;

            if (this.LocationArchitecture != a || startingPoint != null)
            {
                this.wasMayor = this.LocationArchitecture != null && this.LocationArchitecture.Mayor == this;

                Point position = this.Position;
                this.TargetArchitecture = a;
                if (startingPoint.HasValue)
                {
                    //this.ArrivingDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(startingPoint.Value, a.ArchitectureArea) / 10.0));
                    this.ArrivingDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(startingPoint.Value, a.ArchitectureArea) / 10.0));
                }
                else if (this.LocationArchitecture != null)
                {
                    //this.ArrivingDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(this.LocationArchitecture.ArchitectureArea, a.ArchitectureArea) / 10.0));
                    this.ArrivingDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(this.LocationArchitecture.ArchitectureArea, a.ArchitectureArea) / 10.0));
                }
                else if (targetArchitecture != null)
                {
                    //this.ArrivingDays += (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(targetArchitecture.ArchitectureArea, a.ArchitectureArea) / 10.0));
                    this.ArrivingDays += (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(targetArchitecture.ArchitectureArea, a.ArchitectureArea) / 10.0));
                    if ((((this.OutsideTask == OutsideTaskKind.情报) || (this.OutsideTask == OutsideTaskKind.搜索)) || (this.OutsideTask == OutsideTaskKind.技能)) || (this.OutsideTask == OutsideTaskKind.称号))
                    {
                        this.TaskDays = this.ArrivingDays;
                    }
                }
                else if (a.ArchitectureArea != null)
                {
                    //this.ArrivingDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, a.ArchitectureArea.Centre) / 10.0));
                    this.ArrivingDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, a.ArchitectureArea.Centre) / 10.0));
                }
                if (this.ArrivingDays == 0)
                {
                    this.ArrivingDays = 1;
                }
            }
            else
            {
                this.TargetArchitecture = a;
                this.ArrivingDays = 1;
            }

            //this.ArrivingDays = (int) (this.ArrivingDays * (1 - MovementDaysBonus));
            //this.ArrivingDays = Math.Max(1, this.ArrivingDays);
            this.ArrivingDays = (int)(this.ArrivingDays * (1 - MovementDaysBonus));
            this.ArrivingDays = Math.Max(1, this.ArrivingDays);

            if (this.TargetArchitecture != null)
            {
                if (this.Status != PersonStatus.Princess && this.Status != PersonStatus.Captive)
                {
                    this.WorkKind = ArchitectureWorkKind.无;

                    if (this.BelongedArchitecture != null && this.BelongedArchitecture.Mayor == this)
                    {
                        this.BelongedArchitecture.Mayor = null;
                    }

                    if (this.BelongedFaction != null)
                    {
                        this.Status = PersonStatus.Moving;
                    }
                    else
                    {
                        this.Status = PersonStatus.NoFactionMoving;
                    }
                }
                else
                {
                    Session.Current.Scenario.ClearPersonStatusCache();
                }

                this.LocationArchitecture = this.TargetArchitecture;
            }
        }

        public void GoToGeDiDiplomatic(DiplomaticRelationDisplay a) //割地
        {
            if (a == null) return;

            Faction targetFaction = this.BelongedFaction.GetFactionByName(a.FactionName);
            //bool isAI = !Session.Current.Scenario.IsPlayer(this.BelongedFaction);
            //bool isPlayer = Session.Current.Scenario.IsPlayer(targetFaction);
            //if (isAI && isPlayer) return;
            //Architecture targetArchitecture = targetFaction.Leader.BelongedArchitecture;
            Architecture targetArchitecture = targetFaction.Capital;


            if (targetArchitecture == null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.QuanXiang, "QuanXiangDiplomaticRelation", "TruceDiplomaticRelation.jpg", "TruceDiplomaticRelation", "啊，出错了!", true);
                return;
            }

            if (this.LocationArchitecture != targetArchitecture)
            {
                this.outsideDestination = targetArchitecture.Position;
                Point position = this.BelongedArchitecture.Position;
                this.TargetArchitecture = targetArchitecture;

                //this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0));
                this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0)) * Session.Parameters.DayInTurn;

                if (this.taskDays == 0)
                {
                    this.taskDays = 1;
                }

                this.arrivingDays = this.TaskDays * 2;

                this.LocationArchitecture = this.BelongedArchitecture;
                this.WorkKind = ArchitectureWorkKind.无;
                this.OutsideTask = OutsideTaskKind.割地;
                Session.MainGame.mainGameScreen.renwukaishitishi(this, this.TargetArchitecture);

                if (this.BelongedFaction != null)
                {
                    this.Status = PersonStatus.Moving;
                }
                else
                {
                    this.Status = PersonStatus.NoFactionMoving;
                }
            }
        }

        public void GoToQuanXiangDiplomatic(DiplomaticRelationDisplay a) //劝降
        {
            if (a == null) return;

            Faction targetFaction = this.BelongedFaction.GetFactionByName(a.FactionName);
            bool isAI = !Session.Current.Scenario.IsPlayer(this.BelongedFaction);
            bool isPlayer = Session.Current.Scenario.IsPlayer (targetFaction);
            if (isAI && isPlayer ) return;
            //Architecture targetArchitecture = targetFaction.Leader.BelongedArchitecture;
            Architecture targetArchitecture = targetFaction.Capital;

            
            if (targetArchitecture == null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.QuanXiang, "QuanXiangDiplomaticRelation", "TruceDiplomaticRelation.jpg", "TruceDiplomaticRelation", "啊，出错了!", true);
                return;
            }

            if (this.LocationArchitecture != targetArchitecture)
            {
                this.outsideDestination = targetArchitecture.Position;
                Point position = this.BelongedArchitecture.Position;
                this.TargetArchitecture = targetArchitecture;

                this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0));
                if (this.taskDays == 0)
                {
                    this.taskDays = 1;
                }
                if (this.taskDays > 5)
                {
                    this.taskDays = 5;
                }

                this.arrivingDays = this.TaskDays * 2;
                
                this.LocationArchitecture = this.BelongedArchitecture;
                this.WorkKind = ArchitectureWorkKind.无;
                this.OutsideTask = OutsideTaskKind.劝降;
                Session.MainGame.mainGameScreen.renwukaishitishi(this, this.TargetArchitecture);
             
               if (this.BelongedFaction != null)
                {
                    this.Status = PersonStatus.Moving;
                }
                else
                {
                    this.Status = PersonStatus.NoFactionMoving;
                }
            }
        }



        public void GoToDiplomatic(DiplomaticRelationDisplay a)
        {
            if (a == null) return;

            Faction targetFaction = this.BelongedFaction.GetFactionByName(a.FactionName);
            //Architecture targetArchitecture = targetFaction.Leader.BelongedArchitecture;
            Architecture targetArchitecture = targetFaction.Capital;

            if (targetArchitecture == null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.EnhanceDiplomaticRelation, "EnhaneceDiplomaticRelation", "EnhaneceDiplomaticRelation.jpg", "EnhaneceDiplomaticRelation", "啊，出错了!", true);
                return;
            }

            if (this.LocationArchitecture != targetArchitecture)
            {
                this.outsideDestination = targetArchitecture.Position;
                Point position = this.BelongedArchitecture.Position;
                this.TargetArchitecture = targetArchitecture;

                //this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0));
                this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0)) * Session.Parameters.DayInTurn;
                if (this.taskDays == 0)
                {
                    this.taskDays = 1;
                }
                if (this.taskDays > 5)
                {
                    this.taskDays = 5;
                }

                this.arrivingDays = this.TaskDays * 2;

                this.LocationArchitecture = this.BelongedArchitecture;
                this.WorkKind = ArchitectureWorkKind.无;
                this.OutsideTask = OutsideTaskKind.亲善;
                Session.MainGame.mainGameScreen.renwukaishitishi(this, this.TargetArchitecture);
                if (this.BelongedFaction != null)
                {
                    this.Status = PersonStatus.Moving;
                }
                else
                {
                    this.Status = PersonStatus.NoFactionMoving;
                }
            }
        }

        public void GoToTruceDiplomatic(DiplomaticRelationDisplay a)
        {
            if (a == null) return;

            Faction targetFaction = this.BelongedFaction.GetFactionByName(a.FactionName);
            Architecture targetArchitecture = targetFaction.Capital;

            if (targetArchitecture == null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.Truce, "TruceDiplomaticRelation", "TruceDiplomaticRelation.jpg", "TruceDiplomaticRelation", "啊，出错了!", true);
                return;
            }

            if (this.LocationArchitecture != targetArchitecture)
            {
                this.outsideDestination = targetArchitecture.Position;
                Point position = this.BelongedArchitecture.Position;
                this.TargetArchitecture = targetArchitecture;

                //this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0));
                this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0)) * Session.Parameters.DayInTurn;

                if (this.taskDays == 0)
                {
                    this.taskDays = 1;
                }
                if (this.taskDays > 5)
                {
                    this.taskDays = 5;
                }

                this.arrivingDays = this.TaskDays * 2;

                this.LocationArchitecture = this.BelongedArchitecture;
                this.WorkKind = ArchitectureWorkKind.无;
                this.OutsideTask = OutsideTaskKind.停战;
                Session.MainGame.mainGameScreen.renwukaishitishi(this, this.TargetArchitecture);
                if (this.BelongedFaction != null)
                {
                    this.Status = PersonStatus.Moving;
                }
                else
                {
                    this.Status = PersonStatus.NoFactionMoving;
                }

            }
        }

        public void GoToAllyDiplomatic(DiplomaticRelationDisplay a)
        {
            if (a == null) return;

            Faction targetFaction = this.BelongedFaction.GetFactionByName(a.FactionName);
            //Architecture targetArchitecture = targetFaction.Leader.BelongedArchitecture;
            Architecture targetArchitecture = targetFaction.Capital;

            if (targetArchitecture == null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this, this.BelongedFaction.Leader.Name, TextMessageKind.EnhanceDiplomaticRelation, "EnhaneceDiplomaticRelation", "EnhaneceDiplomaticRelation.jpg", "EnhaneceDiplomaticRelation", "啊，出错了!", true);
                return;
            }

            if (this.LocationArchitecture != targetArchitecture)
            {
                this.outsideDestination = targetArchitecture.Position;
                Point position = this.BelongedArchitecture.Position;
                this.TargetArchitecture = targetArchitecture;

                //this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0));
                this.TaskDays = (int)Math.Ceiling((double)(Session.Current.Scenario.GetDistance(position, targetArchitecture.Position) / 10.0)) * Session.Parameters.DayInTurn;
                if (this.taskDays == 0)
                {
                    this.taskDays = 1;
                }
                if (this.taskDays > 5)
                {
                    this.taskDays = 5;
                }

                this.arrivingDays = this.TaskDays * 2;

                this.LocationArchitecture = this.BelongedArchitecture;
                this.WorkKind = ArchitectureWorkKind.无;
                this.OutsideTask = OutsideTaskKind.结盟;
                Session.MainGame.mainGameScreen.renwukaishitishi(this, this.TargetArchitecture);
                if (this.BelongedFaction != null)
                {
                    this.Status = PersonStatus.Moving;
                }
                else
                {
                    this.Status = PersonStatus.NoFactionMoving;
                }

            }
        }

        public void MoveToArchitecture(Architecture a)
        {
            this.MoveToArchitecture(a, null);
        }

        public void NoFactionMove()
        {
            if (this.BelongedFaction == null && this.ArrivingDays == 0 && this.LocationArchitecture != null && this.Status == PersonStatus.NoFaction 
                && !this.IsCaptive && GameObject.Chance((2 + (int)this.Ambition) + (this.LeaderPossibility ? 10 : 0)) && this.Status != PersonStatus.Princess
                )
            {
                if (GameObject.Chance(50 + (this.LeaderPossibility ? 10 : 0)))
                {
                    GameObjectList hirableFactionList = this.GetHirableFactionList();
                    if (hirableFactionList.Count > 0)
                    {
                        GameObjects.Faction faction = hirableFactionList[GameObject.Random(hirableFactionList.Count)] as GameObjects.Faction;
                        if (((faction.Leader != null) && (GetIdealOffset(faction.Leader, this) <= 10)) && ((faction.Capital != null) && (faction.Capital != this.LocationArchitecture)))
                        {
                            this.MoveToArchitecture(faction.Capital);
                            //this.LocationArchitecture.RemoveNoFactionPerson(this);
                        }
                    }
                    else if (this.LeaderPossibility)
                    {
                        GameObjectList list2 = new GameObjectList();
                        foreach (Architecture architecture in this.LocationArchitecture.GetClosestArchitectures(40, 80))
                        {
                            if ((architecture.BelongedFaction == null) && (architecture.RecentlyAttacked <= 0))
                            {
                                list2.Add(architecture);
                            }
                        }
                        if (list2.Count > 0)
                        {
                            if (list2.Count > 1)
                            {
                                list2.PropertyName = "UnitPopulation";
                                list2.IsNumber = true;
                                list2.ReSort();
                            }
                            Architecture a = list2[GameObject.Random(list2.Count / 2)] as Architecture;
                            this.MoveToArchitecture(a);
                            //this.LocationArchitecture.RemoveNoFactionPerson(this);
                        }
                    }
                }
                else
                {
                    if (this.LocationArchitecture.ClosestArchitectures == null)
                    {
                        this.LocationArchitecture.GetClosestArchitectures();
                    }
                    if (this.LocationArchitecture.ClosestArchitectures.Count > 0)
                    {
                        int maxValue = 20;
                        if (maxValue > this.LocationArchitecture.ClosestArchitectures.Count)
                        {
                            maxValue = this.LocationArchitecture.ClosestArchitectures.Count;
                        }
                        maxValue = GameObject.Random(maxValue);
                        this.MoveToArchitecture(this.LocationArchitecture.ClosestArchitectures[maxValue] as Architecture);
                        //this.LocationArchitecture.RemoveNoFactionPerson(this);
                    }
                }
            }
        }

        public void PreDayEvent()
        {
            this.SetDayInfluence();
        }

        private void ProgressArrivingDays()
        {
            this.LastOutsideTask = this.outsideTask;
            if (this.TaskDays > 0)
            {
                //this.TaskDays--;
                //if ((this.TaskDays == 0) && (this.OutsideTask != OutsideTaskKind.无))
                this.TaskDays -= 1;
                if ((this.TaskDays <= 0) && (this.OutsideTask != OutsideTaskKind.无))
                {
                    if (this.BelongedFaction != null)
                    {
                        this.DoOutsideTask();
                    }
                    else
                    {
                        this.Status = PersonStatus.NoFaction;
                        this.TargetArchitecture = null;
                    }
                }
            }
            if (this.ArrivingDays > 0)
            {
                //this.ArrivingDays--;
                //if ((this.ArrivingDays == 0) && (this.TargetArchitecture != null) && this.Status != PersonStatus.Princess)
                this.ArrivingDays -= 1;
                if ((this.ArrivingDays <= 0) && (this.TargetArchitecture != null) && this.Status != PersonStatus.Princess)
                {
                    this.ReturnedDaySince = 0;
                    if (this.BelongedFaction != null)
                    {
                        if (this.TargetArchitecture.BelongedFaction == this.BelongedFaction)
                        {
                            this.Status = PersonStatus.Normal;
                            if (Session.Current.Scenario.IsCurrentPlayer(this.BelongedFaction) && this.TargetArchitecture.TodayPersonArriveNote == false
                                && this.TargetArchitecture.BelongedSection != null && this.TargetArchitecture.BelongedSection.AIDetail.ID == 0)
                            {
                                this.TargetArchitecture.TodayPersonArriveNote = true;
                                Session.MainGame.mainGameScreen.renwudaodatishi(this, this.TargetArchitecture);
                            }
                            if (this.TargetArchitecture.Mayor == null && this.wasMayor && this.TargetArchitecture.BelongedFaction == this.BelongedFaction)
                            {
                                this.TargetArchitecture.MayorID = this.ID;
                                this.TargetArchitecture.MayorOnDutyDays = 0;
                            }
                            this.wasMayor = false;

                            this.TargetArchitecture = null;
                        }
                        else if (this.TargetArchitecture.BelongedFaction != this.BelongedFaction && this.Status == PersonStatus .Captive) //转移俘虏
                        {
                            
                            this.TargetArchitecture.TodayPersonArriveNote = true;
                            this.TargetArchitecture = null;
                        }
                        else if (this.BelongedFaction.Capital != null)
                        {
                            this.MoveToArchitecture(this.BelongedFaction.Capital);
                        }
                        else   //这种情况在现在的程序中应该不会出现。
                        {
                            this.Status = PersonStatus.NoFaction;
                            this.TargetArchitecture = null;
                        }

                    }
                    else if (this.BelongedFaction == null && this.Status == PersonStatus.Captive) //转移俘虏
                    {
                        
                        this.TargetArchitecture.TodayPersonArriveNote = true;
                        this.TargetArchitecture = null;
                    }
                    else
                    {
                        this.Status = PersonStatus.NoFaction;
                        Session.MainGame.mainGameScreen.NoFactionPersonArrivesAtArchitecture(this, this.TargetArchitecture);
                        this.TargetArchitecture = null;
                    }
                    ExtensionInterface.call("ArrivedAtArchitecture", new Object[] { Session.Current.Scenario, this, this.TargetArchitecture });
                }
                if ((this.ArrivingDays <= 0) && (this.TargetArchitecture == null) && (this.LocationArchitecture != null) && (this.Status == PersonStatus.NoFactionMoving))
                {
                    this.Status = PersonStatus.NoFaction;
                    Session.MainGame.mainGameScreen.NoFactionPersonArrivesAtArchitecture(this, this.LocationArchitecture);
                    ExtensionInterface.call("ArrivedAtArchitecture", new Object[] { Session.Current.Scenario, this, this.LocationArchitecture });
                }
            }
        }

        public void ReceiveTreasure(Treasure t)
        {
            this.Treasures.Add(t);
            t.BelongedPerson = this;
            ApplyTreasure(t, false);
        }

        public void ReceiveTreasureList(TreasureList list)
        {
            foreach (Treasure treasure in list)
            {
                this.Treasures.Add(treasure);
                treasure.BelongedPerson = this;
                ApplyTreasure(treasure, false);
            }
        }

        public void SeasonEvent()
        {
        }

        private void SetDayInfluence()
        {
            this.RewardFinished = false;
        }
        /*
        public void ShowPersonMessage(PersonMessage personMessage)
        {
            bool flag = true;
            if ((this.BelongedFaction != null) && (personMessage is SpyMessage))
            {
                SpyMessage sm = personMessage as SpyMessage;
                Point key = new Point(sm.MessageArchitecture.ID, (int)sm.Kind);
                if (!this.BelongedFaction.SpyMessageCloseList.ContainsKey(key))
                {
                    this.HandleSpyMessage(sm);
                    this.BelongedFaction.SpyMessageCloseList.Add(key, null);
                }
                else
                {
                    flag = false;
                }
            }
            if (flag && (this.OnShowMessage != null))
            {
                this.OnShowMessage(this, personMessage);
            }
        }
        */
         
        public override string ToString()
        {
            return (this.NormalName + " 势力：" + this.Faction + " 所在：" + this.Location);
        }

        public void TryToBeAvailable()
        {
            if (GameObject.Chance(20) && this.MeetAvailableCondition())
            {
                this.BeAvailable();
            }
        }

        public int Age
        {
            get
            {
                if (!Immortal && Session.GlobalVariables.PersonNaturalDeath == true && Session.Current.Scenario != null && Session.Current.Scenario.Date != null)
                {
                    return Session.Current.Scenario.Date.Year - this.yearBorn;
                }
                else
                {
                    return 30;
                }
            }
        }

        public string DisplayedAge
        {
            get
            {
                return Session.GlobalVariables.PersonNaturalDeath == true && !this.Immortal ? (Session.Current.Scenario.Date.Year - this.yearBorn).ToString() : "--";
            }
        }

        public int AgricultureAbility
        {
            get
            {
                if (agricultureAbility > 0) return agricultureAbility;
                agricultureAbility = (int)((this.BaseAgricultureAbility + this.IncrementOfAgricultureAbility) * (1f + this.RateIncrementOfAgricultureAbility));
                return agricultureAbility;
            }
        }

        public int zhenzaiAbility
        {
            get
            {
                return this.Intelligence + 2 * this.Politics;
            }
        }

        public int AgricultureWeighing
        {
            get
            {
                return ((this.AgricultureAbility * (this.MultipleOfAgricultureReputation + this.MultipleOfAgricultureTechniquePoint)) * (1 + (this.InternalNoFundNeeded ? 1 : 0)));
            }
        }

        [DataMember]
        public bool Alive
        {
            get
            {
                return this.alive;
            }
            set
            {
                this.alive = value;
            }
        }

        public int AllSkillMerit
        {
            get
            {
                int num = 0;
                foreach (Skill skill in this.Skills.Skills.Values)
                {
                    num += 5 * skill.Level;
                }
                return num;
            }
        }

        [DataMember]
        public int Ambition
        {
            get
            {
                return this.ambition;
            }
            set
            {
                this.ambition = value;
            }
        }

        [DataMember]
        public int ArrivingDays
        {
            get
            {
                return this.arrivingDays;
            }
            set
            {
                this.arrivingDays = value;
            }
        }

        public int AssassinateAbility
        {
            get
            {
                return this.Strength * 2 + this.Intelligence * 2 + this.Calmness * 20 + this.Braveness * 20;
            }
        }

        [DataMember]
        public bool Available
        {
            get
            {
                return this.available;
            }
            set
            {
                this.available = value;
            }
        }

        [DataMember]
        public int AvailableLocation
        {
            get
            {
                return this.availableLocation;
            }
            set
            {
                this.availableLocation = value;
            }
        }

        public int AbilitySum
        {
            get
            {
                return this.Command + this.Strength + this.Intelligence + this.Politics + this.Glamour;
            }
        }

        // precomputed values of y = 1.12 / (1+ 69.06e^(-0.428x))
        private static readonly float[] AGE_FACTORS = { 0.0160f, 0.0243f, 0.0369f, 0.0557f, 0.0832f, 0.1227f, 0.1779f, 0.2516f, 0.3446f, 0.4541f, 0.5726f, 0.6900f, 0.7965f, 0.8856f, 0.9552f };
        private float AbilityAgeFactor
        {
            get
            {
                if (!Session.GlobalVariables.EnableAgeAbilityFactor) return 1;
                if (!this.Alive) return 1;
		        if (this.Trainable) return 1;
                if (this.IsGeneratedChildren) return 1;

                float factor = 1;
                if (this.Age < 0 && !this.IsGeneratedChildren)
                {
                    factor = AGE_FACTORS[0];
                }
                else if (this.Age < Session.GlobalVariables.ChildrenAvailableAge && this.Age < 15 && !this.IsGeneratedChildren)
                {
                    factor = AGE_FACTORS[this.Age];
                }
                else if (this.Age > 60)
                {
                    factor = Math.Max(0.2f, -0.016f * this.Age + 1.96f);
                }

                return factor;
            }
        }

        private float huaiyunAbilityFactor
        {
            get
            {
                if (this.huaiyun)
                {
                    return Math.Min(1, (360 - this.huaiyuntianshu) / 90.0f);
                }
                if (this.huaiyuntianshu < -1)
                {
                    return Math.Min(1, 1 + this.huaiyuntianshu / 180.0f);
                }

                return 1;
            }
        }

        private float huaiyunStrengthFactor
        {
            get
            {
                if (this.huaiyun)
                {
                    return Math.Min(1, (360 - this.huaiyuntianshu) / 180.0f);
                }
                if (this.huaiyuntianshu < -1)
                {
                    return Math.Min(1, 1 + this.huaiyuntianshu / 90.0f);
                }

                return 1;
            }
        }

        private int BaseAgricultureAbility
        {
            get
            {
                return (2 * (this.Politics + this.Glamour));
            }
        }

        public int InheritableCommand
        {
            get
            {
                return this.IsGeneratedChildren ? this.CommandPotential : this.command;
            }
        }

        public int InheritableStrength
        {
            get
            {
                return this.IsGeneratedChildren ? this.StrengthPotential : this.strength;
            }
        }

        public int InheritableIntelligence
        {
            get
            {
                return this.IsGeneratedChildren ? this.IntelligencePotential : this.intelligence;
            }
        }

        public int InheritablePolitics
        {
            get
            {
                return this.IsGeneratedChildren ? this.PoliticsPotential : this.politics;
            }
        }

        public int InheritableGlamour
        {
            get
            {
                return this.IsGeneratedChildren ? this.GlamourPotential : this.glamour;
            }
        }

        [DataMember]
        public int BaseCommand
        {
            get
            {
                return this.command;
            }
            set
            {
                this.command = value;
            }
        }

        private int BaseCommerceAbility
        {
            get
            {
                return ((this.Intelligence + (2 * this.Politics)) + this.Glamour);
            }
        }

        private int BaseDominationAbility
        {
            get
            {
                return (((2 * this.Strength) + this.Command) + this.Glamour);
            }
        }

        private int BaseEnduranceAbility
        {
            get
            {
                return (((this.Strength + this.Command) + this.Intelligence) + this.Politics);
            }
        }

        [DataMember]
        public int BaseGlamour
        {
            get
            {
                return this.glamour;
            }
            set
            {
                this.glamour = value;
            }
        }
        //已经没用了
      /*  [DataMember]
        public float BaseImpactRate
        {
            get
            {
                return this.baseImpactRate;
            }
            set
            {
                this.baseImpactRate = value;
            }
        }*/

        [DataMember]
        public int BaseIntelligence
        {
            get
            {
                return this.intelligence;
            }
            set
            {
                this.intelligence = value;
            }
        }

        private int BaseMoraleAbility
        {
            get
            {
                return ((this.Command + this.Politics) + (2 * this.Glamour));
            }
        }

        [DataMember]
        public int BasePolitics
        {
            get
            {
                return this.politics;
            }
            set
            {
                this.politics = value;
            }
        }

        private int BaseRecruitmentAbility
        {
            get
            {
                return ((2 * this.Command) + (2 * this.Glamour));
            }
        }

        [DataMember]
        public int BaseStrength
        {
            get
            {
                return this.strength;
            }
            set
            {
                this.strength = value;
            }
        }

        private int BaseTechnologyAbility
        {
            get
            {
                return (2 * (this.Intelligence + this.Politics));
            }
        }

        private int BaseTrainingAbility
        {
            get
            {
                return ((2 * this.Strength) + (2 * this.Command));
            }
        }

        [DataMember]
        public PersonBornRegion BornRegion
        {
            get
            {
                return this.bornRegion;
            }
            set
            {
                this.bornRegion = value;
            }
        }

        [DataMember]
        public int BaseBraveness
        {
            get
            {
                return this.braveness;
            }
            set
            {
                this.braveness = value;
            }
        }

        public int Braveness
        {
            get
            {
                return (int)((this.braveness + this.bravenessIncrease) * this.AbilityAgeFactor * this.InjureRate);
            }
            set
            {
                this.braveness = value;
            }
        }

        public PersonList Brothers
        {
            get
            {
                return this.brothers;
            }
            set
            {
                this.brothers = value;
            }
        }
        //以下添加20170226
        //获取亲近人物列表
        public PersonList ClosePersons
        {
            get
            {
                return this.closePersons;
            }
            set
            {
                this.brothers = value;
            }
        }
        //获取厌恶人物列表
        public PersonList HatedPersons
        {
            get
            {
                return this.hatedPersons;
            }
            set
            {
                this.brothers = value;
            }
        }
        //以上添加
        public String BrotherName
        {
            get
            {
                String s = "";
                foreach (Person p in this.Brothers)
                {
                    s += p.Name + " ";
                }
                return s;
            }
        }

        [DataMember]
        public int BubingExperience
        {
            get
            {
                return (int)(this.bubingExperience);
            }
            set
            {
                this.bubingExperience = value;
            }
        }

        [DataMember]
        public string CalledName
        {
            get
            {
                return this.calledName;
            }
            set
            {
                this.calledName = value;
            }
        }

        [DataMember]
        public int BaseCalmness
        {
            get
            {
                return this.calmness;
            }
            set
            {
                this.calmness = value;
            }
        }

        public int Calmness
        {
            get
            {
                return (int)((this.calmness + this.calmnessIncrease) * this.AbilityAgeFactor * this.InjureRate);
            }
            set
            {
                this.calmness = value;
            }
        }

        public int CaptiveAbility
        {
            get
            {
                return (((this.Strength * 3) + (this.Intelligence * 3)) + ((this.Braveness + this.Calmness) * 20));
            }
        }

        public string CharacterString
        {
            get
            {
                return this.Character.Name;
            }
        }

        public string CloseName
        {
            get
            {
                if ((this.calledName != null) && (this.calledName != ""))
                {
                    return this.calledName;
                }
                return this.NormalName;
            }
        }

        public int MilitaryTypeSkillMerit(MilitaryType kind)
        {
            int result = 0;
            foreach (Skill skill in this.Skills.Skills.Values)
            {
                if (skill.Combat && (skill.MilitaryTypeOnly == kind || skill.MilitaryTypeOnly == MilitaryType.其他))
                {
                    result += 5 * skill.Level;
                }
            }
            return result;
        }

        public int MilitaryTypeStuntMerit(MilitaryType kind)
        {
            int result = 0;
            foreach (Stunt stunt in this.Stunts.Stunts.Values)
            {
                if ((stunt.MilitaryTypeOnly == kind || stunt.MilitaryTypeOnly == MilitaryType.其他))
                {
                    result += 30;
                }
            }
            return result;
        }

        public bool HasMilitaryKindTitle(MilitaryKind kind)
        {
            foreach (Title t in this.Titles)
            {
                if (t.MilitaryKindOnly == kind.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasMilitaryTypeTitle(MilitaryType kind)
        {
            foreach (Title t in this.Titles)
            {
                if (t.MilitaryTypeOnly == kind || t.MilitaryTypeOnly == MilitaryType.其他)
                {
                    return true;
                }
            }
            return false;
        }

        public int CombatSkillMerit
        {
            get
            {
                int num = 0;
                foreach (Skill skill in this.Skills.Skills.Values)
                {
                    if (skill.Combat)
                    {
                        num += skill.Merit;
                    }
                }
                return num;
            }
        }

        public int SubOfficerSkillMerit
        {
            get
            {
                int num = 0;
                foreach (Skill skill in this.Skills.Skills.Values)
                {
                    if (skill.Combat)
                    {
                        num += skill.SubOfficerMerit;
                    }
                }
                return num;
            }
        }

        public int Command
        {
            get
            {
                return this.NormalCommand;
            }
            set
            {
                this.command = value;
            }
        }

        [DataMember]
        public int CommandExperience
        {
            get
            {
                return (int)(this.commandExperience);
            }
            set
            {
                this.commandExperience = value;
            }
        }

        public int CommandFromExperience
        {
            get
            {
                return (int) Math.Pow(this.CommandExperience / 0x3e8, 0.9);
            }
        }

        public int CommandIncludingExperience
        {
            get
            {
                return (this.BaseCommand + this.CommandFromExperience);
            }
        }

        public int CommerceAbility
        {
            get
            {
                if (commerceAbility > 0) return commerceAbility;
                commerceAbility = (int)((this.BaseCommerceAbility + this.IncrementOfCommerceAbility) * (1f + this.RateIncrementOfCommerceAbility));
                return commerceAbility;
            }
        }

        public int CommerceWeighing
        {
            get
            {
                return ((this.CommerceAbility * (this.MultipleOfCommerceReputation + this.MultipleOfCommerceTechniquePoint)) * (1 + (this.InternalNoFundNeeded ? 1 : 0)));
            }
        }

        public int ControversyAbility
        {
            get
            {
                return (this.Intelligence + this.Glamour);
            }
        }

        public int ConvinceAbility
        {
            get
            {
                return (int)((this.Glamour * 4) * (1f + this.RateIncrementOfConvince));
            }
        }

        public InformationKind CurrentInformationKind
        {
            get
            {
                if (this.currentInformationKind == null)
                {
                    this.currentInformationKind = Session.Current.Scenario.GameCommonData.AllInformationKinds.GetGameObject(this.informationKindID) as InformationKind;
                }
                return this.currentInformationKind;
            }
            set
            {
                this.currentInformationKind = value;
                if (value != null)
                {
                    this.informationKindID = value.ID;
                }
                else
                {
                    this.informationKindID = -1;
                }
            }
        }

        [DataMember]
        public PersonDeadReason DeadReason
        {
            get
            {
                return this.deadReason;
            }
            set
            {
                this.deadReason = value;
            }
        }

        public int DestroyAbility
        {
            get
            {
                return (int)(((this.Strength + this.Command) + (this.Intelligence * 2)) * (1f + this.RateIncrementOfDestroy));
            }
        }

        public int DominationAbility
        {
            get
            {
                if (dominationAbility > 0) return dominationAbility;
                dominationAbility = (int)((this.BaseDominationAbility + this.IncrementOfDominationAbility) * (1f + this.RateIncrementOfDominationAbility));
                return dominationAbility;
            }
        }

        public int DominationWeighing
        {
            get
            {
                return ((this.DominationAbility * (this.MultipleOfDominationReputation + this.MultipleOfDominationTechniquePoint)) * (1 + (this.InternalNoFundNeeded ? 1 : 0)));
            }
        }

        public int EnduranceAbility
        {
            get
            {
                if (enduranceAbility > 0) return enduranceAbility;
                enduranceAbility = (int)((this.BaseEnduranceAbility + this.IncrementOfEnduranceAbility) * (1f + this.RateIncrementOfEnduranceAbility));
                return enduranceAbility;
            }
        }

        public int EnduranceWeighing
        {
            get
            {
                return ((this.EnduranceAbility * (this.MultipleOfEnduranceReputation + this.MultipleOfEnduranceTechniquePoint)) * (1 + (this.InternalNoFundNeeded ? 1 : 0)));
            }
        }

        public string Faction
        {
            get
            {
                if (this.BelongedFaction != null)
                {
                    return this.BelongedFaction.Name;
                }
                return "----";
            }
        }

        public Person Father
        {
            get
            {
                return this.father;
            }
            set
            {
                this.father = value;
            }
        }

        public PersonList Siblings
        {
            get
            {
                PersonList list = new PersonList();
                foreach (Person p in Session.Current.Scenario.Persons)
                {
                    if ((p.Father == this.Father && p.Father != null) || (p.Mother == this.Mother && p.Mother != null))
                    {
                        list.Add(p);
                    }
                }
                list.PropertyName = "Age";
                list.IsNumber = true;
                list.SmallToBig = false;
                list.ReSort();
                return list;
            }
        }

        public int TitleMerit
        {
            get
            {
                int result = 0;
                foreach (Title t in this.Titles)
                {
                    result += t.Merit;
                }
                return result;
            }
        }

        public int TitleFightingMerit
        {
            get
            {
                int result = 0;
                foreach (Title t in this.Titles)
                {
                    result += t.FightingMerit;
                }
                return result;
            }
        }

        public int TitleSubofficerMerit
        {
            get
            {
                int result = 0;
                foreach (Title t in this.Titles)
                {
                    result += t.SubOfficerMerit;
                }
                return result;
            }
        }

        public int TitleInheritableMerit
        {
            get
            {
                int result = 0;
                foreach (Title t in this.Titles)
                {
                    if (t.Kind.IsInheritable(Session.Current.Scenario.GameCommonData.AllTitles))
                    {
                        result += t.Merit;
                    }
                }
                return result;
            }
        }

        public int FightingForce
        {
            get
            {
                return (int)(
                    (this.Strength * (1 - Session.GlobalVariables.LeadershipOffenceRate) + this.Command * (Session.GlobalVariables.LeadershipOffenceRate + 1)
                    + (this.Intelligence * 0.5)) *
                    (100 + this.TitleFightingMerit
                    + this.TreasureMerit + this.CombatSkillMerit + Math.Pow(this.StuntCount, 0.3) * 30));
            }
        }

        public int SubFightingForce
        {
            get
            {
                return (int)((this.Strength * 0.25 + this.Command * 0.25 + this.Intelligence * 2.5) *
                    (100 + this.TitleSubofficerMerit
                    + this.TreasureMerit + this.SubOfficerSkillMerit));
            }
        }

        public int FightingNumber
        {
            get
            {
                return (((this.Strength * 2) + (this.Command * 2)) + this.Intelligence);
            }
        }

      /*  [DataMember]//已无用
        public PersonForm Form
        {
            get
            {
                return this.form;
            }
            set
            {
                this.form = value;
            }
        }

        public float FormRate
        {
            get
            {
                switch (this.form)
                {
                    case PersonForm.好:
                        return (1f + this.ImpactRateOfGoodForm);

                    case PersonForm.中:
                        return 1f;

                    case PersonForm.差:
                        return (1f - this.ImpactRateOfBadForm);
                }
                return 1f;
            }
        }*/

        [DataMember]
        public int Generation
        {
            get
            {
                return this.generation;
            }
            set
            {
                this.generation = value;
            }
        }

        [DataMember]
        public string GivenName
        {
            get
            {
                return this.givenName;
            }
            set
            {
                this.givenName = value;
            }
        }

        public int Glamour
        {
            get
            {
                return this.NormalGlamour;
            }
            set
            {
                this.glamour = value;
            }
        }

        [DataMember]
        public int GlamourExperience
        {
            get
            {
                return (int)(this.glamourExperience);
            }
            set
            {
                this.glamourExperience = value;
            }
        }

        public int GlamourFromExperience
        {
            get
            {
                return (int)Math.Pow(this.GlamourExperience / 0x3e8, 0.9);
            }
        }

        public int GlamourIncludingExperience
        {
            get
            {
                return (this.BaseGlamour + this.GlamourFromExperience);
            }
        }

        public int GossipAbility
        {
            get
            {
                return (int)(((this.Politics * 2) + (this.Glamour * 2)) * (1f + this.RateIncrementOfGossip));
            }
        }

        public int JailBreakAbility
        {
            get
            {
                return (int)(this.CaptiveAbility * (1f + this.RateIncrementOfJailBreakAbility));
            }
        }

        public bool HasLeaderValidTitle
        {
            get
            {
                foreach (Title t in this.Titles)
                {
                    if (t.Influences.HasTroopLeaderValidInfluence)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasSubofficerValidTitle
        {
            get
            {
                foreach (Title t in this.Titles)
                {
                    foreach (Influence i in t.Influences.Influences.Values)
                    {
                        if (i.Kind.Type == InfluenceType.战斗 || i.Kind.Type == InfluenceType.建筑战斗)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        /*
        public bool HasGuanzhi()
        {
            return this.Guanzhis.Count > 0;
        }
        */

        public bool HasTitle()
        {
           /* if (this.Titles.Count > 0)
            {
                return true;
            }
            return false;
            */
            return this.Titles != null;
        }

        public bool HasSkill(int id)
        {
            return this.Skills.GetSkill(id) != null;
        }

        public bool HasStunt(int id)
        {
            return this.Stunts.GetStunt(id) != null;
        }

        public String TitleName(int kind)
        {
            foreach (Title t in this.Titles)
            {
                if (t.Kind.ID == kind)
                {
                    return t.Level + "级「" + t.Name + "」";
                }
            }
            return "";
        }
        /*
        public String GuanzhiName(int kind)
        {
            foreach (Guanzhi g in this.Guanzhis)
            {
                if (g.Kind.ID == kind)
                {
                    return g.Level + "级「" + g.Name + "」";
                }
            }
            return "";
        }
        */

        public String StuntList
        {
            get
            {
                String result = "";
                foreach (Stunt s in this.Stunts.Stunts.Values)
                {
                    result += s.Name + " ";
                }
                return result;
            }
        }

        public String StudyableSkillList
        {
            get
            {
                String result = this.GetStudySkillList().Count + "个：";
                foreach (Skill s in this.StudySkillList)
                {
                    result += s.Name + "、";
                }
                return result;
            }
        }

        public String StudyableStuntList
        {
            get
            {
                String result = this.GetStudyStuntList().Count + "个：";
                foreach (Stunt s in this.StudyStuntList)
                {
                    result += s.Name + "、 ";
                }
                return result;
            }
        }

        public bool HasLearnableSkill
        {
            get
            {
                if (Session.Current.Scenario.GameCommonData.AllSkills.Count > this.SkillCount)
                {
                    foreach (Skill skill in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
                    {
                        if ((this.Skills.GetSkill(skill.ID) == null) && skill.CanLearn(this))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool HasLearnableStunt
        {
            get
            {
                if (Session.Current.Scenario.GameCommonData.AllStunts.Count > this.StuntCount)
                {
                    foreach (Stunt stunt in Session.Current.Scenario.GameCommonData.AllStunts.Stunts.Values)
                    {
                        if ((this.Stunts.GetStunt(stunt.ID) == null) && stunt.IsLearnable(this))
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public bool HasLearnableTitle
        {
            get
            {
                return this.StudyableTitleCount > 0;
            }
        }

        private List<Title> higherLevelLearnableTitle = null;
        public List<Title> HigherLevelLearnableTitle
        {
            get
            {
                if (higherLevelLearnableTitle != null)
                {
                    return higherLevelLearnableTitle;
                }
                List<Title> title = new List<Title>();
                foreach (Title candidate in Session.Current.Scenario.GameCommonData.AllTitles.Titles.Values)
                {
                    HashSet<TitleKind> hasKind = new HashSet<TitleKind>();
                    foreach (Title t in this.Titles)
                    {
                        if (t.Kind.Equals(candidate.Kind) && candidate.Level > t.Level && candidate.CanLearn(this))
                        {
                            title.Add(candidate);
                        }
                        hasKind.Add(t.Kind);
                    }
                    if (!hasKind.Contains(candidate.Kind) && candidate.CanLearn(this))
                    {
                        title.Add(candidate);
                    }
                }
                higherLevelLearnableTitle = title;
                return title;
            }
        }

        [DataMember]
        public int Ideal
        {
            get
            {
                if (this.ideal < 0) return 0;

                if (this.ideal > 150) return 150;

                return this.ideal;
            }
            set
            {
                this.ideal = value;
            }
        }

        [DataMember]
        public int IdealTendencyIDString
        {
            get; set;
        }

        public string IdealTendencyString
        {
            get
            {
                return ((this.IdealTendency != null) ? this.IdealTendency.Name : "----");
            }
        }

      /*  [DataMember]//已无用
        public float ImpactRateOfBadForm
        {
            get
            {
                return (this.impactRateOfBadForm + (this.BaseImpactRate * this.InfluenceRateOfBadForm));
            }
            set
            {
                this.impactRateOfGoodForm = value;
            }
        }

        [DataMember]
        public float ImpactRateOfGoodForm
        {
            get
            {
                return (this.impactRateOfGoodForm + (this.BaseImpactRate * this.InfluenceRateOfGoodForm));
            }
            set
            {
                this.impactRateOfGoodForm = value;
            }
        }*/

        public int InformationAbility
        {
            get
            {
                //return this.RadiusIncrementOfInformation;
                return ((this.Intelligence * 2) + this.Glamour);

            }
        }

        [DataMember]
        public int InformationKindID
        {
            get
            {
                return this.informationKindID;
            }
            set
            {
                this.informationKindID = value;
            }
        }

        public int InstigateAbility
        {
            get
            {
                return (int)(((this.Intelligence * 2) + (this.Glamour * 2)) * (1f + this.RateIncrementOfInstigate));
            }
        }

        public int Intelligence
        {
            get
            {
                return this.NormalIntelligence;
            }
            set
            {
                this.intelligence = value;
            }
        }

        [DataMember]
        public int IntelligenceExperience
        {
            get
            {
                return (int)(this.intelligenceExperience);
            }
            set
            {
                this.intelligenceExperience = value;
            }
        }

        public int IntelligenceFromExperience
        {
            get
            {
                return (int) Math.Pow(this.IntelligenceExperience / 0x3e8, 0.9);
            }
        }

        public int IntelligenceIncludingExperience
        {
            get
            {
                return (this.BaseIntelligence + this.IntelligenceFromExperience);
            }
        }

        [DataMember]
        public int InternalExperience
        {
            get
            {
                return (int)(this.internalExperience);
            }
            set
            {
                this.internalExperience = value;
            }
        }

        public bool IsCaptive
        {
            get
            {
                return (this.BelongedCaptive != null);
            }
        }

        [DataMember]
        public bool LeaderPossibility
        {
            get
            {
                return this.leaderPossibility;
            }
            set
            {
                this.leaderPossibility = value;
            }
        }

        public bool SameLocationAs(Person b)
        {
            return (this.LocationArchitecture != null && this.LocationArchitecture == b.LocationArchitecture) ||
                (this.LocationTroop != null && this.LocationTroop == b.LocationTroop) || 
                (this.Status == PersonStatus.Princess && this.BelongedArchitecture == b.LocationArchitecture) || 
                (b.Status == PersonStatus.Princess && b.BelongedArchitecture == this.LocationArchitecture);
        }

        public string Location
        {
            get
            {
                if (this.IsCaptive)
                {
                    return "俘虏";
                }
                if (this.LocationArchitecture != null)
                {
                    return this.LocationArchitecture.Name;
                }
                if (this.TargetArchitecture != null)
                {
                    return this.TargetArchitecture.Name;
                }
                if (this.LocationTroop != null)
                {
                    return this.LocationTroop.DisplayName;
                }
                return "----";
            }
        }

        public int Loyalty
        {
            get
            {
                if (this.BelongedFaction != null)
                {
                    if (this == this.BelongedFaction.Leader) return 999;

                    float v = 100;

                    if (this.Status == PersonStatus.Captive)
                    {
                        v -= (4 - this.PersonalLoyalty) * 5;
                    }

                    v += (this.PersonalLoyalty - 2) * 15;
                    v -= (this.Ambition - 2) * 5;

                    v += Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.guanjuedezhongleizidian[this.BelongedFaction.guanjue].Loyalty;

                    v += Math.Min(20, this.ServedYears / 2);

                    if (this.LocationArchitecture != null)
                    {
                        v += this.LocationArchitecture.InfluenceIncrementOfLoyalty;
                    }

                    if (this.BelongedFaction.Leader != null)
                    {
                        if (this.BelongedFaction.Leader.Status == PersonStatus.Captive)
                        {
                            v -= 10 + (4 - this.PersonalLoyalty);
                        }
                        else
                        {
                            v += (this.BelongedFaction.Leader.Glamour - 50) / 50 * 10;
                        }
                        v += Person.GetIdealAttraction(this.BelongedFaction.Leader, this, 0.5f);
                    }
                    if (this.BelongedArchitecture != null && this.BelongedArchitecture.Mayor != null)
                    {
                        if (this.BelongedArchitecture.Mayor.Status == PersonStatus.Captive)
                        {
                            v -= 5 + (4 - this.PersonalLoyalty) / 2;
                        }
                        else
                        {
                            v += (int)((this.BelongedArchitecture.Mayor.Glamour - 50) / 50 * 5 * Math.Min(1, this.BelongedArchitecture.MayorOnDutyDays / 90.0f));
                        }

                    }
                    else
                    {
                        v -= 3;
                    }

                    v += this.MaxTreasureValue / 4.0f;

                    if (this.BelongedArchitecture.Mayor == this)
                    {
                        v += this.Ambition * 3;
                    }

                    if (this.PersonalLoyalty >= 2 && this.BelongedFaction.IsAlien)
                    {
                        v -= (this.PersonalLoyalty - 1) * 10;
                    }

                    if (this.marriageGranter == this.BelongedFaction.Leader && this.Spouse != null)
                    {
                        if (this.Spouse.HasStrainTo(this.BelongedFaction.Leader))
                        {
                            v += 10;
                        }
                        if (this.Spouse.HasCloseStrainTo(this.BelongedFaction.Leader))
                        {
                            v += 10;
                        }
                    }

                    if (this.Spouse != null && this.Spouse.BelongedFaction == this.BelongedFaction)
                    {
                        v += 10;
                    }
                    foreach (Person p in this.Brothers)
                    {
                        if (p.BelongedFaction == this.BelongedFaction)
                        {
                            v += 10;
                        }
                    }
                   
                    if (this.Father != null && this.Father.BelongedFactionWithPrincess == this.BelongedFactionWithPrincess)
                    {
                        v += this.Father.childrenLoyalty;
                    }
                    if (this.Mother != null && this.Mother.BelongedFactionWithPrincess == this.BelongedFactionWithPrincess)
                    {
                        v += this.Mother.childrenLoyalty;
                    }

                    if (this.Hates(this.BelongedFaction.Leader))
                    {
                        v -= 100;
                    }
                    else if (this.Closes(this.BelongedFaction.Leader))
                    {
                        v += 10;
                    }
                    else if (this.IsVeryCloseTo(this.BelongedFaction.Leader))
                    {
                        v += 30;
                    }

                    v += Math.Max(-200, TempLoyaltyChange);

                    v = Math.Max(0, v);

                    return (int) v;
                }
                return 0;
            }
        }

        public int Merit
        {
            get
            {
                return (int)((this.Strength + this.Command + this.Intelligence + this.Politics + this.Glamour) *
                    (100 + this.TitleMerit + this.AllSkillMerit + this.TreasureMerit + Math.Pow(this.StuntCount, 0.3) * 30));
            }
        }

        public int UnalteredUntiredMerit
        {
            get
            {
                return (this.UntiredStrength + this.UntiredCommand + this.UntiredIntelligence + this.UntiredPolitics + this.UntiredGlamour) *
                        (100 + this.TitleInheritableMerit + this.AllSkillMerit);
            }
        }


        public int UntiredMerit
        {
            get
            {
                if (this.BelongedFaction != null && this == this.BelongedFaction.Leader) //君身价公式
                {
                    return ((this.UntiredStrength + this.UntiredCommand + this.UntiredIntelligence + this.UntiredPolitics + this.UntiredGlamour) *
                    (100 + this.TitleInheritableMerit + this.AllSkillMerit)) * 2;
                }
                else if (this.BelongedFaction != null && this == this.BelongedFaction.Prince) //储君身价公式
                {
                    return ((this.UntiredStrength + this.UntiredCommand + this.UntiredIntelligence + this.UntiredPolitics + this.UntiredGlamour) *
                    (100 + this.TitleInheritableMerit + this.AllSkillMerit)) * 3 / 2;
                }
                else
                {

                    return (this.UntiredStrength + this.UntiredCommand + this.UntiredIntelligence + this.UntiredPolitics + this.UntiredGlamour) *
                        (100 + this.TitleInheritableMerit + this.AllSkillMerit);
                }
            }
        }

        public int MoraleAbility
        {
            get
            {
                if (moraleAbility > 0) return moraleAbility;
                moraleAbility = (int)((this.BaseMoraleAbility + this.IncrementOfMoraleAbility) * (1f + this.RateIncrementOfMoraleAbility));
                return moraleAbility;
            }
        }

        public int MoraleWeighing
        {
            get
            {
                return ((this.MoraleAbility * (this.MultipleOfMoraleReputation + this.MultipleOfMoraleTechniquePoint)) * (1 + (this.InternalNoFundNeeded ? 1 : 0)));
            }
        }

        public Person Mother
        {
            get
            {
                return this.mother;
            }
            set
            {
                this.mother = value;
            }
        }

#pragma warning disable CS0108 // 'Person.Name' hides inherited member 'GameObject.Name'. Use the new keyword if hiding was intended.
        public string Name
#pragma warning restore CS0108 // 'Person.Name' hides inherited member 'GameObject.Name'. Use the new keyword if hiding was intended.
        {
            get
            {
                return this.NormalName;
            }
        }

        public int NonFightingNumber
        {
            get
            {
                return ((this.Intelligence + (this.Politics * 2)) + (this.Glamour * 2));
            }
        }

        public int NormalAgricultureAbility
        {
            get
            {
                return (int)(((2 * (this.NormalPolitics + this.NormalGlamour)) + this.IncrementOfAgricultureAbility) * (1f + this.RateIncrementOfAgricultureAbility));
            }
        }

        public int NormalCommand
        {
            get
            {
                return (int)(Math.Min((int)((this.CommandIncludingExperience + this.InfluenceIncrementOfCommand) * this.InfluenceRateOfCommand), Session.GlobalVariables.MaxAbility) * this.TirednessFactor * this.AbilityAgeFactor * this.RelationAbilityFactor * this.huaiyunAbilityFactor * this.InjureRate);
            }
        }

        public int UntiredCommand
        {
            get
            {
                return (int)(Math.Min((int)((this.CommandIncludingExperience + this.InfluenceIncrementOfCommand) * this.InfluenceRateOfCommand), Session.GlobalVariables.MaxAbility) * this.AbilityAgeFactor);
            }
        }

        public int NormalCommerceAbility
        {
            get
            {
                return (int)((((this.NormalIntelligence + (2 * this.NormalPolitics)) + this.NormalGlamour) + this.IncrementOfCommerceAbility) * (1f + this.RateIncrementOfCommerceAbility));
            }
        }

        public int NormalDominationAbility
        {
            get
            {
                return (int)(((((2 * this.NormalStrength) + this.NormalCommand) + this.NormalGlamour) + this.IncrementOfDominationAbility) * (1f + this.RateIncrementOfDominationAbility));
            }
        }

        public int NormalEnduranceAbility
        {
            get
            {
                return (int)(((((this.NormalStrength + this.NormalCommand) + this.NormalIntelligence) + this.NormalPolitics) + this.IncrementOfEnduranceAbility) * (1f + this.RateIncrementOfEnduranceAbility));
            }
        }

        public int NormalGlamour
        {
            get
            {
                return (int)(Math.Min((int)((this.GlamourIncludingExperience + this.InfluenceIncrementOfGlamour) * this.InfluenceRateOfGlamour), Session.GlobalVariables.MaxAbility) * this.TirednessFactor * this.AbilityAgeFactor * this.RelationAbilityFactor * this.huaiyunAbilityFactor * this.InjureRate);
            }
        }

        public int UntiredGlamour
        {
            get
            {
                return (int)(Math.Min((int)((this.GlamourIncludingExperience + this.InfluenceIncrementOfGlamour) * this.InfluenceRateOfGlamour), Session.GlobalVariables.MaxAbility) * this.AbilityAgeFactor);
            }
        }

        public int NormalIntelligence
        {
            get
            {
                return (int)(Math.Min((int)((this.IntelligenceIncludingExperience + this.InfluenceIncrementOfIntelligence) * this.InfluenceRateOfIntelligence), Session.GlobalVariables.MaxAbility) * this.TirednessFactor * this.AbilityAgeFactor * this.RelationAbilityFactor * this.huaiyunAbilityFactor * this.InjureRate);
            }
        }

        public int UntiredIntelligence
        {
            get
            {
                return (int)(Math.Min((int)((this.IntelligenceIncludingExperience + this.InfluenceIncrementOfIntelligence) * this.InfluenceRateOfIntelligence), Session.GlobalVariables.MaxAbility) * this.AbilityAgeFactor);
            }
        }

        public int NormalMoraleAbility
        {
            get
            {
                return (int)((((this.NormalCommand + this.NormalPolitics) + (2 * this.NormalGlamour)) + this.IncrementOfMoraleAbility) * (1f + this.RateIncrementOfMoraleAbility));
            }
        }

        public string NormalName
        {
            get
            {
                return (this.surName + this.givenName);
            }
        }

        public int NormalPolitics
        {
            get
            {
                return (int)(Math.Min((int)((this.PoliticsIncludingExperience + this.InfluenceIncrementOfPolitics) * this.InfluenceRateOfPolitics), Session.GlobalVariables.MaxAbility) * this.TirednessFactor * this.AbilityAgeFactor * this.RelationAbilityFactor * this.huaiyunAbilityFactor * this.InjureRate);
            }
        }

        public int UntiredPolitics
        {
            get
            {
                return (int)(Math.Min((int)((this.PoliticsIncludingExperience + this.InfluenceIncrementOfPolitics) * this.InfluenceRateOfPolitics), Session.GlobalVariables.MaxAbility) * this.AbilityAgeFactor);
            }
        }

        public int NormalRecruitmentAbility
        {
            get
            {
                return (int)((((2 * this.NormalCommand) + (2 * this.NormalGlamour)) + this.IncrementOfRecruitmentAbility) * (1f + this.RateIncrementOfRecruitmentAbility));
            }
        }

        public int NormalStrength
        {
            get
            {
                return (int)(Math.Min((int)((this.StrengthIncludingExperience + this.InfluenceIncrementOfStrength) * this.InfluenceRateOfStrength), Session.GlobalVariables.MaxAbility) * this.TirednessFactor * this.AbilityAgeFactor * this.RelationAbilityFactor * this.huaiyunStrengthFactor * this.InjureRate);
            }
        }

        //[Breamask]单挑用的武力数值，不含有义兄弟加成RelationAbilityFactor
        public int ChallengeStrength
        {
            get
            {
                return (int)(Math.Min((int)((this.StrengthIncludingExperience + this.InfluenceIncrementOfStrength) * this.InfluenceRateOfStrength), Session.GlobalVariables.MaxAbility) * this.TirednessFactor * this.AbilityAgeFactor * this.huaiyunStrengthFactor * this.InjureRate);
            }
        }


        public int UntiredStrength
        {
            get
            {
                return (int)(Math.Min((int)((this.StrengthIncludingExperience + this.InfluenceIncrementOfStrength) * this.InfluenceRateOfStrength), Session.GlobalVariables.MaxAbility) * this.AbilityAgeFactor);
            }
        }

        public int NormalTechnologyAbility
        {
            get
            {
                return (int)(((2 * (this.NormalIntelligence + this.NormalPolitics)) + this.IncrementOfTechnologyAbility) * (1f + this.RateIncrementOfTechnologyAbility));
            }
        }

        public int NormalTrainingAbility
        {
            get
            {
                return (int)((((2 * this.NormalStrength) + (2 * this.NormalCommand)) + this.IncrementOfTrainingAbility) * (1f + this.RateIncrementOfTrainingAbility));
            }
        }

        [DataMember]
        public int NubingExperience
        {
            get
            {
                return (int)(this.nubingExperience);
            }
            set
            {
                this.nubingExperience = value;
            }
        }

        [DataMember]
        public List<int> JoinFactionID
        {
            get
            {
                return this.joinFactionID;
            }
            set
            {
                this.joinFactionID = value;
            }
        }

        [DataMember]
        public Dictionary<int, int> ProhibitedFactionID
        {
            get
            {
                return prohibitedFactionID;
            }
            set
            {
                prohibitedFactionID = value;
            }
        }

        [DataMember]
        public Point? OutsideDestination
        {
            get
            {
                return this.outsideDestination;
            }
            set
            {
                this.outsideDestination = value;
            }
        }

        [DataMember]
        public OutsideTaskKind OutsideTask
        {
            get
            {
                return this.outsideTask;
            }
            set
            {
                this.outsideTask = value;
            }
        }

        public string OutsideTaskDaysString
        {
            get
            {
                if (this.TaskDays > 0)
                {
                    return (this.TaskDays * Session.Parameters.DayInTurn + "天");
                }
                return "----";
            }
        }

        public string OutsideTaskString
        {
            get
            {
                if (this.outsideTask != OutsideTaskKind.无)
                {
                    return this.outsideTask.ToString();
                }
                return "----";
            }
        }

        [DataMember]
        public int PersonalLoyalty
        {
            get
            {
                return this.personalLoyalty;
            }
            set
            {
                this.personalLoyalty = value;
            }
        }

        public int FallbackPictureIndex
        {
            get
            {
                if (this.Sex) return 9997;
                if (this.BaseIntelligence + this.BasePolitics > this.BaseStrength + this.BaseCommand) return 9998; else return 9999;
            }
        }

        [DataMember]
        public int PictureIndex
        {
            get
            {
                return this.pictureIndex;
            }
            set
            {
                this.pictureIndex = value;
            }
        }

        public int Politics
        {
            get
            {
                return this.NormalPolitics;
            }
            set
            {
                this.politics = value;
            }
        }

        [DataMember]
        public int PoliticsExperience
        {
            get
            {
                return (int)(this.politicsExperience);
            }
            set
            {
                this.politicsExperience = value;
            }
        }

        public int PoliticsFromExperience
        {
            get
            {
                return (int)Math.Pow(this.PoliticsExperience / 0x3e8, 0.9);
            }
        }

        public int PoliticsIncludingExperience
        {
            get
            {
                return (this.BasePolitics + this.PoliticsFromExperience);
            }
        }

        //public Texture2D Portrait
        //{
        //    get
        //    {
        //        Texture2D result = Session.Current.Scenario.GetPortrait(this.PictureIndex);
        //        if (this.Age >= 50)
        //        {
        //            result = Session.Current.Scenario.GetPortrait(this.PictureIndex + 0.5f);
        //        }

        //        if (this.Age <= 20)
        //        {
        //            result = Session.Current.Scenario.GetPortrait(this.PictureIndex + 0.2f);
        //        }
        //        return result == null ? Session.Current.Scenario.GetPortrait(9999) : result;
        //    }
        //}

        public Point Position
        {
            get
            {
                if (this.IsCaptive)
                {
                    if (this.BelongedCaptive.LocationArchitecture != null)
                    {
                        return this.BelongedCaptive.LocationArchitecture.Position;
                    }
                    if (this.BelongedCaptive.LocationTroop != null)
                    {
                        return this.BelongedCaptive.LocationTroop.Position;
                    }
                    return this.BelongedCaptive.BelongedFaction.Capital.Position;
                }
                if (this.LocationTroop != null)
                {
                    return this.LocationTroop.Position;
                }
                if (this.LocationArchitecture != null)
                {
                    return this.LocationArchitecture.Position;
                }
                if (this.TargetArchitecture != null)
                {
                    return this.TargetArchitecture.Position;
                }
                if (this.Father != null)
                {
                    return this.Father.Position;
                }
                if (this.Mother != null)
                {
                    return this.Mother.Position;
                }
                return Point.Zero;
            }
        }

        [DataMember]
        public int QibingExperience
        {
            get
            {
                return (int)(this.qibingExperience);
            }
            set
            {
                this.qibingExperience = value;
            }
        }

        [DataMember]
        public int QixieExperience
        {
            get
            {
                return (int)(this.qixieExperience);
            }
            set
            {
                this.qixieExperience = value;
            }
        }

        [DataMember]
        public PersonQualification Qualification
        {
            get
            {
                return this.qualification;
            }
            set
            {
                this.qualification = value;
            }
        }

        public string RealDestinationString
        {
            get
            {
                if (this.LocationTroop != null)
                {
                    return this.LocationTroop.RealDestinationString;
                }
                return "----";
            }
        }

        public int RecruitmentAbility
        {
            get
            {
                return (int)((this.BaseRecruitmentAbility + this.IncrementOfRecruitmentAbility) * (1f + this.RateIncrementOfRecruitmentAbility));
            }
        }

        public void RecruitMilitary(Military m)
        {
            
            if (this.recruitmentMilitary != null)
            {
                this.recruitmentMilitary.StopRecruitment();
            }
            m.StopRecruitment();
            this.WorkKind = ArchitectureWorkKind.补充;
            this.RecruitmentMilitary = m;
            m.RecruitmentPerson = this;
        }

        public Military RecruitmentMilitary
        {
            get
            {
                return this.recruitmentMilitary;
            }
            set
            {
                if (value != null && value.RecruitmentPerson != null)
                {
                    value.RecruitmentPerson.WorkKind = ArchitectureWorkKind.无;
                }
                this.recruitmentMilitary = value;
            }
        }

        public int RecruitmentWeighing
        {
            get
            {
                return (this.RecruitmentAbility * (this.MultipleOfRecruitmentReputation + this.MultipleOfRecruitmentTechniquePoint));
            }
        }

        [DataMember]
        public int Reputation
        {
            get
            {
                return reputation;
            }
            set
            {
                this.reputation = value;
            }
        }

        public string RespectfulName
        {
            get
            {
                if ((this.calledName != null) && (this.calledName != ""))
                {
                    return (this.surName + this.calledName);
                }
                return this.NormalName;
            }
        }

        [DataMember]
        public int RoutCount
        {
            get
            {
                return this.routCount;
            }
            set
            {
                this.routCount = value;
            }
        }

        [DataMember]
        public int RoutedCount
        {
            get
            {
                return this.routedCount;
            }
            set
            {
                this.routedCount = value;
            }
        }

        public int SearchAbility
        {
            get
            {
                return (int)(((this.Intelligence + this.Politics) + (this.Glamour * 2)) * (1f + this.RateIncrementOfSearch));
            }
        }

        public string SectionString
        {
            get
            {
                if (this.BelongedFaction != null)
                {
                    if (this.IsCaptive)
                    {
                        return "----";
                    }
                    if (this.LocationArchitecture != null)
                    {
                        return this.LocationArchitecture.SectionString;
                    }
                    if (this.TargetArchitecture != null)
                    {
                        return this.TargetArchitecture.SectionString;
                    }
                    if (this.LocationTroop != null)
                    {
                        if ((this.LocationTroop.StartingArchitecture != null) && (this.LocationTroop.StartingArchitecture.BelongedFaction == this.BelongedFaction))
                        {
                            return this.LocationTroop.StartingArchitecture.SectionString;
                        }
                        return "----";
                    }
                }
                return "----";
            }
        }

        [DataMember]
        public bool Sex
        {
            get
            {
                return this.sex;
            }
            set
            {
                this.sex = value;
            }
        }

        public string SexString
        {
            get
            {
                if (this.sex)
                {
                    return "女";
                }
                return "男";
            }
        }

        [DataMember]
        public int ShuijunExperience
        {
            get
            {
                return (int)(this.shuijunExperience);
            }
            set
            {
                this.shuijunExperience = value;
            }
        }

        public int SkillCount
        {
            get
            {
                return this.Skills.Skills.Count;
            }
        }

        //public Texture2D SmallPortrait
        //{
        //    get
        //    {
        //        Texture2D result = Session.Current.Scenario.GetSmallPortrait(this.PictureIndex);
        //        if (this.Age >= 50)
        //        {
        //            result = Session.Current.Scenario.GetSmallPortrait(this.PictureIndex + 0.5f);
        //        }
        //        if (this.Age <= 20)
        //        {
        //            result = Session.Current.Scenario.GetSmallPortrait(this.PictureIndex + 0.2f);
        //        }
        //        return result == null ? Session.Current.Scenario.GetSmallPortrait(9999) : result;
        //    }
        //}

        //public Texture2D TroopPortrait
        //{
        //    get
        //    {
        //        Texture2D result = Session.Current.Scenario.GetTroopPortrait(this.PictureIndex);
        //        if (this.Age >= 50)
        //        {
        //            result = Session.Current.Scenario.GetTroopPortrait(this.PictureIndex + 0.5f);
        //        }
        //        if (this.Age <= 20)
        //        {
        //            result = Session.Current.Scenario.GetTroopPortrait(this.PictureIndex + 0.2f);
        //        }
        //        return result == null ? Session.Current.Scenario.GetTroopPortrait(9999) : result;
        //    }
        //}

        //public Texture2D FullPortrait
        //{
        //    get
        //    {
        //        Texture2D result = Session.Current.Scenario.GetFullPortrait(this.PictureIndex);
        //        if (this.Age >= 50)
        //        {
        //            result = Session.Current.Scenario.GetFullPortrait(this.PictureIndex + 0.5f);
        //        }
        //        if (this.Age <= 20)
        //        {
        //            result = Session.Current.Scenario.GetFullPortrait(this.PictureIndex + 0.2f);
        //        }
        //        return result == null ? Session.Current.Scenario.GetFullPortrait(9999) : result;
        //    }
        //}
        public Person Spouse
        {
            get
            {
                return this.spouse;
            }
            set
            {
                this.spouse = value;
            }
        }

        public int SpyAbility
        {
            get
            {
                return ((this.Strength + (this.Intelligence * 2)) + this.Glamour);
            }
        }

        public int SpyDays
        {
            get
            {
                return (this.IncrementOfSpyDays + 80 + this.SpyAbility / 10);
            }
        }

        [DataMember]
        public int Strain
        {
            get
            {
                return this.strain;
            }
            set
            {
                this.strain = value;
            }
        }

        [DataMember]
        public int StratagemExperience
        {
            get
            {
                return (int)(this.stratagemExperience);
            }
            set
            {
                this.stratagemExperience = value;
            }
        }

        [DataMember]
        public PersonStrategyTendency StrategyTendency
        {
            get
            {
                return Session.GlobalVariables.IgnoreStrategyTendency ? PersonStrategyTendency.统一全国 : this.strategyTendency;
            }
            set
            {
                this.strategyTendency = value;
            }
        }

        public int Strength
        {
            get
            {
                return this.NormalStrength;
            }
            set
            {
                this.strength = value;
            }
        }

        [DataMember]
        public int StrengthExperience
        {
            get
            {
                return (int)(this.strengthExperience);
            }
            set
            {
                this.strengthExperience = value;
            }
        }

        public int StrengthFromExperience
        {
            get
            {
                return  (int) Math.Pow(this.StrengthExperience / 0x3e8, 0.9);
            }
        }

        public int StrengthIncludingExperience
        {
            get
            {
                return (this.BaseStrength + this.StrengthFromExperience);
            }
        }

        public int StuntCount
        {
            get
            {
                return this.Stunts.Count;
            }
        }

        [DataMember]
        public string SurName
        {
            get
            {
                return this.surName;
            }
            set
            {
                this.surName = value;
            }
        }

        [DataMember]
        public int TacticsExperience
        {
            get
            {
                return (int)(this.tacticsExperience);
            }
            set
            {
                this.tacticsExperience = value;
            }
        }

        public string TargetString
        {
            get
            {
                if (this.LocationTroop != null)
                {
                    return this.LocationTroop.TargetString;
                }
                return "----";
            }
        }

        [DataMember]
        public int TaskDays
        {
            get
            {
                return this.taskDays;
            }
            set
            {
                this.taskDays = value;
            }
        }

        public int TechnologyAbility
        {
            get
            {
                if (technologyAbility > 0) return technologyAbility;
                technologyAbility = (int)((this.BaseTechnologyAbility + this.IncrementOfTechnologyAbility) * (1f + this.RateIncrementOfTechnologyAbility));
                return technologyAbility;
            }
        }

        public int TechnologyWeighing
        {
            get
            {
                return ((this.TechnologyAbility * (this.MultipleOfTechnologyReputation + this.MultipleOfTechnologyTechniquePoint)) * (1 + (this.InternalNoFundNeeded ? 1 : 0)));
            }
        }

        public int TrainingAbility
        {
            get
            {
                if (trainingAbility > 0) return trainingAbility;
                trainingAbility = (int)((this.BaseTrainingAbility + this.IncrementOfTrainingAbility) * (1f + this.RateIncrementOfTrainingAbility));
                return trainingAbility;
            }
        }

        public int TrainingWeighing
        {
            get
            {
                return (this.TrainingAbility * (this.MultipleOfTrainingReputation + this.MultipleOfTrainingTechniquePoint));
            }
        }

        public string Travel
        {
            get
            {
                if (this.ArrivingDays > 0)
                {
                    return (this.ArrivingDays * Session.Parameters.DayInTurn + "天");
                }
                return "----";
            }
        }

        public int TreasureCount
        {
            get
            {
                return this.Treasures.Count;
            }
        }
        //以下添加20170226
        public int EffectiveTreasureCount
        {
            get
            {
                return this.effectiveTreasures.Values.Count;
            }
        }
        public int TitleCount
        {
            get
            {
                return this.Titles.Count;
            }
        }
        //以上添加
        public int MaxTreasureValue
        {
            get
            {
                int num = 0;
                foreach (Treasure treasure in this.effectiveTreasures.Values)
                {
                    if (num > treasure.Worth)
                    {
                        num = treasure.Worth;
                    }
                }
                return num;
            }
        }


        public int TreasureMerit
        {
            get
            {
                int num = 0;
                foreach (Treasure treasure in this.effectiveTreasures.Values)
                {
                    num += treasure.Worth;
                }
                return num;
            }
        }

        [DataMember]
        public PersonValuationOnGovernment ValuationOnGovernment
        {
            get
            {
                return this.valuationOnGovernment;
            }
            set
            {
                this.valuationOnGovernment = value;
            }
        }

        public float WinningRate
        {
            get
            {
                if ((this.routCount + this.routedCount) == 0)
                {
                    return 0f;
                }
                return (((float)this.routCount) / ((float)(this.routCount + this.routedCount)));
            }
        }

        public int WorkAbility
        {
            get
            {
                return this.GetWorkAbility(this.WorkKind);
            }
        }

        [DataMember]
        public ArchitectureWorkKind WorkKind
        {
            get
            {
                return this.workKind;
            }
            set
            {
                if (this.workKind == ArchitectureWorkKind.补充 && value != ArchitectureWorkKind.补充 && this.recruitmentMilitary != null)
                {
                    this.recruitmentMilitary.RecruitmentPerson = null;
                    this.recruitmentMilitary = null;
                }
                if (Session.Current.Scenario != null)
                {
                    Session.Current.Scenario.ClearPersonWorkCache();
                }
                this.workKind = value;
            }
        }

        public string WorkKindString
        {
            get
            {
                if (this.WorkKind != ArchitectureWorkKind.无)
                {
                    return this.WorkKind.ToString();
                }
                return "----";
            }
        }

        [DataMember]
        public int YearAvailable
        {
            get
            {
                return this.yearAvailable;
            }
            set
            {
                this.yearAvailable = value;
            }
        }

        [DataMember]
        public int YearBorn
        {
            get
            {
                return this.yearBorn;
            }
            set
            {
                this.yearBorn = value;
            }
        }

        [DataMember]
        public int YearDead
        {
            get
            {
                return this.yearDead;
            }
            set
            {
                this.yearDead = value;
            }
        }

        public PersonList ChildrenCanBeSelectedAsPrince()
        {
            PersonList candicate = new PersonList();
            foreach (Person person in Session.Current.Scenario.Persons)
            {
                if (person.Alive && person.Available && person.BelongedCaptive == null && person.sex == false 
                    && person.BelongedFaction == this.BelongedFaction && person.BelongedFaction != null && this == person.Father)
                {
                    candicate.Add(person);
                }
            }
            candicate.PropertyName = "Merit";
            candicate.IsNumber = true;
            candicate.SmallToBig = true;
            candicate.ReSort();
            return candicate;

        }



        public PersonList meichushengdehaiziliebiao()
        {
            PersonList haiziliebiao = new PersonList();
            foreach (Person person in Session.Current.Scenario.Persons)
            {
                if (person.Alive && !person.Available && person.Father == this && person.YearBorn > Session.Current.Scenario.Date.Year)
                {
                    haiziliebiao.Add(person);
                }
            }
            haiziliebiao.PropertyName = "YearBorn";
            haiziliebiao.IsNumber = true;
            haiziliebiao.SmallToBig = true;
            haiziliebiao.ReSort();
            return haiziliebiao;
        }

        public String FatherName
        {
            get
            {
                if (Father == null) return "－－－－";
                return Father.Name;
            }
        }

        public String MotherName
        {
            get
            {
                if (Mother == null) return "－－－－";
                return Mother.Name;
            }
        }

        public String SpouseName
        {
            get
            {
                if (spouse == null) return "－－－－";
                return Spouse.Name;
            }
        }
        ////以下添加20170226
        ////获取亲人小头像        
        //public Texture2D FatherSmallPortrait
        //{
        //    get
        //    {
        //        if (Father == null) return null;
        //        return Father.SmallPortrait;
        //    }
        //}
        //public Texture2D MotherSmallPortrait
        //{
        //    get
        //    {
        //        if (Mother == null) return null;
        //        return Mother.SmallPortrait;
        //    }
        //}
        //public Texture2D SpouseSmallPortrait
        //{
        //    get
        //    {
        //        if (Spouse == null) return null;
        //        return Spouse.SmallPortrait;
        //    }
        //}
        ////以上添加

        public delegate void BeAwardedTreasure(Person person, Treasure t);

        public delegate void BeConfiscatedTreasure(Person person, Treasure t);

        public delegate void BeKilled(Person person, Architecture location);

        public delegate void ChangeLeader(Faction faction, Person leader, bool changeName, string oldName);

        public delegate void ConvinceFailed(Person source, Person destination);

        public delegate void ConvinceSuccess(Person source, Person destination, Faction oldFaction);

        public delegate void JailBreakFailed(Person source, Architecture destination);

        public delegate void JailBreakSuccess(Person source, Captive destination);

        public delegate void Death(Person person, Person killer, Architecture location, Troop locationTroop);

        public delegate void DeathChangeFaction(Person dead, Person leader, string oldName);

        public delegate void DestroyFailed(Person person, Architecture architecture);

        public delegate void DestroySuccess(Person person, Architecture architecture, int down);

        public delegate void GossipFailed(Person person, Architecture architecture);

        public delegate void GossipSuccess(Person person, Architecture architecture);

        public delegate void InformationObtained(Person person, Information information);

        public delegate void qingbaoshibai(Person person);

        public delegate void InstigateFailed(Person person, Architecture architecture);

        public delegate void InstigateSuccess(Person person, Architecture architecture, int down);

        public delegate void Leave(Person person, Architecture location);

        public delegate void SearchFinished(Person person, Architecture architecture, SearchResultPack resultPack);
        /*
        public delegate void ShowMessage(Person person, PersonMessage personMessage);

        public delegate void SpyFailed(Person person, Architecture architecture);

        public delegate void SpyFound(Person person, Person spy);

        public delegate void SpySuccess(Person person, Architecture architecture);
        */
        public delegate void StudySkillFinished(Person person, string skillString, bool success);

        public delegate void StudyStuntFinished(Person person, Stunt stunt, bool success);

        public delegate void StudyTitleFinished(Person person, Title title, bool success);

        public delegate void TreasureFound(Person person, Treasure treasure);

        public delegate void CapturedByArchitecture(Person person, Architecture architecture);

        public bool RecruitableBy(Faction f, int idealLeniency)
        {
            int idealOffset = Person.GetIdealOffset(this, f.Leader);
            //义兄弟或者配偶直接登用。(当前判断是和所在势力的君主)
            if (this.IsVeryCloseTo(f.Leader))
            {
                return true;
            }

            if (this.Hates(f.Leader))
            {
                return false;
            }
            if (Session.GlobalVariables.IdealTendencyValid && idealOffset > this.IdealTendency.Offset + (double)f.Reputation / Session.Current.Scenario.Parameters.MaxReputationForRecruit * 75 + idealLeniency)
            {
                return false;
            }
            if (this.Loyalty > 100 && this.BelongedFaction != f)
            {
                return false;
            }
            if (this.IsCaptive && this.BelongedFaction != null && this == this.BelongedFaction.Leader)
            {
                return false;
            }
            if (this.PersonalLoyalty >= 2 && f.IsAlien)
            {
                return false;
            }
            if (this.ProhibitedFactionID.ContainsKey(f.ID))
            {
                return false;
            }
            return true;
        }

        public void muqinyingxiangnengli(Person muqin)
        {
            this.BaseStrength = (int)(this.BaseStrength * 0.9 + muqin.BaseStrength * 0.1);
            this.BaseStrength += GameObject.Random(3) * (GameObject.Random(2) == 0 ? 1 : -1);

            this.BaseCommand = (int)(this.BaseCommand * 0.9 + muqin.BaseCommand * 0.1);
            this.BaseCommand += GameObject.Random(3) * (GameObject.Random(2) == 0 ? 1 : -1);

            this.BaseIntelligence = (int)(this.BaseIntelligence * 0.9 + muqin.BaseIntelligence * 0.1);
            this.BaseIntelligence += GameObject.Random(3) * (GameObject.Random(2) == 0 ? 1 : -1);

            this.BasePolitics = (int)(this.BasePolitics * 0.9 + muqin.BasePolitics * 0.1);
            this.BasePolitics += GameObject.Random(3) * (GameObject.Random(2) == 0 ? 1 : -1);

            this.BaseGlamour = (int)(this.BaseGlamour * 0.9 + muqin.BaseGlamour * 0.1);
            this.BaseGlamour += GameObject.Random(3) * (GameObject.Random(2) == 0 ? 1 : -1);

            if (!Session.GlobalVariables.createChildrenIgnoreLimit)
            {
                if (this.BaseStrength > 100) this.BaseStrength = 100;
                if (this.BaseStrength < 0) this.BaseStrength = 0;
                if (this.BaseCommand > 100) this.BaseCommand = 100;
                if (this.BaseCommand < 0) this.BaseCommand = 0;
                if (this.BaseIntelligence > 100) this.BaseIntelligence = 100;
                if (this.BaseIntelligence < 0) this.BaseIntelligence = 0;
                if (this.BasePolitics > 100) this.BasePolitics = 100;
                if (this.BasePolitics < 0) this.BasePolitics = 0;
                if (this.BaseGlamour > 100) this.BaseGlamour = 100;
                if (this.BaseGlamour < 0) this.BaseGlamour = 0;
            }
        }

        private static List<String> readTextList(String fileName)
        {
            var lines = Platform.Current.LoadTexts(fileName);

            var result = lines.NullToEmptyList();
            //TextReader tr = new StreamReader(fileName);
            //List<String> result = new List<String>();
            //while (tr.Peek() >= 0)
            //{
            //    result.Add(tr.ReadLine());
            //}
            return result;
        }

        public static List<int> readNumberList(String fileName)
        {
            var lines = Platform.Current.LoadTexts(fileName).NullToEmptyList();

            var result = new List<int>();

            foreach(var line in lines)
            {
                int value;
                if (int.TryParse(line.NullToStringTrim(), out value))
                {
                    result.Add(value);
                }
            }

            //TextReader tr = new StreamReader(fileName);
            //List<int> result = new List<int>();
            //while (tr.Peek() >= 0)
            //{
            //    result.Add(int.Parse(tr.ReadLine()));
            //}
            return result;
        }

        private static void setNewOfficerFace(Person r)
        {
            List<int> pictureList;
            if (r.Sex)
            {
                if (r.BaseCommand + r.BaseStrength > r.BaseIntelligence + r.BasePolitics)
                {
                    pictureList = Person.readNumberList("Content/Data/femalefaceM.txt");
                }
                else
                {
                    pictureList = Person.readNumberList("Content/Data/femalefaceA.txt");
                }
            }
            else
            {
                if (r.BaseCommand < 50 && r.BaseStrength < 50 && r.BaseIntelligence < 50 && r.BasePolitics < 50 && r.BaseGlamour < 50)
                {
                    pictureList = Person.readNumberList("Content/Data/malefaceU.txt");
                }
                else if (r.BaseCommand + r.BaseStrength > r.BaseIntelligence + r.BasePolitics)
                {
                    pictureList = Person.readNumberList("Content/Data/malefaceM.txt");
                }
                else
                {
                    pictureList = Person.readNumberList("Content/Data/maleFaceA.txt");
                }
            }
            r.PictureIndex = pictureList[GameObject.Random(pictureList.Count)];
        }

        private static String GenerateBiography(Person r)
        {
            String biography = "";

            List<String> adjectives = new List<String>();
            List<String> suffixes = new List<String>();
            int strength, command, intelligence, politics, glamour, braveness, calmness, personalLoyalty, ambition;
            strength = command = intelligence = politics = glamour = braveness = calmness = personalLoyalty = ambition = 0;
            foreach (BiographyAdjectives b in Session.Current.Scenario.GameCommonData.AllBiographyAdjectives)
            {
                if (b.Male && r.Sex)
                {
                    continue;
                }
                if (b.Female && !r.Sex)
                {
                    continue;
                }

                if ((b.Strength == 0 || (b.Strength > strength && r.BaseStrength >= b.Strength)) &&
                    (b.Command == 0 || (b.Command > command && r.BaseCommand >= b.Command)) &&
                    (b.Intelligence == 0 || (b.Intelligence > intelligence && r.BaseIntelligence >= b.Intelligence)) &&
                    (b.Politics == 0 || (b.Politics > politics && r.BasePolitics >= b.Politics)) &&
                    (b.Glamour == 0 || (b.Glamour > glamour && r.BaseGlamour >= b.Glamour)) &&
                    (b.Braveness == 0 || (b.Braveness > braveness && r.BaseBraveness >= b.Braveness)) &&
                    (b.Calmness == 0 || (b.Calmness > calmness && r.BaseCalmness >= b.Calmness)) &&
                    (b.PersonalLoyalty == 0 || (b.PersonalLoyalty > personalLoyalty && r.PersonalLoyalty >= b.PersonalLoyalty)) &&
                    (b.Ambition == 0 || (b.Ambition > ambition && r.Ambition >= b.Ambition))
                    )
                {
                    strength = b.Strength;
                    command = b.Command;
                    intelligence = b.Intelligence;
                    politics = b.Politics;
                    glamour = b.Glamour;
                    braveness = b.Braveness;
                    calmness = b.Calmness;
                    personalLoyalty = b.PersonalLoyalty;
                    ambition = b.Ambition;

                    if (b.Text.Count > 0)
                    {
                        adjectives.Add(b.Text[GameObject.Random(b.Text.Count)]);
                    }
                    if (b.SuffixText.Count > 0)
                    {
                        suffixes.Add(b.SuffixText[GameObject.Random(b.SuffixText.Count)]);
                    }
                }
            }
            if (adjectives.Count > 0)
            {
                foreach (String s in adjectives)
                {
                    biography += s + "，";
                }
                biography = biography.Substring(0, biography.Length - 1);
                if (adjectives.Count > 0)
                {
                    biography += "的" + (suffixes.Count > 0 ? suffixes[GameObject.Random(suffixes.Count)] : "将领");
                }
                biography += "。";
            }

            return biography;
        }

        //private enum OfficerType { GENERAL, BRAVE, ADVISOR, POLITICIAN, INTEL_GENERAL, EMPEROR, ALL_ROUNDER, NORMAL_ADVISOR, CHEAP, NORMAL_GENERAL };

        private static PersonGeneratorType generatePersonType()
        {
            PersonGeneratorType gernrateType = new PersonGeneratorType();
            
            //int[] weights = new int[10];
            int typeCount = Session.Current.Scenario.GameCommonData.AllPersonGeneratorTypes.Count;
            Dictionary<int, int> weights = new Dictionary<int, int>();

            foreach (PersonGeneratorType type in Session.Current.Scenario.GameCommonData.AllPersonGeneratorTypes)
            {
                weights[type.ID] = type.generationChance;
            }

            int total = 0;
            foreach (int i in weights.Values)
            {
                total += i;
            }

            int officerType = 0;
            int typeInt = GameObject.Random(total);
            int typeSum = 0;
            foreach (KeyValuePair<int, int> p in weights)
            {
                typeSum += p.Value;
                if (typeInt < typeSum)
                {
                    officerType = p.Key;
                    break;
                }
            }
            gernrateType.ID = officerType;

            return gernrateType;
        }

        public static Person createPerson(Architecture foundLocation, Person finder, bool inGame, bool autoJoin)
        {
            return createPerson(foundLocation, finder, inGame, generatePersonType(), autoJoin);
        }

        public static Person createPerson(PersonGenerateParam param, bool autoJoin)
        {
            return createPerson(param.FoundLocation, param.Finder, param.InGame, param.PreferredType, autoJoin);
        }

        private static Person createPerson(Architecture foundLocation, Person finder, bool inGame, PersonGeneratorType preferredType, bool autoJoin)
        {

            Person r = HandleID();

            PersonGeneratorSetting options = HandlePersonRelation(foundLocation, ref finder, r);
       
            int officerType;
            int titleChance;

            HandlePersonGeneratorType(inGame, preferredType, r, options, out officerType, out titleChance);

            HandleName(r);

            HandleIdeal(foundLocation, r);

            HandlePersonCharacter(r, officerType);

            HandleSkill(r, officerType);

            HandleStunt(r, officerType);

            HandleTitle(r, officerType, titleChance);

            HandleBiography(foundLocation, finder, r);

            HandleStatus(foundLocation, autoJoin, r);

            return r;
        }

        private static void HandlePersonCharacter(Person r, int officerType)
        {
            r.BornRegion = (PersonBornRegion)GameObject.Random(Enum.GetNames(typeof(PersonBornRegion)).Length);

            {
                Dictionary<CharacterKind, int> chances = new Dictionary<CharacterKind, int>();
                foreach (CharacterKind t in Session.Current.Scenario.GameCommonData.AllCharacterKinds)
                {
                    chances.Add(t, t.GenerationChance[(int)officerType]);
                }

                int sum = 0;
                foreach (int i in chances.Values)
                {
                    sum += i;
                }

                int p = GameObject.Random(sum);
                double pt = 0;
                foreach (KeyValuePair<CharacterKind, int> td in chances)
                {
                    pt += td.Value;
                    if (p < pt)
                    {
                        r.Character = td.Key;
                        break;
                    }
                }
            }
        }

        private static void HandleIdeal(Architecture foundLocation, Person r)
        {
            if ((Session.Current.Scenario.IsPlayer(foundLocation.BelongedFaction) && Session.GlobalVariables.PlayerZhaoxianFixIdeal) ||
                (!Session.Current.Scenario.IsPlayer(foundLocation.BelongedFaction) && Session.GlobalVariables.AIZhaoxianFixIdeal))
            {
                GameObjectList ideals = Session.Current.Scenario.GameCommonData.AllIdealTendencyKinds;
                IdealTendencyKind minIdeal = null;
                foreach (IdealTendencyKind itk in ideals)
                {
                    if (minIdeal == null || itk.Offset < minIdeal.Offset)
                    {
                        minIdeal = itk;
                    }
                }

                r.IdealTendency = minIdeal;
                r.Ideal = (foundLocation.BelongedFaction.Leader.Ideal + GameObject.Random(minIdeal.Offset * 2 + 1) - minIdeal.Offset) % 150;


            }
            else
            {
                r.Ideal = GameObject.Random(150);
            }
        }

        private static void HandlePersonGeneratorType(bool inGame, PersonGeneratorType preferredType, Person r, PersonGeneratorSetting options, out int officerType, out int titleChance)
        {
            officerType = preferredType.ID;
       
            titleChance = 0;

            PersonGeneratorType typeParam = (PersonGeneratorType)Session.Current.Scenario.GameCommonData.AllPersonGeneratorTypes.GetGameObject(officerType);

            if (typeParam.genderFix == -1)
            {
                r.Sex = false;
            }
            else if (typeParam.genderFix == 1)
            {
                r.Sex = true;
            }
            else
            {
                r.Sex = GameObject.Chance(options.femaleChance) ? true : false;
            }
            
            r.BaseCommand = GameObject.RandomGaussianRange(typeParam.commandLo, typeParam.commandHi);
            r.BaseStrength = GameObject.RandomGaussianRange(typeParam.strengthLo, typeParam.strengthHi);
            r.BaseIntelligence = GameObject.RandomGaussianRange(typeParam.intelligenceLo, typeParam.intelligenceHi);
            r.BasePolitics = GameObject.RandomGaussianRange(typeParam.politicsLo, typeParam.politicsHi);
            r.BaseGlamour = GameObject.RandomGaussianRange(typeParam.glamourLo, typeParam.glamourHi);
            r.Braveness = GameObject.RandomGaussianRange(typeParam.braveLo, typeParam.braveHi);
            r.Calmness = GameObject.RandomGaussianRange(typeParam.calmnessLo, typeParam.calmnessHi);
            r.PersonalLoyalty = GameObject.RandomGaussianRange(typeParam.personalLoyaltyLo, typeParam.personalLoyaltyHi);
            r.Ambition = GameObject.RandomGaussianRange(typeParam.ambitionLo, typeParam.ambitionHi);
            titleChance = typeParam.titleChance;

            if (typeParam.affectedByRateParameter || Session.GlobalVariables.CreatedOfficerAbilityFactor > 1)
            {
                r.BaseCommand = (int)(r.BaseCommand * Session.GlobalVariables.CreatedOfficerAbilityFactor);
                r.BaseStrength = (int)(r.BaseStrength * Session.GlobalVariables.CreatedOfficerAbilityFactor);
                r.BaseIntelligence = (int)(r.BaseIntelligence * Session.GlobalVariables.CreatedOfficerAbilityFactor);
                r.BasePolitics = (int)(r.BasePolitics * Session.GlobalVariables.CreatedOfficerAbilityFactor);
                r.BaseGlamour = (int)(r.BaseGlamour * Session.GlobalVariables.CreatedOfficerAbilityFactor);
            }
            if (r.BaseCommand < 1) r.Command = 1;
            if (r.BaseStrength < 1) r.Strength = 1;
            if (r.BaseIntelligence < 1) r.Intelligence = 1;
            if (r.BasePolitics < 1) r.Politics = 1;
            if (r.BaseGlamour < 1) r.Glamour = 1;

            setNewOfficerFace(r);

            r.YearBorn = Session.Current.Scenario.Date.Year + GameObject.Random(options.bornLo, options.bornHi);
            r.YearAvailable = Session.Current.Scenario.Date.Year + (inGame ? 0 : GameObject.Random(options.debutLo, options.debutHi));
            r.YearDead = Math.Max(r.YearBorn + GameObject.Random(options.dieLo, options.dieHi), Session.Current.Scenario.Date.Year + options.debutAtLeast);

            r.Reputation = GameObject.Random(51) * 100;

            r.Qualification = (PersonQualification)GameObject.Random(Enum.GetNames(typeof(PersonQualification)).Length);
            r.ValuationOnGovernment = (PersonValuationOnGovernment)GameObject.Random(Enum.GetNames(typeof(PersonValuationOnGovernment)).Length);
            r.StrategyTendency = (PersonStrategyTendency)GameObject.Random(Enum.GetNames(typeof(PersonStrategyTendency)).Length);
            r.IdealTendency = Session.Current.Scenario.GameCommonData.AllIdealTendencyKinds.GetRandomList()[0] as IdealTendencyKind;
        }

        private static void HandleName(Person r)
        {
            List<String> surnameList = Person.readTextList("Content/Data/surname.txt");
            r.SurName = surnameList[GameObject.Random(surnameList.Count)];
            List<String> givenNameList = r.Sex ? Person.readTextList("Content/Data/femalegivenname.txt") : Person.readTextList("Content/Data/malegivenname.txt");
            r.GivenName = givenNameList[GameObject.Random(givenNameList.Count)];
            if (r.GivenName.Length <= 1 && GameObject.Chance(r.Sex ? 90 : 10))
            {
                String s;
                int tries = 0;
                do
                {
                    s = givenNameList[GameObject.Random(givenNameList.Count)];
                    tries++;
                } while (s.Length > 1 && tries < 100);
                r.GivenName += s;
            }
            r.CalledName = "";
        }

        private static PersonGeneratorSetting HandlePersonRelation(Architecture foundLocation, ref Person finder, Person r)
        {
            r.Father = null;
            r.Mother = null;
            r.Generation = 1;
            r.Strain = r.ID;
            if (foundLocation.BelongedFaction != null)
            {
                finder = foundLocation.BelongedFaction.Leader;
            }

            PersonGeneratorSetting options = Session.Current.Scenario.GameCommonData.PersonGeneratorSetting;

            return options;
        }

        private static Person HandleID()
        {
            Person r = new Person();

            //look for empty id
            int id = 25000;
            PersonList pl = Session.Current.Scenario.Persons as PersonList;
            pl.SmallToBig = true;
            pl.IsNumber = true;
            pl.PropertyName = "ID";
            pl.ReSort();
            foreach (Person p in pl)
            {
                if (p.ID == id)
                {
                    id++;

                }
                else if (p.ID > id)
                {
                    break;
                }
            }
            r.ID = id;
            return r;
        }

        private static void HandleStatus(Architecture foundLocation, bool autoJoin, Person r)
        {
            r.Alive = true;
            r.Available = true;
            r.LocationArchitecture = foundLocation;
            if (autoJoin)
            {
                r.Status = PersonStatus.Normal;
            }
            else
            {
                r.Status = PersonStatus.NoFaction;
            }
            r.YearJoin = Session.Current.Scenario.Date.Year;

            Session.Current.Scenario.Persons.Add(r);

            ExtensionInterface.call("CreatePerson", new Object[] { Session.Current.Scenario, r });
        }

        private static void HandleBiography(Architecture foundLocation, Person finder, Person r)
        {
            String biography = "";
            if (foundLocation != null && finder != null)
            {
                biography += "于" + Session.Current.Scenario.Date.Year + "年" + Session.Current.Scenario.Date.Month + "月在" + foundLocation.Name + "被" + finder.Name + "发掘成才。";
            }

            biography += Person.GenerateBiography(r);

            Biography bio = new Biography();
            bio.Brief = biography;
            bio.ID = r.ID;
            bio.FactionColor = 52;
            bio.MilitaryKinds.AddBasicMilitaryKinds();
            Session.Current.Scenario.AllBiographies.AddBiography(bio);

            r.PersonBiography = bio;
        }

        private static void HandleTitle(Person r, int officerType, int titleChance)
        {
            if (GameObject.Chance(titleChance))
            {
                Dictionary<TitleKind, List<Title>> titles = Title.GetKindTitleDictionary();
                foreach (KeyValuePair<TitleKind, List<Title>> kv in titles)
                {
                    Dictionary<Title, float> chances = new Dictionary<Title, float>();
                    foreach (Title t in kv.Value)
                    {
                        if (t.CanBeChosenForGenerated(r))
                        {
                            chances.Add(t, t.GenerationChance[(int)officerType]);
                        }
                    }

                    if (chances.Count > 0)
                    {
                        r.RealTitles.Add(GameObject.WeightedRandom(chances));
                    }
                }
            }
        }

        private static void HandleStunt(Person r, int officerType)
        {
            foreach (Stunt s in Session.Current.Scenario.GameCommonData.AllStunts.Stunts.Values)
            {
                if (s.CanBeChosenForGenerated())
                {
                    int chance = s.GenerationChance[(int)officerType];
                    chance = (int)(chance * Math.Max(0, s.GetRelatedAbility(r) - 50) / 10.0 + 1);
                    if (GameObject.Random(1000) <= chance)
                    {
                        r.Stunts.AddStunt(s);
                    }
                }
            }
        }

        private static void HandleSkill(Person r, int officerType)
        {
            foreach (Skill s in Session.Current.Scenario.GameCommonData.AllSkills.Skills.Values)
            {
                if (s.CanBeChosenForGenerated(r))
                {
                    int chance = s.GenerationChance[(int)officerType];
                    chance = (int)(chance * (Math.Max(0, s.GetRelatedAbility(r) - 50) / 10.0 + 1));
                    if (GameObject.Chance(chance))
                    {
                        r.Skills.AddSkill(s);
                    }
                }
            }
        }

        
        public static Person createChildren(Person father, Person mother)
        {
            Person r = HandleChildrenId(father);

            if (Session.GlobalVariables.PersonNaturalDeath == true)
            {
                HandleChildrenRelation(father, mother, r);

                HandleChildrenName(father, r);

                HandleChildrenType(father, mother, r);
                r.CommandPotential = r.BaseCommand;
                r.StrengthPotential = r.BaseStrength;
                r.IntelligencePotential = r.BaseIntelligence;
                r.PoliticsPotential = r.BasePolitics;
                r.GlamourPotential = r.BaseGlamour;
                r.BaseCommand = 1;
                r.BaseStrength = 1;
                r.BaseIntelligence = 1;
                r.BasePolitics = 1;
                r.BaseGlamour = 1;

                HandleChildrenStatus(father, r);

                HandleChildrenProperty(father, mother, r);

                AdjustChildrenIdeal(father, mother, r);

                HandleChildrenCharacter(father, mother, r);
               
                Architecture bornArch = HandleChildrenRegion(father, mother, r);

                HandleChildrenBiography(father, mother, r, bornArch, false);

                HandleChildrenFaction(father, mother, r);

                r.IsGeneratedChildren = true;
                r.TrainPolicy = (TrainPolicy) Session.Current.Scenario.GameCommonData.AllTrainPolicies.GetGameObject(1);
            }
            else
            {
                HandleChildrenRelation(father, mother, r);

                HandleChildrenName(father, r);

                HandleChildrenType(father, mother, r);

                HandleChildrenStatus(father, r);

                HandleChildrenProperty(father, mother, r);

                AdjustChildrenIdeal(father, mother, r);

                Architecture bornArch = HandleChildrenRegion(father, mother, r);

                HandleChildrenCharacter(father, mother, r);

                HandleChildrenSkill(father, mother, r);

                HandleChildrenStunt(father, mother, r);

                HandleChildrenTitle(father, mother, r);

                HandleChildrenBiography(father, mother, r, bornArch);

                /*r.LocationArchitecture = father.BelongedArchitecture; //mother has no location arch!
                r.BelongedFaction = r.BelongedArchitecture.BelongedFaction;
                r.Available = true;*/
                HandleChildrenFaction(father, mother, r);

                AdjustChildrenRelation(father, mother, r);
            }

            return r;
        }

        private static void AdjustChildrenRelation(Person father, Person mother, Person r)
        {
            foreach (Person p in mother.GetClosePersons())
            {
                if (GameObject.Chance((int)r.personalLoyalty * 25))
                {
                    r.AddClose(p);
                }
            }
            foreach (Person p in mother.GetHatedPersons())
            {
                if (!GameObject.Chance((int)r.personalLoyalty * 25) && !r.IsCloseTo(p))
                {
                    r.AddHated(p);
                }
            }
            foreach (Person p in father.GetClosePersons())
            {
                if (GameObject.Chance((int)r.personalLoyalty * 25))
                {
                    r.AddClose(p);
                }
            }
            foreach (Person p in father.GetHatedPersons())
            {
                if (!GameObject.Chance((int)r.personalLoyalty * 25) && !r.IsCloseTo(p))
                {
                    r.AddHated(p);
                }
            }

            if (GameObject.Chance((int)r.personalLoyalty * 50))
            {
                r.AddClose(r.Father);
            }
            if (GameObject.Chance((int)r.personalLoyalty * 50))
            {
                r.AddClose(r.Mother);
            }
            foreach (Person p in father.ChildrenList)
            {
                if (GameObject.Chance((int)r.personalLoyalty * 20))
                {
                    r.AddClose(p);
                }
            }

            foreach (Person p in mother.ChildrenList)
            {
                if (GameObject.Chance((int)r.personalLoyalty * 20))
                {
                    r.AddClose(p);
                }
            }


            ExtensionInterface.call("CreateChildren", new Object[] { Session.Current.Scenario, r });
        }

        private static void HandleChildrenFaction(Person father, Person mother, Person r)
        {
            r.Alive = true;
            r.JoinFactionID = new List<int>();
            if (father.BelongedFaction != null)
            {
                r.JoinFactionID.Add(father.BelongedFaction.ID);
            }
            if (mother.BelongedFaction != null)
            {
                r.JoinFactionID.Add(mother.BelongedFaction.ID);
            }

            Session.Current.Scenario.Persons.Add(r);
            
        }

        private static void HandleChildrenBiography(Person father, Person mother, Person r, Architecture bornArch, bool generateAbilityBiography = true)
        {
            String biography = "";
            int fatherChildCount = father.NumberOfChildren;
            int motherChildCount = mother.NumberOfChildren;
            String[] order = new String[] { "长", "次", "三", "四", "五", "六", "七", "八" };
            biography += r.Father.Name + "之" + (fatherChildCount > 7 ? "" : order[fatherChildCount]) + (r.Sex ? "女" : "子") + "，" +
                r.Mother.Name + "之" + (motherChildCount > 7 ? "" : order[motherChildCount]) + (r.Sex ? "女" : "子") + "。" +
                "在" + Session.Current.Scenario.Date.Year + "年" + Session.Current.Scenario.Date.Month + "月于" + (bornArch == null ? "" : bornArch.Name) + "出生。";

            Person root = father;
            while (root.Father != null)
            {
                root = root.Father;
            }
            if (root != father)
            {
                biography += root.Name + "的后代。";
            }

            if (generateAbilityBiography)
            {
                biography += Person.GenerateBiography(r);
            }

            Biography bio = new Biography();
            bio.Brief = biography;
            bio.ID = r.ID;
            Biography fatherBio = Session.Current.Scenario.AllBiographies.GetBiography(father.ID);
            if (fatherBio != null)
            {
                bio.FactionColor = fatherBio.FactionColor;
                bio.MilitaryKinds = fatherBio.MilitaryKinds;
            }
            else
            {
                bio.FactionColor = 52;
                bio.MilitaryKinds.AddBasicMilitaryKinds();
            }
            Session.Current.Scenario.AllBiographies.AddBiography(bio);
            r.PersonBiography = bio;
        }

        private static void HandleChildrenTitle(Person father, Person mother, Person r)
        {
            Dictionary<TitleKind, List<Title>> titles = Title.GetKindTitleDictionary();
            foreach (KeyValuePair<TitleKind, List<Title>> i in titles)
            {
                Title ft = father.getTitleOfKind(i.Key);
                Title mt = mother.getTitleOfKind(i.Key);
                int levelTendency = (((ft == null ? 0 : ft.Level) + (mt == null ? 0 : mt.Level)) / 2)
                    + father.childrenTitleChanceIncrease + mother.childrenTitleChanceIncrease;

                if (ft != null && GameObject.Chance(ft.InheritChance) && ft.CheckLimit(father))
                {
                    r.RealTitles.Add(ft);
                }
                else if (mt != null && GameObject.Chance(mt.InheritChance) && mt.CheckLimit(mother))
                {
                    r.RealTitles.Add(mt);
                }
                else
                {
                    int targetLevel = levelTendency + GameObject.Random(3) - 1;
                    if (targetLevel <= 0) continue;

                    List<Title> candidates = new List<Title>();
                    List<Title> lesserCandidates = new List<Title>();
                    List<Title> leastCandidates = new List<Title>();

                    foreach (Title t in i.Value)
                    {
                        if (t.Level == targetLevel && t.CanBeBorn(r))
                        {
                            candidates.Add(t);
                        }
                        else if ((t.Level + 1 == targetLevel || t.Level - 1 == targetLevel) && t.CanBeBorn(r))
                        {
                            lesserCandidates.Add(t);
                        }
                        else if (t.Level < targetLevel && t.CanBeBorn(r))
                        {
                            leastCandidates.Add(t);
                        }
                    }

                    if (candidates.Count > 0)
                    {
                        r.RealTitles.Add(candidates[GameObject.Random(candidates.Count)]);
                    }
                    else if (lesserCandidates.Count > 0)
                    {
                        r.RealTitles.Add(lesserCandidates[GameObject.Random(lesserCandidates.Count)]);
                    }
                    else if (leastCandidates.Count > 0)
                    {
                        r.RealTitles.Add(leastCandidates[GameObject.Random(lesserCandidates.Count)]);
                    }
                }
            }
        }

        private static void HandleChildrenStunt(Person father, Person mother, Person r)
        {
            foreach (Stunt i in father.Stunts.GetStuntList())
            {
                if (GameObject.Chance(50 + father.childrenStuntChanceIncrease) && i.CanBeBorn(r))
                {
                    r.Stunts.AddStunt(i);
                }
            }
            foreach (Stunt i in mother.Stunts.GetStuntList())
            {
                if (GameObject.Chance(50 + mother.childrenStuntChanceIncrease) && i.CanBeBorn(r))
                {
                    r.Stunts.AddStunt(i);
                }
            }
            foreach (Stunt i in Session.Current.Scenario.GameCommonData.AllStunts.GetStuntList())
            {
                if ((GameObject.Random(Session.Current.Scenario.GameCommonData.AllStunts.GetStuntList().Count * 2) == 0 ||
                    GameObject.Chance(father.childrenStuntChanceIncrease + mother.childrenStuntChanceIncrease)) && i.CanBeBorn(r))
                {
                    bool ok = i.LearnConditions.CheckPersonalityCondition(r);
                    if (ok)
                    {
                        r.Stunts.AddStunt(i);
                    }
                }
            }
        }

        private static void HandleChildrenSkill(Person father, Person mother, Person r)
        {
            foreach (Skill i in father.Skills.GetSkillList())
            {
                if (GameObject.Chance(50 + father.childrenSkillChanceIncrease) && i.CanBeBorn(r))
                {
                    r.Skills.AddSkill(i);
                }
            }
            foreach (Skill i in mother.Skills.GetSkillList())
            {
                if (GameObject.Chance(50 + mother.childrenSkillChanceIncrease) && i.CanBeBorn(r))
                {
                    r.Skills.AddSkill(i);
                }
            }
            foreach (Skill i in Session.Current.Scenario.GameCommonData.AllSkills.GetSkillList())
            {
                if (((GameObject.Random(Session.Current.Scenario.GameCommonData.AllSkills.GetSkillList().Count / 2) == 0 && GameObject.Random(i.Level * i.Level / 2 + i.Level) == 0)
                    ||
                    GameObject.Chance(father.childrenSkillChanceIncrease + mother.childrenSkillChanceIncrease)) && i.CanBeBorn(r))
                {
                    r.Skills.AddSkill(i);
                }
            }
        }

        private static void HandleChildrenCharacter(Person father, Person mother, Person r)
        {
            int characterId = 0;
            do
            {
                characterId = GameObject.Random(Session.Current.Scenario.GameCommonData.AllCharacterKinds.Count);
            } while (characterId == 0);
            r.Character = GameObject.Chance(84) ? (GameObject.Chance(50) ? father.Character : mother.Character) : Session.Current.Scenario.GameCommonData.AllCharacterKinds[characterId];
        }

        private static Architecture HandleChildrenRegion(Person father, Person mother, Person r)
        {
            Architecture bornArch = mother.BelongedArchitecture != null ? mother.BelongedArchitecture : father.BelongedArchitecture;

            try //best-effort approach for getting PersonBornRegion
            {
                r.BornRegion = (PersonBornRegion)Enum.Parse(typeof(PersonBornRegion), bornArch.LocationState.Name); //mother has no locationarch...
            }
            catch (Exception)
            {
                r.BornRegion = (PersonBornRegion)GameObject.Random(Enum.GetNames(typeof(PersonBornRegion)).Length);
            }
            return bornArch;
        }

        private static void AdjustChildrenIdeal(Person father, Person mother, Person r)
        {
            r.IdealTendency = GameObject.Chance(84) ? (GameObject.Chance(50) ? father.IdealTendency : mother.IdealTendency) : Session.Current.Scenario.GameCommonData.AllIdealTendencyKinds.GetRandomList()[0] as IdealTendencyKind;
            if (father.BelongedFaction != null || mother.BelongedFaction != null)
            {
                Person leader = father.BelongedFaction == null ? mother.BelongedFaction.Leader : father.BelongedFaction.Leader;
                if (r.IdealTendency.Offset < Person.GetIdealOffset(r, leader))
                {
                    if (leader.IdealTendency.Offset >= 0)
                    {
                        r.Ideal = leader.Ideal + GameObject.Random(r.IdealTendency.Offset * 2 + 1) - r.IdealTendency.Offset;
                        r.Ideal = (r.Ideal + 150) % 150;
                    }
                    else
                    {
                        r.Ideal = leader.Ideal;
                    }
                }
            }
        }

        private static void HandleChildrenProperty(Person father, Person mother, Person r)
        {
            r.Ideal = GameObject.Chance(50) ? father.Ideal + GameObject.Random(10) - 5 : mother.Ideal + GameObject.Random(10) - 5;
            r.Ideal = (r.Ideal + 150) % 150;

            r.Reputation = (int)(Math.Min(200000, father.Reputation + mother.Reputation) * (GameObject.Random(100) / 100.0 * 0.05 + 0.025)) + father.childrenReputationIncrease + mother.childrenReputationIncrease;
            r.Karma = (int)(Math.Max(-2000, Math.Min(500, father.Karma + mother.Karma)) * (GameObject.Random(100) / 100.0 * 0.2 + 0.1));

            r.PersonalLoyalty = (GameObject.Chance(50) ? father.PersonalLoyalty : mother.PersonalLoyalty) + GameObject.Random(3) - 1;
            if (r.PersonalLoyalty < 0) r.PersonalLoyalty = 0;
            if ((int)r.PersonalLoyalty > Enum.GetNames(typeof(PersonLoyalty)).Length) r.PersonalLoyalty = Enum.GetNames(typeof(PersonLoyalty)).Length;

            r.Ambition = (GameObject.Chance(50) ? father.Ambition : mother.Ambition) + GameObject.Random(3) - 1;
            if (r.Ambition < 0) r.Ambition = 0;
            if ((int)r.Ambition > Enum.GetNames(typeof(PersonAmbition)).Length) r.Ambition = Enum.GetNames(typeof(PersonAmbition)).Length;

            r.Qualification = GameObject.Chance(84) ? (GameObject.Chance(50) ? father.Qualification : mother.Qualification) : (PersonQualification)GameObject.Random(Enum.GetNames(typeof(PersonQualification)).Length);

            r.Braveness = (GameObject.Chance(50) ? father.BaseBraveness : mother.BaseBraveness) + GameObject.Random(5) - 2;
            if (r.BaseBraveness < 1) r.Braveness = 1;
            if (r.BaseBraveness > 10 && !Session.GlobalVariables.createChildrenIgnoreLimit) r.Braveness = 10;

            r.Calmness = (GameObject.Chance(50) ? father.BaseCalmness : mother.BaseCalmness) + GameObject.Random(5) - 2;
            if (r.BaseCalmness < 1) r.Calmness = 1;
            if (r.BaseCalmness > 10 && !Session.GlobalVariables.createChildrenIgnoreLimit) r.Calmness = 10;

            r.ValuationOnGovernment = (GameObject.Chance(50) ? father.ValuationOnGovernment : mother.ValuationOnGovernment);

            r.StrategyTendency = (GameObject.Chance(50) ? father.StrategyTendency : mother.StrategyTendency);
        }

        private static void HandleChildrenStatus(Person father, Person r)
        {
            r.YearBorn = Session.Current.Scenario.Date.Year;
            r.YearAvailable = Session.Current.Scenario.Date.Year + Session.GlobalVariables.ChildrenAvailableAge;
            r.YearDead = r.YearBorn + GameObject.Random(Session.Current.Scenario.GameCommonData.PersonGeneratorSetting.dieLo, Session.Current.Scenario.GameCommonData.PersonGeneratorSetting.dieHi);
            if (r.YearDead - r.YearAvailable < Session.Current.Scenario.GameCommonData.PersonGeneratorSetting.debutAtLeast)
            {
                r.YearDead = r.YearAvailable + Session.Current.Scenario.GameCommonData.PersonGeneratorSetting.debutAtLeast;
            }

            if (r.Spouse != null && Session.GlobalVariables.PersonNaturalDeath == true && Math.Abs(r.Spouse.YearBorn - r.YearBorn) > 25)
            {
                r.Spouse.Spouse = null;
                r.Spouse = null;
            }
        }

        private static void HandleChildrenType(Person father, Person mother, Person r)
        {
            int var = 5; //variance / maximum divert from parent ability
            r.BaseCommand = GameObject.Random(Math.Abs(father.InheritableCommand - mother.InheritableCommand) + 2 * var + 1) + Math.Min(father.InheritableCommand, mother.InheritableCommand) - var + father.childrenAbilityIncrease + mother.childrenAbilityIncrease;
            r.BaseStrength = GameObject.Random(Math.Abs(father.InheritableStrength - mother.InheritableStrength) + 2 * var + 1) + Math.Min(father.InheritableStrength, mother.InheritableStrength) - var + father.childrenAbilityIncrease + mother.childrenAbilityIncrease;
            r.BaseIntelligence = GameObject.Random(Math.Abs(father.InheritableIntelligence - mother.InheritableIntelligence) + 2 * var + 1) + Math.Min(father.InheritableIntelligence, mother.InheritableIntelligence) - var + father.childrenAbilityIncrease + mother.childrenAbilityIncrease;
            r.BasePolitics = GameObject.Random(Math.Abs(father.InheritablePolitics - mother.InheritablePolitics) + 2 * var + 1) + Math.Min(father.InheritablePolitics, mother.InheritablePolitics) - var + father.childrenAbilityIncrease + mother.childrenAbilityIncrease;
            r.BaseGlamour = GameObject.Random(Math.Abs(father.InheritableGlamour - mother.InheritableGlamour) + 2 * var + 1) + Math.Min(father.InheritableGlamour, mother.InheritableGlamour) - var + father.childrenAbilityIncrease + mother.childrenAbilityIncrease;

            r.BaseCommand = (int) (r.BaseCommand * Session.GlobalVariables.ChildrenAbilityFactor);
            r.BaseStrength = (int)(r.BaseStrength * Session.GlobalVariables.ChildrenAbilityFactor);
            r.BaseIntelligence = (int)(r.BaseIntelligence * Session.GlobalVariables.ChildrenAbilityFactor);
            r.BasePolitics = (int)(r.BasePolitics * Session.GlobalVariables.ChildrenAbilityFactor);
            r.BaseGlamour = (int)(r.BaseGlamour * Session.GlobalVariables.ChildrenAbilityFactor);

            if (!Session.GlobalVariables.createChildrenIgnoreLimit)
            {
                if (r.BaseStrength > 100) r.BaseStrength = 100;
                if (r.BaseStrength < 0) r.BaseStrength = 0;
                if (r.BaseCommand > 100) r.BaseCommand = 100;
                if (r.BaseCommand < 0) r.BaseCommand = 0;
                if (r.BaseIntelligence > 100) r.BaseIntelligence = 100;
                if (r.BaseIntelligence < 0) r.BaseIntelligence = 0;
                if (r.BasePolitics > 100) r.BasePolitics = 100;
                if (r.BasePolitics < 0) r.BasePolitics = 0;
                if (r.BaseGlamour > 100) r.BaseGlamour = 100;
                if (r.BaseGlamour < 0) r.BaseGlamour = 0;
            }

            setNewOfficerFace(r);
        }

        private static void HandleChildrenName(Person father, Person r)
        {
            r.SurName = father.SurName;
            List<String> givenNameList = r.Sex ? Person.readTextList("Content/Data/femalegivenname.txt") : Person.readTextList("Content/Data/malegivenname.txt");
            r.GivenName = givenNameList[GameObject.Random(givenNameList.Count)];
            if (r.GivenName.Length <= 1 && GameObject.Chance(r.Sex ? 90 : 10))
            {
                String s;
                int tries = 0;
                do
                {
                    s = givenNameList[GameObject.Random(givenNameList.Count)];
                    tries++;
                } while (s.Length > 1 && tries < 100);
                r.GivenName += s;
            }
            r.CalledName = "";
        }

        private static void HandleChildrenRelation(Person father, Person mother, Person r)
        {
            r.Father = father;
            r.Mother = mother;
            r.Generation = father.Generation + 1;
            r.Strain = father.Strain;

            r.Sex = GameObject.Chance(Session.Current.Scenario.GameCommonData.PersonGeneratorSetting.ChildrenFemaleChance) ? true : false;
        }

        private static Person HandleChildrenId(Person father)
        {
            Person r = new Person();

            //look for empty id
            int id = 5000;
            PersonList pl = Session.Current.Scenario.Persons as PersonList;
            pl.SmallToBig = true;
            pl.IsNumber = true;
            pl.PropertyName = "ID";
            pl.ReSort();
            foreach (Person p in pl)
            {
                if (p.ID == id)
                {
                    id++;
                    if (id >= 7000 && id < 10000)
                    {
                        id = 10000;
                    }
                }
                else if (p.ID > id)
                {
                    break;
                }
            }
            r.ID = id;
            return r;
        }

        public bool IsCloseTo(Person p)
        {
            return this.IsVeryCloseTo(p) || this.Closes(p);
        }

        public bool IsVeryCloseTo(Person p)
        {
            return this.Spouse == p || this.Brothers.GameObjects.Contains(p);
        }

        public PersonList AvailableVeryClosePersons
        {
            get
            {
                PersonList result = new PersonList();
                if (this.Spouse != null && this.Spouse.Status == PersonStatus.Normal && this.Spouse.BelongedFaction == this.BelongedFaction && this.Spouse.BelongedArchitecture != null && this.BelongedArchitecture != null
                            && (!Session.Current.Scenario.IsPlayer(this.BelongedFaction) || this.Spouse.BelongedArchitecture.BelongedSection == this.BelongedArchitecture.BelongedSection))
                {
                    result.Add(this.Spouse);
                }
                foreach (Person q in this.Brothers)
                {
                    if (q.Status == PersonStatus.Normal && q.BelongedFaction == this.BelongedFaction && this.BelongedArchitecture != null && q.BelongedArchitecture != null
                        && (!Session.Current.Scenario.IsPlayer(this.BelongedFaction) || q.BelongedArchitecture.BelongedSection == this.BelongedArchitecture.BelongedSection))
                    {
                        result.Add(q);
                    }
                }
                return result;
            }
        }

        public bool DontMoveMeUnlessIMust
        {
            get
            {
                return this.HasFollowingArmy || this.HasLeadingArmy || this.WaitForFeiZi != null ||
                        (this == this.BelongedFaction.Leader && this.LocationArchitecture.meifaxianhuaiyundefeiziliebiao().Count > 0);
            }
        }

        public bool HasCloseStrainTo(Person b)
        {
            if (this.Father == b) return true;
            if (this.Mother == b) return true;

            if (this.Father != null && b.Father == this.Father) return true;
            if (this.Mother != null && b.Mother == this.Mother) return true;

            if (b.Father == this) return true;
            if (b.Mother == this) return true;

            return false;
        }

        public bool HasStrainTo(Person b)
        {
            if (this.HasCloseStrainTo(b)) return true;

            if (this.Strain == b.Strain) return true;

            if (this.Mother != null)
            {
                
                if (this.Mother.Strain == b.Strain)
                {

                    return true;
                }
                        
                
            }

            if (b.Mother != null)
            {
               
                        if (b.Mother.Strain == this.Strain)
                        {
                            return true;
                        }
                
            }

            return false;
        }

        public bool isLegalFeiZi(Person b, bool princess = false)
        {
            if (this == b) return false;

            if (this.Sex == b.Sex) return false;

            if (Session.GlobalVariables.PersonNaturalDeath == true)
            {
                if ((b.Age < 16 || this.Age < 16)) return false;

                if ((b.Age > 50 + (b.Sex ? 0 : 10)) || (this.Age > 50 + (this.Sex ? 0 : 10))) return false;

                if (!princess)
                {
                    if ((Math.Abs(this.Age - b.Age) > 25)) return false;
                }
            }

            if (this.HasStrainTo(b)) return false;

            return true;
        }

        public bool isLegalFeiZiExcludeAge(Person b)
        {
            if (this == b) return false;

            if (this.Sex == b.Sex) return false;
            
            if (this.HasStrainTo(b)) return false;

            return true;
        }

        public Person XuanZeMeiNv(Person nvren)
        {
            Person tookSpouse = null;
            Person leader = this.LocationArchitecture.BelongedFaction.Leader;

            if (!this.LocationArchitecture.BelongedFaction.IsAlien)
            {
                nvren.LocationArchitecture.DecreaseFund(Session.Parameters.NafeiCost);
            }

            bool addHate = false;
            if (nvren.Status == PersonStatus.Captive)
            {
                nvren.SetBelongedCaptive(null, PersonStatus.Normal);
                nvren.ChangeFaction(this.BelongedFaction);
                addHate = true;
            }
            else if (nvren.Status == PersonStatus.NoFaction)
            {
                addHate = true;
            }

            if (addHate)
            {
                nvren.AdjustRelation(leader, 0, -200 * nvren.PersonalLoyalty * nvren.PersonalLoyalty);
                this.DecreaseKarma(1 + nvren.PersonalLoyalty * 2 + Math.Max(0, this.Karma / 5));

                foreach (Person p in Session.Current.Scenario.Persons)
                {
                    if (p == nvren) continue;
                    if (p == leader) continue;
                    if (p.IsVeryCloseTo(nvren))
                    {
                        p.AdjustRelation(leader, 0, -50 * p.PersonalLoyalty * p.PersonalLoyalty);
                    }
                    if (p.HasCloseStrainTo(nvren))
                    {
                        p.AdjustRelation(leader, 0, -50 * p.PersonalLoyalty * p.PersonalLoyalty);
                    }
                }
            }

            nvren.Status = PersonStatus.Princess;
            nvren.workKind = ArchitectureWorkKind.无;

            nvren.PrincessTaker = this.BelongedFactionWithPrincess.Leader;

            nvren.LocationTroop = null;
            nvren.TargetArchitecture = null;

            makeHateCausedByAffair(leader, nvren, leader);

            if (nvren.Spouse != null)
            {
                Person p = new Person();
                foreach (Person person in Session.Current.Scenario.Persons)
                {
                    if (person == nvren.Spouse)
                    {
                        p = person;
                        break;
                    }
                }

                if ((p != null) && p.ID != nvren.LocationArchitecture.BelongedFaction.LeaderID)
                {
                    if (p.Alive)
                    {
                        tookSpouse = p;
                        this.LoseReputationBy(0.05f);

                        this.DecreaseKarma(1 + p.PersonalLoyalty * 2 + Math.Max(0, this.Karma / 5));

                        p.AddHated(this.BelongedFaction.Leader, -200 * p.PersonalLoyalty * p.PersonalLoyalty);
                    }
                }
            }// end if (this.CurrentPerson.Spouse != -1)

            if (nvren.Spouse != null && nvren.Spouse != leader)
            {
                if (nvren.Spouse.Spouse == nvren && (nvren.Spouse.PersonalLoyalty < Session.Current.Scenario.GlobalVariables.KeepSpousePersonalLoyalty || !nvren.Spouse.Sex))
                {
                    nvren.Spouse.Spouse = null;
                }
                if (nvren.PersonalLoyalty < Session.Current.Scenario.GlobalVariables.KeepSpousePersonalLoyalty)
                {
                    nvren.Spouse = null;
                }
            }

            Session.Current.Scenario.YearTable.addBecomePrincessEntry(Session.Current.Scenario.Date, nvren, this);

            ExtensionInterface.call("TakeToHouGong", new Object[] { Session.Current.Scenario, this, nvren });

            return tookSpouse;
        }

        public void GoForHouGong(Person nvren)
        {
            if (this.LocationArchitecture != null && this.Status == PersonStatus.Normal)
            {
                int houGongDays = nvren.Glamour / 4 + GameObject.Random(6) + 10;
                if (houGongDays > 60)
                {
                    houGongDays = GameObject.Random(10) + 60;
                }
                houGongDays /= 2;

                PersonList all = new PersonList();
                all.Add(nvren);

                foreach (Person q in all)
                {
                    if (this.BelongedFaction.hougongValid &&
                        ((q.Sex && q.huaiyuntianshu >= -1) || (this.Sex && this.huaiyuntianshu >= -1)) &&
                        this.NumberOfChildren < Session.GlobalVariables.OfficerChildrenLimit && q.NumberOfChildren < Session.GlobalVariables.OfficerChildrenLimit)
                    {
                        float extraRate = q.PregnancyRate(this);

                        float pregnantChance = Session.GlobalVariables.hougongGetChildrenRate / 100.0f * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIExtraPerson);
                        pregnantChance *= houGongDays * 2 * extraRate;

                        if (GameObject.Chance(Math.Max((int)pregnantChance, Session.Parameters.MinPregnantProb))
                            && !q.huaiyun && !this.huaiyun && this.isLegalFeiZiExcludeAge(q) &&
                            (this.LocationArchitecture.BelongedFaction.Leader.meichushengdehaiziliebiao().Count - this.LocationArchitecture.yihuaiyundefeiziliebiao().Count > 0 || Session.GlobalVariables.createChildren))
                        {
                            q.suoshurenwu = this.ID;
                            this.suoshurenwu = q.ID;
                            if (q.Sex)
                            {
                                q.huaiyun = true;
                                q.huaiyuntianshu = -GameObject.Random(houGongDays);
                            }
                            else
                            {
                                this.huaiyun = true;
                                this.huaiyuntianshu = -GameObject.Random(houGongDays);
                            }
                        }
                    }

                    if (!q.WillHateIfChongxing)
                    {
                        this.AdjustRelation(q, houGongDays / 5.0f * (this.Glamour / 100.0f), 0);
                        q.AdjustRelation(this, houGongDays / 5.0f * (this.Glamour / 100.0f), 0);
                    }

                    if (!q.Hates(this) && !this.Hates(q))
                    {
                        if (GameObject.Random(10000 / (this.CommandExperience + 1)) == 0)
                        {
                            q.AddCommandExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.StrengthExperience + 1)) == 0)
                        {
                            q.AddStrengthExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.IntelligenceExperience + 1)) == 0)
                        {
                            q.AddIntelligenceExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.PoliticsExperience + 1)) == 0)
                        {
                            q.AddPoliticsExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.GlamourExperience + 1)) == 0)
                        {
                            q.AddGlamourExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.BubingExperience + 1)) == 0)
                        {
                            q.AddBubingExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.NubingExperience + 1)) == 0)
                        {
                            q.AddNubingExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.QibingExperience + 1)) == 0)
                        {
                            q.AddQibingExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.ShuijunExperience + 1)) == 0)
                        {
                            q.AddShuijunExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.QixieExperience + 1)) == 0)
                        {
                            q.AddQixieExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.TacticsExperience + 1)) == 0)
                        {
                            q.AddTacticsExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.StratagemExperience + 1)) == 0)
                        {
                            q.AddStratagemExperience(GameObject.Random(houGongDays) + 1);
                        }
                        if (GameObject.Random(10000 / (this.InternalExperience + 1)) == 0)
                        {
                            q.AddInternalExperience(GameObject.Random(houGongDays) + 1);
                        }
                        this.AddGlamourExperience(houGongDays);
                        q.AddGlamourExperience(houGongDays);
                        q.IncreaseReputation(houGongDays * 2);
                    }

                    if (this.huaiyun) break;
                }

                float factor = Session.Current.Scenario.Parameters.HougongRelationHateFactor;
                foreach (Person p in this.BelongedFaction.GetFeiziList())
                {
                    if (((Session.GlobalVariables.PersonNaturalDeath == true && p.Age >= 40) || 
                        (p.WillHateIfChongxing || p.Spouse == p.BelongedFactionWithPrincess.Leader)) && !p.Hates(this))
                    {
                        factor *= 0.9f + (100 - p.Glamour) * 0.001f - (p.Age - 40) * 0.01f;
                    }
                }

                foreach (Person p in this.BelongedFaction.GetFeiziList())
                {
                    if (p == nvren) continue;
                    if (p.faxianhuaiyun || this.faxianhuaiyun) continue;
                    if (((Session.GlobalVariables.PersonNaturalDeath == true && p.Age >= 50) ||
                        (p.WillHateIfChongxing || p.Spouse == p.BelongedFactionWithPrincess.Leader)) && !p.Hates(this)) continue;

                    p.AdjustRelation(this, -houGongDays / 60.0f * (4 - p.PersonalLoyalty) * factor * (Math.Min(10, 50 - p.Age) / 10.0f), -2);
                    p.AdjustRelation(nvren, -houGongDays / 60.0f * (4 - p.PersonalLoyalty) * factor * (Math.Min(10, 50 - p.Age) / 10.0f), -2);
                }

                makeHateCausedByAffair(this, nvren, this);

                if (GameObject.Chance(20) && nvren.GetRelation(this) >= Session.Parameters.VeryCloseThreshold && nvren.Spouse == null && 
                    this.isLegalFeiZi(nvren, true) && nvren.isLegalFeiZi(this, true) && !this.Hates(nvren))
                {
                    nvren.Spouse = this;

                    Session.Current.Scenario.YearTable.addCreateSpouseEntry(Session.Current.Scenario.Date, this, nvren);
                    if (this.OnCreateSpouse != null)
                    {
                        this.OnCreateSpouse(this, nvren);
                    }
                }

                this.OutsideTask = OutsideTaskKind.后宮;
                this.TargetArchitecture = this.LocationArchitecture;
                this.ArrivingDays = houGongDays / Session.Parameters.DayInTurn + 1;
                this.Status = PersonStatus.Moving;
                this.TaskDays = this.ArrivingDays;
                ExtensionInterface.call("GoForHouGong", new Object[] { Session.Current.Scenario, this, nvren });
            }
        }

        public bool WillHateIfChongxing
        {
            get
            {
                return (this.Spouse != null && (this.PersonalLoyalty >= 4 || (this.PersonalLoyalty >= 2 && this.Spouse.Alive))) && (this.BelongedFactionWithPrincess == null || this.Spouse != this.BelongedFactionWithPrincess.Leader);
            }
        }

        public static Dictionary<Person, PersonList> willHateCausedByAffair(Person p, Person q, Person causer, GameObjectList suoshurenwuList, bool simulateMarry)
        {
            Dictionary<Person, PersonList> result = new Dictionary<Person, PersonList>();
            foreach (Person i in suoshurenwuList)
            {
                PersonList t = new PersonList();
                if (i.Status != PersonStatus.Princess)
                {
                    bool unwanted = false;
                    if (i.Spouse == p) continue;
                    if (i.Spouse == q) continue;
                    if (i == p) continue;
                    if (i == q) continue;
                    if (i != null && i != p && p.Status != PersonStatus.Princess && !i.IsCloseTo(p) && p.Spouse != i && !t.HasGameObject(p) && !i.Hates(p) && (!simulateMarry || (i != p && i != q && i != causer)))
                    {
                        t.Add(p);
                        unwanted = true;
                    }
                    if (i != null && i != q && q.Status != PersonStatus.Princess && !i.IsCloseTo(q) && q.Spouse != i && !t.HasGameObject(q) && !i.Hates(q) && (!simulateMarry || (i != p && i != q && i != causer)))
                    {
                        t.Add(q);
                        unwanted = true;
                    }
                    if (unwanted)
                    {
                        if (i != null && i != causer && causer.Status != PersonStatus.Princess && !i.IsCloseTo(causer) && causer.Spouse != i && !t.HasGameObject(causer) && !i.Hates(causer) && (!simulateMarry || (i != p && i != q && i != causer)))
                        {
                            t.Add(causer);
                        }
                    }
                }
                if (result.ContainsKey(i))
                {
                    t.AddRange(result[i]);
                    result[i] = t;
                }
                else
                {
                    result.Add(i, t);
                }
            }

            if (p.Spouse != null && p.WillHateIfChongxing)
            {
                PersonList t = new PersonList();
                if (p != q && !p.Hates(q) && !p.IsVeryCloseTo(q) && !t.HasGameObject(q) && q.Status != PersonStatus.Princess)
                {
                    t.Add(q);
                }
                if (p != causer && !p.Hates(causer) && !p.IsVeryCloseTo(causer) && !t.HasGameObject(causer) && causer.Status != PersonStatus.Princess)
                {
                    t.Add(causer);
                }
                if (result.ContainsKey(p))
                {
                    t.AddRange(result[p]);
                    result[p] = t;
                }
                else
                {
                    result.Add(p, t);
                }

                PersonList u = new PersonList();
                if (p.Spouse != q && !p.Spouse.Hates(q) && !p.Spouse.IsVeryCloseTo(q) && !u.HasGameObject(q) && q.Status != PersonStatus.Princess)
                {
                    u.Add(q);
                    if (p.Spouse != causer && !p.Spouse.Hates(causer) && !p.Spouse.IsVeryCloseTo(causer) && !u.HasGameObject(causer) && causer.Status != PersonStatus.Princess)
                    {
                        u.Add(causer);
                    }
                }
                
                if (result.ContainsKey(p.Spouse))
                {
                    u.AddRange(result[p.Spouse]);
                    result[p.Spouse] = u;
                }
                else
                {
                    result.Add(p.Spouse, u);
                }
            }
            if (q.Spouse != null && q.WillHateIfChongxing)
            {
                PersonList t = new PersonList();
                if (q != p && !q.Hates(p) && !q.IsVeryCloseTo(p) && !t.HasGameObject(p) && p.Status != PersonStatus.Princess)
                {
                    t.Add(p);
                    if (q != causer && !q.Hates(causer) && !q.IsVeryCloseTo(causer) && !t.HasGameObject(causer) && causer.Status != PersonStatus.Princess)
                    {
                        t.Add(causer);
                    }
                }
               
                if (result.ContainsKey(q))
                {
                    t.AddRange(result[q]);
                    result[q] = t;
                }
                else
                {
                    result.Add(q, t);
                }

                PersonList u = new PersonList();
                if (q.Spouse != p && !q.Spouse.Hates(p) && !q.Spouse.IsVeryCloseTo(p) && !u.HasGameObject(p) && p.Status != PersonStatus.Princess)
                {
                    u.Add(p);
                    if (q.Spouse != causer && !q.Spouse.Hates(causer) && !q.Spouse.IsVeryCloseTo(causer) && !u.HasGameObject(causer) && causer.Status != PersonStatus.Princess)
                    {
                        u.Add(causer);
                    }
                }
                
                if (result.ContainsKey(q.Spouse))
                {
                    u.AddRange(result[q.Spouse]);
                    result[q.Spouse] = u;
                }
                else
                {
                    result.Add(q.Spouse, u);
                }
            }

            return result;
        }

        public void makeHateCausedByAffair(Person p, Person q, Person causer)
        {
            GameObjectList list = p.suoshurenwuList.GetList();
            list.AddRange(q.suoshurenwuList);
            Dictionary<Person, PersonList> t = Person.willHateCausedByAffair(p, q, causer, list, false);
            foreach (KeyValuePair<Person, PersonList> i in t)
            {
                foreach (Person j in i.Value)
                {
                    i.Key.AddHated(j, -100 * i.Key.PersonalLoyalty * i.Key.PersonalLoyalty);
                }
            }

            if (!p.suoshurenwuList.HasGameObject(q))
            {
                p.suoshurenwuList.Add(q);
            }
            if (!q.suoshurenwuList.HasGameObject(p))
            {
                q.suoshurenwuList.Add(p);
            }
        }

        public void feiziRelease()
        {
            if (this.Status == PersonStatus.Princess)
            {
                this.Status = PersonStatus.Normal;
                Captive captive = Captive.Create(this, null);
                /*
                if (!this.Hates(this.BelongedFaction.Leader) && this.Spouse != null && this.Spouse != this.BelongedFaction.Leader)
                {
                    this.AdjustRelation(this.BelongedFaction.Leader, 20, 5);
                    this.Spouse.AdjustRelation(this.BelongedFaction.Leader, 20, 5);

                    this.BelongedFaction.Leader.IncreaseReputation(300);
                }
                */
                Session.Current.Scenario.YearTable.addReleaseFromPrincessEntry(Session.Current.Scenario.Date, this, this.BelongedFaction.Leader);
                captive.TransformToNoFactionCaptive();
            }
        }

        public String FoundPregnantString
        {
            get
            {
                return faxianhuaiyun ? "○" : "×";
            }
        }

        public String OfficerString
        {
            get
            {
                if (this.ID >= 25000)
                {
                    return "○";
                }
                return "×";
            }
        }

        public int PregnantDayForShowing
        {
            get
            {
                return faxianhuaiyun ? (huaiyuntianshu < 30 ? 1 : huaiyuntianshu / 30) : 0;
            }
        }

        public MilitaryList LeadingArmies
        {
            get
            {
                MilitaryList result = new MilitaryList();
                if (this.LocationArchitecture != null)
                {
                    foreach (Military i in this.LocationArchitecture.Militaries)
                    {
                        if ((i.FollowedLeader == this || (i.Leader == this && i.Experience > 10)) && !i.IsTransport)
                        {
                            result.Add(i);
                        }
                    }
                }
                return result;
            }
        }

        public bool HasFollowingArmy
        {
            get
            {
                if (this.LocationArchitecture != null)
                {
                    foreach (Military i in this.LocationArchitecture.Militaries)
                    {
                        if (i.FollowedLeader == this && !i.IsTransport)
                        {
                            return true;
                        }
                    }
                }
                else if (this.LocationTroop != null)
                {
                    if (this.LocationTroop.Army.FollowedLeader == this)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasEffectiveLeadingArmy
        {
            get
            {
                if (this.LocationArchitecture != null)
                {
                    foreach (Military i in this.LocationArchitecture.Militaries)
                    {
                        if ((i.Leader == this && i.Experience > 10 && !i.IsTransport) || i.FollowedLeader == this)
                        {
                            return true;
                        }
                    }
                }
                else if (this.LocationTroop != null)
                {
                    if (this.LocationTroop.Army.Experience > 10 || this.LocationTroop.Army.FollowedLeader == this)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasLeadingArmy
        {
            get
            {
                if (this.LocationArchitecture != null)
                {
                    foreach (Military i in this.LocationArchitecture.Militaries)
                    {
                        if ((i.Leader == this || i.FollowedLeader == this) && !i.IsTransport)
                        {
                            return true;
                        }
                    }
                }
                else if (this.LocationTroop != null)
                {
                    if (this.LocationTroop.Leader == this)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsCivil()
        {
            if (this.BaseStrength > 70)
            {
                return false;
            }
            if (this.BaseIntelligence > 60 && this.BaseIntelligence - this.BaseStrength > 20)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool IsBeingTroopPerson
        {
            get
            {
                if (this.LocationArchitecture != null)
                {
                    foreach (Person p in this.LocationArchitecture.Persons)
                    {
                        if (p.preferredTroopPersons.GameObjects.Contains(this) && p.HasLeadingArmy)
                        {
                            return true;
                        }
                    }
                }
                else if (this.LocationTroop != null)
                {
                    if (this.LocationTroop.Army.Leader != this)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool TooTiredToBattle
        {
            get
            {
                return this.Tiredness > this.Braveness * 10 + 30 || this.InjureRate < Math.Max(0.3, 0.8 - this.Braveness * 0.05);
            }
        }

        public int Identity()
        {
            if (this.ID == 7108)  //盗贼
            {
                return 0;
            }
            else if (this.BelongedFaction != null && this == this.BelongedFaction.Leader)  //君主
            {
                return 2;
            }
            else  //普通武将
            {
                return 1;
            }

        }

        public float RelationAbilityFactor
        {
            get
            {
                if (this.LocationTroop == null || this.LocationArchitecture != null) return 1f;

                float rate = 1f;
                foreach (Person p in this.LocationTroop.Persons)
                {
                    if (p == this) continue;
                    float r = 1.0f;
                    if (this.Brothers.GameObjects.Contains(p))
                    {
                        r = (float) ((Session.Parameters.VeryCloseAbilityRate - 1) * Math.Sqrt((float)this.GetRelation(p) / Session.Parameters.VeryCloseThreshold) + 1);
                    }
                    if (!this.Hates(p) && !p.Hates(this))
                    {
                        if (this.Father == p || this.Mother == p)
                        {
                            r = Session.Parameters.CloseAbilityRate;
                        }
                        else if (this == p.Father || this == p.Mother)
                        {
                            r = Session.Parameters.CloseAbilityRate;
                        }
                        else if (this.Father != null && this.Mother != null && p.Father != null && p.Mother != null &&
                          this.Father == p.Father && this.Mother == p.Mother)
                        {
                            r = Session.Parameters.CloseAbilityRate;
                        }
                    }
                    if (r > rate)
                    {
                        rate = r;
                    }
                }
                return rate;
            }
        }

        public bool Closes(Person p)
        {
            return this.closePersons.GameObjects.Contains(p);
        }

        public bool Hates(Person p)
        {
            return this.hatedPersons.GameObjects.Contains(p);
        }

        public void AddHated(Person p)
        {
            AddHated(p, -500);
        }

        public void AddHated(Person p, int relation)
        {
            if (p != null && p != this && !this.Hates(p))
            {
                this.hatedPersons.Add(p);
                this.EnsureRelationAtMost(p, relation);
                this.EnsureRelationAtMost(p, Session.Parameters.HateThreshold);
            }
        }

        public void AddClose(Person p)
        {
            if (p != null && p != this && !this.Closes(p))
            {
                this.closePersons.Add(p);
                this.EnsureRelationAtLeast(p, Session.Parameters.CloseThreshold);
            }
        }

        public void RemoveClose(Person p)
        {
            this.closePersons.Remove(p);
            this.EnsureRelationAtMost(p, Session.Parameters.CloseThreshold / 2);
        }

        public void RemoveHated(Person p)
        {
            this.hatedPersons.Remove(p);
            this.EnsureRelationAtLeast(p, Session.Parameters.HateThreshold / 2);
        }

        public PersonList GetClosePersons()
        {
            PersonList pl = new PersonList();
            foreach (Person p in this.closePersons)
            {
                pl.Add(p);
            }
            return pl;
        }

        public PersonList GetHatedPersons()
        {
            PersonList pl = new PersonList();
            foreach (Person p in this.hatedPersons)
            {
                pl.Add(p);
            }
            return pl;
        }

        public int RelationToLeader
        {
            get
            {
                if (this.BelongedFactionWithPrincess == null) return 0;
                return GetRelation(this.BelongedFactionWithPrincess.Leader);
            }
        }

        public Dictionary<Person, int> GetRelations()
        {
            if (!Session.GlobalVariables.EnablePersonRelations) return new Dictionary<Person, int>();
            return new Dictionary<Person, int>(relations);
        }

        public int GetRelation(Person p)
        {
            if (!Session.GlobalVariables.EnablePersonRelations) return 0;
            if (p == null) return 0;
            if (this.relations.ContainsKey(p))
            {
                return this.relations[p];
            }
            else
            {
                return 0;
            }
        }

        public void SetRelation(Person p, int val)
        {
            if (!Session.GlobalVariables.EnablePersonRelations) return;
            if (this == p) return;
            if (this.relations.ContainsKey(p))
            {
                if (val == 0)
                {
                    this.relations.Remove(p);
                }
                else
                {
                    this.relations[p] = val;
                }
            }
            else
            {
                if (val != 0)
                {
                    this.relations.Add(p, val);
                }
            }
        }

        public Dictionary<Person, int> GetAllRelations()
        {
            return new Dictionary<Person, int>(this.relations);
        }

        public void AdjustRelation(Person p, float factor, float adjust)
        {
            if (!Session.GlobalVariables.EnablePersonRelations) return;
            if (this == p || p == null) return;
            float val;

            float offset = Math.Max(0, Math.Min(75, 37.5f - Person.GetIdealAttraction(p, this) / 2));

            if (factor > 0)
            {
                val = (int)(Math.Max(0, 75 - offset) * 30 * factor / 75 + adjust);
            }
            else
            {
                val = (int)(offset * 30 * factor / 75 + adjust);
            }
            if (val != 0)
            {
                if (this.relations.ContainsKey(p))
                {
                    float actualVal;
                    if (this.relations[p] > 0)
                    {
                        actualVal = val * ((float) Session.Parameters.MaxRelation - this.relations[p]) / Session.Parameters.MaxRelation * 2;
                    }
                    else
                    {
                        actualVal = val;
                    }
                    this.relations[p] = (int)(this.relations[p] + actualVal);
                    if (this.relations[p] > Session.Parameters.MaxRelation)
                    {
                        this.relations[p] = Session.Parameters.MaxRelation;
                    }
                }
                else
                {
                    this.relations.Add(p, (int) val);
                }
            }
            else
            {
                return;
            }

            if (this.relations.ContainsKey(p))
            {
                if (this.relations[p] <= Session.Parameters.HateThreshold && !this.Hates(p))
                {
                    this.AddHated(p);
                }
                if (this.relations[p] >= Session.Parameters.HateThreshold / 2 && this.Hates(p))
                {
                    this.RemoveHated(p);
                }
                if (this.relations[p] <= Session.Parameters.CloseThreshold / 2 && this.Closes(p))
                {
                    this.RemoveClose(p);
                }
                if (this.relations[p] >= Session.Parameters.CloseThreshold && !this.Closes(p))
                {
                    this.AddClose(p);
                }
            }
        }

        public void EnsureRelationAtMost(Person p, int val)
        {
            if (!Session.GlobalVariables.EnablePersonRelations) return;
            if (this.relations.ContainsKey(p))
            {
                if (this.relations[p] > val)
                {
                    this.relations[p] = val;
                }
            }
            else
            {
                this.relations[p] = val;
            }
        }

        public void EnsureRelationAtLeast(Person p, int val)
        {
            if (!Session.GlobalVariables.EnablePersonRelations) return;
            if (this.relations.ContainsKey(p))
            {
                if (this.relations[p] < val)
                {
                    this.relations[p] = val;
                }
            }
            else
            {
                this.relations[p] = val;
            }
        }

        public delegate void CreateSpouse(Person p, Person q);

        public delegate void CreateBrother(Person p, Person q);

        public delegate void CreateSister(Person p, Person q);

        private class PersonSpecialRelationComparer : Comparer<KeyValuePair<int, Person>>
        {
            public override int Compare(KeyValuePair<int, Person> x, KeyValuePair<int, Person> y)
            {
                return Comparer<int>.Default.Compare(x.Key, y.Key);
            }
        }

        public String PersonSpecialRelationString
        {
            get
            {
                String s = "";

                List<KeyValuePair<int, Person>> reverseRel = new List<KeyValuePair<int, Person>>();
                foreach (KeyValuePair<Person, int> pr in this.relations)
                {
                    reverseRel.Add(new KeyValuePair<int, Person>(pr.Value, pr.Key));
                }
                reverseRel.Sort(new PersonSpecialRelationComparer());

                int shown = 0;
                foreach (KeyValuePair<int, Person> pr in reverseRel)
                {
                    if (pr.Key < -100 && shown < 3)
                    {
                        if (pr.Value.Alive && pr.Value.Available)
                        {
                            s += pr.Value.Name + ":" + pr.Key + " ";
                            shown++;
                        }
                    }
                    else break;
                }

                shown = 0;
                reverseRel.Reverse();

                foreach (KeyValuePair<int, Person> pr in reverseRel)
                {
                    if (pr.Key > 100 && shown < 5)
                    {
                        if (pr.Value.Alive && pr.Value.Available)
                        {
                            s += pr.Value.Name + ":" + pr.Key + " ";
                            shown++;
                        }
                    }
                    else break;
                }

                return s;
            }
        }
        //以下添加20170226
        public String PersonSpecialRelationString1
        {
            get
            {
                String s = "";

                List<KeyValuePair<int, Person>> reverseRel = new List<KeyValuePair<int, Person>>();
                foreach (KeyValuePair<Person, int> pr in this.relations)
                {
                    reverseRel.Add(new KeyValuePair<int, Person>(pr.Value, pr.Key));
                }
                reverseRel.Sort(new PersonSpecialRelationComparer());

                int shown = 0;
                foreach (KeyValuePair<int, Person> pr in reverseRel)
                {
                    if (pr.Key < -100 && shown < 6)
                    {
                        if (pr.Value.Alive && pr.Value.Available)
                        {
                            s += pr.Value.Name + ":" + pr.Key + " ";
                            shown++;
                        }
                    }
                    else break;
                }
                return s;
            }
        }
        public String PersonSpecialRelationString2
        {
            get
            {
                String s = "";

                List<KeyValuePair<int, Person>> reverseRel = new List<KeyValuePair<int, Person>>();
                foreach (KeyValuePair<Person, int> pr in this.relations)
                {
                    reverseRel.Add(new KeyValuePair<int, Person>(pr.Value, pr.Key));
                }
                reverseRel.Sort(new PersonSpecialRelationComparer());

                int shown = 0;
                reverseRel.Reverse();
                foreach (KeyValuePair<int, Person> pr in reverseRel)
                {
                    if (pr.Key > 100 && shown < 6)
                    {
                        if (pr.Value.Alive && pr.Value.Available)
                        {
                            s += pr.Value.Name + ":" + pr.Key + " ";
                            shown++;
                        }
                    }
                    else break;
                }
                return s;
            }
        }
        //以上添加
        public static String GetInjuryString(float rate)
        {
            if (rate >= 1)
            {
                return "健康";
            }
            else if (rate >= 0.7)
            {
                return "轻伤";
            }
            else if (rate >= 0.3)
            {
                return "重伤";
            }
            else
            {
                return "濒危";
            }
        }

        public String InjuryString
        {
            get
            {
                return GetInjuryString(this.InjureRate);
            }
        }

        public bool HasInfluenceKind(int id)
        {
            foreach (Influence i in Session.Current.Scenario.GameCommonData.AllInfluences.Influences.Values)
            {
                if (i.Kind.ID == id)
                {
                    foreach (ApplyingPerson j in i.appliedPerson)
                    {
                        if (j.person == this)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        
      

        public float InfluenceKindValueByTreasure(int id)
        {
            float result = 0;
            foreach (Influence i in Session.Current.Scenario.GameCommonData.AllInfluences.Influences.Values)
            {
                if (i.Kind.ID == id)
                {
                    foreach (ApplyingPerson j in i.appliedPerson)
                    {
                        if (j.person == this && j.applier == Applier.Treasure)
                        {
                            result += i.Value;
                        }
                    }
                }
            }
            return result;
        }



        private Person Belongedperson;

        public Person belongedperson
        {
            get
            {
                return this.Belongedperson;
            }
            set
            {
                this.Belongedperson = value;
            }
        }


        private string belongedPersonName="";

        [DataMember]
        public string BelongedPersonName
        {
            get
            {
                if (this.belongedperson != null)
                {
                      return belongedperson.Name.ToString(); ;
                }
                else return belongedPersonName;
            }
            set
            {
                belongedPersonName = value;
            }
        }

        public bool IsValidTeacher
        {
            get
            {
                return this.Alive && (this.Status != PersonStatus.Captive);
            }
        }

        public bool Trainable
        {
            get
            {
                return this.IsGeneratedChildren && this.Alive && this.Age >= 4 && !(this.Available && this.Age >= 12);
            }
        }

        public PersonList TrainableChildren
        {
            get
            {
                PersonList result = new PersonList();
                foreach (Person p in this.ChildrenList)
                {
                    if (p.Trainable)
                    {
                        result.Add(p);
                    }
                }
                return result;
            }
        }

        public TrainPolicyList TrainPolicies()
        {
            return Session.Current.Scenario.GameCommonData.AllTrainPolicies;
        }

        public string TrainPolicyString
        {
            get
            {
                return !this.Trainable || this.TrainPolicy == null ? "----" : this.TrainPolicy.Name;
            }
        }

    }
}

