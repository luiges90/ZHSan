using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using System.Threading;
using Platforms;

namespace Tools
{

    public struct VecDistance
    {
        public Vector2 Vec;
        public float Distance;
    }

    public static class GenericTools
    {
        /// <summary>
        /// 根據时間增長(0-1)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static float GetAlpha(float time, int end)
        {
            try
            {
                float alpha = time * 255;
                if (alpha >= 255) alpha = 254;
                float num = 255 - alpha;
                //255->1
                return Convert.ToByte(num <= end ? end : num);
            }
            catch
            {
                return Convert.ToByte(1);
            }
        }
        /// <summary>
        /// 根據时間增長(0-1)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static float GetAlpha(float time)
        {
            try
            {
                float alpha = time * 255;
                if (alpha >= 255) alpha = 254;
                //255->1
                return Convert.ToByte(255 - alpha);
            }
            catch
            {
                return Convert.ToByte(1);
            }
        }
        /// <summary>
        /// 根據时間減小(0-1)
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static float GetAlpha2(float time)
        {
            try
            {
                float alpha = time * 255;
                if (alpha >= 255) alpha = 255;
                else if (alpha == 0) alpha = 1;
                //1->255
                return Convert.ToByte(alpha);
            }
            catch
            {
                return Convert.ToByte(1);
            }
        }

        public static List<T> GetLast<T>(this List<T> list, int num)
        {
            if (list.Count <= num)
            {
                return list;
            }
            else
            {
                return list.GetListRange(list.Count - num, num);
            }
        }

        /// <summary>
        /// 獲取分頁數據t
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageCount"></param>
        /// <param name="pageid"></param>
        /// <returns></returns>
        public static List<T> GetPageList<T>(List<T> list, string page, int pageSize, ref int pageCount, ref int pageid)
        {
            if (list == null) return null;
            //初始，考慮只有一頁的情況
            List<T> list2 = list;

            //計算總頁數
            pageCount = list.Count / pageSize + 1;
            if (list.Count % pageSize == 0) pageCount--;

            if (!String.IsNullOrEmpty(page))
            {
                pageid = int.Parse(page);

                //頁數大於1頁
                if (pageCount > 1 && list.Count != pageSize)
                {
                    //當前頁小於總頁
                    if (pageid < pageCount)
                    {
                        list2 = list.GetRange((pageid - 1) * pageSize, pageSize);
                    }
                    //當前頁為最後頁
                    else if (pageid == pageCount)
                    {
                        list2 = list.GetRange((pageid - 1) * pageSize, list.Count - (pageid - 1) * pageSize);
                    }
                    else if (list.Count > pageSize)
                    {
                        pageid = 1;
                        list2 = list.GetRange(0, pageSize);
                    }
                }
            }
            //如果數量大於1頁
            else if (list.Count > pageSize)
            {
                pageid = 1;
                list2 = list.GetRange(0, pageSize);
            }
            return list2;
        }
        public static T GetListRandom<T>(List<T> list)
        {
            return list[new Random().Next(0, list.Count)];
        }
        /// <summary>
        /// 獲取隨机對象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="exceptT"></param>
        /// <param name="returnExceptTWhenOne"></param>
        /// <returns></returns>
        public static T GetListRandom<T>(List<T> list, T exceptT, bool returnExceptTWhenOne)
        {
            if (exceptT != null && (list.Count(li => li != null) > 1 || !returnExceptTWhenOne))
            {
                list.Remove(exceptT);
            }
            return list[new Random().Next(0, list.Count)];
        }
        /// <summary>
        /// 獲取隨机對象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="except"></param>
        /// <returns></returns>
        public static T GetListRandom<T>(List<T> list, List<T> except)
        {
            List<T> list1 = (from li in list where !except.Contains(li) select li).ToList();
            if (list1.Count > 0)
            {
                Platform.Sleep(500);
                return list1[new Random().Next(0, list1.Count)];
            }
            else
            {
                return default(T);
            }
        }
        /// <summary>
        /// 在指定List中獲取隨机的指定數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<T> GetRandomList<T>(List<T> list, int count)
        {
            List<T> result = new List<T>();
            Random random = new Random();
            while (result.Count < count && result.Count < list.Count)
            {
                var list2 = list.Where(li => !result.Contains(li)).NullToEmptyList();
                if (list2.Count == 0)
                {
                    break;
                }
                T t = list2[random.Next(0, list2.Count)];
                if (!result.Contains(t))
                {
                    result.Add(t);
                }
            }
            return result;
        }

