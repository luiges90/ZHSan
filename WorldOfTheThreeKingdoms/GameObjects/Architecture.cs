using GameGlobal;
using GameObjects.Animations;
using GameObjects.ArchitectureDetail;
using GameObjects.FactionDetail;
using GameObjects.Influences;
using GameObjects.MapDetail;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using GameObjects.Conditions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Runtime.Serialization;
using GameManager;

namespace GameObjects
{
    //using GameObjects.PersonDetail.GuanzhiKindTable;
    // using GameObjects.PersonDetail.PersonMessages;
    //using GameFreeText;
    [DataContract]
    public class Architecture : GameObject
    {
        class SimulatingFightingForceComparer : IComparer<Troop>
        {
            public int Compare(Troop x, Troop y)
            {
                return y.SimulatingFightingForce - x.SimulatingFightingForce;
            }
        }

        class FightingForceComparer : IComparer<Troop>
        {
            public int Compare(Troop x, Troop y)
            {
                return y.SimulatingFightingForce - x.SimulatingFightingForce;
            }
        }

        public void Init()
        {
            AIAllLinkNodes = new Dictionary<int, LinkNode>();

            AILandLinks = new ArchitectureList();

            AILinkProcedureDetails = new Queue<AILinkProcedureDetail>();

            AIWaterLinks = new ArchitectureList();

            BeMergedMilitaryList = new MilitaryList();

            BuildableFacilityKindList = new GameObjectList();

            actuallyUnreachableArch = new HashSet<Architecture>();

            CampaignMilitaryList = new MilitaryList();
            ChangeCapitalArchitectureList = new ArchitectureList();

            IncrementNumberList = new CombatNumberItemList(CombatNumberDirection.上);

            DecrementNumberList = new CombatNumberItemList(CombatNumberDirection.下);

            Characteristics = new InfluenceTable();

            Facilities = new FacilityList();

            FundPacks = new List<FundPack>();

            FoodPacks = new List<FoodPack>();

            MergeMilitaryList = new MilitaryList();

            Militaries = new MilitaryList();

            UpgradableMilitaryKindList = new MilitaryKindList();

            Informations = new InformationList();
            LevelUpMilitaryList = new MilitaryList();

            OtherArchitectureList = new ArchitectureList();

            NewMilitaryKindList = new MilitaryKindList();

            PopulationPacks = new List<PopulationPack>();
            MilitaryPopulationPacks = new List<PopulationPack>();
            PrivateMilitaryKinds = new MilitaryKindTable();

            RecruitmentMilitaryList = new MilitaryList();
            RedeemCaptiveList = new CaptiveList();
            ResetDiplomaticRelationList = new GameObjectList();
            EnhanceDiplomaticRelationList = new GameObjectList();
            AllyDiplomaticRelationList = new GameObjectList();
            TruceDiplomaticRelationList = new GameObjectList();
            DenounceDiplomaticRelationList = new GameObjectList();
            QuanXiangDiplomaticRelationList = new GameObjectList(); //劝降
            GeDiDiplomaticRelationList = new GameObjectList(); //割地
            RewardPersonList = new PersonList();
            RoutewayDestinationArchitectures = new Dictionary<int, Architecture>();
            RoutewayProcedures = new Queue<RoutewayProcedureDetail>();

            Routeways = new RoutewayList();

            ShelledMilitaryList = new MilitaryList();
            TrainingMilitaryList = new MilitaryList();
            TransferArchitectureList = new ArchitectureList();

            linkNodeRouteway = new Dictionary<LinkNode, Routeway>();

            //MayorID = -1;
            //buildingFacility = -1;
            PathRoutewayID = -1;

            pathFinder = new RoutewayPathFinder();

            CombativityOfRecruitment = 50;
            MoraleOfRecruitment = 50;

            DayLearnTitleDay = Session.Parameters.LearnTitleDays;

            MultipleOfRecovery = 1;
            MultipleOfTraining = 1;

            RateOfClearField = 1;
            RateOfConvincePerson = 1;
            RateOfDestroyArchitecture = 1;
            RateOfFacilityEnduranceDown = 1;
            RateOfFoodReduceRate = 1;
            RateOfGossipArchitecture = 1;
            RateOfHirePerson = 1;
            RateOfInstigateArchitecture = 1;
            RateOfJailBreakArchitecture = 1;
            RateOfNewBubingMilitaryFundCost = 1;
            RateOfNewNubingMilitaryFundCost = 1;
            RateOfNewQibingMilitaryFundCost = 1;
            RateOfNewQixieMilitaryFundCost = 1;
            RateOfNewShuijunMilitaryFundCost = 1;
            RateOfpublic = 1;
            RateOfRewardPerson = 1;
            RateOfRoutewayBuildFundCost = 1;

            disasterChanceDecrease = new Dictionary<int, int>();
            disasterChanceIncrease = new Dictionary<int, int>();
            disasterDamageRateDecrease = new Dictionary<int, float>();

        }

        // public int[] preferredOfficialTypes = {100, 100, 100, 100, 60, 100, 1, 250, 250, 39 };

        private Person mayor = null;

        //[DataMember]
        //public int mayorID = -1;

        private int militaryPopulation = 0;

        [DataMember]
        public bool TodayPersonArriveNote = false;
        private int agriculture;

        [DataMember]
        public int CaptionID = 0;
        //private bool shoudongluyongshibai=false;

        [DataMember]
        public bool HasManualHire = false;

        public Dictionary<int, LinkNode> AIAllLinkNodes = new Dictionary<int, LinkNode>();

        [DataMember]
        public string AILandLinksString;

        [DataMember]
        public string AIWaterLinksString;

        [DataMember]
        public string ArchitectureAreaString { get; set; }

        private bool autoHiring;

        public bool AutoRefillFoodInLongViewArea;
        private bool autoRewarding;
        private bool autoSearching;
        private bool autoZhaoXian;
        private bool autoWorking;
        private bool autoRecruiting;
        private GameArea baseFoodSurplyArea;

        public int ChanceDecrementOfCriticalStrike = 0;

        public Faction BelongedFaction = null;

        public Section BelongedSection = null;

        public ArchitectureList AILandLinks = new ArchitectureList();

        private Queue<AILinkProcedureDetail> AILinkProcedureDetails = new Queue<AILinkProcedureDetail>();

        public ArchitectureList AIWaterLinks = new ArchitectureList();

        public GameArea ArchitectureArea = new GameArea();

        public MilitaryList BeMergedMilitaryList = new MilitaryList();

        public GameObjectList BuildableFacilityKindList = new GameObjectList();

        public HashSet<Architecture> actuallyUnreachableArch = new HashSet<Architecture>();

        private int buildingDaysLeft;
        //private int buildingFacility = -1;

        public MilitaryList CampaignMilitaryList = new MilitaryList();

        public ArchitectureList ChangeCapitalArchitectureList = new ArchitectureList();

        public CombatNumberItemList IncrementNumberList = new CombatNumberItemList(CombatNumberDirection.上);

        public CombatNumberItemList DecrementNumberList = new CombatNumberItemList(CombatNumberDirection.下);

        public InfluenceTable Characteristics = new InfluenceTable();

        public ArchitectureList ClosestArchitectures;

        public FacilityList Facilities = new FacilityList();

        public List<FundPack> FundPacks = new List<FundPack>();

        public List<FoodPack> FoodPacks = new List<FoodPack>();

        public MilitaryList MergeMilitaryList = new MilitaryList();

        public MilitaryList Militaries = new MilitaryList();

        public MilitaryKindList UpgradableMilitaryKindList = new MilitaryKindList();

        public InformationList Informations = new InformationList();
        public MilitaryList LevelUpMilitaryList = new MilitaryList();

        [DataMember]
        public string CharacteristicsString { get; set; }

        public int CombativityOfRecruitment = 50;
        private int commerce;
        private GameArea contactArea;

        public bool CriticalHostile;
        public bool DayAvoidInfluenceByBattle;
        public bool DayAvoidInternalDecrementOnBattle;
        public bool DayAvoidPopulationEscape;
        public int DayLearnTitleDay = Session.Parameters.LearnTitleDays;
        public bool DayLocationLoyaltyNoChange;
        public float DayRateIncrementOfInternal;

        //[DataMember]
        public Legion DefensiveLegion;
        [DataMember]
        public int DefensiveLegionID;
        private int domination;
        private int endurance;

        [DataMember]
        public string FacilitiesString { get; set; }

        private bool facilityEnabled;
        private int food;
        public bool FrontLine;
        private int fund;

        [DataMember]
        public string FundPacksString { get; set; }

        [DataMember]
        public string FoodPacksString { get; set; }

        private bool hireFinished;
        public bool HostileLine;

        public int IncrementOfAgricultureCeiling;
        public int IncrementOfAgriculturePerDay;
        public int IncrementOfCombativityInViewArea;
        public int IncrementOfCommerceCeiling;
        public int IncrementOfCommercePerDay;
        public int IncrementOfDominationPerDay;
        public int IncrementOfEnduranceCeiling;
        public int IncrementOfDominationCeiling;
        public int IncrementOfMoraleCeiling;
        public int IncrementOfEndurancePerDay;
        public int IncrementOfFacilityPositionCount;
        public int IncrementOfFactionReputationPerDay;
        public int IncrementOfFactionTechniquePointPerDay;
        public int IncrementOfMonthFood;
        public int IncrementOfMonthFund;
        public int IncrementOfMoralePerDay;
        public int IncrementOfTechnologyCeiling;
        public int IncrementOfTechnologyPerDay;
        public int IncrementOfViewRadius;
        public int IncrementOfFundCeiling = 0;
        public int IncrementOfFoodCeiling = 0;

        [DataMember]
        public string InformationsString { get; set; }


        private bool isStrategicCenter;

        public bool JustAttacked = false;
        private ArchitectureKind kind;


        [DataMember]
        public int StateID { get; set; }

        public State LocationState;

        private GameArea longViewArea = null;

        [DataMember]
        public string MilitariesString { get; set; }

        private int morale;

        public int MoraleOfRecruitment = 50;
        public float MultipleOfRecovery = 1;
        public float MultipleOfTraining = 1;

        public ArchitectureList OtherArchitectureList = new ArchitectureList();

        public MilitaryKindList NewMilitaryKindList = new MilitaryKindList();

        public bool NoCounterStrikeInArchitecture;
        public bool orientationFrontLine;


        private int PathRoutewayID = -1;

        public Architecture PlanArchitecture;

        [DataMember]
        public int PlanArchitectureID;

        public FacilityKind PlanFacilityKind;

        [DataMember]
        public int PlanFacilityKindID;
        private int population;

        [DataMember]
        public string PopulationPacksString { get; set; }

        [DataMember]
        public string MilitaryPopulationPacksString { get; set; }

        public List<PopulationPack> PopulationPacks = new List<PopulationPack>();
        public List<PopulationPack> MilitaryPopulationPacks = new List<PopulationPack>();
        public MilitaryKindTable PrivateMilitaryKinds = new MilitaryKindTable();

        public float RateIncrementOfPopulationCeiling;
        public float RateIncrementOfMonthFood;
        public float RateIncrementOfMonthFund;
        public float RateIncrementOfNewBubingTroopDefence;
        public float RateIncrementOfNewBubingTroopOffence;
        public float RateIncrementOfNewNubingTroopDefence;
        public float RateIncrementOfNewNubingTroopOffence;
        public float RateIncrementOfNewQibingTroopDefence;
        public float RateIncrementOfNewQibingTroopOffence;
        public float RateIncrementOfNewQixieTroopDefence;
        public float RateIncrementOfNewQixieTroopOffence;
        public float RateIncrementOfNewShuijunTroopDefence;
        public float RateIncrementOfNewShuijunTroopOffence;
        public double RateIncrementOfPopulationDevelop;
        public float RateOfClearField = 1f;
        public float RateOfConvincePerson = 1f;
        public float RateOfDestroyArchitecture = 1f;
        public float RateOfFacilityEnduranceDown = 1f;
        public float RateOfFoodReduceRate = 1f;
        public float RateOfGossipArchitecture = 1f;
        public float RateOfJailBreakArchitecture = 1f;
        public float RateOfHirePerson = 1f;
        public float RateOfInstigateArchitecture = 1f;
        public float RateOfpublic = 1f;
        public float RateOfNewBubingMilitaryFundCost = 1f;
        public float RateOfNewNubingMilitaryFundCost = 1f;
        public float RateOfNewQibingMilitaryFundCost = 1f;
        public float RateOfNewQixieMilitaryFundCost = 1f;
        public float RateOfNewShuijunMilitaryFundCost = 1f;
        public float RateOfRewardPerson = 1f;
        public float RateOfRoutewayBuildFundCost = 1f;
        // public float RateOfSpyArchitecture = 1f;
        [DataMember]
        public int RecentlyAttacked;
        [DataMember]
        public int RecentlyHit;
        [DataMember]
        public int RecentlyBreaked;

        public MilitaryList RecruitmentMilitaryList = new MilitaryList();
        public CaptiveList RedeemCaptiveList = new CaptiveList();
        public GameObjectList ResetDiplomaticRelationList = new GameObjectList();
        public GameObjectList EnhanceDiplomaticRelationList = new GameObjectList();
        public GameObjectList AllyDiplomaticRelationList = new GameObjectList();
        public GameObjectList TruceDiplomaticRelationList = new GameObjectList();
        public GameObjectList DenounceDiplomaticRelationList = new GameObjectList();
        public GameObjectList QuanXiangDiplomaticRelationList = new GameObjectList(); //劝降
        public GameObjectList GeDiDiplomaticRelationList = new GameObjectList(); //割地
        public PersonList RewardPersonList = new PersonList();

        public Troop RobberTroop;

        [DataMember]
        public int RobberTroopID;

        private Dictionary<int, Architecture> RoutewayDestinationArchitectures = new Dictionary<int, Architecture>();
        private Queue<RoutewayProcedureDetail> RoutewayProcedures = new Queue<RoutewayProcedureDetail>();

        public RoutewayList Routeways = new RoutewayList();

        public MilitaryList ShelledMilitaryList = new MilitaryList();
        private bool showNumber;
       // public List<SpyPack> SpyPacks = new List<SpyPack>();
        private float surplusRate;
        private int technology;
        //public SpyMessage TodayNewMilitarySpyMessage;
        // public SpyMessage TodayNewTroopSpyMessage;

        public int TotalStoredForce;
        public int TotalFriendlyForce;
        public int TotalHostileForce;

        public MilitaryList TrainingMilitaryList = new MilitaryList();
        public ArchitectureList TransferArchitectureList = new ArchitectureList();
        public Architecture TransferFoodArchitecture;

        [DataMember]
        public int TransferFoodArchitectureID;

        public Architecture TransferFundArchitecture;
        [DataMember]
        public int TransferFundArchitectureID;
        [DataMember]
        public bool TroopershipAvailable;
        private GameArea viewArea = null;
        [DataMember]
        public zainanlei zainan = new zainanlei();
        public PlatformTexture CaptionTexture;

        public bool noFactionFrontline;
        public int captureChance;
        public int noEscapeChance;
        [DataMember]
        public List<KeyValuePair<int, int>> captiveLoyaltyFall = new List<KeyValuePair<int, int>>();
        [DataMember]
        public bool noFundToSustainFacility;
        public int facilityEnduranceIncrease;
        public Dictionary<int, int> disasterChanceDecrease = new Dictionary<int, int>();
        public Dictionary<int, int> disasterChanceIncrease = new Dictionary<int, int>();
        public Dictionary<int, float> disasterDamageRateDecrease = new Dictionary<int, float>();
        public float militaryPopulationRateIncrease;
        public float enduranceDecreaseRateDrop;

        public bool hostileTroopInViewLastDay = false;
        [DataMember]
        public int SuspendTroopTransfer;
        public bool withoutTruceFrontline;
        public float ExperienceRate;
        public int InfluenceIncrementOfLoyalty;
        public int CommandExperienceIncrease { get; set; }
        public int StrengthExperienceIncrease { get; set; }
        public int IntelligenceExperienceIncrease { get; set; }
        public int PoliticsExperienceIncrease { get; set; }
        public int GlamourExperienceIncrease { get; set; }
        public int ReputationIncrease { get; set; }
        public float TroopTransportDayRate { get; set; }
        public float TroopTransportFundRate { get; set; }
        public float TroopTransportFoodRate { get; set; }

        public ArchitectureList AIBattlingArchitectures { get; set; }
        [DataMember]
        public int MayorOnDutyDays {get;set;}

       // public OngoingBattle Battle { get; set; }

        private String oldFactionName = "";
        [DataMember]
        public String OldFactionName
        {
            get
            {
                if (oldFactionName == null || oldFactionName.Equals(""))
                {
                    if (this.BelongedFaction == null)
                    {
                        return "";
                    }
                    return this.BelongedFaction.Name;
                }
                return oldFactionName;
            }
            set
            {
                oldFactionName = value;
            }
        }
        public float facilityConstructionTimeRateDecrease = 0;

#pragma warning disable CS0067 // The event 'Architecture.OnBeginRecentlyAttacked' is never used
        public event BeginRecentlyAttacked OnBeginRecentlyAttacked;
#pragma warning restore CS0067 // The event 'Architecture.OnBeginRecentlyAttacked' is never used

        public event FacilityCompleted OnFacilityCompleted;

        public event fashengzainan Onfashengzainan;

#pragma warning disable CS0067 // The event 'Architecture.OnHirePerson' is never used
        public event HirePerson OnHirePerson;
#pragma warning restore CS0067 // The event 'Architecture.OnHirePerson' is never used

        public event MilitaryCreate OnMilitaryCreate;

        public event PopulationEnter OnPopulationEnter;

        public event PopulationEscape OnPopulationEscape;

        public event ReleaseCaptiveAfterOccupied OnReleaseCaptiveAfterOccupied;

#pragma warning disable CS0067 // The event 'Architecture.OnRewardPersons' is never used
        public event RewardPersons OnRewardPersons;
#pragma warning restore CS0067 // The event 'Architecture.OnRewardPersons' is never used

        public CaptiveList Captives
        {
            get
            {
                CaptiveList p = Session.Current.Scenario.GetCaptiveList(this);
                p.SetImmutable();
                foreach (Captive c in p) //禁止俘虏自势力武将
                {
                    if (c.CaptiveFaction == c.BelongedFaction)
                    {
                        c.CaptivePerson.SetBelongedCaptive(null, GameObjects.PersonDetail.PersonStatus.Normal);
                    }
                }
                return p;
            }
        }

        public PersonList AllPersonAndChildren
        {
            get
            {
                GameObjectList p = Session.Current.Scenario.GetPersonList(this).GetList();

                HashSet<Person> result = new HashSet<Person>();
                foreach (Person q in p)
                {
                    result.Add(q);
                    foreach (Person r in q.ChildrenList)
                    {
                        result.Add(r);
                    }
                }

                GameObjectList p2 = Session.Current.Scenario.GetMovingPersonList(this).GetList();
                foreach (Person q in p2)
                {
                    result.Add(q);
                    foreach (Person r in q.ChildrenList)
                    {
                        result.Add(r);
                    }
                }

                GameObjectList p3 = Session.Current.Scenario.GetNoFactionPersonList(this).GetList();
                foreach (Person q in p3)
                {
                    result.Add(q);
                    foreach (Person r in q.ChildrenList)
                    {
                        result.Add(r);
                    }
                }

                GameObjectList p4 = Session.Current.Scenario.GetNoFactionMovingPersonList(this).GetList();
                foreach (Person q in p4)
                {
                    result.Add(q);
                    foreach (Person r in q.ChildrenList)
                    {
                        result.Add(r);
                    }
                }

                foreach (Captive q in this.Captives)
                {
                    result.Add(q.CaptivePerson);
                }

                foreach (Person q in this.Feiziliebiao)
                {
                    result.Add(q);
                    foreach (Person r in q.ChildrenList)
                    {
                        result.Add(r);
                    }
                }

                PersonList result2 = new PersonList();
                foreach (Person q in result)
                {
                    result2.Add(q);
                }
                result2.SetImmutable();
                return result2;
            }
        }

        public PersonList PersonAndChildren
        {
            get
            {
                GameObjectList p = Session.Current.Scenario.GetPersonList(this).GetList();
                HashSet<Person> result = new HashSet<Person>();
                foreach (Person q in p)
                {
                    result.Add(q);
                    foreach (Person r in q.ChildrenList)
                    {
                        result.Add(r);
                    }
                }
                foreach (Person q in this.Feiziliebiao)
                {
                    result.Add(q);
                }
                PersonList result2 = new PersonList();
                foreach (Person q in result)
                {
                    result2.Add(q);
                }
                result2.SetImmutable();
                return result2;
            }
        }

        public PersonList Children
        {
            get
            {
                GameObjectList p = Session.Current.Scenario.GetPersonList(this).GetList();
                HashSet<Person> result = new HashSet<Person>();
                foreach (Person q in p)
                {
                    foreach (Person r in q.ChildrenList)
                    {
                        result.Add(r);
                    }
                }
                PersonList result2 = new PersonList();
                foreach (Person q in result)
                {
                    result2.Add(q);
                }
                result2.SetImmutable();
                return result2;
            }
        }

        public PersonList Persons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList MovingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetMovingPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList NoFactionPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetNoFactionPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList NoFactionMovingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetNoFactionMovingPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList Feiziliebiao
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetPrincessPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList ZhenzaiWorkingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetZhenzaiPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList AgricultureWorkingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetAgriculturePersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList CommerceWorkingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetCommercePersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList TechnologyWorkingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetTechnologyPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList DominationWorkingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetDomintaionPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList MoraleWorkingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetMoralePersonList(this);
                p.SetImmutable();
                return p;

            }
        }

        public PersonList EnduranceWorkingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetEndurancePersonList(this);
                p.SetImmutable();
                return p;

            }
        }

        public PersonList TrainingWorkingPersons
        {
            get
            {
                PersonList p = Session.Current.Scenario.GetTrainingPersonList(this);
                p.SetImmutable();
                return p;
            }
        }

        public PersonList DiplomaticWorkingPersons
        {
            get
            {
                PersonList result = new PersonList();
                foreach (Person i in Session.Current.Scenario.Persons)
                {
                    if (i.Status == PersonStatus.Normal && i.LocationArchitecture == this && i.LocationTroop == null && !this.BelongedFaction.MayorList.GameObjects.Contains(i))
                    {
                        result.Add(i);
                    }
                }
                return result;
            }
        }

        public MilitaryList ZhengzaiBuchongDeBiandui()
        {
            MilitaryList zhengzaiBuchongDeBiandui = new MilitaryList();
            foreach (Military military in this.Militaries)
            {
                if (military.RecruitmentPerson != null)
                {
                    zhengzaiBuchongDeBiandui.AddMilitary(military);
                }
            }
            return zhengzaiBuchongDeBiandui;
        }
        [DataMember]
        public int MilitaryPopulation
        {
            get
            {
                return this.militaryPopulation;
            }
            set
            {
                this.militaryPopulation = value;
            }
        }

        private void AddAllAILink(int level, int levelMax, Architecture root, List<Architecture> path)
        {
            path.Add(root);
            if (root != this)
            {
                double num = 0.0;
                for (int i = 1; i < path.Count; i++)
                {
                    num += Session.Current.Scenario.GetDistance(path[i - 1].ArchitectureArea, path[i].ArchitectureArea);
                }
                if (!this.AIAllLinkNodes.ContainsKey(root.ID))
                {
                    LinkNode node = new LinkNode();
                    node.A = root;
                    node.Level = level;
                    foreach (Architecture architecture in path)
                    {
                        node.Path.Add(architecture);
                    }
                    node.Distance = num;
                    this.AIAllLinkNodes.Add(root.ID, node);
                }
                else if ((this.AIAllLinkNodes[root.ID].Level == level) && (this.AIAllLinkNodes[root.ID].Distance > num))
                {
                    this.AIAllLinkNodes[root.ID].Distance = num;
                    this.AIAllLinkNodes[root.ID].Path.Clear();
                    foreach (Architecture architecture in path)
                    {
                        this.AIAllLinkNodes[root.ID].Path.Add(architecture);
                    }
                }
                else if (this.AIAllLinkNodes[root.ID].Level > level)
                {
                    this.AIAllLinkNodes[root.ID].Level = level;
                    this.AIAllLinkNodes[root.ID].Path.Clear();
                    foreach (Architecture architecture in path)
                    {
                        this.AIAllLinkNodes[root.ID].Path.Add(architecture);
                    }
                }
            }
            if (level < levelMax)
            {
                foreach (Architecture architecture in root.GetAILinks())
                {
                    this.AILinkProcedureDetails.Enqueue(new AILinkProcedureDetail(level + 1, architecture, path));
                }
            }
        }

        public String BuildingFacilityName
        {
            get
            {
                int type = BuildingFacility;
                GameObjectList fkl = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKindList();

                foreach (FacilityKind i in fkl)
                {
                    if (type == i.ID)
                    {
                        return i.Name;
                    }
                }
                return "——";
            }
        }

        public String SheshiMiaoshu
        {
            get
            {
                int type = BuildingFacility;
                GameObjectList fkl = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKindList();

                foreach (FacilityKind i in fkl)
                {
                    if (type == i.ID)
                    {
                        return i.Name;
                    }
                }

                if (this.FacilityPositionCount > 0 && this.FacilityPositionLeft <= 0)
                {
                    return "已建满";
                }

                return this.FacilityPositionString;
            }
        }



        public void AddBaseSupplyingArchitecture()
        {
            foreach (Point point in this.BaseFoodSurplyArea.Area)
            {
                if (!Session.Current.Scenario.PositionOutOfRange(point))
                {
                    Session.Current.Scenario.MapTileData[point.X, point.Y].AddSupplyingArchitecture(this);
                }
            }
        }

        private void AddCloseRoutewayDestinationArchitectures(Architecture a, float previousrate)
        {
            foreach (Routeway routeway in a.Routeways)
            {
                float minRate = 1f;
                if ((routeway.EndArchitecture != null) && routeway.IsActiveInArea(routeway.EndArchitecture.GetRoutewayStartArea(), out minRate))
                {
                    float rate = previousrate * (1f - (minRate * this.BelongedFaction.RateOfFoodTransportBetweenArchitectures));
                    if (rate > routeway.EndArchitecture.surplusRate)
                    {
                        routeway.EndArchitecture.surplusRate = rate;
                        routeway.EndArchitecture.PathRoutewayID = routeway.ID;
                        if (!this.RoutewayDestinationArchitectures.ContainsKey(routeway.EndArchitecture.ID))
                        {
                            this.RoutewayDestinationArchitectures.Add(routeway.EndArchitecture.ID, routeway.EndArchitecture);
                        }
                        this.RoutewayProcedures.Enqueue(new RoutewayProcedureDetail(routeway.EndArchitecture, rate));
                    }
                }
            }
        }

        public void AddFundPack(int number, int days)
        {
            if (number > 0)
            {
                FundPack item = new FundPack(number, days);
                this.FundPacks.Add(item);
            }
        }

        public void AddFoodPack(int number, int days)
        {
            if (number > 0)
            {
                FoodPack item = new FoodPack(number, days);
                this.FoodPacks.Add(item);
            }
        }

        /*
        private void AddMessageToTodayMilitaryScaleSpyMessage(Military m)
        {
            this.CreateMilitaryScaleSpyMessage(m);
        }

        private void AddMessageToTodayNewMilitarySpyMessage(Military m)
        {
            if (this.TodayNewMilitarySpyMessage == null)
            {
                this.TodayNewMilitarySpyMessage = this.CreateNewMilitarySpyMessage(m);
            }
            else
            {
                this.TodayNewMilitarySpyMessage.Message3 = this.TodayNewMilitarySpyMessage.Message3 + "," + m.Name;
            }
        }

        private void AddMessageToTodayNewTroopSpyMessage(Troop t, bool hand)
        {
            if (this.TodayNewTroopSpyMessage == null)
            {
                this.TodayNewTroopSpyMessage = this.CreateNewTroopSpyMessage(t, hand);
            }
            else
            {
                this.TodayNewTroopSpyMessage.Message3 = this.TodayNewTroopSpyMessage.Message3 + "," + t.DisplayName;
            }
        }
        */

        public void AddMilitary(Military military)
        {
            this.Militaries.AddMilitary(military);
            military.BelongedArchitecture = this;
        }

        private PersonList SelectSubOfficersToTroop(Troop t)
        {
            PersonList result = new PersonList();
            result.Add(t.Leader);
            if (t.TroopIntelligence < (75 - t.Leader.Calmness))
            {
                foreach (Person person in this.MovablePersons)
                {
                    if (person.WaitForFeiZi != null) continue;
                    if (!person.Selected && person.Intelligence >= 75 - t.Leader.Calmness && !t.Persons.HasGameObject(person) && t.Leader.Character.IntelligenceRate >= 0.75f &&
                        person.Strength < t.TroopStrength && person.Intelligence - t.TroopIntelligence >= 10 && person.FightingForce < t.Leader.FightingForce && !person.HasLeaderValidTitle)
                    {
                        person.Selected = true;
                        result.Add(person);
                        break;
                    }
                }
            }
            if (t.TroopStrength < 75)
            {
                foreach (Person person in this.MovablePersons)
                {
                    if (person.WaitForFeiZi != null) continue;
                    if (!person.Selected && person.Strength >= 75 && !t.Persons.HasGameObject(person) && person.Closes(t.Leader) &&
                        person.Strength - t.TroopStrength >= 10 && person.FightingForce < t.Leader.FightingForce && !person.HasLeaderValidTitle)
                    {
                        person.Selected = true;
                        result.Add(person);
                        break;
                    }
                }
            }
            if (t.TroopCommand < 75)
            {
                foreach (Person person in this.MovablePersons)
                {
                    if (person.WaitForFeiZi != null) continue;
                    if (!person.Selected && person.Command >= 75 && !t.Persons.HasGameObject(person) && person.Closes(t.Leader) &&
                        person.Command - t.TroopCommand >= 10 && person.FightingForce < t.Leader.FightingForce && !person.HasLeaderValidTitle)
                    {
                        person.Selected = true;
                        result.Add(person);
                        break;
                    }
                }
            }

            if (this.MovablePersons.GameObjects.Contains(t.Leader.Spouse))
            {
                if ((!t.Leader.Spouse.Selected && !t.Persons.HasGameObject(t.Leader.Spouse)) && ((t.Leader.Spouse.FightingForce < t.Leader.FightingForce)) && !t.Leader.Spouse.HasLeaderValidTitle)
                {
                    t.Leader.Spouse.Selected = true;
                    result.Add(t.Leader.Spouse);
                }
            }

            GameObjectList pl = this.MovablePersons;
            pl.PropertyName = "SubFightingForce";
            pl.IsNumber = true;
            pl.SmallToBig = false;
            pl.ReSort();
            int personCount = this.PersonCount;
            foreach (Person person in pl)
            {
                if (person.WaitForFeiZi != null) continue;
                if (person.SubFightingForce < t.Leader.FightingForce * result.Count / Math.Min(3, personCount / this.MilitaryCount + 1)) break;
                if (!person.Selected && !t.Persons.HasGameObject(person) && person.FightingForce < t.Leader.FightingForce &&
                    !person.HasLeaderValidTitle && person.HasSubofficerValidTitle)
                {
                    person.Selected = true;
                    result.Add(person);
                }
            }
            /*
            foreach (Person person in this.MovablePersons)
            {
                if (person.WaitForFeiZi != null) continue;
                if ((!person.Selected && !t.Persons.HasGameObject(person)) && ((person.FightingForce < t.Leader.FightingForce) && !person.HasLeaderValidTitle))
                {
                    int incrementPerDayOfCombativity = t.IncrementPerDayOfCombativity;
                    bool immunityOfCaptive = t.ImmunityOfCaptive;
                    int routIncrementOfCombativity = t.RoutIncrementOfCombativity;
                    int attackDecrementOfCombativity = t.AttackDecrementOfCombativity;
                    int count = t.CombatMethods.Count;
                    int chanceIncrementOfCriticalStrike = t.ChanceIncrementOfCriticalStrike;
                    int chanceDecrementOfCriticalStrike = t.ChanceDecrementOfCriticalStrike;
                    int chanceIncrementOfChaosAfterCriticalStrike = t.ChanceIncrementOfChaosAfterCriticalStrike;
                    int avoidSurroundedChance = t.AvoidSurroundedChance;
                    int chaosAfterSurroundAttackChance = t.ChaosAfterSurroundAttackChance;
                    int chanceIncrementOfStratagem = t.ChanceIncrementOfStratagem;
                    int chanceDecrementOfStratagem = t.ChanceDecrementOfStratagem;
                    int chanceIncrementOfChaosAfterStratagem = t.ChanceIncrementOfChaosAfterStratagem;
                    foreach (Skill s in person.Skills.GetSkillList())
                    {
                        s.Influences.PurifyInfluence(this, Applier.Skill, s.ID);
                    }
                    foreach (Title i in person.Titles)
                    {
                        i.Influences.PurifyInfluence(this, Applier.Title, i.ID);
                    }
                    person.ApplySkills(false);
                    person.ApplyTitles(false);
                    if (((((((t.IncrementPerDayOfCombativity > incrementPerDayOfCombativity) || (t.ImmunityOfCaptive != immunityOfCaptive)) || ((t.RoutIncrementOfCombativity > routIncrementOfCombativity) || (t.AttackDecrementOfCombativity > attackDecrementOfCombativity))) || ((t.CombatMethods.Count > count) || (((t.TroopStrength >= 70) && (t.ChanceIncrementOfCriticalStrike > chanceIncrementOfCriticalStrike)) && (t.ChanceIncrementOfCriticalStrike <= 50)))) || (((((t.TroopCommand >= 70) && (t.ChanceDecrementOfCriticalStrike > chanceDecrementOfCriticalStrike)) && (t.ChanceDecrementOfCriticalStrike <= 50)) || (((t.ChanceIncrementOfCriticalStrike >= 10) && (t.ChanceIncrementOfChaosAfterCriticalStrike > chanceIncrementOfChaosAfterCriticalStrike)) && (t.ChanceIncrementOfChaosAfterCriticalStrike <= 100))) || (((t.AvoidSurroundedChance <= 80) && (t.AvoidSurroundedChance > avoidSurroundedChance)) || ((t.ChaosAfterSurroundAttackChance <= 20) && (t.ChaosAfterSurroundAttackChance > chaosAfterSurroundAttackChance))))) || ((((t.TroopIntelligence >= 70) && (t.ChanceIncrementOfStratagem > chanceIncrementOfStratagem)) && (t.ChanceIncrementOfStratagem <= 30)) || (((t.TroopIntelligence >= 70) && (t.ChanceDecrementOfStratagem > chanceDecrementOfStratagem)) && (t.ChanceDecrementOfStratagem <= 30)))) || (((t.TroopIntelligence >= 0x55) && (t.ChanceIncrementOfChaosAfterStratagem > chanceIncrementOfChaosAfterStratagem)) && (t.ChanceIncrementOfChaosAfterStratagem <= 100)))
                    {
                        person.Selected = true;
                        result.Add(person);
                        personCnt++;
                    }
                }
                if (personCnt >= 5) break;
            }
            */
            return result;
        }

        public void AddPopulationPack(int days, int population)
        {
            PopulationPack item = new PopulationPack(days, population);
            this.PopulationPacks.Add(item);
        }

        public void AddMilitaryPopulationPack(int days, int population)
        {
            PopulationPack item = new PopulationPack(days, population);
            this.MilitaryPopulationPacks.Add(item);
        }

        /*
        public void AddSpyPack(Person person, int days)
        {
            SpyPack item = new SpyPack(person, days);
            this.SpyPacks.Add(item);
        }
        */

        public bool AgricultureAvail()
        {
            return (this.Kind.HasAgriculture && this.HasPerson());
        }

        private void RoutewayAI()
        {
            if (GameObject.Random(10) == 0)
            {
                RoutewayList toRemove = new RoutewayList();
                foreach (Routeway r in this.Routeways)
                {
                    if (!r.IsInUsing)
                    {
                        toRemove.Add(r);
                    }
                }
                foreach (Routeway r in toRemove)
                {
                    this.RemoveRoutewayToArchitecture(r.DestinationArchitecture);
                }
            }
        }

        public void AI()
        {
            //this.PlayAIZhaoXian();
            this.PrepareAI();
            this.AIExecute();
            this.RoutewayAI();
            this.AITreasure();
            this.AITrade();
            this.AIMilitary();
            this.AIFacility();
           // this.DiplomaticRelationAI();
            this.AICampaign();
            this.OutsideTacticsAI();
            this.AIWork(false);
            this.InsideTacticsAI();
            this.AIExpand();
            this.AIDiplomaticTactics();

            ExtensionInterface.call("AIArchitecture", new Object[] { Session.Current.Scenario, this });
        }

        private void AIExpand()
        {
            if (this.ExpandAvail())
            {
                this.Expand();
            }
        }

        private void AIExecute()
        {
            if (Session.Current.Scenario.IsPlayer(this.BelongedFaction)) return;
            if (Session.GlobalVariables.AIExecutionRate <= 0) return;

            //AI for executing officers. High ambition and low personal loyalty leads to higher chance of execution (rate lower => higher chance)
            int uncruelty = this.BelongedFaction.Leader.Uncruelty;
            if (uncruelty > Session.Parameters.AIExecuteMaxUncreulty) return;

            //int leaderExecutionRate = uncruelty <= 2 ? 5 : uncruelty * uncruelty * uncruelty * 2;
            //int leaderExecutionRate = uncruelty * uncruelty * uncruelty * 4;
            foreach (Captive i in this.Captives)
            {
                if ((!i.CaptivePerson.RecruitableBy(this.BelongedFaction, (int)((uncruelty - 2) * Session.Parameters.AIExecutePersonIdealToleranceMultiply)) || this.BelongedFaction.Leader.Hates(i.CaptivePerson) || i.CaptivePerson.Hates(this.BelongedFaction.Leader)) &&
                    GameObject.Random((int)(uncruelty * uncruelty * (Session.GlobalVariables.AIExecuteBetterOfficer ? 100000.0 / i.CaptivePerson.Merit : i.CaptivePerson.Merit / 100000.0)
                        * (100.0 / Session.GlobalVariables.AIExecutionRate))) == 0)  //处斩几率修改系数就可以，可设为小数
                {
                    if (!this.BelongedFaction.Leader.HasStrainTo(i.CaptivePerson))
                    {
                        Session.MainGame.mainGameScreen.OnExecute(this.BelongedFaction.Leader, i.CaptivePerson);
                        i.CaptivePerson.execute(this.BelongedFaction);

                        break;
                    }
                }
                if (this.BelongedFaction.IsAlien &&
                    (i.CaptivePerson.PersonalLoyalty >= 2 || this.BelongedFaction.Leader.Hates(i.CaptivePerson) || i.CaptivePerson.Hates(this.BelongedFaction.Leader)) &&
                    GameObject.Chance(10))
                {
                    if (!this.BelongedFaction.Leader.HasStrainTo(i.CaptivePerson) && !this.BelongedFaction.Leader.isLegalFeiZiExcludeAge(i.CaptivePerson))
                    {
                        Session.MainGame.mainGameScreen.OnExecute(this.BelongedFaction.Leader, i.CaptivePerson);
                        i.CaptivePerson.execute(this.BelongedFaction);
                    }

                    break;
                }
            }
            /*foreach (Person i in this.Persons)
            {
                if (!i.RecruitableBy(this.BelongedFaction) &&
                    GameObject.Random((int)(leaderExecutionRate / 2 * (100000.0 / i.Merit))) == 0)
                {
                    i.execute(this.BelongedFaction.Leader);
                    break;
                }
            }*/
        }

        private bool AIExtension()
        {
            if (this.BuildingFacility < 0)
            {
                foreach (FacilityKind kind in Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKindList().GetRandomList())
                {
                    if (kind.IsExtension)
                    {
                        //八哥修改：增加this.ExpectedFund != 0 和 this.PlanFacilityKind.Days != 0 的限制判斷
                        if (!kind.CanBuild(this)) continue;
                        if (kind.FundCost <= this.Fund)
                        {
                            FacilityKind facilityKind = kind;
                            this.BelongedFaction.DepositTechniquePointForFacility(facilityKind.PointCost);
                            this.BeginToBuildAFacility(facilityKind);
                            return true;
                        }
                        else if (this.ExpectedFund != 0 && (kind.FundCost - (this.Fund - this.EnoughFund)) / this.ExpectedFund + 1 <= kind.Days / 15)
                        {
                            this.PlanFacilityKind = kind;
                            if (GameObject.Chance(0x21) && this.PlanFacilityKind.Days != 0 && ((this.BelongedFaction.TechniquePoint + this.BelongedFaction.TechniquePointForFacility) < this.PlanFacilityKind.PointCost))
                            {
                                this.BelongedFaction.SaveTechniquePointForFacility(this.PlanFacilityKind.PointCost / this.PlanFacilityKind.Days);
                            }
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void AIFacility()
        {
            if (((this.PlanArchitecture == null) || GameObject.Chance(10)) && (this.BuildingFacility < 0) && this.FacilityPositionCount > 0)
            {
                if (this.PlanFacilityKind != null && this.BelongedFaction != null)
                {
                    if (this.Technology >= this.PlanFacilityKind.TechnologyNeeded && this.PlanFacilityKind.PositionOccupied <= this.FacilityPositionLeft)
                    {
                        if ((this.Fund >= this.PlanFacilityKind.FundCost) && ((this.BelongedFaction.TechniquePoint + this.BelongedFaction.TechniquePointForFacility) >= this.PlanFacilityKind.PointCost))
                        {
                            this.BelongedFaction.DepositTechniquePointForFacility(this.PlanFacilityKind.PointCost);
                            this.BeginToBuildAFacility(this.PlanFacilityKind);
                            this.PlanFacilityKind = null;
                        }
                        else if (GameObject.Chance(0x21) && ((this.BelongedFaction.TechniquePoint + this.BelongedFaction.TechniquePointForFacility) < this.PlanFacilityKind.PointCost))
                        {
                            this.BelongedFaction.SaveTechniquePointForFacility(this.PlanFacilityKind.PointCost / this.PlanFacilityKind.Days);
                        }
                    }
                    else
                    {
                        this.PlanFacilityKind = null;
                    }
                    if (this.PlanFacilityKind != null && this.PlanFacilityKind.FundCost > this.FundCeiling / 2)
                    {
                        this.PlanFacilityKind = null;
                    }
                }
                else
                {
                    if (AIExtension()) return;

                    //remove useless facilities
                    if (this.BelongedSection != null && this.BelongedSection.AIDetail != null && this.BelongedSection.AIDetail.AllowFacilityRemoval)
                    {
                        foreach (Facility i in this.Facilities)
                        {
                            float value = (float) i.Kind.AIValue(this);
                            foreach (KeyValuePair<Condition, float> weight in i.Kind.AIBuildConditionWeight)
                            {
                                if (weight.Key.CheckCondition(this))
                                {
                                    value *= weight.Value;
                                }
                            }
                            if (value < 0 && this.CanRemoveFacility(i) && i.Kind.rongna == 0)
                            {
                                if (this.FacilityEnabled || i.MaintenanceCost <= 0)
                                {
                                    i.Influences.PurifyInfluence(this, Applier.Facility, i.ID);
                                }
                                this.Facilities.Remove(i);
                                Session.Current.Scenario.Facilities.Remove(i);
                                break;
                            }
                        }
                    }
                    //remove facilities if not enough fund to support
                    if (this.FacilityMaintenanceCost * 30 + 100 > this.ExpectedFund && this.BelongedSection.AIDetail.AllowFacilityRemoval)
                    {
                        GameObjectList f = this.Facilities.GetList();
                        f.PropertyName = "AIValue";
                        f.SmallToBig = true;
                        f.IsNumber = true;
                        f.ReSort();
                        foreach (Facility i in f)
                        {
                            if (this.CanRemoveFacility(i) && i.Kind.NetFundIncrease <= 0 && i.Kind.rongna == 0)
                            {
                                if (this.FacilityEnabled || i.MaintenanceCost <= 0)
                                {
                                    i.Influences.PurifyInfluence(this, Applier.Facility, i.ID);
                                }
                                this.Facilities.Remove(i);
                                Session.Current.Scenario.Facilities.Remove(i);
                            }
                            if (this.FacilityMaintenanceCost * 30 + 100 <= this.ExpectedFund) break;
                        }
                    }

                    //choose facilities
                    double maxValue = double.MinValue;
                    FacilityKind toBuild = null;
                    List<Facility> toDestroy = new List<Facility>();
                    List<Facility> realToDestroy = new List<Facility>();
                    foreach (FacilityKind kind in Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKindList())
                    {
                        if (kind.IsExtension) continue;
                        if (!kind.CanBuild(this)) continue;
                        if (kind.rongna > 0) continue;
                        if ((kind.MaintenanceCost + this.FacilityMaintenanceCost) * 30 + 2000 > this.ExpectedFund && kind.NetFundIncrease <= 0)
                        {
                            continue;
                        }
                        if (kind.FundCost > this.FundCeiling / 2)
                        {
                            continue;
                        }
                        float value = (float)kind.AIValue(this);
                        foreach (KeyValuePair<Condition, float> weight in kind.AIBuildConditionWeight)
                        {
                            if (weight.Key.CheckCondition(this))
                            {
                                value *= weight.Value;
                            }
                        }
                        if (value > 0 && this.ExpectedFund != 0)
                        {
                            int fundMonthToWait = (kind.FundCost - (this.Fund - this.EnoughFund)) / this.ExpectedFund + 1;
                            if (value > maxValue && GameObject.Chance((int)(100 - fundMonthToWait * Session.Parameters.AIFacilityFundMonthWaitParam)) && this.Fund - kind.FundCost > this.EnoughFund)
                            {
                                if (this.FacilityPositionLeft < kind.PositionOccupied)
                                {
                                    if (this.BelongedSection != null && this.BelongedSection.AIDetail != null && this.BelongedSection.AIDetail.AllowFacilityRemoval && this.FacilityPositionLeft < Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetMaxFacilitySpace())
                                    {
                                        int fpl = this.FacilityPositionLeft;
                                        toDestroy.Clear();
                                        foreach (Facility f in this.Facilities.GetRandomList())
                                        {
                                            if (value > f.Kind.AIValue(this) * Session.Parameters.AIFacilityDestroyValueRate && this.CanRemoveFacility(f) && f.Kind.rongna == 0)
                                            {
                                                toDestroy.Add(f);
                                                fpl += f.Kind.PositionOccupied;
                                                if (fpl >= kind.PositionOccupied)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        if (fpl >= kind.PositionOccupied)
                                        {
                                            maxValue = value;
                                            toBuild = kind;
                                            realToDestroy = new List<Facility>(toDestroy);
                                        }
                                        else
                                        {
                                            continue;
                                        }
                                    } else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    maxValue = value;
                                    toBuild = kind;
                                    realToDestroy.Clear();
                                }
                            }
                        }
                    }
                    if (toBuild != null)
                    {
                        //if no space and the facility is good enough than others, remove others
                        foreach (Facility f in realToDestroy)
                        {
                            if (this.FacilityEnabled || f.MaintenanceCost <= 0)
                            {
                                f.Influences.PurifyInfluence(this, Applier.Facility, f.ID);
                            }
                            this.Facilities.Remove(f);
                            Session.Current.Scenario.Facilities.Remove(f);
                        }
                        if (toBuild.PositionOccupied <= this.FacilityPositionLeft)
                        {
                            //actually build it, or put to plan if fund is not enough
                            if (this.BelongedFaction != null && (this.Fund >= toBuild.FundCost) && ((this.BelongedFaction.TechniquePoint + this.BelongedFaction.TechniquePointForFacility) >= toBuild.PointCost))
                            {
                                FacilityKind facilityKind = toBuild;
                                this.BelongedFaction.DepositTechniquePointForFacility(facilityKind.PointCost);
                                this.BeginToBuildAFacility(facilityKind);
                            }
                            else
                            {
                                this.PlanFacilityKind = toBuild;
                                if (this.BelongedFaction != null && GameObject.Chance(0x21) && ((this.BelongedFaction.TechniquePoint + this.BelongedFaction.TechniquePointForFacility) < this.PlanFacilityKind.PointCost))
                                {
                                    this.BelongedFaction.SaveTechniquePointForFacility(this.PlanFacilityKind.PointCost / this.PlanFacilityKind.Days);
                                }

                            }
                        }
                    }
                    /*List<FacilityKind> list3 = new List<FacilityKind>();
                    int facilityPositionLeft = this.FacilityPositionLeft;
                    int iD = 10;
                    int num3 = 0;
                    foreach (FacilityKind kind in Session.Current.Scenario.GameCommonData.AllFacilityKinds.FacilityKinds.Values)
                    {
                        if (((kind.ID > iD) && ((kind.ID / 10) == 1)) && (kind.TechnologyNeeded < this.Technology))
                        {
                            iD = kind.ID;
                        }
                        if (((kind.ID > num3) && ((kind.ID / 10) == 0)) && (kind.TechnologyNeeded < this.Technology))
                        {
                            num3 = kind.ID;
                        }
                    }
                    foreach (FacilityKind kind in Session.Current.Scenario.GameCommonData.AllFacilityKinds.FacilityKinds.Values)
                    {
                        if (((kind.rongna > 0) || (((kind.ID / 10) == 0) && (kind.ID != num3))) || (((kind.ID / 10) == 1) && (kind.ID != iD)))
                        {
                            continue;
                        }
                        if ((((!kind.PopulationRelated || this.Kind.HasPopulation) && ((this.Technology >= kind.TechnologyNeeded) && (facilityPositionLeft >= kind.PositionOccupied)))
                            && ((!kind.UniqueInArchitecture || !this.ArchitectureHasFacilityKind(kind.ID)) && (!kind.UniqueInFaction || !this.FactionHasFacilityKind(kind.ID))))
                            && ((kind.FrontLine && ((this.HostileLine || (this.FrontLine && GameObject.Chance(50))) || (!this.FrontLine && GameObject.Chance(10)))) || (!kind.FrontLine && ((!this.FrontLine || (!this.HostileLine && GameObject.Chance(50))) || (this.HostileLine && GameObject.Chance(5))))))
                        {
                            list.Add(kind);
                            if ((this.Fund >= kind.FundCost) && ((this.BelongedFaction.TechniquePoint + this.BelongedFaction.TechniquePointForFacility) >= kind.PointCost))
                            {
                                list2.Add(kind);
                            }
                        }
                    }
                    if (facilityPositionLeft <= 0)
                    {
                        foreach (Facility facility in this.Facilities.GetList())
                        {
                            if ((((this.Technology > facility.TechnologyNeeded) && this.FacilityIsPossibleOverTechnology(facility.TechnologyNeeded))
                                && ((this.Fund > (facility.FundCost * 10)) && (this.BelongedFaction.TechniquePoint > (facility.PointCost * 10))))
                                && (GameObject.Random(facility.Days * facility.PositionOccupied) < 20)
                                && !facility.Kind.bukechaichu)
                            {
                                if (list.IndexOf(facility.Kind) >= 0)
                                {
                                    continue;
                                }
                                list3.Add(facility.Kind);
                                if (this.FacilityEnabled)
                                {
                                    facility.Influences.PurifyInfluence(this);
                                }
                                this.Facilities.Remove(facility);
                                Session.Current.Scenario.Facilities.Remove(facility);
                            }
                        }
                        if (list3.Count == 0)
                        {
                            return;
                        }
                        facilityPositionLeft = this.FacilityPositionLeft;
                    }
                    if (list2.Count > 0)
                    {
                        FacilityKind facilityKind = list2[GameObject.Random(list2.Count)];
                        this.BelongedFaction.DepositTechniquePointForFacility(facilityKind.PointCost);
                        this.BeginToBuildAFacility(facilityKind);
                    }
                    else if (list.Count > 0)
                    {
                        this.PlanFacilityKind = list[GameObject.Random(list.Count)];
                        if (GameObject.Chance(0x21) && ((this.BelongedFaction.TechniquePoint + this.BelongedFaction.TechniquePointForFacility) < this.PlanFacilityKind.PointCost))
                        {
                            this.BelongedFaction.SaveTechniquePointForFacility(this.PlanFacilityKind.PointCost / this.PlanFacilityKind.Days);
                        }
                    }*/
                }
            }
        }

        public int EnoughPeople
        {
            get
            {
                int fundSupport = this.Fund / (Session.Parameters.InternalFundCost * 30);
                int develop = Math.Max((this.AgricultureCeiling - this.Agriculture) / 90,
                    Math.Max((this.CommerceCeiling - this.Commerce) / 90,
                    Math.Max((this.TechnologyCeiling - this.Technology) / 90,
                    Math.Max((this.EnduranceCeiling - this.Endurance) / 90,
                    Math.Max((this.MoraleCeiling - this.Morale) / 90,
                    (this.DominationCeiling - this.Domination) / 30)))));
                int frontLine = (this.withoutTruceFrontline || this.noFactionFrontline) ? this.EffectiveMilitaryCount * 2 : 0;
                return Math.Min(this.MaxSupportableTroop, Math.Min(Math.Max(develop, frontLine), fundSupport));
            }
        }

        public bool Abandoned
        {
            get
            {
                if (!this.HasHostileTroopsInView()) return false;
                if (this.Endurance >= 30) return false;
                if (this.TotalHostileForce > (this.TotalFriendlyForce + this.TotalStoredForce))
                {
                    return true;
                }
                return false;
            }
        }

        public int TroopReserveScale
        {
            get
            {
                Person leader = this.BelongedFaction.Leader;
                int reserve = (int)(((leader.Calmness - leader.Braveness) * Session.Parameters.AIBackendArmyReserveCalmBraveDifferenceMultiply +
                    (5 - (int)leader.Ambition) * Session.Parameters.AIBackendArmyReserveAmbitionMultiply)
                    * Session.Parameters.AIBackendArmyReserveMultiply + Session.Parameters.AIBackendArmyReserveAdd);

                return reserve;
            }
        }

        public bool HasEnoughTroopReserve
        {
            get
            {
                return this.ArmyScale >= this.TroopReserveScale;
            }
        }

        public void WithdrawPerson()
        {
            if (this.BelongedFaction.ArchitectureCount <= 1) return;
            int num = this.PersonCount - this.MilitaryCount;
            GameObjectList list = this.Persons.GetList();
            if (list.Count > 1)
            {
                list.IsNumber = true;
                list.SmallToBig = true;
                list.PropertyName = "FightingForce";
                list.ReSort();
            }
            Architecture capital = this.BelongedFaction.Capital;
            ArchitectureList otherArchitectureList = this.GetOtherArchitectureList();
            if (capital == this)
            {
                if (otherArchitectureList.Count > 1)
                {
                    otherArchitectureList.IsNumber = true;
                    otherArchitectureList.PropertyName = "ArmyScaleWeighing";
                    otherArchitectureList.ReSort();
                    capital = otherArchitectureList[0] as Architecture;
                }
            }
            ArchitectureList otherArch = this.GetOtherArchitectureList();
            Architecture dest = (Architecture)otherArch[GameObject.Random(otherArch.Count)];
            double minDist = double.MaxValue;
            foreach (Architecture i in otherArchitectureList)
            {
                double distance = Session.Current.Scenario.GetDistance(this.Position, i.Position);
                if (distance < minDist && !dest.Abandoned)
                {
                    minDist = distance;
                    dest = i;
                }
            }
            int num2 = 0;
            while (num2 < num)
            {
                Person p = list[num2] as Person;
                if (!p.HasFollowingArmy && !p.HasLeadingArmy)
                {
                    p.WaitForFeiZi = null;
                    p.MoveToArchitecture(dest);
                    foreach (Person q in p.AvailableVeryClosePersons)
                    {
                        q.MoveToArchitecture(dest);
                    }
                }
                num2++;
            }
        }

        public bool HasEnoughPeople
        {
            get
            {
                return this.PersonCount + this.MovingPersonCount >= this.EnoughPeople;
            }
        }

        public bool CallResource(Architecture src, int fund, int food)
        {
            if (fund == 0 && food == 0) return false;

            int actualTransferFood = food;
            int actualTransferFund = fund;

            src.DecreaseFood(food);
            src.DecreaseFund(fund);

            if (food > 100)
            {
                this.AddFoodPack((int) (food / Session.Current.Scenario.GetResourceConsumptionRate(src, this)), Session.Current.Scenario.GetTransferFundDays(src, this));
            }
            if (fund > 100)
            {
                this.AddFundPack((int) (fund / Session.Current.Scenario.GetResourceConsumptionRate(src, this)), Session.Current.Scenario.GetTransferFundDays(src, this));
            }

            return true;
        }

        public int CallMilitary(Architecture src, int scale)
        {
            MilitaryList leaderlessArmies = new MilitaryList();
            int transferredScale = 0;

            foreach (Military i in src.Militaries)
            {
                if ((i.FollowedLeader == null || i.Leader == null) && !i.IsTransport)
                {
                    leaderlessArmies.Add(i);
                }
            }

            foreach (Military i in leaderlessArmies.GetRandomList())
            {
                if (i.Scales < i.MaxScale && this.IsTroopExceedsLimit)
                {
                    continue;
                }
                if (i.Scales + transferredScale <= scale)
                {
                    src.TransferMilitary(i, this);
                    transferredScale += i.Scales;
                    if (transferredScale >= scale) return transferredScale;
                }
            }

            this.SuspendTroopTransfer = 30;

            return transferredScale;
        }

        /*
        public int CallTroop(Architecture src, int scale)
        {
            MilitaryList leaderlessArmies = new MilitaryList();
            int transferredScale = 0;

            foreach (Military i in src.Militaries)
            {
                if ((i.FollowedLeader == null || i.Leader == null) && !i.IsTransport)
                {
                    leaderlessArmies.Add(i);
                }
            }

            foreach (Military i in leaderlessArmies.GetRandomList())
            {
                if (i.Scales + transferredScale <= scale)
                {
                   // Military transfer = src.TransferMilitary(i, this);
                    Troop built = src.BuildTroopForTransfer(i, this);
                    if (built == null) return transferredScale;

                    transferredScale += i.Scales;
                    if (transferredScale >= scale) return transferredScale;
                }
            }

            /*if (transferredScale < scale)
            {
                foreach (Military i in src.Militaries.GetRandomList())
                {
                    if (i.IsTransport) continue;
                    if (i.Scales <= transferredScale)
                    {
                        if (src.Persons.HasGameObject(i.Leader) || src.Persons.HasGameObject(i.FollowedLeader))
                        {
                            transferredScale += i.Scales;
                            src.BuildTroopForTransfer(i, this);
                            if (transferredScale >= scale) return transferredScale;
                        }
                        else
                        {
                            Person armyLeader = i.FollowedLeader != null ? i.FollowedLeader : i.Leader;
                            if (armyLeader != null && !armyLeader.IsCaptive && armyLeader.LocationArchitecture != null
                                && armyLeader.Status == PersonStatus.Normal
                                &&
                                (!Session.Current.Scenario.IsPlayer(this.BelongedFaction) || armyLeader.LocationArchitecture.BelongedSection == this.BelongedSection))
                            {
                                armyLeader.MoveToArchitecture(this);
                            }
                        }
                    }
                }
            }

            this.SuspendTroopTransfer = 30;

            return transferredScale;
        }*/

        public int CallPeople(Architecture src, int cnt)
        {
            GameObjectList list = src.MovablePersons.GetList();
            if (list.Count > 1)
            {
                list.IsNumber = true;
                list.SmallToBig = false;
                list.PropertyName = this.FrontLine ? "FightingForce" : "Merit";
                list.ReSort();
            }
            if (src != null)
            {
                int num2 = 0;
                int called = 0;
                while (called < cnt && num2 < list.Count)
                {
                    Person p = list[num2] as Person;
                    if (!p.DontMoveMeUnlessIMust )
                    {
                        p.MoveToArchitecture(this);
                        called++;
                        foreach (Person q in p.AvailableVeryClosePersons)
                        {
                            if (!this.BelongedFaction.MayorList.GameObjects.Contains(q))
                            {
                                q.MoveToArchitecture(this);
                                called++;
                            }


                        }
                    }
                    num2++;
                }
                return called;
            }
            return 0;
        }

        public void WithdrawResources()
        {
            if (!this.TransferFoodAvail() || !this.TransferFundAvail()) return;

            int transferFood = Math.Max(0, this.Food - this.EnoughFood);
            int transferFund = Math.Max(0, this.Fund - this.EnoughFund);

            foreach (LinkNode n in this.AIAllLinkNodes.Values)
            {
                if (n.A.BelongedFaction != this.BelongedFaction) continue;

                int toTransferFood = Math.Max(0, Math.Min(transferFood, n.A.FoodCeiling * 4 / 5 - n.A.Food));
                int toTransferFund = Math.Max(0, Math.Min(transferFund, n.A.FundCeiling * 4 / 5 - n.A.Fund));

                transferFood -= toTransferFood;
                transferFund -= toTransferFund;
                n.A.CallResource(this, toTransferFund, toTransferFood);

                if (transferFund <= 0 && transferFood <= 0)
                {
                    return;
                }
            }

            foreach (Architecture a in this.BelongedFaction.Architectures)
            {
                int toTransferFood = Math.Max(0,Math.Min(transferFood, a.FoodCeiling * 4 / 5 - a.Food));
                int toTransferFund = Math.Max(0,Math.Min(transferFund, a.FundCeiling * 4 / 5 - a.Fund));

                transferFood -= toTransferFood;
                transferFund -= toTransferFund;
                a.CallResource(this, toTransferFund, toTransferFood);

                if (transferFund <= 0 && transferFood <= 0)
                {
                    return;
                }
            }

        }

        public bool IsNetLosingPopulation
        {
            get
            {
                return this.RecentlyAttacked <= 0 && this.PopulationDevelopingRate < 0;
            }
        }

        private void assignWork(Person p, ArchitectureWorkKind k, bool[] need, bool needOnlyOneDomination, bool needOnlyOneMorale, bool needOnlyOneTrain)
        {
            switch (k)
            {
                case ArchitectureWorkKind.农业:
                    if (need[0]) p.WorkKind = ArchitectureWorkKind.农业;
                    else p.WorkKind = ArchitectureWorkKind.无;
                    break;
                case ArchitectureWorkKind.商业:
                    if (need[1]) p.WorkKind = ArchitectureWorkKind.商业;
                    else p.WorkKind = ArchitectureWorkKind.无;
                    break;
                case ArchitectureWorkKind.技术:
                    if (need[2]) p.WorkKind = ArchitectureWorkKind.技术;
                    else p.WorkKind = ArchitectureWorkKind.无;
                    break;
                case ArchitectureWorkKind.统治:
                    if (need[3])
                    {
                        p.WorkKind = ArchitectureWorkKind.统治;
                        if (needOnlyOneDomination) // 因为补充导致的统治下降1或2点时，只需要选择1个武将进行统治就足够了
                            need[3] = false;
                    }
                    else p.WorkKind = ArchitectureWorkKind.无;
                    break;
                case ArchitectureWorkKind.民心:
                    if (need[4])
                    {
                        p.WorkKind = ArchitectureWorkKind.民心;
                        if (needOnlyOneMorale) // 因为补充导致的民心下降1或2点时，只需要选择1个武将进行民心就足够了
                            need[4] = false;
                    }
                    else p.WorkKind = ArchitectureWorkKind.无;
                    break;
                case ArchitectureWorkKind.耐久:
                    if (need[5]) p.WorkKind = ArchitectureWorkKind.耐久;
                    else p.WorkKind = ArchitectureWorkKind.无;
                    break;
                case ArchitectureWorkKind.训练:
                    if (need[6])
                    {
                        p.WorkKind = ArchitectureWorkKind.训练;
                        if (needOnlyOneTrain) // 因为补充导致的士气或战意下降1或2点时，只需要选择1个武将进行训练就足够了
                            need[6] = false;
                    }
                    else p.WorkKind = ArchitectureWorkKind.无;
                    break;
                default:
                    p.WorkKind = ArchitectureWorkKind.无;
                    break;
            }
        }

        // 从农业商业技术统治民心耐久训练随机挑一项工作，need储存相应工作是否需要做
        // 根据能力比例随机选择，例如：只有农业商业要做，农业200，商业100。则2/3做农业，1/3做商业
        // 有了这个函数可以避免用resort，太浪费时间而且不科学
        private void assignRandomWork(Person p, bool[] need, bool needOnlyOneDomination, bool needOnlyOneMorale, bool needOnlyOneTrain)
        {
            int totalAbility = 0;
            for (int i = 0; i < need.Length; i++)
            {
                if (need[i])
                {
                    switch (i)
                    {
                        case 0:
                            totalAbility += p.AgricultureAbility;
                            break;
                        case 1:
                            totalAbility += p.CommerceAbility;
                            break;
                        case 2:
                            totalAbility += p.TechnologyAbility;
                            break;
                        case 3:
                            totalAbility += p.DominationAbility;
                            break;
                        case 4:
                            totalAbility += p.MoraleAbility;
                            break;
                        case 5:
                            totalAbility += p.EnduranceAbility;
                            break;
                        case 6:
                            totalAbility += p.TrainingAbility;
                            break;
                    }
                }
            }
            if (totalAbility == 0)
                p.WorkKind = ArchitectureWorkKind.无;
            else
            {
                int workIndex = StaticMethods.Random(totalAbility);
                for (int i = 0; i < need.Length; i++)
                {
                    if (need[i])
                    {
                        switch (i)
                        {
                            case 0:
                                workIndex -= p.AgricultureAbility;
                                if (workIndex < 0) p.WorkKind = ArchitectureWorkKind.农业;
                                break;
                            case 1:
                                workIndex -= p.CommerceAbility;
                                if (workIndex < 0) p.WorkKind = ArchitectureWorkKind.商业;
                                break;
                            case 2:
                                workIndex -= p.TechnologyAbility;
                                if (workIndex < 0) p.WorkKind = ArchitectureWorkKind.技术;
                                break;
                            case 3:
                                workIndex -= p.DominationAbility;
                                if (workIndex < 0)
                                {
                                    p.WorkKind = ArchitectureWorkKind.统治;
                                    if (needOnlyOneDomination) // 因为补充导致的统治下降1或2点时，只需要选择1个武将进行统治就足够了
                                        need[3] = false;
                                }
                                break;
                            case 4:
                                workIndex -= p.MoraleAbility;
                                if (workIndex < 0)
                                {
                                    p.WorkKind = ArchitectureWorkKind.民心;
                                    if (needOnlyOneMorale) // 因为补充导致的民心下降1或2点时，只需要选择1个武将进行民心就足够了
                                        need[4] = false;
                                }
                                break;
                            case 5:
                                workIndex -= p.EnduranceAbility;
                                if (workIndex < 0) p.WorkKind = ArchitectureWorkKind.耐久;
                                break;
                            case 6:
                                workIndex -= p.TrainingAbility;
                                if (workIndex < 0)
                                {
                                    p.WorkKind = ArchitectureWorkKind.训练; // 因为补充导致的士气或战意下降1或2点时，只需要选择1个武将进行训练就足够了
                                    if (needOnlyOneTrain)
                                        need[6] = false;
                                }
                                break;
                        }
                        if (workIndex < 0)
                            break;
                    }
                }
            }
        }

        private bool AdjacentToHostileByWater
        {
            get
            {
                if (this.BelongedFaction == null) return true;
                foreach (Architecture i in AIWaterLinks)
                {
                    if (i.BelongedFaction == null || !this.BelongedFaction.IsFriendly(i.BelongedFaction))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        private void AutoRecruit()
        {
            if (RecruitmentAvail() && this.Fund > 500 && (this.IsFundEnough || this.HasHostileTroopsInView()))
            {
                MilitaryList recruitmentMilitaryList = this.GetRecruitmentMilitaryList();

                recruitmentMilitaryList.PropertyName = "Merit";
                recruitmentMilitaryList.IsNumber = true;
                recruitmentMilitaryList.SmallToBig = false;
                recruitmentMilitaryList.ReSort();

                GameObjectList recruitmentPersonList = this.Persons.GetList();
                recruitmentPersonList.PropertyName = "RecruitmentAbility";
                recruitmentPersonList.IsNumber = true;
                recruitmentPersonList.SmallToBig = false;
                recruitmentPersonList.ReSort();

                int recruitCount = Math.Min(recruitmentMilitaryList.Count, recruitmentPersonList.Count);
                if (this.MilitaryPopulation < 10)
                {
                    recruitCount = 1;
                }

                for (int i = 0; i < recruitCount; ++i)
                {
                    (recruitmentPersonList[i] as Person).RecruitMilitary(recruitmentMilitaryList[i] as Military);
                }
            }
            else
            {
                foreach (Military m in this.Militaries)
                {
                    m.StopRecruitment();
                }
            }
        }

        private void AIWork(bool forPlayer)
        {
            this.StopAllWork();
            if (!this.HasPerson()) return;

            MilitaryList trainingMilitaryList = this.GetTrainingMilitaryList();
            bool needTrain = (trainingMilitaryList.Count > 0);

            if ((!this.IsFundEnough && this.RecentlyAttacked <= 0) || this.Fund < Session.Current.Scenario.Parameters.InternalFundCost) // 资金不足时全武将训练
            {
                if (needTrain)
                {
                    foreach (Person p in this.Persons)
                    {
                        p.WorkKind = ArchitectureWorkKind.训练;
                    }
                }
            }
            else                                                                                                       // 资金足够
            {
                bool[] need = {this.Kind.HasAgriculture && this.Agriculture < this.AgricultureCeiling * 0.8,
                               this.Kind.HasCommerce && this.Commerce < this.CommerceCeiling * 0.8,
                               this.Kind.HasTechnology && this.Technology < this.TechnologyCeiling * 0.8,
                               this.Kind.HasDomination && this.Domination < this.DominationCeiling * 0.8,
                               this.Kind.HasMorale && this.Morale < this.MoraleCeiling * 0.8,
                               this.Kind.HasEndurance && this.Endurance < this.EnduranceCeiling * 0.8,
                               needTrain};
                bool needOnlyOneDomination = false;
                bool needOnlyOneMorale = false;
                bool needOnlyOneTrain = false;

                if (this.RecentlyAttacked > 0 && this.Endurance < this.EnduranceCeiling)
                {
                    need[0] = need[1] = need[2] = false;
                    need[5] = true;
                }
                else if ((!IsFundIncomeEnough && need[1])
                    || (!IsFoodIncomeEnough && need[0])
                    || (this.PopulationDevelopingRate <= 0 && (need[3] || need[4]))
                    || this.Endurance < 30)
                {
                    need[0] &= !IsFoodIncomeEnough;
                    need[1] &= !IsFundIncomeEnough;
                    need[2] = false;
                    need[3] &= this.PopulationDevelopingRate <= 0 && this.Domination <= this.DominationCeiling * 0.9;
                    need[4] &= this.PopulationDevelopingRate <= 0 && this.Morale <= this.MoraleCeiling * 0.75;
                    need[5] = this.Endurance < 30;
                }

                if (!need[0] && !need[1] && !need[2] && !need[3] && !need[4] && !need[5] & !need[6])
                {
                     need = new bool[]{this.Kind.HasAgriculture && this.Agriculture < this.AgricultureCeiling,
                               this.Kind.HasCommerce && this.Commerce < this.CommerceCeiling,
                               this.Kind.HasTechnology && this.Technology < this.TechnologyCeiling,
                               this.Kind.HasDomination && this.Domination < this.DominationCeiling,
                               this.Kind.HasMorale && this.Morale < this.MoraleCeiling,
                               this.Kind.HasEndurance && this.Endurance < this.EnduranceCeiling,
                               needTrain};
                }

                if (trainingMilitaryList.Count == 1)
                {
                    Military m = trainingMilitaryList[0] as Military;                       // 因为补充导致的士气和战意下降1-3点时，只需要选择1个武将进行训练就足够了
                    needOnlyOneTrain = (m.Morale >= m.MoraleCeiling - 3) && (m.Combativity >= m.CombativityCeiling - 3);
                }

                bool recentlyAttacked = this.RecentlyAttacked > 0;
                int number = 0;
                if (!recentlyAttacked) // 最近没有受到攻击
                {
                    foreach (Person p in this.Persons)
                    {
                        p.resetPreferredWorkkind(need);
                        number = StaticMethods.Random(100);
                        if (number < 90) // 90%做第一选择
                        {
                            assignWork(p, p.firstPreferred, need, needOnlyOneDomination, needOnlyOneMorale, needOnlyOneTrain);
                            if (p.WorkKind == ArchitectureWorkKind.无) // 如果不成功，随机挑一样工作
                                assignRandomWork(p, need, needOnlyOneDomination, needOnlyOneMorale, needOnlyOneTrain);
                            if (p.WorkKind == ArchitectureWorkKind.无) // keep everyone busy
                                assignRandomWork(p, new bool[] { true, true, true, true, true, true, true }, false, false, false);
                        }
                        else // 10%随机
                        {
                            assignRandomWork(p, need, needOnlyOneDomination, needOnlyOneMorale, needOnlyOneTrain);
                        }
                    }
                }
                else // 最近受到攻击
                {
                    bool[] need2 = {false, false, this.Kind.HasTechnology && this.Technology < 200, this.Kind.HasDomination && this.Domination < this.DominationCeiling - 5,
                                    this.Kind.HasMorale && this.Morale < Session.Parameters.RecruitmentMorale, this.Kind.HasEndurance && this.Endurance < 500, needTrain};
                    foreach (Person p in this.Persons)
                    {
                        p.resetPreferredWorkkind(need);
                        number = StaticMethods.Random(100);
                        if (number < 50) // 50%做第一选择
                        {
                            assignWork(p, p.firstPreferred, need, needOnlyOneDomination, needOnlyOneMorale, needOnlyOneTrain);
                            if (p.WorkKind == ArchitectureWorkKind.无) // 如果不成功，随机挑一样工作
                                assignRandomWork(p, need, needOnlyOneDomination, needOnlyOneMorale, needOnlyOneTrain);
                        }
                        else // 50%随机
                        {
                            // 进入这里时不做农业商业，某些条件下做技术统治民心耐久，保证城内能新建部队反击
                            assignRandomWork(p, need2, needOnlyOneDomination, needOnlyOneMorale, needOnlyOneTrain);
                            if (p.WorkKind == ArchitectureWorkKind.无) // 可能城市刚受完攻击但技术统治民心耐久都相对较高，上面那句没有分配工作，这时候还需要再分配一次
                                assignRandomWork(p, need, needOnlyOneDomination, needOnlyOneMorale, needOnlyOneTrain);
                        }
                    }
                }

                // 分配完工作后选择人物补充军队
                if (!forPlayer)
                {

                    bool needRecruit = false;
                    bool lotsOfPopulation = GameObject.Chance((int)((((float)this.Population / (float)this.PopulationCeiling) * 100f - 50f) * 2.5));
                    if ((recentlyAttacked || this.BelongedFaction.PlanTechniqueArchitecture != this) && this.Kind.HasPopulation && ((recentlyAttacked || GameObject.Random((int)this.BelongedFaction.Leader.StrategyTendency + 1) == 0) && this.RecruitmentAvail()))
                    {
                        if (this.ArmyScale < this.FewArmyScale || lotsOfPopulation)
                        {
                            needRecruit = true;
                        }
                        else if (!this.IsFoodEnough)
                        {
                            needRecruit = false;
                        }
                        else if (GameObject.Random(Enum.GetNames(typeof(PersonStrategyTendency)).Length) < (int)this.BelongedFaction.Leader.StrategyTendency)
                        {
                            needRecruit = false;
                        }
                        else if (!GameObject.Chance(this.Domination * 4 - 300) || this.Morale < 100)
                        {
                            needRecruit = false;
                        }
                        else
                        {
                            bool nearFrontline = this.FrontLine || this.HostileLine || this.noFactionFrontline;
                            if (!nearFrontline)
                            {
                                foreach (LinkNode a in this.AIAllLinkNodes.Values)
                                {
                                    if (a.Level <= 1 && (a.A.FrontLine || a.A.HostileLine || a.A.noFactionFrontline) && !a.A.Kind.HasPopulation)
                                    {
                                        nearFrontline = true;
                                        break;
                                    }
                                }
                            }

                            needRecruit = this.MilitaryPopulation >
                                this.PopulationDevelopingRate * this.PopulationCeiling * Session.Parameters.AIRecruitPopulationCapMultiply *
                                (nearFrontline ? 1.0 : Session.Parameters.AIRecruitPopulationCapBackendMultiply) /
                                (this.BelongedSection != null && this.BelongedSection.AIDetail.ValueRecruitment ? 2.0 : 1) /
                                ((int)this.BelongedFaction.Leader.StrategyTendency
                                * Session.Parameters.AIRecruitPopulationCapStrategyTendencyMulitply + Session.Parameters.AIRecruitPopulationCapStrategyTendencyAdd)
                                / (this.HostileLine ? Session.Parameters.AIRecruitPopulationCapHostilelineMultiply : 1);
                        }
                    }
                    needRecruit = needRecruit && (GameObject.Chance(this.Persons.Count * 25) || (!need[0] && !need[1] && !need[2])); // 太少武将在城内时就不要补充了，先搞好内政更重要
                    needRecruit = needRecruit && (this.Population > this.PopulationCeiling / 5);
                    if (needRecruit)
                    {
                        AutoRecruit();
                    }
                }

                // 最后再选择人物赈灾
                if (this.kezhenzai() && this.IsFundEnough && this.IsFoodEnough)
                {
                    foreach (Person p in this.Persons)
                    {
                        if (p.zhenzaiAbility > 200)
                        {
                            p.WorkKind = ArchitectureWorkKind.赈灾;
                        }
                    }
                }

                // 新建部队
                if (!forPlayer)
                {
                    this.AIRecruitMilitary();
                }
            }
        }

        public void WithdrawMilitaries()
        {
            GameObjectList list = this.Militaries.GetList();
            foreach (Military m in list)
            {
                this.DisbandMilitary(m);
            }
        }

        private void AIRecruitMilitary()
        {
            bool flag2 = this.RecentlyAttacked > 0;
            if ((this.Kind.HasPopulation && (flag2 || (this.BelongedFaction.PlanTechniqueArchitecture != this))) &&
                (flag2 || (this.Population > ((this.RecruitmentPopulationBoundary * (1 + (int)this.BelongedFaction.Leader.StrategyTendency * 0.5f)) + GameObject.Random(this.RecruitmentPopulationBoundary)))))
            {
                int unfullArmyCount = 0;
                int unfullNavalArmyCount = 0;
                foreach (Military military in this.Militaries)
                {
                    if (military.Scales < ((((float)military.Kind.MaxScale) / ((float)military.Kind.MinScale)) * 0.75f) && !military.IsTransport)
                    {
                        unfullArmyCount++;
                        if (military.Kind.Type == MilitaryType.水军)
                        {
                            unfullNavalArmyCount++;
                        }
                    }
                }
                int unfullArmyCountThreshold;
                if (this.IsFoodAbundant && this.IsFundAbundant)
                {
                    unfullArmyCountThreshold = Math.Min((this.MilitaryPopulation) * (this.BelongedFaction.Leader.Ambition + 1) / Session.Parameters.AINewMilitaryPopulationThresholdDivide + 1, 
                        (this.PersonCount + this.MovingPersonCount) * (this.BelongedFaction.Leader.Ambition + 1) / Session.Parameters.AINewMilitaryPersonThresholdDivide + 1);
                }
                else
                {
                    unfullArmyCountThreshold = 1;
                }
                if (unfullArmyCount < unfullArmyCountThreshold)
                {
                    if (this.AIWaterLinks.Count > 0 && this.IsBesideWater && this.HasShuijunMilitaryKind() && 
                        (this.EffectiveMilitaryCount == 0 || GameObject.Chance((int)(100 - this.ShuijunMilitaryCount / (double)this.EffectiveMilitaryCount * 100))))
                    {
                        this.AIRecruitment(true, false);
                    }
                    else if (this.AILandLinks.Count <= 0)
                    {
                        this.AIRecruitment(true, false);
                    }
                    else
                    {
                        int siegeCount = 0;
                        foreach (Military m in this.Militaries)
                        {
                            if (m.Kind.Type == MilitaryType.器械)
                            {
                                siegeCount++;
                            }
                        }
                        if (siegeCount < this.Militaries.Count / (this.IsBesideWater ? 6 : 3))
                        {
                            this.AIRecruitment(false, true);
                        }
                        else
                        {
                            this.AIRecruitment(false, false);
                        }
                    }
                }
            }

            //disband unused transports except one
            MilitaryList ml = new MilitaryList();
            foreach (Military m in Militaries)
            {
                if (m.IsTransport)
                {
                    ml.Add(m);
                }
            }
            if (ml.Count > 1)
            {
                Military minTroop = null;
                int min = int.MaxValue;
                foreach (Military m in ml)
                {
                    if (m.Quantity < min)
                    {
                        min = m.Quantity;
                        minTroop = m;
                    }
                }
                this.DisbandMilitary(minTroop);
            }
        }

        private void ConvinceNoFactionAI()
        {
            if (this.HasPerson() && this.IsFundEnough && this.HasNoFactionPerson() && !this.HasHostileTroopsInView())
            {
                GameObjectList convincer = this.Persons.GetList();
                convincer.SmallToBig = false;
                convincer.PropertyName = "ConvinceAbility";
                convincer.IsNumber = true;
                convincer.ReSort();

                GameObjectList convinced = this.NoFactionPersons.GetList();
                convinced.SmallToBig = false;
                convinced.PropertyName = "Merit";
                convinced.IsNumber = true;
                convinced.ReSort();

                foreach (Person p in convinced)
                {
                    foreach (Person q in convincer)
                    {
                        if (q.CanConvinceChance(p) > 10 && q.Status == PersonStatus.Normal)
                        {
                            q.OutsideDestination = this.ArchitectureArea.Centre;
                            q.GoForConvince(p);
                            break;
                        }
                    }
                }
            }
        }

        private void ConvinceCaptivesAI(Architecture architecture2)
        {
            if (this.BelongedFaction == null) return;
            if (this.HasHostileTroopsInView()) return;

            GameObjectList convincer = this.Persons.GetList();
            convincer.SmallToBig = false;
            convincer.PropertyName = "ConvinceAbility";
            convincer.IsNumber = true;
            convincer.ReSort();

            GameObjectList convinced = architecture2.Captives;
            convinced.SmallToBig = false;
            convinced.PropertyName = "Merit";
            convinced.IsNumber = true;
            convinced.ReSort();

            foreach (Captive p in convinced)
            {
                foreach (Person q in convincer)
                {
                    if (q.CanConvinceChance(p.CaptivePerson) > 20 && q.Status == PersonStatus.Normal)
                    {
                        q.OutsideDestination = this.ArchitectureArea.Centre;
                        q.GoForConvince(p.CaptivePerson);
                        break;
                    }
                }
            }
        }

        private List<Architecture> GettingInformationArchitectures()
        {
            if (this.BelongedFaction == null) return new List<Architecture>();
            return this.BelongedFaction.GettingInformationArchitectures();
        }

        private void OutsideTacticsAI()
        {
            ConvinceNoFactionAI();

            if (this.PlanArchitecture == null && this.RecentlyAttacked <= 0 && this.HasPerson() && this.IsFundEnough)
            {
                Architecture architecture2;
                int diplomaticRelation;
                Person firstHalfPerson;
                ArchitectureList unknownArch = new ArchitectureList();
                ArchitectureList knownArch = new ArchitectureList();
                foreach (Architecture architecture in this.GetClosestArchitectures(20, 40))
                {
                    if (!this.BelongedFaction.IsArchitectureKnown(architecture))
                    {
                        unknownArch.Add(architecture);
                    }
                    else
                    {
                        knownArch.Add(architecture);
                    }
                }
                if (this.BelongedSection != null && (unknownArch.Count > 0) && this.BelongedSection.AIDetail.AllowInvestigateTactics)
                {
                    if (unknownArch.Count > 1)
                    {
                        unknownArch.PropertyName = "Population";
                        unknownArch.IsNumber = true;
                        unknownArch.ReSort();
                    }
                    if ((((this.RecentlyAttacked <= 0) && (GameObject.Random(40) < GameObject.Random(unknownArch.Count))) && GameObject.Chance(20)) && this.InformationAvail())
                    {
                        architecture2 = unknownArch[GameObject.Random(unknownArch.Count / 2)] as Architecture;
                        List<Architecture> gettingInformation = GettingInformationArchitectures();
                        if (!this.BelongedFaction.IsArchitectureKnown(architecture2) && architecture2.BelongedFaction != null && !this.IsFriendly(architecture2.BelongedFaction) && !gettingInformation.Contains(architecture2))
                        {
                            diplomaticRelation = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, architecture2.BelongedFaction.ID);
                            if (((diplomaticRelation >= 0) && (GameObject.Random(diplomaticRelation + 200) <= GameObject.Random(50))) || ((diplomaticRelation < 0) && (GameObject.Random(Math.Abs(diplomaticRelation) + 100) >= GameObject.Random(100))))
                            {
                                firstHalfPerson = this.GetFirstHalfPerson("InformationAbility");
                                if ((((firstHalfPerson != null) && (!this.HasFollowedLeaderMilitary(firstHalfPerson) || GameObject.Chance(10))) && (GameObject.Random(firstHalfPerson.NonFightingNumber) > GameObject.Random(firstHalfPerson.FightingNumber))) && (GameObject.Random(firstHalfPerson.FightingNumber) < 100))
                                {
                                    firstHalfPerson.CurrentInformationKind = this.GetFirstHalfInformationKind();
                                    if (firstHalfPerson.CurrentInformationKind != null)
                                    {
                                        firstHalfPerson.GoForInformation(Session.Current.Scenario.GetClosestPoint(architecture2.ArchitectureArea, this.Position));
                                    }
                                }
                            }
                        }
                    }
                }

                if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction))
                {

                    if ((this.BelongedSection != null) && ((knownArch.Count > 0) && (this.PlanArchitecture == null)) && this.BelongedSection.AIDetail.AllowPersonTactics)
                    {
                        if (knownArch.Count > 1)
                        {
                            knownArch.PropertyName = "PersonCount";
                            knownArch.IsNumber = true;
                            knownArch.ReSort();
                        }
                        if ((this.HasPerson() && (GameObject.Random(this.Fund) >= this.GossipArchitectureFund)) && GameObject.Chance(50))
                        {
                            ArchitectureList list3 = new ArchitectureList();
                            foreach (Architecture architecture in knownArch)
                            {
                                if ((architecture.BelongedFaction != this.BelongedFaction) && (architecture.BelongedFaction != null))
                                {
                                    list3.Add(architecture);
                                }
                            }
                            if (list3.Count > 0)
                            {
                                architecture2 = list3[GameObject.Random(list3.Count / 2)] as Architecture;
                                if (GameObject.Chance(100 - architecture2.noEscapeChance * 2))
                                {
                                    if (!this.IsFriendly(architecture2.BelongedFaction))
                                    {
                                        diplomaticRelation = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, architecture2.BelongedFaction.ID);
                                        if (((diplomaticRelation >= 0) && (GameObject.Random(diplomaticRelation + 200) <= GameObject.Random(50))) || ((diplomaticRelation < 0) && (GameObject.Random(Math.Abs(diplomaticRelation) + 100) >= GameObject.Random(100))))
                                        {
                                            firstHalfPerson = this.GetFirstHalfPerson("GossipAbility");
                                            if (firstHalfPerson != null && !firstHalfPerson.HasLeadingArmy &&
                                                firstHalfPerson.NonFightingNumber > firstHalfPerson.FightingNumber &&
                                                firstHalfPerson.FightingNumber < 350 && firstHalfPerson != this.BelongedFaction.Leader &&
                                                (firstHalfPerson != firstHalfPerson.BelongedFaction.Leader || firstHalfPerson.ImmunityOfCaptive) &&
                                                GameObject.Random(architecture2.GetGossipablePersonCount() + 4) >= 4
                                                && GameObject.Random(firstHalfPerson.GossipAbility) >= 200
                                                && GameObject.Chance(100 - architecture2.captureChance))
                                            {
                                                firstHalfPerson.GoForGossip(Session.Current.Scenario.GetClosestPoint(architecture2.ArchitectureArea, this.Position));
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if ((this.HasPerson() && (GameObject.Random(this.Fund) >= this.ConvincePersonFund)) && GameObject.Chance(50) && this.BelongedSection.AIDetail.AllowPersonTactics)
                        {
                            ArchitectureList list4 = new ArchitectureList();
                            foreach (Architecture architecture in knownArch)
                            {
                                if (((architecture.BelongedFaction != this.BelongedFaction) && (architecture.BelongedFaction != null)) && architecture.HasPerson())
                                {
                                    list4.Add(architecture);
                                }
                            }
                            foreach (Architecture architecture in this.BelongedFaction.Architectures)
                            {
                                if (architecture.HasCaptive())
                                {
                                    list4.Add(architecture);
                                }
                            }
                            if (list4.Count > 0)
                            {
                                architecture2 = list4[GameObject.Random(list4.Count)] as Architecture;
                                if (architecture2.BelongedFaction == this.BelongedFaction)
                                {
                                    ConvinceCaptivesAI(architecture2);
                                }
                                else if (!this.IsFriendly(architecture2.BelongedFaction) || GameObject.Chance(50))
                                {
                                    diplomaticRelation = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, architecture2.BelongedFaction.ID);
                                    if (((diplomaticRelation >= 0) && (GameObject.Random(diplomaticRelation + 50) <= GameObject.Random(50))) || (diplomaticRelation < 0))
                                    {
                                        Person extremeLoyaltyPerson = architecture2.GetLowestLoyaltyPersonRecruitable();
                                        if ((extremeLoyaltyPerson != null) && ((extremeLoyaltyPerson.Loyalty < 100) && (extremeLoyaltyPerson.BelongedFaction != null)) && (extremeLoyaltyPerson != extremeLoyaltyPerson.BelongedFaction.Leader))
                                        {
                                            firstHalfPerson = this.GetFirstHalfPerson("ConvinceAbility");
                                            if (firstHalfPerson != null)
                                            {
                                                foreach (Person p in architecture2.Persons)
                                                {
                                                    if (firstHalfPerson.CanConvinceChance(p) > 20)
                                                    {
                                                        if (firstHalfPerson != null && !firstHalfPerson.HasLeadingArmy &&
                                                            firstHalfPerson.NonFightingNumber > firstHalfPerson.FightingNumber &&
                                                             firstHalfPerson.FightingNumber < 350 && firstHalfPerson != this.BelongedFaction.Leader &&
                                                            (firstHalfPerson != firstHalfPerson.BelongedFaction.Leader || firstHalfPerson.ImmunityOfCaptive)
                                                             && GameObject.Chance(100 - architecture2.captureChance))
                                                        {
                                                            firstHalfPerson.OutsideDestination = this.ArchitectureArea.Centre;
                                                            firstHalfPerson.GoForConvince(p);
                                                            break;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if ((this.HasPerson() && (GameObject.Random(this.Fund) >= this.JailBreakArchitectureFund)) && GameObject.Chance(50) && this.JailBreakAvail() && this.BelongedSection.AIDetail.AllowPersonTactics)
                        {
                            List<Architecture> a = new List<Architecture>();
                            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
                            {
                                if (architecture.HasFactionCaptive(this.BelongedFaction) && knownArch.GameObjects.Contains(architecture))
                                {
                                    a.Add(architecture);
                                }
                            }
                            if (a.Count > 0)
                            {
                                Architecture target = a[GameObject.Random(a.Count)] as Architecture;
                                if (GameObject.Chance(100 - target.noEscapeChance * 2))
                                {
                                    int totalCaptiveValue = 0;
                                    foreach (Captive c in target.Captives)
                                    {
                                        if (c.CaptiveFaction == this.BelongedFaction)
                                        {
                                            totalCaptiveValue += c.AIWantsTheCaptive;
                                        }
                                    }
                                    if (GameObject.Random(totalCaptiveValue) > GameObject.Random(100000))
                                    {
                                        firstHalfPerson = this.GetFirstHalfPerson("JailBreakAbility");
                                        if (firstHalfPerson != null && !firstHalfPerson.HasLeadingArmy &&
                                                firstHalfPerson.NonFightingNumber > firstHalfPerson.FightingNumber &&
                                                 firstHalfPerson.FightingNumber < 350 && firstHalfPerson != this.BelongedFaction.Leader &&
                                                (firstHalfPerson != firstHalfPerson.BelongedFaction.Leader || firstHalfPerson.ImmunityOfCaptive)
                                                 && GameObject.Chance(100 - target.captureChance))
                                        {
                                            firstHalfPerson.GoForJailBreak(Session.Current.Scenario.GetClosestPoint(target.ArchitectureArea, this.Position));
                                        }
                                    }
                                }
                            }
                        }
                        if (this.HasPerson() && GameObject.Chance(50) && this.AssassinateAvail() && this.BelongedSection.AIDetail.AllowPersonTactics)
                        {
                            if (knownArch.Count > 0)
                            {
                                Architecture target = (Architecture)knownArch[GameObject.Random(knownArch.Count)];
                                if (target.BelongedFaction != null || this.BelongedFaction.IsAlien)
                                {
                                    if (target.BelongedFaction != null && target.BelongedFaction != this.BelongedFaction)
                                    {
                                        diplomaticRelation = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, target.BelongedFaction.ID);
                                    }
                                    else
                                    {
                                        diplomaticRelation = -1000;
                                    }
                                    if (((diplomaticRelation >= 0) && (GameObject.Random(diplomaticRelation + 400) <= GameObject.Random(50))) || (diplomaticRelation < 0))
                                    {
                                        firstHalfPerson = this.GetFirstHalfPerson("AssassinateAbility");
                                        if (firstHalfPerson != null && firstHalfPerson.PersonalLoyalty < 4)
                                        {
                                            foreach (Person p in target.GetAssassinatePersonTarget(this.BelongedFaction))
                                            {
                                                int targetDef;
                                                if (p.Status == PersonStatus.Normal)
                                                {
                                                    targetDef = target.DefendAssassinateAbility;
                                                }
                                                else
                                                {
                                                    targetDef = p.AssassinateAbility;
                                                }
                                                if (firstHalfPerson.CanConvinceChance(p) <= 20 && firstHalfPerson.AssassinateAbility > targetDef * 2)
                                                {
                                                    if (target.BelongedFaction == this.BelongedFaction)
                                                    {
                                                        firstHalfPerson.OutsideDestination = this.ArchitectureArea.Centre;
                                                        firstHalfPerson.GoForAssassinate(p);
                                                        break;
                                                    }
                                                    else if (firstHalfPerson != null && !firstHalfPerson.HasLeadingArmy &&
                                                           firstHalfPerson.NonFightingNumber > firstHalfPerson.FightingNumber &&
                                                            firstHalfPerson.FightingNumber < 350 && firstHalfPerson != this.BelongedFaction.Leader &&
                                                           (firstHalfPerson != firstHalfPerson.BelongedFaction.Leader || firstHalfPerson.ImmunityOfCaptive)
                                                            && GameObject.Chance(100 - target.captureChance))
                                                    {
                                                        firstHalfPerson.OutsideDestination = this.ArchitectureArea.Centre;
                                                        firstHalfPerson.GoForAssassinate(p);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }

            InformationList toRemove = new InformationList();
            int dayCost = this.InformationDayCost;
            foreach (Information i in this.Informations)
            {
                bool stop = true;
                if (i.DaysStarted <= 3 * Session.Parameters.DayInTurn)
                {
                    stop = false;
                }
                else if (i.DaysStarted < GameObject.Random(10) + 30 && this.IsFundIncomeEnough && this.IsFundEnough
                    && dayCost < 500)
                {
                    foreach (Point p in i.Area.Area)
                    {
                        Architecture a = Session.Current.Scenario.GetArchitectureByPosition(p);
                        if (a != null && !this.IsFriendly(a.BelongedFaction))
                        {
                            stop = false;
                            break;
                        }
                    }
                }

                if (stop)
                {
                    bool hasEnemy = false;
                    bool hasOwn = false;
                    foreach (Point p in i.Area.Area)
                    {
                        Troop t = Session.Current.Scenario.GetTroopByPosition(p);
                        if (t != null && !this.IsFriendly(t.BelongedFaction))
                        {
                            hasEnemy = true;
                        }
                        if (t != null && t.BelongedFaction == this.BelongedFaction)
                        {
                            hasOwn = true;
                        }
                        if (hasEnemy && hasOwn)
                        {
                            stop = false;
                            break;
                        }
                    }
                }

                if (stop && this.PlanArchitecture != null)
                {
                    foreach (Point p in i.Area.Area)
                    {
                        Architecture a = Session.Current.Scenario.GetArchitectureByPosition(p);
                        if (a == this.PlanArchitecture)
                        {
                            stop = false;
                            break;
                        }
                    }
                }

                if (!stop)
                {
                    foreach (Information j in this.Informations)
                    {
                        if (i == j) continue;
                        if (toRemove.GameObjects.Contains(i)) continue;
                        if (j.Position == i.Position && j.Radius >= i.Radius && j.DayCost < i.DayCost)
                        {
                            stop = true;
                            break;
                        }
                    }
                }

                if (stop)
                {
                    toRemove.Add(i);
                    dayCost -= i.DayCost;
                }
            }
            foreach (Information i in toRemove)
            {
                i.Purify();
                this.RemoveInformation(i);
                Session.Current.Scenario.Informations.Remove(i);
            }
        }

        public bool IsArchitectureHostileWithoutTruce(Architecture a)
        {
            if (this.IsArchitectureHostile(a))
            {
                DiplomaticRelation rel = Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(this.BelongedFaction.ID, a.BelongedFaction.ID);
                if (rel.Truce > 60)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public bool IsArchitectureHostile(Architecture a)
        {
            if (a.BelongedFaction == null || this.BelongedFaction == null) return false;
            int n = (Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, a.BelongedFaction.ID) + a.BelongedFaction.ArchitectureTotalSize) - this.BelongedFaction.ArchitectureTotalSize;
            return n < 0;
        }

        public bool IsArchitectureCriticalHostile(Architecture a)
        {
            if (a.BelongedFaction == null || this.BelongedFaction == null) return false;
            int n = (Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, a.BelongedFaction.ID) + a.BelongedFaction.ArchitectureTotalSize) - this.BelongedFaction.ArchitectureTotalSize;
            return n < -200;
        }

        public int HostileScale
        {
            get
            {
                int result = 0;
                foreach (LinkNode i in this.AIAllLinkNodes.Values)
                {
                    if (this.IsArchitectureHostile(i.A) && i.Level <= 1)
                    {
                        result += i.A.ArmyScale;
                    }
                }
                return result * 2;
            }
        }

        public int OrientationScale
        {
            get
            {
                int result = 0;
                foreach (LinkNode i in this.AIAllLinkNodes.Values)
                {
                    if (this.BelongedSection.OrientationFaction == i.A.BelongedFaction && i.Level <= 1)
                    {
                        result += i.A.ArmyScale;
                    }
                }
                return result * 2;
            }
        }

        private PersonList GetFirstHalfPersonList(string propertyName)
        {
            PersonList list = new PersonList();
            if (this.Persons.Count > 0)
            {
                foreach (Person p in this.Persons)
                {
                    list.Add(p);
                }
                if (list.Count > 1)
                {
                    list.PropertyName = propertyName;
                    list.IsNumber = true;
                    list.ReSort();
                } else
                {
                    return list;
                }
                list.GameObjects.RemoveRange(list.Count / 2, list.Count - list.Count / 2);
                return (list);
            }
            return new PersonList();
        }

        private void AIAutoSearch()
        {
            if (this.HasHostileTroopsInView()) return;
            foreach (Person person in this.Persons.GetList())
            {
                if (person.WorkKind == ArchitectureWorkKind.无 && person.Tiredness <= 0)
                {
                    person.GoForSearch();
                }
            }
        }
        /*
        private void AIAutoZhaoXian()
        {
            if (Session.Current.Scenario.IsPlayer(this.BelongedFaction))
            {


                    if (GameObject.Chance(10)  && !this.HasEnoughPeople && this.AutoCreatePersonAvail())
                    {
                        this.AutoCreatePerson();
                    }

            }
        }*/

        public void AICampaign()
        {
            this.DefensiveCampaign(null);
            this.OffensiveCampaign();
        }

        private Point? GetRandomStartingPosition(Troop troop)
        {
            GameArea allAvailableArea = this.GetAllAvailableArea(false);
            GameArea sourceArea = new GameArea();
            foreach (Point point in allAvailableArea.Area)
            {
                if (((Session.Current.Scenario.GetArchitectureByPosition(point) == this) && (Session.Current.Scenario.GetTroopByPosition(point) == null)) || troop.IsMovableOnPosition(point))
                {
                    sourceArea.Area.Add(point);
                }
            }
            if (sourceArea.Count == 0)
            {
                return null;
            }
            return sourceArea[GameObject.Random(sourceArea.Count)];
        }

        private Point? GetRandomStartingPosition(Military m)
        {
            GameArea allAvailableArea = this.GetAllAvailableArea(false);
            m.ModifyAreaByTerrainAdaptablity(allAvailableArea);
            if (allAvailableArea.Count == 0)
            {
                return null;
            }
            return allAvailableArea[GameObject.Random(allAvailableArea.Count)];
        }

        /*
        public Troop BuildTransportTroop(Architecture destination, Military military, int food, int fund)
        {
            Troop troop;
            int min = int.MaxValue;
            PersonList leader = new PersonList();
            foreach (Person p in this.Persons)
            {
                if (p.Merit < min && p.Tiredness < 30 && p.InjureRate >= 1 && p.Loyalty > 80 &&
                    (this.HasHostileTroopsInView() || isPersonAllowedIntoTroop(p, military, false)))
                {
                    leader.Clear();
                    leader.Add(p);
                    min = p.Merit;
                }
            }
            if (leader.Count <= 0) return null;
            troop = Troop.CreateSimulateTroop(leader, military, this.Position);
            Point? nullable = this.GetRandomStartingPosition(troop);
            if (!nullable.HasValue)
            {
                return null;
            }
            troop.Destroy(true, false);
            troop = this.CreateTroop(leader, leader[0] as Person, military, food, nullable.Value);
            troop.WillArchitecture = destination;
            troop.zijin = fund;
            this.Fund -= fund;
            Legion legion = this.BelongedFaction.GetLegion(destination);
            if (legion == null)
            {
                legion = this.CreateOffensiveLegion(destination);
            }
            legion.AddTroop(troop);
            this.PostCreateTroop(troop, false);
            return troop;
        }
        */

        private void AIMilitary()
        {
            foreach (Military military in this.GetLevelUpMilitaryList())
            {
                List<MilitaryKind> candidates = military.Kind.GetLevelUpKinds(this);
                Dictionary<MilitaryKind, float> upgradable = new Dictionary<MilitaryKind, float>();
                foreach (MilitaryKind mk in candidates)
                {
                    if (military.Kind.LevelUpAvail(this))
                    {
                        float weight = 1;
                        foreach (KeyValuePair<Condition, float> c in military.Kind.AIUpgradeArchitectureConditionWeight)
                        {
                            if (c.Key.CheckCondition(this))
                            {
                                weight *= c.Value;
                            }
                        }
                        foreach (KeyValuePair<Condition, float> c in military.Kind.AIUpgradeLeaderConditionWeight)
                        {
                            if (c.Key.CheckCondition(military.Leader))
                            {
                                weight *= c.Value;
                            }
                        }
                        weight *= military.Kind.Merit;
                        upgradable.Add(mk, weight);
                    }
                }

                if (upgradable.Count > 0)
                {
                    this.LevelUpMilitary(military, GameObject.WeightedRandom(upgradable));
                }
            }

            bool merged = false;

            MilitaryList merger = this.GetMergeMilitaryList();
            merger.SmallToBig = false;
            merger.PropertyName = "LeaderFightingForce";
            merger.IsNumber = true;
            merger.ReSort();
            foreach (Military i in merger)
            {
                MilitaryList list = this.GetBeMergedMilitaryList(i);
                list.SmallToBig = true;
                list.PropertyName = "LeaderFightingForce";
                list.IsNumber = true;
                list.ReSort();
                foreach (Military j in list)
                {
                    if (i.Quantity + j.Quantity <= i.Kind.MaxScale)
                    {
                        int increment = j.Quantity + i.Quantity - i.Kind.MaxScale;
                        if (increment > 0)
                        {
                            this.IncreasePopulation(increment);
                            this.IncreaseMilitaryPopulation(increment);
                        }
                        if (j.LeaderID == i.LeaderID)
                        {
                            i.IncreaseQuantity(j.Quantity, j.Morale, j.Combativity, j.Experience, j.LeaderExperience);
                        }
                        else
                        {
                            i.IncreaseQuantity(j.Quantity, j.Morale, j.Combativity, j.Experience, 0);
                        }
                        this.RemoveMilitary(j);
                        this.BelongedFaction.RemoveMilitary(j);
                        Session.Current.Scenario.Militaries.Remove(j);
                        merged = true;
                        break;
                    }
                }
                if (merged) break;
            }


        }



        public void  TransferMilitary(Military military, Architecture destination) // 运兵无需武将来运
        {
            if (this.MilitaryCount == 0) return ;
            MilitaryList list = new MilitaryList();
            if ((military.Scales > 5) && (military.Morale >= 80) && (military.Combativity >= 80) && (military.InjuryQuantity < military.Kind.MinScale)
                && !military.IsFewScaleNeedRetreat && military.Kind.Movable && military.Kind.Type != MilitaryType.水军)
            {
                list.Add(military);
            }

            PersonList pl = new PersonList();
            Military military2 = military;
            if ((military2.FollowedLeader != null) && this.Persons.HasGameObject(military2.FollowedLeader) &&
                    military2.FollowedLeader.WaitForFeiZi == null && military2.FollowedLeader.LocationTroop == null
                    && !this.BelongedFaction.MayorList.GameObjects.Contains(military2.FollowedLeader))

            {
                    pl.Add(military2.FollowedLeader);
            }

             if ((((military2.Leader != null) && (military2.LeaderExperience >= 10)) && (((military2.Leader.Strength >= 80) || (military2.Leader.Command >= 80)) || military2.Leader.HasLeaderValidTitle))
                    && this.Persons.HasGameObject(military2.Leader) && military2.Leader.WaitForFeiZi == null && military2.Leader.LocationTroop == null
                    && !this.BelongedFaction.MayorList.GameObjects.Contains(military2.Leader))

             {

                    pl.Add(military2.Leader);
             }

             if (!this.IsSurrounded() && !destination.IsSurrounded ())
             {
                 if (list.Count > 0)
                 {
                     double distance = (double)Session.Current.Scenario.GetDistance(this.ArchitectureArea, destination.ArchitectureArea);
                     foreach (Military m in list)
                     {
                         int fundCost = (int) (military.TransferFundCost(distance) * (1 - this.TroopTransportFundRate));
                         int foodCost = (int) (military.TransferFoodCost(distance) * (1 - this.TroopTransportFoodRate));
                         if (this.Fund >= fundCost &&
                            this.Food >= foodCost)
                         {
                             this.DecreaseFund(military.TransferFundCost(distance));
                             this.DecreaseFood(military.TransferFoodCost(distance));
                             m.StartingArchitecture = this;
                             m.TargetArchitecture = destination;
                             m.ArrivingDays = Math.Max(1, (int) (m.TransferDays(distance) * (1 - this.TroopTransportDayRate)));
                             this.RemoveMilitary(m);
                             this.BelongedFaction.TransferingMilitaries.Add(m);
                             this.BelongedFaction.TransferingMilitaryCount++;
                         }
                     }

                 }

                 if (pl.Count > 0)
                 {
                     foreach (Person p in pl)
                     {
                         p.MoveToArchitecture(destination);
                     }
                 }
             }
          }

        private Troop BuildTroopForTransfer(Military military, Architecture destination)
        {
            if (this.Persons.Count == 0) return null;
            TroopList list = new TroopList();
            this.Persons.ClearSelected();
            if ((military.Scales > 5) && (military.Morale >= 80) && (military.Combativity >= 80) && (military.InjuryQuantity < military.Kind.MinScale)
                && !military.IsFewScaleNeedRetreat && military.Kind.Movable)
            {
                PersonList list2;
                Military military2 = military;
                if ((military2.FollowedLeader != null) && this.Persons.HasGameObject(military2.FollowedLeader) &&
                    !this.BelongedFaction.MayorList.GameObjects.Contains(military2.FollowedLeader )  &&
                    military2.FollowedLeader.WaitForFeiZi == null && military2.FollowedLeader.LocationTroop == null
                     && (!this.HasHostileTroopsInView() || isPersonAllowedIntoTroop(military2.FollowedLeader, military, false)))
                {
                    list2 = new PersonList();
                    list2.Add(military2.FollowedLeader);
                    military2.FollowedLeader.Selected = true;
                    Point? nullable = this.GetRandomStartingPosition(military2);
                    if (!nullable.HasValue)
                    {
                        return null;
                    }

                    Troop troop = this.CreateTroop(list2, military2.FollowedLeader, military2, -1, nullable.Value);
                    troop.WillArchitecture = destination;
                    Legion legion = this.BelongedFaction.GetLegion(destination);
                    if (legion == null)
                    {
                        legion = this.CreateOffensiveLegion(destination);
                    }
                    legion.AddTroop(troop);
                    return troop;
                }
                if ((((military2.Leader != null) && (military2.LeaderExperience >= 10)) && !this.BelongedFaction.MayorList.GameObjects.Contains(military2.Leader ) && (((military2.Leader.Strength >= 80) || (military2.Leader.Command >= 80)) || military2.Leader.HasLeaderValidTitle))
                    && this.Persons.HasGameObject(military2.Leader) && military2.Leader.WaitForFeiZi == null && military2.Leader.LocationTroop == null
                    && (!this.HasHostileTroopsInView() || isPersonAllowedIntoTroop(military2.Leader, military, false)))
                {
                    list2 = new PersonList();
                    list2.Add(military2.Leader);
                    military2.Leader.Selected = true;
                    Point? nullable = this.GetRandomStartingPosition(military2);
                    if (!nullable.HasValue)
                    {
                        return null;
                    }
                    Troop troop = this.CreateTroop(list2, military2.Leader, military2, -1, nullable.Value);
                    troop.WillArchitecture = destination;
                    Legion legion = this.BelongedFaction.GetLegion(destination);
                    if (legion == null)
                    {
                        legion = this.CreateOffensiveLegion(destination);
                    }
                    legion.AddTroop(troop);
                    return troop;
                }
                GameObjectList sortedList;
                if (this.HasHostileTroopsInView())
                {
                    sortedList = new GameObjectList();
                    foreach (Person p in this.Persons)
                    {
                        if (isPersonAllowedIntoTroop(p, military2, false))
                        {
                            sortedList.Add(p);
                        }
                    }
                }
                else
                {
                    sortedList = this.Persons.GetList() as GameObjectList;
                }
                sortedList.PropertyName = "FightingForce";
                sortedList.IsNumber = true;
                sortedList.SmallToBig = true;
                sortedList.ReSort();
                foreach (Person person in sortedList)
                {
                    if (!person.Selected && person.WaitForFeiZi == null && person.LocationTroop == null)
                    {
                        list2 = new PersonList();
                        list2.Add(person);
                        person.Selected = true;
                        Point? nullable = this.GetRandomStartingPosition(military2);
                        if (!nullable.HasValue)
                        {
                            break;
                        }
                        Troop troop = this.CreateTroop(list2, person, military2, -1, nullable.Value);
                        troop.WillArchitecture = destination;
                        Legion legion = this.BelongedFaction.GetLegion(destination);
                        if (legion == null)
                        {
                            legion = this.CreateOffensiveLegion(destination);
                        }
                        legion.AddTroop(troop);
                        return troop;
                    }
                }
            }
            return null;
        }



        private void AIRecruitment(bool water, bool siege)
        {
            if (Session.Current.Scenario.IsPlayer(this.BelongedFaction) && this.BelongedSection != null  && this.BelongedSection.AIDetail .AutoRun && !this.BelongedSection.AIDetail.AllowNewMilitary) return;

            if (this.Population > 0 && (this.IsFundEnough || this.HasHostileTroopsInView()))
            {
                MilitaryKind current;
                Dictionary<int, MilitaryKind>.ValueCollection.Enumerator enumerator;

                Dictionary<MilitaryKind, float> list = new Dictionary<MilitaryKind, float>();
                MilitaryKindList list2 = new MilitaryKindList();
                Dictionary<MilitaryKind, float> allMilitaries = new Dictionary<MilitaryKind, float>();
                MilitaryKindList allMilitaries2 = new MilitaryKindList();

                using (enumerator = this.BelongedFaction.AvailableMilitaryKinds.MilitaryKinds.Values.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (current.IsTransport) continue;
                        if (current.Type == MilitaryType.水军 && this.AIWaterLinks.Count == 0) continue;
                        if (current.Type != MilitaryType.水军 && this.AILandLinks.Count == 0) continue;

                        float weight = 1;
                        foreach (KeyValuePair<Condition, float> c in current.AICreateArchitectureConditionWeight)
                        {
                            if (c.Key.CheckCondition(this))
                            {
                                weight *= c.Value;
                            }
                        }

                        if (((water && current.Type == MilitaryType.水军) || (!water && current.Type != MilitaryType.水军))
                            && ((siege && current.Type == MilitaryType.器械) || (!siege && current.Type != MilitaryType.器械))
                            && current.CreateAvail(this))
                        {
                            list2.Add(current);
                            list.Add(current, weight);
                        }
                        if (current.CreateAvail(this))
                        {
                            allMilitaries2.Add(current);
                            allMilitaries.Add(current, weight);
                        }
                        /*if ((((this.ValueWater == (current.Type == MilitaryType.水军)) || (!water && GameObject.Chance(20))) && current.CreateAvail(this)) && (current.ID != 29))
                        {
                            list.Add(current);
                        }*/
                    }
                }
                using (enumerator = this.PrivateMilitaryKinds.MilitaryKinds.Values.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        current = enumerator.Current;
                        if (current.IsTransport) continue;
                        if (current.Type == MilitaryType.水军 && this.AIWaterLinks.Count == 0) continue;
                        if (current.Type != MilitaryType.水军 && this.AILandLinks.Count == 0) continue;

                        float weight = 1;
                        foreach (KeyValuePair<Condition, float> c in current.AICreateArchitectureConditionWeight)
                        {
                            if (c.Key.CheckCondition(this))
                            {
                                weight *= c.Value;
                            }
                        }
                        weight *= current.Merit;

                        if (((water && current.Type == MilitaryType.水军) || (!water && current.Type != MilitaryType.水军))
                            && ((siege && current.Type == MilitaryType.器械) || (!siege && current.Type != MilitaryType.器械))
                            && current.CreateAvail(this))
                        {
                            list2.Add(current);
                            if (list.ContainsKey(current))
                            {
                                list[current] *= weight;
                            }
                            else
                            {
                                list.Add(current, weight);
                            }
                        }
                        if (current.CreateAvail(this))
                        {
                            allMilitaries2.Add(current);
                            if (allMilitaries.ContainsKey(current))
                            {
                                allMilitaries[current] *= weight;
                            }
                            else
                            {
                                allMilitaries.Add(current, weight);
                            }
                        }
                    }
                }
                if (list.Count > 0)
                {
                    current = GameObject.WeightedRandom(list);
                    this.CreateMilitary(current.findSuccessorCreatable(list2, this));
                }
                else if (allMilitaries.Count > 0)
                {
                    current = GameObject.WeightedRandom(allMilitaries);
                    this.CreateMilitary(current.findSuccessorCreatable(allMilitaries2, this));
                }
                /*if (GameObject.Chance(90))
                {
                    list.PropertyName = "Merit";
                    list.IsNumber = true;
                    list.ReSort();
                    int maxValue = list.Count / 2;
                    if (list.Count > 1)
                    {
                        for (int i = maxValue; i < list.Count; i++)
                        {
                            if ((list[i] as MilitaryKind).Merit == (list[i - 1] as MilitaryKind).Merit)
                            {
                                maxValue++;
                            }
                        }
                    }
                    current = list[GameObject.Random(maxValue)] as MilitaryKind;
                    if ((!this.ValueWater || (current.Type == MilitaryType.水军)) || GameObject.Chance(20))
                    {
                        this.CreateMilitary(current);
                    }
                }
                else
                {
                    current = list[GameObject.Random(list.Count)] as MilitaryKind;
                    if ((!this.ValueWater || (current.Type == MilitaryType.水军)) || GameObject.Chance(20))
                    {
                        this.CreateMilitary(current);
                    }
                }*/
            }
        }



        public bool CompletelyDeveloped
        {
            get
            {
                return (this.Agriculture >= this.AgricultureCeiling && this.Commerce >= this.CommerceCeiling && this.Technology >= this.TechnologyCeiling &&
                    this.Domination >= this.DominationCeiling && this.Endurance >= this.EnduranceCeiling && this.Morale >= this.MoraleCeiling);
            }
        }



        private void AITrade()
        {
            if ((Session.Current.Scenario.Date.Day % Session.Parameters.AITradePeriod) <= Session.Current.Scenario.Parameters.DayInTurn)
            {
                int num;
                if (this.SellFoodAvail())
                {
                    if (!this.IsFundEnough && this.Food > this.EnoughFood * 2)
                    {
                        num = this.Food - this.EnoughFood * 2;
                        if (num > 0)
                        {
                            this.SellFood(num);
                        }
                    }
                    else if (!this.IsFundAbundant && this.Food > this.AbundantFood * 2)
                    {
                        num = this.Food - this.AbundantFood * 2;
                        if (num > 0)
                        {
                            this.SellFood(num);
                        }
                    }
                    else if (this.Fund < this.FundCeiling / 2 && this.Food >= FoodCeiling / 2)
                    {
                        num = this.Food - this.FoodCeiling / 2;
                        if (num > 0)
                        {
                            this.SellFood(num);
                        }
                    }
                }
                else if (this.BuyFoodAvail())
                {
                    if (this.Fund > this.EnoughFund * 2 && !this.IsFoodEnough)
                    {
                        num = this.Fund - this.EnoughFund * 2;
                        if (num > 0)
                        {
                            this.BuyFood(num);
                        }
                    }
                    else if (this.Fund > this.AbundantFund * 2 && !this.IsFoodAbundant)
                    {
                        num = this.Fund - this.AbundantFund * 2;
                        if (num > 0)
                        {
                            this.BuyFood(num);
                        }
                    }
                    else if (this.Fund >= this.FundCeiling / 2 && this.Food < FoodCeiling / 2)
                    {
                        num = this.Fund - this.FundCeiling / 2;
                        if (num > 0)
                        {
                            this.BuyFood(num);
                        }
                    }
                    /*if ((((this.PlanArchitecture == null) && (this.PlanFacilityKind == null)) && (this.BelongedFaction.PlanTechniqueArchitecture != this)) && (((this.Fund >= (this.FundCeiling / 2)) || !this.IsFoodEnough) && this.IsFundAbundant))
                    {
                        num = this.Fund - this.AbundantFund;
                        if (num > 0)
                        {
                            this.BuyFood(num / 2);
                        }
                    }*/
                }
                
                /*else if ((this.SellFoodAvail() && ((this.PlanArchitecture == null) && (this.TransferFoodArchitecture == null))) && (((!this.HostileLine && (this.Fund < (this.FundCeiling / 2))) && !this.IsFoodEnough) && this.IsFoodAbundant))
                {
                    num = this.Food - this.AbundantFood;
                    if (num > 0)
                    {
                        this.SellFood(num / 10);
                    }
                }*/
            }
        }

        private void AITreasure()
        {
            if (GameObject.Chance(Session.Parameters.AITreasureChance) && !Session.Current.Scenario.IsPlayer(this.BelongedFaction))
            {
                /*if (this.HasTreasureToConfiscate())
                {
                    foreach (Person person in this.Persons.GetList())
                    {
                        if (((person != this.BelongedFaction.Leader) && (person.TreasureCount > 0)) && ((person.TreasureCount > Session.Parameters.AITreasureCountMax) ||
                            ((((person.PersonalTitle == null) && GameObject.Chance(50)) || (((person.PersonalTitle != null) && (person.PersonalTitle.Level * Session.Parameters.AITreasureCountCappedTitleLevelMultiply + Session.Parameters.AITreasureCountCappedTitleLevelAdd <= person.TreasureCount)) && GameObject.Chance(25)))
                            && ((person.CombatTitle == null) || (((person.CombatTitle != null) && (person.CombatTitle.Level * Session.Parameters.AITreasureCountCappedTitleLevelMultiply + Session.Parameters.AITreasureCountCappedTitleLevelAdd <= person.TreasureCount)) && GameObject.Chance(50))))))
                        {
                            foreach (Treasure treasure in person.Treasures.GetRandomList())
                            {
                                person.ConfiscatedTreasure(treasure);
                                this.BelongedFaction.Leader.ReceiveTreasure(treasure);
                                break;
                            }
                        }
                    }
                }*/
                if (((this.BelongedFaction.Leader != null) && (this.BelongedFaction.Leader.TreasureCount > Session.Parameters.AITreasureCountMax)) && this.HasTreasureToAward())
                {
                    GameObjectList list = this.Persons.GetList();
                    list.PropertyName = "FightingForce";
                    list.IsNumber = true;
                    list.ReSort();
                    foreach (Person person in list)
                    {
                        if ((person == this.BelongedFaction.Leader) || (person.TreasureCount != 0))
                        {
                            continue;
                        }
                        if (person.TreasureCount < person.TotalTitleLevel * Session.Parameters.AITreasureCountCappedTitleLevelMultiply + Session.Parameters.AITreasureCountCappedTitleLevelAdd)
                        {
                            foreach (Treasure treasure in this.BelongedFaction.Leader.Treasures.GetRandomList())
                            {
                                if (treasure.Worth < Session.Parameters.AIGiveTreasureMaxWorth)
                                {
                                    this.BelongedFaction.Leader.LoseTreasure(treasure);
                                    person.AwardedTreasure(treasure);
                                    break;
                                }
                            }
                            return;
                        }
                    }
                }
            }
        }
        /*
        private void AIWork()
        {
            this.AIAutoHire();
            this.StopAllWork();
            if (((this.PlanArchitecture == null) || GameObject.Chance(10)) && this.HasPerson())
            {
                int num;
                this.ReSortAllWeighingList();
                bool isFundAbundant = this.IsFundAbundant;
                if (this.Fund < ((100 * this.AreaCount) + ((30 - Session.Current.Scenario.Date.Day) * this.FacilityMaintenanceCost)))
                {
                    MilitaryList trainingMilitaryList = this.GetTrainingMilitaryList();
                    if (trainingMilitaryList.Count > 0)
                    {
                        trainingMilitaryList.IsNumber = true;
                        trainingMilitaryList.PropertyName = "Weighing";
                        trainingMilitaryList.ReSort();
                        GameObjectList maxObjects = this.trainingPersons.GetMaxObjects(trainingMilitaryList.Count);
                        for (num = 0; num < maxObjects.Count; num++)
                        {
                            this.AddPersonToTrainingWork(maxObjects[num] as Person, trainingMilitaryList[num] as Military);
                        }
                    }
                    int num2 = 0;
                    if ((GameObject.Chance(50) && this.Kind.HasDomination) && (this.Domination < (this.DominationCeiling * 0.8)))
                    {
                        num2++;
                    }
                    if ((GameObject.Chance(50) && this.Kind.HasEndurance) && (this.Endurance < (this.EnduranceCeiling * 0.2f)))
                    {
                        num2++;
                    }
                    if ((GameObject.Chance(50) && this.Kind.HasMorale) && (this.Morale < Session.Parameters.RecruitmentMorale))
                    {
                        num2++;
                    }
                    if (num2 > 0)
                    {
                        for (num = 0; num < (this.Persons.Count - trainingMilitaryList.Count); num += num2)
                        {
                            foreach (Person person in this.dominationPersons)
                            {
                                if (person.WorkKind == ArchitectureWorkKind.无)
                                {
                                    this.AddPersonToDominationWorkingList(person);
                                    break;
                                }
                            }
                            foreach (Person person in this.endurancePersons)
                            {
                                if (person.WorkKind == ArchitectureWorkKind.无)
                                {
                                    this.AddPersonToEnduranceWorkingList(person);
                                    break;
                                }
                            }
                            foreach (Person person in this.moralePersons)
                            {
                                if (person.WorkKind == ArchitectureWorkKind.无)
                                {
                                    this.AddPersonToMoraleWorkingList(person);
                                    break;
                                }
                            }
                        }
                    }
                }
                else if ((GameObject.Chance(20) || !this.HasBuildingRouteway) || this.IsFundEnough)
                {
                    float num3;
                    bool flag2 = this.RecentlyAttacked > 0;
                    WorkRateList list3 = new WorkRateList();
                    if ((flag2 || (this.BelongedFaction.PlanTechniqueArchitecture != this)) || GameObject.Chance(20))
                    {
                        if (!flag2 || !GameObject.Chance(80))
                        {
                            if (this.Kind.HasAgriculture && (this.Agriculture < this.AgricultureCeiling))
                            {
                                if (this.BelongedSection.AIDetail.ValueAgriculture)
                                {
                                    list3.AddWorkRate(new WorkRate((((float) this.Agriculture) / 4f) / ((float) this.AgricultureCeiling), ArchitectureWorkKind.农业));
                                }
                                else
                                {
                                    list3.AddWorkRate(new WorkRate(((float) this.Agriculture) / ((float) this.AgricultureCeiling), ArchitectureWorkKind.农业));
                                }
                            }
                            if (this.Kind.HasCommerce && (this.Commerce < this.CommerceCeiling))
                            {
                                if (this.BelongedSection.AIDetail.ValueCommerce)
                                {
                                    list3.AddWorkRate(new WorkRate((((float) this.Commerce) / 4f) / ((float) this.CommerceCeiling), ArchitectureWorkKind.商业));
                                }
                                else
                                {
                                    list3.AddWorkRate(new WorkRate(((float) this.Commerce) / ((float) this.CommerceCeiling), ArchitectureWorkKind.商业));
                                }
                            }
                            if (this.Kind.HasTechnology && (this.Technology < this.TechnologyCeiling))
                            {
                                if (this.BelongedSection.AIDetail.ValueTechnology || (GameObject.Chance(50) && (this.IsStateAdmin || this.IsRegionCore)))
                                {
                                    list3.AddWorkRate(new WorkRate((((float) this.Technology) / 4f) / ((float) this.TechnologyCeiling), ArchitectureWorkKind.技术));
                                }
                                else
                                {
                                    list3.AddWorkRate(new WorkRate(((float) this.Technology) / ((float) this.TechnologyCeiling), ArchitectureWorkKind.技术));
                                }
                            }
                        }
                        if (this.Kind.HasDomination && (this.Domination < this.DominationCeiling))
                        {
                            if (this.BelongedSection.AIDetail.ValueDomination)
                            {
                                list3.AddWorkRate(new WorkRate(((((float) this.Domination) / 5f) / 4f) / ((float) this.DominationCeiling), ArchitectureWorkKind.统治));
                            }
                            else
                            {
                                list3.AddWorkRate(new WorkRate((((float) this.Domination) / 5f) / ((float) this.DominationCeiling), ArchitectureWorkKind.统治));
                            }
                        }
                        if (this.Kind.HasMorale && (this.Morale < this.MoraleCeiling))
                        {
                            if (this.BelongedSection.AIDetail.ValueMorale)
                            {
                                list3.AddWorkRate(new WorkRate((((float) this.Morale) / 4f) / ((float) this.MoraleCeiling), ArchitectureWorkKind.民心));
                            }
                            else
                            {
                                list3.AddWorkRate(new WorkRate(((float) this.Morale) / ((float) this.MoraleCeiling), ArchitectureWorkKind.民心));
                            }
                        }
                        if (this.Kind.HasEndurance && (this.Endurance < this.EnduranceCeiling))
                        {
                            if (this.BelongedSection.AIDetail.ValueEndurance)
                            {
                                list3.AddWorkRate(new WorkRate((((float) this.Endurance) / 4f) / ((float) this.EnduranceCeiling), ArchitectureWorkKind.耐久));
                            }
                            else
                            {
                                list3.AddWorkRate(new WorkRate(((float) this.Endurance) / ((float) this.EnduranceCeiling), ArchitectureWorkKind.耐久));
                            }
                        }
                    }
                    MilitaryList list4 = this.GetTrainingMilitaryList();
                    if (list4.Count > 0)
                    {
                        if (flag2)
                        {
                            list3.AddWorkRate(new WorkRate(0f, ArchitectureWorkKind.训练));
                        }
                        else
                        {
                            num3 = 0f;
                            foreach (Military military in list4)
                            {
                                num3 += ((float) military.TrainingWeighing) / ((float) military.MaxTrainingWeighing);
                            }
                            num3 /= (float) list4.Count;
                            if (this.BelongedSection.AIDetail.ValueTraining)
                            {
                                list3.AddWorkRate(new WorkRate(num3 / 4f, ArchitectureWorkKind.训练));
                            }
                            else
                            {
                                list3.AddWorkRate(new WorkRate(num3, ArchitectureWorkKind.训练));
                            }
                        }
                    }
                    MilitaryList recruitmentMilitaryList = null;
                    if (((flag2 || (this.BelongedFaction.PlanTechniqueArchitecture != this)) && this.Kind.HasPopulation) && ((flag2 || (GameObject.Random(GameObject.Square(((int) this.BelongedFaction.Leader.StrategyTendency) + 1)) == 0)) && this.RecruitmentAvail()))
                    {
                        recruitmentMilitaryList = this.GetRecruitmentMilitaryList();
                        if ((this.ArmyScale < this.FewArmyScale) && flag2)
                        {
                            list3.AddWorkRate(new WorkRate(0f, ArchitectureWorkKind.补充));
                        }
                        else if (((this.ArmyScale < this.FewArmyScale) && ((this.BelongedSection.AIDetail.ValueRecruitment && GameObject.Chance(20)) || GameObject.Chance(5))) && this.IsFoodAbundant)
                        {
                            list3.AddWorkRate(new WorkRate(0f, ArchitectureWorkKind.补充));
                        }
                        else if ((((GameObject.Chance(1) || (this.BelongedSection.AIDetail.ValueRecruitment && GameObject.Chance(5))) && ((this.ArmyScale >= this.LargeArmyScale) && this.IsFoodAbundant)) || ((((this.ArmyScale < this.LargeArmyScale) && this.IsFoodEnough) && (((this.IsImportant || (this.AreaCount > 2)) && (this.Population > this.Kind.PopulationBoundary)) || (((this.AreaCount <= 2) && !this.IsImportant) && (this.Population > (this.RecruitmentPopulationBoundary / 2))))) && ((this.BelongedSection.AIDetail.ValueRecruitment && GameObject.Chance(60)) || GameObject.Chance(15)))) && (GameObject.Random(Enum.GetNames(typeof(PersonStrategyTendency)).Length) >=(int) this.BelongedFaction.Leader.StrategyTendency))
                        {
                            num3 = 0f;
                            foreach (Military military in recruitmentMilitaryList)
                            {
                                num3 += ((float) military.RecruitmentWeighing) / ((float) military.MaxRecruitmentWeighing);
                            }
                            num3 /= (float) recruitmentMilitaryList.Count;
                            if (this.BelongedSection.AIDetail.ValueRecruitment)
                            {
                                list3.AddWorkRate(new WorkRate(num3 / 4f, ArchitectureWorkKind.补充));
                            }
                            else
                            {
                                list3.AddWorkRate(new WorkRate(num3, ArchitectureWorkKind.补充));
                            }
                        }
                    }
                    if (list3.Count > 0)
                    {
                        for (num = 0; num < this.Persons.Count; num += list3.Count)
                        {
                            foreach (WorkRate rate in list3.RateList)
                            {
                                switch (rate.workKind)
                                {
                                    case ArchitectureWorkKind.农业:
                                        foreach (Person person in this.agriculturePersons)
                                        {
                                            if ((person.WorkKind == ArchitectureWorkKind.无) && (isFundAbundant || (person.AgricultureAbility >= (120 + (this.AreaCount * 5)))))
                                            {
                                                this.AddPersonToAgricultureWorkingList(person);
                                                break;
                                            }
                                        }
                                        break;

                                    case ArchitectureWorkKind.商业:
                                        foreach (Person person in this.commercePersons)
                                        {
                                            if ((person.WorkKind == ArchitectureWorkKind.无) && (isFundAbundant || (person.CommerceAbility >= (120 + (this.AreaCount * 5)))))
                                            {
                                                this.AddPersonToCommerceWorkingList(person);
                                                break;
                                            }
                                        }
                                        break;

                                    case ArchitectureWorkKind.技术:
                                        foreach (Person person in this.technologyPersons)
                                        {
                                            if ((person.WorkKind == ArchitectureWorkKind.无) && (isFundAbundant || (person.TechnologyAbility >= (120 + (this.AreaCount * 5)))))
                                            {
                                                this.AddPersonToTechnologyWorkingList(person);
                                                break;
                                            }
                                        }
                                        break;

                                    case ArchitectureWorkKind.统治:
                                        foreach (Person person in this.dominationPersons)
                                        {
                                            if ((person.WorkKind == ArchitectureWorkKind.无) && (isFundAbundant || (person.DominationAbility >= (120 + (this.AreaCount * 5)))))
                                            {
                                                this.AddPersonToDominationWorkingList(person);
                                                break;
                                            }
                                        }
                                        break;

                                    case ArchitectureWorkKind.民心:
                                        foreach (Person person in this.moralePersons)
                                        {
                                            if ((person.WorkKind == ArchitectureWorkKind.无) && (isFundAbundant || (person.MoraleAbility >= (120 + (this.AreaCount * 5)))))
                                            {
                                                this.AddPersonToMoraleWorkingList(person);
                                                break;
                                            }
                                        }
                                        break;

                                    case ArchitectureWorkKind.耐久:
                                        foreach (Person person in this.endurancePersons)
                                        {
                                            if ((person.WorkKind == ArchitectureWorkKind.无) && (isFundAbundant || (person.EnduranceAbility >= (120 + (this.AreaCount * 5)))))
                                            {
                                                this.AddPersonToEnduranceWorkingList(person);
                                                break;
                                            }
                                        }
                                        break;

                                    case ArchitectureWorkKind.训练:
                                        foreach (Person person in this.trainingPersons)
                                        {
                                            if (person.WorkKind == ArchitectureWorkKind.无)
                                            {
                                                foreach (Military military in list4.GetRandomList())
                                                {
                                                    if (military.RecruitmentPersonID < 0)
                                                    {
                                                        this.AddPersonToTrainingWork(person, military);
                                                        break;
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        break;

                                    case ArchitectureWorkKind.补充:
                                        foreach (Person person in this.recruitmentPersons)
                                        {
                                            if ((person.WorkKind == ArchitectureWorkKind.无) && (isFundAbundant || (person.RecruitmentAbility >= 120)))
                                            {
                                                if (recruitmentMilitaryList != null)
                                                {
                                                    foreach (Military military in recruitmentMilitaryList.GetRandomList())
                                                    {
                                                        if (military.TrainingPersonID < 0)
                                                        {
                                                            this.AddPersonToRecuitmentWork(person, military);
                                                            break;
                                                        }
                                                    }
                                                }
                                                break;
                                            }
                                        }
                                        break;
                                }
                            }
                        }
                    }
                    if (this.Kind.HasPopulation && (flag2 || (this.BelongedFaction.PlanTechniqueArchitecture != this)))
                    {
                        if (((!flag2 && !this.BelongedSection.AIDetail.ValueNewMilitary) && !GameObject.Chance(10)) || (this.ArmyScale >= this.LargeArmyScale))
                        {
                            MilitaryList list6 = new MilitaryList();
                            foreach (Military military in this.Militaries)
                            {
                                if ((((military.Scales < 15) && (military.Quantity > 0)) && (military.Morale < (military.MoraleCeiling / 2))) && ((military.Kind.PointsPerSoldier <= 1) && (this.BelongedFaction.TechniquePoint > (military.Quantity * (military.Kind.PointsPerSoldier + 1)))))
                                {
                                    list6.Add(military);
                                }
                            }
                            foreach (Military military in list6)
                            {
                                this.DisbandMilitary(military);
                            }
                        }
                        else if (((this.Population > this.RecruitmentPopulationBoundary) || flag2) || ((this.Population >= 10000) && GameObject.Chance(5)))
                        {
                            bool flag3 = true;
                            foreach (Military military in this.Militaries)
                            {
                                if (military.Scales < 15)
                                {
                                    flag3 = false;
                                    break;
                                }
                            }
                            if (flag3)
                            {
                                this.AIRecruitment(false);
                            }
                            else if ((this.ValueWater && !this.HasShuijun()) && this.HasShuijunMilitaryKind())
                            {
                                this.AIRecruitment(true);
                            }
                        }
                    }
                }
            }
        }

        */

        public void AllEnter()
        {
            foreach (Point point in this.GetAllContactArea().Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if (((troopByPosition != null) && (troopByPosition.BelongedFaction == this.BelongedFaction)) && troopByPosition.CanMoveAndEnterAnyway())
                {
                    troopByPosition.Enter(this);
                }
            }
        }

        public bool AllEnterAvail()
        {
            foreach (Point point in this.GetAllContactArea().Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if (((troopByPosition != null) && (troopByPosition.BelongedFaction == this.BelongedFaction)) && troopByPosition.CanMoveAndEnterAnyway())
                {
                    return true;
                }
            }
            return false;
        }

        public void PurifyFactionInfluences()
        {
            if (this.BelongedFaction != null)
            {
                foreach (Technique t in this.BelongedFaction.AvailableTechniques.Techniques.Values)
                {
                    foreach (Influences.Influence i in t.Influences.Influences.Values)
                    {
                        i.PurifyInfluence(this, Applier.Technique, t.ID);
                    }
                }
            }
        }

        public void ApplyFactionInfluences()
        {
            if (this.BelongedFaction != null)
            {
                foreach (Technique t in this.BelongedFaction.AvailableTechniques.Techniques.Values)
                {
                    foreach (Influences.Influence i in t.Influences.Influences.Values)
                    {
                        i.ApplyInfluence(this, Applier.Technique, t.ID);
                    }
                }
            }
        }

        public void ApplyInfluences()
        {
            this.Characteristics.ApplyInfluence(this, Applier.Characteristics, 0);
            this.ApplyFacilityInfluences(false);
        }

        public void ApplyFacilityInfluences(bool skipNoCostFacility)
        {
            foreach (Facility facility in this.Facilities)
            {
                if (!skipNoCostFacility || facility.MaintenanceCost > 0)
                {
                    if (this.FacilityEnabled || facility.MaintenanceCost <= 0)
                    {
                        facility.Influences.ApplyInfluence(this, Applier.Facility, facility.ID);
                    }
                }
            }
        }

        public int GetFacilityKindCount(int id)
        {
            int cnt = 0;
            foreach (Facility facility in this.Facilities)
            {
                if (facility.KindID == id || this.BuildingFacility == id)
                {
                    cnt++;
                }
            }
            return cnt;
        }
        //以下添加20170426
        public int GetFacilityCountForKind(int id)
        {
            int cnt = 0;
            foreach (Facility facility in this.Facilities)
            {
                if (facility.KindID == id)
                {
                    cnt++;
                }
            }
            return cnt;
        }
        public string GetFacilityNameForKind(int id)
        {
            string N = "";
            foreach (Facility facility in this.Facilities)
            {
                if (facility.KindID == id)
                {
                    N = facility.Name;
                }
            }
            return N;
        }
        public string GetFacilityDescriptionForKind(int id)
        {
            string D = "";
            foreach (Facility facility in this.Facilities)
            {
                if (facility.KindID == id)
                {
                    D = facility.Description;
                }
            }
            return D;
        }
        public int GetFacilityPositionOccupiedForKind(int id)
        {
            int D = -1;
            foreach (Facility facility in this.Facilities)
            {
                if (facility.KindID == id)
                {
                    D = facility.PositionOccupied;
                }
            }
            return D;
        }
        public int GetFacilityMaintenanceCostForKind(int id)
        {
            int D = -1;
            foreach (Facility facility in this.Facilities)
            {
                if (facility.KindID == id)
                {
                    D = facility.MaintenanceCost;
                }
            }
            return D;
        }
        public bool HasTheFacilityForKind(int id)
        {
            bool H = false;
            foreach (Facility facility in this.Facilities)
            {
                if (facility.KindID == id)
                {
                    H = true;
                }
            }
            return H;
        }
        //
        public string ArmyQuantityInInformationLevel(InformationLevel level)
        {
            switch (level)
            {
                case InformationLevel.未知:
                    return "----";

                case InformationLevel.无:
                    return "----";

                case InformationLevel.低:
                    return StaticMethods.GetNumberStringByGranularity(this.ArmyQuantity, 0x2710);

                case InformationLevel.中:
                    return StaticMethods.GetNumberStringByGranularity(this.ArmyQuantity, 0x1388);

                case InformationLevel.高:
                    return StaticMethods.GetNumberStringByGranularity(this.ArmyQuantity, 0x3e8);

                case InformationLevel.全:
                    return this.ArmyQuantity.ToString();
            }
            return "----";
        }

        private void AutoDecrement()
        {
            /*if (!(((this.BelongedFaction == null) || (this.RecentlyAttacked <= 0)) || this.DayAvoidInternalDecrementOnBattle))
            {
                int maxValue = (this.RecentlyAttacked / 2) + 1;
                this.DecreaseAgriculture(GameObject.Random(maxValue));
                this.DecreaseCommerce(GameObject.Random(maxValue));
                this.DecreaseTechnology(GameObject.Random(maxValue));
                this.DecreaseMorale(GameObject.Random(maxValue));
            }*/
        }

        public bool AutoHiringAvail()
        {
            return ((this.BelongedSection.AIDetail == null) || !this.BelongedSection.AIDetail.AutoRun);
        }

        private void AutoIncrement()
        {
            if (this.IncrementOfAgriculturePerDay > 0)
            {
                this.IncreaseAgriculture(this.IncrementOfAgriculturePerDay);
            }
            if (this.IncrementOfCommercePerDay > 0)
            {
                this.IncreaseCommerce(this.IncrementOfCommercePerDay);
            }
            if (this.IncrementOfTechnologyPerDay > 0)
            {
                this.IncreaseTechnology(this.IncrementOfTechnologyPerDay);
            }
            if (this.IncrementOfDominationPerDay > 0)
            {
                this.IncreaseDomination(this.IncrementOfDominationPerDay);
            }
            if (this.IncrementOfMoralePerDay > 0)
            {
                this.IncreaseMorale(this.IncrementOfMoralePerDay);
            }
            if ((this.IncrementOfEndurancePerDay > 0) && !((this.Endurance <= 0) && this.HasContactHostileTroop(this.BelongedFaction)))
            {
                this.IncreaseEndurance(this.IncrementOfEndurancePerDay);
            }
            if ((this.IncrementOfFactionReputationPerDay > 0) && (this.BelongedFaction != null))
            {
                this.BelongedFaction.IncreaseReputation(this.IncrementOfFactionReputationPerDay);
            }
            if ((this.IncrementOfFactionTechniquePointPerDay > 0) && (this.BelongedFaction != null))
            {
                this.BelongedFaction.IncreaseTechniquePoint(this.IncrementOfFactionTechniquePointPerDay);
            }
            if ((this.Technology > 0) && (this.BelongedFaction != null))
            {
                this.BelongedFaction.IncreaseTechniquePoint(this.Technology / 5);
            }
            if ((this.BelongedFaction == null) && ((this.RecentlyAttacked <= 0) && !this.HasHostileTroopsInArchitecture()))
            {
                if (this.Endurance < (50 * this.AreaCount))
                {
                    this.Endurance++;
                }
                if (this.Domination < 30)
                {
                    this.Domination++;
                }
            }
        }

        public bool AutoRewardingAvail()
        {
            return ((this.BelongedSection.AIDetail == null) || !this.BelongedSection.AIDetail.AutoRun);
        }

        public bool AutoSearchingAvail()
        {
            return ((this.BelongedSection.AIDetail == null) || !this.BelongedSection.AIDetail.AutoRun);
        }

        /*
        public bool AutoZhaoXianAvail()
        {
            if (Session.Current.Scenario.IsPlayer(this.BelongedFaction) && !this.BelongedSection.AIDetail.AutoRun)
            {
                return true;
            }
            return false;
        }
        */
        public bool AutoWorkingAvail()
        {
            return ((this.BelongedSection.AIDetail == null) || !this.BelongedSection.AIDetail.AutoRun);
        }

        public bool AutoRecruitingAvail()
        {
            return ((this.BelongedSection.AIDetail == null) || !this.BelongedSection.AIDetail.AutoRun);
        }

        public void BeginToBuildAFacility(FacilityKind facilityKind)
        {
            this.BuildingFacility = facilityKind.ID;
            this.BuildingDaysLeft = Math.Max(1, (int)(facilityKind.Days * (1 - this.facilityConstructionTimeRateDecrease)));
            this.DecreaseFund(facilityKind.FundCost);
            if (this.BelongedFaction.TechniquePoint < facilityKind.PointCost)
            {
                this.BelongedFaction.DepositTechniquePointForFacility(facilityKind.PointCost - this.BelongedFaction.TechniquePoint);
                if (this.BelongedFaction.TechniquePoint < facilityKind.PointCost)
                {
                    this.BelongedFaction.DepositTechniquePointForTechnique(facilityKind.PointCost - this.BelongedFaction.TechniquePoint);
                }
            }
            this.BelongedFaction.DecreaseTechniquePoint(facilityKind.PointCost);
            /*
            if (this.HasSpy)
            {
                this.CreateNewFacilitySpyMessage(facilityKind);
            }
             */
            this.PlanFacilityKind = null;
            this.PlanFacilityKindID = -1;
            ExtensionInterface.call("StartBuildFacility", new Object[] { Session.Current.Scenario, this, facilityKind });
        }

        public void StopBuildingFacility()
        {
            FacilityKind fac = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(this.BuildingFacility);
            this.IncreaseFund((int)(fac.FundCost * 0.5 * this.BuildingDaysLeft / fac.Days));
            this.BuildingFacility = -1;
            this.BuildingDaysLeft = 0;
        }

        public void BuildFacility(FacilityKind facilityKind)
        {
            Facility facility = new Facility();
            facility.ID = Session.Current.Scenario.Facilities.GetFreeGameObjectID();
            facility.KindID = facilityKind.ID;
            facility.Endurance = facilityKind.Endurance;
            this.Facilities.AddFacility(facility);
            Session.Current.Scenario.Facilities.AddFacility(facility);
            if (this.FacilityEnabled)
            {
                facility.Influences.ApplyInfluence(this, Applier.Facility, facility.ID);
            }
            if (this.OnFacilityCompleted != null)
            {
                this.OnFacilityCompleted(this, facility);
            }
            ExtensionInterface.call("BuiltFacility", new Object[] { Session.Current.Scenario, this, facilityKind });
        }

        public bool BuildFacilityAvail()
        {
            return ((this.BuildingFacility < 0) && (this.GetBuildableFacilityKindList().Count > 0));
        }

        public bool StopBuildingFacilityAvail()
        {
            return ((this.BuildingFacility >= 0));
        }

        private struct CreateTroopInfo
        {
            public GameObjectList candidates;
            public Person leader;
            public Military military;
            public Point position;
        }

        private bool BuildOffensiveTroop(Architecture destination, LinkKind linkkind, bool offensive, int reserve)
        {
            Troop troop;
            if (linkkind == LinkKind.None)
            {
                return false;
            }

            SortedBoundedSet<Troop> list = new SortedBoundedSet<Troop>(Session.Parameters.MaxAITroopCountCandidates, new SimulatingFightingForceComparer());

            this.Persons.ClearSelected();

            GameObjectList mList = this.Militaries.GetList();
            mList.PropertyName = "Merit";
            mList.IsNumber = true;
            mList.SmallToBig = false;
            mList.ReSort();

            //Label_0309:
            foreach (Military military in mList)
            {
                if (military.Scales < military.RetreatScale * 1.5) continue;
                if (!military.Kind.Movable) continue;
                if (military.IsTransport) continue; //never deal with transports in this function

                bool movable = false;
                foreach (Point p in this.ArchitectureArea.GetContactArea(false).Area)
                {
                    if (this.ArchitectureArea.Area.Contains(p)) continue;
                    if (military.Kind.IsMovableOnPosition(p))
                    {
                        movable = true;
                    }
                }
                if (!movable) continue;

                switch (linkkind)
                {
                    case LinkKind.Land:
                        {
                            if (military.Kind.Type != MilitaryType.水军)
                            {
                                break;
                            }
                            continue;
                        }
                    case LinkKind.Water:
                        {
                            //if ((military.Kind.Type == MilitaryType.水军) || (this.ValueWater && (!offensive || ((military.Quantity >= 0x1f40) && (GameObject.Random(military.Kind.Merit) <= 0)))))
                            if (Session.GlobalVariables.LandArmyCanGoDownWater)
                            {
                                if (!offensive || (military.KindID != 28 && !military.IsTransport))
                                {
                                    break;
                                }
                            }
                            else
                            {
                                if (military.Kind.Type == MilitaryType.水军)
                                {
                                    break;
                                }
                            }
                            continue;
                        }
                }
                if ((((military.Scales > 5) && (military.Morale >= 80)) && (military.Combativity >= 80)) && (military.InjuryQuantity < military.Kind.MinScale)
                    && (!offensive ||
                    (military.Merit > 0)
                    )) //do not use transport teams to attack
                {
                    TroopList candidates = this.AISelectPersonIntoTroop(this, military, true);
                    foreach (Troop t in candidates)
                    {
                        if (t.FightingForce < 10000 && offensive)
                        {
                            t.Destroy(true, false);
                            continue;
                        }
                        Troop removed;
                        list.Add(t, out removed);
                        if (removed != null)
                        {
                            removed.Destroy(true, false);
                        }
                    }
                }
            }

            List<CreateTroopInfo> willCreate = new List<CreateTroopInfo>();
            HashSet<Person> selectedPersons = new HashSet<Person>();
            HashSet<Military> selectedMilitaries = new HashSet<Military>();
            HashSet<Point> takenPosition = new HashSet<Point>();
            bool ranOutOfArea = false;

            foreach (Troop troop2 in list)
            {
                if (this.ArmyScale < reserve) break;
                bool personAlreadyOut = false;
                foreach (Person p in troop2.Candidates)
                {
                    if (selectedPersons.Contains(p))
                    {
                        personAlreadyOut = true;
                        break;
                    }
                }
                if (personAlreadyOut) continue;
                if (selectedMilitaries.Contains(troop2.Army)) continue;

                GameArea allAvailableArea = this.GetAllAvailableArea(false);
                GameArea sourceArea = new GameArea();
                foreach (Point point in allAvailableArea.Area)
                {
                    if (!takenPosition.Contains(point) && troop2.IsMovableOnPosition(point))
                    {
                        sourceArea.Area.Add(point);
                    }
                }
                if (sourceArea.Count == 0)
                {
                    ranOutOfArea = true;
                    break;
                }
                Point position = sourceArea[GameObject.Random(sourceArea.Count)];

                Person leader = troop2.Candidates[0] as Person;
                leader.Selected = true;
                PersonList candidates = this.SelectSubOfficersToTroop(troop2);

                CreateTroopInfo info = new CreateTroopInfo();
                info.candidates = candidates;
                info.leader = leader;
                info.military = troop2.Army;
                info.position = position;
                willCreate.Add(info);

                foreach (Person p in candidates)
                {
                    selectedPersons.Add(p);
                }
                selectedMilitaries.Add(troop2.Army);
                takenPosition.Add(position);
            }

            bool hasCreatedTroop = false;
            if (willCreate.Count > 0)
            {
                int willCreateScale = 0;
                int destScale = 0;
                if (!ranOutOfArea && willCreate.Count < 4)
                {
                    foreach (CreateTroopInfo info in willCreate)
                    {
                        willCreateScale += info.military.FightingForce;
                    }
                    GameObjectList destMilitary = destination.Militaries.GetList();
                    destMilitary.PropertyName = "FightingForce";
                    destMilitary.IsNumber = true;
                    destMilitary.SmallToBig = false;
                    destMilitary.ReSort();
                    int cnt = 0;
                    foreach (Military m in destMilitary)
                    {
                        if (cnt >= destination.Persons.Count) break;
                        destScale += m.FightingForce;
                    }
                }

                if (willCreateScale >= destScale || ranOutOfArea || willCreate.Count >= 4)
                {
                    foreach (CreateTroopInfo info in willCreate)
                    {
                        troop = this.CreateTroop(info.candidates, info.leader, info.military, -1, info.position);
                        troop.WillArchitecture = destination;
                        Legion legion = this.BelongedFaction.GetLegion(destination);
                        if (legion == null)
                        {
                            legion = this.CreateOffensiveLegion(destination);
                        }
                        legion.AddTroop(troop);
                        //this.PostCreateTroop(troop, false);
                        hasCreatedTroop = true;
                    }
                }

            }
            foreach (Troop t in list)
            {
                t.Destroy(true, false);
            }
            return hasCreatedTroop;
        }

        public bool IsSelfFoodEnoughForOffensive(LinkNode node, Routeway routeway)
        {
            MilitaryList cropConsumptionOrderedList = Militaries;
            cropConsumptionOrderedList.PropertyName = "FoodCostPerDay";
            cropConsumptionOrderedList.IsNumber = true;
            cropConsumptionOrderedList.ReSort();
            PersonList leaderablePersonList = new PersonList();
            foreach (Person p in this.Persons)
            {
                if (p.Command >= 40)
                {
                    leaderablePersonList.Add(p);
                }
            }
            double consumptionRate = Session.Current.Scenario.GetDistance(this.ArchitectureArea, node.A.ArchitectureArea) / 50.0 + 1;
            switch (node.Kind)
            {
                case LinkKind.None:
                    return false;

                case LinkKind.Land:
                    {
                        int crop = 0;
                        int troopCnt = 0;
                        foreach (Military m in cropConsumptionOrderedList)
                        {
                            if ((((m.Scales >= 3) && (m.Morale >= 80)) && (m.Combativity >= 80)) && (m.InjuryQuantity < m.Kind.MinScale) && m.Kind.Type != MilitaryType.水军)
                            {
                                crop += m.FoodCostPerDay;
                                troopCnt++;
                                if (troopCnt >= leaderablePersonList.Count) break;
                            }
                        }
                        return (this.Food >= crop * consumptionRate * 1.1);
                    }

                case LinkKind.Water:
                    {
                        int crop = 0;
                        int troopCnt = 0;
                        foreach (Military m in cropConsumptionOrderedList)
                        {
                            if ((((m.Scales >= 3) && (m.Morale >= 80)) && (m.Combativity >= 80)) && (m.InjuryQuantity < m.Kind.MinScale) && m.Kind.Type == MilitaryType.水军)
                            {
                                crop += m.FoodCostPerDay;
                                troopCnt++;
                                if (troopCnt >= leaderablePersonList.Count) break;
                            }
                        }
                        return (this.Food >= crop * consumptionRate * 1.1);
                    }

                case LinkKind.Both:
                    {
                        int crop = 0;
                        int troopCnt = 0;
                        foreach (Military m in cropConsumptionOrderedList)
                        {
                            if ((((m.Scales >= 3) && (m.Morale >= 80)) && (m.Combativity >= 80)) && (m.InjuryQuantity < m.Kind.MinScale))
                            {
                                crop += m.FoodCostPerDay;
                                troopCnt++;
                                if (troopCnt >= leaderablePersonList.Count) break;
                            }
                        }

                        return (this.Food >= crop * consumptionRate * 1.1);
                    }
            }
            return false;
        }

        public Routeway BuildRouteway(LinkNode node, bool hasEnd)
        {
            Point key = new Point(base.ID, node.A.ID);
            if (!this.BelongedFaction.ClosedRouteways.ContainsKey(key))
            {
                Point? nullable;
                Point? nullable2;
                Session.Current.Scenario.GetClosestPointsBetweenTwoAreas(this.GetRoutewayStartPoints(), node.A.GetAIRoutewayEndPoints(this, false), out nullable, out nullable2);
                if (nullable.HasValue && nullable2.HasValue)
                {
                    this.BelongedFaction.RoutewayPathBuilder.MultipleWaterCost = false;
                    this.BelongedFaction.RoutewayPathBuilder.MustUseWater = node.Kind == LinkKind.Water;
                    if (this.BelongedFaction.RoutewayPathAvail(nullable.Value, nullable2.Value, hasEnd))
                    {
                        Routeway routeway = this.CreateRouteway(this.BelongedFaction.GetCurrentRoutewayPath());
                        routeway.DestinationArchitecture = node.A;
                        if (hasEnd)
                        {
                            routeway.EndArchitecture = node.A;
                        }
                        return routeway;
                    }
                    this.BelongedFaction.ClosedRouteways.Add(new Point(base.ID, node.A.ID), null);
                }
            }
            return null;
        }

        public Routeway BuildShortestRouteway(Architecture des, bool noWater)
        {
            Point? nullable;
            Point? nullable2;
            if (!noWater)
            {
                Point key = new Point(base.ID, des.ID);
                if (this.BelongedFaction.ClosedRouteways.ContainsKey(key))
                {
                    return null;
                }
            }
            Session.Current.Scenario.GetClosestPointsBetweenTwoAreas(this.GetRoutewayStartPoints(), des.GetRoutewayStartPoints(), out nullable, out nullable2);
            if (nullable.HasValue && nullable2.HasValue)
            {
                this.BelongedFaction.RoutewayPathBuilder.MultipleWaterCost = noWater;
                if (this.BelongedFaction.RoutewayPathAvail(nullable.Value, nullable2.Value, true))
                {
                    Routeway routeway = this.CreateRouteway(this.BelongedFaction.GetCurrentRoutewayPath());
                    routeway.DestinationArchitecture = des;
                    routeway.EndArchitecture = des;
                    return routeway;
                }
                if (!noWater)
                {
                    this.BelongedFaction.ClosedRouteways.Add(new Point(base.ID, des.ID), null);
                }
            }
            return null;
        }

        public Routeway BuildShortestRouteway(Point point, bool noWater)
        {
            Point closestPoint = Session.Current.Scenario.GetClosestPoint(this.GetRoutewayStartPoints(), point);
            this.BelongedFaction.RoutewayPathBuilder.MultipleWaterCost = noWater;
            if (this.BelongedFaction.RoutewayPathAvail(closestPoint, point, true))
            {
                return this.CreateRouteway(this.BelongedFaction.GetCurrentRoutewayPath());
            }
            return null;
        }

        public void BuyFood(int spendFund)
        {
            this.DecreaseFund(spendFund);
            this.IncreaseFood(spendFund * Session.Parameters.FundToFoodMultiple);
            ExtensionInterface.call("BuyFood", new Object[] { Session.Current.Scenario, this });
        }

        public bool BuyFoodAvail()
        {
            return this.Agriculture >= Session.Parameters.BuyFoodAgriculture && (Session.Current.Scenario.Date.Season == GameSeason.夏 || Session.Current.Scenario.Date.Season == GameSeason.秋) && this.Fund > 0 && this.Food < this.FoodCeiling
                && (Session.Current.Scenario.Date.Month * 483
                + (this.Name.Length > 0 ? this.Name[0] : 735) * 203
                + (this.Name.Length > 1 ? this.Name[1] : 492) * 680
                    + this.ID * 912) % 2 == 0;
        }

        public bool CampaignAvail()
        {
            if ((this.Persons.Count > 0) && (this.Militaries.Count > 0))
            {
                foreach (Military military in this.Militaries)
                {
                    if (((military.Quantity > 0) && (military.Morale > 0)) && (this.GetMilitaryCampaignArea(military).Count > 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool ChangeCapitalAvail()
        {
            return this.BelongedFaction != null && this.BelongedFaction.ArchitectureCount > 1 && this.IsCapital && this.Fund >= this.ChangeCapitalCost && this.BelongedFaction.Leader.Status != PersonStatus.Captive;
        }

        public bool AppointMayorAvail() //任命县令
        {
            if (this.BelongedFaction != null && this.BelongedFaction.Leader.BelongedCaptive == null && this.MayorID == -1  && this.Kind.ID != 4)
            {
                if (Session.Current.Scenario.IsPlayer(this.BelongedFaction) && this.MayorCandicate.Count > 0)
                {
                    return true;
                }

                if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction) && this.AIMayorCandicate.Count > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool RecallMayorAvail() //罢免县令
        {
            if (this.BelongedFaction != null && this.Mayor != null)
            {
                return true;
            }
            return false;
        }

       public void RecallMayor()
       {

           foreach (Person p in this.BelongedFaction .Persons)
           {
               if (p.ID == this.MayorID)
               {
                   this.Mayor = null;
               }
           }
       }

        GameObjectList assassinatablePersons = new GameObjectList();
        Faction assassinatablePersonFaction = null;
        public GameObjectList AssassinatablePersons(Faction f)
        {
            if (assassinatablePersonFaction == f)
            {
                return assassinatablePersons;
            }
            if (this.BelongedFaction == null || this.BelongedFaction.IsFriendly(f))
            {
                assassinatablePersons = this.NoFactionPersons;
                assassinatablePersonFaction = f;
            }
            else
            {
                assassinatablePersons = this.MovablePersons.GetList();
                assassinatablePersons.AddRange(this.NoFactionPersons);
                assassinatablePersonFaction = f;
            }
            return assassinatablePersons;
        }

       public PersonList MovablePersons  //代替原来的Persons列表
       {
           get
           {
               return this.Persons;
           }
       }

        public PersonList MayorCandicate
        {
            get
            {
                PersonList result = new PersonList() ;
                foreach (Person p in this.Persons)
                {
                    if (p != this.BelongedFaction.Leader && this.Mayor != p)
                    {
                        result.Add(p);
                    }
                }

                return result;
            }
        }

        public PersonList AIMayorCandicate
        {
            get
            {
                PersonList result = new PersonList();

                foreach (Person p in this.MayorCandicate)
                {
                    result.Add(p);
                }

                result.PropertyName = "AbilitySum";
                result.IsNumber = true;
                result.SmallToBig = false;
                result.ReSort();
                return result;
            }
        }


        public event Appointmayor OnAppointmayor; //县令年表
        public delegate void Appointmayor(Person p, Person q);
        public void AppointMayor(Person Person)
        {
            Session.Current.Scenario.YearTable.addAppointMayorEntry(Session.Current.Scenario.Date, Person, this.BelongedFaction.Leader);
            if (this.OnAppointmayor != null)
            {
                this.OnAppointmayor(this.BelongedFaction.Leader, Person);
            }
            if (this.Mayor != null )
            {
                //this.Mayor.LocationArchitecture = this;
               // this.IncrementOfEndurancePerDay = 100;

                //this.Mayor.DayRateIncrementOfpublic += 0.7f;  //太守内政加成系数
            }
        }




        public bool SelectPrinceAvail()
        {


            if (this.BelongedFaction != null && this.BelongedFaction.Leader.BelongedCaptive == null &&
                (this.BelongedFaction.PrinceID == -1) &&
                this.Fund >= Session.Parameters.SelectPrinceCost && this.BelongedFaction.Leader.ChildrenCanBeSelectedAsPrince().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public event Selectprince OnSelectprince; //立储
        public delegate void Selectprince(Person p, Person q);
        public void SelectPrince(Person Person)
        {
            Session.Current.Scenario.YearTable.addSelectPrinceEntry(Session.Current.Scenario.Date, Person, this.BelongedFaction.Leader);
            if (this.OnSelectprince != null)
            {
                this.OnSelectprince(this.BelongedFaction.Leader, Person);
            }
        }

        public bool BecomeEmperorLegallyAvail()
        {

            return this.BelongedFaction.BecomeEmperorLegallyAvail();
        }



        public bool CanZhaoXian()
        {
            if (this.BelongedFaction != null && Session.GlobalVariables.ZhaoXianSuccessRate > 0 && Session.Current.Scenario.Date.Month == 3
              && this.BelongedFaction.ZhaoxianFailureCount < 1 && this.BelongedFaction != null && this.BelongedFaction.Leader.Status != PersonStatus.Captive)

            {
                if (this.AvailGeneratorTypeList().Count > 0 )
                {
                    return true;
                }
            }

            return false;
        }


        public PersonGeneratorTypeList AvailGeneratorTypeList()
        {
            PersonGeneratorTypeList list = new PersonGeneratorTypeList();
            foreach (PersonGeneratorType type in Session.Current.Scenario.GameCommonData.AllPersonGeneratorTypes)
            {
                if (this.Fund >= type.CostFund && this.BelongedFaction.GetGeneratorPersonCount(type) < type.FactionLimit)
                {
                    list.Add(type);
                }
            }
            return list;

        }

        private bool IsChanceOfGeneratingOfficer(int factionPersonCount, bool isAI, PersonGeneratorType preferredType)
        {
            float coef = isAI ? Session.Parameters.AIExtraPerson : 1;
            if (coef <= 0)
            {
                return false ;
            }

            float result = GameObject.Random((int)(10000 * Math.Pow(factionPersonCount, Session.Parameters.SearchPersonArchitectureCountPower)));
            float target = Session.GlobalVariables.ZhaoXianSuccessRate * coef * preferredType.generationChance;
            return result < target;
        }

        public void DoZhaoXian(PersonGeneratorType preferredType)
        {

            bool isAI = !Session.Current.Scenario.IsPlayer(this.BelongedFaction);

            if (!IsChanceOfGeneratingOfficer(this.BelongedFaction.PersonCount,isAI,preferredType))
            {
                if (!isAI)
                {
                    Session.MainGame.mainGameScreen.xianshishijiantupian(this.BelongedFaction.Leader, this.Name, "ZhaoXianFailed", "ZhaoXian.jpg", "ZhaoXianFailed", true);
                }
                this.BelongedFaction.ZhaoxianFailureCount++;
                return ;
            }

           // PersonGeneratorType type = new PersonGeneratorType();
            //type.ID = preferredType;
            PersonGenerateParam param = new PersonGenerateParam(this,this.BelongedFaction.Leader, true,preferredType ,isAI);
            Person r = Person.createPerson(param, true);
            if (this.BelongedFaction.IsAlien && r.PersonalLoyalty >= 2)
            {
                r.PersonalLoyalty = GameObject.Random(0, 1);
            }
            r.Ideal = (this.BelongedFaction.Leader.Ideal + GameObject.Random(r.IdealTendency.Offset * 2 + 1) - r.IdealTendency.Offset) % 150;
            this.ZhaoXian(r);
            this.BelongedFaction.YearOfficialLimit++;
            preferredType.TypeCount++;
            this.BelongedFaction.GetGeneratorPersonCount(preferredType);
            this.BelongedFaction.IncrementGeneratorCount(preferredType);
            //this.BelongedFaction.ZongShu[preferredType.ID]++;

            this.DecreaseFund(preferredType.CostFund);

        }

        public void GenerateOfficer(PersonGeneratorType preferredType, bool success) //事件专用
        {
            PersonGenerateParam param = new PersonGenerateParam(this, this.BelongedFaction.Leader, true, preferredType, false);
            Person r = Person.createPerson(param, true);
            //this.ZhaoXian(r);
            this.DecreaseFund(preferredType.CostFund);
        }

        public event Zhaoxian OnZhaoxian; //招贤
        public delegate void Zhaoxian(Person p, Person q);
        public void ZhaoXian(Person person)
        {
            Session.Current.Scenario.YearTable.addZhaoXianEntry(Session.Current.Scenario.Date, person, this.BelongedFaction.Leader);
            if (this.OnZhaoxian != null)
            {
                this.OnZhaoxian(this.BelongedFaction.Leader, person);
            }
        }

        /*
        public int CreatePersonCost
        {
            get
            {
                if (this.BelongedFaction.PersonCount <= 5)
                {
                    return 5000;
                }
                else if (this.BelongedFaction.PersonCount > 5 && this.BelongedFaction.PersonCount <= 10)
                {
                    return 10000;
                }
                else
                {
                    return (this.BelongedFaction.SelfOfficerCount * 1000 + 10000);
                }
            }
        }

        /*
        public bool DengYongAvail()
        {
            if (this.BelongedFaction != null && this.NoFactionOfficerCount > 0)
            {
                foreach (Person person in this.NoFactionOfficers)
                {
                    if (this.Fund > person.UntiredMerit)
                    {
                        return true;
                    }
                }

            }
            return false;
        }


        */

        public bool DismissOfficerAvail()
        {
            if (this.BelongedFaction != null && this.NoFactionOfficerCount > 0)
            {
                return true;
            }
            return false;
        }

        public void DismissOfficer() //遣散野武将
        {

            foreach (Person person in this.NoFactionOfficers)
            {
                person.Alive = false;
                person.Status = PersonStatus.None;
                Session.Current.Scenario.Persons.Remove(person);
                //this.NoFactionOfficers.Remove(person);
            }

        }

        public PersonList NoFactionOfficers //在野野武将列表
        {
            get
            {
                PersonList result = new PersonList();

                foreach (Person p in this.NoFactionPersons)
                {
                    if (p.ID >= 25000)
                    {
                        result.Add(p);
                    }
                }

                return result;
            }
        }

        public int NoFactionOfficerCount
        {
            get
            {
                return (this.NoFactionOfficers.Count);
            }
        }


        public PersonList Kerenmingdeguanyuan
        {
            get
            {
                PersonList list = new PersonList();

                foreach (Person p in this.Persons)
                {
                    if (p != this.BelongedFaction.Leader && p.GetAppointableTitleList().Count > 0)
                    {
                        list.Add(p);
                    }
                }
                return list;
            }
        }



        public bool CanAppoint()
        {
            if (this.BelongedFaction != null && this.Kerenmingdeguanyuan.Count > 0)
            {
                return true;
            }
            return false;
        }

        public PersonList RecallableOfficer
        {
            get
            {
                PersonList list = new PersonList();

                foreach (Person p in this.Persons)
                {
                    if (p != this.BelongedFaction.Leader && p.RecallableTitleList().Count > 0)
                    {
                        list.Add(p);
                    }
                }
                return list;
            }
        }

        public bool RecallOfficerAvail()
        {
            if (this.BelongedFaction != null && this.RecallableOfficer.Count > 0)
            {
                return true;
            }
            return false;
        }

        public bool SelfBecomeEmperorAvail()
        {
            return this.BelongedFaction.SelfBecomeEmperorAvail();
        }

        public void ChangeFaction(Faction faction)
        {
            this.ResetAuto();
            if ((faction != null) && Session.Current.Scenario.IsPlayer(faction))
            {
                this.AutoHiring = true;
                this.AutoRewarding = true;

            }
            if ((faction != null) && (this.BelongedFaction != null))
            {
                this.BelongedFaction.Architectures.Remove(this);
                this.BelongedFaction.RemoveArchitectureKnownData(this);
                if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction))
                {
                    this.ClearRouteways();
                }
                else
                {
                    foreach (Routeway routeway in this.Routeways)
                    {
                        this.BelongedFaction.RemoveRouteway(routeway);
                    }
                }
                foreach (Military military in this.Militaries)
                {
                    this.BelongedFaction.RemoveMilitary(military);
                }
                this.PurifyFactionInfluences();
                this.BelongedFaction = null;
                faction.AddArchitecture(this);
                faction.AddArchitectureKnownData(this);
                foreach (Captive captive in this.Captives.GetList())
                {
                    if (captive.CaptiveFaction == faction)
                    {
                        captive.CaptivePerson.SetBelongedCaptive(null, PersonStatus.Normal);
                    }
                }
                foreach (Military military in this.Militaries)
                {
                    faction.AddMilitary(military);
                }
                foreach (Routeway routeway in this.Routeways)
                {
                    faction.AddRouteway(routeway);
                }
                faction.FirstSection.AddArchitecture(this);
                this.ApplyFactionInfluences();
            }
            if (faction != null)
            {
                //this.jianzhuqizi.qizidezi.Text = faction.ToString().Substring(0, 1);
            }
        }

        private void CheckAmbushTroop(Point p)
        {
            Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(p);
            if (((troopByPosition != null) && (troopByPosition.Status == TroopStatus.埋伏)) && !this.IsFriendly(troopByPosition.BelongedFaction))
            {
                this.DetectAmbush(troopByPosition, this.BelongedFaction.GetKnownAreaData(p));
            }
        }

        private void CheckBuildingFacility()
        {
            if (this.BuildingDaysLeft > 0)
            {
                this.BuildingDaysLeft -= 1;
                if (this.BuildingDaysLeft <= 0)
                {
                    FacilityKind facilityKind = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(this.BuildingFacility);
                    if (facilityKind != null)
                    {
                        this.BuildFacility(facilityKind);
                    }
                    this.BuildingFacility = -1;
                }
            }
        }

        public LinkKind CheckCampaignable(LinkNode node)
        {
            bool flag = true;
            bool flag2 = true;
            for (int i = 1; i < node.Path.Count; i++)
            {
                flag = flag && node.Path[i - 1].IsLandLink(node.Path[i]);
                flag2 = flag2 && node.Path[i - 1].IsWaterLink(node.Path[i]);
            }
            if (flag && flag2)
            {
                return LinkKind.Both;
            }
            if (flag)
            {
                return LinkKind.Land;
            }
            if (flag2)
            {
                return LinkKind.Water;
            }
            return LinkKind.None;
        }

        public void CheckIsFrontLine()
        {
            if (this.BelongedFaction == null) return;
            this.FrontLine = false;
            this.HostileLine = false;
            this.CriticalHostile = false;
            this.noFactionFrontline = false;
            this.orientationFrontLine = false;
            this.withoutTruceFrontline = false;
            this.BelongedFaction.RoutewayPathBuilder.ConsumptionMax = 0.35f;
            foreach (Architecture architecture in this.GetAILinks())
            {
                if (this.IsFriendlyWithoutTruce(architecture.BelongedFaction))
                {
                    continue;
                }
                if (architecture.BelongedFaction == null)
                {
                    noFactionFrontline = true;
                    continue;
                }
                this.FrontLine = true;
                if (this.IsArchitectureHostileWithoutTruce(architecture))
                {
                    this.withoutTruceFrontline = true;
                }
                if (this.IsArchitectureHostile(architecture))
                {
                    this.HostileLine = true;
                }
                if (this.IsArchitectureCriticalHostile(architecture))
                {
                    this.CriticalHostile = true;
                }
                if (this.BelongedSection != null && this.BelongedSection.OrientationFaction == architecture.BelongedFaction)
                {
                    this.orientationFrontLine = true;
                }
            }
            this.BelongedFaction.RoutewayPathBuilder.ConsumptionMax = 0.7f;
        }

        private void RefreshNeutralBuilding()
        {
            if (this.Kind.ID == 250)
            {
                this.Endurance += 50;
                this.Domination += 5;
                this.Morale += 10;
                this.BelongedFaction = null;
                foreach (Troop troop in Session.Current.Scenario.Troops)
                {
                    if (troop.BelongedFaction != null && this.ViewArea.HasPoint(troop.Position))
                    {
                        if (troop.Army.Tiredness > 0)
                        {
                            troop.Army.Tiredness -= Session.GlobalVariables.TroopTirednessDecrease;
                        }

                        if (troop.Morale < 100)
                        {
                            troop.Morale += 10;
                        }

                        if (troop.Combativity < 100)
                        {
                            troop.Combativity += 10;
                        }
                    }
                    if (troop.Army.Tiredness < 0)
                    {
                        troop.Army.Tiredness = 0;
                    }
                }
            }
        }

        private void CheckRobberTroop()
        {
            if (this.RobberTroop != null && this.RobberTroop.BelongedFaction != null)
            {
                this.RobberTroop = null;
                this.RobberTroopID = -1;
            }
            if (this.BelongedFaction != null)
            {
                if ((this.RobberTroop != null) && (this.RobberTroop.RecentlyFighting <= 0))
                {
                    this.RobberTroop.Destroy(true, true);
                    Session.Current.Scenario.Militaries.Remove(this.RobberTroop.Army);
                    Session.Current.Scenario.Troops.RemoveTroop(this.RobberTroop);
                    this.RobberTroop = null;
                }
            }
            else if (this.RobberTroop == null)
            {
                if ((this.JustAttacked && (this.Endurance > 0)) && !this.HasHostileTroopsInArchitecture())
                {
                    List<Point> orientations = new List<Point>();
                    foreach (Troop troop in this.GetHostileTroopsInView())
                    {
                        orientations.Add(troop.Position);
                    }
                    this.CreateRobberTroop(Session.Current.Scenario.GetClosestPosition(this.ArchitectureArea, orientations).Value);
                }
            }
            else if (!(((this.RecentlyAttacked > 0) || (this.RobberTroop.RecentlyFighting > 0)) || this.HasHostileTroopsInView()))
            {
                this.RobberTroop.Destroy(true, true);
                Session.Current.Scenario.Militaries.Remove(this.RobberTroop.Army);
                Session.Current.Scenario.Troops.RemoveTroop(this.RobberTroop);
                this.RobberTroop = null;
            }
        }

        public void ClearFundPacks()
        {
            this.FundPacks.Clear();
        }

        public void ClearFoodPacks()
        {
            this.FoodPacks.Clear();
        }

        public void ClearPopulationPacks()
        {
            this.PopulationPacks.Clear();
        }

        public void ClearRouteways()
        {
            if (this.BelongedFaction != null)
            {
                foreach (Architecture architecture in this.BelongedFaction.Architectures)
                {
                    if (architecture != this)
                    {
                        foreach (Routeway routeway in architecture.HasRoutewayList(this))
                        {
                            routeway.RemoveAfterClose = true;
                        }
                    }
                }
            }
            foreach (Routeway routeway in this.Routeways.GetList())
            {
                Session.Current.Scenario.RemoveRouteway(routeway);
            }
            this.Routeways.Clear();
        }

        /*
        public void ClearSpyPacks()
        {
            this.SpyPacks.Clear();
        }
        */

        private void ClearWork()
        {
            if (this.Agriculture >= this.AgricultureCeiling)
            {
                foreach (Person person in this.AgricultureWorkingPersons)
                {
                    person.WorkKind = ArchitectureWorkKind.无;
                }
            }
            if (this.Commerce >= this.CommerceCeiling)
            {
                foreach (Person person in this.CommerceWorkingPersons)
                {
                    person.WorkKind = ArchitectureWorkKind.无;
                }
            }
            if (this.Technology >= this.TechnologyCeiling)
            {
                foreach (Person person in this.TechnologyWorkingPersons)
                {
                    person.WorkKind = ArchitectureWorkKind.无;
                }
            }

            if (this.Domination >= this.DominationCeiling)
            {
                foreach (Person person in this.DominationWorkingPersons)
                {
                    //this.RemovePersonFromWorkingList(person);
                    person.WorkKind = ArchitectureWorkKind.无;
                }
            }


            if (this.Morale >= this.MoraleCeiling)
            {
                foreach (Person person in this.MoraleWorkingPersons)
                {
                    person.WorkKind = ArchitectureWorkKind.无;
                }
            }
            if (this.Endurance >= this.EnduranceCeiling)
            {
                foreach (Person person in this.EnduranceWorkingPersons)
                {
                    person.WorkKind = ArchitectureWorkKind.无;
                }
            }

            foreach (Military military in this.Militaries)
            {
                if (military.Quantity >= military.Kind.MaxScale || this.Domination < 50 || this.Morale < 100)
                {
                    military.StopRecruitment();

                }
            }

            if (suoyouJunduiDouYijingXunlianHao())
            {
                foreach (Person person in this.TrainingWorkingPersons)
                {
                    person.WorkKind = ArchitectureWorkKind.无;
                }
            }

        }
        private bool suoyouJunduiDouYijingXunlianHao()
        {
            bool JunduiDouYijingXunlianHao = true;
            foreach (Military military in this.Militaries)
            {
                if (military.Morale < military.MoraleCeiling || military.Combativity < military.CombativityCeiling)
                {
                    JunduiDouYijingXunlianHao = false;
                    break;
                }
            }
            return JunduiDouYijingXunlianHao;
        }

        public void CloseAllRouteways()
        {
            foreach (Routeway routeway in this.Routeways.GetList())
            {
                routeway.Close();
            }
        }

        public bool CommerceAvail()
        {
            return (this.Kind.HasCommerce && this.HasPerson());
        }

        public bool ConvincePersonAvail()
        {
            return ((this.HasPerson() && (this.Fund >= this.ConvincePersonFund)) && (this.GetConvincePersonArchitectureArea().Count > 0));
        }

        public Legion CreateDefensiveLegion()
        {
            this.DefensiveLegion = new Legion();
            this.DefensiveLegion.Kind = LegionKind.Defensive;
            this.DefensiveLegion.ID = Session.Current.Scenario.Legions.GetFreeGameObjectID();
            this.DefensiveLegion.StartArchitecture = this;
            this.DefensiveLegion.WillArchitecture = this;
            Session.Current.Scenario.Legions.AddLegionWithEvent(this.DefensiveLegion);
            this.BelongedFaction.AddLegion(this.DefensiveLegion);
            return this.DefensiveLegion;
        }

        /*
        private SpyMessage CreateHireNewPersonSpyMessage(Person person)
        {
            SpyMessage message = new SpyMessage();
            message.Scenario = Session.Current.Scenario;
            message.ID = message.Scenario.SpyMessages.GetFreeGameObjectID();
            message.Kind = SpyMessageKind.HireNewPerson;
            message.MessageFaction = this.BelongedFaction;
            message.MessageArchitecture = this;
            message.Message1 = this.BelongedFaction.Name;
            message.Message2 = base.Name;
            message.Message3 = person.Name;
            message.Message4 = Session.Current.Scenario.Date.ToDateString();
            message.Scenario.SpyMessages.AddMessageWithEvent(message);
            foreach (SpyPack pack in this.SpyPacks)
            {
                int singleWayDays = Session.Current.Scenario.GetSingleWayDays(pack.SpyPerson.Position, this.ArchitectureArea);
                message.AddPersonPack(pack.SpyPerson, singleWayDays);
            }
            return message;
        }*/

        public Military CreateMilitary(MilitaryKind mk)
        {
            Military military = Military.Create(this, mk);
            if (this.OnMilitaryCreate != null)
            {
                this.OnMilitaryCreate(this, military);
            }
            /*
            if (this.HasSpy)
            {
                this.AddMessageToTodayNewMilitarySpyMessage(military);
            }
            */
            return military;
        }
        /*
        private SpyMessage CreateMilitaryScaleSpyMessage(Military m)
        {
            SpyMessage message = new SpyMessage();
            message.Scenario = Session.Current.Scenario;
            message.ID = message.Scenario.SpyMessages.GetFreeGameObjectID();
            message.Kind = SpyMessageKind.MilitaryScale;
            message.MessageFaction = this.BelongedFaction;
            message.MessageArchitecture = this;
            message.Message1 = this.BelongedFaction.Name;
            message.Message2 = base.Name;
            message.Message3 = m.Name;
            message.Message4 = Session.Current.Scenario.Date.ToDateString();
            message.Message5 = (m.Scales * m.Kind.MinScale).ToString();
            message.Scenario.SpyMessages.AddMessageWithEvent(message);
            foreach (SpyPack pack in this.SpyPacks)
            {
                int singleWayDays = Session.Current.Scenario.GetSingleWayDays(pack.SpyPerson.Position, this.ArchitectureArea);
                message.AddPersonPack(pack.SpyPerson, singleWayDays);
            }
            return message;
        }

        private SpyMessage CreateNewFacilitySpyMessage(FacilityKind fk)
        {
            SpyMessage message = new SpyMessage();
            message.Scenario = Session.Current.Scenario;
            message.ID = message.Scenario.SpyMessages.GetFreeGameObjectID();
            message.Kind = SpyMessageKind.NewFacility;
            message.MessageFaction = this.BelongedFaction;
            message.MessageArchitecture = this;
            message.Message1 = this.BelongedFaction.Name;
            message.Message2 = base.Name;
            message.Message3 = fk.Name;
            message.Message4 = Session.Current.Scenario.Date.ToDateString();
            message.Scenario.SpyMessages.AddMessageWithEvent(message);
            foreach (SpyPack pack in this.SpyPacks)
            {
                int singleWayDays = Session.Current.Scenario.GetSingleWayDays(pack.SpyPerson.Position, this.ArchitectureArea);
                message.AddPersonPack(pack.SpyPerson, singleWayDays);
            }
            return message;
        }

        private SpyMessage CreateNewMilitarySpyMessage(Military m)
        {
            SpyMessage message = new SpyMessage();
            message.Scenario = Session.Current.Scenario;
            message.ID = message.Scenario.SpyMessages.GetFreeGameObjectID();
            message.Kind = SpyMessageKind.NewMilitary;
            message.MessageFaction = this.BelongedFaction;
            message.MessageArchitecture = this;
            message.Message1 = this.BelongedFaction.Name;
            message.Message2 = base.Name;
            message.Message3 = m.Name;
            message.Message4 = Session.Current.Scenario.Date.ToDateString();
            message.Scenario.SpyMessages.AddMessageWithEvent(message);
            foreach (SpyPack pack in this.SpyPacks)
            {
                int singleWayDays = Session.Current.Scenario.GetSingleWayDays(pack.SpyPerson.Position, this.ArchitectureArea);
                message.AddPersonPack(pack.SpyPerson, singleWayDays);
            }
            return message;
        }

        private SpyMessage CreateNewTroopSpyMessage(Troop t, bool hand)
        {
            SpyMessage message = new SpyMessage();
            message.Scenario = Session.Current.Scenario;
            message.ID = message.Scenario.SpyMessages.GetFreeGameObjectID();
            message.Kind = SpyMessageKind.NewTroop;
            message.MessageFaction = this.BelongedFaction;
            message.MessageArchitecture = this;
            message.Message1 = this.BelongedFaction.Name;
            message.Message2 = base.Name;
            message.Message3 = t.DisplayName;
            message.Message4 = Session.Current.Scenario.Date.ToDateString();
            if (hand)
            {
                message.Message5 = "不明";
            }
            else
            {
                message.Message5 = (t.WillArchitecture != null) ? t.WillArchitecture.Name : "不明";
            }
            message.Scenario.SpyMessages.AddMessageWithEvent(message);
            foreach (SpyPack pack in this.SpyPacks)
            {
                int singleWayDays = Session.Current.Scenario.GetSingleWayDays(pack.SpyPerson.Position, this.ArchitectureArea);
                message.AddPersonPack(pack.SpyPerson, singleWayDays);
            }
            return message;
        }
        */

        public Legion CreateOffensiveLegion(Architecture willArchitecture)
        {
            Legion legion = new Legion();
            legion.Kind = LegionKind.Offensive;
            legion.StartArchitecture = this;
            legion.WillArchitecture = willArchitecture;
            legion.ID = Session.Current.Scenario.Legions.GetFreeGameObjectID();
            Session.Current.Scenario.Legions.AddLegionWithEvent(legion);
            this.BelongedFaction.AddLegion(legion);
            LinkNode node = null;
            if (this.AIAllLinkNodes.TryGetValue(willArchitecture.ID, out node))
            {
                legion.PreferredRouteway = this.GetRouteway(node, false);
            }
            return legion;
        }

        public void CreateRobberTroop(Point position)
        {
            Military military = new Military();
            military.ID = Session.Current.Scenario.Militaries.GetFreeGameObjectID();
            Session.Current.Scenario.Militaries.AddMilitary(military);
            military.Kind = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(0x15);
            military.Name = military.Kind.Name;
            military.Morale = military.MoraleCeiling;
            military.Combativity = military.CombativityCeiling;
            military.Quantity = (military.Kind.MinScale + (this.Population / 100)) * ((this.AreaCount / 2) + 2);
            if (military.Quantity > military.Kind.MaxScale)
            {
                military.Quantity = military.Kind.MaxScale;
            }
            GameObjectList persons = new GameObjectList();
            Person gameObject = Session.Current.Scenario.Persons.GetGameObject(0x1bc4) as Person;
            persons.Add(gameObject);
            Troop troop = this.CreateTroop(persons, gameObject, military, 0, position);
            troop.WillArchitecture = this;
            this.RobberTroop = troop;
            ExtensionInterface.call("CreateRobberTroop", new Object[] { Session.Current.Scenario, this, troop });
        }

        public Routeway CreateRouteway(Point p)
        {
            if (Session.Current.Scenario.GetTerrainDetailByPosition(p) != null)
            {
                Routeway routeway = new Routeway();
                routeway.ID = Session.Current.Scenario.Routeways.GetFreeGameObjectID();
                Session.Current.Scenario.Routeways.AddRoutewayWithEvent(routeway);
                this.BelongedFaction.AddRouteway(routeway);
                routeway.StartArchitecture = this;
                this.Routeways.Add(routeway);
                routeway.Extend(p);
                ArchitectureList routewayArchitecturesByPosition = Session.Current.Scenario.GetRoutewayArchitecturesByPosition(routeway, p);
                if (routewayArchitecturesByPosition.Count > 0)
                {
                    if (routewayArchitecturesByPosition.Count > 1)
                    {
                        routewayArchitecturesByPosition.PropertyName = "Food";
                        routewayArchitecturesByPosition.IsNumber = true;
                        routewayArchitecturesByPosition.SmallToBig = true;
                        routewayArchitecturesByPosition.ReSort();
                    }
                    routeway.EndArchitecture = routewayArchitecturesByPosition[0] as Architecture;
                    routeway.DestinationArchitecture = routeway.EndArchitecture;
                }
                ExtensionInterface.call("CreateRouteway", new Object[] { Session.Current.Scenario, this, routeway });
                return routeway;
            }
            return null;
        }

        public Routeway CreateRouteway(List<Point> pointlist)
        {
            int num2;
            Routeway routeway = new Routeway();
            routeway.ID = Session.Current.Scenario.Routeways.GetFreeGameObjectID();
            if (Session.GlobalVariables.LiangdaoXitong)
            {
                Session.Current.Scenario.Routeways.AddRoutewayWithEvent(routeway);
                this.BelongedFaction.AddRouteway(routeway);
            }
            else
            {
                routeway.BelongedFaction = this.BelongedFaction;
            }
            routeway.StartArchitecture = this;
            this.Routeways.Add(routeway);
            GameArea routewayStartPoints = this.GetRoutewayStartPoints();
            int num = 0;
            for (num2 = 0; num2 < pointlist.Count; num2++)
            {
                if (routewayStartPoints.HasPoint(pointlist[num2]))
                {
                    num = num2;
                }
            }
            for (num2 = num; num2 < pointlist.Count; num2++)
            {
                routeway.Extend(pointlist[num2]);
            }
            ExtensionInterface.call("CreateRouteway", new Object[] { Session.Current.Scenario, this, routeway });
            return routeway;
        }

        public Troop CreateTroop(GameObjectList persons, Person leader, Military military, int food, Point position)
        {
            return Troop.Create(this, persons, leader, military, food, position);
        }

        public bool CurrentPlayerOwned()
        {
            return ((Session.GlobalVariables.SkyEye && this.HasFaction()) || (this.HasFaction() && (Session.Current.Scenario.NoCurrentPlayer || (this.BelongedFaction == Session.Current.Scenario.CurrentPlayer))));
        }

        public void DamageByGossip(int damage)
        {
            foreach (Person person in this.Persons)
            {
                if (person != this.BelongedFaction.Leader)
                {
                    if (person.TempLoyaltyChange > -20)
                    {
                        person.TempLoyaltyChange -= (StaticMethods.GetRandomValue((int)(damage * (int)(Enum.GetNames(typeof(PersonLoyalty)).Length - person.PersonalLoyalty) * (Math.Min(person.Loyalty, 100) / 100.0)), 100));
                    }
                }
            }
            ExtensionInterface.call("GossipDamage", new Object[] { Session.Current.Scenario, this, damage });
        }

        public void checkEvent()
        {
            GameObjectList list = Session.Current.Scenario.AllEvents.GetList();
            list.PropertyName = "ID";
            list.SmallToBig = true;
            list.IsNumber = true;
            list.ReSort();

            foreach (Event e in list)
            {
                if (e.checkConditions(this))
                {
                    if (!Session.Current.Scenario.EventsToApply.ContainsKey(e))
                    {
                        Session.Current.Scenario.EventsToApply.Add(e, this);
                        e.ApplyEventDialogs(this, Session.MainGame.mainGameScreen);
                    }
                    if (!Session.Current.Scenario.YesEventsToApply.ContainsKey(e) && e.yesEffect.Count > 0)
                    {
                        Session.Current.Scenario.YesEventsToApply.Add(e, this);
                    }
                    if (!Session.Current.Scenario.NoEventsToApply.ContainsKey(e) && e.noEffect.Count > 0)
                    {
                        Session.Current.Scenario.NoEventsToApply.Add(e, this);
                    }
                    /*
                    if (!Session.Current.Scenario.YesArchiEventsToApply.ContainsKey(e))
                    {
                        Session.Current.Scenario.YesArchiEventsToApply.Add(e, this);
                        e.ApplyEventDialogs(this);
                    }
                    if (!Session.Current.Scenario.NoArchiEventsToApply.ContainsKey(e))
                    {
                        Session.Current.Scenario.NoArchiEventsToApply.Add(e, this);
                        e.ApplyEventDialogs(this);
                    }*/
                }
            }
        }

        private void PersonExperienceIncrease()
        {
            foreach (Person p in this.Persons)
            {
                p.CommandExperience += this.CommandExperienceIncrease;
                p.StrengthExperience += this.StrengthExperienceIncrease;
                p.IntelligenceExperience += this.IntelligenceExperienceIncrease;
                p.PoliticsExperience += this.PoliticsExperienceIncrease;
                p.GlamourExperience += this.GlamourExperienceIncrease;
                p.Reputation += this.ReputationIncrease;
            }
        }

        public void DayEvent()
        {
            this.RefreshNeutralBuilding(); //  加buff中立建筑
            this.FundPacksDayEvent();
            this.FoodPacksDayEvent();
            this.PopulationPacksDayEvent();
            //this.SpyPacksDayEvent();
            this.characteristicsDoWork();
            this.InformationDayEvent();
            this.HandleFacilities();
            this.ViewAreaEvent();
            this.StrategicCenterEffect();
            this.AutoDecrement();
            this.AutoIncrement();
            this.Sourrounded();
            this.ResetDayInfluence();
            this.CheckRobberTroop();
            this.PopulationEscapeEvent();
            this.FoodReduce();
            this.RestEvent();
            this.zainanshijian();
            this.captiveEscape();
            this.PersonExperienceIncrease();
            this.checkEvent();
            this.JustAttacked = false;
            ExpectedFoodCache = -1;
            ExpectedFundCache = -1;
            this.SuspendTroopTransfer--;

            if (this.Mayor != null)
            {
                this.MayorOnDutyDays++;
                this.MayorOnDutyDays += Session.Parameters.DayInTurn;
            }

            this.resolveAIQuickBattle();
        }

        private void resolveAIQuickBattle()
        {

            foreach (Architecture a in this.AIBattlingArchitectures.GetList())
            {
                bool aborted = false;

                foreach (Faction f in Session.Current.Scenario.PlayerFactions)
                {
                    if (f.IsArchitectureKnown(this) || f.IsArchitectureKnown(a))
                    {
                        if (this.BelongedFaction.GetLegion(a) == null)
                        {
                            aborted = true;
                        }
                        else
                        {
                            foreach (Troop t in this.BelongedFaction.GetLegion(a).Troops)
                            {
                                t.TargetArchitecture = a;
                                t.WillArchitecture = a;
                                t.QuickBattling = false;
                                a.TotalHostileForce -= t.FightingForce;
                            }
                            if (a.DefensiveLegion != null)
                            {
                                foreach (Troop t in a.DefensiveLegion.Troops)
                                {
                                    t.QuickBattling = false;
                                    this.TotalFriendlyForce -= t.FightingForce;
                                }
                            }
                        }
                        aborted = true;
                    }
                }

                if (!aborted)
                {
                    // offensive troop
                    int reserve = this.getArmyReserveForOffensive();
                    if (this.ArmyScale < reserve)
                    {
                        this.AIBattlingArchitectures.Remove(a);
                        break;
                    }

                    bool built = this.BuildOffensiveTroop(a, LinkKind.Land, true, reserve);
                    if (!built)
                    {
                        this.AIBattlingArchitectures.Remove(a);
                        break;
                    }

                    foreach (Troop t in this.BelongedFaction.GetLegion(a).Troops)
                    {
                        t.TargetArchitecture = null;
                        t.WillArchitecture = null;
                        t.QuickBattling = true;
                        a.TotalHostileForce += t.FightingForce;
                    }

                    if (a.BelongedFaction != null)
                    {
                        // defensive troop
                        a.DefensiveCampaign(this.BelongedFaction.GetLegion(a).Troops);
                        if (a.DefensiveLegion != null)
                        {
                            foreach (Troop t in a.DefensiveLegion.Troops)
                            {
                                t.QuickBattling = true;
                                this.TotalFriendlyForce += t.FightingForce;
                            }
                        }
                    }

                    // fight
                    if (a.DefensiveLegion != null)
                    {
                        GameObjectList defList = a.DefensiveLegion.Troops.GetList();
                        foreach (Troop t in defList)
                        {
                            if (this.BelongedFaction.GetLegion(a).Troops.Count > 0)
                            {
                                TroopList list = this.BelongedFaction.GetLegion(a).Troops;
                                Troop target = (Troop)list[GameObject.Random(list.Count)];
                                t.AttackTroop(target);
                                t.ApplyDamageList();
                            }
                            else
                            {
                                this.AIBattlingArchitectures.Remove(a);
                            }
                        }
                    }
                    GameObjectList attackingTroops = this.BelongedFaction.GetLegion(a).Troops.GetList();
                    foreach (Troop t in attackingTroops)
                    {
                        if (a.Endurance > 0)
                        {
                            t.AttackArchitecture(a);
                            t.ApplyDamageList();
                            if (a.Endurance <= 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            if (a.DefensiveLegion != null && a.DefensiveLegion.Troops.Count > 0)
                            {
                                Troop target = (Troop)a.DefensiveLegion.Troops[GameObject.Random(a.DefensiveLegion.Troops.Count)];
                                t.AttackTroop(target);
                                t.ApplyDamageList();
                            }
                            else
                            {
                                t.Position = a.ArchitectureArea.Centre;
                                t.BelongedFaction = this.BelongedFaction;
                                t.Occupy();

                                foreach (Troop u in attackingTroops)
                                {
                                    u.Position = a.ArchitectureArea.Centre;
                                    u.Enter(a);
                                }
                                this.AIBattlingArchitectures.Remove(a);
                                break;
                            }

                        }

                    }

                    int i = 0;
                    foreach (Military m in this.Militaries)
                    {
                        if (this.Persons.Count > i)
                        {
                            if (GameObject.Chance((m.Kind.OffenceRadius + 1) * (m.Kind.OffenceRadius + 1) * 100 / this.Militaries.Count))
                            {
                                a.Endurance = a.Endurance - Math.Max(1, (int)(m.Offence / 4 * m.Kind.ArchitectureDamageRate * Session.Parameters.ArchitectureDamageRate));
                                if (a.Endurance <= 0) break;
                            }
                        }
                        else
                        {
                            break;
                        }
                        i++;
                    }


                }
                else
                {
                    foreach (Architecture x in this.AIBattlingArchitectures)
                    {
                        x.AIBattlingArchitectures.Remove(this);
                    }
                    this.AIBattlingArchitectures.Clear();
                }
            }
        }

        private void RestEvent()
        {
            foreach (Military m in this.Militaries)
            {
                if (m.Tiredness > 0)
                {
                    m.Tiredness -= Session.GlobalVariables.TirednessDecrease;
                    if (m.Tiredness < 0) m.Tiredness = 0;
                }
            }
            foreach (Person p in this.Persons)
            {
                if (p.Tiredness > 0)
                {
                    p.Tiredness -= Session.GlobalVariables.TirednessDecrease;
                    if (p.Tiredness < 0) p.Tiredness = 0;
                }
            }
            foreach (Person p in this.MovingPersons)
            {
                if (p.Tiredness > 0 && (p.OutsideTask == OutsideTaskKind.后宮))
                {
                    p.Tiredness -= Session.GlobalVariables.TirednessDecrease;
                    if (p.Tiredness < 0) p.Tiredness = 0;
                }
            }
            foreach (Captive c in this.Captives)
            {
                if (c.CaptivePerson.Tiredness > 0)
                {
                    c.CaptivePerson.Tiredness -= Session.GlobalVariables.TirednessDecrease;
                    if (c.CaptivePerson.Tiredness < 0) c.CaptivePerson.Tiredness = 0;
                }
            }
            foreach (Person p in this.Feiziliebiao)
            {
                if (p.Tiredness > 0)
                {
                    p.Tiredness -= Session.GlobalVariables.TirednessDecrease;
                    if (p.Tiredness < 0) p.Tiredness = 0;
                }
            }
        }

        private void captiveEscape()
        {
            foreach (Captive p in this.Captives.GetRandomList())
            {
                if (GameObject.Random(5) == 0 && !GameObject.Chance(p.CaptivePerson.PersonalLoyalty * 25))
                {
                    if (p.CaptiveFaction != null)
                    {
                        bool pass = true;
                        if (p.CaptivePerson.HasStrainTo(p.CaptiveFaction.Leader) && GameObject.Chance(33))
                        {
                            pass = false;
                        }
                        if (p.CaptivePerson.HasCloseStrainTo(p.CaptiveFaction.Leader) && GameObject.Chance(67))
                        {
                            pass = false;
                        }
                        if (p.CaptivePerson.IsCloseTo(p.CaptiveFaction.Leader) && GameObject.Chance(33))
                        {
                            pass = false;
                        }
                        if (p.CaptivePerson.IsVeryCloseTo(p.CaptiveFaction.Leader) && GameObject.Chance(67))
                        {
                            pass = false;
                        }
                        if (p.CaptivePerson.Hates(p.CaptiveFaction.Leader))
                        {
                            pass = true;
                        }
                        if (pass)
                        {
                            p.CaptivePerson.TempLoyaltyChange--;
                        }
                    }
                    else
                    {
                        p.CaptivePerson.TempLoyaltyChange--;
                    }
                }
                if (GameObject.Random((this.Domination * 10 + this.Morale) * 20) + 200 <= GameObject.Random(p.CaptivePerson.CaptiveAbility))
                {
                    if (!GameObject.Chance(noEscapeChance) || GameObject.Chance(p.CaptivePerson.captiveEscapeChance))
                    {
                        p.CaptiveEscape();
                    }
                }
                if (p.CaptivePerson.ArrivingDays > 0 && (GameObject.Chance(p.CaptivePerson.JailBreakAbility / 100) || GameObject.Chance(p.CaptivePerson.captiveEscapeChance)))
                {
                    p.CaptiveEscape();
                }
            }
        }

        public void tingzhizhenzai()
        {
            foreach (Person person in this.ZhenzaiWorkingPersons)
            {
                person.WorkKind = ArchitectureWorkKind.无;
            }
        }

        private void zainanshijian()
        {
            if (Session.Current.Scenario.DaySince < 720 * Session.Parameters.DayInTurn) return;
            if (this.youzainan)
            {
                //this.DecreaseFood(this.ZhenzaiWorkingPersons.Count * 3000);
                this.DecreaseFund(this.ZhenzaiWorkingPersons.Count * this.InternalFundCost);
                this.zhixingzainanshanghai();

                //this.zainan.shengyutianshu--;
                //this.zainan.shengyutianshu -= this.zhenzaijianshaotianshu();

                this.zainan.shengyutianshu -= Session.Parameters.DayInTurn;
                this.zainan.shengyutianshu -= this.zhenzaijianshaotianshu() * Session.Parameters.DayInTurn;

                if (this.zainan.shengyutianshu <= 0)
                {
                    this.youzainan = false;
                    this.tingzhizhenzai();
                }

                if (this.Food <= 0 || this.Fund <= 0)
                {
                    this.tingzhizhenzai();
                }
            }
            else
            {
                if (GameObject.Random(Session.GlobalVariables.zainanfashengjilv) == 0 && this.Kind.CountToMerit)
                {
                    int kindID;
                    kindID = GameObject.Random(Session.Current.Scenario.GameCommonData.suoyouzainanzhonglei.Count);

                    bool doDisaster = true;
                    if (disasterChanceDecrease.ContainsKey(kindID))
                    {
                        if (GameObject.Chance(disasterChanceDecrease[kindID]))
                        {
                            doDisaster = false;
                        }
                    }
                    if (disasterChanceIncrease.ContainsKey(kindID))
                    {
                        if (GameObject.Chance(disasterChanceIncrease[kindID]))
                        {
                            doDisaster = true;
                        }
                    }

                    if (doDisaster)
                    {
                        this.zainan.zainanzhonglei = Session.Current.Scenario.GameCommonData.suoyouzainanzhonglei.Getzainanzhonglei(kindID);
                        this.zainan.shengyutianshu = this.zainan.zainanzhonglei.shijianxiaxian + GameObject.Random(this.zainan.zainanzhonglei.shijianshangxian - this.zainan.zainanzhonglei.shijianxiaxian);
                        this.youzainan = true;
                        ExtensionInterface.call("DisasterHappened", new Object[] { Session.Current.Scenario, this, this.zainan });
                        foreach (Military military in this.Militaries)//发生灾难时不能补充
                        {
                            military.StopRecruitment();
                        }
                        this.Onfashengzainan(this, this.zainan.zainanzhonglei.ID);
                    }
                }
            }
        }

        private int zhenzaijianshaotianshu()
        {
            int tianshu;
            int zhenzainenglizonghe = 0;
            foreach (Person person in this.ZhenzaiWorkingPersons)
            {
                zhenzainenglizonghe += person.zhenzaiAbility;
            }
            float extraProb = (zhenzainenglizonghe % 3000) / 30.0f;
            tianshu = zhenzainenglizonghe / 3000;
            return tianshu + (GameObject.Chance((int)extraProb) ? 1 : 0);
        }

        private float jianzaixishu()
        {
            float xishu;
            int zhenzainenglizonghe = 0;
            foreach (Person person in this.ZhenzaiWorkingPersons)
            {
                zhenzainenglizonghe += person.zhenzaiAbility;
            }
            xishu = 500.0f / zhenzainenglizonghe;
            if (xishu < 0.01f)
            {
                xishu = 0.01f;
            }
            if (xishu > 1f)
            {
                xishu = 1f;
            }
            return xishu;
        }

        private void zhixingzainanshanghai()
        {
            float rate = 1;
            if (disasterDamageRateDecrease.ContainsKey(this.zainan.ID))
            {
                rate = (float)(rate - disasterDamageRateDecrease[this.zainan.ID] / 100.0);
                if (rate < 0) rate = 0;
            }
            this.DecreasePopulation((int)(this.zainan.zainanzhonglei.renkoushanghai * jianzaixishu() * rate));
            this.DecreaseDomination((int)(this.zainan.zainanzhonglei.tongzhishanghai * rate));
            this.xiajiangnaijiu((int)(this.zainan.zainanzhonglei.naijiushanghai * rate));
            this.DecreaseAgriculture((int)(this.zainan.zainanzhonglei.nongyeshanghai * rate));
            this.DecreaseCommerce((int)(this.zainan.zainanzhonglei.shangyeshanghai * rate));
            this.DecreaseTechnology((int)(this.zainan.zainanzhonglei.jishushanghai * rate));
            this.DecreaseMorale((int)(this.zainan.zainanzhonglei.minxinshanghai * jianzaixishu() * rate));
            this.DecreaseFood((int)(this.zainan.zainanzhonglei.FoodDamage * jianzaixishu() * rate));
            this.DecreaseFund((int)(this.zainan.zainanzhonglei.FundDamage * jianzaixishu() * rate));
            if (this.zainan.zainanzhonglei.TroopDamage > 0)
            {
                foreach (Military m in this.Militaries)
                {
                    int loseTroop = Math.Min(m.Quantity, (int)(this.zainan.zainanzhonglei.TroopDamage * jianzaixishu() * rate));
                    m.DecreaseQuantity(loseTroop);
                    m.IncreaseInjuryQuantity(loseTroop * m.Kind.InjuryChance / 100);
                    m.DecreaseMorale(loseTroop / 100);
                    m.DecreaseCombativity(loseTroop / 100);
                }
            }
            if (this.zainan.zainanzhonglei.OfficerDamage > 0)
            {
                foreach (Person p in this.Persons)
                {
                    while (GameObject.Chance(this.zainan.zainanzhonglei.OfficerDamage))
                    {
                        p.InjureRate *= 0.85f;
                        Session.MainGame.mainGameScreen.OnOfficerSick(p);
                        if (p.InjureRate < 0.05 && Session.GlobalVariables.OfficerDieInBattleRate > 0)
                        {
                            p.ToDeath(null, this.BelongedFaction);
                        }
                    }
                }
            }
        }

        public void DecreaseAgriculture(int decrement)
        {
            this.Agriculture -= decrement;
            if (this.Agriculture < 0)
            {
                this.Agriculture = 0;
            }
        }

        public void DecreaseCommerce(int decrement)
        {
            this.Commerce -= decrement;
            if (this.Commerce < 0)
            {
                this.Commerce = 0;
            }
        }

        public int DecreaseDomination(int decrement)
        {
            int domination = decrement;
            if ((this.Domination - decrement) < 0)
            {
                domination = this.Domination;
            }
            this.Domination -= domination;
            return domination;
        }

        public void xiajiangnaijiu(int decrement)  //灾难下降耐久
        {
            this.Endurance -= decrement;
            if (this.Endurance < 0)
            {
                this.Endurance = 0;
            }
        }

        public int DecreaseEndurance(int decrement)
        {
            if (this.Endurance <= 0)
            {
                return 0;
            }

            int endurance = decrement;
            if ((this.Endurance - decrement) < 0)
            {
                endurance = this.Endurance;
            }
            this.Endurance -= endurance;
            this.SetRecentlyAttacked();
            this.DecreaseFacilityEndurance(endurance);
            if (this.Endurance == 0)
            {
                this.RecentlyBreaked = 30;
                this.WallStateChange();
            }
            return endurance;
        }

        public void DecreaseFacilityEndurance(int decrement)
        {
            if (decrement > 0)
            {
                this.Facilities.DecreaseEndurance((int)(decrement * this.RateOfFacilityEnduranceDown));
                foreach (Facility facility in this.Facilities.GetList())
                {
                    if (facility.Endurance <= 0)
                    {
                        this.DemolishFacility(facility);
                    }
                }
            }
        }

        public void DecreaseFood(int decrement)
        {
            this.food -= decrement;
            if (this.food < 0)
            {
                this.food = 0;
            }
        }

        public void DecreaseFund(int decrement)
        {
            this.fund -= decrement;
            if (this.fund < 0)
            {
                this.fund = 0;
            }
        }

        public void DecreaseMorale(int decrement)
        {
            this.Morale -= decrement;
            if (this.Morale < 0)
            {
                this.Morale = 0;
            }
        }

        public int DecreasePopulation(int decrement)
        {
            if (this.population < decrement)
            {
                decrement = this.population;
            }
            this.population -= decrement;
            return decrement;
        }

        public int DecreaseMilitaryPopulation(int decrement)
        {
            if (this.MilitaryPopulation < decrement)
            {
                decrement = this.MilitaryPopulation;
            }
            this.MilitaryPopulation -= decrement;
            return decrement;
        }


        public void DecreaseTechnology(int decrement)
        {
            this.Technology -= decrement;
            if (this.Technology < 0)
            {
                this.Technology = 0;
            }
        }

        private PersonList AISelectPersonIntoTroop_inner(Person leader, PersonList otherPersons, bool markSelected)
        {
            PersonList persons = new PersonList();
            persons.Add(leader);
            if (markSelected)
            {
                leader.Selected = true;
            }
            return persons;
        }

        private bool isPersonAllowedIntoTroop(Person person, Military military, bool offensive)
        {
            bool r = person.LocationArchitecture == this && !person.TooTiredToBattle && GameObject.Random(person.Tiredness / 5) == 0 && (person.Command >= military.Kind.MinCommand);
            foreach (KeyValuePair<Condition, float> c in military.Kind.AILeaderConditionWeight)
            {
                if (c.Key.CheckCondition(person) && c.Value <= 0)
                {
                    return false;
                }
            }
            return r;
        }

        private TroopList AISelectPersonIntoTroop(Architecture from, Military military, bool offensive)
        {
            TroopList result = new TroopList();
            if (military.FollowedLeader != null && from.Persons .HasGameObject(military.FollowedLeader) && military.FollowedLeader.LocationTroop == null
                && isPersonAllowedIntoTroop(military.FollowedLeader, military, offensive))
            {
                if (isPersonAllowedIntoTroop(military.FollowedLeader, military, offensive))
                {
                    result.Add(Troop.CreateSimulateTroop(this.AISelectPersonIntoTroop_inner(military.FollowedLeader, from.Persons  , true), military, from.Position));
                }
            }
            else if (military.Leader != null && military.LeaderExperience >= 10 && (military.Leader.Strength >= 80 || military.Leader.Command >= 80 || military.Leader.HasLeaderValidTitle)
                && from.Persons.HasGameObject(military.Leader) && military.Leader.LocationTroop == null && isPersonAllowedIntoTroop(military.Leader, military, offensive)
               )
            {
                if (isPersonAllowedIntoTroop(military.Leader, military, offensive))
                {
                    result.Add(Troop.CreateSimulateTroop(this.AISelectPersonIntoTroop_inner(military.Leader, from.Persons , true), military, from.Position));
                }
            }
            else
            {
                GameObjectList pl = from.Persons .GetList();
                pl.PropertyName = "FightingForce";
                pl.IsNumber = true;
                pl.SmallToBig = false;
                pl.ReSort();
                foreach (Person person in pl)
                {
                    if (!person.Selected && (!offensive || isPersonAllowedIntoTroop(person, military, offensive)))
                    {
                        if (person.HasMilitaryKindTitle(military.Kind))
                        {
                            result.Add(Troop.CreateSimulateTroop(this.AISelectPersonIntoTroop_inner(person, from.Persons , false), military, from.Position));
                        }
                        else if (person.HasMilitaryTypeTitle(military.Kind.Type))
                        {
                            result.Add(Troop.CreateSimulateTroop(this.AISelectPersonIntoTroop_inner(person, from.Persons , false), military, from.Position));
                        }
                        else if ((this.BelongedFaction.AvailableMilitaryKinds.GetMilitaryKindList().GameObjects.Contains(military.Kind) && military.Kind.RecruitLimit > 10) ||
                            person.FightingForce >= Session.Parameters.AIUniqueTroopFightingForceThreshold || (this.Endurance < 30 && !offensive))
                        {
                            result.Add(Troop.CreateSimulateTroop(this.AISelectPersonIntoTroop_inner(person, from.Persons , false), military, from.Position));
                        }
                    }
                }
            }
            return result;
        }

        private void DefensiveCampaign(TroopList quickBattleList)
        {
            DateTime beforeStart = DateTime.UtcNow;

            List<Point> orientations = new List<Point>();
            TroopList hostileTroopsInView;

            if (quickBattleList == null)
            {
                hostileTroopsInView = this.GetHostileTroopsInView();
                if (hostileTroopsInView.Count <= 0) return;
            }
            else
            {
                hostileTroopsInView = quickBattleList;
            }

            foreach (Troop troop in hostileTroopsInView)
            {
                orientations.Add(troop.Position);
            }

            if ((this.HasPerson() && this.HasCampaignableMilitary()) && (this.GetAllAvailableArea(false).Count != 0))
            {
                if (hostileTroopsInView.Count > 0)
                {
                    TroopList friendlyTroopsInView = this.GetFriendlyTroopsInView();
                    int troopSent = 0;
                    int militaryCount = this.MilitaryCount;

                    int opFactor = 2;
                    if (this.Food > this.AbundantFood)
                    {
                        opFactor = 5;
                    }
                    else if (this.Food < this.EnoughFood)
                    {
                        opFactor = 1;
                    }

                    if (!(this.Endurance > this.EnduranceCeiling * 0.2f && this.TotalFriendlyForce > this.TotalHostileForce * opFactor && friendlyTroopsInView.Count >= 4))
                    {
                        Troop troop2;
                        SortedBoundedSet<Troop> list4 = new SortedBoundedSet<Troop>(Session.Parameters.MaxAITroopCountCandidates, new FightingForceComparer());
                        bool isBesideWater = this.IsBesideWater;

                        foreach (Military military in this.Militaries.GetRandomList())
                        {
                            if (military.IsFewScaleNeedRetreat && this.Endurance >= 30) continue;
                            if ((isBesideWater || (military.Kind.Type != MilitaryType.水军)) && (((((this.Endurance < 30) || military.Kind.AirOffence) || (military.Scales >= 2)) && (military.Morale > 0x2d)) && ((this.Endurance < 30) || (military.InjuryQuantity < military.Kind.MinScale))))
                            {
                                TroopList candidates = this.AISelectPersonIntoTroop(this, military, false);
                                foreach (Troop t in candidates)
                                {
                                    if (t.FightingForce < 10000 && t.FightingForce < (this.TotalHostileForce * opFactor - this.TotalFriendlyForce) / 25)
                                    {
                                        t.Destroy(true, false);
                                        continue;
                                    }
                                    if (t.Army.Scales < 5 && this.Endurance > 30)
                                    {
                                        t.Destroy(true, false);
                                        continue;
                                    }
                                    if (t.FoodMax / 2 > this.Food)
                                    {
                                        t.Destroy(true, false);
                                        continue;
                                    }

                                    Troop removed;
                                    list4.Add(t, out removed);
                                    if (removed != null)
                                    {
                                        removed.Destroy(true, false);
                                    }
                                }
                            }
                        }

                        if (list4.Count > 0)
                        {
                            foreach (Troop troop in list4)
                            {
                                bool personAlreadyOut = false;
                                foreach (Person p in troop.Candidates)
                                {
                                    if (p.LocationTroop != null)
                                    {
                                        personAlreadyOut = true;
                                        break;
                                    }
                                }
                                if (personAlreadyOut) continue;
                                bool militaryOut = true;
                                foreach (Military m in this.Militaries)
                                {
                                    if (troop.Army == m)
                                    {
                                        militaryOut = false;
                                        break;
                                    }
                                }
                                if (militaryOut) continue;

                                Point? nullable = this.GetCampaignPosition(troop, orientations, troop.Army.Scales > 0);
                                if (!nullable.HasValue)
                                {
                                    break;
                                }

                                Person leader = troop.Candidates[0] as Person;
                                PersonList candidates = this.SelectSubOfficersToTroop(troop);
                                troop2 = this.CreateTroop(candidates, leader, troop.Army, -1, nullable.Value);
                                troop2.WillArchitecture = this;
                                if (this.DefensiveLegion == null)
                                {
                                    this.CreateDefensiveLegion();
                                }
                                this.DefensiveLegion.AddTroop(troop2);
                                //this.PostCreateTroop(troop2, false);
                                this.TotalFriendlyForce += troop2.FightingForce;
                                troopSent++;

                                if (this.TotalFriendlyForce > this.TotalHostileForce * opFactor && friendlyTroopsInView.Count + troopSent >= 4)
                                {
                                    break;
                                }
                            }

                            foreach (Troop t in list4)
                            {
                                t.Destroy(true, false);
                            }
                        }
                    }
                }
            }
            else if (!this.HasPerson() && this.HasCampaignableMilitary())
            {
                int totalHostilePersonCount = 0;
                foreach (Troop troop in this.GetHostileTroopsInView())
                {
                    totalHostilePersonCount += troop.PersonCount;
                }
                int send = totalHostilePersonCount / 2;
                foreach (Architecture a in this.BelongedSection.Architectures)
                {
                    if (a == this) continue;
                    if (a.HasHostileTroopsInView()) continue;

                    if (a.PersonCount <= send) continue;

                    this.CallPeople(a, send);

                }
            }

            //not enough defensive troop, call for reinforcements!!
            float rate = (float)Math.Max(1, (200 - this.Endurance) * 0.005 + 1);
            if (quickBattleList == null && this.TotalFriendlyForce < this.TotalHostileForce * rate)
            {
                foreach (LinkNode i in this.AIAllLinkNodes.Values)
                {
                    if (i.Level > 1) break;
                    if (i.A.actuallyUnreachableArch.Contains(this))
                    {
                        continue;
                    }
                    if (this.BelongedFaction == i.A.BelongedFaction && i.A.HasPerson()
                        && i.A.BelongedSection.AIDetail.AutoRun)
                    {

                        SortedBoundedSet<Troop> supportList = new SortedBoundedSet<Troop>(Session.Parameters.MaxAITroopCountCandidates, new FightingForceComparer());
                        Troop troop2;

                        foreach (Military military in i.A.Militaries.GetRandomList())
                        {
                            if (military.IsFewScaleNeedRetreat) continue;
                            if (military.IsTransport) continue;
                            if (this.isArmyNavigableTo(i, military) && (military.Morale > 90) && (military.InjuryQuantity < military.Kind.MinScale))
                            {
                                TroopList candidates = this.AISelectPersonIntoTroop(this, military, true);
                                foreach (Troop t in candidates)
                                {
                                    if ((t.FightingForce < 10000) && (t.Army.Scales < 10))
                                    {
                                        t.Destroy(true, false);
                                        continue;
                                    }

                                    Troop removed;
                                    supportList.Add(t, out removed);
                                    if (removed != null)
                                    {
                                        removed.Destroy(true, false);
                                    }
                                }
                            }
                        }

                        if (supportList.Count > 0)
                        {
                            foreach (Troop troop in supportList)
                            {
                                bool personAlreadyOut = false;
                                foreach (Person p in troop.Candidates)
                                {
                                    if (p.LocationTroop != null)
                                    {
                                        personAlreadyOut = true;
                                        break;
                                    }
                                }
                                if (personAlreadyOut) continue;
                                bool militaryOut = true;
                                foreach (Military m in i.A.Militaries)
                                {
                                    if (troop.Army == m)
                                    {
                                        militaryOut = false;
                                        break;
                                    }
                                }
                                if (militaryOut) continue;

                                Point? nullable = i.A.GetCampaignPosition(troop, orientations, troop.Army.Scales > 0);
                                if (!nullable.HasValue)
                                {
                                    continue;
                                }
                                Person leader = troop.Candidates[0] as Person;
                                PersonList candidates = i.A.SelectSubOfficersToTroop(troop);
                                troop2 = i.A.CreateTroop(candidates, leader, troop.Army, -1, nullable.Value);
                                troop2.WillArchitecture = this;
                                if (this.DefensiveLegion == null)
                                {
                                    this.CreateDefensiveLegion();
                                }
                                this.DefensiveLegion.AddTroop(troop2);
                               // i.A.PostCreateTroop(troop2, false);
                                this.TotalFriendlyForce += troop2.FightingForce;
                            }
                            foreach (Troop t in supportList)
                            {
                                t.Destroy(true, false);
                            }

                        }
                    }

                }
            }
        }

        private bool isArmyNavigableTo(LinkKind kind, Military military)
        {
            return Session.GlobalVariables.LandArmyCanGoDownWater ||
                ((kind == LinkKind.Land && military.Kind.Type != MilitaryType.水军) || (kind == LinkKind.Water && military.Kind.Type == MilitaryType.水军) || kind == LinkKind.Both);
        }

        private bool isArmyNavigableTo(LinkNode targetNode, Military military)
        {
            return Session.GlobalVariables.LandArmyCanGoDownWater ||
                ((targetNode.Kind == LinkKind.Land && military.Kind.Type != MilitaryType.水军) || (targetNode.Kind == LinkKind.Water && military.Kind.Type == MilitaryType.水军) || targetNode.Kind == LinkKind.Both);
        }

        public void DemolishAllRouteways()
        {
            foreach (Routeway routeway in this.Routeways.GetList())
            {
                Session.Current.Scenario.RemoveRouteway(routeway);
            }
        }

        public void DemolishFacility(Facility facility)
        {
            if (this.FacilityEnabled || facility.MaintenanceCost <= 0)
            {
                facility.Influences.PurifyInfluence(this, Applier.Facility, facility.ID);
            }
            this.Facilities.Remove(facility);
            Session.Current.Scenario.Facilities.Remove(facility);
            ExtensionInterface.call("FacilityDemolished", new Object[] { Session.Current.Scenario, this, facility });
        }

        public bool DestroyAvail()
        {
            return ((this.MovablePersons.Count > 0 && (this.Fund >= this.DestroyArchitectureFund)) && (this.GetDestroyArchitectureArea().Count > 0));
        }

        public bool DetailAvail()
        {
            return (Session.GlobalVariables.SkyEye || this.CurrentPlayerOwned());
        }

        private void DetectAmbush(Troop troop, InformationLevel level)
        {
            int chance = 40 - troop.Leader.Calmness;
            if (level <= InformationLevel.中)
            {
                if (troop.OnlyBeDetectedByHighLevelInformation)
                {
                    return;
                }
            }
            else
            {
                chance *= 3;
            }
            if (GameObject.Chance(chance))
            {
                troop.AmbushDetected(troop);
            }
        }

        private void DetectAmbushTroop()
        {
            if (this.BelongedFaction != null)
            {
                GameArea longViewArea = this.LongViewArea;
                foreach (Point point in longViewArea.Area)
                {
                    this.CheckAmbushTroop(point);
                }
            }
        }

        private void DevelopAgriculture()
        {
            if (this.Agriculture != this.AgricultureCeiling)
            {
                foreach (Person person in this.AgricultureWorkingPersons)
                {
                    if (!person.InternalNoFundNeeded)
                    {
                        if (this.Fund < this.InternalFundCost)
                        {
                            continue;
                        }
                        this.DecreaseFund(this.InternalFundCost);
                    }
                    int randomValue = StaticMethods.GetRandomValue((int)((person.AgricultureAbility * this.CurrentRateOfInternal) * Session.Parameters.InternalRate), 500 + (150 * (this.AreaCount - 1)));
                    if (randomValue > 0)
                    {
                        person.AddInternalExperience(randomValue * 2);
                        person.AddPoliticsExperience(randomValue * 2);
                        person.AddGlamourExperience(randomValue * 2);
                        person.IncreaseReputation(randomValue * 4);
                        person.IncreaseOfficerMerit(randomValue * 4);
                        if (GameObject.Random(360 / Session.Current.Scenario.Parameters.DayInTurn) == 0)
                        {
                            person.IncreaseKarma(1);
                        }
                        this.BelongedFaction.IncreaseReputation(randomValue * person.MultipleOfAgricultureReputation);
                        this.BelongedFaction.IncreaseTechniquePoint((randomValue * person.MultipleOfAgricultureTechniquePoint) * 100);
                        this.IncreaseAgriculture(randomValue);
                    }
                }
            }
        }

        private void DevelopArmy()
        {
            foreach (Military military in this.Militaries)
            {
                military.Recovery(this.MultipleOfRecovery);

                this.RecruitmentMilitary(military);
            }
            this.TrainMilitary();
        }

        private void DevelopCommerce()
        {
            if (this.Commerce != this.CommerceCeiling)
            {
                foreach (Person person in this.CommerceWorkingPersons)
                {
                    if (!person.InternalNoFundNeeded)
                    {
                        if (this.Fund < this.InternalFundCost)
                        {
                            continue;
                        }
                        this.DecreaseFund(this.InternalFundCost);
                    }
                    int randomValue = StaticMethods.GetRandomValue((int)((person.CommerceAbility * this.CurrentRateOfInternal) * Session.Parameters.InternalRate), 500 + (150 * (this.AreaCount - 1)));
                    if (randomValue > 0)
                    {
                        person.AddInternalExperience(randomValue * 2);
                        person.AddIntelligenceExperience(randomValue);
                        person.AddPoliticsExperience(randomValue * 2);
                        person.AddGlamourExperience(randomValue);
                        person.IncreaseReputation(randomValue * 4);
                        person.IncreaseOfficerMerit(randomValue * 4);
                        if (GameObject.Random(540 / Session.Current.Scenario.Parameters.DayInTurn) == 0)
                        {
                            person.IncreaseKarma(1);
                        }
                        this.BelongedFaction.IncreaseReputation(randomValue * person.MultipleOfCommerceReputation);
                        this.BelongedFaction.IncreaseTechniquePoint((randomValue * person.MultipleOfCommerceTechniquePoint) * 100);
                        this.IncreaseCommerce(randomValue);
                    }
                }
            }
        }

        public void DevelopDay()
        {
            if (this.kind.HasAgriculture)
            {
                this.DevelopAgriculture();
            }
            if (this.kind.HasCommerce)
            {
                this.DevelopCommerce();
            }
            if (this.kind.HasTechnology)
            {
                this.DevelopTechnology();
            }
            if (this.kind.HasDomination)
            {
                this.DevelopDomination();
            }
            if (this.kind.HasMorale)
            {
                this.DevelopMorale();
            }
            if (this.kind.HasEndurance)
            {
                this.DevelopEndurance();
            }
            if (this.kind.HasPopulation)
            {
                this.DevelopPopulation();
            }
            this.DevelopArmy();
            this.ClearWork();
        }

        public void DevelopDayNoFaction()
        {
            this.DevelopPopulation();
        }

        private void DevelopDomination()
        {
            if (this.Domination != this.DominationCeiling)
            {
                foreach (Person person in this.DominationWorkingPersons)
                {
                    if (!person.InternalNoFundNeeded)
                    {
                        if (this.Fund < this.InternalFundCost)
                        {
                            continue;
                        }
                        this.DecreaseFund(this.InternalFundCost);
                    }
                    int randomValue = StaticMethods.GetRandomValue((int)((person.DominationAbility * this.CurrentRateOfInternal) * Session.Parameters.InternalRate), 500 + (150 * (this.AreaCount - 1)));
                    if (randomValue > 0)
                    {
                        person.AddInternalExperience(randomValue * 2);
                        person.AddStrengthExperience(randomValue * 2);
                        person.AddCommandExperience(randomValue);
                        person.AddGlamourExperience(randomValue);
                        person.IncreaseReputation(randomValue * 4);
                        person.IncreaseOfficerMerit(randomValue * 4);
                        if (GameObject.Random((190 - person.PersonalLoyalty * 10) / Session.Current.Scenario.Parameters.DayInTurn) == 0)
                        {
                            person.IncreaseKarma(1);
                        }
                        else if (GameObject.Random((160 + person.PersonalLoyalty * person.PersonalLoyalty * 20) / Session.Current.Scenario.Parameters.DayInTurn) == 0)
                        {
                            person.DecreaseKarma(1);
                        }
                        this.BelongedFaction.IncreaseReputation(randomValue * person.MultipleOfDominationReputation);
                        this.BelongedFaction.IncreaseTechniquePoint((randomValue * person.MultipleOfDominationTechniquePoint) * 100);
                        this.IncreaseDomination(randomValue);
                    }
                }
            }
        }

        private void DevelopEndurance()
        {
            if ((this.Endurance != this.EnduranceCeiling) && ((this.Endurance != 0) || !this.HasContactHostileTroop(this.BelongedFaction)))
            {
                foreach (Person person in this.EnduranceWorkingPersons)
                {
                    if (!person.InternalNoFundNeeded)
                    {
                        if (this.Fund < this.InternalFundCost)
                        {
                            continue;
                        }
                        this.DecreaseFund(this.InternalFundCost);
                    }
                    int randomValue = StaticMethods.GetRandomValue((int)((person.EnduranceAbility * this.CurrentRateOfInternal) * Session.Parameters.InternalRate), 500 + (150 * (this.AreaCount - 1)));
                    if (randomValue > 0)
                    {
                        person.AddInternalExperience(randomValue * 2);
                        person.AddStrengthExperience(randomValue);
                        person.AddCommandExperience(randomValue);
                        person.AddIntelligenceExperience(randomValue);
                        person.AddPoliticsExperience(randomValue);
                        person.IncreaseReputation(randomValue * 4);
                        person.IncreaseOfficerMerit(randomValue * 4);
                        this.BelongedFaction.IncreaseReputation(randomValue * person.MultipleOfEnduranceReputation);
                        this.BelongedFaction.IncreaseTechniquePoint((randomValue * person.MultipleOfEnduranceTechniquePoint) * 100);
                        this.IncreaseEndurance(randomValue);
                    }
                }
            }
        }

        public void DevelopFood()
        {
            this.IncreaseFood(this.ExpectedFood);
        }

        public void DevelopFund()
        {
            this.IncreaseFund(this.ExpectedFund);
        }

        public void DevelopMilitaryPopulation()
        {

        }

        private void DevelopMonth()
        {
            if (this.BelongedFaction != null)
            {
                if (this.Kind.HasAgriculture)
                {
                    this.DevelopFood();
                }
                if (this.Kind.HasCommerce)
                {
                    this.DevelopFund();
                }

            }

        }

        private void DevelopMorale()
        {
            if (this.Morale != this.MoraleCeiling)
            {
                foreach (Person person in this.MoraleWorkingPersons)
                {
                    if (!person.InternalNoFundNeeded)
                    {
                        if (this.Fund < this.InternalFundCost)
                        {
                            continue;
                        }
                        this.DecreaseFund(this.InternalFundCost);
                    }
                    int randomValue = StaticMethods.GetRandomValue((int)((person.MoraleAbility * this.CurrentRateOfInternal) * Session.Parameters.InternalRate), 500 + (150 * (this.AreaCount - 1)));
                    if (randomValue > 0)
                    {
                        person.AddInternalExperience(randomValue * 2);
                        person.AddCommandExperience(randomValue);
                        person.AddPoliticsExperience(randomValue);
                        person.AddGlamourExperience(randomValue * 2);
                        person.IncreaseReputation(randomValue * 4);
                        person.IncreaseOfficerMerit(randomValue * 4);
                        if (GameObject.Random((190 - person.PersonalLoyalty * 10) / Session.Current.Scenario.Parameters.DayInTurn) == 0)
                        {
                            person.IncreaseKarma(1);
                        }
                        else if (GameObject.Random((160 + person.PersonalLoyalty * person.PersonalLoyalty * 20) / Session.Current.Scenario.Parameters.DayInTurn) == 0)
                        {
                            person.DecreaseKarma(1);
                        }
                        this.BelongedFaction.IncreaseReputation(randomValue * person.MultipleOfMoraleReputation);
                        this.BelongedFaction.IncreaseTechniquePoint((randomValue * person.MultipleOfMoraleTechniquePoint) * 100);
                        this.IncreaseMorale(randomValue);
                    }
                }
            }
        }

        private void DevelopPopulation()
        {
            double populationDevelopingRate = this.PopulationDevelopingRate;
            if (populationDevelopingRate != 0.0)
            {
                //this.IncreasePopulation(StaticMethods.GetRandomValue(this.population + (0x3e8 * this.AreaCount), (int) (1.0 / populationDevelopingRate)));
                int pop = StaticMethods.GetBigRandomValue(this.PopulationCeiling + (1000 * this.AreaCount), (int)(1.0 / populationDevelopingRate));
                this.IncreasePopulation(pop);

                float mPop;
                if (this.Population > 500000)
                {
                    mPop = pop / 4;
                }
                else
                {
                    mPop = (int) (pop * (0.25 + (500000 - this.Population) / 500000 * 0.25) * Session.Parameters.MilitaryPopulationReloadQuantity);
                }
                this.IncreaseMilitaryPopulation((int) mPop);
                if (GameObject.Chance((int) ((mPop - (int) mPop) * 100))) {
                    this.IncreaseMilitaryPopulation(1);
                }
            }
        }

        public void DevelopSeason()
        {
            if (this.BelongedFaction != null)
            {

                if (this.Kind.HasPopulation || this.Kind.HasMorale)
                {
                    this.DevelopMilitaryPopulation();
                }
            }
        }

        private void DevelopTechnology()
        {
            if (this.Technology != this.TechnologyCeiling)
            {
                foreach (Person person in this.TechnologyWorkingPersons)
                {
                    if (!person.InternalNoFundNeeded)
                    {
                        if (this.Fund < this.InternalFundCost)
                        {
                            continue;
                        }
                        this.DecreaseFund(this.InternalFundCost);
                    }
                    int randomValue = StaticMethods.GetRandomValue((int)((person.TechnologyAbility * this.CurrentRateOfInternal) * Session.Parameters.InternalRate), 500 + (150 * (this.AreaCount - 1)));
                    if (randomValue > 0)
                    {
                        person.AddInternalExperience(randomValue * 2);
                        person.AddIntelligenceExperience(randomValue * 2);
                        person.AddPoliticsExperience(randomValue * 2);
                        person.IncreaseReputation(randomValue * 4);
                        person.IncreaseOfficerMerit(randomValue * 4);
                        if (GameObject.Random(450 / Session.Current.Scenario.Parameters.DayInTurn) == 0)
                        {
                            person.IncreaseKarma(1);
                        }
                        this.BelongedFaction.IncreaseReputation(randomValue * person.MultipleOfTechnologyReputation);
                        this.BelongedFaction.IncreaseTechniquePoint((randomValue * person.MultipleOfTechnologyTechniquePoint) * 100);
                        this.IncreaseTechnology(randomValue);
                    }
                }
            }
        }

        public void DevelopYear()
        {
        }

        private void AIDiplomaticTactics()
        {
           // this.AIQuanXiang();
            this.AIGeDi();
        }

        private void AIGeDi()
        {
            return;
#pragma warning disable CS0162 // Unreachable code detected
            if (!this.HasHostileTroopsInView()) return;
#pragma warning restore CS0162 // Unreachable code detected

            if (this.MovablePersons.Count > 0)
            {
                TroopList hostileTroopInView = this.GetHostileTroopsInView();
                TroopList friendlyTroopsInView = this.GetFriendlyTroopsInView();
                int hostileFightingForce = 0;
                int friendlyFightingForce = 0;

                foreach (Troop t in hostileTroopInView)
                {
                    hostileFightingForce += t.FightingForce;
                }
                foreach (Troop t in friendlyTroopsInView)
                {
                    friendlyFightingForce += t.FightingForce;
                }

                if (hostileFightingForce > friendlyFightingForce * (this.BelongedFaction.Leader.PersonalLoyalty + 1) &&
                    GameObject.Chance(40 - this.BelongedFaction.Leader.PersonalLoyalty * 10))
                {
                    Person shizhe = this.MovablePersons[Random(this.MovablePersons.Count)] as Person;
                    DiplomaticRelationDisplay display = this.GetGeDiDiplomaticRelationList()[GameObject.Random(this.GetGeDiDiplomaticRelationList().Count)] as DiplomaticRelationDisplay; ;

                    shizhe.GoToGeDiDiplomatic(display);

                }
            }
        }


        /*
        private void AIQuanXiang()
        {
            if (Session.Current.Scenario.IsPlayer(this.BelongedFaction)) return;

            if (!Session.GlobalVariables.PermitQuanXiang) return;

            if (!GameObject.Chance(50)) return ;

            if (this.QuanXiangDiplomaticRelationAvail())
            {
                PersonList pl = new PersonList();
                DiplomaticRelationDisplay display = this.GetAIQuanXiangDiplomaticRelationList()[GameObject.Random(this.GetAIQuanXiangDiplomaticRelationList().Count)] as DiplomaticRelationDisplay;
                foreach (Person person in this.MovablePersons)
                {
                    if (person.Loyalty >= 100 && person.WorkKind == ArchitectureWorkKind.无
                        && person.Intelligence >= 60 && person.Politics >= 60)
                    {
                        pl.Add(person);
                    }
                }

                if (pl.Count > 1)
                {
                    foreach (Person p in pl)
                    {
                        if (this.Fund >= 50000)
                        {
                            this.Fund -= 50000;

                            p.GoToQuanXiangDiplomatic(display);
                        }
                        //throw new Exception("劝降目标势力" + display.FactionName + "劝降武将" + p.Name + p.ID );
                    }
                }

            }
        }*/

        /*
        private void DiplomaticRelationAI()
        {
            if (((this.PlanArchitecture == null) || GameObject.Chance(10)) && (this.BelongedFaction != null))
            {
            }
        }*/

        public bool DisbandAvail()
        {
            return ((this.Militaries.Count > 0) && this.Kind.HasPopulation);
        }

        public void DisbandMilitary(Military m)
        {
            if (!m.IsTransport)
            {
                this.IncreasePopulation(m.Quantity);
                this.IncreaseMilitaryPopulation(m.Quantity);
            }
            this.RemoveMilitary(m);
            this.BelongedFaction.RemoveMilitary(m);
            Session.Current.Scenario.Militaries.Remove(m);
            ExtensionInterface.call("DisbandMilitary", new Object[] { Session.Current.Scenario, this, m });
        }

        public bool DisbandSectionAvail()
        {
            return (this.BelongedFaction.SectionCount > 1);
        }

        public bool DominationAvail()
        {
            return (this.Kind.HasDomination && this.HasPerson());
        }

        public string DominationInInformationLevel(InformationLevel level)
        {
            switch (level)
            {
                case InformationLevel.未知:
                    return "----";

                case InformationLevel.无:
                    return "----";

                case InformationLevel.低:
                    return StaticMethods.GetNumberStringByGranularity(this.Domination, 20);

                case InformationLevel.中:
                    return StaticMethods.GetNumberStringByGranularity(this.Domination, 10);

                case InformationLevel.高:
                    return StaticMethods.GetNumberStringByGranularity(this.Domination, 5);

                case InformationLevel.全:
                    return this.Domination.ToString();
            }
            return "----";
        }

        public bool EnduranceAvail()
        {
            return (this.Kind.HasEndurance && this.HasPerson());
        }

        public string EnduranceInInformationLevel(InformationLevel level)
        {
            switch (level)
            {
                case InformationLevel.未知:
                    return "----";

                case InformationLevel.无:
                    return "----";

                case InformationLevel.低:
                    return StaticMethods.GetNumberStringByGranularity(this.Endurance, 500);

                case InformationLevel.中:
                    return StaticMethods.GetNumberStringByGranularity(this.Endurance, 200);

                case InformationLevel.高:
                    return StaticMethods.GetNumberStringByGranularity(this.Endurance, 100);

                case InformationLevel.全:
                    return this.Endurance.ToString();
            }
            return "----";
        }

        public bool FacilityBuildable(FacilityKind facilityKind)
        {
            return facilityKind.CanBuild(this) && this.Fund >= facilityKind.FundCost && this.FacilityPositionLeft >= facilityKind.PositionOccupied;
        }

        private void FacilityDoWork()
        {
            foreach (Facility facility in this.Facilities)
            {
                if (this.FacilityEnabled || facility.MaintenanceCost <= 0)
                {
                    facility.DoWork(this);
                }
            }
        }

        private void FacilityMaintenance()
        {
            int facilityMaintenanceCost = noFundToSustainFacility ? 0 : this.FacilityMaintenanceCost;
            if (this.Fund >= facilityMaintenanceCost)
            {
                this.ApplyFacilityInfluences(true);
                this.FacilityEnabled = true;
                this.DecreaseFund(facilityMaintenanceCost);
            }
            else
            {
                this.PurifyFacilityInfluences();
                this.FacilityEnabled = false;
            }
        }

        private void FacilityRecovery()
        {
            if (this.FacilityEnabled)
            {
                this.Facilities.RecoverEndurance(this.facilityEnduranceIncrease);
            }
        }

        public bool FactionHasCaptive()
        {
            return ((this.BelongedFaction != null) ? this.BelongedFaction.HasCaptive() : false);
        }

        public bool FactionHasSelfCaptive()
        {
            return ((this.BelongedFaction != null) ? this.BelongedFaction.HasSelfCaptive() : false);
        }

        public bool FactionHasTreasure()
        {
            if (this.BelongedFaction != null)
            {
                foreach (Person person in this.BelongedFaction.Persons)
                {
                    if (person.TreasureCount > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool FindRouteway(LinkNode node, bool hasEnd, out float rate)
        {
            rate = 1f;
            Point key = new Point(base.ID, node.A.ID);
            if (!this.BelongedFaction.ClosedRouteways.ContainsKey(key))
            {
                Point? nullable;
                Point? nullable2;
                Session.Current.Scenario.GetClosestPointsBetweenTwoAreas(this.GetRoutewayStartPoints(), node.A.GetAIRoutewayEndPoints(this, false), out nullable, out nullable2);
                if (nullable.HasValue && nullable2.HasValue)
                {
                    this.BelongedFaction.RoutewayPathBuilder.MultipleWaterCost = node.Kind == LinkKind.Land;
                    if (this.BelongedFaction.RoutewayPathAvail(nullable.Value, nullable2.Value, hasEnd))
                    {
                        rate = this.BelongedFaction.RoutewayPathBuilder.PathConsumptionRate;
                        return true;
                    }
                }
            }
            return false;
        }

        public void FoodReduce()
        {
            this.DecreaseFood((int)(this.Food * this.FoodReduceDayRate));
        }

        public void FundPacksDayEvent()
        {
            for (int i = this.FundPacks.Count - 1; i >= 0; i--)
            {
                FundPack local1 = this.FundPacks[i];

                if (local1.Days <= 1 && this.IsSurrounded()) continue;

                local1.Days--;
                if (local1.Days <= 0)
                {
                    this.IncreaseFund(local1.Fund);
                    this.FundPacks.RemoveAt(i);
                }
            }
        }

        public void FoodPacksDayEvent()
        {
            for (int i = this.FoodPacks.Count - 1; i >= 0; i--)
            {
                FoodPack local1 = this.FoodPacks[i];

                if (local1.Days <= 1 && this.IsSurrounded()) continue;

                local1.Days--;
                if (local1.Days <= 0)
                {
                    this.IncreaseFood(local1.Food);
                    this.FoodPacks.RemoveAt(i);
                }
            }
        }

        public void GenerateAllAILinkNodes(int levelMax)
        {
            this.AILinkProcedureDetails.Clear();
            this.AIAllLinkNodes.Clear();
            List<Architecture> path = new List<Architecture>();
            this.AILinkProcedureDetails.Enqueue(new AILinkProcedureDetail(0, this, path));
            while (this.AILinkProcedureDetails.Count > 0)
            {
                AILinkProcedureDetail detail = this.AILinkProcedureDetails.Dequeue();
                this.AddAllAILink(detail.Level, levelMax, detail.A, detail.Path);
            }
            foreach (LinkNode node in this.AIAllLinkNodes.Values)
            {
                node.Kind = this.CheckCampaignable(node);
            }
        }

        public GameObjectList GetAILinks()
        {
            GameObjectList list = this.AILandLinks.GetList();
            foreach (Architecture architecture in this.AIWaterLinks)
            {
                if (list.GetGameObject(architecture.ID) == null)
                {
                    list.Add(architecture);
                }
            }
            return list;
        }

        public GameObjectList GetAILinks(int level)
        {
            GameObjectList list = new GameObjectList();
            foreach (LinkNode node in this.AIAllLinkNodes.Values)
            {
                if (node.Level <= level)
                {
                    list.Add(node.A);
                }
            }
            return list;
        }

        public GameArea GetAIRoutewayEndPoints(Architecture a, bool nowater)
        {
            GameArea area = new GameArea();
            if (!this.IsFriendly(a.BelongedFaction))
            {
                foreach (Point point in this.ContactArea.Area)
                {
                    if (a.IsRoutewayPossible(point) && (!nowater || (Session.Current.Scenario.GetTerrainKindByPosition(point) != TerrainKind.水域)))
                    {
                        area.AddPoint(point);
                    }
                }
            }
            if (area.Count == 0)
            {
                foreach (Point point in this.GetRoutewayStartArea().Area)
                {
                    if (a.IsRoutewayPossible(point) && (!nowater || (Session.Current.Scenario.GetTerrainKindByPosition(point) != TerrainKind.水域)))
                    {
                        area.AddPoint(point);
                    }
                }
            }
            if (area.Count == 0)
            {
                foreach (Point point in this.LongViewArea.Area)
                {
                    if (a.IsRoutewayPossible(point))
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }

        public GameArea GetAllAvailableArea(bool Square)
        {
            GameArea area = new GameArea();
            foreach (Point point in this.ContactArea.Area)
            {
                if (Session.Current.Scenario.IsPositionEmpty(point) && Session.Current.Scenario.GetTerrainDetailByPosition(point) != null && Session.Current.Scenario.GetTerrainDetailByPosition(point).RoutewayConsumptionRate < 1)
                {
                    area.AddPoint(point);
                }
            }
            foreach (Point point in this.ArchitectureArea.Area)
            {
                if (!Session.Current.Scenario.PositionIsTroop(point) && Session.Current.Scenario.GetTerrainDetailByPosition(point) != null && Session.Current.Scenario.GetTerrainDetailByPosition(point).RoutewayConsumptionRate < 1)
                {
                    area.AddPoint(point);
                }
            }
            return area;
        }

        public GameArea GetAllContactArea()
        {
            GameArea area = new GameArea();
            foreach (Point point in this.ContactArea.Area)
            {
                area.AddPoint(point);
            }
            foreach (Point point in this.ArchitectureArea.Area)
            {
                area.AddPoint(point);
            }
            return area;
        }

        public PersonList GetAllPersons()
        {
            PersonList list = new PersonList();
            foreach (Person person in this.Persons)
            {
                list.Add(person);
            }
            foreach (Person person in this.MovingPersons)
            {
                list.Add(person);
            }
            return list;
        }

        public TreasureList GetAllTreasureInArchitecture()
        {
            TreasureList list = new TreasureList();
            foreach (Person person in this.GetAllPersons())
            {
                person.AddTreasureToList(list);
            }
            return list;
        }

        public TreasureList GetAllTreasureInArchitectureExceptLeader()
        {
            TreasureList list = new TreasureList();
            if (this.BelongedFaction != null)
            {
                foreach (Person person in this.Persons)
                {
                    if (person != this.BelongedFaction.Leader)
                    {
                        person.AddTreasureToList(list);
                    }
                }
            }
            return list;
        }

        public TreasureList GetAllTreasureInFaction()
        {
            TreasureList list = new TreasureList();
            if (this.BelongedFaction != null)
            {
                foreach (Person person in this.BelongedFaction.Persons)
                {
                    person.AddTreasureToList(list);
                }
            }
            return list;
        }

        public GameArea GetAvailableContactArea(bool Square)
        {
            GameArea area = new GameArea();
            foreach (Point point in this.ContactArea.Area)
            {
                if (Session.Current.Scenario.IsPositionEmpty(point))
                {
                    area.AddPoint(point);
                }
            }
            if (area.Count > 0)
            {
                return area;
            }
            return null;
        }

        public MilitaryList GetBeMergedMilitaryList(Military military)
        {
            this.BeMergedMilitaryList.Clear();
            foreach (Military military2 in this.MergeMilitaryList)
            {
                if ((military2 != military) && (military2.Kind.Equals(military.Kind)))
                {
                    this.BeMergedMilitaryList.Add(military2);
                }
            }
            return this.BeMergedMilitaryList;
        }

        public GameObjectList GetBuildableFacilityKindList()
        {
            this.BuildableFacilityKindList.Clear();
            foreach (FacilityKind kind in Session.Current.Scenario.GameCommonData.AllFacilityKinds.FacilityKinds.Values)
            {
                if (this.BelongedFaction != null && !this.BelongedFaction.hougongValid && kind.rongna > 0 && kind.InfluenceCount == 0) continue;
                if (this.FacilityBuildable(kind))
                {
                    this.BuildableFacilityKindList.Add(kind);
                }
            }
            return this.BuildableFacilityKindList;
        }

        public MilitaryList GetCampaignMilitaryList()
        {
            this.CampaignMilitaryList.Clear();
            foreach (Military military in this.Militaries)
            {
                if ((military.Quantity > 0) && (military.Morale > 0))
                {
                    this.CampaignMilitaryList.AddMilitary(military);
                }
            }
            return this.CampaignMilitaryList;
        }

        public Point? GetCampaignPosition(Troop troop, List<Point> orientations, bool close)
        {
            GameArea allAvailableArea = this.GetAllAvailableArea(false);
            GameArea sourceArea = new GameArea();
            foreach (Point point in allAvailableArea.Area)
            {
                if ((Session.Current.Scenario.GetArchitectureByPosition(point) == this || troop.IsMovableOnPosition(point)) && Session.Current.Scenario.GetTroopByPosition(point) == null)
                {
                    sourceArea.Area.Add(point);
                }
            }
            GameArea highestFightingForceArea = troop.GetHighestFightingForceArea(sourceArea);
            if (highestFightingForceArea != null)
            {
                if (close)
                {
                    return Session.Current.Scenario.GetClosestPosition(highestFightingForceArea, orientations);
                }
                return Session.Current.Scenario.GetFarthestPosition(highestFightingForceArea, orientations);
            }
            return null;
        }

        public ArchitectureList GetChangeCapitalArchitectureList()
        {
            this.ChangeCapitalArchitectureList.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (Architecture architecture in this.BelongedFaction.Architectures)
                {
                    if (architecture != this)
                    {
                        this.ChangeCapitalArchitectureList.Add(architecture);
                    }
                }
            }
            return this.ChangeCapitalArchitectureList;
        }

        public void GetClosestArchitectures()
        {
            this.ClosestArchitectures = new ArchitectureList();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if (architecture != this)
                {
                    this.ClosestArchitectures.Add(architecture);
                }
            }
            this.QuickSortArchitecturesDistance(this.ClosestArchitectures, 0, this.ClosestArchitectures.Count - 1);
        }

        public ArchitectureList GetClosestArchitectures(int count, double maxDistance)
        {
            if (this.ClosestArchitectures == null)
            {
                this.GetClosestArchitectures();
            }
            ArchitectureList list = new ArchitectureList();
            if (count > this.ClosestArchitectures.Count)
            {
                count = this.ClosestArchitectures.Count;
            }
            for (int i = 0; i < count; i++)
            {
                if (Session.Current.Scenario.GetDistance(this.ArchitectureArea, (this.ClosestArchitectures[i] as Architecture).ArchitectureArea) <= maxDistance)
                {
                    list.Add(this.ClosestArchitectures[i]);
                }
                else
                {
                    break;
                }
            }
            if (list.Count == 0)
            {
                list.Add(this.ClosestArchitectures[0]);
            }
            return list;
        }

        public ArchitectureList GetClosestArchitectures(int count)
        {
            return GetClosestArchitectures(count, double.MaxValue);
        }

        public Routeway GetConnectedRouteway(Architecture end)
        {
            foreach (Routeway routeway in this.Routeways)
            {
                if ((routeway.EndArchitecture == end) && routeway.IsActive)
                {
                    return routeway;
                }
            }
            return null;
        }
        //[DataMember]
        public PersonList ConvinceDestinationPersonList = new PersonList();
        public PersonList GetConvinceDestinationPersonList(Faction faction)
        {
            PersonList result = new PersonList();
            if (this.BelongedFaction == faction)
            {
                foreach (Captive captive in this.Captives)
                {
                    result.Add(captive.CaptivePerson);
                }
            }
            else
            {
                foreach (Person person in this.Persons)
                {
                    result.Add(person);
                }
            }
            foreach (Person person in this.NoFactionPersons)
            {
                result.Add(person);
            }
            ConvinceDestinationPersonList = result;
            return result;
        }

        public bool CanMoveFeizi()
        {
            if (this.movableFeizis.Count <= 0) return false;
            //if (this.HasHostileTroopsInView()) return false;
            if (this.BelongedFaction == null) return false;

            foreach (Architecture a in this.BelongedFaction.Architectures)
            {
                if ((a.Meinvkongjian > a.Feiziliebiao.Count || a.BelongedFaction.IsAlien) && a != this && !a.HasHostileTroopsInView())
                {
                    return true;
                }
            }

            return false;
        }

        public GameArea GetFeiziTransferArchitectureArea()
        {
            GameArea area = new GameArea();

            foreach (Architecture architecture in this.BelongedFaction.Architectures)
            {

                if (architecture == this)
                {
                    continue;
                }

                if (architecture.Meinvkongjian <= architecture.Feiziliebiao.Count && !architecture.BelongedFaction.IsAlien)
                {
                    continue;
                }

                foreach (Point point in architecture.ArchitectureArea.Area)
                {
                    area.AddPoint(point);
                }
            }

            return area;
        }

        public PersonList ReleasableFeizis
        {
            get
            {
                PersonList list = new PersonList();
                foreach (Person p in this.Feiziliebiao)
                {
                    if (!this.BelongedFaction.Leader.suoshurenwuList.HasGameObject(p))
                    {
                        list.Add(p);
                    }
                }
                return list;
            }
        }

        public bool HaveReleasableFeizis()
        {
            return ReleasableFeizis.Count > 0;
        }

        public bool MoveCaptiveAvail() //俘虏可移动
        {
            if (this.movableCaptives.Count <= 0) return false;
            foreach (Architecture a in this.BelongedFaction.Architectures)
            {
                if (a != this)
                {
                    return true;
                }
            }

            return false;

        }

        public PersonList movableCaptives //俘虏列表
        {
            get
            {
                PersonList movableCaptives = new PersonList();

                if (this.HasHostileTroopsInView())
                {
                    return movableCaptives;
                }

                foreach (Captive captive in this.Captives)
                {
                    if (captive.CaptivePerson.ArrivingDays <= 0)
                    {
                        movableCaptives.Add(captive.CaptivePerson);
                    }

                }
                return movableCaptives;
            }
        }

        public GameArea GetCaptiveTransferArchitectureArea() //俘虏可移动
        {
            GameArea area = new GameArea();

            foreach (Architecture architecture in this.BelongedFaction.Architectures)
            {

                if (architecture == this)
                {
                    continue;
                }

                if (architecture.HasHostileTroopsInView())
                {
                    continue;
                }

                foreach (Point point in architecture.ArchitectureArea.Area)
                {
                    area.AddPoint(point);
                }
            }

            return area;
        }

        public bool TransferMilitaryAvail()  //运输编队
        {
            if (this.BelongedFaction == null) return false;
            if (this.movableMilitaries.Count <= 0) return false;
            if (this.HasHostileTroopsInView()) return false;

            foreach (Architecture a in this.BelongedFaction.Architectures)
            {
                if (a != this)
                {
                    return true;
                }
            }
            return false ;
        }

        public MilitaryList movableMilitaries
        {
            get
            {
                MilitaryList movableMilitaries = new MilitaryList();
                foreach (Military i in this.Militaries)
                {
                    if (!i.Kind.IsTransport && i.Quantity > 0 && i.Kind.Movable)
                    {
                        movableMilitaries.Add(i);
                    }
                }
                return movableMilitaries;
            }
        }
        /*
        public GameArea GetMilitaryTransferArchitectureArea()
        {
            GameArea area = new GameArea();

            foreach (Architecture architecture in this.BelongedFaction.Architectures)
            {

                if (architecture == this)
                {
                    continue;
                }


                foreach (Point point in architecture.ArchitectureArea.Area)
                {
                    area.AddPoint(point);
                }


            }
            return area;
        }
        */
        public GameArea GetPersonTransferArchitectureArea()
        {

            GameArea area = new GameArea();

            foreach (Architecture architecture in this.BelongedFaction.Architectures)
            {

                if (architecture == this)
                {
                    continue;
                }


                foreach (Point point in architecture.ArchitectureArea.Area)
                {
                    area.AddPoint(point);
                }


            }
            return area;
        }


        public GameArea GetConvincePersonArchitectureArea()
        {
            GameArea area = new GameArea();
            //Label_0121:
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if (architecture.BelongedFaction == this.BelongedFaction)
                {
                    if (!architecture.HasCaptive() && !architecture.HasNoFactionPerson())
                    {
                        //goto Label_0121;
                        continue;
                    }
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        area.AddPoint(point);
                    }
                }
                else
                {
                    if ((!architecture.HasPerson() && !architecture.HasNoFactionPerson()) || !this.BelongedFaction.IsArchitectureKnown(architecture))
                    {
                        continue;
                    }
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }

        public GameArea GetDestroyArchitectureArea()
        {
            GameArea area = new GameArea();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if (!this.IsFriendly(architecture.BelongedFaction) && architecture.BelongedFaction != null)
                {
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }

        public int GetDistanceFromFaction(Faction faction)
        {
            if ((faction == null) || (faction.ArchitectureCount == 0))
            {
                return 0;
            }
            if (this.BelongedFaction == faction)
            {
                return 0;
            }
            if (this.ClosestArchitectures == null)
            {
                this.GetClosestArchitectures();
            }
            int num = 0;
            for (int i = 0; i < this.ClosestArchitectures.Count; i++)
            {
                if ((this.ClosestArchitectures[i] as Architecture).BelongedFaction == faction)
                {
                    num += (i * (this.ClosestArchitectures[i] as Architecture).Population) / 0x2710;
                }
            }
            if (this.BelongedFaction != null)
            {
                int diplomaticRelation = Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, faction.ID);
                if (diplomaticRelation <= -200)
                {
                    num /= 4;
                }
                else if (diplomaticRelation < 0)
                {
                    num /= 1;
                }
            }
            return (num / faction.ArchitectureCount);
        }

        public int GetDistanceFromSection(Section section)
        {
            if ((section == null) || (section.ArchitectureCount == 0))
            {
                return 0;
            }
            if (this.BelongedSection == section)
            {
                return 0;
            }
            int num = 0;
            foreach (Architecture architecture in section.Architectures)
            {
                LinkNode node = null;
                this.AIAllLinkNodes.TryGetValue(architecture.ID, out node);
                if (node != null)
                {
                    num += (int)(node.Level * node.Distance);
                }
                else
                {
                    num += 0x3e8;
                }
                num -= architecture.Population / 0x2710;
            }
            return (num / section.ArchitectureCount);
        }

        public Routeway GetExistingRouteway(Architecture destination)
        {
            foreach (Routeway routeway in this.Routeways)
            {
                if (routeway.DestinationArchitecture == destination)
                {
                    return routeway;
                }
            }
            return null;
        }

        public Captive GetLowestLoyaltyCaptiveRecruitable()
        {
            GameObjectList list = this.Captives.GetRandomList();
            int lowestLoyalty = int.MaxValue;
            Captive target = null;
            if (list.Count > 0)
            {
                foreach (Captive c in list)
                {
                    int idealOffset = Person.GetIdealOffset(c.CaptivePerson, this.BelongedFaction.Leader);
                    if ((!Session.GlobalVariables.IdealTendencyValid || (idealOffset <= c.CaptivePerson.IdealTendency.Offset + (double)this.BelongedFaction.Reputation / Session.Current.Scenario.Parameters.MaxReputationForRecruit * 75))
                        && (!c.CaptivePerson.Hates(this.BelongedFaction.Leader)) && (!this.BelongedFaction.IsAlien || c.CaptivePerson.PersonalLoyalty < 2))
                    {
                        if (c.CaptivePerson.Loyalty < lowestLoyalty)
                        {
                            target = c;
                            lowestLoyalty = c.CaptivePerson.Loyalty;
                        }
                    }
                }
            }
            return target;
        }

        public Captive GetExtremeLoyaltyCaptive(bool low)
        {
            GameObjectList list = this.Captives.GetList();
            if (list.Count > 0)
            {
                if (list.Count > 1)
                {
                    list.PropertyName = "Loyalty";
                    list.IsNumber = true;
                    list.SmallToBig = low;
                    list.ReSort();
                }
                return (list[0] as Captive);
            }
            return null;
        }

        public Person GetLowestLoyaltyPersonRecruitable()
        {
            GameObjectList list = this.Persons.GetRandomList();
            int lowestLoyalty = int.MaxValue;
            Person target = null;
            if (list.Count > 0)
            {
                foreach (Person c in list)
                {
                    int idealOffset = Person.GetIdealOffset(c, this.BelongedFaction.Leader);
                    if ((!Session.GlobalVariables.IdealTendencyValid || (idealOffset <= c.IdealTendency.Offset + (double)this.BelongedFaction.Reputation / Session.Current.Scenario.Parameters.MaxReputationForRecruit * 75))
                        && (!c.Hates(this.BelongedFaction.Leader)) && (!this.BelongedFaction.IsAlien || c.PersonalLoyalty < 2) &&
                        (!c.ProhibitedFactionID.ContainsValue(this.BelongedFaction.ID)) && c.GetRelation(c.VeryClosePersonInArchitecture) < 500)
                    {
                        if (c.Loyalty < lowestLoyalty)
                        {
                            target = c;
                            lowestLoyalty = c.Loyalty;
                        }
                    }
                }
            }
            return target;
        }

        public Person GetExtremeLoyaltyPerson(bool low)
        {
            GameObjectList list = this.Persons.GetList();
            if (list.Count > 0)
            {
                if (list.Count > 1)
                {
                    list.PropertyName = "Loyalty";
                    list.IsNumber = true;
                    list.SmallToBig = low;
                    list.ReSort();
                }
                return (list[0] as Person);
            }
            return null;
        }

        public Person GetExtremePersonFromWorkingList(ArchitectureWorkKind workKind, bool highest)  //大概是选择在冒泡小窗口说话的人
        {
            PersonList agricultureWorkingPersons = null;
            int num2;
            int num3;
            int workAbility;
            switch (workKind)
            {
                case ArchitectureWorkKind.赈灾:
                    agricultureWorkingPersons = this.ZhenzaiWorkingPersons;
                    break;
                case ArchitectureWorkKind.训练:
                    agricultureWorkingPersons = this.TrainingWorkingPersons;
                    break;
                case ArchitectureWorkKind.农业:
                    agricultureWorkingPersons = this.AgricultureWorkingPersons;
                    break;

                case ArchitectureWorkKind.商业:
                    agricultureWorkingPersons = this.CommerceWorkingPersons;
                    break;

                case ArchitectureWorkKind.技术:
                    agricultureWorkingPersons = this.TechnologyWorkingPersons;
                    break;

                case ArchitectureWorkKind.统治:
                    agricultureWorkingPersons = this.DominationWorkingPersons;
                    break;

                case ArchitectureWorkKind.民心:
                    agricultureWorkingPersons = this.MoraleWorkingPersons;
                    break;

                case ArchitectureWorkKind.耐久:
                    agricultureWorkingPersons = this.EnduranceWorkingPersons;
                    break;

                default:
                    return null;
            }
            if (agricultureWorkingPersons.Count == 0)
            {
                return null;
            }
            if (agricultureWorkingPersons.Count == 1)
            {
                return (agricultureWorkingPersons[0] as Person);
            }
            if (highest)
            {
                int num = 0;
                num2 = 0;
                for (num3 = 0; num3 < agricultureWorkingPersons.Count; num3++)
                {
                    workAbility = (agricultureWorkingPersons[num3] as Person).GetWorkAbility(workKind);
                    if (workAbility > num)
                    {
                        num = workAbility;
                        num2 = num3;
                    }
                }
                return (agricultureWorkingPersons[num2] as Person);
            }
            int num5 = 0x7fffffff;
            num2 = 0;
            for (num3 = 0; num3 < agricultureWorkingPersons.Count; num3++)
            {
                workAbility = (agricultureWorkingPersons[num3] as Person).GetWorkAbility(workKind);
                if (workAbility < num5)
                {
                    num5 = workAbility;
                    num2 = num3;
                }
            }
            return (agricultureWorkingPersons[num2] as Person);
        }

        public InformationKind GetFirstHalfInformationKind()
        {
            InformationKindList list = new InformationKindList();
            foreach (InformationKind kind in Session.Current.Scenario.GameCommonData.AllInformationKinds.GetAvailList(this))
            {
                if ((kind.Level <= InformationLevel.中) || GameObject.Chance(20))
                {
                    list.Add(kind);
                }
            }
            if (list.Count > 0)
            {
                if (list.Count > 1)
                {
                    list.PropertyName = "FightingWeighing";
                    list.IsNumber = true;
                    list.ReSort();
                }
                return (list[GameObject.Random(list.Count / 2)] as InformationKind);
            }
            return null;
        }

        private Person GetFirstHalfPerson(string propertyName)
        {
            GameObjectList list = this.MovablePersons.GetList();
            if (list.Count > 0)
            {
                if (list.Count > 1)
                {
                    list.PropertyName = propertyName;
                    list.IsNumber = true;
                    list.ReSort();
                }
                return (list[GameObject.Random(list.Count / 2)] as Person);
            }
            return null;
        }

        public int GetFriendlyTroopFightingForceInView()
        {
            int num = 0;
            foreach (Point point in this.LongViewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if (((troopByPosition != null) && (troopByPosition.BelongedFaction != null)) && this.BelongedFaction.IsFriendly(troopByPosition.BelongedFaction))
                {
                    num += troopByPosition.FightingForce;
                }
            }
            return num;
        }

        public TroopList GetFriendlyTroopsInView()
        {
            GameArea longViewArea = this.LongViewArea;
            TroopList list = new TroopList();
            foreach (Point point in longViewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && troopByPosition.IsFriendly(this.BelongedFaction))
                {
                    list.Add(troopByPosition);
                }
            }
            return list;
        }

        public int GetGossipablePersonCount()
        {
            int num = 0;
            foreach (Person person in this.Persons)
            {
                if ((person.Loyalty <= 100) && (person != this.BelongedFaction.Leader))
                {
                    num++;
                }
            }
            return num;
        }

        public GameArea GetGossipArchitectureArea()
        {
            GameArea area = new GameArea();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if ((((architecture.BelongedFaction != null) && !this.IsFriendly(architecture.BelongedFaction)) && architecture.HasPerson()) && this.BelongedFaction.IsArchitectureKnown(architecture))
                {
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }

        public int DefendAssassinateAbility
        {
            get
            {
                int targetAbility = 0;
                foreach (Person p in this.Persons)
                {
                    if (p.AssassinateAbility > targetAbility)
                    {
                        targetAbility = p.AssassinateAbility;
                    }
                }

                return targetAbility;
            }
        }

        public PersonList GetAssassinatePersonTarget(Faction from)
        {
            PersonList pl = new PersonList();
            foreach (Person p in this.AssassinatablePersons(from))
            {
                pl.Add(p);
            }
            return pl;
        }

        public GameArea GetAssassinateArchitectureArea(Faction from)
        {
            GameArea area = new GameArea();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if (architecture.BelongedFaction != null &&
                    (architecture.AssassinatablePersons(from).Count > 0) && this.BelongedFaction.IsArchitectureKnown(architecture))
                {
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }

        public GameArea GetJailBreakArchitectureArea()
        {
            GameArea area = new GameArea();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if (architecture.BelongedFaction != null && !this.IsFriendly(architecture.BelongedFaction) && architecture.HasFactionCaptive(this.BelongedFaction))
                {
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }

        public TroopList GetHostileTroopsInView()
        {
            GameArea viewArea = this.ViewArea;
            if (this.Endurance < this.EnduranceCeiling / 2)
            {
                viewArea = this.LongViewArea;
            }
            TroopList list = new TroopList();
            foreach (Point point in viewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && (!troopByPosition.IsFriendly(this.BelongedFaction) && (troopByPosition.Status != TroopStatus.埋伏)))
                {
                    int days = 1;
                    if ((((this.BelongedFaction != null) && (troopByPosition.BelongedFaction != null)) && (this.RecentlyAttacked <= 0)) && (Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, troopByPosition.BelongedFaction.ID) >= 0))
                    {
                        days = 0;
                    }
                    if (troopByPosition.DaysToReachPosition(Session.Current.Scenario.GetClosestPoint(this.ArchitectureArea, troopByPosition.Position), days))
                    {
                        list.Add(troopByPosition);
                    }
                }
            }
            return list;
        }


        public bool FindHostileTroopInView()
        {


            GameArea viewArea = this.LongViewArea;


            foreach (Point point in viewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && !troopByPosition.IsTransport && (!troopByPosition.IsFriendly(this.BelongedFaction) && (troopByPosition.Status != TroopStatus.埋伏)))
                {
                    return true;
                }
            }
            return false;
        }

        public GameArea GetInstigateArchitectureArea()
        {
            GameArea area = new GameArea();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if (architecture.Kind.HasDomination && !this.IsFriendly(architecture.BelongedFaction) && architecture.BelongedFaction != null)
                {
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }

        public MilitaryList GetLevelUpMilitaryList()
        {
            this.LevelUpMilitaryList.Clear();
            Dictionary<MilitaryKind, bool> canLevelUp = new Dictionary<MilitaryKind, bool>();
            foreach (Military military in this.Militaries)
            {
                bool hasLevelupable = false;
                foreach (int id in military.Kind.LevelUpKindID)
                {
                    bool flag = false;
                    if (canLevelUp.ContainsKey(military.Kind))
                    {
                        flag = canLevelUp[military.Kind];
                    }
                    else
                    {
                        flag = military.Kind.LevelUpAvail(this);
                        canLevelUp[military.Kind] = flag;
                    }
                    if (flag)
                    {
                        hasLevelupable = true;
                        break;
                    }
                }

                if (((military.InjuryQuantity == 0) && military.Kind != null && military.Kind.CanLevelUp) && (military.Experience >= military.Kind.LevelUpExperience)
                    && hasLevelupable)
                {
                    military.BelongedFaction = this.BelongedFaction;
                    this.LevelUpMilitaryList.AddMilitary(military);
                }
            }
            return this.LevelUpMilitaryList;
        }

        public MilitaryList GetMergeMilitaryList()
        {
            this.MergeMilitaryList.Clear();
            for (int i = 0; i < this.Militaries.Count; i++)
            {
                Military t = this.Militaries[i] as Military;
                if ((t.Quantity != t.Kind.MaxScale) && (t.InjuryQuantity <= 0))
                {
                    foreach (Military military2 in this.Militaries)
                    {
                        if (((t != military2) && (t.Kind.Equals(military2.Kind))) && ((military2.Quantity < military2.Kind.MaxScale) && (military2.InjuryQuantity == 0)))
                        {
                            this.MergeMilitaryList.Add(t);
                            break;
                        }
                    }
                }
            }
            return this.MergeMilitaryList;
        }

        public GameArea GetMilitaryCampaignArea(Military military)
        {
            GameArea allAvailableArea = this.GetAllAvailableArea(false);
            military.ModifyAreaByTerrainAdaptablity(allAvailableArea);
            return allAvailableArea;
        }

        public MilitaryKindList GetUpgradableMilitaryKindList(Military m)
        {
            this.UpgradableMilitaryKindList.Clear();
            foreach (MilitaryKind mk in m.Kind.GetLevelUpKinds(this))
            {
                this.UpgradableMilitaryKindList.Add(mk);
            }
            return this.UpgradableMilitaryKindList;
        }

        public MilitaryKindList GetNewMilitaryKindList()
        {
            this.NewMilitaryKindList.Clear();
            foreach (MilitaryKind kind in this.BelongedFaction.AvailableMilitaryKinds.MilitaryKinds.Values)
            {
                if (kind.CreateAvail(this))
                {
                    this.NewMilitaryKindList.Add(kind);
                }
            }
            foreach (MilitaryKind kind in this.PrivateMilitaryKinds.MilitaryKinds.Values)
            {
                if (kind.CreateAvail(this) && !this.NewMilitaryKindList.GameObjects.Contains(kind))
                {
                    this.NewMilitaryKindList.Add(kind);
                }
            }
            return this.NewMilitaryKindList;
        }

        public ArchitectureList GetOtherArchitectureList()
        {
            this.OtherArchitectureList.Clear();

            if (Session.Current.Scenario.IsPlayer(this.BelongedFaction))
            {
                if (this.BelongedSection != null)
                {
                    foreach (Architecture architecture in this.BelongedSection.Architectures)
                    {
                        if (architecture != this)
                        {
                            this.OtherArchitectureList.Add(architecture);
                        }
                    }
                }
            }

            if (this.OtherArchitectureList.Count == 0)
            {
                if (this.BelongedFaction != null)
                {
                    foreach (Architecture architecture in this.BelongedFaction.Architectures)
                    {
                        if (architecture != this)
                        {
                            this.OtherArchitectureList.Add(architecture);
                        }
                    }
                }
            }

            return this.OtherArchitectureList;
        }


        public ArchitectureList jingongjianzhuliebiao()
        {
            ArchitectureList jianzhuliebiao = new ArchitectureList();
            if (Session.Current.Scenario.youhuangdi())
            {
                jianzhuliebiao.Add(Session.Current.Scenario.huangdisuozaijianzhu());
            }
            return jianzhuliebiao;
        }

        public PersonList PersonConveneList = new PersonList();
        public PersonList GetPersonConveneList()
        {
            PersonList result = new PersonList();
            foreach (Architecture architecture in this.BelongedFaction.Architectures)
            {
                if (architecture != this)
                {
                    foreach (Person person in architecture.MovablePersons)
                    {
                        result.Add(person);
                    }
                }
            }
            PersonConveneList = result;
            return result;
        }

        public PersonList GetPersonListExceptLeader()
        {
            PersonList list = new PersonList();
            if (this.BelongedFaction != null)
            {
                foreach (Person person in this.Persons)
                {
                    if (person != this.BelongedFaction.Leader)
                    {
                        list.Add(person);
                    }
                }
            }
            return list;
        }

        public PersonList PersonStudySkillList = new PersonList();
        public PersonList GetPersonStudySkillList()
        {
            PersonList result = new PersonList();
            foreach (Person person in this.Persons)
            {
                if (person.HasLearnableSkill)
                {
                    result.Add(person);
                }
            }
            PersonStudySkillList = result;
            return result;
        }

        public PersonList PersonStudyStuntList = new PersonList();
        public PersonList GetPersonStudyStuntList()
        {
            PersonList result = new PersonList();
            foreach (Person person in this.Persons)
            {
                if (person.HasLearnableStunt)
                {
                    result.Add(person);
                }
            }
            PersonStudyStuntList = result;
            return result;
        }

        public PersonList PersonStudyTitleList = new PersonList();
        public PersonList GetPersonStudyTitleList()
        {
            PersonList result = new PersonList();
            foreach (Person person in this.Persons)
            {
                if (person.HasLearnableTitle)
                {
                    result.Add(person);
                }
            }
            PersonStudyTitleList = result;
            return result;
        }

        public GameArea GetRealTroopEnterableArea(Troop troop)
        {
            GameArea area = new GameArea();
            foreach (Point point in this.GetTroopEnterableArea(troop).Area)
            {
                //if (!Session.Current.Scenario.PositionIsTroop(point))
                //{
                area.AddPoint(point);
                //}
            }
            return area;
        }

        public MilitaryList GetRecruitmentMilitaryList()
        {
            this.RecruitmentMilitaryList.Clear();
            foreach (Military military in this.Militaries)
            {
                if ((military.Quantity < military.Kind.MaxScale) && (military.InjuryQuantity == 0))
                {
                    this.RecruitmentMilitaryList.AddMilitary(military);
                }
            }
            return this.RecruitmentMilitaryList;
        }



        public CaptiveList GetRedeemCaptiveList()
        {
            this.RedeemCaptiveList.Clear();
            foreach (Captive captive in this.BelongedFaction.SelfCaptives)
            {
                if ((captive.RansomArriveDays == 0) && (captive.Ransom <= this.Fund) && captive.BelongedFaction != null)
                {
                    this.RedeemCaptiveList.Add(captive);
                }
            }
            return this.RedeemCaptiveList;
        }

        public int GetRelationUnderZeroTroopFightingForceInView(out float rationRate)
        {
            int num = 0;
            rationRate = 0f;
            int num2 = 0;
            foreach (Point point in this.LongViewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if (((troopByPosition != null) && (troopByPosition.BelongedFaction != null)) && (Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, troopByPosition.BelongedFaction.ID) < 0))
                {
                    rationRate += ((float)troopByPosition.RationDaysLeft) / ((float)troopByPosition.RationDays);
                    num2++;
                    num += troopByPosition.FightingForce;
                }
            }
            if (num2 > 1)
            {
                rationRate /= (float)num2;
            }
            return num;
        }

        public GameObjectList GetResetDiplomaticRelationList()
        {
            this.ResetDiplomaticRelationList.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if (display.Relation >= Session.GlobalVariables.FriendlyDiplomacyThreshold && (display.LinkedFaction1 != null) && (display.LinkedFaction2 != null))
                    {
                        this.ResetDiplomaticRelationList.Add(display);
                    }
                }
            }
            return this.ResetDiplomaticRelationList;
        }

        public GameObjectList GetEnhanceDiplomaticRelationList()
        {
            this.EnhanceDiplomaticRelationList.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if ((display.LinkedFaction1 != null) && (display.LinkedFaction2 != null))
                    {
                        this.EnhanceDiplomaticRelationList.Add(display);
                    }
                }
            }
            return this.EnhanceDiplomaticRelationList;
        }

        public GameObjectList GetAllyDiplomaticRelationList()
        {
            this.AllyDiplomaticRelationList.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if ((display.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold && display.Relation >= Session.GlobalVariables.FriendlyDiplomacyThreshold * 0.9) && ((display.LinkedFaction1 != null) && (display.LinkedFaction2 != null)))
                    {
                        this.AllyDiplomaticRelationList.Add(display);
                    }
                }
            }
            return this.AllyDiplomaticRelationList;
        }

        public GameObjectList GetTruceDiplomaticRelationList()
        {
            this.TruceDiplomaticRelationList.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if (((display.LinkedFaction1 != null) && (display.LinkedFaction2 != null)) && display.Truce < 1)
                    {
                        this.TruceDiplomaticRelationList.Add(display);
                    }
                }
            }
            return this.TruceDiplomaticRelationList;
        }

        public GameObjectList GetQuanXiangDiplomaticRelationList() //劝降
        {
            this.QuanXiangDiplomaticRelationList.Clear();
            if (this.BelongedFaction != null && Session.Current.Scenario.IsPlayer(this.BelongedFaction))
            {
                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if (display.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold && (display.LinkedFaction1 != null) && (display.LinkedFaction2 != null)
                          && (this.BelongedFaction.AdjecentFactionList.GameObjects.Contains(display.LinkedFaction2) || this.BelongedFaction.AdjecentFactionList.GameObjects.Contains(display.LinkedFaction1)))
                    {
                        this.QuanXiangDiplomaticRelationList.Add(display);
                    }
                }
            }
            return this.QuanXiangDiplomaticRelationList;
        }
        /*
        private GameObjectList AIQuanXiangDiplomaticRelationList = new GameObjectList ();
        public GameObjectList GetAIQuanXiangDiplomaticRelationList()
        {
            this.AIQuanXiangDiplomaticRelationList.Clear();
            if (this.BelongedFaction != null && !Session.Current.Scenario.IsPlayer(this.BelongedFaction))
            {
                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if (display.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold && (display.LinkedFaction1 != null) && (display.LinkedFaction2 != null)
                          && (this.BelongedFaction.AdjecentFactionList.GameObjects.Contains(display.LinkedFaction2) || this.BelongedFaction.AdjecentFactionList.GameObjects.Contains(display.LinkedFaction1)))
                    {

                        //foreach (Faction f in this.BelongedFaction.AdjecentFactionList)
                        {
                            //if (this.BelongedFaction.Reputation > f.Reputation  && this.BelongedFaction.Army != 0 && this.BelongedFaction.Army >= f.Army * 5)
                            {
                                this.AIQuanXiangDiplomaticRelationList.Add(display);
                            }
                        }
                    }

                }
            }
            return this.AIQuanXiangDiplomaticRelationList;
        }
        */

        public GameObjectList GetDenounceDiplomaticRelationList()
        {
            this.DenounceDiplomaticRelationList.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if (display.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold && (display.LinkedFaction1 != null) && (display.LinkedFaction2 != null))
                    {
                        this.DenounceDiplomaticRelationList.Add(display);
                    }
                }
            }
            return this.DenounceDiplomaticRelationList;
        }

        public PersonList GetRewardPersons()
        {
            this.RewardPersonList.Clear();
            foreach (Person person in this.Persons)
            {
                if ((!person.RewardFinished && (person.Loyalty < 100)) && (person != this.BelongedFaction.Leader))
                {
                    this.RewardPersonList.Add(person);
                }
            }
            return this.RewardPersonList;
        }

        public Routeway GetRouteway(LinkNode node, bool hasEnd)
        {
            foreach (Routeway routeway in this.Routeways)
            {
                if ((routeway.DestinationArchitecture == node.A) && (!hasEnd || (routeway.EndArchitecture == node.A)))
                {
                    return routeway;
                }
            }
            return this.BuildRouteway(node, hasEnd);
        }

        public ArchitectureList GetRoutewayDestinationArchitectureList()
        {
            this.RoutewayDestinationArchitectures.Clear();
            this.RoutewayProcedures.Clear();
            foreach (Architecture architecture in this.BelongedFaction.Architectures)
            {
                architecture.surplusRate = 0f;
                architecture.PathRoutewayID = -1;
            }
            this.surplusRate = 1f;
            this.RoutewayProcedures.Enqueue(new RoutewayProcedureDetail(this, 1f));
            while (this.RoutewayProcedures.Count > 0)
            {
                RoutewayProcedureDetail detail = this.RoutewayProcedures.Dequeue();
                this.AddCloseRoutewayDestinationArchitectures(detail.Start, detail.PreviousRate);
            }
            return this.RoutewayDestinationArchitectureList;
        }

        public GameArea GetRoutewayStartArea()
        {
            return this.GetAllContactArea().GetContactArea(false);
        }

        public GameArea GetRoutewayStartPoints()
        {
            GameArea area = new GameArea();
            foreach (Point point in this.GetRoutewayStartArea().Area)
            {
                if (this.IsRoutewayPossible(point))
                {
                    area.AddPoint(point);
                }
            }
            if (area.Count == 0)
            {
                foreach (Point point in this.ContactArea.Area)
                {
                    if (this.IsRoutewayPossible(point))
                    {
                        area.AddPoint(point);
                    }
                }
            }
            if (area.Count == 0)
            {
                foreach (Point point in this.LongViewArea.Area)
                {
                    if (this.IsRoutewayPossible(point))
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }

        public MilitaryList GetShelledMilitaryList(MilitaryType militaryType)
        {
            this.ShelledMilitaryList.Clear();
            foreach (Military military in this.Militaries)
            {
                if (((military.Quantity > 0) && (military.Morale > 0)) && (military.Kind.Type != militaryType))
                {
                    this.ShelledMilitaryList.AddMilitary(military);
                }
            }
            return this.ShelledMilitaryList;
        }

        /*
        public GameArea GetSpyArchitectureArea()
        {
            GameArea area = new GameArea();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if ((architecture.BelongedFaction != null) && (architecture.BelongedFaction != this.BelongedFaction))
                {
                    foreach (Point point in architecture.ArchitectureArea.Area)
                    {
                        area.AddPoint(point);
                    }
                }
            }
            return area;
        }
         */

        public MilitaryList GetTrainingMilitaryList()
        {
            this.TrainingMilitaryList.Clear();
            foreach (Military military in this.Militaries)
            {
                if ((military.Quantity > 0) && ((military.Morale < military.MoraleCeiling) || (military.Combativity < military.CombativityCeiling)))
                {
                    this.TrainingMilitaryList.AddMilitary(military);
                }
            }
            return this.TrainingMilitaryList;
        }

        public ArchitectureList GetTransferArchitectureList()
        {
            this.TransferArchitectureList.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (Architecture architecture in this.BelongedFaction.Architectures)
                {
                    if (architecture != this)
                    {
                        this.TransferArchitectureList.Add(architecture);
                    }
                }
            }
            return this.TransferArchitectureList;
        }

        public TreasureList GetTreasureListOfLeader()
        {
            TreasureList list = new TreasureList();
            if (this.BelongedFaction != null)
            {
                this.BelongedFaction.Leader.AddTreasureToList(list);
            }
            return list;
        }

        public GameArea GetTroopEnterableArea(Troop troop)
        {
            GameArea area = new GameArea();
            foreach (Point point in this.ArchitectureArea.Area)
            {
                if (Session.Current.Scenario.GetWaterPositionMapCost(troop.Army.Kind, point) < 3500)
                {
                    area.AddPoint(point);
                }
            }
            foreach (Point point in this.ContactArea.Area)
            {
                if (troop.IsMovableOnPosition(point) && (Session.Current.Scenario.GetWaterPositionMapCost(troop.Army.Kind, point) < 3500))
                {
                    area.AddPoint(point);
                }
            }
            return area;
        }

        public bool GossipAvail()
        {
            return ((this.MovablePersons.Count > 0 && (this.Fund >= this.GossipArchitectureFund)) && (this.GetGossipArchitectureArea().Count > 0));
        }

        public bool JailBreakAvail()
        {
            return ((this.MovablePersons.Count > 0 && (this.Fund >= this.JailBreakArchitectureFund)) && (this.GetJailBreakArchitectureArea().Count > 0));
        }

        public bool AssassinateAvail()
        {
            return this.MovablePersons.Count > 0 && this.GetAssassinateArchitectureArea(this.BelongedFaction).Count > 0;
        }

        private void characteristicsDoWork()
        {
            foreach (Influence i in this.Characteristics.Influences.Values)
            {
                i.DoWork(this);
            }
        }

        private void HandleFacilities()
        {
            this.CheckBuildingFacility();
            this.FacilityMaintenance();
            this.FacilityRecovery();
            this.FacilityDoWork();
        }

        public bool HasAnyPerson()
        {
            return ((this.Persons.Count > 0) || (this.MovingPersons.Count > 0));
        }

        public bool HasCampaignableMilitary()
        {
            foreach (Military military in this.Militaries)
            {
                if (((military.Quantity > 0) && (military.Morale > 0)) && (military.InjuryQuantity < military.Kind.MinScale))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasCaptive()
        {
            return (this.CaptiveCount > 0);
        }

        public bool HasFactionCaptive(Faction f)
        {
            foreach (Captive c in this.Captives)
            {
                if (c.CaptiveFaction == f)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasCloserOffensiveArchitecture(LinkNode node, out Architecture closer)
        {
            closer = null;
            foreach (Architecture architecture in this.BelongedFaction.Architectures)
            {
                if (architecture != this)
                {
                    LinkNode node2 = null;
                    architecture.AIAllLinkNodes.TryGetValue(node.A.ID, out node2);
                    if (((node2 != null) && (node2.Level < node.Level)) && (node2.Kind != LinkKind.None))
                    {
                        closer = architecture;
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasContactHostileTroop(Faction faction)
        {
            foreach (Point point in this.GetAllContactArea().Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && !troopByPosition.IsFriendly(faction))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasEnoughForceOffensiveMilitary()
        {
            foreach (Military military in this.Militaries)
            {
                if (this.IsOffensiveMilitary(military) && (military.Scales >= 30))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasExperiencedLeaderMilitary(Person person)
        {
            foreach (Military military in this.Militaries)
            {
                if ((military.Leader == person) && (military.LeaderExperience >= 200))
                {
                    return true;
                }
            }
            return false;
        }
        public bool HasPrincess()
        {
            return (this.Feiziliebiao.Count > 0);
        }

        public bool HasLandLink()
        {
            return (this.AILandLinks.Count > 0);
        }

        public bool HasWaterLink()
        {
            return (this.AIWaterLinks.Count > 0);
        }

        public bool HasFacility()
        {
            return (this.FacilityCount > 0);
        }
        public bool HaskechaichuFacility()
        {
            return (this.kechaichudesheshi().Count > 0);
        }
        public bool HasFaction()
        {
            return (this.BelongedFaction != null);
        }

        public bool ArchitectureEditMode()
        {
            return true;
        }

        public bool HasFactionInClose(Faction faction, int level)
        {
            foreach (LinkNode node in this.AIAllLinkNodes.Values)
            {
                if (node.Level > level)
                {
                    return false;
                }
                if (node.A.BelongedFaction == faction)
                {
                    return true;
                }
            }
            return false;
        }

        /*
        public bool HasFactionSpy(Faction faction)
        {
            foreach (SpyPack pack in this.SpyPacks)
            {
                if (pack.SpyPerson.BelongedFaction == faction)
                {
                    return true;
                }
            }
            return false;
        }
        */

        public bool HasFollowedLeaderMilitary(Person person)
        {
            foreach (Military military in this.Militaries)
            {
                if (military.FollowedLeader == person)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HasHostileArchitectureOnPath(LinkNode node)
        {
            foreach (Architecture architecture in node.Path)
            {
                if (((architecture != this) && (architecture != node.A)) && !((architecture.BelongedFaction == null) || this.IsFriendlyWithoutTruce(architecture.BelongedFaction)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasHostileTroopsInArchitecture()
        {
            foreach (Point point in this.ArchitectureArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && !troopByPosition.IsFriendly(this.BelongedFaction))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasOwnFactionTroopsInArchitecture()
        {
            foreach (Point point in this.ArchitectureArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && troopByPosition.BelongedFaction == this.BelongedFaction)
                {
                    return true;
                }
            }
            return false;
        }

        public void ResetLandLink(GameObjectList gameObjectList)
        {
            foreach (Architecture architecture in this.AILandLinks)
            {
                architecture.AILandLinks.Remove(this);
            }
            this.AILandLinks.Clear();
            foreach (GameObject gameObject in gameObjectList)
            {
                Architecture architecture = gameObject as Architecture;
                if (architecture != null)
                {
                    this.AILandLinks.Add(architecture);
                }
            }
            foreach (Architecture architecture in this.AILandLinks)
            {
                architecture.AILandLinks.Add(this);
            }
        }

        public void ResetWaterLink(GameObjectList gameObjectList)
        {
            foreach (Architecture architecture in this.AIWaterLinks)
            {
                architecture.AIWaterLinks.Remove(this);
            }
            this.AIWaterLinks.Clear();
            foreach (GameObject gameObject in gameObjectList)
            {
                Architecture architecture = gameObject as Architecture;
                if (architecture != null)
                {
                    this.AIWaterLinks.Add(architecture);
                }
            }
            foreach (Architecture architecture in this.AIWaterLinks)
            {
                architecture.AIWaterLinks.Add(this);
            }
        }

        public bool HasHostileTroopsInView()
        {
            if (Session.GlobalVariables.AIQuickBattle)
            {
                foreach (Architecture a in Session.Current.Scenario.Architectures)
                {
                    if (a.AIBattlingArchitectures.GameObjects.Contains(this))
                    {
                        return true;
                    }
                }
            }

            GameArea viewArea = this.ViewArea;
            if ((this.RecentlyAttacked > 0) || (this.ArmyScale > this.NormalArmyScale))
            {
                viewArea = this.LongViewArea;
            }
            foreach (Point point in viewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if ((troopByPosition != null) && (!troopByPosition.IsFriendly(this.BelongedFaction) && (troopByPosition.Status != TroopStatus.埋伏)))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasOwnFactionTroopsInView()
        {
            if (Session.GlobalVariables.AIQuickBattle)
            {
                foreach (Architecture a in Session.Current.Scenario.Architectures)
                {
                    if (a.AIBattlingArchitectures.GameObjects.Contains(this))
                    {
                        return true;
                    }
                }
            }

            GameArea viewArea = this.LongViewArea;
            foreach (Point point in viewArea.Area)
            {
                Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                if (troopByPosition != null && troopByPosition.BelongedFaction == this.BelongedFaction)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasMilitary()
        {
            return (this.Militaries.Count > 0);
        }

        public bool HasMovingPerson()
        {
            return (this.MovingPersons.Count > 0);
        }

        public bool HasNoFactionPerson()
        {
            return (this.NoFactionPersonCount > 0);
        }

        public bool HasOffensiveMilitary()
        {
            foreach (Military military in this.Militaries)
            {
                if (this.IsOffensiveMilitary(military))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasOffensiveSectionInClose(out Section section, int level)
        {
            section = null;
            foreach (LinkNode node in this.AIAllLinkNodes.Values)
            {
                if (node.Level > level)
                {
                    return false;
                }
                if (((node.A.BelongedSection != null) && (node.A.BelongedSection.BelongedFaction == this.BelongedFaction)) && node.A.BelongedSection.AIDetail.ValueOffensiveCampaign)
                {
                    section = node.A.BelongedSection;
                    return true;
                }
            }
            return false;
        }

        public bool HasPerson()
        {
            return (this.Persons.Count > 0);
        }

        public bool HasRelationUnderZeroHostileTroopsInView()
        {
            if (this.BelongedFaction != null)
            {
                GameArea viewArea = this.ViewArea;
                if (this.Kind.HasLongView && (this.ArmyScale < this.NormalArmyScale))
                {
                    viewArea = this.LongViewArea;
                }
                foreach (Point point in viewArea.Area)
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                    if ((((troopByPosition != null) && (troopByPosition.BelongedFaction != null)) && (troopByPosition.Status != TroopStatus.埋伏)) && (Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, troopByPosition.BelongedFaction.ID) < 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool HasRouteway(Architecture destination)
        {
            foreach (Routeway routeway in this.Routeways)
            {
                if (routeway.DestinationArchitecture == destination)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasRouteway(LinkNode node, bool hasEnd)
        {
            float rate = 1f;
            foreach (Routeway routeway in this.Routeways)
            {
                if (((routeway.DestinationArchitecture == node.A) && (!hasEnd || (routeway.EndArchitecture == node.A))) && (routeway.LastPoint.ConsumptionRate <= this.BelongedFaction.RoutewayPathBuilder.ConsumptionMax))
                {
                    return true;
                }
            }
            return this.FindRouteway(node, hasEnd, out rate);
        }

        public bool HasRouteway(LinkNode node, bool hasEnd, out float rate)
        {
            foreach (Routeway routeway in this.Routeways)
            {
                if (((routeway.DestinationArchitecture == node.A) && (!hasEnd || (routeway.EndArchitecture == node.A))) && (routeway.LastPoint.ConsumptionRate <= this.BelongedFaction.RoutewayPathBuilder.ConsumptionMax))
                {
                    rate = routeway.LastPoint.ConsumptionRate;
                    return true;
                }
            }
            return this.FindRouteway(node, hasEnd, out rate);
        }

        public RoutewayList HasRoutewayList(Architecture destination)
        {
            RoutewayList list = new RoutewayList();
            foreach (Routeway routeway in this.Routeways)
            {
                if (routeway.DestinationArchitecture == destination)
                {
                    list.Add(routeway);
                }
            }
            return list;
        }

        public bool HasInformation()
        {
            return this.Informations.Count > 0;
        }

        public bool FactionHasInformation()
        {
            return this.BelongedFaction != null && this.BelongedFaction.HasInformation();
        }

        public int ShuijunMilitaryCount
        {
            get
            {
                int result = 0;
                foreach (Military m in this.Militaries)
                {
                    if (m.Kind.Type == MilitaryType.水军)
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        public bool HasShuijun()
        {
            return ShuijunMilitaryCount > 0;
        }

        public bool HasShuijunMilitaryKind()
        {
            foreach (MilitaryKind kind in this.BelongedFaction.AvailableMilitaryKinds.MilitaryKinds.Values)
            {
                if (kind.Type == MilitaryType.水军 && !kind.IsTransport && kind.ID != 28)
                {
                    return true;
                }
            }
            foreach (MilitaryKind kind in this.PrivateMilitaryKinds.MilitaryKinds.Values)
            {
                if (kind.Type == MilitaryType.水军 && !kind.IsTransport && kind.ID != 28)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasTreasure()
        {
            foreach (Person person in this.GetAllPersons())
            {
                if (person.TreasureCount > 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasTreasureToAward()
        {
            if ((this.BelongedFaction != null) && (this.BelongedFaction.Leader != null))
            {
                if (this.BelongedFaction.PersonCount <= 1)
                {
                    return false;
                }
                return this.BelongedFaction.Leader.TreasureCount > 0;
            }
            return false;
        }

        public bool HasTreasureToConfiscate()
        {
            if ((this.BelongedFaction != null) && (this.BelongedFaction.Leader != null))
            {
                return this.BelongedFaction.AllTreasuresExceptLeader.Count > 0;
            }
            return false;
        }

        public bool HasMarriageToMake()
        {
            return this.makeMarryablePersons().Count > 0;
        }

        public bool HasChildrenToTrain()
        {
            return this.BelongedFaction.Leader.TrainableChildren.Count > 0;
        }

        public bool HasUnavailablePerson(PersonList personlist)
        {
            foreach (Person person in personlist)
            {
                if (person.LocationArchitecture == null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasWorkingPerson()
        {
            foreach (Person person in this.Persons)
            {
                if (person.WorkKind != ArchitectureWorkKind.无)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HigtViewTroop(Troop troop)
        {
            return (this.ViewArea.HasPoint(troop.Position) && (((this.BelongedFaction != null) && this.IsFriendly(troop.BelongedFaction)) || (troop.Status != TroopStatus.埋伏)));
        }

        public void IncreaseAgriculture(int increment)
        {
            if (this.AgricultureCeiling == 0) return;
            float actualIncrement = increment > 0 ? increment * (1-(float)this.Agriculture / this.AgricultureCeiling) : increment;
            this.Agriculture += (int) Math.Floor(actualIncrement);
            if (GameObject.Random(1000000) < (actualIncrement - Math.Floor(actualIncrement)) * 1000000)
            {
                this.Agriculture++;
            }

            if (this.Agriculture > this.AgricultureCeiling)
            {
                this.Agriculture = this.AgricultureCeiling;
            }
        }

        public void IncreaseCommerce(int increment)
        {
            if (this.CommerceCeiling == 0) return;
            float actualIncrement = increment > 0 ? increment * (1-(float)this.Commerce / this.CommerceCeiling) : increment;
            this.Commerce += (int)Math.Floor(actualIncrement);
            if (GameObject.Random(1000000) < (actualIncrement - Math.Floor(actualIncrement)) * 1000000)
            {
                this.Commerce++;
            }

            if (this.Commerce > this.CommerceCeiling)
            {
                this.Commerce = this.CommerceCeiling;
            }
        }

        public int IncreaseDomination(int increment)
        {
            if (this.DominationCeiling == 0) return 0;
            int old = this.Domination;

            float actualIncrement = increment > 0 ? increment * (1-(float)this.Domination / this.DominationCeiling) : increment;
            this.Domination += (int)Math.Floor(actualIncrement);
            if (GameObject.Random(1000000) < (actualIncrement - Math.Floor(actualIncrement)) * 1000000)
            {
                this.Domination++;
            }

            if (this.Domination > this.DominationCeiling)
            {
                this.Domination = this.DominationCeiling;
            }

            return this.Domination - old;
        }

        public int IncreaseEndurance(int increment)
        {
            if (increment <= 0)
            {
                return 0;
            }
            if (this.EnduranceCeiling == 0) return 0;

            int old = this.Endurance;

            float actualIncrement = increment > 0 ? increment * (1-(float)this.Endurance / this.EnduranceCeiling):increment;
            this.Endurance += (int)Math.Floor(actualIncrement);
            if (GameObject.Random(1000000) < (actualIncrement - Math.Floor(actualIncrement)) * 1000000)
            {
                this.Endurance++;
            }

            if (this.Endurance > this.EnduranceCeiling)
            {
                this.Endurance = this.EnduranceCeiling;
            }

            if (this.Endurance == 0)
            {
                this.WallStateChange();
            }

            return this.Endurance - old;
        }

        public void IncreaseFood(int increment)
        {
            if ((increment + this.food) > this.FoodCeiling)
            {
                increment = this.FoodCeiling - this.food;
            }
            this.food += increment;
            this.IncrementNumberList.AddNumber(increment, CombatNumberKind.粮草, this.Position);
            this.ShowNumber = true;
        }

        public void IncreaseFund(int increment)
        {
            if ((increment + this.fund) > this.FundCeiling)
            {
                increment = this.FundCeiling - this.fund;
            }
            this.fund += increment;
            this.IncrementNumberList.AddNumber(increment, CombatNumberKind.资金, this.Position);
            this.ShowNumber = true;
        }

        public void IncreaseMilitaryPopulation(int increment)
        {
            this.militaryPopulation += increment;

            if (this.militaryPopulation > this.PopulationCeiling)
            {
                this.militaryPopulation = this.PopulationCeiling;
            }

            if (this.militaryPopulation < 0)
            {
                this.militaryPopulation = 0;
            }


        }



        public void IncreaseMorale(int increment)
        {
            if (this.MoraleCeiling == 0) return;
            float actualIncrement = increment > 0 ? increment * (1-(float)this.Morale / this.MoraleCeiling) : increment;
            this.Morale += (int)Math.Floor(actualIncrement);
            if (GameObject.Random(1000000) < (actualIncrement - Math.Floor(actualIncrement)) * 1000000)
            {
                this.Morale++;
            }

            if (this.Morale > this.MoraleCeiling)
            {
                this.Morale = this.MoraleCeiling;
            }
        }

        public int IncreasePopulation(int increment)
        {
            if (this.PopulationCeiling == 0) return 0;
            int old = this.Population;

            this.Population += increment;

            if (this.Population > this.PopulationCeiling)
            {
                this.Population = this.PopulationCeiling;
            }

            if (this.population < 0)
            {
                this.population = 0;
            }

            return this.Population - old;
        }

        public void IncreaseTechnology(int increment)
        {
            if (this.TechnologyCeiling == 0) return;
            float actualIncrement = increment > 0 ? increment * (1-(float)this.Technology / this.TechnologyCeiling) : increment;
            this.Technology += (int)Math.Floor(actualIncrement);
            if (GameObject.Random(1000000) < (actualIncrement - Math.Floor(actualIncrement)) * 1000000)
            {
                this.Technology++;
            }

            if (this.Technology > this.TechnologyCeiling)
            {
                this.Technology = this.TechnologyCeiling;
            }
        }

        private void IncreaseViewAreaCombativity()
        {
            if (this.IncrementOfCombativityInViewArea > 0)
            {
                foreach (Point point in this.ViewArea.Area)
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                    if ((troopByPosition != null) && this.IsFriendlyWithoutTruce(troopByPosition.BelongedFaction))
                    {
                        troopByPosition.IncreaseCombativity(this.IncrementOfCombativityInViewArea);
                    }
                }
            }
        }

        public bool InformationAvail()
        {
            if (this.MovablePersons.Count > 0 && Session.Current.Scenario.GameCommonData.AllInformationKinds.HasAvailItem(this))
            {
                return true;
            }
            if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction ) && this.BelongedFaction.PersonCount == 1)
            {
                return false;
            }
            return false;
        }

        public bool StopInformationAvail()
        {
            return this.Informations.Count > 0;
        }

        public int InformationDayCost
        {
            get
            {
                int sum = 0;
                foreach (Information i in this.Informations)
                {
                    sum += i.DayCost;
                }
                return sum;
            }
        }

        public string InformationCostString
        {
            get
            {
                return this.InformationDayCost * 30 + "/月";
            }
        }

        public void RemoveAllInformations()
        {
            foreach (Information information in this.Informations.GetList())
            {
                information.Purify();
                this.RemoveInformation(information);
                Session.Current.Scenario.Informations.Remove(information);
            }
        }

        private void InformationDayEvent()
        {
            int cost = this.InformationDayCost;
            if (this.Fund >= cost)
            {
                this.DecreaseFund(cost);
            }
            else
            {
                this.RemoveAllInformations();
            }
            //foreach (Information information in this.Informations)
            //{
            //    information.CheckAmbushTroop();
            //    information.DaysStarted++;
            //}
        }

        private void InsideTacticsAI()
        {
            //if (((this.PlanArchitecture == null) || GameObject.Chance(10)) && this.HasPerson())
            if (this.HasPerson())
            {
                if (((this.RecentlyAttacked <= 0) && (this.PlanArchitecture == null)) && !this.HasHostileTroopsInView())
                {
                    //Label_0221:
                    foreach (Person person in this.Persons.GetList())
                    {
                        if (((!this.withoutTruceFrontline || !GameObject.Chance(5)) && !GameObject.Chance(20)) || (GameObject.Random(Session.Current.Scenario.Date.Day) < GameObject.Random(30)))
                        {
                            continue;
                        }
                        if (GameObject.Chance(100 - Session.Parameters.AutoLearnSkillSuccessRate * Session.Parameters.LearnSkillDays) && person.HasLearnableSkill)
                        {
                            person.GoForStudySkill();
                        }
                        else if (GameObject.Chance(50))
                        {
                            List<Title> higherLevelLearnableTitle = person.HigherLevelLearnableTitle;
                            if (higherLevelLearnableTitle.Count > 0)
                            {
                                person.GoForStudyTitle(higherLevelLearnableTitle[GameObject.Random(higherLevelLearnableTitle.Count)]);
                            }
                        }
                        else if (Session.Current.Scenario.GameCommonData.AllStunts.Count > person.StuntCount)
                        {
                            foreach (Stunt stunt in Session.Current.Scenario.GameCommonData.AllStunts.GetStuntList().GetRandomList())
                            {
                                if ((person.Stunts.GetStunt(stunt.ID) == null) && stunt.IsLearnable(person) &&
                                    GameObject.Chance(100 - Session.Parameters.AutoLearnStuntSuccessRate * Session.Parameters.LearnStuntDays))
                                {
                                    person.GoForStudyStunt(stunt);
                                    break;
                                }
                            }
                            //Label_0220:;
                        }
                    }
                    foreach (Person person in this.Persons.GetList())
                    {
                        if (person.ReturnedDaySince >= 3)
                        {
                            if (this.Fund < Session.Parameters.InternalFundCost ||
                                    (person.WaitForFeiZi == null && person.WorkKind == ArchitectureWorkKind.无 &&
                                    !person.HasFollowingArmy && !person.HasEffectiveLeadingArmy &&
                                    (!this.withoutTruceFrontline || GameObject.Random(person.FightingNumber) < 100)
                                ))
                            {
                                if (person.Tiredness <= 0)
                                {
                                    person.GoForSearch();
                                }
                            }
                        }
                    }
                }
            }
        }

        public bool InstigateAvail()
        {
            return ((this.MovablePersons.Count > 0 && (this.Fund >= this.InstigateArchitectureFund)) && (this.GetInstigateArchitectureArea().Count > 0));
        }

        public bool IsCaptiveInArchitecture(Captive captive)
        {
            return this.Captives.HasGameObject(captive);
        }

        public bool IsFriendly(Faction faction)
        {
            return ((this.BelongedFaction == faction) || ((this.BelongedFaction != null) && this.BelongedFaction.IsFriendly(faction)));
        }

        public bool IsFriendlyWithoutTruce(Faction faction)
        {
            return ((this.BelongedFaction == faction) || ((this.BelongedFaction != null) && this.BelongedFaction.IsFriendlyWithoutTruce(faction)));
        }

        public bool IsFull()
        {
            return (((((this.Agriculture == this.AgricultureCeiling) && (this.Commerce == this.CommerceCeiling)) && ((this.Technology == this.TechnologyCeiling) && (this.Domination == this.DominationCeiling))) && (this.Morale == this.MoraleCeiling)) && (this.Endurance == this.EnduranceCeiling));
        }

        public bool IsGood()
        {
            return (((((this.Agriculture >= (this.AgricultureCeiling * 0.5)) && (this.Commerce >= (this.CommerceCeiling * 0.5))) && ((this.Technology >= (this.TechnologyCeiling * 0.5)) && (this.Domination >= (this.DominationCeiling * 0.7)))) && (this.Morale >= (this.MoraleCeiling * 0.5))) && (this.Endurance >= (this.EnduranceCeiling * 0.5)));
        }

        public bool IsVeryGood()
        {
            return (((((this.Agriculture >= (this.AgricultureCeiling * 0.8)) && (this.Commerce >= (this.CommerceCeiling * 0.8))) && ((this.Technology >= (this.TechnologyCeiling * 0.8)) && (this.Domination >= (this.DominationCeiling * 0.8)))) && (this.Morale >= (this.MoraleCeiling * 0.8))) && (this.Endurance >= (this.EnduranceCeiling * 0.8)));
        }

        public bool IsHostile(Faction faction)
        {
            return ((this.BelongedFaction != null) && this.BelongedFaction.IsHostile(faction));
        }

        public bool IsLandLink(Architecture a)
        {
            return (this.AILandLinks.GetGameObject(a.ID) != null);
        }

        public bool IsMilitaryUnavailable(Military military)
        {
            return (military.BelongedArchitecture == null);
        }

        public bool IsNodeFoodEnough(LinkNode node, Routeway routeway)
        {
            switch (node.Kind)
            {
                case LinkKind.None:
                    return false;

                case LinkKind.Land:
                    return (((node.A.Food * (1f - routeway.LastPoint.ConsumptionRate)) * Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(routeway.Length))) >= (node.A.FoodCostPerDayOfLandMilitaries * ((routeway.Length + 6) - (node.A.LandArmyScale / 8))));

                case LinkKind.Water:
                    return (((node.A.Food * (1f - routeway.LastPoint.ConsumptionRate)) * Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(routeway.Length))) >= (node.A.FoodCostPerDayOfWaterMilitaries * ((routeway.Length + 6) - (node.A.WaterArmyScale / 8))));

                case LinkKind.Both:
                    return (((node.A.Food * (1f - routeway.LastPoint.ConsumptionRate)) * Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(routeway.Length))) >= (node.A.FoodCostPerDayOfAllMilitaries * ((routeway.Length + 6) - (node.A.ArmyScale / 8))));
            }
            return false;
        }

        public bool IsNodeHelpArmyEnough(LinkNode node)
        {
            switch (node.Kind)
            {
                case LinkKind.None:
                    return false;

                case LinkKind.Land:
                    return ((!node.A.IsImportant && (node.A.LandArmyScale >= node.A.FewArmyScale)) || (node.A.LandArmyScale >= node.A.NormalArmyScale));

                case LinkKind.Water:
                    return ((!node.A.IsImportant && (node.A.WaterArmyScale >= node.A.FewArmyScale)) || (node.A.WaterArmyScale >= node.A.NormalArmyScale));

                case LinkKind.Both:
                    return ((!node.A.IsImportant && (node.A.ArmyScale >= node.A.FewArmyScale)) || (node.A.ArmyScale >= node.A.NormalArmyScale));
            }
            return false;
        }

        public bool IsOffensiveMilitary(Military m)
        {
            return ((((m.Scales >= 3) && (m.Morale >= 80)) && (m.Combativity >= 80)) && (m.InjuryQuantity <= m.Kind.MinScale) && (m.Scales >= m.RetreatScale * 1.5));
        }

        public int OffensiveMilitaryCount()
        {
            int r = 0;
            foreach (Military m in this.Militaries)
            {
                if (this.IsOffensiveMilitary(m))
                {
                    r++;
                }
            }
            return r;
        }

        public bool IsOK()
        {
            return (((((this.Agriculture >= (this.AgricultureCeiling * 0.45)) && (this.Commerce >= (this.CommerceCeiling * 0.45))) && ((this.Technology >= (this.TechnologyCeiling * 0.45)) && (this.Domination >= (this.DominationCeiling * 0.7)))) && (this.Morale >= (this.MoraleCeiling * 0.45))) && (this.Endurance >= (this.EnduranceCeiling * 0.4)));
        }

        public bool IsRoutewayPossible(Point p)
        {
            if (Session.Current.Scenario.GetArchitectureByPosition(p) != null)
            {
                return false;
            }
            TerrainDetail terrainDetailByPosition = Session.Current.Scenario.GetTerrainDetailByPosition(p);
            return ((terrainDetailByPosition != null) && ((this.BelongedFaction == null) || ((this.BelongedFaction.RoutewayWorkForce >= terrainDetailByPosition.RoutewayBuildWorkCost) && (terrainDetailByPosition.RoutewayConsumptionRate < 1f))));
        }

        public bool IsSelfFoodEnough(LinkNode node, Routeway routeway)
        {
            //if (routeway.LastPoint != null)   //临时加上，避免跳出
            {
                switch (node.Kind)
                {
                    case LinkKind.None:
                        return false;

                    case LinkKind.Land:
                        return (((this.Food * (1f - routeway.LastPoint.ConsumptionRate)) * Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(routeway.Length))) >= (this.FoodCostPerDayOfLandMilitaries * ((routeway.Length + 6) - (this.LandArmyScale / 8))));

                    case LinkKind.Water:
                        return (((this.Food * (1f - routeway.LastPoint.ConsumptionRate)) * Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(routeway.Length))) >= (this.FoodCostPerDayOfWaterMilitaries * ((routeway.Length + 6) - (this.WaterArmyScale / 8))));

                    case LinkKind.Both:
                        return (((this.Food * (1f - routeway.LastPoint.ConsumptionRate)) * Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(routeway.Length))) >= (this.FoodCostPerDayOfAllMilitaries * ((routeway.Length + 6) - (this.ArmyScale / 8))));
                }
            }
            return false;
        }

        public bool IsSelfHelpArmyEnough(LinkNode node)
        {
            switch (node.Kind)
            {
                case LinkKind.None:
                    return false;

                case LinkKind.Land:
                    return ((!this.IsImportant && (this.LandArmyScale >= this.FewArmyScale)) || (this.LandArmyScale >= this.NormalArmyScale));

                case LinkKind.Water:
                    return ((!this.IsImportant && (this.WaterArmyScale >= this.FewArmyScale)) || (this.WaterArmyScale >= this.NormalArmyScale));

                case LinkKind.Both:
                    return ((!this.IsImportant && (this.ArmyScale >= this.FewArmyScale)) || (this.ArmyScale >= this.NormalArmyScale));
            }
            return false;
        }

        public bool IsSelfMoveArmyEnough(LinkNode node)
        {
            switch (node.Kind)
            {
                case LinkKind.None:
                    return false;

                case LinkKind.Land:
                    return (((((this.IsImportant && (this.HostileLine || this.withoutTruceFrontline)) && (this.LandArmyScale > this.LargeArmyScale)) || (((this.IsImportant && !this.withoutTruceFrontline) && (this.LandArmyScale > this.NormalArmyScale)) || ((!this.IsImportant && this.HostileLine) && (this.LandArmyScale > this.LargeArmyScale)))) || ((!this.IsImportant && this.withoutTruceFrontline) && (this.LandArmyScale > this.NormalArmyScale))) || ((!this.IsImportant && !this.withoutTruceFrontline) && (this.LandArmyScale > this.FewArmyScale)));

                case LinkKind.Water:
                    return (((((this.IsImportant && (this.HostileLine || this.withoutTruceFrontline)) && (this.WaterArmyScale > this.LargeArmyScale)) || (((this.IsImportant && !this.withoutTruceFrontline) && (this.WaterArmyScale > this.NormalArmyScale)) || ((!this.IsImportant && this.HostileLine) && (this.WaterArmyScale > this.LargeArmyScale)))) || ((!this.IsImportant && this.withoutTruceFrontline) && (this.WaterArmyScale > this.NormalArmyScale))) || ((!this.IsImportant && !this.withoutTruceFrontline) && (this.WaterArmyScale > this.FewArmyScale)));

                case LinkKind.Both:
                    return (((((this.IsImportant && (this.HostileLine || this.withoutTruceFrontline)) && (this.ArmyScale > this.LargeArmyScale)) || (((this.IsImportant && !this.withoutTruceFrontline) && (this.ArmyScale > this.NormalArmyScale)) || ((!this.IsImportant && this.HostileLine) && (this.ArmyScale > this.LargeArmyScale)))) || ((!this.IsImportant && this.withoutTruceFrontline) && (this.ArmyScale > this.NormalArmyScale))) || ((!this.IsImportant && !this.withoutTruceFrontline) && (this.ArmyScale > this.FewArmyScale)));
            }
            return false;
        }

        public bool IsSelfOffensiveArmyEnough(LinkNode node)
        {
            switch (node.Kind)
            {
                case LinkKind.None:
                    return false;

                case LinkKind.Land:
                    return (((!Session.Current.Scenario.IsPlayer(node.A.BelongedFaction) && this.LandArmyScale > this.LargeArmyScale) || (node.A.IsImportant && (this.LandArmyScale > node.A.ArmyScale))) || (!node.A.IsImportant && ((this.LandArmyScale * 2) > (node.A.ArmyScale * 3))));

                case LinkKind.Water:
                    return (((!Session.Current.Scenario.IsPlayer(node.A.BelongedFaction) && this.WaterArmyScale > this.LargeArmyScale) || (node.A.IsImportant && (this.WaterArmyScale > node.A.ArmyScale))) || (!node.A.IsImportant && ((this.WaterArmyScale * 2) > (node.A.ArmyScale * 3))));

                case LinkKind.Both:
                    return (((!Session.Current.Scenario.IsPlayer(node.A.BelongedFaction) && this.ArmyScale > this.LargeArmyScale) || (node.A.IsImportant && (this.ArmyScale > node.A.ArmyScale))) || ((!node.A.IsImportant && (this.ArmyScale > this.NormalArmyScale)) && ((this.ArmyScale * 2) > (node.A.ArmyScale * 3))));
            }
            return false;
        }

        public bool IsViewing(Point position)
        {
            return this.LongViewArea.HasPoint(position);
        }

        public bool IsWaterLink(Architecture a)
        {
            return (this.AIWaterLinks.GetGameObject(a.ID) != null);
        }

        public bool LevelUpAvail()
        {
            return GetLevelUpMilitaryList().Count > 0;
        }

        public void LevelUpMilitary(Military m, MilitaryKind militaryKind)
        {
            if ((militaryKind != null) && (m.Kind.LevelUpAvail(this)))
            {
                //this.BelongedFaction.MorphMilitary(m.Kind, militaryKind);
                int num = (m.Quantity * militaryKind.MinScale) / m.Kind.MinScale;
                int num2 = ((m.Experience - m.Kind.LevelUpExperience) * militaryKind.MinScale) / m.Kind.MinScale;
                this.IncreasePopulation(m.Quantity - num);
                this.IncreaseMilitaryPopulation(m.Quantity - num);
                m.Kind = militaryKind;
                m.Quantity = num;
                m.Experience = num2;
                m.Name = m.Kind.Name + "队";
                ExtensionInterface.call("LevelUpMilitary", new Object[] { Session.Current.Scenario, this, m });
            }
        }

        private int RoutewayPathBuilder_OnGetCost(Point position, out float consumptionRate)
        {
            GameArea singleton = new GameArea();
            singleton.AddPoint(position);
            singleton.Centre = position;

            consumptionRate = 0f;
            if (!Session.Current.Scenario.PositionOutOfRange(position))
            {
                int dist = (int)Math.Ceiling(Math.Min(Math.Min(Session.Current.Scenario.GetDistance(singleton, this.pathFinder.startingArchitecture.ArchitectureArea),
                    Session.Current.Scenario.GetDistance(singleton, this.pathFinder.targetArchitecture.ArchitectureArea)), 20));
                if (dist > 4)
                {
                    for (int i = -dist; i <= dist; ++i)
                    {
                        for (int j = Math.Abs(i) - dist; j <= dist - Math.Abs(i); ++j)
                        {
                            Point loc = new Point(position.X + i, position.Y + j);
                            Architecture landedArch = Session.Current.Scenario.GetArchitectureByPosition(loc);

                            if (landedArch != null && landedArch != this.pathFinder.startingArchitecture && landedArch != this.pathFinder.targetArchitecture)
                            {
                                return 1000;
                            }
                        }
                    }
                }

                TerrainDetail terrainDetailByPositionNoCheck = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(position);
                if (terrainDetailByPositionNoCheck != null)
                {
                    Architecture landedArch = Session.Current.Scenario.GetArchitectureByPosition(position);
                    if (landedArch != null && landedArch != this.pathFinder.startingArchitecture && landedArch != this.pathFinder.targetArchitecture)
                    {
                        return 1000;
                    }
                    else if (landedArch == null)
                    {
                        if (this.pathFinder.MultipleWaterCost && !Session.Current.Scenario.IsWaterPositionRoutewayable(position))
                        {
                            return 1000;
                        }
                        if (this.pathFinder.MustUseWater && (terrainDetailByPositionNoCheck.ID != 6))
                        {
                            return 1000;
                        }
                        else if (!terrainDetailByPositionNoCheck.TroopPassable)
                        {
                            return 1000;
                        }
                    }

                    return 1;
                }
            }
            return 1000;
        }

        private int RoutewayPathBuilder_OnGetPenalizedCost(Point position)
        {
            return 0;
        }

        public void FindLinks(ArchitectureList allArch)
        {
            pathFinder.OnGetCost += new RoutewayPathFinder.GetCost(RoutewayPathBuilder_OnGetCost);
            pathFinder.OnGetPenalizedCost += new RoutewayPathFinder.GetPenalizedCost(RoutewayPathBuilder_OnGetPenalizedCost);
            FindLandLinks(allArch, 50);
            FindWaterLinks(allArch, 50);
        }

        private GameArea GetLandTroopMovableArea()
        {
            GameArea a = new GameArea();
            foreach (Point i in this.ArchitectureArea.Area)
            {
                if (Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(i).ID != 6)
                {
                    a.AddPoint(i);
                }
                else
                {
                    TerrainKind t1 = Session.Current.Scenario.GetTerrainKindByPosition(new Point(i.X - 1, i.Y));
                    TerrainKind t2 = Session.Current.Scenario.GetTerrainKindByPosition(new Point(i.X + 1, i.Y));
                    TerrainKind t3 = Session.Current.Scenario.GetTerrainKindByPosition(new Point(i.X, i.Y - 1));
                    TerrainKind t4 = Session.Current.Scenario.GetTerrainKindByPosition(new Point(i.X, i.Y + 1));
                    if (t1 != TerrainKind.水域 && t1 != TerrainKind.无)
                    {
                        a.AddPoint(i);
                    }
                    else if (t2 != TerrainKind.水域 && t2 != TerrainKind.无)
                    {
                        a.AddPoint(i);
                    }
                    else if (t3 != TerrainKind.水域 && t3 != TerrainKind.无)
                    {
                        a.AddPoint(i);
                    }
                    else if (t4 != TerrainKind.水域 && t4 != TerrainKind.无)
                    {
                        a.AddPoint(i);
                    }
                }
            }
            return a;
        }

        private GameArea GetWaterTroopMovableArea()
        {
            return this.ArchitectureArea;
        }

        private RoutewayPathFinder pathFinder = new RoutewayPathFinder();
        private void FindLandLinks(ArchitectureList allArch, int maxDistance)
        {
            foreach (Architecture i in allArch)
            {
                if (i == this) continue;
                if (i.AILandLinks.GameObjects.Contains(this)) continue;
                if (Session.Current.Scenario.GetSimpleDistance(i.Position, this.Position) < maxDistance)
                {
                    pathFinder.ConsumptionMax = 0.7f;
                    pathFinder.startingArchitecture = this;
                    pathFinder.targetArchitecture = i;
                    pathFinder.MultipleWaterCost = !Session.GlobalVariables.LandArmyCanGoDownWater;
                    pathFinder.MustUseWater = false;
                    Point? p1;
                    Point? p2;
                    Session.Current.Scenario.GetClosestPointsBetweenTwoAreas(this.GetLandTroopMovableArea(), i.GetLandTroopMovableArea(), out p1, out p2);
                    if (p1.HasValue && p2.HasValue)
                    {
                        if (pathFinder.GetPath(p1.Value, p2.Value, true))
                        {
                            this.AILandLinks.Add(i);
                            i.AILandLinks.Add(this);
                        }
                    }
                }
            }
        }

        private void FindWaterLinks(ArchitectureList allArch, int maxDistance)
        {
            if (!this.IsBesideWater) return;
            foreach (Architecture i in allArch)
            {
                if (i == this) continue;
                if (!i.IsBesideWater) continue;
                if (i.AIWaterLinks.GameObjects.Contains(this)) continue;
                pathFinder.startingArchitecture = this;
                pathFinder.targetArchitecture = i;
                pathFinder.MultipleWaterCost = false;
                pathFinder.MustUseWater = true;
                if (Session.Current.Scenario.GetSimpleDistance(i.Position, this.Position) < maxDistance)
                {
                    pathFinder.ConsumptionMax = 0.7f;
                    Point? p1;
                    Point? p2;
                    Session.Current.Scenario.GetClosestPointsBetweenTwoAreas(this.GetWaterTroopMovableArea(), i.GetWaterTroopMovableArea(), out p1, out p2);
                    if (p1.HasValue && p2.HasValue)
                    {
                        if (pathFinder.GetPath(p1.Value, p2.Value, true))
                        {
                            this.AIWaterLinks.Add(i);
                            i.AIWaterLinks.Add(this);
                        }
                    }
                }
            }
        }

        public void LoadFromString(GameArea gameArea, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            gameArea.Area.Clear();
            int minX = 10000, minY = 10000, maxX = -10000, maxY = -10000, x, y;
            for (int i = 0; i < strArray.Length; i += 2)
            {
                x = int.Parse(strArray[i]);
                y = int.Parse(strArray[i + 1]);
                gameArea.Area.Add(new Microsoft.Xna.Framework.Point(x, y));
                if (x < minX)
                    minX = x;
                if (x > maxX)
                    maxX = x;
                if (y < minY)
                    minY = y;
                if (y > maxY)
                    maxY = y;
            }
            gameArea.Centre = new Point((minX + maxX) / 2, (minY + maxY) / 2);
        }

        public void LoadAILandLinksFromString(ArchitectureList architectures, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.AILandLinks.Clear();
            foreach (string str in strArray)
            {
                Architecture gameObject = architectures.GetGameObject(int.Parse(str)) as Architecture;
                if (gameObject != null)
                {
                    this.AILandLinks.Add(gameObject);
                }
            }
        }

        public void LoadAIWaterLinksFromString(ArchitectureList architectures, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.AIWaterLinks.Clear();
            foreach (string str in strArray)
            {
                Architecture gameObject = architectures.GetGameObject(int.Parse(str)) as Architecture;
                if (gameObject != null)
                {
                    this.AIWaterLinks.Add(gameObject);
                }
            }
        }

        [DataMember]
        public string CaptivesString { get; set; }

        public List<string> LoadCaptivesFromString(CaptiveList captives, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                foreach (string str in strArray)
                {
                    Captive gameObject = captives.GetGameObject(int.Parse(str)) as Captive;
                    if (gameObject != null)
                    {
                        gameObject.CaptivePerson.LocationArchitecture = this;
                        gameObject.CaptivePerson.LocationTroop = null;
                        gameObject.CaptivePerson.Status = PersonStatus.Captive;
                    }
                    else
                    {
                        errorMsg.Add("俘虜ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("俘虜列表一栏应为半型空格分隔的俘虜ID");
            }
            return errorMsg;
        }

        public List<string> LoadFacilitiesFromString(FacilityList facilities, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Facilities.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Facility gameObject = facilities.GetGameObject(int.Parse(str)) as Facility;
                    if (gameObject != null)
                    {
                        this.Facilities.AddFacility(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("設施ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("設施列表一栏应为半型空格分隔的設施ID");
            }
            return errorMsg;
        }

        public List<string> LoadFundPacksFromString(string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.FundPacks.Clear();
            try
            {
                for (int i = 0; i < strArray.Length; i += 2)
                {
                    this.FundPacks.Add(new FundPack(int.Parse(strArray[i]), int.Parse(strArray[i + 1])));
                }
            }
            catch
            {
                errorMsg.Add("資金包應為半型空格分隔的數字，資金、日數相間");
            }
            return errorMsg;
        }

        public List<string> LoadFoodPacksFromString(string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.FoodPacks.Clear();
            try
            {
                for (int i = 0; i < strArray.Length; i += 2)
                {
                    this.FoodPacks.Add(new FoodPack(int.Parse(strArray[i]), int.Parse(strArray[i + 1])));
                }
            }
            catch
            {
                errorMsg.Add("糧草包應為半型空格分隔的數字，糧草、日數相間");
            }
            return errorMsg;
        }

        public List<string> LoadMilitariesFromString(MilitaryList militaries, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Militaries.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Military gameObject = militaries.GetGameObject(int.Parse(str)) as Military;
                    if (gameObject != null)
                    {
                        this.AddMilitary(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("編隊ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("編隊列表一栏应为半型空格分隔的編隊ID");
            }
            return errorMsg;
        }

        [DataMember]
        public string PersonsString { get; set; }

        [DataMember]
        public string MovingPersonsString { get; set; }

        [DataMember]
        public string NoFactionPersonsString { get; set; }

        [DataMember]
        public string NoFactionMovingPersonsString { get; set; }

        [DataMember]
        public string feiziliebiaoString { get; set; }

        public List<string> LoadPersonsFromString(Dictionary<int, Person> persons, string dataString, PersonStatus status)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            try
            {
                foreach (string str in strArray)
                {
                    Person t = persons[int.Parse(str)];
                    if (t != null && !Session.Current.Scenario.isInCaptiveList(t.ID))
                    {
                        t.LocationArchitecture = this;
                        t.LocationTroop = null;
                        t.Status = status;
                        if (status == PersonStatus.Moving || status == PersonStatus.NoFactionMoving)
                        {
                            t.TargetArchitecture = this;
                        }
                    }
                    else
                    {
                        if (t == null)
                        {
                            errorMsg.Add("人物ID" + str + "不存在");
                        }
                        else
                        {
                            errorMsg.Add("人物ID" + str + "已成為俘虜，不能出現在城池武將列表中");
                        }
                    }
                }
            }
            catch
            {
                errorMsg.Add("人物列表一栏应为半型空格分隔的人物ID");
            }
            return errorMsg;
        }

        public List<string> LoadPopulationPacksFromString(string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.PopulationPacks.Clear();
            try
            {
                for (int i = 0; i < strArray.Length; i += 2)
                {
                    this.PopulationPacks.Add(new PopulationPack(int.Parse(strArray[i]), int.Parse(strArray[i + 1])));
                }
            }
            catch
            {
                errorMsg.Add("人口包應為半型空格分隔的數字，糧草、日數相間");
            }
            return errorMsg;
        }

        public List<string> LoadMilitaryPopulationPacksFromString(string dataString)
        {
            if (dataString == null) return new List<string>();

            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.MilitaryPopulationPacks.Clear();
            try
            {
                for (int i = 0; i < strArray.Length; i += 2)
                {
                    this.MilitaryPopulationPacks.Add(new PopulationPack(int.Parse(strArray[i]), int.Parse(strArray[i + 1])));
                }
            }
            catch
            {
                errorMsg.Add("人口包應為半型空格分隔的數字，糧草、日數相間");
            }
            return errorMsg;
        }

        /*
        public void LoadSpyPacksFromString(string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.SpyPacks.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                Person gameObject = Session.Current.Scenario.Persons.GetGameObject(int.Parse(strArray[i])) as Person;
                if (gameObject != null)
                {
                    this.SpyPacks.Add(new SpyPack(gameObject, int.Parse(strArray[i + 1])));
                }
            }
        }
        */

        public List<string> LoadInformationsFromString(InformationList informations, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Informations.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Information gameObject = informations.GetGameObject(int.Parse(str)) as Information;
                    if (gameObject != null)
                    {
                        this.AddInformation(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("情報" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("情報一欄應為半型空格分隔的情報ID");
            }
            return errorMsg;
        }

        public bool MergeAvail()
        {
            for (int i = 0; i < this.Militaries.Count; i++)
            {
                Military military = this.Militaries[i] as Military;
                if ((military.Quantity != military.Kind.MaxScale) && (military.InjuryQuantity <= 0))
                {
                    foreach (Military military2 in this.Militaries)
                    {
                        if (((military != military2) && (military.Kind.Equals(military2.Kind))) && ((military2.Quantity < military2.Kind.MaxScale) && (military2.InjuryQuantity == 0)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void MonthEvent()
        {
            this.DevelopMonth();
            this.CheckIsFrontLine();
        }

        public bool MoraleAvail()
        {
            return (this.Kind.HasMorale && this.HasPerson());
        }

        public bool NewMilitaryAvail()
        {
            if (this.BelongedFaction != null)
            {
                if (!this.Kind.HasPopulation)
                {
                    return false;
                }
                foreach (MilitaryKind kind in this.BelongedFaction.AvailableMilitaryKinds.MilitaryKinds.Values)
                {
                    if (kind.CreateAvail(this))
                    {
                        return true;
                    }
                }
                foreach (MilitaryKind kind in this.PrivateMilitaryKinds.MilitaryKinds.Values)
                {
                    if (kind.CreateAvail(this))
                    {
                        return true;
                    }
                }
                if (this.BelongedFaction.MilitaryCount > this.BelongedFaction.CityTotalSize * Session.GlobalVariables.FactionMilitaryLimt + 1)
                {
                    return false;
                }
            }
            return false;
        }

        public bool NewSectionAvail()
        {
            return (this.BelongedFaction.ArchitectureCount > 1);
        }

        public int connectedToFactionArchitectureCount(Faction f)
        {
            int result = 0;
            foreach (Architecture a in this.GetAILinks())
            {
                if (a.BelongedFaction == f)
                {
                    result++;
                }
            }
            return result;
        }

        public int connectedNotToFactionArchitectureCount(Faction f)
        {
            int result = 0;
            foreach (Architecture a in this.GetAILinks())
            {
                if (a.BelongedFaction != f)
                {
                    result++;
                }
            }
            return result;
        }

        private int AttackableLandArmyScale
        {
            get
            {
                int result = 0;
                int troopCnt = 0;
                foreach (Military i in this.Militaries)
                {
                    if (i.Kind.Type != MilitaryType.水军)
                    {
                        result += i.Scales;
                        troopCnt++;
                        if (troopCnt >= this.Persons.Count) break;
                    }
                }
                return result;
            }
        }

        private int AttackableWaterArmyScale
        {
            get
            {
                int result = 0;
                int troopCnt = 0;
                foreach (Military i in this.Militaries)
                {
                    result += i.Scales / (i.Kind.Type == MilitaryType.水军 ? 1 : 2);
                    troopCnt++;
                    if (troopCnt >= this.Persons.Count) break;
                }
                return result;
            }
        }

        private long EstimatedIdleFightingForce()
        {
            GameObjectList targetPersons = this.Persons.GetList();
            targetPersons.IsNumber = true;
            targetPersons.SmallToBig = false;
            targetPersons.PropertyName = "FightingForce";
            targetPersons.ReSort();

            GameObjectList targetMilitaries = this.Militaries.GetList();
            targetMilitaries.IsNumber = true;
            targetMilitaries.SmallToBig = false;
            targetMilitaries.PropertyName = "KindMerit";
            targetMilitaries.ReSort();

            long result = 0;
            for (int i = 0; i < targetPersons.Count && i < targetMilitaries.Count; ++i)
            {
                result += (targetPersons[i] as Person).FightingForce * (targetMilitaries[i] as Military).KindMerit;
            }
            return result;
        }

        private int getArmyScaleRequiredForAttack(LinkNode wayToTarget)
        {
            Person leader = this.BelongedFaction.Leader;

            float totalScale = 0;
            
            if (wayToTarget.A.BelongedFaction != null)
            {
                int targetPersonCount = wayToTarget.A.PersonCount;
                targetPersonCount = Math.Min(wayToTarget.A.BelongedFaction.PersonCount, (int) (targetPersonCount + wayToTarget.A.Endurance / Session.Current.Scenario.Parameters.ArchitectureDamageRate * 25));
                int count = 0;
                foreach (Military m in wayToTarget.A.Militaries)
                {
                    if (count > targetPersonCount) break;
                    totalScale += m.Scales;
                    count++;
                }

                totalScale = (int) (totalScale * ((double)wayToTarget.A.EstimatedIdleFightingForce() / (this.EstimatedIdleFightingForce() + 1)));
            }
            else
            {
                totalScale = wayToTarget.A.ArmyScale;
            }

            if (Session.Current.Scenario.IsPlayer(wayToTarget.A.BelongedFaction))
            {
                totalScale = (int) (totalScale * Session.Parameters.AIOffensiveCampaignRequiredScaleFactor);
            }

            return (int)(totalScale + wayToTarget.A.Endurance / 15 +
                            (Session.Parameters.AIOffendDefendTroopAdd + (leader.Calmness - leader.Braveness + (3 - (int)leader.Ambition) * 2) * Session.Parameters.AIOffendDefendTroopMultiply));
        }

        private int getArmyReserveForOffensive()
        {
            int totalThreat = 0;
            int maxThreat = 0;
            int playerMaxThreat = 0;
            int playerTotalThreat = 0;
            foreach (LinkNode i in this.AIAllLinkNodes.Values)
            {
                if (i.Level > 1) break;
                if (i.A.BelongedFaction != null && !this.IsFriendlyWithoutTruce(i.A.BelongedFaction))
                {
                    int threat = (i.Kind == LinkKind.Land ? i.A.AttackableLandArmyScale : i.A.AttackableWaterArmyScale);
                    if (threat > maxThreat)
                    {
                        maxThreat = threat;
                        if (Session.Current.Scenario.IsPlayer(i.A.BelongedFaction))
                        {
                            playerMaxThreat = threat;
                        }
                    }
                    totalThreat += threat;
                    if (Session.Current.Scenario.IsPlayer(i.A.BelongedFaction))
                    {
                        playerTotalThreat += threat;
                    }
                }
            }
            int reserve;
            if (Session.GlobalVariables.PinPointAtPlayer && this.BelongedSection.AIDetail.OrientationKind == SectionOrientationKind.势力 &&
                    Session.Current.Scenario.IsPlayer(this.BelongedSection.OrientationFaction))
            {
                maxThreat = playerMaxThreat;
                totalThreat = playerTotalThreat;
            }
            Person leader = this.BelongedFaction.Leader;
            if (leader.Braveness >= 5 || leader.Calmness <= 5)
            {
                reserve = (int)(maxThreat * (0.8 + (leader.Calmness - leader.Braveness) * 0.1));
            }
            else
            {
                reserve = (int)(totalThreat / 2 * (0.8 + (leader.Calmness - leader.Braveness) * 0.1));
            }

            return reserve;
        }

        public void RemoveRoutewayToArchitecture(Architecture a)
        {
            Routeway toRemove = null;
            foreach (Routeway r in this.Routeways)
            {
                if (r.DestinationArchitecture == a)
                {
                    toRemove = r;
                    break;
                }
            }
            if (toRemove != null)
            {
                toRemove.RemoveAfterClose = true;
                toRemove.Close();
            }
        }

        private bool ignoreReserve = false;
        private Dictionary<LinkNode, Routeway> linkNodeRouteway = new Dictionary<LinkNode, Routeway>();
        private void OffensiveCampaign()
        {
            DateTime beforeStart = DateTime.UtcNow;

            Person leader = this.BelongedFaction.Leader;
            int reserveBase = this.getArmyReserveForOffensive();

            if (this.actuallyUnreachableArch.Contains(this.PlanArchitecture))
            {
                this.RemoveRoutewayToArchitecture(this.PlanArchitecture);
                this.PlanArchitecture = null;
            }

            if (this.BelongedSection != null && !this.BelongedSection.AIDetail.AllowOffensiveCampaign)
            {
                this.RemoveRoutewayToArchitecture(this.PlanArchitecture);
                this.PlanArchitecture = null;
            }
            else if (!this.HasPerson())
            {
                this.RemoveRoutewayToArchitecture(this.PlanArchitecture);
                this.PlanArchitecture = null;
            }
            else if (this.PlanArchitecture != null && this.IsFriendly(this.PlanArchitecture.BelongedFaction))
            {
                this.RemoveRoutewayToArchitecture(this.PlanArchitecture);
                this.PlanArchitecture = null;
            }
            else if ((this.PlanArchitecture != null) || ((this.IsGood() || GameObject.Chance((int)(GameObject.Square((int)leader.Ambition) * Session.Parameters.AIAttackChanceIfUnfull))) &&
              (this.Domination >= this.DominationCeiling * 0.7 || this.Population <= this.Kind.PopulationBoundary / 2)))
            {
                Architecture target = this.PlanArchitecture;
                LinkNode wayToTarget = null;
                if (target == null)
                {
                    ignoreReserve = false;
                    //choose target
                    int maxWeight = int.MinValue;
                    LinkNode maxNode = null;
                    int maxLevel = 1;
                    foreach (LinkNode i in this.AIAllLinkNodes.Values)
                    {
                        if (i.Level > maxLevel && maxNode != null)
                        {
                            break;
                        }
                        else if (i.Level > maxLevel)
                        {
                            maxLevel++;
                        }

                        if (this.actuallyUnreachableArch.Contains(i.A))
                        {
                            continue;
                        }
                        if (this.IsFriendly(i.A.BelongedFaction) || i.Kind == LinkKind.None)
                        {
                            continue;
                        }
                        if (GameObject.Chance(Session.Parameters.AIObeyStrategyTendencyChance) && this.BelongedFaction.Leader.StrategyTendency == PersonStrategyTendency.统一地区 && i.A.LocationState.LinkedRegion != this.LocationState.LinkedRegion)
                        {
                            continue;
                        }
                        if (GameObject.Chance(Session.Parameters.AIObeyStrategyTendencyChance) && this.BelongedFaction.Leader.StrategyTendency == PersonStrategyTendency.统一州 && i.A.LocationState != this.LocationState)
                        {
                            continue;
                        }
                        if (GameObject.Chance(Session.Parameters.AIObeyStrategyTendencyChance) && this.BelongedFaction.Leader.StrategyTendency == PersonStrategyTendency.维持现状)
                        {
                            continue;
                        }
                        if (this.actuallyUnreachableArch.Contains(i.A))
                        {
                            continue;
                        }
                        if (i.A.BelongedFaction != null && i.A.BelongedFaction.ArchitectureCount > 1 && i.A.connectedNotToFactionArchitectureCount(this.BelongedFaction) > 0)
                        {
                            if (this.BelongedSection.AIDetail.OrientationKind == SectionOrientationKind.军区)
                            {
                                continue;
                            }
                            if (this.BelongedSection.AIDetail.OrientationKind == SectionOrientationKind.势力)
                            {
                                if (this.BelongedSection.OrientationFaction != i.A.BelongedFaction)
                                {
                                    continue;
                                }
                            }
                            if (this.BelongedSection.AIDetail.OrientationKind == SectionOrientationKind.州域)
                            {
                                if (this.BelongedSection.OrientationState != i.A.LocationState)
                                {
                                    continue;
                                }
                            }
                            if (this.BelongedSection.AIDetail.OrientationKind == SectionOrientationKind.建筑)
                            {
                                if (this.BelongedSection.OrientationArchitecture != i.A)
                                {
                                    continue;
                                }
                            }
                            if (this.BelongedSection.AIDetail.OrientationKind == SectionOrientationKind.无)
                            {
                                if (i.A.BelongedFaction != null &&
                                    (Session.Current.Scenario.GetDiplomaticRelation(this.BelongedFaction.ID, i.A.BelongedFaction.ID) >= leader.Uncruelty * Session.Parameters.AIOffendMaxDiplomaticRelationMultiply)
                                    && GameObject.Random(leader.Uncruelty * leader.Uncruelty * 10) > 0)
                                {
                                    continue;
                                }
                            }
                        }

                        int reserve = Math.Max(0, reserveBase - i.A.ArmyScale);
                        int armyScaleRequiredForAttack = (int) (this.getArmyScaleRequiredForAttack(i));
                        int armyScaleHere = (i.Kind == LinkKind.Land ? this.LandArmyScale : (this.WaterArmyScale + this.LandArmyScale / 2));
                        if ((this.ArmyScale > this.MaxSupportableTroopScale) && GameObject.Random(20 * (5 - this.BelongedFaction.Leader.Ambition)) == 0)
                        {
                            ignoreReserve = true;
                        }
                        if ((armyScaleHere < armyScaleRequiredForAttack + reserve) && !ignoreReserve)
                        {
                            if ((GameObject.Random((5 - (int)leader.Ambition) * Session.Parameters.AIOffendIgnoreReserveProbAmbitionMultiply - Session.Parameters.AIOffendIgnoreReserveProbAmbitionAdd) == 0 &&
                                (GameObject.Random((leader.Calmness - leader.Braveness) * Session.Parameters.AIOffendIgnoreReserveProbBCDiffMultiply + Session.Parameters.AIOffendIgnoreReserveProbBCDiffAdd)) == 0) &&
                                (GameObject.Chance((int)(((double)armyScaleHere / i.A.ArmyScale - Session.Parameters.AIOffendIgnoreReserveChanceTroopRatioAdd) * Session.Parameters.AIOffendIgnoreReserveChanceTroopRatioMultiply))))
                            {
                                if (armyScaleHere >= armyScaleRequiredForAttack)
                                {
                                    ignoreReserve = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if ((Session.GlobalVariables.PopulationRecruitmentLimit && (this.ArmyQuantity > this.Population)) || this.Population <= 0 ||
                                !this.Kind.HasPopulation || !this.Kind.HasMorale)
                            {
                                if (armyScaleHere >= armyScaleRequiredForAttack)
                                {
                                    ignoreReserve = true;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }

                        Routeway rw;
                        if (!linkNodeRouteway.TryGetValue(i, out rw))
                        {
                            rw = this.GetRouteway(i, true);
                            linkNodeRouteway.Add(i, rw);
                        }

                        if (rw == null)
                        {
                            continue;
                        }

                        Architecture bypass = rw.ByPassHostileArchitecture;
                        LinkNode candidate = i;
                        if (bypass != null)
                        {
                            foreach (LinkNode j in this.AIAllLinkNodes.Values)
                            {
                                if (j.Level > maxLevel)
                                {
                                    break;
                                }
                                if (j.A == bypass)
                                {
                                    candidate = j;
                                }
                            }
                        }
                        if (!IsSelfFoodEnoughForOffensive(i, rw) && !this.IsFoodTwiceAbundant)
                        {
                            continue;
                        }

                        if (candidate == null) continue;

                        int weight = 1000 + (candidate.Kind == LinkKind.Land ? this.LandArmyScale : this.WaterArmyScale) - candidate.A.ArmyScale;
                        weight += weight / 10 * (candidate.A.connectedToFactionArchitectureCount(this.BelongedFaction) - candidate.A.connectedNotToFactionArchitectureCount(this.BelongedFaction));
                        if (i.A.IsImportant)
                        {
                            weight = weight * 3 / 2;
                        }
                        if (i.A.PopulationCeiling > 0 && this.PopulationCeiling > 0)
                        {
                            weight = (int)(weight * ((double)(candidate.A.Population - this.Population) / this.PopulationCeiling / 2 + 0.5));
                        }
                        else
                        {
                            if (i.A.PopulationCeiling <= 0)
                            {
                                weight /= 3;
                            }
                        }
                        if (weight > maxWeight)
                        {
                            maxWeight = weight;
                            maxNode = candidate;
                        }
                    }
                    wayToTarget = maxNode;
                }
                else
                {
                    //get way to target
                    this.AIAllLinkNodes.TryGetValue(this.PlanArchitecture.ID, out wayToTarget);
                }
                if (wayToTarget != null)
                {
                    int reserve = Math.Max(0, reserveBase - wayToTarget.A.ArmyScale);
                    int armyScaleRequiredForAttack = (int)(this.getArmyScaleRequiredForAttack(wayToTarget));
                    int armyScaleHere = (wayToTarget.Kind == LinkKind.Land ? this.LandArmyScale : (this.WaterArmyScale + this.LandArmyScale / 2));

                    if (armyScaleHere < armyScaleRequiredForAttack)
                    {
                        this.PlanArchitecture = null;
                        return;
                    }

                    if (this.BelongedFaction.IsArchitectureKnown(wayToTarget.A))
                    {
                            Routeway routeway = this.GetRouteway(wayToTarget, true);

                            if (routeway == null)
                            {
                                this.PlanArchitecture = null;
                            }
                            else
                            {
                                Architecture bypass = routeway.ByPassHostileArchitecture;
                                if (bypass != null)
                                {
                                    this.PlanArchitecture = bypass;
                                }
                                else if (Session.GlobalVariables.LiangdaoXitong && (routeway.LastPoint.BuildFundCost * (4 + ((wayToTarget.A.AreaCount >= 4) ? 2 : 0))) > this.Fund)
                                {
                                    routeway.Building = false;
                                    this.PlanArchitecture = wayToTarget.A;
                                }
                                else
                                {
                                    double foodRateBySeason = Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(routeway.Length));
                                    if (!(((this.Food * foodRateBySeason) >= (this.FoodCeiling / 3)) || this.IsSelfFoodEnoughForOffensive(wayToTarget, routeway)))
                                    {
                                        routeway.Building = false;
                                        this.PlanArchitecture = wayToTarget.A;
                                    }
                                    else if (Session.GlobalVariables.LiangdaoXitong && (routeway.LastPoint.ConsumptionRate >= 0.1f) && (((int)(routeway.Length * (routeway.LastPoint.ConsumptionRate + 0.2f))) > routeway.LastActivePointIndex))
                                    {
                                        routeway.Building = true;
                                        this.PlanArchitecture = wayToTarget.A;
                                    }
                                    else
                                    {
                                        if (!routeway.IsActive)
                                        {
                                            routeway.Building = true;
                                        }

                                        bool playerKnown = false;
                                        foreach (Faction f in Session.Current.Scenario.PlayerFactions)
                                        {
                                            if (f.IsArchitectureKnown(this) || f.IsArchitectureKnown(wayToTarget.A))
                                            {
                                                playerKnown = true;
                                            }
                                        }

                                        if (Session.GlobalVariables.AIQuickBattle && !playerKnown)
                                        {
                                            this.AIBattlingArchitectures.Add(wayToTarget.A);
                                            this.PlanArchitecture = null;
                                        }
                                        else
                                        {

                                            bool hasCreatedTroop = this.BuildOffensiveTroop(wayToTarget.A, wayToTarget.Kind, true, ignoreReserve ? 0 : reserve);
                                            if (armyScaleHere <= reserve || !hasCreatedTroop)
                                            {
                                                this.PlanArchitecture = null;
                                            }
                                        }
                                    }
                                }
                            }
                    }
                    else if (this.InformationAvail())
                    {
                        Routeway routeway = this.GetRouteway(wayToTarget, true);
                        if ((routeway != null) && ((routeway.LastPoint.BuildFundCost * (4 + ((wayToTarget.A.AreaCount >= 4) ? 2 : 0))) <= this.Fund))
                        {
                            double foodRateBySeason = Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(routeway.Length));
                            if (((this.Food * foodRateBySeason) >= (this.FoodCeiling / 3)) || this.IsSelfFoodEnoughForOffensive(wayToTarget, routeway))
                            {
                                this.PlanArchitecture = wayToTarget.A;
                                Person firstHalfPerson = this.GetFirstHalfPerson("InformationAbility");
                                if (firstHalfPerson != null && firstHalfPerson.LocationArchitecture != null)
                                {
                                    firstHalfPerson.CurrentInformationKind = this.GetFirstHalfInformationKind();
                                    if (firstHalfPerson.CurrentInformationKind != null)
                                    {
                                        List<Architecture> gettingInfo = GettingInformationArchitectures();
                                        if (!gettingInfo.Contains(wayToTarget.A))
                                        {
                                            firstHalfPerson.GoForInformation(Session.Current.Scenario.GetClosestPoint(wayToTarget.A.ArchitectureArea, this.Position));
                                        }
                                    }
                                }
                                else
                                {
                                    this.PlanArchitecture = null;
                                }
                            }
                        }
                    }

                }
            }
        }

        public bool PersonConveneAvail()
        {
            int num = 0;
            if (this.BelongedFaction != null)
            {
                foreach (Architecture architecture in this.BelongedFaction.Architectures)
                {
                    if (architecture != this)
                    {
                        num += architecture.Persons.Count;
                    }
                }
            }
            return (num > 0);
        }

        public bool PersonStudySkillAvail()
        {
            foreach (Person person in this.Persons)
            {
                if (person.HasLearnableSkill)
                {
                    return true;
                }
            }
            return false;
        }

        public bool PersonStudyStuntAvail()
        {
            foreach (Person person in this.Persons)
            {
                if (person.HasLearnableStunt)
                {
                    return true;
                }
            }
            return false;
        }

        public bool PersonStudyTitleAvail()
        {
            foreach (Person person in this.Persons)
            {
                if (person.HasLearnableTitle)
                {
                    return true;
                }
            }
            return false;
        }

        public bool PersonTransferAvail()
        {
            return ((this.BelongedFaction != null) && ((this.MovablePersons.Count > 0) && (this.BelongedFaction.ArchitectureCount > 1)));
        }

        public bool TransportAvailable()
        {
            return this.BelongedFaction != null && (this.Fund > 0 || this.Food > 0) && this.BelongedFaction.ArchitectureCount > 1;
        }

        public void PlayerAISearch()
        {
            this.AIAutoSearch();
        }

        public void PlayerAIWork()
        {
            this.AIWork(true);
        }

        public void PlayerAIRecruit()
        {
            this.AutoRecruit();
        }

        public void PlayerAIHire()
        {
            if (this.ConvincePersonAvail())
            {
                if (this.NoFactionPersons.Count > 0)
                {
                    ConvinceNoFactionAI();
                }

                if (this.Captives.Count > 0)
                {
                    ConvinceCaptivesAI(this);
                }
            }
        }



        public void PlayerAutoAI()
        {
            if (this.AutoWorking)
            {
                this.PlayerAIWork();
            }
            if (this.AutoRecruiting)
            {
                this.PlayerAIRecruit();
            }
            if (this.AutoHiring)
            {
                this.PlayerAIHire();
            }
            if (this.AutoSearching)
            {
                this.PlayerAISearch();
            }
           /* if (this.AutoZhaoXian)
            {
                this.PlayAIZhaoXian();
            }*/
        }

        public void callReturnedOfficerToWork()
        {
            if (this.AutoWorking || this.BelongedSection.AIDetail.AutoRun)
            {
                if (this.BelongedSection.AIDetail.AutoRun)
                {
                    this.AIWork(false);
                }
                else
                {
                    this.PlayerAIWork();
                    if (this.AutoRecruiting)
                    {
                        this.PlayerAIRecruit();
                    }
                }
            }
            if (this.AutoSearching)
            {
                this.PlayerAISearch();
            }

        }

        private void PopulationEscapeEvent()
        {
            if ((((!this.DayAvoidPopulationEscape && this.Kind.HasPopulation) && ((this.Domination < this.DominationCeiling) && (this.RecentlyAttacked > 0))) && ((this.Population > (0x3e8 * this.AreaCount)) && (this.Morale < this.MoraleCeiling))) && (GameObject.Random(((int)Math.Pow((double)(this.Domination + this.Morale), 2.0)) + 0x3e8) < GameObject.Random(0x3e8)))
            {
                int num = 0;
                int maxValue = this.Population / 100;
                foreach (Architecture architecture in this.GetAILinks().GetRandomList())
                {
                    if (architecture.Kind.HasPopulation)
                    {
                        //architecture.AddPopulationPack((int)(Session.Current.Scenario.GetDistance(this.ArchitectureArea, architecture.ArchitectureArea) / 2.0), 1 + GameObject.Random(maxValue));
                        architecture.AddPopulationPack((int)(Session.Current.Scenario.GetDistance(this.ArchitectureArea, architecture.ArchitectureArea) / 2.0) * Session.Parameters.DayInTurn, 1 + GameObject.Random(maxValue));
                        num++;
                    }
                    if (num >= 100)
                    {
                        break;
                    }
                }
                if (num > 0)
                {
                    int decrement = maxValue * num;
                    int militaryPopDecrement = (int) (decrement * ((float)MilitaryPopulation / Population));
                    this.DecreasePopulation(decrement);
                    this.DecreaseMilitaryPopulation(militaryPopDecrement);
                    ExtensionInterface.call("PopulationEscape", new Object[] { Session.Current.Scenario, this, decrement });
                    if (this.OnPopulationEscape != null)
                    {
                        this.OnPopulationEscape(this, decrement);
                    }
                }
            }
        }

        public string PopulationInInformationLevel(InformationLevel level)
        {
            switch (level)
            {
                case InformationLevel.未知:
                    return "----";

                case InformationLevel.无:
                    return "----";

                case InformationLevel.低:
                    return StaticMethods.GetNumberStringByGranularity(this.Population, 0x186a0);

                case InformationLevel.中:
                    return StaticMethods.GetNumberStringByGranularity(this.Population, 0xc350);

                case InformationLevel.高:
                    return StaticMethods.GetNumberStringByGranularity(this.Population, 0x2710);

                case InformationLevel.全:
                    return this.Population.ToString();
            }
            return "----";
        }

        public string MilitaryPopulationInInformationLevel(InformationLevel level)
        {
            switch (level)
            {
                case InformationLevel.未知:
                    return "----";

                case InformationLevel.无:
                    return "----";

                case InformationLevel.低:
                    return StaticMethods.GetNumberStringByGranularity(this.MilitaryPopulation, 0x186a0);

                case InformationLevel.中:
                    return StaticMethods.GetNumberStringByGranularity(this.MilitaryPopulation, 0xc350);

                case InformationLevel.高:
                    return StaticMethods.GetNumberStringByGranularity(this.MilitaryPopulation, 0x2710);

                case InformationLevel.全:
                    return this.MilitaryPopulation.ToString();
            }
            return "----";
        }


        public void PopulationPacksDayEvent()
        {
            for (int i = this.PopulationPacks.Count - 1; i >= 0; i--)
            {
                PopulationPack local1 = this.PopulationPacks[i];
                //local1.Days--;
                local1.Days -= Session.Parameters.DayInTurn;
                if (this.PopulationPacks[i].Days <= 0)
                {
                    this.ReceivePopulation(this.PopulationPacks[i].Population);
                    this.PopulationPacks.RemoveAt(i);
                }
            }

            for (int i = this.MilitaryPopulationPacks.Count - 1; i >= 0; i--)
            {
                PopulationPack local1 = this.MilitaryPopulationPacks[i];
                //local1.Days--;
                local1.Days -= Session.Parameters.DayInTurn;
                if (this.MilitaryPopulationPacks[i].Days <= 0)
                {
                    this.ReceivePopulation(this.MilitaryPopulationPacks[i].Population);
                    this.ReceiveMilitaryPopulation(this.MilitaryPopulationPacks[i].Population);
                    this.MilitaryPopulationPacks.RemoveAt(i);
                }
            }
        }

        /*
        public void PostCreateTroop(Troop troop, bool hand)
        {
            if ((this.BelongedFaction != null) && this.HasSpy)
            {
                this.AddMessageToTodayNewTroopSpyMessage(troop, hand);
            }
        }
        */

        private void PrepareAI()
        {
            this.TotalHostileForce = 0;
            this.TotalFriendlyForce = 0;
            TroopList hostileTroopsInView = this.GetHostileTroopsInView();
            foreach (Troop troop in hostileTroopsInView)
            {
                this.TotalHostileForce += troop.FightingForce;
            }
            if (this.DefensiveLegion == null)
            {
                TroopList friendlyTroopsInView = this.GetFriendlyTroopsInView();
                foreach (Troop troop in friendlyTroopsInView)
                {
                    this.TotalFriendlyForce += troop.FightingForce;
                }
            }
            else
            {
                foreach (Troop i in this.DefensiveLegion.Troops)
                {
                    this.TotalFriendlyForce += i.FightingForce;
                }
            }

            this.TotalStoredForce = 0;
            if (this.Persons.Count > 0) {
                int fightingMerit = 0;

                GameObjectList maxList = this.Persons.GetList();
                maxList.PropertyName = "FightingForce";
                maxList.IsNumber = true;
                maxList.SmallToBig = false;
                maxList.ReSort();
                int cnt = 0;
                foreach (Person p in maxList)
                {
                    cnt++;
                    fightingMerit += p.FightingForce;
                    if (cnt > 5 || cnt > this.MilitaryCount) break;
                }
                int avgFightingMerit = fightingMerit / cnt;
                foreach (Military m in this.Militaries)
                {
                    this.TotalStoredForce += (int) (((m.Offence + m.Defence) * m.Morale / 100.0 + m.Combativity / 4) * fightingMerit / 100);
                }
            }
        }

        public void PurifyFacilityInfluences()
        {
            foreach (Facility facility in this.Facilities)
            {
                if (facility.MaintenanceCost > 0)
                {
                    facility.Influences.PurifyInfluence(this, Applier.Facility, facility.ID);
                }
            }
        }

        private void QuickSortArchitecturesDistance(ArchitectureList List, int begin, int end)
        {
            if (begin < end)
            {
                int num = this.QuickSortPartitionArchitecturesDistance(List, begin, end);
                if (begin < (num - 1))
                {
                    this.QuickSortArchitecturesDistance(List, begin, num - 1);
                }
                if ((num + 1) < end)
                {
                    this.QuickSortArchitecturesDistance(List, num + 1, end);
                }
            }
        }

        private int QuickSortPartitionArchitecturesDistance(ArchitectureList List, int begin, int end)
        {
            Architecture architecture = List[begin] as Architecture;
            int simpleDistance = Session.Current.Scenario.GetSimpleDistance(architecture.Position, this.Position);
            int num2 = begin;
            while (begin < end)
            {
                int num3 = Session.Current.Scenario.GetSimpleDistance((List[end] as Architecture).Position, this.Position);
                while ((begin < end) && (num3 >= simpleDistance))
                {
                    end--;
                    num3 = Session.Current.Scenario.GetSimpleDistance((List[end] as Architecture).Position, this.Position);
                }
                if (begin >= end)
                {
                    return begin;
                }
                this.QuickSortSwapArchitectureDistance(List, begin, end);
                begin++;
                for (num3 = Session.Current.Scenario.GetSimpleDistance((List[begin] as Architecture).Position, this.Position); (begin < end) && (num3 <= simpleDistance); num3 = Session.Current.Scenario.GetSimpleDistance((List[begin] as Architecture).Position, this.Position))
                {
                    begin++;
                }
                if (begin >= end)
                {
                    return begin;
                }
                this.QuickSortSwapArchitectureDistance(List, begin, end);
                end--;
            }
            return begin;
        }

        private void QuickSortSwapArchitectureDistance(ArchitectureList List, int i, int j)
        {
            GameObject obj2 = List[i];
            List[i] = List[j];
            List[j] = obj2;
        }

        public ArchitectureDamage ReceiveAttackDamage(ArchitectureDamage receivedDamage)
        {
            if (receivedDamage.Damage > 0)
            {
                int maxValue = 2 + (receivedDamage.Damage / 5);
                this.DecreaseAgriculture(GameObject.Random(maxValue));
                this.DecreaseCommerce(GameObject.Random(maxValue));
                this.DecreaseTechnology(GameObject.Random(maxValue));
                this.DecreaseMorale(GameObject.Random(maxValue));
                ExtensionInterface.call("ArchitectureReceiveDamage", new Object[] { Session.Current.Scenario, this, receivedDamage });
            }
            return receivedDamage;
        }

        private void ReceivePopulation(int quantity)
        {
            int population = this.Population;
            quantity = this.IncreasePopulation(quantity);
            if (quantity > 0)
            {
                if (this.BelongedFaction != null)
                {
                    float decrease = this.Domination - ((float) this.Domination * population) / this.Population;
                    if (decrease > 1)
                    {
                        this.Domination -= (int) decrease;
                        decrease -= (int)decrease;
                    }
                    if (GameObject.Chance((int) (decrease * 100)))
                    {
                        this.Domination--;
                    }

                    decrease = this.Morale - ((float)this.Morale * population) / this.Population;
                    if (decrease > 1)
                    {
                        this.Morale -= (int)decrease;
                        decrease -= (int)decrease;
                    }
                    if (GameObject.Chance((int)(decrease * 100)))
                    {
                        this.Morale--;
                    }
                }
                if (this.OnPopulationEnter != null)
                {
                    this.OnPopulationEnter(this, quantity);
                }
                ExtensionInterface.call("ReceivePopulation", new Object[] { Session.Current.Scenario, this, quantity });
            }
        }

        private void ReceiveMilitaryPopulation(int quantity)
        {
            this.ReceivePopulation(quantity);
            this.IncreaseMilitaryPopulation(quantity);
        }

        public bool RecruitmentAvail()
        {
            if (this.HasPerson())
            {
                if (this.youzainan)
                {
                    return false;
                }
                if (!this.Kind.HasPopulation || !this.Kind.HasMorale)
                {
                    return false;
                }
                if (Session.GlobalVariables.PopulationRecruitmentLimit && (this.ArmyQuantity > this.Population))
                {
                    return false;
                }
                if (this.BelongedFaction != null && this.BelongedFaction.Army > (long)(this.BelongedFaction.Population * Session.Current.Scenario.Parameters.MilitaryPopulationCap)) //势力兵力超过上限时，不能补充,原有参数替换成在主菜单设置可调的参数
                {
                    return false;
                }
                if (this.Population <= 0 || this.MilitaryPopulation <= 0)
                {
                    return false;
                }
                if (this.Domination < Session.Parameters.RecruitmentDomination)
                {
                    return false;
                }
                if (this.Morale < Session.Parameters.RecruitmentMorale)
                {
                    return false;
                }
                foreach (Military military in this.Militaries)
                {
                    if ((military.Quantity < military.Kind.MaxScale) && (military.InjuryQuantity == 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

#pragma warning disable CS0414 // The field 'Architecture.crlm_recurse_level' is assigned but its value is never used
        private int crlm_recurse_level = 0;
#pragma warning restore CS0414 // The field 'Architecture.crlm_recurse_level' is assigned but its value is never used
        private bool CanRecruitLowerMilitary_r(MilitaryKind mk)
        {
            MilitaryKind current;
            Dictionary<int, MilitaryKind>.ValueCollection.Enumerator enumerator;
            using (enumerator = this.BelongedFaction.AvailableMilitaryKinds.MilitaryKinds.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (current == mk)
                    {
                        return true;
                    }
                }
            }
            using (enumerator = this.PrivateMilitaryKinds.MilitaryKinds.Values.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    if (current == mk)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public bool CanRecruitMilitary(MilitaryKind mk)
        {
            crlm_recurse_level = 0;
            return CanRecruitLowerMilitary_r(mk);
        }

        private void RecruitmentMilitary(Military military)
        {

            if ((((this.MilitaryPopulation != 0) && (this.Population != 0) && (!Session.GlobalVariables.PopulationRecruitmentLimit
                || (this.ArmyQuantity <= this.Population)))  && ((this.Fund >= (Session.Parameters.RecruitmentFundCost * this.AreaCount * (this.CanRecruitMilitary(military.Kind) ? 1 : 10))))
                && (this.Domination >= Session.Parameters.RecruitmentDomination) && ((this.Morale >= Session.Parameters.RecruitmentMorale)
                && ((military.RecruitmentPerson != null) && (military.RecruitmentPerson.BelongedFaction != null)
                && (military.Quantity < military.Kind.MaxScale)) && (military.BelongedFaction != null))))
            {

                int randomValue = StaticMethods.GetRandomValue((int)((military.RecruitmentPerson.RecruitmentAbility * military.Kind.MinScale) * Session.Parameters.RecruitmentRate), 0x7d0);
                randomValue = (int)((float)randomValue * Math.Min(1.0f, ((float)this.Population * Session.Current.Scenario.Parameters.RecruitPopualationDecreaseRate / Math.Max(1, this.ArmyQuantity))));
                int populationDecrement;

                if ((randomValue + military.Quantity) > military.Kind.MaxScale)
                {
                    randomValue = military.Kind.MaxScale - military.Quantity;
                }
                if ((randomValue * military.Kind.PointsPerSoldier) > military.BelongedFaction.TechniquePoint && military.Kind.PointsPerSoldier != 0)
                {
                    if (!(((this.BelongedSection == null) || (this.BelongedSection.AIDetail == null)) || this.BelongedSection.AIDetail.AutoRun))
                    {
                        military.BelongedFaction.DepositTechniquePointForTechnique(randomValue * military.Kind.PointsPerSoldier);
                    }
                    randomValue = military.BelongedFaction.TechniquePoint / military.Kind.PointsPerSoldier;
                }
                populationDecrement = randomValue;
                if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction))
                {
                    randomValue = (int)(randomValue * Session.Parameters.AIRecruitmentSpeedRate);
                }
                if (randomValue > 0)
                {
                    this.DecreaseFund(Session.Parameters.RecruitmentFundCost * this.AreaCount * (this.CanRecruitMilitary(military.Kind) ? 1 : 10));
                    if (populationDecrement > this.MilitaryPopulation)
                    {
                        populationDecrement = this.MilitaryPopulation;
                        randomValue = populationDecrement;
                    }
                    if (populationDecrement > this.Population)
                    {
                        populationDecrement = this.Population;
                        randomValue = populationDecrement;
                    }
                    this.DecreaseMilitaryPopulation(populationDecrement);
                    this.DecreasePopulation(populationDecrement);

                    int scales = military.Scales;
                    military.IncreaseQuantity(randomValue, this.MoraleOfRecruitment, this.CombativityOfRecruitment, 0, 0);
                    /*
                    if (this.HasSpy && ((military.Scales / 10) > (scales / 10)))
                    {
                        this.AddMessageToTodayMilitaryScaleSpyMessage(military);
                    }
                    */
                    if (this.Population < this.RecruitmentPopulationBoundary)
                    {
                        this.DecreaseDomination(GameObject.Random(6));
                        this.DecreaseMorale(GameObject.Random(6) * 2);
                    }
                    else
                    {
                        this.DecreaseDomination(GameObject.Random(2));
                        this.DecreaseMorale(GameObject.Random(2) * 2);
                    }
                    this.BelongedFaction.DecreaseTechniquePoint(randomValue * military.Kind.PointsPerSoldier);
                    int increment = StaticMethods.GetRandomValue(randomValue * 10, military.Kind.MinScale);
                    if (increment > 0)
                    {
                        military.RecruitmentPerson.AddRecruitmentExperience(increment);
                        military.RecruitmentPerson.AddCommandExperience(increment / 2);
                        military.RecruitmentPerson.AddGlamourExperience(increment / 2);
                        military.RecruitmentPerson.IncreaseReputation(increment * 4);
                        military.RecruitmentPerson.IncreaseOfficerMerit(increment * 4);
                        military.RecruitmentPerson.BelongedFaction.IncreaseReputation(increment * 2);
                        military.RecruitmentPerson.BelongedFaction.IncreaseTechniquePoint(increment * 100);
                    }
                }

            }
            else
            {
                if (military.RecruitmentPerson != null)
                {
                    military.StopRecruitment();
                }
            }
        }

        public void RecruitmentMilitary(Military military, float scale)
        {

            if ((((this.MilitaryPopulation != 0) && (this.Population != 0) && (!Session.GlobalVariables.PopulationRecruitmentLimit || (this.ArmyQuantity <= this.Population))) && ((this.Domination >= Session.Parameters.RecruitmentDomination) && (this.Morale >= Session.Parameters.RecruitmentMorale))) && (military.Quantity < military.Kind.MaxScale))
            {
                int decrement = (int)(military.Kind.MinScale * scale);
                int populationDecrement;
                if ((decrement + military.Quantity) > military.Kind.MaxScale)
                {
                    decrement = military.Kind.MaxScale - military.Quantity;
                }
                if ((decrement * military.Kind.PointsPerSoldier) > military.BelongedFaction.TechniquePoint)
                {
                    if (!(((this.BelongedSection == null) || (this.BelongedSection.AIDetail == null)) || this.BelongedSection.AIDetail.AutoRun))
                    {
                        military.BelongedFaction.DepositTechniquePointForTechnique(decrement * military.Kind.PointsPerSoldier);
                    }
                    decrement = military.BelongedFaction.TechniquePoint / military.Kind.PointsPerSoldier;
                }
                populationDecrement = decrement;
                if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction))
                {
                    decrement = (int)(decrement * Session.Parameters.AIRecruitmentSpeedRate);
                }
                if (decrement > 0)
                {
                    if (populationDecrement > this.MilitaryPopulation)
                    {
                        populationDecrement = this.MilitaryPopulation;
                        decrement = populationDecrement;
                    }
                    if (populationDecrement > this.Population)
                    {
                        populationDecrement = this.Population;
                        decrement = populationDecrement;
                    }
                    this.DecreaseMilitaryPopulation(populationDecrement);
                    this.DecreasePopulation(populationDecrement);
                    int scales = military.Scales;
                    military.IncreaseQuantity(decrement, this.MoraleOfRecruitment, this.CombativityOfRecruitment, 0, 0);
                    /*
                    if (this.HasSpy && ((military.Scales / 10) > (scales / 10)))
                    {
                        this.AddMessageToTodayMilitaryScaleSpyMessage(military);
                    }
                    */
                    if (this.Population < this.RecruitmentPopulationBoundary)
                    {
                        this.DecreaseDomination(GameObject.Random(6));
                        this.DecreaseMorale(GameObject.Random(6) * 2);
                    }
                    else
                    {
                        this.DecreaseDomination(GameObject.Random(2));
                        this.DecreaseMorale(GameObject.Random(2) * 2);
                    }
                    this.BelongedFaction.DecreaseTechniquePoint(decrement * military.Kind.PointsPerSoldier);
                    int randomValue = StaticMethods.GetRandomValue(decrement * 10, military.Kind.MinScale);
                    if (randomValue > 0)
                    {
                        military.BelongedFaction.IncreaseReputation(randomValue * 2);
                        military.BelongedFaction.IncreaseTechniquePoint(randomValue * 100);
                    }
                }

            }
        }

        public bool RedeemAvail()
        {
            if (this.FactionHasSelfCaptive())
            {
                return this.GetRedeemCaptiveList().Count > 0;
            }
            return false;
        }

        public void RefreshViewArea()
        {
            if (!Session.Current.Scenario.Preparing)
            {
                foreach (Point point in this.ViewArea.Area)
                {
                    if (!Session.Current.Scenario.PositionOutOfRange(point))
                    {
                        Session.Current.Scenario.MapTileData[point.X, point.Y].RemoveHighViewingArchitecture(this);
                    }
                }
                foreach (Point point in this.LongViewArea.Area)
                {
                    if (!Session.Current.Scenario.PositionOutOfRange(point))
                    {
                        Session.Current.Scenario.MapTileData[point.X, point.Y].RemoveViewingArchitecture(this);
                    }
                }
            }
            this.ViewArea = null;
            this.LongViewArea = null;
            foreach (Point point in this.ViewArea.Area)
            {
                if (!Session.Current.Scenario.PositionOutOfRange(point))
                {
                    Session.Current.Scenario.MapTileData[point.X, point.Y].AddHighViewingArchitecture(this);
                }
            }
            foreach (Point point in this.LongViewArea.Area)
            {
                if (!Session.Current.Scenario.PositionOutOfRange(point))
                {
                    Session.Current.Scenario.MapTileData[point.X, point.Y].AddViewingArchitecture(this);
                }
            }
        }

        public bool RegionCoreEffectAvail()
        {
            return (this.Kind.HasTechnology && (this.Technology >= ((int)(this.TechnologyCeiling * 0.8))));
        }

        public bool RegroupSectionAvail()
        {
            return (this.BelongedFaction.SectionCount > 0);
        }

        private void ReleaseAllCaptive()
        {
            if (this.HasCaptive())
            {
                PersonList persons = new PersonList();
                foreach (Captive captive in this.Captives.GetList())
                {
                    if (((captive.CaptivePerson != null) && (captive.CaptiveFaction != null)) && (captive.CaptiveFaction.Capital != null))
                    {
                        Architecture moveTo = captive.CaptiveFaction.Capital;
                        persons.Add(captive.CaptivePerson);
                        Person p = captive.CaptivePerson;
                        captive.CaptivePerson.SetBelongedCaptive(null, PersonStatus.Normal);
                        p.MoveToArchitecture(moveTo);
                    }
                }
                if ((persons.Count > 0) && (this.OnReleaseCaptiveAfterOccupied != null))
                {
                    this.OnReleaseCaptiveAfterOccupied(this, persons);
                }
            }
        }

        public bool ReleaseCaptiveAvail()
        {
            return (this.BelongedFaction.CaptiveCount > 0);
        }

        public void RemoveBaseSupplyingArchitecture()
        {
            foreach (Point point in this.BaseFoodSurplyArea.Area)
            {
                if (!Session.Current.Scenario.PositionOutOfRange(point))
                {
                    Session.Current.Scenario.MapTileData[point.X, point.Y].RemoveSupplyingArchitecture(this);
                }
            }
        }

        public void RemoveInactiveRouteways()
        {
            foreach (Routeway routeway in this.Routeways.GetList())
            {
                if (!(routeway.Building || (routeway.LastActivePointIndex >= 0)))
                {
                    Session.Current.Scenario.RemoveRouteway(routeway);
                }
            }
        }

        public void RemoveMilitary(Military military)
        {
            this.Militaries.Remove(military);
            military.StopRecruitment();
            military.BelongedArchitecture = null;
        }

        public void RemovePopulationPack(PopulationPack pp)
        {
            this.PopulationPacks.Remove(pp);
        }

        /*
        public void RemoveSpyPack(SpyPack sp)
        {
            this.SpyPacks.Remove(sp);
        }
        */

        private void ResetAuto()
        {
            this.AutoHiring = false;
            this.AutoRewarding = false;
            this.AutoWorking = false;
            this.AutoSearching = false;
           // this.AutoZhaoXian = false;
        }

        private void ResetDayInfluence()
        {
            if (this.RecentlyAttacked > 0)
            {
                this.RecentlyAttacked--;
            }
            if (this.RecentlyBreaked > 0)
            {
                this.RecentlyBreaked--;
            }
            if (this.RecentlyHit > 0)
            {
                this.RecentlyHit--;
            }
        }

        public bool ResetDiplomaticRelationAvail()
        {
            if (this.BelongedFaction == null)
            {
                return false;
            }
            return (this.HasFriendlyDiplomaticRelation && (this.BelongedFaction.TroopCountExcludeTransport == 0));
        }

        public bool AllyDiplomaticRelationAvail()
        {
            if (this.BelongedFaction == null)
            {
                return false;
            }

            foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
            {
                if ((display.Relation <= Session.GlobalVariables.FriendlyDiplomacyThreshold) && (display.Relation >= Session.GlobalVariables.FriendlyDiplomacyThreshold * 0.9) && (this.Fund > 20000) && (this.Persons.Count > 0))
                {
                    return true;
                }
            }

            return false;
        }

        public bool EnhanceDiplomaticRelationAvail()
        {
            if (this.BelongedFaction == null)
            {
                return false;
            }
            return ((this.Fund > 10000) && (this.Persons.Count > 0));
        }

        public GameObjectList GetGeDiDiplomaticRelationList() //割地
        {
            this.GeDiDiplomaticRelationList.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if (display.LinkedFaction1 != null && display.LinkedFaction2 != null)
                    {
                        this.GeDiDiplomaticRelationList.Add(display);
                    }
                }
            }
            return this.GeDiDiplomaticRelationList;
        }

        public bool GeDiDiplomaticRelationAvail() //割地
        {
            if (this.BelongedFaction == null)
            {
                return false;
            }

            return (this.BelongedFaction.ArchitectureCount > 1 && this.MovablePersons.Count > 0);
        }


        public bool QuanXiangDiplomaticRelationAvail() //劝降
        {

                if (!Session.GlobalVariables.PermitQuanXiang) return false;

                if (this.BelongedFaction == null)
                {
                    return false;
                }

                foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
                {
                    if ((display.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold) && this.Fund > 50000 && (this.MovablePersons.Count > 0))
                    {
                        if (Session.Current.Scenario.IsPlayer(this.BelongedFaction) && this.GetQuanXiangDiplomaticRelationList().Count > 0)
                        {
                            return true;
                        }

                       /* if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction) && this.GetAIQuanXiangDiplomaticRelationList().Count > 0)
                        {
                            return true;
                        }*/
                    }
                }


                return false;

        }

        public bool TruceDiplomaticRelationAvail()
        {
            if (this.BelongedFaction == null)
            {
                return false;
            }
            return ((this.Fund > 50000) && (this.Persons.Count > 0));
        }

        public bool DenounceDiplomaticRelationAvail()
        {
            if (this.BelongedFaction == null)
            {
                return false;
            }

            foreach (DiplomaticRelationDisplay display in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationDisplayListByFactionID(this.BelongedFaction.ID))
            {
                if ((display.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold) && (this.Fund > 120000))
                {
                    return true;
                }
            }

            return false;
        }


        public void ResetFaction(Faction faction)
        {
            Faction oldFaction = this.BelongedFaction;
            this.ResetAuto();
            this.PlanFacilityKind = null;
            this.PlanFacilityKindID = -1;
            this.SuspendTroopTransfer = 0;
            if ((faction != null) && Session.Current.Scenario.IsPlayer(faction))
            {
                this.AutoHiring = true;
                this.AutoRewarding = true;

            }
            if (this.BelongedFaction != null && this.BelongedFaction != faction)
            {
                this.ClearFundPacks();
                this.ClearFoodPacks();
                this.ClearRouteways();
                this.ReleaseAllCaptive();
                this.PurifyFactionInfluences();
                if (this.BelongedSection != null)
                {
                    this.BelongedSection.RemoveArchitecture(this);
                }
                this.DefensiveLegion = null;
                if (this == this.BelongedFaction.Capital || this.BelongedFaction.ArchitectureCount <= 1)
                {
                    Person leader = this.BelongedFaction.Leader;
                    while (this.Persons.Count > 0)
                    {
                        Person person2 = this.Persons[0] as Person;

                        Session.Current.Scenario.YearTable.addBecomeNoFactionDueToDestructionEntry(Session.Current.Scenario.Date, person2, this.BelongedFaction);
                        person2.Status = PersonStatus.NoFaction;
                        person2.LocationArchitecture = this;
                        if (leader == person2)
                        {
                            person2.Reputation = (int)(person2.Reputation * 0.6);
                        }
                        else
                        {
                            person2.Reputation = (int)(person2.Reputation * 0.95);
                        }

                        int hateDays = (int)((Math.Pow(5, person2.PersonalLoyalty - 2) * 10 + GameObject.Random(20) - 30) * Session.GlobalVariables.ProhibitFactionAgainstDestroyer);
                        if (hateDays > 0)
                        {
                            if (person2.ProhibitedFactionID.ContainsKey(faction.ID))
                            {
                                person2.ProhibitedFactionID[faction.ID] += hateDays;
                            }
                            else
                            {
                                person2.ProhibitedFactionID.Add(faction.ID, hateDays);
                            }
                        }
                    }
                    //this.Persons.Clear();
                    while (this.MovingPersons.Count > 0)
                    {
                        Person person2 = this.MovingPersons[0] as Person;
                        Session.Current.Scenario.YearTable.addBecomeNoFactionDueToDestructionEntry(Session.Current.Scenario.Date, person2, this.BelongedFaction);
                        person2.OutsideTask = OutsideTaskKind.无;
                        person2.TaskDays = 0;
                        person2.Status = PersonStatus.NoFactionMoving;

                        person2.LocationArchitecture = this;
                        person2.TargetArchitecture = null;

                        if (leader == person2)
                        {
                            person2.Reputation = (int)(person2.Reputation * 0.6);
                        }
                        else
                        {
                            person2.Reputation = (int)(person2.Reputation * 0.95);
                        }

                        int hateDays = (int)(Math.Pow(5, person2.PersonalLoyalty - 2) * 10 + GameObject.Random(20) - 10);
                        if (hateDays > 0)
                        {
                            if (person2.ProhibitedFactionID.ContainsKey(faction.ID))
                            {
                                person2.ProhibitedFactionID[faction.ID] += hateDays;
                            }
                            else
                            {
                                person2.ProhibitedFactionID.Add(faction.ID, hateDays);
                            }
                        }
                    }

                    //if ((leader.LocationTroop == null) || leader.IsCaptive)
                    {
                        TroopList list = new TroopList();
                        foreach (Troop troop in this.BelongedFaction.Troops)
                        {
                            list.Add(troop);
                        }
                        foreach (Troop troop in list)
                        {
                            troop.FactionDestroy();
                        }
                        if (faction != null)
                        {
                            faction.CheckLeaderDeath(leader);
                        }
                        this.BelongedFaction.Destroy();

                    }
                    this.BelongedFaction.Capital = null;
                }
                else
                {
                    while (this.Persons.Count > 0)
                    {
                        if ((this.Persons[0] as Person).LocationArchitecture != null)
                        {
                            (this.Persons[0] as Person).MoveToArchitecture(this.BelongedFaction.Capital, this.Position);
                        }
                    }
                    while (this.MovingPersons.Count > 0)
                    {
                        if ((this.MovingPersons[0] as Person).LocationArchitecture != null)
                        {
                            (this.MovingPersons[0] as Person).MoveToArchitecture(this.BelongedFaction.Capital, this.Position);
                        }
                    }
                    foreach (Military m in this.BelongedFaction.TransferingMilitaries.GetList())
                    {
                        if (m.TargetArchitecture == this)
                        {
                            this.TransferMilitary(m, this.BelongedFaction.Capital);
                        }
                    }
                }
                if (this.BelongedFaction != null)
                {
                    this.BelongedFaction.RemoveArchitectureMilitaries(this);
                    this.BelongedFaction.RemoveArchitectureKnownData(this);
                    this.BelongedFaction.RemoveArchitecture(this);
                }
                if (faction != null)
                {
                    faction.AddArchitecture(this);
                    this.ApplyFactionInfluences();
                    faction.AddArchitectureMilitaries(this);
                }
                else
                {
                    this.BelongedFaction = null;
                }
            }
            else if (faction != null)
            {
                faction.AddArchitecture(this);
                this.ApplyFactionInfluences();
                faction.AddArchitectureMilitaries(this);
            }

            if (faction != null)
            {
                //this.jianzhuqizi.qizidezi.Text = faction.ToString().Substring(0, 1);
            }

            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                architecture.RefreshViewArea();
            }
            foreach (Troop troop in Session.Current.Scenario.Troops)
            {
                troop.RefreshViewArchitectureRelatedArea();
            }
            this.AIBattlingArchitectures.Clear();
            foreach (LinkNode i in this.AIAllLinkNodes.Values)
            {
                i.A.CheckIsFrontLine();
            }
            this.CheckIsFrontLine();
            ExtensionInterface.call("ArchitectureResetFaction", new Object[] { Session.Current.Scenario, this, oldFaction });
        }

        private void ReSortAllWeighingList(PersonList zhenzaiPersons, PersonList agriculturePersons, PersonList commercePersons,
            PersonList technologyPersons, PersonList dominationPersons, PersonList moralePersons, PersonList endurancePersons,
            PersonList recruitmentPersons, PersonList trainingPersons, MilitaryList weighingMilitaries)
        {
            PersonList pl = this.Persons;
            zhenzaiPersons.Clear();
            if (this.kezhenzai())
            {
                foreach (Person person in pl)
                {
                    zhenzaiPersons.Add(person);
                }
                zhenzaiPersons.IsNumber = true;
                zhenzaiPersons.PropertyName = "zhenzaiWeighing";
                zhenzaiPersons.ReSort();
            }
            agriculturePersons.Clear();
            if (this.Kind.HasAgriculture)
            {
                foreach (Person person in pl)
                {
                    agriculturePersons.Add(person);
                }
                agriculturePersons.IsNumber = true;
                agriculturePersons.PropertyName = "AgricultureWeighing";
                agriculturePersons.ReSort();
            }
            commercePersons.Clear();
            if (this.Kind.HasCommerce)
            {
                foreach (Person person in pl)
                {
                    commercePersons.Add(person);
                }
                commercePersons.IsNumber = true;
                commercePersons.PropertyName = "CommerceWeighing";
                commercePersons.ReSort();
            }
            technologyPersons.Clear();
            if (this.Kind.HasTechnology)
            {
                foreach (Person person in pl)
                {
                    technologyPersons.Add(person);
                }
                technologyPersons.IsNumber = true;
                technologyPersons.PropertyName = "TechnologyWeighing";
                technologyPersons.ReSort();
            }
            dominationPersons.Clear();
            if (this.Kind.HasDomination)
            {
                foreach (Person person in pl)
                {
                    dominationPersons.Add(person);
                }
                dominationPersons.IsNumber = true;
                dominationPersons.PropertyName = "DominationWeighing";
                dominationPersons.ReSort();
            }
            moralePersons.Clear();
            if (this.Kind.HasMorale)
            {
                foreach (Person person in pl)
                {
                    moralePersons.Add(person);
                }
                moralePersons.IsNumber = true;
                moralePersons.PropertyName = "MoraleWeighing";
                moralePersons.ReSort();
            }
            endurancePersons.Clear();
            if (this.Kind.HasEndurance)
            {
                foreach (Person person in pl)
                {
                    endurancePersons.Add(person);
                }
                endurancePersons.IsNumber = true;
                endurancePersons.PropertyName = "EnduranceWeighing";
                endurancePersons.ReSort();
            }
            trainingPersons.Clear();
            foreach (Person person in pl)
            {
                trainingPersons.Add(person);
            }
            trainingPersons.IsNumber = true;
            trainingPersons.PropertyName = "TrainingWeighing";
            trainingPersons.ReSort();
            recruitmentPersons.Clear();
            foreach (Person person in this.Persons)
            {
                recruitmentPersons.Add(person);
            }
            recruitmentPersons.IsNumber = true;
            recruitmentPersons.PropertyName = "RecruitmentWeighing";
            recruitmentPersons.ReSort();
            weighingMilitaries.Clear();
            foreach (Military military in this.Militaries)
            {
                weighingMilitaries.Add(military);
            }
            weighingMilitaries.IsNumber = true;
            weighingMilitaries.PropertyName = "Weighing";
            weighingMilitaries.ReSort();
        }

        public bool RoutewayAvail()
        {
            if (!CaiyongLiangdaoXitong()) return false;
            foreach (Point point in this.GetRoutewayStartArea().Area)
            {
                if (this.IsRoutewayPossible(point))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CaiyongLiangdaoXitong()
        {
            if (Session.GlobalVariables.LiangdaoXitong == false)
            {
                return false;
            }
            else
            {
                return true;
            }

        }


        public string SaveFundPacksToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (FundPack pack in this.FundPacks)
            {
                builder.Append(string.Concat(new object[] { pack.Fund, " ", pack.Days, " " }));
            }
            return builder.ToString();
        }

        public string SaveFoodPacksToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (FoodPack pack in this.FoodPacks)
            {
                builder.Append(string.Concat(new object[] { pack.Food, " ", pack.Days, " " }));
            }
            return builder.ToString();
        }

        public string SavePopulationPacksToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (PopulationPack pack in this.PopulationPacks)
            {
                builder.Append(string.Concat(new object[] { pack.Days, " ", pack.Population, " " }));
            }
            return builder.ToString();
        }

        /*
        public string SaveSpyPacksToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (SpyPack pack in this.SpyPacks)
            {
                builder.Append(string.Concat(new object[] { pack.SpyPerson.ID, " ", pack.Days, " " }));
            }
            return builder.ToString();
        }
        */

        public bool SearchAvail()
        {
            return this.HasPerson();
        }

        public void SellFood(int spendFood)
        {
            this.DecreaseFood(spendFood);
            this.IncreaseFund(spendFood / Session.Parameters.FoodToFundDivisor);
            ExtensionInterface.call("SellFood", new Object[] { Session.Current.Scenario, this });
        }

        public bool SellFoodAvail()
        {
            return this.Commerce >= Session.Parameters.SellFoodCommerce && (Session.Current.Scenario.Date.Season == GameSeason.冬 || Session.Current.Scenario.Date.Season == GameSeason.春) && this.Food > 0 && this.Fund < this.FundCeiling
                && (Session.Current.Scenario.Date.Month * 671
                + (this.Name.Length > 0 ? this.Name[0] : 321) * 864
                + (this.Name.Length > 1 ? this.Name[1] : 384) * 259
                    + this.ID * 513) % 2 == 0;
        }

        public void SetLongViewArea(GameArea area)
        {
            this.longViewArea = area;
        }

        public void SetRecentlyAttacked()
        {
            if (this.RecentlyAttacked <= 0)
            {
                ExtensionInterface.call("ArchitectureBeingAttacked", new Object[] { Session.Current.Scenario, this });
                this.JustAttacked = true;
                /*
                if (this.BelongedFaction != null)
                {
                    this.BelongedFaction.StopToControl = true;
                }
                if (this.OnBeginRecentlyAttacked != null)
                {
                    this.OnBeginRecentlyAttacked(this);
                }
                */

            }
            this.RecentlyAttacked = 10;
            this.RecentlyHit = 10;
            //this.AttackedReminder();
        }

        public void SetViewArea(GameArea area)
        {
            this.viewArea = area;
        }

        private void Sourrounded()
        {
            if (((this.BelongedFaction != null) && (this.Endurance > 0)) && this.Kind.HasDomination)
            {
                int num = 0;
                foreach (Point point in this.ContactArea.Area)
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                    if (!((troopByPosition == null) || this.IsFriendlyWithoutTruce(troopByPosition.BelongedFaction)))
                    {
                        num++;
                    }
                }
                if (num > this.AreaCount)
                {
                    int decrement = (num - this.AreaCount) * Session.Parameters.SurroundArchitectureDominationUnit;
                    decrement = this.DecreaseDomination(decrement);
                    ExtensionInterface.call("ArchitectureSurrounded", new Object[] { Session.Current.Scenario, this });
                    if (decrement > 0)
                    {
                        this.DecrementNumberList.AddNumber(decrement, CombatNumberKind.士气, this.Position);
                    }
                }
            }
        }

        public bool IsSurrounded()
        {
            if (((this.BelongedFaction != null) && (this.Endurance > 0)))
            {
                int num = 0;
                foreach (Point point in this.ContactArea.Area)
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                    if (!((troopByPosition == null) || this.IsFriendlyWithoutTruce(troopByPosition.BelongedFaction)))
                    {
                        num++;
                    }
                }
                if (num > this.AreaCount)
                {
                    return true;
                }
            }
            if (this.Endurance <= 0)
            {
                foreach (Point point in this.ContactArea.Area)
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                    if (!((troopByPosition == null) || this.IsFriendlyWithoutTruce(troopByPosition.BelongedFaction)))
                    {
                        return true;
                    }
                }
                foreach (Point point in this.ArchitectureArea.Area)
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                    if (!((troopByPosition == null) || this.IsFriendlyWithoutTruce(troopByPosition.BelongedFaction)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /*
        public bool SpyAvail()
        {
            return ((this.HasPerson() && (this.Fund >= this.SpyArchitectureFund)) && (this.GetSpyArchitectureArea().Count > 0));
        }


        public void SpyPacksDayEvent()
        {
            this.TodayNewMilitarySpyMessage = null;
            this.TodayNewTroopSpyMessage = null;
            for (int i = this.SpyPacks.Count - 1; i >= 0; i--)
            {
                SpyPack local1 = this.SpyPacks[i];
                local1.Days--;
                if ((this.SpyPacks[i].Days <= 0) || ((this.SpyPacks[i].SpyPerson != null) && (this.SpyPacks[i].SpyPerson.BelongedFaction == this.BelongedFaction)))
                {
                    this.SpyPacks.RemoveAt(i);
                }
            }
        }
        */

        public bool StateAdminEffectAvail()
        {
            return (this.Kind != null && this.Kind.HasTechnology && (this.Technology >= ((int)(this.TechnologyCeiling * 0.5))));
        }

        private void StopAllWork()
        {
            foreach (Person person in this.Persons)
            {
                person.WorkKind = ArchitectureWorkKind.无;
            }
        }

        private void StopCostFundWork()
        {
            foreach (Person person in this.Persons)
            {
                if ((person.WorkKind != ArchitectureWorkKind.无) && (person.WorkKind != ArchitectureWorkKind.训练))
                {
                    person.WorkKind = ArchitectureWorkKind.无;
                }
            }
        }

        private void StrategicCenterEffect()
        {
            if ((this.BelongedFaction != null) && this.IsStrategicCenter)
            {
                foreach (Point point in this.LongViewArea.Area)
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                    if (troopByPosition != null)
                    {
                        if (this.IsFriendlyWithoutTruce(troopByPosition.BelongedFaction))
                        {
                            troopByPosition.IncreaseCombativity(5);
                        }
                        else
                        {
                            troopByPosition.DecreaseCombativity(10);
                        }
                    }
                }
                //if (Session.Current.Scenario.Date.Day == 30)
                if (GameObject.Random(30 / Session.Parameters.DayInTurn) == 0)
                {
                    GameObjectList aILinks = this.GetAILinks();
                    aILinks.Add(this);
                    foreach (Architecture architecture in aILinks)
                    {
                        if (this.IsFriendlyWithoutTruce(architecture.BelongedFaction))
                        {
                            if (architecture.Kind.HasDomination)
                            {
                                int number = architecture.IncreaseDomination(20);
                                if (number > 0)
                                {
                                    architecture.IncrementNumberList.AddNumber(number, CombatNumberKind.士气, architecture.Position);
                                }
                            }
                        }
                        else if (this.IsHostile(architecture.BelongedFaction) && architecture.Kind.HasDomination)
                        {
                            int num2 = architecture.DecreaseDomination(10);
                            if (num2 > 0)
                            {
                                architecture.DecrementNumberList.AddNumber(num2, CombatNumberKind.士气, architecture.Position);
                            }
                        }
                    }
                }
            }
        }

        private int TargetingTroopCount(Architecture a)
        {
            int num = 0;
            foreach (Troop troop in this.BelongedFaction.Troops)
            {
                if (troop.WillArchitecture == a)
                {
                    num++;
                }
            }
            return num;
        }

        public bool TechnologyAvail()
        {
            return (this.Kind.HasTechnology && this.HasPerson());
        }

        public override string ToString()
        {
            return string.Concat(new object[] { base.Name, "  ", this.KindString, "  ", this.FactionString, "  ", this.Persons.Count, "人" });
        }

        public bool TrainingAvail()
        {
            if (this.HasPerson())
            {
                foreach (Military military in this.Militaries)
                {
                    if ((military.Quantity > 0) && ((military.Morale < military.MoraleCeiling) || (military.Combativity < military.CombativityCeiling)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        private void TrainMilitary()  //训练编队
        {
            int meiXunlianHaoDeBianduiShu;
            meiXunlianHaoDeBianduiShu = this.MeiXunlianHaoDeBianduiShu();
            if (meiXunlianHaoDeBianduiShu == 0)
            {
                return;
            }
            int zongXunlianNengli = 0;
            int pingjunXunlianNengli;
            foreach (Person person in this.TrainingWorkingPersons)
            {
                zongXunlianNengli += person.TrainingAbility;
            }
            pingjunXunlianNengli = zongXunlianNengli / meiXunlianHaoDeBianduiShu;

            if (pingjunXunlianNengli > 0)
            {
                int pingjunJinyan = 0;
                foreach (Military military in this.Militaries)
                {
                    if (military.Morale < military.MoraleCeiling)
                    {
                        int randomValue = StaticMethods.GetRandomValue((int)((pingjunXunlianNengli * this.MultipleOfTraining) * Session.Parameters.TrainingRate), 200 + (10 * (military.Scales + military.InjuryQuantity / military.Kind.MinScale)));
                        if (randomValue > 0)
                        {
                            if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction))
                            {
                                randomValue = (int)(randomValue * Session.Parameters.AITrainingSpeedRate);
                            }
                            pingjunJinyan = randomValue / this.TrainingWorkingPersons.Count;
                            List<Person> needRemoval = new List<Person>();
                            foreach (Person person in this.TrainingWorkingPersons)
                            {
                                if (person.BelongedFaction == null)
                                {
                                    needRemoval.Add(person);
                                }
                                else
                                {
                                    //person.AddTrainingExperience(pingjunJinyan * 2);
                                    person.AddCommandExperience(pingjunJinyan * 2);
                                    person.IncreaseReputation(pingjunJinyan * 3);
                                    person.IncreaseOfficerMerit(pingjunJinyan * 3);
                                    person.BelongedFaction.IncreaseReputation(pingjunJinyan * 2);
                                    person.BelongedFaction.IncreaseTechniquePoint(pingjunJinyan * 50);
                                }
                            }
                            foreach (Person p in needRemoval)
                            {
                                this.TrainingWorkingPersons.Remove(p);
                            }
                            military.IncreaseMorale(randomValue);
                        }
                    }
                    if (military != null && military.Combativity < military.CombativityCeiling && this.TrainingWorkingPersons.Count > 0)
                    {
                        int increment = StaticMethods.GetRandomValue((int)((pingjunXunlianNengli * this.MultipleOfTraining) * Session.Parameters.TrainingRate), 50 + (5 * (military.Scales + military.InjuryQuantity / military.Kind.MinScale)));
                        if (increment > 0)
                        {
                            if (!Session.Current.Scenario.IsPlayer(this.BelongedFaction))
                            {
                                increment = (int)(increment * Session.Parameters.AITrainingSpeedRate);
                            }
                            List<Person> needRemoval = new List<Person>();
                            pingjunJinyan = increment / this.TrainingWorkingPersons.Count;
                            foreach (Person person in this.TrainingWorkingPersons)
                            {
                                if (person.BelongedFaction == null)
                                {
                                    needRemoval.Add(person);
                                }
                                else
                                {
                                    //person.AddTrainingExperience(pingjunJinyan);
                                    person.AddStrengthExperience(pingjunJinyan);
                                    person.IncreaseReputation(pingjunJinyan);
                                    person.IncreaseOfficerMerit(pingjunJinyan);
                                    person.BelongedFaction.IncreaseReputation(0);
                                    person.BelongedFaction.IncreaseTechniquePoint(pingjunJinyan * 20);
                                }
                            }
                            foreach (Person p in needRemoval)
                            {
                                this.TrainingWorkingPersons.Remove(p);
                            }
                            military.IncreaseCombativity(increment);
                        }
                    }

                }

            }
        }


        public bool TransferFoodAvail()
        {
            if (this.IsSurrounded()) return false;
            return ((this.Fund > 0) && (this.GetOtherArchitectureList().Count > 0));
        }

        public bool TransferFundAvail()
        {
            if (this.IsSurrounded()) return false;
            return ((this.Fund > 0) && (this.GetOtherArchitectureList().Count > 0));
        }

        public bool TroopershipAvail()
        {
            if ((((Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(0x1c) != null)
                && (this.Persons.Count > 0)) && (this.Militaries.Count > 0)) && this.IsBesideWater && !Session.GlobalVariables.LandArmyCanGoDownWater)
            {
                foreach (Military military in this.Militaries)
                {
                    if ((((military.Quantity > 0) && (military.Morale > 0)) && (military.Kind.Type != MilitaryType.水军)) && (this.GetMilitaryCampaignArea(military).Count > 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void ViewAreaEvent()
        {
            this.DetectAmbushTroop();
            this.IncreaseViewAreaCombativity();
        }

        public bool ViewTroop(Troop troop)
        {
            return (this.LongViewArea.HasPoint(troop.Position) && (((this.BelongedFaction != null) && this.IsFriendly(troop.BelongedFaction)) || (troop.Status != TroopStatus.埋伏)));
        }

        public void WallStateChange()
        {
            foreach (Troop troop in Session.Current.Scenario.Troops)
            {
                if (!this.IsFriendly(troop.BelongedFaction))
                {
                    troop.RefreshViewArchitectureRelatedArea();
                }
            }
        }

        private void YearEnd()
        {
        }

        public void YearEvent()
        {
            this.YearEnd();
        }

        public PersonList makeMarryablePersons()
        {
            PersonList result = new PersonList();

            if (this.Fund < Session.Parameters.MakeMarriageCost) return result;

            foreach (Person p in this.Persons)
            {
                if (p.MakeMarryable().Count > 0)
                {
                    result.Add(p);
                }
            }
            return result;
        }

        public int AbundantFood
        {
            get
            {
                int num = 0;
                foreach (Legion legion in this.BelongedFaction.Legions)
                {
                    if (legion.Kind == LegionKind.Defensive)
                    {
                        if (legion == this.DefensiveLegion)
                        {
                            num += legion.FoodCostPerDay * 80;
                        }
                    }
                    else if (((legion.Kind == LegionKind.Offensive) && (legion.PreferredRouteway != null)) && (legion.PreferredRouteway.StartArchitecture == this))
                    {
                        num += legion.FoodCostPerDay * 80;
                    }
                }
                int factor = 80;
                if (HasHostileTroopsInView())
                {
                    factor = 180;
                }
                int num2 = (((int)(Math.Sqrt((double)this.Population) * 400.0)) + (this.FoodCostPerDayOfAllMilitaries * factor)) + num;
                if (!this.HostileLine)
                {
                    num2 /= 2;
                }
                if (!this.withoutTruceFrontline)
                {
                    num2 /= 2;
                }
                return num2;
            }
        }

        public int AbundantFund
        {
            get
            {
                int num = this.FacilityMaintenanceCost * 60;
                num += this.RoutewayActiveCost * 60;
                num += this.PersonCount * Session.Parameters.InternalFundCost * 30;
                num += (this.BelongedFaction.BecomeEmperorLegallyAvail() || this.BelongedFaction.SelfBecomeEmperorAvail()) && this.BelongedFaction.Capital == this ? 100000 : 0;
                num += this.BelongedFaction.Leader.WaitForFeiZi != null ? Session.Parameters.NafeiCost : 0;
                num += (int)(Math.Sqrt(this.Population) * 8.0);
                if (this.withoutTruceFrontline)
                {
                    num += this.Population / 50;
                }
                num += this.BelongedFaction.Capital == this ? this.BelongedFaction.FundToAdvance : 0;
                num += this.InformationDayCost * 15;
                num += this.PlanFacilityKind == null ? 0 : this.PlanFacilityKind.FundCost;
                num += this.BelongedFaction != null && this.BelongedFaction.PlanTechniqueArchitecture == this ? this.BelongedFaction.getTechniqueActualFundCost(this.BelongedFaction.PlanTechnique) : 0;
                return num;
            }
        }
        [DataMember]
        public int Agriculture
        {
            get
            {
                return this.agriculture;
            }
            set
            {
                this.agriculture = value;
            }
        }

        public int AgricultureCeiling
        {
            get
            {
                return this.kind.HasAgriculture ? ((this.Kind.AgricultureBase + (this.Kind.AgricultureUnit * (this.JianzhuGuimo - 1))) + this.IncrementOfAgricultureCeiling) : 0;
            }
        }

        public string AgricultureString
        {
            get
            {
                return (this.Agriculture + "/" + this.AgricultureCeiling);
            }
        }

        public string AILandLinksDisplayString
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (Architecture architecture in this.AILandLinks)
                {
                    builder.Append(architecture.Name + " ");
                }
                return builder.ToString();
            }
        }

        public string AIWaterLinksDisplayString
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (Architecture architecture in this.AIWaterLinks)
                {
                    builder.Append(architecture.Name + " ");
                }
                return builder.ToString();
            }
        }

        public int AreaCount
        {
            get
            {
                //return this.ArchitectureArea.Count;
                return 1;

            }
        }

        public int JianzhuGuimo
        {
            get
            {
                return this.ArchitectureArea.Count;


            }
        }


        public int ArmyQuantity
        {
            get
            {
                int num = 0;
                foreach (Military military in this.Militaries)
                {
                    num += military.Quantity;
                }
                return num;
            }
        }

        public int ArmyScale
        {
            get
            {
                if (this.BelongedFaction != null)
                {
                    int num = 0;
                    foreach (Military military in this.Militaries)
                    {
                        num += military.Scales;
                    }
                    return num;
                }
                return ((this.AreaCount * 5) + ((this.Population / 0x2710) / 2));
            }
        }

        public int InverseArmyScaleWeighing
        {
            get
            {
                return (int)((10000.0 / this.ArmyScale) * (((((((this.IsCapital ? 2 : 1) + (this.IsStateAdmin ? 1 : 0)) + (this.IsRegionCore ? 1 : 0)) + (this.IsStrategicCenter ? 1 : 0)) + (this.withoutTruceFrontline ? 2 : 0)) + (this.HostileLine ? 2 : 0)) + (this.CriticalHostile ? 3 : 0)));
            }
        }

        public int ArmyScaleWeighing
        {
            get
            {
                return ((this.ArmyScale + 10) * (((((((this.IsCapital ? 2 : 1) + (this.IsStateAdmin ? 1 : 0)) + (this.IsRegionCore ? 1 : 0)) + (this.IsStrategicCenter ? 1 : 0)) + (this.withoutTruceFrontline ? 2 : 0)) + (this.HostileLine ? 2 : 0)) + (this.CriticalHostile ? 3 : 0)));
            }
        }
        [DataMember]
        public bool AutoHiring
        {
            get
            {
                return this.autoHiring;
            }
            set
            {
                this.autoHiring = value;
            }
        }
        [DataMember]
        public bool AutoRewarding
        {
            get
            {
                return this.autoRewarding;
            }
            set
            {
                this.autoRewarding = value;
            }
        }
        [DataMember]
        public bool AutoSearching
        {
            get
            {
                return this.autoSearching;
            }
            set
            {
                this.autoSearching = value;
            }
        }
        [DataMember]
        public bool AutoZhaoXian
        {
            get
            {
                return this.autoZhaoXian;
            }
            set
            {
                this.autoZhaoXian = value;
            }
        }
        [DataMember]
        public bool AutoWorking
        {
            get
            {
                return this.autoWorking;
            }
            set
            {
                this.autoWorking = value;
            }
        }
        [DataMember]
        public bool AutoRecruiting
        {
            get
            {
                return this.autoRecruiting;
            }
            set
            {
                this.autoRecruiting = value;
            }
        }

        public GameArea BaseFoodSurplyArea
        {
            get
            {
                if (this.baseFoodSurplyArea == null)
                {
                    this.baseFoodSurplyArea = this.LongViewArea;
                    /*this.baseFoodSurplyArea = new GameArea();
                    foreach (Point point in this.ArchitectureArea.GetContactArea(true).Area)
                    {
                        this.baseFoodSurplyArea.AddPoint(point);
                    }
                    foreach (Point point in this.ArchitectureArea.Area)
                    {
                        this.baseFoodSurplyArea.AddPoint(point);
                    }
                    return this.baseFoodSurplyArea;*/
                }
                return this.baseFoodSurplyArea;
            }
            set
            {
                this.baseFoodSurplyArea = value;
            }

        }
        [DataMember]
        public int BuildingDaysLeft
        {
            get
            {
                return this.buildingDaysLeft;
            }
            set
            {
                this.buildingDaysLeft = value;
            }
        }

        public int BuildingDaysLeftText
        {
            get
            {
                return this.buildingDaysLeft * Session.Parameters.DayInTurn;
            }
        }

        [DataMember]
        public int BuildingFacility { get; set; }
        //{
        //    get
        //    {
        //        return this.buildingFacility;
        //    }
        //    set
        //    {
        //        this.buildingFacility = value;
        //    }
        //}

        public int CaptiveCount
        {
            get
            {
                return this.Captives.Count;
            }
        }

        public int ChangeCapitalCost
        {
            get
            {
                return (Session.Parameters.ChangeCapitalCost * this.AreaCount);
            }
        }

        [DataMember]
        public int Commerce
        {
            get
            {
                return this.commerce;
            }
            set
            {
                this.commerce = value;
            }
        }

        public int CommerceCeiling
        {
            get
            {
                return this.kind.HasCommerce ? ((this.Kind.CommerceBase + (this.Kind.CommerceUnit * (this.JianzhuGuimo - 1))) + this.IncrementOfCommerceCeiling) : 0;
            }
        }

        public string CommerceString
        {
            get
            {
                return (this.Commerce + "/" + this.CommerceCeiling);
            }
        }

        public GameArea ContactArea
        {
            get
            {
                if (this.contactArea == null)
                {
                    this.contactArea = this.ArchitectureArea.GetContactArea(false);
                }
                return this.contactArea;
            }
            set
            {
                this.contactArea = value;
            }
        }

        public int ConvincePersonFund
        {
            get
            {
                return (int)(Session.Parameters.ConvincePersonCost * this.RateOfConvincePerson);
            }
        }

        public int ConvincePersonMaxCount
        {
            get
            {
                if (this.ConvincePersonFund == 0) return int.MaxValue;
                return (this.Fund / this.ConvincePersonFund);
            }
        }

        public float CurrentRateOfInternal
        {
            get
            {
                //县令内政加成系数
                float mayorRate = 0;
                float leaderRate = 0;
                if (this.Mayor != null && this.Mayor.Status != PersonStatus.Captive)
                {
                    mayorRate = ((float)this.Mayor.Politics / 100 + (float)this.Mayor.Intelligence / 100) / 2 * Math.Min(1, this.MayorOnDutyDays / 90.0f);
                }
                if (this.BelongedFaction != null && this.BelongedFaction.Leader != null && this.BelongedFaction.Leader.Status != PersonStatus.Captive)
                {
                    leaderRate = ((float)this.BelongedFaction.Leader.Politics / 100 + (float)this.BelongedFaction.Leader.Intelligence / 100) / 2;
                    leaderRate *= 0.2f;
                }

                return (this.RateOfpublic + this.DayRateIncrementOfInternal + mayorRate);
            }
        }

        public float CurrentSurplusRate
        {
            get
            {
                return this.surplusRate;
            }
        }

        public int DestroyArchitectureFund
        {
            get
            {
                return (int)(Session.Parameters.DestroyArchitectureCost * this.RateOfDestroyArchitecture);
            }
        }

        public int DestroyPersonMaxCount
        {
            get
            {
                if (this.DestroyArchitectureFund == 0) return int.MaxValue;
                return (this.Fund / this.DestroyArchitectureFund);
            }
        }
        [DataMember]
        public int Domination
        {
            get
            {
               return this.domination;

            }
            set
            {
                this.domination = value;
            }
        }

        public int DominationCeiling
        {
            get
            {
                return this.kind.HasDomination ? (this.Kind.DominationBase + (this.Kind.DominationUnit * (this.JianzhuGuimo - 1)) + this.IncrementOfDominationCeiling) : 0;
            }
        }

        public string DominationString
        {
            get
            {
                return (this.Domination + "/" + this.DominationCeiling);
            }
        }
        [DataMember]
        public int Endurance
        {
            get
            {
                return this.endurance;
            }
            set
            {
                this.endurance = value;
            }
        }

        public int EnduranceCeiling
        {
            get
            {
                return this.kind.HasEndurance ? ((this.Kind.EnduranceBase + (this.Kind.EnduranceUnit * (this.JianzhuGuimo - 1))) + this.IncrementOfEnduranceCeiling) : 0;
            }
        }

        public string EnduranceString
        {
            get
            {
                return (this.Endurance + "/" + this.EnduranceCeiling);
            }
        }

        public int EnoughFood
        {
            get
            {
                int num = 0;
                foreach (Legion legion in this.BelongedFaction.Legions)
                {
                    if (legion.Kind == LegionKind.Defensive)
                    {
                        if (legion == this.DefensiveLegion)
                        {
                            num += legion.FoodCostPerDay * 30;
                        }
                    }
                    else if (((legion.Kind == LegionKind.Offensive) && (legion.PreferredRouteway != null)) && (legion.PreferredRouteway.StartArchitecture == this))
                    {
                        num += legion.FoodCostPerDay * 30;
                    }
                }
                int factor = 30;
                if (HasHostileTroopsInView())
                {
                    factor = 180;
                }

                int num2 = this.FoodCostPerDayOfAllMilitaries * factor + num;

                return num2;
            }
        }

        public int EnoughFund
        {
            get
            {
                int num = this.FacilityMaintenanceCost * 30;
                num += this.RoutewayActiveCost * 30;
                num += this.InformationDayCost * 15;
                num += this.PlanFacilityKind == null ? 0 : this.PlanFacilityKind.FundCost;
                num += this.BelongedFaction != null && this.BelongedFaction.PlanTechniqueArchitecture == this ? this.BelongedFaction.getTechniqueActualFundCost(this.BelongedFaction.PlanTechnique) : 0;
                num += this.PersonCount * this.InternalFundCost * 30;
                return num;
            }
        }

        public bool IsFoodIncomeEnough
        {
            get
            {
                return this.ExpectedFood * Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.Season) - this.EnoughFood / 8 >= 0;
            }
        }

        public bool IsFundIncomeEnough
        {
            get
            {
                return this.ExpectedFund - this.EnoughFund >= 0;
            }
        }

        private int ExpectedFoodCache = -1;
        public int ExpectedFood
        {
            get
            {
                if (ExpectedFoodCache > 0)
                {
                    return ExpectedFoodCache;
                }
                int num = this.Agriculture + ((int)((Math.Pow((double)this.Population, 0.3) * Math.Pow((double)this.Agriculture, 0.8)) * 47.0));
                num += this.IncrementOfMonthFood;
                num += (int)(this.RateIncrementOfMonthFood * num);
                num = (int)(num * Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.Season));
                if ((this.LocationState.StateAdmin != null) && this.LocationState.StateAdmin.StateAdminEffectAvail())
                {
                    if (this.IsFriendlyWithoutTruce(this.LocationState.StateAdmin.BelongedFaction))
                    {
                        num += (int)(num * 0.2);
                    }
                    else if (this.IsHostile(this.LocationState.StateAdmin.BelongedFaction))
                    {
                        num -= (int)(num * 0.2);
                    }
                }

                if (this.BelongedFaction != null)
                {
                    num = (int)(num * this.BelongedFaction.InternalSurplusRate);
                }
                num = (int)(num * Session.Parameters.FoodRate);
                if (Session.Current.Scenario.HasAIResourceBonus(this.BelongedSection))
                {
                    num = (int)(num * Session.Parameters.AIFoodRate);
                }
                if (Session.GlobalVariables.MultipleResource)
                {
                    num *= 2;
                }
                num += 10000;
                ExpectedFoodCache = num;
                return num;
            }
        }

        public string ExpectedFoodString
        {
            get
            {
                return (this.ExpectedFood + "/月");
            }
        }

        private int ExpectedFundCache = -1;
        public int ExpectedFund
        {
            get
            {
                if (ExpectedFundCache > 0)
                {
                    return ExpectedFundCache;
                }
                int num = this.Commerce + ((int)((Math.Pow((double)this.Population, 0.6) * Math.Pow((double)this.Commerce, 0.8)) / 59.0));
                num += this.IncrementOfMonthFund;
                num += (int)(this.RateIncrementOfMonthFund * num);
                if ((this.LocationState.StateAdmin != null) && this.LocationState.StateAdmin.StateAdminEffectAvail())
                {
                    if (this.IsFriendlyWithoutTruce(this.LocationState.StateAdmin.BelongedFaction))
                    {
                        num += (int)(num * 0.2);
                    }
                    else if (this.IsHostile(this.LocationState.StateAdmin.BelongedFaction))
                    {
                        num -= (int)(num * 0.2);
                    }
                }
                if (this.BelongedFaction != null)
                {
                    num = (int)(num * this.BelongedFaction.InternalSurplusRate);
                }
                num = (int)(num * Session.Parameters.FundRate);
                if (Session.Current.Scenario.HasAIResourceBonus(this.BelongedSection))
                {
                    num = (int)(num * Session.Parameters.AIFundRate);
                }
                if (Session.GlobalVariables.MultipleResource)
                {
                    num *= 2;
                }
                num += 100;
                ExpectedFundCache = num;
                return num;
            }
        }

        public string ExpectedFundString
        {
            get
            {
                return (this.ExpectedFund + "/月");
            }
        }

        public int FacilityCount
        {
            get
            {
                return this.Facilities.Count;
            }
        }
        [DataMember]
        public bool FacilityEnabled
        {
            get
            {
                return this.facilityEnabled;
            }
            set
            {
                this.facilityEnabled = value;
            }
        }

        public string FacilityEnabledString
        {
            get
            {
                return (this.FacilityEnabled ? "○" : "×");
            }
        }

        public int FacilityMaintenanceCost
        {
            get
            {
                int num = 0;
                foreach (Facility facility in this.Facilities)
                {
                    num += facility.MaintenanceCost;
                }

                if (this.Feiziliebiao.Count > 0 && (this.BelongedFaction == null || !this.BelongedFaction.IsAlien))
                {
                    num += this.Feiziliebiao.Count * Session.Parameters.PrincessMaintainenceCost;
                }

                return num;
            }
        }

        public string FacilityMaintenanceCostString
        {
            get
            {
                return (this.FacilityMaintenanceCost * 30 + "/月");
            }
        }

        public int FacilityPositionCount
        {
            get
            {
                return (this.Kind.FacilityPositionUnit * (this.JianzhuGuimo + this.IncrementOfFacilityPositionCount));
            }
        }

        public int FacilityPositionLeft
        {
            get
            {
                int facilityPositionCount = this.FacilityPositionCount;
                foreach (Facility facility in this.Facilities)
                {
                    facilityPositionCount -= facility.PositionOccupied;
                }
                if (this.BuildingFacility >= 0)
                {
                    FacilityKind facilityKind = Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(this.BuildingFacility);
                    if (facilityKind != null)
                    {
                        facilityPositionCount -= facilityKind.PositionOccupied;
                    }
                }
                return facilityPositionCount;
            }
        }

        public string FacilityPositionString
        {
            get
            {
                return ((this.FacilityPositionCount - this.FacilityPositionLeft) + "/" + this.FacilityPositionCount);
            }
        }

        public bool FactionAutoRefuse
        {
            get
            {
                return ((this.BelongedFaction != null) && this.BelongedFaction.AutoRefuse);
            }
        }

        public string FactionInternalSurplusRatePercentString
        {
            get
            {
                if (this.BelongedFaction != null)
                {
                    return StaticMethods.GetPercentString(this.BelongedFaction.InternalSurplusRate, 3);
                }
                return "----";
            }
        }

        public string FactionString
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

        public int FewArmyScale
        {
            get
            {
                return (10 + (2 * this.AreaCount));
            }
        }
        [DataMember]
        public int Food
        {
            get
            {
                return this.food;
            }
            set
            {
                this.food = value;
            }
        }

        public int FoodCeiling
        {
            get
            {
                return (this.kind.FoodMaxUnit * this.JianzhuGuimo) + this.IncrementOfFoodCeiling;
            }
        }

        public int FoodCostPerDayOfAllMilitaries
        {
            get
            {
                int num = 0;
                foreach (Military military in this.Militaries)
                {
                    num += military.FoodCostPerDay;
                }
                return num;
            }
        }

        public int MaxSupportableTroopScale
        {
            get
            {
                if (Session.GlobalVariables.PopulationRecruitmentLimit)
                {
                    MilitaryKind b = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.MilitaryKinds[0];
                    return this.Population / (b.MaxScale / b.MinScale);
                }
                int cost = this.FoodCostPerDayOfAllMilitaries * 60;
                if (cost < this.FoodCeiling * 0.9)
                {
                    return int.MaxValue;
                }
                return (int)((this.FoodCeiling / (double)cost) * this.ArmyScale);
            }
        }

        public MilitaryList TransferingMilitaries
        {
            get
            {
                MilitaryList result = new MilitaryList();
                if (this.BelongedFaction == null) return result;
                foreach (Military m in this.BelongedFaction.TransferingMilitaries)
                {
                    if (m.TargetArchitecture == this)
                    {
                        result.Add(m);
                    }
                }
                return result;
            }
        }

        public int TransferingMilitariesScale
        {
            get
            {
                int s = 0;
                foreach (Military m in this.TransferingMilitaries)
                {
                    s += m.Scales;
                }
                return s;
            }
        }

        public bool IsTroopExceedsLimit
        {
            get
            {
                if (Session.GlobalVariables.PopulationRecruitmentLimit)
                {
                    return this.Population < this.ArmyQuantity + this.TransferingMilitariesScale;
                }
                return false;
            }
        }

        public int MaxSupportableTroop
        {
            get
            {
                if (Session.GlobalVariables.PopulationRecruitmentLimit)
                {
                    return this.Population / Session.Current.Scenario.GameCommonData.AllMilitaryKinds.MilitaryKinds[0].MaxScale;
                }
                int cost = this.FoodCostPerDayOfAllMilitaries * 60;
                if (cost < this.FoodCeiling * 0.9)
                {
                    return int.MaxValue;
                }
                return (int)((this.FoodCeiling / (double)cost) * this.Militaries.Count);
            }
        }

        public int FoodCostPerDayOfLandMilitaries
        {
            get
            {
                int num = 0;
                foreach (Military military in this.Militaries)
                {
                    if (military.Kind.Type != MilitaryType.水军)
                    {
                        num += military.FoodCostPerDay;
                    }
                }
                return num;
            }
        }

        public int FoodCostPerDayOfWaterMilitaries
        {
            get
            {
                int num = 0;
                foreach (Military military in this.Militaries)
                {
                    if (military.Kind.Type == MilitaryType.水军)
                    {
                        num += military.FoodCostPerDay;
                    }
                }
                return num;
            }
        }

        public float FoodReduceDayRate
        {
            get
            {
                return (0.001f * this.RateOfFoodReduceRate);
            }
        }

        public string FoodReduceDayRateString
        {
            get
            {
                return (Math.Round((double)this.FoodReduceDayRate, 4).ToString() + "/日");
            }
        }
        [DataMember]
        public int Fund
        {
            get
            {
                return this.fund;
            }
            set
            {
                this.fund = value;
            }
        }

        public int FundCeiling
        {
            get
            {
                return (this.Kind.FundMaxUnit * this.JianzhuGuimo) + this.IncrementOfFundCeiling;
            }
        }

        public int FundInPack
        {
            get
            {
                int num = 0;
                foreach (FundPack pack in this.FundPacks)
                {
                    num += pack.Fund;
                }
                return num;
            }
        }

        public int FoodInPack
        {
            get
            {
                int num = 0;
                foreach (FoodPack pack in this.FoodPacks)
                {
                    num += pack.Food;
                }
                return num;
            }
        }

        public string FundPackString
        {
            get
            {
                String s = "";
                foreach (FundPack p in this.FundPacks)
                {
                    s += (p.Days * Session.Current.Scenario.Parameters.DayInTurn) + "天" + p.Fund + "。";
                }
                return s;
            }
        }

        public string FoodPackString
        {
            get
            {
                String s = "";
                foreach (FoodPack p in this.FoodPacks)
                {
                    s += (p.Days * Session.Current.Scenario.Parameters.DayInTurn) + "天" + p.Food + "。";
                }
                return s;
            }
        }

        public int GossipArchitectureFund
        {
            get
            {
                return (int)(Session.Parameters.GossipArchitectureCost * this.RateOfGossipArchitecture);
            }
        }

        public int JailBreakArchitectureFund
        {
            get
            {
                return (int)(Session.Parameters.JailBreakArchitectureCost * this.RateOfJailBreakArchitecture);
            }
        }

        public int GossipPersonMaxCount
        {
            get
            {
                if (this.GossipArchitectureFund == 0) return int.MaxValue;
                return (this.Fund / this.GossipArchitectureFund);
            }
        }

        public int JailBreakPersonMaxCount
        {
            get
            {
                if (this.JailBreakArchitectureFund == 0) return int.MaxValue;
                return (this.Fund / this.JailBreakArchitectureFund);
            }
        }

        public bool HasBuildingRouteway
        {
            get
            {
                foreach (Routeway routeway in this.Routeways)
                {
                    if (routeway.Building)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasDefensiveLegion
        {
            get
            {
                return (this.DefensiveLegion != null);
            }
        }

        public bool HasFriendlyDiplomaticRelation
        {
            get
            {
                if (this.BelongedFaction == null)
                {
                    return false;
                }
                return this.BelongedFaction.HasFriendlyDiplomaticRelation;
            }
        }

        private bool HasHirablePerson
        {
            get
            {
                foreach (Person person in this.NoFactionPersons)
                {
                    if (person.IsHirable(this.BelongedFaction))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /*
        public bool HasSpy
        {
            get
            {
                return (this.SpyPacks.Count > 0);
            }
        }
        */

        private int HirablePersonCount
        {
            get
            {
                int num = 0;
                foreach (Person person in this.NoFactionPersons)
                {
                    if (person.IsHirable(this.BelongedFaction))
                    {
                        num++;
                    }
                }
                return num;
            }
        }
        [DataMember]
        public bool HireFinished
        {
            get
            {
                return this.hireFinished;
            }
            set
            {
                this.hireFinished = value;
            }
        }

        public int InstigateArchitectureFund
        {
            get
            {
                return (int)(Session.Parameters.InstigateArchitectureCost * this.RateOfInstigateArchitecture);
            }
        }

        public int InstigatePersonMaxCount
        {
            get
            {
                if (this.InstigateArchitectureFund == 0) return int.MaxValue;
                return (this.Fund / this.InstigateArchitectureFund);
            }
        }

        public int InternalFundCost
        {
            get
            {
                return (Session.Parameters.InternalFundCost * this.AreaCount);
            }
        }

        public bool IsBesideWater
        {
            get
            {
                foreach (Point point in this.ArchitectureArea.GetContactArea(false).Area)
                {
                    if (!Session.Current.Scenario.PositionOutOfRange(point) && (Session.Current.Scenario.ScenarioMap.MapData[point.X, point.Y] == 6))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsCapital
        {
            get
            {
                return ((this.BelongedFaction != null) && (this.BelongedFaction.Capital == this));
            }
        }

        public bool IsFoodAbundant
        {
            get
            {
                return ((this.Food >= this.FoodCeiling) || (this.Food + this.FoodInPack >= this.AbundantFood));
            }
        }

        public bool IsFoodEnough
        {
            get
            {
                return ((this.Food >= this.FoodCeiling) || (this.Food + this.FoodInPack >= this.EnoughFood));
            }
        }

        public bool IsFoodTwiceAbundant
        {
            get
            {
                return ((this.Food >= this.FoodCeiling) || (this.Food + this.FoodInPack > (this.AbundantFood * 2)));
            }
        }

        public bool IsFundAbundant
        {
            get
            {
                return (this.Fund >= this.FundCeiling) || (this.Fund + this.FundInPack >= this.AbundantFund);
            }
        }

        public bool IsFundEnough
        {
            get
            {
                return (this.Fund >= this.FundCeiling) || (this.Fund + this.FundInPack >= this.EnoughFund);
            }
        }

        public bool IsImportant
        {
            get
            {
                return (((this.IsCapital || this.IsStrategicCenter) || this.IsStateAdmin) || this.IsRegionCore || this.huangdisuozai);
            }
        }

        public bool IsRegionCore
        {
            get
            {
                return (this.LocationState.LinkedRegion.RegionCore == this);
            }
        }

        public bool IsStateAdmin
        {
            get
            {
                return (this.LocationState.StateAdmin == this);
            }
        }
        [DataMember]
        public bool IsStrategicCenter
        {
            get
            {
                return this.isStrategicCenter;
            }
            set
            {
                this.isStrategicCenter = value;
            }
        }

        public string IsStrategicCenterString
        {
            get
            {
                return (this.IsStrategicCenter ? "○" : "×");
            }
        }

        [DataMember]
        public int KindId { get; set; }

        public ArchitectureKind Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                this.kind = value;
            }
        }

        public string KindString
        {
            get
            {
                return this.Kind.Name;
            }
        }

        public int LandArmyScale
        {
            get
            {
                if (this.BelongedFaction != null)
                {
                    int num = 0;
                    foreach (Military military in this.Militaries)
                    {
                        if (military.Kind.Type != MilitaryType.水军)
                        {
                            num += military.Scales;
                        }
                    }
                    return num;
                }
                return ((this.AreaCount * 5) + ((this.Population / 0x2710) / 2));
            }
        }

        public int LargeArmyScale
        {
            get
            {
                return (40 + (10 * this.AreaCount));
            }
        }

        public GameArea LongViewArea
        {
            get
            {
                //Why Kind is null sometimes?
                if (this.Kind == null || !this.Kind.HasLongView)
                {
                    return this.ViewArea;
                }
                if (this.longViewArea != null)
                {
                    return this.longViewArea;
                }
                return (this.longViewArea = GameArea.GetAreaFromArea(this.ArchitectureArea, this.LongViewDistance, this.kind.HasObliqueView, this.BelongedFaction));
            }
            set
            {
                this.longViewArea = value;
            }
        }

        public int LongViewDistance
        {
            get
            {
                return (this.ViewDistance * 2);
            }
        }

        public int MilitaryCount
        {
            get
            {
                return this.Militaries.Count;
            }
        }

        public int EffectiveMilitaryCount
        {
            get
            {
                int result = 0;
                foreach (Military m in this.Militaries)
                {
                    if (!m.IsTransport)
                    {
                        result++;
                    }
                }
                return result;
            }
        }
        [DataMember]
        public int Morale
        {
            get
            {
                return this.morale;
            }
            set
            {
                if (this.morale == -2147483648)
                {
                    int z = 0;
                    z++;
                }
                this.morale = value;
            }
        }

        public int MoraleCeiling
        {
            get
            {
                return this.kind.HasMorale ? (this.Kind.MoraleBase + (this.Kind.MoraleUnit * (this.JianzhuGuimo - 1)) + this.IncrementOfMoraleCeiling) : 0;
            }
        }

        public string MoraleString
        {
            get
            {
                return (this.Morale + "/" + this.MoraleCeiling);
            }
        }

        public string FrontLineString
        {
            get
            {
                return this.FrontLine ? "○" : "×";
            }
        }

        public int MovingPersonCount
        {
            get
            {
                return this.MovingPersons.Count;
            }
        }

        public int NoFactionPersonCount
        {
            get
            {
                return this.NoFactionPersons.Count;
            }
        }

        public int NormalArmyScale
        {
            get
            {
                return (20 + (5 * this.AreaCount));
            }
        }

        public ArchitectureList OtherArchitectures
        {
            get
            {
                ArchitectureList list = new ArchitectureList();
                foreach (Architecture architecture in this.BelongedFaction.Architectures)
                {
                    if (architecture != this)
                    {
                        list.Add(architecture);
                    }
                }
                return list;
            }
        }

        public double PDRAgricultureFix
        {
            get
            {
                if (this.Agriculture >= ((int)(this.AgricultureCeiling * 0.6)))
                {
                    return 2E-05;
                }
                if (this.Agriculture < ((int)(this.AgricultureCeiling * 0.3)))
                {
                    return -2E-05;
                }
                return 0.0;
            }
        }

        public double PDRCommerceFix
        {
            get
            {
                if (this.Commerce >= ((int)(this.CommerceCeiling * 0.6)))
                {
                    return 2E-05;
                }
                if (this.Commerce < ((int)(this.CommerceCeiling * 0.3)))
                {
                    return -2E-05;
                }
                return 0.0;
            }
        }

        public double PDRDominationFix
        {
            get
            {
                if (this.Domination >= ((int)(this.DominationCeiling * 0.8)))
                {
                    return 2E-05;
                }
                if (this.Domination < ((int)(this.DominationCeiling * 0.2)))
                {
                    return -0.0001;
                }
                if (this.Domination < ((int)(this.DominationCeiling * 0.5)))
                {
                    return -2E-05;
                }
                return 0.0;
            }
        }

        public double PDRMoraleFix
        {
            get
            {
                if (this.Morale >= ((int)(this.MoraleCeiling * 0.6)))
                {
                    return 2E-05;
                }
                if (this.Morale < ((int)(this.MoraleCeiling * 0.1)))
                {
                    return -0.0001;
                }
                if (this.Morale < ((int)(this.MoraleCeiling * 0.3)))
                {
                    return -2E-05;
                }
                return 0.0;
            }
        }

        public int PersonCount
        {
            get
            {
                return this.Persons.Count;
            }
        }
        [DataMember]
        public int Population
        {
            get
            {
                return this.population;
            }
            set
            {
                this.population = value;
            }
        }

        public int PopulationCeiling
        {
            get
            {
                return (int)((this.Kind.PopulationBase + (this.Kind.PopulationUnit * (this.JianzhuGuimo - 1))) * (1 + this.RateIncrementOfPopulationCeiling));
            }
        }

        public double PopulationDevelopingRate
        {
            get
            {
                double num = Math.Round((double)(((((Session.Parameters.DefaultPopulationDevelopingRate + this.PDRAgricultureFix) + this.PDRCommerceFix) + this.PDRDominationFix) + this.PDRMoraleFix) + this.RateIncrementOfPopulationDevelop), 6);
                if (!((this.RecentlyHit <= 0) || this.DayAvoidInfluenceByBattle))
                {
                    num += -0.00030000000000000003;
                }

                return num * Session.Parameters.PopulationDevelopingRate;
            }
        }

        public double PopulationDevelopingRateString
        {
            get
            {
                return Math.Round((double)(this.PopulationDevelopingRate / 0.0001), 3);
            }
        }

        public Point Position
        {
            get
            {
                return this.ArchitectureArea.TopLeft;
            }
        }

        public int RecruitmentPopulationBoundary
        {
            get
            {
                // return (this.Kind.PopulationBoundary * this.AreaCount);
                return (this.Kind.PopulationBoundary);
            }
        }

        public string RegionEffectString
        {
            get
            {
                if ((this.LocationState.LinkedRegion.RegionCore != null) && this.LocationState.LinkedRegion.RegionCore.RegionCoreEffectAvail())
                {
                    if (this.LocationState.LinkedRegion.RegionCore.IsFriendlyWithoutTruce(this.BelongedFaction))
                    {
                        return "正面";
                    }
                    if (this.LocationState.LinkedRegion.RegionCore.IsHostile(this.BelongedFaction))
                    {
                        return "负面";
                    }
                }
                return "----";
            }
        }

        public string RegionString
        {
            get
            {
                return this.LocationState.LinkedRegionString;
            }
        }

        public int RewardPersonFund
        {
            get
            {
                return (int)(Session.Parameters.RewardPersonCost * this.RateOfRewardPerson);
            }
        }

        public int RewardPersonMaxCount
        {
            get
            {
                if (this.RewardPersonFund == 0) return int.MaxValue;
                return (this.Fund / this.RewardPersonFund);
            }
        }

        public int RoutewayActiveCost
        {
            get
            {
                int num = 0;
                foreach (Routeway routeway in this.Routeways)
                {
                    if (routeway.LastActivePoint != null)
                    {
                        num += routeway.LastActivePoint.ActiveFundCost;
                    }
                }
                return num;
            }
        }

        public string RoutewayActiveCostString
        {
            get
            {
                return (this.RoutewayActiveCost * 30 + "/月");
            }
        }

        public ArchitectureList RoutewayDestinationArchitectureList
        {
            get
            {
                ArchitectureList list = new ArchitectureList();
                foreach (Architecture architecture in this.RoutewayDestinationArchitectures.Values)
                {
                    list.Add(architecture);
                }
                return list;
            }
        }

        public string SectionString
        {
            get
            {
                if (this.BelongedSection != null)
                {
                    return this.BelongedSection.Name;
                }
                return "----";
            }
        }
        public bool ShowNumber
        {
            get
            {
                return this.showNumber;
            }
            set
            {
                this.showNumber = value;
                if (!value && this.IncrementNumberList != null && this.DecrementNumberList != null)
                {
                    this.IncrementNumberList.Clear();
                    this.DecrementNumberList.Clear();
                }
            }
        }

        /*
        public int SpyArchitectureFund
        {
            get
            {
                return (int)(Session.Parameters.SendSpyCost * this.RateOfSpyArchitecture);
            }
        }

        public int SpyPersonMaxCount
        {
            get
            {
                if (this.SpyArchitectureFund == 0) return int.MaxValue;
                return (this.Fund / this.SpyArchitectureFund);
            }
        }
        */

        public string StateEffectString
        {
            get
            {
                if ((this.LocationState.StateAdmin != null) && this.LocationState.StateAdmin.StateAdminEffectAvail())
                {
                    if (this.LocationState.StateAdmin.IsFriendlyWithoutTruce(this.BelongedFaction))
                    {
                        return "正面";
                    }
                    if (this.LocationState.StateAdmin.IsHostile(this.BelongedFaction))
                    {
                        return "负面";
                    }
                }
                return "----";
            }
        }

        public string StateString
        {
            get
            {
                return this.LocationState.Name;
            }
        }

        [DataMember]
        public int Technology
        {
            get
            {
                return this.technology;
            }
            set
            {
                this.technology = value;
            }
        }

        public int TechnologyCeiling
        {
            get
            {
                return this.kind.HasTechnology ? ((this.Kind.TechnologyBase + (this.Kind.TechnologyUnit * (this.JianzhuGuimo - 1))) + this.IncrementOfTechnologyCeiling) : 0;
            }
        }

        public string TechnologyString
        {
            get
            {
                return (this.Technology + "/" + this.TechnologyCeiling);
            }
        }

        public PlatformTexture Texture
        {
            get
            {
                return this.Kind.Texture;
            }
        }

        public int TransferFoodArchitectureCount
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.OtherArchitectures)
                {
                    if (architecture.TransferFoodArchitecture == this)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public int UnitPopulation
        {
            get
            {
                return (this.Population / this.AreaCount);
            }
        }

        public bool ValueWater
        {
            get
            {
                return (this.Kind.HasHarbor || this.TroopershipAvailable);
            }
        }

        public int VeryFewArmyScale
        {
            get
            {
                return (5 + this.AreaCount);
            }
        }

        public GameArea ViewArea
        {
            get
            {
                if (this.viewArea != null)
                {
                    return this.viewArea;
                }
                return (this.viewArea = GameArea.GetAreaFromArea(this.ArchitectureArea, this.ViewDistance, this.kind == null ? false : this.kind.HasObliqueView, this.BelongedFaction));
            }
            set
            {
                this.viewArea = value;
            }
        }

        public int ViewDistance
        {
            get
            {
                return (this.kind == null ? 0 : (this.Kind.ViewDistance + (this.AreaCount / this.Kind.ViewDistanceIncrementDivisor))) + this.IncrementOfViewRadius;
            }
        }

        public int WaterArmyScale
        {
            get
            {
                if (this.BelongedFaction != null)
                {
                    int num = 0;
                    foreach (Military military in this.Militaries)
                    {
                        if (military.Kind.Type == MilitaryType.水军 && !military.IsTransport)
                        {
                            num += military.Scales;
                        }
                        else if (this.ValueWater)
                        {
                            num += military.Quantity / 0x7d0;
                        }
                    }
                    return num;
                }
                return ((this.AreaCount * 5) + ((this.Population / 0x2710) / 2));
            }
        }

        private class AILinkProcedureDetail
        {
            public Architecture A;
            public int Level;
            public List<Architecture> Path = new List<Architecture>();

            public AILinkProcedureDetail(int level, Architecture a, List<Architecture> path)
            {
                this.Level = level;
                this.A = a;
                foreach (Architecture architecture in path)
                {
                    this.Path.Add(architecture);
                }
            }
        }
        /*
        public FreeText jianzhubiaoti
        {
            get;
            set;
        }
        */
        //[DataMember]
        public qizi jianzhuqizi
        {
            get;
            set;
        }
        [DataMember]
        public bool youzainan
        {
            get;
            set;
        }

        public string zainanming
        {
            get
            {
                if (this.youzainan)
                {
                    return this.zainan.zainanzhonglei.Name;
                }
                else
                {
                    return "——";
                }
            }
        }

        public string zainanshengyutianshu
        {
            get
            {
                if (this.youzainan)
                {
                    return (this.zainan.shengyutianshu * Session.Parameters.DayInTurn).ToString();
                }
                else
                {
                    return "——";
                }
            }
        }

        public bool kezhenzai()
        {

            if (this.youzainan && this.Fund > 0 && this.Food > 0 && this.HasPerson())
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool kenafei()
        {
            if (this.BelongedFaction != null && !this.BelongedFaction.hougongValid) return false;

            if (this.nvxingwujiang().Count > 0 && (this.Fund >= Session.Parameters.NafeiCost || this.BelongedFaction.IsAlien) &&
                (this.Meinvkongjian > this.Feiziliebiao.Count || this.BelongedFaction.IsAlien)
                && this.Persons.GameObjects.Contains(this.BelongedFaction.Leader))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool kejinhougong()
        {
            if (this.meifaxianhuaiyundefeiziliebiao().Count != 0 && this.Persons.GameObjects.Contains(this.BelongedFaction.Leader) && !this.BelongedFaction.Leader.huaiyun)
            {

                return true;
            }
            return false;

        }


        public int ExpandFund()
        {
            if (this.JianzhuGuimo == 1)
            {
                return 100000;
            }
            else if (this.JianzhuGuimo == 5)
            {
                return 200000;
            }
            else
            {
                return 0;
            }
        }
        public List<Point> ExpandPoint()
        {
            List<Point> xinjiadedian = new List<Point>();
            if (this.JianzhuGuimo == 1)
            {

                xinjiadedian.Add(new Point(zhongxindian.X - 1, zhongxindian.Y));
                xinjiadedian.Add(new Point(zhongxindian.X + 1, zhongxindian.Y));
                xinjiadedian.Add(new Point(zhongxindian.X, zhongxindian.Y - 1));
                xinjiadedian.Add(new Point(zhongxindian.X, zhongxindian.Y + 1));


            }
            else if (this.JianzhuGuimo == 5)
            {

                xinjiadedian.Add(new Point(zhongxindian.X - 2, zhongxindian.Y));
                xinjiadedian.Add(new Point(zhongxindian.X + 2, zhongxindian.Y));
                xinjiadedian.Add(new Point(zhongxindian.X, zhongxindian.Y - 2));
                xinjiadedian.Add(new Point(zhongxindian.X, zhongxindian.Y + 2));
                xinjiadedian.Add(new Point(zhongxindian.X - 1, zhongxindian.Y - 1));
                xinjiadedian.Add(new Point(zhongxindian.X - 1, zhongxindian.Y + 1));
                xinjiadedian.Add(new Point(zhongxindian.X + 1, zhongxindian.Y - 1));
                xinjiadedian.Add(new Point(zhongxindian.X + 1, zhongxindian.Y + 1));
            }
            else
            {
                return null;
            }
            return xinjiadedian;
        }

        public bool ExpandAvail()
        {
            if (this.Fund < this.ExpandFund()) return false;
            if (this.JianzhuGuimo != 1 && this.JianzhuGuimo != 5) return false;
            if (Session.Current.Scenario.ScenarioMap.UseSimpleArchImages) return false;

            if (this.Kind.Expandable < this.JianzhuGuimo) return false;

            TerrainDetail terrainKindByPosition;
            foreach (Point point in this.ExpandPoint())
            {
                if (Session.Current.Scenario.PositionOutOfRange(point))
                {
                    return false;
                }
                terrainKindByPosition = Session.Current.Scenario.GetTerrainDetailByPosition(point);
                if (!terrainKindByPosition.CanExtendInto)
                {
                    return false;
                }
            }
            if (this.HasHostileTroopsInView()) return false;

            List<Condition> conditions = new List<Condition>();
            for (int i = 0; i < Session.Parameters.ExpandConditions.Count; i++)
            {
                Condition c = Session.Current.Scenario.GameCommonData.AllConditions.GetCondition(Session.Parameters.ExpandConditions[i]);
                conditions.Add(c);
            }
            if (!Condition.CheckConditionList(conditions, this)) return false;

            return true;
        }

        public void Expand()
        {
            this.Fund -= this.ExpandFund();
            foreach (Point point in this.ExpandPoint())
            {
                this.ArchitectureArea.AddPoint(point);
            }


            this.ContactArea = this.ArchitectureArea.GetContactArea(false);
            this.ViewArea = GameArea.GetAreaFromArea(this.ArchitectureArea, this.ViewDistance, this.Kind.HasObliqueView, this.BelongedFaction);
            this.LongViewArea = GameArea.GetAreaFromArea(this.ArchitectureArea, this.LongViewDistance, this.Kind.HasObliqueView, this.BelongedFaction);
            this.BaseFoodSurplyArea = this.LongViewArea;

            foreach (Point point in this.ArchitectureArea.Area)
            {
                Session.Current.Scenario.MapTileData[point.X, point.Y].TileArchitecture = this;
            }

            Session.Current.Scenario.SetMapTileArchitecture(this);

            this.ViewArea = null;
            this.LongViewArea = null;
            if (!Session.Current.Scenario.Preparing)
            {
                foreach (Architecture architecture2 in Session.Current.Scenario.Architectures)
                {
                    architecture2.RefreshViewArea();
                }
                foreach (Troop troop in Session.Current.Scenario.Troops)
                {
                    troop.RefreshViewArchitectureRelatedArea();
                }
            }
            if (this.AutoRefillFoodInLongViewArea)
            {
                foreach (Point point in this.LongViewArea.Area)
                {
                    if (!Session.Current.Scenario.PositionOutOfRange(point))
                    {
                        Session.Current.Scenario.MapTileData[point.X, point.Y].AddSupplyingArchitecture(this);
                    }
                }
            }
            this.BelongedFaction.AddArchitectureKnownData(this);
        }

        public PersonList meifaxianhuaiyundefeiziliebiao()
        {
            PersonList meihuailiebiao = new PersonList();
            foreach (Person person in this.Feiziliebiao)
            {
                if (!person.faxianhuaiyun && this.BelongedFaction.Leader.isLegalFeiZiExcludeAge(person) && person.ArrivingDays <= 0)
                    meihuailiebiao.Add(person);
            }
            return meihuailiebiao;
        }

        public PersonList movableFeizis
        {
            get
            {
                PersonList meihuailiebiao = new PersonList();
                foreach (Person person in this.Feiziliebiao)
                {
                    if (person.ArrivingDays <= 0)
                        meihuailiebiao.Add(person);
                }
                return meihuailiebiao;
            }
        }

        /*
        private bool younvxingwujiang()
        {
            foreach (Person person in this.Persons)
            {
                if (person.Sex != person.BelongedFaction.Leader.Sex)
                {
                    return true;
                }
            }
            return false;
        }
        */

        public PersonList nvxingwujiang()
        {
            PersonList nvxingwujiangliebiao = new PersonList();
            foreach (Person person in this.Persons)
            {
                if (person.ArrivingDays > 0) continue;
                if (this.BelongedFaction.Leader.isLegalFeiZi(person, true) || (this.BelongedFaction.IsAlien && this.BelongedFaction.Leader.isLegalFeiZiExcludeAge(person)))
                {
                    nvxingwujiangliebiao.Add(person);
                }
            }
            if (this.BelongedFaction.IsAlien)
            {
                foreach (Captive c in this.Captives)
                {
                    Person person = c.CaptivePerson;
                    if (person.ArrivingDays > 0) continue;
                    if (this.BelongedFaction.Leader.isLegalFeiZiExcludeAge(person))
                    {
                        nvxingwujiangliebiao.Add(person);
                    }
                }
            }
            return nvxingwujiangliebiao;
        }

        public PersonList CanKilledPersons()
        {
            PersonList personList = new PersonList();
            foreach (Person person in this.Persons)
            {
                if (person.ID != this.BelongedFaction.LeaderID)
                {
                    personList.Add(person);
                }
            }

            return personList;
        }

        public PersonList CanKilledCaptives()
        {
            PersonList personList = new PersonList();
            foreach (Captive captive in this.Captives)
            {

                personList.Add(captive.CaptivePerson);

            }

            return personList;
        }

        public bool PrincessChangeLeader(bool byOccupy, Faction capturer, Person p)
        {
            bool result = false;
            if (p.Spouse != null)
            {
                if (p.Spouse.Spouse == p)
                {
                    p.Spouse.Spouse = null;
                }
                p.Spouse = null;
            }
            if (capturer.Leader.isLegalFeiZiExcludeAge(p) && capturer.hougongValid)
             {
                if (byOccupy)
                {
                    Session.Current.Scenario.YearTable.addChangeFactionPrincessEntry(Session.Current.Scenario.Date, p, capturer);
                }
                if (p.Spouse != null && this.BelongedFaction != null)
                {
                    p.Spouse.AddHated(this.BelongedFaction.Leader, -200 * p.Spouse.PersonalLoyalty * p.Spouse.PersonalLoyalty);
                }
                result = true;
             }
             else
             {
                p.Status = PersonStatus.Normal;
                if (byOccupy)
                {
                    Session.Current.Scenario.YearTable.addOutOfPrincessEntry(Session.Current.Scenario.Date, p, capturer);
                }
                else
                {
                    Session.Current.Scenario.YearTable.addOutOfPrincessByLeaderDeathEntry(Session.Current.Scenario.Date, p, capturer);
                }
            }
            return result;
        }

        public int Meinvkongjian
        {
            get
            {
                int kongjian = 0;
                foreach (Facility facility in this.Facilities)
                {
                    kongjian += facility.Kind.rongna;
                }
                return kongjian;
            }
        }

        public string meinvkongjianzifu
        {
            get
            {
                return this.Feiziliebiao.Count.ToString() + "/" + this.Meinvkongjian.ToString();
            }
        }

        public bool CanRemoveFacility(Facility f)
        {
            if (f.Kind.bukechaichu) return false;
            // if (this.Meinvkongjian - this.Feiziliebiao.Count < f.Kind.rongna && this.BelongedFaction != null && !this.BelongedFaction.IsAlien) return false;
            return true;
        }

        public FacilityList kechaichudesheshi()
        {
            FacilityList kechaichu = new FacilityList();
            foreach (Facility facility in this.Facilities)
            {
                if (this.CanRemoveFacility(facility))
                {
                    kechaichu.Add(facility);
                }
            }
            return kechaichu;
        }

        public PersonList yihuaiyundefeiziliebiao()
        {
            PersonList feiziliebiao = new PersonList();
            foreach (Person feizi in this.Feiziliebiao)
            {
                if (feizi.huaiyun)
                {
                    feiziliebiao.Add(feizi);
                }
            }
            return feiziliebiao;
        }

        [DataMember]
        public bool huangdisuozai
        {
            get;
            set;
        }

        public bool kejingongzijin()
        {
            if (Session.Current.Scenario.Date.Month == 3 && Session.Current.Scenario.youhuangdi() && this.Fund > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool KillPersonAvail()
        {
            if (this.PersonCount - (this.Persons.GameObjects.Contains(this.BelongedFaction.Leader) ? 1 : 0) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }



        public bool kejingongliangcao()
        {
            if (Session.Current.Scenario.Date.Month == 3 && Session.Current.Scenario.youhuangdi() && this.Food > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Point zhongxindian
        {
            get
            {
                int xzonghe = 0;
                int yzonghe = 0;
                int xpingjunzhi;
                int ypingjunzhi;
                foreach (Point p in this.ArchitectureArea.Area)
                {
                    xzonghe += p.X;
                    yzonghe += p.Y;
                }
                xpingjunzhi = xzonghe / this.JianzhuGuimo;
                ypingjunzhi = yzonghe / this.JianzhuGuimo;
                foreach (Point p in this.ArchitectureArea.Area)
                {
                    if (p.X == xpingjunzhi && p.Y == ypingjunzhi)
                    {
                        return p;
                    }
                }
                return this.ArchitectureArea.Area[0];
            }
        }

        public Point dingdian
        {
            get
            {
                Point zuishangmiandedian = this.ArchitectureArea.Area[0];
                foreach (Point p in this.ArchitectureArea.Area)
                {
                    if (p.Y < zuishangmiandedian.Y)
                    {
                        zuishangmiandedian = p;
                    }
                }

                if (this.kind != null && this.Kind.ID == 2)  //如果是关隘
                {
                    if (this.JianzhuGuimo == 1)
                    {
                        return zuishangmiandedian;
                    }
                    if (this.ArchitectureArea.Area[0].X == this.ArchitectureArea.Area[1].X)
                    {
                        return zuishangmiandedian;
                    }
                    else
                    {
                        return this.zhongxindian;
                    }

                }

                return zuishangmiandedian;
            }
        }

        public int IdlingPersonCount
        {
            get
            {
                int result = 0;
                foreach (Person person in this.Persons)
                {
                    if (person.WorkKind == ArchitectureWorkKind.无)
                    {
                        result++;
                    }
                }
                return result;
            }
        }

        public String yocelanPersonString
        {
            get
            {
                return IdlingPersonCount + "/" + PersonCount;
            }
        }

        public Person Mayor
        {
            get
            {
                if (this.MayorID == -1 ) return null ;

                if ( this.mayor == null )
                {
                    this.mayor = Session.Current.Scenario.Persons.GetGameObject(this.MayorID)as Person ;
                }

                if (this.mayor != null && this.BelongedFaction != null &&
                    (this.mayor == this.BelongedFaction .Leader || !this.mayor.Alive || !this.mayor.Available
                    || this.mayor.BelongedFaction != this.BelongedFaction
                    || this.mayor.BelongedFaction == null || (this.mayor.LocationArchitecture != this && this.mayor.BelongedTroop == null )))
                {
                    this.Mayor = null;
                }
                return this.mayor;
            }
            set
            {
                this.mayor = value;
                if (this.mayor != null)
                {
                    this.MayorID = this.mayor.ID;
                }
                else
                {
                    this.MayorID = -1;
                }
                this.MayorOnDutyDays = 0;
            }
        }
        [DataMember]
        public int MayorID { get; set; }
        //{
        //    get
        //    {
        //        return this.mayorID;
        //    }
        //    set
        //    {
        //        this.mayorID = value;
        //        //this.MayorOnDutyDays = 0;
        //    }
        //}

        public string MayorName
        {
            get
            {
                return ((this.Mayor != null) ? this.Mayor.Name : "----");
            }
        }


        public Person Advisor
        {
            get
            {
                GameObjectList sorted = this.Persons.GetList();

                if (sorted.Count == 0) return null;

                sorted.IsNumber = true;
                sorted.PropertyName = "Intelligence";
                sorted.SmallToBig = false;
                sorted.ReSort();

                PersonList cropped = new PersonList();
                foreach (Person p in sorted)
                {
                    if (p.Intelligence >= 70)
                    {
                        cropped.Add(p);
                    }
                    else
                    {
                        break;
                    }
                }

                if (cropped.Count == 0) return null;

                return (Person) cropped[GameObject.Random(cropped.Count / 5)];
            }
        }

        private int MeiXunlianHaoDeBianduiShu()
        {
            int bianduiShu = 0;
            foreach (Military military in this.Militaries)
            {
                if (military.Morale < military.MoraleCeiling || military.Combativity < military.CombativityCeiling)
                {
                    bianduiShu++;
                }
            }
            return bianduiShu;
        }

        public ArchitectureList ArchitectureListWithoutSelf()
        {
            ArchitectureList architectureList = new ArchitectureList();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                architectureList.Add(architecture);
            }
            architectureList.Remove(this);
            return architectureList;
        }

        public void AddInformation(Information information)
        {
            this.Informations.AddInformation(information);
            information.BelongedArchitecture = this;
        }

        public void RemoveInformation(Information information)
        {
            this.Informations.Remove(information);
            information.BelongedArchitecture = null;
        }

        public delegate void BeginRecentlyAttacked(Architecture architecture);

        public delegate void FacilityCompleted(Architecture architecture, Facility facility);

        public delegate void fashengzainan(Architecture architecture, int zainanID);

        public delegate void HirePerson(PersonList personList);

        public delegate void MilitaryCreate(Architecture architecture, Military military);

        public delegate void PopulationEnter(Architecture a, int quantity);

        public delegate void PopulationEscape(Architecture a, int quantity);

        public delegate void ReleaseCaptiveAfterOccupied(Architecture architecture, PersonList persons);

        public delegate void RewardPersons(Architecture architecture, GameObjectList personlist);

        [StructLayout(LayoutKind.Sequential)]
        private struct RoutewayProcedureDetail
        {
            public Architecture Start;
            public float PreviousRate;
            public RoutewayProcedureDetail(Architecture a, float rate)
            {
                this.Start = a;
                this.PreviousRate = rate;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WorkRate
        {
            public float rate;
            public ArchitectureWorkKind workKind;
            public WorkRate(float r, ArchitectureWorkKind k)
            {
                this.rate = r;
                this.workKind = k;
            }
        }

        public class WorkRateList
        {
            public List<Architecture.WorkRate> RateList = new List<Architecture.WorkRate>();

            public void AddWorkRate(Architecture.WorkRate wr)
            {
                for (int i = 0; i < this.RateList.Count; i++)
                {
                    if (wr.rate <= this.RateList[i].rate)
                    {
                        this.RateList.Insert(i, wr);
                        return;
                    }
                }
                this.RateList.Add(wr);
            }

            public int Count
            {
                get
                {
                    return this.RateList.Count;
                }
            }
        }

        public float InfluenceKindValue(int id)
        {
            float result = 0;
            foreach (Influence i in Session.Current.Scenario.GameCommonData.AllInfluences.Influences.Values)
            {
                if (i.Kind.ID == id)
                {
                    foreach (ApplyingArchitecture j in i.appliedArch)
                    {
                        if (j.arch == this)
                        {
                            result += i.Value;
                        }
                    }
                }
            }
            return result;
        }

        public Person GetMaxFightingForcePerson()
        {
            int temp = 0;
            Person person = new Person();
            foreach (Person p in this.Persons)
            {
                if(p.FightingForce>temp)
                {
                    person = p;
                }
            }
            return person;
        }

    }
}
