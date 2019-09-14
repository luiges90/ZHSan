using GameGlobal;
using GameObjects.TroopDetail;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;
using GameManager;

namespace GameObjects
{
    [DataContract]
    public class Military : GameObject
    {
        private Architecture belongedArchitecture;
        public Faction BelongedFaction;
        public Troop BelongedTroop;
        private int combativity;
        private float experience;
        private Person followedLeader;
        private int followedLeaderID = -1;
        private int injuryQuantity;
        private MilitaryKind kind;
        [DataMember]
        private int kindID;
        private Person leader;
        [DataMember]
        private int leaderExperience;
        private int leaderID = -1;
        private int morale;
        private int quantity;
        private Person recruitmentPerson;
        public Military ShelledMilitary;

        [DataMember]
        public int ShelledMilitaryID;

        public Military ShellingMilitary;

        private int tiredness;

        private int arrivingDays;
        private Architecture startingArchitecture;
        private Architecture targetArchitecture;
        private int startingArchitectureID = -1;
        private int targetArchitectureID = -1;

        public void Init()
        {
        }

        [DataMember]
        public int RoutCount { get; set; }
        [DataMember]
        public int YearCreated { get; set; }
        [DataMember]
        public int TroopDamageDealt { get; set; }
        [DataMember]
        public int TroopBeDamageDealt { get; set; }
        [DataMember]
        public int ArchitectureDamageDealt { get; set; }
        [DataMember]
        public int OfficerKillCount { get; set; }
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

        [DataMember]
        public int belongedArchitectureID;

        public int KindMerit
        {
            get
            {
                return this.Kind.Merit;
            }
        }

        public Architecture BelongedArchitecture
        {
            get
            {
                if (belongedArchitectureID == -1) return null;

                if (belongedArchitecture == null)
                {
                    belongedArchitecture = (Architecture) Session.Current.Scenario.Architectures.GetGameObject(belongedArchitectureID);
                }
                return belongedArchitecture;
            }
            set
            {
                belongedArchitecture = value;
                if (value != null)
                {
                    belongedArchitectureID = value.ID;
                } else
                {
                    belongedArchitectureID = -1;
                }
            }
        }

        [DataMember]
        public int Tiredness
        {
            get
            {
                return tiredness;
            }
            set
            {
                tiredness = value;
            }
        }

        public int Merit
        {
            get
            {
                return this.Kind.Merit * 2000 + this.Experience * 3 + (this.FollowedLeaderID >= 0 ? 1000 : this.LeaderExperience) * 3;
            }
        }

        public bool IsTransport
        {
            get
            {
                return this.Kind.IsTransport;
            }
        }

        public void ApplyFollowedLeader(Troop troop)
        {
            if (this.FollowedLeader == troop.Leader)
            {
                troop.RateOfOffence += Session.Parameters.FollowedLeaderOffenceRateIncrement;
                troop.RateOfDefence += Session.Parameters.FollowedLeaderDefenceRateIncrement;
            }
        }

        public static Military Create(Architecture architecture, MilitaryKind kind)
        {
            Military military = new Military();
            
            military.KindID = kind.ID;
            military.ID = Session.Current.Scenario.Militaries.GetFreeGameObjectID();
            if (kind.RecruitLimit == 1)
            {
                military.Name = kind.Name;
            }
            else
            {
                military.Name = kind.Name + "队";
            }
            architecture.AddMilitary(military);
            architecture.BelongedFaction.AddMilitary(military);
            Session.Current.Scenario.Militaries.AddMilitary(military);
            architecture.DecreaseFund((int) (kind.CreateCost * kind.GetRateOfNewMilitary(architecture)));
            if (kind.IsTransport)
            {
                military.Quantity = kind.MaxScale;
                military.Morale = military.MoraleCeiling;
                military.Combativity = military.CombativityCeiling;
            }
            military.YearCreated = Session.Current.Scenario.Date.Year;
            return military;
        }

        public int DecreaseCombativity(int decrement)
        {
            if (this.Combativity < decrement)
            {
                decrement = this.Combativity;
            }
            this.Combativity -= decrement;
            return decrement;
        }

        public void DecreaseInjuryQuantity(int decrement)
        {
            this.InjuryQuantity -= decrement;
            if (this.InjuryQuantity < 0)
            {
                this.InjuryQuantity = 0;
            }
        }

        public int DecreaseMorale(int decrement)
        {
            if (this.Morale < decrement)
            {
                decrement = this.Morale;
            }
            this.Morale -= decrement;
            return decrement;
        }

        public bool DecreaseQuantity(int decrement)
        {
            this.Quantity -= decrement;
            if (this.Quantity <= 0)
            {
                this.Quantity = 0;
                return true;
            }
            return false;
        }

