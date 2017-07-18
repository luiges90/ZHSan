using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class CaptiveList : GameObjectList
    {
        public void AddCaptiveWithEvent(Captive captive)
        {
            base.Add(captive);
            captive.OnPlayerRelease += new Captive.PlayerRelease(this.captive_OnPlayerRelease);
            captive.OnRelease += new Captive.Release(this.captive_OnRelease);
            captive.OnSelfRelease += new Captive.SelfRelease(this.captive_OnSelfRelease);
            captive.OnEscape += new Captive.Escape(this.captive_OnEscape);
        }

        public void BindEvents()
        {
            foreach (var ga in GameObjects)
            {
                var captive = (Captive)ga;
                captive.ClearEvents();
                captive.OnPlayerRelease += new Captive.PlayerRelease(this.captive_OnPlayerRelease);
                captive.OnRelease += new Captive.Release(this.captive_OnRelease);
                captive.OnSelfRelease += new Captive.SelfRelease(this.captive_OnSelfRelease);
                captive.OnEscape += new Captive.Escape(this.captive_OnEscape);
            }
        }

        private void captive_OnPlayerRelease(Faction from, Faction to, Captive captive)
        {
            from.Scenario.GameScreen.CaptivePlayerRelease(from, to, captive);
        }

        private void captive_OnRelease(bool success, Faction from, Faction to, Person person)
        {
            from.Scenario.GameScreen.CaptiveRelease(success, from, to, person);
        }

        private void captive_OnSelfRelease(Captive captive)
        {
            captive.Scenario.GameScreen.SelfCaptiveRelease(captive);
        }

        private void captive_OnEscape(Captive captive)
        {
            //captive.Scenario.GameScreen.CaptiveEscape(captive);
        }
    }
}

