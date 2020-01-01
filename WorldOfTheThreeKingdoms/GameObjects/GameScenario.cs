using GameGlobal;
using GameObjects.Animations;
using GameObjects.ArchitectureDetail;
using GameObjects.ArchitectureDetail.EventEffect;
using GameObjects.Conditions;
using GameObjects.FactionDetail;
using GameObjects.Influences;
using GameObjects.MapDetail;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Linq;
using Tools;
using GameManager;
using Platforms;
using WorldOfTheThreeKingdoms.GameScreens;

namespace GameObjects
{
    //using GameObjects.PersonDetail.PersonMessages;
    //using GameFreeText;
    [DataContract]
    public class GameScenario
    {
        //public GameFreeText.FreeText GameProgressCaution;

        [DataMember]
        public string MOD { get; set; }

        public static string SCENARIO_ERROR_TEXT_FILE
        {
            get
            {
                return Platform.Current.DirectoryName(Platform.Current.Location) + "/GameData/ScenarioErrors.txt";
            }
        }

        private Dictionary<int, Architecture> AllArchitectures = new Dictionary<int, Architecture>();
        private Dictionary<int, Person> AllPersons = new Dictionary<int, Person>();

        public FactionList PlayerFactions = new FactionList();
        public PersonList PreparedAvailablePersons = new PersonList();
        public bool Preparing = false;

        public Dictionary<TroopEvent, TroopList> TroopEventsToApply = new Dictionary<TroopEvent, TroopList>();

        public Dictionary<Event, Architecture> EventsToApply = new Dictionary<Event, Architecture>();
        public Dictionary<Event, Architecture> YesEventsToApply = new Dictionary<Event, Architecture>();
        public Dictionary<Event, Architecture> NoEventsToApply = new Dictionary<Event, Architecture>();

        // 缓存地图上有几支部队在埋伏
        private int numberOfAmbushTroop = -1;
        public static int savemaxcounts=49;
        // public Dictionary<Event, Architecture> YesArchiEventsToApply = new Dictionary<Event, Architecture>();
        //public Dictionary<Event, Architecture> NoArchiEventsToApply = new Dictionary<Event, Architecture>();

        public bool EnableLoadAndSave = true;

        // public OngoingBattleList AllOngoingBattles = new OngoingBattleList();

        private PersonList emptyPersonList = new PersonList();
        private CaptiveList emptyCaptiveList = new CaptiveList();

        public Dictionary<PathCacheKey, List<Point>> pathCache = new Dictionary<PathCacheKey, List<Point>>();

        public bool JustSaved = false;

        public GameScenario Clone()
        {
            return this.MemberwiseClone() as GameScenario;
        }

        [DataMember]
        public Dictionary<int, int[]> AiBattlingArchitectureStrings = new Dictionary<int, int[]>();

        [DataMember]
        public ArchitectureList Architectures = new ArchitectureList();
        
        public Faction CurrentFaction;
        public Faction CurrentPlayer;

        [DataMember]
        public GameDate Date = new GameDate();

        [DataMember]
        public DiplomaticRelationTable DiplomaticRelations = new DiplomaticRelationTable();

        [DataMember]
        public FacilityList Facilities = new FacilityList();

        [DataMember]
        public FactionListWithQueue Factions = new FactionListWithQueue();

        [DataMember]
        public PositionTable FireTable = new PositionTable();

        [DataMember]
        public CommonData GameCommonData = new CommonData();
        
        public TileAnimationGenerator GeneratorOfTileAnimation;

        [DataMember]
        public InformationList Informations = new InformationList();

        [DataMember]
        public LegionList Legions = new LegionList();

        public TileData[,] MapTileData;

        [DataMember]
        public MilitaryList Militaries = new MilitaryList();

        private Person neutralPerson;
        public bool NewInfluence;

        [DataMember]
        public NoFoodTable NoFoodDictionary = new NoFoodTable();

        public int[,] PenalizedMapData;

        [DataMember]
        public Dictionary<int, int> FatherIds = new Dictionary<int, int>();
        [DataMember]
        public Dictionary<int, int> MotherIds = new Dictionary<int, int>();
        [DataMember]
        public Dictionary<int, int> SpouseIds = new Dictionary<int, int>();
        [DataMember]
        public Dictionary<int, int[]> BrotherIds = new Dictionary<int, int[]>();
        [DataMember]
        public Dictionary<int, int[]> SuoshuIds = new Dictionary<int, int[]>();
        [DataMember]
        public Dictionary<int, int[]> CloseIds = new Dictionary<int, int[]>();
        [DataMember]
        public Dictionary<int, int[]> HatedIds = new Dictionary<int, int[]>();
        [DataMember]
        public Dictionary<int, int> MarriageGranterId = new Dictionary<int, int>();

        [DataMember]
        public List<PersonIDRelation> PersonRelationIds = new List<PersonIDRelation>();

        [DataMember]
        public PersonList Persons = new PersonList();

        [DataMember]
        public List<int> PlayerList { get; set; }  

        [DataMember]
        public string CurrentPlayerID { get; set; }

        [DataMember]
        public string PlayerInfo { get; set; }        

        [DataMember]
        public RegionList Regions = new RegionList();

        [DataMember]
        public RoutewayList Routeways = new RoutewayList();

        [DataMember]
        public string ScenarioDescription;

        [DataMember]
        public Map ScenarioMap = new Map();

        [DataMember]
        public string ScenarioTitle;

        [DataMember]
        public SectionList Sections = new SectionList();
        //public GameMessageList SpyMessages = new GameMessageList();

        [DataMember]
        public StateList States = new StateList();

        public int[] TerrainAdaptability;
        public bool Threading;

        [DataMember]
        public TreasureList Treasures = new TreasureList();

        [DataMember]
        public TroopEventList TroopEvents = new TroopEventList();

        [DataMember]
        public TroopListWithQueue Troops = new TroopListWithQueue();

        [DataMember]
        public YearTable YearTable = new YearTable();

        [DataMember]
        public EventList AllEvents = new EventList();

        public String LoadedFileName;

        [DataMember]
        public bool UsingOwnCommonData;

        [DataMember]
        public BiographyTable AllBiographies = new BiographyTable();

        [DataMember]
        public int GameTime;

        private DateTime sessionStartTime;

        public bool needAutoSave = false;

        public int NumberOfAmbushTroop
        {
            get
            {
                if (numberOfAmbushTroop >= 0)
                    return numberOfAmbushTroop;
                else
                {
                    int number = 0;
                    foreach (Troop t in Troops)
                    {
                        if (t.Status == TroopStatus.埋伏)
                            number++;
                    }
                    numberOfAmbushTroop = number;
                    return numberOfAmbushTroop;
                }
            }
        }

        public event AfterLoadScenario OnAfterLoadScenario;

        public event AfterSaveScenario OnAfterSaveScenario;

        public event NewFactionAppear OnNewFactionAppear;

        public bool scenarioJustLoaded;

        [DataMember]
        public int DaySince { get; set; }

        [DataMember]
        public Parameters Parameters { get; set; }

        [DataMember]
        public GlobalVariables GlobalVariables { get; set; }

        public GameScenario()
        {

        }
          
        public void Init()
        {
            this.GeneratorOfTileAnimation = new TileAnimationGenerator();

            //public static readonly string SCENARIO_ERROR_TEXT_FILE = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/GameData/ScenarioErrors.txt";
            AllArchitectures = new Dictionary<int, Architecture>();
            AllPersons = new Dictionary<int, Person>();

            PlayerFactions = new FactionList();
            PreparedAvailablePersons = new PersonList();
            Preparing = false;

            TroopEventsToApply = new Dictionary<TroopEvent, TroopList>();

            EventsToApply = new Dictionary<Event, Architecture>();
            YesEventsToApply = new Dictionary<Event, Architecture>();
            NoEventsToApply = new Dictionary<Event, Architecture>();

            // 缓存地图上有几支部队在埋伏
            numberOfAmbushTroop = -1;

            EnableLoadAndSave = true;

            emptyPersonList = new PersonList();
            emptyCaptiveList = new CaptiveList();

            pathCache = new Dictionary<PathCacheKey, List<Point>>();

            if (this.UsingOwnCommonData)
            {
                this.GameCommonData = CommonData.Current;
            }
        }

        private Dictionary<Architecture, PersonList>
             NormalPLCache, MovingPLCache, NoFactionPLCache, NoFactionMovingPLCache, PrincessPLCache,
             ZhenzaiPLCache, AgriculturePLCache, CommercePLCache, TechnologyPLCache,
             DominationPLCache, MoralePLCache, EndurancePLCache, TrainingPLCache;
        private Dictionary<Architecture, CaptiveList> CaptivePLCache;

        public PersonList GetPersonList(Architecture a)
        {
            if (NormalPLCache == null)
            {
                CreatePersonStatusCache();
            }
            if (!this.NormalPLCache.ContainsKey(a)) return emptyPersonList;
            return NormalPLCache[a];
        }

        public PersonList GetMovingPersonList(Architecture a)
        {
            if (MovingPLCache == null)
            {
                CreatePersonStatusCache();
            }
            if (!this.MovingPLCache.ContainsKey(a)) return emptyPersonList;
            return MovingPLCache[a];
        }

        public PersonList GetNoFactionPersonList(Architecture a)
        {
            if (NoFactionPLCache == null)
            {
                CreatePersonStatusCache();
            }
            if (!this.NoFactionPLCache.ContainsKey(a)) return emptyPersonList;
            return NoFactionPLCache[a];
        }

        public PersonList GetNoFactionMovingPersonList(Architecture a)
        {
            if (NoFactionMovingPLCache == null)
            {
                CreatePersonStatusCache();
            }
            if (!this.NoFactionMovingPLCache.ContainsKey(a)) return emptyPersonList;
            return NoFactionMovingPLCache[a];
        }

        public PersonList GetPrincessPersonList(Architecture a)
        {
            if (PrincessPLCache == null)
            {
                CreatePersonStatusCache();
            }
            if (!this.PrincessPLCache.ContainsKey(a)) return emptyPersonList;
            return PrincessPLCache[a];
        }

        public CaptiveList GetCaptiveList(Architecture a)
        {
            if (CaptivePLCache == null)
            {
                CreatePersonStatusCache();
            }
            if (!this.CaptivePLCache.ContainsKey(a)) return emptyCaptiveList;
            return CaptivePLCache[a];
        }

        public PersonList GetZhenzaiPersonList(Architecture a)
        {
            if (ZhenzaiPLCache == null)
            {
                CreatePersonWorkCache();
            }
            if (!this.ZhenzaiPLCache.ContainsKey(a)) return emptyPersonList;
            return ZhenzaiPLCache[a];
        }

        public PersonList GetAgriculturePersonList(Architecture a)
        {
            if (AgriculturePLCache == null)
            {
                CreatePersonWorkCache();
            }
            if (!this.AgriculturePLCache.ContainsKey(a)) return emptyPersonList;
            return AgriculturePLCache[a];
        }

        public PersonList GetCommercePersonList(Architecture a)
        {
            if (CommercePLCache == null)
            {
                CreatePersonWorkCache();
            }
            if (!this.CommercePLCache.ContainsKey(a)) return emptyPersonList;
            return CommercePLCache[a];
        }

        public PersonList GetTechnologyPersonList(Architecture a)
        {
            if (TechnologyPLCache == null)
            {
                CreatePersonWorkCache();
            }
            if (!this.TechnologyPLCache.ContainsKey(a)) return emptyPersonList;
            return TechnologyPLCache[a];
        }

        public PersonList GetDomintaionPersonList(Architecture a)
        {
            if (DominationPLCache == null)
            {
                CreatePersonWorkCache();
            }
            if (!this.DominationPLCache.ContainsKey(a)) return emptyPersonList;
            return DominationPLCache[a];
        }

        public PersonList GetMoralePersonList(Architecture a)
        {
            if (MoralePLCache == null)
            {
                CreatePersonWorkCache();
            }
            if (!this.MoralePLCache.ContainsKey(a)) return emptyPersonList;
            return MoralePLCache[a];
        }

        public PersonList GetEndurancePersonList(Architecture a)
        {
            if (EndurancePLCache == null)
            {
                CreatePersonWorkCache();
            }
            if (!this.EndurancePLCache.ContainsKey(a)) return emptyPersonList;
            return EndurancePLCache[a];
        }

        public PersonList GetTrainingPersonList(Architecture a)
        {
            if (TrainingPLCache == null)
            {
                CreatePersonWorkCache();
            }
            if (!this.TrainingPLCache.ContainsKey(a)) return emptyPersonList;
            return TrainingPLCache[a];
        }

