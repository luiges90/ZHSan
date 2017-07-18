using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class zainanzhongleibiao
    {
        [DataMember]
        public Dictionary<int, zainanzhongleilei> zainanzhongleizidian = new Dictionary<int, zainanzhongleilei>();

        public bool Addzainanzhonglei(zainanzhongleilei zainanzhonglei)
        {
            if (this.zainanzhongleizidian.ContainsKey(zainanzhonglei.ID))
            {
                return false;
            }
            this.zainanzhongleizidian.Add(zainanzhonglei.ID, zainanzhonglei);
            return true;
        }

        public void Clear()
        {
            this.zainanzhongleizidian.Clear();
        }

        public zainanzhongleilei Getzainanzhonglei(int zainanzhongleiID)
        {
            zainanzhongleilei kind = null;
            this.zainanzhongleizidian.TryGetValue(zainanzhongleiID, out kind);
            return kind;
        }

        public GameObjectList Getzainanzhongleiliebiao()
        {
            GameObjectList list = new GameObjectList();
            foreach (zainanzhongleilei kind in this.zainanzhongleizidian.Values)
            {
                list.Add(kind);
            }
            return list;
        }

        public void LoadFromString(zainanzhongleibiao suoyouzainanzhonglei, string zainanzhongleiIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = zainanzhongleiIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            zainanzhongleilei kind = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (suoyouzainanzhonglei.zainanzhongleizidian.TryGetValue(int.Parse(strArray[i]), out kind))
                {
                    this.Addzainanzhonglei(kind);
                }
            }
        }

        public bool Removezainanzhonglei(int id)
        {
            if (!this.zainanzhongleizidian.ContainsKey(id))
            {
                return false;
            }
            this.zainanzhongleizidian.Remove(id);
            return true;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (zainanzhongleilei kind in this.zainanzhongleizidian.Values)
            {
                str = str + kind.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.zainanzhongleizidian.Count;
            }
        }
    }
}

