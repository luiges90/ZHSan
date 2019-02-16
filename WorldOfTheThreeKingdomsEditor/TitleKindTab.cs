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
    class TitleKindTab : BaseTab<TitleKind>
    {
		public string TitleTypeID_CHS = "编号";
		public string TitleTypeIDDesc_CHS = "称号类型的编号。";
		public string TitleTypeName_CHS = "名称";
		public string TitleTypeNameDesc_CHS = "称号类型的名称。";
		public string TitleTypeCombat_CHS = "战斗用";
		public string TitleTypeCombatDesc_CHS = "勾选后，称号会被认定为战斗类称号。";
		public string TitleTypeRandomTeachable_CHS = "随机教授";
		public string TitleTypeRandomTeachableDesc_CHS = "勾选后，称号可被随机教授给其他人物。";
		public string TitleTypeRecallable_CHS = "可回收";
		public string TitleTypeRecallableDesc_CHS = "勾选后，称号可被回收。";
		public string TitleTypeStudyDay_CHS = "研修时间";
		public string TitleTypeStudyDayDesc_CHS = "设定修得称号所需要的时间。";
		public string TitleTypeSuccessRate_CHS = "成功率";
		public string TitleTypeSuccessRateDesc_CHS = "设定修得称号的机率。";
		
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.GameCommonData.AllTitleKinds.TitleKinds);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { TitleTypeID_CHS, TitleTypeIDDesc_CHS },
                { TitleTypeName_CHS, TitleTypeNameDesc_CHS },
                { TitleTypeCombat_CHS, TitleTypeCombatDesc_CHS },
                { TitleTypeRandomTeachable_CHS, TitleTypeRandomTeachableDesc_CHS },
                { TitleTypeRecallable_CHS, TitleTypeRecallableDesc_CHS },
                { TitleTypeStudyDay_CHS, TitleTypeStudyDayDesc_CHS },
                { TitleTypeSuccessRate_CHS, TitleTypeSuccessRateDesc_CHS },
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "Combat",
                "RandomTeachable",
                "Recallable",
                "StudyDay",
                "SuccessRate",
            };
        }

        public TitleKindTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
