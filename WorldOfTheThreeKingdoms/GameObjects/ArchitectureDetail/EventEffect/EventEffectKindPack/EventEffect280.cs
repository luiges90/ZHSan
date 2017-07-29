using GameManager;
using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{


    [DataContract]public class EventEffect280: EventEffectKind
    {
        private int  mergeFactionID;
        
        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.mergeFactionID = int.Parse(parameter);
                
            }
            catch
            {
                
            }
           
        }

        public override void ApplyEffectKind(Person person, Event e)
        {
            FactionList factionlist = Session.Current.Scenario.Factions;
            Faction oldFaction = person .BelongedFaction ;
            Faction mergeFaction = factionlist.GetGameObject(mergeFactionID) as Faction;

            if (oldFaction != null && mergeFaction != null && person == oldFaction.Leader)
            {
                oldFaction.ChangeFaction(mergeFaction);
                
                //oldFaction.Leader.InitialLoyalty();
            }
           
        }



    }
}