        private void CreatePersonWorkCache()
        {
            ZhenzaiPLCache = new Dictionary<Architecture, PersonList>();
            AgriculturePLCache = new Dictionary<Architecture, PersonList>();
            CommercePLCache = new Dictionary<Architecture, PersonList>();
            TechnologyPLCache = new Dictionary<Architecture, PersonList>();
            DominationPLCache = new Dictionary<Architecture, PersonList>();
            MoralePLCache = new Dictionary<Architecture, PersonList>();
            EndurancePLCache = new Dictionary<Architecture, PersonList>();
            TrainingPLCache = new Dictionary<Architecture, PersonList>();

            foreach (Person i in this.AvailablePersons)
            {
                if (i.Status == PersonStatus.Normal && i.WorkKind == ArchitectureWorkKind.赈灾 && (i.LocationTroop == null || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.ZhenzaiPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.ZhenzaiPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    ZhenzaiPLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Normal && i.WorkKind == ArchitectureWorkKind.农业 && (i.LocationTroop == null || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.AgriculturePLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.AgriculturePLCache[i.LocationArchitecture] = new PersonList();
                    }
                    AgriculturePLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Normal && i.WorkKind == ArchitectureWorkKind.商业 && (i.LocationTroop == null || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.CommercePLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.CommercePLCache[i.LocationArchitecture] = new PersonList();
                    }
                    CommercePLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Normal && i.WorkKind == ArchitectureWorkKind.技术 && (i.LocationTroop == null || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.TechnologyPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.TechnologyPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    TechnologyPLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Normal && i.WorkKind == ArchitectureWorkKind.统治 && (i.LocationTroop == null || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.DominationPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.DominationPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    DominationPLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Normal && i.WorkKind == ArchitectureWorkKind.民心 && (i.LocationTroop == null || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.MoralePLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.MoralePLCache[i.LocationArchitecture] = new PersonList();
                    }
                    MoralePLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Normal && i.WorkKind == ArchitectureWorkKind.耐久 && (i.LocationTroop == null || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.EndurancePLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.EndurancePLCache[i.LocationArchitecture] = new PersonList();
                    }
                    EndurancePLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Normal && i.WorkKind == ArchitectureWorkKind.训练 && (i.LocationTroop == null || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.TrainingPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.TrainingPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    TrainingPLCache[i.LocationArchitecture].Add(i);
                }
            }
        }

        private void CreatePersonStatusCache()
        {
            NormalPLCache = new Dictionary<Architecture, PersonList>();
            MovingPLCache = new Dictionary<Architecture, PersonList>();
            NoFactionPLCache = new Dictionary<Architecture, PersonList>();
            NoFactionMovingPLCache = new Dictionary<Architecture, PersonList>();
            PrincessPLCache = new Dictionary<Architecture, PersonList>();
            CaptivePLCache = new Dictionary<Architecture, CaptiveList>();

            foreach (Person i in this.AvailablePersons)
            {
                if (i.Status == PersonStatus.Normal && i.LocationArchitecture != null && (i.LocationTroop == null || i.LocationTroop.Destroyed || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.NormalPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.NormalPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    NormalPLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Moving && i.LocationArchitecture != null && (i.LocationTroop == null || i.LocationTroop.Destroyed || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.MovingPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.MovingPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    MovingPLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.NoFaction && i.LocationArchitecture != null && (i.LocationTroop == null || i.LocationTroop.Destroyed || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.NoFactionPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.NoFactionPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    NoFactionPLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.NoFactionMoving && i.LocationArchitecture != null && (i.LocationTroop == null || i.LocationTroop.Destroyed || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.NoFactionMovingPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.NoFactionMovingPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    NoFactionMovingPLCache[i.LocationArchitecture].Add(i);
                }
                if (i.Status == PersonStatus.Princess && i.LocationArchitecture != null && (i.LocationTroop == null || i.LocationTroop.Destroyed || !this.Troops.GameObjects.Contains(i.LocationTroop)))
                {
                    if (!this.PrincessPLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.PrincessPLCache[i.LocationArchitecture] = new PersonList();
                    }
                    PrincessPLCache[i.LocationArchitecture].Add(i);
                }
            }
            foreach (Captive i in this.Captives)
            {
                if (i.LocationArchitecture != null && i.CaptivePerson != null)
                {
                    if (!this.CaptivePLCache.ContainsKey(i.LocationArchitecture))
                    {
                        this.CaptivePLCache[i.LocationArchitecture] = new CaptiveList();
                    }
                    CaptivePLCache[i.LocationArchitecture].Add(i);
                }
            }
        }

        public void ClearPersonStatusCache()
        {
            NormalPLCache = MovingPLCache = NoFactionPLCache = NoFactionMovingPLCache = PrincessPLCache = null;
            CaptivePLCache = null;
        }

        public void ClearPersonWorkCache()
        {
            ZhenzaiPLCache = AgriculturePLCache = CommercePLCache = TechnologyPLCache =
            DominationPLCache = MoralePLCache = EndurancePLCache = TrainingPLCache = null;
        }

        [DataMember]
        public CaptiveList captiveData = new CaptiveList();

        public CaptiveList Captives
        {
            get
            {
                CaptiveList result = new CaptiveList();
                foreach (Person i in this.Persons)
                {
                    if (i.Status == PersonStatus.Captive)
                    {
                        if (i.BelongedCaptive == null)
                        {
                            continue;
                        }
                        result.Add(i.BelongedCaptive);
                    }
                }
                return result;
            }
        }

        public PersonList AvailablePersons
        {
            get
            {
                PersonList result = new PersonList();
                foreach (Person i in this.Persons)
                {
                    if (i.Status != PersonStatus.None && i.Alive && i.Available)
                    {
                        result.Add(i);
                    }
                }
                return result;
            }
        }

        public PersonList DeadPersons
        {
            get
            {
                PersonList result = new PersonList();
                foreach (Person i in this.Persons)
                {
                    if (i.Status != PersonStatus.None && !i.Alive && i.Available)
                    {
                        result.Add(i);
                    }
                }
                return result;
            }
        }

        public void AddPositionAreaInfluence(Troop troop, Point position, AreaInfluenceKind kind, int offset, float rate)
        {
            if (!this.PositionOutOfRange(position))
            {
                Troop troopByPositionNoCheck = this.GetTroopByPositionNoCheck(position);
                this.MapTileData[position.X, position.Y].AddAreaInfluence(troop, kind, offset, rate, troopByPositionNoCheck);
            }
        }

        public void AddPositionContactingTroop(Troop troop, Point position)
        {
            if (!this.PositionOutOfRange(position))
            {
                this.MapTileData[position.X, position.Y].AddContactingTroop(troop);
            }
        }

        public void AddPositionOffencingTroop(Troop troop, Point position)
        {
            if (!this.PositionOutOfRange(position))
            {
                this.MapTileData[position.X, position.Y].AddOffencingTroop(troop);
            }
        }

        public void AddPositionStratagemingTroop(Troop troop, Point position)
        {
            if (!this.PositionOutOfRange(position))
            {
                this.MapTileData[position.X, position.Y].AddStratagemingTroop(troop);
            }
        }

        public void AddPositionViewingTroopNoCheck(Troop troop, Point position)
        {
            this.MapTileData[position.X, position.Y].AddViewingTroop(troop);
        }
        /*
        private void AddPreparedAvailablePersons()
        {
            foreach (Person person in this.PreparedAvailablePersons)
            {
                Architecture gameObject = this.Architectures.GetGameObject(person.AvailableLocation) as Architecture;
                person.Available = true;
                foreach (Treasure treasure in person.Treasures)
                {
                    treasure.Available = true;
                }
                if (person.Father > 0)
                {
                    foreach (Person p in this.Persons)
                    {
                        if (p.ID == person.Father)
                        {
                            if (p.Available && p.Alive && p.LocationArchitecture != null && p.BelongedFaction != null&&p.BelongedCaptive==null )
                            {
                                p.LocationArchitecture.AddPerson(person);
                                p.BelongedFaction.AddPerson(person);
                                Session.MainGame.mainGameScreen.xianshishijiantupian(p.BelongedFaction.Leader,(this.Persons.GetGameObject(person.Father) as Person).Name,"ChildJoin","","",person.Name ,false);
                                Session.MainGame.mainGameScreen.xianshishijiantupian(person, p.LocationArchitecture.Name, "ChildJoinSelfTalk", "", "",  false);

                            }
                        }
                    }
                }
                else if (person.Mother > 0)
                {
                    foreach (Person p in this.Persons)
                    {
                        if (p.ID == person.Mother)
                        {
                            if (p.Available && p.Alive && p.LocationArchitecture != null && p.BelongedFaction != null && p.BelongedCaptive == null)
                            {
                                p.LocationArchitecture.AddPerson(person);
                                p.BelongedFaction.AddPerson(person);
                                Session.MainGame.mainGameScreen.xianshishijiantupian(p.BelongedFaction.Leader, (this.Persons.GetGameObject(person.Mother) as Person).Name, "ChildJoin", "", "", person.Name, false);
                                Session.MainGame.mainGameScreen.xianshishijiantupian(person, p.LocationArchitecture.Name, "ChildJoinSelfTalk", "", "", false);

                            }
                        }
                    }
                }
                else if (person.Spouse > 0 )
                {
                    foreach (Person p in this.Persons)
                    {
                        if (p.ID == person.Spouse)
                        {
                            if (p.Alive && p.LocationArchitecture != null && p.BelongedFaction != null && p.BelongedCaptive == null)
                            {
                                p.LocationArchitecture.AddPerson(person);
                                p.BelongedFaction.AddPerson(person);
                                if (person.Sex) //女的
                                {
                                    Session.MainGame.mainGameScreen.xianshishijiantupian(person, (this.Persons.GetGameObject(person.Spouse) as Person).Name, "FemaleSpouseJoin", "", "", false);
                                }
                                else
                                {
                                    Session.MainGame.mainGameScreen.xianshishijiantupian(person, (this.Persons.GetGameObject(person.Spouse) as Person).Name, "MaleSpouseJoin", "", "", false);

                                }

                            }
                        }
                    }
                }
                else
                {
                    gameObject.AddNoFactionPerson(person);
                }
                this.AvailablePersons.Add(person);
            }
            this.PreparedAvailablePersons.Clear();
        }
        */

        private void AddPreparedAvailablePersons()
        {
            foreach (Person person in this.PreparedAvailablePersons)
            {
                person.Available = true;
                foreach (Treasure treasure in person.Treasures)
                {
                    treasure.Available = true;
                }

                List<GameObject> candidates = new List<GameObject>();
                candidates.Add(person.Spouse);
                candidates.AddRange(person.Brothers.GameObjects);
                candidates.Add(person.Father);
                candidates.Add(person.Mother);
                candidates.AddRange(person.Siblings.GameObjects);
                candidates.Add(person.Spouse?.Father);
                candidates.Add(person.Spouse?.Mother);

                Person joinToPerson = null;
                foreach (Person q in candidates)
                {
                    if (q != null && q.Available && q.Alive && q.BelongedCaptive == null)
                    {
                        joinToPerson = q;
                        break;
                    }
                }
                
                if (joinToPerson != null)
                {
                    person.LocationArchitecture = joinToPerson.BelongedArchitecture;
                    person.Status = joinToPerson.Status;
                    if (person.Status == PersonStatus.Moving || person.Status == PersonStatus.NoFactionMoving)
                    {
                        person.Status = PersonStatus.Normal;
                    }
                    else if (person.Status == PersonStatus.Princess)
                    {
                        person.Status = PersonStatus.Normal;
                    }
                    person.YearJoin = this.Date.Year;

                    if (joinToPerson.BelongedFactionWithPrincess != null)
                    {
                        if (person.Father == joinToPerson || person.Mother == joinToPerson)
                        {
                            Session.MainGame.mainGameScreen.xianshishijiantupian(joinToPerson.BelongedFactionWithPrincess.Leader, joinToPerson.Name, TextMessageKind.ChildJoin, "ChildJoin", "", "", person.Name, false);
                            if (person.LocationArchitecture != null)
                            {
                                Session.MainGame.mainGameScreen.xianshishijiantupian(person, person.LocationArchitecture.Name, TextMessageKind.ChildJoinSelfTalk, "ChildJoinSelfTalk", "", "", false);
                            }
                        }
                        else
                        {
                            Faction f = joinToPerson.BelongedFactionWithPrincess;
                            Session.MainGame.mainGameScreen.xianshishijiantupian(person, person.LocationArchitecture.Name, TextMessageKind.PersonJoin, "PersonJoin", "", "", f.Name, false);
                        }
                    }
                    this.AvailablePersons.Add(person);
                    if (joinToPerson.BelongedFactionWithPrincess != null) { 
                        Session.MainGame.mainGameScreen.haizizhangdachengren(joinToPerson, person, false);
                    }
                    this.YearTable.addGrownBecomeAvailableEntry(this.Date, person);
                    continue;
                }

                bool joined = false;
                foreach (int id in person.JoinFactionID)
                {
                    Faction f = (Faction)this.Factions.GetGameObject(id);
                    if (f != null)
                    {
                        this.AvailablePersons.Add(person);
                        person.LocationArchitecture = f.Capital;
                        person.Status = PersonStatus.Normal;
                        person.YearJoin = this.Date.Year;
                        Session.MainGame.mainGameScreen.xianshishijiantupian(person, f.Capital.Name, TextMessageKind.PersonJoin, "PersonJoin", "", "", f.Name, false);
                        this.YearTable.addGrownBecomeAvailableEntry(this.Date, person);
                        Session.MainGame.mainGameScreen.haizizhangdachengren(joinToPerson, person, false);
                        joined = true;
                        break;
                    }
                }
                if (joined) continue;

                person.LocationArchitecture = this.Architectures.GetGameObject(person.AvailableLocation) as Architecture;
                person.Status = PersonStatus.NoFaction;
            }
            this.PreparedAvailablePersons.Clear();
        }

        public void haizichusheng(Person person, Person father, Person muqin, bool doAffect)
        {
            person.Available = true;
            foreach (Treasure treasure in person.Treasures)
            {
                treasure.Available = true;
            }

            person.LocationArchitecture = muqin.BelongedArchitecture;
            person.ChangeFaction(muqin.BelongedFaction);

            if (muqin.IsCaptive)
            {
                Captive.Create(person, muqin.BelongedArchitecture == null ? null : muqin.BelongedArchitecture.BelongedFaction);
            }

            ExtensionInterface.call("ChildrenJoinFaction", new Object[] { this, person });

            Session.MainGame.mainGameScreen.haizizhangdachengren(person, person, true);
        }

        public void ApplyFireTable()
        {
            foreach (Point point in this.FireTable.Positions)
            {
                this.GeneratorOfTileAnimation.AddTileAnimation(TileAnimationKind.火焰, point, true);
            }
        }

        public void ApplyTroopEvents()
        {
            if (this.TroopEventsToApply.Count != 0)
            {
                foreach (TroopEvent event2 in this.TroopEvents)
                {
                    TroopList list = null;
                    if (this.TroopEventsToApply.TryGetValue(event2, out list))
                    {
                        foreach (Troop troop in list.GetList())
                        {
                            event2.ApplyEventEffects(troop);
                        }
                    }
                }
                this.TroopEventsToApply.Clear();
            }
        }

        public void ApplyYesEvents()
        {
            foreach (KeyValuePair<Event, Architecture> i in this.YesEventsToApply)
            {
                i.Key.DoYesApplyEvent(i.Value);
                i.Key.happened = true;
            }
            this.YesEventsToApply.Clear();
            this.NoEventsToApply.Clear();
        }

        public void ApplyNoEvents()
        {
            foreach (KeyValuePair<Event, Architecture> i in this.NoEventsToApply)
            {
                i.Key.DoNoApplyEvent(i.Value);
                i.Key.happened = true;
            }
            this.YesEventsToApply.Clear();
            this.NoEventsToApply.Clear();
            /*
            foreach (KeyValuePair<Event, Architecture> i in this.NoArchiEventsToApply)
            {
                i.Key.DoNoApplyEvent(i.Value);
                i.Key.happened = true;
            }
            this.NoArchiEventsToApply.Clear();
             */
        }
        /*
        public void ApplyYesArchiEvents()
        {
            foreach (KeyValuePair<Event, Architecture> i in this.YesArchiEventsToApply)
            {
                i.Key.DoYesArchiApplyEvent(i.Value);
                i.Key.happened = true;
            }
            this.YesArchiEventsToApply.Clear();
        }

        public void ApplyNoArchiEvents()
        {
            foreach (KeyValuePair<Event, Architecture> i in this.NoArchiEventsToApply)
            {
                i.Key.DoNoArchiApplyEvent(i.Value);
                i.Key.happened = true;
            }
            this.NoArchiEventsToApply.Clear();
        }*/

        public void ApplyEvents()
        {
            Dictionary<Event, Architecture> events = this.EventsToApply;
            foreach (KeyValuePair<Event, Architecture> i in events)
            {
                i.Key.DoApplyEvent(i.Value);
                i.Key.happened = true;
            }

            this.EventsToApply.Clear();
        }

        public void ChangeDiplomaticRelation(int faction1, int faction2, int offset)
        {
            if (faction1 != faction2)
            {
                DiplomaticRelation diplomaticRelation = this.DiplomaticRelations.GetDiplomaticRelation(faction1, faction2);
                if (diplomaticRelation != null)
                {
                    diplomaticRelation.Relation += offset;
                }
            }
        }

        public void SetDiplomaticRelationIfHigher(int faction1, int faction2, int value)
        {
            if (faction1 != faction2)
            {
                DiplomaticRelation diplomaticRelation = this.DiplomaticRelations.GetDiplomaticRelation(faction1, faction2);
                if (diplomaticRelation != null)
                {
                    if (diplomaticRelation.Relation > value)
                    {
                        diplomaticRelation.Relation = value;
                    }
                }
            }
        }

        public void SetDiplomaticRelationTruce(int faction1, int faction2, int value)
        {
            if (faction1 != faction2)
            {
                DiplomaticRelation diplomaticRelation = this.DiplomaticRelations.GetDiplomaticRelation(faction1, faction2);
                if (diplomaticRelation != null)
                {
                    diplomaticRelation.Truce = value;
                }
            }
        }

        private void CheckGameEnd()
        {
            FactionList noArchFaction = new FactionList();
            foreach (Faction f in this.Factions)
            {
                if (f.ArchitectureCount == 0)
                {
                    noArchFaction.Add(f);
                }
            }

            foreach (Faction f in noArchFaction)
            {
                this.Factions.Remove(f);
            }

            if (this.Factions.Count == 1)
            {
                ExtensionInterface.call("GameEnd", new Object[] { this });
                if (this.CurrentPlayer != null && !this.runScenarioEnd(this.CurrentPlayer.Capital, Session.MainGame.mainGameScreen))
                {
                    Session.MainGame.mainGameScreen.GameEndWithUnite(this.Factions[0] as Faction);
                }
            }
        }

        public void Clear()
        {
            this.AllEvents.Clear();
            this.TroopEvents.Clear();
            this.Persons.Clear();
            this.AvailablePersons.Clear();
            this.PreparedAvailablePersons.Clear();
            this.Captives.Clear();
            this.Facilities.Clear();
            this.Militaries.Clear();
            this.Treasures.Clear();
            this.Informations.Clear();
            //this.SpyMessages.Clear();
            this.Routeways.Clear();
            GameObjectList t1 = this.Troops.GetList();
            foreach (Troop t in t1)
            {
                t.Destroy(true, false);
            }
            this.Troops.Clear();
            this.Legions.Clear();
            this.Architectures.Clear();
            this.Sections.Clear();
            this.Factions.Clear();
            this.Regions.Clear();
            this.States.Clear();
            this.ScenarioMap.Clear();
            this.PlayerFactions.Clear();
            this.FireTable.Clear();
            this.NoFoodDictionary.Clear();
            this.DiplomaticRelations.Clear();
            this.GeneratorOfTileAnimation.Clear();
            this.YearTable.Clear();

            //this.GameCommonData.Clear();

            this.CurrentFaction = null;
            this.CurrentPlayer = null;
        }

        public void ClearPenalizedMapDataByArea(GameArea gameArea)
        {
            foreach (Point point in gameArea.Area)
            {
                if (!this.PositionOutOfRange(point))
                {
                    this.PenalizedMapData[point.X, point.Y] = 0;
                }
            }
        }

        public void ClearPenalizedMapDataByPosition(Point position)
        {
            this.PenalizedMapData[position.X, position.Y] = 0;
        }

        public void ClearPositionFire(Point position)
        {
            this.FireTable.RemovePosition(position);
            this.GeneratorOfTileAnimation.RemoveTileAnimation(TileAnimationKind.火焰, position, true);
        }

        public void CreateNewFaction(Person leader)
        {
            if (leader.Status != PersonStatus.Normal && leader.Status != PersonStatus.NoFaction) return;

            Faction newFaction = new Faction();
            newFaction.Init();
            newFaction.ID = this.Factions.GetFreeGameObjectID();
            this.Factions.AddFactionWithEvent(newFaction);
            foreach (Faction faction2 in this.Factions)
            {
                if (faction2 != newFaction)
                {
                    this.DiplomaticRelations.AddDiplomaticRelation(newFaction.ID, faction2.ID, 0);
                }
            }
            newFaction.Leader = leader;
            newFaction.Reputation = leader.Reputation;
            newFaction.Name = leader.Name;
            if (leader.PersonBiography != null)
            {
                foreach (MilitaryKind kind in leader.PersonBiography.MilitaryKinds.MilitaryKinds.Values)
                {
                    newFaction.BaseMilitaryKinds.AddMilitaryKind(kind);
                }
                newFaction.ColorIndex = leader.PersonBiography.FactionColor;
            }
            else
            {
                newFaction.BaseMilitaryKinds.AddBasicMilitaryKinds();
                newFaction.ColorIndex = -1;
            }

            List<int> allUnusedColors = new List<int>();
            for (int i = 0; i < this.GameCommonData.AllColors.Count; ++i)
            {
                allUnusedColors.Add(i);
            }
            foreach (Faction f in this.Factions)
            {
                allUnusedColors.Remove(f.ColorIndex);
            }
            if (allUnusedColors.Count == 0)
            {
                newFaction.ColorIndex = GameObject.Random(this.GameCommonData.AllColors.Count);
            }
            else
            {
                if (!allUnusedColors.Contains(newFaction.ColorIndex))
                {
                    newFaction.ColorIndex = allUnusedColors[GameObject.Random(allUnusedColors.Count)];
                }
            }

            newFaction.FactionColor = this.GameCommonData.AllColors[newFaction.ColorIndex];

            Architecture newFactionCapital = leader.LocationArchitecture;
            Faction oldFaction = newFactionCapital.BelongedFaction;

            if (oldFaction != null)
            {
                foreach (Technique tech in oldFaction.AvailableTechniques.GetTechniqueList())
                {
                    newFaction.AvailableTechniques.AddTechnique(tech);
                }

                if (oldFaction.IsAlien && leader.PersonalLoyalty < 2)
                {
                    newFaction.IsAlien = true;
                }
            }

            newFaction.Capital = newFactionCapital;

            if (leader.BelongedFaction == null)
            {
                leader.Status = PersonStatus.Normal;
            }
            else
            {
                this.ChangeDiplomaticRelation(newFaction.ID, newFactionCapital.BelongedFaction.ID, -500);
            }
            newFaction.PrepareData();

            newFactionCapital.ResetFaction(newFaction);

            newFaction.AddArchitectureKnownData(newFactionCapital);
            newFaction.FirstSection.AddArchitecture(newFactionCapital);

            leader.MoveToArchitecture(newFactionCapital);

            foreach (Point p in newFactionCapital.ArchitectureArea.Area)
            {
                Troop t = GetTroopByPositionNoCheck(p);
                if (t != null)
                {
                    t.Morale = -100;
                    Troop.CheckTroopRout(t);
                }
            }

            if (oldFaction != null && !GameObject.Chance((int)oldFaction.Leader.PersonalLoyalty * 10))
            {
                oldFaction.Leader.AddHated(leader, -2000);
                leader.AdjustRelation(oldFaction.Leader, -20f, -10);
            }

            if (oldFaction != null)
            {
                int oldFactionLoyalty = oldFaction.Leader.PersonalLoyalty;
                leader.DecreaseKarma(Math.Max(12, 12 + 5 * oldFactionLoyalty + oldFaction.Leader.Karma / 2));
            }

            foreach (Person p in this.AvailablePersons)
            {
                if ((p.BelongedFaction == null || p.BelongedFaction == oldFaction) && !p.IsCaptive && p.Status != PersonStatus.Princess && p != leader)
                {
                    int offset = Person.GetIdealOffset(leader, p);
                    if (p.HasCloseStrainTo(leader) || p.IsVeryCloseTo(leader) || (GameObject.Chance(100 - offset * 20) && p.BelongedFaction == oldFaction))
                    {
                        if (p.BelongedFaction == null || p.IsVeryCloseTo(leader) || (GameObject.Chance(100 - ((int)p.PersonalLoyalty) * 25 + (5 - offset) * 10)
                            && GameObject.Chance(220 - p.Loyalty * 2 + (5 - offset) * 20)))
                        {
                            if (p.BelongedFaction != null)
                            {
                                p.BelongedFaction.Leader.AdjustRelation(p, -15f - p.PersonalLoyalty * 1.5f, -8);
                                p.BelongedFaction.Leader.AdjustRelation(newFaction.Leader, -5f, -2.5f);
                                p.AdjustRelation(p.BelongedFaction.Leader, -3f, -2);
                                p.ChangeFaction(newFaction);
                                p.DecreaseKarma(5 - p.BelongedFaction.Leader.PersonalLoyalty - Math.Min(0, p.BelongedFaction.Leader.Karma / 2));
                            }
                            newFaction.Leader.AdjustRelation(p, 10f, 3);
                            p.AdjustRelation(newFaction.Leader, 3f, 1);
                            if (p.LocationTroop == null)
                            {
                                p.MoveToArchitecture(newFactionCapital);
                            }
                            else
                            {
                                p.LocationTroop.ChangeFaction(newFaction);
                            }
                        }
                    }
                }
            }
            ExtensionInterface.call("CreateNewFaction", new Object[] { this, oldFaction, newFaction, newFactionCapital });

            this.YearTable.addNewFactionEntry(this.Date, oldFaction, newFaction, newFactionCapital);
            if (this.OnNewFactionAppear != null)
            {
                this.OnNewFactionAppear(newFaction);
            }
        }

        public int PlayerArchitectureCount
        {
            get
            {
                int r = 0;
                foreach (Faction f in this.Factions)
                {
                    if (this.IsPlayer(f))
                    {
                        r += f.ArchitectureCount;
                    }
                }
                return r;
            }
        }
        /*
        private void OngoingBattleDayEvent()
        {
            List<OngoingBattle> toRemove = new List<OngoingBattle>();
            foreach (OngoingBattle ob in this.AllOngoingBattles)
            {
                ob.CalmDay++;
                if (ob.CalmDay >= 5)
                {
                    Dictionary<Faction, int> factionDamages = new Dictionary<Faction, int>();
                    List<Person> persons = new List<Person>();
                    foreach (Person p in this.Persons)
                    {
                        if (p.Battle == ob && p.BelongedFaction != null) 
                        {
                            persons.Add(p);
                            if (!factionDamages.ContainsKey(p.BelongedFaction))
                            {
                               factionDamages.Add(p.BelongedFaction, 0); 
                            }
                            factionDamages[p.BelongedFaction] += p.BattleSelfDamage;
                        }
                    }

                    ArchitectureList battleArch = ob.Architectures;

                    bool first = true;
                    foreach (Person p in persons)
                    {
                        this.YearTable.addBattleEntry(first, this.Date, ob, p, battleArch, factionDamages);
                        p.Battle = null;
                        first = false;
                    }

                    foreach (Architecture a in battleArch)
                    {
                        a.OldFactionName = a.BelongedFaction == null ? "贼军" : a.BelongedFaction.Name;
                        a.Battle = null;
                    }


                    toRemove.Add(ob);
                }
            }

            foreach (OngoingBattle i in toRemove)
            {
                this.AllOngoingBattles.Remove(i);
            }
        }
        */
        public void DayPassedEvent()
        {
            ExtensionInterface.call("DayEvent", new Object[] { this });

            JustSaved = false;

            //this.GameProgressCaution.Text = "开始";
            Session.Parameters.DayEvent(this.PlayerArchitectureCount);

            /*this.ClearPersonStatusCache();
            this.ClearPersonWorkCache();*/

            //clearupRepeatedOfficers();

            this.Troops.FinalizeQueue();
            this.Factions.BuildQueue(false);

            this.TrainChildren();
            this.Architectures.NoFactionDevelop();
            this.FireDayEvent();
            this.NoFoodPositionDayEvent();

            this.NewFaction();

            //this.GameProgressCaution.Text = "运行外交";
            foreach (DiplomaticRelationDisplay display in this.DiplomaticRelations.GetAllDiplomaticRelationDisplayList())
            {
                if (display.Truce > 0)
                {
                    display.Truce--;
                }
            }
            //this.GameProgressCaution.Text = "运行势力";
            //this.OngoingBattleDayEvent();

            foreach (Faction faction in this.Factions.GetRandomList())
            {
                faction.DayEvent();
            }
            foreach (Architecture architecture in this.Architectures.GetRandomList())
            {
                architecture.DayEvent();
            }
            foreach (Routeway routeway in this.Routeways.GetRandomList())
            {
                routeway.DayEvent();
            }
            foreach (Legion legion in this.Legions.GetRandomList())
            {
                legion.DayEvent();
                if (legion.Troops.Count == 0)
                {
                    legion.Disband();
                    this.Legions.Remove(legion);
                }
            }
            //this.GameProgressCaution.Text = "运行军队";
            foreach (Troop troop in this.Troops.GetRandomList())
            {
                if (troop.BelongedFaction == null)
                {
                    troop.DayEvent();
                }
            }

            this.detectCurrentPlayerBattleState(this.CurrentPlayer);

            this.militaryKindEvent();
            this.titleDayEvent();
            this.guanzhiDayEvent();


            //this.GameProgressCaution.Text = "运行人物";
            foreach (Person person in this.AvailablePersons.GetList())
            {
                person.PreDayEvent();
            }
            foreach (Person person in this.AvailablePersons.GetRandomList())
            {
                person.DayEvent();
            }
            this.AdjustGlobalPersonRelation();
            this.AddPreparedAvailablePersons();
            /*
            foreach (SpyMessage message in this.SpyMessages.GetRandomList())
            {
                message.DayEvent();
            }
             */
            foreach (Captive captive in this.Captives.GetRandomList())
            {
                captive.DayEvent();
            }
            this.CheckGameEnd();

            //this.DaySince++;
            this.DaySince += Session.Parameters.DayInTurn;

            ExtensionInterface.call("PostDayEvent", new Object[] { this });

            scenarioJustLoaded = false;
            Session.MainGame.mainGameScreen.LoadScenarioInInitialization = false;
            numberOfAmbushTroop = -1; // 缓存有几支部队在埋伏，绝大多数时候地图上根本没有埋伏部队，这时候不需要叫浪费时间的函数detectAmbushTroop

            Session.MainGame.mainGameScreen.DisposeMapTileMemory(false, false);
        }

        private void militaryKindEvent()
        {
            foreach (MilitaryKind m in this.GameCommonData.AllMilitaryKinds.MilitaryKinds.Values)
            {
                if (m.Persons.Count > 0 && m.ObtainProb > 0)
                {
                    foreach (Person p in m.Persons)
                    {
                        if (GameObject.Random(m.ObtainProb) == 0)
                        {
                            if (p.BelongedFaction != null && !p.BelongedFaction.BaseMilitaryKinds.MilitaryKinds.ContainsValue(m))
                            {
                                p.BelongedFaction.BaseMilitaryKinds.AddMilitaryKind(m);
                                Session.MainGame.mainGameScreen.xianshishijiantupian(p, m.Name, TextMessageKind.ObtainMilitaryKind, "ObtainMilitaryKind", "", "", false);
                            }
                        }
                    }
                }
            }
        }

        private void guanzhiDayEvent()
        {

            List<Title> ManualAwardTitles = new List<Title>();
            foreach (Title t in this.GameCommonData.AllTitles.Titles.Values)
            {
                if (t.ManualAward)
                {
                    ManualAwardTitles.Add(t);
                }
            }
            foreach (Title t in ManualAwardTitles)
            {
                if (t.AutoLearn > 0 && GameObject.Random(t.AutoLearn) == 0)
                {
                    PersonList candidates = new PersonList();
                    if (t.Persons.Count > 0)
                    {
                        foreach (Person p in t.Persons)
                        {
                            if (p.Available && p.Alive)
                            {
                                candidates.Add(p);
                            }
                        }
                    }
                    else
                    {
                        candidates = this.AvailablePersons;
                    }
                    foreach (Person p in candidates)
                    {
                        if ((!this.IsPlayer(p.BelongedFaction) || Session.GlobalVariables.PermitManualAwardTitleAutoLearn) && !p.HasHigherLevelTitle(t) && !t.ManualAward && t.CanLearn(p, true))
                        {
                            p.AwardTitle(t);
                        }
                    }
                }
            }
        }


        private static Person courier = null;
        private void titleDayEvent()
        {
            if (courier == null)
            {
                courier = (Person)this.Persons.GetGameObject(7200);
            }
            foreach (Title t in this.GameCommonData.AllTitles.Titles.Values)
            {
                if (t.AutoLearn > 0 && GameObject.Random(t.AutoLearn) == 0)
                {
                    PersonList candidates = new PersonList();
                    if (t.Persons.Count > 0)
                    {
                        foreach (Person p in t.Persons)
                        {
                            if (p.Available && p.Alive)
                            {
                                candidates.Add(p);
                            }
                        }
                    }
                    else
                    {
                        candidates = this.AvailablePersons;
                    }
                    foreach (Person p in candidates)
                    {
                        if (!p.HasHigherLevelTitle(t) && t.CanLearn(p, true) && !t.ManualAward)
                        {
                            p.LearnTitle(t);
                            Session.MainGame.mainGameScreen.AutoLearnTitle(p, courier, t);
                        }
                        else if (p.HasTitle() && t.WillLose(p))
                        {
                            p.LoseTitle();
                        }
                    }
                }
            }
        }

        private void detectCurrentPlayerBattleState(Faction faction, bool init = false)
        {

            if (faction == null) return;
            //defend
            ZhandouZhuangtai originalBattleState = faction.BattleState;
            bool fangshou = false;
            int fightingArchitectureCount = 0;
            foreach (Architecture architecture in faction.Architectures)
            {
                if (architecture.BelongedFaction == null) continue;

                if (architecture.BelongedSection == null || architecture.BelongedSection.AIDetail.AutoRun) continue;

                if (architecture.FindHostileTroopInView())
                {
                    fightingArchitectureCount++;

                    if (!architecture.hostileTroopInViewLastDay)  //如果已经提醒过就不再提醒
                    {
                        //architecture.JustAttacked = true;
                        architecture.BelongedFaction.StopToControl = Setting.Current.GlobalVariables.StopToControlOnAttack;
                        architecture.RecentlyAttacked = 5;
                        Session.MainGame.mainGameScreen.ArchitectureBeginRecentlyAttacked(architecture);  //提示玩家建筑视野范围内出现敌军。

                    }
                    architecture.hostileTroopInViewLastDay = true;

                }
                else
                {
                    architecture.hostileTroopInViewLastDay = false;
                }

            }
            if (fightingArchitectureCount == 0)
            {
                fangshou = false;
            }
            else
            {
                fangshou = true;
            }
            //attack
            bool jingong = false;

            foreach (Troop t in faction.Troops)
            {
                if (t.HasHostileArchitectureInView())         //||t.HasHostileTroopInView())
                {
                    jingong = true;
                    break;
                }
            }

            if (!jingong && !fangshou)
            {
                faction.BattleState = ZhandouZhuangtai.和平;
            }
            else if (jingong && !fangshou)
            {
                faction.BattleState = ZhandouZhuangtai.进攻;

            }
            else if (!jingong && fangshou)
            {
                faction.BattleState = ZhandouZhuangtai.防守;

            }
            else
            {
                faction.BattleState = ZhandouZhuangtai.攻守兼备;
            }

            if (originalBattleState != faction.BattleState || init)
            {
                Session.MainGame.mainGameScreen.SwichMusic(this.Date.Season);
            }

        }

        public void DayStartingEvent()
        {
            this.Factions.SetControlling(false);
            
            foreach (Troop troop in this.Troops.GetList())
            {
                if (troop.BelongedFaction == null || troop.BelongedLegion == null || !troop.BelongedLegion.Troops.HasGameObject(troop))
                {
                    troop.AI();
                }
            }
            this.Troops.BuildQueue();
            foreach (Architecture architecture in this.Architectures.GetList())
            {
                architecture.HireFinished = false;
                architecture.HasManualHire = false;
                architecture.TodayPersonArriveNote = false;

            }
        }

        public void FireDayEvent()
        {
            List<Point> list = new List<Point>();
            foreach (Point point in this.FireTable.Positions)
            {
                if (GameObject.Chance(Session.Parameters.FireStayProb))
                {
                    list.Add(point);
                }
            }
            foreach (Point point in list)
            {
                this.ClearPositionFire(point);
            }
            list.Clear();
            foreach (Point point in this.FireTable.Positions)
            {
                list.Add(point);
            }
            foreach (Point point in list)
            {
                this.FireSpread(point);
            }
        }

        public void FireSpread(Point position)
        {
            GameArea area = GameArea.GetArea(position, 1, false);
            foreach (Point point in area.Area)
            {
                if ((point != position) && this.IsFireVaild(point, false, MilitaryType.步兵))
                {
                    if (this.PositionIsOnFire(point))
                    {
                        continue;
                    }
                    int chance = 0;
                    switch (this.GetTerrainKindByPosition(position))
                    {
                        case TerrainKind.平原:
                            chance = 3;
                            break;

                        case TerrainKind.草原:
                            chance = 4;
                            break;

                        case TerrainKind.森林:
                            chance = 10;
                            break;

                        case TerrainKind.山地:
                            chance = 6;
                            break;
                    }
                    if (GameObject.Chance((int)(chance * Session.Parameters.FireSpreadProbMultiply)))
                    {
                        this.SetPositionOnFire(point);
                        Troop troopByPosition = this.GetTroopByPosition(point);
                        if (troopByPosition != null)
                        {
                            troopByPosition.BurntBySpreadFire();
                        }
                    }
                }
            }
        }

        public RoutewayList GetActiveRoutewayListByPosition(Point position)
        {
            RoutewayList list = new RoutewayList();
            if (!this.PositionOutOfRange(position))
            {
                if (this.MapTileData[position.X, position.Y].TileRouteways == null)
                {
                    return list;
                }
                foreach (Routeway routeway in this.MapTileData[position.X, position.Y].TileRouteways)
                {
                    if (routeway.IsActive || routeway.IsPointActive(position))
                    {
                        list.Add(routeway);
                    }
                }
            }
            return list;
        }

        public Architecture GetArchitectureByPosition(Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return null;
            }
            return this.MapTileData[position.X, position.Y].TileArchitecture;
        }

        public Architecture GetArchitectureByPositionNoCheck(Point position)
        {
            return this.MapTileData[position.X, position.Y].TileArchitecture;
        }

        public GameArea GetAreaWithinDistance(Point centre, int distance, bool includingCentre)
        {
            GameArea area = new GameArea();
            for (int i = -distance; i <= distance; i++)
            {
                for (int j = -distance; j <= distance; j++)
                {
                    Point fromPosition = new Point(centre.X + i, centre.Y + j);
                    if ((includingCentre || !(fromPosition == centre)) && (this.GetDistance(fromPosition, centre) <= distance))
                    {
                        area.AddPoint(fromPosition);
                    }
                }
            }
            return area;
        }

        public Point GetClosestPoint(GameArea area, Point fromPosition)
        {
            int simpleDistance = 0, minSimpleDistance = int.MaxValue;
            double distance = 0, minDistance = double.MaxValue;
            Point point = new Point();
            foreach (Point point2 in area.Area)
            {
                simpleDistance = this.GetSimpleDistance(fromPosition, point2);
                if (simpleDistance <= minSimpleDistance)
                {
                    distance = this.GetDistance(fromPosition, point2);
                    if (distance < minDistance)
                    {
                        minSimpleDistance = simpleDistance;
                        minDistance = distance;
                        point = point2;
                    }
                }
            }
            return point;
        }

        public void GetClosestPointsBetweenTwoAreas(GameArea area1, GameArea area2, out Point? out1, out Point? out2)
        {
            out1 = null;
            out2 = null;
            int simpleDistance = 0, minSimpleDistance = int.MaxValue;
            double distance = 0, minDistance = double.MaxValue;
            foreach (Point point in area1.Area)
            {
                foreach (Point point2 in area2.Area)
                {
                    simpleDistance = this.GetSimpleDistance(point, point2);
                    if (simpleDistance <= minSimpleDistance)
                    {
                        distance = this.GetDistance(point, point2);
                        if (distance < minDistance)
                        {
                            minSimpleDistance = simpleDistance;
                            minDistance = distance;
                            out1 = new Point?(point);
                            out2 = new Point?(point2);
                        }
                    }
                }
            }
        }

        public Point? GetClosestPosition(GameArea area, List<Point> orientations)
        {
            Point? nullable = null;
            int num = 0x7fffffff;
            foreach (Point point in area.Area)
            {
                int num2 = 0;
                foreach (Point point2 in orientations)
                {
                    num2 += this.GetSimpleDistance(point, point2);
                }
                if (num2 < num)
                {
                    num = num2;
                    nullable = new Point?(point);
                }
            }
            return nullable;
        }

        public string GetCoordinateString(Point position)
        {
            return (position.X + "," + position.Y);
        }

        public int GetDiplomaticRelation(int faction1, int faction2)
        {
            if (faction1 != faction2)
            {
                DiplomaticRelation diplomaticRelation = this.DiplomaticRelations.GetDiplomaticRelation(faction1, faction2);
                if (diplomaticRelation != null)
                {
                    return diplomaticRelation.Relation;
                }
            }
            return 0;
        }

        public int GetDiplomaticRelationTruce(int faction1, int faction2)
        {
            if (faction1 != faction2)
            {
                DiplomaticRelation diplomaticRelation = this.DiplomaticRelations.GetDiplomaticRelation(faction1, faction2);
                if (diplomaticRelation != null)
                {
                    return diplomaticRelation.Truce;
                }
            }
            return 0;
        }

        public double GetResourceConsumptionRate(Architecture a, Troop b)
        {
            return this.GetDistance(b.Position, a.ArchitectureArea) / 50.0 + 1;
        }

        public double GetResourceConsumptionRate(Architecture a, Architecture b)
        {
            return this.GetDistance(a.ArchitectureArea, b.ArchitectureArea) / 150.0 + 1;
        }

        public double GetDistance(GameArea fromArea, GameArea toArea)
        {
            // 上面这段浪费太多时间O(n^2)，下面仅需要O(1)，一个非常近似的值已经足够
            double distance = GetDistance(fromArea.Centre, toArea.Centre);

            if (distance < 0) return 0;

            distance -= (1 + Math.Sqrt(2 * fromArea.Count + 1)) / 2;
            distance -= (1 + Math.Sqrt(2 * toArea.Count + 1)) / 2;

            return distance;
        }

        public double GetDistance(Point fromPosition, GameArea toArea)
        {
            // O(1) instead of O(n)
            double distance = GetDistance(fromPosition, toArea.Centre);

            distance -= (1 + Math.Sqrt(2 * toArea.Count + 1)) / 2;

            return distance;
        }

        public double GetDistance(Point fromPosition, Point toPosition)
        {
            return Math.Sqrt(Math.Pow(toPosition.X - fromPosition.X, 2) + Math.Pow(toPosition.Y - fromPosition.Y, 2));
        }

        public Point? GetFarthestPosition(GameArea area, List<Point> orientations)
        {
            Point? nullable = null;
            int num = -2147483648;
            foreach (Point point in area.Area)
            {
                int num2 = 0;
                foreach (Point point2 in orientations)
                {
                    num2 += this.GetSimpleDistance(point, point2);
                }
                if (num2 > num)
                {
                    num = num2;
                    nullable = new Point?(point);
                }
            }
            return nullable;
        } 

        public ArchitectureList GetHighViewingArchitecturesByPosition(Point position)
        {
            ArchitectureList list = new ArchitectureList();
            if (!this.PositionOutOfRange(position))
            {
                if (this.MapTileData[position.X, position.Y].HighViewingArchitectures == null)
                {
                    return list;
                }
                foreach (Architecture architecture in this.MapTileData[position.X, position.Y].HighViewingArchitectures)
                {
                    list.Add(architecture);
                }
            }
            return list;
        }

        public string GetPlayerInfo()
        {
            if (this.CurrentPlayer != null)
            {
                if (this.PlayerFactions.Count > 1)
                {
                    return (this.CurrentPlayer.Name + " 等");
                }
                if (this.PlayerFactions.Count == 1)
                {
                    return this.CurrentPlayer.Name;
                }
                return "电脑";
            }
            return "电脑";
        }

        //public Texture2D GetPortrait(float id)
        //{
        //    return Session.MainGame.mainGameScreen.GetPortrait(id);
        //}

        public int GetPositionHostileOffencingDiscredit(Troop troop, Point position)
        {
            return this.MapTileData[position.X, position.Y].GetPositionHostileOffencingDiscredit(troop);
        }

        public int GetPositionMapCost(Faction faction, Point position)
        {
            Architecture architectureByPositionNoCheck = this.GetArchitectureByPositionNoCheck(position);
            if (architectureByPositionNoCheck != null)
            {
                if ((architectureByPositionNoCheck.Endurance > 0) && (architectureByPositionNoCheck.BelongedFaction != faction))
                {
                    return 0xdac;
                }
                return 5;
            }
            Troop troopByPositionNoCheck = this.GetTroopByPositionNoCheck(position);
            if (troopByPositionNoCheck != null)
            {
                if (!((faction != null) && faction.IsFriendly(troopByPositionNoCheck.BelongedFaction)))
                {
                    return 0xdac;
                }
                return 0;
            }
            if (this.PositionIsOnFire(position))
            {
                return 10;
            }
            return 0;
        }

        public Point GetProperDestination(Point from, Point to)
        {
            double distance = this.GetDistance(from, to);
            if (distance > 15.0)
            {
                return new Point(from.X + ((int)(((double)((to.X - from.X) * 15)) / distance)), from.Y + ((int)(((double)((to.Y - from.Y) * 15)) / distance)));
            }
            return to;
        }

        public int GetReturnDays(Point destination, GameArea fromArea)
        {
            int num = (int)Math.Ceiling((double)(this.GetDistance(destination, this.GetClosestPoint(fromArea, destination)) / 10.0));
            num *= 2;
            if (num == 0)
            {
                num = 1;
            }
            return num;
        }

        public ArchitectureList GetRoutewayArchitecturesByPosition(Routeway routeway, Point position)
        {
            ArchitectureList list = new ArchitectureList();
            if (!this.PositionOutOfRange(position))
            {
                foreach (Architecture architecture in routeway.BelongedFaction.Architectures)
                {
                    if ((architecture != routeway.StartArchitecture) && architecture.GetRoutewayStartArea().HasPoint(position))
                    {
                        list.Add(architecture);
                    }
                }
            }
            return list;
        }

        public Routeway GetRoutewayByPosition(Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return null;
            }
            if (this.MapTileData[position.X, position.Y].TileRouteways == null)
            {
                return null;
            }
            if (this.MapTileData[position.X, position.Y].TileRouteways.Count == 0)
            {
                return null;
            }
            return this.MapTileData[position.X, position.Y].TileRouteways[0];
        }

        public Routeway GetRoutewayByPositionAndFaction(Point position, Faction faction)
        {
            if (!this.PositionOutOfRange(position))
            {
                if (this.MapTileData[position.X, position.Y].TileRouteways == null)
                {
                    return null;
                }
                foreach (Routeway routeway in this.MapTileData[position.X, position.Y].TileRouteways)
                {
                    if (((routeway.BelongedFaction == faction) && (routeway.StartArchitecture != null)) && ((((routeway.DestinationArchitecture == null) || !routeway.StartArchitecture.BelongedSection.AIDetail.AutoRun) || routeway.Building) || (routeway.LastActivePointIndex >= 0)))
                    {
                        return routeway;
                    }
                }
            }
            return null;
        }

        public List<Routeway> GetRoutewaysByPositionAndFaction(Point position, Faction faction)
        {
            List<Routeway> list = new List<Routeway>();
            if (!this.PositionOutOfRange(position))
            {
                if (this.MapTileData[position.X, position.Y].TileRouteways == null)
                {
                    return list;
                }
                foreach (Routeway routeway in this.MapTileData[position.X, position.Y].TileRouteways)
                {
                    if (routeway.BelongedFaction == faction)
                    {
                        list.Add(routeway);
                    }
                }
            }
            return list;
        }

        public int GetSimpleDistance(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }

        public int GetSingleWayDays(Point destination, GameArea fromArea)
        {
            int num = (int)Math.Ceiling((double)(this.GetDistance(destination, this.GetClosestPoint(fromArea, destination)) / 10.0));
            if (num == 0)
            {
                num = 1;
            }
            return num;
        }

        //public Texture2D GetSmallPortrait(float id)
        //{
        //    return Session.MainGame.mainGameScreen.GetSmallPortrait(id);
        //}

        //public Texture2D GetTroopPortrait(float id)
        //{
        //    return Session.MainGame.mainGameScreen.GetTroopPortrait(id);
        //}
        //public Texture2D GetFullPortrait(float id)
        //{
        //    return Session.MainGame.mainGameScreen.GetFullPortrait(id);
        //}

        public ArchitectureList GetSupplyArchitecturesByPositionAndFaction(Point position, Faction faction)
        {
            ArchitectureList list = new ArchitectureList();
            if (!this.PositionOutOfRange(position))
            {
                if (this.MapTileData[position.X, position.Y].SupplyingArchitectures == null)
                {
                    return list;
                }
                foreach (Architecture architecture in this.MapTileData[position.X, position.Y].SupplyingArchitectures)
                {
                    //if (faction.IsFriendly(architecture.BelongedFaction))
                    if (faction == architecture.BelongedFaction)
                    {
                        list.Add(architecture);
                    }
                }
            }
            return list;
        }

        public List<RoutePoint> GetSupplyRoutePointsByPositionAndFaction(Point position, Faction faction)
        {
            List<RoutePoint> list = new List<RoutePoint>();
            if (!this.PositionOutOfRange(position))
            {
                if (this.MapTileData[position.X, position.Y].SupplyingRoutePoints == null)
                {
                    return list;
                }
                foreach (RoutePoint point in this.MapTileData[position.X, position.Y].SupplyingRoutePoints)
                {
                    if (point.BelongedRouteway.IsSupporting(faction))
                    {
                        list.Add(point);
                    }
                }
            }
            return list;
        }

        public TerrainDetail GetTerrainDetailByPosition(Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return null;
            }
            return this.GameCommonData.AllTerrainDetails.GetTerrainDetail(ScenarioMap.MapData[position.X, position.Y]);
        }

        public TerrainDetail GetTerrainDetailByPositionNoCheck(Point position)
        {
            return this.GameCommonData.AllTerrainDetails.GetTerrainDetail(ScenarioMap.MapData[position.X, position.Y]);
        }

        public TerrainKind GetTerrainKindByPosition(Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return TerrainKind.无;
            }
            return (TerrainKind)ScenarioMap.MapData[position.X, position.Y];
        }

        public TerrainKind GetTerrainKindByPositionNoCheck(Point position)
        {
            return (TerrainKind)ScenarioMap.MapData[position.X, position.Y];
        }

        public string GetTerrainNameByPosition(Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return "----";
            }
            return this.GameCommonData.AllTerrainDetails.GetTerrainDetail(ScenarioMap.MapData[position.X, position.Y]).Name;
        }

        public int GetTransferFundDays(Architecture from, Architecture to)
        {
            //return (int)Math.Ceiling(this.GetDistance(from.ArchitectureArea, to.ArchitectureArea) / 2.5);
            return (int)Math.Ceiling(this.GetDistance(from.ArchitectureArea, to.ArchitectureArea) / 2.5);
        }


        public Troop GetTroopByPosition(Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return null;
            }
            return this.MapTileData[position.X, position.Y].TileTroop;
        }

        public Troop GetTroopByPositionNoCheck(Point position)
        {
            return this.MapTileData[position.X, position.Y].TileTroop;
        }

        public ArchitectureList GetViewingArchitecturesByPosition(Point position)
        {
            ArchitectureList list = new ArchitectureList();
            if (!this.PositionOutOfRange(position))
            {
                if (this.MapTileData[position.X, position.Y].ViewingArchitectures == null)
                {
                    return list;
                }
                foreach (Architecture architecture in this.MapTileData[position.X, position.Y].ViewingArchitectures)
                {
                    list.Add(architecture);
                }
            }
            return list;
        }

        public int GetWaterPositionMapCost(MilitaryKind kind, Point position)
        {
            if (ScenarioMap.MapData[position.X, position.Y] == 6)
            {
                if (Session.GlobalVariables.LandArmyCanGoDownWater)
                {
                    return 0;
                }

                if (this.GetArchitectureByPositionNoCheck(position) != null)
                {
                    return 0;
                }
                if (kind.Type == MilitaryType.水军)
                {
                    return 0;
                }
                int num = 0;
                Point point = new Point(position.X - 1, position.Y);
                if (!(this.PositionOutOfRange(point) || (ScenarioMap.MapData[point.X, point.Y] != 6)))
                {
                    num++;
                }
                Point point2 = new Point(position.X, position.Y - 1);
                if (!(this.PositionOutOfRange(point2) || (ScenarioMap.MapData[point2.X, point2.Y] != 6)))
                {
                    num++;
                }
                Point point3 = new Point(position.X + 1, position.Y);
                if (!(this.PositionOutOfRange(point3) || (ScenarioMap.MapData[point3.X, point3.Y] != 6)))
                {
                    num++;
                }
                if (num > 2)
                {
                    return 0xdac;
                }
                Point point4 = new Point(position.X, position.Y + 1);
                if (!(this.PositionOutOfRange(point4) || (ScenarioMap.MapData[point4.X, point4.Y] != 6)))
                {
                    num++;
                }
                if (num > 2)
                {
                    return 0xdac;
                }
            }
            else
            {
                if (kind.Type != MilitaryType.水军 || kind.IsShell || kind.IsTransport)
                {
                    return 0;
                }

                Architecture a = this.GetArchitectureByPositionNoCheck(position);
                if (a != null && !a.Kind.ShipCanEnter)
                {
                    return 0xdac;
                }
            }
            return 0;
        }

        private bool HasSameIdealFaction(Person person)
        {
            if ((person.BelongedFaction != null) && (person.BelongedFaction.Leader == person))
            {
                return true;
            }
            foreach (Faction faction in this.Factions)
            {
                if ((faction.Leader != null) && (faction.Leader.Ideal == person.Ideal))
                {
                    return true;
                }
            }
            return false;
        }

        public int HostileContactingTroopsCount(Faction faction, Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return 0;
            }
            return this.MapTileData[position.X, position.Y].HostileContactingTroopsCount(faction);
        }

        public int HostileOffencingTroopsCount(Faction faction, Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return 0;
            }
            return this.MapTileData[position.X, position.Y].HostileOffencingTroopsCount(faction);
        }

