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
                {"InheritChance", "20" },
                {"GenerationChance","0 0 0 0 0 0 0 0 0 0" }
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

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Kind", "类别" },
                { "Level", "等级" },
                { "Name", "名称" },
                { "Combat", "战斗" },
                { "ManualAward", "手动授予" },
                { "InfluencesString", "影响列表" },
                { "ConditionsString", "条件列表" },
                { "Prerequisite", "条件" },
                { "Description", "描述" },
                { "GenerateConditionsString", "生成武將条件" },
                { "ArchitectureConditionsString", "建筑条件" },
                { "FactionConditionsString", "势力条件" },
                { "LostConditionsString", "失去条件" },
                { "AutoLearn", "自动习得机率：每天有1除以此数的机率自动习得这个称号。0为不会自动习得" },
                { "AutoLearnText", "习得对话" },
                { "AutoLearnTextByCourier", "习得传令官对话" },
                { "MapLimit", "全地图数目上限" },
                { "FactionLimit", "势力数目上限" },
                { "InheritChance", "继承机率" },
                { "General", "将军型武将(如夏侯惇、关羽、孙策等)能取得此技能的机率参数" },
                { "Brave", "勇猛型武将(如典韦、胡车儿等)能取得此技能的机率参数" },
                { "Advisor", "军师型武将(如诸葛亮、司马懿、陆逊等)能取得此技能的机率参数" },
                { "Politician", "识者型武将(如陈群、董允等)能取得此技能的机率参数" },
                { "IntelGeneral", "智将型武将(如姜维、甘宁等)能取得此技能的机率参数" },
                { "Emperor", "君主型武将(如刘备、孙权等)能取得此技能的机率参数" },
                { "AllRounder", "全能型武将能取得此技能的机率参数" },
                { "Normal", "平凡文官型武将能取得此技能的机率参数" },
                { "Normal2", "平凡武官型武将能取得此技能的机率参数" },
                { "Cheap", "平凡女官型武将能取得此技能的机率参数" },
                { "Ability", "此技能的相关能力、0-4为武统智政魅" },
                {"GenerationChance","不同生成武将类型获得机率" }
            };
        }


        public TitleTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
