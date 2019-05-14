using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class MilitaryTab : BaseTab<Military>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Militaries);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"Morale", "100"},
                {"Combativity", "100" },
                {"Quantity", "1000"},
                {"FollowedLeaderID", "-1"},
                {"LeaderID", "-1" },
                {"TrainingPersonID", "-1" },
                {"RecruitmentPersonID", "-1" },
                {"ShelledMilitary", "-1" },
                {"StartingArchitectureID", "-1" },
                {"TargetArchitectureID", "-1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "KindID",
                "Quantity",
                "Morale",
                "Combativity",
                "Experience",
                "InjuryQuantity",
                "FollowedLeaderID",
                "LeaderID",
                "LeaderExperience",
                "Tiredness",
                "ArrivingDays",
                "StartingArchitectureID",
                "TargetArchitectureID"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Name", "队名" },
                { "kindID", "种类ID" },
                { "Quantity", "人数" },
                { "Morale", "士气" },
                { "Combativity", "战意" },
                { "Experience", "经验" },
                { "InjuryQuantity", "伤兵" },
                { "FollowedLeaderID", "追随将领ID" },
                { "LeaderID", "队长ID" },
                { "leaderExperience", "队长经验" },
                { "Tiredness", "疲累度" },
                { "ArrivingDays", "到达时间" },
                { "belongedArchitectureID", "所属建筑" },
                { "StartingArchitectureID", "出发建筑" },
                { "TargetArchitectureID", "目标建筑" },
                { "ShelledMilitaryID", "被包裹编队" },
                { "RecruitmentPersonID", "补充人员ID" },
                { "TroopDamageDealt", "总士兵伤害" },
                { "RoutCount", "击破数" },
                { "YearCreated", "编成年" },
                { "TroopBeDamageDealt", "总受士兵伤害" },
                { "ArchitectureDamageDealt", "总建筑伤害" },
                { "StratagemSuccessCount", "计略成功次数" },
                { "StratagemFailCount", "计略失败次数" },
                { "StratagemBeSuccessCount", "中计次数" },
                { "StratagemBeFailCount", "计略阻挡次数" },
                { "OfficerKillCount", "致武将战死数" },
                { "CaptiveCount", "俘获将领次数" },
            };
        }

        public MilitaryTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
