

using GameManager;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class EventList : GameObjectList
    {
        public void AddEventWithEvent(Event te, bool add = true)
        {
            if (add)
            {
                base.Add(te);
            }
            te.OnApplyEvent += new Event.ApplyEvent(this.te_OnApplyEvent);
        }

        private void te_OnApplyEvent(Event te, Architecture a, Screen screen)
        {
            screen.ApplyEvent(te, a, screen);
        }
    }
}