        public int HostileViewingTroopsCount(Faction faction, Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return 0;
            }
            return this.MapTileData[position.X, position.Y].HostileViewingTroopsCount(faction);
        }

        public void InitialGameData()
        {
            this.InitializeSectionData();
            this.InitializeRoutewayData();
            this.InitializeArchitectureData();
            this.InitializeMilitariesData();
            this.InitializeTroopData();
            this.InitializeCaptiveData();
            this.InitializePersonData();
            //this.InitializeSpyMessageData();

            foreach (Person p in this.Persons)
            {
                foreach (Title t in p.UniqueTitles.Titles.Values)
                {
                    t.Persons.Add(p);
                }
                foreach (MilitaryKind m in p.UniqueMilitaryKinds.MilitaryKinds.Values)
                {
                    m.Persons.Add(p);
                }
            }

            if (Session.GlobalVariables.RemoveSpouseIfNotAvailable)
            {
                foreach (Person p in Persons)
                {
                    if (!p.Available && p.Spouse != null && !p.Spouse.Available)
                    {
                        p.suoshurenwuList.Remove(p.Spouse);
                        p.Spouse = null;
                    }
                }
            }

            Session.Parameters.MigrateData();

            /*
            this.GameProgressCaution = new GameFreeText.FreeText(new System.Drawing.Font("宋体", 16f), new Color(1f, 1f, 1f));
            this.GameProgressCaution.Text = "——";
            this.GameProgressCaution.Align=TextAlign.Middle;
            */
        }

        public void InitializeArchitectureMapTile()
        {
            foreach (Architecture architecture in this.Architectures)
            {
                foreach (Point point in architecture.ArchitectureArea.Area)
                {
                    this.MapTileData[point.X, point.Y].TileArchitecture = architecture;
                }
            }
            foreach (Architecture architecture in this.Architectures)
            {
                this.SetMapTileArchitecture(architecture);
            }
        }

        private void InitializeArchitectureData()
        {
            foreach (Architecture architecture in this.Architectures)
            {
                if (architecture.PlanArchitectureID >= 0)
                {
                    architecture.PlanArchitecture = this.Architectures.GetGameObject(architecture.PlanArchitectureID) as Architecture;
                }
                if (architecture.TransferFundArchitectureID >= 0)
                {
                    architecture.TransferFundArchitecture = this.Architectures.GetGameObject(architecture.TransferFundArchitectureID) as Architecture;
                }
                if (architecture.TransferFoodArchitectureID >= 0)
                {
                    architecture.TransferFoodArchitecture = this.Architectures.GetGameObject(architecture.TransferFoodArchitectureID) as Architecture;
                }
                if (architecture.DefensiveLegionID >= 0)
                {
                    architecture.DefensiveLegion = this.Legions.GetGameObject(architecture.DefensiveLegionID) as Legion;
                }
                if (architecture.RobberTroopID >= 0)
                {
                    architecture.RobberTroop = this.Troops.GetGameObject(architecture.RobberTroopID) as Troop;
                }
            }

            bool redoLinks = false;
            foreach (Architecture architecture2 in this.Architectures)
            {
                architecture2.LoadAILandLinksFromString(this.Architectures, architecture2.AILandLinksString);
                architecture2.LoadAIWaterLinksFromString(this.Architectures, architecture2.AIWaterLinksString);
            }
            foreach (Architecture architecture2 in this.Architectures)
            {
                if (architecture2.AILandLinks.Count == 0 && architecture2.AIWaterLinks.Count == 0)
                {
                    redoLinks = true;
                    break;
                }
            }
            if (redoLinks)
            {
                foreach (Architecture architecture2 in this.Architectures)
                {
                    architecture2.AILandLinks.Clear();
                    architecture2.AIWaterLinks.Clear();
                }
                foreach (Architecture architecture2 in this.Architectures)
                {
                    architecture2.FindLinks(this.Architectures);
                }
            }

            foreach (Architecture architecture in this.Architectures)
            {
                if (architecture.BelongedFaction != null)
                {
                    architecture.CheckIsFrontLine();
                }
                architecture.GenerateAllAILinkNodes(2);
            }

            /*foreach (Architecture a in this.Architectures)
            {
                foreach (LinkNode i in a.AILandLinks)
                {
                    Point? p1;
                    Point? p2;
                    this.GetClosestPointsBetweenTwoAreas(a.ArchitectureArea, i.A.ArchitectureArea, out p1, out p2);

                    if (p1 != null && p2 != null){
                        Military m = new Military();
                        Troop t = new Troop();

                        t.pathFinder.GetFirstTierPath(p1.Value, p2.Value);
                        this.pathCache[new PathCacheKey(a, i.A)] = new List<Point>(t.FirstTierPath);
                    }
                }
            }*/
        }

        private void InitializeCaptiveData()
        {
            foreach (Captive captive in this.Captives)
            {
                if (captive.CaptiveFactionID >= 0)
                {
                    captive.CaptiveFaction = this.Factions.GetGameObject(captive.CaptiveFactionID) as Faction;
                }
                if (captive.RansomArchitectureID >= 0)
                {
                    captive.RansomArchitecture = this.Architectures.GetGameObject(captive.RansomArchitectureID) as Architecture;
                }
            }
        }

        private void InitializeFactionData()
        {
            foreach (Faction faction in this.Factions)
            {
                faction.PrepareData();
            }
        }

        public void InitializeMapData()
        {
            this.MapTileData = new TileData[ScenarioMap.MapDimensions.X, ScenarioMap.MapDimensions.Y];
            this.PenalizedMapData = new int[ScenarioMap.MapDimensions.X, ScenarioMap.MapDimensions.Y];
        }

        private void InitializeMilitaryData()
        {
            foreach (Military military in this.Militaries)
            {
                if (military.ShelledMilitaryID >= 0)
                {
                    military.SetShelledMilitary(this.Militaries.GetGameObject(military.ShelledMilitaryID) as Military);
                }
            }
        }

        private void InitializePersonData()
        {
            foreach (Person person in this.Persons)
            {
                if (person.ConvincingPersonID >= 0)
                {
                    person.ConvincingPerson = this.Persons.GetGameObject(person.ConvincingPersonID) as Person;
                }
            }
        }

        private void InitializeRoutewayData()
        {
            foreach (Routeway routeway in this.Routeways)
            {
                routeway.RefreshRoutewayPointsData();
            }
        }

        public void InitializeScenarioPlayerFactions(List<int> factionIDs)
        {
            this.PlayerFactions.LoadFromString(this.Factions, StaticMethods.SaveToString(factionIDs));
        }

        private void InitializeSectionData()
        {
            foreach (Section section in this.Sections)
            {
                if (section.OrientationFactionID >= 0)
                {
                    section.OrientationFaction = this.Factions.GetGameObject(section.OrientationFactionID) as Faction;
                }
                if (section.OrientationSectionID >= 0)
                {
                    section.OrientationSection = this.Sections.GetGameObject(section.OrientationSectionID) as Section;
                }
                if (section.OrientationStateID >= 0)
                {
                    section.OrientationState = this.States.GetGameObject(section.OrientationStateID) as State;
                }
                if (section.OrientationArchitectureID >= 0)
                {
                    section.OrientationArchitecture = this.Architectures.GetGameObject(section.OrientationArchitectureID) as Architecture;
                }
            }
        }

        /*
        private void InitializeSpyMessageData()
        {
            foreach (SpyMessage message in this.SpyMessages)
            {
                if (message.MessageFactionID >= 0)
                {
                    message.MessageFaction = this.Factions.GetGameObject(message.MessageFactionID) as Faction;
                }
                if (message.MessageArchitectureID >= 0)
                {
                    message.MessageArchitecture = this.Architectures.GetGameObject(message.MessageArchitectureID) as Architecture;
                }
            }
        }
        */

        private void InitializeTroopData()
        {
            TroopList toRemove = new TroopList();
            foreach (Troop troop in this.Troops)
            {
                if (troop.Leader == null || troop.Army == null || troop.Army.Kind == null)
                {
                    toRemove.Add(troop);
                }
                else if (troop.Persons.Count == 0)
                {
                    troop.Leader.LocationTroop = troop;
                }
            }
            foreach (Troop troop in toRemove)
            {
                if (troop.BelongedFaction != null)
                {
                    troop.BelongedFaction.RemoveTroop(troop);
                }
                this.Troops.Remove(troop);
            }

            foreach (Troop troop in this.Troops)
            {
                troop.Initialize();
            }
            foreach (TroopEvent event2 in this.TroopEvents)
            {
                if (event2.AfterEventHappened >= 0)
                {
                    event2.AfterHappenedEvent = this.TroopEvents.GetGameObject(event2.AfterEventHappened) as TroopEvent;
                }
            }
        }

        private void InitializeMilitariesData()
        {
            MilitaryList toRemove = new MilitaryList();
            foreach (Military military in this.Militaries)
            {
                if (military.Kind == null)
                {
                    toRemove.Add(military);
                }
            }
            foreach (Military military in toRemove)
            {
                if (military.BelongedArchitecture != null)
                {
                    military.BelongedArchitecture.RemoveMilitary(military);
                }
                this.Militaries.Remove(military);
            }
        }

        public bool IsCurrentPlayer(Faction faction)
        {
            return (this.CurrentPlayer == faction);
        }

        public bool IsFireVaild(Point position, bool typevalid, MilitaryType type)
        {
            if (this.GetArchitectureByPosition(position) != null)
            {
                return false;
            }
            TerrainKind terrainKindByPosition = this.GetTerrainKindByPosition(position);
            return (((typevalid && (type == MilitaryType.水军)) && (terrainKindByPosition == TerrainKind.水域)) || ((((terrainKindByPosition == TerrainKind.平原) || (terrainKindByPosition == TerrainKind.草原)) || (terrainKindByPosition == TerrainKind.森林)) || (terrainKindByPosition == TerrainKind.山地)));
        }

