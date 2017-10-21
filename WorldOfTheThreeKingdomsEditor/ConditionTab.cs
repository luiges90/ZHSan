using GameObjects;
using GameObjects.Conditions;
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
    class ConditionTab : BaseTab<Condition>
    {
        protected override GameObjectList GetDataList(GameScenario scen)
        {
            return scen.GameCommonData.AllConditions.GetConditionList();
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
                "Parameter",
                "Parameter2",
            };
        }

        public ConditionTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
