using GameObjects;
using System;


using System.Runtime.Serialization;namespace GameObjects.ArchitectureDetail.EventEffect
{

    [DataContract]public class EventEffect2200 : EventEffectKind
    {
        private int increment;

        public override void ApplyEffectKind(Faction f, Event e)
        {
            GameObjectList d = base.Scenario.DiplomaticRelations.GetDiplomaticRelationListByFactionID(f.ID);
            foreach (GameObjects.FactionDetail.DiplomaticRelation i in d)
            {
                i.Relation += increment;
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

