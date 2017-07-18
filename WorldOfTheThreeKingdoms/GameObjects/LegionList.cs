using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class LegionList : GameObjectList
    {
        public void AddLegionWithEvent(Legion legion)
        {
            base.Add(legion);
        }

        public void RemoveLegion(Legion legion)
        {
            base.Remove(legion);
        }
    }
}

