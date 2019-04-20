using GameGlobal;
using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class RoutePoint
    {
        [DataMember]
        public int ActiveFundCost;
        
        public Routeway BelongedRouteway;
        [DataMember]
        public int BuildFundCost;
        [DataMember]
        public int BuildWorkCost;
        [DataMember]
        public float ConsumptionRate;
        [DataMember]
        public SimpleDirection Direction;
        [DataMember]
        public int Index;
        [DataMember]
        public Point Position;
        [DataMember]
        public SimpleDirection PreviousDirection;
    }
}

