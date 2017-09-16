using GameObjects;
using GameObjects.PersonDetail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    public class PersonTab : BaseTab<Person>
    {
        protected override String[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Available",
                "Alive",
                "SurName",
                "GivenName",
                "CalledName",
                "Sex",
                "PictureIndex",
                "Ideal",
                "IdealTendencyIDString",
                "LeaderPossibility",
                "PCharacter",
                "YearAvailable",
                "YearBorn",
                "YearDead",
                "YearJoin",
                "DeadReason",
                "BaseStrength",
                "BaseCommand",
                "BaseIntelligence",
                "BasePolitics",
                "BaseGlamour",
                "Reputation",
                "Braveness",
                "Calmness",
                "SkillsString",
                "RealTitlesString",
                "StudyingTitleString",
                "StuntsString",
                "StudyingStuntString",
                "UniqueTitlesString",
                "UniqueMilitaryKindsString",
                "Generation",
                "Strain",
                "huaiyun",
                "faxianhuaiyun",
                "huaiyuntianshu",
                "shoshurenwu",
                "suoshurenwuList",
                "MarriageGranter",
                "TempLoyaltyChange",
                "BornRegion",
                "AvailableLocation",
                "PersonalLoyalty",
                "Ambition",
                "Qualification",
                "ValuationOnGovernment",
                "StrategyTendency",
                "OldFactionID",
                "ProhibitedFactionID",
                "IsGeneratedChildren",
                "CommandPotential",
                "StrengthPotential",
                "IntelligencePotential",
                "GlamourPotential",
                "TrainPolicy"
            };
        }

        protected override Dictionary<String, String> GetDefaultValues()
        {
            return new Dictionary<string, string>
            {
                { "StudyingTitleString", "-1" },
                { "StudyingStuntString", "-1" },
                { "huaiyuntianshu", "-1" },
                { "suoshurenwu", "-1" },
                { "ConvincingPersonID", "-1" },
                { "waitForFeiZiPeriod", "30" },
                { "waitForFeiziId", "-1" },
                { "InjureRate", "1" },
                { "Generation", "1" },
                { "InformationKindID", "-1" }
            };
        }

        public PersonTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }

        

    }
}
