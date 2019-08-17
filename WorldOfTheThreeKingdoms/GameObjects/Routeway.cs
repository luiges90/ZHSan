using GameGlobal;
using GameObjects.MapDetail;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using GameManager;


namespace GameObjects
{
    [DataContract]
    public class Routeway : GameObject
    {
        [DataMember]
        private bool avoidWater;
        
        public Faction BelongedFaction;

        [DataMember]
        private bool building;
        
        public Architecture DestinationArchitecture;
        
        public Architecture EndArchitecture;

        [DataMember]
        private bool HasSupportedLegion = false;
        [DataMember]
        private int inefficiencyDays;
        [DataMember]
        private int lastActivePointIndex = -1;
        [DataMember]
        private bool removeAfterClose;
        
        public void Init()
        {
            RouteArea = new Dictionary<Point, RoutePoint>();
        }

        public Dictionary<Point, RoutePoint> RouteArea = new Dictionary<Point, RoutePoint>();

        [DataMember]
        public LinkedList<RoutePoint> RoutePoints = new LinkedList<RoutePoint>();

        private bool showArea;
        
        [DataMember]
        public int StartArchitectureString { get; set; }

        [DataMember]
        public int EndArchitectureString { get; set; }

        [DataMember]
        public int DestinationArchitectureString { get; set; }

        [DataMember]
        public int BelongedFactionString { get; set; }

        [DataMember]
        public Boolean Developing { get; private set; }

        public Architecture StartArchitecture;

        private void AddRoutePointArea(RoutePoint routePoint)
        {
            GameArea area = GameArea.GetViewArea(routePoint.Position, this.Radius, true, this.BelongedFaction);
            foreach (Point point in area.Area)
            {
                if (!Session.Current.Scenario.PositionOutOfRange(point) && !this.RouteArea.ContainsKey(point))
                {
                    this.RouteArea.Add(point, routePoint);
                    Session.Current.Scenario.MapTileData[point.X, point.Y].AddSupplyingRoutePoint(routePoint);
                }
            }
        }

        public void Build()
        {
            this.Building = !this.Building;
        }

        public bool BuildAvail()
        {
            return ((this.StartArchitecture != null) && !this.IsActive && Session.GlobalVariables.LiangdaoXitong);
        }

        public void Clear()
        {
            while (this.RoutePoints.Last != null)
            {
                this.RemoveLastPoint();
            }
        }

        public void Close()
        {
            this.Building = false;
            this.InefficiencyDays = 0;
            if (!this.IsActive)
            {
                this.DestinationToEnd();
            }
            foreach (RoutePoint point in this.RoutePoints)
            {
                if (point.Index > this.LastActivePointIndex)
                {
                    break;
                }
                this.RemoveRoutePointArea(point);
                this.BelongedFaction.RemovePositionInformation(point.Position, Session.GlobalVariables.RoutewayInformationLevel);
            }
            this.LastActivePointIndex = -1;
            this.RouteArea.Clear();
            if (this.RemoveAfterClose)
            {
                Session.Current.Scenario.RemoveRouteway(this);
            }
        }

        public void CloseAt(Point p)
        {
            this.Building = false;
            this.ShrinkAt(p);
        }

        public bool CloseAvail()
        {
            return (this.Building || (this.LastActivePointIndex >= 0));
        }

