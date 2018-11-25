using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TroopEventTab : BaseTab<TroopEvent>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.TroopEvents);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"AfterEventHappened", "-1"},
                {"Chance", "100" },
                {"LaunchPerson", "-1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "Happened",
                "Repeatable",
                "AfterEventHappened",
                "LaunchPersonString",
                "ConditionsString",
                "HappenChance",
                "TargetPersonsString",
                "SelfEffectsString",
                "EffectPersonsString",
                "EffectAreasString",
                "Image",
                "Sound"
            };
        }

        public TroopEventTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
