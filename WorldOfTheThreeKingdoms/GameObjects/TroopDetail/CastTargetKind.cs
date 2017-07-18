using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class CastTargetKind : GameObject
    {
        public void Apply(Troop troop)
        {
            troop.CastTargetKind = (TroopCastTargetKind) base.ID;
        }
    }
}

