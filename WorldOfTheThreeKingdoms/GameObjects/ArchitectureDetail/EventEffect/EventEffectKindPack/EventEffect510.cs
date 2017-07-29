using GameManager;
using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect510 : EventEffectKind
    {
        private int type;

        public override void ApplyEffectKind(Person person, Event e)
        {
            Treasure t = Session.Current.Scenario.Treasures.GetGameObject(type) as Treasure;
            if (t.BelongedPerson != null && t.BelongedPerson == person)
            {
                person.LoseTreasure(t);
                t.Available = false;
                t.HidePlace = Session.Current.Scenario.Architectures.GameObjects[GameObject.Random(Session.Current.Scenario.Architectures.GameObjects.Count)] as Architecture;
            }
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.type = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}

