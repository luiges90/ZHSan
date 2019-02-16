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
		public string CaptiveID_CHS = "编号";
		public string CaptiveIDDesc_CHS = "俘虏表单的编号。";
		public string CaptiveCharacterID_CHS = "俘虏编号";
		public string CaptiveCharacterIDDesc_CHS = "被俘人物的编号。";
		public string CaptiveCharFactionID_CHS = "俘虏势力";
		public string CaptiveCharFactionIDDesc_CHS = "被俘人物的势力编号。";
		public string RansomReceivedCityID_CHS = "受赎城池";
		public string RansomReceivedCityIDDesc_CHS = "设定接收赎金的城池。";
		public string RansomArrivalDays_CHS = "抵达天数";
		public string RansomArrivalDaysDesc_CHS = "设定赎金抵达的天数。";
		public string RansomAmount_CHS = "赎金金额";
		public string RansomAmountDesc_CHS = "设定赎金的金额。";
		
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Captives);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"CaptiveCharacterID", "-1"},
                {"CaptiveCharFactionID", "-1" },
                {"RansomReceivedCityID", "-1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "CaptiveCharacterID",
                "CaptiveCharFactionID",
				"RansomReceivedCityID",
				"RansomArrivalDays",
				"RansomAmount",
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { CaptiveID_CHS, CaptiveIDDesc_CHS },
                { CaptiveCharacterID_CHS, CaptiveCharacterIDDesc_CHS },
                { CaptiveCharFactionID_CHS, CaptiveCharFactionIDDesc_CHS },
                { RansomReceivedCityID_CHS, RansomReceivedCityIDDesc_CHS },
                { RansomArrivalDays_CHS, RansomArrivalDaysDesc_CHS },
                { RansomAmount_CHS, RansomAmountDesc_CHS },
            };
        }

        public CaptiveTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
