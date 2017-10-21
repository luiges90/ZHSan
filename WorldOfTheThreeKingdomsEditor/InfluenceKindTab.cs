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
        protected override GameObjectList GetDataList(GameScenario scen)
        {
            return scen.GameCommonData.AllInfluenceKinds.GetInfluenceKindList();
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

        public InfleunceKindTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
