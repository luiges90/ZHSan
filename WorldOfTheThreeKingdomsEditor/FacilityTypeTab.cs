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
    class FacilityTypeTab : BaseTab<FacilityKind>
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
                "EffectsString",
                "ConditionTableString",
                "rongna",
                "bukechaichu"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"Name","名称" },
                {"Description","描述" },
                {"AILevel","AI强度" },
                {"PositionOccupied","占用位置" },
                {"TechnologyNeeded","新建所需技术" },
                {"FundCost","新建所需资金" },
                {"MaintenanceCost","维持费用。每天所需资金维持此设施，否则不会运作" },
                {"PointCost","新建所需技巧" },
                {"Days","建造所需时间" },
                {"Endurance","耐久度" },
                {"ArchitectureLimit","建筑上限" },
                {"FactionLimit","势力上限" },
                {"PopulationRelated","人口相关" },
                {"EffectsString","影响" },
                {"ConditionTableString","兴建条件" },
                {"rongna","可容纳妃子数" },
                {"bukechaichu","不可拆除" }
            };
        }

        public FacilityTypeTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
