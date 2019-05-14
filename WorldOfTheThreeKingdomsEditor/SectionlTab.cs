using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class SectionTab : BaseTab<Section>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Sections);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "AIDetailIDString",
                "OrientationFactionID",
                "OrientationSectionID",
                "OrientationStateID",
                "OrientationArchitectureID",
                "ArchitecturesString",
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"ID","ID"},
                {"Name","名称"},
                {"AIDetailIDString","委任类型"},
                {"OrientationFactionID","目标势力"},
                {"OrientationSectionID","目标军团"},
                {"OrientationStateID","目标州域"},
                {"OrientationArchitectureID","目标建筑"},
                {"ArchitecturesString","建筑列表"},

            };
        }

        public SectionTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
