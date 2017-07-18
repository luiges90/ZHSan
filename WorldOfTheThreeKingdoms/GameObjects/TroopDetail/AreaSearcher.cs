using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;



namespace GameObjects.TroopDetail
{
    public class AreaSearcher
    {
        protected Dictionary<Point, AreaSquare> closeDictionary = new Dictionary<Point, AreaSquare>();
        protected List<AreaSquare> closeList = new List<AreaSquare>();
        private Point directionPosition;
        private bool EnableDirection = false;
        protected Dictionary<Point, AreaSquare> openDictionary = new Dictionary<Point, AreaSquare>();
        protected List<AreaSquare> openList = new List<AreaSquare>();
        private Point startPosition;

        public event Compare OnCompare;

        public event GetCost OnGetCost;

        private AreaSquare AddToCloseList()
        {
            AreaSquare square = this.RemoveFromOpenList();
            if ((square != null) && !this.IsInCloseList(square.Position))
            {
                this.closeDictionary.Add(square.Position, square);
                this.closeList.Add(square);
            }
            return square;
        }

        private void AddToCloseList(AreaSquare square)
        {
            if (!this.closeDictionary.ContainsKey(square.Position))
            {
                this.closeList.Add(square);
                this.closeDictionary.Add(square.Position, square);
            }
        }

        private void AddToOpenList(AreaSquare square)
        {
            this.openList.Add(square);
            int x = this.openList.Count - 1;
            square.Index = x;
            for (int i = (x - 1) / 2; this.openList[x].F < this.openList[i].F; i = (x - 1) / 2)
            {
                this.SwapSquare(x, i, this.openList);
                if (i == 0)
                {
                    break;
                }
                x = i;
            }
            if (!this.openDictionary.ContainsKey(square.Position))
            {
                this.openDictionary.Add(square.Position, square);
            }
        }

