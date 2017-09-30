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
        protected override GameObjectList GetDataList(GameScenario scen)
        {
            return scen.Militaries;
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
                "kindID",
                "Quantity",
                "Morale",
                "Combativity",
                "Experience",
                "InjuryQuantity",
                "FollowedLeaderID",
                "LeaderID",
                "LeaderExperience",
                "Tiredness",
                "ArrivingDays"
            };
        }

        public MilitaryTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
