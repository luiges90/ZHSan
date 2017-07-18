using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class InformationList : GameObjectList
    {
        public void AddInformation(Information information)
        {
            base.Add(information);
        }
    }
}

