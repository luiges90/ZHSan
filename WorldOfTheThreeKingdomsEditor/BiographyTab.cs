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
                "FactionColor",
                "MilitaryKindsString",
                "Brief",
                "History",
                "Romance",
                "InGame"
            };
        }

        public BiographyTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