        public bool IsLastPlayer(Faction faction)
        {
            if (faction == null)
            {
                return false;
            }
            foreach (Faction faction2 in this.PlayerFactions)
            {
                if ((faction2 != faction) && !faction2.Passed)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsPlayer(Faction faction)
        {
            return ((faction != null) && (this.PlayerFactions.GetGameObject(faction.ID) != null));
        }

        public bool HasAIResourceBonus(Section section)
        {
            if (Session.GlobalVariables.PlayerAutoSectionHasAIResourceBonus)
            {
                return section != null && (!IsPlayer(section.BelongedFaction) || !section.AIDetail.AutoRun);
            }
            else
            {
                return section != null && !IsPlayer(section.BelongedFaction);
            }
        }

        public bool IsPlayerControlling()
        {
            return (((this.CurrentPlayer != null) && (this.CurrentFaction == this.CurrentPlayer)) && this.CurrentPlayer.Controlling);
        }

        public bool IsPositionDisplayable(Point position)
        {
            return (Session.MainGame.mainGameScreen.TileInScreen(position) && ((Session.GlobalVariables.SkyEye || (this.CurrentPlayer == null)) || this.CurrentPlayer.IsPositionKnown(position)));
        }

        public bool IsPositionEmpty(Point position)
        {
            if (this.PositionIsArchitecture(position))
            {
                return false;
            }
            if (this.PositionIsTroop(position))
            {
                return false;
            }
            return true;
        }

        public bool IsPositionMovable(Point position, Faction faction)
        {
            if (this.PositionIsTroop(position))
            {
                return false;
            }
            Architecture architectureByPosition = this.GetArchitectureByPosition(position);
            return ((architectureByPosition == null) || (architectureByPosition.BelongedFaction == faction));
        }

        public bool IsTheBottomTroop(Troop troop)
        {
            return (this.MapTileData[troop.Position.X, troop.Position.Y].TileTroop == troop);
        }

        public bool IsTroopViewingPosition(Troop troop, Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return false;
            }
            return this.MapTileData[position.X, position.Y].IsTroopViewing(troop);
        }

        public bool IsWaterPositionRoutewayable(Point position)
        {
            if (ScenarioMap.MapData[position.X, position.Y] == 6)
            {
                int num = 0;
                Point point = new Point(position.X - 1, position.Y);
                if (!(this.PositionOutOfRange(point) || (ScenarioMap.MapData[point.X, point.Y] != 6)))
                {
                    num++;
                }
                Point point2 = new Point(position.X, position.Y - 1);
                if (!(this.PositionOutOfRange(point2) || (ScenarioMap.MapData[point2.X, point2.Y] != 6)))
                {
                    num++;
                }
                Point point3 = new Point(position.X + 1, position.Y);
                if (!(this.PositionOutOfRange(point3) || (ScenarioMap.MapData[point3.X, point3.Y] != 6)))
                {
                    num++;
                }
                if (num > 2)
                {
                    return false;
                }
                Point point4 = new Point(position.X, position.Y + 1);
                if (!(this.PositionOutOfRange(point4) || (ScenarioMap.MapData[point4.X, point4.Y] != 6)))
                {
                    num++;
                }
                if (num > 2)
                {
                    return false;
                }
            }
            return true;
        }

        public bool SaveAvail()
        {
            return (this.IsPlayerControlling() && this.EnableLoadAndSave && !Session.GlobalVariables.HardcoreMode);
        }

        public bool LoadAvail()
        {
            return (this.IsPlayerControlling() && this.EnableLoadAndSave && !Session.GlobalVariables.HardcoreMode);
        }

        public bool isInCaptiveList(int personId)
        {
            foreach (Captive i in this.Captives)
            {
                if (i.CaptivePerson.ID == personId)
                {
                    return true;
                }
            }

            return false;
        }
        
        public static CommonData ProcessCommonData(CommonData commonData)
        {
            List<string> errorMsg = new List<string>();

            commonData.NumberGenerator = new CombatNumberGenerator();

            commonData.TroopAnimations = new TroopAnimation();

            errorMsg.AddRange(LoadGameCommonData());

            if (commonData.AllTerrainDetails != null && commonData.AllTerrainDetails.TerrainDetails != null)
            {
                foreach (var terrainDetail in commonData.AllTerrainDetails.TerrainDetails)
                {
                    terrainDetail.Value.Init();
                }
            }

            if (commonData.AllInfluences != null && commonData.AllInfluences.Influences != null)
            {
                foreach (var influence in commonData.AllInfluences.Influences)
                {
                    influence.Value.Init();
                }
            }

            if (commonData.AllFacilityKinds != null && commonData.AllFacilityKinds.FacilityKinds != null)
            {
                foreach (var facilityKind in commonData.AllFacilityKinds.FacilityKinds)
                {
                    facilityKind.Value.Init();

                    facilityKind.Value.Influences.LoadFromString(commonData.AllInfluences, facilityKind.Value.InfluencesString);

                    facilityKind.Value.Conditions.LoadFromString(commonData.AllConditions, facilityKind.Value.ConditionTableString);

                    Condition.LoadConditionWeightFromString(commonData.AllConditions, facilityKind.Value.AIBuildConditionWeightString, out facilityKind.Value.AIBuildConditionWeight);
                }
            }

            if (commonData.AllTechniques != null && commonData.AllTechniques.Techniques != null)
            {
                foreach (var technique in commonData.AllTechniques.Techniques)
                {
                    technique.Value.Init();
                    technique.Value.Influences.LoadFromString(commonData.AllInfluences, technique.Value.InfluencesString);
                    technique.Value.Conditions.LoadFromString(commonData.AllConditions, technique.Value.ConditionTableString);
                    Condition.LoadConditionWeightFromString(commonData.AllConditions, technique.Value.AIConditionWeightString, out technique.Value.AIConditionWeight);
                }
            }

            if (commonData.AllSkills != null && commonData.AllSkills.Skills != null)
            {
                foreach (var skill in commonData.AllSkills.Skills)
                {
                    skill.Value.Init();
                    skill.Value.Influences.LoadFromString(commonData.AllInfluences, skill.Value.InfluencesString);
                    skill.Value.Conditions.LoadFromString(commonData.AllConditions, skill.Value.ConditionTableString);
                }
            }

            if (commonData.AllTitles != null && commonData.AllTitles.Titles != null)
            {
                foreach (var title in commonData.AllTitles.Titles)
                {
                    title.Value.Init();
                    title.Value.Influences.LoadFromString(commonData.AllInfluences, title.Value.InfluencesString);
                    title.Value.Conditions.LoadFromString(commonData.AllConditions, title.Value.ConditionTableString);
                    title.Value.ArchitectureConditions.LoadFromString(commonData.AllConditions, title.Value.ArchitectureConditionsString);
                    title.Value.FactionConditions.LoadFromString(commonData.AllConditions, title.Value.FactionConditionsString);
                    title.Value.LoseConditions.LoadFromString(commonData.AllConditions, title.Value.LoseConditionsString);
                    title.Value.GenerateConditions.LoadFromString(commonData.AllConditions, title.Value.GenerateConditionsString);
                }
            }

            if (commonData.AllMilitaryKinds != null && commonData.AllMilitaryKinds.MilitaryKinds != null)
            {
                foreach (var militaryKind in commonData.AllMilitaryKinds.MilitaryKinds)
                {
                    militaryKind.Value.Init();

                    militaryKind.Value.Influences.LoadFromString(commonData.AllInfluences, militaryKind.Value.InfluencesString);

                    militaryKind.Value.CreateConditions.LoadFromString(commonData.AllConditions, militaryKind.Value.CreateConditionsString);

                    Condition.LoadConditionWeightFromString(commonData.AllConditions, militaryKind.Value.AICreateArchitectureConditionWeightString, out militaryKind.Value.AICreateArchitectureConditionWeight);
                    Condition.LoadConditionWeightFromString(commonData.AllConditions, militaryKind.Value.AIUpgradeArchitectureConditionWeightString, out militaryKind.Value.AIUpgradeArchitectureConditionWeight);
                    Condition.LoadConditionWeightFromString(commonData.AllConditions, militaryKind.Value.AIUpgradeLeaderConditionWeightString, out militaryKind.Value.AIUpgradeLeaderConditionWeight);
                    Condition.LoadConditionWeightFromString(commonData.AllConditions, militaryKind.Value.AILeaderConditionWeightString, out militaryKind.Value.AILeaderConditionWeight);

                    militaryKind.Value.successor = new MilitaryKindTable();
                    militaryKind.Value.successor.LoadFromString(commonData.AllMilitaryKinds, militaryKind.Value.SuccessorString);
                }
            }

            if (commonData.AllCombatMethods != null && commonData.AllCombatMethods.CombatMethods != null)
            {
                foreach (var combatMethod in commonData.AllCombatMethods.CombatMethods)
                {
                    combatMethod.Value.Init();

                    combatMethod.Value.Influences.LoadFromString(commonData.AllInfluences, combatMethod.Value.InfluencesString);

                    combatMethod.Value.AttackDefault = commonData.AllAttackDefaultKinds.GetGameObject(combatMethod.Value.AttackDefaultString) as AttackDefaultKind;
                    combatMethod.Value.AttackTarget = commonData.AllAttackTargetKinds.GetGameObject(combatMethod.Value.AttackTargetString) as AttackTargetKind;

                    combatMethod.Value.CastConditions.LoadFromString(commonData.AllConditions, combatMethod.Value.CastConditionsString);

                    Condition.LoadConditionWeightFromString(commonData.AllConditions, combatMethod.Value.AIConditionWeightSelfString, out combatMethod.Value.AIConditionWeightSelf);
                    Condition.LoadConditionWeightFromString(commonData.AllConditions, combatMethod.Value.AIConditionWeightEnemyString, out combatMethod.Value.AIConditionWeightEnemy);
                }
            }

            if (commonData.AllStunts != null && commonData.AllStunts.Stunts != null)
            {
                foreach (var stunt in commonData.AllStunts.Stunts)
                {
                    stunt.Value.Init();
                    stunt.Value.Influences.LoadFromString(commonData.AllInfluences, stunt.Value.InfluencesString);
                    stunt.Value.CastConditions.LoadFromString(commonData.AllConditions, stunt.Value.CastConditionsString);
                    stunt.Value.LearnConditions.LoadFromString(commonData.AllConditions, stunt.Value.LearnConditionsString);
                    stunt.Value.AIConditions.LoadFromString(commonData.AllConditions, stunt.Value.AIConditionsString);
                }
            }

            if (commonData.AllStratagems != null && commonData.AllStratagems.Stratagems != null)
            {
                foreach (var stratagem in commonData.AllStratagems.Stratagems)
                {
                    stratagem.Value.Init();
                    stratagem.Value.Influences.LoadFromString(commonData.AllInfluences, stratagem.Value.InfluencesString);
                    stratagem.Value.CastConditions.LoadFromString(commonData.AllConditions, stratagem.Value.CastConditionsString);
                    stratagem.Value.CastDefault = commonData.AllCastDefaultKinds.GetGameObject(stratagem.Value.CastDefaultString) as CastDefaultKind;
                    stratagem.Value.CastTarget = commonData.AllCastTargetKinds.GetGameObject(stratagem.Value.CastTargetString) as CastTargetKind;
                    Condition.LoadConditionWeightFromString(commonData.AllConditions, stratagem.Value.AIConditionWeightSelfString, out stratagem.Value.AIConditionWeightSelf);
                    Condition.LoadConditionWeightFromString(commonData.AllConditions, stratagem.Value.AIConditionWeightEnemyString, out stratagem.Value.AIConditionWeightEnemy);
                }
            }

            return commonData;
        }

        public List<string> ProcessScenarioData(bool fromScenario, bool editing = false)  //读剧本和读存档都调用了此函数
        {
            List<string> errorMsg = new List<string>();

            Init();
            
            scenarioJustLoaded = true;
                        
            ScenarioMap.LoadMapData(ScenarioMap.MapDataString, ScenarioMap.MapDimensions.X, ScenarioMap.MapDimensions.Y);
            ScenarioMap.Init();
                       
            //if (Platform.PlatFormType == PlatFormType.Android || Platform.PlatFormType == PlatFormType.iOS || Platform.PlatFormType == PlatFormType.Win)
            //{
//                ScenarioMap.TileWidth = 50;
                //ScenarioMap.TileHeight = 50;
            //}

            foreach (State state in this.States)
            {
                state.Init();
                state.LoadContactStatesFromString(this.States, state.ContactStatesString);
            }

            foreach (Region region in this.Regions)
            {
                region.Init();
                //region.StatesListString = reader["States"].ToString();
                region.LoadStatesFromString(this.States, region.StatesListString);
            }

            foreach (Person person in Persons)
            {
                List<string> errors = new List<string>();

                person.Init();

                //person.IdealTendencyIDString = (short)reader["IdealTendency"];
                person.IdealTendency = this.GameCommonData.AllIdealTendencyKinds.GetGameObject(person.IdealTendencyIDString) as IdealTendencyKind;

                person.Character = this.GameCommonData.AllCharacterKinds[person.PCharacter];

                //person.UniqueMilitaryKindsString = reader["UniqueMilitaryKinds"].ToString();
                //person.UniqueTitlesString = reader["UniqueTitles"].ToString();

                try
                {
                    errors.AddRange(person.UniqueMilitaryKinds.LoadFromString(this.GameCommonData.AllMilitaryKinds, person.UniqueMilitaryKindsString));
                    errors.AddRange(person.UniqueTitles.LoadFromString(this.GameCommonData.AllTitles, person.UniqueTitlesString));
                    //errors.AddRange(person.Guanzhis.LoadFromString(this.GameCommonData.AllTitles, reader["Guanzhis"].ToString()));
                }
                catch
                {
                }

                //person.SkillsString = reader["Skills"].ToString();
                person.Skills.LoadFromString(this.GameCommonData.AllSkills, person.SkillsString);

                //person.StudyingTitleString = (short)reader["StudyingTitle"];
                person.StudyingTitle = this.GameCommonData.AllTitles.GetTitle(person.StudyingTitleString);

                try
                {
                    errors.AddRange(person.LoadTitleFromString(person.RealTitlesString, this.GameCommonData.AllTitles));
                }
                catch
                {
                    Title t = this.GameCommonData.AllTitles.GetTitle(person.PersonalTitleString);
                    if (t != null)
                    {
                        person.RealTitles.Add(t);
                    }
                    t = this.GameCommonData.AllTitles.GetTitle(person.CombatTitleString);
                    if (t != null)
                    {
                        person.RealTitles.Add(t);
                    }
                }

                //person.StuntsString = reader["Stunts"].ToString();
                //person.StudyingStuntString = (short)reader["StudyingStunt"];

                try
                {
                    person.Stunts.LoadFromString(this.GameCommonData.AllStunts, person.StuntsString);
                    person.StudyingStunt = this.GameCommonData.AllStunts.GetStunt(person.StudyingStuntString);
                }
                catch
                {
                }

                //person.TrainPolicyIDString = (short)reader["TrainPolicy"];
                person.TrainPolicy = (TrainPolicy)this.GameCommonData.AllTrainPolicies.GetGameObject(person.TrainPolicyIDString);

                //person.preferredTroopPersonsString = reader["PreferredTroopPersons"].ToString();

                this.Persons.AddPersonWithEvent(person, false);  //所有武将，并加载武将事件

                this.AllPersons.Add(person.ID, person);   //武将字典

                // this.AllChildren.Add(person, person.NumberOfChildren);

                if (person.Available && person.Alive)
                {
                    this.AvailablePersons.Add(person);  //已出场武将
                }
            }
            
            foreach (Person p in this.Persons)
            {
                p.WaitForFeiZi = this.Persons.GetGameObject(p.waitForFeiziId) as Person;
                List<string> e = p.preferredTroopPersons.LoadFromString(this.Persons, p.preferredTroopPersonsString);
                if (e.Count > 0)
                {
                    errorMsg.Add("人物ID" + p.ID + "：副将一栏：");
                    errorMsg.AddRange(e);
                }
            }

            foreach (KeyValuePair<int, int> i in FatherIds)
            {
                if (this.Persons.GetGameObject(i.Key) != null)
                {
                    (this.Persons.GetGameObject(i.Key) as Person).Father = this.Persons.GetGameObject(i.Value) as Person;
                }
            }

            foreach (KeyValuePair<int, int> i in MotherIds)
            {
                if (this.Persons.GetGameObject(i.Key) != null)
                {
                    (this.Persons.GetGameObject(i.Key) as Person).Mother = this.Persons.GetGameObject(i.Value) as Person;
                }
            }

            foreach (KeyValuePair<int, int> i in SpouseIds)
            {
                Person p = (this.Persons.GetGameObject(i.Key) as Person);
                Person q = this.Persons.GetGameObject(i.Value) as Person;
                if (p != null)
                {
                    p.Spouse = q;
                    if (q != null && fromScenario)
                    {
                        p.EnsureRelationAtLeast(q, Session.Parameters.VeryCloseThreshold);
                    }
                }
            }

            foreach (KeyValuePair<int, int[]> i in BrotherIds)
            {
                if (i.Value.Length == 1 && i.Value[0] != -1)
                {
                    foreach (KeyValuePair<int, int[]> j in BrotherIds)
                    {
                        if (j.Value.Length > 0 && i.Value[0] == j.Value[0])
                        {
                            Person p = this.Persons.GetGameObject(i.Key) as Person;
                            Person q = this.Persons.GetGameObject(j.Key) as Person;
                            if (p != null)
                            {
                                p.Brothers.Add(q);
                                if (q != null && fromScenario)
                                {
                                    p.EnsureRelationAtLeast(q, Session.Parameters.VeryCloseThreshold);
                                }
                            }
                        }
                    }
                }
                else
                {
                    Person p = this.Persons.GetGameObject(i.Key) as Person;
                    foreach (int j in i.Value)
                    {
                        Person q = this.Persons.GetGameObject(j) as Person;
                        if (q != null)
                        {
                            if (p != null)
                            {
                                p.Brothers.Add(q);
                                if (q != null && fromScenario)
                                {
                                    p.EnsureRelationAtLeast(q, Session.Parameters.VeryCloseThreshold);
                                }
                            }
                        }
                        else
                        {
                            errorMsg.Add("人物ID" + p.ID + "：义兄弟ID" + j + "不存在");
                        }
                    }
                }
            }

            foreach (KeyValuePair<int, int[]> i in CloseIds)
            {
                Person p = this.Persons.GetGameObject(i.Key) as Person;
                foreach (int j in i.Value)
                {
                    Person q = this.Persons.GetGameObject(j) as Person;
                    if (p != null && q != null)
                    {
                        p.AddClose(q);
                    }
                    else if (p != null)
                    {
                        errorMsg.Add("人物ID" + p.ID + "：亲爱武将ID" + j + "不存在");
                    }
                }
            }

            foreach (KeyValuePair<int, int[]> i in HatedIds)
            {
                Person p = this.Persons.GetGameObject(i.Key) as Person;
                foreach (int j in i.Value)
                {
                    Person q = this.Persons.GetGameObject(j) as Person;
                    if (p != null && q != null)
                    {
                        p.AddHated(q);
                    }
                    else if (p != null)
                    {
                        errorMsg.Add("人物ID" + p.ID + "：厌恶武将ID" + j + "不存在");
                    }
                }
            }

            foreach (KeyValuePair<int, int[]> i in SuoshuIds)
            {
                Person p = this.Persons.GetGameObject(i.Key) as Person;
                foreach (int j in i.Value)
                {
                    Person q = this.Persons.GetGameObject(j) as Person;
                    if (p != null && q != null)
                    {
                        p.suoshurenwuList.Add(q);
                    }
                    else if (p != null)
                    {
                        errorMsg.Add("人物ID" + p.ID + "：所属人物表ID" + j + "不存在");
                    } 
                    else
                    {
                        errorMsg.Add("人物ID" + p + "：所属人物表ID" + j + "不存在");
                    }
                }
            }

            foreach (KeyValuePair<int, int> i in MarriageGranterId)
            {
                if ((this.Persons.GetGameObject(i.Key) as Person) != null)
                {
                    (this.Persons.GetGameObject(i.Key) as Person).marriageGranter = this.Persons.GetGameObject(i.Value) as Person;
                }
            }

            foreach (Person p in this.Persons)
            {
                if (p.Spouse != null && !p.suoshurenwuList.HasGameObject(p.Spouse))
                {
                    p.suoshurenwuList.Add(p.Spouse);
                    p.Spouse.suoshurenwuList.Add(p);
                }
            }

            foreach (var biography in this.AllBiographies.Biographys)
            {
                biography.Value.Init();
                Person p = (Person)this.Persons.GetGameObject(biography.Value.ID);
                if (p != null)
                {
                    List<string> e = new List<string>();

                    if (!String.IsNullOrEmpty(biography.Value.MilitaryKindsString))
                    {
                        e = biography.Value.MilitaryKinds.LoadFromString(this.GameCommonData.AllMilitaryKinds, biography.Value.MilitaryKindsString);
                    }

                    if (e.Count > 0)
                    {
                        errorMsg.Add("列传人物ID" + biography.Value.ID + "：");
                        errorMsg.AddRange(e);
                    }
                    if (biography.Value.MilitaryKinds.MilitaryKinds.Count == 0)
                    {
                        errorMsg.Add("列传人物ID" + biography.Value.ID + "：没有基本兵种。");
                    }
                    p.PersonBiography = biography.Value;
                }
            }

            foreach (Person p in this.Persons)
            {
                if (p.PersonBiography == null)
                {
                    p.PersonBiography = new Biography();
                    p.PersonBiography.FactionColor = 52;
                    p.PersonBiography.MilitaryKinds.AddBasicMilitaryKinds();
                    p.PersonBiography.Brief = "";
                    p.PersonBiography.History = "";
                    p.PersonBiography.Romance = "";
                    p.PersonBiography.InGame = "";
                    p.PersonBiography.ID = p.ID;
                    this.AllBiographies.AddBiography(p.PersonBiography);
                }
            }

            foreach (var relation in PersonRelationIds)
            {
                Person person1 = this.Persons.GetGameObject(relation.PersonID1) as Person;
                Person person2 = this.Persons.GetGameObject(relation.PersonID2) as Person;

                if (person1 == null)
                {
                    errorMsg.Add("人物关系：武将ID" + relation.PersonID1 + "不存在");
                }
                if (person2 == null)
                {
                    errorMsg.Add("人物关系：武将ID" + relation.PersonID2 + "不存在");
                }
                if (person1 != null && person2 != null)
                {
                    person1.SetRelation(person2, relation.Relation);
                }
            }

            if (this.captiveData != null && !editing)
            {
                foreach (Captive captive in this.captiveData)
                {
                    captive.CaptivePerson = this.Persons.GetGameObject(captive.CaptivePersonID) as Person;
                    if (captive.CaptivePerson == null)
                    {
                        errorMsg.Add("俘虏ID" + captive.ID + "：武将ID" + captive.CaptivePersonID + "不存在");
                        continue;
                    }
                    else
                    {
                        captive.CaptivePerson.SetBelongedCaptive(captive, PersonStatus.Captive);

                        captive.CaptivePerson.Status = PersonStatus.Captive;
                    }

                }
            }

            this.Captives.BindEvents();

            foreach (Military military in this.Militaries)
            {
                military.Init();

                if (this.GameCommonData.AllMilitaryKinds.GetMilitaryKind(military.KindID) == null)
                {
                    errorMsg.Add("编队ID" + military.ID + "：兵种ID" + military.KindID + "不存在");
                    continue;
                }
                foreach (Person p in this.Persons)
                {
                    if (p.ID == military.RecruitmentPersonID)
                    {
                        //p.RecruitmentMilitary = military;
                        p.RecruitMilitary(military);
                    }
                }
            }

            this.InitializeMilitaryData();

            foreach (Facility facility in this.Facilities)
            {
                if (this.GameCommonData.AllFacilityKinds.GetFacilityKind(facility.KindID) == null)
                {
                    errorMsg.Add("设施ID" + facility.ID + "：设施种类ID" + facility.KindID + "不存在");
                    continue;
                }
            }

            //foreach (Information information in this.Informations)
            //{

            //}

            foreach (Architecture architecture in this.Architectures)
            {
                List<string> e = new List<string>();

                architecture.Init();
                
                architecture.Kind = this.GameCommonData.AllArchitectureKinds.GetArchitectureKind(architecture.KindId);

                if (architecture.Kind == null)
                {
                    e.Add("建筑种类ID" + architecture.KindId + "不存在");
                }

                architecture.LocationState = this.States.GetGameObject(architecture.StateID) as State;
                if (architecture.LocationState == null)
                {
                    e.Add("州域ID" + architecture.KindId + "不存在");
                }
                else
                {
                    architecture.LocationState.Architectures.Add(architecture);
                    architecture.LocationState.LinkedRegion.Architectures.Add(architecture);
                    if (architecture.LocationState.StateAdminID == architecture.ID)
                    {
                        architecture.LocationState.StateAdmin = architecture;
                    }
                    if (architecture.LocationState.LinkedRegion.RegionCoreID == architecture.ID)
                    {
                        architecture.LocationState.LinkedRegion.RegionCore = architecture;
                    }
                }

                //architecture.CharacteristicsString = reader["Characteristics"].ToString();
                e.AddRange(architecture.Characteristics.LoadFromString(this.GameCommonData.AllInfluences, architecture.CharacteristicsString));

                //architecture.ArchitectureAreaString = reader["Area"].ToString();

                if (architecture.ArchitectureArea == null)
                {
                    architecture.ArchitectureArea = new GameArea();
                    architecture.LoadFromString(architecture.ArchitectureArea, architecture.ArchitectureAreaString);
                }

                //if (architecture.ArchitectureArea == null)
                //{
                //    architecture.ArchitectureArea = new GameArea();
                //}

                //if (architecture.ArchitectureArea.Area == null)
                //{
                //    architecture.ArchitectureArea.Area = new List<Point>();
                //}

                //architecture.PersonsString = reader["Persons"].ToString();
                //architecture.MovingPersonsString = reader["MovingPersons"].ToString();
                //architecture.NoFactionPersonsString = reader["NoFactionPersons"].ToString();
                //architecture.NoFactionMovingPersonsString = reader["NoFactionMovingPersons"].ToString();
                //architecture.feiziliebiaoString = reader["feiziliebiao"].ToString();

                e.AddRange(architecture.LoadPersonsFromString(this.AllPersons, architecture.PersonsString, PersonStatus.Normal));
                e.AddRange(architecture.LoadPersonsFromString(this.AllPersons, architecture.MovingPersonsString, PersonStatus.Moving));
                e.AddRange(architecture.LoadPersonsFromString(this.AllPersons, architecture.NoFactionPersonsString, PersonStatus.NoFaction));
                e.AddRange(architecture.LoadPersonsFromString(this.AllPersons, architecture.NoFactionMovingPersonsString, PersonStatus.NoFactionMoving));
                e.AddRange(architecture.LoadPersonsFromString(this.AllPersons, architecture.feiziliebiaoString, PersonStatus.Princess));

                //architecture.MilitariesString = reader["Militaries"].ToString();

                //architecture.FacilitiesString = reader["Facilities"].ToString();

                e.AddRange(architecture.LoadMilitariesFromString(this.Militaries, architecture.MilitariesString));
                e.AddRange(architecture.LoadFacilitiesFromString(this.Facilities, architecture.FacilitiesString));

                //architecture.FundPacksString = reader["FundPacks"].ToString();

                //architecture.FoodPacksString = reader["FoodPacks"].ToString();

                e.AddRange(architecture.LoadFundPacksFromString(architecture.FundPacksString));
                try
                {
                    e.AddRange(architecture.LoadFoodPacksFromString(architecture.FoodPacksString));
                }
                catch { }

                //architecture.PopulationPacksString = reader["PopulationPacks"].ToString();
                e.AddRange(architecture.LoadPopulationPacksFromString(architecture.PopulationPacksString));

                e.AddRange(architecture.LoadMilitaryPopulationPacksFromString(architecture.MilitaryPopulationPacksString));

                //architecture.CaptivesString = reader["Captives"].ToString();
                e.AddRange(architecture.LoadCaptivesFromString(this.Captives, architecture.CaptivesString));

                //architecture.AILandLinksString = reader["AILandLinks"].ToString();
                //architecture.AIWaterLinksString = reader["AIWaterLinks"].ToString();

                try
                {
                    architecture.zainan.zainanzhonglei = this.GameCommonData.suoyouzainanzhonglei.Getzainanzhonglei(architecture.zainan.zainanleixing);
                }
                catch
                {
                    architecture.youzainan = false;
                }

                try
                {
                    //architecture.InformationsString = (string)reader["Informations"];
                    e.AddRange(architecture.LoadInformationsFromString(this.Informations, architecture.InformationsString));
                }
                catch
                {
                }

                architecture.AIBattlingArchitectures = new ArchitectureList();

                if (e.Count > 0)
                {
                    errorMsg.Add("建筑ID" + architecture.ID + "：");
                    errorMsg.AddRange(e);
                }
                //else
                //{
                    this.Architectures.AddArchitectureWithEvent(architecture, false);
                //后面宝物的所在地有用到此allar，所以要先将城池加入字典，否则会造成宝物所在地为空
                this.AllArchitectures.Add(architecture.ID, architecture);
                //}

            }

            foreach (KeyValuePair<int, int[]> a in AiBattlingArchitectureStrings)
            {
                foreach (int i in a.Value)
                {
                    (this.Architectures.GetGameObject(a.Key) as Architecture).AIBattlingArchitectures.Add((this.Architectures.GetGameObject(i) as Architecture));
                }
            }

            foreach(Routeway routeway in Routeways)
            {
                List<string> e = new List<string>();

                routeway.Init();

                //routeway.StartArchitectureString = (int)reader["StartArchitecture"];
                routeway.StartArchitecture = this.Architectures.GetGameObject(routeway.StartArchitectureString) as Architecture;

                if (routeway.StartArchitecture != null)
                {
                    routeway.StartArchitecture.Routeways.Add(routeway);
                }
                else
                {
                    e.Add("建筑ID" + routeway.StartArchitectureString + "不存在");
                }

                //routeway.EndArchitectureString = (int)reader["EndArchitecture"];
                routeway.EndArchitecture = this.Architectures.GetGameObject(routeway.EndArchitectureString) as Architecture;

                //routeway.DestinationArchitectureString = (int)reader["DestinationArchitecture"];
                routeway.DestinationArchitecture = this.Architectures.GetGameObject(routeway.DestinationArchitectureString) as Architecture;

                routeway.BelongedFaction = this.Factions.GetGameObject(routeway.BelongedFactionString) as Faction;

                //routeway.LoadRoutePointsFromString(reader["Points"].ToString());

                if (e.Count > 0)
                {
                    errorMsg.Add("粮道ID" + routeway.ID + "：");
                    errorMsg.AddRange(e);
                }
                //this.Routeways.AddRoutewayWithEvent(routeway);
            }

            this.Troops.Init();
            
            foreach (Troop troop in this.Troops)
            {
                List<string> errors = new List<string>();

                troop.Init();

                //troop.StartingArchitectureString = (short)reader["StartingArchitecture"];
                troop.StartingArchitecture = this.Architectures.GetGameObject(troop.StartingArchitectureString) as Architecture;

                if (troop.StartingArchitecture == null)
                {
                    errors.Add("起始建筑ID" + troop.StartingArchitectureString + "不存在");
                }

                //troop.PersonsString = reader["Persons"].ToString();
                //troop.LeaderIDString = (short)reader["LeaderID"];

                errors.AddRange(troop.LoadPersonsFromString(this.AllPersons, troop.PersonsString, troop.LeaderIDString));

                //troop.MilitaryID = (short)reader["MilitaryID"];
                //if (this.Militaries.GetGameObject(troop.MilitaryID) == null)
                //{
                //    errors.Add("编队ID" + troop.MilitaryID + "不存在");
                //}

                //troop.CaptivesString = reader["Captives"].ToString();
                errors.AddRange(troop.LoadCaptivesFromString(this.Captives, troop.CaptivesString.NullToString("")));

                //troop.EventInfluencesString = reader["EventInfluences"].ToString();
                errors.AddRange(troop.EventInfluences.LoadFromString(this.GameCommonData.AllInfluences, troop.EventInfluencesString.NullToString("")));

                errors.AddRange(troop.LoadCombatMethodFromString(this.GameCommonData.AllCombatMethods, troop.CombatMethodsString.NullToString("")));

                //troop.CurrentStuntIDString = (short)reader["CurrentStunt"];
                troop.CurrentStunt = this.GameCommonData.AllStunts.GetStunt(troop.CurrentStuntIDString);

                troop.CurrentStratagem = this.GameCommonData.AllStratagems.GetStratagem(troop.CurrentStratagemID);

                if (errors.Count > 0)
                {
                    errors.Add("部队ID" + troop.ID + "：");
                    errorMsg.AddRange(errors);
                }

                if (troop.Army != null)
                {
                    this.Troops.AddTroopWithEvent(troop, false);
                }
            }

            foreach(Legion legion in this.Legions)
            {
                legion.Init();

                //legion.StartArchitectureString = (int)reader["StartArchitecture"];
                legion.StartArchitecture = this.Architectures.GetGameObject(legion.StartArchitectureString) as Architecture;

                //legion.WillArchitectureString = (int)reader["WillArchitecture"];
                legion.WillArchitecture = this.Architectures.GetGameObject(legion.WillArchitectureString) as Architecture;

                //legion.PreferredRoutewayString = (int)reader["PreferredRouteway"];
                legion.PreferredRouteway = this.Routeways.GetGameObject(legion.PreferredRoutewayString) as Routeway;

                //legion.InformationDestination = StaticMethods.LoadFromString(reader["InformationDestination"].ToString());

                //legion.CoreTroopString = (int)reader["CoreTroop"];
                legion.CoreTroop = this.Troops.GetGameObject(legion.CoreTroopString) as Troop;

                //legion.TroopsString = reader["Troops"].ToString();
                legion.LoadTroopsFromString(this.Troops, legion.TroopsString);

                //this.Legions.AddLegionWithEvent(legion);
            }

            foreach (Section section in this.Sections)
            {
                section.Init();

                List<string> e = new List<string>();
                //section.AIDetailIDString = (short)reader["AIDetail"];
                section.AIDetail = this.GameCommonData.AllSectionAIDetails.GetSectionAIDetail(section.AIDetailIDString);

                if (section.AIDetail == null)
                {
                    e.Add("军区委任类型" + section.AIDetailIDString + "不存在");
                }

                //section.ArchitecturesString = reader["Architectures"].ToString();
                e.AddRange(section.LoadArchitecturesFromString(this.Architectures, section.ArchitecturesString));

                if (e.Count > 0)
                {
                    errorMsg.Add("军区ID" + section.ID + "：");
                    errorMsg.AddRange(e);
                }

                //this.Sections.AddSectionWithEvent(section);
            }

            foreach (Faction faction in this.Factions)
            {
                List<string> e = new List<string>();

                faction.Init();

                //faction.ArchitecturesString = reader["Architectures"].ToString();
                e.AddRange(faction.LoadArchitecturesFromString(this.Architectures, faction.ArchitecturesString));

                //faction.SectionsString = reader["Sections"].ToString();
                e.AddRange(faction.LoadSectionsFromString(this.Sections, faction.SectionsString));

                //faction.TroopListString = reader["Troops"].ToString();
                e.AddRange(faction.LoadTroopsFromString(this.Troops, faction.TroopListString));

                //faction.InformationsString = reader["Informations"].ToString();
                e.AddRange(faction.LoadInformationsFromString(this.Informations, faction.InformationsString));

                //faction.RoutewaysString = reader["Routeways"].ToString();
                e.AddRange(faction.LoadRoutewaysFromString(this.Routeways, faction.RoutewaysString));

                //faction.LegionsString = reader["Legions"].ToString();
                e.AddRange(faction.LoadLegionsFromString(this.Legions, faction.LegionsString));

                //faction.BaseMilitaryKindsString = reader["BaseMilitaryKinds"].ToString();
                faction.BaseMilitaryKinds.LoadFromString(this.GameCommonData.AllMilitaryKinds, faction.BaseMilitaryKindsString);

                //faction.AvailableTechniquesString = reader["AvailableTechniques"].ToString();
                e.AddRange(faction.AvailableTechniques.LoadFromString(this.GameCommonData.AllTechniques, faction.AvailableTechniquesString));

                //faction.PlanTechniqueString = (short)reader["PlanTechnique"];
                faction.PlanTechnique = this.GameCommonData.AllTechniques.GetTechnique(faction.PlanTechniqueString);

                //faction.TransferingMilitariesString = reader["TransferingMilitaries"].ToString();
                e.AddRange(faction.LoadTransferingMilitariesFromString(this.Militaries, faction.TransferingMilitariesString.NullToString()));

                //faction.MilitariesString = reader["Militaries"].ToString();
                e.AddRange(faction.LoadMilitariesFromString(this.Militaries, faction.MilitariesString.NullToString()));

                //faction.GetGeneratorPersonCountString = reader["GetGeneratorPersonCount"].ToString();
                e.AddRange(faction.LoadGeneratorPersonCountFromString(faction.GetGeneratorPersonCountString.NullToString()));
                if (faction.PrinceID != -1 && this.Persons.GetGameObject(faction.PrinceID) as Person != null)//取消储君序列化，原有的方法会导致二次存档后储君为空
                {
                    faction.Prince = this.Persons.GetGameObject(faction.PrinceID) as Person;
                }
                if (faction.AvailableMilitaryKinds.GetMilitaryKindList().Count == 0)
                {
                    faction.AvailableMilitaryKinds.AddMilitaryKind(this.GameCommonData.AllMilitaryKinds.GetMilitaryKind(0));
                    faction.AvailableMilitaryKinds.AddMilitaryKind(this.GameCommonData.AllMilitaryKinds.GetMilitaryKind(1));
                    faction.AvailableMilitaryKinds.AddMilitaryKind(this.GameCommonData.AllMilitaryKinds.GetMilitaryKind(2));
                }
                if (e.Count > 0)
                {
                    errorMsg.Add("势力ID" + faction.ID + "：");
                    errorMsg.AddRange(e);
                }

                this.Factions.AddFactionWithEvent(faction, false);
            }

            this.DiplomaticRelations.Init(this.Factions);

            foreach (Treasure treasure in this.Treasures)
            {
                treasure.Init();

                //treasure.HidePlaceIDString = (short)reader["HidePlace"];
                treasure.HidePlace = this.AllArchitectures.ContainsKey(treasure.HidePlaceIDString) ? this.AllArchitectures[treasure.HidePlaceIDString] : null;

                //treasure.BelongedPersonIDString = (short)reader["BelongedPerson"];
                treasure.BelongedPerson = this.AllPersons.ContainsKey(treasure.BelongedPersonIDString) ? this.AllPersons[treasure.BelongedPersonIDString] : null;
                
                if (treasure.BelongedPerson != null)
                {
                    treasure.BelongedPerson.Treasures.Add(treasure);
                }

                //treasure.InfluencesString = reader["Influences"].ToString();
                treasure.Influences.LoadFromString(this.GameCommonData.AllInfluences, treasure.InfluencesString);

                //this.Treasures.AddTreasure(treasure);
            }

            //foreach (var dr in this.DiplomaticRelations.DiplomaticRelations)
            //{

            //}

            foreach (TroopEvent te in this.TroopEvents)
            {
                te.Init();

                //te.LaunchPersonString = (short)reader["LaunchPerson"];
                te.LaunchPerson = this.Persons.GetGameObject(te.LaunchPersonString) as Person;

                //te.ConditionsString = reader["Conditions"].ToString();
                te.Conditions.LoadFromString(this.GameCommonData.AllConditions, te.ConditionsString);

                //te.TargetPersonsString = reader["TargetPersons"].ToString();
                te.LoadTargetPersonFromString(this.AllPersons, te.TargetPersonsString);

                //te.SelfEffectsString = reader["EffectSelf"].ToString();
                te.LoadSelfEffectFromString(this.GameCommonData.AllTroopEventEffects, te.SelfEffectsString);

                //te.EffectPersonsString = reader["EffectPersons"].ToString();
                te.LoadEffectPersonFromString(this.AllPersons, this.GameCommonData.AllTroopEventEffects, te.EffectPersonsString);

                //te.EffectAreasString = reader["EffectAreas"].ToString();
                te.LoadEffectAreaFromString(this.GameCommonData.AllTroopEventEffects, te.EffectAreasString);

                te.LoadDialogFromString(this.AllPersons, te.dialogString);
                if (te.TryToShowString == null) te.TryToShowString = "";
                this.TroopEvents.AddTroopEventWithEvent(te, false);
            }

            foreach (Event e in this.AllEvents)
            {
                e.Init();

                //e.personString = reader["PersonId"].ToString();
                e.LoadPersonIdFromString(this.Persons, e.personString);

                //e.PersonCondString = reader["PersonCond"].ToString();
                e.LoadPersonCondFromString(this.GameCommonData.AllConditions, e.PersonCondString);

                //e.architectureString = reader["ArchitectureID"].ToString();
                e.LoadArchitectureFromString(this.Architectures, e.architectureString);

                //e.architectureCondString = reader["ArchitectureCond"].ToString();
                e.LoadArchitctureCondFromString(this.GameCommonData.AllConditions, e.architectureCondString);

                //e.factionString = reader["FactionID"].ToString();
                e.LoadFactionFromString(this.Factions, e.factionString);

                //e.factionCondString = reader["FactionCond"].ToString();
                e.LoadFactionCondFromString(this.GameCommonData.AllConditions, e.factionCondString);

                //e.effectString = reader["Effect"].ToString();
                e.LoadEffectFromString(this.GameCommonData.AllEventEffects, e.effectString);

                //e.architectureEffectString = reader["ArchitectureEffect"].ToString();
                e.LoadArchitectureEffectFromString(this.GameCommonData.AllEventEffects, e.architectureEffectString);

                //e.factionEffectIDString = reader["FactionEffect"].ToString();
                e.LoadFactionEffectFromString(this.GameCommonData.AllEventEffects, e.factionEffectIDString);

                if (e.dialogString != null)
                {
                    e.LoadDialogFromString(e.dialogString);
                }

                //e.yesEffectString = reader["YesEffect"].ToString();
                e.LoadYesEffectFromString(this.GameCommonData.AllEventEffects, e.yesEffectString);
                //e.noEffectString = reader["NoEffect"].ToString();
                e.LoadNoEffectFromString(this.GameCommonData.AllEventEffects, e.noEffectString);

                if (e.yesdialogString != null)
                {
                    e.LoadyesDialogFromString(e.yesdialogString);
                }
                if (e.nodialogString != null)
                {
                    e.LoadnoDialogFromString(e.nodialogString);
                }

                //e.yesArchitectureEffectString = reader["YesArchitectureEffect"].ToString();
                //e.noArchitectureEffectString = reader["NoArchitectureEffect"].ToString();
                e.LoadYesArchitectureEffectFromString(this.GameCommonData.AllEventEffects, e.yesArchitectureEffectString);
                e.LoadNoArchitectureEffectFromString(this.GameCommonData.AllEventEffects, e.noArchitectureEffectString);

                if (e.scenBiographyString != null)
                {
                    e.LoadScenBiographyFromString(e.scenBiographyString);
                }

                if (e.TryToShowString == null) e.TryToShowString = "";
                //e.LoadScenBiographyFromString(reader["ScenBiography"].ToString());
                this.AllEvents.AddEventWithEvent(e, false);
            }
            if(!editing)//这里不加条件的话，用剧本编辑器读取有错剧本时，可能出现游戏主程序能读剧本而编辑器打不开剧本的情况
            {
                foreach (Person p in this.Persons)
                {
                    if (p.Status == PersonStatus.Normal || p.Status == PersonStatus.Moving)
                    {
                        if (p.LocationArchitecture != null && p.LocationArchitecture.BelongedFaction == null)
                        {
                            errorMsg.Add("武将ID" + p.ID + "在一座没有势力的城池仕官");
                            if (p.Status == PersonStatus.Normal)
                            {
                                p.Status = PersonStatus.NoFaction;
                            }
                            else
                            {
                                p.Status = PersonStatus.NoFactionMoving;
                            }
                        }
                    }
                    if (p.Status == PersonStatus.Moving || p.Status == PersonStatus.NoFactionMoving)
                    {
                        if (p.ArrivingDays <= 0)
                        {
                            errorMsg.Add("武将ID" + p.ID + "正移动，但没有移动天数");
                            p.ArrivingDays = 1;
                        }
                    }
                    if (p.Available && p.Alive && p.LocationArchitecture == null && p.LocationTroop == null && (p.ID < 7000 || p.ID >= 8000))
                    {
                        if (p.Status != PersonStatus.Princess)
                        {
                            errorMsg.Add("武将ID" + p.ID + "已登场，但没有所属建筑");
                            p.Available = false;
                            p.Alive = false;
                            p.Status = PersonStatus.None;
                        }
                    }
                }
                ClearTempDic();
            }

            this.YearTable.Init();
            //this.YearTable = new YearTable();

            this.AllPersons.Clear();
            this.AllArchitectures.Clear();

            this.alterTransportShipAdaptibility();

            //using (TextWriter tw = new StreamWriter(SCENARIO_ERROR_TEXT_FILE))
            //{
            //    foreach (string s in errorMsg)
            //    {
            //        tw.WriteLine(s);
            //    }
            //}

            ExtensionInterface.call("Load", new Object[] { this });

            return errorMsg;
        }

        void ClearTempDic()
        {
            FatherIds.Clear();
            MotherIds.Clear();
            SpouseIds.Clear();
            BrotherIds.Clear();
            SuoshuIds.Clear();
            CloseIds.Clear();
            HatedIds.Clear();
            MarriageGranterId.Clear();
            PersonRelationIds.Clear();
        }
        
        private void alterTransportShipAdaptibility()
        {
            MilitaryKind militaryKind = this.GameCommonData.AllMilitaryKinds.GetMilitaryKind(28);
            if (Session.GlobalVariables.LandArmyCanGoDownWater)
            {
                militaryKind.OneAdaptabilityKind = 0;
                /*militaryKind.PlainAdaptability = 5;
                militaryKind.GrasslandAdaptability = 5;
                militaryKind.ForrestAdaptability = 6;
                militaryKind.MarshAdaptability = 100;
                militaryKind.MountainAdaptability = 10;
                militaryKind.WaterAdaptability = 5;
                militaryKind.RidgeAdaptability = 100;
                militaryKind.WastelandAdaptability = 6;
                militaryKind.DesertAdaptability = 10;
                militaryKind.CliffAdaptability = 7;*/
            }
            else
            {
                militaryKind.OneAdaptabilityKind = 6;
                militaryKind.PlainAdaptability = 100;
                militaryKind.GrasslandAdaptability = 100;
                militaryKind.ForrestAdaptability = 100;
                militaryKind.MarshAdaptability = 100;
                militaryKind.MountainAdaptability = 100;
                //militaryKind.WaterAdaptability = 5;
                militaryKind.RidgeAdaptability = 100;
                militaryKind.WastelandAdaptability = 100;
                militaryKind.DesertAdaptability = 100;
                militaryKind.CliffAdaptability = 100;
            }
        }

        private void ApplyInformations()
        {
            foreach (Information i in this.Informations)
            {
                i.Apply();
            }
        }

        public void ForceOptionsOnAutoplay()
        {
            if (this.PlayerFactions.Count == 0)
            {
                Session.GlobalVariables.SkyEye = true;
                Session.GlobalVariables.EnableCheat = true;
                Session.GlobalVariables.HardcoreMode = false;
            }
        }

        public void InitPluginsWithScenario(MainGameScreen screen)
        {
            foreach (GameObject plugin in screen.PluginList)
            {
                if (plugin is IScenarioAwarePlugin)
                {
                    ((IScenarioAwarePlugin)plugin).SetScenario();
                }
            }
        }

        private void MigrateScenario()
        {
            foreach (Architecture a in this.Architectures)
            {
                if (a.MilitaryPopulation == 0)
                {
                    a.MilitaryPopulation = (int) (a.Population * (0.25 + (500000 - a.Population) / 500000 * 0.25));
                }
            }
        }

        public void AfterLoadGameScenario(MainGameScreen screen)
        {
            MigrateScenario();

            this.InitPluginsWithScenario(screen);
            this.InitializeMapData();
            this.TroopAnimations.UpdateDirectionAnimations(ScenarioMap.TileWidth);
            this.ApplyFireTable();
            this.InitializeArchitectureMapTile();
            this.InitializeFactionData();
            this.ApplyInformations();
            this.Preparing = true;
            this.Factions.BuildQueue(true);
            this.Factions.ApplyInfluences();
            this.Architectures.ApplyInfluences();
            this.Persons.ApplyInfluences();
            this.Preparing = false;
            this.InitialGameData();
            Session.Parameters.InitBaseRates();

            if (this.OnAfterLoadScenario != null)
            {
                this.OnAfterLoadScenario();
            }

            this.LoadedFileName = "";

            this.sessionStartTime = DateTime.Now;
        }

        public void AfterLoadSaveFile(MainGameScreen screen)
        {
            this.InitPluginsWithScenario(screen);
            this.InitializeMapData();
            this.TroopAnimations.UpdateDirectionAnimations(ScenarioMap.TileWidth);
            this.ApplyFireTable();
            this.InitializeArchitectureMapTile();
            this.InitializeFactionData();
            this.ApplyInformations();
            this.Preparing = true;

            this.Factions.BuildQueue(true);  //待考慮效果
            
            this.Factions.ApplyInfluences();            
            this.Architectures.ApplyInfluences();
            this.Persons.ApplyInfluences();

            this.Preparing = false;

            this.InitialGameData();

            if (this.OnAfterLoadScenario != null)
            {
                this.OnAfterLoadScenario();
            }
            
            if (this.PlayerFactions.Count == 0)
            {
                oldDialogShowTime = Setting.Current.GlobalVariables.DialogShowTime;
                Setting.Current.GlobalVariables.DialogShowTime = 0;
            }
            else
            {
                //if (oldDialogShowTime >= 0)
                if (oldDialogShowTime > 0)
                {
                    Setting.Current.GlobalVariables.DialogShowTime = oldDialogShowTime;
                }
                else
                {
                    //Setting.Current.GlobalVariables.DialogShowTime = Session.globalVariablesBasic.DialogShowTime;
                }
            } 
            this.ForceOptionsOnAutoplay();

            this.sessionStartTime = DateTime.Now;
        }

        public void AfterInit()
        {
            if (this.CurrentPlayer != null)
            {
                detectCurrentPlayerBattleState(this.CurrentPlayer, true);
                this.CurrentPlayer.RefreshImportantPerson();
            }
        }

        private int oldDialogShowTime = -1;

        private void AIMergeAgainstPlayer()
        {
            if (this.PlayerFactions.Count == 0) return;
            if (this.Factions.Count < 3) return;
            if (!Session.GlobalVariables.PermitFactionMerge) return;
            if (Session.GlobalVariables.AIMergeAgainstPlayer < 0) return;

            Faction strongestAI = null;
            Faction strongestPlayer = null;
            int strongestAIPower = int.MinValue;
            int strongestPlayerPower = int.MinValue;

            foreach (Faction f in this.Factions)
            {
                if (this.IsPlayer(f))
                {
                    if (f.Power > strongestPlayerPower)
                    {
                        strongestPlayerPower = f.Power;
                        strongestPlayer = f;
                    }
                }
                else
                {
                    FactionList adjacent = f.GetAdjecentFactions();
                    bool nextToPlayer = false;
                    foreach (Faction g in adjacent)
                    {
                        if (this.IsPlayer(g) && this.GetDiplomaticRelation(f.ID, g.ID) < -100)
                        {
                            nextToPlayer = true;
                            break;
                        }
                    }

                    if (!nextToPlayer) continue;

                    if (f.Power > strongestAIPower)
                    {
                        strongestAIPower = f.Power;
                        strongestAI = f;
                    }
                }
            }

            if (strongestAI == null || strongestPlayer == null) return;


            if (GameObject.Chance((int)(((float)strongestPlayerPower / strongestAIPower - Session.GlobalVariables.AIMergeAgainstPlayer) * 100)))
            {
                GameObjectList fl = this.Factions.GetList();
                fl.IsNumber = true;
                fl.PropertyName = "Power";
                fl.SmallToBig = false;
                fl.ReSort();

                Faction toMerge = null;
                foreach (Faction f in fl)
                {
                    if (this.IsPlayer(f) || f == strongestAI) continue;

                    if (!f.Leader.Hates(strongestAI.Leader))
                    {
                        if (GameObject.Chance((int)(Person.GetIdealAttraction(strongestAI.Leader, f.Leader) + strongestPlayerPower / strongestAIPower * 100)))
                        {
                            if (strongestAI.adjacentTo(f) && this.GetDiplomaticRelation(strongestAI.ID, f.ID) > 0)
                            {
                                toMerge = f;
                                break;
                            }
                        }
                    }
                }

                if (toMerge != null)
                {
                    if (toMerge.Power > strongestAI.Power)
                    {
                        Faction temp = toMerge;
                        toMerge = strongestAI;
                        strongestAI = temp;
                    }
                    Session.MainGame.mainGameScreen.OnAIMergeAgainstPlayer(strongestPlayer, strongestAI, toMerge);
                    this.YearTable.addChangeFactionEntry(this.Date, toMerge, strongestAI);
                    GameObjectList rebelCandidates = toMerge.Persons.GetList();
                    toMerge.ChangeFaction(strongestAI);
                    toMerge.AfterChangeLeader(strongestAI, rebelCandidates, toMerge.Leader, strongestAI.Leader);
                }
            }

        }

        public void MonthPassedEvent()
        {
            ExtensionInterface.call("MonthEvent", new Object[] { this });

            this.AIMergeAgainstPlayer();

            foreach (Faction faction in this.Factions.GetRandomList())
            {
                faction.MonthEvent();
            }
            foreach (Person person in this.Persons)
            {
                person.TryToBeAvailable();
            }
            this.AddPreparedAvailablePersons();
            foreach (Person person in this.AvailablePersons.GetRandomList())
            {
                person.MonthEvent();
            }
            foreach (Architecture architecture in this.Architectures.GetRandomList())
            {
                architecture.MonthEvent();
            }
            foreach (MilitaryKind kind in this.GameCommonData.AllMilitaryKinds.MilitaryKinds.Values)
            {
#pragma warning disable CS0219 // The variable 'flag' is assigned but its value is never used
                bool flag = true;
#pragma warning restore CS0219 // The variable 'flag' is assigned but its value is never used
                foreach (Troop troop in this.Troops)
                {
                    if ((troop.Army.Kind == kind) && Session.MainGame.mainGameScreen.TileInScreen(troop.Position))
                    {
                        flag = false;
                        break;
                    }
                }
                //if (flag)
                //{
                //    kind.Textures.Dispose();
                //}
            }
        }

        private void AdjustGlobalPersonRelation()
        {
            foreach (Person p in this.Persons)
            {
                if (p.Available && p.Alive && GameObject.Random(120 / Session.Parameters.DayInTurn) == 0)
                {
                    foreach (Person q in this.Persons)
                    {
                        if (p == q) continue;
                        if (!q.Alive)
                        {
                            p.SetRelation(q, 0);
                            q.SetRelation(p, 0);
                            continue;
                        }

                        if (q.Available && q.Alive && p.BelongedFactionWithPrincess != null && GameObject.Random(30 / Session.Parameters.DayInTurn) == 0)
                        {
                            float likeability = Person.GetIdealAttraction(p, q) * 8 + q.Glamour * 0.75f + p.Glamour * 0.25f + q.PersonalLoyalty * 7.5f + p.PersonalLoyalty * 2.5f - q.Ambition * 5 - p.Ambition * 5 - 100;
                            
                            bool sameWork = p.SameLocationAs(q) &&
                                    (
                                        (p.Status == PersonStatus.Normal && q.Status == PersonStatus.Normal &&
                                            ((p.WorkKind == q.WorkKind) || (p.OutsideTask == q.OutsideTask))
                                        ) ||
                                        (p.Status == PersonStatus.Princess && q.Status == PersonStatus.Princess)
                                    );
                            float factor = 0.0f;
                            
                            if (p.LocationTroop == q.LocationTroop && p.LocationTroop != null && q.LocationTroop != null)
                            {
                                factor = 3.0f;
                            }
                            else if (sameWork)
                            {
                                factor = 1.0f;
                            } 
                            else if (p.SameLocationAs(q) && GameObject.Chance(50))
                            {
                                factor = 1.0f;
                            }
                            else if (p.BelongedFactionWithPrincess == q.BelongedFactionWithPrincess && GameObject.Chance(20))
                            {
                                factor = 1.0f;
                            }

                            if (factor > 0)
                            {
                                if (GameObject.Chance((int) (likeability / 4.0f)))
                                {
                                    p.AdjustRelation(q, 3f * factor, 2 * factor);
                                    q.AdjustRelation(p, 3f * factor, 2 * factor);
                                }
                                else if (GameObject.Chance((int)(-likeability / 4.0f)))
                                {
                                    p.AdjustRelation(q, -3f * factor, -2 * factor);
                                    q.AdjustRelation(p, -3f * factor, -2 * factor);
                                }
                            }
                        }

                        if (p.GetRelation(q) > 0)
                        {
                            if (!p.Closes(q) && GameObject.Chance((5 - p.PersonalLoyalty) * 20 - 10))
                            {
                                float d = (float) Session.Parameters.CloseThreshold / Math.Max(10, p.GetRelation(q));
                                if (p.LocationArchitecture == q.LocationArchitecture || p.LocationTroop == q.LocationTroop)
                                {
                                    p.AdjustRelation(q, -d / 10f, 0);
                                }
                                else
                                {
                                    p.AdjustRelation(q, -d / 25f, 0);
                                }

                                if (p.GetRelation(q) < 0)
                                {
                                    p.SetRelation(q, 0);
                                }
                            }
                        }
                        else if (p.GetRelation(q) < 0)
                        {
                            if (!p.Hates(q))
                            {
                                float d = Session.Parameters.HateThreshold / -p.GetRelation(q) / 5f;
                                if (p.Status == PersonStatus.Princess && q.Status == PersonStatus.Princess)
                                {
                                    d *= 4;
                                }
                                if (p.LocationArchitecture == q.LocationArchitecture || p.LocationTroop == q.LocationTroop)
                                {
                                    p.AdjustRelation(q, -d / 10f, 0);
                                }
                                else
                                {
                                    p.AdjustRelation(q, -d / 25f, 0);
                                }

                                if (p.GetRelation(q) > 0)
                                {
                                    p.SetRelation(q, 0);
                                }
                            }
                        }
                    }
                }
            }
        }

        public void MonthStartingEvent()
        {
        }

        public void SeasonChangeEvent()
        {
            if (!scenarioJustLoaded)
            {
                ExtensionInterface.call("SeasonEvent", new Object[] { this });
                if ((this.Date.Month == 3 || this.Date.Month == 6 || this.Date.Month == 9 || this.Date.Month == 12) && this.Date.Day <= Session.Current.Scenario.Parameters.DayInTurn)
                {
                    foreach (Faction faction in this.Factions.GetRandomList())
                    {
                        faction.SeasonEvent();
                    }
                    foreach (Architecture architecture in this.Architectures.GetRandomList())
                    {
                        architecture.DevelopSeason();
                    }
                }
            }
        }

        public bool MoreThanOneTroopOnPosition(Point position)
        {
            return (this.MapTileData[position.X, position.Y].TroopCount > 1);
        }

        public void NewFaction()
        {
            if (GameObject.Random(15) == 0)
            {
                this.NewFaction(this.AvailablePersons, false, false);
            }
        }

        public void NewFaction(PersonList candidates, bool leaderChange, bool nonInherited)
        {
            if (Session.GlobalVariables.WujiangYoukenengDuli == false) return;

            PersonList list = new PersonList();
            foreach (Person person in candidates)
            {
                if (person.YoukenengChuangjianXinShili())   //里面包含武将有可能独立的参数
                {
                    if ((person.Ambition > 1 && GameObject.Random((5 - person.Ambition) * (5 - person.Ambition) * (5 - person.Ambition)) == 0) ||
                        (person.BelongedFaction != null && person.Hates(person.BelongedFaction.Leader)))
                    {
                        list.Add(person);
                    }
                }
            }

            if (list.Count == 0) return;

            Person p = (Person)list[GameObject.Random(list.Count)];
            int cnt = 0;
            foreach (Person person8 in list)
            {
                cnt++;
                if (!leaderChange && cnt > 1)
                {
                    break;
                }

                if (leaderChange)
                {
                    p = person8;
                }

                Architecture location = p.BelongedArchitecture;
                Faction faction = p.BelongedFaction;
                if (location == null) continue;
                if (faction != null && !p.Hates(faction.Leader))
                {
                    if (p.Loyalty >= 100) continue;
                    if (p.Loyalty >= 90 && !p.LeaderPossibility) continue;
                }
                if (faction != null && Person.GetIdealOffset(faction.Leader, p) <= 10 && !p.Hates(faction.Leader)) continue;
                if (faction != null && location == faction.Capital) continue;
                //if (GameObject.Random(15) != 0) return;

                if (GameObject.Random(location.Population + location.ArmyScale * 5000 +
                        location.Domination * 200 + location.Morale * 10) >
                    GameObject.Random(p.Reputation *
                    (p.LeaderPossibility ? 3 : 1) *
                    (leaderChange && nonInherited ? p.Ambition * p.Ambition * (p.Glamour / 20) : 1) *
                    (faction != null && leaderChange && nonInherited ? Person.GetIdealOffset(p, faction.Leader) / 10 + 1 : 1) *
                    (faction != null && (p.Hates(faction.Leader) || faction.Leader.Hates(p)) ? (leaderChange ? 10000 : 3) : 1) *
                    (faction == null ? 3 : 1))) continue;
                this.CreateNewFaction(p);
            }
        }

        private void NoFoodPositionDayEvent()
        {
            List<NoFoodPosition> list = new List<NoFoodPosition>();
            foreach (NoFoodPosition position in this.NoFoodDictionary.Positions.Values)
            {
                position.Days--;
                if (position.Days <= 0)
                {
                    list.Add(position);
                }
            }
            foreach (NoFoodPosition position in list)
            {
                this.NoFoodDictionary.RemovePosition(position);
            }
        }

        public bool PositionIsArchitecture(Point position)
        {
            return (this.GetArchitectureByPosition(position) != null);
        }

        public bool PositionIsOnFire(Point position)
        {
            if (this.PositionOutOfRange(position))
            {
                return false;
            }
            return this.FireTable.HasPosition(position);
        }

        public bool PositionIsOnFireNoCheck(Point position)
        {
            return this.FireTable.HasPosition(position);
        }

        public bool PositionIsTroop(Point position)
        {
            return (this.GetTroopByPosition(position) != null);
        }

        public bool PositionOutOfRange(Point position)
        {
            return ScenarioMap.PositionOutOfRange(position);
        }

        public string PositionString(Point position)
        {

            if (this.PositionIsArchitecture(position))
            {
                return this.GetArchitectureByPositionNoCheck(position).Name;
            }
            /*
            if (this.PositionIsTroop(position))
            {
                return this.GetTroopByPositionNoCheck(position).DisplayName;
            }
            */
            return (this.GetTerrainNameByPosition(position) + " " + this.GetCoordinateString(position));
        }

        public void ReflectDiplomaticRelations(int src, int des, int offset)
        {
            foreach (DiplomaticRelation relation in this.DiplomaticRelations.GetDiplomaticRelationListByFactionID(des))
            {
                int theOtherFactionID = relation.GetTheOtherFactionID(des);
                if ((theOtherFactionID != src) && (Math.Abs(relation.Relation) >= 100))
                {
                    int num2 = this.DiplomaticRelations.GetDiplomaticRelation(src, theOtherFactionID).Relation;
                    if ((num2 > -GlobalVariables.FriendlyDiplomacyThreshold) && (num2 < Session.GlobalVariables.FriendlyDiplomacyThreshold))
                    {
                        int num3 = relation.Relation;
                        if (num3 > 0x3e8)
                        {
                            num3 = 0x3e8;
                        }
                        else if (num3 < -0x3e8)
                        {
                            num3 = -0x3e8;
                        }
                        this.ChangeDiplomaticRelation(src, theOtherFactionID, (offset * num3) / 0x3e8);
                    }
                }
            }
        }

        public void RemovePositionAreaInfluence(Troop troop, Point position)
        {
            if (!this.PositionOutOfRange(position))
            {
                Troop troopByPositionNoCheck = this.GetTroopByPositionNoCheck(position);
                this.MapTileData[position.X, position.Y].RemoveAreaInfluence(troop, troopByPositionNoCheck);
                if (troopByPositionNoCheck != null)
                {
                    troopByPositionNoCheck.RefreshDataOfAreaInfluence();
                }
            }
        }

        public void RemovePositionContactingTroop(Troop troop, Point position)
        {
            if (!this.PositionOutOfRange(position))
            {
                this.MapTileData[position.X, position.Y].RemoveContactingTroop(troop);
            }
        }

        public void RemovePositionOffencingTroop(Troop troop, Point position)
        {
            if (!this.PositionOutOfRange(position))
            {
                this.MapTileData[position.X, position.Y].RemoveOffencingTroop(troop);
            }
        }

        public void RemovePositionStratagemingTroop(Troop troop, Point position)
        {
            if (!this.PositionOutOfRange(position))
            {
                this.MapTileData[position.X, position.Y].RemoveStratagemingTroop(troop);
            }
        }

        public void RemovePositionViewingTroopNoCheck(Troop troop, Point position)
        {
            this.MapTileData[position.X, position.Y].RemoveViewingTroop(troop);
        }

        public void RemoveRouteway(Routeway routeway)
        {
            if (routeway.FirstPoint != null)
            {
                routeway.CutAt(routeway.FirstPoint.Position);
            }
            if (routeway.StartArchitecture != null)
            {
                routeway.StartArchitecture.Routeways.Remove(routeway);
            }
            if (routeway.BelongedFaction != null)
            {
                routeway.BelongedFaction.RemoveRouteway(routeway);
            }
            this.Routeways.Remove(routeway);
        }

        public void ResetMapTileTroop(Point position)
        {
            if (this.MapTileData[position.X, position.Y].TileTroop != null && this.MapTileData[position.X, position.Y].TileTroop.Destroyed)
            {
                TileData data1 = this.MapTileData[position.X, position.Y];
                data1.TroopCount--;
                this.MapTileData[position.X, position.Y].TileTroop = null;
            }
        }

        public void ReallyResetMapTileTroop()
        {
            for (int i = 0; i < this.MapTileData.GetLength(0); ++i)
            {
                for (int j = 0; j < this.MapTileData.GetLength(1); ++j)
                {
                    TileData t = this.MapTileData[i, j];
                    if (t.ContactingTroops != null)
                    {
                        t.ContactingTroops.RemoveAll(u => u == null || u.Destroyed || u.Simulating);
                        if (t.ContactingTroops.Count == 0)
                        {
                            // Yes I mean it. Too many empty lists kill the memory.......
                            this.MapTileData[i, j].ContactingTroops = null;
                        }
                        else
                        {
                            t.ContactingTroops.Capacity = t.ContactingTroops.Count;
                        }
                    }
                    if (t.OffencingTroops != null)
                    {
                        t.OffencingTroops.RemoveAll(u => u == null || u.Destroyed || u.Simulating);
                        if (t.OffencingTroops.Count == 0)
                        {
                            this.MapTileData[i, j].OffencingTroops = null;
                        }
                        else
                        {
                            t.OffencingTroops.Capacity = t.OffencingTroops.Count;
                        }
                    }
                    if (t.StratagemingTroops != null)
                    {
                        t.StratagemingTroops.RemoveAll(u => u == null || u.Destroyed || u.Simulating);
                        if (t.StratagemingTroops.Count == 0)
                        {
                            this.MapTileData[i, j].StratagemingTroops = null;
                        }
                        else
                        {
                            t.StratagemingTroops.Capacity = t.StratagemingTroops.Count;
                        }
                    }
                    if (t.ViewingTroops != null)
                    {
                        t.ViewingTroops.RemoveAll(u => u == null || u.Destroyed || u.Simulating);
                        if (t.ViewingTroops.Count == 0)
                        {
                            this.MapTileData[i, j].ViewingTroops = null;
                        }
                        else
                        {
                            t.ViewingTroops.Capacity = t.ViewingTroops.Count;
                        }
                    }

                    if (t.AreaInfluenceList != null)
                    {
                        t.AreaInfluenceList.RemoveAll(u => u == null || u.Owner.Destroyed || u.Owner.Simulating);
                        if (t.AreaInfluenceList.Count == 0)
                        {
                            this.MapTileData[i, j].AreaInfluenceList = null;
                        }
                        else
                        {
                            t.AreaInfluenceList.Capacity = t.AreaInfluenceList.Count;
                        }
                    }

                    if (t.TileRouteways != null)
                    {
                        if (t.TileRouteways.Count == 0)
                        {
                            this.MapTileData[i, j].TileRouteways = null;
                        }
                        else
                        {
                            t.TileRouteways.Capacity = t.TileRouteways.Count;
                        }
                    }

                    if (t.SupplyingRoutePoints != null)
                    {
                        if (t.SupplyingRoutePoints.Count == 0)
                        {
                            this.MapTileData[i, j].SupplyingRoutePoints = null;
                        }
                        else
                        {
                            t.SupplyingRoutePoints.Capacity = t.SupplyingRoutePoints.Count;
                        }
                    }

                    if (t.SupplyingRoutePoints != null)
                    {
                        if (t.SupplyingRoutePoints.Count == 0)
                        {
                            this.MapTileData[i, j].SupplyingRoutePoints = null;
                        }
                        else
                        {
                            t.SupplyingRoutePoints.Capacity = t.SupplyingRoutePoints.Count;
                        }
                    }

                    if (t.TileTroop != null && (t.TileTroop.Destroyed || t.TileTroop.Simulating))
                    {
                        this.MapTileData[i, j].TileTroop = null;
                    }
                }
            }
        }

        public bool SaveGameScenario(string LoadedFileName, bool saveMap, bool saveCommonData, bool saveSettings, bool disposeMemory = true, bool fullPathProvided = false, bool editing = false)
        {
            if (this.GameTime < 0)
            {
                this.GameTime = 0;
            }
            if(!editing)
            {
                this.GameTime += (int)DateTime.Now.Subtract(sessionStartTime).TotalSeconds;
            }
            sessionStartTime = DateTime.Now;

            List<string> errors = new List<string>();

            ClearPersonStatusCache();
            ClearPersonWorkCache();

            this.Architectures.GameObjects = this.Architectures.GameObjects.OrderBy(x => x.ID).ToList();
            this.AllBiographies.Biographys = this.AllBiographies.Biographys.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            this.Captives.GameObjects = this.Captives.GameObjects.OrderBy(x => x.ID).ToList();
            this.AllEvents.GameObjects = this.AllEvents.GameObjects.OrderBy(x => x.ID).ToList();
            this.Facilities.GameObjects = this.Facilities.GameObjects.OrderBy(x => x.ID).ToList();
            this.Factions.GameObjects = this.Factions.GameObjects.OrderBy(x => x.ID).ToList();
            this.Informations.GameObjects = this.Informations.GameObjects.OrderBy(x => x.ID).ToList();
            this.Legions.GameObjects = this.Legions.GameObjects.OrderBy(x => x.ID).ToList();
            this.Militaries.GameObjects = this.Militaries.GameObjects.OrderBy(x => x.ID).ToList();
            this.Persons.GameObjects = this.Persons.GameObjects.OrderBy(x => x.ID).ToList();
            this.Routeways.GameObjects = this.Routeways.GameObjects.OrderBy(x => x.ID).ToList();
            this.Sections.GameObjects = this.Sections.GameObjects.OrderBy(x => x.ID).ToList();
            this.Treasures.GameObjects = this.Treasures.GameObjects.OrderBy(x => x.ID).ToList();
            this.Troops.GameObjects = this.Troops.GameObjects.OrderBy(x => x.ID).ToList();
            this.TroopEvents.GameObjects = this.TroopEvents.GameObjects.OrderBy(x => x.ID).ToList();
            this.DiplomaticRelations.DiplomaticRelations = this.DiplomaticRelations.DiplomaticRelations.OrderBy(x => x.Value.RelationFaction1ID).ToDictionary(x => x.Key, y => y.Value);
            if(editing)
            {
                this.FatherIds = this.FatherIds.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                this.MotherIds = this.MotherIds.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                this.SpouseIds = this.SpouseIds.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                this.BrotherIds = this.BrotherIds.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                this.SuoshuIds = this.SuoshuIds.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                this.CloseIds = this.CloseIds.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                this.HatedIds = this.HatedIds.OrderBy(x => x.Key).ToDictionary(x => x.Key, y => y.Value);
                this.PersonRelationIds = this.PersonRelationIds.OrderBy(x => x.PersonID1).ToList();
            }

            if (!disposeMemory)
            {
                this.DisposeLotsOfMemory();
            }

            if (!editing)
            {
                foreach (Faction faction in this.Factions)
                {
                    faction.SectionsString = faction.Sections.SaveToString();
                    faction.ArchitecturesString = faction.Architectures.SaveToString();
                    faction.TroopListString = faction.Troops.SaveToString(); ;
                    faction.InformationsString = faction.Informations.SaveToString();
                    faction.RoutewaysString = faction.Routeways.SaveToString();
                    faction.LegionsString = faction.Legions.SaveToString();
                    faction.BaseMilitaryKindsString = faction.BaseMilitaryKinds.SaveToString();
                    faction.AvailableTechniquesString = faction.AvailableTechniques.SaveToString();
                    faction.PlanTechniqueString = (faction.PlanTechnique != null) ? faction.PlanTechnique.ID : -1;
                    faction.GetGeneratorPersonCountString = faction.SaveGeneratorPersonCountToString();
                    faction.TransferingMilitariesString = faction.TransferingMilitaries.SaveToString();
                    faction.MilitariesString = faction.Militaries.SaveToString();
                    faction.PrinceID = faction.Prince != null ? faction.Prince.ID : -1;
                }
            }

            foreach (Section section in this.Sections)
            {
                section.EnsureSectionArchitecture();
                if (!editing)
                {
                    section.AIDetailIDString = section.AIDetail.ID;
                    section.OrientationFactionID = (section.OrientationFaction != null) ? section.OrientationFaction.ID : -1;
                    section.OrientationSectionID = (section.OrientationSection != null) ? section.OrientationSection.ID : -1;
                    section.OrientationStateID = (section.OrientationState != null) ? section.OrientationState.ID : -1;
                    section.OrientationArchitectureID = (section.OrientationArchitecture != null) ? section.OrientationArchitecture.ID : -1;
                    section.ArchitecturesString = section.Architectures.SaveToString();
                }
            }

            if (!editing)
            {
                foreach (Architecture architecture in this.Architectures)
                {
                    architecture.KindId = architecture.Kind.ID;
                    architecture.StateID = architecture.LocationState.ID;
                    architecture.CharacteristicsString = architecture.Characteristics.SaveToString();

                    architecture.ArchitectureAreaString = StaticMethods.SaveToString(architecture.ArchitectureArea.Area);

                    architecture.PersonsString = architecture.Persons.SaveToString();
                    architecture.MovingPersonsString = architecture.MovingPersons.SaveToString();
                    architecture.NoFactionPersonsString = architecture.NoFactionPersons.SaveToString();
                    architecture.NoFactionMovingPersonsString = architecture.NoFactionMovingPersons.SaveToString();

                    //row["AgricultureWorkingPersons"] = architecture.AgricultureWorkingPersons.SaveToString();
                    //row["CommerceWorkingPersons"] = architecture.CommerceWorkingPersons.SaveToString();
                    //row["TechnologyWorkingPersons"] = architecture.TechnologyWorkingPersons.SaveToString();
                    //row["DominationWorkingPersons"] = architecture.DominationWorkingPersons.SaveToString();
                    //row["MoraleWorkingPersons"] = architecture.MoraleWorkingPersons.SaveToString();
                    //row["EnduranceWorkingPersons"] = architecture.EnduranceWorkingPersons.SaveToString();
                    //row["zhenzaiWorkingPersons"] = architecture.ZhenzaiWorkingPersons.SaveToString();
                    //row["TrainingWorkingPersons"] = architecture.TrainingWorkingPersons.SaveToString();

                    architecture.feiziliebiaoString = architecture.Feiziliebiao.SaveToString();
                    architecture.MilitariesString = architecture.Militaries.SaveToString();
                    architecture.FacilitiesString = architecture.Facilities.SaveToString();

                    architecture.PlanFacilityKindID = (architecture.PlanFacilityKind != null) ? architecture.PlanFacilityKind.ID : -1;

                    architecture.FundPacksString = architecture.SaveFundPacksToString();
                    architecture.FoodPacksString = architecture.SaveFoodPacksToString();
                    architecture.PopulationPacksString = architecture.SavePopulationPacksToString();

                    architecture.PlanArchitectureID = (architecture.PlanArchitecture != null) ? architecture.PlanArchitecture.ID : -1;

                    architecture.TransferFundArchitectureID = (architecture.TransferFundArchitecture != null) ? architecture.TransferFundArchitecture.ID : -1;

                    architecture.TransferFoodArchitectureID = (architecture.TransferFoodArchitecture != null) ? architecture.TransferFoodArchitecture.ID : -1;

                    architecture.DefensiveLegionID = (architecture.DefensiveLegion != null) ? architecture.DefensiveLegion.ID : -1;

                    architecture.CaptivesString = architecture.Captives.SaveToString();

                    architecture.RobberTroopID = (architecture.RobberTroop != null) ? architecture.RobberTroop.ID : -1;

                    architecture.AILandLinksString = architecture.AILandLinks.SaveToString();

                    architecture.AIWaterLinksString = architecture.AIWaterLinks.SaveToString();

                    //row["zainanleixing"] = architecture.zainan.zainanzhonglei.ID;
                    //row["zainanshengyutianshu"] = architecture.zainan.shengyutianshu;

                    architecture.InformationsString = architecture.Informations.SaveToString();

                    //string s = "";
                    //foreach (Architecture i in architecture.AIBattlingArchitectures)
                    //{
                    //    s += i.ID + " ";
                    //}
                    //row["AIBattlingArchitectures"] = s;
                }
            }

            foreach (Legion legion in this.Legions)
            {
                legion.StartArchitectureString = (legion.StartArchitecture != null) ? legion.StartArchitecture.ID : -1;
                legion.WillArchitectureString = (legion.WillArchitecture != null) ? legion.WillArchitecture.ID : -1;

                legion.PreferredRoutewayString = (legion.PreferredRouteway != null) ? legion.PreferredRouteway.ID : -1;

                legion.CoreTroopString = (legion.CoreTroop != null) ? legion.CoreTroop.ID : -1;

                legion.TroopsString = legion.Troops.SaveToString();
            }

            foreach (Troop troop in this.Troops)
            {
                troop.LeaderIDString = troop.Leader.ID;

                troop.MilitaryID = troop.Army.ID;

                troop.StartingArchitectureString = (troop.StartingArchitecture != null) ? troop.StartingArchitecture.ID : -1;
                troop.PersonsString = troop.SavePersonsToString();

                //row["PositionX"] = troop.Position.X;
                //row["PositionY"] = troop.Position.Y;
                //row["RealDestinationX"] = troop.RealDestination.X;
                //row["RealDestinationY"] = troop.RealDestination.Y;

                troop.WillTroopID = troop.RealWillTroop == null ? -1 : troop.RealWillTroop.ID;
                troop.WillArchitectureID = troop.RealWillArchitecture == null ? -1 : troop.RealWillArchitecture.ID;

                troop.CaptivesString = troop.Captives.SaveToString();

                troop.EventInfluencesString = troop.EventInfluences.SaveToString();

                troop.CombatMethodsString = troop.CombatMethods.SaveToString();

                troop.CurrentStuntIDString = (troop.CurrentStunt != null) ? troop.CurrentStunt.ID : -1;
                
            }

            if (saveMap)
            {
                foreach (TroopEvent event2 in this.TroopEvents)
                {
                    event2.AfterEventHappened = (event2.AfterHappenedEvent != null) ? event2.AfterHappenedEvent.ID : -1;
                    event2.LaunchPersonString = (event2.LaunchPerson != null) ? event2.LaunchPerson.ID : -1;
                    event2.ConditionsString = event2.Conditions.SaveToString();
                    event2.TargetPersonsString = event2.SaveTargetPersonToString();
                    event2.SelfEffectsString = event2.SaveSelfEffectToString();
                    event2.EffectPersonsString = event2.SaveEffectPersonToString();
                    event2.EffectAreasString = event2.SaveEffectAreaToString();
                    event2.dialogString = event2.SaveDialogToString();
                }
            }

            foreach (Routeway routeway in this.Routeways)
            {
                if ((routeway.StartArchitecture != null) && ((routeway.Building || (routeway.LastActivePointIndex >= 0)) || (routeway.StartArchitecture.BelongedSection == null || (!routeway.StartArchitecture.BelongedSection.AIDetail.AutoRun && this.IsPlayer(routeway.StartArchitecture.BelongedFaction)))))
                {
                    routeway.StartArchitectureString = (routeway.StartArchitecture != null) ? routeway.StartArchitecture.ID : -1;
                    routeway.EndArchitectureString = (routeway.EndArchitecture != null) ? routeway.EndArchitecture.ID : -1;
                    routeway.DestinationArchitectureString = (routeway.DestinationArchitecture != null) ? routeway.DestinationArchitecture.ID : -1;
                }
            }

            foreach (Military military in this.Militaries)
            {
                military.FollowedLeaderID = (military.FollowedLeader != null) ? military.FollowedLeader.ID : -1;
                military.LeaderID = (military.Leader != null) ? military.Leader.ID : -1;

                //row["LeaderExperience"] = military.LeaderExperience;

                //row["TrainingPersonID"] = -1;

                military.RecruitmentPersonID = military.RecruitmentPerson == null ? -1 : military.RecruitmentPerson.ID;
                military.ShelledMilitaryID = (military.ShelledMilitary != null) ? military.ShelledMilitary.ID : -1;
            }

            foreach (Captive captive in this.Captives)
            {
                captive.CaptivePersonID = (captive.CaptivePerson != null) ? captive.CaptivePerson.ID : -1;
                captive.CaptiveFactionID = (captive.CaptiveFaction != null) ? captive.CaptiveFaction.ID : -1;
                captive.RansomArchitectureID = (captive.RansomArchitecture != null) ? captive.RansomArchitecture.ID : -1;
            }

            if (!editing)
            {
                ClearTempDic();
            }

            if (!editing)
            {
                foreach (Person person in this.Persons)
                {
                    person.UniqueTitlesString = person.UniqueTitles.SaveToString();
                    person.UniqueMilitaryKindsString = person.UniqueMilitaryKinds.SaveToString();
                    person.IdealTendencyIDString = (person.IdealTendency != null) ? person.IdealTendency.ID : -1;
                    if (person.Character != null)
                    {
                        person.PCharacter = person.Character.ID;
                    }
                    person.UniqueTitlesString = person.UniqueTitles.SaveToString();
                    person.UniqueMilitaryKindsString = person.UniqueMilitaryKinds.SaveToString();

                    //row["Braveness"] = person.BaseBraveness;                    
                    //row["Calmness"] = person.BaseCalmness;
                    //row["Loyalty"] = person.Loyalty;

                    FatherIds[person.ID] = person.Father == null ? -1 : person.Father.ID;
                    MotherIds[person.ID] = person.Mother == null ? -1 : person.Mother.ID;
                    SpouseIds[person.ID] = person.Spouse == null ? -1 : person.Spouse.ID;

                    String brotherStr = "";
                    foreach (Person p in person.Brothers)
                    {
                        brotherStr += p.ID + " ";
                    }

                    String str;
                    char[] separator = separator = new char[] { ' ', '\n', '\r', '\t' };
                    String[] strArray;
                    int[] intArray;
                    try
                    {
                        str = brotherStr;
                        strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        intArray = new int[strArray.Length];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            intArray[i] = int.Parse(strArray[i]);
                        }
                        BrotherIds.Add(person.ID, intArray);
                    }
                    catch
                    {
                        errors.Add("义兄弟一栏应为半型空格分隔的人物ID");
                    }

                    String suoshuStr = "";
                    foreach (Person p in person.suoshurenwuList)
                    {
                        suoshuStr += p.ID + " ";
                    }

                    if (suoshuStr != null)
                    {
                        try
                        {
                            strArray = suoshuStr.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            intArray = new int[strArray.Length];
                            for (int i = 0; i < strArray.Length; i++)
                            {
                                intArray[i] = int.Parse(strArray[i]);
                            }
                            SuoshuIds.Add(person.ID, intArray);
                        }
                        catch
                        {
                            errors.Add("所属人物表一栏应为半型空格分隔的人物ID");
                        }
                    }

                    String closeStr = "";
                    String hatedStr = "";
                    foreach (Person p in person.GetClosePersons())
                    {
                        closeStr += p.ID + " ";
                    }
                    foreach (Person p in person.GetHatedPersons())
                    {
                        hatedStr += p.ID + " ";
                    }

                    try
                    {
                        str = closeStr;
                        strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        intArray = new int[strArray.Length];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            intArray[i] = int.Parse(strArray[i]);
                        }
                        CloseIds.Add(person.ID, intArray);
                    }
                    catch
                    {
                        errors.Add("亲爱武将一栏应为半型空格分隔的人物ID");
                    }

                    try
                    {
                        str = hatedStr;
                        strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        intArray = new int[strArray.Length];
                        for (int i = 0; i < strArray.Length; i++)
                        {
                            intArray[i] = int.Parse(strArray[i]);
                        }
                        HatedIds.Add(person.ID, intArray);
                    }
                    catch
                    {
                        errors.Add("厌恶武将一栏应为半型空格分隔的人物ID");
                    }

                    MarriageGranterId.Add(person.ID, person.marriageGranter != null ? person.marriageGranter.ID : -1);

                    //row["TrainingMilitaryID"] = -1;
                    //row["RecruitmentMilitaryID"] = person.RecruitmentMilitary == null ? -1 : person.RecruitmentMilitary.ID;

                    person.ConvincingPersonID = (person.ConvincingPerson != null) ? person.ConvincingPerson.ID : -1;

                    person.SkillsString = person.Skills.SaveToString();
                    person.RealTitlesString = person.SaveTitleToString();
                    person.StudyingTitleString = (person.StudyingTitle != null) ? person.StudyingTitle.ID : -1;

                    person.StuntsString = person.Stunts.SaveToString();
                    person.StudyingStuntString = (person.StudyingStunt != null) ? person.StudyingStunt.ID : -1;

                    person.waitForFeiziId = (person.WaitForFeiZi != null) ? person.WaitForFeiZi.ID : -1;
                    person.preferredTroopPersonsString = person.preferredTroopPersons.SaveToString();

                    person.TrainPolicyIDString = person.TrainPolicy == null ? -1 : person.TrainPolicy.ID;

                    foreach (KeyValuePair<Person, int> pi in person.GetRelations())
                    {
                        var personIDRelation = new PersonIDRelation()
                        {
                            PersonID1 = person.ID,
                            PersonID2 = pi.Key.ID,
                            Relation = pi.Value
                        };
                        PersonRelationIds.Add(personIDRelation);
                    }
                }
            }
            if(!editing)
            {
                captiveData = this.Captives;
            }

