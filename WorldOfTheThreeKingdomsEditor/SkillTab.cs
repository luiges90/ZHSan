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
    class SkillTab : BaseTab<Skill>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllSkills.Skills);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"GenerationChance","0 0 0 0 0 0 0 0 0 0" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "DisplayRow",
                "DisplayCol",
                "Kind",
                "Level",
                "Name",
                "Combat",
                "InfluencesString",
                "ConditionTableString",
                "Prerequisite",
                "Description",
                "GenerateConditionsString",
                "GenerationChance",
                "RelatedAbility"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "DisplayRow", "显示行" },
                { "DisplayCol", "显示列" },
                { "Kind", "类别" },
                { "Level", "等级" },
                { "Name", "名称" },
                { "Combat", "战斗" },
                { "InfluencesString", "影响列表" },
                { "ConditionTableString", "条件列表" },
                { "Prerequisite", "条件描述" },
                { "Description", "描述" },
                { "GenerateConditionsString", "生成武将条件" },
                { "GenerationChance","不同生成武将类型获得机率" },
                { "RelatedAbility", "此技能的相关能力、0-4为武统智政魅" },
            };
        }

        public SkillTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
