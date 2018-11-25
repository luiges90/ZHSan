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
                {"MayorID", "-1"},
                {"BuildingFacility", "-1"},
                {"PlanFacilityKind", "-1"},
                {"PlanArchitecture", "-1"},
                {"TransferFundArchitecture", "-1"},
                {"TransferFoodArchitecture", "-1"},
                {"DefensiveLegion", "-1"},
                {"RobberTroop", "-1"}
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new string[]
            {
                "ID",
                "CaptionID",
                "Name",
                "KindID",
                "IsStrategicCenter",
                "StateID",
                "CharacteristicsString",
                "Area",
                "MayorID",
                "PersonsString",
                "MovingPersonsString",
                "NoFactionPersonsString",
                "NoFactionMovingPersonsString",
                "feiziliebiaoString",
                "CaptivesString",
                "Population",
                "MilitaryPopulation",
                "Fund",
                "Food",
                "Agriculture",
                "Commerce",
                "Technology",
                "Domination",
                "Morale",
                "Endurance",
                "MilitariesString",
                "FacilitiesString",
                "BuildingFacility",
                "BuildingDaysLeft",
                "InformationsString",
                "huangdisuozai"
            };
        }

        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Architectures);
        }

        public ArchitectureTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
