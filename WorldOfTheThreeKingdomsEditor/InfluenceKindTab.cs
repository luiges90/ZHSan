using GameObjects;
using GameObjects.Influences;
using GameObjects.PersonDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class InfleunceKindTab : BaseTab<InfluenceKind>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllInfluenceKinds.InfluenceKinds);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"AIPersonValuePow", "1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Type",
                "Name",
                "Combat",
                "AIPersonValue",
                "AIPersonValuePow",
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"Type","种类" },
                {"Name","名称" },
                {"Combat","战斗" },
                {"AIPersonValue","武將AI值" },
                {"AIPersonValuePow","武將AI值乘冪" }
            };
        }

        public InfleunceKindTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
