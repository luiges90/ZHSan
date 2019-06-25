using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GameObjects;
using GameObjects.Conditions;
using GameObjects.Influences;
using GameObjects.PersonDetail;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;
using System.Data;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using OfficeOpenXml;

namespace WorldOfTheThreeKingdomsEditor
{
    /// <summary>
    /// Sets.xaml 的交互逻辑
    /// </summary>
    public partial class Sets : Window
    {

        private DataTable dt;
        private DataTable dtsce;
        public string scename;
        public string typ;
        public string path1;
        public Sets(GameScenario scen)
        {
            InitializeComponent();
          //  FieldInfo[] fields = getFieldInfos();
          //  PropertyInfo[] properties = getPropertyInfos();
        }

        public void InitialSets()
        {
            XmlDocument document = new XmlDocument();
            string allxml = System.IO.File.ReadAllText(path1);
            document.LoadXml(allxml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            dt = new DataTable();
            dt.Columns.Add("参数");
            dt.Columns.Add("中文");
            dt.Columns.Add("数值");
            for (int i = 0; i < nextSibling.Attributes.Count; i++)
            {
                DataRow dr = dt.NewRow();
                dr[0] = nextSibling.Attributes[i].Name;
                dr[1] = nextSibling.Attributes[i].Name;
                if (this.typ == "Glo")
                {
                    for (int ii = 0; ii < Glochi.Count; ii +=2)
                    {
                        if(nextSibling.Attributes[i].Name == Glochi[ii])
                        {
                            dr[1] = Glochi[ii+1];
                        }
                    }

                }
                else if (this.typ == "Para")
                {
                    for (int ii = 0; ii < Parachi.Count; ii += 2)
                    {
                        if (nextSibling.Attributes[i].Name == Parachi[ii])
                        {
                            dr[1] = Parachi[ii + 1];
                        }
                    }

                }
                dr[2] = nextSibling.Attributes[i].Value;
                dt.Rows.Add(dr);
            }
            dt.Columns[0].ReadOnly = true;
            dt.Columns[1].ReadOnly = true;
            dgSetGlo2.ItemsSource = dt.AsDataView();
            if(this.scename !=null && this.scename !="")
            {
                string str = "";
                if (this.typ == "Glo")
                {
                    str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GlobalVariables.xml";
                }
                else if (this.typ == "Para")
                {
                    str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GameParameters.xml";
                }
                if (File.Exists(str))
                {
                    InitialSceSets(str);
                }
            }
        }

        private void InitialSceSets(string str)
        {
            XmlDocument document = new XmlDocument();
            string allxml = System.IO.File.ReadAllText(str);
            // string xml = Platform.Current.LoadText("Content/Data/GlobalVariables.xml");
            document.LoadXml(allxml);
            XmlNode nextSibling = document.FirstChild.NextSibling;
            dtsce = new DataTable();
            dtsce.Columns.Add("参数");
            dtsce.Columns.Add("中文");
            dtsce.Columns.Add("数值");
            for (int i = 0; i < nextSibling.Attributes.Count; i++)
            {
                DataRow dr = dtsce.NewRow();
                dr[0] = nextSibling.Attributes[i].Name;
                dr[1] = nextSibling.Attributes[i].Name;
                if (this.typ == "Glo")
                {
                    for (int ii = 0; ii < Glochi.Count; ii += 2)
                    {
                        if (nextSibling.Attributes[i].Name == Glochi[ii])
                        {
                            dr[1] = Glochi[ii + 1];
                        }
                    }

                }
                else if (this.typ == "Para")
                {
                    for (int ii = 0; ii < Parachi.Count; ii += 2)
                    {
                        if (nextSibling.Attributes[i].Name == Parachi[ii])
                        {
                            dr[1] = Parachi[ii + 1];
                        }
                    }

                }
                dr[2] = nextSibling.Attributes[i].Value;
                dtsce.Rows.Add(dr);
            }
            dtsce.Columns[0].ReadOnly = true;
            dtsce.Columns[1].ReadOnly = true;
            dgSetGlo.ItemsSource = dtsce.AsDataView();

        }

        private Type[] supportedTypes = new Type[]
{
            typeof(bool),
            typeof(byte),
            typeof(short),
            typeof(int),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(char),
            typeof(string),
            typeof(ConditionKind),
            typeof(InfluenceKind),
            typeof(TitleKind),
            typeof(List<int>),
            typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind),
            typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind)
};

