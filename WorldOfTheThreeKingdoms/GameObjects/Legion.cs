using GameGlobal;
using GameObjects.FactionDetail;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class Legion : GameObject
    {

        public Faction BelongedFaction;

        [DataMember]
        public int CoreTroopString { get; set; }

        public Troop CoreTroop;
        [DataMember]
        public Point? InformationDestination = null;
        private LegionKind kind;

        [DataMember]
        public int PreferredRoutewayString { get; set; }

        public Routeway PreferredRouteway;

        [DataMember]
        public int StartArchitectureString { get; set; }

        public Architecture StartArchitecture;

        [DataMember]
        public List<SupplyingRoutewayPack> SupplyingRouteways = new List<SupplyingRoutewayPack>();

        [DataMember]
        public List<Point> TakenPositions = new List<Point>();

        [DataMember]
        public string TroopsString { get; set; }

        public TroopList Troops = new TroopList();

        [DataMember]
        public int WillArchitectureString { get; set; }

        public Architecture WillArchitecture;

        public void Init()
        {
            Troops = new TroopList();
        }

        public void AddRoutewayCredit(Routeway routeway, int credit)
        {
            foreach (SupplyingRoutewayPack pack in this.SupplyingRouteways)
            {
                if (pack.SupplyingRouteway == routeway)
                {
                    pack.Credit += credit;
                    return;
                }
            }
            SupplyingRoutewayPack item = new SupplyingRoutewayPack();
            item.SupplyingRouteway = routeway;
            item.Credit = credit;
            this.SupplyingRouteways.Add(item);
        }

        public void AddTroop(Troop troop)
        {
            troop.BelongedLegion = this;
            this.Troops.Add(troop);
        }

        public void AI()
        {
            this.CallRouteway();
            this.ResetCoreTroop();
            this.TroopAI();
        }

        public void AIWithAuto()
        {
            this.ResetCoreTroop();
            this.TakenPositions.Clear();
            foreach (Troop troop in this.Troops.GetList())
            {
                if (troop.Auto || troop.StartingArchitecture.BelongedSection.AIDetail.AutoRun)
                {
                    troop.AI();
                }
            }
        }

        public void CallInformation()
        {
            if (!this.InformationDestination.HasValue)
            {
                PersonList list = new PersonList();
                foreach (LinkNode node in this.WillArchitecture.AIAllLinkNodes.Values)
                {
                    if ((((node.A.BelongedFaction == this.BelongedFaction) && node.A.BelongedSection != null && 
                        node.A.BelongedSection.AIDetail.AllowInvestigateTactics) && node.A.InformationAvail()) &&
                        (node.A.RecentlyAttacked <= 0))
                    {
                        foreach (Person person in node.A.MovablePersons)
                        {
                            if (person.LocationArchitecture != null)
                            {
                                list.Add(person);
                            }
                        }
                        if (list.Count >= 10)
                        {
                            break;
                        }
                    }
                }
                if (list.Count > 0)
                {
                    Person person = list[GameObject.Random(list.Count)] as Person;
                    InformationKindList availList = base.Scenario.GameCommonData.AllInformationKinds.GetAvailList(person.LocationArchitecture);
                    if (availList.Count > 0)
                    {
                        if (availList.Count > 1)
                        {
                            if (this.WillArchitecture.BelongedFaction == null)
                            {
                                availList.PropertyName = "CostFund";
                                availList.SmallToBig = true;
                            }
                            else
                            {
                                availList.PropertyName = "FightingWeighing";
                            }
                            availList.IsNumber = true;
                            availList.ReSort();
                        }
                        this.SetInformationPosition();
                        if (this.InformationDestination.HasValue)
                        {
                            person.CurrentInformationKind = availList[GameObject.Random(availList.Count / 2)] as InformationKind;
                            person.GoForInformation(this.InformationDestination.Value);
                        }
                    }
                }
            }
        }

        private void CallRouteway()
        {
            if (!GlobalVariables.LiangdaoXitong) return;
            if (this.WillArchitecture != null)
            {
                int foodCostPerDay;
                LinkNode node2;
                Routeway routeway;
                if (this.Kind == LegionKind.Offensive)
                {
                    if ((this.WillArchitecture.BelongedFaction != this.BelongedFaction) || (this.WillArchitecture.RecentlyAttacked > 0))
                    {
                        foodCostPerDay = this.FoodCostPerDay;
                        if (((this.PreferredRouteway == null) || (!this.PreferredRouteway.Building && (this.PreferredRouteway.LastActivePointIndex < 0))) || ((this.PreferredRouteway.LastPoint != null) && !this.PreferredRouteway.IsEnough(this.PreferredRouteway.LastPoint.ConsumptionRate, foodCostPerDay * 12)))
                        {
                            foreach (LinkNode node in this.WillArchitecture.AIAllLinkNodes.Values)
                            {
                                if (node.Level > 2)
                                {
                                    break;
                                }
                                if (((node.A.BelongedFaction == this.BelongedFaction) && (node.A.RecentlyAttacked <= 0)) && (node.A.Food >= (foodCostPerDay * 15)))
                                {
                                    node2 = null;
                                    if (node.A.AIAllLinkNodes.TryGetValue(this.WillArchitecture.ID, out node2))
                                    {
                                        routeway = node.A.GetRouteway(node2, true);
                                        if (((routeway != null) && (routeway.LastPoint != null) && (node.A.Fund >= (routeway.LastPoint.BuildFundCost * (2 + ((this.WillArchitecture.AreaCount >= 4) ? 1 : 0))))) && routeway.ByPassHostileArchitecture == null)
                                        {
                                            routeway.Building = true;
                                            this.PreferredRouteway = routeway;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if ((this.Kind == LegionKind.Defensive) && (this.WillArchitecture.BelongedFaction == this.BelongedFaction))
                {
                    foodCostPerDay = this.FoodCostPerDay;
                    if ((this.WillArchitecture.Food < (foodCostPerDay * 12)) && (((this.PreferredRouteway == null) || (!this.PreferredRouteway.Building && (this.PreferredRouteway.LastActivePointIndex < 0))) || ((this.PreferredRouteway.LastPoint != null) && !this.PreferredRouteway.IsEnough(this.PreferredRouteway.LastPoint.ConsumptionRate, foodCostPerDay * 12))))
                    {
                        foreach (LinkNode node in this.WillArchitecture.AIAllLinkNodes.Values)
                        {
                            if (node.Level > 2)
                            {
                                break;
                            }
                            if (((node.A.BelongedFaction == this.BelongedFaction) && (node.A.RecentlyAttacked <= 0)) && (node.A.Food >= (foodCostPerDay * 15)))
                            {
                                node2 = null;
                                if (node.A.AIAllLinkNodes.TryGetValue(this.WillArchitecture.ID, out node2))
                                {
                                    routeway = node.A.GetRouteway(node2, true);
                                    if ((routeway != null) && (routeway.LastPoint != null) && (node.A.Fund >= (routeway.LastPoint.BuildFundCost * 2)))
                                    {
                                        routeway.Building = true;
                                        this.PreferredRouteway = routeway;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void DayEvent()
        {
            this.SupplyingRouteways.Clear();
            foreach (Troop troop in this.Troops.GetList())
            {
                troop.DayEvent();
            }
            Routeway maxCreditRouteway = this.GetMaxCreditRouteway();
            if (maxCreditRouteway != null)
            {
                this.PreferredRouteway = maxCreditRouteway;
            }
        }

        public void Disband()
        {
            this.PreferredRouteway = null;
            this.StartArchitecture = null;
            this.WillArchitecture = null;
            this.CoreTroop = null;
            foreach (Troop t in this.Troops)
            {
                t.BelongedLegion = null;
            }

            this.Troops.Clear();
            if (this.BelongedFaction != null)
            {
                foreach (Architecture architecture in this.BelongedFaction.Architectures)
                {
                    if (architecture.DefensiveLegion == this)
                    {
                        architecture.DefensiveLegion = null;
                    }
                }
                this.BelongedFaction.RemoveLegion(this);
            }
        }

        public int GetLegionHostileTroopFightingForceInView()
        {
            TroopList list = new TroopList();
            int num = 0;
            foreach (Troop troop in this.Troops)
            {
                foreach (Troop troop2 in troop.GetHostileTroopsInView())
                {
                    if (!list.HasGameObject(troop2))
                    {
                        list.Add(troop2);
                        num += troop2.FightingForce;
                    }
                }
            }
            return num;
        }

        public Architecture GetLegionTroopFactionStartArchitecture()
        {
            foreach (Troop troop in this.Troops)
            {
                if ((troop.StartingArchitecture != null) && (troop.StartingArchitecture.BelongedFaction == this.BelongedFaction))
                {
                    return troop.StartingArchitecture;
                }
            }
            return null;
        }

        public int GetLegionTroopFightingForce()
        {
            int num = 0;
            foreach (Troop troop in this.Troops)
            {
                num += troop.FightingForce;
            }
            return num;
        }

        public Routeway GetMaxCreditRouteway()
        {
            int credit = 0;
            Routeway supplyingRouteway = null;
            foreach (SupplyingRoutewayPack pack in this.SupplyingRouteways)
            {
                if (pack.Credit > credit)
                {
                    credit = pack.Credit;
                    supplyingRouteway = pack.SupplyingRouteway;
                }
            }
            return supplyingRouteway;
        }

        public int GetMinTroopFoodCost()
        {
            if (this.Troops.Count <= 0)
            {
                return 0;
            }
            int foodCostPerDay = 0x7fffffff;
            foreach (Troop troop in this.Troops)
            {
                if (troop.FoodCostPerDay < foodCostPerDay)
                {
                    foodCostPerDay = troop.FoodCostPerDay;
                }
            }
            return foodCostPerDay;
        }

        public Troop GetWillClosestTroop()
        {
            if (this.Troops.Count == 1)
            {
                return (this.Troops[0] as Troop);
            }
            double maxValue = double.MaxValue;
            Troop troop = null;
            foreach (Troop troop2 in this.Troops)
            {
                double distance = base.Scenario.GetDistance(troop2.Position, this.WillArchitecture.ArchitectureArea);
                if (distance < maxValue)
                {
                    maxValue = distance;
                    troop = troop2;
                }
            }
            return troop;
        }

        public bool HasMovingTroopStartFromArchitecture(Architecture start)
        {
            foreach (Troop troop in this.Troops)
            {
                if ((troop.StartingArchitecture == start) && !troop.IsBaseViewingArchitecture(troop.WillArchitecture))
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasTroop(Troop troop)
        {
            return this.Troops.HasGameObject(troop.ID);
        }

        public void LoadTroopsFromString(TroopList troops, string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Troops.Clear();
            foreach (string str in strArray)
            {
                Troop gameObject = troops.GetGameObject(int.Parse(str)) as Troop;
                if (gameObject != null)
                {
                    this.AddTroop(gameObject);
                }
            }
        }

        public void RemoveTroop(Troop troop)
        {
            troop.BelongedLegion = null;
            this.Troops.Remove(troop);
        }

        public void ResetCoreTroop()
        {
            if (this.Troops.Count > 0)
            {
                if (this.Troops.Count > 1)
                {
                    this.Troops.PropertyName = "Weighing";
                    this.Troops.IsNumber = true;
                    this.Troops.ReSort();
                }
                this.CoreTroop = this.Troops[0] as Troop;
            }
        }

        public void SetInformationPosition()
        {
            List<Point> orientations = new List<Point>();
            foreach (Troop troop in this.Troops)
            {
                orientations.Add(troop.Position);
            }
            this.InformationDestination = base.Scenario.GetClosestPosition(this.WillArchitecture.ArchitectureArea, orientations);
        }

        public void TroopAI()
        {
            this.TakenPositions.Clear();
            foreach (Troop troop in this.Troops.GetList())
            {
                troop.AI();
            }
        }

        public int FoodCostPerDay
        {
            get
            {
                int num = 0;
                foreach (Troop troop in this.Troops)
                {
                    num += troop.FoodCostPerDay;
                }
                return num;
            }
        }

        public bool HasCuttingRoutewayTroop
        {
            get
            {
                foreach (Troop troop in this.Troops)
                {
                    if (troop.CutRoutewayDays > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasTroopViewingWillArchitecture
        {
            get
            {
                foreach (Troop troop in this.Troops)
                {
                    if (troop.IsBaseViewingArchitecture(this.WillArchitecture))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool HasTroopWillArchitectureIsWillArchitecture
        {
            get
            {
                foreach (Troop troop in this.Troops)
                {
                    if (troop.WillArchitecture == this.WillArchitecture)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public int ArmyScale
        {
            get
            {
                int r = 0;
                foreach (Troop t in this.Troops)
                {
                    r += t.Army.Scales;
                }
                return r;
            }
        }
        [DataMember]
        public LegionKind Kind
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
    }
}

