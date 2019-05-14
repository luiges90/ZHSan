using GameObjects;
using GameObjects.Influences;
using GameObjects.Conditions;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using GameManager;
using Microsoft.Xna.Framework;
using GameGlobal;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class MilitaryKind : GameObject
    {
        private float fireDamageRate;
        private bool airOffence;
        private float architectureCounterDamageRate;
        private float architectureDamageRate;
        private bool arrowOffence;
        private TroopAttackDefaultKind attackDefaultKind;
        private TroopAttackTargetKind attackTargetKind;
        private bool beCountered;
        private bool canLevelUp;
        private TroopCastDefaultKind castDefaultKind;
        private TroopCastTargetKind castTargetKind;
        private int cliffAdaptability;
        private float cliffRate;
        private bool contactOffence;
        private bool counterOffence;
        private bool createBesideWater;
        private int createCost;
        private int createTechnology;
        private int defence;
        private int defencePer100Experience;
        private int defencePerScale;
        private string description;
        private int desertAdaptability;
        private float desertRate;
        private int foodPerSoldier;
        private int forrestAdaptability;
        private float forrestRate;
        private int grasslandAdaptability;
        private float grasslandRate;

        [DataMember]
        public string InfluencesString
        {
            get;
            set;
        }

        private int injuryChance;
        private bool isShell;
        private int levelUpExperience;

        private int marshAdaptability;
        private float marshRate;
        private int maxScale;
        private int merit;
        private int minScale;
        private int mountainAdaptability;
        private float mountainRate;
        private int movability;
        private bool obliqueOffence;
        private bool obliqueStratagem;
        private bool obliqueView;
        private int offence;
        private bool offenceOnlyBeforeMove;
        private int offencePer100Experience;
        private int offencePerScale;
        private int offenceRadius;
        private int oneAdaptabilityKind;
        private int plainAdaptability;
        private float plainRate;
        private int pointsPerSoldier;
        private int rationDays;
        private int ridgeAdaptability;
        private float ridgeRate;
        //[DataMember]
        public TroopSounds Sounds;
        private int speed;
        private int stratagemRadius;

        public TroopTextures Textures;
        private MilitaryType type;
        private int recruitLimit;
        private int viewRadius;
        private int wastelandAdaptability;
        private float wastelandRate;
        private int waterAdaptability;
        private float waterRate;
        [DataMember]
        public int zijinshangxian;

        public InfluenceTable Influences = new InfluenceTable();

        private List<int> levelUpKindID = new List<int>();

        private int titleInfluence = -1;

        private int morphToKindId = -1;

        public Dictionary<Condition, float> AICreateArchitectureConditionWeight = new Dictionary<Condition, float>();

        public Dictionary<Condition, float> AIUpgradeArchitectureConditionWeight = new Dictionary<Condition, float>();

        public Dictionary<Condition, float> AIUpgradeLeaderConditionWeight = new Dictionary<Condition, float>();

        public Dictionary<Condition, float> AILeaderConditionWeight = new Dictionary<Condition, float>();

        public ConditionTable CreateConditions = new ConditionTable();

        public PersonList Persons = new PersonList();

        public void Init()
        {
            Influences = new InfluenceTable();

            if (levelUpKindID == null || levelUpKindID.Count == 0)
            {
                levelUpKindID = new List<int>();
            }
            else
            {

            }

            titleInfluence = -1;

           // morphToKindId = -1;

            AICreateArchitectureConditionWeight = new Dictionary<Condition, float>();

            AIUpgradeArchitectureConditionWeight = new Dictionary<Condition, float>();

            AIUpgradeLeaderConditionWeight = new Dictionary<Condition, float>();

            AILeaderConditionWeight = new Dictionary<Condition, float>();

            CreateConditions = new ConditionTable();

            Persons = new PersonList();
        }

        [DataMember]
        public string CreateConditionsString
        {
            get;
            set;
        }

        [DataMember]
        public string AICreateArchitectureConditionWeightString
        {
            get;
            set;
        }

        [DataMember]
        public string AIUpgradeArchitectureConditionWeightString
        {
            get;
            set;
        }

        [DataMember]
        public string AIUpgradeLeaderConditionWeightString
        {
            get;
            set;
        }

        [DataMember]
        public string AILeaderConditionWeightString
        {
            get;
            set;
        }

        [DataMember]
        public string SuccessorString
        {
            get;
            set;
        }

        public MilitaryKindTable successor;
        private bool findSuccessor_visited;
        [DataMember]
        public int MinCommand { get; set; }
        
        [DataMember]
        public int ObtainProb
        {
            get;
            set;
        }

        public bool LevelUpAvail(Architecture a)
        {
            return this.CheckConditions(a) && this.GetLevelUpKinds(a).Count > 0;
        }

        public bool CreateAvail(Architecture a)
        {
            if (this.IsShell)
            {
                return false;
            }
            if ((a.Fund < (this.CreateCost * this.GetRateOfNewMilitary(a))) || (a.Technology < this.CreateTechnology))
            {
                return false;
            }
            if (a.BelongedFaction.IsMilitaryKindOverLimit(base.ID))
            {
                return false;
            }
            if (!(!this.CreateBesideWater || a.IsBesideWater))
            {
                return false;
            }
            if (!this.CheckConditions(a))
            {
                return false;
            }
            return true;
        }

        public bool IsTransport
        {
            get
            {
                return this.ID == 29;
            }
        }

        public MilitaryKind findSuccessorCreatable(MilitaryKindList allMilitaryKinds, Architecture recruiter)
        {
            foreach (MilitaryKind i in allMilitaryKinds)
            {
                i.findSuccessor_visited = false;
            }
            return findSuccessorRecruitable_r(allMilitaryKinds, recruiter, this);
        }

        private MilitaryKind findSuccessorRecruitable_r(MilitaryKindList allMilitaryKinds, Architecture recruiter, MilitaryKind prev)
        {
            if (prev.successor.GetMilitaryKindList().Count == 0)
            {
                return prev;
            }
            prev.findSuccessor_visited = true;
            MilitaryKindList toVisit = new MilitaryKindList();
            foreach (MilitaryKind i in prev.successor.GetMilitaryKindList())
            {
                if (!i.findSuccessor_visited && recruiter.GetNewMilitaryKindList().GameObjects.Contains(i) && allMilitaryKinds.GetList().GameObjects.Contains(i))
                {
                    toVisit.Add(i);
                }
            }
            if (toVisit.Count == 0)
            {
                return prev;
            }
            return findSuccessorRecruitable_r(allMilitaryKinds, recruiter, toVisit[GameObject.Random(toVisit.Count)] as MilitaryKind);
        }

        public GameObjectList GetInfluenceList()
        {
            return this.Influences.GetInfluenceList();
        }

        public float GetRateOfNewMilitary(Architecture a)
        {
            switch (this.Type)
            {
                case MilitaryType.步兵:
                    return a.RateOfNewBubingMilitaryFundCost;

                case MilitaryType.弩兵:
                    return a.RateOfNewNubingMilitaryFundCost;

                case MilitaryType.骑兵:
                    return a.RateOfNewQibingMilitaryFundCost;

                case MilitaryType.水军:
                    return a.RateOfNewShuijunMilitaryFundCost;

                case MilitaryType.器械:
                    return a.RateOfNewQixieMilitaryFundCost;
            }
            return 1f;
        }

        public int[] Adaptabilities
        {
            get
            {
                return new int[]{this.PlainAdaptability, this.GrasslandAdaptability, this.ForrestAdaptability, this.WastelandAdaptability, this.MarshAdaptability,
                    this.MountainAdaptability, this.CliffAdaptability, this.RidgeAdaptability, this.WaterAdaptability};
            }
        }

        public bool Movable
        {
            get
            {
                foreach (int i in this.Adaptabilities) 
                {
                    if (this.Movability >= i)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public int GetTerrainAdaptability(TerrainKind terrain)
        {
            switch (terrain)
            {
                case TerrainKind.无:
                    return 0xdac;

                case TerrainKind.平原:
                    return this.PlainAdaptability;

                case TerrainKind.草原:
                    return this.GrasslandAdaptability;

                case TerrainKind.森林:
                    return this.ForrestAdaptability;

                case TerrainKind.湿地:
                    return this.MarshAdaptability;

                case TerrainKind.山地:
                    return this.MountainAdaptability;

                case TerrainKind.水域:
                    return this.WaterAdaptability;

                case TerrainKind.峻岭:
                    return this.RidgeAdaptability;

                case TerrainKind.荒地:
                    return this.WastelandAdaptability;

                case TerrainKind.沙漠:
                    return this.DesertAdaptability;

                case TerrainKind.栈道:
                    return this.CliffAdaptability;
            }
            return 0xdac;
        }

        public bool IsMovableOnPosition(Point position)
        {
            return (this.GetTerrainAdaptability(Session.Current.Scenario.GetTerrainKindByPosition(position)) <= this.Movability);
        }

        public override string ToString()
        {
            return (base.Name + "  " + this.Type.ToString());
        }
        [DataMember]
        public float FireDamageRate
        {
            get
            {
                return this.fireDamageRate;
            }
            set
            {
                this.fireDamageRate = value;
            }
        }
        [DataMember]
        public bool AirOffence
        {
            get
            {
                return this.airOffence;
            }
            set
            {
                this.airOffence = value;
            }
        }
        [DataMember]
        public float ArchitectureCounterDamageRate
        {
            get
            {
                return this.architectureCounterDamageRate;
            }
            set
            {
                this.architectureCounterDamageRate = value;
            }
        }
        [DataMember]
        public float ArchitectureDamageRate
        {
            get
            {
                return this.architectureDamageRate;
            }
            set
            {
                this.architectureDamageRate = value;
            }
        }
        [DataMember]
        public bool ArrowOffence
        {
            get
            {
                return this.arrowOffence;
            }
            set
            {
                this.arrowOffence = value;
            }
        }

        public string ArrowOffenceString
        {
            get
            {
                return (this.arrowOffence ? "○" : "×");
            }
        }
        [DataMember]
        public TroopAttackDefaultKind AttackDefaultKind
        {
            get
            {
                return this.attackDefaultKind;
            }
            set
            {
                this.attackDefaultKind = value;
            }
        }
        [DataMember]
        public TroopAttackTargetKind AttackTargetKind
        {
            get
            {
                return this.attackTargetKind;
            }
            set
            {
                this.attackTargetKind = value;
            }
        }
        [DataMember]
        public bool BeCountered
        {
            get
            {
                return this.beCountered;
            }
            set
            {
                this.beCountered = value;
            }
        }

        public string BeCounteredString
        {
            get
            {
                return (this.beCountered ? "○" : "×");
            }
        }
        [DataMember]
        public bool CanLevelUp
        {
            get
            {
                return this.canLevelUp;
            }
            set
            {
                this.canLevelUp = value;
            }
        }

        public string CanLevelUpString
        {
            get
            {
                return (this.CanLevelUp ? "○" : "×");
            }
        }
        [DataMember]
        public TroopCastDefaultKind CastDefaultKind
        {
            get
            {
                return this.castDefaultKind;
            }
            set
            {
                this.castDefaultKind = value;
            }
        }
        [DataMember]
        public TroopCastTargetKind CastTargetKind
        {
            get
            {
                return this.castTargetKind;
            }
            set
            {
                this.castTargetKind = value;
            }
        }
        [DataMember]
        public int CliffAdaptability
        {
            get
            {
                return this.cliffAdaptability;
            }
            set
            {
                this.cliffAdaptability = value;
            }
        }
        [DataMember]
        public float CliffRate
        {
            get
            {
                return this.cliffRate;
            }
            set
            {
                this.cliffRate = value;
            }
        }
        [DataMember]
        public bool ContactOffence
        {
            get
            {
                return this.contactOffence;
            }
            set
            {
                this.contactOffence = value;
            }
        }

        public string ContactOffenceString
        {
            get
            {
                return (this.contactOffence ? "○" : "×");
            }
        }
        [DataMember]
        public bool CounterOffence
        {
            get
            {
                return this.counterOffence;
            }
            set
            {
                this.counterOffence = value;
            }
        }

        public string CounterOffenceString
        {
            get
            {
                return (this.counterOffence ? "○" : "×");
            }
        }
        [DataMember]
        public bool CreateBesideWater
        {
            get
            {
                return this.createBesideWater;
            }
            set
            {
                this.createBesideWater = value;
            }
        }

        public string CreateBesideWaterString
        {
            get
            {
                return (this.CreateBesideWater ? "○" : "×");
            }
        }
        [DataMember]
        public int CreateCost
        {
            get
            {
                return this.createCost;
            }
            set
            {
                this.createCost = value;
            }
        }
        [DataMember]
        public int CreateTechnology
        {
            get
            {
                return this.createTechnology;
            }
            set
            {
                this.createTechnology = value;
            }
        }
        [DataMember]
        public int Defence
        {
            get
            {
                return this.defence;
            }
            set
            {
                this.defence = value;
            }
        }
        [DataMember]
        public int DefencePer100Experience
        {
            get
            {
                return this.defencePer100Experience;
            }
            set
            {
                this.defencePer100Experience = value;
            }
        }
        [DataMember]
        public int DefencePerScale
        {
            get
            {
                return this.defencePerScale;
            }
            set
            {
                this.defencePerScale = value;
            }
        }
        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }
        [DataMember]
        public int DesertAdaptability
        {
            get
            {
                return this.desertAdaptability;
            }
            set
            {
                this.desertAdaptability = value;
            }
        }
        [DataMember]
        public float DesertRate
        {
            get
            {
                return this.desertRate;
            }
            set
            {
                this.desertRate = value;
            }
        }
        [DataMember]
        public int FoodPerSoldier
        {
            get
            {
                return this.foodPerSoldier;
            }
            set
            {
                this.foodPerSoldier = value;
            }
        }
        [DataMember]
        public int ForrestAdaptability
        {
            get
            {
                return this.forrestAdaptability;
            }
            set
            {
                this.forrestAdaptability = value;
            }
        }
        [DataMember]
        public float ForrestRate
        {
            get
            {
                return this.forrestRate;
            }
            set
            {
                this.forrestRate = value;
            }
        }
        [DataMember]
        public int GrasslandAdaptability
        {
            get
            {
                return this.grasslandAdaptability;
            }
            set
            {
                this.grasslandAdaptability = value;
            }
        }
        [DataMember]
        public float GrasslandRate
        {
            get
            {
                return this.grasslandRate;
            }
            set
            {
                this.grasslandRate = value;
            }
        }

        public int InfluenceCount
        {
            get
            {
                return this.Influences.Count;
            }
        }
        [DataMember]
        public int InjuryChance
        {
            get
            {
                return this.injuryChance;
            }
            set
            {
                this.injuryChance = value;
            }
        }
        [DataMember]
        public bool IsShell
        {
            get
            {
                return this.isShell;
            }
            set
            {
                this.isShell = value;
            }
        }

        public string IsShellString
        {
            get
            {
                return (this.IsShell ? "○" : "×");
            }
        }
        [DataMember]
        public int LevelUpExperience
        {
            get
            {
                return this.levelUpExperience;
            }
            set
            {
                this.levelUpExperience = value;
            }
        }
        [DataMember]
        public List<int> LevelUpKindID
        {
            get
            {
                return this.levelUpKindID;
            }
            set
            {
                this.levelUpKindID = value;
                this.levelUpKindID.RemoveAll(i => i == -1);
            }
        }

        public List<MilitaryKind> GetLevelUpKinds(Architecture a)
        {
            List<MilitaryKind> result = new List<MilitaryKind>();
            foreach (int id in LevelUpKindID) 
            {
                if (!a.BelongedFaction.IsMilitaryKindOverLimit(id))
                {
                    result.Add(Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(id));
                }
            }
            return result;
        }
        [DataMember]
        public int MarshAdaptability
        {
            get
            {
                return this.marshAdaptability;
            }
            set
            {
                this.marshAdaptability = value;
            }
        }
        [DataMember]
        public float MarshRate
        {
            get
            {
                return this.marshRate;
            }
            set
            {
                this.marshRate = value;
            }
        }
        [DataMember]
        public int MaxScale
        {
            get
            {
                return this.maxScale;
            }
            set
            {
                this.maxScale = value;
            }
        }
        [DataMember]
        public int Merit
        {
            get
            {
                return this.merit;
            }
            set
            {
                this.merit = value;
            }
        }
        [DataMember]
        public int MinScale
        {
            get
            {
                return this.minScale;
            }
            set
            {
                this.minScale = value;
            }
        }
        [DataMember]
        public int MountainAdaptability
        {
            get
            {
                return this.mountainAdaptability;
            }
            set
            {
                this.mountainAdaptability = value;
            }
        }
        [DataMember]
        public float MountainRate
        {
            get
            {
                return this.mountainRate;
            }
            set
            {
                this.mountainRate = value;
            }
        }
        [DataMember]
        public int Movability
        {
            get
            {
                return this.movability;
            }
            set
            {
                this.movability = value;
            }
        }
        [DataMember]
        public bool ObliqueOffence
        {
            get
            {
                return this.obliqueOffence;
            }
            set
            {
                this.obliqueOffence = value;
            }
        }

        public string ObliqueOffenceString
        {
            get
            {
                return (this.obliqueOffence ? "○" : "×");
            }
        }
        [DataMember]
        public bool ObliqueStratagem
        {
            get
            {
                return this.obliqueStratagem;
            }
            set
            {
                this.obliqueStratagem = value;
            }
        }

        public string ObliqueStratagemString
        {
            get
            {
                return (this.ObliqueStratagem ? "○" : "×");
            }
        }
        [DataMember]
        public bool ObliqueView
        {
            get
            {
                return this.obliqueView;
            }
            set
            {
                this.obliqueView = value;
            }
        }

        public string ObliqueViewString
        {
            get
            {
                return (this.obliqueView ? "○" : "×");
            }
        }
        [DataMember]
        public int Offence
        {
            get
            {
                return this.offence;
            }
            set
            {
                this.offence = value;
            }
        }
        [DataMember]
        public bool OffenceOnlyBeforeMove
        {
            get
            {
                return this.offenceOnlyBeforeMove;
            }
            set
            {
                this.offenceOnlyBeforeMove = value;
            }
        }

        public string OffenceOnlyBeforeMoveString
        {
            get
            {
                return (this.offenceOnlyBeforeMove ? "○" : "×");
            }
        }
        [DataMember]
        public int OffencePer100Experience
        {
            get
            {
                return this.offencePer100Experience;
            }
            set
            {
                this.offencePer100Experience = value;
            }
        }
        [DataMember]
        public int OffencePerScale
        {
            get
            {
                return this.offencePerScale;
            }
            set
            {
                this.offencePerScale = value;
            }
        }
        [DataMember]
        public int OffenceRadius
        {
            get
            {
                return this.offenceRadius;
            }
            set
            {
                this.offenceRadius = value;
            }
        }
        [DataMember]
        public int OneAdaptabilityKind
        {
            get
            {
                return this.oneAdaptabilityKind;
            }
            set
            {
                this.oneAdaptabilityKind = value;
            }
        }
        [DataMember]
        public int PlainAdaptability
        {
            get
            {
                return this.plainAdaptability;
            }
            set
            {
                this.plainAdaptability = value;
            }
        }
        [DataMember]
        public float PlainRate
        {
            get
            {
                return this.plainRate;
            }
            set
            {
                this.plainRate = value;
            }
        }
        [DataMember]
        public int PointsPerSoldier
        {
            get
            {
                return this.pointsPerSoldier;
            }
            set
            {
                this.pointsPerSoldier = value;
            }
        }
        [DataMember]
        public int RationDays
        {
            get
            {
                return this.rationDays;
            }
            set
            {
                this.rationDays = value;
            }
        }
        [DataMember]
        public int RidgeAdaptability
        {
            get
            {
                return this.ridgeAdaptability;
            }
            set
            {
                this.ridgeAdaptability = value;
            }
        }
        [DataMember]
        public float RidgeRate
        {
            get
            {
                return this.ridgeRate;
            }
            set
            {
                this.ridgeRate = value;
            }
        }
        [DataMember]
        public int Speed
        {
            get
            {
                return this.speed;
            }
            set
            {
                this.speed = value;
            }
        }
        [DataMember]
        public int StratagemRadius
        {
            get
            {
                return this.stratagemRadius;
            }
            set
            {
                this.stratagemRadius = value;
            }
        }
        [DataMember]
        public int TitleInfluence
        {
            get
            {
                return this.titleInfluence;
            }
            set
            {
                this.titleInfluence = value;
            }
        }
        [DataMember]
        public MilitaryType Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }
        [DataMember]
        public int RecruitLimit
        {
            get
            {
                return this.recruitLimit;
            }
            set
            {
                this.recruitLimit = value;
            }
        }
        [DataMember]
        public int ViewRadius
        {
            get
            {
                return this.viewRadius;
            }
            set
            {
                this.viewRadius = value;
            }
        }
        [DataMember]
        public int WastelandAdaptability
        {
            get
            {
                return this.wastelandAdaptability;
            }
            set
            {
                this.wastelandAdaptability = value;
            }
        }
        [DataMember]
        public float WastelandRate
        {
            get
            {
                return this.wastelandRate;
            }
            set
            {
                this.wastelandRate = value;
            }
        }
        [DataMember]
        public int WaterAdaptability
        {
            get
            {
                return this.waterAdaptability;
            }
            set
            {
                this.waterAdaptability = value;
            }
        }
        [DataMember]
        public float WaterRate
        {
            get
            {
                return this.waterRate;
            }
            set
            {
                this.waterRate = value;
            }
        }
        [DataMember]
        public int MorphToKindId
        {
            get
            {
                return this.morphToKindId;
            }
            set
            {
                this.morphToKindId = value;
            }
        }

        public MilitaryKind MorphTo
        {
            get
            {
                if (!Session.Current.Scenario.GameCommonData.AllMilitaryKinds.MilitaryKinds.ContainsKey(morphToKindId)) return null;
                return Session.Current.Scenario.GameCommonData.AllMilitaryKinds.MilitaryKinds[this.morphToKindId];
            }
        }

        public bool CheckConditions(Architecture a)
        {
            return Condition.CheckConditionList(this.CreateConditions.Conditions.Values, a);
        }
        /*
        public int EachMilitaryKindCount(Faction f)
        {
            int count = 0;
           // MilitaryKind mk = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(id);
            if (f != null)
            {
                foreach (Military military in f.Militaries)
                {
                    if (military.RealKindID == this.ID )
                    {
                        count++;
                    }
                }
            }
            
            return count;
        }
        */
    }
}
