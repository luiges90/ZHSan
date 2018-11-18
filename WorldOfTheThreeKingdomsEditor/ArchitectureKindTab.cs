using GameObjects;
using GameObjects.ArchitectureDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class ArchitectureKindTab : BaseTab<ArchitectureKind>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllArchitectureKinds.ArchitectureKinds);
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
                "HasAgriculture",
                "AgricultureBase",
                "AgricultureUnit",
                "HasCommerce",
                "CommerceBase",
                "CommerceUnit",
                "HasTechnology",
                "TechnologyBase",
                "TechnologyUnit",
                "HasDomination",
                "DominationBase",
                "DominationUnit",
                "HasMorale",
                "MoraleBase",
                "MoraleUnit",
                "HasEndurance",
                "EnduranceBase",
                "EnduranceUnit",
                "HasPopulation",
                "PopulationBase",
                "PopulationUnit",
                "PopulationBoundary",
                "ViewDistance",
                "VDIncrementDivisor",
                "HasObliqueView",
                "HasLongView",
                "HasHarbor",
                "ShipCanEnter",
                "FacilityPositionUnit",
                "FundMaxUnit",
                "FoodMaxUnit",
                "CountToMerit",
                "Expandable"
            };
        }

        public ArchitectureKindTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