        public int GetTerrainAdaptability(TerrainKind terrain)
        {
            switch (terrain)
            {
                case TerrainKind.无:
                    return 0xdac;

                case TerrainKind.平原:
                    return this.Kind.PlainAdaptability;

                case TerrainKind.草原:
                    return this.Kind.GrasslandAdaptability;

                case TerrainKind.森林:
                    return this.Kind.ForrestAdaptability;

                case TerrainKind.湿地:
                    return this.Kind.MarshAdaptability;

                case TerrainKind.山地:
                    return this.Kind.MountainAdaptability;

                case TerrainKind.水域:
                    return this.Kind.WaterAdaptability;

                case TerrainKind.峻岭:
                    return this.Kind.RidgeAdaptability;

                case TerrainKind.荒地:
                    return this.Kind.WastelandAdaptability;

                case TerrainKind.沙漠:
                    return this.Kind.DesertAdaptability;

                case TerrainKind.栈道:
                    return this.Kind.CliffAdaptability;
            }
            return 0xdac;
        }

        public int IncreaseCombativity(int increment)
        {
            if ((this.Combativity + increment) > this.CombativityCeiling)
            {
                increment = this.CombativityCeiling - this.Combativity;
            }
            this.Combativity += increment;
            return increment;
        }

        public void IncreaseExperience(int increment)
        {
            if (this.ShelledMilitary == null)
            {
                this.experience += increment * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIArmyExperienceRate);
                if (this.experience > Session.GlobalVariables.MaxMilitaryExperience)
                {
                    this.experience = Session.GlobalVariables.MaxMilitaryExperience;
                }
            }
            else
            {
                this.ShelledMilitary.experience += increment * (Session.Current.Scenario.IsPlayer(this.BelongedFaction) ? 1 : Session.Parameters.AIArmyExperienceRate);
                if (this.ShelledMilitary.experience > Session.GlobalVariables.MaxMilitaryExperience)
                {
                    this.ShelledMilitary.experience = Session.GlobalVariables.MaxMilitaryExperience;
                }
            }
        }

        public void IncreaseInjuryQuantity(int increment)
        {
            if (increment > 0)
            {
                this.InjuryQuantity += increment;
            }
        }

        public bool IncreaseLeaderExperience(int increment)
        {
            if (this.LeaderID != this.FollowedLeaderID)
            {
                this.LeaderExperience += increment;
                if (this.LeaderExperience >= 0x3e8)
                {
                    this.LeaderExperience = 0;
                    this.FollowedLeader = this.Leader;
                    return true;
                }
            }
            return false;
        }

        public int IncreaseMorale(int increment)
        {
            if ((this.Morale + increment) > this.MoraleCeiling)
            {
                increment = this.MoraleCeiling - this.Morale;
            }
            this.Morale += increment;
            return increment;
        }

        public bool IncreaseQuantity(int increment)
        {
            this.Quantity += increment;
            if (this.Quantity >= this.Kind.MaxScale)
            {
                this.Quantity = this.Kind.MaxScale;
                return true;
            }
            return false;
        }

        public bool IncreaseQuantity(int increment, int morale, int combativity, int experience, int leaderExperience)
        {
            if (increment > 0)
            {
                this.Morale = ((this.Quantity * this.Morale) + (increment * morale)) / (increment + this.Quantity);
                this.Combativity = ((this.Quantity * this.Combativity) + (increment * combativity)) / (increment + this.Quantity);
                this.Experience = ((this.Quantity * this.Experience) + (increment * experience)) / (increment + this.Quantity);
                this.LeaderExperience = ((this.Quantity * this.LeaderExperience) + (increment * leaderExperience)) / (increment + this.Quantity);
            }
            return this.IncreaseQuantity(increment);
        }

        public bool IsFollowedLeader(Person person)
        {
            return (person.ID == this.FollowedLeaderID);
        }

        public void ModifyAreaByTerrainAdaptablity(GameArea area)
        {
            for (int i = area.Count - 1; i >= 0; i--)
            {
                Architecture architectureByPosition = Session.Current.Scenario.GetArchitectureByPosition(area[i]);
                if (((architectureByPosition == null) || (this.BelongedFaction != architectureByPosition.BelongedFaction))
                    && this.GetTerrainAdaptability(Session.Current.Scenario.GetTerrainKindByPosition(area[i])) > this.Kind.Movability)
                {
                        area.Area.RemoveAt(i);
                } 
                else if (Session.Current.Scenario.GetWaterPositionMapCost(this.Kind, area[i]) >= 0xdac)
                {
                    area.Area.RemoveAt(i);
                }
            }
        }

        public void Recovery(int multiple)
        {
            if (this.InjuryQuantity > 0)
            {
                int decrement = (this.Kind.MinScale * multiple) / 2;
                if (decrement > this.InjuryQuantity)
                {
                    decrement = this.InjuryQuantity;
                }
                this.DecreaseInjuryQuantity(decrement);
                this.IncreaseQuantity(decrement);
            }
        }

