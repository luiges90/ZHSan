using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using GameObjects;
using System.Runtime.InteropServices;
using GameObjects.Animations;
using GameObjects.ArchitectureDetail;
using GameObjects.Conditions;
using GameObjects.FactionDetail;
using GameObjects.Influences;
using GameObjects.MapDetail;
using GameObjects.PersonDetail;
using GameObjects.SectionDetail;
using GameObjects.TroopDetail;
using GameGlobal;
using Tools;


namespace json_and_excel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private string[] loads = new string[] { };
        private string load = "";
        private string 已导入文件类型;
        private string 将导出文件类型;
        private void 导入mdb文件(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "旧档案 (*.mdb)|*.mdb";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() ==System.Windows.Forms. DialogResult.OK)
            {
                已导入文件类型 = "mdb";
                已导入文件标题.Text = "已导入文件:" + openFileDialog.FileNames.Count() + "个," + "类型" + 已导入文件类型;
                this.导入文件的处理(openFileDialog);
                导出为excel文件(sender, e);
                button4.Enabled = false;
                button5.Enabled = true;
                button6.Enabled = false;
                是剧本.Enabled = false;
                保存commdata信息到存档剧本.Enabled = false;
                保存配置信息到存档剧本.Enabled = false;
                单独保存commdata信息.Enabled = false;
            }
        }

        private void 导入文件的处理(OpenFileDialog openFileDialog)
        {
            string str = "";
            已导入文件.Items.Clear();
            已导入文件.Enabled = true;
            for (int i = 0; i < openFileDialog.FileNames.Length; i++)//根据数组长度定义循环次数
            {
                str += openFileDialog.FileNames.GetValue(i).ToString() + "\n";//获取文件文件名
                已导入文件.Items.Add(openFileDialog.FileNames.GetValue(i).ToString());
            }
            load = str;
            str = "已载入文件\n" + str;
            MessageBox.Show(str);
            button7.Enabled = true;
        }

        private void 清除所有导入文件(object sender, EventArgs e)
        {
            已导入文件类型 = "";
            已导入文件标题.Text = "已导入文件";
            已导入文件.Items.Clear();
            button4.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            button7.Enabled = false;
            button8.Enabled = false;
            将导出文件标题.Text = "将导出文件";
            将导出文件类型 = "";
            将导出文件.Items.Clear();
            是剧本.Enabled = false;
            保存commdata信息到存档剧本.Enabled = false;
            保存配置信息到存档剧本.Enabled = false;
            单独保存commdata信息.Enabled = false;
        }

        private void 导入excel文件(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "excel文件 (*.xlsx)|*.xlsx|excel文件 (*.xls)|*.xls";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                已导入文件类型 = "excel";
                已导入文件标题.Text = "已导入文件:" + openFileDialog.FileNames.Count() + "个," + "类型" + 已导入文件类型;
                this.导入文件的处理(openFileDialog);
                导出为json文件(sender, e);
                button4.Enabled = false;
                button5.Enabled = false;
                button6.Enabled = true;
                是剧本.Enabled = true;
                保存commdata信息到存档剧本.Enabled = true;
                保存配置信息到存档剧本.Enabled = true;
                单独保存commdata信息.Enabled = true;
            }
        }

        private void 导入json文件(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "json文件 (*.json)|*.json";
            openFileDialog.Multiselect = true;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                已导入文件类型 = "json";
                已导入文件标题.Text = "已导入文件:" + openFileDialog.FileNames.Count() + "个," + "类型" + 已导入文件类型;
                this.导入文件的处理(openFileDialog);
                导出为excel文件(sender,e);
                button4.Enabled = false;
                button5.Enabled = true;
                button6.Enabled = false;
                是剧本.Enabled = false;
                保存commdata信息到存档剧本.Enabled = false;
                保存配置信息到存档剧本.Enabled = false;
                单独保存commdata信息.Enabled = false;
            }
        }

        private void 导出为excel文件(object sender, EventArgs e)
        {
            将导出文件类型 = "xls";
            将导出文件标题.Text = "将导出文件 至 此文件夹";
            将导出文件.Items.Clear();
            将导出文件.Items.Add("");
            将导出文件.Items.Add(Application.StartupPath + @"\转换生成文件");
            将导出文件.Items.Add("");
            将导出文件.Items.Add("同名xls文件");
            button8.Enabled = true;
        }

        private void 导出为json文件(object sender, EventArgs e)
        {
            将导出文件类型 = "json";
            将导出文件标题.Text = "将导出文件 至 此文件夹";
            将导出文件.Items.Clear();
            将导出文件.Items.Add("");
            将导出文件.Items.Add(Application.StartupPath + @"\转换生成文件");
            将导出文件.Items.Add("");
            将导出文件.Items.Add("同名json文件");
            button8.Enabled = true;
        }

        private GameScenario scenario = null;
        private void 确认导出(object sender, EventArgs e)
        {
            string currentPath = Directory.GetCurrentDirectory();
            OleDbConnection mdb文件 = new OleDbConnection();
            List<string> 表名 = new List<string>();

            定时信息框("执行导出");
            if (!Directory.Exists(@Application.StartupPath + @"\转换生成文件"))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(@Application.StartupPath + @"\转换生成文件");
            }
            for (int i = 0; i <= 已导入文件.Items.Count - 1; i++)
            {
                if (File.Exists(Application.StartupPath + "\\转换生成文件\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "." + 将导出文件类型))
                {
                    File.Delete(Application.StartupPath + "\\转换生成文件\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "." + 将导出文件类型);
                }
                if (已导入文件类型 == "excel" && 将导出文件类型 == "json")
                {
                    if (!是剧本.Checked)
                    {
                        if (存档取值字段().Contains(Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString())))
                        {
                            exceltojson(i);
                        }
                        else
                        {
                            MessageBox.Show("警告，文件名有误，存档的文件名只能是由Save00-Save09的excel文件加上Save00-Save09地形信息的txt文件转换而来");
                        }
                    }
                    else
                    {
                        exceltojson(i);
                    }
                }
                else if (已导入文件类型 == "json" && 将导出文件类型 == "xls")
                {
                    if (File.Exists(Application.StartupPath + "\\转换生成文件\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "." + "xlsx"))
                    {
                        File.Delete(Application.StartupPath + "\\转换生成文件\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "." + "xlsx");
                    }
                    CommonData.Current = Tools.SimpleSerializer.DeserializeJsonFile<CommonData>(@"Content\Data\Common\CommonData.json", false, false);
                    GameScenario.ProcessCommonData(CommonData.Current);
                    scenario = WorldOfTheThreeKingdoms.GameScreens.MainGameScreen.LoadScenarioData(已导入文件.Items[i].ToString(), true, null, true);

                    jsontoexcel(i);
                }
                else if (已导入文件类型 == "mdb" && 将导出文件类型 == "xls")
                {
                    mdbtoexcel(表名,mdb文件, i);
 
                    /* //以下自用
                     string 城池 = "select ID as ID , MayorID as 县令ID , CaptionID as 名称编号 , Name as 名称 , Kind as 种类 , IsStrategicCenter as 战略要冲 , StateID as 州域ID , Characteristics as 特色 , Area as 区域 , Persons as 所属人物 , MovingPersons as 移动中人物 , NoFactionPersons as 在野人物 , NoFactionMovingPersons as 移动中在野人物 , Population as 人口 , Fund as 资金 , Food as 粮草 , Agriculture as 农业 , Commerce as 商业 , Technology as 技术 , Domination as 统治 , Morale as 民心 , Endurance as 耐久 , AutoHiring as 委任录用 , AutoRewarding as 委任褒奖 , AutoWorking as 委任工作 , AutoSearching as 委任搜索 , AutoRecruiting as 委任补充 , HireFinished as 录用已完成 , FacilityEnabled as 设施有效 , AgricultureWorkingPersons as 农业人物列表 , CommerceWorkingPersons as 商业人物列表 , TechnologyWorkingPersons as 技术人物列表 , DominationWorkingPersons as 统治人物列表 , MoraleWorkingPersons as 民心人物列表 , EnduranceWorkingPersons as 耐久人物列表 , zhenzaiWorkingPersons as 赈灾人物列表 , TrainingWorkingPersons as 训练人物列表 , Militaries as 拥有的编队 , Facilities as 设施列表 , BuildingFacility as 建设中设施 , BuildingDaysLeft as 建设剩余天数 , PlanFacilityKind as 计划设施 , FundPacks as 资金包 , FoodPacks as 粮草包 , TodayNewMilitarySpyMessage as 今日新建编队对应的间谍消息 , TodayNewTroopSpyMessage as 今日部队出征对应的间谍消息 , PopulationPacks as 人口包 , PlanArchitecture as 计划建筑 , TransferFundArchitecture as 转移资金目标 , TransferFoodArchitecture as 转移粮草目标 , DefensiveLegion as 防御军团 , Captives as 俘虏列表 , RobberTroop as 盗贼部队 , RecentlyAttacked as 最近被攻击过 , RecentlyBreaked as 最近被攻破过 , Informations as 情报列表 , AILandLinks as 陆上连接建筑 , AIWaterLinks as 水上连接建筑 , youzainan as 灾难 , zainanleixing as 灾难类型 , zainanshengyutianshu as 灾难剩余天数 , feiziliebiao as 妃子列表 , Emperor as 皇帝 , MilitaryPopulation as 兵役人口 , SuspendTransfer as 暂停运输 , SuspendTroopTransfer as 暂停士兵运输 , Battle as Battle , OldFactionName as 旧势力   into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[城池] from Architecture";
                     string 城池类型 = "select ID as ID ,Name as 名称 ,AgricultureBase as 农业基础 ,AgricultureUnit as 农业单位 ,CommerceBase as 商业基础 ,CommerceUnit as 商业单位 ,TechnologyBase as 技术基础 ,TechnologyUnit as 技术单位 ,DominationBase as 统治基础 ,DominationUnit as 统治单位 ,MoraleBase as 民心基础 ,MoraleUnit as 民心单位 ,EnduranceBase as 耐久基础 ,EnduranceUnit as 耐久单位 ,PopulationBase as 人口基础 ,PopulationUnit as 人口单位 ,PopulationBoundary as 征兵人口界限 ,ViewDistance as 基本视野范围 ,VDIncrementDivisor as 视野范围增量除数 ,HasObliqueView as 斜向视角 ,HasLongView as 长视距 ,HasPopulation as 人口 ,HasAgriculture as 农业 ,HasCommerce as 商业 ,HasTechnology as 技术 ,HasDomination as 统治 ,HasMorale as 民心 ,HasEndurance as 耐久 ,HasHarbor as 可编组运兵船 ,FacilityPositionUnit as 设施单位空间 ,FundMaxUnit as 最大资金单位 ,FoodMaxUnit as 最大粮草单位 ,CountToMerit as 官职所包括 ,Expandable as 可扩建 ,ShipCanEnter as 船只可进入  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[城池类型] from ArchitectureKind";
                     string 默认攻击类型 = "select ID as ID ,Name as 名称  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[默认攻击类型] from AttackDefaultKind";
                     string 指向攻击类型 = "select ID as ID ,Name as 名称  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[指向攻击类型] from AttackTargetKind";
                     string 人物简历 = "select ID as ID ,SurName as 姓 ,GivenName as 名 ,Brief as 简要 ,Romance as 演义 ,History as 历史 ,InGame as 剧本 ,FactionColor as 势力颜色 ,MilitaryKinds as 兵种列表  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[人物简历] from Biography";
                     string 简历类型 = "select ID as ID ,Strength as 武力最少 ,Command as 统率最少 ,Intelligence as 智力最少 ,Politics as 政治最少 ,Glamour as 魅力最少 ,Braveness as 勇猛度最少 ,Calmness as 冷静度最少 ,Male as 只限男性 ,Female as 只限女性 ,PersonalLoyalty as 义理最少 ,Ambition as 野心最少 ,BioText as 文字 ,SuffixText as 称号文字  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[简历类型] from BiographyAdjectives";
                     string 俘虏 = "select ID as ID ,CaptivePerson as 俘虏人物 ,CaptiveFaction as 俘虏势力 ,RansomArchitecture as 赎金目标建筑 ,RansomFund as 赎金量 ,RansomArriveDays as 赎金到达时间  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[俘虏] from Captive";
                     string 默认施法类型 = "select ID as ID ,Name as 名称  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[默认施法类型] from CastDefaultKind";
                     string 指向施法类型 = "select ID as ID ,Name as 名称  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[指向施法类型] from CastTargetKind";
                     string 性格类型 = "select ID as ID ,Name as Name ,IntelligenceRate as 智从率 ,ChallengeChance as 单挑基本几率 ,ControversyChance as 论战基本几率 ,General as 将军 ,Brave as 勇将 ,Advisor as 军师 ,Politician as 识者 ,IntelGeneral as 智将 ,Emperor as 君主 ,AllRounder as 全能 ,Normal as 平凡文 ,Normal2 as 平凡武 ,Cheap as 庸才  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[性格类型] from CharacterKind";
                     string 颜色 = "select ID as ID ,Code as Code  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[颜色] from Color";
                     string 战法 = "select ID as ID ,Name as 名称 ,Description as 描述 ,Combativity as 所需战意 ,Influences as 影响列表 ,AttackDefault as 攻击默认 ,AttackTarget as 攻击目标 ,ArchitectureTarget as 目标可能为建筑 ,CastConditions as 使用条件 ,ViewingHostile as 视野内敌军越多越有可能使用 ,AnimationKind as 动画  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[战法] from CombatMethod";
                     string 条件 = "select ID as ID ,Kind as 对应种类 ,Name as 名称 ,Parameter as 参数 ,Parameter2 as 参数2  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[条件] from Condition";
                     string 条件类型 = "select ID as ID ,Name as 名称  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[条件类型] from ConditionKind";
                     string 外交关系 = "selectFaction1ID as 势力一ID ,Faction2ID as 势力二ID ,Truce as 外交关系 ,Relation as 停战时间  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[外交关系] from DiplomaticRelation";
                     string 灾难类型 = "select ID as ID ,名称 as 名称 ,时间下限 as 时间下限 ,时间上限 as 时间上限 ,人口伤害 as 人口伤害 ,军队伤害 as 军队伤害 ,统治伤害 as 统治伤害 ,耐久伤害 as 耐久伤害 ,资金伤害 as 资金伤害 ,军粮伤害 as 军粮伤害 ,农业伤害 as 农业伤害 ,商业伤害 as 商业伤害 ,技术伤害 as 技术伤害 ,民心伤害 as 民心伤害 ,武将伤害 as 武将伤害  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[灾难类型] from DisasterKind";
                     string 事件 = "select ID as ID ,Name as 名称 ,Happened as 已发生过 ,Repeatable as 可以重复 ,AfterEventHappened as 某事件发生之后 ,Chance as 发动几率 ,GloballyDisplayed as 全势力可见 ,StartYear as 开始年 ,StartMonth as 开始月 ,EndYear as 结束年 ,EndMonth as 结束月 ,PersonId as 武将编号 ,PersonCond as 武将条件 ,ArchitectureID as 建筑编号 ,ArchitectureCond as 建筑条件 ,FactionID as 势力编号 ,FactionCond as 势力条件 ,Dialog as 对话 ,Effect as 效果 ,ArchitectureEffect as 建筑效果 ,FactionEffect as 势力效果 ,ShowImage as 图片 ,ShowSound as 音效 ,ScenBiography as 武将列传 ,YesEffect as 選是的效果 ,NoEffect as 選否的效果 ,YesArchitectureEffect as YesArchitectureEffect ,NoArchitectureEffect as NoArchitectureEffect  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[事件] from Event";
                     string 事件效果 = "select ID as ID ,Name as 名称 ,Parameter as 参数 ,Parameter2 as 参数2 ,Kind as Kind  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[事件效果] from EventEffect";
                     string 事件效果类型 = "select ID as ID ,Name as 名称  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[事件效果类型] from EventEffectKind";
                     string 设施 = "select ID as ID ,KindID as KindID ,Endurance as 耐久  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[设施] from Facility";
                     string 设施类型 = "select ID as ID ,Name as 名称 ,Description as 描述 ,AILevel as AI強度 ,PositionOccupied as 占用位置 ,TechnologyNeeded as 新建技术 ,FundCost as 新建资金 ,MaintenanceCost as 维持费用 ,PointCost as 新建技巧 ,Days as 建造时间 ,Endurance as 耐久度 ,ArchitectureLimit as 建筑上限 ,FactionLimit as 势力上限 ,PopulationRelated as 人口相关 ,Influences as 影响 ,Conditions as 兴建条件 ,rongna as 容纳 ,bukechaichu as 不可拆除  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[设施类型] from FacilityKind";
                     string 势力 = "select ID as ID ,Passed as 是否进行过了 ,PreUserControlFinished as 用户控制前的任务是否完成 ,Controlling as 拥有当前控制权 ,LeaderID as 君主ID ,ColorIndex as 颜色编号 ,FName as 势力名 ,CapitalID as 都城ID ,TechniquePoint as 技巧点数 ,TechniquePointForTechnique as 为升级技巧所保留的技巧点数 ,TechniquePointForFacility as 为建造设施所保留的技巧点数 ,Reputation as 声望 ,Sections as 军区 ,Informations as 情报列表 ,Architectures as 建筑列表 ,Troops as 部队列表 ,Routeways as 粮道列表 ,Legions as 军团列表 ,BaseMilitaryKinds as 基本兵种列表 ,UpgradingTechnique as 正在升级中的技巧 ,UpgradingDaysLeft as 剩余时间 ,AvailableTechniques as 已有技巧 ,PreferredTechniqueKinds as 偏好技巧类别 ,PlanTechnique as 计划技巧 ,AutoRefuse as 自动拒绝释放俘虏 ,chaotinggongxiandu as 朝廷贡献度 ,guanjue as 官爵 ,IsAlien as 异族 ,NotPlayerSelectable as 玩家可选 ,PrinceID as 储君ID ,CreatePersonTimes as CreatePersonTimes ,YearOfficialLimit as YearOfficialLimit ,MilitaryCount as 编队数量 ,TransferingMilitaryCount as 编队运输数量 ,GetGeneratorPersonCount as GetGeneratorPersonCount  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[势力] from Faction";
                     string GameData = "selectPlayerList as 玩家列表 ,CurrentPlayer as 当前玩家 ,FactionQueue as 势力队列 ,FireTable as 火焰列表 ,NoFoodTable as 无粮列表 ,DaySince as 劇本自今天數  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[GameData] from GameData";
                     string GameParameters = "selectName as AbilityExperienceRate ,Value as 1  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[GameParameters] from GameParameters";
                     string GameSurvey = "selectTitle as 标题 ,GYear as 年 ,GMonth as 月 ,GDay as 日 ,SaveTime as 保存时间 ,PlayerInfo as 存档信息 ,JumpPosition as 初始位置 ,Description as 描述 ,GameTime as 遊戲時間  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[GameSurvey] from GameSurvey";
                     string GlobalVariables = "selectName as AdditionalPersonAvailable ,Value as False  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[GlobalVariables] from GlobalVariables";
                     string 官爵类型 = "select ID as ID ,名称 as 名称 ,声望上限 as 声望上限 ,需要贡献度 as 需要贡献度 ,需要城池 as 需要城池 ,ShowDialog as 显示传令  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[官爵类型] from guanjuezhonglei";
                     string 出仕考虑类型 = "select ID as ID ,Name as 名称 ,Offset as 偏差范围  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[出仕考虑类型] from IdealTendencyKind";
                     string 影响 = "select ID as ID ,Kind as 对应种类 ,Name as 名称 ,Description as 描述 ,Parameter as 参数 ,Parameter2 as 参数2  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[影响] from Influence";
                     string 影响类型 = "select ID as ID ,Type as 种类 ,Name as 名称 ,Combat as 战斗 ,AIPersonValue as 武將AI值 ,AIPersonValuePow as 武將AI值乘冪  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[影响类型] from InfluenceKind";
                     string 情报 = "select ID as ID ,iLevel as 等级 ,PositionX as PositionX ,PositionY as PositionY ,Radius as 半径 ,Oblique as 斜向 ,DayCost as 耗费资金 ,DaysLeft as 剩余天数 ,DaysStarted as 已执行日数  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[情报] from Information";
                     string 情报类型 = "select ID as ID ,iLevel as 等级 ,Radius as 半径 ,Oblique as 斜向 ,CostFund as 所需资金  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[情报类型] from InformationKind";
                     string 军团 = "select ID as ID ,Kind as 类型 ,StartArchitecture as 起始建筑 ,WillArchitecture as 目标建筑 ,PreferredRouteway as 偏好粮道 ,InformationDestination as InformationDestination ,CoreTroop as 核心部队 ,Troops as 部队列表  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[军团] from Legion";
                     string 地图 = "select ID as ID ,Name as 名称 ,TileWidth as 格宽 ,DimensionX as DimensionX ,DimensionY as DimensionY ,MapData as 地图数据 ,FileName as 文件名 ,kuaishu as 地图张数 ,meikuaidexiaokuaishu as 每张图含方格数 ,useSimpleArchImages as 使用一格式城池圖像  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[地图] from Map";
                     string 编队 = "select ID as ID ,Name as 队名 ,KindID as 种类ID ,Quantity as 人数 ,Morale as 士气 ,Combativity as 战意 ,Experience as 经验 ,InjuryQuantity as 伤兵 ,FollowedLeaderID as 追随将领ID ,LeaderID as 队长ID ,LeaderExperience as 队长经验 ,TrainingPersonID as 训练人ID ,RecruitmentPersonID as 补充人ID ,ShelledMilitary as 被包裹编队 ,Tiredness as 疲累度 ,ArrivingDays as 到达时间 ,StartingArchitectureID as 出发地 ,TargetArchitectureID as 目标建筑  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[编队] from Military";
                     string 编队类型 = "select ID as ID ,Type as 类别 ,Name as 名称 ,Description as 描述 ,Merit as 强度（AI） ,Successor as 较强兵种ID ,Speed as 行动速率 ,ObtainProb as 获得机率 ,TitleInfluence as 出兵称号影响 ,CreateCost as 新建资金 ,CreateTechnology as 新建所需技术 ,IsShell as 是否外壳 ,CreateBesideWater as 水边新建 ,Offence as 攻击 ,Defence as 防御 ,OffenceRadius as 攻击半径 ,CounterOffence as 能否反击 ,BeCountered as 能否被反击 ,ObliqueOffence as 斜向攻击 ,ArrowOffence as 箭矢攻击 ,AirOffence as 凌空攻击 ,ContactOffence as 近身攻击 ,OffenceOnlyBeforeMove as 只能在移动前攻击 ,ArchitectureDamageRate as 建筑伤害系数 ,ArchitectureCounterDamageRate as 建筑反击承受率 ,StratagemRadius as 计略范围 ,ObliqueStratagem as 斜向计略 ,ViewRadius as 视野半径 ,ObliqueView as 斜向视野 ,InjuryRate as 伤兵概率 ,Movability as 行动力 ,OneAdaptabilityKind as 单一适性种类 ,PlainAdaptability as 平原适性 ,GrasslandAdaptability as 草地适性 ,ForrestAdaptability as 森林适性  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[编队类型] from MilitaryKind";
                     string 人物 = "select ID as ID ,Available as 已出场 ,Alive as 是否活着 ,SurName as 姓氏 ,GivenName as 名字 ,CalledName as 字 ,Sex as 性别 ,Pic as 头像序号 ,Ideal as 相性 ,IdealTendency as 出仕相性考虑 ,LeaderPossibility as 新建势力可能性 ,PCharacter as 性格 ,YearAvailable as 出场年份 ,YearBorn as 出生年份 ,YearDead as 死亡年份 ,DeadReason as 死亡原因 ,Strength as 武勇 ,Command as 统率 ,Intelligence as 智谋 ,Politics as 政治 ,Glamour as 魅力 ,Reputation as 名声 ,UniqueTitles as 独有称号 ,UniqueMilitaryKinds as 独有兵种 ,StrengthExperience as 武勇经验 ,CommandExperience as 统率经验 ,IntelligenceExperience as 智谋经验 ,PoliticsExperience as 政治经验 ,GlamourExperience as 魅力经验 ,InternalExperience as 内政经验 ,TacticsExperience as 策略经验 ,BubingExperience as 步兵经验 ,NubingExperience as 弩兵经验 ,QibingExperience as 骑兵经验 ,ShuijunExperience as 水军经验  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[人物] from Person";
                     string 人物创建 = "select ID as ID ,BornLo as 出生年最小 ,BornHi as 出生年最大 ,DebutLo as 登場年最小 ,DebutHi as 登場年最大 ,DieLo as 壽命最小 ,DieHi as 壽命最大 ,FemaleChance as 女武将机率 ,DebutAtLeast as 登场年数最少  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[人物创建] from PersonGenerator";
                     string 人物创建类型 = "select ID as ID ,TypeName as 类型 ,GenerationChance as 生成机率比重 ,CommandLo as 统率最低 ,CommandHi as 统率最高 ,StrengthLo as 武力最低 ,StrengthHi as 武力最高 ,IntelligenceLo as 智力最低 ,IntelligenceHi as 智力最高 ,PoliticsLo as 政治最低 ,PoliticsHi as 政治最高 ,GlamourLo as 魅力最低 ,GlamourHi as 魅力最高 ,BraveLo as 勇猛度最低 ,BraveHi as 勇猛度最高 ,CalmnessLo as 冷静度最低 ,CalmnessHi as 冷静度最高 ,PersonalLoyaltyLo as 义理最低 ,PersonalLoyaltyHi as 义理最高 ,AmbitionLo as 野心最低 ,AmbitionHi as 野心最高 ,TitleChance as 有称号机率 ,AffectedByRateParameter as 是否受武将能力乘数设置影响 ,TypeCount as TypeCount ,FactionLimit as 势力上限 ,CostFund as 资金  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[人物创建类型] from PersonGeneratorType";
                     string 人物关系 = "selectPerson1 as 人物1 ,Person2 as 人物2 ,Relation as 關係  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[人物关系] from PersonRelation";
                     string 地域 = "select ID as ID ,Name as 名称 ,States as 下属州 ,RegionCore as 地区核心  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[地域] from Region";
                     string 粮道 = "select ID as ID ,Building as 疏通中 ,ShowArea as 显示补给范围 ,RemoveAfterClose as 关闭后删除 ,LastActivePointIndex as 最后的畅通点 ,InefficiencyDays as 低效率天数 ,StartArchitecture as 起点建筑 ,EndArchitecture as 终点建筑 ,DestinationArchitecture as 目标建筑 ,Points as 路径  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[粮道] from Routeway";
                     string 军区战略 = "select ID as ID ,Name as 名称 ,Description as 说明 ,OrientationKind as 目标种类 ,AutoRun as 是否自动执行 ,ValueAgriculture as 重视农业 ,ValueCommerce as 重视商业 ,ValueTechnology as 重视技术 ,ValueDomination as 重视统治 ,ValueMorale as 重视民心 ,ValueEndurance as 重视耐久 ,ValueTraining as 重视训练 ,ValueRecruitment as 重视补充 ,ValueNewMilitary as 重视新建编队 ,ValueOffensiveCampaign as 重视攻击 ,AllowInvestigateTactics as 允许使用情报和间谍 ,AllowOffensiveTactics as 允许使用煽动和破坏 ,AllowPersonTactics as 允许使用流言和说服 ,AllowOffensiveCampaign as 允许攻击 ,AllowFundTransfer as 允许输送资金 ,AllowFoodTransfer as 允许输送粮草 ,AllowMilitaryTransfer as 允许输送部队 ,AllowFacilityRemoval as 允许拆除设施 ,AllowNewMilitary as 允许新编编队  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[军区战略] from SectionAIDetail";
                     string 军区 = "select ID as ID ,Name as 名称 ,AIDetail as 委任类型 ,OrientationFaction as 目标势力 ,OrientationSection as 目标军区 ,OrientationState as 目标州域 ,OrientationArchitecture as 目标建筑 ,Architectures as 建筑列表  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[军区] from Sections";
                     string 技能 = "select ID as ID ,DisplayRow as 显示行 ,DisplayCol as 显示列 ,Kind as 类别 ,Level as 等级 ,Combat as 战斗 ,Name as 名称 ,Description as 描述 ,Prerequisite as 条件描述 ,Influences as 影响列表 ,Conditions as 条件列表 ,General as 将军 ,Brave as 勇将 ,Advisor as 军师 ,Politician as 识者 ,IntelGeneral as 智将 ,Emperor as 君主 ,AllRounder as 全能 ,Normal as 平凡文 ,Normal2 as 平凡武 ,Cheap as 庸才 ,Ability as 相关能力  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[技能] from Skill";
                     string 州 = "select ID as ID ,Name as 名称 ,ContactStates as 邻接州 ,StateAdmin as 州治所  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[州] from State";
                     string 计略 = "select ID as ID ,Name as 名称 ,Description as 描述 ,Combativity as 所需战意 ,Chance as 基本成功率 ,TechniquePoint as 成功技巧 ,Friendly as 友好 ,Self as 自身 ,AnimationKind as 动画 ,Influences as 影响列表 ,CastDefault as 施展默认 ,CastTarget as 施展目标 ,ArchitectureTarget as 目标可能为建筑 ,RequireInfluneceToUse as 有相应影响才能使用 ,CastConditions as 使用条件列表  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[计略] from Stratagem";
                     string 特技 = "select ID as ID ,Name as 名称 ,Combativity as 消耗战意 ,Period as 延续天数 ,Animation as 动画 ,Influences as 影响列表 ,CastConditions as 使用条件列表 ,LearnConditions as 修习条件列表 ,AIConditions as AI触发条件 ,General as 将军 ,Brave as 勇将 ,Advisor as 军师 ,Politician as 识者 ,IntelGeneral as 智将 ,Emperor as 君主 ,AllRounder as 全能 ,Normal as 平凡文 ,Normal2 as 平凡武 ,Cheap as 庸才 ,Ability as 相关能力  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[特技] from Stunt";
                     string 科技 = "select ID as ID ,Kind as 种类 ,DisplayRow as 显示行 ,DisplayCol as 显示列 ,Name as 名称 ,Description as 描述 ,PreID as PreID ,PostID as PostID ,Reputation as 需要声望 ,FundCost as 资金消耗 ,PointCost as 技巧点数消耗 ,Days as 升级时间 ,Influences as 影响列表 ,Conditions as 条件列表  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[科技] from Technique";
                     string 地形 = "select ID as 地形编号 ,Name as 地形名称 ,GraphicLayer as 图形层次 ,ViewThrough as 视线可穿透 ,RoutewayBuildFundCost as 粮道开通资金消耗 ,RoutewayActiveFundCost as 粮道维持资金消耗 ,RoutewayBuildWorkCost as 粮道开通工作量 ,RoutewayConsumptionRate as 粮草消耗率 ,FoodDeposit as 粮草蕴藏量 ,FoodRegainDays as 粮草恢复天数 ,FoodSpringRate as 春粮系数 ,FoodSummerRate as 夏粮系数 ,FoodAutumnRate as 秋粮系数 ,FoodWinterRate as 冬粮系数 ,FireDamageRate as 火焰伤害率  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[地形] from TerrainDetail";
                     string 个性语言 = "select ID as  ,SurName as  ,GivenName as  ,CriticalStrike as  ,CriticalStrikeOnArchitecture as  ,ReceiveCriticalStrike as  ,Surround as  ,Rout as  ,DualInitiativeWin as  ,DualPassiveWin as  ,ControversyInitiativeWin as  ,ControversyPassiveWin as  ,Chaos as  ,DeepChaos as  ,CastDeepChaos as  ,RecoverFromChaos as  ,TrappedByStratagem as  ,HelpedByStratagem as  ,ResistHarmfulStratagem as  ,ResistHelpfulStratagem as  ,AntiAttack as  ,BreakWall as  ,OutburstAngry as  ,OutburstQuiet as   into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[个性语言] from TextMessage";
                     string 个性语言类型 = "select ID as ID ,Description as 說明  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[个性语言类型] from TextMessageKind";
                     string 个性语言全图 = "select ID as ID ,Person as 武将编号 ,Kind as 种类 ,Messages as 信息文字  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[个性语言全图] from TextMessageMap";
                     string 动画 = "select ID as ID ,Name as 名称 ,FrameCount as 帧数 ,StayCount as 停留帧数 ,Back as 显示在深层  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[动画] from TileAnimation";
                     string 称号 = "select ID as ID ,Kind as 类别 ,Level as 等级 ,Combat as 战斗 ,Name as 名称 ,Description as 描述 ,Prerequisite as 条件 ,Influences as 影响列表 ,Conditions as 条件列表 ,ArchitectureConditions as 建筑条件 ,FactionConditions as 势力条件 ,LoseConditions as 失去条件 ,AutoLearn as 自动习得机率 ,AutoLearnText as 习得对话 ,AutoLearnTextByCourier as 习得传令官对话 ,MapLimit as 全地图数目上限 ,FactionLimit as 势力数目上限 ,InheritChance as 继承机率 ,General as 将军 ,Brave as 勇将 ,Advisor as 军师 ,Politician as 识者 ,IntelGeneral as 智将 ,Emperor as 君主 ,AllRounder as 全能 ,Normal as 平凡文 ,Normal2 as 平凡武 ,Cheap as 庸才 ,Ability as 相关能力 ,ManualAward as 手动授予  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[称号] from Title";
                     string 称号类型 = "select ID as ID ,KName as 名称 ,Combat as 战斗 ,StudyDay as 習得日數 ,SuccessRate as 習得成功率  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[称号类型] from TitleKind";
                     string 宝物 = "select ID as ID ,Name as 名称 ,Pic as 图像 ,Worth as 价值 ,Available as 已出现 ,HidePlace as 隐藏于建筑 ,TreasureGroup as 宝物种类 ,AppearYear as 出现年 ,BelongedPerson as 属于人物 ,Influences as 影响列表 ,Description as 介绍  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[宝物] from Treasure";
                     string 部队 = "select ID as ID ,LeaderID as 队长ID ,Controllable as 可控 ,Status as 状态 ,Direction as 方向 ,Auto as 委任 ,Operated as 操作完成 ,Food as 粮草 ,StartingArchitecture as 出发地 ,Persons as 所属人物 ,PositionX as 位置X ,PositionY as 位置Y ,DestinationX as 目标X ,DestinationY as 目标Y ,RealDestinationX as 真实目标X ,RealDestinationY as 真实目标Y ,FirstTierPath as 第一层路径 ,SecondTierPath as 第二层路径 ,ThirdTierPath as 第三层路径 ,FirstIndex as FirstIndex ,SecondIndex as SecondIndex ,ThirdIndex as ThirdIndex ,MilitaryID as 编队ID ,AttackDefaultKind as 攻击默认种类 ,AttackTargetKind as 攻击目标种类 ,CastDefaultKind as 施展默认种类 ,CastTargetKind as 施展目标种类 ,跟随部队ID as 目标部队 ,TargetTroopID as 目标建筑 ,TargetArchitectureID as 意愿部队 ,WillTroopID as 意愿建筑 ,WillArchitectureID as 当前战法 ,CurrentCombatMethodID as 当前计略 ,CurrentStratagemID as 自施展位置X ,SelfCastPositionX as 自施展位置Y  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[部队] from Troop";
                     string 部队动画 = "select ID as ID ,Name as 名称 ,FrameCount as 帧数 ,StayCount as 停留帧数  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[部队动画] from TroopAnimation";
                     string 部队事件 = "select ID as ID ,Name as 名称 ,Happened as 已发生过 ,Repeatable as 可以重复 ,AfterEventHappened as 某事件发生之后 ,LaunchPerson as 发动人物 ,Conditions as 发动条件 ,Chance as 发动几率 ,CheckAreaKind as 搜索范围 ,TargetPersons as 目标人物列表 ,Dialogs as 人物对话 ,EffectSelf as 自身效果 ,EffectPersons as 特定人物效果 ,EffectAreas as 特定范围效果 ,ShowImage as 图片 ,ShowSound as 音效  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[部队事件] from TroopEvent";
                     string 部队事件效果 = "select ID as ID ,Kind as 种类 ,Name as 名称 ,Parameter as 参数  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[部队事件效果] from TroopEventEffect";
                     string 部队事件效果类型 = "select ID as ID ,Name as 名称  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[部队事件效果类型] from TroopEventEffectKind";
                     string 年表 = "select ID as ID ,GYear as 年 ,GMonth as 月 ,GDay as 日 ,Faction as 势力 ,Content as 内容 ,IsGloballyKnown as 是否全地图看到  into   [Excel 8.0;database=" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[年表] from YearTable";
                     */
                 /*   if (表名.Contains("Architecture"))
                    {
                        OleDbCommand command = new OleDbCommand(城池, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("ArchitectureKind"))
                    {
                        OleDbCommand command = new OleDbCommand(城池类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("AttackDefaultKind"))
                    {
                        OleDbCommand command = new OleDbCommand(默认攻击类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("AttackTargetKind"))
                    {
                        OleDbCommand command = new OleDbCommand(指向攻击类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Biography"))
                    {
                        OleDbCommand command = new OleDbCommand(人物简历, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("BiographyAdjectives"))
                    {
                        OleDbCommand command = new OleDbCommand(简历类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Captive"))
                    {
                        OleDbCommand command = new OleDbCommand(俘虏, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("CastDefaultKind"))
                    {
                        OleDbCommand command = new OleDbCommand(默认施法类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("CastTargetKind"))
                    {
                        OleDbCommand command = new OleDbCommand(指向施法类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("CharacterKind"))
                    {
                        OleDbCommand command = new OleDbCommand(性格类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Color"))
                    {
                        OleDbCommand command = new OleDbCommand(颜色, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("CombatMethod"))
                    {
                        OleDbCommand command = new OleDbCommand(战法, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Condition"))
                    {
                        OleDbCommand command = new OleDbCommand(条件, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("ConditionKind"))
                    {
                        OleDbCommand command = new OleDbCommand(条件类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("DiplomaticRelation"))
                    {
                        OleDbCommand command = new OleDbCommand(外交关系, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("DisasterKind"))
                    {
                        OleDbCommand command = new OleDbCommand(灾难类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Event"))
                    {
                        OleDbCommand command = new OleDbCommand(事件, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("EventEffect"))
                    {
                        OleDbCommand command = new OleDbCommand(事件效果, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("EventEffectKind"))
                    {
                        OleDbCommand command = new OleDbCommand(事件效果类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Facility"))
                    {
                        OleDbCommand command = new OleDbCommand(设施, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("FacilityKind"))
                    {
                        OleDbCommand command = new OleDbCommand(设施类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Faction"))
                    {
                        OleDbCommand command = new OleDbCommand(势力, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("GameData"))
                    {
                        OleDbCommand command = new OleDbCommand(GameData, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("GameParameters"))
                    {
                        OleDbCommand command = new OleDbCommand(GameParameters, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("GameSurvey"))
                    {
                        OleDbCommand command = new OleDbCommand(GameSurvey, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("GlobalVariables"))
                    {
                        OleDbCommand command = new OleDbCommand(GlobalVariables, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("guanjuezhonglei"))
                    {
                        OleDbCommand command = new OleDbCommand(官爵类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("IdealTendencyKind"))
                    {
                        OleDbCommand command = new OleDbCommand(出仕考虑类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Influence"))
                    {
                        OleDbCommand command = new OleDbCommand(影响, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("InfluenceKind"))
                    {
                        OleDbCommand command = new OleDbCommand(影响类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Information"))
                    {
                        OleDbCommand command = new OleDbCommand(情报, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("InformationKind"))
                    {
                        OleDbCommand command = new OleDbCommand(情报类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Legion"))
                    {
                        OleDbCommand command = new OleDbCommand(军团, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Map"))
                    {
                        OleDbCommand command = new OleDbCommand(地图, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Military"))
                    {
                        OleDbCommand command = new OleDbCommand(编队, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("MilitaryKind"))
                    {
                        OleDbCommand command = new OleDbCommand(编队类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Person"))
                    {
                        OleDbCommand command = new OleDbCommand(人物, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("PersonGenerator"))
                    {
                        OleDbCommand command = new OleDbCommand(人物创建, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("PersonGeneratorType"))
                    {
                        OleDbCommand command = new OleDbCommand(人物创建类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("PersonRelation"))
                    {
                        OleDbCommand command = new OleDbCommand(人物关系, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Region"))
                    {
                        OleDbCommand command = new OleDbCommand(地域, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Routeway"))
                    {
                        OleDbCommand command = new OleDbCommand(粮道, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("SectionAIDetail"))
                    {
                        OleDbCommand command = new OleDbCommand(军区战略, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Sections"))
                    {
                        OleDbCommand command = new OleDbCommand(军区, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Skill"))
                    {
                        OleDbCommand command = new OleDbCommand(技能, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("State"))
                    {
                        OleDbCommand command = new OleDbCommand(州, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Stratagem"))
                    {
                        OleDbCommand command = new OleDbCommand(计略, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Stunt"))
                    {
                        OleDbCommand command = new OleDbCommand(特技, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Technique"))
                    {
                        OleDbCommand command = new OleDbCommand(科技, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TerrainDetail"))
                    {
                        OleDbCommand command = new OleDbCommand(地形, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TextMessage"))
                    {
                        OleDbCommand command = new OleDbCommand(个性语言, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TextMessageKind"))
                    {
                        OleDbCommand command = new OleDbCommand(个性语言类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TextMessageMap"))
                    {
                        OleDbCommand command = new OleDbCommand(个性语言全图, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TileAnimation"))
                    {
                        OleDbCommand command = new OleDbCommand(动画, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Title"))
                    {
                        OleDbCommand command = new OleDbCommand(称号, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TitleKind"))
                    {
                        OleDbCommand command = new OleDbCommand(称号类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Treasure"))
                    {
                        OleDbCommand command = new OleDbCommand(宝物, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("Troop"))
                    {
                        OleDbCommand command = new OleDbCommand(部队, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TroopAnimation"))
                    {
                        OleDbCommand command = new OleDbCommand(部队动画, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TroopEvent"))
                    {
                        OleDbCommand command = new OleDbCommand(部队事件, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TroopEventEffect"))
                    {
                        OleDbCommand command = new OleDbCommand(部队事件效果, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("TroopEventEffectKind"))
                    {
                        OleDbCommand command = new OleDbCommand(部队事件效果类型, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    if (表名.Contains("YearTable"))
                    {
                        OleDbCommand command = new OleDbCommand(年表, mdb文件);
                        command.ExecuteNonQuery();
                    }
                    mdb文件.Close();*/
                }
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            MessageBox.Show("所有文件都已经转换为同名" + 将导出文件类型 + "文件");
        }

        private void mdbtoexcel(List<string> 表名, OleDbConnection mdb文件,int i)
        {
            if (!Directory.Exists(@Application.StartupPath + @"\转换生成文件\mdb转excel"))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(@Application.StartupPath + @"\转换生成文件\mdb转excel");
            }

            mdb文件.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + 已导入文件.Items[i].ToString();

            表名.Clear();
            mdb文件.Open();
            DataTable dt = mdb文件.GetSchema("Tables");
            foreach (DataRow row in dt.Rows)
            {
                string 表类型 = (string)row["TABLE_TYPE"];
                if (表类型.Contains("TABLE"))
                {
                    表名.Add(row["TABLE_NAME"].ToString());
                }
            }
            //以下通用
            string 城池 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Architecture] from Architecture";
            string 城池类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[ArchitectureKind] from ArchitectureKind";
            string 默认攻击类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[AttackDefaultKind] from AttackDefaultKind";
            string 指向攻击类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[AttackTargetKind] from AttackTargetKind";
            string 人物简历 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Biography] from Biography";
            string 简历类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[BiographyAdjectives] from BiographyAdjectives";
            string 俘虏 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Captive] from Captive";
            string 默认施法类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[CastDefaultKind] from CastDefaultKind";
            string 指向施法类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[CastTargetKind] from CastTargetKind";
            string 性格类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[CharacterKind] from CharacterKind";
            string 颜色 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Color] from Color";
            string 战法 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[CombatMethod] from CombatMethod";
            string 条件 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Condition] from Condition";
            string 条件类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[ConditionKind] from ConditionKind";
            string 外交关系 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[DiplomaticRelation] from DiplomaticRelation";
            string 灾难类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[DisasterKind] from DisasterKind";
            string 事件 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Event] from Event";
            string 事件效果 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[EventEffect] from EventEffect";
            string 事件效果类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[EventEffectKind] from EventEffectKind";
            string 设施 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Facility] from Facility";
            string 设施类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[FacilityKind] from FacilityKind";
            string 势力 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Faction] from Faction";
            string GameData = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[GameData] from GameData";
            string GameParameters = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[GameParameters] from GameParameters";
            string GameSurvey = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[GameSurvey] from GameSurvey";
            string GlobalVariables = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[GlobalVariables] from GlobalVariables";
            string 官爵类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[guanjuezhonglei] from guanjuezhonglei";
            string 出仕考虑类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[IdealTendencyKind] from IdealTendencyKind";
            string 影响 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Influence] from Influence";
            string 影响类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[InfluenceKind] from InfluenceKind";
            string 情报 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Information] from Information";
            string 情报类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[InformationKind] from InformationKind";
            string 军团 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Legion] from Legion";
            string 地图 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Map] from Map";
            string 编队 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Military] from Military";
            string 编队类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[MilitaryKind] from MilitaryKind";
            string 人物 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Person] from Person";
            string 人物创建 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[PersonGenerator] from PersonGenerator";
            string 人物创建类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[PersonGeneratorType] from PersonGeneratorType";
            string 人物关系 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[PersonRelation] from PersonRelation";
            string 地域 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Region] from Region";
            string 粮道 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Routeway] from Routeway";
            string 军区战略 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[SectionAIDetail] from SectionAIDetail";
            string 军区 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Sections] from Sections";
            string 技能 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Skill] from Skill";
            string 州 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[State] from State";
            string 计略 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Stratagem] from Stratagem";
            string 特技 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Stunt] from Stunt";
            string 科技 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Technique] from Technique";
            string 地形 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TerrainDetail] from TerrainDetail";
            string 个性语言 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TextMessage] from TextMessage";
            string 个性语言类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TextMessageKind] from TextMessageKind";
            string 个性语言全图 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TextMessageMap] from TextMessageMap";
            string 动画 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TileAnimation] from TileAnimation";
            string 称号 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Title] from Title";
            string 称号类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TitleKind] from TitleKind";
            string 宝物 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Treasure] from Treasure";
            string 部队 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[Troop] from Troop";
            string 部队动画 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TroopAnimation] from TroopAnimation";
            string 部队事件 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TroopEvent] from TroopEvent";
            string 部队事件效果 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TroopEventEffect] from TroopEventEffect";
            string 部队事件效果类型 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[TroopEventEffectKind] from TroopEventEffectKind";
            string 年表 = " select * into   [Excel 8.0;database=" +Application.StartupPath + "\\转换生成文件\\mdb转excel\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "].[YearTable] from YearTable";

            if (表名.Contains("Architecture"))
            {
                OleDbCommand command = new OleDbCommand(城池, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("ArchitectureKind"))
            {
                OleDbCommand command = new OleDbCommand(城池类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("AttackDefaultKind"))
            {
                OleDbCommand command = new OleDbCommand(默认攻击类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("AttackTargetKind"))
            {
                OleDbCommand command = new OleDbCommand(指向攻击类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Biography"))
            {
                OleDbCommand command = new OleDbCommand(人物简历, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("BiographyAdjectives"))
            {
                OleDbCommand command = new OleDbCommand(简历类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Captive"))
            {
                OleDbCommand command = new OleDbCommand(俘虏, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("CastDefaultKind"))
            {
                OleDbCommand command = new OleDbCommand(默认施法类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("CastTargetKind"))
            {
                OleDbCommand command = new OleDbCommand(指向施法类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("CharacterKind"))
            {
                OleDbCommand command = new OleDbCommand(性格类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Color"))
            {
                OleDbCommand command = new OleDbCommand(颜色, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("CombatMethod"))
            {
                OleDbCommand command = new OleDbCommand(战法, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Condition"))
            {
                OleDbCommand command = new OleDbCommand(条件, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("ConditionKind"))
            {
                OleDbCommand command = new OleDbCommand(条件类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("DiplomaticRelation"))
            {
                OleDbCommand command = new OleDbCommand(外交关系, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("DisasterKind"))
            {
                OleDbCommand command = new OleDbCommand(灾难类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Event"))
            {
                OleDbCommand command = new OleDbCommand(事件, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("EventEffect"))
            {
                OleDbCommand command = new OleDbCommand(事件效果, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("EventEffectKind"))
            {
                OleDbCommand command = new OleDbCommand(事件效果类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Facility"))
            {
                OleDbCommand command = new OleDbCommand(设施, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("FacilityKind"))
            {
                OleDbCommand command = new OleDbCommand(设施类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Faction"))
            {
                OleDbCommand command = new OleDbCommand(势力, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("GameData"))
            {
                OleDbCommand command = new OleDbCommand(GameData, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("GameParameters"))
            {
                OleDbCommand command = new OleDbCommand(GameParameters, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("GameSurvey"))
            {
                OleDbCommand command = new OleDbCommand(GameSurvey, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("GlobalVariables"))
            {
                OleDbCommand command = new OleDbCommand(GlobalVariables, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("guanjuezhonglei"))
            {
                OleDbCommand command = new OleDbCommand(官爵类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("IdealTendencyKind"))
            {
                OleDbCommand command = new OleDbCommand(出仕考虑类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Influence"))
            {
                OleDbCommand command = new OleDbCommand(影响, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("InfluenceKind"))
            {
                OleDbCommand command = new OleDbCommand(影响类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Information"))
            {
                OleDbCommand command = new OleDbCommand(情报, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("InformationKind"))
            {
                OleDbCommand command = new OleDbCommand(情报类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Legion"))
            {
                OleDbCommand command = new OleDbCommand(军团, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Map"))
            {
                OleDbCommand command = new OleDbCommand(地图, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Military"))
            {
                OleDbCommand command = new OleDbCommand(编队, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("MilitaryKind"))
            {
                OleDbCommand command = new OleDbCommand(编队类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Person"))
            {
                OleDbCommand command = new OleDbCommand(人物, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("PersonGenerator"))
            {
                OleDbCommand command = new OleDbCommand(人物创建, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("PersonGeneratorType"))
            {
                OleDbCommand command = new OleDbCommand(人物创建类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("PersonRelation"))
            {
                OleDbCommand command = new OleDbCommand(人物关系, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Region"))
            {
                OleDbCommand command = new OleDbCommand(地域, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Routeway"))
            {
                OleDbCommand command = new OleDbCommand(粮道, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("SectionAIDetail"))
            {
                OleDbCommand command = new OleDbCommand(军区战略, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Sections"))
            {
                OleDbCommand command = new OleDbCommand(军区, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Skill"))
            {
                OleDbCommand command = new OleDbCommand(技能, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("State"))
            {
                OleDbCommand command = new OleDbCommand(州, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Stratagem"))
            {
                OleDbCommand command = new OleDbCommand(计略, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Stunt"))
            {
                OleDbCommand command = new OleDbCommand(特技, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Technique"))
            {
                OleDbCommand command = new OleDbCommand(科技, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TerrainDetail"))
            {
                OleDbCommand command = new OleDbCommand(地形, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TextMessage"))
            {
                OleDbCommand command = new OleDbCommand(个性语言, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TextMessageKind"))
            {
                OleDbCommand command = new OleDbCommand(个性语言类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TextMessageMap"))
            {
                OleDbCommand command = new OleDbCommand(个性语言全图, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TileAnimation"))
            {
                OleDbCommand command = new OleDbCommand(动画, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Title"))
            {
                OleDbCommand command = new OleDbCommand(称号, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TitleKind"))
            {
                OleDbCommand command = new OleDbCommand(称号类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Treasure"))
            {
                OleDbCommand command = new OleDbCommand(宝物, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("Troop"))
            {
                OleDbCommand command = new OleDbCommand(部队, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TroopAnimation"))
            {
                OleDbCommand command = new OleDbCommand(部队动画, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TroopEvent"))
            {
                OleDbCommand command = new OleDbCommand(部队事件, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TroopEventEffect"))
            {
                OleDbCommand command = new OleDbCommand(部队事件效果, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("TroopEventEffectKind"))
            {
                OleDbCommand command = new OleDbCommand(部队事件效果类型, mdb文件);
                command.ExecuteNonQuery();
            }
            if (表名.Contains("YearTable"))
            {
                OleDbCommand command = new OleDbCommand(年表, mdb文件);
                command.ExecuteNonQuery();
            }
            mdb文件.Close();
        }


        private void copytoexcel(Excel.Application aaa, Excel.Workbook workBook, DataTable dt,int m)
        {
            StringBuilder sb = new StringBuilder();
            if (m < workBook.Worksheets.Count - 1)
            {
                for (int k = 0; k < dt.Columns.Count; k++)
                {
                    // 添加列名称  
                    sb.Append(dt.Columns[k].ColumnName.ToString() + "\t");
                }
                sb.Append(Environment.NewLine);
            }
            // 添加行数据  
            for (int ii = 0; ii < dt.Rows.Count; ii++)
            {
                DataRow row = dt.Rows[ii];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    sb.Append(row[j].ToString() + "\t");
                }
                sb.Append(Environment.NewLine);
            }
            Clipboard.SetText(sb.ToString());//系统剪切板
            if (m >= workBook.Worksheets.Count - 1)
            {
                workBook.Worksheets[m].Cells[3, 1].PasteSpecial();
                // workBook.Worksheets[m].Cells[3, 1].PasteSpecial(Excel.XlPasteType.xlPasteValues);//excel自带的粘贴
            }
            else
            {
                workBook.Worksheets[m].Cells[1, 1].PasteSpecial();
                // workBook.Worksheets[m].Cells[1, 1].PasteSpecial(Excel.XlPasteType.xlPasteValues);
            }
        }

        private void exceltojson(int k)
        {
            scenario = new GameScenario();
            scenario.Init();
            int i = 0;
            string 连接符 = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + 已导入文件.Items[k].ToString()+ "; Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"";

            System.Data.OleDb.OleDbDataAdapter 数据连接 = new System.Data.OleDb.OleDbDataAdapter();
            System.Data.DataSet ds = new System.Data.DataSet();
            数据连接 = new OleDbDataAdapter("select  * from [Architecture$]", 连接符);
            数据连接.Fill(ds, "Architecture");
            数据连接 = new OleDbDataAdapter("select  * from [ArchitectureKind$]", 连接符);
            数据连接.Fill(ds, "ArchitectureKind");
            数据连接 = new OleDbDataAdapter("select  * from [AttackDefaultKind$]", 连接符);
            数据连接.Fill(ds, "AttackDefaultKind");
            数据连接 = new OleDbDataAdapter("select  * from [AttackTargetKind$]", 连接符);
            数据连接.Fill(ds, "AttackTargetKind");
            数据连接 = new OleDbDataAdapter("select  * from [Biography$]", 连接符);
            数据连接.Fill(ds, "Biography");
            数据连接 = new OleDbDataAdapter("select  * from [BiographyAdjectives$]", 连接符);
            数据连接.Fill(ds, "BiographyAdjectives");
            数据连接 = new OleDbDataAdapter("select  * from [Captive$]", 连接符);
            数据连接.Fill(ds, "Captive");
            数据连接 = new OleDbDataAdapter("select  * from [CastDefaultKind$]", 连接符);
            数据连接.Fill(ds, "CastDefaultKind");
            数据连接 = new OleDbDataAdapter("select  * from [CastTargetKind$]", 连接符);
            数据连接.Fill(ds, "CastTargetKind");
            数据连接 = new OleDbDataAdapter("select  * from [CharacterKind$]", 连接符);
            数据连接.Fill(ds, "CharacterKind");
            数据连接 = new OleDbDataAdapter("select  * from [Color$]", 连接符);
            数据连接.Fill(ds, "Color");
            数据连接 = new OleDbDataAdapter("select  * from [CombatMethod$]", 连接符);
            数据连接.Fill(ds, "CombatMethod");
            数据连接 = new OleDbDataAdapter("select  * from [Condition$]", 连接符);
            数据连接.Fill(ds, "Condition");
            数据连接 = new OleDbDataAdapter("select  * from [ConditionKind$]", 连接符);
            数据连接.Fill(ds, "ConditionKind");
            数据连接 = new OleDbDataAdapter("select  * from [DiplomaticRelation$]", 连接符);
            数据连接.Fill(ds, "DiplomaticRelation");
            数据连接 = new OleDbDataAdapter("select  * from [DisasterKind$]", 连接符);
            数据连接.Fill(ds, "DisasterKind");
            数据连接 = new OleDbDataAdapter("select  * from [Event$]", 连接符);
            数据连接.Fill(ds, "Event");
            数据连接 = new OleDbDataAdapter("select  * from [EventEffect$]", 连接符);
            数据连接.Fill(ds, "EventEffect");
            数据连接 = new OleDbDataAdapter("select  * from [EventEffectKind$]", 连接符);
            数据连接.Fill(ds, "EventEffectKind");
            数据连接 = new OleDbDataAdapter("select  * from [Facility$]", 连接符);
            数据连接.Fill(ds, "Facility");
            数据连接 = new OleDbDataAdapter("select  * from [FacilityKind$]", 连接符);
            数据连接.Fill(ds, "FacilityKind");
            数据连接 = new OleDbDataAdapter("select  * from [Faction$]", 连接符);
            数据连接.Fill(ds, "Faction");
            数据连接 = new OleDbDataAdapter("select  * from [guanjuezhonglei$]", 连接符);
            数据连接.Fill(ds, "guanjuezhonglei");
            数据连接 = new OleDbDataAdapter("select  * from [IdealTendencyKind$]", 连接符);
            数据连接.Fill(ds, "IdealTendencyKind");
            数据连接 = new OleDbDataAdapter("select  * from [Influence$]", 连接符);
            数据连接.Fill(ds, "Influence");
            数据连接 = new OleDbDataAdapter("select  * from [InfluenceKind$]", 连接符);
            数据连接.Fill(ds, "InfluenceKind");
            数据连接 = new OleDbDataAdapter("select  * from [Information$]", 连接符);
            数据连接.Fill(ds, "Information");
            数据连接 = new OleDbDataAdapter("select  * from [InformationKind$]", 连接符);
            数据连接.Fill(ds, "InformationKind");
            数据连接 = new OleDbDataAdapter("select  * from [Legion$]", 连接符);
            数据连接.Fill(ds, "Legion");
            数据连接 = new OleDbDataAdapter("select  * from [Map$]", 连接符);
            数据连接.Fill(ds, "Map");
            数据连接 = new OleDbDataAdapter("select  * from [Military$]", 连接符);
            数据连接.Fill(ds, "Military");
            数据连接 = new OleDbDataAdapter("select  * from [MilitaryKind$]", 连接符);
            数据连接.Fill(ds, "MilitaryKind");
            数据连接 = new OleDbDataAdapter("select  * from [Person$]", 连接符);
            数据连接.Fill(ds, "Person");
            数据连接 = new OleDbDataAdapter("select  * from [PersonGenerator$]", 连接符);
            数据连接.Fill(ds, "PersonGenerator");
            数据连接 = new OleDbDataAdapter("select  * from [PersonGeneratorType$]", 连接符);
            数据连接.Fill(ds, "PersonGeneratorType");
            数据连接 = new OleDbDataAdapter("select  * from [Region$]", 连接符);
            数据连接.Fill(ds, "Region");
            数据连接 = new OleDbDataAdapter("select  * from [Routeway$]", 连接符);
            数据连接.Fill(ds, "Routeway");
            数据连接 = new OleDbDataAdapter("select  * from [SectionAIDetail$]", 连接符);
            数据连接.Fill(ds, "SectionAIDetail");
            数据连接 = new OleDbDataAdapter("select  * from [Sections$]", 连接符);
            数据连接.Fill(ds, "Sections");
            数据连接 = new OleDbDataAdapter("select  * from [Skill$]", 连接符);
            数据连接.Fill(ds, "Skill");
            数据连接 = new OleDbDataAdapter("select  * from [State$]", 连接符);
            数据连接.Fill(ds, "State");
            数据连接 = new OleDbDataAdapter("select  * from [Stratagem$]", 连接符);
            数据连接.Fill(ds, "Stratagem");
            数据连接 = new OleDbDataAdapter("select  * from [Stunt$]", 连接符);
            数据连接.Fill(ds, "Stunt");
            数据连接 = new OleDbDataAdapter("select  * from [Technique$]", 连接符);
            数据连接.Fill(ds, "Technique");
            数据连接 = new OleDbDataAdapter("select  * from [TerrainDetail$]", 连接符);
            数据连接.Fill(ds, "TerrainDetail");
            数据连接 = new OleDbDataAdapter("select  * from [TextMessages$]", 连接符);
            数据连接.Fill(ds, "TextMessages");
            数据连接 = new OleDbDataAdapter("select  * from [TextMessageKind$]", 连接符);
            数据连接.Fill(ds, "TextMessageKind");
            数据连接 = new OleDbDataAdapter("select  * from [TileAnimation$]", 连接符);
            数据连接.Fill(ds, "TileAnimation");
            数据连接 = new OleDbDataAdapter("select  * from [Title$]", 连接符);
            数据连接.Fill(ds, "Title");
            数据连接 = new OleDbDataAdapter("select  * from [TitleKind$]", 连接符);
            数据连接.Fill(ds, "TitleKind");
            数据连接 = new OleDbDataAdapter("select  * from [TrainPolicy$]", 连接符);
            数据连接.Fill(ds, "TrainPolicy");
            数据连接 = new OleDbDataAdapter("select  * from [Treasure$]", 连接符);
            数据连接.Fill(ds, "Treasure");
            数据连接 = new OleDbDataAdapter("select  * from [Troop$]", 连接符);
            数据连接.Fill(ds, "Troop");
            数据连接 = new OleDbDataAdapter("select  * from [TroopAnimation$]", 连接符);
            数据连接.Fill(ds, "TroopAnimation");
            数据连接 = new OleDbDataAdapter("select  * from [TroopEvent$]", 连接符);
            数据连接.Fill(ds, "TroopEvent");
            数据连接 = new OleDbDataAdapter("select  * from [TroopEventEffect$]", 连接符);
            数据连接.Fill(ds, "TroopEventEffect");
            数据连接 = new OleDbDataAdapter("select  * from [TroopEventEffectKind$]", 连接符);
            数据连接.Fill(ds, "TroopEventEffectKind");
            数据连接 = new OleDbDataAdapter("select  * from [YearTable$]", 连接符);
            数据连接.Fill(ds, "YearTable");
            数据连接 = new OleDbDataAdapter("select  * from [Parameters$]", 连接符);
            数据连接.Fill(ds, "Parameters");
            数据连接 = new OleDbDataAdapter("select  * from [GlobalVariables$]", 连接符);
            数据连接.Fill(ds, "GlobalVariables");

            DataTable dt = new DataTable();
            //把GameGlobal.Parameters （class类）的所有public 字段和类型加入到映射表中
            System.Reflection.FieldInfo[] 非getset字段表 = typeof(GameGlobal.Parameters).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            System.Reflection.PropertyInfo[] getset字段表 = null;
            string[] 取值字段 = new string[] { };
            取值字段 = Para参数取值字段();
            dt = ds.Tables["Parameters"];
            for (i = 1; i < dt.Rows.Count; i++)
            {
                if (i == 1)
                {
                    scenario.Parameters = new GameGlobal.Parameters();
                }
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (v.Name == dt.Rows[i][0].ToString() && 取值字段.Contains(v.Name))//如果字段的值=第一列中的文本，则
                    {
                        if (dt.Rows[i][1].ToString() == "真")//excel会默认把true和false大写，之后转换到datatable时，又会变成汉字真假
                        {
                            dt.Rows[i][1] = "True";
                        }
                        else if (dt.Rows[i][1].ToString() == "假")
                        {
                            dt.Rows[i][1] = "False";
                        }
                        if (v.Name == "ExpandConditions")
                        {
                            List<int> listii = new List<int>();
                            GameGlobal.StaticMethods.LoadFromString(listii, dt.Rows[i][1].ToString());
                            // scenario.Parameters.ExpandConditions = listii;
                            v.SetValue(scenario.Parameters, listii);
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][1].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(scenario.Parameters, (int)de);
                            }
                            else
                            {
                                v.SetValue(scenario.Parameters, int.Parse(dt.Rows[i][1].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][1].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(scenario.Parameters, (float)de);
                            }
                            else
                            {
                                v.SetValue(scenario.Parameters, float.Parse(dt.Rows[i][1].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(scenario.Parameters, bool.Parse(dt.Rows[i][1].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(scenario.Parameters, dt.Rows[i][1].ToString());
                        }
                    }
                }
            }

            非getset字段表 = typeof(GameGlobal.GlobalVariables).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            dt = ds.Tables["GlobalVariables"];
            for (i = 1; i < dt.Rows.Count; i++)
            {
                if (i == 1)
                {
                    scenario.GlobalVariables = new GameGlobal.GlobalVariables();
                }
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (v.Name == dt.Rows[i][0].ToString())//如果字段的值=第一列中的文本，则
                    {
                        if (dt.Rows[i][1].ToString() == "真")//excel会默认把true和false大写，之后转换到datatable时，又会变成汉字真假
                        {
                            dt.Rows[i][1] = "True";
                        }
                        else if (dt.Rows[i][1].ToString() == "假")
                        {
                            dt.Rows[i][1] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][1].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(scenario.GlobalVariables, (int)de);
                            }
                            else
                            {
                                v.SetValue(scenario.GlobalVariables, int.Parse(dt.Rows[i][1].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][1].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(scenario.GlobalVariables, (float)de);
                            }
                            else
                            {
                                v.SetValue(scenario.GlobalVariables, float.Parse(dt.Rows[i][1].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(scenario.GlobalVariables, bool.Parse(dt.Rows[i][1].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(scenario.GlobalVariables, dt.Rows[i][1].ToString());
                        }
                        if (v.Name== "PersonNaturalDeath")
                        {
                            scenario.GlobalVariables.PersonNaturalDeath=bool.Parse( dt.Rows[i][1].ToString());
                        }
                    }
                }
            }

            非getset字段表 = typeof(Animation).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 动画取值字段();
            dt = ds.Tables["TileAnimation"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Animation a = new Animation();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllTileAnimations.AddAnimation(a);
            }


            非getset字段表 = typeof(TerrainDetail).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 地形取值字段();
            dt = ds.Tables["TerrainDetail"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                TerrainDetail a = new TerrainDetail();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllTerrainDetails.AddTerrainDetail(a);
            }

            取值字段 = 个性语言取值字段();
            dt = ds.Tables["TextMessages"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                TextMessageKind kind = (TextMessageKind)int.Parse(dt.Rows[i]["Kind"].ToString());
                List<string> list = new List<string>();
                StaticMethods.LoadFromString(list, dt.Rows[i]["Messages"].ToString());
                scenario.GameCommonData.AllTextMessages.AddTextMessages(int.Parse(dt.Rows[i]["Person"].ToString()), kind, list);
            }

            非getset字段表 = typeof(ArchitectureKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));


            取值字段 = 城池类型取值字段();
            dt = ds.Tables["ArchitectureKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                ArchitectureKind a = new ArchitectureKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllArchitectureKinds.AddArchitectureKind(a);
            }

            非getset字段表 = typeof(AttackDefaultKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 常规取值字段();
            dt = ds.Tables["AttackDefaultKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                AttackDefaultKind a = new AttackDefaultKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllAttackDefaultKinds.Add(a);
            }


            非getset字段表 = typeof(AttackTargetKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 常规取值字段();
            dt = ds.Tables["AttackTargetKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                AttackTargetKind a = new AttackTargetKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllAttackTargetKinds.Add(a);
            }

            非getset字段表 = typeof(BiographyAdjectives).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 简介类型取值字段();
            dt = ds.Tables["BiographyAdjectives"];
            scenario.GameCommonData.AllBiographyAdjectives = scenario.GameCommonData.AllBiographyAdjectives.OrderBy(bio => bio.ID).ToList();
            for (i = 0; i < dt.Rows.Count; i++)
            {
                BiographyAdjectives a = new BiographyAdjectives();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                StaticMethods.LoadFromString(a.SuffixText, dt.Rows[i]["SuffixText"].ToString());
                StaticMethods.LoadFromString(a.Text, dt.Rows[i]["Text"].ToString());
                scenario.GameCommonData.AllBiographyAdjectives.Add(a);
            }

            非getset字段表 = typeof(CastDefaultKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 常规取值字段();
            dt = ds.Tables["CastDefaultKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                CastDefaultKind a = new CastDefaultKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllCastDefaultKinds.Add(a);
            }

            非getset字段表 = typeof(CastTargetKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 常规取值字段();
            dt = ds.Tables["CastTargetKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                CastTargetKind a = new CastTargetKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllCastTargetKinds.Add(a);
            }

            非getset字段表 = typeof(CharacterKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 人物性格取值字段();
            dt = ds.Tables["CharacterKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                CharacterKind a = new CharacterKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (pi.Name == "GenerationChance")
                        {
                            char[] separator = new char[] { ' ', ',', '\r', '\t' };
                            string ss = dt.Rows[i][pi.Name].ToString();
                            string[] str = ss.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            a.GenerationChance = new int[str.Length];
                            for (int iii = 0; iii < str.Length; iii++)
                            {
                                a.GenerationChance[iii] = int.Parse(str[iii]);
                            }
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        else if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }

                    }
                }
                scenario.GameCommonData.AllCharacterKinds.Add(a);
            }

            非getset字段表 = typeof(Microsoft.Xna.Framework.Color).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 颜色取值字段();
            dt = ds.Tables["Color"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Microsoft.Xna.Framework.Color a = new Microsoft.Xna.Framework.Color();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (pi.Name == "R")
                        {
                            a.R = byte.Parse(dt.Rows[i][pi.Name].ToString());
                        }
                        else if (pi.Name == "B")
                        {
                            a.B = byte.Parse(dt.Rows[i][pi.Name].ToString());
                        }
                        else if (pi.Name == "G")
                        {
                            a.G = byte.Parse(dt.Rows[i][pi.Name].ToString());
                        }
                        else if (pi.Name == "A")
                        {
                            a.A = byte.Parse(dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllColors.Add(a);
            }

            非getset字段表 = typeof(CombatMethod).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));


            取值字段 = 战法取值字段();
            dt = ds.Tables["CombatMethod"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                CombatMethod a = new CombatMethod();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.AttackDefault = (AttackDefaultKind)scenario.GameCommonData.AllAttackDefaultKinds.GetGameObject(a.AttackDefaultString);
                a.AttackTarget = (AttackTargetKind)scenario.GameCommonData.AllAttackTargetKinds.GetGameObject(a.AttackTargetString);
                a.AnimationKind = (TileAnimationKind)int.Parse(dt.Rows[i]["AnimationKind"].ToString());
                scenario.GameCommonData.AllCombatMethods.AddCombatMethod(a);
            }

            非getset字段表 = typeof(ConditionKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 常规取值字段();
            dt = ds.Tables["ConditionKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                ConditionKind a = ConditionKindFactory.CreateConditionKindByID(int.Parse(dt.Rows[i]["ID"].ToString()));
                if (a != null)
                {
                    a.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    a.Name = dt.Rows[i]["Name"].ToString();
                }
                scenario.GameCommonData.AllConditionKinds.ConditionKinds.Add(a.ID, a);
                scenario.GameCommonData.AllConditionKinds.AddConditionKind(a);
            }

            非getset字段表 = typeof(Condition).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 条件事件效果取值字段();
            dt = ds.Tables["Condition"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Condition a = new Condition();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.Name == "Kind")
                        {
                            a.Kind = scenario.GameCommonData.AllConditionKinds.GetConditionKind(int.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllConditions.AddCondition(a);
            }


            非getset字段表 = typeof(zainanzhongleilei).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 灾难类型取值字段();
            dt = ds.Tables["DisasterKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                zainanzhongleilei a = new zainanzhongleilei();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.suoyouzainanzhonglei.Addzainanzhonglei(a);
            }

            非getset字段表 = typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffectKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 常规取值字段();
            dt = ds.Tables["EventEffectKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                GameObjects.ArchitectureDetail.EventEffect.EventEffectKind a = GameObjects.ArchitectureDetail.EventEffect.EventEffectKindFactory.CreateEventEffectKindByID(int.Parse(dt.Rows[i]["ID"].ToString()));
                if (a != null)
                {
                    a.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    a.Name = dt.Rows[i]["Name"].ToString();
                }
                scenario.GameCommonData.AllEventEffectKinds.AddEventEffectKind(a);
            }

            非getset字段表 = typeof(GameObjects.ArchitectureDetail.EventEffect.EventEffect).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 条件事件效果取值字段();
            dt = ds.Tables["EventEffect"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                GameObjects.ArchitectureDetail.EventEffect.EventEffect a = new GameObjects.ArchitectureDetail.EventEffect.EventEffect();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (v.Name == "Kind")
                        {
                            a.Kind = scenario.GameCommonData.AllEventEffectKinds.GetEventEffectKind(int.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllEventEffects.AddEventEffect(a);
            }

            非getset字段表 = typeof(FacilityKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 设施类型取值字段();
            dt = ds.Tables["FacilityKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                FacilityKind a = new FacilityKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name) && pi.Name != "Description")//简介description是只读的，所以不能赋值
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllFacilityKinds.AddFacilityKind(a);
            }

            非getset字段表 = typeof(guanjuezhongleilei).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 官爵类型取值字段();
            dt = ds.Tables["guanjuezhonglei"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                guanjuezhongleilei a = new guanjuezhongleilei();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.suoyouguanjuezhonglei.Addguanjuedezhonglei(a);
            }

            非getset字段表 = typeof(IdealTendencyKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 出仕考虑类型取值字段();
            dt = ds.Tables["IdealTendencyKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                IdealTendencyKind a = new IdealTendencyKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllIdealTendencyKinds.Add(a);
            }

            非getset字段表 = typeof(InfluenceKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 影响类型取值字段();
            dt = ds.Tables["InfluenceKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                InfluenceKind a = InfluenceKindFactory.CreateInfluenceKindByID(int.Parse(dt.Rows[i]["ID"].ToString()));
                if (a != null)
                {
                    a.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    a.Name = dt.Rows[i]["Name"].ToString();
                    a.Type = (InfluenceType)int.Parse(dt.Rows[i]["Type"].ToString());
                    a.AIPersonValue = float.Parse(dt.Rows[i]["AIPersonValue"].ToString());
                    a.AIPersonValuePow = float.Parse(dt.Rows[i]["AIPersonValuePow"].ToString());
                }
                scenario.GameCommonData.AllInfluenceKinds.AddInfluenceKind(a);
            }

            非getset字段表 = typeof(Influence).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 影响取值字段();
            dt = ds.Tables["Influence"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Influence a = new Influence();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.Name == "Kind")
                        {
                            a.Kind = scenario.GameCommonData.AllInfluenceKinds.GetInfluenceKind(int.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllInfluences.AddInfluence(a);
            }

            非getset字段表 = typeof(InformationKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 情报类型取值字段();
            dt = ds.Tables["InformationKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                InformationKind a = new InformationKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.Level = (InformationLevel)int.Parse(dt.Rows[i]["Level"].ToString());
                scenario.GameCommonData.AllInformationKinds.Add(a);
            }


            非getset字段表 = typeof(MilitaryKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 编队类型取值字段();
            dt = ds.Tables["MilitaryKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                MilitaryKind a = new MilitaryKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.Name == "LevelUpKindID")
                        {
                            GameGlobal.StaticMethods.LoadFromString(a.LevelUpKindID, dt.Rows[i][v.Name].ToString());
                        }

                        else if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                GameGlobal.StaticMethods.LoadFromString(a.LevelUpKindID, dt.Rows[i]["LevelUpKindID"].ToString());
                a.AttackDefaultKind = (TroopAttackDefaultKind)int.Parse(dt.Rows[i]["AttackDefaultKind"].ToString());
                a.AttackTargetKind = (TroopAttackTargetKind)int.Parse(dt.Rows[i]["AttackTargetKind"].ToString());
                a.CastDefaultKind = (TroopCastDefaultKind)int.Parse(dt.Rows[i]["CastDefaultKind"].ToString());
                a.CastTargetKind = (TroopCastTargetKind)int.Parse(dt.Rows[i]["CastTargetKind"].ToString());
                a.Type = (MilitaryType)int.Parse(dt.Rows[i]["Type"].ToString());
                scenario.GameCommonData.AllMilitaryKinds.AddMilitaryKind(a);
            }

            非getset字段表 = typeof(PersonGeneratorSetting).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 生成人物取值字段();
            dt = ds.Tables["PersonGenerator"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                PersonGeneratorSetting a = new PersonGeneratorSetting();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.PersonGeneratorSetting = a;
            }

            非getset字段表 = typeof(PersonGeneratorType).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 生成人物类型取值字段();
            dt = ds.Tables["PersonGeneratorType"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                PersonGeneratorType a = new PersonGeneratorType();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllPersonGeneratorTypes.Add(a);
            }

            非getset字段表 = typeof(SectionAIDetail).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 军区战略取值字段();
            dt = ds.Tables["SectionAIDetail"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                SectionAIDetail a = new SectionAIDetail();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.OrientationKind = (SectionOrientationKind)int.Parse(dt.Rows[i]["OrientationKind"].ToString());
                scenario.GameCommonData.AllSectionAIDetails.AddSectionAIDetail(a);
            }

            非getset字段表 = typeof(Skill).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 技能取值字段();
            dt = ds.Tables["Skill"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Skill a = new Skill();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (pi.Name == "GenerationChance")
                    {
                        char[] separator = new char[] { ' ', ',', '\r', '\t' };
                        string ss = dt.Rows[i][pi.Name].ToString();
                        string[] str = ss.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        a.GenerationChance = new int[str.Length];
                        for (int iii = 0; iii < str.Length; iii++)
                        {
                            a.GenerationChance[iii] = int.Parse(str[iii]);
                        }
                    }
                    else if (取值字段.Contains(pi.Name) && pi.Name != "Description" && pi.Name != "Prerequisite")
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllSkills.AddSkill(a);
            }

            非getset字段表 = typeof(Stratagem).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 计略取值字段();
            dt = ds.Tables["Stratagem"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Stratagem a = new Stratagem();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.AnimationKind = (TileAnimationKind)int.Parse(dt.Rows[i]["AnimationKind"].ToString());
                scenario.GameCommonData.AllStratagems.AddStratagem(a);
            }

            非getset字段表 = typeof(Stunt).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 特技取值字段();
            dt = ds.Tables["Stunt"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Stunt a = new Stunt();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (pi.Name == "GenerationChance")
                        {
                            char[] separator = new char[] { ' ', ',', '\r', '\t' };
                            string ss = dt.Rows[i][pi.Name].ToString();
                            string[] str = ss.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            a.GenerationChance = new int[str.Length];
                            for (int iii = 0; iii < str.Length; iii++)
                            {
                                a.GenerationChance[iii] = int.Parse(str[iii]);
                            }
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllStunts.AddStunt(a);
            }

            非getset字段表 = typeof(Technique).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 科技取值字段();
            dt = ds.Tables["Technique"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Technique a = new Technique();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllTechniques.AddTechnique(a);
            }

            非getset字段表 = typeof(TitleKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 称号类型取值字段();
            dt = ds.Tables["TitleKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                TitleKind a = new TitleKind();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllTitleKinds.AddTitleKind(a);
            }


            非getset字段表 = typeof(Title).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 称号取值字段();
            dt = ds.Tables["Title"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Title a = new Title();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (v.Name == "Kind")
                        {
                            a.Kind = scenario.GameCommonData.AllTitleKinds.GetTitleKind(int.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name) && pi.Name != "Description" && pi.Name != "Prerequisite")
                    {
                        if (pi.Name == "GenerationChance")
                        {
                            char[] separator = new char[] { ' ', ',', '\r', '\t' };
                            string ss = dt.Rows[i][pi.Name].ToString();
                            string[] str = ss.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                            a.GenerationChance = new int[str.Length];
                            for (int iii = 0; iii < str.Length; iii++)
                            {
                                a.GenerationChance[iii] = int.Parse(str[iii]);
                            }
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.Kind = scenario.GameCommonData.AllTitleKinds.GetTitleKind(int.Parse(dt.Rows[i]["Kind"].ToString()));
                scenario.GameCommonData.AllTitles.AddTitle(a);
            }

            非getset字段表 = typeof(TrainPolicy).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 培育方针取值字段();
            dt = ds.Tables["TrainPolicy"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                TrainPolicy a = new TrainPolicy();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllTrainPolicies.Add(a);
            }

            非getset字段表 = typeof(Animation).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 部队动画取值字段();
            dt = ds.Tables["TroopAnimation"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Animation a = new Animation();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    //if (取值字段.Contains(v.Name) &&v.Name != "TextureHeight" && v.Name !="")
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllTroopAnimations.AddAnimation(a);
            }

            非getset字段表 = typeof(GameObjects.TroopDetail.EventEffect.EventEffectKind).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 常规取值字段();
            dt = ds.Tables["TroopEventEffectKind"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                GameObjects.TroopDetail.EventEffect.EventEffectKind a = GameObjects.TroopDetail.EventEffect.EventEffectKindFactory.CreateEventEffectKindByID(int.Parse(dt.Rows[i]["ID"].ToString()));
                if (a != null)
                {
                    a.ID = int.Parse(dt.Rows[i]["ID"].ToString());
                    a.Name = dt.Rows[i]["Name"].ToString();
                }
                scenario.GameCommonData.AllTroopEventEffectKinds.AddEventEffectKind(a);
            }

            非getset字段表 = typeof(GameObjects.TroopDetail.EventEffect.EventEffect).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 部队事件效果取值字段();
            dt = ds.Tables["TroopEventEffect"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                GameObjects.TroopDetail.EventEffect.EventEffect a = new GameObjects.TroopDetail.EventEffect.EventEffect();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (v.Name == "Kind")
                        {
                            a.Kind = scenario.GameCommonData.AllTroopEventEffectKinds.GetEventEffectKind(int.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.GameCommonData.AllTroopEventEffects.AddEventEffect(a);
            }


            非getset字段表 = typeof(Map).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 地图取值字段();
            dt = ds.Tables["Map"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Map a = scenario.ScenarioMap;
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name) && pi.Name != "JumpPosition")
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.MapDimensions = LoadFromString(a.MapDimensions, dt.Rows[i]["MapDimensions"].ToString());
                a.JumpPosition = LoadFromString(a.JumpPosition, dt.Rows[i]["JumpPosition"].ToString());
                scenario.ScenarioTitle = dt.Rows[i]["ScenarioTitle"].ToString();
                LoadFromString(scenario.Date, dt.Rows[i]["Date"].ToString());
                scenario.DaySince = int.Parse(dt.Rows[i]["DaySince"].ToString());
                scenario.GameTime = int.Parse(dt.Rows[i]["GameTime"].ToString());
                scenario.CurrentPlayerID = dt.Rows[i]["CurrentPlayerID"].ToString();
                scenario.PlayerInfo = dt.Rows[i]["PlayerInfo"].ToString();
                scenario.FireTable.Positions = new HashSet<Microsoft.Xna.Framework.Point>();
                scenario.FireTable.LoadFromString(dt.Rows[i]["FirePoint"].ToString());
                scenario.NoFoodDictionary.LoadFromString(dt.Rows[i]["NoFoodDictionaryPoint"].ToString());
                scenario.PlayerList = new List<int>();
                GameGlobal.StaticMethods.LoadFromString(scenario.PlayerList, dt.Rows[i]["PlayerList"].ToString());
                scenario.Factions.FactionQueue = dt.Rows[i]["FactionQueue"].ToString();
                scenario.ScenarioDescription = dt.Rows[i]["ScenarioDescription"].ToString();
                scenario.ScenarioTitle = dt.Rows[i]["ScenarioTitle"].ToString();
                a.MapDataString = File.ReadAllText(Path.GetDirectoryName(已导入文件.Items[i].ToString()) + @"\" + Path.GetFileNameWithoutExtension(已导入文件.Items[k].ToString()) + "地形信息.txt");
               // a.MapDataString = File.ReadAllText(@"F:\安装程序\各种资源与修改\ZHSan\json and excel\bin\Debug\Save03Map.txt");//读取同目录同名txt地图数据
                a.LoadMapData(a.MapDataString, a.MapDimensions.X, a.MapDimensions.Y);
                a.TileWidth = 50;
                a.TileHeight = 50;
            }
            非getset字段表 = typeof(State).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 州域取值字段();
            dt = ds.Tables["State"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                State a = new State();
                a.Init();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.States.Add(a);
            }

            非getset字段表 = typeof(GameObjects.ArchitectureDetail.Region).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 地域取值字段();
            dt = ds.Tables["Region"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                GameObjects.ArchitectureDetail.Region a = new GameObjects.ArchitectureDetail.Region();
                a.Init();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.Regions.Add(a);
            }

            非getset字段表 = typeof(Facility).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 设施取值字段();
            dt = ds.Tables["Facility"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Facility a = new Facility();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.Facilities.Add(a);
            }


            非getset字段表 = typeof(Person).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 人物取值字段();
            dt = ds.Tables["Person"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Person a = new Person();
                a.Init();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name) && pi.Name != "Loyalty")
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.DeadReason = (PersonDeadReason)Enum.Parse(typeof(PersonDeadReason), dt.Rows[i]["DeadReason"].ToString());
                a.Qualification = (PersonQualification)Enum.Parse(typeof(PersonQualification), dt.Rows[i]["Qualification"].ToString());
                a.ValuationOnGovernment = (PersonValuationOnGovernment)Enum.Parse(typeof(PersonValuationOnGovernment), dt.Rows[i]["ValuationOnGovernment"].ToString());
                a.StrategyTendency = (PersonStrategyTendency)Enum.Parse(typeof(PersonStrategyTendency), dt.Rows[i]["StrategyTendency"].ToString());
                a.WorkKind = (ArchitectureWorkKind)Enum.Parse(typeof(ArchitectureWorkKind), dt.Rows[i]["WorkKind"].ToString());
                a.OldWorkKind = (ArchitectureWorkKind)Enum.Parse(typeof(ArchitectureWorkKind), dt.Rows[i]["OldWorkKind"].ToString());
                a.firstPreferred = (ArchitectureWorkKind)Enum.Parse(typeof(ArchitectureWorkKind), dt.Rows[i]["firstPreferred"].ToString());
                a.OutsideTask = (OutsideTaskKind)Enum.Parse(typeof(OutsideTaskKind), dt.Rows[i]["OutsideTask"].ToString());
                a.LastOutsideTask = (OutsideTaskKind)Enum.Parse(typeof(OutsideTaskKind), dt.Rows[i]["LastOutsideTask"].ToString());
                a.BornRegion = (PersonBornRegion)Enum.Parse(typeof(PersonBornRegion), dt.Rows[i]["BornRegion"].ToString());
                Microsoft.Xna.Framework.Point p = new Microsoft.Xna.Framework.Point();
                if (dt.Rows[i]["OutsideDestination"].ToString() != "")
                {
                    a.OutsideDestination = LoadFromString(p, dt.Rows[i]["OutsideDestination"].ToString());
                }
                GameGlobal.StaticMethods.LoadFromString(a.JoinFactionID, dt.Rows[i]["JoinFactionID"].ToString());
                GameGlobal.StaticMethods.LoadFromString(a.ProhibitedFactionID, dt.Rows[i]["ProhibitedFactionID"].ToString());
                scenario.Persons.Add(a);
                scenario.FatherIds.Add(a.ID, int.Parse(dt.Rows[i]["father"].ToString()));
                scenario.MotherIds.Add(a.ID, int.Parse(dt.Rows[i]["mother"].ToString()));
                scenario.SpouseIds.Add(a.ID, int.Parse(dt.Rows[i]["spouse"].ToString()));
                scenario.MarriageGranterId.Add(a.ID, int.Parse(dt.Rows[i]["marriageGranter"].ToString()));
                int[] aaa = new int[] { };
                aaa = LoadFromString(aaa, dt.Rows[i]["brothers"].ToString());
                scenario.BrotherIds.Add(a.ID, aaa);
                aaa = LoadFromString(aaa, dt.Rows[i]["ClosePersons"].ToString());
                scenario.CloseIds.Add(a.ID, aaa);
                aaa = LoadFromString(aaa, dt.Rows[i]["HatedPersons"].ToString());
                scenario.HatedIds.Add(a.ID, aaa);
                aaa = LoadFromString(aaa, dt.Rows[i]["suoshurenwuList"].ToString());
                scenario.SuoshuIds.Add(a.ID, aaa);
                if (dt.Rows[i]["relations"].ToString() != "")
                {
                    char[] separator = new char[] { ' ', ',', '\r', '\t' };
                    string[] str = dt.Rows[i]["relations"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (int ii = 0; ii < str.Length; ii += 2)
                    {
                        PersonIDRelation pr = new PersonIDRelation();
                        pr.PersonID1 = a.ID;
                        pr.PersonID2 = int.Parse(str[ii]);
                        pr.Relation = int.Parse(str[ii + 1]);
                        scenario.PersonRelationIds.Add(pr);
                    }
                }
            }


            非getset字段表 = typeof(Biography).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 简介取值字段();
            dt = ds.Tables["Biography"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Biography a = new Biography();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {

                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.Name == "InGame")
                        {
                            a.InGame = dt.Rows[i][v.Name].ToString().Replace(" ", "\n");
                        }
                        else if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.Name == "InGame")
                        {
                            a.InGame = dt.Rows[i][pi.Name].ToString().Replace(" ", "\n");
                        }
                        else if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else if (dt.Rows[i][pi.Name].ToString() != "")
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.AllBiographies.AddBiography(a);
            }


            非getset字段表 = typeof(Captive).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 俘虏取值字段();
            dt = ds.Tables["Captive"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Captive a = new Captive();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.CaptivePerson = (Person)scenario.Persons.GetGameObject(a.CaptivePersonID);
                if (a.CaptivePerson != null)
                {
                    a.CaptivePerson.SetBelongedCaptive(a, PersonStatus.Captive);
                }
                a.CaptivePerson.Status = PersonStatus.Captive;
                if (!scenario.isInCaptiveList(a.CaptivePersonID))
                {
                    scenario.Captives.AddCaptiveWithEvent(a);
                }
                scenario.Captives.Add(a);
            }


            非getset字段表 = typeof(Military).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 编队取值字段();
            dt = ds.Tables["Military"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Military a = new Military();
                a.Init();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.ShelledMilitaryID = -1;//
                scenario.Militaries.AddMilitary(a);
            }


            非getset字段表 = typeof(Legion).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 军团取值字段();
            dt = ds.Tables["Legion"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Legion a = new Legion();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                if (dt.Rows[i]["InformationDestination"].ToString() == "")
                {
                    a.InformationDestination = null;
                }
                //a.PreferredRoutewayString;
                a.Kind = (LegionKind)Enum.Parse(typeof(LegionKind), dt.Rows[i]["Kind"].ToString());
                if(dt.Rows[i]["TakenPositions"].ToString() !="")
                {
                    a.TakenPositions= LoadFromString(a.TakenPositions, dt.Rows[i]["TakenPositions"].ToString());
                }
                Microsoft.Xna.Framework.Point p = new Microsoft.Xna.Framework.Point();
                if(dt.Rows[i]["InformationDestination"].ToString() !="")
                {
                    a.InformationDestination = LoadFromString(p, dt.Rows[i]["InformationDestination"].ToString());
                }


                a.SupplyingRouteways = new List<SupplyingRoutewayPack>(); ;//先故意留空

                scenario.Legions.Add(a);
            }


            非getset字段表 = typeof(Architecture).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 城池取值字段();
            dt = ds.Tables["Architecture"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Architecture a = new Architecture();
                List<string> e = new List<string>();

                a.Init();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                if (a.DefensiveLegionID != -1)
                {
                    a.DefensiveLegion = scenario.Legions.GetGameObject(a.DefensiveLegionID) as Legion;
                }
                a.jianzhuqizi = new qizi();
                a.jianzhuqizi.qizipoint = LoadFromString(a.jianzhuqizi.qizipoint, dt.Rows[i]["jianzhuqizi"].ToString());
                a.ArchitectureArea = new GameArea();
                a.LoadFromString(a.ArchitectureArea, a.ArchitectureAreaString);
                a.ConvinceDestinationPersonList.LoadFromString(scenario.Persons, dt.Rows[i]["ConvinceDestinationPersonList"].ToString());
                a.LoadMilitariesFromString(scenario.Militaries, a.MilitariesString);
                if (a.youzainan)
                {
                    string ss = dt.Rows[i]["zainan"].ToString();
                    string[] ssss = LoadFromString(ss);
                    a.zainan.ID = int.Parse(ssss[0]);
                    a.zainan.Name = ssss[1];
                    a.zainan.shengyutianshu = int.Parse(ssss[2]);
                    a.zainan.zainanleixing = int.Parse(ssss[3]);
                }
                if (dt.Rows[i]["captiveLoyaltyFall"].ToString() != "")
                {
                    string ss = dt.Rows[i]["captiveLoyaltyFall"].ToString();
                    string[] ssss = LoadFromString(ss);
                    for (int kk = 0; kk < ssss.Length - 1; kk++)
                    {
                        a.captiveLoyaltyFall.Add(new KeyValuePair<int, int>(int.Parse(ssss[kk]), int.Parse(ssss[kk + 1])));
                        kk++;
                    }
                }
                scenario.AiBattlingArchitectureStrings.Add(a.ID, new int[] { });
                scenario.Architectures.Add(a);
            }


            非getset字段表 = typeof(Event).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 事件取值字段();
            dt = ds.Tables["Event"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Event a = new Event();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.LoadDialogFromString(dt.Rows[i]["dialog"].ToString());
                a.LoadnoDialogFromString(dt.Rows[i]["noDialog"].ToString());
                a.LoadyesDialogFromString(dt.Rows[i]["yesdialog"].ToString());
                a.LoadScenBiographyFromString(dt.Rows[i]["scenBiography"].ToString());
                scenario.AllEvents.Add(a);
            }

            非getset字段表 = typeof(YearTable).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 年表取值字段();
            dt = ds.Tables["YearTable"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                int id = int.Parse(dt.Rows[i]["ID"].ToString());
                GameDate date = new GameDate();
                LoadFromString(date, dt.Rows[i]["Date"].ToString());
                YearTableEntry a = new YearTableEntry(id, date, null, dt.Rows[i]["Content"].ToString(), bool.Parse(dt.Rows[i]["IsGloballyKnown"].ToString()));
                a.FactionsString = dt.Rows[i]["FactionsString"].ToString();
                scenario.YearTable.AddTableEntry(a);
            }

            非getset字段表 = typeof(TroopEvent).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 部队事件取值字段();
            dt = ds.Tables["TroopEvent"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                TroopEvent a = new TroopEvent();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }

                char[] separator = new char[] { ' ', '{', '}', '\r', '\t' };
                string ss = dt.Rows[i]["Dialogs"].ToString();
                string[] str = ss.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                for (int iii = 0; iii < str.Length; iii += 2)
                {
                    PersonDialog dia = new PersonDialog();
                    dia.SpeakingPersonID = int.Parse(str[iii]);
                    dia.Text = str[iii + 1];
                    a.Dialogs.Add(dia);
                }
                scenario.TroopEvents.Add(a);
            }


            非getset字段表 = typeof(Treasure).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 宝物取值字段();
            dt = ds.Tables["Treasure"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Treasure a = new Treasure();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.Treasures.AddTreasure(a);
            }

            非getset字段表 = typeof(Troop).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 部队取值字段();
            dt = ds.Tables["Troop"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Troop a = new Troop();
                // a.Init();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name) && pi.Name != "DrawAnimation")
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                GameManager.Session.Current.IsWorking = true;
                a.DrawAnimation = true;
                a.Position = LoadFromString(a.Position, dt.Rows[i]["Position"].ToString());
                a.Destination = LoadFromString(a.Destination, dt.Rows[i]["Destination"].ToString());
                a.RealDestination = LoadFromString(a.RealDestination, dt.Rows[i]["RealDestination"].ToString());
                a.SelfCastPosition = LoadFromString(a.SelfCastPosition, dt.Rows[i]["SelfCastPosition"].ToString());
                a.minglingweizhi = LoadFromString(a.minglingweizhi, dt.Rows[i]["minglingweizhi"].ToString());
                a.OrientationPosition = LoadFromString(a.OrientationPosition, dt.Rows[i]["OrientationPosition"].ToString());
                a.PreviousPosition = LoadFromString(a.PreviousPosition, dt.Rows[i]["PreviousPosition"].ToString());
                if (dt.Rows[i]["FirstTierPath"].ToString() != "")
                {
                    a.FirstTierPath= LoadFromString(a.FirstTierPath, dt.Rows[i]["FirstTierPath"].ToString());
                }
                if (dt.Rows[i]["SecondTierPath"].ToString() != "")
                {
                    a.SecondTierPath= LoadFromString(a.SecondTierPath, dt.Rows[i]["SecondTierPath"].ToString());
                }
                if (dt.Rows[i]["ThirdTierPath"].ToString() != "")
                {
                    a.ThirdTierPath= LoadFromString(a.ThirdTierPath, dt.Rows[i]["ThirdTierPath"].ToString());
                }
                if (dt.Rows[i]["ThirdTierPath"].ToString() != "")
                {
                    a.MoveAnimationFrames= LoadFromString(a.MoveAnimationFrames, dt.Rows[i]["MoveAnimationFrames"].ToString());
                }
                a.AttackDefaultKind = (TroopAttackDefaultKind)Enum.Parse(typeof(TroopAttackDefaultKind), dt.Rows[i]["AttackDefaultKind"].ToString());
                a.AttackTargetKind = (TroopAttackTargetKind)Enum.Parse(typeof(TroopAttackTargetKind), dt.Rows[i]["AttackTargetKind"].ToString());
                a.CastDefaultKind = (TroopCastDefaultKind)Enum.Parse(typeof(TroopCastDefaultKind), dt.Rows[i]["CastDefaultKind"].ToString());
                a.CastTargetKind = (TroopCastTargetKind)Enum.Parse(typeof(TroopCastTargetKind), dt.Rows[i]["CastTargetKind"].ToString());
                a.CurrentTileAnimationKind = (TileAnimationKind)Enum.Parse(typeof(TileAnimationKind), dt.Rows[i]["CurrentTileAnimationKind"].ToString());
                a.Effect = (TroopEffect)Enum.Parse(typeof(TroopEffect), dt.Rows[i]["Effect"].ToString());
                a.FriendlyAction = (FriendlyActionKind)Enum.Parse(typeof(FriendlyActionKind), dt.Rows[i]["FriendlyAction"].ToString());
                a.HostileAction = (HostileActionKind)Enum.Parse(typeof(HostileActionKind), dt.Rows[i]["HostileAction"].ToString());
                a.PreAction = (TroopPreAction)Enum.Parse(typeof(TroopPreAction), dt.Rows[i]["PreAction"].ToString());
                a.TroopStatus = (TroopStatus)Enum.Parse(typeof(TroopStatus), dt.Rows[i]["TroopStatus"].ToString());
                if (dt.Rows[i]["Stunts"].ToString() != "")
                {
                    char[] separator = new char[] { ' ', ',', '\r', '\t' };
                    string[] str = dt.Rows[i]["Stunts"].ToString().Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    for (int ii = 0; ii < str.Length; ii++)
                    {
                        Stunt s = scenario.GameCommonData.AllStunts.GetStunt(int.Parse(str[ii]));
                        a.Stunts.AddStunt(s);
                    }
                }
                Military m = scenario.Militaries.GetGameObject(a.MilitaryID) as Military;
                if (m != null)
                {
                    a.Morale = m.Morale;
                    a.Quantity = m.Quantity;
                }
                scenario.Troops.Add(a);
            }
            GameManager.Session.Current.IsWorking = false;

            非getset字段表 = typeof(Information).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 情报取值字段();
            dt = ds.Tables["Information"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Information a = new Information();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                a.Position = LoadFromString(a.Position, dt.Rows[i]["Position"].ToString());
                a.Level = (InformationLevel)int.Parse(dt.Rows[i]["Level"].ToString());
                scenario.Informations.AddInformation(a);
            }
            scenario.Routeways = new RoutewayList();
            /*  粮道保存有问题，暂时跳过
                        非getset字段表 = typeof(Routeway).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
                        取值字段 = 粮道取值字段();
                        dt = ds.Tables["Routeway"];
                        for (i = 0; i < dt.Rows.Count; i++)
                        {
                            Routeway a = new Routeway();
                            if (i == 0)
                            {
                                getset字段表 = ((Type)a.GetType()).GetProperties();
                            }
                            //非getset字段的处理与赋值
                            foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                            {
                                if (取值字段.Contains(v.Name))
                                {
                                    if (dt.Rows[i][v.Name].ToString() == "真")
                                    {
                                        dt.Rows[i][v.Name] = "True";
                                    }
                                    else if (dt.Rows[i][v.Name].ToString() == "假")
                                    {
                                        dt.Rows[i][v.Name] = "False";
                                    }
                                    if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                                    {
                                        string temp = dt.Rows[i][v.Name].ToString();
                                        if (temp.Contains("E"))//科学计数法转换
                                        {
                                            Decimal de;
                                            Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                            v.SetValue(a, (int)de);
                                        }
                                        else
                                        {
                                            v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                                        }
                                    }
                                    else if (v.FieldType.ToString() == "System.Single")
                                    {
                                        string temp = dt.Rows[i][v.Name].ToString();
                                        if (temp.Contains("E"))//科学计数法转换
                                        {
                                            Decimal de;
                                            Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                            v.SetValue(a, (float)de);
                                        }
                                        else
                                        {
                                            v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                                        }
                                    }
                                    else if (v.FieldType.ToString() == "System.Boolean")
                                    {
                                        v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                                    }
                                    else if (v.FieldType.ToString() == "System.String")
                                    {
                                        v.SetValue(a, dt.Rows[i][v.Name].ToString());
                                    }
                                }

                            }
                            //getset字段的处理与赋值
                            foreach (System.Reflection.PropertyInfo pi in getset字段表)
                            {
                                if (取值字段.Contains(pi.Name))
                                {
                                    if (dt.Rows[i][pi.Name].ToString() == "真")
                                    {
                                        dt.Rows[i][pi.Name] = "True";
                                    }
                                    else if (dt.Rows[i][pi.Name].ToString() == "假")
                                    {
                                        dt.Rows[i][pi.Name] = "False";
                                    }
                                    if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                                    {
                                        string temp = dt.Rows[i][pi.Name].ToString();
                                        if (temp.Contains("E"))//科学计数法转换
                                        {
                                            Decimal de;
                                            Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                            pi.SetValue(a, (int)de);
                                        }
                                        else
                                        {
                                            pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                                        }
                                    }
                                    else if (pi.PropertyType.ToString() == "System.Single")
                                    {
                                        string temp = dt.Rows[i][pi.Name].ToString();
                                        if (temp.Contains("E"))//科学计数法转换
                                        {
                                            Decimal de;
                                            Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                            pi.SetValue(a, (float)de);
                                        }
                                        else
                                        {
                                            pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                                        }
                                    }
                                    else if (pi.PropertyType.ToString() == "System.Boolean")
                                    {
                                        pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                                    }
                                    else if (pi.PropertyType.ToString() == "System.String")
                                    {
                                        pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                                    }
                                }
                            }
                            a.Position = LoadFromString(a.Position, dt.Rows[i]["Position"].ToString());
                            a.Level = (InformationLevel)int.Parse(dt.Rows[i]["AttackDefaultKind"].ToString());
                            scenario.Troops.Add(a);
                        }*/

            非getset字段表 = typeof(Section).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 军区取值字段();
            dt = ds.Tables["Sections"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Section a = new Section();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.Sections.Add(a);
            }


            非getset字段表 = typeof(DiplomaticRelation).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 势力关系取值字段();
            dt = ds.Tables["DiplomaticRelation"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                DiplomaticRelation a = new DiplomaticRelation();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
                scenario.DiplomaticRelations.AddDiplomaticRelation(a);
            }


            非getset字段表 = typeof(Faction).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            取值字段 = 势力取值字段();
            dt = ds.Tables["Faction"];
            for (i = 0; i < dt.Rows.Count; i++)
            {
                Faction a = new Faction();
                if (i == 0)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                //非getset字段的处理与赋值
                foreach (var v in 非getset字段表)//foreach映射表中的所有字段
                {
                    if (取值字段.Contains(v.Name))
                    {
                        if (dt.Rows[i][v.Name].ToString() == "真")
                        {
                            dt.Rows[i][v.Name] = "True";
                        }
                        else if (dt.Rows[i][v.Name].ToString() == "假")
                        {
                            dt.Rows[i][v.Name] = "False";
                        }
                        if (v.FieldType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (int)de);
                            }
                            else
                            {
                                v.SetValue(a, int.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][v.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                v.SetValue(a, (float)de);
                            }
                            else
                            {
                                v.SetValue(a, float.Parse(dt.Rows[i][v.Name].ToString()));
                            }
                        }
                        else if (v.FieldType.ToString() == "System.Boolean")
                        {
                            v.SetValue(a, bool.Parse(dt.Rows[i][v.Name].ToString()));
                        }
                        else if (v.FieldType.ToString() == "System.String")
                        {
                            v.SetValue(a, dt.Rows[i][v.Name].ToString());
                        }
                    }

                }
                //getset字段的处理与赋值
                foreach (System.Reflection.PropertyInfo pi in getset字段表)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        if (dt.Rows[i][pi.Name].ToString() == "真")
                        {
                            dt.Rows[i][pi.Name] = "True";
                        }
                        else if (dt.Rows[i][pi.Name].ToString() == "假")
                        {
                            dt.Rows[i][pi.Name] = "False";
                        }
                        if (pi.PropertyType.ToString() == "System.Int32")//尝试把文本转为int，如无异常则为scenario中的parameters的与v的name相同的对象与实例赋值，注意，科学计数法有问题，会跳出
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (int)de);
                            }
                            else
                            {
                                pi.SetValue(a, int.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Single")
                        {
                            string temp = dt.Rows[i][pi.Name].ToString();
                            if (temp.Contains("E"))//科学计数法转换
                            {
                                Decimal de;
                                Decimal.TryParse(temp, System.Globalization.NumberStyles.Any, null, out de);
                                pi.SetValue(a, (float)de);
                            }
                            else
                            {
                                pi.SetValue(a, float.Parse(dt.Rows[i][pi.Name].ToString()));
                            }
                        }
                        else if (pi.PropertyType.ToString() == "System.Boolean")
                        {
                            pi.SetValue(a, bool.Parse(dt.Rows[i][pi.Name].ToString()));
                        }
                        else if (pi.PropertyType.ToString() == "System.String")
                        {
                            pi.SetValue(a, dt.Rows[i][pi.Name].ToString());
                        }
                    }
                }
               // a.PreferredTechniqueKinds
                a.ClosedRouteways = new Dictionary<Microsoft.Xna.Framework.Point, object>();
                a.SpyMessageCloseList = new Dictionary<Microsoft.Xna.Framework.Point, object>();
               /* a.PrinceID =int.Parse( dt.Rows[i]["PrinceID"].ToString());
                if (a.PrinceID !=-1)
                {
                    a.Prince = (Person)scenario.Persons.GetGameObject(a.PrinceID);
                }
                a.PrinceID = int.Parse(dt.Rows[i]["PrinceID"].ToString());*/
                if (dt.Rows[i]["PreferredTechniqueKinds"].ToString() != "")
                {
                    a.PreferredTechniqueKinds= LoadFromString(a.PreferredTechniqueKinds, dt.Rows[i]["PreferredTechniqueKinds"].ToString());
                }
                ids = ids + a.ID + ",";
                names = names + a.Name + ",";
                scenario.Factions.Add(a);
            }

            //准备把scenario保存成json文件
            // scenario.GameCommonData = null;
            scenario.captivestocaptiveData(scenario.Captives);
            GameManager.Setting.Current = new GameManager.Setting();
            GameManager.Setting.Current.GlobalVariables = scenario.GlobalVariables;
            GameManager.Session.Current.Scenario = scenario;
            if(this.单独保存commdata信息.Checked)
            {
                GameScenario.SaveGameCommonData(scenario);
                string ss1 = "";
                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(CommonData));
                using (MemoryStream stream = new MemoryStream())
                {
                    //lock (Platform.SerializerLock)
                    {
                        serializer.WriteObject(stream, scenario.GameCommonData);
                    }
                    var array = stream.ToArray();
                    ss1 = Encoding.UTF8.GetString(array, 0, array.Length);
                }
                    ss1 = ss1.Replace("{\"", "{\r\n\"");
                ss1 = ss1.Replace("[{", "[\r\n{");
                ss1 = ss1.Replace(",\"", ",\r\n\"");
                ss1 = ss1.Replace("}", "\r\n}");
                ss1 = ss1.Replace("},{", "},\r\n{");
                ss1 = ss1.Replace("}]", "}\r\n]");
                File.WriteAllText(Application.StartupPath + @"\转换生成文件\CommonData.json", ss1);
            }
            if (this.保存commdata信息到存档剧本.Checked)
            {
                if (!this.单独保存commdata信息.Checked)
                {
                    GameScenario.SaveGameCommonData(scenario);
                }
                else
                {

                }
            }
            else
            {
                scenario.GameCommonData = null;
            }
            if (!this.保存配置信息到存档剧本.Checked)
            {
                scenario.GlobalVariables = null;
                scenario.Parameters = null;
            }
            else
            {

            }
            string path = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WorldOfTheThreeKingdoms\Save\" + "Save09.json";
            //   System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(GameScenario));
            //   string json = "";
            /* GameManager.Setting.Current.GlobalVariables = scenario.GlobalVariables;
             using (MemoryStream stream = new MemoryStream())
             {
                 //lock (Platform.SerializerLock)
                 {
                     serializer.WriteObject(stream, scenario);
                 }
                 var array = stream.ToArray();
                 json = Encoding.UTF8.GetString(array, 0, array.Length);
             }*/
            //   json =Tools.SimpleSerializer. SerializeJson(scenario, false, false);
            /* if (File.Exists(path))
             {
                 File.Delete(path);
             }
             File.WriteAllText(path, json);*/
            string 我的文档路径 = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WorldOfTheThreeKingdoms\Save\" + "Save09.json";
           // bool result = Tools.SimpleSerializer.SerializeJsonFile(scenario, System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\WorldOfTheThreeKingdoms\Save\" + "Save09.json", false, false, true);
            bool result = Tools.SimpleSerializer.SerializeJsonFile(scenario, Application.StartupPath +@"\转换生成文件\" + Path.GetFileNameWithoutExtension(已导入文件.Items[k].ToString()) + ".json", false, false, true);
            var saves = GameScenario.LoadScenarioSaves();

            if (result)
            {
                if (!是剧本.Checked)
                {
                    int id;
                    string name = Path.GetFileNameWithoutExtension(已导入文件.Items[k].ToString());
                    if (int.TryParse(name.Replace("Save", ""), out id))
                    {
                        string time = scenario.Date.Year + "-" + scenario.Date.Month + "-" + scenario.Date.Day;
                        saves[id] = new GameManager.Scenario()
                        {
                            Create = DateTime.Now.ToSeasonDateTime(),
                            Desc = scenario.ScenarioDescription,
                            IDs = "",
                            Info = scenario.PlayerInfo,
                            Name = name,
                            Names = "",
                            Path = "",
                            PlayTime = scenario.GameTime.ToString(),
                            Player = "",
                            Players = String.Join(",", scenario.PlayerList.NullToEmptyList()),
                            Time = time.ToSeasonDate(),
                            Title = scenario.ScenarioTitle
                        };
                        string saveFile = Application.StartupPath + "\\转换生成文件\\" + "Saves.json";
                        //SimpleSerializer.SerializeJsonFile(saves, saveFile);
                        string s2 = Newtonsoft.Json.JsonConvert.SerializeObject(saves, Newtonsoft.Json.Formatting.Indented);//Newtonsoft.Json.Formatting.Indented 这个表示会自动换行
                        File.WriteAllText(saveFile, s2);
                      //  SimpleSerializer.SerializeJsonFile(saves, saveFile, false, false, true);//上面2行实际就就在进行此行效果，但是有自动换行

                    }
                }
                else if (是剧本.Checked)
                {
                    string saveDir = Application.StartupPath + @"\转换生成文件\" + Path.GetFileNameWithoutExtension(已导入文件.Items[k].ToString()) + ".json";
                    string scenraios = Application.StartupPath + @"\Content\Data\Scenario\Scenarios.json";
                    List<GameManager.Scenario> scesList = null;
                    if (File.Exists(scenraios))
                    {
                        scesList = SimpleSerializer.DeserializeJsonFile<List<GameManager.Scenario>>(scenraios, false).NullToEmptyList();
                    }
                    if (scesList == null)
                    {
                        scesList = new List<GameManager.Scenario>();
                    }
                    int idd = 0;
                    foreach (var v in scesList)
                    {
                        if (v.Name == Path.GetFileNameWithoutExtension(已导入文件.Items[k].ToString()))
                        {
                            idd = scesList.IndexOf(v);
                        }
                    }
                    string time = scenario.Date.Year + "-" + scenario.Date.Month + "-" + scenario.Date.Day;
                    GameManager.Scenario s1 = new GameManager.Scenario()
                    {
                        Create = DateTime.Now.ToSeasonDateTime(),
                        Desc = scenario.ScenarioDescription,
                        First = StaticMethods.SaveToString(scenario.ScenarioMap.JumpPosition),
                        IDs = ids,
                        Info = "电脑",
                        Name = Path.GetFileNameWithoutExtension(已导入文件.Items[k].ToString()),
                        Names = names,
                        //  Path = "",
                        // PlayTime = scenario.GameTime.ToString(),
                        // Player = "",
                        //  Players = String.Join(",", scenario.PlayerList.NullToEmptyList()),
                        Time = time.ToSeasonDate(),
                        Title = scenario.ScenarioTitle
                    };
                    if(idd !=0)
                    {
                        scesList[idd] = s1;
                    }
                    else
                    {
                        scesList.Add(s1);
                    }
                    string saveFile = Application.StartupPath + "\\转换生成文件\\" + "Scenarios.json";
                   string s2=Newtonsoft.Json. JsonConvert.SerializeObject(scesList, Newtonsoft.Json.Formatting.Indented);//Newtonsoft.Json.Formatting.Indented 这个表示会自动换行
                    File.WriteAllText(saveFile, s2);
                    // SimpleSerializer.SerializeJsonFile(scesList, saveFile, true, false, true);
                }
            }

            ds.Dispose();
            GC.Collect();
            GC.WaitForPendingFinalizers();

        }
      //  private bool isscenario = false;
        private string ids = "";
        private string names = "";
        private void jsontoexcel(int i)
        {
            DataTable dt = new DataTable();
            string[] 取值字段 = new string[] { };
            System.Reflection.PropertyInfo[] oProps = null;
            Excel.Application aaa = new Excel.Application();
            Excel.Workbook workBook;
            int m = 1;
            int n = 1;
            int nn = 1;
            object missing = System.Reflection.Missing.Value;
            aaa.Visible = false;
            aaa.UserControl = true;
            workBook = aaa.Workbooks.Open(Application.StartupPath + "\\Content\\Data\\xlsTemplate.XLSX", missing, false, missing, missing, missing, missing, missing, missing, true, missing, missing, missing, missing, missing);
            System.Reflection.FieldInfo[] 非getset字段表 = typeof(Architecture).GetFields();
            System.Reflection.PropertyInfo[] getset字段表 = null;
            取值字段 = 城池取值字段();
            foreach (Architecture a in scenario.Architectures)
            {
                /*                //oProps = null;
                                取值字段 = 城池取值字段();
                              //  refreshdt(dt, oProps,a);
                                //使用反射获取T类型的属性信息，返回一个PropertyInfo类型的集合
                                if (oProps == null)
                                {
                                    oProps = ((Type)a.GetType()).GetProperties();
                                    //循环PropertyInfo数组,并按string[]的顺序取类型
                                    foreach (string s in 取值字段)
                                    {
                                        foreach (System.Reflection.PropertyInfo pi in oProps)
                                        {
                                            // if (城池取值字段().Contains(pi.Name))
                                            if (pi.Name == s)
                                            {
                                                Type colType = pi.PropertyType;//得到属性的类型
                                                                               //如果属性为泛型类型
                                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition()
                                                == typeof(Nullable<>)))
                                                {   //获取泛型类型的参数
                                                    colType = colType.GetGenericArguments()[0];
                                                }
                                                if(pi.Name == "ConvinceDestinationPersonList")
                                                {
                                                    colType = System.Type.GetType("System.String");
                                                }
                                                //将类型的属性名称与属性类型作为DataTable的列数据
                                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                                            }
                                        }
                                    }
                                    //  dt.Columns= dt.Columns.OrderBy(x => x == null ? "" : x.Name, 城池取值字段).ToArray();
                                }
                                //新建一个用于添加到DataTable中的DataRow对象
                                DataRow dr = dt.NewRow();
                                //循环遍历属性集合
                                foreach (System.Reflection.PropertyInfo pi in oProps)
                                {   //为DataRow中的指定列赋值
                                    if (取值字段.Contains(pi.Name))
                                    {
                                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                                        DBNull.Value : pi.GetValue(a, null);
                                    }
                                }
                                dr["ConvinceDestinationPersonList"] = a.ConvinceDestinationPersonList.SaveToString();
                                //将具有结果值的DataRow添加到DataTable集合中
                                dt.Rows.Add(dr);*/
                if (n == 1)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                DataRow dr = dt.NewRow();
                foreach (string s in 取值字段)
                {
                    if (nn == 1)
                    {
                        dt.Columns.Add(s);
                    }
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == s)
                        {
                            dr[s] = v.GetValue(a);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo pi in getset字段表)
                    {
                        if (pi.Name == s)
                        {
                            dr[s] = pi.GetValue(a);
                        }
                    }
                }
                dr["ConvinceDestinationPersonList"] = a.ConvinceDestinationPersonList.SaveToString();
                dr["jianzhuqizi"] =a.jianzhuqizi !=null? a.jianzhuqizi.qizipoint.ToString() : "";
                string ssss = "";
                foreach (KeyValuePair<int, int> var in a.captiveLoyaltyFall)
                {
                    ssss = ssss + "{" + var.Key + "  " + var.Value + "},";
                }
                dr["captiveLoyaltyFall"] = ssss;
                if (a.youzainan)
                {
                    dr["zainan"] = a.zainan.ID + "," + a.zainan.Name + "," + a.zainan.shengyutianshu + "," + a.zainan.zainanleixing;
                }
                else
                {
                    dr["zainan"] = "";
                }
                dt.Rows.Add(dr);
                n = 0;
                nn = 0;
            }
            if (scenario.Architectures.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 城池类型取值字段();
            m += 1;
            n = 1;
            nn = 1;
            非getset字段表 = typeof(ArchitectureKind).GetFields();
            foreach (GameObjects.ArchitectureDetail.ArchitectureKind a in CommonData.Current.AllArchitectureKinds.GetArchitectureKindList())
            {
                if (n == 1)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                DataRow dr = dt.NewRow();
                foreach (string s in 取值字段)
                {
                    if (nn == 1)
                    {
                        dt.Columns.Add(s);
                    }
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == s)
                        {
                            dr[s] = v.GetValue(a);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo pi in getset字段表)
                    {
                        if (pi.Name == s)
                        {
                            dr[s] = pi.GetValue(a);
                        }
                    }
                }
                dt.Rows.Add(dr);
                n = 0;
                nn = 0;
            }
            if (CommonData.Current.AllArchitectureKinds.GetArchitectureKindList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 常规取值字段();
            m += 1;
            n = 1;
            nn = 1;
            非getset字段表 = typeof(AttackDefaultKind).GetFields();
            foreach (GameObjects.TroopDetail.AttackDefaultKind a in CommonData.Current.AllAttackDefaultKinds)
            {
                if (n == 1)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                DataRow dr = dt.NewRow();
                foreach (string s in 取值字段)
                {
                    if (nn == 1)
                    {
                        dt.Columns.Add(s);
                    }
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == s)
                        {
                            dr[s] = v.GetValue(a);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo pi in getset字段表)
                    {
                        if (pi.Name == s)
                        {
                            dr[s] = pi.GetValue(a);
                        }
                    }
                }
                dt.Rows.Add(dr);
                n = 0;
                nn = 0;
            }
            if (CommonData.Current.AllAttackDefaultKinds.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 常规取值字段();
            m += 1;
            n = 1;
            nn = 1;
            非getset字段表 = typeof(AttackTargetKind).GetFields();
            foreach (GameObjects.TroopDetail.AttackTargetKind a in CommonData.Current.AllAttackTargetKinds)
            {
                if (n == 1)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                DataRow dr = dt.NewRow();
                foreach (string s in 取值字段)
                {
                    if (nn == 1)
                    {
                        dt.Columns.Add(s);
                    }
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == s)
                        {
                            dr[s] = v.GetValue(a);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo pi in getset字段表)
                    {
                        if (pi.Name == s)
                        {
                            dr[s] = pi.GetValue(a);
                        }
                    }
                }
                dt.Rows.Add(dr);
                n = 0;
                nn = 0;
            }
            if (CommonData.Current.AllAttackTargetKinds.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 简介取值字段();
            m += 1;
            List<GameObjects.PersonDetail.Biography> bio = new List<GameObjects.PersonDetail.Biography>();
            foreach (GameObjects.PersonDetail.Biography a in scenario.AllBiographies.GetBiographyList())
            {
                bio.Add(a);
            }
            bio = bio.OrderBy(Influence => Influence.ID).ToList();
            foreach (GameObjects.PersonDetail.Biography a in bio)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["Brief"] = a.Brief.Replace("\n", " ");
                dr["Romance"] = a.Romance.Replace("\n", " ");
                dr["InGame"] = a.InGame.Replace("\n", " ");
                dr["History"] = a.History.Replace("\n", " ");
                dt.Rows.Add(dr);
            }
            if (bio.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 简介类型取值字段();
            m += 1;
            List<BiographyAdjectives> bas = new List<BiographyAdjectives>();
            foreach (BiographyAdjectives a in CommonData.Current.AllBiographyAdjectives)
            {
                bas.Add(a);
            }
            bas = bas.OrderBy(Influence => Influence.ID).ToList();
            foreach (GameObjects.PersonDetail.BiographyAdjectives a in bas)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                if ((s == "SuffixText") || (s == "Text"))
                                {
                                    colType = System.Type.GetType("System.String");
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["SuffixText"] = GameGlobal.StaticMethods.SaveToString(a.SuffixText);
                dr["Text"] = GameGlobal.StaticMethods.SaveToString(a.Text);
                dt.Rows.Add(dr);

            }
            if (CommonData.Current.AllBiographyAdjectives.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 俘虏取值字段();
            m += 1;
            List<Captive> cap = new List<Captive>();
            foreach (Captive a in scenario.Captives)
            {
                cap.Add(a);
            }
            cap = cap.OrderBy(Influence => Influence.ID).ToList();
            foreach (Captive a in cap)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = (a.CaptivePerson != null) ? a.ID : -1;
                dr["CaptivePersonID"] = (a.CaptivePerson != null) ? a.CaptivePerson.ID : -1;
                dr["CaptiveFactionID"] = (a.CaptiveFaction != null) ? a.CaptiveFaction.ID : -1;
                dr["RansomArchitectureID"] = (a.RansomArchitecture != null) ? a.RansomArchitecture.ID : -1;
                dr["RansomFund"] = a.RansomFund;
                dr["RansomArriveDays"] = a.RansomArriveDays;
                dt.Rows.Add(dr);
            }
            if (cap.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 常规取值字段();
            m += 1;
            foreach (GameObjects.TroopDetail.CastDefaultKind a in CommonData.Current.AllCastDefaultKinds)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllCastDefaultKinds.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 常规取值字段();
            m += 1;
            foreach (GameObjects.TroopDetail.CastTargetKind a in CommonData.Current.AllCastTargetKinds)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllCastTargetKinds.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 人物性格取值字段();
            m += 1;
            foreach (GameObjects.PersonDetail.CharacterKind a in CommonData.Current.AllCharacterKinds)
            {
                if (dt.Columns.Count == 0)
                {
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["IntelligenceRate"] = a.IntelligenceRate;
                dr["ChallengeChance"] = a.ChallengeChance;
                dr["ControversyChance"] = a.ControversyChance;
                dr["GenerationChance"] = string.Join(",", a.GenerationChance);
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllCharacterKinds.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 颜色取值字段();
            m += 1;
            foreach (Microsoft.Xna.Framework.Color a in CommonData.Current.AllColors)
            {
                if (dt.Columns.Count == 0)
                {
                    dt.Columns.Add(new DataColumn("ID", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("A", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("B", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("G", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("R", System.Type.GetType("System.String")));
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = CommonData.Current.AllColors.IndexOf(a);
                dr["A"] = a.A;
                dr["B"] = a.B;
                dr["G"] = a.G;
                dr["R"] = a.R;
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllColors.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 战法取值字段();
            m += 1;
            List<CombatMethod> coms = new List<CombatMethod>();
            foreach (GameObjects.TroopDetail.CombatMethod a in CommonData.Current.AllCombatMethods.GetCombatMethodList())
            {
                coms.Add(a);
            }
            coms = coms.OrderBy(CombatMethod => CombatMethod.ID).ToList();
            foreach (GameObjects.TroopDetail.CombatMethod a in coms)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllCombatMethods.GetCombatMethodList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 条件事件效果取值字段();
            m += 1;
            List<GameObjects.Conditions.Condition> ccc = new List<GameObjects.Conditions.Condition>();
            foreach (GameObjects.Conditions.Condition a in CommonData.Current.AllConditions.GetConditionList())
            {
                ccc.Add(a);
            }
            ccc = ccc.OrderBy(Influence => Influence.ID).ToList();
            foreach (GameObjects.Conditions.Condition a in ccc)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                    dt.Columns.Add(new DataColumn("Kind", System.Type.GetType("System.String")));
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["Kind"] = a.Kind.ID;
                dt.Rows.Add(dr);
            }
            if (ccc.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 常规取值字段();
            m += 1;
            foreach (GameObjects.Conditions.ConditionKind a in CommonData.Current.AllConditionKinds.GetConditionKindList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllConditionKinds.GetConditionKindList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 势力关系取值字段();
            m += 1;
            foreach (GameObjects.FactionDetail.DiplomaticRelation a in scenario.DiplomaticRelations.DiplomaticRelations.Values)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (scenario.DiplomaticRelations.DiplomaticRelations.Values.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 灾难类型取值字段();
            m += 1;
            foreach (GameObjects.zainanzhongleilei a in CommonData.Current.suoyouzainanzhonglei.Getzainanzhongleiliebiao())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.suoyouzainanzhonglei.Getzainanzhongleiliebiao().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 事件取值字段();
            m += 1;
            foreach (Event a in scenario.AllEvents)
            {
                if (dt.Columns.Count == 0)
                {
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["happened"] = a.happened;
                dr["repeatable"] = a.repeatable;
                dr["AfterEventHappened"] = a.AfterEventHappened;
                dr["happenChance"] = a.happenChance;
                dr["GloballyDisplayed"] = a.GloballyDisplayed;
                dr["StartYear"] = a.StartYear;
                dr["StartMonth"] = a.StartMonth;
                dr["EndYear"] = a.EndYear;
                dr["EndMonth"] = a.EndMonth;
                dr["personString"] = a.personString;
                dr["PersonCondString"] = a.PersonCondString;
                dr["architectureString"] = a.architectureString;
                dr["architectureCondString"] = a.architectureCondString;
                dr["factionString"] = a.factionString;
                dr["factionCondString"] = a.factionCondString;
                dr["dialog"] = a.SaveDialogToString();
                dr["effectString"] = a.effectString;
                dr["architectureEffectString"] = a.architectureEffectString;
                dr["factionEffectIDString"] = a.factionEffectIDString;
                dr["Image"] = a.Image;
                dr["Sound"] = a.Sound;
                dr["scenBiography"] = a.SaveScenBiographyToString();
                dr["yesdialog"] = a.SaveyesDialogToString();
                dr["nodialog"] = a.SavenoDialogToString();
                dr["yesEffectString"] = a.yesEffectString;
                dr["noEffectString"] = a.noEffectString;
                dr["yesArchitectureEffectString"] = a.yesArchitectureEffectString;
                dr["noArchitectureEffectString"] = a.noArchitectureEffectString;
                dr["nextScenario"] = a.nextScenario;
                dt.Rows.Add(dr);
            }
            if (scenario.AllEvents.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 条件事件效果取值字段();
            m += 1;
            foreach (GameObjects.ArchitectureDetail.EventEffect.EventEffect a in CommonData.Current.AllEventEffects.GetEventEffectList())
            {
                if (dt.Columns.Count == 0)
                {
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["Parameter"] = a.Parameter;
                dr["Parameter2"] = a.Parameter2;
                dr["Kind"] = a.Kind.ID;
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllEventEffects.GetEventEffectList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 常规取值字段();
            m += 1;
            foreach (GameObjects.ArchitectureDetail.EventEffect.EventEffectKind a in CommonData.Current.AllEventEffectKinds.GetEventEffectKindList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllEventEffectKinds.GetEventEffectKindList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 设施取值字段();
            m += 1;
            foreach (GameObjects.Facility a in scenario.Facilities)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (scenario.Facilities.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 设施类型取值字段();
            m += 1;
            List<GameObjects.ArchitectureDetail.FacilityKind> ll = new List<GameObjects.ArchitectureDetail.FacilityKind>();
            foreach (GameObjects.ArchitectureDetail.FacilityKind a in CommonData.Current.AllFacilityKinds.GetFacilityKindList())
            {
                ll.Add(a);
            }
            ll = ll.OrderBy(FacilityKind => FacilityKind.ID).ToList();
            foreach (GameObjects.ArchitectureDetail.FacilityKind a in ll)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (ll.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 势力取值字段();
            m += 1;
            n = 1;
            nn = 1;
            非getset字段表 = typeof(Faction).GetFields();
            foreach (Faction a in scenario.Factions)
            {
                if (n == 1)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                DataRow dr = dt.NewRow();
                foreach (string s in 取值字段)
                {
                    if (nn == 1)
                    {
                        dt.Columns.Add(s);
                    }
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == s)
                        {
                            dr[s] = v.GetValue(a);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo pi in getset字段表)
                    {
                        if (pi.Name == s)
                        {
                            dr[s] = pi.GetValue(a);
                        }
                    }
                }
                dr["SpyMessageCloseList"] = "";//间谍相关
                dr["ClosedRouteways"] = "";//粮道相关
                dr["PreferredTechniqueKinds"] = string.Join(",", a.PreferredTechniqueKinds);
                dt.Rows.Add(dr);
                n = 0;
                nn = 0;
            }
            if (scenario.Factions.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 官爵类型取值字段();
            m += 1;
            foreach (guanjuezhongleilei a in CommonData.Current.suoyouguanjuezhonglei.Getguanjuedezhongleiliebiao())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.suoyouguanjuezhonglei.Getguanjuedezhongleiliebiao().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 出仕考虑类型取值字段();
            m += 1;
            foreach (GameObjects.PersonDetail.IdealTendencyKind a in CommonData.Current.AllIdealTendencyKinds)
            {
                if (dt.Columns.Count == 0)
                {
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["Offset"] = a.Offset;
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllIdealTendencyKinds.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 影响取值字段();
            m += 1;
            List<GameObjects.Influences.Influence> lll = new List<GameObjects.Influences.Influence>();
            foreach (GameObjects.Influences.Influence a in CommonData.Current.AllInfluences.GetInfluenceList())
            {
                lll.Add(a);
            }
            lll = lll.OrderBy(Influence => Influence.ID).ToList();
            foreach (GameObjects.Influences.Influence a in lll)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s && s != "Kind")
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                    dt.Columns.Add(new DataColumn("Kind", System.Type.GetType("System.String")));
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["Kind"] = a.Kind.ID;
                dt.Rows.Add(dr);
            }
            if (lll.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 影响类型取值字段();
            m += 1;
            List<GameObjects.Influences.InfluenceKind> llll = new List<GameObjects.Influences.InfluenceKind>();
            foreach (GameObjects.Influences.InfluenceKind a in CommonData.Current.AllInfluenceKinds.GetInfluenceKindList())
            {
                llll.Add(a);
            }
            llll = llll.OrderBy(InfluenceKind => InfluenceKind.ID).ToList();
            foreach (GameObjects.Influences.InfluenceKind a in llll)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (llll.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 情报取值字段();
            m += 1;
            List<Information> lllll = new List<Information>();
            foreach (Information a in scenario.Informations)
            {
                lllll.Add(a);
            }
            lllll = lllll.OrderBy(Information => Information.ID).ToList();
            foreach (Information a in lllll)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (lllll.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 情报类型取值字段();
            m += 1;
            foreach (GameObjects.FactionDetail.InformationKind a in CommonData.Current.AllInformationKinds)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllInformationKinds.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 军团取值字段();
            m += 1;
            n = 1;
            nn = 1;
            非getset字段表 = typeof(Legion).GetFields();
            List<Legion> llllll = new List<Legion>();
            foreach (Legion a in scenario.Legions)
            {
                llllll.Add(a);
            }
            llllll = llllll.OrderBy(Legion => Legion.ID).ToList();
            foreach (Legion a in llllll)
            {
                if (n == 1)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                DataRow dr = dt.NewRow();
                foreach (string s in 取值字段)
                {
                    if (nn == 1)
                    {
                        dt.Columns.Add(s);
                    }
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == s)
                        {
                            dr[s] = v.GetValue(a);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo pi in getset字段表)
                    {
                        if (pi.Name == s)
                        {
                            dr[s] = pi.GetValue(a);
                        }
                    }
                }
                dr["SupplyingRouteways"] = "";
                dr["TakenPositions"] = a.TakenPositions != null ? string.Join(",", a.TakenPositions) : "";
                dt.Rows.Add(dr);
                n = 0;
                nn = 0;
            }
            if (llllll.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 地图取值字段();
            m += 1;
            Map b = scenario.ScenarioMap;
            {
                if (oProps == null)
                {
                    oProps = ((Type)b.GetType()).GetProperties();
                    int iiii = 0;
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                        iiii++;
                        if (iiii > 6)
                        {
                            dt.Columns.Add(s);
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(b, null) == null ?
                        DBNull.Value : pi.GetValue(b, null);
                    }
                }
                dr["ScenarioTitle"] = scenario.ScenarioTitle;
                dr["FirePoint"] = scenario.FireTable.SaveToString();
                dr["NoFoodDictionaryPoint"] = scenario.NoFoodDictionary.SaveToString();
                dr["Date"] = scenario.Date;
                dr["DaySince"] = scenario.DaySince;
                dr["GameTime"] = scenario.GameTime;
                dr["CurrentPlayerID"] = scenario.CurrentPlayerID;
                dr["PlayerInfo"] = scenario.PlayerInfo;
                dr["PlayerList"] = GameGlobal.StaticMethods.SaveToString(scenario.PlayerList);
                dr["FactionQueue"] = scenario.Factions.FactionQueue;
                dr["ScenarioDescription"] = scenario.ScenarioDescription.Replace("\n","");
                dr["JumpPosition"] = scenario.ScenarioMap.JumpPosition;
                dt.Rows.Add(dr);
            }
            copytoexcel(aaa, workBook, dt, m);

            if (!Directory.Exists(@Application.StartupPath + @"\转换生成文件"))//如果不存在就创建file文件夹
            {
                Directory.CreateDirectory(@Application.StartupPath + @"\转换生成文件");
            }
            StreamWriter sw = new StreamWriter(Application.StartupPath + "\\转换生成文件\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()) + "地形信息.txt", false, Encoding.GetEncoding("gb2312"));
            sw.Write(b.MapDataString);
            sw.Flush();
            sw.Close();
            sw.Dispose();

            dt = new DataTable();
            oProps = null;
            取值字段 = 编队取值字段();
            m += 1;
            List<GameObjects.Military> mis = new List<GameObjects.Military>();
            foreach (Military a in scenario.Militaries)
            {
                mis.Add(a);
            }
            mis = mis.OrderBy(Influence => Influence.ID).ToList();
            foreach (Military a in mis)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (scenario.Militaries.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 编队类型取值字段();
            m += 1;
            List<GameObjects.TroopDetail.MilitaryKind> qq = new List<GameObjects.TroopDetail.MilitaryKind>();
            foreach (GameObjects.TroopDetail.MilitaryKind a in CommonData.Current.AllMilitaryKinds.GetMilitaryKindList())
            {
                qq.Add(a);
            }
            qq = qq.OrderBy(Influence => Influence.ID).ToList();
            foreach (GameObjects.TroopDetail.MilitaryKind a in qq)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                    dt.Columns.Add(new DataColumn("Sounds", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("LevelUpKindID", System.Type.GetType("System.String")));
                    dt.Columns.Add(new DataColumn("zijinshangxian", System.Type.GetType("System.String")));
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["LevelUpKindID"] = GameGlobal.StaticMethods.SaveToString(a.LevelUpKindID);
                dr["zijinshangxian"] = a.zijinshangxian;
                dr["Sounds"] = ""; //默认为空
                dt.Rows.Add(dr);
            }
            if (qq.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 人物取值字段();
            m += 1;
            n = 1;
            nn = 1;
            非getset字段表 = typeof(Person).GetFields();
            foreach (Person a in scenario.Persons)
            {
                if (n == 1)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                DataRow dr = dt.NewRow();
                foreach (string s in 取值字段)
                {
                    if (nn == 1)
                    {
                        dt.Columns.Add(s);
                    }
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == s)
                        {
                            dr[s] = v.GetValue(a);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo pi in getset字段表)
                    {
                        if (pi.Name == s)
                        {
                            dr[s] = pi.GetValue(a);
                        }
                    }
                }
                string rela = "";
                foreach (KeyValuePair<Person, int> pi in a.GetRelations())
                {
                    rela += pi.Key.ID.ToString() + " " + pi.Value.ToString() + ",";
                }
                if (a.GetRelations().Count > 0)
                {
                    rela = rela.Substring(0, rela.Length - 1);
                }
                dr["relations"] = rela;
                dr["father"] = a.Father == null ? -1 : a.Father.ID;
                dr["mother"] = a.Mother == null ? -1 : a.Mother.ID;
                dr["spouse"] = a.Spouse == null ? -1 : a.Spouse.ID;
                dr["brothers"] = 人物列表(a.Brothers);
                dr["ClosePersons"] = 人物列表(a.ClosePersons);
                dr["HatedPersons"] = 人物列表(a.HatedPersons);
                dr["suoshurenwuList"] = 人物列表(a.suoshurenwuList);
                dr["JoinFactionID"] = GameGlobal.StaticMethods.SaveToString(a.JoinFactionID);
                dr["ProhibitedFactionID"] = GameGlobal.StaticMethods.SaveToString(a.ProhibitedFactionID);
                dr["marriageGranter"] = a.marriageGranter == null ? -1 : a.marriageGranter.ID;
                dt.Rows.Add(dr);
                n = 0;
                nn = 0;
            }
            if (scenario.Persons.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 生成人物取值字段();
            m += 1;
            GameObjects.PersonDetail.PersonGeneratorSetting sss = CommonData.Current.PersonGeneratorSetting;
            if (dt.Columns.Count == 0)
            {
                foreach (string s in 取值字段)
                {
                    dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                }
            }
            DataRow drr = dt.NewRow();
            drr["ID"] = CommonData.Current.PersonGeneratorSetting.ID;
            drr["bornLo"] = sss.bornLo;
            drr["bornHi"] = sss.bornHi;
            drr["debutLo"] = sss.debutLo;
            drr["debutHi"] = sss.debutHi;
            drr["dieLo"] = sss.dieLo;
            drr["dieHi"] = sss.dieHi;
            drr["femaleChance"] = sss.femaleChance;
            drr["debutAtLeast"] = sss.debutAtLeast;
            drr["ChildrenFemaleChance"] = sss.ChildrenFemaleChance;
            dt.Rows.Add(drr);
            copytoexcel(aaa, workBook, dt, m);

            dt = new DataTable();
            oProps = null;
            取值字段 = 生成人物类型取值字段();
            m += 1;
            foreach (GameObjects.PersonDetail.PersonGeneratorType a in CommonData.Current.AllPersonGeneratorTypes)
            {
                if (dt.Columns.Count == 0)
                {
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["generationChance"] = a.generationChance;
                dr["commandLo"] = a.commandLo;
                dr["commandHi"] = a.commandHi;
                dr["strengthLo"] = a.strengthLo;
                dr["strengthHi"] = a.strengthHi;
                dr["intelligenceLo"] = a.intelligenceLo;
                dr["intelligenceHi"] = a.intelligenceHi;
                dr["politicsLo"] = a.politicsLo;
                dr["politicsHi"] = a.politicsHi;
                dr["glamourLo"] = a.glamourLo;
                dr["glamourHi"] = a.glamourHi;
                dr["braveLo"] = a.braveLo;
                dr["braveHi"] = a.braveHi;
                dr["calmnessLo"] = a.calmnessLo;
                dr["calmnessHi"] = a.calmnessHi;
                dr["personalLoyaltyLo"] = a.personalLoyaltyLo;
                dr["personalLoyaltyHi"] = a.personalLoyaltyHi;
                dr["ambitionLo"] = a.ambitionLo;
                dr["ambitionHi"] = a.ambitionHi;
                dr["titleChance"] = a.titleChance;
                dr["affectedByRateParameter"] = a.affectedByRateParameter;
                dr["TypeCount"] = a.TypeCount;
                dr["FactionLimit"] = a.FactionLimit;
                dr["CostFund"] = a.CostFund;
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllPersonGeneratorTypes.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 地域取值字段();
            m += 1;
            foreach (GameObjects.ArchitectureDetail.Region a in scenario.Regions)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["StatesListString"] = a.StatesListString;
                dr["RegionCoreID"] = a.RegionCore != null ? a.RegionCore.ID : -1;
                dt.Rows.Add(dr);
            }
            if (scenario.Regions.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 粮道取值字段();
            m += 1;
            foreach (Routeway a in scenario.Routeways)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["RoutePoints"] = string.Join(",", a.RoutePoints);
                dt.Rows.Add(dr);
            }
            if (scenario.Routeways.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 军区战略取值字段();
            m += 1;
            foreach (GameObjects.SectionDetail.SectionAIDetail a in CommonData.Current.AllSectionAIDetails.GetSectionAIDetailList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["OrientationKind"] = (int)a.OrientationKind;
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllSectionAIDetails.GetSectionAIDetailList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 军区取值字段();
            m += 1;
            List<Section> sec = new List<Section>();
            foreach (Section a in scenario.Sections)
            {
                sec.Add(a);
            }
            sec = sec.OrderBy(section => section.ID).ToList();
            foreach (Section a in sec)
            {
                if (dt.Columns.Count == 0)
                {
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(new DataColumn(s, System.Type.GetType("System.String")));
                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["AIDetailIDString"] = a.AIDetailIDString;
                dr["OrientationFactionID"] = a.OrientationFactionID;
                dr["OrientationSectionID"] = a.OrientationSectionID;
                dr["OrientationStateID"] = a.OrientationStateID;
                dr["OrientationArchitectureID"] = a.OrientationArchitectureID;
                dr["ArchitecturesString"] = a.ArchitecturesString;
                dt.Rows.Add(dr);
            }
            if (scenario.Sections.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 技能取值字段();
            m += 1;
            foreach (GameObjects.PersonDetail.Skill a in CommonData.Current.AllSkills.GetSkillList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                if (s == "GenerationChance")
                                {
                                    colType = System.Type.GetType("System.String");
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["GenerationChance"] = string.Join(",", a.GenerationChance.ToList());
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllSkills.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 州域取值字段();
            m += 1;
            foreach (GameObjects.ArchitectureDetail.State a in scenario.States)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(s);
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["ContactStatesString"] = a.ContactStatesString;
                dr["StateAdminID"] = a.StateAdminID;
                dt.Rows.Add(dr);
            }
            if (scenario.States.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 计略取值字段();
            m += 1;
            foreach (GameObjects.TroopDetail.Stratagem a in CommonData.Current.AllStratagems.GetStratagemList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllStratagems.GetStratagemList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 特技取值字段();
            m += 1;
            foreach (GameObjects.PersonDetail.Stunt a in CommonData.Current.AllStunts.GetStuntList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                if (s == "GenerationChance")
                                {
                                    colType = System.Type.GetType("System.String");
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["GenerationChance"] = string.Join(",", a.GenerationChance.ToList());
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllStunts.GetStuntList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 科技取值字段();
            m += 1;
            List<GameObjects.FactionDetail.Technique> tec = new List<GameObjects.FactionDetail.Technique>();
            foreach (GameObjects.FactionDetail.Technique a in CommonData.Current.AllTechniques.GetTechniqueList())
            {
                tec.Add(a);
            }
            tec = tec.OrderBy(Technique => Technique.ID).ToList();
            foreach (GameObjects.FactionDetail.Technique a in tec)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTechniques.GetTechniqueList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 地形取值字段();
            m += 1;
            foreach (GameObjects.MapDetail.TerrainDetail a in CommonData.Current.AllTerrainDetails.GetTerrainDetailList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTerrainDetails.GetTerrainDetailList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 个性语言取值字段();
            m += 1;
            int tempp = 1;
            foreach (KeyValuePair<KeyValuePair<int, GameObjects.PersonDetail.TextMessageKind>, List<string>> a in CommonData.Current.AllTextMessages.GetAllMessages())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(s);

                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = tempp++;
                dr["Person"] = a.Key.Key;
                dr["Kind"] = (int)a.Key.Value;
                dr["Messages"] = GameGlobal.StaticMethods.SaveToString(a.Value);
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTextMessages.GetAllMessages().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            m += 1;//跳过说明文档个性语言种类

            dt = new DataTable();
            oProps = null;
            取值字段 = 动画取值字段();
            m += 1;
            foreach (GameObjects.Animations.Animation a in CommonData.Current.AllTileAnimations.GetAnimationList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(s);

                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["FrameCount"] = a.FrameCount;
                dr["StayCount"] = a.StayCount;
                dr["Back"] = a.Back;
                dr["TextureWidth"] = a.TextureWidth;
                dr["TextureHeight"] = a.TextureHeight;
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTileAnimations.GetAnimationList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 称号取值字段();
            m += 1;
            List<GameObjects.PersonDetail.Title> tit = new List<GameObjects.PersonDetail.Title>();
            foreach (GameObjects.PersonDetail.Title a in CommonData.Current.AllTitles.GetTitleList())
            {
                tit.Add(a);
            }
            tit = tit.OrderBy(title => title.ID).ToList();
            foreach (GameObjects.PersonDetail.Title a in tit)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                if (s == "GenerationChance" || s == "Kind")
                                {
                                    colType = System.Type.GetType("System.String");
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["Kind"] = a.Kind.ID;
                dr["GenerationChance"] = string.Join(",", a.GenerationChance.ToList());
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTitles.GetTitleList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 称号类型取值字段();
            m += 1;
            List<GameObjects.PersonDetail.TitleKind> tk = new List<GameObjects.PersonDetail.TitleKind>();
            foreach (GameObjects.PersonDetail.TitleKind a in CommonData.Current.AllTitleKinds.GetTitleKindList())
            {
                tk.Add(a);
            }
            tk = tk.OrderBy(Influence => Influence.ID).ToList();
            foreach (GameObjects.PersonDetail.TitleKind a in tk)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTitleKinds.GetTitleKindList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 培育方针取值字段();
            m += 1;
            foreach (GameObjects.PersonDetail.TrainPolicy a in CommonData.Current.AllTrainPolicies)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTrainPolicies.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 宝物取值字段();
            m += 1;
            List<Treasure> tre = new List<Treasure>();
            foreach (Treasure a in scenario.Treasures)
            {
                tre.Add(a);
            }
            tre = tre.OrderBy(Influence => Influence.ID).ToList();
            foreach (Treasure a in tre)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (tre.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 部队取值字段();
            m += 1;
            List<Troop> tro = new List<Troop>();
            foreach (Troop a in scenario.Troops)
            {
                tro.Add(a);
            }
            tro = tro.OrderBy(Influence => Influence.ID).ToList();
            n = 1;
            nn = 1;
            非getset字段表 = typeof(Troop).GetFields();

            foreach (Troop a in tro)
            {
                if (n == 1)
                {
                    getset字段表 = ((Type)a.GetType()).GetProperties();
                }
                DataRow dr = dt.NewRow();
                foreach (string s in 取值字段)
                {
                    if (nn == 1)
                    {
                        dt.Columns.Add(s);
                    }
                    foreach (var v in 非getset字段表)
                    {
                        if (v.Name == s)
                        {
                            dr[s] = v.GetValue(a);
                        }
                    }
                    foreach (System.Reflection.PropertyInfo pi in getset字段表)
                    {
                        if (pi.Name == s && s != "DrawAnimation")
                        {
                            dr[s] = pi.GetValue(a);
                        }
                    }
                }
                dr["DrawAnimation"] = GameManager.Session.GlobalVariables.DrawTroopAnimation;
                dr["AllowedStrategems"] = StaticMethods.SaveToString(a.AllowedStrategems);
                dr["MoveAnimationFrames"] = a.MoveAnimationFrames != null ? string.Join(",", a.MoveAnimationFrames) : "";
                string ss = "";
                foreach (Stunt s in a.Stunts.Stunts.Values)
                {
                    ss += s.ID + ",";
                }
                dr["Stunts"] = ss;
                dr["FirstTierPath"] = a.FirstTierPath != null ? string.Join(",", a.FirstTierPath) : "";
                dr["SecondTierPath"] = a.SecondTierPath != null ? string.Join(",", a.SecondTierPath) : "";
                dr["ThirdTierPath"] = a.ThirdTierPath != null ? string.Join(",", a.ThirdTierPath) : "";
                dt.Rows.Add(dr);
                n = 0;
                nn = 0;
            }
            if (scenario.Troops.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 部队动画取值字段();
            m += 1;
            foreach (GameObjects.Animations.Animation a in CommonData.Current.AllTroopAnimations.GetAnimationList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(s);

                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Name"] = a.Name;
                dr["FrameCount"] = a.FrameCount;
                dr["StayCount"] = a.StayCount;
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTroopAnimations.GetAnimationList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 部队事件取值字段();
            m += 1;
            foreach (TroopEvent a in scenario.TroopEvents)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                        if (s == "AfterEventHappened" || s == "CheckArea" || s == "Dialogs" || s == "Image" || s == "Sound")
                        {
                            dt.Columns.Add(s);
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["AfterEventHappened"] = a.AfterEventHappened;
                dr["CheckArea"] = (int)a.CheckArea;
                string str = "";
                foreach (PersonDialog dialog in a.Dialogs)
                {
                    str += "{" + dialog.SpeakingPersonID.ToString() + " " + dialog.Text + "}";
                }
                dr["Dialogs"] = str;
                dr["Image"] = a.Image;
                dr["Sound"] = a.Sound;
                dt.Rows.Add(dr);
            }
            if (scenario.TroopEvents.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 部队事件效果取值字段();
            m += 1;
            foreach (GameObjects.TroopDetail.EventEffect.EventEffect a in CommonData.Current.AllTroopEventEffects.GetEventEffectList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                        if (s == "Kind")
                        {
                            dt.Columns.Add(s);
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dr["Kind"] = a.Kind.ID;
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTroopEventEffects.GetEventEffectList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 常规取值字段();
            m += 1;
            foreach (GameObjects.TroopDetail.EventEffect.EventEffectKind a in CommonData.Current.AllTroopEventEffectKinds.GetEventEffectKindList())
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        foreach (System.Reflection.PropertyInfo pi in oProps)
                        {
                            if (pi.Name == s)
                            {
                                Type colType = pi.PropertyType;
                                if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    colType = colType.GetGenericArguments()[0];
                                }
                                dt.Columns.Add(new DataColumn(pi.Name, colType));
                            }
                        }
                    }
                }
                DataRow dr = dt.NewRow();
                foreach (System.Reflection.PropertyInfo pi in oProps)
                {
                    if (取值字段.Contains(pi.Name))
                    {
                        dr[pi.Name] = pi.GetValue(a, null) == null ?
                        DBNull.Value : pi.GetValue(a, null);
                    }
                }
                dt.Rows.Add(dr);
            }
            if (CommonData.Current.AllTroopEventEffectKinds.GetEventEffectKindList().Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = 年表取值字段();
            m += 1;
            foreach (YearTableEntry a in scenario.YearTable)
            {
                if (oProps == null)
                {
                    oProps = ((Type)a.GetType()).GetProperties();
                    foreach (string s in 取值字段)
                    {
                        dt.Columns.Add(s);

                    }
                }
                DataRow dr = dt.NewRow();
                dr["ID"] = a.ID;
                dr["Date"] = a.Date;
                dr["FactionsString"] = a.FactionsString;
                dr["Content"] = a.Content;
                dr["IsGloballyKnown"] = a.IsGloballyKnown;
                dt.Rows.Add(dr);
            }
            if (scenario.YearTable.Count > 0)
            {
                copytoexcel(aaa, workBook, dt, m);
            }

            dt = new DataTable();
            oProps = null;
            取值字段 = Para参数取值字段();
            m += 1;
            GameGlobal.Parameters pa = GameManager.Session.Parameters;
            System.Reflection.FieldInfo[] 字段表 = typeof(GameGlobal.Parameters).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            if (oProps == null)
            {
                dt.Columns.Add("Name");
                dt.Columns.Add("Value");
            }
            foreach (string s in 取值字段)
            {
                DataRow dr = dt.NewRow();
                foreach (var v in 字段表)
                {
                    if (s == v.Name)
                    {
                        dr["Name"] = v.Name;
                        dr["Value"] = v.GetValue(pa);
                    }
                    if (s == "ExpandConditions")
                    {
                        // dr["Name"] = v.Name;
                        dr["Value"] = GameGlobal.StaticMethods.SaveToString(pa.ExpandConditions);
                    }
                }
                dt.Rows.Add(dr);
            }
            copytoexcel(aaa, workBook, dt, m);

            dt = new DataTable();
            oProps = null;
            取值字段 = Glob参数取值字段();
            m += 1;
            GameGlobal.GlobalVariables glo = GameManager.Session.GlobalVariables;
            字段表 = typeof(GameGlobal.GlobalVariables).GetFields((System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance));
            if (oProps == null)
            {
                dt.Columns.Add("Name");
                dt.Columns.Add("Value");
            }
            foreach (string s in 取值字段)
            {
                DataRow dr = dt.NewRow();
                foreach (var v in 字段表)
                {
                    if (s == v.Name)
                    {
                        dr["Name"] = v.Name;
                        dr["Value"] = v.GetValue(glo);
                    }
                    if (s == "CurrentMapLayer")
                    {
                        dr["Value"] = (int)glo.CurrentMapLayer;
                    }
                    else if (s == "RoutewayInformationLevel")
                    {
                        dr["Value"] = (int)glo.RoutewayInformationLevel;
                    }
                    else if (s == "ScoutRoutewayInformationLevel")
                    {
                        dr["Value"] = (int)glo.ScoutRoutewayInformationLevel;
                    }
                }
                dt.Rows.Add(dr);
            }
            copytoexcel(aaa, workBook, dt, m);


            workBook.SaveAs(@Application.StartupPath + "\\转换生成文件\\" + Path.GetFileNameWithoutExtension(已导入文件.Items[i].ToString()), 51);//fileformat用 51表示的是存储的格式版本为xlsx
            workBook.Close();
            aaa.Quit();
            workBook = null;
            aaa = null;
            dt.Dispose();
        }

        private string[] 城池取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "AILandLinksString",
                "AIWaterLinksString",
                "Agriculture",
                "ArchitectureAreaString",
                "AutoHiring",
                "AutoRecruiting",
                "AutoRefillFoodInLongViewArea",
                "AutoRewarding",
                "AutoSearching",
                "AutoWorking",
                "AutoZhaoXian",
                "BuildingDaysLeft",
                "BuildingFacility",
                "CaptionID",
                "CaptivesString",
                "CharacteristicsString",
                "Commerce",
                "DefensiveLegion",
                "DefensiveLegionID",
                "Domination",
                "Endurance",
                "FacilitiesString",
                "FacilityEnabled",
                "Food",
                "FoodPacksString",
                "Fund",
                "FundPacksString",
                "HasManualHire",
                "HireFinished",
                "InformationsString",
                "IsStrategicCenter",
                "KindId",
                "MayorID",
                "MayorOnDutyDays",
                "MilitariesString",
                "MilitaryPopulation",
                "Morale",
                "MovingPersonsString",
                "NoFactionMovingPersonsString",
                "NoFactionPersonsString",
                "OldFactionName",
                "PersonsString",
                "PlanArchitectureID",
                "PlanFacilityKindID",
                "Population",
                "PopulationPacksString",
                "RecentlyAttacked",
                "RecentlyBreaked",
                "RobberTroopID",
                "StateID",
                "SuspendTroopTransfer",
                "Technology",
                "TodayPersonArriveNote",
                "TransferFoodArchitectureID",
                "TransferFundArchitectureID",
                "TroopershipAvailable",
                "feiziliebiaoString",
                "huangdisuozai",
                "jianzhuqizi",
                "noFundToSustainFacility",
                "ConvinceDestinationPersonList",
                "captiveLoyaltyFall",
                "youzainan",
                "zainan",
            };
        }
        private string[] 城池类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "AgricultureBase",
                "AgricultureUnit",
                "CommerceBase",
                "CommerceUnit",
                "TechnologyBase",
                "TechnologyUnit",
                "DominationBase",
                "DominationUnit",
                "MoraleBase",
                "MoraleUnit",
                "EnduranceBase",
                "EnduranceUnit",
                "PopulationBase",
                "PopulationUnit",
                "PopulationBoundary",
                "ViewDistance",
                "ViewDistanceIncrementDivisor",
                "HasObliqueView",
                "HasLongView",
                "HasPopulation",
                "HasAgriculture",
                "HasCommerce",
                "HasTechnology",
                "HasDomination",
                "HasMorale",
                "HasEndurance",
                "HasHarbor",
                "FacilityPositionUnit",
                "FundMaxUnit",
                "FoodMaxUnit",
                "CountToMerit",
                "Expandable",
                "ShipCanEnter"
            };
        }
        private string[] 常规取值字段()
        {
            return new string[]
            {
                "ID",
                "Name"
            };
        }
        private string[] 简介取值字段()
        {
            return new string[]
            {
                "ID",
                "Brief",
                "Romance",
                "History",
                "InGame",
                "FactionColor",
                "MilitaryKindsString"
            };
        }
        private string[] 简介类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Strength",
                "Command",
                "Intelligence",
                "Politics",
                "Glamour",
                "Braveness",
                "Calmness",
                "Male",
                "Female",
                "PersonalLoyalty",
                "Ambition",
                "SuffixText",
                "Text"
            };
        }
        private string[] 俘虏取值字段()
        {
            return new string[]
            {
                "ID",
                "CaptivePersonID",
                "CaptiveFactionID",
                "RansomArchitectureID",
                "RansomFund",
                "RansomArriveDays",
            };
        }
        private string[] 人物性格取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "IntelligenceRate",
                "ChallengeChance",
                "ControversyChance",
                "GenerationChance",
            };
        }
        private string[] 颜色取值字段()
        {
            return new string[]
            {
                "ID",
                "A",
                "B",
                "G",
                "R",
            };
        }
        private string[] 战法取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Description",
                "Combativity",
                "InfluencesString",
                "AttackDefaultString",
                "AttackTargetString",
                "ArchitectureTarget",
                "CastConditionsString",
                "ViewingHostile",
                "AnimationKind",
                "AIConditionWeightSelfString",
                "AIConditionWeightEnemyString",
            };
        }
        private string[] 条件事件效果取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Parameter",
                "Parameter2",
                "Kind",
            };
        }
        private string[] 势力关系取值字段()
        {
            return new string[]
            {
                "RelationFaction1ID",
                "RelationFaction2ID",
                "Relation",
                "Truce",
            };
        }
        private string[] 灾难类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "shijianxiaxian",
                "shijianshangxian",
                "renkoushanghai",
                "TroopDamage",
                "tongzhishanghai",
                "naijiushanghai",
                "FundDamage",
                "FoodDamage",
                "nongyeshanghai",
                "shangyeshanghai",
                "jishushanghai",
                "minxinshanghai",
                "OfficerDamage"
            };
        }
        private string[] 事件取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "happened",
                "repeatable",
                "AfterEventHappened",
                "happenChance",
                "GloballyDisplayed",
                "StartYear",
                "StartMonth",
                "EndYear",
                "EndMonth",
                "personString",
                "PersonCondString",
                "architectureString",
                "architectureCondString",
                "factionString",
                "factionCondString",
                "dialog",
                "effectString",
                "architectureEffectString",
                "factionEffectIDString",
                "Image",
                "Sound",
                "scenBiography",
                "yesdialog",
                "nodialog",
                "yesEffectString",
                "noEffectString",
                "yesArchitectureEffectString",
                "noArchitectureEffectString",
                "nextScenario",
            };
        }
        private string[] 设施取值字段()
        {
            return new string[]
            {
                "ID",
                "KindID",
                "Endurance"
            };
        }
        private string[] 设施类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Description",
                "AILevel",
                "PositionOccupied",
                "TechnologyNeeded",
                "FundCost",
                "MaintenanceCost",
                "PointCost",
                "Days",
                "Endurance",
                "ArchitectureLimit",
                "FactionLimit",
                "PopulationRelated",
                "InfluencesString",
                "ConditionTableString",
                "rongna",
                "bukechaichu"
            };
        }
        private string[] 势力取值字段()
        {
            return new string[]
            {
                "ID",
                "LeaderID",
                "ColorIndex",
                "Name",
                "CapitalID",
                "TechniquePoint",
                "TechniquePointForTechnique",
                "TechniquePointForFacility",
                "Reputation",
                "SectionsString",
                "InformationsString",
                "ArchitecturesString",
                "TroopListString",
                "RoutewaysString",
                "LegionsString",
                "BaseMilitaryKindsString",
                "UpgradingTechnique",
                "UpgradingDaysLeft",
                "AvailableTechniquesString",
                "PreferredTechniqueKinds",
                "PlanTechniqueString",
                "AutoRefuse",
                "chaotinggongxiandu",
                "guanjue",
                "IsAlien",
                "NotPlayerSelectable",
                "PrinceID",
                "YearOfficialLimit",
                "MilitaryCount",
                "TransferingMilitaryCount",
                "GetGeneratorPersonCountString",
                "TransferingMilitariesString",
                "MilitariesString",
                "ZhaoxianFailureCount",
                "ClosedRouteways",
                "Destroyed",
                "SecondTierXResidue",
                "SecondTierYResidue",
                "SpyMessageCloseList",
                "ThirdTierXResidue",
                "ThirdTierYResidue",

            };
        }
        private string[] Gamedata取值字段()
        {
            return new string[]
            {
                "PlayerList",
                "CurrentPlayer",
                "FactionQueue",
                "FireTable",
                "NoFoodTable",
                "DaySince",
            };
        }
        private string[] GameSurvey取值字段()
        {
            return new string[]
            {
                "Title",
                "GYear",
                "GMonth",
                "GDay",
                "SaveTime",
                "PlayerInfo",
                "JumpPosition",
                "Description",
                "GameTime",
            };
        }
        private string[] GlobalVariables取值字段()
        {
            return new string[]
            {
                "Name",
                "Value",
            };
        }
        private string[] 官爵类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "shengwangshangxian",
                "xuyaogongxiandu",
                "xuyaochengchi",
                "ShowDialog",
                "Loyalty",
            };
        }
        private string[] 出仕考虑类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Offset",
            };
        }
        private string[] 影响取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Description",
                "Parameter",
                "Parameter2",
                 "Kind",
            };
        }
        private string[] 影响类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Type",
                "Name",
                "Combat",
                "AIPersonValue",
                "AIPersonValuePow",
                "TroopLeaderValid",
            };
        }
        private string[] 情报取值字段()
        {
            return new string[]
            {
                "ID",
                "Level",
                "Position",
                "PositionY",
                "Radius",
                "Oblique",
                "DayCost",
                "DaysLeft",
                "DaysStarted",
            };
        }
        private string[] 情报类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Level",
                "Radius",
                "Oblique",
                "CostFund",
            };
        }
        private string[] 军团取值字段()
        {
            return new string[]
            {
                "ID",
                "Kind",
                "StartArchitectureString",
                "WillArchitectureString",
                "PreferredRoutewayString",
                "InformationDestination",
                "CoreTroopString",
                "TroopsString",
                "SupplyingRouteways",
                "TakenPositions",
            };
        }
        private string[] 地图取值字段()
        {
            return new string[]
            {
                "TileWidth",
                "MapDimensions",
                "MapName",
                "NumberOfTiles",
                "NumberOfSquaresInEachTile",
                "UseSimpleArchImages",
                "ScenarioTitle",
                "FirePoint",
                "NoFoodDictionaryPoint",
                "Date",
                "DaySince",
                "GameTime",
                "CurrentPlayerID",
                "PlayerInfo",
                "PlayerList",
                "FactionQueue",
                "ScenarioDescription",
                "JumpPosition",
            };
        }
        private string[] 编队取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "KindID",
                "Quantity",
                "Morale",
                "Combativity",
                "Experience",
                "InjuryQuantity",
                "FollowedLeaderID",
                "LeaderID",
                "LeaderExperience",
                "RecruitmentPersonID",
                "Tiredness",
                "ArrivingDays",
                "StartingArchitectureID",
                "TargetArchitectureID",
                "RoutCount",
                "YearCreated",
                "TroopDamageDealt",
                "TroopBeDamageDealt",
                "ArchitectureDamageDealt",
                "StratagemSuccessCount",
                "StratagemFailCount",
                "StratagemBeSuccessCount",
                "StratagemBeFailCount",
                "OfficerKillCount",
                "CaptiveCount",
            };
        }
        private string[] 编队类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Type",
                "Name",
                "Description",
                "Merit",
                "SuccessorString",
                "Speed",
                "ObtainProb",
                "CreateCost",
                "CreateTechnology",
                "IsShell",
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
                "OffenceOnlyBeforeMove",
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
                "OffencePer100Experience",
                "DefencePer100Experience",
                "AttackDefaultKind",
                "AttackTargetKind",
                "CastDefaultKind",
                "CastTargetKind",
                "InfluencesString",
                "zijinshangxian",
                "MorphToKindId",
                "MinCommand",
                "CreateConditionsString",
                "LevelUpExperience",
                "TitleInfluence",
                "AICreateArchitectureConditionWeightString",
                "AILeaderConditionWeightString",
                "AIUpgradeArchitectureConditionWeightString",
                "AIUpgradeLeaderConditionWeightString",
                "Sounds",

            };
        }
        private string[] 人物取值字段()
        {
            return new string[]
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
                "DeadReason",
                "BaseStrength",
                "BaseCommand",
                "BaseIntelligence",
                "BasePolitics",
                "BaseGlamour",
                "Reputation",
                "UniqueTitlesString",
                "UniqueMilitaryKindsString",
                "StrengthExperience",
                "CommandExperience",
                "IntelligenceExperience",
                "PoliticsExperience",
                "GlamourExperience",
                "InternalExperience",
                "TacticsExperience",
                "BubingExperience",
                "NubingExperience",
                "QibingExperience",
                "ShuijunExperience",
                "QixieExperience",
                "StratagemExperience",
                "RoutCount",
                "RoutedCount",
                "BaseBraveness",
                "BaseCalmness",
                "Loyalty",
                "BornRegion",
                "AvailableLocation",
                "Strain",
                "relations",
                "father",
                "mother",
                "spouse",
                "brothers",
                "ClosePersons",
                "HatedPersons",
                "suoshurenwuList",
                "JoinFactionID",
                "ProhibitedFactionID",
                "Generation",
                "PersonalLoyalty",
                "Ambition",
                "Qualification",
                "ValuationOnGovernment",
                "StrategyTendency",
                "WorkKind",
                "OldWorkKind",
                "ArrivingDays",
                "TaskDays",
                "OutsideTask",
                "OutsideDestination",
                "ConvincingPersonID",
                "InformationKindID",
                "SkillsString",
                "RealTitlesString",
                "StudyingTitleString",
                "StuntsString",
                "StudyingStuntString",
                "huaiyun",
                "faxianhuaiyun",
                "huaiyuntianshu",
                "suoshurenwu",
                "waitForFeiziId",
                "waitForFeiZiPeriod",
                "preferredTroopPersonsString",
                "YearJoin",
                "TroopDamageDealt",
                "TroopBeDamageDealt",
                "ArchitectureDamageDealt",
                "RebelCount",
                "ExecuteCount",
                "FleeCount",
                "CaptiveCount",
                "HeldCaptiveCount",
                "StratagemSuccessCount",
                "StratagemFailCount",
                "StratagemBeSuccessCount",
                "StratagemBeFailCount",
                "Tiredness",
                "OfficerKillCount",
                "InjureRate",
                "BattleSelfDamage",
                "NumberOfChildren",
                "Tags",
                "marriageGranter",
                "TempLoyaltyChange",
                "BelongedCaptive",
                "BelongedPersonName",
                "CombatTitleString",
                "CommandPotential",
                "GlamourPotential",
                "IntelligencePotential",
                "IsGeneratedChildren",
                "LastOutsideTask",
                "ManualStudy",
                "PersonalTitleString",
                "PoliticsPotential",
                "ReturnedDaySince",
                "RewardFinished",
                "StrengthPotential",
                "TrainPolicyIDString",
                "firstPreferred",
                "shoudongsousuo",
                "wasMayor",
            };
        }
        private string[] 生成人物取值字段()
        {
            return new string[]
            {
                "ID",
                "bornLo",
                "bornHi",
                "debutLo",
                "debutHi",
                "dieLo",
                "dieHi",
                "femaleChance",
                "debutAtLeast",
                "ChildrenFemaleChance",
            };
        }
        private string[] 生成人物类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "generationChance",
                "commandLo",
                "commandHi",
                "strengthLo",
                "strengthHi",
                "intelligenceLo",
                "intelligenceHi",
                "politicsLo",
                "politicsHi",
                "glamourLo",
                "glamourHi",
                "braveLo",
                "braveHi",
                "calmnessLo",
                "calmnessHi",
                "personalLoyaltyLo",
                "personalLoyaltyHi",
                "ambitionLo",
                "ambitionHi",
                "titleChance",
                "affectedByRateParameter",
                "TypeCount",
                "FactionLimit",
                "CostFund",
            };
        }
        private string[] 人物关系取值字段()
        {
            return new string[]
            {
                "Person1",
                "Person2",
                "Relation",
            };
        }
        private string[] 地域取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "StatesListString",
                "RegionCoreID",
            };
        }
        private string[] 粮道取值字段()
        {
            return new string[]
            {
                "ID",
                "Building",
                "ShowArea",
                "RemoveAfterClose",
                "LastActivePointIndex",
                "InefficiencyDays",
                "StartArchitectureString",
                "EndArchitectureString",
                "DestinationArchitectureString",
                "RoutePoints",
            };
        }
        private string[] 军区战略取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Description",
                "OrientationKind",
                "AutoRun",
                "ValueAgriculture",
                "ValueCommerce",
                "ValueTechnology",
                "ValueDomination",
                "ValueMorale",
                "ValueEndurance",
                "ValueTraining",
                "ValueRecruitment",
                "ValueNewMilitary",
                "ValueOffensiveCampaign",
                "AllowInvestigateTactics",
                "AllowOffensiveTactics",
                "AllowPersonTactics",
                "AllowOffensiveCampaign",
                "AllowFundTransfer",
                "AllowFoodTransfer",
                "AllowMilitaryTransfer",
                "AllowFacilityRemoval",
                "AllowNewMilitary",
            };
        }
        private string[] 军区取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "AIDetailIDString",
                "OrientationFactionID",
                "OrientationSectionID",
                "OrientationStateID",
                "OrientationArchitectureID",
                "ArchitecturesString",
            };
        }
        private string[] 技能取值字段()
        {
            return new string[]
            {
                "ID",
                "DisplayRow",
                "DisplayCol",
                "Kind",
                "Level",
                "Combat",
                "Name",
                "Description",
                "Prerequisite",
                "InfluencesString",
                "ConditionTableString",
                "GenerationChance",
                "RelatedAbility",
            };
        }
        private string[] 州域取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "ContactStatesString",
                "StateAdminID",
            };
        }
        private string[] 计略取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Description",
                "Combativity",
                "Chance",
                "TechniquePoint",
                "Friendly",
                "Self",
                "AnimationKind",
                "InfluencesString",
                "CastDefaultString",
                "CastTargetString",
                "ArchitectureTarget",
                "RequireInfluenceToUse",
                "CastConditionsString",
                "AIConditionWeightSelfString",
                "AIConditionWeightEnemyString",
            };
        }
        private string[] 特技取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Combativity",
                "Period",
                "Animation",
                "InfluencesString",
                "CastConditionsString",
                "LearnConditionsString",
                "GenerateConditionsString",
                "AIConditionsString",
                "GenerationChance",
                "RelatedAbility",
            };
        }
        private string[] 科技取值字段()
        {
            return new string[]
            {
                "ID",
                "Kind",
                "DisplayRow",
                "DisplayCol",
                "Name",
                "Description",
                "PreID",
                "PostID",
                "Reputation",
                "FundCost",
                "PointCost",
                "Days",
                "InfluencesString",
                "ConditionTableString",
                "AIConditionWeightString",
            };
        }
        private string[] 地形取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "GraphicLayer",
                "ViewThrough",
                "RoutewayBuildFundCost",
                "RoutewayActiveFundCost",
                "RoutewayBuildWorkCost",
                "RoutewayConsumptionRate",
                "FoodDeposit",
                "FoodRegainDays",
                "FoodSpringRate",
                "FoodSummerRate",
                "FoodAutumnRate",
                "FoodWinterRate",
                "FireDamageRate",
            };
        }
        private string[] 个性语言类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Description",
            };
        }
        private string[] 个性语言取值字段()
        {
            return new string[]
            {
                "ID",
                "Person",
                "Kind",
                "Messages",
            };
        }
        private string[] 动画取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "FrameCount",
                "StayCount",
                "Back",
                "TextureWidth",
                "TextureHeight",
            };
        }
        private string[] 称号取值字段()
        {
            return new string[]
            {
                "ID",
                "Kind",
                "Level",
                "Combat",
                "Name",
                "Description",
                "Prerequisite",
                "InfluencesString",
                "ConditionTableString",
                "ArchitectureConditionsString",
                "FactionConditionsString",
                "GenerateConditionsString",
                "LoseConditionsString",
                "AutoLearn",
                "AutoLearnText",
                "AutoLearnTextByCourier",
                "MapLimit",
                "FactionLimit",
                "InheritChance",
                "GenerationChance",
                "RelatedAbility",
                "ManualAward",
            };
        }
        private string[] 称号类型取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Combat",
                "StudyDay",
                "SuccessRate",
                "Recallable",
                "RandomTeachable",
            };
        }
        private string[] 培育方针取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Description",
                "Command",
                "Strength",
                "Intelligence",
                "Politics",
                "Glamour",
                "Skill",
                "Stunt",
                "Title",
            };
        }
        private string[] 宝物取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Pic",
                "Worth",
                "Available",
                "HidePlaceIDString",
                "TreasureGroup",
                "AppearYear",
                "BelongedPersonIDString",
                "InfluencesString",
                "Description",
            };
        }
        private string[] 部队取值字段()
        {
            return new string[]
            {
                "ID",
                "LeaderIDString",
                "Controllable",
                "Status",
                "Direction",
                "Auto",
                "Operated",
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
        private string[] 部队动画取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "FrameCount",
                "StayCount",
            };
        }
        private string[] 部队事件取值字段()
        {
            return new string[]
            {
                "ID",
                "Name",
                "Happened",
                "Repeatable",
                "AfterEventHappened",
                "LaunchPersonString",
                "ConditionsString",
                "HappenChance",
                "CheckArea",
                "TargetPersonsString",
                "Dialogs",
                "SelfEffectsString",
                "EffectPersonsString",
                "EffectAreasString",
                "Image",
                "Sound",
            };
        }
        private string[] 部队事件效果取值字段()
        {
            return new string[]
            {
                "ID",
                "Kind",
                "Name",
                "Parameter",
            };
        }
        private string[] 年表取值字段()
        {
            return new string[]
            {
                "ID",
                "Date",
                "FactionsString",
                "Content",
                "IsGloballyKnown",
            };
        }
        private string[] Para参数取值字段()
        {
            return new string[]
            {
                "AIArchitectureDamageRate",
                "AbilityExperienceRate",
                "AIAntiStratagem",
                "AIAntiStratagemIncreaseRate",
                "AIAntiSurround",
                "AIAntiSurroundIncreaseRate",
                "AIArchitectureDamageYearIncreaseRate",
                "AIArmyExperienceRate",
                "AIArmyExperienceYearIncreaseRate",
                "AIAttackChanceIfUnfull",
                "AIBackendArmyReserveAdd",
                "AIBackendArmyReserveAmbitionMultiply",
                "AIBackendArmyReserveCalmBraveDifferenceMultiply",
                "AIBackendArmyReserveMultiply",
                "AIBuildHougongMaxSizeAdd",
                "AIBuildHougongSkipSizeChance",
                "AIBuildHougongSpaceBuiltProbWeight",
                "AIBuildHougongUnambitionProbWeight",
                "AIChongxingChanceAdd",
                "AIChongxingChanceMultiply",
                "AIEncirclePlayerRate",
                "AIEncircleRank",
                "AIEncircleVar",
                "AIExecuteMaxUncreulty",
                "AIExecutePersonIdealToleranceMultiply",
                "AIExtraPerson",
                "AIExtraPersonIncreaseRate",
                "AIFacilityDestroyValueRate",
                "AIFacilityFundMonthWaitParam",
                "AIFoodRate",
                "AIFoodYearIncreaseRate",
                "AIFundRate",
                "AIFundYearIncreaseRate",
                "AIGiveTreasureMaxWorth",
                "AIHougongArchitectureCountProbMultiply",
                "AIHougongArchitectureCountProbPower",
                "AINafeiAbilityThresholdRate",
                "AINafeiMaxAgeThresholdAdd",
                "AINafeiMaxAgeThresholdMultiply",
                "AINafeiSkipChanceAdd",
                "AINafeiSkipChanceMultiply",
                "AINafeiStealSpouseThresholdRateAdd",
                "AINafeiStealSpouseThresholdRateMultiply",
                "AINafeiUncreultyProbAdd",
                "AINewMilitaryPersonThresholdDivide",
                "AINewMilitaryPopulationThresholdDivide",
                "AIObeyStrategyTendencyChance",
                "AIOffendDefendingTroopRate",
                "AIOffendDefendTroopAdd",
                "AIOffendDefendTroopMultiply",
                "AIOffendIgnoreReserveChanceTroopRatioAdd",
                "AIOffendIgnoreReserveChanceTroopRatioMultiply",
                "AIOffendIgnoreReserveProbAmbitionAdd",
                "AIOffendIgnoreReserveProbAmbitionMultiply",
                "AIOffendIgnoreReserveProbBCDiffAdd",
                "AIOffendIgnoreReserveProbBCDiffMultiply",
                "AIOffendMaxDiplomaticRelationMultiply",
                "AIOffendReserveAdd",
                "AIOffendReserveBCDiffMultiply",
                "AIOfficerExperienceRate",
                "AIOfficerExperienceYearIncreaseRate",
                "AIRecruitmentSpeedRate",
                "AIRecruitmentSpeedYearIncreaseRate",
                "AIRecruitPopulationCapBackendMultiply",
                "AIRecruitPopulationCapHostilelineMultiply",
                "AIRecruitPopulationCapMultiply",
                "AIRecruitPopulationCapStrategyTendencyAdd",
                "AIRecruitPopulationCapStrategyTendencyMulitply",
                "AITirednessDecrease",
                "AITradePeriod",
                "AITrainingSpeedRate",
                "AITrainingSpeedYearIncreaseRate",
                "AITreasureChance",
                "AITreasureCountCappedTitleLevelAdd",
                "AITreasureCountCappedTitleLevelMultiply",
                "AITreasureCountMax",
                "AITroopDefenceRate",
                "AITroopDefenceYearIncreaseRate",
                "AITroopOffenceRate",
                "AITroopOffenceYearIncreaseRate",
                "AIUniqueTroopFightingForceThreshold",
                "ArchitectureDamageRate",
                "ArmyExperienceRate",
                "AutoLearnSkillSuccessRate",
                "AutoLearnStuntSuccessRate",
                "BasicAIExtraPerson",
                "BuyFoodAgriculture",
                "ChangeCapitalCost",
                "ClearFieldAgricultureCostUnit",
                "ClearFieldFundCostUnit",
                "CloseAbilityRate",
                "CloseThreshold",
                "ConvincePersonCost",
                "DayInTurn",
                "DefaultPopulationDevelopingRate",
                "DestroyArchitectureCost",
                "ExpandConditions",
                "FindTreasureChance",
                "FireDamageScale",
                "FireSpreadProbMultiply",
                "FireStayProb",
                "FollowedLeaderDefenceRateIncrement",
                "FollowedLeaderOffenceRateIncrement",
                "FoodRate",
                "FoodToFundDivisor",
                "FundRate",
                "FundToFoodMultiple",
                "GossipArchitectureCost",
                "HateThreshold",
                "HireNoFactionPersonCost",
                "InstigateArchitectureCost",
                "InternalExperienceRate",
                "InternalFundCost",
                "InternalRate",
                "InternalSurplusFactor",
                "InternalSurplusMinEffect",
                "JailBreakArchitectureCost",
                "LearnSkillDays",
                "LearnSkillSuccessRate",
                "LearnStuntDays",
                "LearnStuntSuccessRate",
                "LearnTitleDays",
                "LearnTitleSuccessRate",
                "MakeMarriageCost",
                "MakeMarrigeIdealLimit",
                "MaxAITroopCountCandidates",
                "MilitaryPopulationCap",
                "MilitaryPopulationReloadQuantity",
                "MinPregnantProb",
                "NafeiCost",
                "PopulationDevelopingRate",
                "PrincessMaintainenceCost",
                "RansomRate",
                "RecruitmentDomination",
                "RecruitmentFundCost",
                "RecruitmentMorale",
                "RecruitmentRate",
                "RetainFeiziPersonalLoyalty",
                "RewardPersonCost",
                "SearchDays",
                "SearchPersonArchitectureCountPower",
                "SelectPrinceCost",
                "SellFoodCommerce",
                "SendSpyCost",
                "SurroundArchitectureDominationUnit",
                "TrainingRate",
                "TransferCostPerMilitary",
                "TransferFoodPerMilitary",
                "TroopDamageRate",
                "VeryCloseAbilityRate",
                "VeryCloseThreshold",
            };
        }
        private string[] Glob参数取值字段()
        {
            return new string[]
            {
                "AdditionalPersonAvailable",
                "AIAutoTakeNoFactionCaptives",
                "AIAutoTakeNoFactionPerson",
                "AIAutoTakePlayerCaptiveOnlyUnfull",
                "AIAutoTakePlayerCaptives",
                "AIExecuteBetterOfficer",
                "AIExecutionRate",
                "AIMergeAgainstPlayer",
                "AINoTeamTransfer",
                "AIQuickBattle",
                "AIZhaoxianFixIdeal",
                "ArmyPopulationCap",
                "AutoSaveFrequency",
                "CalculateAverageCostOfTiers",
                "ChildrenAbilityFactor",
                "ChildrenAvailableAge",
                "CommonPersonAvailable",
                "createChildren",
                "createChildrenIgnoreLimit",
                "CreatedOfficerAbilityFactor",
                "CreateRandomOfficerChance",
                "CurrentMapLayer",
                "DialogShowTime",
                "doAutoSave",
                "DrawMapVeil",
                "DrawTroopAnimation",
                "EnableAgeAbilityFactor",
                "EnableCheat",
                "EnableExtensions",
                "EnablePersonRelations",
                "EnableResposiveThreading",
                "EncryptSave",
                "FactionMilitaryLimt",
                "FactionRunningTicksLimitInOneFrame",
                "FastBattleSpeed",
                "FixedUnnaturalDeathAge",
                "FriendlyDiplomacyThreshold",
                "FullScreen",
                "GameDifficulty",
                "getChildrenRate",
                "getRaisedSoliderRate",
                "HardcoreMode",
                "HintPopulation",
                "HintPopulationUnder1000",
                "hougongGetChildrenRate",
                "IdealTendencyValid",
                "IgnoreStrategyTendency",
                "internalSurplusRateForAI",
                "internalSurplusRateForPlayer",
                "LandArmyCanGoDownWater",
                "LeadershipOffenceRate",
                "LiangdaoXitong",
                "LoadBackGroundMapTexture",
                "lockChildrenLoyalty",
                "MapScrollSpeed",
                "MaxAbility",
                "MaxCountOfKnownPaths",
                "maxExperience",
                "MaxMilitaryExperience",
                "MaxTimeOfAnimationFrame",
                "MilitaryKindSpeedValid",
                "MultipleResource",
                "NoHintOnSmallFacility",
                "OfficerChildrenLimit",
                "OfficerDieInBattleRate",
                "PermitFactionMerge",
                "PermitManualAwardTitleAutoLearn",
                "PermitQuanXiang",
                "PersonDieInChallenge",
                "PersonNaturalDeath",
                "PinPointAtPlayer",
                "PlayBattleSound",
                "PlayerAutoSectionHasAIResourceBonus",
                "PlayerPersonAvailable",
                "PlayerZhaoxianFixIdeal",
                "PlayMusic",
                "PlayNormalSound",
                "PopulationRecruitmentLimit",
                "ProhibitFactionAgainstDestroyer",
                "RemoveSpouseIfNotAvailable",
                "RoutewayInformationLevel",
                "RunWhileNotFocused",
                "ScoutRoutewayInformationLevel",
                "ShowChallengeAnimation",
                "ShowGrid",
                "SingleSelectionOneClick",
                "SkyEye",
                "SkyEyeSimpleNotification",
                "StopToControlOnAttack",
                "SurroundFactor",
                "TabListDetailLevel",
                "TechniquePointMultiple",
                "TirednessDecrease",
                "TirednessIncrease",
                "TroopMoveFrameCount",
                "TroopMoveLimitOnce",
                "TroopMoveSpeed",
                "TroopTirednessDecrease",
                "WujiangYoukenengDuli",
                "zainanfashengjilv",
                "zhaoxianOfficerMax",
                "ZhaoXianSuccessRate",
            };
        }
        private string[] 存档取值字段()
        {
            return new string[]
            {
                "Save00",
                "Save01",
                "Save02",
                "Save03",
                "Save04",
                "Save05",
                "Save06",
                "Save07",
                "Save08",
                "Save09",
                "Save10",
            };
        }
        private string 人物列表(PersonList l)
        {
            String s = "";
            foreach (Person p in l)
            {
                s += p.ID + " ";
            }
            return s;
        }

        private void 定时信息框(string 内容)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Tick += new System.EventHandler(timer1_Tick);
            timer.Interval =3000;//3000毫秒
            timer.Start();
            MessageBox.Show(内容, "此窗口3秒后自动关闭");
        }

        private static void LoadFromString(GameDate gameDate, string gamedatestring)
        {
            char[] separator = new char[] { ' ', '年', '月',  '日', '\n', '\r', '\t' };
            string[] strArray = gamedatestring.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 3)
            {
                gameDate.Year = int.Parse(strArray[0]);
                gameDate.Month = int.Parse(strArray[1]);
                gameDate.Day = int.Parse(strArray[2]);
            }
        }

        private static Microsoft.Xna.Framework.Point LoadFromString(Microsoft.Xna.Framework.Point p,string pointstring)
        {
            char[] separator = new char[] { ' ','{','}', 'X', 'Y', ':', '\n', '\r', '\t' };
            string[] strArray = pointstring.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length == 2)
            {
                return new Microsoft.Xna.Framework.Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
            }
            return new Microsoft.Xna.Framework.Point();
        }
        
        private  static List<Microsoft.Xna.Framework.Point> LoadFromString(List<Microsoft.Xna.Framework.Point> pointList, string pointstring)
        {
            char[] separator = new char[] { ' ',',', '{', '}', 'X', 'Y', ':', '\n', '\r', '\t' };
            string[] strArray = pointstring.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            pointList = new List<Microsoft.Xna.Framework.Point>();
            for (int i = 0; i < strArray.Length; i += 2)
            {
                pointList.Add(new Microsoft.Xna.Framework.Point(int.Parse(strArray[i]), int.Parse(strArray[i + 1])));
            }
            return pointList;
        }
        private static int[] LoadFromString(int[] aaa, string 文本)
        {
            char[] separator = new char[] { ' ', '{', '}', ',', '\n', '\r', '\t' };
            string[] strArray = 文本.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            aaa = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++)
            {
                aaa[i] = int.Parse(strArray[i]);
            }
            return aaa;
        }

        private static List<int>  LoadFromString(List<int> aaa, string 文本)
        {
            char[] separator = new char[] { ' ', '{', '}', ',', '\n', '\r', '\t' };
            string[] strArray = 文本.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            aaa = new List<int>();
            for (int i = 0; i < strArray.Length; i++)
            {
                aaa.Add( int.Parse(strArray[i]));
            }
            return aaa;
        }

        private static string[] LoadFromString(string 文本)
        {
            char[] separator = new char[] { ' ', '{', '}',',', '\n', '\r', '\t' };
            string[] strArray = 文本.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length > 0)
            {
                return strArray;
            }
            else return new string[] { };
        }

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        public const int WM_CLOSE = 0x10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            IntPtr ptr = FindWindow(null, "此窗口3秒后自动关闭");
            if (ptr != IntPtr.Zero )
            {     //找到则关闭MessageBox窗口     
                PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            }
            ((Timer)sender).Stop();
        }
    }
}
