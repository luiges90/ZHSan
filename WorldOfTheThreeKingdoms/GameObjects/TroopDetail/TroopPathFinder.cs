using GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;



namespace GameObjects.TroopDetail
{

    public class TroopPathFinder
    {
        private PathSearcher conflictionPathSearcher;
        private Point CurrentDestination;
        private Point End;
        private TierPathFinder firstTierPathFinder;
        private AreaSearcher movableAreaSearcher;
        private TierPathFinder secondTierPathFinder;
        private TierPathFinder simplePathFinder;
        private Point Start;
        private TierPathFinder thirdTierPathFinder;
        private Troop troop;
        private AreaSearcher troopAreaSearcher;

        public TroopPathFinder(Troop troop)
        {
            this.troop = troop;
            this.firstTierPathFinder = new TierPathFinder();
            this.secondTierPathFinder = new TierPathFinder();
            this.thirdTierPathFinder = new TierPathFinder();
            this.simplePathFinder = new TierPathFinder();
            this.movableAreaSearcher = new AreaSearcher();
            this.troopAreaSearcher = new AreaSearcher();
            this.conflictionPathSearcher = new PathSearcher();
            this.firstTierPathFinder.OnGetCost += new TierPathFinder.GetCost(this.firstTierPathFinder_OnGetCost);
            this.firstTierPathFinder.OnGetPenalizedCost += new TierPathFinder.GetPenalizedCost(this.firstTierPathFinder_OnGetPenalizedCost);
            this.secondTierPathFinder.OnGetCost += new TierPathFinder.GetCost(this.secondTierPathFinder_OnGetCost);
            this.thirdTierPathFinder.OnGetCost += new TierPathFinder.GetCost(this.thirdTierPathFinder_OnGetCost);
            this.simplePathFinder.OnGetCost += new TierPathFinder.GetCost(this.simplePathFinder_OnGetCost);
            this.movableAreaSearcher.OnGetCost += new AreaSearcher.GetCost(this.movableAreaSearcher_OnGetCost);
            this.movableAreaSearcher.OnCompare += new AreaSearcher.Compare(this.movableAreaSearcher_OnCompare);
            this.troopAreaSearcher.OnGetCost += new AreaSearcher.GetCost(this.troopAreaSearcher_OnGetCost);
            this.conflictionPathSearcher.OnCheckPosition += new PathSearcher.CheckPosition(this.conflictionPathSearcher_OnCheckPosition);
        }

        private bool BuildFirstTierPath(Point start, Point end, MilitaryKind kind)
        {
            this.troop.ClearFirstTierPath();
            if (end == this.troop.Destination)
            {
                this.troop.ClearSecondTierPath();
            }
            if (this.firstTierPathFinder.GetPath(start, end, kind))
            {
                this.troop.FirstTierPath = new List<Point>();
                this.firstTierPathFinder.SetPath(this.troop.FirstTierPath);
            }
            else
            {
                this.troop.Destination = this.troop.Position;
            }
            return true;
        }

        private List<Point> BuildFirstTierSimulatePath(Point start, Point end, MilitaryKind kind)
        {
            if (this.firstTierPathFinder.GetPath(start, end, kind))
            {
                List<Point> path = new List<Point>();
                this.firstTierPathFinder.SetPath(path);
                return path;
            }
            return null;
        }

        private bool BuildModifyFirstTierPath(Point start, Point end, List<Point> middlePath, MilitaryKind kind)
        {
            if (this.firstTierPathFinder.GetPath(start, end, kind))
            {
                this.firstTierPathFinder.SetPath(middlePath);
                return true;
            }
            middlePath = null;
            return false;
        }

