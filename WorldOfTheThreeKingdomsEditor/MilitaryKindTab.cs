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
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllMilitaryKinds.MilitaryKinds);
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
                "SuccessorString",
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
                "InjuryChance",
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
                "InfluencesString",
                "MinCommand",
                "MorphTo",
                "CreateConditionsString"

            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Type", "类别" },
                { "Name", "名称" },
                { "Description", "描述" },
                { "Merit", "强度（AI）" },
                { "SuccessorString", "较强兵种ID：如果AI准备征召这个兵种的话，会考虑征召在这列表中的兵种，而这列表的兵种是绝对强于这个兵种" },
                { "Speed", "行动速率：行动速率高的部队将优先行动。行动速率＝兵种本身的行动速率×士气÷士气上限" },
                { "ObtainProb", "获得机率：每天有1除以此数的机率，拥有武将的势力可获得这个兵种" },/////注意，这里  1/此数   这样的表达，在转换后会造成编辑器bug
                { "TitleInfluence", "出兵称号影响" },
                { "CreateCost", "新建资金" },
                { "CreateTechnology", "新建所需技术" },
                { "CreateBesideWater", "水边新建" },
                { "Offence", "攻击" },
                { "Defence", "防御" },
                { "OffenceRadius", "攻击半径" },
                { "CounterOffence", "能否反击" },
                { "BeCountered", "能否被反击" },
                { "ObliqueOffence", "斜向攻击" },
                { "ArrowOffence", "箭矢攻击：弓箭攻击，投石车等部队不属于弓箭攻击" },
                { "AirOffence", "凌空攻击：是否可以攻击建筑内的部队" },
                { "ContactOffence", "近身攻击" },
                { "ArchitectureDamageRate", "建筑伤害系数" },
                { "ArchitectureCounterDamageRate", "建筑反击承受率" },
                { "StratagemRadius", "计略范围" },
                { "ObliqueStratagem", "斜向计略" },
                { "ViewRadius", "视野半径" },
                { "ObliqueView", "斜向视野" },
                { "InjuryChance", "伤兵概率" },
                { "Movability", "行动力" },
                { "OneAdaptabilityKind", "单一适性种类" },
                { "PlainAdaptability", "平原适性" },
                { "GrasslandAdaptability", "草地适性" },
                { "ForrestAdaptability", "森林适性" },
                { "MarshAdaptability", "湿地适性" },
                { "MountainAdaptability", "山地适性" },
                { "WaterAdaptability", "水域适性" },
                { "RidgeAdaptability", "峻岭适性" },
                { "WastelandAdaptability", "荒地适性" },
                { "DesertAdaptability", "沙漠适性" },
                { "CliffAdaptability", "棧道适性" },
                { "PlainRate", "平原乘数" },
                { "GrasslandRate", "草地乘数" },
                { "ForrestRate", "森林乘数" },
                { "MarshRate", "湿地乘数" },
                { "MountainRate", "山地乘数" },
                { "WaterRate", "水域乘数" },
                { "RidgeRate", "峻岭乘数" },
                { "WastelandRate", "荒地乘数" },
                { "DesertRate", "沙漠乘数" },
                { "CliffRate", "棧道乘数" },
                { "FireDamageRate", "受火伤率" },
                { "RecruitLimit", "势力编队上限" },
                { "FoodPerSoldier", "每个士兵每天消耗的粮草数" },
                { "RationDays", "口粮天数" },
                { "PointsPerSoldier", "每补充1人所需的技巧点数" },
                { "MinScale", "成军最小规模" },
                { "OffencePerScale", "一个单位规模所增加的攻击力" },
                { "DefencePerScale", "一个单位规模所增加的防御力" },
                { "MaxScale", "最大规模" },
                { "CanLevelUp", "能否升级" },
                { "LevelUpKindID", "升级成的兵种ID" },
                { "LevelUpExperience", "升级经验" },
                { "OffencePer100Experience", "每一百经验增加的攻击力" },
                { "DefencePer100Experience", "每一百经验增加的防御力" },
                { "InfluencesString", "影响列表" },
                { "MinCommand", "最低统率(AI)" },
                { "MorphTo", "变换至兵种" },
                { "CreateConditionsString", "新编条件：编队所在建筑条件" },
                { "zijinshangxian", "资金上限" },
                { "AttackDefaultKind", "攻击默认类型" },
                { "AttackTargetKind", "攻击目标类型" },
                { "CastDefaultKind", "施展默认类型" },
                { "CastTargetKind", "施展目标类型" },
                { "IsShell", "是否外壳" },
                { "OffenceOnlyBeforeMove", "只能在移动前攻击" },
                { "MorphToKindId", "变换至兵种" },
            };
        }

        public MilitaryKindTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
