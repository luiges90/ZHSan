using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class TitleKindTable
    {
        [DataMember]
        public Dictionary<int, TitleKind> TitleKinds = new Dictionary<int, TitleKind>();

        public bool AddTitleKind(TitleKind title)
        {
            if (this.TitleKinds.ContainsKey(title.ID))
            {
                return false;
            }
            this.TitleKinds.Add(title.ID, title);
            return true;
        }

        public void Clear()
        {
            this.TitleKinds.Clear();
        }

        public TitleKind GetTitleKind(int titleID)
        {
            TitleKind title = null;
            this.TitleKinds.TryGetValue(titleID, out title);
            return title;
        }

        public GameObjectList GetTitleKindList()
        {
            GameObjectList list = new GameObjectList();
            foreach (TitleKind title in this.TitleKinds.Values)
            {
                list.Add(title);
            }
            return list;
        }

        public void LoadFromString(TitleKindTable allTitles, string titleIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = titleIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            TitleKind title = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allTitles.TitleKinds.TryGetValue(int.Parse(strArray[i]), out title))
                {
                    this.AddTitleKind(title);
                }
            }
        }

        public string SaveToString()
        {
            string str = "";
            foreach (TitleKind title in this.TitleKinds.Values)
            {
                str = str + title.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.TitleKinds.Count;
            }
        }

        public int CombatKind
        {
            get
            {
                foreach (TitleKind tk in this.TitleKinds.Values)
                {
                    if (tk.Combat)
                    {
                        return tk.ID;
                    }
                }
                return -1;
            }
        }
    }
}

