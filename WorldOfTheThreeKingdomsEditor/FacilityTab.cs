using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class FacilityTab : BaseTab<Facility>
    {
        protected override GameObjectList GetDataList(GameScenario scen)
        {
            return scen.Facilities;
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
                "KindID",
                "Endurance"
            };
        }

        public FacilityTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
