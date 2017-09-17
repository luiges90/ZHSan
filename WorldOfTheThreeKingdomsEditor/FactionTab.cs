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

            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new string[]
            {
            };
        }

        protected override GameObjectList GetDataList(GameScenario scen)
        {
            return scen.Factions;
        }

        public FactionTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
