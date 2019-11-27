using GameGlobal;
using GameObjects.ArchitectureDetail;
using GameObjects.FactionDetail;
using GameObjects.MapDetail;
using GameObjects.SectionDetail;
using GameObjects.TroopDetail;
using GameObjects.PersonDetail;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Linq;
using GameObjects.Influences;
using System.Text;
using GameObjects.Conditions;
using System.Runtime.Serialization;
using GameManager;

namespace GameObjects
{

    [DataContract]
    public class Faction : GameObject
    {
       // public int PrinceID = -1;
        //public int AllRoundOfficerCount;
        private int militarycount;
        private int transferingmilitarycount;
        [DataMember]
        public int ZhaoxianFailureCount = 0;
        [DataMember]
        public int YearOfficialLimit = 0;
        private Person prince = null;
        private int princeID = -1;
        private bool isAlien = false;
        private int guanjuedezhi = 0;
        private int chaotinggongxiandudezhi = 0;
        public bool AIFinished;
#pragma warning disable CS0169 // The field 'Faction.AIThread' is never used
        //private Thread AIThread;
#pragma warning restore CS0169 // The field 'Faction.AIThread' is never used
        public ZhandouZhuangtai BattleState = ZhandouZhuangtai.和平;

        public bool AllowAttackAfterMoveOfBubing;
        public bool AllowAttackAfterMoveOfNubing;
        public bool AllowAttackAfterMoveOfQibing;
        public bool AllowAttackAfterMoveOfQixie;
        public bool AllowAttackAfterMoveOfShuijun;
        public int AntiArrowChanceIncrementOfBubing;
        public int AntiArrowChanceIncrementOfNubing;
        public int AntiArrowChanceIncrementOfQibing;
        public int AntiArrowChanceIncrementOfQixie;
        public int AntiArrowChanceIncrementOfShuijun;
        public int AntiCriticalStrikeChanceIncrementWhileCombatMethodOfBubing;
        public int AntiCriticalStrikeChanceIncrementWhileCombatMethodOfNubing;
        public int AntiCriticalStrikeChanceIncrementWhileCombatMethodOfQibing;
        public int AntiCriticalStrikeChanceIncrementWhileCombatMethodOfQixie;
        public int AntiCriticalStrikeChanceIncrementWhileCombatMethodOfShuijun;
        public int troopSequence = -1;


        private int[,] architectureAdjustCost;

        public void Init()
        {
            BattleState = ZhandouZhuangtai.和平;

            Architectures = new ArchitectureList();

            AvailableTechniques = new TechniqueTable();

            BaseMilitaryKinds = new MilitaryKindTable();

            KnownTroops = new Dictionary<int, Troop>();

            Sections = new SectionList();

            Informations = new InformationList();
            KnownTroops = new Dictionary<int, Troop>();

            Legions = new LegionList();

            LevelOfView = InformationLevel.中;

            RoutewayPathBuilder = new RoutewayPathFinder();

            ClosedRouteways = new Dictionary<Point, object>();

            Routeways = new RoutewayList();

            SecondTierKnownPaths = new Dictionary<ClosedPathEndpoints, List<Point>>();

            TechniqueMilitaryKinds = new MilitaryKindTable();

            ThirdTierKnownPaths = new Dictionary<ClosedPathEndpoints, List<Point>>();

            Troops = new TroopList();

            TransferingMilitaries = new MilitaryList();

            count = new Dictionary<PersonGeneratorType, int>();

            RateOfCombativityRecoveryAfterAttacked = 0;
            RateOfCombativityRecoveryAfterStratagemFail = 0;
            RateOfCombativityRecoveryAfterStratagemSuccess = 0;
            RateOfFoodTransportBetweenArchitectures = 1;
            RateOfRoutewayConsumption = 1;

            techniqueFundCostRateDecrease = new List<float>();
            techniquePointCostRateDecrease = new List<float>();
            techniqueTimeRateDecrease = new List<float>();
            techniqueReputationRateDecrease = new List<float>();

            CriticalOfMillitaryType = new int[5];
            AntiCriticalOfMillitaryType = new int[5];
            ArchitectureDamageOfMillitaryType = new float[5];
            SpeedOfMillitaryType = new float[5];
            for (int i = 0; i < 5; ++i)
            {
                ArchitectureDamageOfMillitaryType[i] = 1;
                SpeedOfMillitaryType[i] = 1;
            }
            ViewAreaOfMillitaryType = new int[5];
            StratagemOfMillitaryType = new int[5];
            AntiStratagemOfMillitaryType = new int[5];

            this.FactionColor = Session.Current.Scenario.GameCommonData.AllColors[this.ColorIndex];

            this.RoutewayPathBuilder = new RoutewayPathFinder();
            this.RoutewayPathBuilder.OnGetCost += new RoutewayPathFinder.GetCost(this.RoutewayPathBuilder_OnGetCost);
            this.RoutewayPathBuilder.OnGetPenalizedCost += new RoutewayPathFinder.GetPenalizedCost(this.RoutewayPathBuilder_OnGetPenalizedCost);
        }

        [DataMember]
        public string ArchitecturesString { get; set; }

        public ArchitectureList Architectures = new ArchitectureList();
        private int armyScale = 0;

        [DataMember]
        public bool AutoRefuse;

        [DataMember]
        public string AvailableTechniquesString { get; set; }

        public TechniqueTable AvailableTechniques = new TechniqueTable();

        [DataMember]
        public string BaseMilitaryKindsString { get; set; }

        public MilitaryKindTable BaseMilitaryKinds = new MilitaryKindTable();

        private Architecture capital;
        private int capitalID;

        public Dictionary<Point, object> ClosedRouteways = new Dictionary<Point, object>();
        private int colorIndex;
        private bool controlling;

        public int CriticalStrikeChanceIncrementWhileCombatMethodOfBubing;
        public int CriticalStrikeChanceIncrementWhileCombatMethodOfNubing;
        public int CriticalStrikeChanceIncrementWhileCombatMethodOfQibing;
        public int CriticalStrikeChanceIncrementWhileCombatMethodOfQixie;
        public int CriticalStrikeChanceIncrementWhileCombatMethodOfShuijun;
        public float DefenceRateOfBubing;
        public float DefenceRateOfNubing;
        public float DefenceRateOfQibing;
        public float DefenceRateOfQixie;
        public float DefenceRateOfShuijun;
        public float DefenceRateWhileCombatMethodOfBubing;
        public float DefenceRateWhileCombatMethodOfNubing;
        public float DefenceRateWhileCombatMethodOfQibing;
        public float DefenceRateWhileCombatMethodOfQixie;
        public float DefenceRateWhileCombatMethodOfShuijun;
        [DataMember]
        public bool Destroyed;
        public Color FactionColor;
        public int IncrementOfAntiCriticalStrikeChance;
        public int IncrementOfChaosDaysAfterPhisicalAttack;
        public int IncrementOfCombativityCeiling;
        public int IncrementOfCriticalStrikeChance;
        public int IncrementOfResistStratagemChance;
        public int IncrementOfRoutewayRadius;
        public int IncrementOfRoutewayWorkforce;
        public int IncrementOfStratagemSuccessChance;
        public int IncrementOfViewRadius;

        [DataMember]
        public string InformationsString { get; set; }

        public InformationList Informations = new InformationList();
        private Dictionary<Point, InformationTile> knownAreaData;
        public Dictionary<int, Troop> KnownTroops = new Dictionary<int, Troop>();
        private Person leader = null;
        private int leaderID;

        [DataMember]
        public string LegionsString { get; set; }

        public LegionList Legions = new LegionList();
        public InformationLevel LevelOfView = InformationLevel.中;
        private int[,] mapData;
        // public MilitaryList Militaries = new MilitaryList();

        public int NoCounterChanceIncrementOfBubing;
        public int NoCounterChanceIncrementOfNubing;
        public int NoCounterChanceIncrementOfQibing;
        public int NoCounterChanceIncrementOfQixie;
        public int NoCounterChanceIncrementOfShuijun;
        public int OffenceRadiusIncrementOfBubing;
        public int OffenceRadiusIncrementOfNubing;
        public int OffenceRadiusIncrementOfQibing;
        public int OffenceRadiusIncrementOfQixie;
        public int OffenceRadiusIncrementOfShuijun;
        public float OffenceRateOfBubing;
        public float OffenceRateOfNubing;
        public float OffenceRateOfQibing;
        public float OffenceRateOfQixie;
        public float OffenceRateOfShuijun;
        public float OffenceRateWhileCombatMethodOfBubing;
        public float OffenceRateWhileCombatMethodOfNubing;
        public float OffenceRateWhileCombatMethodOfQibing;
        public float OffenceRateWhileCombatMethodOfQixie;
        public float OffenceRateWhileCombatMethodOfShuijun;

        private bool passed;

        [DataMember]
        public int PlanTechniqueString { get; set; }

        public Technique PlanTechnique;
        public Architecture PlanTechniqueArchitecture;

        [DataMember]
        public List<int> PreferredTechniqueKinds = new List<int>();

        private bool preUserControlFinished = true;

        public float RateIncrementOfTerrainRate;
        public float RateOfCombativityRecoveryAfterAttacked;
        public float RateOfCombativityRecoveryAfterStratagemFail;
        public float RateOfCombativityRecoveryAfterStratagemSuccess;
        public float RateOfFoodTransportBetweenArchitectures = 1f;
        public float RateOfRoutewayConsumption = 1f;
        private int reputation;

        public RoutewayPathFinder RoutewayPathBuilder = new RoutewayPathFinder();

        [DataMember]
        public string RoutewaysString { get; set; }

        public RoutewayList Routeways = new RoutewayList();
        private Dictionary<ClosedPathEndpoints, List<Point>> SecondTierKnownPaths = new Dictionary<ClosedPathEndpoints, List<Point>>();
        private int[,] secondTierMapCost;
        [DataMember]
        public int SecondTierXResidue = 0;
        [DataMember]
        public int SecondTierYResidue = 0;
        
        [DataMember]
        public string SectionsString { get; set; }

        public SectionList Sections = new SectionList();

        public bool StopToControl;

        public MilitaryKindTable TechniqueMilitaryKinds = new MilitaryKindTable();
        private int techniquePoint;
        private int techniquePointForFacility;
        private int techniquePointForTechnique;
        private Dictionary<ClosedPathEndpoints, List<Point>> ThirdTierKnownPaths = new Dictionary<ClosedPathEndpoints, List<Point>>();
        private int[,] thirdTierMapCost;
        [DataMember]
        public int ThirdTierXResidue = 0;
        [DataMember]
        public int ThirdTierYResidue = 0;

        [DataMember]
        public string TroopListString { get; set; }

        public TroopList Troops = new TroopList();
       // [DataMember]//后面有public的datamember
        private int upgradingDaysLeft;
        //  [DataMember]//后面有public的datamember
        private int upgradingTechnique = -1;
        // private Dictionary<MilitaryKind, int> militaryKindCounts = new Dictionary<MilitaryKind, int>();

        public List<float> techniqueReputationRateDecrease = new List<float>();
        public List<float> techniquePointCostRateDecrease = new List<float>();
        public List<float> techniqueTimeRateDecrease = new List<float>();
        public List<float> techniqueFundCostRateDecrease = new List<float>();

        [DataMember]
        public bool NotPlayerSelectable = false;

        public int[] CriticalOfMillitaryType = new int[5];
        public int[] AntiCriticalOfMillitaryType = new int[5];
        public float[] ArchitectureDamageOfMillitaryType = { 1f, 1f, 1f, 1f, 1f };
        public float[] SpeedOfMillitaryType = { 1f, 1f, 1f, 1f, 1f };
        public int[] ViewAreaOfMillitaryType = new int[5];
        public int[] StratagemOfMillitaryType = new int[5];
        public int[] AntiStratagemOfMillitaryType = new int[5];

        public String Counsellor;
        public String Governor;
        public String FiveTigers;

        public event AfterCatchLeader OnAfterCatchLeader;

        public event FactionDestroy OnFactionDestroy;

        public event ForcedChangeCapital OnForcedChangeCapital;

        public event GetControl OnGetControl;

        public event InitiativeChangeCapital OnInitiativeChangeCapital;

        public event TechniqueFinished OnTechniqueFinished;

        public event FactionUpgradeTechnique OnUpgradeTechnique;

        public PersonList Persons
        {
            get
            {
                PersonList result = new PersonList();
                //foreach (Person i in Session.Current.Scenario.Persons)
                //{
                //    if ((i.Status == GameObjects.PersonDetail.PersonStatus.Normal || i.Status == GameObjects.PersonDetail.PersonStatus.Moving)
                //        && i.BelongedFaction == this)
                //    {
                //        result.Add(i);
                //    }
                //}
                foreach (Architecture a in Architectures)
                {
                    foreach (Person p in a.Persons)
                        result.Add(p);
                    foreach (Person p in a.MovingPersons)
                        result.Add(p);
                }
                foreach (Troop t in Troops)
                {
                    foreach (Person p in t.Persons)
                        result.Add(p);
                }
                foreach (Captive c in Session.Current.Scenario.Captives)
                {
                    if (c.CaptiveFaction == this)
                    {
                        result.Add(c.CaptivePerson);
                    }
                }

                return result;
            }
        }

        public PersonList MayorList
        {
            get
            {
                PersonList result = new PersonList();
                foreach (Architecture a in this.Architectures)
                {
                    if (a.Mayor != null)
                    {
                        result.Add(a.Mayor);
                    }
                }
                return result;
            }
        }
        /*
        public PersonList ConvinceableMayorList   //可劝降的太守列表
        {
            get
            {
                PersonList list = new PersonList();
                foreach (Faction f in Session.Current.Scenario.Factions)
                {
                    foreach (Architecture a in f.Architectures)
                    {
                        if (f != this && a.Mayor != null)
                        {
                            list.Add(a.Mayor);
                        }
                    }
                }
                return list;
            }
        }
 
        public PersonList ConvinceableLeaderList //可劝降的君主列表
        {
            get
            {
                 PersonList list = new PersonList();
                 foreach (Faction f in Session.Current.Scenario.Factions )
                 {
                     if (f != this )
                     {
                         list.Add(f.Leader);
                     }
                 }
                return list ;
            }
        }
        */


        public TreasureList AllTreasuresExceptLeader
        {
            get
            {
                TreasureList list = new TreasureList();
                foreach (Person person in this.Persons)
                {
                    if (person != this.Leader)
                    {
                        person.AddTreasureToList(list);
                    }
                }
                return list;
            }
        }

        public PersonList PersonsInArchitecturesExceptLeader
        {
            get
            {
                PersonList result = new PersonList();
                foreach (Architecture a in Architectures)
                {
                    foreach (Person p in a.Persons)
                    {
                        if (p != this.Leader)  result.Add(p);
                    }
                }
                return result;
            }
        }

        public CaptiveList Captives
        {
            get
            {
                CaptiveList result = new CaptiveList();
                foreach (Captive i in Session.Current.Scenario.Captives)
                {
                    if (i.BelongedFaction == this)
                    {
                        result.Add(i);
                    }
                }
                return result;
            }
        }

        public CaptiveList SelfCaptives
        {
            get
            {
                CaptiveList result = new CaptiveList();
                foreach (Captive i in Session.Current.Scenario.Captives)
                {
                    if (i.CaptiveFaction == this)
                    {
                        result.Add(i);
                    }
                }
                return result;
            }
        }
        
