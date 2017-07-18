using GameObjects.MapDetail;
using GameObjects.ArchitectureDetail;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class GameArea
    {
        [DataMember]
        public List<Point> Area = new List<Point>();
        [DataMember]
        public Point Centre;
        private Point? topleft = null;

        public void AddPoint(Point point)
        {
            this.Area.Add(point);
            this.topleft = null;
        }

        private static void CheckPoint(GameArea Area, List<Point> BlackAngles, Point point, GameScenario Scenario, Faction faction)
        {
            TerrainDetail terrainDetailByPosition = Scenario.GetTerrainDetailByPosition(point);
            if (terrainDetailByPosition != null)
            {
                if (terrainDetailByPosition.ViewThrough)
                {
                    if (faction != null)
                    {
                        Architecture architectureByPosition = Scenario.GetArchitectureByPosition(point);
                        if (!(architectureByPosition == null || architectureByPosition.Endurance <= 0 || faction.IsFriendlyWithoutTruce(architectureByPosition.BelongedFaction)))
                        {
                            BlackAngles.Add(point);
                            return;
                        }
                    }
                    if (!IsInBlackAngle(Area.Centre, BlackAngles, point))
                    {
                        Area.AddPoint(point);
                    }
                }
                else
                {
                    BlackAngles.Add(point);
                }
            }
        }

        public void CombineArea(GameArea AreaToCombine, Dictionary<Point, object> ClosedList)
        {
            foreach (Point point in AreaToCombine.Area)
            {
                if (!ClosedList.ContainsKey(point))
                {
                    ClosedList.Add(point, null);
                }
            }
        }

        public bool Contact(Point p, bool oblique)
        {
            return this.GetContactArea(oblique).HasPoint(p);
        }

        public override bool Equals(object obj)
        {
            if (obj is GameArea)
            {
                if (this.Centre != (obj as GameArea).Centre)
                {
                    return false;
                }
                Dictionary<Point, object> dictionary = new Dictionary<Point, object>();
                foreach (Point point in this.Area)
                {
                    dictionary.Add(point, null);
                }
                foreach (Point point in (obj as GameArea).Area)
                {
                    if (!dictionary.ContainsKey(point))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

        private static double GetAngle(Point centre, Point point)
        {
            int num = point.X - centre.X;
            int num2 = point.Y - centre.Y;
            return Math.Asin(((double) num2) / Math.Sqrt((double) ((num * num) + (num2 * num2))));
        }

        public static GameArea GetArea(Point Centre, int Radius, bool Oblique)
        {
            GameArea area = new GameArea();
            List<float> list = new List<float>();
            area.Centre = Centre;
            if (Oblique)
            {
                for (int i = Centre.X - Radius; i <= Centre.X + Radius; i++)
                {
                    for (int j = Centre.Y - Radius; j <= Centre.Y + Radius; j++)
                    {
                        area.AddPoint(new Point(i, j));
                    }
                }
            }
            else
            {
                for (int i = -Radius; i <= Radius; i++)
                {
                    if (i <= 0)
                    {
                        for (int j = -Radius - i; j <= i + Radius; j++)
                        {
                            area.AddPoint(new Point(Centre.X + i, Centre.Y + j));
                        }
                    }
                    else
                    {
                        for (int j = i - Radius; j <= Radius - i; j++)
                        {
                            area.AddPoint(new Point(Centre.X + i, Centre.Y + j));
                        }
                    }
                }
            }
            return area;
        }

        public static GameArea GetAreaFromArea(GameArea area, int radius, bool oblique, GameScenario Scenario, Faction faction)
        {
            /*int longRadius;
            if (area.Count <= 1)
                longRadius = radius;
            else if (area.Count <= 5)
                longRadius = radius + 1;
            else
                longRadius = radius + 2;
            GameArea candidateArea = GetArea(area.Centre, longRadius, oblique);
            if (longRadius >= (radius + 1))
            {
                candidateArea.Area.Remove(new Point(area.Centre.X - longRadius, area.Centre.Y - longRadius));
                candidateArea.Area.Remove(new Point(area.Centre.X - longRadius, area.Centre.Y + longRadius));
                candidateArea.Area.Remove(new Point(area.Centre.X + longRadius, area.Centre.Y - longRadius));
                candidateArea.Area.Remove(new Point(area.Centre.X + longRadius, area.Centre.Y + longRadius));
            }
            if (longRadius >= (radius + 2))
            {
                candidateArea.Area.Remove(new Point(area.Centre.X - longRadius + 1, area.Centre.Y - longRadius));
                candidateArea.Area.Remove(new Point(area.Centre.X - longRadius, area.Centre.Y - longRadius + 1));
                candidateArea.Area.Remove(new Point(area.Centre.X - longRadius + 1, area.Centre.Y + longRadius));
                candidateArea.Area.Remove(new Point(area.Centre.X - longRadius, area.Centre.Y + longRadius - 1));
                candidateArea.Area.Remove(new Point(area.Centre.X + longRadius - 1, area.Centre.Y - longRadius));
                candidateArea.Area.Remove(new Point(area.Centre.X + longRadius, area.Centre.Y - longRadius + 1));
                candidateArea.Area.Remove(new Point(area.Centre.X + longRadius - 1, area.Centre.Y + longRadius));
                candidateArea.Area.Remove(new Point(area.Centre.X + longRadius, area.Centre.Y + longRadius - 1));
            }
            return candidateArea;*/
            Dictionary<Point, object> closedList = new Dictionary<Point, object>();
            GameArea area2 = new GameArea();
            foreach (Point point in area.Area)
            {
                area2.CombineArea(GetViewArea(point, radius, oblique, Scenario, faction), closedList);
            }
            foreach (Point point in closedList.Keys)
            {
                area2.Area.Add(point);
            }
            return area2;
        }

        public GameArea GetContactArea(bool oblique)
        {
            return GetContactArea(oblique, null, false, false);
        }

        public GameArea GetContactArea(bool oblique, GameScenario scen, bool waterOnly, bool landOnly)
        {
            GameArea area = new GameArea();
            Dictionary<Point, object> dictionary = new Dictionary<Point, object>();
            foreach (Point point2 in this.Area)
            {
                bool ok = true;
                if (waterOnly && scen.GetTerrainDetailByPositionNoCheck(point2).ID != 6)
                {
                    ok = false;
                }
                if (landOnly && scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6)
                {
                    ok = false;
                }
                if (ok)
                {
                    dictionary.Add(point2, null);
                }
            }
            foreach (Point point2 in this.Area)
            {
                Point key = new Point(point2.X - 1, point2.Y);
                if (!dictionary.ContainsKey(key) && (!waterOnly || scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6))
                {
                    dictionary.Add(key, null);
                    area.AddPoint(key);
                }
                key = new Point(point2.X + 1, point2.Y);
                if (!dictionary.ContainsKey(key) && (!waterOnly || scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6))
                {
                    dictionary.Add(key, null);
                    area.AddPoint(key);
                }
                key = new Point(point2.X, point2.Y - 1);
                if (!dictionary.ContainsKey(key) && (!waterOnly || scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6))
                {
                    dictionary.Add(key, null);
                    area.AddPoint(key);
                }
                key = new Point(point2.X, point2.Y + 1);
                if (!dictionary.ContainsKey(key) && (!waterOnly || scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6))
                {
                    dictionary.Add(key, null);
                    area.AddPoint(key);
                }
                if (oblique)
                {
                    key = new Point(point2.X - 1, point2.Y - 1);
                    if (!dictionary.ContainsKey(key) && (!waterOnly || scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6))
                    {
                        dictionary.Add(key, null);
                        area.AddPoint(key);
                    }
                    key = new Point(point2.X + 1, point2.Y - 1);
                    if (!dictionary.ContainsKey(key) && (!waterOnly || scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6))
                    {
                        dictionary.Add(key, null);
                        area.AddPoint(key);
                    }
                    key = new Point(point2.X - 1, point2.Y + 1);
                    if (!dictionary.ContainsKey(key) && (!waterOnly || scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6))
                    {
                        dictionary.Add(key, null);
                        area.AddPoint(key);
                    }
                    key = new Point(point2.X + 1, point2.Y + 1);
                    if (!dictionary.ContainsKey(key) && (!waterOnly || scen.GetTerrainDetailByPositionNoCheck(point2).ID == 6))
                    {
                        dictionary.Add(key, null);
                        area.AddPoint(key);
                    }
                }
            }
            return area;
        }

        public int GetPointIndex(Point point)
        {
            for (int i = 0; i < this.Area.Count; i++)
            {
                if (this.Area[i] == point)
                {
                    return i;
                }
            }
            return -1;
        }

        public static GameArea GetViewArea(Point Centre, int Radius, bool Oblique, GameScenario Scenario, Faction faction)
        {
            GameArea area = new GameArea();
            List<Point> blackAngles = new List<Point>();
            area.Centre = Centre;
            if (Radius < 0)
                return area;
            if (Radius == 0)
            {
                area.AddPoint(Centre);
                return area;
            }
            for (int i = 0; i <= Radius; i++)
            {
                int num2;
                if (i == 0)
                {
                    num2 = 0;
                    while (num2 <= Radius)
                    {
                        if (num2 != 0)
                        {
                            CheckPoint(area, blackAngles, new Point(Centre.X, Centre.Y + num2), Scenario, faction);
                            CheckPoint(area, blackAngles, new Point(Centre.X, Centre.Y - num2), Scenario, faction);
                        }
                        else
                        {
                            area.AddPoint(Centre);
                        }
                        num2++;
                    }
                }
                else
                {
                    int num3 = i;
                    if (Oblique)
                    {
                        num3 = 0;
                    }
                    num2 = 0;
                    while (num2 <= (Radius - num3))
                    {
                        CheckPoint(area, blackAngles, new Point(Centre.X + i, Centre.Y + num2), Scenario, faction);
                        if (num2 != 0)
                        {
                            CheckPoint(area, blackAngles, new Point(Centre.X + i, Centre.Y - num2), Scenario, faction);
                        }
                        num2++;
                    }
                    for (num2 = 0; num2 <= (Radius - num3); num2++)
                    {
                        CheckPoint(area, blackAngles, new Point(Centre.X - i, Centre.Y + num2), Scenario, faction);
                        if (num2 != 0)
                        {
                            CheckPoint(area, blackAngles, new Point(Centre.X - i, Centre.Y - num2), Scenario, faction);
                        }
                    }
                }
            }
            
            return area;
        }

        public bool HasPoint(Point point)
        {
            foreach (Point point2 in this.Area)
            {
                if (point2 == point)
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsInBlackAngle(Point centre, List<Point> BlackAngles, Point anglePoint)
        {
            foreach (Point point in BlackAngles)
            {
                int num = point.X - centre.X;
                int num2 = point.Y - centre.Y;
                double num3 = 3.1415926535897931 / (5.0 * Math.Sqrt((double) ((num * num) + (num2 * num2))));
                double angle = GetAngle(centre, point);
                double num5 = GetAngle(centre, anglePoint);
                if (IsInSameRegion(centre, point, anglePoint) && (Math.Round(Math.Abs((double) (angle - num5)), 5) <= Math.Round(num3, 5)))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsInSameRegion(Point centre, Point point1, Point point2)
        {
            return ((((point1.X - centre.X) * (point2.X - centre.X)) >= 0) && (((point1.Y - centre.Y) * (point2.Y - centre.Y)) >= 0));
        }

        public void MoveArea(int XOffset, int YOffset)
        {
            if ((XOffset != 0) || (YOffset != 0))
            {
                this.Centre.X += XOffset;
                this.Centre.Y += YOffset;
                for (int i = 0; i < this.Count; i++)
                {
                    this.Area[i] = new Point(this.Area[i].X + XOffset, this.Area[i].Y + YOffset);
                }
                this.topleft = null;
            }
        }

        public void RemoveArea(GameArea area)
        {
            foreach (Point point in area.Area)
            {
                this.Area.Remove(point);
            }
        }

        public void RemovePoints(List<Point> points)
        {
            foreach (Point point in points)
            {
                this.Area.Remove(point);
            }
        }

        private void ResetTopLeft()
        {
            if (this.Area.Count == 0)
            {
                this.topleft = null;
            }
            else if (this.Area.Count == 1)
            {
                this.topleft = new Point?(this.Area[0]);
            }
            else
            {
                int num = this.Area[0].X * this.Area[0].Y;
                int num2 = 0;
                int num3 = 0;
                for (int i = 1; i < this.Area.Count; i++)
                {
                    num3 = this.Area[i].X * this.Area[i].Y;
                    if (num3 < num)
                    {
                        num2 = i;
                        num = num3;
                    }
                }
                this.topleft = new Point?(this.Area[num2]);
            }
        }

        public int Count
        {
            get
            {
                return this.Area.Count;
            }
        }

        public Point this[int index]
        {
            get
            {
                return this.Area[index];
            }
            set
            {
                this.Area[index] = value;
            }
        }

        public Point TopLeft
        {
            get
            {
                if (!this.topleft.HasValue)
                {
                    this.ResetTopLeft();
                }
                return this.topleft.Value;
            }
        }


    }
}

