using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class FactionTab : BaseTab<Faction>
    {
        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"CapitalID", "-1"},
                {"PlanTechnique", "-1"},
                {"PrinceID", "-1"},
                {"BaseMilitaryKindsString", "0 1 3"},
                {"UpgradingTechnique", "-1" },
                {"AvailableTechniquesString", ""}
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new string[]
            {
                "ID",
                "Name",
                "LeaderID",
                "PrinceID",
                "ColorIndex",
                "FName",
                "CapitalID",
                "Reputation",
                "ArchitecturesString",
                "BaseMilitaryKindsString",
                "TechniquePoint",
                "TechniquePointForTechnique",
                "TechniquePointForFacility",
                "SectionsString",
                "InformationsString",
                "TroopsString",
                "RoutewaysString",
                "chaotinggongxiandu",
                "guanjue",
                "UpgradingTechnique",
                "UpgradingDaysLeft",
                "AvailableTechniquesString",
                "PreferredTechniqueKinds",
                "IsAlien",
                "NotPlayerSelectable"
            };
        }

         protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"ID","ID"},
                {"LeaderID","君主ID"},
                {"ColorIndex","颜色编号"},
                {"Name","势力名"},
                {"CapitalID","都城ID"},
                {"TechniquePoint","技巧点数"},
                {"TechniquePointForTechnique","为升级技巧所保留的技巧点数"},
                {"TechniquePointForFacility","为建造设施所保留的技巧点数"},
                {"Reputation","声望"},
                {"SectionsString","军区列表"},
                {"InformationsString","情报列表"},
                {"ArchitecturesString","建筑列表"},
                {"TroopListString","部队列表"},
                {"RoutewaysString","粮道列表"},
                {"LegionsString","军团列表"},
                {"BaseMilitaryKindsString","基本兵种列表"},
                { "UpgradingTechnique","正在升级中的技巧"},
                {"UpgradingDaysLeft","正在升级中的技巧剩余时间"},
                {"AvailableTechniquesString","已有技巧"},
                {"PreferredTechniqueKinds","偏好技巧类别"},
                {"PlanTechniqueString","计划技巧"},
                {"AutoRefuse","自动拒绝释放俘虏"},
                {"chaotinggongxiandu","朝廷贡献度"},
                {"guanjue","官爵"},
                {"IsAlien","异族"},
                {"NotPlayerSelectable","玩家不可选"},
                {"PrinceID","储君ID"},
                {"YearOfficialLimit","本年已招贤数量"},
                {"MilitaryCount","编队总数"},
                {"TransferingMilitaryCount","运输编队总数"},
                {"GetGeneratorPersonCountString","生成武将总数"},
                {"TransferingMilitariesString","运输编队列表"},
                {"MilitariesString","编队列表"},
                {"ZhaoxianFailureCount","招贤失败次数"},
                {"ClosedRouteways","ClosedRouteways"},
                {"Destroyed","被击破"},
                {"SecondTierXResidue","SecondTierXResidue"},
                {"SecondTierYResidue","SecondTierYResidue"},
                {"SpyMessageCloseList","SpyMessageCloseList"},
                {"ThirdTierXResidue","ThirdTierXResidue"},
                {"ThirdTierYResidue","ThirdTierYResidue"},

            };
        }

        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Factions);
        }

        public FactionTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
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

                NewFactionWindow newWindow = new NewFactionWindow(edit, dataGrid, getScen());
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
                mainWindow.initTables(new string[] { "dgArchitecture", "dgDiplomaticRelation" });
            }
        }
    }
}
