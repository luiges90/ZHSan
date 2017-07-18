using GameObjects;
using System;
using System.Collections.Generic;

using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{
    [DataContract]
    public class EventEffectTable
    {
        [DataMember]
        public Dictionary<int, EventEffect> EventEffects = new Dictionary<int, EventEffect>();

        public bool AddEventEffect(EventEffect e)
        {
            if (this.EventEffects.ContainsKey(e.ID))
            {
                return false;
            }
            this.EventEffects.Add(e.ID, e);
            return true;
        }

        public void Clear()
        {
            this.EventEffects.Clear();
        }

        public EventEffect GetEventEffect(int id)
        {
            EventEffect effect = null;
            this.EventEffects.TryGetValue(id, out effect);
            return effect;
        }

        public GameObjectList GetEventEffectList()
        {
            GameObjectList list = new GameObjectList();
            foreach (EventEffect effect in this.EventEffects.Values)
            {
                list.Add(effect);
            }
            return list;
        }

        public bool HasEventEffect(int id)
        {
            return this.EventEffects.ContainsKey(id);
        }

        public void LoadFromString(EventEffectTable allEventEffects, string influenceIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = influenceIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            EventEffect effect = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allEventEffects.EventEffects.TryGetValue(int.Parse(strArray[i]), out effect))
                {
                    this.AddEventEffect(effect);
                }
            }
        }

        public string SaveToString()
        {
            string str = "";
            foreach (EventEffect effect in this.EventEffects.Values)
            {
                str = str + effect.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.EventEffects.Count;
            }
        }
    }
}

