using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class SectionAIDetailTab : BaseTab<GameObjects.SectionDetail.SectionAIDetail>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllSectionAIDetails.SectionAIDetails);
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
                "Description",
                "OrientationKind",
                "AutoRun",
                "ValueAgriculture",
                "ValueCommerce",
                "ValueTechnology",
                "ValueDomination",
                "ValueMorale",
                "ValueEndurance",
                "ValueTraining",
                "ValueRecruitment",
                "ValueNewMilitary",
                "ValueOffensiveCampaign",
                "AllowInvestigateTactics",
                "AllowOffensiveTactics",
                "AllowPersonTactics",
                "AllowOffensiveCampaign",
                "AllowFundTransfer",
                "AllowFoodTransfer",
                "AllowMilitaryTransfer",
                "AllowFacilityRemoval",
                "AllowNewMilitary",
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"ID","ID"},
                {"Name","名称"},
                {"Description","说明"},
                {"OrientationKind","目标种类"},
                {"AutoRun","是否自动执行"},
                {"ValueAgriculture","重视农业"},
                {"ValueCommerce","重视商业"},
                {"ValueTechnology","重视技术"},
                {"ValueDomination","重视统治"},
                {"ValueMorale","重视民心"},
                {"ValueEndurance","重视耐久"},
                {"ValueTraining","重视训练"},
                {"ValueRecruitment","重视补充"},
                {"ValueNewMilitary","重视新建编队"},
                {"ValueOffensiveCampaign","重视攻击"},
                {"AllowInvestigateTactics","允许使用情报和间谍"},
                {"AllowOffensiveTactics","允许使用煽动和破坏"},
                {"AllowPersonTactics","允许使用流言和说服"},
                {"AllowOffensiveCampaign","允许攻击"},
                {"AllowFundTransfer","允许输送资金"},
                {"AllowFoodTransfer","允许输送粮草"},
                {"AllowMilitaryTransfer","允许输送部队"},
                {"AllowFacilityRemoval","允许拆除设施"},
                {"AllowNewMilitary","允许新编编队"},

            };
        }

        public SectionAIDetailTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
