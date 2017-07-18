using GameObjects;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class CastDefaultKindList : GameObjectList
    {
        public CastDefaultKindList GetSelectedList(TroopCastDefaultKind kind)
        {
            CastDefaultKindList list = new CastDefaultKindList();
            foreach (CastDefaultKind kind2 in base.GameObjects)
            {
                if (kind2.ID ==(int) kind)
                {
                    list.Add(kind2);
                }
            }
            return list;
        }
    }
}

