using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class TreasureList : GameObjectList
    {
        public void AddTreasure(Treasure treasure)
        {
            base.GameObjects.Add(treasure);
        }
    }
}

