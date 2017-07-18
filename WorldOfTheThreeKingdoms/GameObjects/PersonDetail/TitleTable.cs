using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.PersonDetail
{
    [DataContract]
    public class TitleTable
    {
        [DataMember]
        public Dictionary<int, Title> Titles = new Dictionary<int, Title>();

        public bool AddTitle(Title title)
        {
            if (this.Titles.ContainsKey(title.ID))
            {
                return false;
            }
            this.Titles.Add(title.ID, title);
            return true;
        }

        public void Clear()
        {
            this.Titles.Clear();
        }

        public Title GetTitle(int titleID)
        {
            Title title = null;
            this.Titles.TryGetValue(titleID, out title);
            return title;
        }

        public GameObjectList GetTitleList()
        {
            GameObjectList list = new GameObjectList();
            foreach (Title title in this.Titles.Values)
            {
                list.Add(title);
            }
            return list;
        }

        public List<string> LoadFromString(TitleTable allTitles, string titleIDs)
        {
            List<string> errorMsg = new List<string>();

            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = titleIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Title title = null;
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (allTitles.Titles.TryGetValue(int.Parse(strArray[i]), out title))
                    {
                        this.AddTitle(title);
                    }
                    else
                    {
                        errorMsg.Add("称号ID" + int.Parse(strArray[i]) + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("兵种一栏应为半型空格分隔的影响ID");
            }

            return errorMsg;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (Title title in this.Titles.Values)
            {
                str = str + title.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.Titles.Count;
            }
        }
    }
}

