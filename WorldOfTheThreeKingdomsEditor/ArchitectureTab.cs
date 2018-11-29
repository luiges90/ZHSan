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
                "huangdisuozai",
                "youzainan",
                "zainanleixing",
                "zainanshengyutianshu"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "CaptionID", "名称编号" },
                { "Name", "名称" },
                { "KindID", "种类ID" },
                { "IsStrategicCenter", "战略要冲" },
                { "StateID", "州域ID" },
                { "CharacteristicsString", "特色" },
                { "Area","区域" },
                { "MayorID","县令ID" },
                { "PersonsString","所属人物" },
                { "MovingPersonsString","移动中人物" },
                { "NoFactionPersonsString","在野人物" },
                { "NoFactionMovingPersonsString","移动中在野人物" },
                { "feiziliebiaoString","妃子列表" },
                { "CaptivesString","俘虏列表" },
                { "Population","人口" },
                { "MilitaryPopulation","兵役人口" },
                { "Fund","资金" },
                { "Food","粮草" },
                { "Agriculture","农业" },
                { "Commerce","商业" },
                { "Technology","技术" },
                { "Domination","统治" },
                { "Morale","民心" },
                { "Endurance","耐久" },
                { "MilitariesString","拥有的编队" },
                { "FacilitiesString","设施列表" },
                { "BuildingFacility","建设中设施" },
                { "BuildingDaysLeft","建设剩余天数" },
                { "InformationsString","情报列表" },
                { "huangdisuozai","皇帝" },
                { "youzainan", "灾难" },
                { "zainanleixing", "灾难类型"},
                { "zainanshengyutianshu", "灾难剩余天数" }
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
