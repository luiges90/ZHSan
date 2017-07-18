using GameObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class StratagemTable
    {
        [DataMember]
        public Dictionary<int, Stratagem> Stratagems = new Dictionary<int, Stratagem>();

        public bool AddStratagem(Stratagem Stratagem)
        {
            if (this.Stratagems.ContainsKey(Stratagem.ID))
            {
                return false;
            }
            this.Stratagems.Add(Stratagem.ID, Stratagem);
            return true;
        }

        public void Clear()
        {
            this.Stratagems.Clear();
        }

        public Stratagem GetStratagem(int StratagemID)
        {
            Stratagem stratagem = null;
            this.Stratagems.TryGetValue(StratagemID, out stratagem);
            return stratagem;
        }

        public GameObjectList GetStratagemList()
        {
            GameObjectList list = new GameObjectList();
            foreach (Stratagem stratagem in this.Stratagems.Values)
            {
                list.Add(stratagem);
            }
            return list;
        }
    }
}

