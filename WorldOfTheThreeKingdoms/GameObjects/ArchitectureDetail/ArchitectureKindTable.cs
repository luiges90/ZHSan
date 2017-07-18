using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.ArchitectureDetail
{
    [DataContract]
    public class ArchitectureKindTable
    {
        [DataMember]
        public Dictionary<int, ArchitectureKind> ArchitectureKinds = new Dictionary<int, ArchitectureKind>();

        public bool AddArchitectureKind(ArchitectureKind architectureKind)
        {
            if (this.ArchitectureKinds.ContainsKey(architectureKind.ID))
            {
                return false;
            }
            this.ArchitectureKinds.Add(architectureKind.ID, architectureKind);
            return true;
        }

        public void Clear()
        {
            this.ArchitectureKinds.Clear();
        }

        public ArchitectureKind GetArchitectureKind(int architectureKindID)
        {
            ArchitectureKind kind = null;
            this.ArchitectureKinds.TryGetValue(architectureKindID, out kind);
            return kind;
        }

        public GameObjectList GetArchitectureKindList()
        {
            GameObjectList list = new GameObjectList();
            foreach (ArchitectureKind kind in this.ArchitectureKinds.Values)
            {
                list.Add(kind);
            }
            return list;
        }
    }
}