            if (saveMap)
            {
                this.ScenarioMap.MapDataString = ScenarioMap.SaveToString();//修复游戏中编辑地形后无法保存
                foreach (Region region in this.Regions)
                {
                    region.StatesListString = region.States.SaveToString();
                    region.RegionCoreID = (region.RegionCore != null) ? region.RegionCore.ID : -1;
                }

                foreach (State state in this.States)
                {
                    state.ContactStatesString = state.ContactStates.SaveToString();
                    state.StateAdminID = (state.StateAdmin != null) ? state.StateAdmin.ID : -1;
                }
            }

            foreach (Treasure treasure in this.Treasures)
            {
                treasure.BelongedPersonIDString = (treasure.BelongedPerson != null) ? treasure.BelongedPerson.ID : -1;
                treasure.HidePlaceIDString = (treasure.HidePlace != null) ? treasure.HidePlace.ID : -1;
                treasure.InfluencesString = treasure.Influences.SaveToString();
            }

            foreach (YearTableEntry yt in this.YearTable)
            {
                string factionStr = "";
                foreach (Faction f in yt.Factions)
                {
                    if (f != null)
                    {
                        factionStr += f.ID + " ";
                    }
                }
                yt.FactionsString = factionStr;
            }

            if (saveMap && !editing)
            {
                foreach (Event e in this.AllEvents)
                {
                    e.personString = e.SavePersonIdToString();
                    e.PersonCondString = e.SavePersonCondToString();
                    e.architectureString = e.architecture.SaveToString();
                    e.architectureCondString = e.SaveArchitecureCondToString();
                    e.factionString = e.faction.SaveToString();
                    e.factionCondString = e.SaveFactionCondToString();
                    e.dialogString = e.SaveDialogToString();
                    e.effectString = e.SaveEventEffectToString();
                    e.architectureEffectString = e.SaveArchitectureEffectToString();
                    e.factionEffectIDString = e.SaveFactionEffectToString();
                    e.yesdialogString = e.SaveyesDialogToString();
                    e.nodialogString = e.SavenoDialogToString();
                    e.yesEffectString = e.SaveYesEffectToString();
                    e.noEffectString = e.SaveNoEffectToString();
                    e.yesArchitectureEffectString = e.SaveYesArchitectureEffectToString();
                    e.noArchitectureEffectString = e.SaveNoArchitectureEffectToString();
                    e.scenBiographyString = e.SaveScenBiographyToString();
                }
            }

