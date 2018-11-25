using GameObjects;
using GameObjects.ArchitectureDetail;
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
    class FacilityKindTab : BaseTab<FacilityKind>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllFacilityKinds.FacilityKinds);
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
                "AILevel",
                "PositionOccupied",
                "TechnologyNeeded",
                "FundCost",
                "MaintenanceCost",
                "PointCost",
                "Days",
                "Endurance",
                "ArchitectureLimit",
                "FactionLimit",
                "PopulationRelated",
                "InfluencesString",
                "ConditionTableString",
                "rongna",
                "bukechaichu"
            };
        }

        public FacilityKindTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
