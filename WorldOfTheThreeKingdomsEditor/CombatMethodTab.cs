using GameObjects;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class CombatMethodTab : BaseTab<CombatMethod>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllCombatMethods.CombatMethods);
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
                "Combativity",
                "InfluencesString",
                "AttackDefault",
                "AttackTarget",
                "ArchitectureTarget",
                "CastConditionsString",
                "ViewingHostile",
                "AnimationKind"
            };
        }

        public CombatMethodTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
