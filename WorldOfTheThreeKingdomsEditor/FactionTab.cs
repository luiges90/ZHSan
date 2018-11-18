using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class FactionTab : BaseTab<Faction>
    {
        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"CapitalID", "-1"},
                {"PlanTechnique", "-1"},
                {"PrinceID", "-1"},
                {"BaseMilitaryKindsString", "0 1"},
                {"UpgradingTechnique", "-1" },
                {"AvailableTechniquesString", ""}
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new string[]
            {
                "ID",
                "Name",
                "LeaderID",
                "PrinceID",
                "ColorIndex",
                "FName",
                "CapitalID",
                "Reputation",
                "ArchitecturesString",
                "BaseMilitaryKindsString",
                "TechniquePoint",
                "TechniquePointForTechnique",
                "TechniquePointForFacility",
                "SectionsString",
                "InformationsString",
                "TroopsString",
                "RoutewaysString",
                "LegionsString",
                "chaotinggongxiandu",
                "guanjue",
                "UpgradingTechnique",
                "UpgradingDaysLeft",
                "AvailableTechniquesString",
                "PreferredTechniqueKinds",
                "PlanTechnique",
                "IsAlien",
                "NotPlayerSelectable"
            };
        }

        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Factions);
        }

        public FactionTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