        public int LoseInjuredTroop(float rate)
        {
            if (this.InjuryQuantity > 0)
            {
                int decrement = (int)(this.Kind.MinScale * rate);
                if (decrement > this.InjuryQuantity)
                {
                    decrement = this.InjuryQuantity;
                }
                this.DecreaseInjuryQuantity(decrement);
                //this.IncreaseQuantity(decrement);
                return decrement;
            }
            return 0;
        }

        public int Recovery(float rate)
        {
            if (this.InjuryQuantity > 0)
            {
                int decrement = (int) (this.Kind.MinScale * rate);
                if (decrement > this.InjuryQuantity)
                {
                    decrement = this.InjuryQuantity;
                }
                this.DecreaseInjuryQuantity(decrement);
                this.IncreaseQuantity(decrement);
                return decrement;
            }
            return 0;
        }

        public void SetShelledMilitary(Military military)
        {
            if (this.ShelledMilitary != null)
            {
                this.ShelledMilitary.ShellingMilitary = null;
            }
            this.ShelledMilitary = military;
            if (military != null)
            {
                military.ShellingMilitary = this;
            }
        }

        public static Military SimCreate(Architecture architecture, MilitaryKind kind)
        {
            Military military = new Military();
            military.KindID = kind.ID;
            military.ID = Session.Current.Scenario.Militaries.GetFreeGameObjectID();
            if (kind.RecruitLimit == 1)
            {
                military.Name = kind.Name;
                return military;
            }
            military.Name = kind.Name + "队";
            return military;
        }

        public void SimulateSetLeader(Person person)
        {
            if (person != null)
            {
                if (this.ShelledMilitary == null)
                {
                    this.leader = person;
                    this.leaderID = person.ID;
                }
                else
                {
                    this.ShelledMilitary.leader = person;
                    this.ShelledMilitary.leaderID = person.ID;
                }
            }
        }

        public void StopRecruitment()
        {
            if (this.RecruitmentPerson != null)
            {
                this.RecruitmentPerson.WorkKind = ArchitectureWorkKind.无;
            }
            if (this.RecruitmentPerson != null) // 需要重复检查一遍，因为上面可能将this.RecruitmentPerson变null了
            {
                this.RecruitmentPerson.RecruitmentMilitary = null;
                this.RecruitmentPerson = null;
            }
        }

        public override string ToString()
        {
            return string.Concat(new object[] { base.Name, " ", this.Kind.Name, " ", this.Quantity });
        }

