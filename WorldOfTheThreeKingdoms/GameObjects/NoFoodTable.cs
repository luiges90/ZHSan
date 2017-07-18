using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


namespace GameObjects
{
    [DataContract]
    public class NoFoodTable
    {
        [DataMember]
        public Dictionary<Point, NoFoodPosition> Positions = new Dictionary<Point, NoFoodPosition>();

        public void AddPosition(NoFoodPosition position)
        {
            if (!this.HasPosition(position.Position))
            {
                this.Positions.Add(position.Position, position);
            }
        }

        public void Clear()
        {
            this.Positions.Clear();
        }

        public bool HasPosition(Point position)
        {
            return this.Positions.ContainsKey(position);
        }

        public void LoadFromString(string dataString)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = dataString.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            this.Clear();
            for (int i = 0; i < strArray.Length; i += 3)
            {
                this.AddPosition(new NoFoodPosition(new Point(int.Parse(strArray[i]), int.Parse(strArray[i + 1])), int.Parse(strArray[i + 2])));
            }
        }

        public bool RemovePosition(NoFoodPosition position)
        {
            return this.Positions.Remove(position.Position);
        }

        public string SaveToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (NoFoodPosition position in this.Positions.Values)
            {
                builder.Append(string.Concat(new object[] { position.Position.X, " ", position.Position.Y, " ", position.Days, " " }));
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