        /// <summary>
        /// 在指定List中獲取隨机的指定數
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<T> GetRandomList<T>(List<T> list, int count, List<T> must, List<T> except)
        {
            var list2 = list.NullToEmptyList().Where(li => !must.NullToEmptyList().Contains(li) && !except.NullToEmptyList().Contains(li)).NullToEmptyList();
            var others = GetRandomList(list2, count - must.NullToEmptyList().Count);
            return must.NullToEmptyList().Union(others).NullToEmptyList();
        }

        /// <summary>
        /// 區間
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static List<T> GetListRange<T>(this List<T> list, int start, int count)
        {
            if (start < list.Count)
            {
                return list.Count >= (start + count) ? list.GetRange(start, count) : list.GetRange(start, list.Count - start);
            }
            else
            {
                return list;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
        }

        //public static IEnumerable<T> FindAll<T>(this IEnumerable<T> enumeration, Action<T> action)
        //{
        //    foreach (T item in enumeration)
        //    {
        //        action(item);
        //        yield return item;
        //    }
        //}

        public static void ForEach<T>(this List<T> enumeration, Action<T> action)
        {
            var count = enumeration.Count;
            for (int i = 0; i < count; i++) // foreach (T item in enumeration)
            {
                if (i < enumeration.Count)
                {
                    var item = enumeration[i];
                    action(item);
                }
            }
        }

        public static void Remove(this string[] array, string arr)
        {
            if (array != null && array.Length > 0)
            {
                array = array.ToList().Where(ar => ar.Trim() != arr.Trim()).NullToEmptyList().ToArray();
            }
        }

        public static List<T> RemoveNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list.NullToEmptyList().Where(li => li != null && !String.IsNullOrWhiteSpace(li.ToString())).NullToEmptyList();
        }

