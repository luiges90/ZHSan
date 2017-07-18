using GameObjects;
using System;
using System.Collections.Generic;


namespace GameObjects.PersonDetail
{
    /*
    public class GuanzhiKindTable
    {
        
        public Dictionary<int, GuanzhiKind> GuanzhiKinds = new Dictionary<int, GuanzhiKind>();

        public bool AddGuanzhiKind(GuanzhiKind guanzhi)
        {
            if (this.GuanzhiKinds.ContainsKey(guanzhi.ID))
            {
                return false;
            }
            this.GuanzhiKinds.Add(guanzhi.ID, guanzhi);
            return true;
        }

        public void Clear()
        {
            this.GuanzhiKinds.Clear();
        }

        public GuanzhiKind GetGuanzhiKind(int guanzhiID)
        {
            GuanzhiKind guanzhi = null;
            this.GuanzhiKinds.TryGetValue(guanzhiID, out guanzhi);
            return guanzhi;
        }

        public GameObjectList GetGuanzhiKindList()
        {
            GameObjectList list = new GameObjectList();
            foreach (GuanzhiKind guanzhi in this.GuanzhiKinds.Values)
            {
                list.Add(guanzhi);
            }
            return list;
        }

        public void LoadFromString(GuanzhiKindTable allGuanzhis, string guanzhiIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = guanzhiIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            GuanzhiKind guanzhi = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allGuanzhis.GuanzhiKinds.TryGetValue(int.Parse(strArray[i]), out guanzhi))
                {
                    this.AddGuanzhiKind(guanzhi);
                }
            }
        }

        public string SaveToString()
        {
            string str = "";
            foreach (GuanzhiKind guanzhi in this.GuanzhiKinds.Values)
            {
                str = str + guanzhi.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.GuanzhiKinds.Count;
            }
        }

        
    }*/
}

