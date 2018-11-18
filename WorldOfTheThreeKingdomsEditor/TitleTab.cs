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
    class TitleTab : BaseTab<Title>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllTitles.Titles);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"MapLimit", "9999" },
                {"FactionLimit", "9999" },
                {"InheritChance", "20" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Kind",
                "Level",
                "Name",
                "Combat",
                "ManualAward",
                "InfluencesString",
                "ConditionsString",
                "Prerequisite",
                "Description",
                "GenerateConditionsString",
                "ArchitectureConditionsString",
                "FactionConditionsString",
                "LostConditionsString",
                "AutoLearn",
                "AutoLearnText",
                "AutoLearnTextByCourier",
                "MapLimit",
                "FactionLimit",
                "InheritChance",
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

        public TitleTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
