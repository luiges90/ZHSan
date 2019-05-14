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

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Name", "名称" },
                { "HasAgriculture", "农业" },
                { "AgricultureBase", "农业基础 建筑农业值上限为：农业基础+农业单位*建筑规模" },
                { "AgricultureUnit", "农业单位" },
                { "HasCommerce", "商业" },
                { "CommerceBase", "商业基础 建筑商业值上限为：商业基础+商业单位*建筑规模" },
                { "CommerceUnit", "商业单位" },
                { "HasTechnology", "技术" },
                { "TechnologyBase", "技术基础 建筑技术值上限为：技术基础+技术单位*建筑规模" },
                { "TechnologyUnit", "技术单位" },
                { "HasDomination", "统治" },
                { "DominationBase", "统治基础 建筑统治值上限为：统治基础+ 统治单位*建筑规模" },
                { "DominationUnit", "统治单位" },
                { "HasMorale", "民心" },
                { "MoraleBase", "民心基础 建筑民心值上限为：民心基础+民心单位*建筑规模" },
                { "MoraleUnit", "民心单位" },
                { "HasEndurance", "耐久" },
                { "EnduranceBase", "耐久基础 建筑耐久值上限为：耐久基础+耐久单位*建筑规模" },
                { "EnduranceUnit", "耐久单位" },
                { "HasPopulation", "人口" },
                { "PopulationBase", "人口基础 建筑人口值上限为：人口基础+人口单位*建筑规模" },
                { "PopulationUnit", "人口单位" },
                { "ViewDistance", "基本视野范围" },
                { "VDIncrementDivisor", "视野范围增量除数 视野距离=基本视野范围+建筑规模除以此值" },
                { "HasObliqueView", "斜向视野" },
                { "HasLongView", "长视距" },
                { "HasHarbor", "可编组运兵船" },
                { "FacilityPositionUnit", "设施单位空间 此值*建筑规模为最终值" },
                { "FundMaxUnit", "最大资金单位 此值*建筑规模为最终值" },
                { "FoodMaxUnit", "最大粮草单位 此值*建筑规模为最终值" },
                { "CountToMerit", "是否属于官职所需城池类型" },
                { "Expandable", "可扩建 4为可扩建一次，8为可扩建两次" },
                { "PopulationBoundary", "征兵人口界限" },
                { "ViewDistanceIncrementDivisor", "视野范围增量除数" },
                { "ShipCanEnter", "船只可进入" },
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
                "ViewDistanceIncrementDivisor",
                "HasObliqueView",
                "HasLongView",
                "HasHarbor",
                "FacilityPositionUnit",
                "FundMaxUnit",
                "FoodMaxUnit",
                "CountToMerit",
                "Expandable"
            };
        }

        public ArchitectureKindTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
