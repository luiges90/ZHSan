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
        public TechniqueTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
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