        private PathResult conflictionPathSearcher_OnCheckPosition(Point position, List<Point> middlePath, MilitaryKind kind)
        {
            TroopList list = new TroopList();
            TroopList list2 = new TroopList();
            foreach (Troop troop in this.troop.BelongedFaction.KnownTroops.Values)
            {
                if (!troop.IsFriendly(this.troop.BelongedFaction))
                {
                    switch (this.troop.HostileAction)
                    {
                        case HostileActionKind.EvadeEffect:
                            if (troop.OffenceArea.HasPoint(position))
                            {
                                list.Add(troop);
                            }
                            break;

                        case HostileActionKind.EvadeView:
                            if (troop.ViewArea.HasPoint(position))
                            {
                                list.Add(troop);
                            }
                            break;
                    }
                }
                else if (troop.Position == position)
                {
                    list2.Add(troop);
                }
            }
            if ((list.Count > 0) || (list2.Count > 0))
            {
                bool flag = false;
                foreach (Troop troop in list)
                {
                    switch (this.troop.HostileAction)
                    {
                        case HostileActionKind.NotCare:
                            this.troop.Scenario.SetPenalizedMapDataByPosition(troop.Position, 0xdac);
                            break;

                        case HostileActionKind.Attack:
                            this.troop.Scenario.SetPenalizedMapDataByPosition(troop.Position, 0xdac);
                            break;

                        case HostileActionKind.EvadeEffect:
                            this.troop.Scenario.SetPenalizedMapDataByArea(troop.OffenceArea, 1);
                            break;

                        case HostileActionKind.EvadeView:
                            this.troop.Scenario.SetPenalizedMapDataByArea(troop.ViewArea, 1);
                            break;
                    }
                }
                /*foreach (Troop troop in list2)
                {
                    switch (this.troop.FriendlyAction)
                    {
                    }
                }*/
                flag = this.ModifyFirstTierPath(this.troop.Position, this.troop.FirstTierDestination, middlePath, kind);
                foreach (Troop troop in list)
                {
                    switch (this.troop.HostileAction)
                    {
                        case HostileActionKind.NotCare:
                            this.troop.Scenario.ClearPenalizedMapDataByPosition(troop.Position);
                            break;

                        case HostileActionKind.Attack:
                            this.troop.Scenario.ClearPenalizedMapDataByPosition(troop.Position);
                            break;

                        case HostileActionKind.EvadeEffect:
                            this.troop.Scenario.ClearPenalizedMapDataByArea(troop.OffenceArea);
                            break;

                        case HostileActionKind.EvadeView:
                            this.troop.Scenario.ClearPenalizedMapDataByArea(troop.ViewArea);
                            break;
                    }
                }
                /*foreach (Troop troop in list2)
                {
                    switch (this.troop.FriendlyAction)
                    {
                    }
                }*/
                if (flag)
                {
                    return PathResult.Found;
                }
                return PathResult.NotFound;
            }
            return PathResult.Aborted;
        }

        public void FindFirstTierPath(Point start, Point end, List<Point> list, MilitaryKind kind)
        {
            this.Start = start;
            this.End = end;
            this.troop.MovabilityLeft = this.troop.Movability;
            if (this.troop.GetPossibleMoveByPosition(end, kind) >= 0xdac)
            {
                if (this.movableAreaSearcher.Search(end, start, GameObjectConsts.FindMovableDestinationMaxCheckCount, kind))
                {
                    if (this.CurrentDestination == this.End)
                    {
                        this.troop.MovabilityLeft = -1;
                        return;
                    }
                    if (this.firstTierPathFinder.GetPath(this.Start, this.CurrentDestination, kind))
                    {
                        this.firstTierPathFinder.SetPath(list);
                    }
                }
                this.troop.MovabilityLeft = -1;
            }
            else
            {
                if (this.firstTierPathFinder.GetPath(this.Start, this.End, kind))
                {
                    this.firstTierPathFinder.SetPath(list);
                }
                this.troop.MovabilityLeft = -1;
            }
        }

        private int firstTierPathFinder_OnGetCost(Point position, bool oblique, int DirectionCost, MilitaryKind kind)
        {
            return this.troop.GetCostByPosition(position, oblique, DirectionCost, kind);
        }

        private int firstTierPathFinder_OnGetPenalizedCost(Point position, MilitaryKind kind)
        {
            if (this.troop.Scenario.PositionOutOfRange(position)) return 0;
            return this.troop.Scenario.PenalizedMapData[position.X, position.Y];
        }

        public GameArea GetDayArea(int Days)
        {
            return this.firstTierPathFinder.GetDayArea(this.troop, Days);
        }

        public bool GetFirstTierPath(Point start, Point end, MilitaryKind kind)
        {
            this.Start = start;
            this.End = end;
            if (this.troop.GetPossibleMoveByPosition(end, kind) >= 0xdac)
            {
                if (this.movableAreaSearcher.Search(end, start, GameObjectConsts.FindMovableDestinationMaxCheckCount, kind))
                {
                    if (this.CurrentDestination == this.End)
                    {
                        this.troop.FirstTierPath = new List<Point>();
                        return true;
                    }
                    return this.BuildFirstTierPath(start, this.CurrentDestination, kind);
                }
                return false;
            }
            return this.BuildFirstTierPath(start, end, kind);
        }

        public List<Point> GetFirstTierSimulatePath(Point start, Point end, MilitaryKind kind)
        {
            this.Start = start;
            this.End = end;
            if (this.troop.GetPossibleMoveByPosition(end, kind) >= 0xdac)
            {
                if (this.movableAreaSearcher.Search(end, start, GameObjectConsts.FindMovableDestinationMaxCheckCount, kind))
                {
                    return this.BuildFirstTierSimulatePath(start, this.CurrentDestination, kind);
                }
                return null;
            }
            return this.BuildFirstTierSimulatePath(start, end, kind);
        }

