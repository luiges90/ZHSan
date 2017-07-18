using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.Conditions
{
    [DataContract]
    public class ConditionKindTable
    {
        [DataMember]
        public Dictionary<int, ConditionKind> ConditionKinds = new Dictionary<int, ConditionKind>();

        public bool AddConditionKind(ConditionKind ck)
        {
            if (this.ConditionKinds.ContainsKey(ck.ID))
            {
                return false;
            }
            this.ConditionKinds.Add(ck.ID, ck);
            return true;
        }

        public void Clear()
        {
            this.ConditionKinds.Clear();
        }

        public ConditionKind GetConditionKind(int id)
        {
            ConditionKind kind = null;
            this.ConditionKinds.TryGetValue(id, out kind);
            return kind;
        }

        public GameObjectList GetConditionKindList()
        {
            GameObjectList list = new GameObjectList();
            foreach (ConditionKind kind in this.ConditionKinds.Values)
            {
                list.Add(kind);
            }
            return list;
        }

        public void LoadFromString(ConditionKindTable allConditionKinds, string conditionIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = conditionIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            ConditionKind kind = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allConditionKinds.ConditionKinds.TryGetValue(int.Parse(strArray[i]), out kind))
                {
                    this.AddConditionKind(kind);
                }
            }
        }

        public string SaveToString()
        {
            string str = "";
            foreach (ConditionKind kind in this.ConditionKinds.Values)
            {
                str = str + kind.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.ConditionKinds.Count;
            }
        }
    }
}