        public static bool IsInTwoPoints(Vector2 vec1, Vector2 vec2, Vector2 point)
        {
            if (vec1.X < point.X && point.X < vec2.X && vec1.Y < point.Y && point.Y < vec2.Y)
            //if (vec1.X <= point.X && point.X <= vec2.X && vec1.Y <= point.Y && point.Y <= vec2.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static Vector2 GetNearestPoint(Vector2 start, Vector2[] movePositions)
        {
            Vector2? nextPos = null;
            for (int i = 0, j = 1; j < movePositions.Length; i++, j++)
            {
                var posi = movePositions[i];
                var posj = movePositions[j];
                if (IsInTwoPoints(posi, posj, start))
                {
                    nextPos = posj;
                    break;
                }
            }

            if (nextPos == null)
            {
                nextPos = movePositions.Last();
            }

            return (Vector2)nextPos;
        }

        public static void GetNearestPointFromPos(Vector2 pos, Vector2[] positions, ref Vector2? nearPos, ref float? nearDis)
        {
            nearPos = null;
            nearDis = null;
            foreach (var po in positions)
            {
                float distance = Convert.ToSingle(Math.Sqrt((po.X - pos.X) * (po.X - pos.X) + (po.Y - pos.Y) * (po.Y - pos.Y)));
                if (nearDis == null || distance < nearDis)
                {
                    nearPos = pos;
                    nearDis = distance;
                }
            }
        }

        public static double ComputeTotalDistance(List<Vector2> vecs)
        {
            double distance = 0f;
            if (vecs != null && vecs.Count > 0)
            {
                Vector2 preVec = vecs[0];
                for (int i = 1; i < vecs.Count; i++)
                {
                    var nowVec = vecs[i];
                    distance = distance + Math.Sqrt((nowVec.Y - preVec.Y) * (nowVec.Y - preVec.Y) + (nowVec.X - preVec.X) * (nowVec.X - preVec.X));
                    preVec = nowVec;
                }
            }
            return distance;
        }

        public static float ComputeTwoDistanceDouble(Vector2 start, Vector2 end)
        {
            return (end.Y - start.Y) * (end.Y - start.Y) + (end.X - start.X) * (end.X - start.X);
        }

        public static float ComputeTwoDistance(Vector2 start, Vector2 end)
        {
            float distanceDouble = ComputeTwoDistanceDouble(start, end);
            return Convert.ToSingle(Math.Sqrt(distanceDouble));
        }

        public static double[] Root2(double a, double b, double c)
        {
            double[] Roots = new double[2];
            double Delt = b * b - 4 * a * c;
            if (Delt >= 0)
            {
                Roots[0] = (-b + Math.Sqrt(Delt)) / 2 * a;
                Roots[1] = (-b - Math.Sqrt(Delt)) / 2 * a;
                return Roots;
            }
            else
            {
                Roots = null;
                return Roots;
            }
        }

        //獲取同多個多邊形的相交點
        public static List<Vector2[]> GetIntersectsArea(Vector2 start, Vector2 end, List<Vector2[]> polygonPointsList)
        {
            var resultPoints = new List<Vector2[]>();

            foreach (var poly in polygonPointsList)
            {
                for (int i = 0, j = 1; i <= poly.Length; j = i, i++)
                {
                    Vector2 a1, a2;

                    if (i == poly.Length)
                    {
                        a1 = poly[0];
                    }
                    else
                    {
                        a1 = poly[i];
                    }
                    a2 = poly[j];
                    Vector2 vector = Vector2.Zero;
                    bool inter = Intersects(start, end, a1, a2, out vector);
                    if (inter && resultPoints.FirstOrDefault(re => re[2] == vector) == null)
                    {
                        Vector2[] vecs = new Vector2[] { a1, a2, vector };
                        resultPoints.Add(vecs);
                    }
                }
            }

            return resultPoints;
        }

        // a1 is line1 start, a2 is line1 end, b1 is line2 start, b2 is line2 end
        public static bool Intersects(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2, out Vector2 intersection)
        {
            intersection = Vector2.Zero;

            Vector2 b = a2 - a1;
            Vector2 d = b2 - b1;
            float bDotDPerp = b.X * d.Y - b.Y * d.X;

            // if b dot d == 0, it means the lines are parallel so have infinite intersection points
            if (bDotDPerp == 0)
                return false;

            Vector2 c = b1 - a1;
            float t = (c.X * d.Y - c.Y * d.X) / bDotDPerp;
            if (t < 0 || t > 1)
                return false;

            float u = (c.X * b.Y - c.Y * b.X) / bDotDPerp;
            if (u < 0 || u > 1)
                return false;

            intersection = a1 + t * b;

            return true;
        }

        /// <summary>  
        /// 判断点是否在多边形内.  
        /// ----------原理----------  
        /// 注意到如果从P作水平向左的射线的话，如果P在多边形内部，那么这条射线与多边形的交点必为奇数，  
        /// 如果P在多边形外部，则交点个数必为偶数(0也在内)。  
        /// </summary>  
        /// <param name="checkPoint">要判断的点</param>  
        /// <param name="polygonPoints">多边形的顶点</param>  
        /// <returns></returns>  
        public static bool IsInPolygon(Vector2 checkPoint, List<Vector2> polygonPoints)
        {
            bool inside = false;
            int pointCount = polygonPoints.Count;
            Vector2 p1, p2;
            for (int i = 0, j = pointCount - 1; i < pointCount; j = i, i++)//第一个点和最后一个点作为第一条线，之后是第一个点和第二个点作为第二条线，之后是第二个点与第三个点，第三个点与第四个点...  
            {
                p1 = polygonPoints[i];
                p2 = polygonPoints[j];
                if (checkPoint.Y < p2.Y)
                {//p2在射线之上  
                    if (p1.Y <= checkPoint.Y)
                    {//p1正好在射线中或者射线下方  
                        if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) > (checkPoint.X - p1.X) * (p2.Y - p1.Y))//斜率判断,在P1和P2之间且在P1P2右侧  
                        {
                            //射线与多边形交点为奇数时则在多边形之内，若为偶数个交点时则在多边形之外。  
                            //由于inside初始值为false，即交点数为零。所以当有第一个交点时，则必为奇数，则在内部，此时为inside=(!inside)  
                            //所以当有第二个交点时，则必为偶数，则在外部，此时为inside=(!inside)  
                            inside = (!inside);
                        }
                    }
                }
                else if (checkPoint.Y < p1.Y)
                {
                    //p2正好在射线中或者在射线下方，p1在射线上  
                    if ((checkPoint.Y - p1.Y) * (p2.X - p1.X) < (checkPoint.X - p1.X) * (p2.Y - p1.Y))//斜率判断,在P1和P2之间且在P1P2右侧  
                    {
                        inside = (!inside);
                    }
                }
            }
            return inside;
        }

        public static bool IsInCircle(Vector2 vecCircle, float radius, Vector2 vecPoint)
        {
            var vecDis = vecPoint - vecCircle;
            var distance = Convert.ToSingle(Math.Sqrt(vecDis.Y * vecDis.Y + vecDis.X * vecDis.X));
            return distance <= radius;
        }