        public bool ContainsPoint(Point p)
        {
            foreach (RoutePoint point in this.RoutePoints)
            {
                if (point.Position == p)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CutAt(Point p)
        {
            if (!this.ContainsPoint(p))
            {
                return false;
            }
            while (this.RoutePoints.Last != null)
            {
                if (!(this.RoutePoints.Last.Value.Position != p))
                {
                    break;
                }
                this.RemoveLastPoint();
            }
            this.RemoveLastPoint();
            if (this.RoutePoints.Last != null)
            {
                if (this.LastActivePointIndex > this.LastPoint.Index)
                {
                    this.LastActivePointIndex = this.LastPoint.Index;
                }
                this.LastPoint.Direction = SimpleDirection.None;
            }
            return (this.RoutePoints.Count == 0);
        }

        public void DayEvent()
        {
            if (!Session.GlobalVariables.LiangdaoXitong) return;
            LinkedListNode<RoutePoint> lastActiveNode = this.LastActiveNode;
            if (lastActiveNode != null)
            {
                if (this.StartArchitecture.Fund >= lastActiveNode.Value.ActiveFundCost)
                {
                    this.StartArchitecture.DecreaseFund(lastActiveNode.Value.ActiveFundCost);
                    this.BelongedFaction.IncreaseTechniquePoint(lastActiveNode.Value.ActiveFundCost * 2);
                    this.BelongedFaction.IncreaseReputation(lastActiveNode.Value.ActiveFundCost / 40);
                    if (this.InefficiencyDays > 0)
                    {
                        //this.InefficiencyDays--;
                        this.InefficiencyDays -= Session.Parameters.DayInTurn;
                    }
                }
                else
                {
                    //this.InefficiencyDays++;
                    //if (this.InefficiencyDays >= 10)
                    this.InefficiencyDays += Session.Parameters.DayInTurn;
                    if (this.InefficiencyDays >= 10 * Session.Parameters.DayInTurn)
                    {
                        this.Close();
                        return;
                    }
                }
            }
            else if (this.HasSupportedLegion)
            {
                this.Building = true;
            }

            if (this.StartArchitecture.BelongedSection == null)
            {
                this.Close();
                return;
            }

            if (this.EndArchitecture != null && 
                this.StartArchitecture.BelongedSection.AIDetail.AutoRun && this.BelongedFaction == this.EndArchitecture.BelongedFaction)
            {
                this.Close();
                return;
            }

            if (this.Building)
            {
                this.ExpandActiveRouteway(lastActiveNode);
            }
        }

        public void DestinationToEnd()
        {
            if (((this.EndArchitecture == null) && (this.DestinationArchitecture != null)) && this.BelongedFaction.IsFriendly(this.DestinationArchitecture.BelongedFaction))
            {
                GameArea routewayStartArea = this.DestinationArchitecture.GetRoutewayStartArea();
                foreach (Point point in routewayStartArea.Area)
                {
                    LinkedListNode<RoutePoint> node = this.GetNode(point);
                    if (node != null)
                    {
                        this.CutAt(node.Value.Position);
                        this.EndArchitecture = this.DestinationArchitecture;
                    }
                }
            }
        }

        public bool EndedInArchitectureRoutewayStartArea(Architecture des)
        {
            GameArea routewayStartArea = des.GetRoutewayStartArea();
            foreach (Point point in routewayStartArea.Area)
            {
                if (this.LastPoint.Position == point)
                {
                    return true;
                }
            }
            return false;
        }

        private void ExpandActiveRouteway(LinkedListNode<RoutePoint> node)
        {
            this.Developing = false;
            Troop troopByPositionNoCheck;
            int routewayWorkForce = this.BelongedFaction.RoutewayWorkForce;
            if (node == null)
            {
                node = this.RoutePoints.First;
                if (node == null) return;
                troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(node.Value.Position);
                if ((!((troopByPositionNoCheck == null) || this.BelongedFaction.IsFriendly(troopByPositionNoCheck.BelongedFaction)) || Session.Current.Scenario.PositionIsOnFire(node.Value.Position)) || ((routewayWorkForce < node.Value.BuildWorkCost) || (this.StartArchitecture.Fund < node.Value.BuildFundCost)))
                {
                    return;
                }
                this.StartArchitecture.DecreaseFund(node.Value.BuildFundCost);
                this.Developing = true;
                this.LastActivePointIndex++;
                this.AddRoutePointArea(node.Value);
                routewayWorkForce -= node.Value.BuildWorkCost;
                this.BelongedFaction.AddPositionInformation(node.Value.Position, Session.GlobalVariables.RoutewayInformationLevel);
                this.BelongedFaction.IncreaseTechniquePoint(node.Value.BuildFundCost * 2);
                this.BelongedFaction.IncreaseReputation(node.Value.BuildFundCost / 20);
            }
            while (node.Next != null)
            {
                troopByPositionNoCheck = Session.Current.Scenario.GetTroopByPositionNoCheck(node.Next.Value.Position);
                if (!((troopByPositionNoCheck == null) || this.BelongedFaction.IsFriendly(troopByPositionNoCheck.BelongedFaction)) || Session.Current.Scenario.PositionIsOnFire(node.Next.Value.Position))
                {
                    break;
                }
                int decrement = node.Next.Value.BuildFundCost - node.Value.BuildFundCost;
                if ((routewayWorkForce < node.Next.Value.BuildWorkCost) || (this.StartArchitecture.Fund < decrement))
                {
                    break;
                }
                this.StartArchitecture.DecreaseFund(decrement);
                this.Developing = true;
                this.LastActivePointIndex++;
                this.AddRoutePointArea(node.Next.Value);
                routewayWorkForce -= node.Next.Value.BuildWorkCost;
                this.BelongedFaction.AddPositionInformation(node.Next.Value.Position, Session.GlobalVariables.RoutewayInformationLevel);
                this.BelongedFaction.IncreaseTechniquePoint(decrement * 2);
                this.BelongedFaction.IncreaseReputation(decrement / 20);
                node = node.Next;
            }
        }

        public void Extend(Point p)
        {
            RoutePoint point = new RoutePoint();
            point.Position = p;
            TerrainDetail terrainDetailByPosition = Session.Current.Scenario.GetTerrainDetailByPosition(p);
            if (this.BelongedFaction != null)
            {
                if (this.RoutePoints.Count <= 0)
                {
                    point.Index = 0;
                    point.PreviousDirection = SimpleDirection.None;
                    point.Direction = SimpleDirection.None;
                    point.ConsumptionRate = terrainDetailByPosition.RoutewayConsumptionRate * this.BelongedFaction.RateOfRoutewayConsumption;
                    point.BuildFundCost = (int)(terrainDetailByPosition.RoutewayBuildFundCost * this.StartArchitecture.RateOfRoutewayBuildFundCost);
                    point.ActiveFundCost = terrainDetailByPosition.RoutewayActiveFundCost;
                    point.BuildWorkCost = terrainDetailByPosition.RoutewayBuildWorkCost;
                }
                else
                {
                    point.Index = this.LastPoint.Index + 1;
                    int num = p.X - this.LastPoint.Position.X;
                    int num2 = p.Y - this.LastPoint.Position.Y;
                    switch (num)
                    {
                        case -1:
                            this.LastPoint.Direction = SimpleDirection.Left;
                            break;

                        case 0:
                            switch (num2)
                            {
                                case -1:
                                    this.LastPoint.Direction = SimpleDirection.Up;
                                    break;

                                case 1:
                                    this.LastPoint.Direction = SimpleDirection.Down;
                                    break;
                            }
                            break;

                        case 1:
                            this.LastPoint.Direction = SimpleDirection.Right;
                            break;
                    }
                    point.PreviousDirection = this.LastPoint.Direction;
                    point.Direction = SimpleDirection.None;
                    point.ConsumptionRate = this.LastPoint.ConsumptionRate + (terrainDetailByPosition.RoutewayConsumptionRate * this.BelongedFaction.RateOfRoutewayConsumption);
                    point.BuildFundCost = this.LastPoint.BuildFundCost + ((int)(terrainDetailByPosition.RoutewayBuildFundCost * this.StartArchitecture.RateOfRoutewayBuildFundCost));
                    point.ActiveFundCost = this.LastPoint.ActiveFundCost + terrainDetailByPosition.RoutewayActiveFundCost;
                    point.BuildWorkCost = terrainDetailByPosition.RoutewayBuildWorkCost;
                }
                point.BelongedRouteway = this;
                this.RoutePoints.AddLast(point);
                if (this.LastActivePointIndex >= point.Index)
                {
                    this.BelongedFaction.AddPositionInformation(p, Session.GlobalVariables.RoutewayInformationLevel);
                }
                Session.Current.Scenario.MapTileData[p.X, p.Y].AddTileRouteway(this);
            }
        }

        public void ExtendTo(Point point)
        {
            this.BelongedFaction.RoutewayPathBuilder.ConsumptionMax = 0.2f;
            if (this.BelongedFaction.RoutewayPathAvail(this.LastPoint.Position, point, true))
            {
                List<Point> currentRoutewayPath = this.BelongedFaction.GetCurrentRoutewayPath();
                if (currentRoutewayPath.Count > 1)
                {
                    for (int i = 1; i < currentRoutewayPath.Count; i++)
                    {
                        if (this.GetPoint(currentRoutewayPath[i]) != null)
                        {
                            break;
                        }
                        this.Extend(currentRoutewayPath[i]);
                    }
                }
            }
            this.BelongedFaction.RoutewayPathBuilder.ConsumptionMax = 0.7f;
        }

        public LinkedListNode<RoutePoint> GetActiveNode(Point p)
        {
            for (LinkedListNode<RoutePoint> node = this.RoutePoints.First; node != null; node = node.Next)
            {
                if ((node.Value.Index <= this.LastActivePointIndex) && (node.Value.Position == p))
                {
                    return node;
                }
            }
            return null;
        }

        public LinkedListNode<RoutePoint> GetNode(Point p)
        {
            for (LinkedListNode<RoutePoint> node = this.RoutePoints.Last; node != null; node = node.Previous)
            {
                if (node.Value.Position == p)
                {
                    return node;
                }
            }
            return null;
        }

        public RoutePoint GetPoint(Point p)
        {
            foreach (RoutePoint point in this.RoutePoints)
            {
                if (point.Position == p)
                {
                    return point;
                }
            }
            return null;
        }

        public bool HasPointInArchitectureRoutewayStartArea(Architecture des)
        {
            GameArea routewayStartArea = des.GetRoutewayStartArea();
            foreach (Point point in routewayStartArea.Area)
            {
                if (this.GetNode(point) != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsActiveInArea(GameArea area, out float minRate)
        {
            minRate = 1f;
            bool flag = false;
            LinkedListNode<RoutePoint> activeNode = null;
            foreach (Point point in area.Area)
            {
                activeNode = this.GetActiveNode(point);
                if (activeNode != null)
                {
                    flag = true;
                    if (activeNode.Value.ConsumptionRate < minRate)
                    {
                        minRate = activeNode.Value.ConsumptionRate;
                    }
                }
            }
            return flag;
        }

        public bool IsEnough(Point position, int food)
        {
            RoutePoint point = null;
            this.RouteArea.TryGetValue(position, out point);
            return ((point != null) && this.IsEnough(point.ConsumptionRate, food));
        }

        public bool IsEnough(float rate, int food)
        {
            if (food <= 0)
            {
                return true;
            }
            if (rate >= 1f)
            {
                return false;
            }
            return (this.StartArchitecture.Food >= (((float) food) / (1f - rate)));
        }

        public bool IsPointActive(Point p)
        {
            foreach (RoutePoint point in this.RoutePoints)
            {
                if ((point.Position == p) && (point.Index <= this.LastActivePointIndex))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsPositionPossible(Point p)
        {
            if (Session.Current.Scenario.GetArchitectureByPosition(p) != null)
            {
                return false;
            }
            TerrainDetail terrainDetailByPosition = Session.Current.Scenario.GetTerrainDetailByPosition(p);
            return ((terrainDetailByPosition != null) && ((this.BelongedFaction.RoutewayWorkForce >= terrainDetailByPosition.RoutewayBuildWorkCost) && (Math.Round((double) (terrainDetailByPosition.RoutewayConsumptionRate + this.LastPoint.ConsumptionRate), 3) < 1.0)));
        }

        public bool IsSupporting(Faction faction)
        {
            return ((this.BelongedFaction != null) && this.BelongedFaction.IsFriendly(faction));
        }

        public bool IsSupporting(Point position)
        {
            return this.RouteArea.ContainsKey(position);
        }

        public void LoadRoutePointsFromString(string points)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = points.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.RoutePoints.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                RoutePoint point = new RoutePoint();
                point.Position = new Point(int.Parse(strArray[i]), int.Parse(strArray[i + 1]));
                this.RoutePoints.AddLast(point);
            }
        }

        public int Refill(Troop troop, float rate)
        {
            if (this.InefficiencyDays > 0)
            {
                rate *= 2f;
            }
            if (rate < 1f)
            {
                rate = 1f - rate;
                int increment = troop.FoodMax - troop.Food;
                if (increment > 0)
                {
                    int num2 = (int) (this.StartArchitecture.Food * rate);
                    if ((num2 + troop.Food) >= troop.FoodCostPerDay)
                    {
                        if (num2 >= increment)
                        {
                            troop.IncreaseFood(increment);
                            this.StartArchitecture.DecreaseFood((int) (((float) increment) / rate));
                            this.BelongedFaction.IncreaseTechniquePoint(increment / 200);
                            this.BelongedFaction.IncreaseReputation(increment / 0x2710);
                            return (int) (increment * rate);
                        }
                        int num3 = (((num2 + troop.Food) / troop.FoodCostPerDay) * troop.FoodCostPerDay) - troop.Food;
                        troop.IncreaseFood(num3);
                        this.StartArchitecture.DecreaseFood((int) (((float) num3) / rate));
                        this.BelongedFaction.IncreaseTechniquePoint(num3 / 200);
                        this.BelongedFaction.IncreaseReputation(num3 / 0x2710);
                        return (int) (num3 * rate);
                    }
                }
            }
            return 0;
        }

        private void RefreshRouteArea()
        {
            this.RouteArea.Clear();
            foreach (RoutePoint point in this.RoutePoints)
            {
                if (point.Index > this.LastActivePointIndex)
                {
                    break;
                }
                this.AddRoutePointArea(point);
            }
        }

        public void RefreshRoutewayPointsData()
        {
            LinkedList<RoutePoint> routePoints = this.RoutePoints;
            this.RoutePoints = new LinkedList<RoutePoint>();
            foreach (RoutePoint point in routePoints)
            {
                this.Extend(point.Position);
            }
            this.RefreshRouteArea();
        }

        public void ReGenerateRoutePointArea()
        {
            foreach (Point point in this.RouteArea.Keys)
            {
                RoutePoint point2 = null;
                this.RouteArea.TryGetValue(point, out point2);
                if (point2 != null)
                {
                    Session.Current.Scenario.MapTileData[point.X, point.Y].RemoveSupplyingRoutePoint(point2);
                }
            }
            this.RouteArea.Clear();
            foreach (RoutePoint point2 in this.RoutePoints)
            {
                if (point2.Index <= this.LastActivePointIndex)
                {
                    this.AddRoutePointArea(point2);
                }
            }
        }

        public void RemoveLastPoint()
        {
            if (this.LastActivePointIndex >= this.LastPoint.Index)
            {
                this.RemoveRoutePointArea(this.LastPoint);
                this.BelongedFaction.RemovePositionInformation(this.LastPoint.Position, Session.GlobalVariables.RoutewayInformationLevel);
            }
            Session.Current.Scenario.MapTileData[this.LastPoint.Position.X, this.LastPoint.Position.Y].RemoveTileRouteway(this);
            this.RoutePoints.RemoveLast();
        }

        private void RemoveRoutePointArea(RoutePoint routePoint)
        {
            GameArea area = GameArea.GetArea(routePoint.Position, this.Radius, true);
            foreach (Point point in area.Area)
            {
                RoutePoint point2 = null;
                this.RouteArea.TryGetValue(point, out point2);
                if (point2 == routePoint)
                {
                    this.RouteArea.Remove(point);
                    Session.Current.Scenario.MapTileData[point.X, point.Y].RemoveSupplyingRoutePoint(routePoint);
                }
            }
        }

        public void ResetRoutePointConsumptionRate(float rate)
        {
            foreach (RoutePoint point in this.RoutePoints)
            {
                point.ConsumptionRate *= rate;
            }
        }

        public void ReverseDirection()
        {
            bool isActive = this.IsActive;
            int lastActivePointIndex = this.LastActivePointIndex;
            this.ShrinkAt(this.FirstPoint.Position);
            Architecture startArchitecture = this.StartArchitecture;
            this.StartArchitecture.Routeways.Remove(this);
            this.StartArchitecture = this.EndArchitecture;
            this.StartArchitecture.Routeways.Add(this);
            this.EndArchitecture = startArchitecture;
            LinkedList<RoutePoint> routePoints = this.RoutePoints;
            this.RoutePoints = new LinkedList<RoutePoint>();
            while (routePoints.Last != null)
            {
                Session.Current.Scenario.MapTileData[routePoints.Last.Value.Position.X, routePoints.Last.Value.Position.Y].RemoveTileRouteway(this);
                this.Extend(routePoints.Last.Value.Position);
                routePoints.RemoveLast();
            }
            this.LastActivePointIndex = lastActivePointIndex;
            if (this.LastActivePointIndex < (this.RoutePoints.Count - 1))
            {
                this.LastActivePointIndex = -1;
            }
            this.Building = false;
            this.RefreshRouteArea();
        }

        public string SaveRoutePointsToString()
        {
            if (this.RoutePoints.Count == 0)
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            foreach (RoutePoint point in this.RoutePoints)
            {
                builder.Append(point.Position.X.ToString() + " " + point.Position.Y.ToString() + " ");
            }
            return builder.ToString();
        }

        public int ShrinkAt(Point p)
        {
            if (!this.IsPointActive(p))
            {
                return 0;
            }
            LinkedListNode<RoutePoint> lastActiveNode = this.LastActiveNode;
            int num = 0;
            while (lastActiveNode != null)
            {
                if (this.LastActivePointIndex >= lastActiveNode.Value.Index)
                {
                    num++;
                    this.RemoveRoutePointArea(lastActiveNode.Value);
                    this.BelongedFaction.RemovePositionInformation(lastActiveNode.Value.Position, Session.GlobalVariables.RoutewayInformationLevel);
                }
                if (lastActiveNode.Value.Position == p)
                {
                    if (this.LastActivePointIndex >= lastActiveNode.Value.Index)
                    {
                        this.LastActivePointIndex = lastActiveNode.Value.Index - 1;
                    }
                    return num;
                }
                lastActiveNode = lastActiveNode.Previous;
            }
            return num;
        }

        [DataMember]
        public bool AvoidWater
        {
            get
            {
                return this.avoidWater;
            }
            set
            {
                this.avoidWater = value;
            }
        }

        [DataMember]
        public bool Building
        {
            get
            {
                return this.building;
            }
            set
            {
                this.building = value;
            }
        }

        public Architecture ByPassHostileArchitecture
        {
            get
            {
                if ((this.StartArchitecture != null) && (this.DestinationArchitecture != null))
                {
                    foreach (RoutePoint point in this.RoutePoints)
                    {
                        foreach (Architecture architecture in Session.Current.Scenario.GetHighViewingArchitecturesByPosition(point.Position))
                        {
                            if (((architecture != this.DestinationArchitecture) && (architecture.BelongedFaction != null)) && !(this.BelongedFaction.IsFriendlyWithoutTruce(architecture.BelongedFaction) || (Session.Current.Scenario.GetDistance(this.StartArchitecture.ArchitectureArea, architecture.ArchitectureArea) >= Session.Current.Scenario.GetDistance(this.StartArchitecture.ArchitectureArea, this.DestinationArchitecture.ArchitectureArea))))
                            {
                                return architecture;
                            }
                        }
                    }
                }
                return null;
            }
        }

        public GameArea CurrentExtendArea
        {
            get
            {
                if (this.RoutePoints.Count == 0)
                {
                    throw new Exception("Empty Routeway Error!");
                }
                GameArea area = new GameArea();
                GameArea area2 = new GameArea();
                area2.AddPoint(this.LastPoint.Position);
                foreach (Point point in area2.GetContactArea(false).Area)
                {
                    if (this.IsPositionPossible(point) && !this.ContainsPoint(point))
                    {
                        area.AddPoint(point);
                    }
                }
                return area;
            }
        }

        public string DisplayName
        {
            get
            {
                if ((this.StartArchitecture != null) && (this.EndArchitecture != null))
                {
                    return ("粮道“" + this.StartArchitecture.Name + "-" + this.EndArchitecture.Name + "”");
                }
                if (this.StartArchitecture != null)
                {
                    return string.Concat(new object[] { "粮道“", this.StartArchitecture.Name, "-", this.LastPoint.Position.X, ",", this.LastPoint.Position.Y, "”" });
                }
                return "粮道";
            }
        }

        public RoutePoint FirstPoint
        {
            get
            {
                if (this.RoutePoints.First != null)
                {
                    return this.RoutePoints.First.Value;
                }
                return null;
            }
        }

        public bool HasSupportingTroop
        {
            get
            {
                foreach (Point point in this.RouteArea.Keys)
                {
                    Troop troopByPosition = Session.Current.Scenario.GetTroopByPosition(point);
                    if (((troopByPosition != null) && this.BelongedFaction.IsFriendlyWithoutTruce(troopByPosition.BelongedFaction)) && this.IsEnough(point, troopByPosition.FoodCostPerDay))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        [DataMember]
        public int InefficiencyDays
        {
            get
            {
                return this.inefficiencyDays;
            }
            set
            {
                this.inefficiencyDays = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return ((this.LastActivePointIndex >= 0) && (this.LastActivePointIndex == (this.RoutePoints.Count - 1)));
            }
        }

        public bool IsInUsing
        {
            get
            {
                this.HasSupportedLegion = false;
                if ((this.StartArchitecture.PlanArchitecture != null) && (this.StartArchitecture.PlanArchitecture == this.DestinationArchitecture))
                {
                    LinkNode node = null;
                    this.StartArchitecture.AIAllLinkNodes.TryGetValue(this.DestinationArchitecture.ID, out node);
                    if (node != null)
                    {
                        float foodRateBySeason = Session.Current.Scenario.Date.GetFoodRateBySeason(Session.Current.Scenario.Date.GetSeason(this.Length));
                        if (((this.StartArchitecture.Food * foodRateBySeason) >= (this.StartArchitecture.FoodCeiling / 3)) || this.StartArchitecture.IsSelfFoodEnough(node, this))
                        {
                            return true;
                        }
                    }
                }
                if (this.BelongedFaction != null)
                {
                    foreach (Legion legion in this.BelongedFaction.Legions)
                    {
                        if ((legion.Troops.Count > 0) && (legion.WillArchitecture != null))
                        {
                            int minTroopFoodCost = legion.GetMinTroopFoodCost();
                            if ((minTroopFoodCost <= this.StartArchitecture.Food) || ((Session.Current.Scenario.Date.Day >= 0x1c) && (minTroopFoodCost <= (this.StartArchitecture.Food + this.StartArchitecture.ExpectedFood))))
                            {
                                if ((legion.PreferredRouteway == this) && ((legion.WillArchitecture.BelongedFaction != this.BelongedFaction) || (legion.WillArchitecture.RecentlyAttacked > 0)))
                                {
                                    this.HasSupportedLegion = true;
                                    return true;
                                }
                                if ((legion.WillArchitecture == this.DestinationArchitecture) && legion.HasMovingTroopStartFromArchitecture(this.StartArchitecture))
                                {
                                    return true;
                                }
                                if ((((legion.Kind == LegionKind.Offensive) && (this.Building || this.IsActive)) && (this.DestinationArchitecture == legion.WillArchitecture)) && this.IsEnough(this.LastPoint.ConsumptionRate, minTroopFoodCost * 30))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                if (this.HasSupportingTroop)
                {
                    return true;
                }
                if (this.StartArchitecture.TransferFoodArchitecture != null)
                {
                    if (this.StartArchitecture.TransferFoodArchitecture.BelongedFaction != this.BelongedFaction)
                    {
                        return false;
                    }
                    if (this.StartArchitecture.TransferFoodArchitecture == this.DestinationArchitecture)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsShowingArea
        {
            get
            {
                return this.ShowArea;
            }
        }

        public LinkedListNode<RoutePoint> LastActiveNode
        {
            get
            {
                if ((this.LastActivePointIndex < 0) || (this.RoutePoints.Count == 0))
                {
                    return null;
                }
                int index = 0;
                LinkedListNode<RoutePoint> first = this.RoutePoints.First;
                while (index < this.LastActivePointIndex)
                {
                    first = first.Next;
                    index = first.Value.Index;
                }
                return first;
            }
        }

        public RoutePoint LastActivePoint
        {
            get
            {
                if ((this.LastActivePointIndex < 0) || (this.RoutePoints.Count == 0))
                {
                    return null;
                }
                int index = 0;
                LinkedListNode<RoutePoint> first = this.RoutePoints.First;
                while (index < this.LastActivePointIndex)
                {
                    first = first.Next;
                    index = first.Value.Index;
                }
                return first.Value;
            }
        }

        [DataMember]
        public int LastActivePointIndex
        {
            get
            {
                return this.lastActivePointIndex;
            }
            set
            {
                this.lastActivePointIndex = value;
            }
        }

        public RoutePoint LastPoint
        {
            get
            {
                if (this.RoutePoints.Last != null)
                {
                    return this.RoutePoints.Last.Value;
                }
                return null;
            }
        }

        public int Length
        {
            get
            {
                return this.RoutePoints.Count;
            }
        }

        public int Radius
        {
            get
            {
                return (1 + this.BelongedFaction.IncrementOfRoutewayRadius);
            }
        }

        [DataMember]
        public bool RemoveAfterClose
        {
            get
            {
                return this.removeAfterClose;
            }
            set
            {
                this.removeAfterClose = value;
            }
        }

        [DataMember]
        public bool ShowArea
        {
            get
            {
                return this.showArea;
            }
            set
            {
                this.showArea = value;
            }
        }
    }
}

