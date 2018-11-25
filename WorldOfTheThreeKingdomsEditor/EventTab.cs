using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class EventTab : BaseTab<Event>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.AllEvents);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"AfterEventHappened", "-1"},
                {"Chance", "100" },
                {"StartMonth", "1" },
                {"EndYear", "99999" },
                {"EndMonth", "12" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "happened",
                "repeatable",
                "Minor",
                "AfterEventHappened",
                "happenChance",
                "GloballyDisplayed",
                "StartYear",
                "StartMonth",
                "EndYear",
                "EndMonth",
                "personString",
                "PersonCondString",
                "architectureString",
                "architectureCondString",
                "factionString",
                "factionCondString",
                "dialogString",
                "effectString",
                "architectureEffectString",
                "factionEffectIDString",
                "Image",
                "Sound",
                "yesdialogString",
                "nodialogString",
                "yesEffectString",
                "noEffectString",
                "yesArchitectureEffectString",
                "noArchitectureEffectString",
                "scenBiographyString"
            };
        }

        public EventTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
