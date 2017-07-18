using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class CastDefaultKind : GameObject
    {
        public void Apply(Troop troop)
        {
            troop.CastDefaultKind = (TroopCastDefaultKind) base.ID;
        }
    }
}

