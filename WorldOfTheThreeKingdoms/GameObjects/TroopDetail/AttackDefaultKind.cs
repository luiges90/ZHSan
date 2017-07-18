using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class AttackDefaultKind : GameObject
    {
        public void Apply(Troop troop)
        {
            troop.AttackDefaultKind = (TroopAttackDefaultKind) base.ID;
        }
    }
}