        private FieldInfo[] getFieldInfos()
        {
            return typeof(GameGlobal.GlobalVariables).GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.FieldType) || x.FieldType.IsEnum)
                .ToArray();
        }

        private PropertyInfo[] getPropertyInfos()
        {
            return typeof(GameGlobal.GlobalVariables).GetType().GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(x => Attribute.IsDefined(x, typeof(DataMemberAttribute)))
                .Where(x => supportedTypes.Contains(x.PropertyType) || x.PropertyType.IsEnum)
                .ToArray();
        }


        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            dgSetGlo.Width = 0.42 * this.Width;
            dgSetGlo2.Width = 0.42 * this.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InitialSets();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string str = "";
            if(this.typ=="Glo")
            {
                str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GlobalVariables.xml";
            }
            else if (this.typ == "Para")
            {
                str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GameParameters.xml";
            }
            if (File.Exists(str))
            {
                InitialSceSets(str);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            XmlNode docNode = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(docNode);
            XmlElement element=document.CreateElement("GlobalVariables");
            string str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GlobalVariables.xml";
            if (this.typ == "Para")
            {
                element = document.CreateElement("GameParameters");
                str = Environment.CurrentDirectory + @"\Content\Data\Scenario\" + scename + "GameParameters.xml";
            }
            for (int i = 0; i < dtsce.Rows.Count; i++)
            {
                element.SetAttribute(dtsce.Rows[i][0].ToString(), dtsce.Rows[i][2].ToString());
            }
            document.AppendChild(element);
            try
            {
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
                File.WriteAllText(str, document.OuterXml);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            XmlDocument document = new XmlDocument();
            XmlNode docNode = document.CreateXmlDeclaration("1.0", "utf-8", null);
            document.AppendChild(docNode);
            XmlElement element = document.CreateElement("GlobalVariables");
            string str = Environment.CurrentDirectory + @"\Content\Data\GlobalVariables.xml";
            if (this.typ == "Para")
            {
                element = document.CreateElement("GameParameters");
                str = Environment.CurrentDirectory + @"\Content\Data\GameParameters.xml";
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                element.SetAttribute(dt.Rows[i][0].ToString(), dt.Rows[i][2].ToString());
            }
            document.AppendChild(element);
            try
            {
                if (File.Exists(str))
                {
                    File.Delete(str);
                }
                File.WriteAllText(str, document.OuterXml);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void Sceaddset_Click(object sender, RoutedEventArgs e)
        {
            if (scename != null && scename != "")
            {
                if (dtsce == null)
                {
                    dtsce = new DataTable();
                    dtsce.Columns.Add("参数");
                    dtsce.Columns.Add("中文");
                    dtsce.Columns.Add("数值");
                }
                DataTable dt2 = dtsce;
                if (dgSetGlo2.SelectedCells.Count >= 1)
                {
                    for (int i = 0; i < dgSetGlo2.SelectedItems.Count; i++)
                    {
                        DataRow row = dt2.NewRow();
                        DataRowView dro = (DataRowView)dgSetGlo2.SelectedItems[i];
                        row[0] = dro.Row[0];
                        row[1] = dro.Row[1];
                        row[2] = dro.Row[2];
                        dt2.Rows.Add(row);
                    }
                }
                dgSetGlo.ItemsSource = dt2.AsDataView();
            }
        }

        private void Scedelset_Click(object sender, RoutedEventArgs e)
        {
            for (int i = dgSetGlo.SelectedCells.Count - 1; i > 0; i -= dgSetGlo.Columns.Count)
            {
                ((DataRowView)dgSetGlo.SelectedCells[i].Item).Row.Delete();
            }
        }

        private List<string> Glochi = new List<string>()
        {
            "MapScrollSpeed",
            "地图移动速度",
            "TroopMoveSpeed",
            "部队移动速度（越大越慢）",
            "RunWhileNotFocused",
            "游戏失去焦点时自动进行",
            "PlayMusic",
            "播放音乐",
            "PlayNormalSound",
            "播放常规音效",
            "PlayBattleSound",
            "播放战斗音效",
            "DrawMapVeil",
            "显示地图烟雾",
            "DrawTroopAnimation",
            "显示部队动画",
            "SkyEye",
            "开启天眼",
            "MultipleResource",
            "双倍资源",
            "SingleSelectionOneClick",
            "从某列表中选择单一项时单击即确定",
            "NoHintOnSmallFacility",
            "不提示小型设施的建筑完成",
            "HintPopulation",
            "提示人口迁徙",
            "HintPopulationUnder1000",
            "提示1000人以下的人口迁徙",
            "PopulationRecruitmentLimit",
            "城池人口小于兵力时禁止征兵",
            "MilitaryKindSpeedValid",
            "部队速率有效",
            "CommonPersonAvailable",
            "普通人物登场",
            "AdditionalPersonAvailable",
            "附加人物登场(8000-9000)",
            "PlayerPersonAvailable",
            "玩家人物登场(8000以上)",
            "PersonNaturalDeath",
            "武将年龄有效",
            "IdealTendencyValid",
            "出仕相性考虑有效",
            "PinPointAtPlayer",
            "电脑视玩家为最大敌人",
            "IgnoreStrategyTendency",
            "忽略君主战略倾向",
            "createChildren",
            "生成虚拟子嗣",
            "zainanfashengjilv",
            "灾难发生几率",
            "doAutoSave",
            "自动存档",
            "createChildrenIgnoreLimit",
            "虚拟子嗣能力可超越上限",
            "internalSurplusRateForPlayer",
            "收入缩减率对玩家有效",
            "internalSurplusRateForAI",
            "收入缩减率对电脑有效",
            "getChildrenRate",
            "武将怀孕几率",
            "hougongGetChildrenRate",
            "妃子怀孕几率",
            "AIExecutionRate",
            "电脑处斩几率",
            "AIExecuteBetterOfficer",
            "电脑优先处斩能力高者",
            "maxExperience",
            "最大经验值",
            "lockChildrenLoyalty",
            "子女忠诚度锁定最高",
            "AIAutoTakeNoFactionCaptives",
            "电脑必成功说服无势力俘虏",
            "AIAutoTakeNoFactionPerson",
            "电脑必成功说服城中在野武将",
            "AIAutoTakePlayerCaptives",
            "电脑必成功说服玩家势力俘虏",
            "AIAutoTakePlayerCaptiveOnlyUnfull",
            "电脑必说服势力限忠诚不满100",
            "DialogShowTime",
            "对话显示时间",
            "TechniquePointMultiple",
            "技巧点乘数",
            "PermitFactionMerge",
            "允许势力合并",
            "GameDifficulty",
            "游戏难度",
            "LeadershipOffenceRate",
            "部队队长将略对部队影响参数",
            "LiangdaoXitong",
            "粮道系统",
            "WujiangYoukenengDuli",
            "武将有可能独立",
            "FastBattleSpeed",
            "战斗速度",
            "EnableCheat",
            "允许作弊",
            "HardcoreMode",
            "硬核模式",
            "LandArmyCanGoDownWater",
            "部队可直接下水",
            "MaxAbility",
            "最大能力",
            "TirednessIncrease",
            "疲劳度增长",
            "TirednessDecrease",
            "疲劳度下降",
            "EnableAgeAbilityFactor",
            "年龄影响能力",
            "TabListDetailLevel",
            "资料显示详细度",
            "EnableExtensions",
            "EnableExtensions",
            "EncryptSave",
            "EncryptSave",
            "AutoSaveFrequency",
            "自动存档时间",
            "ShowChallengeAnimation",
            "单挑演示",
            "PersonDieInChallenge",
            "单挑致死",
            "OfficerDieInBattleRate",
            "武将战死率",
            "OfficerChildrenLimit",
            "武将子女上限",
            "StopToControlOnAttack",
            "被攻击时暂停游戏",
            "MaxMilitaryExperience",
            "部队经验上限",
            "CreateRandomOfficerChance",
            "搜索武将成功率",
            "ZhaoXianSuccessRate",
            "招贤成功率",
            "CreatedOfficerAbilityFactor",
            "生成武将能力系数",
            "EnablePersonRelations",
            "武将关系随游戏调整",
            "ChildrenAvailableAge",
            "子女登场年龄",
            "FullScreen",
            "全屏",
            "FriendlyDiplomacyThreshold",
            "同盟所需关系度",
            "SurroundFactor",
            "触发围攻时效果加成因子",
            //"ArmyPopulationCap",
            //"势力兵员总数大于人口乘参数时禁止征兵",
            "PermitQuanXiang",
            "是否允许劝降",
            "PermitManualAwardTitleAutoLearn",
            "手动称号自动习得",
            "zhaoxianOfficerMax",
            "招贤上限",
            "FactionMilitaryLimt",
            "势力部队数量限制",
            "FixedUnnaturalDeathAge",
            "修正非正常死亡年龄",
            "AIQuickBattle",
            "使用简易AI战斗算法",
            "PlayerAutoSectionHasAIResourceBonus",
            "玩家委任军团享受电脑资源加成",
            "ChildrenAbilityFactor",
            "生成子女能力系数",
            "ProhibitFactionAgainstDestroyer",
            "被击破势力后禁仕天数系数",
            "AIMergeAgainstPlayer",
            "电脑因玩家合并",
            "RemoveSpouseIfNotAvailable",
            "武将死后消除配偶关系",
            "SkyEyeSimpleNotification",
            "天眼后不播报不重要对话",
            "AutoMultipleMarriage",
            "自动结婚",
            "BornHistoricalChildren",
            "出生历史子嗣",
            "hougongAlienOnly",
            "只有异族可以后宫",
        };

        private List<string> Parachi = new List<string>()
        {
            "FindTreasureChance",
            "搜寻宝物机率",
            "LearnSkillDays",
            "学习技能时间",
            "LearnStuntDays",
            "学习特技时间",
            "LearnTitleDays",
            "学习称号时间",
            "SearchDays",
            "搜索所需天数",
            "LearnSkillSuccessRate",
            "学习技能成功概率",
            "LearnStuntSuccessRate",
            "学习特技成功概率",
            "LearnTitleSuccessRate",
            "学习称号成功概率",
            "FollowedLeaderOffenceRateIncrement",
            "追随将领加攻",
            "FollowedLeaderDefenceRateIncrement",
            "追随将领加攻",
            "InternalRate",
            "内政效率",
            "TrainingRate",
            "训练效率",
            "RecruitmentRate",
            "补充效率",
            "FundRate",
            "资金收入率",
            "FoodRate",
            "粮草收入率",
            "TroopDamageRate",
            "部队伤害率",
            "ArchitectureDamageRate",
            "建筑伤害率",
            "DefaultPopulationDevelopingRate",
            "人口默认增长",
            "BuyFoodAgriculture",
            "买粮所需农业",
            "SellFoodCommerce",
            "卖粮所需商业",
            "FundToFoodMultiple",
            "资金转换乘数",
            "FoodToFundDivisor",
            "粮草换金除数",
            "InternalFundCost",
            "内政消耗资金",
            "RecruitmentFundCost",
            "补充资金单位",
            "RecruitmentDomination",
            "补充最小统治",
            "RecruitmentMorale",
            "补充最小民心",
            "ChangeCapitalCost",
            "迁都资金单位",
            "ConvincePersonCost",
            "说服所需资金",
            "RewardPersonCost",
            "褒奖所需资金",
            "DestroyArchitectureCost",
            "破坏所需资金",
            "InstigateArchitectureCost",
            "煽动所需资金",
            "GossipArchitectureCost",
            "流言所需资金",
            "JailBreakArchitectureCost",
            "劫牢所需资金",
            "SurroundArchitectureDominationUnit",
            "围城(降)统治单位",
            "FireDamageScale",
            "火焰伤害率",
            "AIFundRate",
            "电脑资金收入率基数",
            "AIFoodRate",
            "电脑粮草收入率基数",
            "AITroopOffenceRate",
            "电脑部队攻击力基数",
            "AITroopDefenceRate",
            "电脑部队防御力基数",
            "AIArchitectureDamageRate",
            "电脑建筑伤害率基数",
            "AITrainingSpeedRate",
            "电脑训练速度基数",
            "AIRecruitmentSpeedRate",
            "电脑征兵速度基数",
            "AIFundYearIncreaseRate",
            "电脑资金收入率增量",
            "AIFoodYearIncreaseRate",
            "电脑粮草收入率增量",
            "AITroopOffenceYearIncreaseRate",
            "电脑部队攻击力增量",
            "AITroopDefenceYearIncreaseRate",
            "电脑部队防御力增量",
            "AIArchitectureDamageYearIncreaseRate",
            "电脑建筑伤害率增量",
            "AITrainingSpeedYearIncreaseRate",
            "电脑训练速度增量",
            "AIRecruitmentSpeedYearIncreaseRate",
            "电脑征兵速度增量",
            "AIArmyExperienceYearIncreaseRate",
            "电脑部队经验获得率增量",
            "AIOfficerExperienceYearIncreaseRate",
            "电脑武将经验获得率增量",
            "AIOfficerExperienceRate",
            "电脑武将经验获得率基数",
            "AIArmyExperienceRate",
            "电脑部队经验获得率基数",
            "AIBackendArmyReserveCalmBraveDifferenceMultiply",
            "电脑城池战略储备部队规模参数",
            "AIBackendArmyReserveAmbitionMultiply",
            "电脑城池战略储备部队规模野心",
            "AIBackendArmyReserveAdd",
            "电脑城池战略储备部队规模基数",
            "AIBackendArmyReserveMultiply",
            "电脑城池战略储备部队规模乘数",
            "AITradePeriod",
            "电脑买卖密度",
            "AITreasureChance",
            "电脑赐宝机率",
            "AITreasureCountMax",
            "势力君主最大宝物持有数",
            "AITreasureCountCappedTitleLevelAdd",
            "电脑赐宝加数机率",
            "AITreasureCountCappedTitleLevelMultiply",
            "电脑赐宝乘数机率",
            "AIGiveTreasureMaxWorth",
            "电脑赐宝物(最大价值)",
            "AIFacilityFundMonthWaitParam",
            "电脑建造设施储备资金下限参数",
            "AIFacilityDestroyValueRate",
            "电脑拆除设施参数",
            "AIBuildHougongUnambitionProbWeight",
            "电脑建造后宫设施野心相关参数",
            "AIBuildHougongSpaceBuiltProbWeight",
            "电脑建造后宫设施妃子空间相关参数",
            "AIBuildHougongMaxSizeAdd",
            "电脑建造后宫设施妃子空间相关参数",
            "AIBuildHougongSkipSizeChance",
            "AIBuildHougongSkipSizeChance",
            "AINafeiUncreultyProbAdd",
            "AINafeiUncreultyProbAdd",
            "AIHougongArchitectureCountProbMultiply",
            "AIHougongArchitectureCountProbMultiply",
            "AIHougongArchitectureCountProbPower",
            "AIHougongArchitectureCountProbPower",
            "AINafeiAbilityThresholdRate",
            "AINafeiAbilityThresholdRate",
            "AINafeiStealSpouseThresholdRateAdd",
            "AINafeiStealSpouseThresholdRateAdd",
            "AINafeiStealSpouseThresholdRateMultiply",
            "AINafeiStealSpouseThresholdRateMultiply",
            "AINafeiMaxAgeThresholdAdd",
            "AINafeiMaxAgeThresholdAdd",
            "AINafeiMaxAgeThresholdMultiply",
            "AINafeiMaxAgeThresholdMultiply",
            "AINafeiSkipChanceAdd",
            "AINafeiSkipChanceAdd",
            "AINafeiSkipChanceMultiply",
            "AINafeiSkipChanceMultiply",
            "AIChongxingChanceAdd",
            "AIChongxingChanceAdd",
            "AIChongxingChanceMultiply",
            "AIChongxingChanceMultiply",
            "AIRecruitPopulationCapMultiply",
            "电脑征兵判定相关人口上限乘数",
            "AIRecruitPopulationCapBackendMultiply",
            "电脑征兵判定相关人口下限乘数",
            "AIRecruitPopulationCapHostilelineMultiply",
            "电脑前线城池征兵判定相关人口下限乘数",
            "AIRecruitPopulationCapStrategyTendencyMulitply",
            "电脑征兵判定战略倾向乘数",
            "AIRecruitPopulationCapStrategyTendencyAdd",
            "电脑征兵判定战略倾向加数",
            "AINewMilitaryPopulationThresholdDivide",
            "AINewMilitaryPopulationThresholdDivide",
            "AINewMilitaryPersonThresholdDivide",
            "AINewMilitaryPersonThresholdDivide",
            "AIExecuteMaxUncreulty",
            "电脑君主处决俘虏允许义理减野心相关参数",
            "AIExecutePersonIdealToleranceMultiply",
            "电脑君主处决俘虏个人倾向乘数",
            "FireStayProb",
            "火焰持续燃烧机率",
            "FireSpreadProbMultiply",
            "火焰扩散乘数",
            "MinPregnantProb",
            "后宫最低怀孕机率",
            "InternalExperienceRate",
            "内政经验参数",
            "AbilityExperienceRate",
            "能力经验参数",
            "ArmyExperienceRate",
            "部队经验参数",
            "AIAttackChanceIfUnfull",
            "电脑不满编攻击机率",
            "AIObeyStrategyTendencyChance",
            "电脑跟从战略倾向机率",
            "AIOffendMaxDiplomaticRelationMultiply",
            "电脑进攻势力友好度下限判定参数",
            "AIOffendDefendTroopAdd",
            "电脑攻击防御部队相关加数",
            "AIOffendDefendTroopMultiply",
            "电脑攻击防御部队相关乘数",
            "AIOffendIgnoreReserveProbAmbitionMultiply",
            "AIOffendIgnoreReserveProbAmbitionMultiply",
            "AIOffendIgnoreReserveProbAmbitionAdd",
            "AIOffendIgnoreReserveProbAmbitionAdd",
            "AIOffendIgnoreReserveProbBCDiffMultiply",
            "AIOffendIgnoreReserveProbBCDiffMultiply",
            "AIOffendIgnoreReserveProbBCDiffAdd",
            "AIOffendIgnoreReserveProbBCDiffAdd",
            "AIOffendIgnoreReserveChanceTroopRatioAdd",
            "AIOffendIgnoreReserveChanceTroopRatioAdd",
            "AIOffendIgnoreReserveChanceTroopRatioMultiply",
            "AIOffendIgnoreReserveChanceTroopRatioMultiply",
            "PrincessMaintainenceCost",
            "后宫维持费",
            "AIUniqueTroopFightingForceThreshold",
            "AIUniqueTroopFightingForceThreshold",
            "MilitaryPopulationCap",
            "兵役上限(势力兵员总数大于人口乘参数时禁止征兵)",
            "MilitaryPopulationReloadQuantity",
            "兵役增量倍数",
            "CloseThreshold",
            "亲近武将初始亲密度",
            "HateThreshold",
            "憎恨武将初始亲密度",
            "VeryCloseThreshold",
            "亲爱武将初始亲密度",
            "MaxAITroopCountCandidates",
            "MaxAITroopCountCandidates",
            "PopulationDevelopingRate",
            "人口增长率倍数",
            "AIAntiStratagem",
            "电脑部队抗计率",
            "AIAntiSurround",
            "电脑部队抗围率",
            "AIAntiSurroundIncreaseRate",
            "电脑部队抗围率加成",
            "AIAntiStratagemIncreaseRate",
            "电脑部队抗计率加成",
            "CloseAbilityRate",
            "亲近武将关系能力加成",
            "VeryCloseAbilityRate",
            "亲爱武将关系能力加成",
            "AIEncirclePlayerRate",
            "电脑声讨玩家机率",
            "InternalSurplusFactor",
            "收入缩减参数，越大缩减越慢",
            "AIExtraPerson",
            "电脑额外人才",
            "AIExtraPersonIncreaseRate",
            "电脑额外人才加成",
            "ExpandConditions",
            "城池扩建条件",
            "SearchPersonArchitectureCountPower",
            "SearchPersonArchitectureCountPower",
            "AIEncircleRank",
            "AIEncircleRank",
            "AIEncircleVar",
            "AIEncircleVar",
            "SelectPrinceCost",
            "立储君所需资金",
            "TransferCostPerMilitary",
            "运输每一队编队所需资金",
            "TransferFoodPerMilitary",
            "运输每一队编队所需粮食",
            "AutoLearnSkillSuccessRate",
            "自动学习技能成功机率",
            "AutoLearnStuntSuccessRate",
            "自动学习特技成功机率",
            "RansomRate",
            "赎回武将机率",
            "DayInTurn",
            "每回回合数",
            "MaxRelation",
            "武将关系上限",
            "HougongRelationHateFactor",
            "HougongRelationHateFactor",
            "AIMaxFeizi",
            "电脑妃子上限",
            "MaxReputationForRecruit",
            "招武将声望加成最高需要声望上限",
            "TroopMoraleChange",
            "周围友军部队被击破时士气减少参数",
            "RecruitPopualationDecreaseRate",
            "征兵时人口减少参数",
            "MakeMarriageCost",
            "赐婚所需资金",
            "NafeiCost",
            "纳妃所需资金",
        };
    }
}