        [DataMember]
        public int Combativity
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    return this.combativity;
                }
                return this.ShelledMilitary.Combativity;
            }
            set
            {
                if (this.ShelledMilitary == null)
                {
                    this.combativity = value;
                }
                else
                {
                    this.ShelledMilitary.Combativity = value;
                }
            }
        }

        public int CombativityCeiling
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    if (this.BelongedFaction != null)
                    {
                        return (100 + this.BelongedFaction.IncrementOfCombativityCeiling);
                    }
                }
                else if (this.ShelledMilitary.BelongedFaction != null)
                {
                    return (100 + this.ShelledMilitary.BelongedFaction.IncrementOfCombativityCeiling);
                }
                return 100;
            }
        }

        public int Defence
        {
            get
            {
                return (int) ((this.Kind.Defence + (this.Kind.DefencePerScale * this.Scales)) + (this.Kind.DefencePer100Experience * (Math.Sqrt(this.Experience) / 10)));
            }
        }

        [DataMember]
        public int Experience
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    return (int) this.experience;
                }
                return this.ShelledMilitary.Experience;
            }
            set
            {
                if (this.ShelledMilitary == null)
                {
                    this.experience = value;
                }
                else
                {
                    this.ShelledMilitary.Experience = value;
                }
            }
        }

        public String ExperienceWithLimit
        {
            get
            {
                if (this.CanLevelUp)
                {
                    return this.Experience + "/" + this.Kind.LevelUpExperience;
                }
                else
                {
                    return this.Experience.ToString();
                }
            }
        }

        public Person FollowedLeader
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    if (this.followedLeader == null)
                    {
                        this.followedLeader = Session.Current.Scenario.Persons.GetGameObject(this.followedLeaderID) as Person;
                    }
                    return this.followedLeader;
                }
                return this.ShelledMilitary.FollowedLeader;
            }
            set
            {
                if (this.ShelledMilitary == null)
                {
                    this.followedLeader = value;
                    if (this.followedLeader != null)
                    {
                        this.followedLeaderID = this.followedLeader.ID;
                    }
                    else
                    {
                        this.followedLeaderID = -1;
                    }
                }
                else
                {
                    this.ShelledMilitary.FollowedLeader = value;
                }
            }
        }
        [DataMember]
        public int FollowedLeaderID
        {
            get
            {
                return this.followedLeaderID;
            }
            set
            {
                this.followedLeaderID = value;
            }
        }

        public string FollowedLeaderName
        {
            get
            {
                if (this.FollowedLeader == null)
                {
                    return "----";
                }
                return this.FollowedLeader.Name;
            }
        }

        public int FoodCostPerDay
        {
            get
            {
                return (this.Kind.FoodPerSoldier * this.TotalQuantity);
            }
        }

        public int FoodMax
        {
            get
            {
                return (this.FoodCostPerDay * this.RationDays);
            }
        }

        public int InjuryChance
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    return this.Kind.InjuryChance;
                }
                return this.ShelledMilitary.InjuryChance;
            }
        }

        [DataMember]
        public int InjuryQuantity
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    return this.injuryQuantity;
                }
                return this.ShelledMilitary.InjuryQuantity;
            }
            set
            {
                if (this.ShelledMilitary == null)
                {
                    this.injuryQuantity = value;
                }
                else
                {
                    this.ShelledMilitary.InjuryQuantity = value;
                }
                if (this.injuryQuantity < 0)
                {
                    this.injuryQuantity = 0;
                }
            }
        }

        public MilitaryKind Kind  //小写为真实值，大写转换为运兵船时改变为假值
        {
            get
            {
                if (this.kind == null)
                {
                    this.kind = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(this.kindID);
                }
                if (this.BelongedArchitecture!=null )
                {
                    return this.kind;
                }
                else
                {
                    if (this.bushiShuijunBingqieChuyuShuiyu())
                    {
                        return Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(28);  //运兵船
                    }
                    else
                    {
                        return this.kind;
                    }
                }
            }
            set
            {
                this.kind = value;
                if (this.kind != null)
                {
                    this.kindID = this.kind.ID;
                }
                else
                {
                    this.kindID = -1;
                }
            }
        }

        public int KindID   //小写为真实值，大写转换为运兵船时改变为假值
        {
            get
            {
                
                if (this.BelongedArchitecture != null)
                {
                    return this.kindID;
                }
                else
                {
                    if (this.bushiShuijunBingqieChuyuShuiyu())
                    {
                        return 28;  //运兵船
                    }
                    else
                    {
                        return this.kindID;
                    }
                }

            }
            set
            {
                this.kindID = value;
                if (Session.Current.Scenario != null && Session.Current.Scenario.GameCommonData != null && Session.Current.Scenario.GameCommonData.AllMilitaryKinds != null)
                {
                    this.kind = Session.Current.Scenario.GameCommonData.AllMilitaryKinds.GetMilitaryKind(this.kindID);
                }
            }
        }

        public int RealKindID  
        {
            get
            {
                return this.kindID;
            }
        }

        public MilitaryKind RealMilitaryKind
        {
            get
            {
                return this.kind;
            }
        }

        public string RealKind
        {
            get
            {
                if (this.ShelledMilitary == null)  //没包裹军队
                {
                    return this.kind.Name;


                }
                else   //包裹军队，部队改为进入水中自动切换运兵船之后已经没有这种情况，保留代码是为了和以前的存档兼容。
                {
                    return this.ShelledMilitary.kind.Name;
                }



            }
        }

        public bool bushiShuijunBingqieChuyuShuiyu(Point position)
        {
            if (Session.GlobalVariables.LandArmyCanGoDownWater && kind != null && kind.Type != MilitaryType.水军 &&
                Session.Current.Scenario.GetTerrainKindByPosition(position) == TerrainKind.水域)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool bushiShuijunBingqieChuyuShuiyu()
        {
            return bushiShuijunBingqieChuyuShuiyu(this.Position);
        }

        public string KindString
        {
            get
            {
                return this.Kind.Name;
            }
        }

        public int LeaderFightingForce
        {
            get
            {
                return this.Leader == null ? 0 : this.Leader.FightingForce;
            }
        }

        public Person Leader
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    if (this.leader == null)
                    {
                        this.leader = Session.Current.Scenario.Persons.GetGameObject(this.leaderID) as Person;
                    }
                    return this.leader;
                }
                return this.ShelledMilitary.Leader;
            }
            set
            {
                if (this.ShelledMilitary == null)
                {
                    this.leader = value;
                    if (this.leader != null)
                    {
                        if (this.leaderID != this.leader.ID)
                        {
                            this.LeaderExperience = 0;
                            this.leaderID = this.leader.ID;
                        }
                    }
                    else
                    {
                        this.leaderID = -1;
                    }
                }
                else
                {
                    this.ShelledMilitary.Leader = value;
                }
            }
        }

        public int LeaderExperience
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    return this.leaderExperience;
                }
                return this.ShelledMilitary.LeaderExperience;
            }
            set
            {
                if (this.ShelledMilitary == null)
                {
                    this.leaderExperience = value;
                }
                else
                {
                    this.ShelledMilitary.LeaderExperience = value;
                }
            }
        }

        [DataMember]
        public int RecruitmentPersonID { get; set; }

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
                if (this.ShelledMilitary == null)
                {
                    if (this.Leader == null)
                    {
                        return "----";
                    }
                    return this.Leader.Name;
                }
                if (this.ShelledMilitary.Leader == null)
                {
                    return "----";
                }
                return this.ShelledMilitary.Leader.Name;
            }
        }

        public string LocationString
        {
            get
            {
                if (this.BelongedArchitecture != null)
                {
                    return this.BelongedArchitecture.Name;
                }
                if (this.BelongedTroop != null)
                {
                    return this.BelongedTroop.DisplayName;
                }
                if (this.ShellingMilitary != null)
                {
                    return this.ShellingMilitary.LocationString;
                }
                return "----";
            }
        }

        public int MaxRecruitmentWeighing
        {
            get
            {
                return (this.Kind.MaxScale * (this.Kind.PointsPerSoldier + 1));
            }
        }

        public int MaxTrainingWeighing
        {
            get
            {
                return ((this.Kind.MaxScale * this.MoraleCeiling) * this.CombativityCeiling);
            }
        }


        [DataMember]
        public int Morale
        {
            get
            {
                if (this.morale > this.MoraleCeiling)
                {
                    this.morale = this.MoraleCeiling;
                }
                if (this.ShelledMilitary == null)
                {
                    return this.morale;
                }
                return this.ShelledMilitary.Morale;
            }
            set
            {
                if (this.ShelledMilitary == null)
                {
                    this.morale = value;
                }
                else
                {
                    this.ShelledMilitary.Morale = value;
                }
            }
        }

        public int EncourageMoraleCeiling
        {
            get
            {
                return 100;
            }
        }

        public int MoraleCeiling
        {
            get
            {
                return this.BelongedTroop != null && this.BelongedArchitecture == null ? 120 : 100;
            }
        }

        public int Offence
        {
            get
            {
                return (int) ((this.Kind.Offence + (this.Kind.OffencePerScale * this.Scales)) + (this.Kind.OffencePer100Experience * (Math.Sqrt(this.Experience) / 10)));
            }
        }

        public Point Position
        {
            get
            {
                if (this.BelongedTroop != null)
                {
                    return this.BelongedTroop.Position;
                }
                if (this.BelongedArchitecture != null)
                {
                    return this.BelongedArchitecture.Position;
                }
                if (this.ShellingMilitary != null)
                {
                    return this.ShellingMilitary.Position;
                }
                return Point.Zero;
            }
        }
        [DataMember]
        public int Quantity
        {
            get
            {
                if (this.ShelledMilitary == null)
                {
                    return this.quantity;
                }
                return this.ShelledMilitary.Quantity;
            }
            set
            {
                if (this.ShelledMilitary == null)
                {
                    this.quantity = value;
                }
                else
                {
                    this.ShelledMilitary.Quantity = value;
                }
            }
        }

        public int RationDays
        {
            get
            {
                return this.Kind.RationDays;
            }
        }

        public int zijinzuidazhi
        {
            get
            {
                return this.Kind.zijinshangxian ;
            }
        }



        public Person RecruitmentPerson
        {
            get
            {
                return recruitmentPerson;
            }
            set
            {
                recruitmentPerson = value;
            }
        }

        public string RecruitmentString
        {
            get
            {
                if ((this.BelongedArchitecture != null) && (this.RecruitmentPerson != null))
                {
                    return this.RecruitmentPerson.Name;
                }
                return "----";
            }
        }

        public int RecruitmentWeighing
        {
            get
            {
                return this.Quantity;
            }
        }

        public int Scales
        {
            get
            {
                return (this.Quantity / this.Kind.MinScale);
            }
        }

        public int TotalQuantity
        {
            get
            {
                return (this.Quantity + this.InjuryQuantity);
            }
        }

        public string TrainingString
        {
            get
            {
                if (this.Morale >= this.MoraleCeiling && this.Combativity >= this.CombativityCeiling)
                {
                    return "√";
                }
                else if (this.BelongedArchitecture != null && this.BelongedArchitecture.TrainingWorkingPersons.Count > 0)
                {
                    return "↑";
                }
                else
                {
                    return "----";
                }
            }
        }

        public int TrainingWeighing
        {
            get
            {
                return (((this.Kind.MaxScale - this.Scales) * this.Morale) * this.Combativity);
            }
        }

        public int Weighing
        {
            get
            {
                return ((this.Offence + this.Defence) * (((this.Kind.ViewRadius + (this.Kind.FireDamageRate >= 1.5 ? -1 : 0)) + (this.Kind.ObliqueView ? 1 : 0)) + (this.Kind.RecruitLimit <= 10 ? 1 : 0)));
            }
        }

        public String  BuchongZhuangtai
        {
            get
            {
                if (this.TotalQuantity >= this.Kind.MaxScale)
                {
                    return "√";
                }
                else if ((this.BelongedArchitecture != null) && (this.RecruitmentPerson != null))
                {
                    return "↑";
                }
                else
                {
                    return "";
                }
            }
        }

        public String  YijingXunlianHao
        {
            get
            {
                if (this.Morale>=this.MoraleCeiling && this.Combativity>=this.CombativityCeiling)
                {
                    return "√";
                }
                else if (this.BelongedArchitecture != null && this.BelongedArchitecture.TrainingWorkingPersons.Count > 0)
                {
                    return "↑";
                }
                else
                {
                    return "";
                }
            }
        }

        public int RecoverCost
        {
            get
            {
                if (this.BelongedFaction == null) return 0;
                
                int result = (int)(this.RealMilitaryKind.CreateCost + this.Experience * 5 + (this.FollowedLeader != null ? 1000 : this.LeaderExperience) / 1000.0 * 5000);
                if (!this.BelongedFaction.AvailableMilitaryKinds.GetMilitaryKindList().GameObjects.Contains(this.RealMilitaryKind) || this.RealMilitaryKind.RecruitLimit == 1)
                {
                    result *= 2;
                }
                return result;
            }
        }

        public double RetreatScale
        {
            get
            {
                double retreatScaleRatio = Math.Min(0.5, this.RecoverCost / 50000.0);
                return this.RealMilitaryKind.MaxScale / this.RealMilitaryKind.MinScale * retreatScaleRatio;
            }
        }

        public bool IsFewScaleNeedRetreat
        {
            get
            {
                if (this.BelongedFaction == null) return false;
                if (this.IsTransport) return false;
                if (this.IsShell)
                {
                    return this.Quantity / this.RealMilitaryKind.MinScale < this.RetreatScale;
                }
                else
                {
                    return this.Scales < this.RetreatScale;
                }
            }
        }

        public bool FollowedLeaderAvailable
        {
            get
            {
                return ((this.BelongedArchitecture != null && this.BelongedArchitecture.Persons.GameObjects.Contains(this.FollowedLeader)) ||
                    (this.BelongedTroop != null && !this.BelongedTroop.Destroyed && this.BelongedTroop.Leader == this.FollowedLeader)) && 
                    this.FollowedLeader != null && this.FollowedLeader.Status == GameObjects.PersonDetail.PersonStatus.Normal;
            }
        }

        public bool LeaderAvailable
        {
            get
            {
                return ((this.BelongedArchitecture != null && this.BelongedArchitecture.Persons.GameObjects.Contains(this.Leader)) ||
                    (this.BelongedTroop != null && !this.BelongedTroop.Destroyed && this.BelongedTroop.Leader == this.leader)) &&
                    this.Leader != null && this.Leader.Status == GameObjects.PersonDetail.PersonStatus.Normal;
            }
        }

        public bool AllPersonsAvailable
        {
            get
            {
                if (this.BelongedTroop != null) return true;
                if (this.BelongedArchitecture == null) return false;
                if (this.Leader == null) return false;
                foreach (Person p in this.Leader.preferredTroopPersons)
                {
                    if (!this.BelongedArchitecture.Persons.GameObjects.Contains(p))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public int FightingForce
        {
            get
            {
                double influenceValue = 0;
                foreach (Influences.Influence i in this.Kind.Influences.Influences.Values)
                {
                    influenceValue += i.AIPersonValue;
                }
                return (int) (this.Offence + this.Defence + influenceValue * 2);
            }
        }

        public float FireDamageRate
        {
            get
            {
                return this.Kind.FireDamageRate;
            }
        }

        public bool AirOffence
        {
            get
            {
                return this.Kind.AirOffence;
            }
        }

        public float ArchitectureCounterDamageRate
        {
            get
            {
                return this.Kind.ArchitectureCounterDamageRate;
            }
        }

        public float ArchitectureDamageRate
        {
            get
            {
                return this.Kind.ArchitectureDamageRate;
            }
        }

        public bool ArrowOffence
        {
            get
            {
                return this.Kind.ArrowOffence;
            }
        }

        public string ArrowOffenceString
        {
            get
            {
                return (this.Kind.ArrowOffence ? "○" : "×");
            }
        }

        public TroopAttackDefaultKind AttackDefaultKind
        {
            get
            {
                return this.Kind.AttackDefaultKind;
            }
        }

        public TroopAttackTargetKind AttackTargetKind
        {
            get
            {
                return this.Kind.AttackTargetKind;
            }
        }

        public bool BeCountered
        {
            get
            {
                return this.Kind.BeCountered;
            }
        }

        public string BeCounteredString
        {
            get
            {
                return (this.Kind.BeCountered ? "○" : "×");
            }
        }

        public bool CanLevelUp
        {
            get
            {
                return this.Kind.CanLevelUp;
            }
        }

        public string CanLevelUpString
        {
            get
            {
                return (this.Kind.CanLevelUp ? "○" : "×");
            }
        }

        public TroopCastDefaultKind CastDefaultKind
        {
            get
            {
                return this.Kind.CastDefaultKind;
            }
        }

        public TroopCastTargetKind CastTargetKind
        {
            get
            {
                return this.Kind.CastTargetKind;
            }
        }

        public int CliffAdaptability
        {
            get
            {
                return this.Kind.CliffAdaptability;
            }
        }

        public float CliffRate
        {
            get
            {
                return this.Kind.CliffRate;
            }
        }

        public bool ContactOffence
        {
            get
            {
                return this.Kind.ContactOffence;
            }
        }

        public string ContactOffenceString
        {
            get
            {
                return (this.Kind.ContactOffence ? "○" : "×");
            }
        }

        public bool CounterOffence
        {
            get
            {
                return this.Kind.CounterOffence;
            }
        }

        public string CounterOffenceString
        {
            get
            {
                return (this.Kind.CounterOffence ? "○" : "×");
            }
        }

        public bool CreateBesideWater
        {
            get
            {
                return this.Kind.CreateBesideWater;
            }
        }

        public string CreateBesideWaterString
        {
            get
            {
                return (this.Kind.CreateBesideWater ? "○" : "×");
            }
        }

        public int CreateCost
        {
            get
            {
                return this.Kind.CreateCost;
            }
        }

        public int CreateTechnology
        {
            get
            {
                return this.Kind.CreateTechnology;
            }
        }

        public string Description
        {
            get
            {
                return this.Kind.Description;
            }
        }

        public int DesertAdaptability
        {
            get
            {
                return this.Kind.DesertAdaptability;
            }
        }

        public float DesertRate
        {
            get
            {
                return this.Kind.DesertRate;
            }
        }

        public int FoodPerSoldier
        {
            get
            {
                return this.Kind.FoodPerSoldier;
            }
        }

        public int ForrestAdaptability
        {
            get
            {
                return this.Kind.ForrestAdaptability;
            }
        }

        public float ForrestRate
        {
            get
            {
                return this.Kind.ForrestRate;
            }
        }

        public int GrasslandAdaptability
        {
            get
            {
                return this.Kind.GrasslandAdaptability;
            }
        }

        public float GrasslandRate
        {
            get
            {
                return this.Kind.GrasslandRate;
            }
        }

        public int InfluenceCount
        {
            get
            {
                return this.Kind.Influences.Count;
            }
        }

        public bool IsShell
        {
            get
            {
                return this.Kind.IsShell;
            }
        }

        public string IsShellString
        {
            get
            {
                return (this.Kind.IsShell ? "○" : "×");
            }
        }

        public int LevelUpExperience
        {
            get
            {
                return this.Kind.LevelUpExperience;
            }
        }

        public int MarshAdaptability
        {
            get
            {
                return this.Kind.MarshAdaptability;
            }
        }

        public float MarshRate
        {
            get
            {
                return this.Kind.MarshRate;
            }
        }

        public int MaxScale
        {
            get
            {
                return this.Kind.MaxScale;
            }
        }

        public int MinScale
        {
            get
            {
                return this.Kind.MinScale;
            }
        }

        public int MountainAdaptability
        {
            get
            {
                return this.Kind.MountainAdaptability;
            }
        }

        public float MountainRate
        {
            get
            {
                return this.Kind.MountainRate;
            }
        }

        public int Movability
        {
            get
            {
                return this.Kind.Movability;
            }
        }

        public bool ObliqueOffence
        {
            get
            {
                return this.Kind.ObliqueOffence;
            }
        }

        public string ObliqueOffenceString
        {
            get
            {
                return (this.Kind.ObliqueOffence ? "○" : "×");
            }
        }

        public bool ObliqueStratagem
        {
            get
            {
                return this.Kind.ObliqueStratagem;
            }
        }

        public string ObliqueStratagemString
        {
            get
            {
                return (this.Kind.ObliqueStratagem ? "○" : "×");
            }
        }

        public bool ObliqueView
        {
            get
            {
                return this.Kind.ObliqueView;
            }
        }

        public string ObliqueViewString
        {
            get
            {
                return (this.Kind.ObliqueView ? "○" : "×");
            }
        }

        public bool OffenceOnlyBeforeMove
        {
            get
            {
                return this.Kind.OffenceOnlyBeforeMove;
            }
        }

        public string OffenceOnlyBeforeMoveString
        {
            get
            {
                return (this.Kind.OffenceOnlyBeforeMove ? "○" : "×");
            }
        }

        public int OffenceRadius
        {
            get
            {
                return this.Kind.OffenceRadius;
            }
        }

        public int OneAdaptabilityKind
        {
            get
            {
                return this.Kind.OneAdaptabilityKind;
            }
        }

        public int PlainAdaptability
        {
            get
            {
                return this.Kind.PlainAdaptability;
            }
        }

        public float PlainRate
        {
            get
            {
                return this.Kind.PlainRate;
            }
        }

        public int PointsPerSoldier
        {
            get
            {
                return this.Kind.PointsPerSoldier;
            }
        }

        public int RidgeAdaptability
        {
            get
            {
                return this.Kind.RidgeAdaptability;
            }
        }

        public float RidgeRate
        {
            get
            {
                return this.Kind.RidgeRate;
            }
        }

        public int Speed
        {
            get
            {
                return this.Kind.Speed;
            }
        }

        public int StratagemRadius
        {
            get
            {
                return this.Kind.StratagemRadius;
            }
        }

        public int TitleInfluence
        {
            get
            {
                return this.Kind.TitleInfluence;
            }
        }

        public MilitaryType Type
        {
            get
            {
                return this.Kind.Type;
            }
        }

        public int RecruitLimit
        {
            get
            {
                return this.Kind.RecruitLimit;
            }
        }

        public int ViewRadius
        {
            get
            {
                return this.Kind.ViewRadius;
            }
        }

        public int WastelandAdaptability
        {
            get
            {
                return this.Kind.WastelandAdaptability;
            }
        }

        public float WastelandRate
        {
            get
            {
                return this.Kind.WastelandRate;
            }
        }

        public int WaterAdaptability
        {
            get
            {
                return this.Kind.WaterAdaptability;
            }
        }

        public float WaterRate
        {
            get
            {
                return this.Kind.WaterRate;
            }
        }

        public int MorphToKindId
        {
            get
            {
                return this.Kind.MorphToKindId;
            }
        }

        public GameObjectList GetInfluenceList()
        {
            return this.Kind.Influences.GetInfluenceList();
        }

        public Architecture TargetArchitecture
        {
            get
            {
                if (this.targetArchitectureID == -1) return null;
                
                if (this.targetArchitecture == null)
                {
                    this.targetArchitecture = Session.Current.Scenario.Architectures.GetGameObject(this.targetArchitectureID) as Architecture;
                }
                return this.targetArchitecture;
            }
            set
            {
                if (this.targetArchitecture == null && value == null) return;
                this.targetArchitecture = value;
                
                if (value != null)
                {
                    this.targetArchitectureID = value.ID;
                }
                else
                {
                    this.targetArchitectureID = -1;
                }
            }
        }
        [DataMember]
        public int TargetArchitectureID
        {
            get
            {
                return this.targetArchitectureID;
            }
            set
            {
                this.targetArchitectureID = value;
            }
        }

        public string TargetArchitectureString
        {
            get
            {
                if (this.targetArchitectureID < 0)
                {
                    return "----";
                }
                return this.TargetArchitecture.Name;
            }
        }

        public Architecture StartingArchitecture
        {
            get
            {
                if (this.startingArchitectureID == -1) return null;

                if (this.startingArchitecture == null)
                {
                    this.startingArchitecture = Session.Current.Scenario.Architectures.GetGameObject(this.startingArchitectureID) as Architecture;
                }
                return this.startingArchitecture;
            }
            set
            {
                if (this.startingArchitecture == null && value == null) return;
                this.startingArchitecture = value;

                if (value != null)
                {
                    this.startingArchitectureID = value.ID;
                }
                else
                {
                    this.startingArchitectureID = -1;
                }
            }
        }
        [DataMember]
        public int StartingArchitectureID
        {
            get
            {
                return this.startingArchitectureID;
            }
            set
            {
                this.startingArchitectureID = value;
            }
        }

        public string StartingArchitectureString
        {
            get
            {
                if (this.startingArchitectureID < 0)
                {
                    return "----";
                }
                return this. StartingArchitecture.Name;
            
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

        public int ServedYears
        {
            get
            {
                int year = Session.Current.Scenario.Date.Year - this.YearCreated;
                int sinceBeginning = Session.Current.Scenario.DaySince / 360;
                return Math.Min(year, sinceBeginning);
            }
        }

        public int TransferDays(double distance)
        {
            return (int) Math.Ceiling(distance / this.Movability * 6);
        }

        public int TransferFundCost(double distance)
        {
            return (int) (distance * 10);
        }

        public int TransferFoodCost(double distance)
        {
            return (int)(TransferDays(distance) * this.Kind.FoodPerSoldier * this.Quantity);
        }


    }
}

