using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.ArchitectureDetail
{
    [DataContract]
    public class FacilityKindTable
    {
        [DataMember]
        public Dictionary<int, FacilityKind> FacilityKinds = new Dictionary<int, FacilityKind>();

        private int maxFacilitySpace = -1;
        public int GetMaxFacilitySpace()
        {
            if (maxFacilitySpace < 0)
            {
                foreach (FacilityKind fk in this.GetFacilityKindList())
                {
                    if (fk.PositionOccupied > maxFacilitySpace)
                    {
                        maxFacilitySpace = fk.PositionOccupied;
                    }
                }
            }
            return maxFacilitySpace;
        }

        public bool AddFacilityKind(FacilityKind facilityKind)
        {
            if (this.FacilityKinds.ContainsKey(facilityKind.ID))
            {
                return false;
            }
            this.FacilityKinds.Add(facilityKind.ID, facilityKind);
            return true;
        }

        public void Clear()
        {
            this.FacilityKinds.Clear();
        }

        public FacilityKind GetFacilityKind(int facilityKindID)
        {
            FacilityKind kind = null;
            this.FacilityKinds.TryGetValue(facilityKindID, out kind);
            return kind;
        }

        public GameObjectList GetFacilityKindList()
        {
            GameObjectList list = new GameObjectList();
            foreach (FacilityKind kind in this.FacilityKinds.Values)
            {
                list.Add(kind);
            }
            return list;
        }

        public void LoadFromString(FacilityKindTable allFacilityKinds, string facilityKindIDs)
        {
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = facilityKindIDs.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            FacilityKind kind = null;
            for (int i = 0; i < strArray.Length; i++)
            {
                if (allFacilityKinds.FacilityKinds.TryGetValue(int.Parse(strArray[i]), out kind))
                {
                    this.AddFacilityKind(kind);
                }
            }
        }

        public bool RemoveFacilityKind(int id)
        {
            if (!this.FacilityKinds.ContainsKey(id))
            {
                return false;
            }
            this.FacilityKinds.Remove(id);
            return true;
        }

        public string SaveToString()
        {
            string str = "";
            foreach (FacilityKind kind in this.FacilityKinds.Values)
            {
                str = str + kind.ID.ToString() + " ";
            }
            return str;
        }

        public int Count
        {
            get
            {
                return this.FacilityKinds.Count;
            }
        }
    }
}

