using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.ArchitectureDetail.EventEffect
{
    [DataContract]
    public class EventEffectKindTable
    {
        [DataMember]
        public Dictionary<int, EventEffectKind> EventEffectKinds = new Dictionary<int, EventEffectKind>();

        public bool AddEventEffectKind(EventEffectKind e)
        {
            if (this.EventEffectKinds.ContainsKey(e.ID))
            {
                return false;
            }
            this.EventEffectKinds.Add(e.ID, e);
            return true;
        }

        public void Clear()
        {
            this.EventEffectKinds.Clear();
        }

        public EventEffectKind GetEventEffectKind(int id)
        {
            EventEffectKind kind = null;
            this.EventEffectKinds.TryGetValue(id, out kind);
            return kind;
        }

        public GameObjectList GetEventEffectKindList()
        {
            GameObjectList list = new GameObjectList();
            foreach (EventEffectKind kind in this.EventEffectKinds.Values)
            {
                list.Add(kind);
            }
            return list;
        }

        public bool HasEventEffectKind(int id)
        {
            return this.EventEffectKinds.ContainsKey(id);
        }

        public void LoadFromString(EventEffectKindTable allEventEffectKinds, string influenceIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = influenceIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            EventEffectKind kind = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allEventEffectKinds.EventEffectKinds.TryGetValue(int.Parse(strArray[i]), out kind))
                {
                    this.AddEventEffectKind(kind);
                }
            }
        }

        public string SaveToString()
        {
            string str = "";
            foreach (EventEffectKind kind in this.EventEffectKinds.Values)
            {
                str = str + kind.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.EventEffectKinds.Count;
            }
        }
    }
}

