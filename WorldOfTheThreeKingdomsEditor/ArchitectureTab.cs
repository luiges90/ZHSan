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
                "KindId",
                "IsStrategicCenter",
                "StateID",
                "CharacteristicsString",
                "ArchitectureAreaString",
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
                "zainan",
                "zainanshengyutianshu"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"ID","ID"},
                {"Name","名称"},
                {"AILandLinksString","陆上连接建筑"},
                {"AIWaterLinksString","水上连接建筑"},
                {"Agriculture","农业"},
                {"ArchitectureAreaString","区域"},
                {"AutoHiring","委任录用"},
                {"AutoRecruiting","委任补充"},
                {"AutoRefillFoodInLongViewArea","AutoRefillFoodInLongViewArea"},
                {"AutoRewarding","委任褒奖"},
                {"AutoSearching","委任搜索"},
                {"AutoWorking","委任工作"},
                {"AutoZhaoXian","委任招贤"},
                {"BuildingDaysLeft","建设剩余天数"},
                {"BuildingFacility","建设中设施"},
                {"CaptionID","名称图片编号"},
                {"CaptivesString","俘虏列表"},
                {"CharacteristicsString","特色"},
                {"Commerce","商业"},
                {"DefensiveLegion","DefensiveLegion"},
                {"DefensiveLegionID","防御军团ID"},
                {"Domination","统治"},
                {"Endurance","耐久"},
                {"FacilitiesString","设施列表"},
                {"FacilityEnabled","设施有效"},
                {"Food","粮草"},
                {"FoodPacksString","粮草包"},
                {"Fund","资金"},
                {"FundPacksString","资金包"},
                {"HasManualHire","HasManualHire"},
                {"HireFinished","录用已完成"},
                {"InformationsString","情报列表"},
                {"IsStrategicCenter","战略要冲"},
                {"KindId","种类"},
                {"MayorID","县令ID"},
                {"MayorOnDutyDays","MayorOnDutyDays"},
                {"MilitariesString","拥有的编队"},
                {"MilitaryPopulation","兵役人口"},
                {"Morale","民心"},
                {"MovingPersonsString","移动中人物"},
                {"NoFactionMovingPersonsString","移动中在野人物"},
                {"NoFactionPersonsString","在野人物"},
                {"OldFactionName","旧势力"},
                {"PersonsString","拥有人物"},
                {"PlanArchitectureID","计划建筑"},
                {"PlanFacilityKindID","计划设施"},
                {"Population","人口"},
                {"PopulationPacksString","人口包"},
                {"RecentlyAttacked","最近被攻击过"},
                {"RecentlyBreaked","最近被攻破过"},
                {"RobberTroopID","盗贼部队"},
                {"StateID","州域ID"},
                {"SuspendTroopTransfer","暂停士兵运输"},
                {"Technology","技术"},
                {"TodayPersonArriveNote","TodayPersonArriveNote"},
                {"TransferFoodArchitectureID","转移粮草目标"},
                {"TransferFundArchitectureID","转移资金目标"},
                {"TroopershipAvailable",""},
                {"feiziliebiaoString","妃子列表"},
                {"huangdisuozai","皇帝所在"},
                {"jianzhuqizi","jianzhuqizi"},
                {"noFundToSustainFacility","noFundToSustainFacility"},
                {"ConvinceDestinationPersonList","ConvinceDestinationPersonList"},
                {"captiveLoyaltyFall","captiveLoyaltyFall"},
                {"youzainan","灾难"},
                {"zainan","灾难类型"},
                {"MilitaryPopulationPacksString","兵役人口包"},

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
