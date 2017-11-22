using GameObjects;
using GameObjects.TroopDetail.EventEffect;
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
    class TroopEventEffectKindTab : BaseTab<EventEffectKind>
    {
        protected override GameObjectList GetDataList(GameScenario scen)
        {
            return scen.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKindList();
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
                "Name"
            };
        }

        public TroopEventEffectKindTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
