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
    class StuntTab : BaseTab<Stunt>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllStunts.Stunts);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                { "Period", "1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "Combativity",
                "Period",
                "Animation",
                "InfluencesString",
                "CastConditionsString",
                "LearnConditionsString",
                "AIConditionsString",
                "GenerateConditionsString",
                "General",
                "Brave",
                "Advisor",
                "Politician",
                "IntelGeneral",
                "Emperor",
                "AllRounder",
                "Normal",
                "Normal2",
                "Cheap",
                "Ability"
            };
        }

        public StuntTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
