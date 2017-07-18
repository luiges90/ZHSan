using GameObjects.FactionDetail;
using System;
using System.Runtime.Serialization;

namespace GameObjects
{
    [DataContract]
    public class FactionList : GameObjectList
    {
        public void AddFactionWithEvent(Faction faction, bool add = true)
        {
            if (add)
            {
                base.GameObjects.Add(faction);
            }
            if (faction.Scenario.GameScreen != null)
            {
                faction.OnGetControl += new Faction.GetControl(this.faction_OnGetControl);
                faction.OnFactionDestroy += new Faction.FactionDestroy(this.faction_OnFactionDestroy);
                faction.OnAfterCatchLeader += new Faction.AfterCatchLeader(this.faction_OnAfterCatchLeader);
                faction.OnUpgradeTechnique += new Faction.FactionUpgradeTechnique(this.faction_OnUpgradeTechnique);
                faction.OnInitiativeChangeCapital += new Faction.InitiativeChangeCapital(this.faction_OnInitiativeChangeCapital);
                faction.OnForcedChangeCapital += new Faction.ForcedChangeCapital(this.faction_OnForcedChangeCapital);
                faction.OnTechniqueFinished += new Faction.TechniqueFinished(this.faction_OnTechniqueFinished);
            }
        }

        public void ApplyInfluences()
        {
            foreach (Faction faction in base.GameObjects)
            {
                faction.ApplyTechniques();
            }
        }

        private void faction_OnAfterCatchLeader(Person leader, Faction faction)
        {
            faction.Scenario.GameScreen.FactionAfterCatchLeader(leader, faction);
        }

        private void faction_OnFactionDestroy(Faction faction)
        {
            faction.Scenario.GameScreen.FactionDestroy(faction);
        }

        private void faction_OnForcedChangeCapital(Faction faction, Architecture oldCapital, Architecture newCapital)
        {
            faction.Scenario.GameScreen.FactionForcedChangeCapital(faction, oldCapital, newCapital);
        }

        private void faction_OnGetControl(Faction faction)
        {
            faction.Scenario.GameScreen.FactionGetControl(faction);
        }

        private void faction_OnInitiativeChangeCapital(Faction faction, Architecture oldCapital, Architecture newCapital)
        {
            faction.Scenario.GameScreen.FactionInitialtiveChangeCapital(faction, oldCapital, newCapital);
        }

        private void faction_OnTechniqueFinished(Faction faction, Technique technique)
        {
            faction.Scenario.GameScreen.FactionTechniqueFinished(faction, technique);
        }

        private void faction_OnUpgradeTechnique(Faction faction, Technique technique, Architecture architecture)
        {
            faction.Scenario.GameScreen.FactionUpgradeTechnique(faction, technique, architecture);
        }

        public void RemoveFaction(Faction faction)
        {
            if (base.HasGameObject(faction))
            {
                foreach (Architecture architecture in faction.Architectures.GetList())
                {
                    faction.RemoveArchitecture(architecture);
                }
                foreach (Troop troop in faction.Troops.GetList())
                {
                    faction.RemoveTroop(troop);
                }
                foreach (Legion legion in faction.Legions.GetList())
                {
                    faction.RemoveLegion(legion);
                }
                foreach (Section section in faction.Sections.GetList())
                {
                    faction.RemoveSection(section);
                }
                base.GameObjects.Remove(faction);
            }
        }
    }
}

