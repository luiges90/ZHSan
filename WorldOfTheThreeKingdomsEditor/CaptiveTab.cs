using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class CaptiveTab : BaseTab<Captive>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.captiveData);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"CaptivePerson", "-1"},
                {"CaptiveFaction", "-1" },
                {"RansomArchitecture", "-1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "CaptivePersonID",
                "CaptiveFactionID",
                "RansomArchitectureID",
                "RansomFund",
                "RansomArriveDays",
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "CaptivePersonID", "俘虏人物" },
                { "CaptiveFactionID",  "俘虏势力" },
                { "RansomArchitectureID", "赎金目标建筑" },
                { "RansomFund",  "赎金金额" },
                { "RansomArriveDays", "赎金到达时间" },
            };
        }

        public CaptiveTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
