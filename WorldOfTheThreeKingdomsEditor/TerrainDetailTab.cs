using GameObjects;
using GameObjects.MapDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TerrainDetailTab : BaseTab<TerrainDetail>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.GameCommonData.AllTerrainDetails.GetTerrainDetailList());
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                //{"CaptivePerson", "-1"},
                //{"CaptiveFaction", "-1" },
                //{"RansomArchitecture", "-1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "GraphicLayer",
                "ViewThrough",
                "RoutewayBuildFundCost",
                "RoutewayActiveFundCost",
                "RoutewayBuildWorkCost",
                "RoutewayConsumptionRate",
                "FoodDeposit",
                "FoodRegainDays",
                "FoodSpringRate",
                "FoodSummerRate",
                "FoodAutumnRate",
                "FoodWinterRate",
                "FireDamageRate",
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "ID","ID" },
                { "Name","名称"},
                { "GraphicLayer","图形层次" },
                { "ViewThrough", "视线可穿透"},
                { "RoutewayBuildFundCost","粮道开通资金消耗" },
                { "RoutewayActiveFundCost", "粮道维持资金消耗"},
                { "RoutewayBuildWorkCost","粮道开通工作量"},
                { "RoutewayConsumptionRate","粮草消耗率"},
                { "FoodDeposit","粮草蕴藏量"},
                { "FoodRegainDays","粮草恢复天数"},
                { "FoodSpringRate","春粮系数"},
                { "FoodSummerRate","夏粮系数"},
                { "FoodAutumnRate","秋粮系数"},
                { "FoodWinterRate","冬粮系数"},
                { "FireDamageRate","火焰伤害率"}
            };
        }

        public TerrainDetailTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
