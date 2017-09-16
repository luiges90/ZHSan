using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class ArchitectureTab : BaseTab<Architecture>
    {
        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {

            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new string[]
            {
            };
        }

        public ArchitectureTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
