using GameObjects;
using System;
using GameManager;

using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect250 : EventEffectKind
    {
        private int  targetFactionID;

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.targetFactionID = int.Parse(parameter);
                //targetFaction = factions.GetGameObject(int.Parse(parameter)) as Faction;
            }
            catch
            {
                
            }
           
        }


        public override void ApplyEffectKind(Person person, Event e)
        {
            FactionList factionlist = Session.Current.Scenario.Factions;
            Faction targetFaction = factionlist.GetGameObject(targetFactionID) as Faction;
            /*
            if (targetFaction != null)
            {
                throw new Exception("targetFaction 为" + targetFaction.Name);
            }
            */

            if (person.BelongedFaction == null && person.LocationArchitecture != null && targetFaction != null)
            {
                //person.Status = GameObjects.PersonDetail.PersonStatus.Normal;
                person.MoveToArchitecture(targetFaction.Capital);
                person.ChangeFaction(targetFaction);
            }
            else if (person.LocationArchitecture != null && person.LocationArchitecture.BelongedFaction != null)
            {
                person.MoveToArchitecture(targetFaction.Capital);
                person.ChangeFaction(targetFaction);
                //person.ChangeFaction(targetFaction);
            }
        }

       

    }
}

