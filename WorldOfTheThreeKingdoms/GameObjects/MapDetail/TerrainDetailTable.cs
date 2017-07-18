using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.MapDetail
{
    [DataContract]
    public class TerrainDetailTable
    {
        [DataMember]
        public Dictionary<int, TerrainDetail> TerrainDetails = new Dictionary<int, TerrainDetail>();

        public bool AddTerrainDetail(TerrainDetail terrainDetail)
        {
            if (this.TerrainDetails.ContainsKey(terrainDetail.ID))
            {
                return false;
            }
            this.TerrainDetails.Add(terrainDetail.ID, terrainDetail);
            return true;
        }

        public void Clear()
        {
            this.TerrainDetails.Clear();
        }

        public TerrainDetail GetTerrainDetail(int terrainDetailID)
        {
            TerrainDetail detail = null;
            this.TerrainDetails.TryGetValue(terrainDetailID, out detail);
            return detail;
        }

        public GameObjectList GetTerrainDetailList()
        {
            GameObjectList list = new GameObjectList();
            foreach (TerrainDetail detail in this.TerrainDetails.Values)
            {
                list.Add(detail);
            }
            return list;
        }

        public void LoadFromString(TerrainDetailTable allTerrainDetails, string terrainDetailIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = terrainDetailIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            TerrainDetail detail = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allTerrainDetails.TerrainDetails.TryGetValue(int.Parse(strArray[i]), out detail))
                {
                    this.AddTerrainDetail(detail);
                }
            }
        }

        public bool RemoveTerrainDetail(int id)
        {
            if (!this.TerrainDetails.ContainsKey(id))
            {
                return false;
            }
            this.TerrainDetails.Remove(id);
            return true;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (TerrainDetail detail in this.TerrainDetails.Values)
            {
                str = str + detail.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.TerrainDetails.Count;
            }
        }
    }
}

