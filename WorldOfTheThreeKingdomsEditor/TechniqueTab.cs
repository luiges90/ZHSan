using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;
using GameObjects.FactionDetail;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TechniqueTab : BaseTab<Technique>
    {
        public TechniqueTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }

        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllTechniques.Techniques);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Kind", "种类" },
                { "Name", "名称" },
                { "Description", "描述" },
                { "Days", "升级时间" },
                { "FundCost", "资金消耗" },
                { "PointCost", "技巧点数消耗" },
                { "Reputation", "需要声望" },
                { "InfluencesString", "影响列表" },
                { "PreID", "前置所需科技ID" },
                { "PostID", "后置可学科技ID" },
                { "DisplayCol", "显示列" },
                { "DisplayRow", "显示行" },
                { "AIConditionWeightString", "AI条件列表" },
                { "ConditionTableString", "条件列表" },
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Kind",
                "Name",
                "Description",
                "Days",
                "FundCost",
                "PointCost",
                "Reputation",
                "InfluencesString",
                "PreID",
                "PostID",
                "DisplayCol",
                "DisplayRow",
                "AIConditionWeightString",
                "ConditionTableString"
            };
        }
    }
}
