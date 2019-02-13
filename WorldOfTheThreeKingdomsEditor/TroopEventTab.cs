using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TroopEventTab : BaseTab<TroopEvent>
    {
		public string TroopEventID_CHS = "编号";
		public string TroopEventIDDesc_CHS = "部队事件的编号。";
		public string TroopEventName_CHS = "名称";
		public string TroopEventNameDesc_CHS = "部队事件的名称。";
		public string TroopEventHasHappened_CHS = "已发生";
		public string TroopEventHasHappenedDesc_CHS = "勾选后，该事件会被判定为已发生。";
		public string TroopEventIsRepeatable_CHS = "可重复";
		public string TroopEventIsRepeatableDesc_CHS = "勾选后，该事件会被定义为可重复触发。";
		public string TroopEventPredecessorEventID_CHS = "前置事件";
		public string TroopEventPredecessorEventID_CHSDesc_CHS = "指定该事件被触发前，所需要的前置事件编号。";
		public string TroopEventReqChar_CHS = "发动人物";
		public string TroopEventReqCharDesc_CHS = "指定发动事件的人物，事件效果会以人物所在部队为中心。设定为-1，则允许任意部队触发事件。";
		public string TroopEventDialogString_CHS = "会话脚本";
		public string TroopEventDialogStringDesc_CHS = "人物编号后面跟随要说的话。以空格分隔各人物会话，留空则无事件会话。";
		public string TroopEventConditionString_CHS = "触发条件";
		public string TroopEventConditionStringDesc_CHS = "发动事件的部队，所需要满足的条件编号。留空则为无条件满足。";
		public string TroopEventTriggerChance_CHS = "触发几率";
		public string TroopEventTriggerChanceDesc_CHS = "设定事件被触发的机率，范围为0-100。";
		public string TroopEventCharRelationString_CHS = "人物关系";
		public string TroopEventCharRelationStringDesc_CHS = "每个关系后跟随一个人物编号。0为非友好，1为友好。留空则不在搜索范围检查此条件。";
		public string TroopEventSelfEffectString_CHS = "部队效果";
		public string TroopEventSelfEffectStringDesc_CHS = "发动事件的部队，所触发的效果列表";
		public string TroopEventCharEffectString_CHS = "影响人物";
		public string TroopEventCharEffectStringDesc_CHS = "每个人物编号后，跟随一个部队事件效果。以空格分隔各影响人物定义。";
		public string TroopEventEffectAreaString_CHS = "影响范围";
		public string TroopEventEffectAreaStringDesc_CHS = "0为视野中敌军，1为视野中友军，2为攻击范围中敌军，3为攻击范围中友军，4为周围八格中敌军，5为周围八格中友军。每个范围后跟随一个部队事件效果，以空格分隔各效果定义。";
		public string TroopEventImage_CHS = "图片";
		public string TroopEventImageDesc_CHS = "指定事件图片的档案名称，图片档案放在Content/Textures/GameComponents/tupianwenzi/Data/tupian里。";
		public string TroopEventSound_CHS = "音效";
		public string TroopEventSoundDesc_CHS = "指定事件音效的档案名称，音效档案放在Content/Textures/GameComponents/tupianwenzi/Data/yinxiao里。";
		
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.TroopEvents);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"PredecessorEventID", "-1"},
                {"Chance", "100" },
                {"LaunchPerson", "-1" }
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                { TroopEventID_CHS, TroopEventIDDesc_CHS },
                { TroopEventName_CHS, TroopEventNameDesc_CHS },
                { TroopEventHasHappened_CHS, TroopEventHasHappenedDesc_CHS },
                { TroopEventIsRepeatable_CHS, TroopEventIsRepeatableDesc_CHS },
                { TroopEventPredecessorEventID_CHS, TroopEventPredecessorEventID_CHSDesc_CHS },
                { TroopEventReqChar_CHS, TroopEventReqCharDesc_CHS },
                { TroopEventDialogString_CHS, TroopEventDialogStringDesc_CHS },
				{ TroopEventConditionString_CHS, TroopEventConditionStringDesc_CHS },
                { TroopEventTriggerChance_CHS, TroopEventTriggerChanceDesc_CHS },
                { TroopEventCharRelationString_CHS, TroopEventCharRelationStringDesc_CHS },
                { TroopEventSelfEffectString_CHS, TroopEventSelfEffectStringDesc_CHS },
				{ TroopEventCharEffectString_CHS, TroopEventCharEffectStringDesc_CHS },
                { TroopEventEffectAreaString_CHS, TroopEventEffectAreaStringDesc_CHS },
                { TroopEventImage_CHS, TroopEventImageDesc_CHS },
                { TroopEventSound_CHS, TroopEventSoundDesc_CHS },
            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "Name",
                "Happened",
                "Repeatable",
                "PredecessorEventID",
                "LaunchPersonString",
                "EventDialogString",
                "ConditionsString",
                "HappenChance",
                "CharRelationsString",
                "SelfEffectsString",
                "CharEffectsString",
                "EffectAreasString",
                "Image",
                "Sound"
            };
        }

        public TroopEventTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
