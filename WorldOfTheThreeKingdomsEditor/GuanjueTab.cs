using GameObjects;
using GameObjects.PersonDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class GuanjueTab : BaseTab<guanjuezhongleilei>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.suoyouguanjuezhonglei.guanjuedezhongleizidian);
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
                "shengwangshangxian",
                "xuyaogongxiandu",
                "xuyaochengchi",
                "ShowDialog",
                "Loyalty"

            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Name", "名称" },
                { "shengwangshangxian", "声望上限" },
                { "xuyaogongxiandu", "封官所需朝廷贡献度" },
                { "xuyaochengchi", "封官所需城池列表" },
                { "ShowDialog", "是否全地图显示封官对话" },
                { "Loyalty", "手下武将忠诚度变化值" },

            };
        }

        public GuanjueTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
