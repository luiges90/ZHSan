using GameObjects;
using System;
using System.Runtime.Serialization;

namespace GameObjects.TroopDetail
{
    [DataContract]
    public class AttackTargetKind : GameObject
    {
        public void Apply(Troop troop)
        {
            troop.AttackTargetKind = (TroopAttackTargetKind) base.ID;
        }
    }
}