            this.CurrentPlayerID = ((this.CurrentPlayer != null) ? this.CurrentPlayer.ID : -1).ToString();
            if(!editing)
            {
                this.PlayerList = this.PlayerFactions.GameObjects.Select(ob => ob.ID).NullToEmptyList();
                this.PlayerInfo = this.GetPlayerInfo();
            }
            this.Factions.FactionQueue = this.Factions.SaveQueueToString();


            //row["JumpPosition"] = StaticMethods.SaveToString(new Point?(ScenarioMap.JumpPosition));

            if (this.OnAfterSaveScenario != null)
            {
                this.OnAfterSaveScenario();
            }

            foreach (Biography i in this.AllBiographies.Biographys.Values)
            {
                i.MilitaryKindsString = i.MilitaryKinds.SaveToString();
            }

            var scenarioClone = this.Clone();            

            if (saveCommonData || UsingOwnCommonData)
            {
                SaveGameCommonData(scenarioClone);
            }
            else
            {
                scenarioClone.GameCommonData = null;
            }


            if (saveSettings)
            {

            }
            else
            {
                //scenarioClone.Parameters = null;
                //scenarioClone.GlobalVariables = null;
            }

            var saves = LoadScenarioSaves();
            string file = LoadedFileName;
            if (!fullPathProvided)
            {
                file = @"Save\" + LoadedFileName;
            }

