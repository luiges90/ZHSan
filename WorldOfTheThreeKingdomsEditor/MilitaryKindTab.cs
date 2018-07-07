using GameObjects;
using GameObjects.ArchitectureDetail;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.PersonDetail;
using GameObjects.TroopDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class MilitaryKindTab : BaseTab<MilitaryKind>
    {
        protected override GameObjectList GetDataList(GameScenario scen)
        {
            return scen.GameCommonData.AllMilitaryKinds.GetMilitaryKindList();
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"TitleInfluence", "-1" },
                {"MorphTo", "-1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Type",
                "Name",
                "Description",
                "Merit",
                "Successor",
                "Speed",
                "ObtainProb",
                "TitleInfluence",
                "CreateCost",
                "CreateTechnology",
                "CreateBesideWater",
                "Offence",
                "Defence",
                "OffenceRadius",
                "CounterOffence",
                "BeCountered",
                "ObliqueOffence",
                "ArrowOffence",
                "AirOffence",
                "ContactOffence",
                "ArchitectureDamageRate",
                "ArchitectureCounterDamageRate",
                "StratagemRadius",
                "ObliqueStratagem",
                "ViewRadius",
                "ObliqueView",
                "InjuryRate",
                "Movability",
                "OneAdaptabilityKind",
                "PlainAdaptability",
                "GrasslandAdaptability",
                "ForrestAdaptability",
                "MarshAdaptability",
                "MountainAdaptability",
                "WaterAdaptability",
                "RidgeAdaptability",
                "WastelandAdaptability",
                "DesertAdaptability",
                "CliffAdaptability",
                "PlainRate",
                "GrasslandRate",
                "ForrestRate",
                "MarshRate",
                "MountainRate",
                "WaterRate",
                "RidgeRate",
                "WastelandRate",
                "DesertRate",
                "CliffRate",
                "FireDamageRate",
                "RecruitLimit",
                "FoodPerSoldier",
                "RationDays",
                "PointsPerSoldier",
                "MinScale",
                "OffencePerScale",
                "DefencePerScale",
                "MaxScale",
                "CanLevelUp",
                "LevelUpKindID",
                "LevelUpExperience",
                "OffencePer100Experience",
                "DefencePer100Experience",
                "Influences",
                "MinCommand",
                "MorphTo"

            };
        }

        public MilitaryKindTab(GameScenario scen, DataGrid dg)
        {
            init(scen, dg);
        }
    }
}
