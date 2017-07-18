using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.Influences
{
    [DataContract]
    public class InfluenceKindTable
    {
        [DataMember]
        public Dictionary<int, InfluenceKind> InfluenceKinds = new Dictionary<int, InfluenceKind>();

        public bool AddInfluenceKind(InfluenceKind ik)
        {
            if (this.InfluenceKinds.ContainsKey(ik.ID))
            {
                return false;
            }
            this.InfluenceKinds.Add(ik.ID, ik);
            return true;
        }

        public void Clear()
        {
            this.InfluenceKinds.Clear();
        }

        public InfluenceKind GetInfluenceKind(int id)
        {
            InfluenceKind kind = null;
            this.InfluenceKinds.TryGetValue(id, out kind);
            return kind;
        }

        public GameObjectList GetInfluenceKindList()
        {
            GameObjectList list = new GameObjectList();
            foreach (InfluenceKind kind in this.InfluenceKinds.Values)
            {
                list.Add(kind);
            }
            return list;
        }

        public bool HasInfluenceKind(int id)
        {
            return this.InfluenceKinds.ContainsKey(id);
        }

        public void LoadFromString(InfluenceKindTable allInfluenceKinds, string influenceIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = influenceIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            InfluenceKind kind = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allInfluenceKinds.InfluenceKinds.TryGetValue(int.Parse(strArray[i]), out kind))
                {
                    this.AddInfluenceKind(kind);
                }
            }
        }

        public string SaveToString()
        {
            string str = "";
            foreach (InfluenceKind kind in this.InfluenceKinds.Values)
            {
                str = str + kind.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.InfluenceKinds.Count;
            }
        }

        public bool HasTroopLeaderValidInfluenceKind
        {
            get
            {
                foreach (InfluenceKind kind in this.InfluenceKinds.Values)
                {
                    if (kind.TroopLeaderValid)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}

