using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;



namespace GameObjects.MapDetail
{

    public class RoutewayPathFinder
    {
        //private int BottomSquareCost;
        //private int LeftSquareCost;
        //private int RightSquareCost;
        //private int TopSquareCost;
        public float ConsumptionMax = 0.7f;
        public bool MultipleWaterCost;
        public bool MustUseWater;
        public Architecture startingArchitecture, targetArchitecture;

        private Dictionary<Point, RoutewaySquare> closeDictionary = new Dictionary<Point, RoutewaySquare>();
        private List<RoutewaySquare> closeList = new List<RoutewaySquare>();
        private Dictionary<Point, RoutewaySquare> openDictionary = new Dictionary<Point, RoutewaySquare>();
        private SortedList<int, RoutewaySquare> openList = new SortedList<int, RoutewaySquare>();

        public event GetCost OnGetCost;

        public event GetPenalizedCost OnGetPenalizedCost;

        private int distance(Point a, Point b)
        {
            return (Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y)) * 200; // 200 = 40 * 5 (40 for the map cost（平原粮道工作量） and 5 for Movability multiplier)
        }

        private RoutewaySquare AddToCloseList()
        {
            RoutewaySquare square = this.RemoveFromOpenList();
            closeList.Add(square);
            closeDictionary.Add(square.Position, square);            
            return square;
        }

        private void AddToCloseList(RoutewaySquare square)
        {
            closeList.Add(square);
            closeDictionary.Add(square.Position, square);
        }

        private void AddToOpenList(RoutewaySquare square)
        {
            int key = square.F * 160000 + (square.Position.X * 400 + square.Position.Y); // to break tie because Dictionary doesn't allow duplicate keys (only need square.F)
            openList.Add(key, square);
            openDictionary.Add(square.Position, square);
        }

        private void CheckAdjacentSquares(RoutewaySquare currentSquare, Point end)
        {
            this.MakeSquare(currentSquare, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), end);
            this.MakeSquare(currentSquare, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), end);
            this.MakeSquare(currentSquare, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), end);
            this.MakeSquare(currentSquare, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), end);
        }

        private int GetCostByPosition(Point position, out float consumptionRate)
        {
            if (this.OnGetCost != null)
            {
                return this.OnGetCost(position, out consumptionRate);
            }
            consumptionRate = 0f;
            return 0x3e8;
        }

        public bool GetPath(Point start, Point end, bool hasEnd)
        {
            bool flag = false;
            openDictionary.Clear();
            openList.Clear();
            closeDictionary.Clear();
            closeList.Clear();
            RoutewaySquare square = new RoutewaySquare();
            square.Position = start;
            this.AddToCloseList(square);
            if (start == end)
            {
                return true;
            }
            do
            {
                this.CheckAdjacentSquares(square, end);
                if (this.openDictionary.Count == 0)
                {
                    openDictionary = new Dictionary<Point, RoutewaySquare>();
                    closeDictionary = new Dictionary<Point, RoutewaySquare>();
                    return flag;
                }
                square = this.AddToCloseList();
                if (square == null)
                {
                    openDictionary = new Dictionary<Point, RoutewaySquare>();
                    closeDictionary = new Dictionary<Point, RoutewaySquare>();
                    return flag;
                }
                flag = square.Position == end;
                if (square.ConsumptionRate >= this.ConsumptionMax)
                {
                    if ((this.closeList.Count <= 1) || hasEnd)
                    {
                        openDictionary = new Dictionary<Point, RoutewaySquare>();
                        closeDictionary = new Dictionary<Point, RoutewaySquare>();
                        return flag;
                    }
                    //this.closeList.RemoveAt(this.closeList.Count - 1);
                    flag = true;
                }
            } while (!flag);

            // throw away the old dictionary to free up memory
            openDictionary = new Dictionary<Point, RoutewaySquare>();
            closeDictionary = new Dictionary<Point, RoutewaySquare>();
            return flag;
        }

        private int GetPenalizedCostByPosition(Point position)
        {
            if (this.OnGetPenalizedCost != null)
            {
                return this.OnGetPenalizedCost(position);
            }
            return 0;
        }

        private RoutewaySquare GetSquareFromOpenList(Point position)
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

        private int MakeSquare(RoutewaySquare currentSquare, Point position, Point end)
        {
            float consumptionRate = 0f;
            int costByPosition = this.GetCostByPosition(position, out consumptionRate);
            if (!this.IsInCloseList(position) && (costByPosition < 0x3e8))
            {
                RoutewaySquare square = new RoutewaySquare();
                int num2 = currentSquare.RealG + (5 * costByPosition);
                RoutewaySquare squareFromOpenList = this.GetSquareFromOpenList(position);
                if (squareFromOpenList == null)
                {
                    square.Parent = currentSquare;
                    square.Position = position;
                    square.PenalizedCost = this.GetPenalizedCostByPosition(position);
                    square.H = distance(position, end);
                    square.RealG = num2;
                    square.ConsumptionRate = currentSquare.ConsumptionRate + consumptionRate;
                    this.AddToOpenList(square);
                }
                else if (num2 < squareFromOpenList.RealG)
                {
                    openDictionary.Remove(position);
                    openList.Remove(squareFromOpenList.F * 160000 + (squareFromOpenList.Position.X * 400 + squareFromOpenList.Position.Y));
                    square.Parent = currentSquare;
                    square.Position = position;
                    square.PenalizedCost = this.GetPenalizedCostByPosition(position);
                    square.H = distance(position, end);
                    square.RealG = num2;
                    square.ConsumptionRate = currentSquare.ConsumptionRate + consumptionRate;
                    this.AddToOpenList(square);
                }
            }
            return costByPosition;
        }

        private RoutewaySquare RemoveFromOpenList()
        {
            if (openDictionary.Count <= 0)
            {
                return null;
            }
            RoutewaySquare square = openList.Values[0];
            openList.RemoveAt(0);
            openDictionary.Remove(square.Position);
            return square;
        }

        public void SetPath(List<Point> path)
        {
            path.Clear();
            List<Point> list = new List<Point>();
            RoutewaySquare parent = this.closeList[this.closeList.Count - 1];
            do
            {
                list.Add(parent.Position);
                parent = parent.Parent;
            }
            while (parent != null);
            for (int i = 1; i <= list.Count; i++)
            {
                path.Add(list[list.Count - i]);
            }
        }

        /*private void SwapSquare(int x, int y, List<RoutewaySquare> list)
        {
            RoutewaySquare square = list[x];
            list[x] = list[y];
            list[y] = square;
            list[x].Index = x;
            list[y].Index = y;
        }*/

        /*private void UpResortOpenList(RoutewaySquare square, int index)
        {
            if (index != 0)
            {
                int x = index;
                int y = (x - 1) / 2;
                while (true)
                {
                    if (this.openList[x].F >= this.openList[y].F)
                    {
                        return;
                    }
                    this.SwapSquare(x, y, this.openList);
                    if (y == 0)
                    {
                        return;
                    }
                    x = y;
                    y = (x - 1) / 2;
                }
            }
        }*/

        public float PathConsumptionRate
        {
            get
            {
                if (this.closeList.Count > 0)
                {
                    return this.closeList[this.closeList.Count - 1].ConsumptionRate;
                }
                return 0f;
            }
        }

        public delegate int GetCost(Point position, out float consumptionRate);

        public delegate int GetPenalizedCost(Point position);
    }
}

