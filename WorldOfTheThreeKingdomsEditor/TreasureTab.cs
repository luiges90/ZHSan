using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TreasureTab : BaseTab<Treasure>
    {
		public string TreasureID_CHS = "编号";
		public string TreasureIDDesc_CHS = "宝物编号";
		public string TreasureName_CHS = "名称";
		public string TreasureNameDesc_CHS = "宝物名称";
		public string TreasureImage_CHS = "图片";
		public string TreasureImageDesc_CHS = "设定宝物使用的图片编号。";
		public string TreasureValue_CHS = "价值";
		public string TreasureValueDesc_CHS = "宝物价值";
		public string TreasureIsAvailable_CHS = "登场";
		public string TreasureIsAvailableDesc_CHS = "勾选后，宝物会在剧本起始时登场。";
		public string TreasureHiddenPlaceIDString_CHS = "隐藏地点";
		public string TreasureHiddenPlaceIDStringDesc_CHS = "指定宝物可被探索发现的地点。";
		public string TreasureGroup_CHS = "种类";
		public string TreasureGroupDesc_CHS = "相同种类的宝物，效果无法叠加。";
		public string TreasureAppearYear_CHS = "发现年份";
		public string TreasureAppearYearDesc_CHS = "设定宝物可被探索发现的年份。";
		public string TreasureOwnershipIDString_CHS = "拥有者";
		public string TreasureOwnershipIDStringDesc_CHS = "指定拥有该宝物的角色编号。";
		public string TreasureEffectsIDString_CHS = "效果";
		public string TreasureEffectsIDStringDesc_CHS = "设定宝物所附带的加成，以空格分隔各项加成。";
		public string TreasureDescription_CHS = "说明";
		public string TreasureDescriptionDesc_CHS = "宝物的介绍与说明。";
		
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Treasures);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"HiddenPlace", "-1"},
                {"Ownership", "-1" }
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "TreasureImage",
                "Worth",
                "Available",
                "HiddenPlaceIDString",
                "TreasureGroup",
                "AppearYear",
                "OwnershipIDString",
                "EffectsString",
                "Description"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { TreasureID_CHS, TreasureIDDesc_CHS },
                { TreasureName_CHS, TreasureNameDesc_CHS },
                { TreasureImage_CHS, TreasureImageDesc_CHS },
                { TreasureValue_CHS, TreasureValueDesc_CHS },
                { TreasureIsAvailable_CHS, TreasureIsAvailableDesc_CHS },
                { TreasureHiddenPlaceIDString_CHS, TreasureHiddenPlaceIDStringDesc_CHS },
                { TreasureGroup_CHS, TreasureGroupDesc_CHS },
				{ TreasureAppearYear_CHS, TreasureAppearYearDesc_CHS },
                { TreasureOwnershipIDString_CHS, TreasureOwnershipIDStringDesc_CHS },
                { TreasureEffectsIDString_CHS, TreasureEffectsIDStringDesc_CHS },
                { TreasureDescription_CHS, TreasureDescriptionDesc_CHS },
            };
        }

        public TreasureTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
