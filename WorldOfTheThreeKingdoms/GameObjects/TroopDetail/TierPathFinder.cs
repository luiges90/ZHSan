using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;



namespace GameObjects.TroopDetail
{
    public class TierPathFinder
    {
        //private int BottomSquareCost;
        //private int LeftSquareCost;
        //private Dictionary<Point, GameSquare> openDictionary = new Dictionary<Point, GameSquare>();
        //private int RightSquareCost;
        //private int TopSquareCost;
        //private Point endPoint;
        private Dictionary<Point, GameSquare> openDictionary = new Dictionary<Point, GameSquare>();
        private SortedList<int, GameSquare> openList = new SortedList<int, GameSquare>();
        private Dictionary<Point, GameSquare> closeDictionary = new Dictionary<Point, GameSquare>();
        private List<GameSquare> closeList = new List<GameSquare>();
        private List<Point> lastPath = new List<Point>();
        
        public event GetCost OnGetCost;

        public event GetPenalizedCost OnGetPenalizedCost;

        private int distance(Point a, Point b)
        {
            return (Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y)) * 30; // 25 = 5 * 5 (5 for the map cost and 5 for Movability multiplier) (a little overestimate is better, use 30)            
        }

        private GameSquare AddToCloseList()
        {
            GameSquare square = this.RemoveFromOpenList();
            closeList.Add(square);
            closeDictionary.Add(square.Position, square);
            return square;
        }

        private void AddToCloseList(GameSquare square)
        {
            closeList.Add(square);
            closeDictionary.Add(square.Position, square);
        }

        private void AddToOpenList(GameSquare square, bool useAStar)
        {
            int key = 0;
            if (useAStar) // A* search
                key = square.F * 160000 + (square.Position.X * 400 + square.Position.Y); // to break tie because Dictionary doesn't allow duplicate keys (only need square.F)
            else          // least cost first search
                key = square.G * 160000 + (square.Position.X * 400 + square.Position.Y);
            openList.Add(key, square);
            openDictionary.Add(square.Position, square);
        }

