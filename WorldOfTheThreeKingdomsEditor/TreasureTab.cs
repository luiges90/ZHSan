using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TreasureTab : BaseTab<Treasure>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Treasures);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"HidePlaceIDString", "-1"},
                {"BelongedPersonIDString", "-1" }
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Name", "名称" },
                { "Pic", "图像" },
                { "Worth", "价值" },
                { "Available", "已出现" },
                { "HidePlaceIDString", "隐藏于建筑" },
                { "TreasureGroup", "宝物种类：此值相同的话，这些宝物不叠加" },
                { "AppearYear", "出现年" },
                { "BelongedPersonIDString", "属于人物" },
                { "InfluencesString", "影响列表" },
                { "Description", "介绍" },
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "Pic",
                "Worth",
                "Available",
                "HidePlaceIDString",
                "TreasureGroup",
                "AppearYear",
                "BelongedPersonIDString",
                "InfluencesString",
                "Description"
            };
        }

        public TreasureTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
