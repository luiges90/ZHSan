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
               {"Name","势力名"},
               {"LeaderID","君主ID"},
               {"PrinceID","储君ID"},
               {"ColorIndex","颜色编号"},
               {"FName","势力名"},
               {"CapitalID","都城ID"},
               {"Reputation","声望"},
               {"ArchitecturesString","建筑列表"},
               {"BaseMilitaryKindsString","基本兵种列表"},
               {"TechniquePoint","技巧点数"},
               {"TechniquePointForTechnique","为升级技巧所保留的技巧点数"},
               {"TechniquePointForFacility","为建造设施所保留的技巧点数"},
               {"SectionsString","军区列表"},
               {"InformationsString","情报列表"},
               {"TroopsString","部队列表"},
               {"RoutewaysString","粮道列表"},
               {"chaotinggongxiandu","朝廷贡献度"},
               {"guanjue","官爵"},
               {"UpgradingTechnique","正在升级中的技巧"},
               {"UpgradingDaysLeft","正在升级中的技巧剩余时间"},
               {"AvailableTechniquesString","已有技巧"},
               {"PreferredTechniqueKinds","偏好技巧类别"},
               {"IsAlien","异族"},
               {"NotPlayerSelectable","玩家不可选" }
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
    }
}
