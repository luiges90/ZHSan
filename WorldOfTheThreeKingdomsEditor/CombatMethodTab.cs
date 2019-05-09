using GameObjects;
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
    class CombatMethodTab : BaseTab<CombatMethod>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllCombatMethods.CombatMethods);
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
                "Name",
                "Description",
                "Combativity",
                "InfluencesString",
                "ArchitectureTarget",
                "CastConditionsString",
                "ViewingHostile",
                "AnimationKind"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { "Name","名称" },
                { "Description","描述" },
                { "Combativity","所需战意" },
                { "InfluencesString","影响列表" },
                { "ArchitectureTarget","目标可能为建筑" },
                { "CastConditionsString","使用条件" },
                { "ViewingHostile","视野内敌军越多越有可能使用" },
                { "AnimationKind","动画" },
                { "AttackDefaultString","攻击默认类型" },
                { "AttackTargetString","攻击目标类型" },
            };
        }

        public CombatMethodTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
