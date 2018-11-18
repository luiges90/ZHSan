using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TroopTab : BaseTab<Troop>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Troops);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"StartingArchitecture", "-1"},
                {"MilitaryID", "-1" },
                {"TargetTroopID", "-1"},
                {"TargetArchitectureID", "-1" },
                {"WillTroopID", "-1" },
                {"WillArchitectureID", "-1" },
                {"CurrentCombatMethodID", "-1" },
                {"CurrentStratagemID", "-1" },
                {"CurrentStunt", "-1" },
                {"ForceTroopTarget", "-1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "LeaderIDString",
                "PersonsString",
                "Food",
                "MilitaryID",
                "CaptivesString"
            };
        }

        public TroopTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
