using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TroopEventTab : BaseTab<TroopEvent>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.TroopEvents);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"AfterEventHappened", "-1"},
                {"Chance", "100" },
                {"LaunchPerson", "-1" }
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Name", "名称" },
                { "Happened", "已发生过" },
                { "Repeatable", "可以重复" },
                { "AfterEventHappened", "某事件发生之后：需要在某事件发生过之后才能触发" },
                { "LaunchPersonString", "发动人物：发动事件的人物，所有效果将以本人物所在部队为中心。如果为-1，则每个部队都可以触发此事件。" },
                { "dialogString", "人物对话：每个人物ID后跟随其要说的话。以空格分隔。留空则无对话。" },
                { "ConditionsString", "发动条件：发动事件的部队所需要满足的条件，留空则为满足。" },
                { "HappenChance", "发动几率：0--100" },
                { "TargetPersonsString", "目标人物列表：每个关系之后跟随一个人物ID。0：非友好；1：友好。留空则不在搜索范围检查此条件。" },
                { "SelfEffectsString", "自身效果：发动部队的效果列表" },
                { "EffectPersonsString", "特定人物效果：每个人物ID后跟随一个效果种类。以空格分隔。" },
                { "EffectAreasString", "特定范围效果：范围类别：0：视野内所有敌军；1：视野内所有友军；2：攻击范围内所有敌军；3：攻击范围内所有友军；4：周围八格内所有敌军；5：周围八格内所有友军；每个范围类别后跟随一个效果种类。以空格分隔。" },
                { "Image","图片。图片档案放在Content目录Textures目录GameComponents目录tupianwenzi目录Data目录tupian里" },
                { "Sound","音效。音效档案放在Content目录Textures目录GameComponents目录tupianwenzi目录Data目录yinxiao里" },
                { "CheckArea","0--视野内 1--周边八格 2--攻击范围 用来搜索目标人物列表" },
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "Happened",
                "Repeatable",
                "AfterEventHappened",
                "LaunchPersonString",
                "dialogString",
                "ConditionsString",
                "HappenChance",
                "TargetPersonsString",
                "SelfEffectsString",
                "EffectPersonsString",
                "EffectAreasString",
                "Image",
                "Sound"
            };
        }

        public TroopEventTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