            //bool zip = true;

            //if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop)
            //{
            //    zip = false;
            //}

            bool result = SimpleSerializer.SerializeJsonFile(scenarioClone, file, false, false, fullPathProvided);

            if (result)
            {
                int id;

                string name = LoadedFileName.Replace(".json", "");

                if (int.TryParse(name.Replace("Save", ""), out id))
                {
                    string time = scenarioClone.Date.Year + "-" + scenarioClone.Date.Month + "-" + scenarioClone.Date.Day;

                    saves[id] = new Scenario()
                    {
                        Create = DateTime.Now.ToSeasonDateTime(),
                        Desc = scenarioClone.ScenarioDescription,
                        IDs = "",
                        Info = scenarioClone.PlayerInfo,
                        Name = name,
                        Names = "",
                        Path = "",
                        PlayTime = GameTime.ToString(),
                        Player = "",
                        Players = String.Join(",", scenarioClone.PlayerList.NullToEmptyList()),
                        Time = time.ToSeasonDate(),
                        Title = scenarioClone.ScenarioTitle
                    };
                    if(!editing)
                    {
                        SaveScenarioSaves(saves);
                    }
                    else 
                    {
                        string saveDir = @"Save\";
                        string saveFile = saveDir + "Saves.json";
                        SimpleSerializer.SerializeJsonFile(saves, saveFile);
                    }
                }
            }

            scenarioClone = null;

            JustSaved = true;

            //ExtensionInterface.call("Save", new Object[] { this });

