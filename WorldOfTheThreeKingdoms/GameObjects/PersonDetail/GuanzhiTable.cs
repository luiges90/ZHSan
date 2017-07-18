using GameObjects;
using System;
using System.Collections.Generic;


namespace GameObjects.PersonDetail
{
    /*
    public class GuanzhiTable
    {
        
        public Dictionary<int, Guanzhi > Guanzhis = new Dictionary<int, Guanzhi>();

        public bool AddGuanzhi(Guanzhi guanzhi)
        {
            if (this.Guanzhis.ContainsKey(guanzhi.ID))
            {
                return false;
            }
            this.Guanzhis.Add(guanzhi.ID, guanzhi);
            return true;
        }

        public void Clear()
        {
            this.Guanzhis.Clear();
        }

        public Guanzhi GetGuanzhi(int guanzhiID)
        {
            Guanzhi guanzhi = null;
            this.Guanzhis.TryGetValue(guanzhiID, out guanzhi);
            return guanzhi;
        }

        public GameObjectList GetGuanzhiList()
        {
            GameObjectList list = new GameObjectList();
            foreach (Guanzhi guanzhi in this.Guanzhis.Values)
            {
                list.Add(guanzhi);
            }
            return list;
        }

        public List<string> LoadFromString(GuanzhiTable allGuanzhis, string guanzhiIDs)
        {
            List<string> errorMsg = new List<string>();

            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = guanzhiIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Guanzhi guanzhi = null;
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (allGuanzhis.Guanzhis.TryGetValue(int.Parse(strArray[i]), out guanzhi))
                    {
                        this.AddGuanzhi(guanzhi);
                    }
                    else
                    {
                        errorMsg.Add("官职ID" + int.Parse(strArray[i]) + "不存在");
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
            foreach (Guanzhi guanzhi in this.Guanzhis.Values)
            {
                str = str + guanzhi.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.Guanzhis.Count;
            }
        }
    }*/
}
