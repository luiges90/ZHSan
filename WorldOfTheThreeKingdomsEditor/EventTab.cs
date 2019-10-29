using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class EventTab : BaseTab<Event>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.AllEvents);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"AfterEventHappened", "-1"},
                {"Chance", "100" },
                {"StartMonth", "1" },
                {"EndYear", "99999" },
                {"EndMonth", "12" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "happened",
                "repeatable",
                "Minor",
                "AfterEventHappened",
                "happenChance",
                "GloballyDisplayed",
                "StartYear",
                "StartMonth",
                "EndYear",
                "EndMonth",
                "personString",
                "PersonCondString",
                "architectureString",
                "architectureCondString",
                "factionString",
                "factionCondString",
                "dialogString",
                "effectString",
                "architectureEffectString",
                "factionEffectIDString",
                "Image",
                "Sound",
                "yesdialogString",
                "nodialogString",
                "yesEffectString",
                "noEffectString",
                "yesArchitectureEffectString",
                "noArchitectureEffectString",
                "scenBiographyString"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Name",  "名称" },
                { "happened", "已发生过" },
                { "repeatable","可以重复" },
                { "Minor","不重要 不重要的事件不会出现对话，除非涉及君主" },
                { "AfterEventHappened","某事件发生之后 需要在某事件发生过之后才能触发" },
                { "happenChance","发动几率 实际机率为1除以此数" },
                { "GloballyDisplayed","全势力可见" },
                { "StartYear","开始年" },
                { "StartMonth","开始月" },
                { "EndYear","结束年" },
                { "EndMonth","结束月" },
                { "personString","武将编号 指定可能触发的武将ID，以空格分隔，先指定第k个武将，后跟随一个武将ID，如0 100 0 234 1 346 2 -1 可在同一个k指定多个武将，则代表列表中任何一个 -1代表任何武将" },
                { "PersonCondString","武将条件 触发的武将需符合的条件，以空格分隔，先指定第k个武将，后跟随一个武将ID" },
                { "architectureString","建筑编号 触发时，指定所有武将所在建筑的ID，以空格分隔 留空代表任何建筑" },
                { "architectureCondString","建筑条件 触发时，所有武将所在的建筑需符合的条件 如使用武将条件，将检查该建筑的县令" },
                { "factionString","势力编号 触发时，指定所有武将所在势力的ID，以空格分隔 留空代表任何势力" },
                { "factionCondString","势力条件 触发时，所有武将所在的势力需符合的条件 如使用武将条件，将检查该势力的君主" },
                { "dialogString","对话 先指定第k个武将，后跟随一段对话 以空格分隔 可使用%k表示第k个武将的姓名"},
                { "effectString","效果 先指定第k个武将 后跟随一个效果种类 以空格分隔" },
                { "architectureEffectString","建筑效果 武将所在建筑效果，以空格分隔 如使用武将效果，将应用于该建筑的县令" },
                { "factionEffectIDString","势力效果 武将所在势力效果，以空格分隔 如使用武将效果，将应用于该势力的君主" },
                { "Image","图片 图片档案放在Content目录Textures目录GameComponents目录tupianwenzi目录Data目录tupian里" },
                { "Sound","音效 音效档案放在Content目录Textures目录GameComponents目录tupianwenzi目录Data目录yinxiao里" },
                { "yesdialogString","选是的对话 先指定第k个武将，后跟随一段对话 以空格分隔 可使用%k表示第k个武将的姓名" },
                { "nodialogString","选否的对话 先指定第k个武将，后跟随一段对话 以空格分隔 可使用%k表示第k个武将的姓名" },
                { "yesEffectString","选是的效果 如果填上，这事件会有选项 选是后执行这些效果 先指定第k个武将，后跟随一个效果种类 以空格分隔" },
                { "noEffectString","选否的效果 如果填上，这事件会有选项 选否后执行这些效果 先指定第k个武将，后跟随一个效果种类 以空格分隔" },
                { "yesArchitectureEffectString","选是的建筑效果 武将所在建筑效果，以空格分隔 如使用武将效果，将应用于该建筑的县令" },
                { "noArchitectureEffectString","选否的建筑效果 武将所在建筑效果，以空格分隔 如使用武将效果，将应用于该建筑的县令" },
                { "scenBiographyString", "武将列传 先指定第k个武将，后跟随一段武将列传 以空格分隔 可使用%k表示第k个武将的姓名" },
                { "nextScenario", "下一剧本，暂时无用" }
        };
        }

        public EventTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }

        private string datagridname;
        private MainWindow mainWindow;
        public override void createWindow(bool edit, DataGrid dataGrid, MainWindow mainWindow)
        {
            if (!settingUp)
            {
                this.mainWindow = mainWindow;

                NewEventWindow newWindow = new NewEventWindow(edit, dataGrid, getScen());
                datagridname = dataGrid.Name;
                newWindow.Closed += NewWindow_Closed;
                newWindow.ShowDialog();
            }
        }

        private void NewWindow_Closed(object sender, EventArgs e)
        {
            if (!settingUp)
            {
                initdt();
                mainWindow.initTables(new string[] { });
            }
        }
    }
}
