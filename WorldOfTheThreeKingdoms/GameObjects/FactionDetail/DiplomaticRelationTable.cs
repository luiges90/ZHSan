using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.FactionDetail
{
    [DataContract]
    public class DiplomaticRelationTable
    {
        [DataMember]
        public Dictionary<int, DiplomaticRelation> DiplomaticRelations = new Dictionary<int, DiplomaticRelation>();

        public bool AddDiplomaticRelation(DiplomaticRelation dr)
        {
            int hashCode = this.GetHashCode(dr.RelationFaction1ID, dr.RelationFaction2ID);
            int key = this.GetHashCode(dr.RelationFaction2ID, dr.RelationFaction1ID);
            if (this.DiplomaticRelations.ContainsKey(hashCode) || this.DiplomaticRelations.ContainsKey(key))
            {
                return false;
            }
            this.DiplomaticRelations.Add(hashCode, dr);
            return true;
        }

        public bool AddDiplomaticRelation(int faction1ID, int faction2ID, int relation)
        {
            int hashCode = this.GetHashCode(faction1ID, faction2ID);
            int key = this.GetHashCode(faction2ID, faction1ID);
            if (this.DiplomaticRelations.ContainsKey(hashCode) || this.DiplomaticRelations.ContainsKey(key))
            {
                return false;
            }
            this.DiplomaticRelations.Add(hashCode, new DiplomaticRelation(faction1ID, faction2ID, relation));
            return true;
        }

        public void Clear()
        {
            this.DiplomaticRelations.Clear();
        }

        public DiplomaticRelation GetDiplomaticRelation(int faction1ID, int faction2ID)
        {
            int hashCode = this.GetHashCode(faction1ID, faction2ID);
            DiplomaticRelation relation = null;
            this.DiplomaticRelations.TryGetValue(hashCode, out relation);
            if (relation == null)
            {
                hashCode = this.GetHashCode(faction2ID, faction1ID);
                this.DiplomaticRelations.TryGetValue(hashCode, out relation);
            }
            if (relation == null)
            {
                this.AddDiplomaticRelation(faction1ID, faction2ID, 0);
                this.DiplomaticRelations.TryGetValue(this.GetHashCode(faction1ID, faction2ID), out relation);
            }
            return relation;
        }

        public GameObjectList GetAllDiplomaticRelationDisplayList()
        {
            GameObjectList list = new GameObjectList();
            foreach (DiplomaticRelation relation in this.DiplomaticRelations.Values)
            {
                list.Add(new DiplomaticRelationDisplay(relation, relation.RelationFaction1String));
            }
            return list;
        }

        public GameObjectList GetDiplomaticRelationDisplayListByFactionID(int factionID)
        {
            GameObjectList list = new GameObjectList();
            foreach (DiplomaticRelation relation in this.DiplomaticRelations.Values)
            {
                if (relation.RelationFaction1ID == factionID)
                {
                    list.Add(new DiplomaticRelationDisplay(relation, relation.RelationFaction2String));
                }
                else if (relation.RelationFaction2ID == factionID)
                {
                    list.Add(new DiplomaticRelationDisplay(relation, relation.RelationFaction1String));
                }
            }
            return list;
        }

        public GameObjectList GetDiplomaticRelationListByFactionID(int factionID)
        {
            GameObjectList list = new GameObjectList();
            foreach (DiplomaticRelation relation in this.DiplomaticRelations.Values)
            {
                if ((relation.RelationFaction1ID == factionID) || (relation.RelationFaction2ID == factionID))
                {
                    list.Add(relation);
                }
            }
            return list;
        }

        public GameObjectList GetDiplomaticRelationListByFactionName(string factionName)
        {
            GameObjectList list = new GameObjectList();
            foreach (DiplomaticRelation relation in this.DiplomaticRelations.Values)
            {
                if ((relation.RelationFaction1String == factionName) || (relation.RelationFaction2String == factionName))
                {
                    list.Add(relation);
                }
            }
            return list;
        }

        private int GetHashCode(int id1, int id2)
        {
            return (id1.ToString() + " " + id2.ToString()).GetHashCode();
        }

        public void RemoveDiplomaticRelationByFactionID(int factionID)
        {
            foreach (DiplomaticRelation relation in this.GetDiplomaticRelationListByFactionID(factionID))
            {
                this.DiplomaticRelations.Remove(relation.GetHashCode());
            }
        }

        public int Count
        {
            get
            {
                return this.DiplomaticRelations.Count;
            }
        }
    }
}