        private void CheckAdjacentSquares(AreaSquare currentSquare, MilitaryKind kind)
        {
            int num;
            int num2;
            if (this.EnableDirection)
            {
                int num3 = this.directionPosition.X - currentSquare.Position.X;
                int num4 = this.directionPosition.Y - currentSquare.Position.Y;
                num = (num3 > 0) ? 1 : -1;
                num2 = (num4 > 0) ? 1 : -1;
                if (num4 == 0)
                {
                    num2 = 0;
                }
                else if (Math.Abs((decimal) (num3 / num4)) > 2M)
                {
                    num2 = 0;
                }
                if (num3 == 0)
                {
                    num = 0;
                }
                else if (Math.Abs((decimal) (num4 / num3)) > 2M)
                {
                    num = 0;
                }
            }
            else
            {
                num = 0;
                num2 = 0;
            }
            switch (num)
            {
                case -1:
                    switch (num2)
                    {
                        case -1:
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            break;

                        case 0:
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            break;

                        case 1:
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            break;
                    }
                    break;

                case 0:
                    switch (num2)
                    {
                        case -1:
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            break;

                        case 0:
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            break;

                        case 1:
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            break;
                    }
                    break;

                case 1:
                    switch (num2)
                    {
                        case -1:
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            break;

                        case 0:
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            break;

                        case 1:
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y), kind);
                            this.MakeSquare(currentSquare, false, new Point(currentSquare.Position.X, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y + 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X + 1, currentSquare.Position.Y - 1), kind);
                            this.MakeSquare(currentSquare, true, new Point(currentSquare.Position.X - 1, currentSquare.Position.Y - 1), kind);
                            break;
                    }
                    break;
            }
        }

        private int GetCostByPosition(Point position, bool oblique, MilitaryKind kind)
        {
            if (this.OnGetCost != null)
            {
                return this.OnGetCost(position, oblique, kind);
            }
            return 0xdac;
        }

        private AreaSquare GetSquareFromOpenList(Point position)
        {
            if (this.openDictionary.ContainsKey(position))
            {
                return this.openDictionary[position];
            }
            return null;
        }

        private bool IsInCloseList(Point position)
        {
            return this.closeDictionary.ContainsKey(position);
        }

        private bool IsInOpenList(Point position)
        {
            return this.openDictionary.ContainsKey(position);
        }

        private void MakeSquare(AreaSquare currentSquare, bool oblique, Point position, MilitaryKind kind)
        {
            if (!this.IsInCloseList(position))
            {
                AreaSquare square2;
                int costByPosition = this.GetCostByPosition(position, oblique, kind);
                if (costByPosition < 0xdac)
                {
                    int num2;
                    if (oblique)
                    {
                        num2 = currentSquare.G + (7 * costByPosition);
                    }
                    else
                    {
                        num2 = currentSquare.G + (5 * costByPosition);
                    }
                    AreaSquare squareFromOpenList = this.GetSquareFromOpenList(position);
                    if (squareFromOpenList == null)
                    {
                        square2 = new AreaSquare();
                        square2.Parent = currentSquare;
                        square2.Position = position;
                        square2.G = num2;
                        square2.H = (((Math.Abs((int) (this.startPosition.X - position.X)) + Math.Abs((int) (this.startPosition.Y - position.Y))) + Math.Abs((int) (position.X - this.directionPosition.X))) + Math.Abs((int) (position.Y - this.directionPosition.Y))) * 5;
                        this.AddToOpenList(square2);
                    }
                    else if (num2 > squareFromOpenList.G)
                    {
                        squareFromOpenList.Parent = currentSquare;
                        squareFromOpenList.G = num2;
                        this.UpResortOpenList(squareFromOpenList, squareFromOpenList.Index);
                    }
                }
                else
                {
                    square2 = new AreaSquare();
                    square2.Position = position;
                    if (oblique)
                    {
                        square2.G = currentSquare.G + (7 * costByPosition);
                    }
                    else
                    {
                        square2.G = currentSquare.G + (5 * costByPosition);
                    }
                    this.AddToOpenList(square2);
                }
            }
        }

        private AreaSquare RemoveFromOpenList()
        {
            if (this.openList.Count <= 0)
            {
                return null;
            }
            this.SwapSquare(0, this.openList.Count - 1, this.openList);
            AreaSquare square = this.openList[this.openList.Count - 1];
            square.Index = -1;
            this.openList.RemoveAt(this.openList.Count - 1);
            this.openDictionary.Remove(square.Position);
            int x = 0;
            int y = x;
            while (true)
            {
                if (((x * 2) + 2) < this.openList.Count)
                {
                    if (this.openList[x].F > this.openList[(x * 2) + 1].F)
                    {
                        y = (x * 2) + 1;
                    }
                    if (this.openList[y].F > this.openList[y + 1].F)
                    {
                        y++;
                    }
                }
                else if ((((x * 2) + 1) < this.openList.Count) && (this.openList[x].F > this.openList[(x * 2) + 1].F))
                {
                    y = (x * 2) + 1;
                }
                if (y == x)
                {
                    return square;
                }
                this.SwapSquare(x, y, this.openList);
                x = y;
            }
        }

        public bool Search(Point start, Point direction, int MaxCheckCount, MilitaryKind kind)
        {
            this.startPosition = start;
            this.directionPosition = direction;
            this.EnableDirection = start != direction;
            bool flag = false;
            this.openDictionary.Clear();
            this.closeDictionary.Clear();
            this.openList.Clear();
            this.closeList.Clear();
            AreaSquare square = new AreaSquare();
            square.Position = start;
            this.AddToCloseList(square);
            int num = 0;
            do
            {
                this.CheckAdjacentSquares(square, kind);
                if (this.openList.Count == 0)
                {
                    break;
                }
                square = this.AddToCloseList();
                if (square == null)
                {
                    break;
                }
                if (this.OnCompare != null)
                {
                    flag = this.OnCompare(square.Position, kind);
                }
                else
                {
                    return false;
                }
                num++;
            }
            while (!flag && (num < MaxCheckCount));
            return flag;
        }

        public void SetPath(List<Point> path)
        {
            path.Clear();
            List<Point> list = new List<Point>();
            AreaSquare parent = this.closeList[this.closeList.Count - 1];
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

        private void SwapSquare(int x, int y, List<AreaSquare> list)
        {
            AreaSquare square = list[x];
            list[x] = list[y];
            list[y] = square;
            list[x].Index = x;
            list[y].Index = y;
        }

        private void UpResortOpenList(AreaSquare square, int index)
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
        }

        public delegate bool Compare(Point position, MilitaryKind kind);

        public delegate int GetCost(Point position, bool oblique, MilitaryKind kind);
    }
}