        public ArchitectureList ArchitecturesExcluding(Architecture a)
        {
            ArchitectureList result = new ArchitectureList();
            foreach (Architecture i in this.Architectures)
            {
                if (i != a)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public int GetTechniqueUsefulness(Technique tech)
        {
            int result = 0;
            foreach (Influences.Influence i in tech.Influences.GetInfluenceList())
            {
                switch (i.Kind.ID)
                {
                    case 1030:
                    case 2400:
                    case 2420:
                    case 2430:
                        if (!Session.GlobalVariables.LiangdaoXitong) break;
                        result = Math.Max(100, result);
                        break;
                    case 2000:
                    case 2010:
                    case 2020:
                    case 2030:
                    case 2200:
                    case 2210:
                    case 2220:
                    case 2230:
                    case 2240:
                    case 2250:
                        if (int.Parse(i.Parameter) == 3)
                        {
                            bool hasWater = false;
                            foreach (Architecture a in this.Architectures)
                            {
                                if (a.IsBesideWater)
                                {
                                    hasWater = true;
                                    break;
                                }
                            }
                            if (!hasWater) break;
                        }
                        else if (int.Parse(i.Parameter) == 4)
                        {
                            bool hasSiege = false;
                            foreach (MilitaryKind mk in this.AvailableMilitaryKinds.MilitaryKinds.Values)
                            {
                                if (mk.Type == MilitaryType.器械)
                                {
                                    hasSiege = true;
                                }
                            }
                            if (!hasSiege) break;
                        }
                        else if (int.Parse(i.Parameter) == 0)
                        {
                            bool hasSiege = false;
                            foreach (MilitaryKind mk in this.AvailableMilitaryKinds.MilitaryKinds.Values)
                            {
                                if (mk.Type == MilitaryType.步兵)
                                {
                                    hasSiege = true;
                                }
                            }
                            if (!hasSiege) break;
                        }
                        else if (int.Parse(i.Parameter) == 1)
                        {
                            bool hasSiege = false;
                            foreach (MilitaryKind mk in this.AvailableMilitaryKinds.MilitaryKinds.Values)
                            {
                                if (mk.Type == MilitaryType.弩兵)
                                {
                                    hasSiege = true;
                                }
                            }
                            if (!hasSiege) break;
                        }
                        else if (int.Parse(i.Parameter) == 2)
                        {
                            bool hasSiege = false;
                            foreach (MilitaryKind mk in this.AvailableMilitaryKinds.MilitaryKinds.Values)
                            {
                                if (mk.Type == MilitaryType.骑兵)
                                {
                                    hasSiege = true;
                                }
                            }
                            if (!hasSiege) break;
                        }
                        result = Math.Max(100, result);
                        break;
                    default:
                        result = Math.Max(100, result);
                        break;
                }
            }
            return result;
        }

        public void AddArchitecture(Architecture architecture)
        {
            this.Architectures.Add(architecture);
            if (architecture.BelongedFaction != null)
            {
                if (architecture.BelongedFaction == this)
                {
                    return;
                }
                architecture.BelongedFaction.RemoveArchitecture(architecture);
            }
            architecture.BelongedFaction = this;
        }

        public List<Point> GetAllKnownArea()
        {
            List<Point> result = new List<Point>();
            foreach (Point p in this.knownAreaData.Keys)
            {
                if (this.GetKnownAreaData(p) != InformationLevel.无 && this.GetKnownAreaData(p) != InformationLevel.未知)
                {
                    result.Add(p);
                }
            }
            return result;
        }

        private TroopList visibleTroopsCache = null;
        public TroopList GetVisibleTroops()
        {
            if (visibleTroopsCache != null)
            {
                return visibleTroopsCache;
            }
            else
            {
                TroopList result = new TroopList();
                foreach (Point p in this.GetAllKnownArea())
                {
                    Troop troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(p);
                    if ((troopByPositionNoCheck != null) && !troopByPositionNoCheck.Destroyed)
                    {
                        result.Add(troopByPositionNoCheck);
                    }
                }
                visibleTroopsCache = result;
                return result;
            }
        }

        private void AddKnownAreaData(Point p, InformationLevel level)
        {
            if (!this.knownAreaData.ContainsKey(p))
            {
                InformationTile it = new InformationTile();
                it.AddInformationLevel(level);
                this.knownAreaData.Add(p, it);
            }
            else
            {
                InformationTile it = this.knownAreaData[p];
                it.AddInformationLevel(level);
                this.knownAreaData[p] = it;
            }
        }

        private void RemoveKnownAreaData(Point p, InformationLevel level)
        {
            if (this.knownAreaData.ContainsKey(p))
            {
                InformationTile it = this.knownAreaData[p];
                it.RemoveInformationLevel(level);
                this.knownAreaData[p] = it;
                if (it.Level == InformationLevel.无)
                {
                    this.knownAreaData.Remove(p);
                }
            }
        }

        private InformationLevel getInformationLevel(Point p)
        {
            if (!this.knownAreaData.ContainsKey(p))
            {
                return InformationLevel.无;
            }
            else
            {
                return this.knownAreaData[p].Level;
            }
        }

        public void AddArchitectureKnownData(Architecture a)
        {
            foreach (Point point in a.ArchitectureArea.Area)
            {
                this.AddKnownAreaData(point, InformationLevel.全);
            }
            foreach (Point point in a.ViewArea.Area)
            {
                if (!Session.Current.Scenario.PositionOutOfRange(point))
                {
                    this.AddKnownAreaData(point, InformationLevel.高);
                }
            }
            if (a.Kind != null && a.Kind.HasLongView)
            {
                foreach (Point point in a.LongViewArea.Area)
                {
                    if (!Session.Current.Scenario.PositionOutOfRange(point))
                    {
                        this.AddKnownAreaData(point, InformationLevel.中);
                    }
                }
            }
        }

        public void AddArchitectureMilitaries(Architecture architecture)
        {
            foreach (Military military in architecture.Militaries)
            {
                this.AddMilitary(military);
            }
        }

        public void AddInformation(Information information)
        {
            this.Informations.AddInformation(information);
            information.BelongedFaction = this;
        }

        public void AddLegion(Legion legion)
        {
            this.Legions.Add(legion);
            legion.BelongedFaction = this;
        }

        public void AddMilitary(Military military)
        {
            this.Militaries.AddMilitary(military);
           /* if (this.militaryKindCounts.ContainsKey(military.RealMilitaryKind))
            {
                this.militaryKindCounts[military.Kind]++;
            }
            else
            {
                this.militaryKindCounts[military.Kind] = 1;
            }*/
            military.BelongedFaction = this;
        }

        public void AddPositionInformation(Point position, InformationLevel level)
        {
            if (!Session.Current.Scenario.PositionOutOfRange(position))
            {
                this.AddKnownAreaData(position, level);
            }
        }

        public void AddRouteway(Routeway routeway)
        {
            this.Routeways.AddRoutewayWithEvent(routeway);
            routeway.BelongedFaction = this;
        }

        public void AddSecondTierKnownPath(List<Point> path)
        {
            if (path != null)
            {
                ClosedPathEndpoints key = new ClosedPathEndpoints(path[0], path[path.Count - 1]);
                if (!this.SecondTierKnownPaths.ContainsKey(key))
                {
                    if (this.SecondTierKnownPaths.Count > Session.GlobalVariables.MaxCountOfKnownPaths)
                    {
                        this.SecondTierKnownPaths.Clear();
                    }
                    this.SecondTierKnownPaths.Add(key, path);
                }
            }
        }

        public void AddSection(Section section)
        {
            this.Sections.Add(section);
            section.BelongedFaction = this;
        }

        public void AddTechniqueMilitaryKind(int kindID)
        {
            MilitaryKind militaryKind = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(kindID);
            if (militaryKind != null)
            {
                this.TechniqueMilitaryKinds.AddMilitaryKind(militaryKind);
            }
        }

        public void AddThirdTierKnownPath(List<Point> path)
        {
            if (path != null)
            {
                ClosedPathEndpoints key = new ClosedPathEndpoints(path[0], path[path.Count - 1]);
                if (!this.ThirdTierKnownPaths.ContainsKey(key))
                {
                    if (this.ThirdTierKnownPaths.Count > Session.GlobalVariables.MaxCountOfKnownPaths)
                    {
                        this.ThirdTierKnownPaths.Clear();
                    }
                    this.ThirdTierKnownPaths.Add(key, path);
                }
            }
        }

        public void AddTroop(Troop troop)
        {
            this.Troops.Add(troop);
            if (troop.BelongedFaction != null)
            {
                if (troop.BelongedFaction == this)
                {
                    return;
                }
                troop.BelongedFaction.RemoveTroop(troop);
            }
            troop.BelongedFaction = this;
        }

        public void AddTroopKnownAreaData(Troop troop)
        {
            foreach (Point point in troop.ViewArea.Area)
            {
                if (Session.Current.Scenario.PositionOutOfRange(point))
                {
                    continue;
                }
                if (point == troop.ViewArea.Centre)
                {
                    this.AddKnownAreaData(point, InformationLevel.全);
                }
                else
                {
                    this.AddKnownAreaData(point, troop.ScoutLevel);
                }
            }
        }

        public void AddTroopMilitary(Troop troop)
        {
            //兼容以前的旧存档？
            if (troop.Army != null)
            {
                if (troop.Army.ShelledMilitary == null)
                {
                    this.AddMilitary(troop.Army);
                }
                else
                {
                    this.AddMilitary(troop.Army.ShelledMilitary);
                }
            }
        }

        public void AdjustByArchitecture(Architecture architecture, int cost)
        {
            foreach (Point point in architecture.ArchitectureArea.Area)
            {
                this.architectureAdjustCost[point.X, point.Y] = cost;
            }
        }

        protected void AdjustByArchitectures()
        {
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if (!architecture.IsFriendly(this))
                {
                    this.AdjustByArchitecture(architecture, 0xdac);
                }
            }
        }

        public void AdjustMapCost()
        {
            this.AdjustByArchitectures();
        }

        private bool IsPersonForHouGong(Person p, bool alreadyTaken = false)
        {
            if (!this.Leader.isLegalFeiZiExcludeAge(p) || !p.isLegalFeiZiExcludeAge(this.Leader)) return false;

            if (p.Sex && p.Age >= 45) return false;

            if (this.Leader.Sex && this.Leader.Age >= 45) return false;

            if (p.Spouse == this.Leader || this.Leader.Spouse == p) return true;
        
            if (p.BelongedFaction != null && p.marriageGranter == p.BelongedFaction.Leader)
            {
                return false;
            }

            bool hasSon = false;
            if (this.Leader.NumberOfChildren > 0)
            {
                foreach (Person q in this.Leader.ChildrenList)
                {
                    if (!q.Sex)
                    {
                        hasSon = true;
                    }
                }
            }

            int unAmbition = Enum.GetNames(typeof(PersonAmbition)).Length - (int)this.Leader.Ambition;
            bool take = (p.UntiredMerit > ((unAmbition - 1) * Session.Parameters.AINafeiAbilityThresholdRate) || !hasSon) &&
                                        (!((bool)Session.GlobalVariables.PersonNaturalDeath) || (p.Age >= 16 && (p.Age <= Session.Parameters.AINafeiMaxAgeThresholdAdd + (int)leader.Ambition * Session.Parameters.AINafeiMaxAgeThresholdMultiply || !hasSon))) &&
                                        p.marriageGranter != this.Leader && !p.Hates(this.Leader);
   
            Person hater = WillHateLeaderDueToAffair(p, this.Leader, p.suoshurenwuList.GetList(), false);

            if (this.IsAlien && (hater == null || hater.PersonalLoyalty >= 2))
            {
                if (hater == null || hater.BelongedFaction != this)
                {
                    return true;
                }
            }

            if (hater != null)
            {
                if (this.leader.PersonalLoyalty >= 4) return false;
                if (this.leader.PersonalLoyalty >= 3 && this.leader.NumberOfChildren > 0) return false;
                if (this.leader.PersonalLoyalty >= 2 && (p.PersonalLoyalty >= 4 || (p.PersonalLoyalty >= 2 && p.Spouse != null && p.Spouse.Alive))) return false;
            }

            return (take || alreadyTaken) && (hater == null || (leader.PersonalLoyalty <= (int)PersonLoyalty.普通 && hater.UnalteredUntiredMerit * (leader.PersonalLoyalty * Session.Parameters.AINafeiStealSpouseThresholdRateMultiply + Session.Parameters.AINafeiStealSpouseThresholdRateAdd) < this.Leader.UnalteredUntiredMerit));
        }

        private Person WillHateLeaderDueToAffair(Person p, Person q, GameObjectList suoshu, bool simulateMarry)
        {
            Dictionary<Person, PersonList> haters = Person.willHateCausedByAffair(p, q, this.Leader, suoshu, simulateMarry);
            PersonList leaderHaters = new PersonList();
            foreach (KeyValuePair<Person, PersonList> i in haters)
            {
                if (i.Value.HasGameObject(this.Leader))
                {
                    leaderHaters.Add(i.Key);
                }
            }

            int unAmbition = Enum.GetNames(typeof(PersonAmbition)).Length - (int)this.Leader.Ambition;
            Person spousePerson = null;
            int maxMerit = 0;
            foreach (Person i in leaderHaters)
            {
                if (i.Alive && i != this.Leader && !i.Hates(this.Leader) && i.UntiredMerit > maxMerit)
                {
                    spousePerson = i;
                    maxMerit = i.UntiredMerit;
                }
            }

            return spousePerson;
        }

        private void AIActuallyMakeMarriage(Person p, Person q)
        {
            if (p.LocationArchitecture == q.LocationArchitecture && p.LocationArchitecture != null &&
                                p.LocationArchitecture.Fund >= Session.Parameters.MakeMarriageCost)
            {
                if (p.WaitForFeiZi != null)
                {
                    p.WaitForFeiZi.WaitForFeiZi = null;
                }
                if (q.WaitForFeiZi != null)
                {
                    q.WaitForFeiZi.WaitForFeiZi = null;
                }
                p.Marry(q, this.Leader);
                p.WaitForFeiZi = null;
                q.WaitForFeiZi = null;
            }
            else
            {
                if (p.WaitForFeiZi != null)
                {
                    p.WaitForFeiZi.WaitForFeiZi = null;
                }
                if (q.WaitForFeiZi != null)
                {
                    q.WaitForFeiZi.WaitForFeiZi = null;
                }
                p.WaitForFeiZi = q;
                q.WaitForFeiZi = p;
                if (p.LocationArchitecture != q.LocationArchitecture)
                {
                    if (q.Status == PersonStatus.Normal && q.LocationArchitecture != null && q.LocationTroop == null &&
                        p.BelongedArchitecture.Fund >= Session.Parameters.MakeMarriageCost)
                    {
                        q.MoveToArchitecture(p.BelongedArchitecture);
                    }
                    else if (p.Status == PersonStatus.Normal && p.LocationArchitecture != null && p.LocationTroop == null &&
                        q.BelongedArchitecture.Fund >= Session.Parameters.MakeMarriageCost)
                    {
                        p.MoveToArchitecture(q.BelongedArchitecture);
                    }
                }
            }
        }

        private void AIMakeMarriage()
        {
            if (Session.Current.Scenario.IsPlayer(this)) return;

            if (this.Leader.Status == PersonStatus.Captive) return;

            foreach (Person p in this.Persons)
            {
                if (p.WaitForFeiZi != null)
                {
                    if ((p.BelongedFaction != this || p.Spouse != null
                        || (p.WaitForFeiZi.BelongedFaction != this && p.WaitForFeiZi.Spouse != null)))
                    {
                        if (p.WaitForFeiZi != null)
                        {
                            p.WaitForFeiZi.WaitForFeiZi = null;
                        }
                        p.WaitForFeiZi = null;
                    }
                    else if (!p.isLegalFeiZiExcludeAge(p.WaitForFeiZi) || p.WaitForFeiZi.isLegalFeiZiExcludeAge(p))
                    {
                        if (p.WaitForFeiZi != null)
                        {
                            p.WaitForFeiZi.WaitForFeiZi = null;
                        }
                        p.WaitForFeiZi = null;
                    }
                    else
                    {
                        if (p.Status == PersonStatus.Normal && p.LocationArchitecture != null
                            && p.LocationTroop == null)
                        {
                            if (p.LocationArchitecture.Fund >= Session.Parameters.MakeMarriageCost)
                            {
                                p.Marry(p.WaitForFeiZi, this.Leader);
                                if (p.WaitForFeiZi != null)
                                {
                                    p.WaitForFeiZi.WaitForFeiZi = null;
                                }
                                p.WaitForFeiZi = null;
                            }
                        }
                    }
                }
            }

            if (GameObject.Random(10) == 0)
            {
                if (leader.Spouse != null && leader.WaitForFeiZi != null && leader.Age < 40 && leader.Status == PersonStatus.Normal)
                {
                    PersonList leaderMarryable = this.Leader.MakeMarryableInFaction();
                    if (leaderMarryable.Count > 0)
                    {
                        Person q = this.Leader;
                        leaderMarryable.PropertyName = "UntiredMerit";
                        leaderMarryable.IsNumber = true;
                        leaderMarryable.SmallToBig = false;
                        leaderMarryable.ReSort();
                        foreach (Person p in leaderMarryable)
                        {
                            if (p.WaitForFeiZi == null)
                            {
                                GameObjectList simulatSuoshu = p.suoshurenwuList.GetList();
                                simulatSuoshu.Add(p);
                                simulatSuoshu.AddRange(q.suoshurenwuList.GetList());
                                simulatSuoshu.Add(q);

                                Person hater = WillHateLeaderDueToAffair(p, q, simulatSuoshu, false);
                                if (hater != null && hater != p && hater != q) continue;

                                AIActuallyMakeMarriage(p, q);
                                break;
                            }
                        }
                    }
                }

                GameObjectList pl = this.Persons.GetList();
                pl.PropertyName = "UntiredMerit";
                pl.IsNumber = true;
                pl.SmallToBig = false;
                pl.ReSort();
                foreach (Person p in pl)
                {
                    if (p.WaitForFeiZi != null) continue;
                    if (p.Spouse != null) continue;
                    if (p.Status != PersonStatus.Normal) continue;
                    PersonList allCandidates = p.MakeMarryableInFaction();
                    PersonList candidates = new PersonList();
                    foreach (Person q in allCandidates)
                    {
                        if (Math.Abs(q.Age - p.Age) <= 15)
                        {
                            candidates.Add(q);
                        }
                    }
                    if (candidates.Count > 0)
                    {
                        Person q = candidates.GetMaxUntiredMeritPerson();
                        if (q.WaitForFeiZi == null)
                        {
                            if (this.hougongValid)
                            {
                                if ((IsPersonForHouGong(p) || IsPersonForHouGong(q)) && !(this.Leader == p || this.Leader == q)) continue;
                            }

                            GameObjectList simulatSuoshu = p.suoshurenwuList.GetList();
                            simulatSuoshu.Add(p);
                            simulatSuoshu.AddRange(q.suoshurenwuList.GetList());
                            simulatSuoshu.Add(q);

                            Person hater = WillHateLeaderDueToAffair(p, q, simulatSuoshu, false);
                            if (hater != null) continue;

                            AIActuallyMakeMarriage(p, q);
                            break;
                        }
                    }
                }
            }
        }

        public int TotalFeiziCount()
        {
            int count = 0;
            foreach (Architecture a in this.Architectures)
            {
                count += a.Feiziliebiao.Count;
            }
            return count;
        }

        public int TotalFeiziSpaceCount()
        {
            int count = 0;
            foreach (Architecture a in this.Architectures)
            {
                count += a.Meinvkongjian;
            }
            return count;
        }

        private void AIHouGong()
        {
            if (Session.Current.Scenario.IsPlayer(this)) return;

            int uncruelty = this.Leader.Uncruelty;
            int unAmbition = Enum.GetNames(typeof(PersonAmbition)).Length - (int)this.Leader.Ambition;

            // move
            foreach (Architecture a in this.Architectures)
            {
                if (a.Feiziliebiao.Count > 0)
                {
                    Architecture dest = null;
                    int maxPop = 0;
                    foreach (Architecture b in this.Architectures)
                    {
                        if (!b.withoutTruceFrontline && !b.JustAttacked && !b.HasHostileTroopsInView() &&
                            (b.Meinvkongjian > b.Feiziliebiao.Count || b.BelongedFaction.IsAlien))
                        {
                            if (b.Endurance > maxPop)
                            {
                                maxPop = b.Endurance;
                                dest = b;
                            }
                        }
                    }
                    if (dest == null)
                    {
                        foreach (Architecture b in this.Architectures)
                        {
                            if (b.RecentlyAttacked <= 0 && b.RecentlyBreaked <= 0 &&
                                (b.Meinvkongjian > b.Feiziliebiao.Count || b.BelongedFaction.IsAlien))
                            {
                                if (b.Endurance > maxPop)
                                {
                                    maxPop = b.Endurance;
                                    dest = b;
                                }
                            }
                        }
                    }
                    if (dest == null)
                    {
                        foreach (Architecture b in this.Architectures)
                        {
                            if (b.Endurance > 30 &&
                                (b.Meinvkongjian > b.Feiziliebiao.Count || b.BelongedFaction.IsAlien))
                            {
                                if (b.Endurance > maxPop)
                                {
                                    maxPop = b.Endurance;
                                    dest = b;
                                }
                            }
                        }
                    }
                    if (dest != null)
                    {
                        int cnt = dest.BelongedFaction.IsAlien ? 9999 : dest.Meinvkongjian - dest.Feiziliebiao.Count;
                        GameObjectList list = a.Feiziliebiao.GetList();
                        list.PropertyName = "Merit";
                        list.IsNumber = true;
                        list.SmallToBig = false;
                        list.ReSort();
                        int moved = 0;
                        foreach (Person p in list)
                        {
                            if (p.ArrivingDays <= 0 && p.LocationArchitecture != dest)
                            {
                                p.MoveToArchitecture(dest);
                                moved++;
                                if (moved >= cnt) break;
                            }
                        }
                    }
                }
            }

            if (GameObject.Random(10) == 0)
            {
                //release
                foreach (Architecture a in this.Architectures)
                {
                    foreach (Person p in a.ReleasableFeizis)
                    {
                        if (!IsPersonForHouGong(p, true))
                        {
                            if (!this.Leader.suoshurenwuList.HasGameObject(p) && !p.Hates(this.Leader) && p.RecruitableBy(this, 0))
                            {
                                p.feiziRelease();
                            }
                        }
                    }
                }
            }

            
            // if (this.Leader.LocationArchitecture == null || this.Leader.LocationArchitecture.HasHostileTroopsInView()) return;

            if (!this.hougongValid) return;

            if (this.Leader.NumberOfChildren >= Session.GlobalVariables.OfficerChildrenLimit) return;

            if (this.Leader.Age <= 12) return;

            // build hougong
            if (this.meinvkongjian() - this.feiziCount() <= 0 && !this.isAlien && TotalFeiziSpaceCount() < Session.Current.Scenario.Parameters.AIMaxFeizi &&
                GameObject.Random((int)(GameObject.Square(unAmbition) * Session.Parameters.AIBuildHougongUnambitionProbWeight + GameObject.Square(this.meinvkongjian()) * unAmbition * Session.Parameters.AIBuildHougongSpaceBuiltProbWeight)) == 0)
            {
                Architecture buildAt = null;
                bool planned = false;
                foreach (Architecture a in this.Architectures)
                {
                    if (a.FrontLine) continue;
                    if (a.ExpectedFund - a.EnoughFund <= 50 * 30) continue;
                    if (a.Kind.FacilityPositionUnit <= 0) continue;
                    if (a.PlanFacilityKind != null && a.PlanFacilityKind.rongna > 0)
                    {
                        planned = true;
                        break;
                    }
                    if (a.BuildingFacility >= 0 && Session.Current.Scenario.GameCommonData.AllFacilityKinds.GetFacilityKind(a.BuildingFacility).rongna > 0)
                    {
                        planned = true;
                        break;
                    }

                    if (buildAt == null || a.Population > buildAt.Population)
                    {
                        buildAt = a;
                    }
                }

                if (!planned && buildAt != null)
                {
                    int maxHgSize = (12 - uncruelty) + Math.Max(0, buildAt.FacilityPositionCount / buildAt.Kind.FacilityPositionUnit - 5) + Session.Parameters.AIBuildHougongMaxSizeAdd;
                    FacilityKind hougong = null;
                    foreach (FacilityKind fk in Session.Current.Scenario.GameCommonData.AllFacilityKinds.FacilityKinds.Values)
                    {
                        if (!fk.CanBuild(buildAt)) continue;
                        if (fk.FundCost > buildAt.Fund) continue;
                        if (fk.rongna > 0 && fk.rongna < maxHgSize && GameObject.Chance(Session.Parameters.AIBuildHougongSkipSizeChance))
                        {
                            if (hougong == null || hougong.rongna < fk.rongna)
                            {
                                hougong = fk;
                            }
                        }
                    }
                    if (hougong != null)
                    {
                        int facilityPositionLeft = buildAt.FacilityPositionLeft;
                        if (facilityPositionLeft < hougong.PositionOccupied && buildAt.FacilityPositionCount >= hougong.PositionOccupied)
                        {
                            FacilityList fl = new FacilityList();
                            foreach (Facility f in buildAt.Facilities)
                            {
                                if (f.location.CanRemoveFacility(f))
                                {
                                    fl.Add(f);
                                }
                            }

                            int totalRemovableSpace = 0;
                            foreach (Facility f in fl)
                            {
                                totalRemovableSpace += f.PositionOccupied;
                            }

                            if (totalRemovableSpace >= hougong.PositionOccupied)
                            {
                                fl.PropertyName = "AIValue";
                                fl.IsNumber = true;
                                fl.SmallToBig = true;
                                fl.ReSort();

                                while (buildAt.FacilityPositionLeft < hougong.PositionOccupied && fl.Count > 0)
                                {
                                    Facility f = fl[0] as Facility;
                                    if (buildAt.FacilityEnabled || f.MaintenanceCost <= 0)
                                    {
                                        f.Influences.PurifyInfluence(this, Applier.Facility, f.ID);
                                    }
                                    buildAt.Facilities.Remove(f);
                                    Session.Current.Scenario.Facilities.Remove(f);
                                    fl.Remove(f);
                                }
                            }

                            facilityPositionLeft = buildAt.FacilityPositionLeft;
                        }
                        if (facilityPositionLeft >= hougong.PositionOccupied)
                        {
                            if ((this.Fund >= hougong.FundCost) && ((buildAt.BelongedFaction.TechniquePoint + buildAt.BelongedFaction.TechniquePointForFacility) >= hougong.PointCost))
                            {
                                buildAt.PlanFacilityKind = null;
                                buildAt.BelongedFaction.DepositTechniquePointForFacility(hougong.PointCost);
                                buildAt.BeginToBuildAFacility(hougong);
                            }
                            else
                            {
                                buildAt.PlanFacilityKind = hougong;
                                if (GameObject.Chance(0x21) && ((buildAt.BelongedFaction.TechniquePoint + buildAt.BelongedFaction.TechniquePointForFacility) < buildAt.PlanFacilityKind.PointCost))
                                {
                                    buildAt.BelongedFaction.SaveTechniquePointForFacility(buildAt.PlanFacilityKind.PointCost / buildAt.PlanFacilityKind.Days);
                                }
                            }
                        }
                    }
                }
            }

            //nafei
            if (leader.WaitForFeiZi != null && leader.Status == PersonStatus.Normal && leader.LocationArchitecture != null &&
                this.Leader.LocationTroop == null)
            {
                if ((this.Leader.LocationArchitecture.Meinvkongjian - this.Leader.LocationArchitecture.Feiziliebiao.Count <= 0 && ! this.IsAlien) ||
                    !this.Leader.isLegalFeiZiExcludeAge(leader.WaitForFeiZi) ||
                    this.Leader.WaitForFeiZi.BelongedFaction != this)
                {
                    leader.WaitForFeiZi.WaitForFeiZi = null;
                    leader.WaitForFeiZi = null;
                }
                else if (this.Leader.LocationArchitecture.Fund >= Session.Parameters.NafeiCost)
                {
                    if (this.Leader.WaitForFeiZi.LocationArchitecture == this.Leader.LocationArchitecture &&
                        this.Leader.WaitForFeiZi.Status == PersonStatus.Normal)
                    {
                        this.Leader.XuanZeMeiNv(this.Leader.WaitForFeiZi);
                        this.Leader.WaitForFeiZi.WaitForFeiZi = null;
                        this.Leader.WaitForFeiZi = null;
                    }
                }
            }
            else if (this.Leader.Status == PersonStatus.Normal && this.Leader.LocationArchitecture != null &&
                this.Leader.LocationTroop == null && this.Leader.WaitForFeiZi == null && (TotalFeiziCount() < Session.Current.Scenario.Parameters.AIMaxFeizi || this.IsAlien))
            {
                Architecture dest = null;
                if ((this.Leader.LocationArchitecture.Meinvkongjian - this.Leader.LocationArchitecture.Feiziliebiao.Count > 0 && 
                    this.Leader.LocationArchitecture.Fund >= Session.Parameters.NafeiCost + this.Leader.LocationArchitecture.EnoughFund) || this.IsAlien)
                {
                    dest = this.Leader.LocationArchitecture;
                }
                else
                {
                    foreach (Architecture a in this.Architectures)
                    {
                        if (((a.Meinvkongjian - a.Feiziliebiao.Count > 0 && a.Fund >= Session.Parameters.NafeiCost + a.EnoughFund) || this.IsAlien) 
                            && (dest == null || a.Population > dest.Population))
                        {
                            dest = a;
                        }
                    }
                }

                if (dest != null)
                {
                    PersonList candidate = new PersonList();
                    foreach (Architecture a in this.Architectures)
                    {
                        foreach (Person p in a.nvxingwujiang())
                        {
                            Person spousePerson = p.Spouse == null ? null : p.Spouse;
                            if (IsPersonForHouGong(p) && p.WaitForFeiZi == null && p.BelongedArchitecture != null && !p.IsCaptive)
                            {
                                candidate.Add(p);
                            }
                        }
                    }
                   
                    if (this.IsAlien)
                    {
                        foreach (Architecture a in this.Architectures)
                        {
                            foreach (Person p in a.NoFactionPersons)
                            {
                                if (!this.Leader.isLegalFeiZiExcludeAge(p) || !p.isLegalFeiZiExcludeAge(this.Leader)) continue;
                                Person spousePerson = p.Spouse == null ? null : p.Spouse;
                                if (IsPersonForHouGong(p) && p.WaitForFeiZi == null && p.BelongedArchitecture != null && !p.IsCaptive)
                                {
                                    candidate.Add(p);
                                }
                            }
                        }
                        foreach (Captive c in this.Captives)
                        {
                            Person p = c.CaptivePerson;
                            if (!this.Leader.isLegalFeiZiExcludeAge(p) || !p.isLegalFeiZiExcludeAge(this.Leader)) continue;
                            Person spousePerson = p.Spouse == null ? null : p.Spouse;
                            if (IsPersonForHouGong(p) && p.WaitForFeiZi == null && p.BelongedArchitecture != null)
                            {
                                candidate.Add(p);
                            }
                        }
                    }
                    candidate.PropertyName = "UntiredMerit";
                    candidate.IsNumber = true;
                    candidate.SmallToBig = false;
                    candidate.ReSort();
                    Person toTake = null;
                    foreach (Person p in candidate)
                    {
                        if (p.Status != PersonStatus.Moving && p.Status != PersonStatus.Princess && p.LocationArchitecture != null && p.LocationTroop == null)
                        {
                            if ((!p.RecruitableBy(this, 0) && GameObject.Random((int)unAmbition) == 0) || GameObject.Chance((int)(Session.Parameters.AINafeiSkipChanceAdd + (int)leader.Ambition * Session.Parameters.AINafeiSkipChanceMultiply)))
                            {
                                toTake = p;
                                break;
                            }
                        }
                    }

                    if (toTake != null)
                    {
                        if (this.Leader.LocationArchitecture == dest)
                        {
                            if (toTake.LocationArchitecture == dest)
                            {
                                this.Leader.XuanZeMeiNv(toTake);
                                toTake.WaitForFeiZi = null;
                                leader.WaitForFeiZi = null;
                            }
                            else if (toTake.Status == PersonStatus.Normal)
                            {
                                toTake.MoveToArchitecture(dest);
                                toTake.WaitForFeiZi = this.Leader;
                                this.Leader.WaitForFeiZi = toTake;
                            }
                            else if (this.IsAlien)
                            {
                                this.Leader.XuanZeMeiNv(toTake);
                                toTake.WaitForFeiZi = null;
                                leader.WaitForFeiZi = null;
                            }
                        }
                        else
                        {
                            if (toTake.LocationArchitecture == dest)
                            {
                                this.Leader.MoveToArchitecture(dest);
                                toTake.WaitForFeiZi = this.Leader;
                                this.Leader.WaitForFeiZi = toTake;
                            }
                            else if (toTake.Status == PersonStatus.Normal)
                            {
                                this.Leader.MoveToArchitecture(dest);
                                toTake.MoveToArchitecture(dest);
                                toTake.WaitForFeiZi = this.Leader;
                                this.Leader.WaitForFeiZi = toTake;
                            }
                            else if (this.IsAlien)
                            {
                                this.Leader.XuanZeMeiNv(toTake);
                                toTake.WaitForFeiZi = null;
                                leader.WaitForFeiZi = null;
                            }
                        }
                    }
                }
            }

            //chongxing
            if (this.Leader.Status == PersonStatus.Normal && this.Leader.LocationArchitecture != null && this.Leader.LocationTroop == null &&
                !this.Leader.huaiyun && this.Leader.WaitForFeiZi == null)
            {
                if ((
                GameObject.Chance((int)((int)this.Leader.Ambition * Session.Parameters.AIChongxingChanceMultiply + Session.Parameters.AIChongxingChanceAdd))
                ||
                GameObject.Chance((int)Math.Round(Session.Parameters.AIHougongArchitectureCountProbMultiply * Math.Pow(this.ArchitectureCount, Session.Parameters.AIHougongArchitectureCountProbPower)))
                ))
                {
                    Person target = null;
                    Architecture location = null;
                    float max = 0;
                    foreach (Architecture a in this.Architectures)
                    {
                        foreach (Person p in a.meifaxianhuaiyundefeiziliebiao())
                        {
                            if (p.huaiyun) continue;
                            if (IsPersonForHouGong(p, true))
                            {
                                float v = p.UntiredMerit * p.PregnancyRate(this.Leader);
                                if (p.Hates(this.Leader))
                                {
                                    v /= 100;
                                }
                                if (p.Spouse != null)
                                {
                                    v /= 2;
                                }
                                if (p.GetRelation(this.Leader) < 0)
                                {
                                    v *= Math.Abs(p.GetRelation(this.Leader)) / 50;
                                }
                                if (v > max)
                                {
                                    target = p;
                                    location = a;
                                    max = v;
                                }
                            }
                        }
                    }
                    if (target != null)
                    {
                        if (location == this.Leader.LocationArchitecture)
                        {
                            this.Leader.GoForHouGong(target);
                        }
                        else
                        {
                            this.Leader.MoveToArchitecture(location);
                        }
                    }
                }
            }
        }

        private void AIDiplomacy()
        {
            if (this.Leader.Status == PersonStatus.Captive) return;

            foreach (Faction f in Session.Current.Scenario.PlayerFactions) 
            {
                if (!this.adjacentTo(f)) continue;
                if (this.IsFriendly(f)) continue;
                if (this == f) continue;
                if (GameObject.Random(1000) < Session.Parameters.AIEncirclePlayerRate && GameObject.Chance(f.ArchitectureCount))
                {
                    if (GetEncircleFactionList(f, true) == null) continue;
                    foreach (Architecture a in this.Architectures)
                    {
                        if (a.Fund > 120000 + a.AbundantFund)
                        {
                            Encircle(a, f);
                            return;
                        }
                    }
                }
            }

            if (GameObject.Random(180 * Math.Max(1, 5 - this.Leader.Ambition)) == 0 && GameObject.Chance(100 - Session.Parameters.AIEncirclePlayerRate))
            {
                GameObjectList factions = this.GetAdjecentHostileFactions();
                if (factions.Count == 0) return;

                factions.PropertyName = "Power";
                factions.IsNumber = true;
                factions.SmallToBig = false;
                factions.ReSort();

                int rank = Session.Parameters.AIEncircleRank + GameObject.Random(Session.Parameters.AIEncircleVar * 2) - Session.Parameters.AIEncircleVar;
                rank = Math.Min(rank, 100);
                rank = Math.Max(rank, 0);
                Faction target = (Faction)factions[(factions.Count - 1) * rank / 100];
                int rel = Session.Current.Scenario.GetDiplomaticRelation(this.ID, target.ID);
                if (target != this && rel < 0 && GetEncircleFactionList(target, true) != null)
                {
                    if (GameObject.Chance(Math.Abs(rel) / 10))
                    {
                        foreach (Architecture a in this.Architectures)
                        {
                            if (a.Fund > 120000 + a.AbundantFund)
                            {
                                Encircle(a, target);
                                return;
                            }
                        }
                    }
                }
            }
        }

        private int? powerCache = null;
        public int Power
        {
            get
            {
                if (powerCache == null)
                {
                    powerCache = this.ArchitectureCount * 10000 + this.TotalPersonMerit / 10 + this.Population / 10 + this.ArmyScale * 100;
                }
                return powerCache.Value;
            }
        }

        private void AI()
        {
            Session.Current.Scenario.Threading = true;
            this.AIFinished = false;
            this.AIPrepare();
            this.AIDiplomacy();
            this.AISections();
            this.AICapital();
            this.AICaptives();
            this.AITechniques();
            this.AIMakeMarriage();
            this.AISelectPrince();
            this.AIZhaoXian();
            this.AIAppointMayor();
            this.AIHouGong();
            this.AIArchitectures();
            this.AITransfer();
            this.AILegions();
            this.AITrainChildren();
            this.AIFinished = true;
            Session.Current.Scenario.Threading = false;
        }

        class DistanceComparer : IComparer<GameObject>
        {
            private Architecture target;
            public DistanceComparer(Architecture target)
            {
                this.target = target;
            }

            public int Compare(GameObject x, GameObject y)
            {
                double a = Session.Current.Scenario.GetDistance(target.Position, ((Architecture)x).Position);
                double b = Session.Current.Scenario.GetDistance(target.Position, ((Architecture)y).Position);
                if (a > b)
                {
                    return 1;
                }
                else if (a < b)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
        }

        private void WithdrwalTransfer(ArchitectureList architectures)
        {
            foreach (Architecture a in architectures)
            {
                if (a.Abandoned)
                {
                    a.WithdrawPerson();
                    a.WithdrawMilitaries();
                }
                if (a.HasHostileTroopsInView())
                {
                    a.WithdrawResources();
                }
            }
        }

        public void FullTransfer(ArchitectureList srcArch, ArchitectureList destArch, bool resource, bool person, bool military)
        {
            foreach (Architecture a in srcArch)
            {
                if (a.Abandoned) continue;

                List<GameObject> candidates = new List<GameObject>(destArch.GameObjects);
                candidates.Sort(new DistanceComparer(a));

                if (a.Fund >= a.FundCeiling * 0.9 && resource && a.IsFundAbundant)
                {
                    foreach (Architecture b in candidates)
                    {
                        if (b.Fund + b.FundInPack < b.FundCeiling * 0.8 && !b.Abandoned && b != a)
                        {
                            int toTransfer = (int) (Math.Min(a.Fund - a.FundCeiling * (a.FrontLine ? 0.7 : 0.5), b.FundCeiling * 0.8 - b.Fund - b.FundInPack));
                            b.CallResource(a, toTransfer, 0);
                            if (a.Fund < a.FundCeiling * 0.9) break;
                        }
                    }
                }
                if (a.Food >= a.FoodCeiling * 0.9 && resource && a.IsFoodAbundant)
                {
                    foreach (Architecture b in candidates)
                    {
                        if (b.Food + b.FoodInPack < b.FoodCeiling * 0.8 && !b.Abandoned && b != a)
                        {
                            int toTransfer = (int)(Math.Min(a.Food - a.FoodCeiling * (a.FrontLine ? 0.7 : 0.5), b.FoodCeiling * 0.8 - b.Food - b.FoodInPack));
                            b.CallResource(a, 0, toTransfer);
                            if (a.Food < a.FoodCeiling * 0.9) break;
                        }
                    }
                }

                if (a.IsTroopExceedsLimit && military && !a.FrontLine)
                {
                    int toSend = a.ArmyScale / 2;
                    foreach (Architecture b in candidates)
                    {
                        if (b.FrontLine && b.IsFoodTwiceAbundant && !b.Abandoned & b != a)
                        {
                            int sent = b.CallMilitary(a, toSend);
                            toSend -= sent;
                            if (toSend <= 0) break;
                        }
                    }
                }
            }
        }

        public void PersonRegroupTransfer(ArchitectureList archs)
        {
            if (GameObject.Random(30 / Session.Current.Scenario.Parameters.DayInTurn) == 0)
            {
                foreach (Architecture a in archs)
                {
                    foreach (Person p in a.Persons)
                    {
                        if (p.Status == PersonStatus.Normal && p.LocationArchitecture != null && p.LocationTroop == null)
                        {
                            if (p.Spouse != null && p.Spouse.Status == PersonStatus.Normal && p.Spouse.BelongedFaction == p.BelongedFaction && p.Spouse.BelongedArchitecture.BelongedSection.AIDetail.AutoRun 
                                && p.LocationArchitecture != p.Spouse.LocationArchitecture && p.Spouse.LocationArchitecture != null && p.Spouse.LocationTroop == null)
                            {
                                foreach (Military m in p.Spouse.LeadingArmies)
                                {
                                    p.Spouse.LocationArchitecture.TransferMilitary(m, p.LocationArchitecture);
                                }
                                p.Spouse.MoveToArchitecture(p.LocationArchitecture);
                            }
                            
                            if (p.Brothers.Count > 0)
                            {
                                foreach (Person q in p.Brothers)
                                {
                                    if (q != null && q.Status == PersonStatus.Normal && q.BelongedFaction == p.BelongedFaction && q.BelongedArchitecture.BelongedSection.AIDetail.AutoRun 
                                            && p.LocationArchitecture != q.LocationArchitecture && q.LocationArchitecture != null && q.LocationTroop == null)
                                    {
                                        foreach (Military m in q.LeadingArmies)
                                        {
                                            q.LocationArchitecture.TransferMilitary(m, p.LocationArchitecture);
                                        }
                                        q.MoveToArchitecture(p.LocationArchitecture);
                                    }
                                }
                            }
                        }

                    }
                }
            }
        }

        public void AllocationTransfer(ArchitectureList srcArch, ArchitectureList destArch, bool resource, bool person, bool military)
        {
            ArchitectureList scope = new ArchitectureList();

            Dictionary<Architecture, int> minPerson = new Dictionary<Architecture, int>();
            Dictionary<Architecture, int> minFund = new Dictionary<Architecture, int>();
            Dictionary<Architecture, int> minFood = new Dictionary<Architecture, int>();
            Dictionary<Architecture, int> minTroop = new Dictionary<Architecture, int>();
            Dictionary<Architecture, int> goodPerson = new Dictionary<Architecture, int>();
            Dictionary<Architecture, int> goodFund = new Dictionary<Architecture, int>();
            Dictionary<Architecture, int> goodFood = new Dictionary<Architecture, int>();
            Dictionary<Architecture, int> goodTroop = new Dictionary<Architecture, int>();
            bool urgent = false;

            int totalPerson = 0;
            int totalFrontline = 0;
            foreach (Architecture a in srcArch)
            {
                foreach (Person p in a.Persons)
                {
                    if (p.Command > 50)
                    {
                        totalPerson++;
                    }
                }
                if (a.FrontLine)
                {
                    totalFrontline++;
                }
            }
            int avgFrontlinePerson;
            if (totalFrontline == 0)
            {
                avgFrontlinePerson = 0;
            }
            else
            {
                avgFrontlinePerson = totalPerson / totalFrontline;
            }

            foreach (Architecture a in srcArch.GameObjects.Union(destArch.GameObjects))
            {
                if (!a.Abandoned)
                {
                    scope.Add(a);

                    if (a.HasHostileTroopsInView() || a.RecentlyAttacked > 0)
                    {
                        minPerson.Add(a, a.EnoughPeople + a.JianzhuGuimo * 3);
                        minTroop.Add(a, a.TroopReserveScale); // defensiveCampaign will deal with this
                        urgent = true;
                    }
                    else if (a.PlanArchitecture != null)
                    {
                        minPerson.Add(a, Math.Min(avgFrontlinePerson, a.EnoughPeople));
                        minTroop.Add(a, a.TroopReserveScale);
                    }
                    else if (a.FrontLine)
                    {
                        minPerson.Add(a, Math.Min(3, a.EnoughPeople));
                        minTroop.Add(a, a.TroopReserveScale);
                    }
                    else if (a.IsNetLosingPopulation)
                    {
                        minPerson.Add(a, a.EnoughPeople);
                        minTroop.Add(a, 0);
                    }
                    else
                    {
                        minPerson.Add(a, 0);
                        minTroop.Add(a, 0);
                    }
                    minFund.Add(a, Math.Min(a.FundCeiling * 9 / 10, a.EnoughFund));
                    minFood.Add(a, Math.Min(a.FoodCeiling * 9 / 10, a.EnoughFood));

                    if (a.HostileLine || a.orientationFrontLine)
                    {
                        int troop = Math.Max(a.HostileScale, a.OrientationScale);
                        if (a.IsVeryGood())
                        {
                            goodPerson.Add(a, Math.Max(avgFrontlinePerson, minPerson[a]));
                        }
                        else
                        {
                            goodPerson.Add(a, Math.Max(Math.Max(avgFrontlinePerson, a.EnoughPeople), minPerson[a]));
                        }
                        goodTroop.Add(a, Math.Max(troop, minTroop[a]));
                        goodFood.Add(a, Math.Min(a.FoodCeiling * 9 / 10, Math.Max(a.AbundantFood * 2, minFood[a])));
                        goodFund.Add(a, Math.Min(a.FundCeiling * 9 / 10, Math.Max(a.AbundantFund, minFund[a])));
                    }
                    else if (a.FrontLine)
                    {
                        goodPerson.Add(a, Math.Max(avgFrontlinePerson, minPerson[a]));
                        goodTroop.Add(a, Math.Max(a.TroopReserveScale, minTroop[a]));
                        goodFood.Add(a, Math.Min(a.FoodCeiling * 9 / 10, Math.Max(a.AbundantFood * 2, minFood[a])));
                        goodFund.Add(a, Math.Min(a.FundCeiling * 9 / 10, Math.Max(a.AbundantFund, minFund[a])));
                    }
                    else if (!a.IsVeryGood())
                    {
                        goodPerson.Add(a, Math.Max(a.EnoughPeople, minPerson[a]));
                        goodTroop.Add(a, Math.Max(a.TroopReserveScale, minTroop[a]));
                        goodFund.Add(a, Math.Min(a.FundCeiling * 9 / 10, Math.Max(a.AbundantFund, minFund[a])));
                        goodFood.Add(a, Math.Min(a.FoodCeiling * 9 / 10, Math.Max(a.AbundantFood * 2, minFood[a])));
                    }
                    else
                    {
                        goodPerson.Add(a, Math.Max(1, minPerson[a]));
                        goodTroop.Add(a, Math.Max(a.TroopReserveScale, minTroop[a]));
                        goodFund.Add(a, Math.Min(a.FundCeiling * 9 / 10, Math.Max(a.AbundantFund, minFund[a])));
                        goodFood.Add(a, Math.Min(a.FoodCeiling * 9 / 10, Math.Max(a.AbundantFood * 2, minFood[a])));
                    }
                }
            }

            scope.PropertyName = "Population";
            scope.SmallToBig = false;
            scope.IsNumber = true;
            scope.ReSort();

            for (int priority = 0; priority < 3; ++priority)
            {
                foreach (Architecture a in destArch)
                {
                    if (a.Abandoned) continue;

                    if (!a.HasHostileTroopsInView() && priority < 1) continue;
                    if (a.HasHostileTroopsInView() && priority >= 1) continue;

                    if (!a.FrontLine && priority < 2) continue;
                    if (a.FrontLine && priority >= 2) continue;

                    if (a.ArmyScale < minTroop[a] && military && (a.SuspendTroopTransfer <= 0 || urgent))
                    {
                        int deficit = minTroop[a] * 2 - a.ArmyScale;

                        List<GameObject> candidates = new List<GameObject>(srcArch.GameObjects);
                        candidates.Sort(new DistanceComparer(a));

                        foreach (Architecture b in candidates)
                        {
                            if (b.Abandoned || b == a) continue;
                            if (b.ArmyScale > goodTroop[b])
                            {
                                int send = Math.Min(deficit, b.ArmyScale - goodTroop[b]);
                                send = a.CallMilitary(b, send);
                                if (send > 0)
                                {
                                    deficit -= send;
                                    if (deficit <= 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (deficit > 0)
                        {
                            foreach (Architecture b in candidates)
                            {
                                if (b.Abandoned || b == a) continue;
                                int send = Math.Min(deficit, b.ArmyScale - minTroop[b]);
                                send = a.CallMilitary(b, send);
                                if (send > 0)
                                {
                                    deficit -= send;
                                    if (deficit <= 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (a.PersonCount + a.MovingPersonCount < minPerson[a] && person)
                    {
                        int deficit = minPerson[a] - a.PersonCount - a.MovingPersonCount;

                        List<GameObject> candidates = new List<GameObject>(srcArch.GameObjects);
                        candidates.Sort(new DistanceComparer(a));

                        foreach (Architecture b in candidates)
                        {
                            if (b.Abandoned || b == a) continue;
                            if (b.PersonCount + b.MovingPersonCount > goodPerson[b])
                            {
                                int send = Math.Min(deficit, b.PersonCount + b.MovingPersonCount - goodPerson[b]);
                                send = a.CallPeople(b, send);
                                if (send > 0)
                                {
                                    deficit -= send;
                                    if (deficit <= 0)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        if (deficit > 0)
                        {
                            foreach (Architecture b in candidates)
                            {
                                if (b.Abandoned || b == a) continue;
                                if (b.PersonCount + b.MovingPersonCount > minPerson[b])
                                {
                                    int send = Math.Min(deficit, b.PersonCount + b.MovingPersonCount - minPerson[b]);
                                    send = a.CallPeople(b, send);
                                    if (send > 0)
                                    {
                                        deficit -= send;
                                        if (deficit <= 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        if (deficit > 0 && urgent)
                        {
                            foreach (Architecture b in candidates)
                            {
                                if (b.Abandoned || b == a) continue;
                                if (b.PersonCount + b.MovingPersonCount > 1 && !b.HasHostileTroopsInView() && b.RecentlyAttacked == 0)
                                {
                                    int send = Math.Min(deficit, b.PersonCount + b.MovingPersonCount - minPerson[b]);
                                    send = a.CallPeople(b, send);
                                    if (send > 0)
                                    {
                                        deficit -= send;
                                        if (deficit <= 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }

                    if ((a.Fund + a.FundInPack < minFund[a] || a.Food + a.FoodInPack < minFood[a]) && resource)
                    {
                        int deficitFund = Math.Max(0, minFund[a] * 2 - a.Fund - a.FundInPack);
                        int deficitFood = Math.Max(0, minFood[a] * 2 - a.Food - a.FoodInPack);
                        deficitFood = Math.Min(deficitFood, a.FoodCeiling * 9 / 10 - a.FoodInPack - a.Food);
                        deficitFund = Math.Min(deficitFund, a.FundCeiling * 9 / 10- a.FundInPack - a.Fund);

                        if (deficitFund > 0 || deficitFood > 0)
                        {
                            List<GameObject> candidates = new List<GameObject>(srcArch.GameObjects);
                            candidates.Sort(new DistanceComparer(a));

                            foreach (Architecture b in candidates)
                            {
                                if (b.Abandoned || b == a) continue;
                                if (!b.withoutTruceFrontline && !b.HasHostileTroopsInView())
                                {
                                    if (b.Fund >= goodFund[b] * 2 || b.Food >= goodFood[b] * 2)
                                    {
                                        int transferFund = Math.Max(0, Math.Min(deficitFund, b.Fund - goodFund[b] * 2));
                                        int transferFood = Math.Max(0, Math.Min(deficitFood, b.Food - goodFood[b] * 2));
                                        if (a.CallResource(b, transferFund, transferFood))
                                        {
                                            deficitFund -= transferFund;
                                            deficitFood -= transferFood;
                                            if (deficitFood <= 0 && deficitFund <= 0)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                            if (deficitFood > 0 || deficitFund > 0)
                            {
                                foreach (Architecture b in candidates)
                                {
                                    if (b.Abandoned || b == a) continue;
                                    if (!b.HasHostileTroopsInView())
                                    {
                                        if (b.Fund >= goodFund[b] * 2 || b.Food >= goodFood[b] * 2)
                                        {
                                            int transferFund = Math.Max(0, Math.Min(deficitFund, b.Fund - goodFund[b] * 2));
                                            int transferFood = Math.Max(0, Math.Min(deficitFood, b.Food - goodFood[b] * 2));
                                            if (a.CallResource(b, transferFund, transferFood))
                                            {
                                                deficitFund -= transferFund;
                                                deficitFood -= transferFood;
                                                if (deficitFood <= 0 && deficitFund <= 0)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (deficitFood > 0 || deficitFund > 0)
                            {
                                foreach (Architecture b in candidates)
                                {
                                    if (b.Abandoned || b == a) continue;
                                    if (!b.HasHostileTroopsInView())
                                    {
                                        if (b.Fund >= goodFund[b] || b.Food >= goodFood[b])
                                        {
                                            int transferFund = Math.Max(0, Math.Min(deficitFund, b.Fund - goodFund[b]));
                                            int transferFood = Math.Max(0, Math.Min(deficitFood, b.Food - goodFood[b]));
                                            if (a.CallResource(b, transferFund, transferFood))
                                            {
                                                deficitFund -= transferFund;
                                                deficitFood -= transferFood;
                                                if (deficitFood <= 0 && deficitFund <= 0)
                                                {
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

            foreach (Architecture a in destArch)
            {
                if (a.Abandoned) continue;

                if (a.ArmyScale < goodTroop[a] && military && a.SuspendTroopTransfer <= 0)
                {
                    int deficit = goodTroop[a] * 2 - a.ArmyScale;

                    List<GameObject> candidates = new List<GameObject>(srcArch.GameObjects);
                    candidates.Sort(new DistanceComparer(a));

                    foreach (Architecture b in candidates)
                    {
                        if (b.Abandoned || b == a) continue;
                        if (b.ArmyScale > goodTroop[b])
                        {
                            int send = Math.Min(deficit, b.ArmyScale - goodTroop[b]);
                            send = a.CallMilitary(b, send);
                            if (send > 0)
                            {
                                deficit -= send;
                                if (deficit <= 0)
                                {
                                    break;
                                }
                            }
                        }
                    }

                }

                if (a.PersonCount + a.MovingPersonCount < goodPerson[a] && person)
                {
                    int deficit = goodPerson[a] - a.PersonCount - a.MovingPersonCount;

                    List<GameObject> candidates = new List<GameObject>(srcArch.GameObjects);
                    candidates.Sort(new DistanceComparer(a));

                    foreach (Architecture b in candidates)
                    {
                        if (b.Abandoned || b == a) continue;
                        if (b.PersonCount + b.MovingPersonCount > goodPerson[b])
                        {
                            int send = Math.Min(deficit, b.PersonCount + b.MovingPersonCount - goodPerson[b]);
                            send = a.CallPeople(b, send);
                            if (send > 0)
                            {
                                deficit -= send;
                                if (deficit <= 0)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                if ((a.Fund + a.FundInPack < goodFund[a] || a.Food + a.FoodInPack < goodFood[a]) && resource)
                {
                    int deficitFund = Math.Max(0, goodFund[a] * 2 - a.Fund - a.FundInPack);
                    int deficitFood = Math.Max(0, goodFood[a] * 2 - a.Food - a.FoodInPack);
                    deficitFood = Math.Min(deficitFood, a.FoodCeiling * 9 / 10 - a.FoodInPack - a.Food);
                    deficitFund = Math.Min(deficitFund, a.FundCeiling * 9 / 10 - a.FundInPack - a.Fund);

                    if (deficitFund > 0 || deficitFood > 0)
                    {
                        List<GameObject> candidates = new List<GameObject>(srcArch.GameObjects);
                        candidates.Sort(new DistanceComparer(a));

                        foreach (Architecture b in candidates)
                        {
                            if (b.Abandoned || b == a) continue;
                            if (!b.withoutTruceFrontline && !b.HasHostileTroopsInView())
                            {
                                if (b.Fund >= goodFund[b] * 2 || b.Food >= goodFood[b] * 2)
                                {
                                    int transferFund = Math.Max(0, Math.Min(deficitFund, b.Fund - goodFund[b] * 2));
                                    int transferFood = Math.Max(0, Math.Min(deficitFood, b.Food - goodFood[b] * 2));
                                    if (a.CallResource(b, transferFund, transferFood))
                                    {
                                        deficitFund -= transferFund;
                                        deficitFood -= transferFood;
                                        if (deficitFood <= 0 && deficitFund <= 0)
                                        {
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                        if (deficitFood > 0 || deficitFund > 0)
                        {
                            foreach (Architecture b in candidates)
                            {
                                if (b.Abandoned || b == a) continue;
                                if (!b.HasHostileTroopsInView())
                                {
                                    if (b.Fund >= goodFund[b] * 2 || b.Food >= goodFood[b] * 2)
                                    {
                                        int transferFund = Math.Max(0, Math.Min(deficitFund, b.Fund - goodFund[b] * 2));
                                        int transferFood = Math.Max(0, Math.Min(deficitFood, b.Food - goodFood[b] * 2));
                                        if (a.CallResource(b, transferFund, transferFood))
                                        {
                                            deficitFund -= transferFund;
                                            deficitFood -= transferFood;
                                            if (deficitFood <= 0 && deficitFund <= 0)
                                            {
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

        public void AIVeryClosePersonTransfer()
        {
            if (GameObject.Chance(10))
            {
                foreach (Person p in this.Persons)
                {
                    if (p.LocationArchitecture != null && p.LocationTroop == null && p.Status == PersonStatus.Normal)
                    {
                        foreach (Person q in p.AvailableVeryClosePersons)
                        {
                            if (q.LocationArchitecture != null && q.LocationTroop == null && q.LocationArchitecture != p.LocationArchitecture &&
                                q.Status == PersonStatus.Normal && !q.DontMoveMeUnlessIMust && !this.MayorList.GameObjects.Contains(q))
                            {
                                if (Session.Current.Scenario.IsPlayer(this))
                                {
                                    if (p.LocationArchitecture.BelongedSection.AIDetail.AutoRun && q.LocationArchitecture.BelongedSection.AIDetail.AutoRun)
                                    {
                                        q.MoveToArchitecture(p.LocationArchitecture);
                                    }
                                }
                                else
                                {
                                    q.MoveToArchitecture(p.LocationArchitecture);
                                }
                            }
                        }
                    }
                }
            }
        }


        public void AITransferPlanning(ArchitectureList architectures)
        {
            WithdrwalTransfer(architectures);
            AllocationTransfer(architectures, architectures, true, true, true);
            PersonRegroupTransfer(architectures);
            if (GameObject.Chance(10))
            {
                FullTransfer(architectures, architectures, true, true, true);
            }
        }

        private void PlayerAITransfer()
        {
            foreach (Section s in this.Sections)
            {
                if (s.AIDetail.AutoRun)
                {
                    s.AIIntraTransfer();
                    if (s.AIDetail.AllowFoodTransfer || s.AIDetail.AllowFundTransfer || s.AIDetail.AllowMilitaryTransfer)
                    {
                        s.AIInterTransfer();
                    }
                }
            }
        }

        private void AITransfer()
        {
            if (this.Architectures.Count > 1)
            {
                this.AITransferPlanning(this.Architectures);
            }
        }

        private void AIArchitectures()
        {
            foreach (Architecture architecture in this.Architectures.GetRandomList())
            {
                architecture.AI();
            }
        }

        private void AICapital()
        {
            if ((this.ArchitectureCount != 0) && (this.Capital != null))
            {
                Architecture architecture;
                int num2;
                if ((this.Capital.Endurance < 30) && (this.Capital.RecentlyAttacked > 0))
                {
                    if (this.Capital.ChangeCapitalAvail() && this.Capital.HasHostileTroopsInView())
                    {
                        float rationRate = 0f;
                        if (this.Capital.GetRelationUnderZeroTroopFightingForceInView(out rationRate) > (this.Capital.GetFriendlyTroopFightingForceInView() * 3))
                        {
                            this.Capital.DecreaseFund(this.Capital.ChangeCapitalCost);
                            architecture = this.SelectNewCapital();
                            if (this.Capital.Fund > this.Capital.EnoughFund)
                            {
                                num2 = this.Capital.Fund - this.Capital.EnoughFund;
                                this.Capital.DecreaseFund(num2);
                                //architecture.AddFundPack(num2, (int)(Session.Current.Scenario.GetDistance(this.Capital.ArchitectureArea, architecture.ArchitectureArea) / 5.0));
                                architecture.AddFundPack(num2, (int)(Session.Current.Scenario.GetDistance(this.Capital.ArchitectureArea, architecture.ArchitectureArea) / 5.0));
                            }
                            this.ChangeCapital(architecture);
                        }
                    }
                }
                else if (this.Capital.ChangeCapitalAvail())
                {
                    architecture = this.SelectNewCapital();
                    if (((architecture != null) && (architecture.Population > (this.Capital.Population + 0x2710))) && ((architecture.Endurance * architecture.Domination) > (this.Capital.Endurance * this.Capital.Domination)))
                    {
                        this.Capital.DecreaseFund(this.Capital.ChangeCapitalCost);
                        if (this.Capital.Fund > this.Capital.EnoughFund)
                        {
                            num2 = this.Capital.Fund - this.Capital.EnoughFund;
                            this.Capital.DecreaseFund(num2);
                            //architecture.AddFundPack(num2, (int)(Session.Current.Scenario.GetDistance(this.Capital.ArchitectureArea, architecture.ArchitectureArea) / 5.0));
                            architecture.AddFundPack(num2, (int)(Session.Current.Scenario.GetDistance(this.Capital.ArchitectureArea, architecture.ArchitectureArea) / 5.0));
                        }
                        this.ChangeCapital(architecture);
                    }
                }
            }
        }

        private void AICaptives()
        {
            this.AISelfReleaseCaptives();
            this.AIRedeemCaptives();
        }

        private void AILegions()
        {
            foreach (Legion legion in this.Legions.GetRandomList())
            {
                legion.AI();
            }
        }

        private void AITrainChildren()
        {
            if (GameObject.Random(90 / Session.Current.Scenario.Parameters.DayInTurn) == 0)
            {
                foreach (Person p in this.Children)
                {
                    if (!p.Trainable) continue;
                    if (Session.Current.Scenario.IsPlayer(this) && (p.Father == this.Leader || p.Mother == this.Leader)) continue;

                    if (p.Age >= 5 && p.Age < 8)
                    {
                        Dictionary<TrainPolicy, float> candidates = new Dictionary<TrainPolicy, float>();
                        foreach (TrainPolicy tp in Session.Current.Scenario.GameCommonData.AllTrainPolicies)
                        {
                            float c = (p.CommandPotential - p.Command) * tp.Command / tp.WeightSum + 1;
                            float s = (p.StrengthPotential - p.Strength) * tp.Strength / tp.WeightSum + 1;
                            float i = (p.IntelligencePotential - p.Intelligence) * tp.Intelligence / tp.WeightSum + 1;
                            float o = (p.PoliticsPotential - p.Politics) * tp.Politics / tp.WeightSum + 1;
                            float g = (p.GlamourPotential - p.Glamour) * tp.Glamour / tp.WeightSum + 1;
                            candidates.Add(tp, c + s + i + o + g);
                        }
                        p.TrainPolicy = candidates.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                    }
                    else if (p.Age >= 8) 
                    {
                        float unfinishedSkillFactor;
                        int unlearnedSkillCount = 0;
                        int learnedSkillCount = 0;
                        foreach (Skill j in p.Father.GetSkillList())
                        {
                            learnedSkillCount += j.Merit;
                            if (!p.HasSkill(j.ID))
                            {
                                unlearnedSkillCount += j.Merit;
                            }
                        }
                        foreach (Skill j in p.Mother.GetSkillList())
                        {
                            learnedSkillCount += j.Merit;
                            if (!p.HasSkill(j.ID))
                            {
                                unlearnedSkillCount += j.Merit;
                            }
                        }
                        int total = learnedSkillCount + unlearnedSkillCount;
                        if (total > 0)
                        {
                            unfinishedSkillFactor = ((float)unlearnedSkillCount / total);
                        }
                        else
                        {
                            unfinishedSkillFactor = 0;
                        }

                        float unfinishedStuntFactor;
                        int unlearnedStuntCount = 0;
                        int learnedStuntCount = 0;
                        foreach (Stunt j in p.Father.GetStuntList())
                        {
                            learnedStuntCount++;
                            if (!p.HasStunt(j.ID))
                            {
                                unlearnedStuntCount++;
                            }
                        }
                        foreach (Stunt j in p.Mother.GetStuntList())
                        {
                            learnedStuntCount++;
                            if (!p.HasStunt(j.ID))
                            {
                                unlearnedStuntCount++;
                            }
                        }
                        total = learnedStuntCount + unlearnedStuntCount;
                        if (total > 0)
                        {
                            unfinishedStuntFactor = ((float)unlearnedStuntCount / total);
                        }
                        else
                        {
                            unfinishedStuntFactor = 0;
                        }

                        float unfinishedTitleFactor;
                        int unlearnedTitleLevels = 0;
                        int learnedTitleLevels = 0;
                        foreach (TitleKind tk in Session.Current.Scenario.GameCommonData.AllTitleKinds.TitleKinds.Values)
                        {
                            if (!tk.RandomTeachable) continue;
                            Title fatherTitle = p.Father.getTitleOfKind(tk);
                            Title motherTitle = p.Mother.getTitleOfKind(tk);
                            Title pTitle = p.getTitleOfKind(tk);

                            int fm = 0;
                            int mm = 0;
                            if (fatherTitle != null && fatherTitle.CanBeBorn(p))
                            {
                                fm = fatherTitle.Merit;
                            }
                            if (motherTitle != null && motherTitle.CanBeBorn(p))
                            {
                                mm = motherTitle.Merit;
                            }

                            unlearnedTitleLevels += Math.Max(fm, mm);

                            if (pTitle != null)
                            {
                                learnedTitleLevels += pTitle.Merit;
                            }
                        }
                        if (unlearnedTitleLevels > 0)
                        {
                            unfinishedTitleFactor = (float)unlearnedTitleLevels / (unlearnedTitleLevels + learnedTitleLevels);
                        }
                        else
                        {
                            unfinishedTitleFactor = 0;
                        }

                        Dictionary<TrainPolicy, float> candidates = new Dictionary<TrainPolicy, float>();
                        foreach (TrainPolicy tp in Session.Current.Scenario.GameCommonData.AllTrainPolicies)
                        {
                            float c = (p.CommandPotential - p.Command) * tp.Command / tp.WeightSum + 1;
                            float s = (p.StrengthPotential - p.Strength) * tp.Strength / tp.WeightSum + 1;
                            float i = (p.IntelligencePotential - p.Intelligence) * tp.Intelligence / tp.WeightSum + 1;
                            float o = (p.PoliticsPotential - p.Politics) * tp.Politics / tp.WeightSum + 1;
                            float g = (p.GlamourPotential - p.Glamour) * tp.Glamour / tp.WeightSum + 1;

                            float skill = 0;
                            float stunt = 0;
                            float title = 0;
                            int abyMax = Math.Max(Math.Max(Math.Max(Math.Max(p.Strength, p.Command), p.Intelligence), p.Politics), p.Glamour);
                            int csiMax = Math.Max(Math.Max(p.Command, p.Strength), p.Intelligence);
                            if (abyMax > 50)
                            {
                                skill = (abyMax - 50) * (100 / 50.0f) * tp.Skill / tp.WeightSum + 1;
                                skill *= unfinishedSkillFactor;
                            }
                            if (csiMax > 60)
                            {
                                stunt = (csiMax - 60) * (100 / 40.0f) * tp.Stunt / tp.WeightSum + 1;
                                stunt *= unfinishedStuntFactor;
                            }
                            if (abyMax > 70 && p.Age >= 8)
                            {
                                title = (abyMax - 70) * (100 / 30.0f) * tp.Title / tp.WeightSum + 1;
                                title *= unfinishedTitleFactor;
                            }

                            candidates.Add(tp, c + s + i + o + g + skill + stunt + title);
                        }
                        p.TrainPolicy = candidates.Aggregate((x, y) => x.Value > y.Value ? x : y).Key;
                    }
                }
            }
        }

        private void AIPrepare()
        {
            if ((Session.Current.Scenario.Date.Day <= Session.Current.Scenario.Parameters.DayInTurn) && ((Session.Current.Scenario.Date.Month % 3) == 1))
            {
                foreach (Architecture architecture in this.Architectures)
                {
                    architecture.CheckIsFrontLine();
                }
            }
        }

        private void AIRedeemCaptives()
        {
            if (this.HasSelfCaptive())
            {
                foreach (Captive captive in this.SelfCaptives.GetRandomList())
                {
                    if (captive.BelongedFaction == null)
                    {
                        captive.CaptivePerson.SetBelongedCaptive(null, PersonStatus.Normal);
                        if ((captive.CaptivePerson != null) && (captive.CaptiveFaction != null))
                        {
                            captive.CaptivePerson.MoveToArchitecture(captive.CaptiveFaction.Capital);
                        }
                        
                        continue;
                    }
                    if ((captive.BelongedFaction.Capital != null) && (captive.RansomArriveDays <= 0))
                    {
                        if (captive.CaptivePerson == this.Leader && this.Capital.Fund >= captive.Ransom)
                        {
                            captive.SendRansom(captive.BelongedFaction.Capital, this.Capital);
                            continue;
                        }
                        int diplomaticRelation = Session.Current.Scenario.GetDiplomaticRelation(captive.BelongedFaction.ID, base.ID);
                        if ((diplomaticRelation >= 0) || (GameObject.Random(Math.Abs(diplomaticRelation) + 50) < 50))
                        {
                            int ransom = captive.Ransom;
                            if (GameObject.Random(ransom) > GameObject.Random(0x7d0))
                            {
                                foreach (Architecture architecture in captive.BelongedFaction.Capital.GetClosestArchitectures(Session.Current.Scenario.Architectures.Count - 1))
                                {
                                    if ((architecture.BelongedFaction != this) || ((architecture.PlanArchitecture != null) || !(architecture.IsFundEnough || !architecture.HasHostileTroopsInView())))
                                    {
                                        continue;
                                    }
                                    if (architecture.Fund >= ransom)
                                    {
                                        if (GameObject.Random(architecture.Fund) >= (ransom - 1))
                                        {
                                            captive.SendRansom(captive.BelongedFaction.Capital, architecture);
                                            break;
                                        }
                                        if (GameObject.Chance(10))
                                        {
                                            break;
                                        }
                                    }
                                    else if (GameObject.Chance(1))
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AISections()
        {
            if ((this.ArchitectureCount != 0) && (Session.Current.Scenario.GameCommonData.AllSectionAIDetails.Count > 0))
            {
                this.RebuildSections();
                foreach (Section section in this.Sections.GetList())
                {
                    section.AI();
                }
            }
        }

        private void AISelfReleaseCaptives()
        {
            return;
            /*if (this.HasCaptive())
            {
                foreach (Captive captive in this.Captives.GetList())
                {
                    if (((captive.CaptiveFaction == null) || (captive.BelongedFaction == null)) || ((captive.BelongedFaction.Capital == null) || ((captive.LocationTroop != null) && (captive.LocationTroop.RecentlyFighting > 0))))
                    {
                        continue;
                    }
                    int diplomaticRelation = Session.Current.Scenario.GetDiplomaticRelation(captive.CaptiveFaction.ID, base.ID);
                    if ((diplomaticRelation >= 0) && (GameObject.Random(diplomaticRelation + 50) >= GameObject.Random(50)))
                    {
                        captive.SelfReleaseCaptive();
                        continue;
                    }
                    if (diplomaticRelation < 0)
                    {
                        int chance = Math.Abs((int) (diplomaticRelation / 5));
                        if (chance >= 100)
                        {
                            chance = 0x63;
                        }
                        if (!(GameObject.Chance(chance) || (GameObject.Random(Math.Abs(diplomaticRelation) + 50) >= GameObject.Random(50))))
                        {
                            captive.SelfReleaseCaptive();
                            continue;
                        }
                    }
                }
            }*/
        }

        private void AITechniques()
        {
            if ((this.ArchitectureCount != 0) && (this.UpgradingTechnique < 0))
            {
                if (this.PlanTechnique == null)
                {
                    if (this.PreferredTechniqueKinds.Count > 0)
                    {
                        Dictionary<Technique, float> list = new Dictionary<Technique, float>();
                        float preferredTechniqueComplition = this.GetPreferredTechniqueComplition();
                        foreach (Technique technique in Session.Current.Scenario.GameCommonData.AllTechniques.Techniques.Values)
                        {
                            if (!this.IsTechniqueUpgradable(technique))
                            {
                                continue;
                            }
                            if (this.GetTechniqueUsefulness(technique) <= 0) continue;

                            float weight = 1;
                            foreach (KeyValuePair<Condition, float> c in technique.AIConditionWeight)
                            {
                                if (c.Key.CheckCondition(this))
                                {
                                    weight *= c.Value;  
                                }
                            }

                            if (preferredTechniqueComplition < 0.5f)
                            {
                                if (this.PreferredTechniqueKinds.IndexOf(technique.Kind) >= 0)
                                {
                                    list.Add(technique, weight);
                                }
                            }
                            else if (preferredTechniqueComplition < 0.75f)
                            {
                                if ((this.PreferredTechniqueKinds.IndexOf(technique.Kind) >= 0) || GameObject.Chance(0x19))
                                {
                                    list.Add(technique, weight);
                                }
                            }
                            else if (preferredTechniqueComplition < 1f)
                            {
                                if ((this.PreferredTechniqueKinds.IndexOf(technique.Kind) >= 0) || GameObject.Chance(50))
                                {
                                    list.Add(technique, weight);
                                }
                            }
                            else if ((this.PreferredTechniqueKinds.IndexOf(technique.Kind) >= 0) || GameObject.Chance(0x4b))
                            {
                                list.Add(technique, weight);
                            }
                        }
                        if (list.Count > 0)
                        {
                            this.PlanTechnique = GameObject.WeightedRandom(list);
                        }
                        else
                        {
                            this.PlanTechnique = this.GetRandomTechnique();
                        }
                    }
                    else
                    {
                        this.PlanTechnique = this.GetRandomTechnique();
                    }
                }
                if (this.PlanTechnique != null)
                {
                    if (((this.TechniquePoint + this.TechniquePointForTechnique) >= this.getTechniqueActualPointCost(this.PlanTechnique)) && (this.Reputation >= this.getTechniqueActualReputation(this.PlanTechnique)))
                    {
                        if (this.ArchitectureCount > 1)
                        {
                            this.Architectures.PropertyName = "Fund";
                            this.Architectures.IsNumber = true;
                            this.Architectures.ReSort();
                        }
                        Architecture a = this.Architectures[0] as Architecture;
                        if (a.IsFundEnough)
                        {
                            this.PlanTechniqueArchitecture = this.Architectures[0] as Architecture;
                            if (this.PlanTechniqueArchitecture.Fund >= this.getTechniqueActualFundCost(this.PlanTechnique))
                            {
                                this.DepositTechniquePointForTechnique(this.TechniquePointForTechnique);
                                this.UpgradeTechnique(this.PlanTechnique, this.PlanTechniqueArchitecture);
                                this.PlanTechniqueArchitecture = null;
                                this.PlanTechnique = null;
                            }
                        }
                        else
                        {
                            this.PlanTechniqueArchitecture = null;
                            this.PlanTechnique = null;
                        }
                    }
                    else if ((this.Reputation >= this.getTechniqueActualReputation(this.PlanTechnique)) && GameObject.Chance(0x21))
                    {
                        this.SaveTechniquePointForTechnique(this.getTechniqueActualPointCost(this.PlanTechnique) / this.PlanTechnique.Days);
                    }
                    else if (GameObject.Chance(10))
                    {
                        this.PlanTechniqueArchitecture = null;
                        this.PlanTechnique = null;
                    }
                }
            }
        }

        public void ApplyTechniques()
        {
            foreach (Technique technique in this.AvailableTechniques.Techniques.Values)
            {
                technique.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Technique, technique.ID);
            }
        }

        private void SetSectionAIDetailPinAtPlayer()
        {
            if (Session.GlobalVariables.PinPointAtPlayer)
            {
                foreach (Section i in this.Sections)
                {
                    //if ((((this.FirstSection.ArchitectureScale / 2) - (this.FirstSection.ArchitectureCount / 2)) + 1) * 20 <= this.ArmyScale)
                    //{
                    //FactionList playerFactions = Session.Current.Scenario.PlayerFactions.GetRandomList() as FactionList;
                    FactionList playerFactions = Session.Current.Scenario.PlayerFactions;
                    bool assigned = false;
                    foreach (Architecture j in i.Architectures)
                    {
                        foreach (Faction k in playerFactions)
                        {
                            if (j.HasFactionInClose(k, 1))
                            {
                                GameObjectList sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.势力, true, true, true, false, true);
                                if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                {
                                    this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                    this.FirstSection.OrientationFaction = k;
                                    assigned = true;
                                }
                                break;
                            }
                        }
                        if (assigned) break;
                    }
                    //}
                }
            }
        }

        private void BuildSectionByArchitectureList(GameObjectList architecturelist)
        {
            if (architecturelist.Count != 0)
            {
                Section section;
                GameObjectList list;
                if (architecturelist.Count == 1)
                {
                    section = new Section();
                    section.ID = Session.Current.Scenario.Sections.GetFreeGameObjectID();

                    this.AddSection(section);
                    Session.Current.Scenario.Sections.AddSectionWithEvent(section);
                    list = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.无, true, false, true, true, false);
                    if (list.Count > 0)
                    {
                        section.AIDetail = list[GameObject.Random(list.Count)] as SectionAIDetail;
                    }
                    else
                    {
                        section.AIDetail = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailList()[0] as SectionAIDetail;
                    }
                    section.AddArchitecture(architecturelist[0] as Architecture);
                }
                else
                {
                    architecturelist.PropertyName = "Population";
                    architecturelist.IsNumber = true;
                    architecturelist.ReSort();
                    int count = 0;
                    int num2 = 2 + (this.ArchitectureCount / 8);
                    if (architecturelist.Count < ((num2 * 3) / 2))
                    {
                        count = architecturelist.Count;
                    }
                    else
                    {
                        count = num2;
                    }
                    section = new Section();
                    section.ID = Session.Current.Scenario.Sections.GetFreeGameObjectID();
                    
                    this.AddSection(section);
                    Session.Current.Scenario.Sections.AddSectionWithEvent(section);
                    list = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.无, true, false, true, true, false);
                    if (list.Count > 0)
                    {
                        section.AIDetail = list[GameObject.Random(list.Count)] as SectionAIDetail;
                    }
                    else
                    {
                        section.AIDetail = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailList()[0] as SectionAIDetail;
                    }
                    Architecture architecture = architecturelist[0] as Architecture;
                    section.AddArchitecture(architecture);
                    if (architecture.ClosestArchitectures == null)
                    {
                        architecture.GetClosestArchitectures();
                    }
                    if (architecture.AIAllLinkNodes.Count == 0)
                    {
                        architecture.GenerateAllAILinkNodes(2);
                    }
                    foreach (LinkNode node in architecture.AIAllLinkNodes.Values)
                    {
                        if (node.Level > 2)
                        {
                            break;
                        }
                        if ((node.A.BelongedFaction == this) && architecturelist.HasGameObject(node.A))
                        {
                            section.AddArchitecture(node.A);
                        }
                        if (section.ArchitectureCount >= count)
                        {
                            break;
                        }
                    }
                    if (count == architecturelist.Count)
                    {
                        if (section.ArchitectureCount < count)
                        {
                            foreach (Architecture architecture2 in section.Architectures)
                            {
                                architecturelist.Remove(architecture2);
                            }
                            if (this.SectionCount == 1)
                            {
                                foreach (Architecture architecture2 in architecturelist)
                                {
                                    this.FirstSection.AddArchitecture(architecture2);
                                }
                            }
                            else
                            {
                                foreach (Architecture architecture2 in architecturelist)
                                {
                                    int num3 = 0x7fffffff;
                                    Section section2 = null;
                                    foreach (Section section3 in this.Sections)
                                    {
                                        int distanceFromSection = architecture2.GetDistanceFromSection(section3);
                                        if (distanceFromSection < num3)
                                        {
                                            num3 = distanceFromSection;
                                            section2 = section3;
                                        }
                                    }
                                    section2.AddArchitecture(architecture2);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (Architecture architecture2 in section.Architectures)
                        {
                            architecturelist.Remove(architecture2);
                        }
                        this.BuildSectionByArchitectureList(architecturelist);
                    }
                }
            }
        }

        public void ChangeCapital(Architecture newCapital)
        {
            if (this.Capital != newCapital)
            {
                Architecture capital = this.Capital;
                this.Capital = newCapital;
                Session.Current.Scenario.YearTable.addChangeCapitalEntry(Session.Current.Scenario.Date, this, newCapital);
                if (this.OnInitiativeChangeCapital != null)
                {
                    this.OnInitiativeChangeCapital(this, capital, this.Capital);
                }
                ExtensionInterface.call("ChangeCapital", new Object[] { Session.Current.Scenario, this });
            }
        }

        public void ChangeFaction(Faction faction)
        {
            GameObjectList list = this.Architectures.GetList();
            foreach (Architecture architecture in list)
            {
                architecture.ChangeFaction(faction);
            }
            foreach (Architecture architecture in list)
            {
                architecture.CheckIsFrontLine();
            }
            foreach (Troop troop in this.Troops.GetList())
            {
                troop.ChangeFaction(faction);
            }
            foreach (Section section in this.Sections.GetList())
            {
                this.RemoveSection(section);
                Session.Current.Scenario.Sections.Remove(section);
            }
            
            this.Destroy();
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                architecture.RefreshViewArea();
            }
            foreach (Troop troop in Session.Current.Scenario.Troops)
            {
                troop.RefreshViewArchitectureRelatedArea();
            }
            ExtensionInterface.call("ChangeFaction", new Object[] { Session.Current.Scenario, this });
        }

        public void AfterChangeLeader(Faction newFaction, GameObjectList candidates, Person oldLeader, Person newLeader)
        {
            foreach (Architecture a in this.Architectures)
            {
                foreach (Person p in a.Feiziliebiao)
                {
                    a.PrincessChangeLeader(false, a.BelongedFaction, p);
                }
            }
            PersonList pl = new PersonList();
            pl.AddRange(candidates);

            Session.Current.Scenario.NewFaction(pl, true, oldLeader.Strain != newLeader.Strain && !newLeader.IsVeryCloseTo(oldLeader));
        }

        public Faction ChangeLeaderAfterLeaderDeath()
        {
            Person leader = this.Leader;
            Architecture locationArchitecture = this.Leader.LocationArchitecture;
            this.Leader.Status = GameObjects.PersonDetail.PersonStatus.None;
            // this.Leader.Available = false;
            // Session.Current.Scenario.Persons.Remove(this.Leader);
            Session.Current.Scenario.AvailablePersons.Remove(this.Leader);
            Person person2 = null;
            PersonList list = new PersonList();
            if (person2 == null)
            {
                if (this.Prince != null && this.Prince != this.Leader && this.Prince.BelongedFaction == this)
                {
                    person2 = this.Prince;
                }
            }

            if (person2 == null)
            {
                list.Clear();
                foreach (Person person3 in Session.Current.Scenario.Persons)
                {
                    if ((person3.Father != null) && (person3.Sex == this.Leader.Sex) && (this.Leader == person3.Father) && person3 != this.Leader
                         && (person3.BelongedFaction == this || !person3.Available) && person3.Alive && (person3.ID < 7000 || person3.ID >= 8000))
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "YearBorn";
                        list.IsNumber = true;
                        list.SmallToBig = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }

            if (person2 == null)
            {
                list.Clear();
                foreach (Person person3 in Session.Current.Scenario.Persons)
                {
                    if ((person3.Father != null) && (person3.Sex == this.Leader.Sex) && (this.Leader.Father == person3.Father)
                        && person3 != this.Leader && (person3.BelongedFaction == this || !person3.Available) && person3.Alive && (person3.ID < 7000 || person3.ID >= 8000))
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "YearBorn";
                        list.IsNumber = true;
                        list.SmallToBig = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 == null)
            {
                list.Clear();
                foreach (Person person3 in Session.Current.Scenario.Persons)
                {
                    if ((person3.Strain >= 0) && (person3.Sex == this.Leader.Sex) && (this.Leader.Strain == person3.Strain) && person3 != this.Leader
                         && (person3.BelongedFaction == this || !person3.Available) && person3.Alive && (person3.ID < 7000 || person3.ID >= 8000))
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "YearBorn";
                        list.IsNumber = true;
                        list.SmallToBig = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 == null)
            {
                list.Clear();
                foreach (Person person3 in this.Leader.Brothers)
                {
                    if (person3 != this.Leader && person3.BelongedFaction == this)
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "Glamour";
                        list.IsNumber = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 == null)
            {
                foreach (Person person3 in Session.Current.Scenario.Persons)
                {
                    if ((person3.Mother != null) && (person3.Sex == this.Leader.Sex) && ((this.Leader.Mother == person3.Mother) || (person3.Mother == this.Leader))
                        && person3 != this.Leader && person3.BelongedFaction == this && person3.Alive && (person3.ID < 7000 || person3.ID >= 8000))
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "YearBorn";
                        list.IsNumber = true;
                        list.SmallToBig = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 == null)
            {
                foreach (Person person3 in Session.Current.Scenario.Persons)
                {
                    if (person3.Father != null && (person3.Sex == this.Leader.Sex) && ((person3.Father.Father != null && person3.Father.Father == this.Leader) || (person3.Father.Mother != null && person3.Father.Mother == this.Leader))
                        && person3 != this.Leader && person3.BelongedFaction == this && person3.Alive && (person3.ID < 7000 || person3.ID >= 8000))
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "YearBorn";
                        list.IsNumber = true;
                        list.SmallToBig = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 == null)
            {
                foreach (Person person3 in Session.Current.Scenario.Persons)
                {
                    if (person3.Mother != null && (person3.Sex == this.Leader.Sex) && ((person3.Mother.Father != null && person3.Mother.Father == this.Leader) || (person3.Mother.Mother != null && person3.Mother.Mother == this.Leader))
                        && person3 != this.Leader && person3.BelongedFaction == this && person3.Alive && (person3.ID < 7000 || person3.ID >= 8000))
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "YearBorn";
                        list.IsNumber = true;
                        list.SmallToBig = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 == null && Session.GlobalVariables.PermitFactionMerge && !this.IsAlien)
            {
                float num = -75;
                Faction diplomaticFaction = null;
                foreach (DiplomaticRelation relation in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(base.ID))
                {
                    if (relation.RelationFaction1 == null || relation.RelationFaction2 == null) continue;
                    float attr = Person.GetIdealAttraction(relation.RelationFaction1.Leader, relation.RelationFaction2.Leader);
                    if ((relation.Relation >= Session.GlobalVariables.FriendlyDiplomacyThreshold) && 
                        (num < attr) &&
                        !relation.RelationFaction1.IsAlien && !relation.RelationFaction2.IsAlien)
                    {
                        num = attr;
                        diplomaticFaction = relation.GetDiplomaticFaction(base.ID);
                    }
                }

                if (diplomaticFaction != null)
                {
                    Session.Current.Scenario.YearTable.addChangeFactionEntry(Session.Current.Scenario.Date, this, diplomaticFaction);
                    GameObjectList rebelCandidates = this.Persons.GetList();
                    this.ChangeFaction(diplomaticFaction);
                    foreach (Treasure treasure in leader.Treasures.GetList())
                    {
                        treasure.HidePlace = locationArchitecture;
                        leader.LoseTreasure(treasure);
                        treasure.Available = false;
                    }
                    this.AfterChangeLeader(diplomaticFaction, rebelCandidates, leader, diplomaticFaction.Leader);
                    return diplomaticFaction;
                }
            }
            if (person2 == null)
            {
                list.Clear();
                foreach (Person person3 in this.Persons)
                {
                    if ((this.Leader.Ideal == person3.Ideal) && (person3.Sex == this.Leader.Sex) && person3 != this.Leader)
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "Merit";
                        list.IsNumber = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 == null)
            {
                list.Clear();
                foreach (Person person3 in this.Persons)
                {
                    if (person3.Sex == this.Leader.Sex && person3 != this.Leader)
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "Merit";
                        list.IsNumber = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 == null)
            {
                list.Clear();
                foreach (Person person3 in this.Persons)
                {
                    if (person3 != this.Leader)
                    {
                        list.Add(person3);
                    }
                }
                if (list.Count > 0)
                {
                    if (list.Count > 1)
                    {
                        list.PropertyName = "Merit";
                        list.IsNumber = true;
                        list.ReSort();
                    }
                    person2 = list[0] as Person;
                }
            }
            if (person2 != null)
            {
                if (!person2.Available)
                {
                    person2.Available = true;
                    Session.Current.Scenario.AvailablePersons.Add(person2);
                    person2.LocationArchitecture = this.Capital;
                    person2.Status = PersonStatus.Normal;
                    person2.YearJoin = Session.Current.Scenario.Date.Year;
                    Session.MainGame.mainGameScreen.xianshishijiantupian(person2, this.Capital.Name, TextMessageKind.PersonJoin, "PersonJoin", "", "", this.Name, false);
                    Session.Current.Scenario.YearTable.addGrownBecomeAvailableEntry(Session.Current.Scenario.Date, person2);
                }
                this.Leader = person2;
                if (!((this.Leader.LocationTroop == null) || this.Leader.IsCaptive))
                {
                    this.Leader.LocationTroop.RefreshWithPersonList(this.Leader.LocationTroop.Persons.GetList());
                }
                foreach (Treasure treasure in leader.Treasures.GetList())
                {
                    leader.LoseTreasure(treasure);
                    this.Leader.ReceiveTreasure(treasure);
                }
                ExtensionInterface.call("ChangeKing", new Object[] { Session.Current.Scenario, this });
                Session.Current.Scenario.YearTable.addChangeKingEntry(Session.Current.Scenario.Date, this.Leader, this, leader);
                this.AfterChangeLeader(this, this.Persons, leader, this.Leader);
                return this;
            }
            foreach (Treasure treasure in leader.Treasures.GetList())
            {
                treasure.HidePlace = locationArchitecture;
                leader.LoseTreasure(treasure);
                treasure.Available = false;
            }
            
            return null;
        }

        public void CheckLeaderDeath(Person leader)
        {
            if ((((((leader.LocationArchitecture != null) && (leader.LocationArchitecture.BelongedFaction == this.Leader.BelongedFaction)) 
                || ((leader.LocationTroop != null) && (leader.LocationTroop.BelongedFaction == this.Leader.BelongedFaction))) 
                && (GameObject.Random(leader.CaptiveAbility) < GameObject.Random(this.Leader.CaptiveAbility))) 
                && Session.Current.Scenario.IsPlayer(this)) && (this.OnAfterCatchLeader != null))
            {
                this.OnAfterCatchLeader(leader, this);
            }
        }

        public void ClearRouteways()
        {
            while (this.RoutewayCount > 0)
            {
                Session.Current.Scenario.RemoveRouteway(this.Routeways[0] as Routeway);
            }
        }

        private void ClearSections()
        {
            foreach (Section section in this.Sections.GetList())
            {
                this.RemoveSection(section);
                Session.Current.Scenario.Sections.Remove(section);
            }
            foreach (Architecture architecture in this.Architectures)
            {
                architecture.BelongedSection = null;
            }
        }

        public Section CreateFirstSection()
        {
            if ((this.Capital != null) && (this.ArchitectureCount > 0))
            {
                Section section = new Section();
                section.ID = Session.Current.Scenario.Sections.GetFreeGameObjectID();
                section.Name = this.Capital.Name + "军区";
                section.AIDetail = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.无, true, false, true, true, false)[0] as SectionAIDetail;
                foreach (Architecture architecture in this.Architectures)
                {
                    section.AddArchitecture(architecture);
                }
                this.AddSection(section);
                Session.Current.Scenario.Sections.AddSectionWithEvent(section);
                return section;
            }
            return null;
        }

        public void DayEvent()
        {
           // this.SpyMessageCloseList.Clear();
            this.TechniquesDayEvent();
            this.InformationDayEvent();
            this.MilitaryDayEvent();
            if (!Session.Current.Scenario.IsPlayer(this))
            {
               // this.AISelectPrince();
                this.AIchaotingshijian();
                this.AIBecomeEmperor();
            }
            this.armyScale = this.ArmyScale; // 小写的是每天的缓存，因为被InternalSurplusRate叫很多次，不想每次都全部重新计算，大写的才是真正的值
            this.InternalSurplusRateCache = -1;
            this.visibleTroopsCache = null;
            this.RefreshImportantPerson();
            this.troopSequence = -1;
        }

        public void RefreshImportantPerson()
        {
            //阿柒:得到本势力除君主外的所有人物的名单
            List<Person> PersonInCurrentFaction = new List<Person>();
            foreach (Person person in this.Persons)
            {
                if (person != this.Leader)
                {
                    PersonInCurrentFaction.Add(person);
                }
            }

            //阿柒:排序得到智力最高的命名为军师
            if (PersonInCurrentFaction.Count >= 1)
            {
                List<Person> t = PersonInCurrentFaction.OrderByDescending(Person => Person.IntelligenceIncludingExperience).ToList();
                if (t[0].IntelligenceIncludingExperience >= 70)
                {
                    Counsellor = string.Concat(new object[] { t[0].Name, "(", t[0].IntelligenceIncludingExperience.ToString(), ")" });
                }
                else
                {
                    Counsellor = string.Concat(new object[] { "----" });
                }
            }
            else
            {
                Counsellor = string.Concat(new object[] { "----" });
            }

            //阿柒:排序得到统帅最高的命名为都督
            if (PersonInCurrentFaction.Count >= 1)
            {
                List<Person> t = PersonInCurrentFaction.OrderByDescending(Person => Person.CommandIncludingExperience).ToList();
                if (t[0].CommandIncludingExperience >= 70)
                {
                    Governor = string.Concat(new object[] { t[0].Name, "(", t[0].CommandIncludingExperience.ToString(), ")" });
                }
                else
                {
                    Governor = string.Concat(new object[] { "----" });
                }
            }
            else
            {
                Governor = string.Concat(new object[] { "----" });
            }

            //阿柒:排序得到武勇最高的命名为五虎将
            string[] FivetigerString = new string[5] { "----", "----", "----", "----", "----" };

            if (PersonInCurrentFaction.Count >= 1)
            {
                List<Person> Fivetiger = PersonInCurrentFaction.OrderByDescending(Person => Person.StrengthIncludingExperience).ToList();
                for (int i = 0; i < PersonInCurrentFaction.Count; i++)
                {
                    if (Fivetiger[i].StrengthIncludingExperience >= 70)
                    {
                        FivetigerString[i] = Fivetiger[i].Name + "(" + Fivetiger[i].StrengthIncludingExperience.ToString() + ")";
                    }
                    if (i == 4) break;
                }
            }
            FiveTigers = string.Concat(new object[] { FivetigerString[0], " • ", FivetigerString[1], " • ", FivetigerString[2], " • ", FivetigerString[3], " • ", FivetigerString[4] });
        }

        [DataMember]
        public string TransferingMilitariesString { get; set; }

        public MilitaryList TransferingMilitaries { get; set; }

        public List<string> LoadTransferingMilitariesFromString(MilitaryList militaries, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.TransferingMilitaries.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Military gameObject = militaries.GetGameObject(int.Parse(str)) as Military;
                    if (gameObject != null)
                    {
                        this.TransferingMilitaries.AddMilitary(gameObject);
                        this.AddMilitary(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("编队ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("编队列表一栏应为半型空格分隔的编队ID");
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
                        this.Militaries.AddMilitary(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("编队ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("编队列表一栏应为半型空格分隔的编队ID");
            }
            return errorMsg;
        }

        private void HandleMilitary(Military military)
        {
            if (military.ArrivingDays != 0)
            {
                military.ArrivingDays = 0;
            }
            military.StartingArchitecture = null;
            military.TargetArchitecture = null;
            this.TransferingMilitaries.Remove(military);
            this.TransferingMilitaryCount--;
        }

        private void MilitaryDayEvent()
        {
            //if (this.TransferingMilitaryCount == 0) return;
            foreach (Military m in this.TransferingMilitaries.GetList())
            {
                m.ArrivingDays--;

                if (m.ArrivingDays <= 0)
                {
                    if (m.StartingArchitecture != null && m.TargetArchitecture != null  && m.TargetArchitecture.BelongedFaction != null 
                        && m.TargetArchitecture.BelongedFaction == this && m.BelongedArchitecture == null )                                            
                    {                       
                        m.TargetArchitecture.AddMilitary(m);
                        Session.MainGame.mainGameScreen.TransferMilitaryArrivesAtArchitecture(m, m.TargetArchitecture);                       
                    }
                   
                    this.HandleMilitary(m);
                }
                else 
                {
                    if (m.StartingArchitecture != null && m.TargetArchitecture != null && m.TargetArchitecture.BelongedFaction != null && m.TargetArchitecture.BelongedFaction != this) //运兵过程中目标建筑被占领，停止运输，编队返回出发建筑
                    {
                        
                            if (m.StartingArchitecture.BelongedFaction != null && m.BelongedArchitecture == null)
                            {
                                
                                m.StartingArchitecture.AddMilitary(m);
                            }
                            
                            this.HandleMilitary(m);
                        
                    }
                    else if (m.StartingArchitecture != null && m.TargetArchitecture != null && m.StartingArchitecture.BelongedFaction != null
                        && m.TargetArchitecture.BelongedFaction != null && m.TargetArchitecture.IsSurrounded())   //运兵过程中目标建筑被围城，停止运兵,编队返回出发建筑
                    {
                        if (m.BelongedArchitecture == null)
                        {
                            m.StartingArchitecture.AddMilitary(m);
                        }
                       
                        this.HandleMilitary(m);
                    }


                    else if (m.StartingArchitecture != null && m.StartingArchitecture.BelongedFaction == null) //势力灭亡，停止运兵，编队暂时消失
                    {
                        this.HandleMilitary(m);

                    }
                }

            }
           
        }

        private void AISelectPrince()
        {
            if (!Session.Current.Scenario.IsPlayer(this))
            {
                if (GameObject.Random(10) == 0 && (this.Capital != null) && this.Capital.BelongedFaction == this && this.Capital.SelectPrinceAvail())
                {
                    Person person = this.Leader.ChildrenCanBeSelectedAsPrince()[0] as Person;
                    if (person.ID != this.PrinceID)
                    {
                        this.PrinceID = person.ID;
                        this.Capital.DecreaseFund(Session.Parameters.SelectPrinceCost);
                        this.Capital.SelectPrince(person); //AI立储年表和报告
                        //Session.MainGame.mainGameScreen.xianshishijiantupian(this.Leader, person.Name, "SelectPrince", "", "", true);
                    }

                }
            }
        }

        private void AIAppointMayor()
        {
            foreach (Architecture a in this.Architectures)
            {
                if (!Session.Current.Scenario.IsPlayer(this) || a.BelongedSection.AIDetail.AutoRun)
                {
                    if (a.AppointMayorAvail())
                    {
                        Person person = a.AIMayorCandicate[0] as Person;
                        a.MayorID = person.ID;
                        a.AppointMayor(person);
                        a.MayorOnDutyDays = 0;
                    }
                }
            }
        }

        [DataMember]
        public string GetGeneratorPersonCountString { get; set; }

        private Dictionary<PersonGeneratorType, int> count = new Dictionary<PersonGeneratorType, int>();

        public void IncrementGeneratorCount(PersonGeneratorType type)
        {
            if (count.ContainsKey(type))
            {
                count[type]++;
            }
            else
            {
                count.Add(type, 1);
            }
        }

        public int GetGeneratorPersonCount(PersonGeneratorType type)
        {
            return count.ContainsKey(type) ? count[type] : 0;
        }

         

       // private List<PersonGeneratorType> allTypes = new List<PersonGeneratorType>();
      //  private Dictionary<PersonGeneratorType, int> types = new Dictionary<PersonGeneratorType, int>();

        public string SaveGeneratorPersonCountToString()
        {            
            StringBuilder sb = new StringBuilder();
            foreach (PersonGeneratorType type in Session.Current.Scenario.GameCommonData.AllPersonGeneratorTypes)
            {
                sb.AppendFormat("{0}:{1},", type.ID, count.ContainsKey(type) ? count[type] : 0);
            }
            return sb.Length > 0 ? sb.ToString(0, sb.Length - 1) : "";
        }

        public List<string> LoadGeneratorPersonCountFromString(String s)
        {
            List<string> errorMsg = new List<string>();

            if (String.IsNullOrEmpty(s))
            {
                return errorMsg;
            }

            count.Clear();
            string[] sArray = s.Split(',');
            foreach (string ss in sArray)
            {
                string[] arr = ss.Split(':');
                int typeID = int.Parse(arr[0]);
                int typeCount = int.Parse(arr[1]);
                PersonGeneratorType type = FindPersonGeneratorType(typeID);
                if (type == null || typeCount < 0)
                {
                    errorMsg.Add("Invaild Person Generator Count, typeID=" + typeID + " typeCount=" + typeCount);
                }
                else if (!count.ContainsKey(type))
                {
                    count.Add(type, typeCount);
                }
            }
            return errorMsg;                
        }

        public PersonGeneratorType FindPersonGeneratorType(int id)
        {
            foreach (PersonGeneratorType type in Session.Current.Scenario.GameCommonData.AllPersonGeneratorTypes)
            {
                if (type.ID == id)
                {
                    return type;
                }
            }
            return null;
        }


        private void AIZhaoXian()
        {
            if (Session.GlobalVariables.ZhaoXianSuccessRate <= 0) return;

            if (Session.Current.Scenario.IsPlayer(this)) return;

            int feiziCount = this.feiziCount();
            foreach (Architecture a in this.Architectures)
            {
                while (a.CanZhaoXian() && !a.HasEnoughPeople && (PersonCount <= 1 || a.IsFundEnough))
                {
                    PersonGeneratorTypeList list = a.AvailGeneratorTypeList();
                    Dictionary<PersonGeneratorType, float> weights = new Dictionary<PersonGeneratorType, float>();

                    int eFund;
                    if (PersonCount <= 1)
                    {
                        eFund = 0;
                    }
                    else
                    {
                        eFund = Math.Min(a.EnoughFund * PersonCount / 2, a.AbundantFund);
                    }
                    foreach (PersonGeneratorType t in list)
                    {
                        if (t.CostFund + eFund < a.Fund)
                        {
                            weights[t] = t.CostFund * t.generationChance;
                            if (Session.Current.Scenario.GlobalVariables.hougongGetChildrenRate > 0)
                            {
                                if (this.IsAlien && feiziCount <= 0 && t.genderFix == 1)
                                {
                                    weights[t] *= 10;
                                }
                            }
                        }
                    }
                    
                    if (weights.Count > 0)
                    {
                        PersonGeneratorType type = GameObject.WeightedRandom(weights);
                        a.DoZhaoXian(type);
                    }
                    else
                    {
                        break;
                    }

                }
            }     
            
        }


        private void AIchaotingshijian()
        {
            if (Session.Current.Scenario.youhuangdi() && !Session.Current.Scenario.IsPlayer(this) && !this.IsAlien
                && (this.guanjue < Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Count - 1))
            {
                if (Session.Current.Scenario.Date.Month == 3)
                {
                    this.AIjingong();
                }
            }
        }

        public int FundToAdvance
        {
            get
            {
                int cashToGive = 0;
                foreach (guanjuezhongleilei g in Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhongleiliebiao())
                {
                    if (g.xuyaochengchi <= this.ArchitectureCount)
                    {
                        cashToGive = g.xuyaogongxiandu - this.chaotinggongxiandu;
                    }
                }
                return cashToGive;
            }
        }

        private void AIjingong()
        {
            if (this.IsAlien) return;

            int cashToGive = this.FundToAdvance;

            if (cashToGive > 0)
            {
                int givenValue = 0;
                Dictionary<Architecture, int> archGiveFund = new Dictionary<Architecture, int>();
                foreach (Architecture a in this.Architectures.GetRandomList())
                {
                    int canGiveFund = a.Fund - a.EnoughFund;
                    if (canGiveFund >= 1000)
                    {
                        if (canGiveFund + givenValue >= cashToGive)
                        {
                            canGiveFund = cashToGive - givenValue;
                        }
                        givenValue += canGiveFund;
                        archGiveFund[a] = canGiveFund;
                    }
                    if (givenValue >= cashToGive)
                    {
                        foreach (KeyValuePair<Architecture, int> i in archGiveFund)
                        {
                            i.Key.DecreaseFund(i.Value);
                            if (!this.Architectures.HasGameObject(Session.Current.Scenario.huangdisuozaijianzhu()))
                            {
                                Session.Current.Scenario.huangdisuozaijianzhu().IncreaseFund(i.Value);
                            }
                            this.chaotinggongxiandu += i.Value;
                            Session.MainGame.mainGameScreen.shilijingong(this, i.Value, "资金");
                        }
                        break;
                    }
                }
            }

            /*int jingongshue;
            int gongxianduzangzhang=0;
            bool jingongliangcao;
            float jingongxishu=0f;
            switch (this.Leader.ValuationOnGovernment)
            {
                case PersonValuationOnGovernment.无视  :
                    jingongxishu=0.1f;
                    break;
                case PersonValuationOnGovernment.普通 :
                    jingongxishu=0.2f;
                    break;
                case PersonValuationOnGovernment.重视 :
                    jingongxishu=0.3f;
                    break;
                    
            }
            jingongliangcao = (GameObject.Random(8) == 0);
            if (jingongliangcao)
            {
                if (this.Architectures.HasGameObject(Session.Current.Scenario.huangdisuozaijianzhu())) jingongxishu = 0.3f;

                jingongshue = (int)(this.Capital.Food  * jingongxishu);
                this.Capital.DecreaseFood(jingongshue);
                if (!this.Architectures.HasGameObject(Session.Current.Scenario.huangdisuozaijianzhu()))
                {
                    Session.Current.Scenario.huangdisuozaijianzhu().IncreaseFood(jingongshue);
                }
                gongxianduzangzhang = jingongshue / 200;
                this.chaotinggongxiandu += gongxianduzangzhang;

            }
            else
            {
                if (this.Architectures.HasGameObject(Session.Current.Scenario.huangdisuozaijianzhu())) jingongxishu = 0.5f;

                jingongshue = (int)(this.Capital.Fund * jingongxishu);
                this.Capital.DecreaseFund(jingongshue);
                if (!this.Architectures.HasGameObject(Session.Current.Scenario.huangdisuozaijianzhu()))
                {
                    Session.Current.Scenario.huangdisuozaijianzhu().IncreaseFund(jingongshue);

                }
                gongxianduzangzhang = jingongshue;
                this.chaotinggongxiandu += gongxianduzangzhang;

            }*/
            //Session.MainGame.mainGameScreen.shilijingong(this,jingongshue,jingongliangcao?"粮草":"资金");

        }

        public void DecreaseReputation(int decrement)
        {
            this.reputation -= decrement;
            if (this.reputation < 0)
            {
                this.reputation = 0;
            }
        }

        public void DecreaseTechniquePoint(int decrement)
        {
            this.techniquePoint -= decrement;
            if (this.techniquePoint < 0)
            {
                this.techniquePoint = 0;
            }
        }

        public void DepositTechniquePointForFacility(int deposit)
        {
            if (deposit > this.techniquePointForFacility)
            {
                deposit = this.techniquePointForFacility;
            }
            this.techniquePointForFacility -= deposit;
            this.techniquePoint += deposit;
        }

        public void DepositTechniquePointForTechnique(int deposit)
        {
            if (deposit > this.techniquePointForTechnique)
            {
                deposit = this.techniquePointForTechnique;
            }
            this.techniquePointForTechnique -= deposit;
            this.techniquePoint += deposit;
        }

        public void Destroy()
        {
            Session.Current.Scenario.YearTable.addFactionDestroyedEntry(Session.Current.Scenario.Date, this);
            this.Leader.Reputation /= 2;
            if (this.OnFactionDestroy != null)
            {
                this.OnFactionDestroy(this);
            }
            foreach (Captive captive in this.SelfCaptives.GetList())
            {
                //captive.TransformToNoFaction();
                captive.TransformToNoFactionCaptive();
            }
            /*
            foreach (Troop troop in this.Troops.GetList())
            {
                troop.Destroy();
            }
            */

            foreach (Section section in this.Sections.GetList())
            {
                this.RemoveSection(section);
                Session.Current.Scenario.Sections.Remove(section);
            }
            Session.Current.Scenario.DiplomaticRelations.RemoveDiplomaticRelationByFactionID(base.ID);
            Session.Current.Scenario.Factions.Remove(this);
            Session.Current.Scenario.PlayerFactions.Remove(this);
            this.Destroyed = true;
            ExtensionInterface.call("FactionDestroyed", new Object[] { Session.Current.Scenario, this });
        }

        private void Develop()
        {
            this.DevelopArchitectures();
        }

        private void DevelopArchitectures()
        {
            foreach (Architecture architecture in this.Architectures)
            {
                architecture.DevelopDay();
            }
        }

        private void FactionDiplomaticRelation()
        {
            this.ResetFriendlyDiplomaticRelations();
        }

        public void EndControl()
        {
            this.ClearRouteways();
            foreach (Troop t in this.Troops)
            {
                t.ManualControl = false;
            }
        }

        public Section GetAnotherSection(Section section)
        {
            foreach (Section section2 in this.Sections)
            {
                if (section2 != section)
                {
                    return section2;
                }
            }
            return null;
        }

        public InformationLevel GetArchitectureKnownLevel(Architecture a)
        {
            InformationLevel level = InformationLevel.无;
            foreach (Point point in a.ArchitectureArea.Area)
            {
                if (this.getInformationLevel(point) > level)
                {
                    level = this.getInformationLevel(point);
                }
            }
            return level;
        }

        public GameArea GetAvailableTroopDestination(Troop troop)
        {
            GameArea area = new GameArea();
            for (int i = 0; i < Session.Current.Scenario.MapTileData.GetLength(0); i++)
            {
                for (int j = 0; j < Session.Current.Scenario.MapTileData.GetLength(1); j++)
                {
                    Point point = new Point(i, j);
                    area.AddPoint(point);
                }
            }
            if (area.Count > 0)
            {
                return area;
            }
            return null;
        }

        private int GetAverageValueOfSecondTier(int x, int y)
        {
            return 0;
        }

        private int GetAverageValueOfThirdTier(int x, int y)
        {
            return 0;
        }

        public List<Point> GetCurrentRoutewayPath()
        {
            List<Point> path = new List<Point>();
            this.RoutewayPathBuilder.SetPath(path);
            return path;
        }

        public FactionList GetHostileFactions()
        {
            FactionList list = new FactionList();
            foreach (Faction faction in Session.Current.Scenario.Factions)
            {
                if (!((faction == this) || this.IsFriendly(faction)))
                {
                    list.Add(faction);
                }
            }
            return list;
        }

        public InformationLevel GetKnownAreaData(Point position)
        {
            if (Session.Current.Scenario.PositionOutOfRange(position))
            {
                return InformationLevel.未知;
            }
            return this.getInformationLevel(position);
        }

        public InformationLevel GetKnownAreaDataNoCheck(Point position)
        {
            return this.getInformationLevel(position);
        }

        public Legion GetLegion(Architecture will)
        {
            foreach (Legion legion in this.Legions)
            {
                if (legion.WillArchitecture == will)
                {
                    return legion;
                }
            }
            return null;
        }

        public int GetMapCost(Troop troop, Point position, MilitaryKind kind)
        {
            if (Session.Current.Scenario.PositionOutOfRange(position))
            {
                return 0xdac;
            }
            if (Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(position).RoutewayConsumptionRate >= 1)
            {
                return 0xdac;
            }
            int terrainAdaptability = 0;

            Architecture onArch = Session.Current.Scenario.GetArchitectureByPositionNoCheck(position);
            if (onArch == null)
            {
                terrainAdaptability = troop.GetTerrainAdaptability((TerrainKind)this.mapData[position.X, position.Y]);
            }
            int waterPunishment = 0;
            if (this.mapData[position.X, position.Y] == 6 && kind.Type != MilitaryType.水军 && onArch == null)
            {
                waterPunishment = 3;
            }
            return ((terrainAdaptability + Session.Current.Scenario.GetWaterPositionMapCost(kind, position)) + Session.Current.Scenario.GetPositionMapCost(this, position) + waterPunishment);
        }

        public FactionList GetOtherFactions()
        {
            FactionList list = new FactionList();
            foreach (Faction faction in Session.Current.Scenario.Factions)
            {
                if (faction != this)
                {
                    list.Add(faction);
                }
            }
            return list;
        }

        public SectionList GetOtherSections(Section section)
        {
            SectionList list = new SectionList();
            foreach (Section section2 in this.Sections)
            {
                if (section2 != section)
                {
                    list.Add(section2);
                }
            }
            return list;
        }

        private float GetPreferredTechniqueComplition()
        {
            int num = 0;
            int num2 = 0;
            foreach (Technique technique in Session.Current.Scenario.GameCommonData.AllTechniques.Techniques.Values)
            {
                if (this.PreferredTechniqueKinds.IndexOf(technique.Kind) >= 0)
                {
                    num2++;
                    if (this.HasTechnique(technique.ID))
                    {
                        num++;
                    }
                }
            }
            if (num2 > 0)
            {
                return (((float)num) / ((float)num2));
            }
            return 0f;
        }

        private Technique GetRandomTechnique()
        {
            Dictionary<Technique, float> list = new Dictionary<Technique, float>();
            foreach (Technique technique in Session.Current.Scenario.GameCommonData.AllTechniques.Techniques.Values)
            {
                if (this.IsTechniqueUpgradable(technique) && this.GetTechniqueUsefulness(technique) > 0)
                {
                    float weight = 1;
                    foreach (KeyValuePair<Condition, float> c in technique.AIConditionWeight)
                    {
                        if (c.Key.CheckCondition(this))
                        {
                            weight *= c.Value;
                        }
                    }

                    list.Add(technique, weight);
                }
            }
            if (list.Count > 0)
            {
                return GameObject.WeightedRandom(list);
            }
            return null;
        }

        public List<Point> GetSecondTierKnownPath(Point start, Point end)
        {
            ClosedPathEndpoints key = new ClosedPathEndpoints(start, end);
            if (this.SecondTierKnownPaths.ContainsKey(key))
            {
                return this.SecondTierKnownPaths[key];
            }
            return null;
        }

        public List<Point> GetThirdTierKnownPath(Point start, Point end)
        {
            ClosedPathEndpoints key = new ClosedPathEndpoints(start, end);
            if (this.ThirdTierKnownPaths.ContainsKey(key))
            {
                return this.ThirdTierKnownPaths[key];
            }
            return null;
        }

        private int GetThreat(Faction faction)
        {
            if (faction == null)
            {
                return 0;
            }
            return ((faction.ArchitectureTotalSize * 10) - Session.Current.Scenario.GetDiplomaticRelation(base.ID, faction.ID));
        }

        public GameObjectList GetUnderZeroDiplomaticRelationFactions()
        {
            GameObjectList list = new GameObjectList();
            foreach (DiplomaticRelation relation in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(base.ID))
            {
                if (relation.Relation < 0)
                {
                    if (relation.RelationFaction1 == this)
                    {
                        list.Add(relation.RelationFaction2);
                    }
                    else
                    {
                        list.Add(relation.RelationFaction1);
                    }
                }
            }
            return list;
        }

        public Faction GetFactionByName(string FactionName)
        {
            foreach (Faction i in Session.Current.Scenario.Factions)
            {
                if (i.Name == FactionName) return i;
            }
            return null;
        }

        public void HandleForcedChangeCapital()
        {
            this.Reputation /= 2;
            if (this.Architectures.Count != 1)
            {
                Architecture capital = this.Capital;
                if (this.Architectures.Count == 2)
                {
                    foreach (Architecture architecture2 in this.Architectures)
                    {
                        if (architecture2 != this.Capital)
                        {
                            this.Capital = architecture2;
                            if (this.OnForcedChangeCapital != null)
                            {
                                this.OnForcedChangeCapital(this, capital, this.Capital);
                            }
                            Session.Current.Scenario.YearTable.addChangeCapitalEntry(Session.Current.Scenario.Date, this, this.Capital);
                            break;
                        }
                    }
                }
                else
                {
                    this.Capital = this.SelectNewCapital();
                    if (this.OnForcedChangeCapital != null)
                    {
                        this.OnForcedChangeCapital(this, capital, this.Capital);
                    }
                    Session.Current.Scenario.YearTable.addChangeCapitalEntry(Session.Current.Scenario.Date, this, this.Capital);
                }
            }
            ExtensionInterface.call("ForceChangeCapital", new Object[] { Session.Current.Scenario, this });
        }

        public bool HasArchitecture(Architecture architecture)
        {
            return this.Architectures.HasGameObject(architecture);
        }

        public bool HasCaptive()
        {
            return (this.CaptiveCount > 0);
        }

        /*
        public int EachMilitaryKindCount(int id)
        {
            int count = 0;
            foreach (Military military in this.Militaries)
            {
                if (military.RealKindID == id)
                {
                    count++;
                }
            }
            MilitaryKind mk = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(id);
            return count;
        }
        */
         
        public bool IsMilitaryKindOverLimit(int id)
        {
            int count = 0;
            foreach (Military military in this.Militaries)
            {
                if (military.RealKindID == id)
                {
                    count++;
                }
            }
            

            MilitaryKind mk = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(id);
            return count >= mk.RecruitLimit;
            
            
        }

        public bool HasPerson(Person person)
        {
            return this.Persons.HasGameObject(person);
        }

        public bool HasSelfCaptive()
        {
            return (this.SelfCaptiveCount > 0);
        }

        public bool HasTechnique(int id)
        {
            return (this.AvailableTechniques.GetTechnique(id) != null);
        }

        public void IncreaseReputation(int increment)
        {
            this.reputation += increment;
            if (this.reputation > this.shengwangshangxian) this.reputation = this.shengwangshangxian;

        }

        public void IncreaseTechniquePoint(int increment)
        {
            this.techniquePoint = (int)(this.techniquePoint + increment * Session.GlobalVariables.TechniquePointMultiple);
        }

        public List<Architecture> GettingInformationArchitectures()
        {
            List<Architecture> result = new List<Architecture>();
            foreach (Person p in this.Persons)
            {
                if (p.OutsideTask == OutsideTaskKind.情报)
                {
                    Architecture a = Session.Current.Scenario.GetArchitectureByPositionNoCheck(p.OutsideDestination.Value);
                    result.Add(a);
                }
            }
            return result;
        }

        private void InformationDayEvent()
        {
            InformationList list = new InformationList();
            foreach (Information information in this.Informations)
            {
                information.DaysLeft -= Session.Parameters.DayInTurn;
                information.DaysStarted += Session.Parameters.DayInTurn;
                if (information.DaysLeft <= 0)
                {
                    list.Add(information);
                }
                else
                {
                    information.CheckAmbushTroop();
                }
            }
            foreach (Architecture a in this.Architectures)
            {
                foreach (Information info in a.Informations)
                {
                    info.DaysStarted += Session.Parameters.DayInTurn;
                }
            }
            foreach (Information information in list)
            {
                information.Purify();
                this.RemoveInformation(information);
                Session.Current.Scenario.Informations.Remove(information);
            }
        }

        public bool IsArchitectureKnown(Architecture a)
        {
            foreach (Point point in a.ArchitectureArea.Area)
            {
                if (this.getInformationLevel(point) != InformationLevel.无)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsTroopKnown(Troop t)
        {
            return this.getInformationLevel(t.Position) != InformationLevel.无;
        }

        public bool IsFriendly(Faction faction)
        {
            if (faction == null)
            {
                return false;
            }
            return ((faction == this) || (Session.Current.Scenario.GetDiplomaticRelation(base.ID, faction.ID) >= Session.GlobalVariables.FriendlyDiplomacyThreshold) || (Session.Current.Scenario.GetDiplomaticRelationTruce(base.ID, faction.ID) > 0));
        }

        public bool IsFriendlyWithoutTruce(Faction faction)
        {
            if (faction == null)
            {
                return false;
            }
            return (faction == this) || (Session.Current.Scenario.GetDiplomaticRelation(base.ID, faction.ID) >= Session.GlobalVariables.FriendlyDiplomacyThreshold);
        }

        public bool IsHostile(Faction faction)
        {
            if (faction == null)
            {
                return false;
            }
            if (faction == this)
            {
                return false;
            }
            return (Session.Current.Scenario.GetDiplomaticRelation(base.ID, faction.ID) < 0);
        }

        public bool IsPositionKnown(Point position)
        {
            if (Session.Current.Scenario.PositionOutOfRange(position))
            {
                return false;
            }
            return (this.getInformationLevel(position) != InformationLevel.无);
        }

        private bool IsTechniqueUpgradable(Technique t)
        {
            return (!this.HasTechnique(t.ID) && ((t.PreID < 0) || this.HasTechnique(t.PreID))) && t.CanResearch(this);
        }

        public bool IsTechniqueUpgrading(int id)
        {
            return (id == this.UpgradingTechnique);
        }

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
                        errorMsg.Add("情报ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("情报列表应为半型空格分隔的情报ID");
            }
            return errorMsg;
        }

        public List<string> LoadLegionsFromString(LegionList legions, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Legions.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Legion gameObject = legions.GetGameObject(int.Parse(str)) as Legion;
                    if (gameObject != null)
                    {
                        this.AddLegion(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("LegionID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("Legion列表应为半型空格分隔的LegionID");
            }
            return errorMsg;
        }

        public List<string> LoadRoutewaysFromString(RoutewayList routeways, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Routeways.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Routeway gameObject = routeways.GetGameObject(int.Parse(str)) as Routeway;
                    if (gameObject != null)
                    {
                        this.AddRouteway(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("粮道ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("粮道列表应为半型空格分隔的粮道ID");
            }
            return errorMsg;
        }

        public List<string> LoadArchitecturesFromString(ArchitectureList architectures, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Architectures.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Architecture architecture = architectures.GetGameObject(int.Parse(str)) as Architecture;
                    if (architecture != null)
                    {
                        this.AddArchitecture(architecture);
                        this.AddArchitectureMilitaries(architecture);
                        
                    }
                    else
                    {
                        errorMsg.Add("建筑ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("建筑列表应为半型空格分隔的建筑ID");
            }
            return errorMsg;
        }

        public List<string> LoadSectionsFromString(SectionList sections, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Sections.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Section section = sections.GetGameObject(int.Parse(str)) as Section;
                    if (section != null)
                    {
                        this.AddSection(section);
                    }
                    else
                    {
                        errorMsg.Add("军区ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("军区列表应为半型空格分隔的军区ID");
            }
            if (this.SectionCount == 0)
            {
                // errorMsg.Add("没有军区");
            }
            return errorMsg;
        }

        public List<string> LoadTroopsFromString(TroopList troops, string dataString)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Troops.Clear();
            try
            {
                foreach (string str in strArray)
                {
                    Troop gameObject = troops.GetGameObject(int.Parse(str)) as Troop;
                    if (gameObject != null)
                    {
                        this.AddTroop(gameObject);
                        this.AddTroopMilitary(gameObject);
                    }
                    else
                    {
                        errorMsg.Add("部队ID" + str + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("部队列表应为半型空格分隔的部队ID");
            }
            return errorMsg;
        }

        public int getTechniqueActualPointCost(Technique technique)
        {
            if (this.techniquePointCostRateDecrease.Count == 0) return technique.PointCost;
            return (int)Math.Round(technique.PointCost * (1 - this.techniquePointCostRateDecrease.Max()));
        }

        public int getTechniqueActualReputation(Technique technique)
        {
            if (this.techniqueReputationRateDecrease.Count == 0) return technique.Reputation;
            return (int)Math.Round(technique.Reputation * (1 - this.techniqueReputationRateDecrease.Max()));
        }

        public int getTechniqueActualFundCost(Technique technique)
        {
            if (this.techniqueFundCostRateDecrease.Count == 0) return technique.FundCost;
            return (int)Math.Round(technique.FundCost * (1 - this.techniqueFundCostRateDecrease.Max()));
        }

        public int getTechniqueActualTime(Technique technique)
        {
            if (this.techniqueTimeRateDecrease.Count == 0) return technique.Days;
            return (int)Math.Round(technique.Days * (1 - techniqueTimeRateDecrease.Max()));
        }

        public bool MatchTechnique(Technique technique, Architecture architecture)
        {
            return (((((this.TotalTechniquePoint >= this.getTechniqueActualPointCost(technique)) &&
                (this.Reputation >= this.getTechniqueActualReputation(technique))) &&
                (architecture.Fund >= this.getTechniqueActualFundCost(technique))) &&
                (this.HasTechnique(technique.PreID) || (technique.PreID < 0))) && (this.UpgradingTechnique < 0)) && technique.CanResearch(this);
        }

        public void MonthEvent()
        {
            this.FactionDiplomaticRelation();
            powerCache = null;
            
        }

        private void PlayerAI()
        {
            Session.Current.Scenario.Threading = true;
            this.AIFinished = false;
            this.AIPrepare();
            this.PlayerAITransfer();
            this.PlayerTechniqueAI();
            this.PlayerAIArchitectures();
            this.PlayerAILegions();
            this.PlayerAIAppointMayor();
            this.AITrainChildren();
            this.AIFinished = true;
            Session.Current.Scenario.Threading = false;
        }

        private void PlayerAIArchitectures()
        {
            foreach (Architecture architecture in this.Architectures.GetRandomList())
            {
                if (architecture.BelongedSection == null || architecture.BelongedSection.AIDetail.AutoRun)
                {
                    if (architecture.BelongedSection == null)
                    {
                        architecture.BelongedSection = architecture.BelongedFaction.FirstSection;
                    }
                    architecture.AI();
                }
                else
                {
                    architecture.PlayerAutoAI();
                }
            }
        }

        private void PlayerAILegions()
        {
            foreach (Legion legion in this.Legions.GetRandomList())
            {
                if ((legion.StartArchitecture != null) && (legion.StartArchitecture.BelongedFaction == this))
                {
                    if (legion.StartArchitecture.BelongedSection.AIDetail.AutoRun)
                    {
                        legion.AI();
                    }
                    else
                    {
                        legion.AIWithAuto();
                    }
                }
                else
                {
                    legion.AIWithAuto();
                }
            }
        }

        private void PlayerAIAppointMayor()
        {
            AIAppointMayor();
        }

        private void PlayerTechniqueAI()
        {
            this.SaveTechniquePointForTechnique(this.TechniquePoint / 4);
        }

        public void PrepareData()
        {
            this.mapData = Session.Current.Scenario.ScenarioMap.MapData;
            this.architectureAdjustCost = new int[Session.Current.Scenario.ScenarioMap.MapDimensions.X, Session.Current.Scenario.ScenarioMap.MapDimensions.Y];
            this.knownAreaData = new Dictionary<Point, InformationTile>();
            this.PrepareSecondTierMapCost();
            this.PrepareThirdTierMapCost();
            this.PrepareKnownAreaData();
            this.PrepareInformations();
        }

        public void PrepareInformations()
        {
            foreach (Information information in this.Informations)
            {
                information.Initialize();
            }
        }

        protected void PrepareKnownAreaData()
        {
            foreach (Architecture architecture in this.Architectures)
            {
                this.AddArchitectureKnownData(architecture);
            }
        }

        private void PrepareSecondTierMapCost()
        {
            this.SecondTierXResidue = Session.Current.Scenario.ScenarioMap.MapDimensions.X % GameObjectConsts.SecondTierSquareSize;
            this.SecondTierYResidue = Session.Current.Scenario.ScenarioMap.MapDimensions.Y % GameObjectConsts.SecondTierSquareSize;
            int num = Session.Current.Scenario.ScenarioMap.MapDimensions.X / GameObjectConsts.SecondTierSquareSize;
            int num2 = Session.Current.Scenario.ScenarioMap.MapDimensions.Y / GameObjectConsts.SecondTierSquareSize;
            if (this.SecondTierXResidue > 0)
            {
                num++;
            }
            if (this.SecondTierYResidue > 0)
            {
                num2++;
            }
            this.secondTierMapCost = new int[num, num2];
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    this.secondTierMapCost[i, j] = this.GetAverageValueOfSecondTier(i, j);
                }
            }
        }

        private void PrepareThirdTierMapCost()
        {
            this.ThirdTierXResidue = Session.Current.Scenario.ScenarioMap.MapDimensions.X % GameObjectConsts.ThirdTierSquareSize;
            this.ThirdTierYResidue = Session.Current.Scenario.ScenarioMap.MapDimensions.Y % GameObjectConsts.ThirdTierSquareSize;
            int num = Session.Current.Scenario.ScenarioMap.MapDimensions.X / GameObjectConsts.ThirdTierSquareSize;
            int num2 = Session.Current.Scenario.ScenarioMap.MapDimensions.Y / GameObjectConsts.ThirdTierSquareSize;
            if (this.ThirdTierXResidue > 0)
            {
                num++;
            }
            if (this.ThirdTierYResidue > 0)
            {
                num2++;
            }
            this.thirdTierMapCost = new int[num, num2];
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num2; j++)
                {
                    this.thirdTierMapCost[i, j] = this.GetAverageValueOfThirdTier(i, j);
                }
            }
        }

        public void PurifyTechniques()
        {
            foreach (Technique technique in this.AvailableTechniques.Techniques.Values)
            {
                technique.Influences.PurifyInfluence(this, GameObjects.Influences.Applier.Technique, technique.ID);
            }
        }

        private void RebuildSections()
        {
            if ((this.Capital != null) && (Session.MainGame.mainGameScreen.LoadScenarioInInitialization || Session.Current.Scenario.Date.Day == 1 || this.SectionCount == 0))
            {
                if (Session.MainGame.mainGameScreen.LoadScenarioInInitialization || ((Session.Current.Scenario.Date.Month % 3) == 1))
                {
                    this.ClearSections();
                    this.BuildSectionByArchitectureList(this.Architectures.GetList());
                    foreach (Section section in this.Sections)
                    {
                        section.RefreshSectionName();
                    }
                }
                this.SetSectionAIDetail();
                this.SetSectionAIDetailPinAtPlayer();
            }
        }

        public void RemoveArchitecture(Architecture architecture)
        {
            this.Architectures.Remove(architecture);
            architecture.BelongedFaction = null;
        }

        public void RemoveArchitectureKnownData(Architecture a)
        {
            foreach (Point point in a.ArchitectureArea.Area)
            {
                this.RemoveKnownAreaData(point, InformationLevel.全);
            }
            foreach (Point point in a.ViewArea.Area)
            {
                if (!Session.Current.Scenario.PositionOutOfRange(point))
                {
                    this.RemoveKnownAreaData(point, InformationLevel.高);
                }
            }
            if (a.Kind.HasLongView)
            {
                foreach (Point point in a.LongViewArea.Area)
                {
                    if (!Session.Current.Scenario.PositionOutOfRange(point))
                    {
                        this.RemoveKnownAreaData(point, InformationLevel.中);
                    }
                }
            }
        }

        public void RemoveArchitectureMilitaries(Architecture architecture)
        {
            foreach (Military military in architecture.Militaries)
            {
                this.RemoveMilitary(military);
            }
        }

        public void RemoveInformation(Information information)
        {
            this.Informations.Remove(information);
            information.BelongedFaction = null;
        }

        public void RemoveLegion(Legion legion)
        {
            this.Legions.Remove(legion);
            legion.BelongedFaction = null;
        }

        public void RemoveMilitary(Military military)
        {
            this.Militaries.Remove(military);
            /*if (this.militaryKindCounts.ContainsKey(military.Kind))
            {
                this.militaryKindCounts[military.Kind]--;
            }*/
            military.BelongedFaction = null;
        }

        /*
        public void MorphMilitary(MilitaryKind before, MilitaryKind after)
        {
            if (this.militaryKindCounts.ContainsKey(before))
            {
                this.militaryKindCounts[before]--;
            }
            if (this.militaryKindCounts.ContainsKey(after))
            {
                this.militaryKindCounts[after]++;
            }
            else
            {
                this.militaryKindCounts[after] = 1;
            }
        }
        */
        public void RemovePositionInformation(Point position, InformationLevel level)
        {
            if (!Session.Current.Scenario.PositionOutOfRange(position))
            {
                this.RemoveKnownAreaData(position, level);
            }
        }

        public void RemoveRouteway(Routeway routeway)
        {
            this.Routeways.Remove(routeway);
            Session.Current.Scenario.Routeways.Remove(routeway);
            routeway.BelongedFaction = null;
        }

        public void RemoveSection(Section section)
        {
            this.Sections.Remove(section);
            section.BelongedFaction = null;
        }

        public void RemoveTroop(Troop troop)
        {
            this.Troops.Remove(troop);
            troop.BelongedFaction = null;
        }

        public void RemoveTroopKnownAreaData(Troop troop)
        {
            foreach (Point point in troop.ViewArea.Area)
            {
                if (Session.Current.Scenario.PositionOutOfRange(point))
                {
                    continue;
                }
                if (point == troop.ViewArea.Centre)
                {
                    this.RemoveKnownAreaData(point, InformationLevel.全);
                }
                else
                {
                    this.RemoveKnownAreaData(point, troop.ScoutLevel);
                }
            }
        }

        public void RemoveTroopMilitary(Troop troop)
        {
            this.RemoveMilitary(troop.Army);
        }

        public bool adjacentTo(Faction f)
        {
            if (this == f) return false;
            if (f != null)
            {
                foreach (Architecture i in this.Architectures)
                {
                    foreach (Architecture j in f.Architectures)
                    {
                        if (i.AILandLinks.GameObjects.Contains(j) || i.AIWaterLinks.GameObjects.Contains(j))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public FactionList AdjecentFactionList
        {
            get
            {
               /* FactionList list = new FactionList();
                foreach (Faction f in this.GetAdjecentFactions())
                {
                    list.Add(f);
                }
                return list;
                */
                return this.GetAdjecentFactions();
            }
        }

        public FactionList GetAdjecentFactions()
        {
            FactionList result = new FactionList();
            foreach (Faction f in Session.Current.Scenario.Factions)
            {
                if (this.adjacentTo(f))
                {
                    result.Add(f);
                }
            }
            return result;
        }

        public FactionList GetAdjecentHostileFactions()
        {
            FactionList result = new FactionList();
            foreach (Faction f in Session.Current.Scenario.Factions)
            {
                if (this.adjacentTo(f) && this.IsHostile(f))
                {
                    result.Add(f);
                }
            }
            return result;
        }

        private bool hasNonFriendlyFrontline
        {
            get
            {
                foreach (Architecture i in this.Architectures)
                {
                    foreach (Architecture j in i.AILandLinks)
                    {
                        if (j.BelongedFaction == null) continue;
                        if (j.BelongedFaction.ID == this.ID) continue;
                        if (Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(j.BelongedFaction.ID, this.ID).Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold)
                        {
                            return true;
                        }
                    }
                    foreach (Architecture j in i.AIWaterLinks)
                    {
                        if (j.BelongedFaction == null) continue;
                        if (j.BelongedFaction.ID == this.ID) continue;
                        if (Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(j.BelongedFaction.ID, this.ID).Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        public FactionList GetEncircleFactionList(Faction target, bool simulate) 
        {
            FactionList encircleList = new FactionList();
            foreach (Faction f in Session.Current.Scenario.Factions)
            {
                if (Session.Current.Scenario.IsPlayer(f)) continue; // TODO let player choose whether to enter
                if (f.Leader.Status == PersonStatus.Captive) continue;
                if ((f != target) && (f.Leader.StrategyTendency != PersonStrategyTendency.维持现状 || Session.GlobalVariables.IgnoreStrategyTendency) && !f.IsAlien)
                {
                    if (((Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(target.ID, f.ID).Relation +
                        Person.GetIdealOffset(target.Leader, f.Leader) * 1.5) < 0
                        && (GameObject.Chance(60 - Math.Min(60, target.Leader.Karma)) || simulate) && !f.IsFriendly(target) && 
                        (f.adjacentTo(target) || GameObject.Chance(30 - Math.Min(60, target.Leader.Karma) / 2) || simulate))
                        )
                    {
                        encircleList.Add(f);
                    }
                }
            }
            if (encircleList.Count >= 3 && (!simulate || encircleList.Count >= 6))
            {
                return encircleList;
            }
            else
            {
                return null;
            }
        }

        public void CheckEncircleDiplomatic(Faction target)
        {
            FactionList encircleList = GetEncircleFactionList(target, false);
            if (encircleList != null)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this.Leader, this.Leader.Name, TextMessageKind.EncircleDiplomaticRelation, "EncircleDiplomaticRelation", "EncircleDiplomaticRelation.jpg", "EncircleDiplomaticRelation", target.Name, true);
                foreach (Faction i in encircleList)
                {
                    foreach (Faction j in encircleList)
                    {
                        if (i != j)
                        {
                            if (Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(i.ID, j.ID).Truce < 180)
                            {
                                Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(i.ID, j.ID).Truce = 180;
                            }
                        }
                    }
                    target.Leader.AdjustRelation(i.Leader, -6f, -4);
                    i.Leader.AdjustRelation(target.Leader, -1.5f, -1);
                }
            }
        }

        public void Encircle(Architecture encircler, Faction toEncircle)
        {
            Session.MainGame.mainGameScreen.xianshishijiantupian(Session.Current.Scenario.NeutralPerson, encircler.BelongedFaction.Leader.Name, "DenounceDiplomaticRelation", "DenounceDiplomaticRelation.jpg", "DenounceDiplomaticRelation", toEncircle.Name, true);

            if (encircler.Fund < 120000) return;
            encircler.Fund -= 120000;

            DiplomaticRelation rel = Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(this.ID, toEncircle.ID);
            if (rel.Relation > -Session.GlobalVariables.FriendlyDiplomacyThreshold)
            {
                rel.Relation -= 100;
                toEncircle.Leader.AdjustRelation(this.Leader, -9f, -6);
                this.Leader.AdjustRelation(toEncircle.Leader, -3f, -2);
            }
            else
            {
                rel.Relation -= 50;
                toEncircle.Leader.AdjustRelation(this.Leader, -9f, -6);
                this.Leader.AdjustRelation(toEncircle.Leader, -3f, -2);
            }
            //处理所有势力和被声讨方的关系
            foreach (DiplomaticRelation f in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(toEncircle.ID))
            {
                if (f.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold / 2)
                {
                    f.Relation -= 100;
                }
            }
            //加入包围圈判定
            this.CheckEncircleDiplomatic(toEncircle);
        }

        private void ResetFriendlyDiplomaticRelations()
        {
            foreach (DiplomaticRelation i in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(base.ID))
            {
                if (this.IsAlien) continue;
                Faction opposite = i.GetDiplomaticFaction(this.ID);
                if (opposite != null && i.Relation >= -Session.GlobalVariables.FriendlyDiplomacyThreshold && opposite.IsAlien)
                {
                    i.Relation -= 15; 
                }
            }

            if (Session.Current.Scenario.IsPlayer(this)) return;

            //bool relationBroken = false;

            if (Session.GlobalVariables.PinPointAtPlayer)
            {
                foreach (DiplomaticRelation i in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(base.ID))
                {
                    Faction opposite = i.GetDiplomaticFaction(this.ID);
                    if (i.Relation >= -Session.GlobalVariables.FriendlyDiplomacyThreshold && !this.IsFriendly(opposite) &&
                        Session.Current.Scenario.IsPlayer(opposite))
                    {
                        i.Relation -= 15; //focus到玩家的时候，每月降低15点友好度
                    }
                }
            }

            if ((this.Leader.StrategyTendency != PersonStrategyTendency.维持现状))
            {
                // Break Relations
                FactionList nonFriendlyFactions = new FactionList();
                foreach (Faction f in Session.Current.Scenario.Factions)
                {
                    if (this.adjacentTo(f) && Session.Current.Scenario.GetDiplomaticRelation(this.ID, f.ID) < Session.GlobalVariables.FriendlyDiplomacyThreshold)
                    {
                        nonFriendlyFactions.Add(f);
                    }
                }
                
                FactionList nearbyFactions = this.GetAdjecentFactions();
                Faction toBreak = null;
                int power = int.MaxValue;
                int totalPower = 0;
                foreach (Faction f in nearbyFactions)
                {
                    DiplomaticRelation rel = Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(this.ID, f.ID);
                    if (rel.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold)
                    {
                        totalPower += f.Power;
                    }
                }
                foreach (Faction f in nearbyFactions)
                {
                    DiplomaticRelation rel = Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(this.ID, f.ID);
                    if (rel.Truce <= 0)
                    {
                        int unAmbition = 4 - this.Leader.Ambition;
                        if (nonFriendlyFactions.Count == 0 && f.Power < this.Power)
                        {
                            power = f.Power;
                            toBreak = f;
                        }
                        else if (this.Power > totalPower * ((unAmbition * unAmbition + (this.Leader.Calmness - this.Leader.Braveness) / 4) * 0.2 + 0.6) && 
                            rel.Relation >= Session.GlobalVariables.FriendlyDiplomacyThreshold)
                        {
                            float ratio = (float)this.Power / f.Power;
                            if (GameObject.Chance((int)((ratio - 1) * this.Leader.Ambition * 10)))
                            {
                                power = f.Power;
                                toBreak = f;
                            }
                        }
                    }
                }

                if (toBreak != null)
                {
                    Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelation(this.ID, toBreak.ID).Relation = 0;

                    this.Leader.DecreaseKarma(5);

                    //AI宣布主动解盟
                    Session.MainGame.mainGameScreen.xianshishijiantupian(toBreak.Leader, this.Leader.Name, TextMessageKind.ResetDiplomaticRelation, "ResetDiplomaticRelation", "ResetDiplomaticRelation.jpg", "ResetDiplomaticRelation", toBreak.LeaderName, true);
                }

                // Randomly alter relations
                foreach (DiplomaticRelation rel in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(base.ID))
                {
                    Faction opposite = rel.GetDiplomaticFaction(this.ID);
                    if (opposite != null)
                    {
                        if (rel.Relation > 300) continue;
                        if (rel.Truce > 0)
                        {
                            rel.Relation += 5;
                        }
                        rel.Relation += (int)(Person.GetIdealAttraction(opposite.Leader, this.Leader) * (GameObject.Random(100) / 1000.0f + 0.1f) / (Math.Abs(rel.Relation) / 10.0f + 1));
                    }
                }

                    /*
                    int minTroop = int.MaxValue;
                    DiplomaticRelation minTroopFactionRelation = null;
                    Faction minTroopFactionopposite = null;

                    foreach (DiplomaticRelation i in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(base.ID))
                    {
                        Faction opposite = i.GetDiplomaticFaction(this.ID);
                        //if (i.Relation < 300) continue; 
                        if (!this.adjacentTo(opposite)) continue;    //不接壤的AI不主动改变关系值
                        if (GameObject.Chance((int)((double)this.armyScale / opposite.ArmyScale * ((int)this.Leader.Ambition + 1) * 20))
                            && i.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold)
                        {
                            i.Relation -= (7 + (int)Random(15)); //根据总兵力情况每月随机减少
                            i.Relation -= (Person.GetIdealOffset(this.Leader, opposite.Leader)) / 10;
                            relationBroken = true;
                            break;
                        }
                        //增加关系300以上，随机一个降低数值后主动解盟的情况
                        if (GameObject.Chance((int)(Person.GetIdealOffset(this.Leader, opposite.Leader) / 3)) && i.Relation >= 300)
                        {
                            i.Relation -= (7 + (int)Random(15));
                            i.Relation -= (Person.GetIdealOffset(this.Leader, opposite.Leader)) / 10;
                            relationBroken = true;
                            if (i.Relation < Session.GlobalVariables.FriendlyDiplomacyThreshold)
                            {
                                //显示联盟破裂画面
                                Session.MainGame.mainGameScreen.xianshishijiantupian(this.Leader, this.Leader.Name, TextMessageKind.BreakDiplomaticRelation, "BreakDiplomaticRelation", "BreakDiplomaticRelation.jpg", "BreakDiplomaticRelation", opposite.Leader.Name, true);
                            }
                            break;
                        }

                        if (!this.hasNonFriendlyFrontline)
                        {
                            if (opposite.ArmyScale < minTroop)
                            {
                                minTroop = opposite.ArmyScale;
                                minTroopFactionRelation = i;
                                minTroopFactionopposite = i.GetDiplomaticFaction(this.ID);
                            }
                        }
                    }
                    if (minTroopFactionRelation != null && !relationBroken)
                    {
                        minTroopFactionRelation.Relation = 0;
                        //AI宣布主动解盟
                        Session.MainGame.mainGameScreen.xianshishijiantupian(this.Leader, this.Leader.Name, TextMessageKind.ResetDiplomaticRelation, "ResetDiplomaticRelation", "ResetDiplomaticRelation.jpg", "ResetDiplomaticRelation", minTroopFactionopposite.LeaderName, true);
                    }
                    */
                }
        }

        public bool RoutewayPathAvail(Point start, Point end, bool hasEnd)
        {
            bool result = this.RoutewayPathBuilder.GetPath(start, end, hasEnd);
            return result;
        }

        private int RoutewayPathBuilder_OnGetCost(Point position, out float consumptionRate)
        {
            consumptionRate = 0f;
            if (!Session.Current.Scenario.PositionOutOfRange(position))
            {
                if (Session.Current.Scenario.GetArchitectureByPositionNoCheck(position) != null)
                {
                    return 0x3e8;
                }
                if (this.RoutewayPathBuilder.MultipleWaterCost && !Session.Current.Scenario.IsWaterPositionRoutewayable(position))
                {
                    return 0x3e8;
                }
                TerrainDetail terrainDetailByPositionNoCheck = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(position);
                if (terrainDetailByPositionNoCheck != null)
                {
                    consumptionRate = terrainDetailByPositionNoCheck.RoutewayConsumptionRate;
                    int routewayBuildWorkCost = terrainDetailByPositionNoCheck.RoutewayBuildWorkCost;
                    int routewayWorkForce = this.RoutewayWorkForce;
                    if (this.RoutewayPathBuilder.MultipleWaterCost && (terrainDetailByPositionNoCheck.ID == 6))
                    {
                        routewayBuildWorkCost = this.RoutewayWorkForce;
                    }
                    return ((routewayBuildWorkCost <= routewayWorkForce) ? routewayBuildWorkCost : 0x3e8);
                }
            }
            return 0x3e8;
        }

        private int RoutewayPathBuilder_OnGetPenalizedCost(Point position)
        {
            foreach (Architecture architecture in Session.Current.Scenario.GetHighViewingArchitecturesByPosition(position))
            {
                if (!((architecture.BelongedFaction == null) || this.IsFriendly(architecture.BelongedFaction)))
                {
                    return (this.RoutewayWorkForce / 2);
                }
            }
            return 0;
        }

        public bool Run()
        {
            if (!this.preUserControlFinished)
            {
                this.Develop();
                this.preUserControlFinished = true;
            }
            if (this.Controlling || this.Passed)
            {
                return this.Passed;
            }
            if (Session.Current.Scenario.IsPlayer(this))
            {
                if (this.WantControl)
                {
                    if (!Session.Current.Scenario.Threading)
                    {
                        if (!this.AIFinished)
                        {
                            /*thread = new Thread(new ThreadStart(this.PlayerAI));
                            thread.Start();
                            thread.Join();
                            thread = null;*/
                            this.PlayerAI();
                            return false;
                        }
                        this.Controlling = true;
                        if (this.OnGetControl != null)
                        {
                            this.OnGetControl(this);
                        }
                        return false;
                    }
                    return false;
                }
                if (!Session.Current.Scenario.Threading)
                {
                    if (!this.AIFinished)
                    {
                        /*thread = new Thread(new ThreadStart(this.PlayerAI));
                            thread.Start();
                            thread.Join();
                            thread = null;*/
                        this.PlayerAI();
                        return false;
                    }
                    this.Passed = true;
                    return true;
                }
                return false;
            }

            if (!this.AIFinished)
            {
                /*thread = new Thread(new ThreadStart(this.AI));
                        thread.Start();
                        thread.Join();
                        thread = null;*/
                this.AI();
                return false;
            }
            this.Passed = true;
            return true;
        }

        public string SaveLegionsToString()
        {
            string str = "";
            foreach (Legion legion in this.Legions)
            {
                str = str + legion.ID.ToString() + " ";
            }
            return str;
        }

        public string SaveSectionsToString()
        {
            string str = "";
            foreach (Section section in this.Sections)
            {
                str = str + section.ID.ToString() + " ";
            }
            return str;
        }

        public void SaveTechniquePointForFacility(int credit)
        {
            if (credit > this.techniquePoint)
            {
                credit = this.techniquePoint;
            }
            this.techniquePoint -= credit;
            this.techniquePointForFacility += credit;
        }

        public void SaveTechniquePointForTechnique(int credit)
        {
            if (credit > this.techniquePoint)
            {
                credit = this.techniquePoint;
            }
            this.techniquePoint -= credit;
            this.techniquePointForTechnique += credit;
        }

        public string SaveTroopsToString()
        {
            string str = "";
            foreach (Troop troop in this.Troops)
            {
                str = str + troop.ID.ToString() + " ";
            }
            return str;
        }

        public void SeasonEvent()
        {
            this.RefrehCreatePersonTimes();
            this.shizheshengguan();
        }

        private void RefrehCreatePersonTimes()
        {
            if (Session.Current.Scenario.Date.Day <= Session.Current.Scenario.Parameters.DayInTurn)
            {
                this.ZhaoxianFailureCount = 0;
            }
        }

        public bool BecomeEmperorLegallyAvail()  //可以禅位
        {
            if (this.IsAlien || !Session.Current.Scenario.youhuangdi())
            {
                return false;
            }
            if (this.guanjue != Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Count - 2)  //不是王
            {
                return false;
            }
            if (this.Capital.Fund < 100000)
            {
                return false;
            }
            guanjuezhongleilei shengjiguanjue = new guanjuezhongleilei();
            shengjiguanjue = Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue + 1);
            if (this.HasEmperor() && this.chaotinggongxiandu >= shengjiguanjue.xuyaogongxiandu && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
            {
                return true;
            }
            return false;
        }


        public bool SelfBecomeEmperorAvail()  //可以称帝
        {
            if (this.guanjue != Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Count - 2)  //不是王
            {
                return false;
            }
            if (this.Capital.Fund < 100000)
            {
                return false;
            }
            guanjuezhongleilei shengjiguanjue = new guanjuezhongleilei();
            shengjiguanjue = Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue + 1);

            if (this.IsAlien)
            {
                if (this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (Session.Current.Scenario.youhuangdi())
                {
                    if (!this.HasEmperor() && this.chaotinggongxiandu >= shengjiguanjue.xuyaogongxiandu && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    if (this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
        }

        private void AIBecomeEmperor()
        {
            if (this.guanjue != Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Count - 2)  //不是王
            {
                return;
            }
            if (this.Capital == null || this.Capital.Fund < 100000)
            {
                return;
            }
            guanjuezhongleilei shengjiguanjue = new guanjuezhongleilei();
            shengjiguanjue = Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue + 1);
            if (Session.Current.Scenario.youhuangdi())
            {
                if (this.IsAlien && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    this.SelfBecomeEmperor();
                }
                else if (!this.IsAlien && this.chaotinggongxiandu >= shengjiguanjue.xuyaogongxiandu && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    if (this.HasEmperor())
                    {
                        foreach (Faction f in Session.Current.Scenario.Factions.GameObjects)
                        {
                            if (f == this) continue;
                            if (f.guanjue == Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Count - 1) continue;
                            if (GameObject.Random((int)(((int)this.Leader.Ambition + 1) * 2 * (this.ArchitectureCount / (double)f.ArchitectureCount))) == 0)
                            {
                                return;
                            }
                        }
                        this.BecomeEmperorLegally();
                    }
                    else if (this.Leader.ValuationOnGovernment == PersonValuationOnGovernment.无视 ||
                        (this.Leader.ValuationOnGovernment == PersonValuationOnGovernment.普通 && GameObject.Chance(5)))
                    {
                        /*Faction owningEmperor = null;
                        foreach (Faction f in Session.Current.Scenario.Factions.GameObjects)
                        {
                            if (f.HasEmperor())
                            {
                                owningEmperor = f;
                                break;
                            }
                        }*/
                        if (GameObject.Random((int)((5 - (int)this.Leader.Ambition) * 100 / (double)this.ArchitectureCount)) == 0)
                        {
                            this.SelfBecomeEmperor();
                        }
                    }
                }
            }
            else
            {
                if (this.IsAlien && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    this.SelfBecomeEmperor();
                }
                else if (!this.IsAlien && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    this.SelfBecomeEmperor();
                }
            }

        }

        public void BecomeEmperorLegally()
        {
            this.guanjue++;
            Session.Current.Scenario.YearTable.addBecomeEmperorLegallyEntry(Session.Current.Scenario.Date, Session.Current.Scenario.Persons.GetGameObject(7000) as Person, this);
            Session.MainGame.mainGameScreen.xianshishijiantupian(Session.Current.Scenario.Persons.GetGameObject(7000) as Person, this.LeaderName, TextMessageKind.BecomeEmperorLegally, "BecomeEmperorLegally", "shanwei.jpg", "",
                Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue).Name, true);
            Session.MainGame.mainGameScreen.xiejinxingjilu("BecomeEmperorLegally", this.LeaderName,
                Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue).Name, this.Leader.Position);
            this.Capital.DecreaseFund(100000);

            ExtensionInterface.call("BecomeEmperorLegally", new Object[] { Session.Current.Scenario, this });
            Session.Current.Scenario.BecomeNoEmperor();
        }

        private bool HasEmperor()
        {
            foreach (Architecture a in this.Architectures)
            {
                if (a.huangdisuozai) return true;
            }
            return false;
        }



        private void shizheshengguan()
        {
            if (this.guanjue >= Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Count - 2)  //已经是王或者皇帝
            {
                return;
            }
            guanjuezhongleilei shengjiguanjue = new guanjuezhongleilei();
            shengjiguanjue = Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue + 1);
            if (Session.Current.Scenario.youhuangdi())
            {
                if (this.IsAlien && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    this.SelfAdvancement();
                }
                else if (!this.IsAlien && this.chaotinggongxiandu >= shengjiguanjue.xuyaogongxiandu && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    this.Advancement();
                }
            }
            else
            {
                if (this.IsAlien && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    this.SelfAdvancement();
                }
                else if (!this.IsAlien && this.chengchigeshu() >= shengjiguanjue.xuyaochengchi)
                {
                    this.SelfAdvancement();
                }
            }



        }

        public void SelfBecomeEmperor()
        {
            this.guanjue++;
            Session.Current.Scenario.YearTable.addSelfBecomeEmperorEntry(Session.Current.Scenario.Date, this);
            Session.MainGame.mainGameScreen.xianshishijiantupian(this.Leader, this.LeaderName, TextMessageKind.BecomeEmperorIllegally, "Zili", "BecomeEmperor.jpg", "",
                Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue).Name, true);
            Session.MainGame.mainGameScreen.xiejinxingjilu("Zili", this.LeaderName,
                Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue).Name, this.Leader.Position);
            this.Capital.DecreaseFund(100000);
            if (!Session.Current.Scenario.youhuangdi() || this.IsAlien)
            {
                return;
            }
            else
            {
                this.DoSelfBecomeEmperorInfluence();
            }
            ExtensionInterface.call("SelfBecomeEmperor", new Object[] { Session.Current.Scenario, this });
        }

        private void DoSelfBecomeEmperorInfluence()
        {
            foreach (Architecture a in this.Architectures)
            {
                a.Morale = (int)(a.Morale * 0.9f);
            }
#pragma warning disable CS0168 // The variable 'loyaltyMultiplier' is declared but never used
            float loyaltyMultiplier;
#pragma warning restore CS0168 // The variable 'loyaltyMultiplier' is declared but never used
            foreach (Person person in this.Persons)
            {
                if (person == this.Leader)
                {
                    continue;
                }

                switch (person.ValuationOnGovernment)
                {
                    case PersonValuationOnGovernment.无视:
                        break;
                    case PersonValuationOnGovernment.普通:
                        person.TempLoyaltyChange = -10;
                        break;
                    case PersonValuationOnGovernment.重视:
                        person.TempLoyaltyChange = -60;
                        break;
                    default:
                        continue;

                }
            }
            Session.MainGame.mainGameScreen.xianshishijiantupian(this.Leader, "", TextMessageKind.SelfBecomeInfluenceConsequence, "SelfBecomeEmperorInfluence", "", "", true);
        }


        private void Advancement()
        {
            this.guanjue++;

            guanjuezhongleilei gj = Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue);
            if (Session.Current.Scenario.IsPlayer(this) || gj.ShowDialog)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(Session.Current.Scenario.Persons.GetGameObject(7000) as Person, this.LeaderName, TextMessageKind.RiseEmperorClass, "shengguan", "shengguan.jpg", "",
                   gj.Name, true);
                Session.MainGame.mainGameScreen.xiejinxingjilu("shengguan", this.LeaderName,
                    gj.Name, this.Leader.Position);
            }

            Session.Current.Scenario.YearTable.addAdvanceGuanjueEntry(Session.Current.Scenario.Date, this, Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue));
            ExtensionInterface.call("Advancement", new Object[] { Session.Current.Scenario, this });
        }

        private void SelfAdvancement()
        {
            this.guanjue++;

            guanjuezhongleilei gj = Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue);
            if (Session.Current.Scenario.IsPlayer(this) || gj.ShowDialog)
            {
                Session.MainGame.mainGameScreen.xianshishijiantupian(this.Leader, this.LeaderName, TextMessageKind.SelfRiseEmperorClass, "Zili", "", "",
                    Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue).Name, true);
                Session.MainGame.mainGameScreen.xiejinxingjilu("Zili", this.LeaderName,
                    Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue).Name, this.Leader.Position);
            }
            Session.Current.Scenario.YearTable.addSelfAdvanceGuanjueEntry(Session.Current.Scenario.Date, this, Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue));
            ExtensionInterface.call("SelfAdvancement", new Object[] { Session.Current.Scenario, this });
        }

        public int CityCount
        {
            get
            {
                return chengchigeshu();
            }
        }

        public int chengchigeshu()
        {
            int geshu = 0;
            foreach (Architecture a in this.Architectures)
            {
                if (a.Kind.CountToMerit)
                {
                    geshu++;
                }
            }
            return geshu;
        }

        public string guanjuezifuchuan
        {
            get
            {
                return Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue).Name;
            }
        }

        public int shengwangshangxian
        {
            get
            {
                return Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue).shengwangshangxian;
            }

        }
        public int shengguanxuyaogongxiandu
        {
            get
            {
                if (this.IsAlien || !Session.Current.Scenario.youhuangdi())
                {
                    return 0;
                }
                else
                {
                    if (this.guanjue >= Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Count - 1)
                    {
                        return 0;
                    }
                    else
                    {
                        return Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue + 1).xuyaogongxiandu;

                    }
                }
            }

        }
        public int shengguanxuyaochengchi
        {
            get
            {
                if (this.guanjue >= Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Count - 1)
                {
                    return 0;
                }
                else
                {
                    return Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhonglei(this.guanjue + 1).xuyaochengchi;

                }
            }

        }

        public Architecture SelectNewCapital()
        {
            int population = 0;
            Architecture architecture = null;
            foreach (Architecture architecture2 in this.Architectures)
            {
                if ((architecture2 != this.Capital) && (architecture2.Population >= population))
                {
                    architecture = architecture2;
                    population = architecture2.Population;
                }
            }
            return architecture;
        }

        private void SetSectionAIDetail()
        {
            foreach (Section s in this.Sections)
            {
                GameObjectList candidates = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(true, false);
                if (candidates.Count > 0)
                {
                    s.AIDetail = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetail(candidates[GameObject.Random(candidates.Count)].ID);
                }
            }
        }

        private void SetSectionAIDetail_OLD()
        {
            Faction faction;
            int num2;
            int threat;
            GameObjectList sectionNoOrientationAutoAIDetailsByConditions;
            int num5;
            GameObjectList list5;
            if (this.SectionCount <= 1)
            {
                num5 = ((this.FirstSection.ArchitectureScale / 2) - (this.FirstSection.ArchitectureCount / 2)) + 1;
                if (this.ArmyScale < (num5 * 6))
                {
                    if (!this.Capital.IsOK())
                    {
                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.无, true, false, false, false, false);
                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                        {
                            this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                        }
                    }
                    else
                    {
                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(false, true);
                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                        {
                            this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                        }
                    }
                    return;
                }
                if (this.ArmyScale < (num5 * 12))
                {
                    if (!this.Capital.IsOK())
                    {
                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.无, true, false, false, false, false);
                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                        {
                            this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                        }
                    }
                    else
                    {
                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(false, true);
                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                        {
                            this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                        }
                    }
                    return;
                }
                if (this.ArmyScale < (num5 * 20))
                {
                    if (!this.Capital.IsOK())
                    {
                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.无, true, false, false, false, false);
                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                        {
                            this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                        }
                    }
                    else
                    {
                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(true, true);
                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                        {
                            this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                        }
                    }
                    return;
                }
                if (this.ArmyScale >= (num5 * 30))
                {
                    if (!this.Capital.IsGood())
                    {
                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(true, false);
                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                        {
                            this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                        }
                        return;
                    }
                    faction = null;
                    num2 = -2147483648;
                    foreach (Faction faction2 in Session.Current.Scenario.Factions.GetRandomList())
                    {
                        if (((faction2 != this) && !this.IsFriendly(faction2)) && (faction2.Capital != null))
                        {
                            threat = this.GetThreat(faction2);
                            if ((threat > num2) || GameObject.Chance(20))
                            {
                                num2 = threat;
                                faction = faction2;
                            }
                        }
                    }
                    if ((faction != null) && (this.FirstSection.OrientationFaction != faction))
                    {
                        foreach (Architecture architecture in this.Architectures)
                        {
                            if (architecture.HasFactionInClose(faction, 1))
                            {
                                sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.势力, true, true, true, false, true);
                                if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                {
                                    this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                    this.FirstSection.OrientationFaction = faction;
                                }
                                break;
                            }
                        }
                    }
                    list5 = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.州域, true, true, true, false, true);
                    if (list5.Count > 0)
                    {
                        this.FirstSection.AIDetail = list5[GameObject.Random(list5.Count)] as SectionAIDetail;
                        if (this.Capital.LocationState.GetFactionScale(this) < 100)
                        {
                            this.FirstSection.OrientationState = this.Capital.LocationState;
                        }
                        else
                        {
                            this.FirstSection.OrientationState = this.Capital.LocationState.ContactStates[GameObject.Random(this.Capital.LocationState.ContactStates.Count)] as State;
                        }
                    }
                    return;
                }
                if (!this.Capital.IsGood())
                {
                    sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(true, false);
                    if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                    {
                        this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                    }
                    return;
                }
                faction = null;
                num2 = -2147483648;
                foreach (Faction faction2 in this.GetUnderZeroDiplomaticRelationFactions().GetRandomList())
                {
                    threat = this.GetThreat(faction2);
                    if ((threat > num2) || GameObject.Chance(20))
                    {
                        num2 = threat;
                        faction = faction2;
                    }
                }
                if ((faction != null) && (this.FirstSection.OrientationFaction != faction))
                {
                    foreach (Architecture architecture in this.Architectures)
                    {
                        if (architecture.HasFactionInClose(faction, 1))
                        {
                            sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.势力, true, true, true, false, true);
                            if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                            {
                                this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                this.FirstSection.OrientationFaction = faction;
                            }
                            break;
                        }
                    }
                }
            }
            else
            {
                int num = (this.ArmyScale / 60) - (int)this.Leader.StrategyTendency;
                if (num <= 0)
                {
                    foreach (Section section in this.Sections)
                    {
                        if (section.AIDetail.OrientationKind == SectionOrientationKind.无)
                        {
                            Section section2;
                            num5 = this.FirstSection.ArchitectureScale - (this.FirstSection.ArchitectureCount / 2);
                            if (section.GetHostileScale() > 0)
                            {
                                if (section.ArmyScale > (num5 * 6))
                                {
                                    foreach (Architecture architecture in section.Architectures)
                                    {
                                        section2 = null;
                                        if (architecture.HasOffensiveSectionInClose(out section2, 1))
                                        {
                                            sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.军区, true, false, false, true, true);
                                            if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                            {
                                                section.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                                section.OrientationSection = section2;
                                            }
                                            break;
                                        }
                                    }
                                    if (section.OrientationSection == null)
                                    {
                                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(true, true);
                                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                        {
                                            this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                        }
                                    }
                                }
                                else
                                {
                                    sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(false, true);
                                    if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                    {
                                        this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                    }
                                }
                            }
                            else if ((section.GetFrontScale() > 0) && (section.ArmyScale < (num5 * 6)))
                            {
                                sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(true, false);
                                if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                {
                                    this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                }
                            }
                            else
                            {
                                foreach (Architecture architecture in section.Architectures)
                                {
                                    section2 = null;
                                    if (architecture.HasOffensiveSectionInClose(out section2, 1))
                                    {
                                        sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.军区, true, false, false, false, false);
                                        if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                        {
                                            section.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                            section.OrientationSection = section2;
                                        }
                                        break;
                                    }
                                }
                                if (section.OrientationSection == null)
                                {
                                    sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionNoOrientationAutoAIDetailsByConditions(false, false);
                                    if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                    {
                                        this.FirstSection.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                    }
                                }
                            }
                        }
                    }
                    return;
                }
                faction = null;
                num2 = -2147483648;
                int num3 = 0;
                foreach (Faction faction2 in this.GetUnderZeroDiplomaticRelationFactions().GetRandomList())
                {
                    threat = this.GetThreat(faction2);
                    if ((threat > num2) || GameObject.Chance(20))
                    {
                        num2 = threat;
                        faction = faction2;
                    }
                }
                GameObjectList list = this.Sections.GetList();
                list.PropertyName = "ArmyScale";
                list.IsNumber = true;
                list.ReSort();
                if (faction != null)
                {
                    foreach (Section section in list)
                    {
                        if (section.ArmyScale < 0x19)
                        {
                            break;
                        }
                        foreach (Architecture architecture in section.Architectures)
                        {
                            if (architecture.HasFactionInClose(faction, 1))
                            {
                                sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.势力, true, true, true, false, true);
                                if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                {
                                    section.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                    section.OrientationFaction = faction;
                                    num3++;
                                }
                                break;
                            }
                        }
                        if (num3 >= num)
                        {
                            break;
                        }
                    }
                    return;
                }
                if (this.Capital.LocationState.GetFactionScale(this) < 100)
                {
                    foreach (Section section in list)
                    {
                        if (section.ArmyScale < 0x19)
                        {
                            break;
                        }
                        if (this.Capital.LocationState.GetSectionScale(section) >= 60)
                        {
                            sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.州域, true, true, true, false, true);
                            if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                            {
                                section.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                section.OrientationState = this.Capital.LocationState;
                                num3++;
                            }
                            if (num3 >= num)
                            {
                                break;
                            }
                        }
                    }
                    return;
                }
                if (this.Capital.LocationState.LinkedRegion.GetFactionScale(this) < 100)
                {
                    foreach (Section section in list)
                    {
                        if (section.ArmyScale < 0x19)
                        {
                            break;
                        }
                        if (this.Capital.LocationState.LinkedRegion.GetSectionScale(section) >= 60)
                        {
                            sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.州域, true, true, true, false, true);
                            if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                            {
                                section.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                foreach (Architecture architecture in section.Architectures)
                                {
                                    if (architecture.LocationState.LinkedRegion == this.Capital.LocationState.LinkedRegion)
                                    {
                                        section.OrientationState = architecture.LocationState;
                                        break;
                                    }
                                }
                                num3++;
                            }
                            if (num3 >= num)
                            {
                                break;
                            }
                        }
                    }
                    return;
                }
                StateList list3 = new StateList();
                foreach (State state in this.Capital.LocationState.LinkedRegion.States)
                {
                    foreach (State state2 in state.ContactStates)
                    {
                        if ((state2.LinkedRegion != this.Capital.LocationState.LinkedRegion) && (state2.GetFactionScale(this) < 100))
                        {
                            list3.Add(state2);
                        }
                    }
                }
                if (list3.Count > 0)
                {
                    foreach (Section section in list)
                    {
                        if (section.ArmyScale < 0x19)
                        {
                            break;
                        }
                        if (this.Capital.LocationState.LinkedRegion.GetSectionScale(section) >= 60)
                        {
                            sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.州域, true, true, true, false, true);
                            if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                            {
                                section.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                section.OrientationState = list3[GameObject.Random(list3.Count)] as State;
                                num3++;
                            }
                            if (num3 >= num)
                            {
                                break;
                            }
                        }
                    }
                }
                if (num3 < num)
                {
                    foreach (Section section in list)
                    {
                        if (section.ArmyScale < 0x19)
                        {
                            return;
                        }
                        if (this.Capital.LocationState.LinkedRegion.GetSectionScale(section) <= 0)
                        {
                            Architecture maxPopulationArchitecture = section.MaxPopulationArchitecture;
                            if (maxPopulationArchitecture != null)
                            {
                                StateList list4 = new StateList();
                                foreach (State state3 in maxPopulationArchitecture.LocationState.ContactStates)
                                {
                                    if ((state3.LinkedRegion != this.Capital.LocationState.LinkedRegion) && (state3.GetFactionScale(this) < 100))
                                    {
                                        list4.Add(state3);
                                    }
                                }
                                if (list4.Count > 0)
                                {
                                    sectionNoOrientationAutoAIDetailsByConditions = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.州域, true, true, true, false, true);
                                    if (sectionNoOrientationAutoAIDetailsByConditions.Count > 0)
                                    {
                                        section.AIDetail = sectionNoOrientationAutoAIDetailsByConditions[GameObject.Random(sectionNoOrientationAutoAIDetailsByConditions.Count)] as SectionAIDetail;
                                        section.OrientationState = list4[GameObject.Random(list4.Count)] as State;
                                        num3++;
                                    }
                                    if (num3 >= num)
                                    {
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                return;
            }
            list5 = Session.Current.Scenario.GameCommonData.AllSectionAIDetails.GetSectionAIDetailsByConditions(SectionOrientationKind.州域, true, true, true, false, true);
            if (list5.Count > 0)
            {
                this.FirstSection.AIDetail = list5[GameObject.Random(list5.Count)] as SectionAIDetail;
                if (this.Capital.LocationState.GetFactionScale(this) < 100)
                {
                    this.FirstSection.OrientationState = this.Capital.LocationState;
                }
                else
                {
                    this.FirstSection.OrientationState = this.Capital.LocationState.ContactStates[GameObject.Random(this.Capital.LocationState.ContactStates.Count)] as State;
                }
            }
        }

        private void TechniquesDayEvent()
        {
            if (this.UpgradingTechnique >= 0)
            {
                this.UpgradingDaysLeft--;
                if (this.UpgradingDaysLeft <= 0)
                {
                    Technique technique = Session.Current.Scenario.GameCommonData.AllTechniques.GetTechnique(this.UpgradingTechnique);
                    if (technique != null)
                    {
                        this.AvailableTechniques.AddTechnique(technique);
                        Session.Current.Scenario.NewInfluence = true;
                        technique.Influences.ApplyInfluence(this, GameObjects.Influences.Applier.Technique, technique.ID);
                        Session.Current.Scenario.NewInfluence = false;
                        if (this.OnTechniqueFinished != null)
                        {
                            this.OnTechniqueFinished(this, technique);
                        }
                        ExtensionInterface.call("TechniqueUpgradeComplete", new Object[] { Session.Current.Scenario, this, technique });
                        Session.Current.Scenario.YearTable.addFactionTechniqueCompletedEntry(Session.Current.Scenario.Date, this, technique);
                        Session.MainGame.mainGameScreen.TechniqueComplete(this, technique);
                    }
                    this.UpgradingTechnique = -1;
                }
            }
        }

        public override string ToString()
        {
            return base.Name;
        }

        public void UpgradeTechnique(Technique technique, Architecture architecture)
        {
            this.UpgradingTechnique = technique.ID;
            this.UpgradingDaysLeft = getTechniqueActualTime(technique);
            if (this.TechniquePoint < this.getTechniqueActualPointCost(technique))
            {
                this.DepositTechniquePointForTechnique(this.getTechniqueActualPointCost(technique) - this.TechniquePoint);
                if (this.TechniquePoint < this.getTechniqueActualPointCost(technique))
                {
                    this.DepositTechniquePointForFacility(this.getTechniqueActualPointCost(technique) - this.TechniquePoint);
                }
            }
            this.DecreaseTechniquePoint(this.getTechniqueActualPointCost(technique));
            architecture.DecreaseFund(this.getTechniqueActualFundCost(technique));
            ExtensionInterface.call("UpgradeTechnique", new Object[] { Session.Current.Scenario, this });
            if (this.OnUpgradeTechnique != null)
            {
                this.OnUpgradeTechnique(this, technique, architecture);
            }
        }

        public void YearEvent()
        {
        }

        public int ArchitectureCount
        {
            get
            {
                return this.Architectures.Count;
            }
        }

        public int ArchitectureTotalSize
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.JianzhuGuimo;
                }
                return num;
            }
        }

        public int CityTotalSize
        {
            get 
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    if (architecture.Kind.ID != 2 || architecture.Kind.ID != 4)
                    {
                        num += architecture.JianzhuGuimo;
                    }
                }
                return num;
            }
        }

        public long Army
        {
            get
            {
                long num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.ArmyQuantity;
                }
                foreach (Military m in this.TransferingMilitaries)
                {
                    num += m.Quantity;
                }
                foreach (Troop troop in this.Troops)
                {
                    num += troop.Quantity;
                }
                return num;
            }
        }

        public int ArmyScale
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.ArmyScale;
                }
                foreach (Troop troop in this.Troops)
                {
                    if (troop.Army != null)
                    {
                        num += troop.Army.Scales;
                    }
                }
                return num;
            }
        }

        public MilitaryKindTable AvailableMilitaryKinds
        {
            get
            {
                MilitaryKindTable table = new MilitaryKindTable();
                foreach (MilitaryKind kind in this.BaseMilitaryKinds.MilitaryKinds.Values)
                {
                    table.AddMilitaryKind(kind);
                }
                foreach (MilitaryKind kind in this.TechniqueMilitaryKinds.MilitaryKinds.Values)
                {
                    table.AddMilitaryKind(kind);
                }
                return table;
            }
        }

        public Architecture Capital
        {
            get
            {
                if (this.Architectures.Count == 0) return null;
                if (this.CapitalID == -1)
                {
                    this.CapitalID = this.Architectures[GameObject.Random(this.Architectures.Count)].ID;
                }
                if (this.capital == null)
                {
                    this.capital = Session.Current.Scenario.Architectures.GetGameObject(this.capitalID) as Architecture;
                }
                return this.capital;
            }
            set
            {
                this.capital = value;
                if (this.capital != null)
                {
                    this.capitalID = this.capital.ID;
                }
                else
                {
                    this.capitalID = -1;
                }
            }
        }
        [DataMember]
        public int CapitalID
        {
            get
            {
                return this.capitalID;
            }
            set
            {
                this.capitalID = value;
                this.capital = null;
            }
        }

        public string CapitalName
        {
            get
            {
                if (this.Capital != null)
                {
                    return this.Capital.Name;
                }
                return "----";
            }
        }

        public int CaptiveCount
        {
            get
            {
                return this.Captives.Count;
            }
        }
        [DataMember]
        public int ColorIndex
        {
            get
            {
                return this.colorIndex;
            }
            set
            {
                this.colorIndex = value;
            }
        }
        
        public bool Controlling
        {
            get
            {
                return this.controlling;
            }
            set
            {
                this.controlling = value;
            }
        }

        public int GetFacilityKindCount(int id)
        {
            int cnt = 0;
            foreach (Architecture a in this.Architectures)
            {
                cnt += a.GetFacilityKindCount(id);
            }
            return cnt;
        }

        public Section FirstSection
        {
            get
            {
                if (this.SectionCount > 0)
                {
                    return (this.Sections[0] as Section);
                }
                return this.CreateFirstSection();
            }
        }

        public long Food
        {
            get
            {
                long num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.Food;
                }
                return num;
            }
        }

        public int FriendlyDiplomaticRelationCount
        {
            get
            {
                int num = 0;
                foreach (DiplomaticRelation relation in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(base.ID))
                {
                    if (relation.Relation >= Session.GlobalVariables.FriendlyDiplomacyThreshold)
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public int Fund
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.Fund;
                }
                return num;
            }
        }

        public float GetCurrentRoutewayConsumptionRate
        {
            get
            {
                return this.RoutewayPathBuilder.PathConsumptionRate;
            }
        }

        public bool HasFriendlyDiplomaticRelation
        {
            get
            {
                foreach (DiplomaticRelation relation in Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(base.ID))
                {
                    if (relation.Relation >= Session.GlobalVariables.FriendlyDiplomacyThreshold)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public int feiziCount()
        {
            int r = 0;
            foreach (Architecture a in this.Architectures)
            {
                r += a.Feiziliebiao.Count;
            }
            return r;
        }

        public PersonList GetFeiziList()
        {
            PersonList result = new PersonList();
            foreach (Architecture a in this.Architectures)
            {
                foreach (Person p in a.Feiziliebiao)
                {
                    result.Add(p);
                }
            }
            return result;
        }

        public int meinvkongjian()
        {
            int r = 0;
            foreach (Architecture a in this.Architectures)
            {
                r += a.Meinvkongjian;
            }
            return r;
        }

        public int InformationCount
        {
            get
            {
                return this.Informations.Count;
            }
        }

        private float InternalSurplusRateCache = -1;
        public float InternalSurplusRate
        {
            get
            {
                if (InternalSurplusRateCache > 0)
                    return InternalSurplusRateCache;

                if ((!Session.Current.Scenario.IsPlayer(this) && !Session.GlobalVariables.internalSurplusRateForAI) || (Session.Current.Scenario.IsPlayer(this) && !Session.GlobalVariables.internalSurplusRateForPlayer))
                {
                    InternalSurplusRateCache = 1;
                    return 1;
                }

                float num = (Session.Parameters.InternalSurplusFactor - this.Power) / (float) Session.Parameters.InternalSurplusFactor;

                if (num < 0.2f)
                {
                    num = 0.2f;
                }

                InternalSurplusRateCache = num;
                return num;
            }
        }

        public string InternalSurplusRatePercentString
        {
            get
            {
                return StaticMethods.GetPercentString(this.InternalSurplusRate, 3);
            }
        }

        public Person Leader
        {
            get
            {
                if (this.leaderID == -1)
                {
                    this.leaderID = this.Persons.GetMaxMeritPerson().ID;
                }
                if (this.leader == null && Session.Current.Scenario != null && Session.Current.Scenario.Persons != null)
                {
                    this.leader = Session.Current.Scenario.Persons.GetGameObject(this.LeaderID) as Person;
                }
                return this.leader;
            }
            set
            {
                this.leader = value;
                if (this.leader != null)
                {
                    this.LeaderID = this.leader.ID;
                }
                else
                {
                    this.LeaderID = -1;
                }
            }
        }
        [DataMember]
        public int LeaderID
        {
            get
            {
                return this.leaderID;
            }
            set
            {
                this.leaderID = value;
            }
        }

        public string LeaderName
        {
            get
            {
                return ((this.Leader != null) ? this.Leader.Name : "----");
            }
        }
        // [DataMember]//取消储君序列化，原有的方法会导致二次存档后储君为空
        public Person Prince
        {
            get
            {
                if (this.princeID == -1) return null;

                if (this.prince == null)
                {
                    this.prince = Session.Current.Scenario.Persons.GetGameObject(this.PrinceID) as Person;
                }

                //检查储君有效性
                if (this.prince != null && (this.prince == this.Leader || !this.prince.Alive || !this.prince.Available || this.prince.BelongedFaction != this 
                    || this.prince.BelongedFaction == null))
                {
                    this.Prince = null;
                }
                return this.prince;
            }
            set
            {
                this.prince = value;
                if (this.prince != null)
                {
                    this.PrinceID = this.prince.ID;
                }
                else
                {
                    this.PrinceID = -1;
                }
            }
        }
        [DataMember]
        public int PrinceID
        {
            get
            {
                return this.princeID;
            }
            set
            {
                this.princeID = value;
            }
        }

        public string PrinceName
        {
            get
            {
                return ((this.Prince != null) ? this.Prince.Name : "----");
            }
        }

        public int LegionCount
        {
            get
            {
                return this.Legions.Count;
            }
        }
        [DataMember]
        public string MilitariesString { get; set; }

        public MilitaryList Militaries
        {
            get
            {
                MilitaryList list = new MilitaryList();
                /*
                foreach (Military military in Session.Current.Scenario.Militaries)
                {
                    if (military.BelongedArchitecture != null && military.BelongedArchitecture.BelongedFaction == this)
                    {
                        Militaries.Add(military);
                    }

                }*/
                if (this.Architectures != null)
                {
                    foreach (Architecture a in this.Architectures)
                    {
                        foreach (Military military in a.Militaries)
                        {
                            list.Add(military);
                        }
                    }
                }

                if (this.TransferingMilitaries != null)
                {
                    foreach (Military military in this.TransferingMilitaries)
                    {
                        list.Add(military);
                    }
                }

                if (this.Troops != null)
                {
                    foreach (Troop troop in this.Troops)
                    {
                        if (troop.Army != null)
                        {
                            if (troop.Army.ShelledMilitary == null)
                            {
                                list.Add(troop.Army);
                            }
                            else
                            {
                                list.Add(troop.Army.ShelledMilitary);
                            }
                        }
                    }
                }

                return list;

            }
        }

        [DataMember]
        public int MilitaryCount
        {
            get
            {
                return this.Militaries.Count ;
            }
            set
            {
                this.militarycount = value;
            }
        }
        [DataMember]
        public int TransferingMilitaryCount
        {
            get
            {
                return this.TransferingMilitaries.Count;
            }
            set
            {
                this.transferingmilitarycount = value;
            }
        }

      
        public bool Passed
        {
            get
            {
                return this.passed;
            }
            set
            {
                this.passed = value;
            }
        }

        public int PersonCount
        {
            // 切记不要用this.Persons.Count，因为这样会把全势力人物一个一个加入PersonList，非常慢
            get
            {
                int result = 0;
                foreach (Architecture a in Architectures)
                {
                    result += a.Persons.Count;
                    result += a.MovingPersons.Count;
                }
                foreach (Troop t in Troops)
                {
                    result += t.PersonCount;
                }
                foreach (Captive c in Session.Current.Scenario.Captives)
                {
                    if (c.CaptiveFaction == this)
                    {
                        result++;
                    }
                }
                
                
                return result;
            }
        }

        public PersonList SelfOfficers //自势力野武将
        {
            get 
            {
                PersonList result = new PersonList();
                foreach (Person person in this.Persons)
                {
                    if (person.ID >= 25000)
                    {
                        result .Add (person);
                    }
                }
                return result;
            }
        }


        public int SelfOfficerCount //本势力野武将总数
        {
            get
            {
                return (this.SelfOfficers.Count);

            }
        }

        public int Population
        {
            get
            {
                int num = 0;
                foreach (Architecture architecture in this.Architectures)
                {
                    num += architecture.Population;
                }
                return num;
            }
        }
        
        public bool PreUserControlFinished
        {
            get
            {
                return this.preUserControlFinished;
            }
            set
            {
                this.preUserControlFinished = value;
            }
        }
        [DataMember]
        public int Reputation
        {
            get
            {
                return this.reputation;
            }
            set
            {
                this.reputation = value;
            }
        }

        public int RoutewayCount
        {
            get
            {
                return this.Routeways.Count;
            }
        }

        public int RoutewayWorkForce
        {
            get
            {
                return (100 + this.IncrementOfRoutewayWorkforce);
            }
        }

        public int[,] SecondTierMapCost
        {
            get
            {
                return this.secondTierMapCost;
            }
        }

        public int SectionCount
        {
            get
            {
                return this.Sections.Count;
            }
        }

        public int SelfCaptiveCount
        {
            get
            {
                return this.SelfCaptives.Count;
            }
        }
        [DataMember]
        public int TechniquePoint
        {
            get
            {
                return this.techniquePoint;
            }
            set
            {
                this.techniquePoint = value;
            }
        }
        [DataMember]
        public int TechniquePointForFacility
        {
            get
            {
                return this.techniquePointForFacility;
            }
            set
            {
                this.techniquePointForFacility = value;
            }
        }
        [DataMember]
        public int TechniquePointForTechnique
        {
            get
            {
                return this.techniquePointForTechnique;
            }
            set
            {
                this.techniquePointForTechnique = value;
            }
        }

        public int[,] ThirdTierMapCost
        {
            get
            {
                return this.thirdTierMapCost;
            }
        }

        public int TotalTechniquePoint
        {
            get
            {
                return ((this.techniquePoint + this.techniquePointForTechnique) + this.techniquePointForFacility);
            }
        }

        public int TroopCount
        {
            get
            {
                return this.Troops.Count;
            }
        }

        public int TroopCountExcludeTransport
        {
            get
            {
                int r = 0;
                foreach (Troop t in this.Troops)
                {
                    if (!t.IsTransport)
                    {
                        r++;
                    }
                }
                return r;
            }
        }
        [DataMember]
        public int UpgradingDaysLeft
        {
            get
            {
                return this.upgradingDaysLeft;
            }
            set
            {
                this.upgradingDaysLeft = value;
            }
        }
        [DataMember]
        public int UpgradingTechnique
        {
            get
            {
                return this.upgradingTechnique;
            }
            set
            {
                this.upgradingTechnique = value;
            }
        }

        private bool WantControl
        {
            get
            {
                bool stopToControl = this.StopToControl;
                if (!stopToControl)
                {
                    stopToControl = Session.Current.Scenario.Date.DaysLeft == 0;
                }
                return stopToControl;
            }
        }

        public int TotalPersonMerit
        {
            get
            {
                int result = 0;
                foreach (Person p in this.Persons)
                {
                    result += p.Merit;
                }
                return result;
            }
        }

        public int TotalPersonFightingForce
        {
            get
            {
                int result = 0;
                foreach (Person p in this.Persons)
                {
                    result += p.FightingForce;
                }
                return result;
            }
        }

        public bool hougongValid
        {
            get
            {
                return Session.GlobalVariables.hougongGetChildrenRate > 0 && (!Session.GlobalVariables.hougongAlienOnly || this.IsAlien);
            }
        }

        [DataMember]
        public int chaotinggongxiandu
        {
            get
            {
                return this.chaotinggongxiandudezhi;
            }
            set
            {
                this.chaotinggongxiandudezhi = value;
            }
        }
        [DataMember]
        public int guanjue
        {
            get
            {
                return this.guanjuedezhi;
            }
            set
            {
                this.guanjuedezhi = value;
            }
        }
        [DataMember]
        public bool IsAlien
        {
            get
            {
                return this.isAlien;
            }
            set
            {
                this.isAlien = value;
            }
        }

        public InformationList GetAllInformationList()
        {
            InformationList result = new InformationList();
            foreach (Information i in this.Informations)
            {
                result.Add(i);
            }
            foreach (Architecture a in this.Architectures)
            {
                foreach (Information i in a.Informations)
                {
                    result.Add(i);
                }
            }
            return result;
        }

        public Architecture GetGeDiArchitecture(Faction targetFaction)
        {
            Architecture result = null;
            int dist = int.MaxValue;

            foreach (Architecture a in this.Architectures)
            {
                if (a != this.Capital)
                {
                    int innerDist = int.MaxValue;
                    foreach (Architecture b in targetFaction.Architectures)
                    {
                        int d = Session.Current.Scenario.GetSimpleDistance(a.ArchitectureArea.Centre, b.ArchitectureArea.Centre);

                        if (d < innerDist)
                        {
                            d = innerDist;
                        }
                    }

                    if (innerDist < dist && !a.HasHostileTroopsInView())
                    {
                        dist = innerDist;
                        result = a;
                    }
                }
            }

            return result;
        }

        public bool HasInformation()
        {
            return this.GetAllInformationList().Count > 0;
        }

        public delegate void AfterCatchLeader(Person leader, Faction faction);

        [StructLayout(LayoutKind.Sequential)]
        public struct ClosedPathEndpoints
        {
            public Point Start;
            public Point End;
            public ClosedPathEndpoints(Point start, Point end)
            {
                this.Start = start;
                this.End = end;
            }

            public static bool operator !=(Faction.ClosedPathEndpoints a, Faction.ClosedPathEndpoints b)
            {
                return ((a.Start != b.Start) || (a.End != b.End));
            }

            public static bool operator ==(Faction.ClosedPathEndpoints a, Faction.ClosedPathEndpoints b)
            {
                return ((a.Start == b.Start) && (a.End == b.End));
            }

            public override int GetHashCode()
            {
                return (this.Start.GetHashCode() ^ this.End.GetHashCode());
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override string ToString()
            {
                return (this.Start.ToString() + " " + this.End.ToString());
            }
        }

        public delegate void FactionDestroy(Faction faction);

        public delegate void FactionUpgradeTechnique(Faction faction, Technique technique, Architecture architecture);

        public delegate void ForcedChangeCapital(Faction faction, Architecture oldCapital, Architecture newCapital);

        public delegate void GetControl(Faction faction);

        public delegate void InitiativeChangeCapital(Faction faction, Architecture oldCapital, Architecture newCapital);

        public delegate void TechniqueFinished(Faction faction, Technique technique);

        public int MaxPossibleReputation
        {
            get
            {
                int maxReputation = int.MinValue;
                foreach (guanjuezhongleilei i in Session.Current.Scenario.GameCommonData.suoyouguanjuezhonglei.Getguanjuedezhongleiliebiao())
                {
                    if (i.shengwangshangxian > maxReputation)
                    {
                        maxReputation = i.shengwangshangxian;
                    }
                }
                return maxReputation;
            }
        }

        public float InfluenceKindValue(int id)
        {
            float result = 0;
            foreach (Influence i in Session.Current.Scenario.GameCommonData.AllInfluences.Influences.Values)
            {
                if (i.Kind.ID == id)
                {
                    foreach (ApplyingFaction j in i.appliedFaction)
                    {
                        if (j.faction == this)
                        {
                            result += i.Value;
                        }
                    }
                }
            }
            return result;
        }

        public PersonList Children
        {
            get
            {
                HashSet<Person> result = new HashSet<Person>();
                foreach (Person p in Session.Current.Scenario.Persons)
                {
                    if (p.Alive && !p.Available && p.Age >= 0 && ((p.Father != null && p.Father.BelongedFactionWithPrincess == this) || (p.Mother != null && p.Mother.BelongedFactionWithPrincess == this))
                        && (p.ID < 7000 || p.ID > 8000))
                    {
                        if (!(p.Father != null && p.Father.Alive && (p.Father.BelongedFactionWithPrincess != this)))
                        {
                            result.Add(p);
                        }
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
    }
}

