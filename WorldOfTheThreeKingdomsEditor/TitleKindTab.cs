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
    class TitleKindTab : BaseTab<TitleKind>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllTitleKinds.TitleKinds);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                //{"AIPersonValuePow", "1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "Combat",
                "StudyDay",
                "SuccessRate",
                "Recallable",
                "RandomTeachable",
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"Name","名称" },
                {"Combat","战斗" },
                {"StudyDay","习得天数" },
                {"SuccessRate","习得成功率" },
                {"Recallable","可免除" },
                {"RandomTeachable","可额外传授" }
            };
        }

        public TitleKindTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
