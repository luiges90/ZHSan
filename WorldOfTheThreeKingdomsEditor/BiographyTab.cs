using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;
using GameObjects.PersonDetail;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class BiographyTab : BaseTab<GameObjects.PersonDetail.Biography>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.AllBiographies.Biographys);
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
                "FactionColor",
                "MilitaryKindsString",
                "Brief",
                "History",
                "Romance",
                "InGame"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"Name","姓名" },
                {"Brief","简要" },
                {"Romance","演义" },
                {"History","历史" },
                {"InGame","剧本" },
                {"FactionColor","势力颜色 此武将自立时使用的势力颜色" },
                {"MilitaryKindsString","兵种列表 此武将自立时使用的基本兵种" }
            };
        }

        public BiographyTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