            return true;
        }

        public static List<string> LoadGameCommonData()
        {
            var errorMsg = new List<string>();
            
            var conditionKinds = new ConditionKindTable();
            foreach (var conditionKind in CommonData.Current.AllConditionKinds.ConditionKinds)
            {
                int num = conditionKind.Key;
                ConditionKind ck = ConditionKindFactory.CreateConditionKindByID(num);
                if (ck != null)
                {
                    ck.ID = num;
                    ck.Name = conditionKind.Value.Name;
                    conditionKinds.AddConditionKind(ck);
                }
                else
                {
                    errorMsg.Add("条件类型ID" + num + "不存在于游戏中。");
                }
            }
            CommonData.Current.AllConditionKinds = conditionKinds;

            foreach (var condition in CommonData.Current.AllConditions.Conditions)
            {
                var Kind = condition.Value.Kind;
                if (Kind == null)
                {

                }
                else
                {
                    CommonData.Current.AllConditionKinds.ConditionKinds.TryGetValue(Kind.ID, out condition.Value.Kind);
                }
            }

            var influenceKinds = new InfluenceKindTable();
            foreach (var influenceKind in CommonData.Current.AllInfluenceKinds.InfluenceKinds)
            {
                int num = influenceKind.Key;
                InfluenceKind ck = InfluenceKindFactory.CreateInfluenceKindByID(num);
                if (ck != null)
                {
                    ck.ID = num;
                    ck.Name = influenceKind.Value.Name;
                    ck.Type = influenceKind.Value.Type;
                    ck.Combat = influenceKind.Value.Combat;
                    ck.AIPersonValue = influenceKind.Value.AIPersonValue;
                    ck.AIPersonValuePow = influenceKind.Value.AIPersonValuePow;
                    influenceKinds.AddInfluenceKind(ck);
                }
                else
                {
                    errorMsg.Add("条件类型ID" + num + "不存在于游戏中。");
                }
            }
            CommonData.Current.AllInfluenceKinds = influenceKinds;

            foreach (var influence in CommonData.Current.AllInfluences.Influences)
            {
                var kind = influence.Value.Kind;
                if (kind == null)
                {

                }
                else
                {
                    CommonData.Current.AllInfluenceKinds.InfluenceKinds.TryGetValue(kind.ID, out influence.Value.Kind);
                }
            }

            var eventEffectKinds = new EventEffectKindTable();
            foreach (var eventEffectKind in CommonData.Current.AllEventEffectKinds.EventEffectKinds)
            {
                int num = eventEffectKind.Key;
                EventEffectKind ck = EventEffectKindFactory.CreateEventEffectKindByID(num);
                if (ck != null)
                {
                    ck.ID = num;
                    ck.Name = eventEffectKind.Value.Name;
                    eventEffectKinds.AddEventEffectKind(ck);
                }
                else
                {
                    errorMsg.Add("条件类型ID" + num + "不存在于游戏中。");
                }
            }
            CommonData.Current.AllEventEffectKinds = eventEffectKinds;

            foreach (var eventEffect in CommonData.Current.AllEventEffects.EventEffects)
            {
                var kind = eventEffect.Value.Kind;
                if (kind == null)
                {

                }
                else
                {
                    CommonData.Current.AllEventEffectKinds.EventEffectKinds.TryGetValue(kind.ID, out eventEffect.Value.Kind);
                }
            }

            var troopEventEffectKinds = new GameObjects.TroopDetail.EventEffect.EventEffectKindTable();
            foreach (var eventEffectKind in CommonData.Current.AllTroopEventEffectKinds.EventEffectKinds)
            {
                int num = eventEffectKind.Key;
                GameObjects.TroopDetail.EventEffect.EventEffectKind ck = GameObjects.TroopDetail.EventEffect.EventEffectKindFactory.CreateEventEffectKindByID(num);
                if (ck != null)
                {
                    ck.ID = num;
                    ck.Name = eventEffectKind.Value.Name;
                    troopEventEffectKinds.AddEventEffectKind(ck);
                }
                else
                {
                    errorMsg.Add("条件类型ID" + num + "不存在于游戏中。");
                }
            }
            CommonData.Current.AllTroopEventEffectKinds = troopEventEffectKinds;

            foreach (var eventEffect in CommonData.Current.AllTroopEventEffects.EventEffects)
            {
                var kind = eventEffect.Value.Kind;
                if (kind == null)
                {
                    
                }
                else
                {
                    CommonData.Current.AllTroopEventEffectKinds.EventEffectKinds.TryGetValue(kind.ID, out eventEffect.Value.Kind);
                }
            }
            return errorMsg;
        }

        public static void SaveGameCommonData(GameScenario scenario)
        {
            var commonData = scenario.GameCommonData.Clone();
            commonData.AllTitles.Titles = commonData.AllTitles.Titles.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllArchitectureKinds.ArchitectureKinds = commonData.AllArchitectureKinds.ArchitectureKinds.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllBiographyAdjectives = commonData.AllBiographyAdjectives.OrderBy(x => x.ID).ToList();
            commonData.AllCombatMethods.CombatMethods = commonData.AllCombatMethods.CombatMethods.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllConditionKinds.ConditionKinds = commonData.AllConditionKinds.ConditionKinds.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllConditions.Conditions = commonData.AllConditions.Conditions.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllEventEffectKinds.EventEffectKinds = commonData.AllEventEffectKinds.EventEffectKinds.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllEventEffects.EventEffects = commonData.AllEventEffects.EventEffects.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllFacilityKinds.FacilityKinds = commonData.AllFacilityKinds.FacilityKinds.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllInfluenceKinds.InfluenceKinds = commonData.AllInfluenceKinds.InfluenceKinds.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllInfluences.Influences = commonData.AllInfluences.Influences.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllMilitaryKinds.MilitaryKinds= commonData.AllMilitaryKinds.MilitaryKinds.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllSkills.Skills = commonData.AllSkills.Skills.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllStratagems.Stratagems = commonData.AllStratagems.Stratagems.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllStunts.Stunts = commonData.AllStunts.Stunts.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllTechniques.Techniques = commonData.AllTechniques.Techniques.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllTitleKinds.TitleKinds = commonData.AllTitleKinds.TitleKinds.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllTitles.Titles = commonData.AllTitles.Titles.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllTroopEventEffectKinds.EventEffectKinds = commonData.AllTroopEventEffectKinds.EventEffectKinds.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllTroopEventEffects.EventEffects = commonData.AllTroopEventEffects.EventEffects.OrderBy(x => x.Value.ID).ToDictionary(x => x.Key, y => y.Value);
            commonData.AllTextMessages.textMessages= commonData.AllTextMessages.textMessages.OrderBy(x => x.Key.Key).ToDictionary(x => x.Key, y => y.Value);
            var errorMsg = new List<string>();

            var conditionKinds = new ConditionKindTable();
            foreach (var conditionKind in commonData.AllConditionKinds.ConditionKinds)
            {
                int num = conditionKind.Key;
                ConditionKind ck = new ConditionKind(); // ConditionKindFactory.CreateConditionKindByID(num);
                if (ck != null)
                {
                    ck.ID = num;
                    ck.Name = conditionKind.Value.Name;
                    conditionKinds.AddConditionKind(ck);
                }
                else
                {
                    errorMsg.Add("条件类型ID" + num + "不存在于游戏中。");
                }
            }
            commonData.AllConditionKinds = conditionKinds;

            var allConditions = new ConditionTable();
            foreach (var condition in commonData.AllConditions.Conditions)
            {
                var conditionClone = condition.Value.Clone();
                if (conditionClone.Kind == null)
                {

                }
                else
                {
                    commonData.AllConditionKinds.ConditionKinds.TryGetValue(conditionClone.Kind.ID, out conditionClone.Kind);
                    allConditions.AddCondition(conditionClone);
                }
            }
            commonData.AllConditions = allConditions;

            var influenceKinds = new InfluenceKindTable();
            foreach (var influenceKind in commonData.AllInfluenceKinds.InfluenceKinds)
            {
                int num = influenceKind.Key;
                InfluenceKind ck = new InfluenceKind(); // InfluenceKindFactory.CreateInfluenceKindByID(num);
                if (ck != null)
                {
                    ck.ID = num;
                    ck.Type = influenceKind.Value.Type;
                    ck.Name = influenceKind.Value.Name;
                    ck.Combat = influenceKind.Value.Combat;
                    ck.AIPersonValue = influenceKind.Value.AIPersonValue;
                    ck.AIPersonValuePow = influenceKind.Value.AIPersonValuePow;
                    influenceKinds.AddInfluenceKind(ck);
                }
                else
                {
                    errorMsg.Add("条件类型ID" + num + "不存在于游戏中。");
                }
            }
            commonData.AllInfluenceKinds = influenceKinds;

            var allInfluences = new InfluenceTable();
            foreach (var influence in commonData.AllInfluences.Influences)
            {
                var inf = influence.Value.Clone();
                commonData.AllInfluenceKinds.InfluenceKinds.TryGetValue(inf.Kind.ID, out inf.Kind);
                allInfluences.AddInfluence(inf);
            }
            commonData.AllInfluences = allInfluences;

            var eventEffectKinds = new EventEffectKindTable();
            foreach (var eventEffectKind in commonData.AllEventEffectKinds.EventEffectKinds)
            {
                int num = eventEffectKind.Key;
                EventEffectKind ck = new EventEffectKind(); // EventEffectKindFactory.CreateEventEffectKindByID(num);
                if (ck != null)
                {
                    ck.ID = num;
                    ck.Name = eventEffectKind.Value.Name;
                    eventEffectKinds.AddEventEffectKind(ck);
                }
                else
                {
                    errorMsg.Add("条件类型ID" + num + "不存在于游戏中。");
                }
            }
            commonData.AllEventEffectKinds = eventEffectKinds;

            var eventEffects = new EventEffectTable();
            foreach (var eventEffect in commonData.AllEventEffects.EventEffects)
            {
                var eve = eventEffect.Value.Clone();
                if (eve.Kind == null)
                {

                }
                else
                {
                    commonData.AllEventEffectKinds.EventEffectKinds.TryGetValue(eve.Kind.ID, out eve.Kind);
                    eventEffects.AddEventEffect(eve);
                }
            }
            commonData.AllEventEffects = eventEffects;

            var troopEventEffectKinds = new GameObjects.TroopDetail.EventEffect.EventEffectKindTable();
            foreach (var eventEffectKind in commonData.AllTroopEventEffectKinds.EventEffectKinds)
            {
                int num = eventEffectKind.Key;
                GameObjects.TroopDetail.EventEffect.EventEffectKind ck = new TroopDetail.EventEffect.EventEffectKind(); // GameObjects.TroopDetail.EventEffect.EventEffectKindFactory.CreateEventEffectKindByID(num);
                if (ck != null)
                {
                    ck.ID = num;
                    ck.Name = eventEffectKind.Value.Name;
                    troopEventEffectKinds.AddEventEffectKind(ck);
                }
                else
                {
                    errorMsg.Add("条件类型ID" + num + "不存在于游戏中。");
                }
            }
            commonData.AllTroopEventEffectKinds = troopEventEffectKinds;

            var allTroopEventEffects = new TroopDetail.EventEffect.EventEffectTable();
            foreach (var eventEffect in commonData.AllTroopEventEffects.EventEffects)
            {
                var eve = eventEffect.Value.Clone();
                if (eve.Kind == null)
                {

                }
                else
                {
                    commonData.AllTroopEventEffectKinds.EventEffectKinds.TryGetValue(eve.Kind.ID, out eve.Kind);
                    allTroopEventEffects.AddEventEffect(eve);
                }
            }
            commonData.AllTroopEventEffects = allTroopEventEffects;

            scenario.GameCommonData = commonData;
        }

        public static List<Scenario> LoadScenarioSaves()
        {
            string saveDir = @"Save\";

            if (!Platform.Current.UserDirectoryExist(saveDir))
            {
                Platform.Current.UserDirectoryCreate(saveDir);
            }

            string saveFile = saveDir + "Saves.json";

            List<Scenario> scesList = null;

            if (Platform.Current.UserFileExist(new string[] { saveFile })[0])
            {
                scesList = SimpleSerializer.DeserializeJsonFile<List<Scenario>>(saveFile, true).NullToEmptyList();
            }
            
            if (scesList == null)
            {
                scesList = new List<Scenario>();

                for (int i = 0; i <= savemaxcounts+1; i++)
                {
                    var sce = new Scenario()
                    {
                        ID = i < 10 ? "0" + i.ToString() : i.ToString()
                    };
                    scesList.Add(sce);
                }
            }
            else if(scesList.Count<=GameScenario.savemaxcounts)
            {
                for (int i = scesList.Count; i <= savemaxcounts ; i++)
                {
                    var sce = new Scenario()
                    {
                        ID = i < 10 ? "0" + i.ToString() : i.ToString()
                    };
                    scesList.Add(sce);
                }
            }

            return scesList;
        }

        public static void SaveScenarioSaves(List<Scenario> saves)
        {
            string saveDir = @"Save\";
            string saveFile = saveDir + "Saves.json";

            SimpleSerializer.SerializeJsonFile(saves, saveFile);

            if (Session.MainGame.mainMenuScreen.MenuType == WorldOfTheThreeKingdoms.GameScreens.MenuType.Save)
            {
                Session.MainGame.mainMenuScreen.InitScenarioSaveList();
            }
        }

        public void DisposeLotsOfMemory()
        {
            //foreach (MilitaryKind kind in this.GameCommonData.AllMilitaryKinds.MilitaryKinds.Values)
            //{
            //    kind.Textures.Dispose();
            //}
            //foreach (Animation a in this.GameCommonData.AllTroopAnimations.Animations.Values)
            //{
            //    a.disposeTexture();
            //}
            //foreach (Architecture a in this.Architectures)
            //{
            //    if (a.CaptionTexture != null)
            //    {
            //        a.CaptionTexture.Dispose();
            //        a.CaptionTexture = null;
            //    }
            //}
            //foreach (ArchitectureKind k in this.GameCommonData.AllArchitectureKinds.ArchitectureKinds.Values)
            //{
            //    if (k.Texture != null)
            //    {
            //        k.ClearTexture();
            //    }
            //}
            //foreach (Treasure t in this.Treasures)
            //{
            //    t.disposeTexture();
            //}
            //foreach (TerrainDetail t in this.GameCommonData.AllTerrainDetails.TerrainDetails.Values)
            //{
            //    if (t.Textures != null)
            //    {
            //        //foreach (var u in t.Textures.BasicTextures)
            //        //{
            //        //    u.Dispose();
            //        //}
            //        foreach (Texture u in t.Textures.BottomEdgeTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.BottomLeftCornerTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.BottomLeftTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.BottomRightCornerTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.BottomRightTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.BottomTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.CentreTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.LeftEdgeTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.LeftTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.RightEdgeTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.RightTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.LeftEdgeTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.TopEdgeTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.TopLeftCornerTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.TopLeftTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.TopRightCornerTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.TopRightTextures)
            //        {
            //            u.Dispose();
            //        }
            //        foreach (Texture u in t.Textures.TopTextures)
            //        {
            //            u.Dispose();
            //        }
            //    }
            //    t.Textures = null;
            //}

            if (Session.MainGame != null && Session.MainGame.mainGameScreen != null)
            {
                Session.MainGame.mainGameScreen.DisposeMapTileMemory(true, false);
            }
        }

        public void SetMapTileArchitecture(Architecture architecture)
        {
            if (!architecture.AutoRefillFoodInLongViewArea)
            {
                architecture.AddBaseSupplyingArchitecture();
            }
            foreach (Point point in architecture.ViewArea.Area)
            {
                if (!this.PositionOutOfRange(point))
                {
                    this.MapTileData[point.X, point.Y].AddHighViewingArchitecture(architecture);
                }
            }
            foreach (Point point in architecture.LongViewArea.Area)
            {
                if (!this.PositionOutOfRange(point))
                {
                    this.MapTileData[point.X, point.Y].AddViewingArchitecture(architecture);
                }
            }
        }

        public void SetMapTileTroop(Troop troop)
        {
            if (this.MapTileData[troop.PreviousPosition.X, troop.PreviousPosition.Y].TroopCount > 0)
            {
                TileData data1 = this.MapTileData[troop.PreviousPosition.X, troop.PreviousPosition.Y];
                data1.TroopCount--;
            }
            if (this.MapTileData[troop.PreviousPosition.X, troop.PreviousPosition.Y].TileTroop == troop)
            {
                this.MapTileData[troop.PreviousPosition.X, troop.PreviousPosition.Y].TileTroop = null;
                /*foreach (Troop t in this.Troops)
                {
                    if (!t.Destroyed && t.Position == troop.PreviousPosition)
                    {
                        this.MapTileData[troop.PreviousPosition.X, troop.PreviousPosition.Y].TileTroop = t;
                        break;
                    }
                }*/
            }
            TileData data2 = this.MapTileData[troop.Position.X, troop.Position.Y];
            data2.TroopCount++;
            if (this.MapTileData[troop.Position.X, troop.Position.Y].TileTroop == null)
            {
                this.MapTileData[troop.Position.X, troop.Position.Y].TileTroop = troop;
            }
        }

        public void SetPenalizedMapDataByArea(GameArea gameArea, int cost)
        {
            foreach (Point point in gameArea.Area)
            {
                if (!this.PositionOutOfRange(point))
                {
                    this.PenalizedMapData[point.X, point.Y] = cost;
                }
            }
            this.SetPenalizedMapDataByPosition(gameArea.Centre, 0xdac);
        }

        public void SetPenalizedMapDataByPosition(Point position, int cost)
        {
            this.PenalizedMapData[position.X, position.Y] = cost;
        }

        public void SetPlayerFactionList(GameObjectList factions)
        {
            this.PlayerFactions.Clear();
            if (factions != null)
            {
                foreach (Faction faction in factions)
                {
                    this.PlayerFactions.Add(faction);
                }
            }
        }

        public void SetPositionOnFire(Point position)
        {
            this.FireTable.AddPosition(position);
            this.GeneratorOfTileAnimation.AddTileAnimation(TileAnimationKind.火焰, position, true);
        }

        public void YearPassedEvent()
        {
            ExtensionInterface.call("YearEvent", new Object[] { this });
            foreach (Architecture architecture in this.Architectures.GetRandomList())
            {
                architecture.YearEvent();
            }

            foreach (Faction faction in this.Factions)
            {
                faction.YearOfficialLimit = 0;
            }
            foreach (Person p in this.Persons)
            {
                if (p.Available && p.IsGeneratedChildren && p.Age >= Session.GlobalVariables.ChildrenAvailableAge)
                {
                    p.IsGeneratedChildren = false;
                }
            }
        }

        public void YearStartingEvent()
        {
        }

        public bool Animating
        {
            get
            {
                return this.Troops.HasAnimatingTroop;
            }
        }

        public Person NeutralPerson
        {
            get
            {
                if (this.neutralPerson == null)
                {
                    this.neutralPerson = this.Persons.GetGameObject(0x1b5f) as Person;
                }
                return this.neutralPerson;
            }
        }

        public bool NoCurrentPlayer
        {
            get
            {
                return (this.CurrentPlayer == null);
            }
        }

        public TroopAnimation TroopAnimations
        {
            get
            {
                return this.GameCommonData.TroopAnimations;
            }
        }

        private Architecture huangdisuozai = null;
        public Architecture huangdisuozaijianzhu()
        {
            if (huangdisuozai == null)
            {
                foreach (Architecture a in this.Architectures)
                {
                    if (a.huangdisuozai) huangdisuozai = a;
                }
            }
            return huangdisuozai;
        }

        public bool youhuangdi()
        {
            foreach (Architecture a in this.Architectures)
            {
                if (a.huangdisuozai) return true;
            }
            return false;
        }

        public delegate void AfterLoadScenario();

        public delegate void AfterSaveScenario();

        public delegate void NewFactionAppear(Faction faction);

        public void BecomeNoEmperor()
        {
            foreach (Architecture a in this.Architectures)
            {
                if (a.huangdisuozai)
                {
                    a.huangdisuozai = false;
                    this.huangdisuozai = null;
                }
            }

            Person neutralPerson = this.NeutralPerson;
            if (neutralPerson == null)
            {
                if (this.CurrentPlayer != null)
                {
                    neutralPerson = this.CurrentPlayer.Leader;
                }
                else
                {
                    if (this.Factions.Count <= 0)
                    {
                        return;
                    }
                    neutralPerson = (this.Factions[0] as Faction).Leader;
                }
            }

            Session.MainGame.mainGameScreen.xianshishijiantupian(neutralPerson, "汉朝", "FactionDestroy", "shilimiewang.jpg", "shilimiewang", true);

        }

        public YearTable getFactionYearTable(Faction f)
        {
            YearTable result = new YearTable();
            foreach (YearTableEntry i in this.YearTable)
            {
                if (i.IsGloballyKnown || i.Factions.GameObjects.Contains(f) || Session.GlobalVariables.SkyEye)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public YearTable getFactionYearTableRecentYears(Faction f, int y)
        {
            YearTable result = new YearTable();
            foreach (YearTableEntry i in this.YearTable)
            {
                if ((i.IsGloballyKnown || i.Factions.GameObjects.Contains(f) || Session.GlobalVariables.SkyEye) &&
                    i.Date.Year > this.Date.Year - y)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public YearTable getOnlyFactionYearTable(Faction f)
        {
            YearTable result = new YearTable();
            foreach (YearTableEntry i in this.YearTable)
            {
                if (i.Factions.GameObjects.Contains(f))
                {
                    result.Add(i);
                }
            }
            return result;
        }
        public bool runScenarioStart(Architecture triggerArch, Screen screen)
        {
            bool ran = false;
            foreach (Event e in this.AllEvents)
            {
                if ((e.IsStart() && e.matchEventPersons(triggerArch)) || e.checkConditions(triggerArch))
                {
                    if (!this.EventsToApply.ContainsKey(e))
                    {
                        this.EventsToApply.Add(e, triggerArch);
                        e.ApplyEventDialogs(triggerArch, screen);
                        ran = true;
                    }
                    if (!this.YesEventsToApply.ContainsKey(e) && e.yesEffect.Count > 0)
                    {
                        this.YesEventsToApply.Add(e, triggerArch);
                        ran = true;
                    }
                    if (!this.NoEventsToApply.ContainsKey(e) && e.noEffect.Count > 0)
                    {
                        this.NoEventsToApply.Add(e, triggerArch);
                        ran = true;
                    }
                    /*
                    if (!this.YesArchiEventsToApply.ContainsKey(e))
                    {
                        this.YesArchiEventsToApply.Add(e, triggerArch);

                        e.ApplyEventDialogs(triggerArch);
                        ran = true;
                    }
                    if (!this.NoArchiEventsToApply.ContainsKey(e))
                    {
                        this.NoArchiEventsToApply.Add(e, triggerArch);
                        e.ApplyEventDialogs(triggerArch);
                        ran = true;
                    }
                    */
                }
            }
            return ran;
        }

        public bool runScenarioEnd(Architecture triggerArch, Screen screen)
        {
            bool ran = false;
            foreach (Event e in this.AllEvents)
            {
                if ((e.IsEnd() && e.matchEventPersons(triggerArch)) || e.checkConditions(triggerArch))
                {
                    if (!this.EventsToApply.ContainsKey(e))
                    {
                        this.EventsToApply.Add(e, triggerArch);
                        e.ApplyEventDialogs(triggerArch, screen);
                        ran = true;
                    }

                    if (!this.YesEventsToApply.ContainsKey(e) && e.yesEffect.Count > 0)
                    {
                        this.YesEventsToApply.Add(e, triggerArch);
                        ran = true;
                    }
                    if (!this.NoEventsToApply.ContainsKey(e) && e.noEffect.Count > 0)
                    {
                        this.NoEventsToApply.Add(e, triggerArch);
                        ran = true;
                    }
                    /*
                    if (!this.YesArchiEventsToApply.ContainsKey(e))
                    {
                        this.YesArchiEventsToApply.Add(e, triggerArch);

                        e.ApplyEventDialogs(triggerArch);
                        ran = true;
                    }
                    if (!this.NoArchiEventsToApply.ContainsKey(e))
                    {
                        this.NoArchiEventsToApply.Add(e, triggerArch);
                        e.ApplyEventDialogs(triggerArch);
                        ran = true;
                    }*/
                }
            }
            return ran;
        }

        public PersonList Officers() //野武将列表
        {
            PersonList result = new PersonList();
            foreach (Person person in this.Persons)
            {
                if (person.Available && person.Alive)
                {
                    if (person.ID >= 25000)
                    {
                        result.Add(person);
                    }
                }

            }

            return result;
        }

        public int OfficerCount //野武将总数
        {
            get
            {
                return (this.Officers().Count);
            }
        }

        public int OfficerLimit
        {
            get
            {
                return Session.GlobalVariables.zhaoxianOfficerMax;
            }
        }

        public int GetAITroopCount()
        {
            int cnt = 0;
            foreach (Troop t in this.Troops)
            {
                if (!this.IsPlayer(t.BelongedFaction))
                {
                    cnt++;
                }
            }
            return cnt;
        }

        public bool IsKnownToAnyPlayer(Architecture a)
        {
            if (Session.GlobalVariables.SkyEye) return true;
            foreach (Faction f in this.PlayerFactions)
            {
                if (f.IsArchitectureKnown(a)) return true;
            }
            return false;
        }

        public bool IsKnownToAnyPlayer(Troop a)
        {
            if (Session.GlobalVariables.SkyEye) return true;
            foreach (Faction f in this.PlayerFactions)
            {
                if (f.IsTroopKnown(a)) return true;
            }
            return false;
        }

        public void TrainChildren()
        {
            foreach (Person p in this.Persons)
            {
                //if (p.Trainable && GameObject.Random(30) == 0)
                if (p.Trainable && GameObject.Random((int)(20 / (IsPlayer(p.Father.BelongedFaction) ? 1 : Session.Current.Scenario.Parameters.AIExtraPerson) / Session.Parameters.DayInTurn)) == 0)
                {
                    if (p.TrainPolicy == null)
                    {
                        p.TrainPolicy = (TrainPolicy)this.GameCommonData.AllTrainPolicies.GetGameObject(1);
                    }
                    Dictionary<int, float> weighting = p.TrainPolicy.Weighting;
                    if (p.Age < 8) // No attempt to learn title until age 8
                    {
                        weighting.Remove(8);
                    }
                    int r = GameObject.WeightedRandom(weighting);

                    Person parental = p.Father;
                    if (!parental.IsValidTeacher)
                    {
                        parental = p.Mother;
                    }
                    if (!parental.IsValidTeacher)
                    {
                        GameObjectList candidate = new GameObjectList();
                        foreach (Person q in this.Persons)
                        {
                            if (q.IsValidTeacher && ((q.Father == p.Father) || (q.Mother == p.Mother)) && q != p)
                            {
                                candidate.Add(q);
                            }
                        }
                        candidate.PropertyName = "Age";
                        candidate.IsNumber = true;
                        candidate.SmallToBig = false;
                        candidate.ReSort();
                        if (candidate.Count > 0)
                        {
                            parental = (Person)candidate[0];
                        }
                    }
                    if (!parental.IsValidTeacher)
                    {
                        GameObjectList candidate = new GameObjectList();
                        foreach (Person q in this.Persons)
                        {
                            if (q.IsValidTeacher && q.HasStrainTo(p) && q != p && q.IsValidTeacher)
                            {
                                candidate.Add(q);
                            }
                        }
                        candidate.PropertyName = "Age";
                        candidate.IsNumber = true;
                        candidate.SmallToBig = false;
                        candidate.ReSort();
                        if (candidate.Count > 0)
                        {
                            parental = (Person)candidate[0];
                        }
                    }

                    int siblingCount = 0;

                    if (parental != null && parental.LocationArchitecture != null)
                    {
                        foreach (Person q in parental.LocationArchitecture.PersonAndChildren)
                        {
                            if (q.Trainable)
                            {
                                siblingCount++;
                            }
                        }
                    }

                    siblingCount++;

                    switch (r)
                    {
                        case 1:
                            {
                                PersonList teachers = new PersonList();

                                if (parental != null && parental.BelongedArchitecture != null)
                                {
                                    GameObjectList list = parental.BelongedArchitecture.PersonAndChildren.GetRandomList();
                                    list.AddRange(parental.BelongedArchitecture.NoFactionPersons);
                                    foreach (Person q in list)
                                    {
                                        if (q.Strength > p.Strength && q.Age > 8 && q.IsValidTeacher && (GameObject.Chance(100 / (list.Count + siblingCount)) || q == parental || q == p.Father || q == p.Mother))
                                        {
                                            if (q.HasStrainTo(p) || q.Closes(p.Father) || q.Closes(p.Mother) || q.Closes(parental) || GameObject.Chance(q.GetRelation(parental) / 20))
                                            {
                                                teachers.Add(q);
                                            }
                                        }
                                    }

                                }
                                foreach (Person q in teachers)
                                {
                                    if (p.Hates(q)) continue;
                                    if (q.Hates(p)) continue;
                                    if (GameObject.Chance(85 - q.Ambition * 15) && (q.Hates(p.Father) || q.Hates(p.Mother) || p.Father.Hates(q) || p.Mother.Hates(q))) continue;
                                    if (GameObject.Chance((int)((q.Strength - p.Strength + 50 + q.childrenAbilityIncrease) * ((float)p.StrengthPotential / p.Strength))))
                                    {
                                        p.Strength += GameObject.Random(Math.Max((p.StrengthPotential * 6 / 5 - p.Strength) / 10, 1) + 1);
                                        p.AdjustRelation(q, 0, 5);
                                        q.AdjustRelation(p, 0, 5);
                                        if (GameObject.Chance(30))
                                        {
                                            Dictionary<Person, int> rels = q.GetAllRelations();
                                            foreach (KeyValuePair<Person, int> rel in rels)
                                            {
                                                if (GameObject.Chance(100 / rels.Count))
                                                {
                                                    p.AdjustRelation(rel.Key, 0, Math.Min(5, rel.Value / 10));
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case 2:
                            {
                                PersonList teachers = new PersonList();

                                if (parental != null && parental.BelongedArchitecture != null)
                                {
                                    GameObjectList list = parental.BelongedArchitecture.PersonAndChildren.GetRandomList();
                                    list.AddRange(parental.BelongedArchitecture.NoFactionPersons);
                                    foreach (Person q in list)
                                    {
                                        if (q.Command > p.Command && q.Age > 8 && q.IsValidTeacher && (GameObject.Chance(100 / (list.Count + siblingCount)) || q == parental || q == p.Father || q == p.Mother))
                                        {
                                            if (q.HasStrainTo(p) || q.Closes(p.Father) || q.Closes(p.Mother) || q.Closes(parental) || GameObject.Chance(q.GetRelation(parental) / 20))
                                            {
                                                teachers.Add(q);
                                            }
                                        }
                                    }

                                }
                                foreach (Person q in teachers)
                                {
                                    if (p.Hates(q)) continue;
                                    if (q.Hates(p)) continue;
                                    if (GameObject.Chance(85 - q.Ambition * 15) && (q.Hates(p.Father) || q.Hates(p.Mother) || p.Father.Hates(q) || p.Mother.Hates(q))) continue;
                                    if (GameObject.Chance((int)((q.Command - p.Command + 50 + q.childrenAbilityIncrease) * ((float)p.CommandPotential / p.Command))))
                                    {
                                        p.Command += GameObject.Random(Math.Max((p.CommandPotential * 6 / 5 - p.Command) / 10, 1) + 1);
                                        p.AdjustRelation(q, 0, 5);
                                        q.AdjustRelation(p, 0, 5);
                                        if (GameObject.Chance(30))
                                        {
                                            Dictionary<Person, int> rels = q.GetAllRelations();
                                            foreach (KeyValuePair<Person, int> rel in rels)
                                            {
                                                if (GameObject.Chance(100 / rels.Count))
                                                {
                                                    p.AdjustRelation(rel.Key, 0, Math.Min(5, rel.Value / 10));
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case 3:
                            {
                                PersonList teachers = new PersonList();

                                if (parental != null && parental.BelongedArchitecture != null)
                                {
                                    GameObjectList list = parental.BelongedArchitecture.PersonAndChildren.GetRandomList();
                                    list.AddRange(parental.BelongedArchitecture.NoFactionPersons);
                                    foreach (Person q in list)
                                    {
                                        if (q.Intelligence > p.Intelligence && q.Age > 8 && q.IsValidTeacher && (GameObject.Chance(100 / (list.Count + siblingCount)) || q == parental || q == p.Father || q == p.Mother))
                                        {
                                            if (q.HasStrainTo(p) || q.Closes(p.Father) || q.Closes(p.Mother) || q.Closes(parental) || GameObject.Chance(q.GetRelation(parental) / 20))
                                            {
                                                teachers.Add(q);
                                            }
                                        }
                                    }
                                }
                                foreach (Person q in teachers)
                                {
                                    if (p.Hates(q)) continue;
                                    if (q.Hates(p)) continue;
                                    if (GameObject.Chance(85 - q.Ambition * 15) && (q.Hates(p.Father) || q.Hates(p.Mother) || p.Father.Hates(q) || p.Mother.Hates(q))) continue;
                                    if (GameObject.Chance((int)((q.Intelligence - p.Intelligence + 50 + q.childrenAbilityIncrease) * ((float)p.IntelligencePotential / p.Intelligence))))
                                    {
                                        p.Intelligence += GameObject.Random(Math.Max((p.IntelligencePotential * 6 / 5 - p.Intelligence) / 10, 1) + 1);
                                        p.AdjustRelation(q, 0, 5);
                                        q.AdjustRelation(p, 0, 5);
                                        if (GameObject.Chance(30))
                                        {
                                            Dictionary<Person, int> rels = q.GetAllRelations();
                                            foreach (KeyValuePair<Person, int> rel in rels)
                                            {
                                                if (GameObject.Chance(100 / rels.Count))
                                                {
                                                    p.AdjustRelation(rel.Key, 0, Math.Min(5, rel.Value / 10));
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case 4:
                            {
                                PersonList teachers = new PersonList();

                                if (parental != null && parental.BelongedArchitecture != null)
                                {
                                    GameObjectList list = parental.BelongedArchitecture.PersonAndChildren.GetRandomList();
                                    list.AddRange(parental.BelongedArchitecture.NoFactionPersons);
                                    foreach (Person q in list)
                                    {
                                        if (q.Politics > p.Politics && q.Age > 8 && q.IsValidTeacher && (GameObject.Chance(100 / (list.Count + siblingCount)) || q == parental || q == p.Father || q == p.Mother))
                                        {
                                            if (q.HasStrainTo(p) || q.Closes(p.Father) || q.Closes(p.Mother) || q.Closes(parental) || GameObject.Chance(q.GetRelation(parental) / 20))
                                            {
                                                teachers.Add(q);
                                            }
                                        }
                                    }

                                }
                                foreach (Person q in teachers)
                                {
                                    if (p.Hates(q)) continue;
                                    if (q.Hates(p)) continue;
                                    if (GameObject.Chance(85 - q.Ambition * 15) && (q.Hates(p.Father) || q.Hates(p.Mother) || p.Father.Hates(q) || p.Mother.Hates(q))) continue;
                                    if (GameObject.Chance((int)((q.Politics - p.Politics + 50 + q.childrenAbilityIncrease) * ((float)p.PoliticsPotential / p.Politics))))
                                    {
                                        p.Politics += GameObject.Random(Math.Max((p.PoliticsPotential * 6 / 5 - p.Politics) / 10, 1) + 1);
                                        p.AdjustRelation(q, 0, 5);
                                        q.AdjustRelation(p, 0, 5);
                                        if (GameObject.Chance(30))
                                        {
                                            Dictionary<Person, int> rels = q.GetAllRelations();
                                            foreach (KeyValuePair<Person, int> rel in rels)
                                            {
                                                if (GameObject.Chance(100 / rels.Count))
                                                {
                                                    p.AdjustRelation(rel.Key, 0, Math.Min(5, rel.Value / 10));
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case 5:
                            {
                                PersonList teachers = new PersonList();

                                if (parental != null && parental.BelongedArchitecture != null)
                                {
                                    GameObjectList list = parental.BelongedArchitecture.PersonAndChildren.GetRandomList();
                                    list.AddRange(parental.BelongedArchitecture.NoFactionPersons);
                                    foreach (Person q in list)
                                    {
                                        if (q.Glamour > p.Glamour && q.Age > 8 && q.IsValidTeacher && (GameObject.Chance(100 / (list.Count + siblingCount)) || q == parental || q == p.Father || q == p.Mother))
                                        {
                                            if (q.HasStrainTo(p) || q.Closes(p.Father) || q.Closes(p.Mother) || q.Closes(parental) || GameObject.Chance(q.GetRelation(parental) / 20))
                                            {
                                                teachers.Add(q);
                                            }
                                        }
                                    }

                                }
                                foreach (Person q in teachers)
                                {
                                    if (p.Hates(q)) continue;
                                    if (q.Hates(p)) continue;
                                    if (GameObject.Chance(85 - q.Ambition * 15) && (q.Hates(p.Father) || q.Hates(p.Mother) || p.Father.Hates(q) || p.Mother.Hates(q))) continue;
                                    if (GameObject.Chance((int)((q.Glamour - p.Glamour + 50 + q.childrenAbilityIncrease) * ((float)p.GlamourPotential / p.Glamour))))
                                    {
                                        p.Glamour += GameObject.Random(Math.Max((p.GlamourPotential * 6 / 5 - p.Glamour) / 10, 1) + 1);
                                        p.AdjustRelation(q, 0, 5);
                                        q.AdjustRelation(p, 0, 5);
                                        if (GameObject.Chance(30))
                                        {
                                            Dictionary<Person, int> rels = q.GetAllRelations();
                                            foreach (KeyValuePair<Person, int> rel in rels)
                                            {
                                                if (GameObject.Chance(100 / rels.Count))
                                                {
                                                    p.AdjustRelation(rel.Key, 0, Math.Min(5, rel.Value / 10));
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case 6:
                            {
                                PersonList teachers = new PersonList();

                                if (parental != null && parental.BelongedArchitecture != null)
                                {
                                    GameObjectList list = parental.BelongedArchitecture.PersonAndChildren.GetRandomList();
                                    list.AddRange(parental.BelongedArchitecture.NoFactionPersons);
                                    foreach (Person q in list)
                                    {
                                        if ((GameObject.Chance(100 / (list.Count + siblingCount)) || q == parental || q == p.Father || q == p.Mother) && q.IsValidTeacher && q.Age > 8)
                                        {
                                            if (q.HasStrainTo(p) || q.Closes(p.Father) || q.Closes(p.Mother) || q.Closes(parental) || GameObject.Chance(q.GetRelation(parental) / 20))
                                            {
                                                teachers.Add(q);
                                            }
                                        }
                                    }
                                }

                                foreach (Person q in teachers)
                                {
                                    if (p.Hates(q)) continue;
                                    if (q.Hates(p)) continue;
                                    if (GameObject.Chance(85 - q.Ambition * 15) && (q.Hates(p.Father) || q.Hates(p.Mother) || p.Father.Hates(q) || p.Mother.Hates(q))) continue;
                                    if (q.Skills.Count <= 0) continue;
                                    List<Skill> skillToTeach = new List<Skill>();
                                    foreach (Skill s in q.Skills.Skills.Values)
                                    {
                                        if (s.CanBeBorn(p))
                                        {
                                            skillToTeach.Add(s);
                                        }
                                    }
                                    List<Skill> candidates = new List<Skill>();
                                    foreach (Skill s in this.GameCommonData.AllSkills.Skills.Values)
                                    {
                                        if (s.CanBeBorn(p) && GameObject.Chance((s.GetRelatedAbility(q) - 70) / 5) && GameObject.Chance(100 / s.Level))
                                        {
                                            skillToTeach.Add(s);
                                        }
                                    }

                                    List<Skill> realSkillToTeach = new List<Skill>();
                                    realSkillToTeach.Add(skillToTeach[GameObject.Random(skillToTeach.Count)]);
                                    realSkillToTeach.Add(skillToTeach[GameObject.Random(skillToTeach.Count)]);
                                    realSkillToTeach.Add(skillToTeach[GameObject.Random(skillToTeach.Count)]);

                                    foreach (Skill t in realSkillToTeach)
                                    {
                                        int extraChance = 0;
                                        if (p.Father.GetSkillList().GameObjects.Contains(t) || p.Mother.GetSkillList().GameObjects.Contains(t))
                                        {
                                            extraChance += 5;
                                        }
                                        if (GameObject.Chance(100 / t.Level + q.childrenSkillChanceIncrease + extraChance))
                                        {
                                            p.Skills.AddSkill(t);
                                            p.AdjustRelation(q, 0, 5);
                                            q.AdjustRelation(p, 0, 5);
                                            if (GameObject.Chance(30))
                                            {
                                                Dictionary<Person, int> rels = q.GetAllRelations();
                                                foreach (KeyValuePair<Person, int> rel in rels)
                                                {
                                                    if (GameObject.Chance(100 / rels.Count))
                                                    {
                                                        p.AdjustRelation(rel.Key, 0, Math.Min(5, rel.Value / 10));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case 7:
                            {
                                PersonList teachers = new PersonList();

                                if (parental != null && parental.BelongedArchitecture != null)
                                {
                                    GameObjectList list = parental.BelongedArchitecture.PersonAndChildren.GetRandomList();
                                    list.AddRange(parental.BelongedArchitecture.NoFactionPersons);
                                    foreach (Person q in list)
                                    {
                                        if ((GameObject.Chance(100 / (list.Count + siblingCount)) || q == parental || q == p.Father || q == p.Mother) && q.IsValidTeacher && q.Age > 8)
                                        {
                                            if (q.HasStrainTo(p) || q.Closes(p.Father) || q.Closes(p.Mother) || q.Closes(parental) || GameObject.Chance(q.GetRelation(parental) / 20))
                                            {
                                                teachers.Add(q);
                                            }
                                        }
                                    }

                                }

                                foreach (Person q in teachers)
                                {
                                    if (p.Hates(q)) continue;
                                    if (q.Hates(p)) continue;
                                    if (GameObject.Chance(85 - q.Ambition * 15) && (q.Hates(p.Father) || q.Hates(p.Mother) || p.Father.Hates(q) || p.Mother.Hates(q))) continue;
                                    List<Stunt> stuntToTeach = new List<Stunt>();
                                    foreach (Stunt s in q.Stunts.Stunts.Values)
                                    {
                                        if (s.CanBeBorn(p))
                                        {
                                            stuntToTeach.Add(s);
                                        }
                                    }

                                    List<Stunt> candidates = new List<Stunt>();
                                    foreach (Stunt s in this.GameCommonData.AllStunts.Stunts.Values)
                                    {
                                        if (s.CanBeBorn(p))
                                        {
                                            candidates.Add(s);
                                        }
                                    }
                                    if (candidates.Count > 0 && GameObject.Chance((q.Strength + q.Command + q.Intelligence - 210) / 15))
                                    {
                                        stuntToTeach.Add(candidates[GameObject.Random(candidates.Count)]);
                                    }

                                    if (stuntToTeach.Count > 0)
                                    {
                                        Stunt t = stuntToTeach[GameObject.Random(stuntToTeach.Count)];
                                        int extraChance = 0;
                                        if (p.Father.GetStuntList().GameObjects.Contains(t) || p.Mother.GetStuntList().GameObjects.Contains(t))
                                        {
                                            extraChance += 10;
                                        }
                                        if (GameObject.Chance((10 + q.childrenStuntChanceIncrease + extraChance) / 3))
                                        {
                                            p.Stunts.AddStunt(t);
                                            p.AdjustRelation(q, 0, 10);
                                            q.AdjustRelation(p, 0, 10);
                                            if (GameObject.Chance(30))
                                            {
                                                Dictionary<Person, int> rels = q.GetAllRelations();
                                                foreach (KeyValuePair<Person, int> rel in rels)
                                                {
                                                    if (GameObject.Chance(100 / rels.Count))
                                                    {
                                                        p.AdjustRelation(rel.Key, 0, Math.Min(10, rel.Value / 10));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                break;
                            }
                        case 8:
                            {
                                PersonList teachers = new PersonList();

                                if (parental != null && parental.BelongedArchitecture != null)
                                {
                                    GameObjectList list = parental.BelongedArchitecture.PersonAndChildren.GetRandomList();
                                    list.AddRange(parental.BelongedArchitecture.NoFactionPersons);
                                    foreach (Person q in list)
                                    {
                                        if ((GameObject.Chance(100 / (list.Count + siblingCount)) || q == parental || q == p.Father || q == p.Mother) && q.IsValidTeacher && q.Age > 8)
                                        {
                                            if (q.HasStrainTo(p) || q.Closes(p.Father) || q.Closes(p.Mother) || q.Closes(parental) || GameObject.Chance(q.GetRelation(parental) / 10))
                                            {
                                                teachers.Add(q);
                                            }
                                        }
                                    }

                                }

                                foreach (Person q in teachers)
                                {
                                    if (p.Hates(q)) continue;
                                    if (q.Hates(p)) continue;
                                    if (GameObject.Chance(85 - q.Ambition * 15) && (q.Hates(p.Father) || q.Hates(p.Mother) || p.Father.Hates(q) || p.Mother.Hates(q))) continue;
                                    List<Title> toTeach = q.Titles;
                                    int maxLevel = 1;
                                    foreach (Title t in toTeach)
                                    {
                                        if (t.Level > maxLevel && t.Kind.RandomTeachable)
                                        {
                                            maxLevel = t.Level;
                                        }
                                    }

                                    foreach (Title t in this.GameCommonData.AllTitles.Titles.Values)
                                    {
                                        if (t.Kind.RandomTeachable && t.Level <= maxLevel + q.childrenTitleChanceIncrease + 1 && GameObject.Chance(t.InheritChance) && t.CanBeBorn(p))
                                        {
                                            toTeach.Add(t);
                                        }
                                    }

                                    foreach (Title t in toTeach)
                                    {
                                        int extraChance = 0;
                                        if (p.Father.RealTitles.Contains(t) || p.Mother.RealTitles.Contains(t))
                                        {
                                            extraChance += 5;
                                        }
                                        if (GameObject.Chance(t.InheritChance * 3 + q.childrenTitleChanceIncrease * 3 + extraChance) && t.CanBeBorn(p))
                                        {
                                            Title existing = null;
                                            foreach (Title u in p.Titles)
                                            {
                                                if (u.Kind.Equals(t.Kind))
                                                {
                                                    existing = u;
                                                    break;
                                                }
                                            }

                                            // TODO let player choose
                                            if (existing == null || existing.Level < t.Level || (existing.Level == t.Level && existing.Merit < t.Merit))
                                            {
                                                if (existing != null)
                                                {
                                                    p.RealTitles.Remove(existing);
                                                }
                                                p.RealTitles.Add(t);

                                                p.AdjustRelation(q, 0, 5 * t.Level);
                                                q.AdjustRelation(p, 0, 5 * t.Level);
                                                if (GameObject.Chance(30))
                                                {
                                                    Dictionary<Person, int> rels = q.GetAllRelations();
                                                    foreach (KeyValuePair<Person, int> rel in rels)
                                                    {
                                                        if (GameObject.Chance(100 / rels.Count))
                                                        {
                                                            p.AdjustRelation(rel.Key, 0, Math.Min(5 * t.Level, rel.Value / 10));
                                                        }
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }

                                break;
                            }
                    }

                }
            }
        }

        public bool SkyEyeSimpleNotification(GameObject gameobject)
        {
            if (Session.GlobalVariables.SkyEyeSimpleNotification && gameobject != null)
            {
                if (gameobject is Person && (this.CurrentPlayer == null || !this.CurrentPlayer.IsPositionKnown((gameobject as Person).Position)))
                {
                    return true;
                }
                if (gameobject is Troop && (this.CurrentPlayer == null || !this.CurrentPlayer.IsPositionKnown((gameobject as Troop).Position)))
                {
                    return true;
                }
                if (gameobject is Architecture && (this.CurrentPlayer == null || !this.CurrentPlayer.IsArchitectureKnown((gameobject as Architecture))))
                {
                    return true;
                }
            }
            return false;
        }

        public void captivestocaptiveData(CaptiveList captives)
        {
            this.captiveData = captives;
        }
    }
}
