using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.FactionDetail
{
    [DataContract]
    public class TechniqueTable
    {
        [DataMember]
        public Dictionary<int, Technique> Techniques = new Dictionary<int, Technique>();

        public bool AddTechnique(Technique technique)
        {
            if (this.Techniques.ContainsKey(technique.ID))
            {
                return false;
            }
            this.Techniques.Add(technique.ID, technique);
            return true;
        }

        public void Clear()
        {
            this.Techniques.Clear();
        }

        public Technique GetTechnique(int techniqueID)
        {
            Technique technique = null;
            this.Techniques.TryGetValue(techniqueID, out technique);
            return technique;
        }

        public GameObjectList GetTechniqueList()
        {
            GameObjectList list = new GameObjectList();
            foreach (Technique technique in this.Techniques.Values)
            {
                list.Add(technique);
            }
            return list;
        }

        public List<string> LoadFromString(TechniqueTable allTechniques, string techniqueIDs)
        {
            List<string> errorMsg = new List<string>();
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = techniqueIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            Technique technique = null;
            try
            {
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (allTechniques.Techniques.TryGetValue(int.Parse(strArray[i]), out technique))
                    {
                        this.AddTechnique(technique);
                    }
                    else
                    {
                        errorMsg.Add("技巧ID" + strArray[i] + "不存在");
                    }
                }
            }
            catch
            {
                errorMsg.Add("技巧列表应为半型空格分隔的技巧ID");
            }
            return errorMsg;
        }

        public bool RemoveTechniuqe(int id)
        {
            if (!this.Techniques.ContainsKey(id))
            {
                return false;
            }
            this.Techniques.Remove(id);
            return true;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (Technique technique in this.Techniques.Values)
            {
                str = str + technique.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.Techniques.Count;
            }
        }
    }
}