        public bool GetPath(Point start, Point end, MilitaryKind kind)
        {
            if (Troop.LaunchThirdPathFinder(start, end, kind))
            {
                return this.GetThirdTierPath(start, end, kind);
            }
            if (Troop.LaunchSecondPathFinder(start, end, kind))
            {
                return this.GetSecondTierPath(start, end, kind);
            }
            return this.GetFirstTierPath(start, end, kind);
        }

        public bool GetSecondTierPath(Point start, Point end, MilitaryKind kind)
        {
            this.troop.ClearSecondTierPath();
            if (end == this.troop.Destination)
            {
                this.troop.ClearThirdTierPath();
            }
            Point point = new Point(start.X / GameObjectConsts.SecondTierSquareSize, start.Y / GameObjectConsts.SecondTierSquareSize);
            Point point2 = new Point(end.X / GameObjectConsts.SecondTierSquareSize, end.Y / GameObjectConsts.SecondTierSquareSize);
            this.troop.SecondTierPath = this.troop.BelongedFaction.GetSecondTierKnownPath(point, point2);
            if (this.troop.SecondTierPath == null)
            {
                if (this.secondTierPathFinder.GetPath(point, point2, kind))
                {
                    this.troop.SecondTierPath = new List<Point>();
                    this.secondTierPathFinder.SetPath(this.troop.SecondTierPath);
                    this.troop.BelongedFaction.AddSecondTierKnownPath(this.troop.SecondTierPath);
                    return true;
                }
                this.troop.Destination = this.troop.Position;
                this.troop.ClearThirdTierPath();
            }
            return false;
        }

        public List<Point> GetSimplePath(Point start, Point end, MilitaryKind kind)
        {
            List<Point> path = new List<Point>();
            if (this.simplePathFinder.GetPath(start, end, kind))
            {
                path = new List<Point>();
                this.simplePathFinder.SetPath(path);
                return path;
            }
            return path;
        }

        public bool GetThirdTierPath(Point start, Point end, MilitaryKind kind)
        {
            this.troop.ClearThirdTierPath();
            Point point = new Point(start.X / GameObjectConsts.ThirdTierSquareSize, start.Y / GameObjectConsts.ThirdTierSquareSize);
            Point point2 = new Point(end.X / GameObjectConsts.ThirdTierSquareSize, end.Y / GameObjectConsts.ThirdTierSquareSize);
            this.troop.ThirdTierPath = this.troop.BelongedFaction.GetThirdTierKnownPath(point, point2);
            if (this.troop.ThirdTierPath == null)
            {
                if (this.thirdTierPathFinder.GetPath(point, point2, kind))
                {
                    this.troop.ThirdTierPath = new List<Point>();
                    this.thirdTierPathFinder.SetPath(this.troop.ThirdTierPath);
                    this.troop.BelongedFaction.AddThirdTierKnownPath(this.troop.ThirdTierPath);
                    return true;
                }
                this.troop.Destination = this.troop.Position;
            }
            return false;
        }

        private bool ModifyFirstTierPath(Point start, Point end, List<Point> middlePath, MilitaryKind kind)
        {
            this.Start = start;
            this.End = end;
            if (this.troop.GetPossibleMoveByPosition(end, kind) >= 0xdac)
            {
                return (this.movableAreaSearcher.Search(end, start, GameObjectConsts.FindMovableDestinationMaxCheckCount, kind) && this.BuildModifyFirstTierPath(start, this.CurrentDestination, middlePath, kind));
            }
            return this.BuildModifyFirstTierPath(start, end, middlePath, kind);
        }

        public bool ModifyPathByConfliction(Troop troop, int StartingIndex)
        {
            return this.conflictionPathSearcher.Search(troop, StartingIndex, troop.ViewRadius);
        }

        private bool movableAreaSearcher_OnCompare(Point position, MilitaryKind kind)
        {
            if (this.troop.GetPossibleMoveByPosition(position, kind) >= 0xdac)
            {
                return false;
            }
            if (this.End == this.troop.Destination)
            {
                this.troop.Destination = position;
            }
            this.CurrentDestination = position;
            return true;
        }

        private int movableAreaSearcher_OnGetCost(Point position, bool oblique, MilitaryKind kind)
        {
            return this.troop.GetCostByPosition(position, false, -1, kind);
        }

        private int secondTierPathFinder_OnGetCost(Point position, bool oblique, int DirectionCost, MilitaryKind kind)
        {
            return this.troop.GetCostBySecondTierPosition(position);
        }

        private int simplePathFinder_OnGetCost(Point position, bool Oblique, int DirectionCost, MilitaryKind kind)
        {
            return 1;
        }

        private int thirdTierPathFinder_OnGetCost(Point position, bool oblique, int DirectionCost, MilitaryKind kind)
        {
            return this.troop.GetCostByThirdTierPosition(position);
        }

        private int troopAreaSearcher_OnGetCost(Point position, bool oblique, MilitaryKind kind)
        {
            return this.troop.GetCostByPosition(position, oblique, -1, kind);
        }
    }
}

