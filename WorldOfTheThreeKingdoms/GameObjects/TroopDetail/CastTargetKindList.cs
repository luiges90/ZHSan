using GameObjects;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class CastTargetKindList : GameObjectList
    {
        public CastTargetKindList GetSelectedList(TroopCastTargetKind kind)
        {
            CastTargetKindList list = new CastTargetKindList();
            foreach (CastTargetKind kind2 in base.GameObjects)
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

