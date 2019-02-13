using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameObjects;
using GameObjects.PersonDetail;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class BiographyTab : BaseTab<GameObjects.PersonDetail.Biography>
    {
		public string CharID_CHS = "编号";
		public string CharIDDesc_CHS = "角色编号";
		public string CharFactionColor_CHS = "势力颜色";
		public string CharFactionColorDesc_CHS = "设定角色自立后，所创建势力的代表颜色。";
		public string CharAllowedTroopTypesString_CHS = "势力兵种";
		public string CharAllowedTroopTypesStringDesc_CHS = "设定角色自立后，所创建势力的初始兵种。";
		public string CharBriefIntro_CHS = "简介";
		public string CharBriefIntroDesc_CHS = "角色简介";
		public string CharHistoricalBio_CHS = "历史";
		public string CharHistoricalBioDesc_CHS = "角色历史列传";
		public string CharMythicalBio_CHS = "演义";
		public string CharMythicalBioDesc_CHS = "角色演义列传";
		public string CharInGameRecords_CHS = "经历";
		public string CharInGameRecordsDesc_CHS = "角色在剧本中的重大记事。";
		
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectDictionaryItemList(scen.AllBiographies.Biographys);
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
                "FactionColor",
                "AllowedTroopTypesString",
                "BriefIntro",
                "HistoricalIntro",
                "RomancingIntro",
                "InGame"
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { CharID_CHS, CharIDDesc_CHS },
                { CharFactionColor_CHS, CharFactionColorDesc_CHS },
                { CharAllowedTroopTypesString_CHS, CharAllowedTroopTypesStringDesc_CHS },
                { CharBriefIntro_CHS, CharBriefIntroDesc_CHS },
                { CharHistoricalBio_CHS, CharHistoricalBioDesc_CHS },
                { CharMythicalBio_CHS, CharMythicalBioDesc_CHS },
                { CharInGameRecords_CHS, CharInGameRecordsDesc_CHS },
            };
        }

        public BiographyTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
