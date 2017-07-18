using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class CombatMethodTable
    {
        [DataMember]
        public Dictionary<int, CombatMethod> CombatMethods = new Dictionary<int, CombatMethod>();

        public bool AddCombatMethod(CombatMethod combatMethod)
        {
            if (this.CombatMethods.ContainsKey(combatMethod.ID))
            {
                return false;
            }
            this.CombatMethods.Add(combatMethod.ID, combatMethod);
            return true;
        }

        public bool RemoveCombatMethod(CombatMethod combatMethod)
        {
            if (!this.CombatMethods.ContainsKey(combatMethod.ID))
            {
                return false;
            }
            this.CombatMethods.Remove(combatMethod.ID);
            return true;
        }

        public void Clear()
        {
            this.CombatMethods.Clear();
        }

        public CombatMethod GetCombatMethod(int combatMethodID)
        {
            CombatMethod method = null;
            this.CombatMethods.TryGetValue(combatMethodID, out method);
            return method;
        }

        public GameObjectList GetCombatMethodList()
        {
            GameObjectList list = new GameObjectList();
            foreach (CombatMethod method in this.CombatMethods.Values)
            {
                list.Add(method);
            }
            return list;
        }

        public int Count
        {
            get
            {
                return this.CombatMethods.Count;
            }
        }
    }
}

