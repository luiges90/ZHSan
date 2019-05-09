using GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WorldOfTheThreeKingdomsEditor
{
    class TroopTab : BaseTab<Troop>
    {
        protected override IItemList GetDataList(GameScenario scen)
        {
            return new GameObjectItemList(scen.Troops);
        }

        protected override Dictionary<string, string> GetDefaultValues()
        {
            return new Dictionary<string, string>()
            {
                {"StartingArchitecture", "-1"},
                {"MilitaryID", "-1" },
                {"TargetTroopID", "-1"},
                {"TargetArchitectureID", "-1" },
                {"WillTroopID", "-1" },
                {"WillArchitectureID", "-1" },
                {"CurrentCombatMethodID", "-1" },
                {"CurrentStratagemID", "-1" },
                {"CurrentStunt", "-1" },
                {"ForceTroopTarget", "-1" }
            };
        }

        protected override Dictionary<String, String> GetHelpText()
        {
            return new Dictionary<string, string>()
            {
                {"LeaderIDString","队长ID"},
                {"Controllable","可控"},
                {"Status","状态"},
                {"Direction","方向"},
                {"Auto","委任"},
                {"Food","粮草"},
                {"StartingArchitectureString","出发地"},
                {"PersonsString","部队武将"},
                {"Position","位置"},
                {"Destination","目标位置"},
                {"RealDestination","真实目标位置"},
                {"FirstTierPath","第一层路径"},
                {"SecondTierPath","第二层路径"},
                {"ThirdTierPath","第三层路径"},
                {"FirstIndex",""},
                {"SecondIndex",""},
                {"ThirdIndex",""},
                {"MilitaryID","编队ID"},
                {"CastDefaultKind","施展默认种类"},
                {"CastTargetKind","施展目标种类"},
                {"AttackDefaultKind","攻击默认种类"},
                {"AttackTargetKind","攻击目标种类"},
                {"TargetTroopID","目标部队"},
                {"TargetArchitectureID","目标建筑"},
                {"WillTroopID","意愿部队"},
                {"WillArchitectureID","意愿建筑"},
                {"CurrentCombatMethodID","当前战法"},
                {"CurrentStratagemID","当前计略"},
                {"SelfCastPosition","自施展位置"},
                {"ChaosDayLeft","混乱剩余天数"},
                {"CutRoutewayDays","切断粮道剩余天数"},
                {"CaptivesString","俘虏列表"},
                {"RecentlyFighting","近期战斗过"},
                {"TechnologyIncrement","出发点技术攻防加成"},
                {"EventInfluencesString","事件影响"},
                {"CurrentStuntIDString","当前特技"},
                {"StuntDayLeft","特技剩余天数"},
                {"zijin","资金"},
                {"mingling","命令"},
                {"ManualControl","手动控制"},
                {"ForceTroopTargetId","强制目标部队"},
                {"QuickBattling","快速战斗"},
                {"AllowedStrategems","AllowedStrategems"},
                {"captureChance","captureChance"},
                {"chongshemubiaoweizhibiaoji","chongshemubiaoweizhibiaoji"},
                {"CurrentTileAnimationKind","CurrentTileAnimationKind"},
                {"Destroyed","Destroyed"},
                {"DrawAnimation","是否显示动画"},
                {"Effect","Effect"},
                {"FriendlyAction","FriendlyAction"},
                {"HasPath","HasPath"},
                {"HasToDoCombatAction","HasToDoCombatAction"},
                {"HostileAction","HostileAction"},
                {"minglingweizhi","minglingweizhi"},
                {"Morale","士气"},
                {"MoveAnimationFrames","MoveAnimationFrames"},
                {"Moved","Moved"},
                {"OperationDone","操作完成"},
                {"OrientationPosition","OrientationPosition"},
                {"OutburstDefenceMultiple","OutburstDefenceMultiple"},
                {"OutburstNeverBeIntoChaos","OutburstNeverBeIntoChaos"},
                {"OutburstOffenceMultiple","OutburstOffenceMultiple"},
                {"OutburstPreventCriticalStrike","OutburstPreventCriticalStrike"},
                {"PreAction","PreAction"},
                {"PreviousPosition","PreviousPosition"},
                {"Quantity","士兵数"},
                {"QueueEnded","QueueEnded"},
                {"ShowPath","ShowPath"},
                {"Simulating","Simulating"},
                {"SimulatingCombatMethod","SimulatingCombatMethod"},
                {"StepNotFinished","StepNotFinished"},
                {"Stunts","Stunts"},
                {"TroopStatus","TroopStatus"},
                {"WaitForDeepChaosFrameCount","WaitForDeepChaosFrameCount"},

            };
        }

        protected override string[] GetRawItemOrder()
        {
            return new String[]
            {
                "ID",
                "LeaderIDString",
                "Controllable",
                "Status",
                "Direction",
                "Auto",
                "Food",
                "StartingArchitectureString",
                "PersonsString",
                "Position",
                "Destination",
                "RealDestination",
                "FirstTierPath",
                "SecondTierPath",
                "ThirdTierPath",
                "FirstIndex",
                "SecondIndex",
                "ThirdIndex",
                "MilitaryID",
                "CastDefaultKind",
                "CastTargetKind",
                "AttackDefaultKind",
                "AttackTargetKind",
                "TargetTroopID",
                "TargetArchitectureID",
                "WillTroopID",
                "WillArchitectureID",
                "CurrentCombatMethodID",
                "CurrentStratagemID",
                "SelfCastPosition",
                "ChaosDayLeft",
                "CutRoutewayDays",
                "CaptivesString",
                "RecentlyFighting",
                "TechnologyIncrement",
                "EventInfluencesString",
                "CurrentStuntIDString",
                "StuntDayLeft",
                "zijin",
                "mingling",
                "ManualControl",
                "ForceTroopTargetId",
                "QuickBattling",
                "AllowedStrategems",
                "captureChance",
                "chongshemubiaoweizhibiaoji",
                "CurrentTileAnimationKind",
                "Destroyed",
                "DrawAnimation",
                "Effect",
                "FriendlyAction",
                "HasPath",
                "HasToDoCombatAction",
                "HostileAction",
                "minglingweizhi",
                "Morale",
                "MoveAnimationFrames",
                "Moved",
                "OperationDone",
                "OrientationPosition",
                "OutburstDefenceMultiple",
                "OutburstNeverBeIntoChaos",
                "OutburstOffenceMultiple",
                "OutburstPreventCriticalStrike",
                "PreAction",
                "PreviousPosition",
                "Quantity",
                "QueueEnded",
                "ShowPath",
                "Simulating",
                "SimulatingCombatMethod",
                "StepNotFinished",
                "Stunts",
                "TroopStatus",
                "WaitForDeepChaosFrameCount",
            };
        }

        public TroopTab(GameScenario scen, DataGrid dg, TextBlock helpTextBlock)
        {
            init(scen, dg, helpTextBlock);
        }
    }
}
