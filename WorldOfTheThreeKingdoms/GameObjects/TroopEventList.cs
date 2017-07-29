using GameManager;
using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class TroopEventList : GameObjectList
    {
        public void AddTroopEventWithEvent(TroopEvent te, bool add = true)
        {
            if (add)
            {
                base.Add(te);
            }
            te.OnApplyTroopEvent += new TroopEvent.ApplyTroopEvent(this.te_OnApplyTroopEvent);
        }

        private void te_OnApplyTroopEvent(TroopEvent te, Troop troop)
        {
            Session.MainGame.mainGameScreen.TroopApplyTroopEvent(te, troop);
        }
    }
}

