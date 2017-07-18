using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace GameObjects
{
    [DataContract]
    public class PositionTable
    {
        //public Dictionary<int, Point> Positions = new Dictionary<int, Point>();
        [DataMember]
        public HashSet<Point> Positions = new HashSet<Point>();

        public void AddPosition(Point position)
        {
            /*int hashCode = position.ToString().GetHashCode();
            if (!this.Positions.ContainsKey(hashCode))
            {
                this.Positions.Add(hashCode, position);
            }*/
            this.Positions.Add(position);
        }

        public void Clear()
        {
            this.Positions.Clear();
        }

        public bool HasPosition(Point position)
        {
            /*int hashCode = position.ToString().GetHashCode();
            return this.Positions.ContainsKey(hashCode);*/
            return this.Positions.Contains(position);
        }

        public void LoadFromString(string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Clear();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                this.AddPosition(new Point(int.Parse(strArray[i]), int.Parse(strArray[i + 1])));
            }
        }

        public bool RemovePosition(Point position)
        {
            /*int hashCode = position.ToString().GetHashCode();
            return this.Positions.Remove(hashCode);*/
            return this.Positions.Remove(position);
        }

        public string SaveToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (Point point in this.Positions)
            {
                builder.Append(point.X.ToString() + " " + point.Y.ToString() + " ");
            }
            return builder.ToString();
        }

        public int Count
        {
            get
            {
                return this.Positions.Count;
            }
        }
    }
}

