using GameManager;
using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{


    [DataContract]public class EventEffect2300 : EventEffectKind
    {
        private int increment;
        private int targetFactionID;

        public override void ApplyEffectKind(Faction f, Event e)
        {
            GameObjectList d = Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(f.ID);
            Faction f2 = Session.Current.Scenario.Factions.GetGameObject(targetFactionID) as Faction;
            //GameObjectList c = Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(f2.ID);
            foreach (GameObjects.FactionDetail.DiplomaticRelation i in d)
            {
                if ((i.RelationFaction1ID == f.ID && i.RelationFaction2ID == f2.ID) || (i.RelationFaction1ID == f2.ID && i.RelationFaction2ID == f.ID))
                {
                    Session.Current.Scenario.ChangeDiplomaticRelation(f.ID, f2.ID, increment);
                }
            }
               
            //throw new Exception("targetFactionID=" + targetFactionID + "f的ID=" + f.ID);
        }

        public override void InitializeParameter(string parameter)
        {
            try
            {
                this.increment = int.Parse(parameter);
            }
            catch
            {
            }
        }
        public override void InitializeParameter2(string parameter)
        {
            try
            {
                this.targetFactionID = int.Parse(parameter);
            }
            catch
            {
            }
        }
    }
}
