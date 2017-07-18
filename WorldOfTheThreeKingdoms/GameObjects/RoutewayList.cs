using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class RoutewayList : GameObjectList
    {
        public void AddRoutewayWithEvent(Routeway routeway)
        {
            base.Add(routeway);
        }
    }
}

