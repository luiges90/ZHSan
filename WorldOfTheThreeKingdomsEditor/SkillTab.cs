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
                "ConditionsString",
                "Prerequisite",
                "Description",
                "GenerateConditionsString",
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

        public SkillTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
