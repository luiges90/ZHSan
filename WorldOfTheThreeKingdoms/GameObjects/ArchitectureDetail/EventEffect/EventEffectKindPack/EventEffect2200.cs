using GameManager;
using GameObjects;
using System;


using System.Runtime.Serialization;
using Tools;

namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect2200 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Faction f, Event e)
        {
            try
            {
                if (f == null)
                {
                    throw new Exception("f=null");
                }
                if (Session.Current.Scenario.DiplomaticRelations == null)
                {
                    throw new Exception("Session.Current.Scenario.DiplomaticRelations=null");
                }
                GameObjectList d = Session.Current.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(f.ID);
                if (d == null)
                {
                    throw new Exception("d=null");
                }
                foreach (GameObjects.FactionDetail.DiplomaticRelation i in d)
                {
                    i.Relation += increment;
                }
            }
            catch (Exception ex)
            {
                WebTools.TakeWarnMsg("ApplyEffectKind:ID" + ID, "", ex);
            }
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
    }
}