        private void CheckAdjacentSquares(GameSquare currentSquare, Point end, bool useAStar, MilitaryKind kind)
        {
            int leftSquareCost = this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), end, -1, useAStar, kind);
            int topSquareCost = this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), end, -1, useAStar, kind);
            int rightSquareCost = this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), end, -1, useAStar, kind);
            int bottomSquareCost = this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), end, -1, useAStar, kind);
            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), end, (topSquareCost > leftSquareCost) ? topSquareCost : leftSquareCost, useAStar, kind);
            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), end, (bottomSquareCost > leftSquareCost) ? bottomSquareCost : leftSquareCost, useAStar, kind);
            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), end, (topSquareCost > rightSquareCost) ? topSquareCost : rightSquareCost, useAStar, kind);
            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), end, (bottomSquareCost > rightSquareCost) ? bottomSquareCost : rightSquareCost, useAStar, kind);
        }

        private int GetCostByPosition(Point position, bool oblique, int DirectionCost, MilitaryKind kind)
        {
            if (this.OnGetCost != null)
            {
                return this.OnGetCost(position, oblique, DirectionCost, kind);
            }
            return 0xdac;
        }
        
        private void saveLastPath() {
            lastPath.Clear();
            List<Point> list = new List<Point>();
            GameSquare parent = this.closeList[this.closeList.Count - 1];
            do
            {
                list.Add(parent.Position);
                parent = parent.Parent;
            }
            while (parent != null);
            for (int i = 1; i <= list.Count; i++)
            {
                lastPath.Add(list[list.Count - i]);
            }
        }

        public GameArea GetDayArea(Troop troop, int Days)
        {
            GameArea area = new GameArea();
            openDictionary.Clear();
            openList.Clear();
            closeDictionary.Clear();
            closeList.Clear();
            GameSquare square = new GameSquare();
            square.Position = troop.Position;
            this.AddToCloseList(square);
            int num = troop.Movability * Days;
            int movabilityLeft = troop.MovabilityLeft;
            //int num3 = troop.RealMovability * Days;
            troop.MovabilityLeft = num;
            MilitaryKind kind = troop.Army.Kind;
            do
            {
                CheckAdjacentSquares(square, troop.Position, false, kind);
                if (this.openList.Count == 0)
                {
                    break;
                }
                square = this.AddToCloseList();
                if (square == null)
                {
                    break;
                }
                if (num >= square.G)
                {
                    if (!Session.Current.Scenario.PositionIsTroop(square.Position))
                    {
                        area.AddPoint(square.Position);
                    }
                }
                else
                {
                    break;
                }
                if (closeList.Count > 2500 || closeDictionary.Count > 2500) break;
            } while (true);
            troop.MovabilityLeft = movabilityLeft;
            saveLastPath();
            openDictionary.Clear();
            openList.Clear();
            closeDictionary.Clear();
            closeList.Clear();
            return area;
        }

        public bool GetPath(Point start, Point end, MilitaryKind kind)
        {
            bool flag = false;
            openDictionary.Clear();
            openList.Clear();
            closeDictionary.Clear();
            closeList.Clear();
            GameSquare square = new GameSquare();
            square.Position = start;
            this.AddToCloseList(square);
            if (start == end)
            {
                lastPath = new List<Point>();
                lastPath.Add(start);
                return true;
            }
            do
            {
                CheckAdjacentSquares(square, end, true, kind);
                if (this.openList.Count == 0)
                {
                    break;
                }
                square = this.AddToCloseList();
                if (square == null)
                {
                    break;
                }
                flag = square.Position == end;
                if (closeList.Count > 2500 || closeDictionary.Count > 2500) break;
            }
            while (!flag && (square.RealG < 0xdac));
            saveLastPath();
            openDictionary.Clear();
            openList.Clear();
            closeDictionary.Clear();
            closeList.Clear();
            return flag;
        }

        private int GetPenalizedCostByPosition(Point position, MilitaryKind kind)
        {
            if (this.OnGetPenalizedCost != null)
            {
                return this.OnGetPenalizedCost(position, kind);
            }
            return 0;
        }

        private GameSquare GetSquareFromOpenList(Point position)
        {
            if (IsInOpenList(position))
            {
                return openDictionary[position];
            }
            return null;
        }

        private bool IsInCloseList(Point position)
        {
            return closeDictionary.ContainsKey(position);
        }

        private bool IsInOpenList(Point position)
        {
            return openDictionary.ContainsKey(position);
        }

        private int MakeSquare(GameSquare currentSquare, bool oblique, Point position, Point end, int DirectionCost, bool useAStar, MilitaryKind kind)
        {
            int num = this.GetCostByPosition(position, oblique, DirectionCost, kind);
            if (!this.IsInCloseList(position) && (num < 0xdac))
            {
                GameSquare square = new GameSquare();
                int num2;
                if (oblique)
                {
                    num2 = currentSquare.RealG + (7 * num);
                }
                else
                {
                    num2 = currentSquare.RealG + (5 * num);
                }
                GameSquare squareFromOpenList = this.GetSquareFromOpenList(position);
                if (squareFromOpenList == null)
                {
                    square.Parent = currentSquare;
                    square.Position = position;
                    square.PenalizedCost = this.GetPenalizedCostByPosition(position, kind);
                    if (useAStar)
                        square.H = distance(position, end);
                    square.RealG = num2;
                    this.AddToOpenList(square, useAStar);
                }
                else if (num2 < squareFromOpenList.RealG)
                {
                    openDictionary.Remove(position);
                    if (useAStar)
                        openList.Remove(squareFromOpenList.F * 160000 + (squareFromOpenList.Position.X * 400 + squareFromOpenList.Position.Y));
                    else
                        openList.Remove(squareFromOpenList.G * 160000 + (squareFromOpenList.Position.X * 400 + squareFromOpenList.Position.Y));
                    square.Parent = currentSquare;
                    square.Position = position;
                    square.PenalizedCost = this.GetPenalizedCostByPosition(position, kind);
                    if (useAStar)
                        square.H = distance(position, end);
                    square.RealG = num2;
                    this.AddToOpenList(square, useAStar);
                }
            }
            return num;
        }

        private GameSquare RemoveFromOpenList()
        {
            if (openDictionary.Count <= 0)
            {
                return null;
            }
            GameSquare square = openList.Values[0];
            openList.RemoveAt(0);
            openDictionary.Remove(square.Position);
            return square;
        }

        public void SetPath(List<Point> path)
        {
            path.Clear();
            foreach (Point p in lastPath)
            {
                path.Add(new Point(p.X, p.Y));
            }
        }

        public delegate int GetCost(Point position, bool Oblique, int DirectionCost, MilitaryKind kind);

        public delegate int GetPenalizedCost(Point position, MilitaryKind kind);
    }
}

