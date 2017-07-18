using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.FactionDetail
{
    [DataContract]
    public class InformationKindList : GameObjectList
    {
        public InformationKindList GetAvailList(Architecture a)
        {
            InformationKindList list = new InformationKindList();
            foreach (InformationKind kind in base.GameObjects)
            {
                if (kind.Avail(a))
                {
                    list.Add(kind);
                }
            }
            return list;
        }

        public bool HasAvailItem(Architecture a)
        {
            foreach (InformationKind kind in base.GameObjects)
            {
                if (kind.Avail(a))
                {
                    return true;
                }
            }
            return false;
        }
    }
}