        public static bool IsTwoSquareIntersect(Vector2 vecSquare, float vecRadius, Vector2 vecNowPos)
        {
            float R2 = vecRadius * 2;
            var vecs = new Vector2[]
            {
                vecSquare + new Vector2(-R2, -R2),
                vecSquare + new Vector2(R2, -R2),
                vecSquare + new Vector2(R2, R2),
                vecSquare + new Vector2(-R2, R2)
            };

            return IsInTwoPoints(vecs[0], vecs[2], vecNowPos);
        }

        public static bool IsTwoCirclesIntersect(Vector2 vecCircle1, float vecRadius1, Vector2 vecCircle2, float vecRadius2)
        {
            var vecDis = vecCircle2 - vecCircle1;
            var distance = Convert.ToSingle(Math.Sqrt(vecDis.Y * vecDis.Y + vecDis.X * vecDis.X));
            return distance < vecRadius1 + vecRadius2;
        }

        /// <summary>
        /// 判断两条线是否相交
        /// </summary>
        /// <param name="a">线段1起点坐标</param>
        /// <param name="b">线段1终点坐标</param>
        /// <param name="c">线段2起点坐标</param>
        /// <param name="d">线段2终点坐标</param>
        /// <param name="intersection">相交点坐标</param>
        /// <returns>是否相交 0:两线平行  -1:不平行且未相交  1:两线相交</returns>
        static int GetIntersection(Point a, Point b, Point c, Point d, ref Point intersection)
        {
            //判断异常
            if (Math.Abs(b.X - a.Y) + Math.Abs(b.X - a.X) + Math.Abs(d.Y - c.Y) + Math.Abs(d.X - c.X) == 0)
            {
                if (c.X - a.X == 0)
                {
                    //Debug.Print("ABCD是同一个点！");
                }
                else
                {
                    //Debug.Print("AB是一个点，CD是一个点，且AC不同！");
                }
                return 0;
            }

            if (Math.Abs(b.Y - a.Y) + Math.Abs(b.X - a.X) == 0)
            {
                if ((a.X - d.X) * (c.Y - d.Y) - (a.Y - d.Y) * (c.X - d.X) == 0)
                {
                    //Debug.Print("A、B是一个点，且在CD线段上！");
                }
                else
                {
                    //Debug.Print("A、B是一个点，且不在CD线段上！");
                }
                return 0;
            }
            if (Math.Abs(d.Y - c.Y) + Math.Abs(d.X - c.X) == 0)
            {
                if ((d.X - b.X) * (a.Y - b.Y) - (d.Y - b.Y) * (a.X - b.X) == 0)
                {
                    //Debug.Print("C、D是一个点，且在AB线段上！");
                }
                else
                {
                    //Debug.Print("C、D是一个点，且不在AB线段上！");
                }
            }

            if ((b.Y - a.Y) * (c.X - d.X) - (b.X - a.X) * (c.Y - d.Y) == 0)
            {
                //Debug.Print("线段平行，无交点！");
                return 0;
            }

            intersection.X = ((b.X - a.X) * (c.X - d.X) * (c.Y - a.Y) - c.X * (b.X - a.X) * (c.Y - d.Y) + a.X * (b.Y - a.Y) * (c.X - d.X)) / ((b.Y - a.Y) * (c.X - d.X) - (b.X - a.X) * (c.Y - d.Y));
            intersection.Y = ((b.Y - a.Y) * (c.Y - d.Y) * (c.X - a.X) - c.Y * (b.Y - a.Y) * (c.X - d.X) + a.Y * (b.X - a.X) * (c.Y - d.Y)) / ((b.X - a.X) * (c.Y - d.Y) - (b.Y - a.Y) * (c.X - d.X));

            if ((intersection.X - a.X) * (intersection.X - b.X) <= 0 && (intersection.X - c.X) * (intersection.X - d.X) <= 0 && (intersection.Y - a.Y) * (intersection.Y - b.Y) <= 0 && (intersection.Y - c.Y) * (intersection.Y - d.Y) <= 0)
            {
                //Debug.Print("线段相交于点(" + intersection.X + "," + intersection.Y + ")！");
                return 1; //'相交
            }
            else
            {
                //Debug.Print("线段相交于虚交点(" + intersection.X + "," + intersection.Y + ")！");
                return -1; //'相交但不在线段上
            }
        }


    }

}
