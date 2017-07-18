using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameObjects;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class guanjuezhongleibiao
    {
        [DataMember]
        public Dictionary<int, guanjuezhongleilei> guanjuedezhongleizidian = new Dictionary<int, guanjuezhongleilei>();

        public bool Addguanjuedezhonglei(guanjuezhongleilei guanjuedezhonglei)
        {
            if (this.guanjuedezhongleizidian.ContainsKey(guanjuedezhonglei.ID))
            {
                return false;
            }
            this.guanjuedezhongleizidian.Add(guanjuedezhonglei.ID, guanjuedezhonglei);
            return true;
        }

        public void Clear()
        {
            this.guanjuedezhongleizidian.Clear();
        }

        public guanjuezhongleilei Getguanjuedezhonglei(int guanjuedezhongleiID)
        {
            guanjuezhongleilei kind = null;
            this.guanjuedezhongleizidian.TryGetValue(guanjuedezhongleiID, out kind);
            return kind;
        }

        public GameObjectList Getguanjuedezhongleiliebiao()
        {
            GameObjectList list = new GameObjectList();
            foreach (guanjuezhongleilei kind in this.guanjuedezhongleizidian.Values)
            {
                list.Add(kind);
            }
            return list;
        }

        public void LoadFromString(guanjuezhongleibiao suoyouguanjuedezhonglei, string guanjuedezhongleiIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = guanjuedezhongleiIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            guanjuezhongleilei kind = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (suoyouguanjuedezhonglei.guanjuedezhongleizidian.TryGetValue(int.Parse(strArray[i]), out kind))
                {
                    this.Addguanjuedezhonglei(kind);
                }
            }
        }

        public bool Removeguanjuedezhonglei(int id)
        {
            if (!this.guanjuedezhongleizidian.ContainsKey(id))
            {
                return false;
            }
            this.guanjuedezhongleizidian.Remove(id);
            return true;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (guanjuezhongleilei kind in this.guanjuedezhongleizidian.Values)
            {
                str = str + kind.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.guanjuedezhongleizidian.Count;
            }
        }
    }
}
