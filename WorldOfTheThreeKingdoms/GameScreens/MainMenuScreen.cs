using GameManager;
using GameObjects;
using GamePanels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tools;

namespace WorldOfTheThreeKingdoms.GameScreens
{
    public enum MenuType
    {
        None,
        New,
        Config,
        Save,
        Setting,
        About,
        Start
    }

    public class MainMenuScreen
    {
        public static MainMenuScreen Current = null;

        public MenuType MenuType = MenuType.None;

        float textGameElapsed = 0f;

        float menuTypeElapsed = 0f;

        float startElapsed = 0f;

        float startCircle = 15f;

        bool forward = true;

        Vector2 startPos = Vector2.Zero;

        bool isClosing = false;

        List<ButtonTexture> btList = null;

        List<ButtonTexture> btScenarioList = null;

        List<ButtonTexture> btSaveList = null;

        List<ButtonTexture> btSettingList = null;

        List<LinkButton> lbSettingList = null;

        List<ButtonTexture> btConfigList1 = null;

        List<ButtonTexture> btConfigList2 = null;

        List<ButtonTexture> btConfigList3 = null;

        List<ButtonTexture> btConfigList4 = null;

        ButtonTexture btPre, btNext, btPre1, btNext1;

        int pageIndex = 1;
        int pageCount = 0;
        int pageid = 1;
        int page = 0;

        int pageIndex1 = 1;
        int pageCount1 = 0;
        int pageid1 = 1;
        int page1 = 0;

        List<Scenario> ScenarioList = new List<Scenario>();

        Scenario CurrentScenario = null;

        List<Scenario> ScenarioSaveList = new List<Scenario>();

        List<ButtonTexture> btScenarioSelectList = new List<ButtonTexture>();
        List<ButtonTexture> btScenarioSelectListPaged = new List<ButtonTexture>();

        List<ButtonTexture> btScenarioPlayersList = new List<ButtonTexture>();
        List<ButtonTexture> btScenarioPlayersListPaged = new List<ButtonTexture>();

        string CurrentSetting = "基本";

        string[] hards1 = new string[] { "beginner", "easy", "normal", "hard", "veryhard", "custom" };
        string[] hards2 = new string[] { "入门", "初级", "上级", "超级", "修罗", "自订" };

        string[] AvaliableResolutions = new string[] { "1024*768", "1280*720", "1368*768", "1440*900", "1920*1080" };

        //private bool doNotSetDifficultyToCustom = false;

        NumericSetTextureF nstMusic, nstSound;

        NumericSetTextureF nstArmySpeed, nstDialogTime, nstBattleSpeed, nstAutoSaveTime;

        NumericSetTextureF nstViewDetail, nstGeneralBattleDead, nstGeneralYun, nstFeiZiYun, nstZhaoXian, nstSearchGen, nstZaiNan, nstDayInTurn;

        NumericSetTextureF nstMaxExperience, nstMaxSkill, nstPiLeiUp, nstPiLeiDown, nstExperienceUp, nstTreasureDiscover;

        NumericSetTextureF nstFollowAttachPlus, nstFollowDefencePlus, nstSearchTime, nstGeneralChildsMax, nstGeneralChildsAppear, nstGeneralChildsSkill, nstChildsSkill, nstForbiddenDays;

        NumericSetTextureF nstNeiZheng, nstXunLian, nstBuChong, nstZiJing, nstLiangCao, nstBuDui, nstJianZhu, nstRenKou, nstWeiCheng, nstHuoYan,
                          nstMaiLiangNongYe, nstMaiLiangShangYe, nstZiJingHuanLiang, nstLiangCaoHuanZiJing, nstJiqiaoDian, nstNeiZhengZiJing, nstBuChongZiJing, nstBuChongTongZhi, nstBuChongMinXin, nstQianDuZiJing,
                          nstShuoFuZiJing, nstBaoJiangZiJing, nstJieLaoZiJing, nstPoHuaiZiJing, nstShanDongZiJing, nstLiuYanZiJing, nstBingYiShangXian, nstBingYiZengLiang, nstBuDuiJingYan, nstTongShuaiGongJi;

        NumericSetTextureF nstDianNaoChuZhan, nstDianNaoShengTao, nstDianNaoYinWanJiaHeBing,
            nstDianNaoZiJing1, nstDianNaoZiJing2, nstDianNaoLiangCao1, nstDianNaoLiangCao2, nstDianNaoBuDuiGongJi1, nstDianNaoBuDuiGongJi2,
            nstDianNaoFangYu1, nstDianNaoFangYu2, nstDianNaoShangHai1, nstDianNaoShangHai2, nstDianNaoXunLian1, nstDianNaoXunLian2,
            nstDianNaoZhengBing1, nstDianNaoZhengBing2, nstDianNaoWuJiangJingYan1, nstDianNaoWuJiangJingYan2, nstDianNaoBuDuiJingYan1, nstDianNaoBuDuiJingYan2,
            nstDianNaoKangJi1, nstDianNaoKangJi2, nstDianNaoKangWei1, nstDianNaoKangWei2, nstDianNaoEWai1, nstDianNaoEWai2;

        TextBox tbGamerName = null;
        TextBox tbBattleSpeed = null;

        ButtonTexture btnSmallResolution = null;
        ButtonTexture btnLargeResolution = null;

        //public ButtonTexture btnTextureAlpha = null;

        List<ButtonTexture> cbAIHardList = null;

        private int AIEncircleRank = 0;
        private int AIEncircleVar = 0;

        List<ButtonTexture> btAboutList = null;

        int textLevel = 0;

        string[] texts = new string[]
        {
        };

        string[] aboutLines = new string[]
        {
            String.Format("中华三国志"),
"游戏的更新与讨论请关注:",
"官方论坛:http://www.zhsan.com ",
"百度贴吧:中华三国志吧",
"订阅号:中三之家（zhsan_com）",
"",
"原作者：Clip_on",
"负责人：耒戈氏",
"执行秘书：一世恋",
"",
"程序制作：,",
"耒戈氏  45399735  kpxp  月落乌江  兔巴哥(手机版移植)   majorcheng(外交) breamask(单挑)  我勒个去(其他)  最爱艾氏兄弟  YPZhou(程序优化)",
"",
"美工UI模型：",
"caibao2009（地图）  酷热7(地图、UI)  月落乌江(地图)  sgb6700tt(界面)  Ftian80(头像)  无限时段(粮道) PTMASTER酱(帅旗)   睿哥（头像）  常山（兵模、宣传图） ",
            " 黄金魔导师（地图兵摸UI）    古月（宣传图）",
"",
"剧本：",
"zwm213  清风凉亭  yesf  lakina006  amisuzuki  yesf  酷热7  粒粒橙  o哀之咏叹调o  髀里肉生o0  一身是胆  一世恋  caibao2009  哈哈哈呵呵呵  helokero13 ",
            " yuanhouzi123(称号)  熊家依若(武将)  黑翼喵  郭文斌(兵种)  asmz2002(影响文字)  枫舞影(对话文字)  余襄子（传记） 燕返（测试）",
"",
"宣传运营：",
"一世恋（论坛、steam上架、游侠）  酷热7（论坛）  郭文斌（论坛）  陸陸大顺(论坛)  阿伍（论坛、贴吧、qq群、steam社区）  米卡柳丁（订阅号）  天青色等煙雨（TAPTAP）",
            "  赵凤、二娃子、李存孝（qq群）  快乐自逍遥（贴吧）",
"",
"音乐音效配音",
"一丝不狗  洛书  ciaos597  欧阳冰凌  四步花开  虚君  易萧",
"特别鸣谢《嗜血印》之艺龙李叔支持的音乐音效",
"特别鸣谢《炎黄战纪》支持的音乐音效",
"",
"视频动画制作",
"欧阳小锤（开始动画）  秋木萋萋（开始动画）  ",
"",
"该游戏为学习交流之用，游戏制作者对其所制作部分分别享有版权，游戏中图片、音乐等资源版权为原制作团队所有。未经许可禁止把该游戏用作商业用途。,",
"",
"感谢广大网友的支持！"

        };

        int currentStartVersion = 1;
        string[] startLines = new string[]
        {
"新人教程已经发布，在商店页面右侧的游戏手册下载使用。欢迎各位新伙伴学习了解。配了图文，很细致了，相信愿意玩的伙伴可以入门了。因为有大量图片，",
"文件有20M，请耐心下载了解。请各位伙伴先阅读此文章，再发表评论和疑问。",
"",
"另外游戏无法启动问题已经解决（已经设置默认要求安装软件环境）",
"",
"祝大家玩的开心！",
            "",
            "-----------------------------------",
            "中华三国志(v1.2.1)更新",
            "修正假想劇本裡，一開局武將大量死亡",
            "新增教程劇本",
            "新增遊戲開始公告",
            "編輯器標題新增中文說明",
            "調整暗殺公式，可以對在野武將進行，有可能捕獲武將",
            "增加或、否定條件項",
            "增加PNG圖片Premultiply及PremultiplyAlpha功能 (by 兔巴哥)",
        };

        string message = "";

        public MainMenuScreen()
        {
            if (Platform.Current.UserFileExist(new string[] { "startRead.txt" })[0])
            {
                string content = Platform.Current.GetUserText("startRead.txt");
                int version;
                int.TryParse(content, out version);
                if (version >= currentStartVersion)
                {
                    MenuType = MenuType.None;
                }
                else
                {
                    MenuType = MenuType.Start;
                }
            }
            else
            {
                MenuType = MenuType.Start;
            }
            Platform.Current.SaveUserFile("startRead.txt", currentStartVersion.ToString());
            //None

            Current = this;

            btList = new List<ButtonTexture>() { };

            var btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "New", new Vector2(100, 600));
            btOne.OnButtonPress += (sender, e) =>
            {
                menuTypeElapsed = 0f;
                UnCheckTextBoxs();
                MenuType = MenuType.New;
                InitScenarioList();
            };
            btList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "Save", new Vector2(310, 600));
            btOne.OnButtonPress += (sender, e) =>
            {
                menuTypeElapsed = 0f;
                UnCheckTextBoxs();
                MenuType = MenuType.Save;
                InitScenarioSaveList();
            };
            btList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "Setting", new Vector2(520, 600));
            btOne.OnButtonPress += (sender, e) =>
            {
                menuTypeElapsed = 0f;
                UnCheckTextBoxs();
                MenuType = MenuType.Setting;
            };
            btList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "About", new Vector2(730, 600));
            btOne.OnButtonPress += (sender, e) =>
            {
                menuTypeElapsed = 0f;
                UnCheckTextBoxs();
                MenuType = MenuType.About;
            };
            btList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "Exit", new Vector2(940, 600));
            btOne.OnButtonPress += (sender, e) =>
            {
                Platform.Current.Exit();
            };
            if (Platform.PlatFormType == PlatFormType.iOS || Platform.PlatFormType == PlatFormType.UWP)
            {
                btOne.Visible = false;
            }
            btList.Add(btOne);

            //New

            btScenarioList = new List<ButtonTexture>();
            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\ReadyButton", "OK", new Vector2(800 + 220, 615));
            btOne.OnButtonPress += (sender, e) =>
            {
                //SoundPlayer player = new SoundPlayer("Content/Textures/Resources/SaveSelect/Open");
                //player.Play();

                Platform.Current.PlayEffect(@"Content\Sound\Open");

                if (CurrentScenario == null)
                {
                    message = "请选择剧本。";
                }
                else
                {
                    if (MenuType == MenuType.New)
                    {
                        Session.globalVariablesTemp = Session.globalVariablesBasic.Clone();
                        Session.parametersTemp = Session.parametersBasic.Clone();
                        InitConfig();
                        MenuType = MenuType.Config;
                    }
                    else
                    {
                        MenuType = MenuType.New;
                        Session.StartScenario(CurrentScenario, false);
                    }
                }

            };
            btScenarioList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\ReadyButton", "Cancel", new Vector2(800, 615));
            btOne.OnButtonPress += (sender, e) =>
            {
                if (MenuType == MenuType.New)
                {
                    isClosing = true;
                    menuTypeElapsed = 0.5f;
                }
                else
                {
                    MenuType = MenuType.New;
                }
            };
            btScenarioList.Add(btOne);

            btSaveList = new List<ButtonTexture>();
            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\ReadyButton", "Load", new Vector2(800 + 220, 615));
            btOne.OnButtonPress += (sender, e) =>
            {
                //SoundPlayer player = new SoundPlayer("Content/Textures/Resources/SaveSelect/Open");
                //player.Play();

                Platform.Current.PlayEffect(@"Content\Sound\Open");

                if (CurrentScenario == null)
                {
                    message = "请选择存档。";
                }
                else
                {
                    Session.StartScenario(CurrentScenario, true);
                }
            };
            btSaveList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\ReadyButton", "Cancel", new Vector2(800, 615));
            btOne.OnButtonPress += (sender, e) =>
            {
                isClosing = true;
                menuTypeElapsed = 0.5f;
            };
            btSaveList.Add(btOne);

            lbSettingList = new List<LinkButton>();
            var lbOne = new LinkButton("基本")
            {
                Position = new Vector2(50, 40)
            };
            lbOne.OnButtonPress += (sender, e) =>
            {
                CurrentSetting = "基本";
                UnCheckTextBoxs();
            };
            lbSettingList.Add(lbOne);

            lbOne = new LinkButton("人物")
            {
                Position = new Vector2(50 + 100 * 1, 40)
            };
            lbOne.OnButtonPress += (sender, e) =>
            {
                CurrentSetting = "人物";
                UnCheckTextBoxs();
            };
            lbSettingList.Add(lbOne);

            lbOne = new LinkButton("参数")
            {
                Position = new Vector2(50 + 100 * 2, 40)
            };
            lbOne.OnButtonPress += (sender, e) =>
            {
                CurrentSetting = "参数";
                UnCheckTextBoxs();
            };
            lbSettingList.Add(lbOne);

            lbOne = new LinkButton("电脑")
            {
                Position = new Vector2(50 + 100 * 3, 40)
            };
            lbOne.OnButtonPress += (sender, e) =>
            {
                CurrentSetting = "电脑";
                UnCheckTextBoxs();
            };
            lbSettingList.Add(lbOne);

            btSettingList = new List<ButtonTexture>();
            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\ReadyButton", "Cancel", new Vector2(800 + 220, 615));
            btOne.OnButtonPress += (sender, e) =>
            {
                UnCheckTextBoxs();

                Setting.Save();

                isClosing = true;
                menuTypeElapsed = 0.5f;
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150, 115))
            {
                ID = "简体中文"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                Setting.Current.Language = "cn";
                Session.LoadFont(Setting.Current.Language);
                InitSetting();
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150 + 200, 115))
            {
                ID = "传统中文"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                Setting.Current.Language = "tw";
                Session.LoadFont(Setting.Current.Language);
                InitSetting();
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150, 115 + 60))
            {
                ID = "窗口模式"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                Setting.Current.DisplayMode = "Window";
                InitSetting();
                Platform.Current.SetFullScreen(false);
                Platform.GraphicsApplyChanges();
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150 + 200, 115 + 60))
            {
                ID = "全屏模式"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                Setting.Current.DisplayMode = "Full";
                InitSetting();
                Platform.Current.SetFullScreen(true);
                Platform.GraphicsApplyChanges();
            };
            btSettingList.Add(btOne);


            int heightBase = 90;

            int height = 85;

            int left1 = 50;

            int left2 = 50 + 620;

            btConfigList1 = new List<ButtonTexture>();

            /*
            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase))
            {
                ID = "LiangDao"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.LiangdaoXitong = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.LiangdaoXitong = true;
                }
            };
            btConfigList1.Add(btOne);
            */

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 0.0f))
            {
                ID = "ChuShi"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.IdealTendencyValid = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.IdealTendencyValid = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 0.5f))
            {
                ID = "BuDuiSuLv"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.MilitaryKindSpeedValid = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.MilitaryKindSpeedValid = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 1.0f))
            {
                ID = "DanTiaoSiWang"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.PersonDieInChallenge = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.PersonDieInChallenge = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 1.5f))
            {
                ID = "NianLingYouXiao"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.PersonNaturalDeath = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.PersonNaturalDeath = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 2.0f))
            {
                ID = "NianLingYingXiang"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.EnableAgeAbilityFactor = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.EnableAgeAbilityFactor = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 2.5f))
            {
                ID = "WuJiangDuli"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.WujiangYoukenengDuli = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.WujiangYoukenengDuli = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 3.0f))
            {
                ID = "ShiLiHeBing"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.PermitFactionMerge = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.PermitFactionMerge = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 3.5f))
            {
                ID = "RenKouXiaoYu"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.PopulationRecruitmentLimit = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.PopulationRecruitmentLimit = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 4.0f))
            {
                ID = "KaiQiTianYan"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.SkyEye = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.SkyEye = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 4.5f))
            {
                ID = "hougongAlienOnly"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.hougongAlienOnly = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.hougongAlienOnly = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left2, heightBase + height * 0f))
            {
                ID = "KaiQiZuoBi"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.EnableCheat = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.EnableCheat = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left2, heightBase + height * 0.5f))
            {
                ID = "YingHeMoshi"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.HardcoreMode = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.HardcoreMode = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left2, heightBase + height * 1.0f))
            {
                ID = "ShengChengZiSi"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.createChildren = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.createChildren = true;
                }
            };
            btConfigList1.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left2, heightBase + height * 1.5f))
            {
                ID = "JianYiAI"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.AIQuickBattle = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.AIQuickBattle = true;
                }
            };
            btConfigList1.Add(btOne);

            nstViewDetail = new NumericSetTextureF(0, 9, 9, null, new Vector2(left2 + 300, heightBase + height * 2.0f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 5,
                Unit = 1
            };

            nstGeneralBattleDead = new NumericSetTextureF(0, 100, 100, null, new Vector2(left2 + 300, heightBase + height * 2.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstGeneralYun = new NumericSetTextureF(0, 300, 300, null, new Vector2(left2 + 300, heightBase + height * 3.0f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstFeiZiYun = new NumericSetTextureF(0, 300, 300, null, new Vector2(left2 + 300, heightBase + height * 3.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstZhaoXian = new NumericSetTextureF(0, 300, 300, null, new Vector2(left2 + 300, heightBase + height * 4.0f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstSearchGen = new NumericSetTextureF(0, 300, 300, null, new Vector2(left2 + 300, heightBase + height * 4.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstZaiNan = new NumericSetTextureF(0, 100000, 100000, null, new Vector2(left2 + 300, heightBase + height * 5.0f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 50000,
                Unit = 1000
            };

            nstDayInTurn = new NumericSetTextureF(1, 6, 6, null, new Vector2(left2 + 300, heightBase + height * 5.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 1,
                Unit = 1
            };

            btConfigList2 = new List<ButtonTexture>();

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 0f))
            {
                ID = "XuNiShangXian"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.createChildrenIgnoreLimit = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.createChildrenIgnoreLimit = true;
                }
            };
            btConfigList2.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 0.5f))
            {
                ID = "WuJiangGuanXi"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.EnablePersonRelations = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.EnablePersonRelations = true;
                }
            };
            btConfigList2.Add(btOne);

            nstMaxExperience = new NumericSetTextureF(1, 300000, 300000, null, new Vector2(left1 + 200, heightBase + height * 1f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 30000,
                Unit = 1000
            };

            nstMaxSkill = new NumericSetTextureF(1, 400, 400, null, new Vector2(left1 + 200, heightBase + height * 1.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 150,
                Unit = 1
            };

            nstPiLeiUp = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 2f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 1,
                Unit = 1
            };

            nstPiLeiDown = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 2.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 2,
                Unit = 1
            };

            nstExperienceUp = new NumericSetTextureF(0, 3, 3, null, new Vector2(left1 + 200, heightBase + height * 3f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 1,
                Unit = 0.1f
            };

            nstTreasureDiscover = new NumericSetTextureF(0, 100, 100, null, new Vector2(left1 + 200, heightBase + height * 3.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstFollowAttachPlus = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + 200, heightBase + height * 0f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 0.2f,
                Unit = 0.1f
            };

            nstFollowDefencePlus = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + 200, heightBase + height * 0.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 0.2f,
                Unit = 0.1f
            };

            nstSearchTime = new NumericSetTextureF(1, 30, 30, null, new Vector2(left2 + 200, heightBase + height * 1f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstGeneralChildsMax = new NumericSetTextureF(1, 100, 100, null, new Vector2(left2 + 200, heightBase + height * 1.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstGeneralChildsAppear = new NumericSetTextureF(1, 30, 30, null, new Vector2(left2 + 200, heightBase + height * 2f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstGeneralChildsSkill = new NumericSetTextureF(0, 3, 3, null, new Vector2(left2 + 200, heightBase + height * 2.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 1,
                Unit = 0.1f
            };

            nstChildsSkill = new NumericSetTextureF(0, 3, 3, null, new Vector2(left2 + 200, heightBase + height * 3f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 1,
                Unit = 0.1f
            };

            nstForbiddenDays = new NumericSetTextureF(0, 3, 3, null, new Vector2(left2 + 200, heightBase + height * 3.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            //参数

            left1 = left1 - 25;

            nstNeiZheng = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 0f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstXunLian = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 0.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstBuChong = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 1.0f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstZiJing = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 1.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstLiangCao = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 2.0f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstBuDui = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 2.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstJianZhu = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 3f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstRenKou = new NumericSetTextureF(0.000001f, 0.001f, 0.001f, null, new Vector2(left1 + 200, heightBase + height * 3.5f), true)
            {
                FloatNum = 5,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.00001f
            };

            nstWeiCheng = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 4f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstHuoYan = new NumericSetTextureF(0, 10, 10, null, new Vector2(left1 + 200, heightBase + height * 4.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            left2 = left1 + 400;

            nstMaiLiangNongYe = new NumericSetTextureF(1, 2000, 2000, null, new Vector2(left2 + 200, heightBase + height * 0f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstMaiLiangShangYe = new NumericSetTextureF(1, 2000, 2000, null, new Vector2(left2 + 200, heightBase + height * 0.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstZiJingHuanLiang = new NumericSetTextureF(1, 1000, 1000, null, new Vector2(left2 + 200, heightBase + height * 1.0f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstLiangCaoHuanZiJing = new NumericSetTextureF(1, 5000, 5000, null, new Vector2(left2 + 200, heightBase + height * 1.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstJiqiaoDian = new NumericSetTextureF(0, 3, 3, null, new Vector2(left2 + 200, heightBase + height * 2f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstNeiZhengZiJing = new NumericSetTextureF(1, 100, 100, null, new Vector2(left2 + 200, heightBase + height * 2.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstBuChongZiJing = new NumericSetTextureF(1, 100, 100, null, new Vector2(left2 + 200, heightBase + height * 3f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstBuChongTongZhi = new NumericSetTextureF(1, 100, 100, null, new Vector2(left2 + 200, heightBase + height * 3.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstBuChongMinXin = new NumericSetTextureF(1, 300, 300, null, new Vector2(left2 + 200, heightBase + height * 4f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            nstQianDuZiJing = new NumericSetTextureF(1, 30000, 30000, null, new Vector2(left2 + 200, heightBase + height * 4.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 100
            };

            int left3 = left2 + 400;

            nstShuoFuZiJing = new NumericSetTextureF(1, 1000, 1000, null, new Vector2(left3 + 200, heightBase + height * 0f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstBaoJiangZiJing = new NumericSetTextureF(1, 1000, 1000, null, new Vector2(left3 + 200, heightBase + height * 0.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstJieLaoZiJing = new NumericSetTextureF(1, 1000, 1000, null, new Vector2(left3 + 200, heightBase + height * 1f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstPoHuaiZiJing = new NumericSetTextureF(1, 1000, 1000, null, new Vector2(left3 + 200, heightBase + height * 1.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstShanDongZiJing = new NumericSetTextureF(1, 1000, 1000, null, new Vector2(left3 + 200, heightBase + height * 2f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstLiuYanZiJing = new NumericSetTextureF(1, 1000, 1000, null, new Vector2(left3 + 200, heightBase + height * 2.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 10
            };

            nstBingYiShangXian = new NumericSetTextureF(0, 1, 1, null, new Vector2(left3 + 200, heightBase + height * 3f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 1,
                Unit = 0.1f
            };

            nstBingYiZengLiang = new NumericSetTextureF(0, 10, 10, null, new Vector2(left3 + 200, heightBase + height * 3.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstBuDuiJingYan = new NumericSetTextureF(1000, 300000, 300000, null, new Vector2(left3 + 200, heightBase + height * 4f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 100
            };

            nstTongShuaiGongJi = new NumericSetTextureF(0, 1, 1, null, new Vector2(left3 + 200, heightBase + height * 4.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            left1 = 50;

            cbAIHardList = new List<ButtonTexture>();

            for (int i = 0; i < hards1.Length; i++)
            {
                var hard = hards1[i];

                btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left2 + 135 + 120 * i, 40))
                {
                    ID = hard
                };
                btOne.OnButtonPress += (sender, e) =>
                {
                    var bt = (ButtonTexture)sender;

                    if (bt.ID == "beginner")
                    {
                        this.setDifficultyParameters(Difficulty.beginner);
                    }
                    else if (bt.ID == "easy")
                    {
                        this.setDifficultyParameters(Difficulty.easy);
                    }
                    else if (bt.ID == "normal")
                    {
                        this.setDifficultyParameters(Difficulty.normal);
                    }
                    else if (bt.ID == "hard")
                    {
                        this.setDifficultyParameters(Difficulty.hard);
                    }
                    else if (bt.ID == "veryhard")
                    {
                        this.setDifficultyParameters(Difficulty.veryhard);
                    }
                    else if (bt.ID == "custom")
                    {
                        this.setDifficultyParameters(Difficulty.custom);
                    }

                    //Setting.Current.Difficulty = ((ButtonTexture)sender).ID;
                    //InitSetting();
                };
                cbAIHardList.Add(btOne);
            }

            btConfigList4 = new List<ButtonTexture>();

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 0f))
            {
                ID = "DianNaoShuoFuFuLu"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionCaptives = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionCaptives = true;
                }
                setDifficultyToCustom(sender, e);
            };
            btConfigList4.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 0.5f))
            {
                ID = "DianNaoChengZhongWuJiang"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionPerson = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionPerson = true;
                }
                setDifficultyToCustom(sender, e);
            };
            btConfigList4.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 1f))
            {
                ID = "DianNaoShuoFuWanJiaFuLu"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptives = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptives = true;
                }
                setDifficultyToCustom(sender, e);
            };
            btConfigList4.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 1.5f))
            {
                ID = "DianNaoFuLuZhongCheng"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptiveOnlyUnfull = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptiveOnlyUnfull = true;
                }
                setDifficultyToCustom(sender, e);
            };
            btConfigList4.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 2f))
            {
                ID = "DianNaoWanJiaDiRen"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.PinPointAtPlayer = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.PinPointAtPlayer = true;
                }
                setDifficultyToCustom(sender, e);
            };
            btConfigList4.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 2.5f))
            {
                ID = "ShouRuSuoJianWanJia"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.internalSurplusRateForPlayer = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.internalSurplusRateForPlayer = true;
                }
                setDifficultyToCustom(sender, e);
            };
            btConfigList4.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 3f))
            {
                ID = "ShouRuSuoJianDianNao"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.internalSurplusRateForAI = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.internalSurplusRateForAI = true;
                }
                setDifficultyToCustom(sender, e);
            };
            btConfigList4.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 3.5f))
            {
                ID = "HuLueDianNaoZhanLue"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.IgnoreStrategyTendency = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.IgnoreStrategyTendency = true;
                }
            };
            btConfigList4.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 4f))
            {
                ID = "DianNaoYouXianNengLi"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Session.globalVariablesTemp.AIExecuteBetterOfficer = false;
                }
                else
                {
                    bt.Selected = true;
                    Session.globalVariablesTemp.AIExecuteBetterOfficer = true;
                }
            };
            btConfigList4.Add(btOne);

            int leftPlus1 = 200;
            int leftPlus2 = 350;
            int leftPlus3 = 605;

            nstDianNaoChuZhan = new NumericSetTextureF(0, 1, 1, null, new Vector2(left1 + leftPlus1, heightBase + height * 4.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoShengTao = new NumericSetTextureF(0, 100, 100, null, new Vector2(left1 + leftPlus1, heightBase + height * 5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoYinWanJiaHeBing = new NumericSetTextureF(-1, 1, 1, null, new Vector2(left1 + leftPlus1, heightBase + height * 5.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoZiJing1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 0f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoZiJing2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 0f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoLiangCao1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 0.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoLiangCao2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 0.5f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoBuDuiGongJi1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 1f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoBuDuiGongJi2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 1f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoFangYu1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 1.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoFangYu2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 1.5f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoShangHai1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 2f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoShangHai2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 2f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoXunLian1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 2.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoXunLian2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 2.5f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoZhengBing1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 3f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoZhengBing2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 3f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoWuJiangJingYan1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 3.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoWuJiangJingYan2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 3.5f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoBuDuiJingYan1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 4f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoBuDuiJingYan2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 4f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoKangJi1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 4.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoKangJi2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 4.5f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoKangWei1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoKangWei2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 5f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };

            nstDianNaoEWai1 = new NumericSetTextureF(0, 10, 10, null, new Vector2(left2 + leftPlus2, heightBase + height * 5.5f), true)
            {
                FloatNum = 1,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.1f
            };

            nstDianNaoEWai2 = new NumericSetTextureF(0, 1, 1, null, new Vector2(left2 + leftPlus3, heightBase + height * 5.5f), true)
            {
                FloatNum = 3,
                DisNumber = false,
                NowNumber = 10,
                Unit = 0.005f
            };


            //int height = 85;

            int left = 50 + 620;

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120))
            {
                ID = "CommonSound"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.PlayNormalSound = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.PlayNormalSound = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 0.5f))
            {
                ID = "BattleSound"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.PlayBattleSound = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.PlayBattleSound = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 1f))
            {
                ID = "MapSmoke"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.DrawMapVeil = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.DrawMapVeil = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 1.5f))
            {
                ID = "ArmyAnimation"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.DrawTroopAnimation = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.DrawTroopAnimation = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 2f))
            {
                ID = "AttackStop"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.StopToControlOnAttack = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.StopToControlOnAttack = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 2.5f))
            {
                ID = "ClickConfirm"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.SingleSelectionOneClick = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.SingleSelectionOneClick = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 3f))
            {
                ID = "NoneNoticeSmall"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.NoHintOnSmallFacility = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.NoHintOnSmallFacility = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 3.5f))
            {
                ID = "NoticePopulationMove"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.HintPopulation = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.HintPopulation = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 4f))
            {
                ID = "NoticePopulationMove1000"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.HintPopulationUnder1000 = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.HintPopulationUnder1000 = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 4.5f))
            {
                ID = "AutoSave"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.doAutoSave = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.doAutoSave = true;
                }
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left, 120 + height * 5f))
            {
                ID = "OutFocus"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                var bt = (ButtonTexture)sender;
                if (bt.Selected)
                {
                    bt.Selected = false;
                    Setting.Current.GlobalVariables.RunWhileNotFocused = false;
                }
                else
                {
                    bt.Selected = true;
                    Setting.Current.GlobalVariables.RunWhileNotFocused = true;
                }
            };
            btSettingList.Add(btOne);

            nstMusic = new NumericSetTextureF(0, 100, 100, null, new Vector2(150, 115 + 60 * 2), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = Setting.Current.MusicVolume
            };

            nstSound = new NumericSetTextureF(0, 100, 100, null, new Vector2(150, 115 + 60 * 3), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = Setting.Current.SoundVolume
            };

            nstArmySpeed = new NumericSetTextureF(0, 10, 10, null, new Vector2(50 + 300, 120 + height * 3.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 1,
                Unit = 1
            };

            nstDialogTime = new NumericSetTextureF(0, 20, 20, null, new Vector2(50 + 300, 120 + height * 4f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 1,
                Unit = 1
            };

            nstBattleSpeed = new NumericSetTextureF(0, 10, 10, null, new Vector2(50 + 300, 120 + height * 4.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 1,
                Unit = 1
            };

            nstAutoSaveTime = new NumericSetTextureF(0, 60, 60, null, new Vector2(left + 360, 120 + height * 4.5f), true)
            {
                IntMode = true,
                DisNumber = false,
                NowNumber = 10,
                Unit = 1
            };

            tbBattleSpeed = new TextBox(TextBoxStyle.Small, "")
            {
                TextBoxMode = TextBoxMode.Normal,
                MaxLength = 5,
                Position = new Vector2(150, 120 + 60 * 2)
            };
            tbBattleSpeed.Text = Setting.Current.BattleSpeed;
            tbBattleSpeed.OnTextBoxSelected += (sender, e) =>
            {
                UnCheckTextBoxs();
                tbBattleSpeed.Selected = true;
            };

            btnSmallResolution = new ButtonTexture(@"Content\Textures\Resources\Start\NumberSetS", "Minus", null)
            {
                Position = new Vector2(150, 115 + 60 * 4)
            };
            btnSmallResolution.OnButtonPress += (sender, e) =>
            {
                var index = AvaliableResolutions.ToList().IndexOf(Setting.Current.Resolution);
                if (index < 0)
                {
                    index = 0;
                }
                if (index > 0)
                {
                    Setting.Current.Resolution = AvaliableResolutions[index - 1];
                    Session.ChangeDisplay(false);
                }
            };

            btnLargeResolution = new ButtonTexture(@"Content\Textures\Resources\Start\NumberSetS", "Plus", null)
            {
                Position = new Vector2(150 + 250, 115 + 60 * 4)
            };
            btnLargeResolution.OnButtonPress += (sender, e) =>
            {
                var index = AvaliableResolutions.ToList().IndexOf(Setting.Current.Resolution);
                if (index < 0)
                {
                    index = 0;
                }
                if (index < AvaliableResolutions.Length - 1)
                {
                    Setting.Current.Resolution = AvaliableResolutions[index + 1];
                    Session.ChangeDisplay(false);
                }
            };

            //btnTextureAlpha = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(50, 120 + 85 * 5f))
            //{
            //    ID = "AutoSave"
            //};
            //btnTextureAlpha.OnButtonPress += (sender, e) =>
            //{
            //    var bt = (ButtonTexture)sender;
            //    if (bt.Selected)
            //    {
            //        bt.Selected = false;
            //        Setting.Current.GlobalVariables.doAutoSave = false;
            //    }
            //    else
            //    {
            //        bt.Selected = true;
            //        Setting.Current.GlobalVariables.doAutoSave = true;
            //    }
            //};

            tbGamerName = new TextBox(TextBoxStyle.Small, "")
            {
                MaxLength = 20,
                Position = new Vector2(150, 120 + 60 * 4)
            };

            //if (Platform.PlatFormType == PlatFormType.Win)
            //{
            //    tbGamerName.TextBoxMode = TextBoxMode.Normal;
            //}
            //else
            //{
            //    tbGamerName.TextBoxMode = TextBoxMode.Chinese;
            //}

            //tbGamerName.Text = Setting.Current.GamerName;
            //tbGamerName.OnTextBoxSelected += (sender, e) =>
            //{
            //    UnCheckTextBoxs();
            //    tbGamerName.Selected = true;
            //};

            btAboutList = new List<ButtonTexture>();
            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\ReadyButton", "Cancel", new Vector2(800 + 220, 615));
            btOne.OnButtonPress += (sender, e) =>
            {
                isClosing = true;
                menuTypeElapsed = 0.5f;
            };
            btAboutList.Add(btOne);

            btPre = new ButtonTexture(@"Content\Textures\Resources\Start\LeftAndRight", "Left", new Vector2(528, 612))
            {
                Enable = false
            };
            btPre.OnButtonPress += (sender, e) =>
            {
                if (pageIndex > 1)
                {
                    pageIndex--;
                }

            };

            btNext = new ButtonTexture(@"Content\Textures\Resources\Start\LeftAndRight", "Right", new Vector2(528 + 120, 612))
            {
                Enable = false
            };
            btNext.OnButtonPress += (sender, e) =>
            {
                if (pageIndex < pageCount)
                {
                    pageIndex++;
                }

            };

            btPre1 = new ButtonTexture(@"Content\Textures\Resources\Start\LeftAndRight", "Left", new Vector2(860, 612))
            {
                Enable = false
            };
            btPre1.OnButtonPress += (sender, e) =>
            {
                if (pageIndex1 > 1)
                {
                    pageIndex1--;
                }

            };

            btNext1 = new ButtonTexture(@"Content\Textures\Resources\Start\LeftAndRight", "Right", new Vector2(860 + 120, 612))
            {
                Enable = false
            };
            btNext1.OnButtonPress += (sender, e) =>
            {
                if (pageIndex1 < pageCount1)
                {
                    pageIndex1++;
                }

            };

            InitSetting();
        }

        void UnCheckTextBoxs()
        {
            message = "";
            tbGamerName.Selected = tbBattleSpeed.Selected = false;
        }

        void InitScenarioList()
        {
            string path = @"Content\Data\Scenario\";

            string file = path + "Scenarios.json";

            pageIndex = pageIndex1 = 1;

            CurrentScenario = null;

            btScenarioSelectList = new List<ButtonTexture>();

            btScenarioPlayersList = new List<ButtonTexture>();

            ScenarioList = SimpleSerializer.DeserializeJsonFile<List<Scenario>>(file, false, false, false);
            //var str = SimpleSerializer.SerializeJson(ScenarioList, false, true, true);

            foreach (var sce in ScenarioList)
            {
                //var path0 = @"Content\Data\Scenario\" + sce.Name + ".json";

                //var str = Platform.Current.LoadText(path0);

                //var str2 = WordTools.TranslationWords(str, false, true);

                //Platform.Current.WriteAllText(sce.Name + ".json", str2);

                var btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", null)
                {
                    ID = sce.Name
                };
                btOne.OnButtonPress += (sender, e) =>
                {
                    string id = ((ButtonTexture)sender).ID;

                    btScenarioSelectList.ForEach(bt => bt.Selected = false);

                    ((ButtonTexture)sender).Selected = true;

                    CurrentScenario = ScenarioList.FirstOrDefault(sc => sc.Name == id);

                    var iDs = CurrentScenario.IDs.Split(',').NullToEmptyList();
                    var names = CurrentScenario.Names.Split(',').NullToEmptyList();

                    btScenarioPlayersList = new List<ButtonTexture>();

                    for (int i = 0; i < iDs.Count; i++)
                    {
                        var id0 = iDs[i];
                        var btPlayer = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", null)
                        {
                            ID = id0
                        };
                        btPlayer.OnButtonPress += (sender0, e0) =>
                        {
                            var btP = (ButtonTexture)sender0;
                            btP.Selected = !btP.Selected;
                        };
                        btScenarioPlayersList.Add(btPlayer);
                    }

                    pageIndex1 = 1;
                };
                btScenarioSelectList.Add(btOne);
            }
            //var sces = new List<Scenario>();
            //var files = Platform.Current.GetFiles(path).NullToEmptyList().Where(fi => fi.EndsWith(".mdb")).NullToEmptyList();
            //foreach (var fi in files)
            //{
            //    var sce = GetScenario(fi);
            //    sces.Add(sce);
            //}
            //var sers = SimpleSerializer.SerializeJson(sces, true).TranslationWords(false, false);
        }

        private void setDifficultyParameters(Difficulty d)
        {
            //doNotSetDifficultyToCustom = true;

            //nstDianNaoChuZhan, , ,           

            //btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 3.5f))
            //{
            //    ID = "HuLueDianNaoZhanLue"
            //};
            //btOne.OnButtonPress += (sender, e) =>
            //{
            //    var bt = (ButtonTexture)sender;
            //    if (bt.Selected)
            //    {
            //        bt.Selected = false;
            //        Session.globalVariablesTemp.IgnoreStrategyTendency = false;
            //    }
            //    else
            //    {
            //        bt.Selected = true;
            //        Session.globalVariablesTemp.IgnoreStrategyTendency = true;
            //    }
            //};
            //btConfigList4.Add(btOne);

            //btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(left1, heightBase + height * 4f))
            //{
            //    ID = "DianNaoYouXianNengLi"
            //};
            //btOne.OnButtonPress += (sender, e) =>
            //{
            //    var bt = (ButtonTexture)sender;
            //    if (bt.Selected)
            //    {
            //        bt.Selected = false;
            //        Session.globalVariablesTemp.AIExecuteBetterOfficer = false;
            //    }
            //    else
            //    {
            //        bt.Selected = true;
            //        Session.globalVariablesTemp.AIExecuteBetterOfficer = true;
            //    }
            //};
            //btConfigList4.Add(btOne);

            changeDifficultySelection(d);

            switch (d)
            {
                case Difficulty.beginner:

                    this.nstDianNaoZiJing1.NowNumber = 0.7f;
                    this.nstDianNaoLiangCao1.NowNumber = 0.7f;
                    this.nstDianNaoShangHai1.NowNumber = 0.7f;
                    this.nstDianNaoBuDuiGongJi1.NowNumber = 0.7f;
                    this.nstDianNaoFangYu1.NowNumber = 0.7f;
                    this.nstDianNaoZhengBing1.NowNumber = 0.7f;
                    this.nstDianNaoXunLian1.NowNumber = 0.7f;
                    this.nstDianNaoWuJiangJingYan1.NowNumber = 0.7f;
                    this.nstDianNaoBuDuiJingYan1.NowNumber = 0.7f;
                    this.nstDianNaoKangJi1.NowNumber = 0;
                    this.nstDianNaoKangWei1.NowNumber = 0;
                    this.nstDianNaoZiJing2.NowNumber = 0.0f;
                    this.nstDianNaoLiangCao2.NowNumber = 0.0f;
                    this.nstDianNaoShangHai2.NowNumber = 0.0f;
                    this.nstDianNaoBuDuiGongJi2.NowNumber = 0.0f;
                    this.nstDianNaoFangYu2.NowNumber = 0.0f;
                    this.nstDianNaoZhengBing2.NowNumber = 0.0f;
                    this.nstDianNaoXunLian2.NowNumber = 0.0f;
                    this.nstDianNaoWuJiangJingYan2.NowNumber = 0.0f;
                    this.nstDianNaoBuDuiJingYan2.NowNumber = 0.0f;
                    this.nstDianNaoKangJi2.NowNumber = 0.0f;
                    this.nstDianNaoKangWei2.NowNumber = 0.0f;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoWanJiaDiRen").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianWanJia").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianDianNao").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuFuLu").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoChengZhongWuJiang").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuWanJiaFuLu").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoFuLuZhongCheng").Selected = false;
                    Session.globalVariablesTemp.PinPointAtPlayer = false;
                    Session.globalVariablesTemp.internalSurplusRateForPlayer = false;
                    Session.globalVariablesTemp.internalSurplusRateForAI = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionCaptives = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionPerson = false;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptives = false;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptiveOnlyUnfull = false;

                    this.nstDianNaoShengTao.NowNumber = 0;
                    this.nstDianNaoEWai1.NowNumber = 1.0f;
                    this.nstDianNaoEWai2.NowNumber = 0.0f;

                    this.nstDianNaoYinWanJiaHeBing.NowNumber = -1f;
                    this.AIEncircleRank = 0;
                    this.AIEncircleVar = 0;

                    break;
                case Difficulty.easy:

                    this.nstDianNaoZiJing1.NowNumber = 1.0f;
                    this.nstDianNaoLiangCao1.NowNumber = 1.0f;
                    this.nstDianNaoShangHai1.NowNumber = 1.0f;
                    this.nstDianNaoBuDuiGongJi1.NowNumber = 1.0f;
                    this.nstDianNaoFangYu1.NowNumber = 1.0f;
                    this.nstDianNaoZhengBing1.NowNumber = 1.0f;
                    this.nstDianNaoXunLian1.NowNumber = 1.0f;
                    this.nstDianNaoWuJiangJingYan1.NowNumber = 1.0f;
                    this.nstDianNaoBuDuiJingYan1.NowNumber = 1.0f;
                    this.nstDianNaoKangJi1.NowNumber = 0;
                    this.nstDianNaoKangWei1.NowNumber = 0;
                    this.nstDianNaoZiJing2.NowNumber = 0.0f;
                    this.nstDianNaoLiangCao2.NowNumber = 0.0f;
                    this.nstDianNaoShangHai2.NowNumber = 0.0f;
                    this.nstDianNaoBuDuiGongJi2.NowNumber = 0.0f;
                    this.nstDianNaoFangYu2.NowNumber = 0.0f;
                    this.nstDianNaoZhengBing2.NowNumber = 0.0f;
                    this.nstDianNaoXunLian2.NowNumber = 0.0f;
                    this.nstDianNaoWuJiangJingYan2.NowNumber = 0.0f;
                    this.nstDianNaoBuDuiJingYan2.NowNumber = 0.0f;
                    this.nstDianNaoKangJi2.NowNumber = 0.0f;
                    this.nstDianNaoKangWei2.NowNumber = 0.0f;

                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoWanJiaDiRen").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianWanJia").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianDianNao").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuFuLu").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoChengZhongWuJiang").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuWanJiaFuLu").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoFuLuZhongCheng").Selected = false;
                    Session.globalVariablesTemp.PinPointAtPlayer = false;
                    Session.globalVariablesTemp.internalSurplusRateForPlayer = false;
                    Session.globalVariablesTemp.internalSurplusRateForAI = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionCaptives = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionPerson = false;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptives = false;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptiveOnlyUnfull = false;

                    this.nstDianNaoShengTao.NowNumber = 0;
                    this.nstDianNaoEWai1.NowNumber = 1.0f;
                    this.nstDianNaoEWai2.NowNumber = 0.0f;
                    this.nstDianNaoYinWanJiaHeBing.NowNumber = -1f;
                    this.AIEncircleRank = 15;
                    this.AIEncircleVar = 15;
                    break;

                case Difficulty.normal:

                    this.nstDianNaoZiJing1.NowNumber = 2.0f;
                    this.nstDianNaoLiangCao1.NowNumber = 2.0f;
                    this.nstDianNaoShangHai1.NowNumber = 1.0f;
                    this.nstDianNaoBuDuiGongJi1.NowNumber = 1.0f;
                    this.nstDianNaoFangYu1.NowNumber = 1.0f;
                    this.nstDianNaoZhengBing1.NowNumber = 1.2f;
                    this.nstDianNaoXunLian1.NowNumber = 1.2f;
                    this.nstDianNaoWuJiangJingYan1.NowNumber = 1.0f;
                    this.nstDianNaoBuDuiJingYan1.NowNumber = 1.5f;
                    this.nstDianNaoKangJi1.NowNumber = 0f;
                    this.nstDianNaoKangWei1.NowNumber = 0f;
                    this.nstDianNaoZiJing2.NowNumber = 0.02f;
                    this.nstDianNaoLiangCao2.NowNumber = 0.02f;
                    this.nstDianNaoShangHai2.NowNumber = 0.0f;
                    this.nstDianNaoBuDuiGongJi2.NowNumber = 0.0f;
                    this.nstDianNaoFangYu2.NowNumber = 0.0f;
                    this.nstDianNaoZhengBing2.NowNumber = 0.02f;
                    this.nstDianNaoXunLian2.NowNumber = 0.02f;
                    this.nstDianNaoWuJiangJingYan2.NowNumber = 0.0f;
                    this.nstDianNaoBuDuiJingYan2.NowNumber = 0.01f;
                    this.nstDianNaoKangJi2.NowNumber = 0.0f;
                    this.nstDianNaoKangWei2.NowNumber = 0.0f;

                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoWanJiaDiRen").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianWanJia").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianDianNao").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuFuLu").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoChengZhongWuJiang").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuWanJiaFuLu").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoFuLuZhongCheng").Selected = false;
                    Session.globalVariablesTemp.PinPointAtPlayer = false;
                    Session.globalVariablesTemp.internalSurplusRateForPlayer = false;
                    Session.globalVariablesTemp.internalSurplusRateForAI = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionCaptives = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionPerson = false;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptives = false;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptiveOnlyUnfull = false;

                    this.nstDianNaoShengTao.NowNumber = 5f;
                    this.nstDianNaoEWai1.NowNumber = 1.2f;
                    this.nstDianNaoEWai2.NowNumber = 0.0f;

                    this.nstDianNaoYinWanJiaHeBing.NowNumber = -1f;

                    this.AIEncircleRank = 30;
                    this.AIEncircleVar = 30;

                    break;

                case Difficulty.hard:

                    this.nstDianNaoZiJing1.NowNumber = 3.0f;
                    this.nstDianNaoLiangCao1.NowNumber = 3.0f;
                    this.nstDianNaoShangHai1.NowNumber = 1.2f;
                    this.nstDianNaoBuDuiGongJi1.NowNumber = 1.0f;
                    this.nstDianNaoFangYu1.NowNumber = 1.2f;
                    this.nstDianNaoZhengBing1.NowNumber = 1.5f;
                    this.nstDianNaoXunLian1.NowNumber = 1.5f;
                    this.nstDianNaoWuJiangJingYan1.NowNumber = 1.0f;
                    this.nstDianNaoBuDuiJingYan1.NowNumber = 2.0f;
                    this.nstDianNaoKangJi1.NowNumber = 0f;
                    this.nstDianNaoKangWei1.NowNumber = 0f;
                    this.nstDianNaoZiJing2.NowNumber = 0.05f;
                    this.nstDianNaoLiangCao2.NowNumber = 0.05f;
                    this.nstDianNaoShangHai2.NowNumber = 0.005f;
                    this.nstDianNaoBuDuiGongJi2.NowNumber = 0.0f;
                    this.nstDianNaoFangYu2.NowNumber = 0.01f;
                    this.nstDianNaoZhengBing2.NowNumber = 0.05f;
                    this.nstDianNaoXunLian2.NowNumber = 0.05f;
                    this.nstDianNaoWuJiangJingYan2.NowNumber = 0.0f;
                    this.nstDianNaoBuDuiJingYan2.NowNumber = 0.02f;
                    this.nstDianNaoKangJi2.NowNumber = 0.1f;
                    this.nstDianNaoKangWei2.NowNumber = 0.1f;

                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoWanJiaDiRen").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianWanJia").Selected = true;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianDianNao").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuFuLu").Selected = true;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoChengZhongWuJiang").Selected = true;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuWanJiaFuLu").Selected = true;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoFuLuZhongCheng").Selected = true;
                    Session.globalVariablesTemp.PinPointAtPlayer = false;
                    Session.globalVariablesTemp.internalSurplusRateForPlayer = true;
                    Session.globalVariablesTemp.internalSurplusRateForAI = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionCaptives = true;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionPerson = true;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptives = true;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptiveOnlyUnfull = true;

                    this.nstDianNaoShengTao.NowNumber = 10f;
                    this.nstDianNaoEWai1.NowNumber = 1.5f;
                    this.nstDianNaoEWai2.NowNumber = 0.01f;

                    this.nstDianNaoYinWanJiaHeBing.NowNumber = -1f;

                    this.AIEncircleRank = 70;
                    this.AIEncircleVar = 30;

                    break;

                case Difficulty.veryhard:

                    this.nstDianNaoZiJing1.NowNumber = 5.0f;
                    this.nstDianNaoLiangCao1.NowNumber = 5.0f;
                    this.nstDianNaoShangHai1.NowNumber = 1.5f;
                    this.nstDianNaoBuDuiGongJi1.NowNumber = 1.2f;
                    this.nstDianNaoFangYu1.NowNumber = 1.5f;
                    this.nstDianNaoZhengBing1.NowNumber = 3.0f;
                    this.nstDianNaoXunLian1.NowNumber = 3.0f;
                    this.nstDianNaoWuJiangJingYan1.NowNumber = 1.0f;
                    this.nstDianNaoBuDuiJingYan1.NowNumber = 4.0f;
                    this.nstDianNaoKangJi1.NowNumber = 0f;
                    this.nstDianNaoKangWei1.NowNumber = 0f;
                    this.nstDianNaoZiJing2.NowNumber = 0.05f;
                    this.nstDianNaoLiangCao2.NowNumber = 0.05f;
                    this.nstDianNaoShangHai2.NowNumber = 0.02f;
                    this.nstDianNaoBuDuiGongJi2.NowNumber = 0.0f;
                    this.nstDianNaoFangYu2.NowNumber = 0.05f;
                    this.nstDianNaoZhengBing2.NowNumber = 0.1f;
                    this.nstDianNaoXunLian2.NowNumber = 0.1f;
                    this.nstDianNaoWuJiangJingYan2.NowNumber = 0.0f;
                    this.nstDianNaoBuDuiJingYan2.NowNumber = 0.1f;
                    this.nstDianNaoKangJi2.NowNumber = 0.2f;
                    this.nstDianNaoKangWei2.NowNumber = 0.2f;

                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoWanJiaDiRen").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianWanJia").Selected = true;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianDianNao").Selected = false;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuFuLu").Selected = true;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoChengZhongWuJiang").Selected = true;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuWanJiaFuLu").Selected = true;
                    btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoFuLuZhongCheng").Selected = true;
                    Session.globalVariablesTemp.PinPointAtPlayer = false;
                    Session.globalVariablesTemp.internalSurplusRateForPlayer = true;
                    Session.globalVariablesTemp.internalSurplusRateForAI = false;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionCaptives = true;
                    Session.globalVariablesTemp.AIAutoTakeNoFactionPerson = true;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptives = true;
                    Session.globalVariablesTemp.AIAutoTakePlayerCaptiveOnlyUnfull = true;

                    this.nstDianNaoShengTao.NowNumber = 20f;
                    this.nstDianNaoEWai1.NowNumber = 3.0f;
                    this.nstDianNaoEWai2.NowNumber = 0.05f;

                    this.nstDianNaoYinWanJiaHeBing.NowNumber = -1f;

                    this.AIEncircleRank = 100;
                    this.AIEncircleVar = 0;

                    break;
            }
            //doNotSetDifficultyToCustom = false;
        }

        private void setDifficultyToCustom(object sender, EventArgs e)
        {
            //if (!doNotSetDifficultyToCustom)
            //{
            changeDifficultySelection(Difficulty.custom);
            //}
        }

        private void changeDifficultySelection(Difficulty d)
        {
            cbAIHardList.ForEach(bt => bt.Selected = false);

            switch (d)
            {
                case Difficulty.beginner: cbAIHardList.FirstOrDefault(cb => cb.ID == "beginner").Selected = true; break;
                case Difficulty.easy: cbAIHardList.FirstOrDefault(cb => cb.ID == "easy").Selected = true; break;
                case Difficulty.normal: cbAIHardList.FirstOrDefault(cb => cb.ID == "normal").Selected = true; break;
                case Difficulty.hard: cbAIHardList.FirstOrDefault(cb => cb.ID == "hard").Selected = true; break;
                case Difficulty.veryhard: cbAIHardList.FirstOrDefault(cb => cb.ID == "veryhard").Selected = true; break;
                case Difficulty.custom: cbAIHardList.FirstOrDefault(cb => cb.ID == "custom").Selected = true; break;
                default: cbAIHardList.FirstOrDefault(cb => cb.ID == "custom").Selected = true; break;
            }
        }

        public void InitScenarioSaveList()
        {
            pageIndex = pageIndex1 = 1;

            btScenarioSelectList = new List<ButtonTexture>();

            btScenarioPlayersList = new List<ButtonTexture>();

            ScenarioList = GameScenario.LoadScenarioSaves();

            CurrentScenario = null;

            foreach (var sce in ScenarioList)
            {
                var btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", null)
                {
                    ID = sce.Name
                };
                btOne.OnButtonPress += (sender, e) =>
                {
                    string id = ((ButtonTexture)sender).ID;

                    btScenarioSelectList.ForEach(bt => bt.Selected = false);

                    ((ButtonTexture)sender).Selected = true;

                    CurrentScenario = ScenarioList.FirstOrDefault(sc => sc.Name == id);

                    //pageIndex = 1;
                };
                btScenarioSelectList.Add(btOne);
            }

        }

        //Scenario GetScenario(string fileName)
        //{
        //    OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder
        //    {
        //        DataSource = fileName,
        //        Provider = "Microsoft.Jet.OLEDB.4.0"
        //    };
        //    OleDbConnection dbConnection = new OleDbConnection(builder.ConnectionString);

        //    OleDbCommand command = new OleDbCommand("Select * From GameSurvey", dbConnection);
        //    dbConnection.Open();
        //    OleDbDataReader reader = command.ExecuteReader();
        //    reader.Read();

        //    var sce = new Scenario()
        //    {
        //        Name = Path.GetFileNameWithoutExtension(fileName),
        //        Title = reader["Title"].ToString(),
        //        Time = String.Format("{0}-{1}-{2}", reader["GYear"].ToString(), reader["GMonth"].ToString(), reader["GDay"].ToString()),
        //        Desc = reader["Description"].ToString(),
        //        Create = reader["SaveTime"].ToString(),
        //        Info = reader["PlayerInfo"].ToString(),
        //        First = reader["JumpPosition"].ToString()
        //    };

        //    dbConnection.Close();

        //    var faction = GameScenario.GetGameScenarioFactions(dbConnection);

        //    sce.IDs = String.Join(",", faction.GameObjects.Select(go => (go as Faction).ID.ToString()));

        //    sce.Names = String.Join(",", faction.GameObjects.Select(go => (go as Faction).Name.ToString()).ToArray());

        //    //string gameScenarioSurveyText = GameScenario.GetGameScenarioSurveyText(dbConnection);
        //    return sce;
        //}

        void InitConfig()
        {
            //基本

            //btConfigList1.FirstOrDefault(bt => bt.ID == "LiangDao").Selected = Session.globalVariablesTemp.LiangdaoXitong;

            btConfigList1.FirstOrDefault(bt => bt.ID == "ChuShi").Selected = Session.globalVariablesTemp.IdealTendencyValid;

            btConfigList1.FirstOrDefault(bt => bt.ID == "BuDuiSuLv").Selected = Session.globalVariablesTemp.MilitaryKindSpeedValid;

            btConfigList1.FirstOrDefault(bt => bt.ID == "DanTiaoSiWang").Selected = Session.globalVariablesTemp.PersonDieInChallenge;

            btConfigList1.FirstOrDefault(bt => bt.ID == "NianLingYouXiao").Selected = (bool)Session.globalVariablesTemp.PersonNaturalDeath;

            btConfigList1.FirstOrDefault(bt => bt.ID == "NianLingYingXiang").Selected = (bool)Session.globalVariablesTemp.EnableAgeAbilityFactor;

            btConfigList1.FirstOrDefault(bt => bt.ID == "WuJiangDuli").Selected = (bool)Session.globalVariablesTemp.WujiangYoukenengDuli;

            btConfigList1.FirstOrDefault(bt => bt.ID == "ShiLiHeBing").Selected = (bool)Session.globalVariablesTemp.PermitFactionMerge;

            btConfigList1.FirstOrDefault(bt => bt.ID == "RenKouXiaoYu").Selected = (bool)Session.globalVariablesTemp.PopulationRecruitmentLimit;

            btConfigList1.FirstOrDefault(bt => bt.ID == "KaiQiTianYan").Selected = (bool)Session.globalVariablesTemp.SkyEye;

            btConfigList1.FirstOrDefault(bt => bt.ID == "KaiQiZuoBi").Selected = (bool)Session.globalVariablesTemp.EnableCheat;

            btConfigList1.FirstOrDefault(bt => bt.ID == "YingHeMoshi").Selected = (bool)Session.globalVariablesTemp.HardcoreMode;

            btConfigList1.FirstOrDefault(bt => bt.ID == "ShengChengZiSi").Selected = (bool)Session.globalVariablesTemp.createChildren;

            btConfigList1.FirstOrDefault(bt => bt.ID == "JianYiAI").Selected = (bool)Session.globalVariablesTemp.AIQuickBattle;

            btConfigList1.FirstOrDefault(bt => bt.ID == "hougongAlienOnly").Selected = (bool)Session.globalVariablesTemp.hougongAlienOnly;

            nstViewDetail.NowNumber = Session.globalVariablesTemp.TabListDetailLevel;

            nstGeneralBattleDead.NowNumber = Session.globalVariablesTemp.OfficerDieInBattleRate;

            nstGeneralYun.NowNumber = Session.globalVariablesTemp.getChildrenRate;

            nstFeiZiYun.NowNumber = Session.globalVariablesTemp.hougongGetChildrenRate;

            nstZhaoXian.NowNumber = Session.globalVariablesTemp.ZhaoXianSuccessRate;

            nstSearchGen.NowNumber = Session.globalVariablesTemp.CreateRandomOfficerChance;

            nstZaiNan.NowNumber = Session.globalVariablesTemp.zainanfashengjilv;

            nstDayInTurn.NowNumber = Session.parametersTemp.DayInTurn;

            //人物

            btConfigList2.FirstOrDefault(bt => bt.ID == "XuNiShangXian").Selected = (bool)Session.globalVariablesTemp.createChildrenIgnoreLimit;

            btConfigList2.FirstOrDefault(bt => bt.ID == "WuJiangGuanXi").Selected = (bool)Session.globalVariablesTemp.EnablePersonRelations;

            nstMaxExperience.NowNumber = Session.globalVariablesTemp.maxExperience;

            nstMaxSkill.NowNumber = Session.globalVariablesTemp.MaxAbility;

            nstPiLeiUp.NowNumber = Session.globalVariablesTemp.TirednessIncrease;

            nstPiLeiDown.NowNumber = Session.globalVariablesTemp.TirednessDecrease;

            nstExperienceUp.NowNumber = Session.parametersTemp.AbilityExperienceRate;

            nstTreasureDiscover.NowNumber = Session.parametersTemp.FindTreasureChance;

            nstFollowAttachPlus.NowNumber = Session.parametersTemp.FollowedLeaderOffenceRateIncrement;

            nstFollowDefencePlus.NowNumber = Session.parametersTemp.FollowedLeaderDefenceRateIncrement;

            nstSearchTime.NowNumber = Session.parametersTemp.SearchDays;

            nstGeneralChildsMax.NowNumber = Session.globalVariablesTemp.OfficerChildrenLimit;

            nstGeneralChildsAppear.NowNumber = Session.globalVariablesTemp.ChildrenAvailableAge;

            nstGeneralChildsSkill.NowNumber = Session.globalVariablesTemp.CreatedOfficerAbilityFactor;

            nstChildsSkill.NowNumber = Session.globalVariablesTemp.ChildrenAbilityFactor;

            nstForbiddenDays.NowNumber = Session.globalVariablesTemp.ProhibitFactionAgainstDestroyer;


            //參數

            nstNeiZheng.NowNumber = Session.parametersTemp.InternalRate;

            nstXunLian.NowNumber = Session.parametersTemp.TrainingRate;

            nstBuChong.NowNumber = Session.parametersTemp.RecruitmentRate;

            nstZiJing.NowNumber = Session.parametersTemp.FundRate;

            nstLiangCao.NowNumber = Session.parametersTemp.FoodRate;

            nstBuDui.NowNumber = Session.parametersTemp.TroopDamageRate;

            nstJianZhu.NowNumber = Session.parametersTemp.ArchitectureDamageRate;

            nstRenKou.NowNumber = Session.parametersTemp.DefaultPopulationDevelopingRate;

            nstWeiCheng.NowNumber = Session.parametersTemp.SurroundArchitectureDominationUnit;

            nstHuoYan.NowNumber = Session.parametersTemp.FireDamageScale;

            nstMaiLiangNongYe.NowNumber = Session.parametersTemp.BuyFoodAgriculture;

            nstMaiLiangShangYe.NowNumber = Session.parametersTemp.SellFoodCommerce;

            nstZiJingHuanLiang.NowNumber = Session.parametersTemp.FundToFoodMultiple;

            nstLiangCaoHuanZiJing.NowNumber = Session.parametersTemp.FoodToFundDivisor;

            nstJiqiaoDian.NowNumber = Session.globalVariablesTemp.TechniquePointMultiple;

            nstNeiZhengZiJing.NowNumber = Session.parametersTemp.InternalFundCost;

            nstBuChongZiJing.NowNumber = Session.parametersTemp.RecruitmentFundCost;

            nstBuChongTongZhi.NowNumber = Session.parametersTemp.RecruitmentDomination;

            nstBuChongMinXin.NowNumber = Session.parametersTemp.RecruitmentMorale;

            nstQianDuZiJing.NowNumber = Session.parametersTemp.ChangeCapitalCost;

            nstShuoFuZiJing.NowNumber = Session.parametersTemp.ConvincePersonCost;

            nstBaoJiangZiJing.NowNumber = Session.parametersTemp.RewardPersonCost;

            nstJieLaoZiJing.NowNumber = Session.parametersTemp.JailBreakArchitectureCost;

            nstPoHuaiZiJing.NowNumber = Session.parametersTemp.DestroyArchitectureCost;

            nstShanDongZiJing.NowNumber = Session.parametersTemp.InstigateArchitectureCost;

            nstLiuYanZiJing.NowNumber = Session.parametersTemp.GossipArchitectureCost;

            nstBingYiShangXian.NowNumber = Session.parametersTemp.MilitaryPopulationCap;

            nstBingYiZengLiang.NowNumber = Session.parametersTemp.MilitaryPopulationReloadQuantity;

            nstBuDuiJingYan.NowNumber = Session.globalVariablesTemp.MaxMilitaryExperience;

            nstTongShuaiGongJi.NowNumber = Session.globalVariablesTemp.LeadershipOffenceRate;

            //電腦

            changeDifficultySelection(Difficulty.easy);

            btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuFuLu").Selected = (bool)Session.globalVariablesTemp.AIAutoTakeNoFactionCaptives;

            btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoChengZhongWuJiang").Selected = (bool)Session.globalVariablesTemp.AIAutoTakeNoFactionPerson;

            btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoShuoFuWanJiaFuLu").Selected = (bool)Session.globalVariablesTemp.AIAutoTakePlayerCaptives;

            btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoFuLuZhongCheng").Selected = (bool)Session.globalVariablesTemp.AIAutoTakePlayerCaptiveOnlyUnfull;

            btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoWanJiaDiRen").Selected = (bool)Session.globalVariablesTemp.PinPointAtPlayer;

            btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianWanJia").Selected = (bool)Session.globalVariablesTemp.internalSurplusRateForPlayer;

            btConfigList4.FirstOrDefault(bt => bt.ID == "ShouRuSuoJianDianNao").Selected = (bool)Session.globalVariablesTemp.internalSurplusRateForAI;

            btConfigList4.FirstOrDefault(bt => bt.ID == "HuLueDianNaoZhanLue").Selected = (bool)Session.globalVariablesTemp.IgnoreStrategyTendency;

            btConfigList4.FirstOrDefault(bt => bt.ID == "DianNaoYouXianNengLi").Selected = (bool)Session.globalVariablesTemp.AIExecuteBetterOfficer;

            nstDianNaoShengTao.NowNumber = Session.parametersTemp.AIEncirclePlayerRate;

            nstDianNaoYinWanJiaHeBing.NowNumber = Session.globalVariablesTemp.AIMergeAgainstPlayer;

            nstDianNaoChuZhan.NowNumber = Session.globalVariablesTemp.AIExecutionRate;

            nstDianNaoZiJing1.NowNumber = Session.parametersTemp.AIFundRate;

            nstDianNaoZiJing2.NowNumber = Session.parametersTemp.AIFundYearIncreaseRate;

            nstDianNaoLiangCao1.NowNumber = Session.parametersTemp.AIFoodRate;

            nstDianNaoLiangCao2.NowNumber = Session.parametersTemp.AIFoodYearIncreaseRate;

            nstDianNaoBuDuiGongJi1.NowNumber = Session.parametersTemp.AITroopOffenceRate;

            nstDianNaoBuDuiGongJi2.NowNumber = Session.parametersTemp.AITroopOffenceYearIncreaseRate;

            nstDianNaoFangYu1.NowNumber = Session.parametersTemp.AITroopDefenceRate;

            nstDianNaoFangYu2.NowNumber = Session.parametersTemp.AITroopDefenceYearIncreaseRate;

            nstDianNaoShangHai1.NowNumber = Session.parametersTemp.AIArchitectureDamageRate;

            nstDianNaoShangHai2.NowNumber = Session.parametersTemp.AIArchitectureDamageYearIncreaseRate;

            nstDianNaoXunLian1.NowNumber = Session.parametersTemp.AITrainingSpeedRate;

            nstDianNaoXunLian2.NowNumber = Session.parametersTemp.AITrainingSpeedYearIncreaseRate;

            nstDianNaoZhengBing1.NowNumber = Session.parametersTemp.AIRecruitmentSpeedRate;

            nstDianNaoZhengBing2.NowNumber = Session.parametersTemp.AIRecruitmentSpeedYearIncreaseRate;

            nstDianNaoWuJiangJingYan1.NowNumber = Session.parametersTemp.AIOfficerExperienceRate;

            nstDianNaoWuJiangJingYan2.NowNumber = Session.parametersTemp.AIOfficerExperienceYearIncreaseRate;

            nstDianNaoBuDuiJingYan1.NowNumber = Session.parametersTemp.AIArmyExperienceRate;

            nstDianNaoBuDuiJingYan2.NowNumber = Session.parametersTemp.AIArmyExperienceYearIncreaseRate;

            nstDianNaoKangJi1.NowNumber = Session.parametersTemp.AIAntiStratagem;

            nstDianNaoKangJi2.NowNumber = Session.parametersTemp.AIAntiStratagemIncreaseRate;

            nstDianNaoKangWei1.NowNumber = Session.parametersTemp.AIAntiSurround;

            nstDianNaoKangWei2.NowNumber = Session.parametersTemp.AIAntiSurroundIncreaseRate;

            nstDianNaoEWai1.NowNumber = Session.parametersTemp.AIExtraPerson;

            nstDianNaoEWai2.NowNumber = Session.parametersTemp.AIExtraPersonIncreaseRate;

            AIEncircleRank = Session.parametersTemp.AIEncircleRank;
            AIEncircleVar = Session.parametersTemp.AIEncircleVar;

            //doNotSetDifficultyToCustom = false;           

        }

        void InitSetting()
        {
            var btSimple = btSettingList.FirstOrDefault(bt => bt.ID == "简体中文");
            var btTradition = btSettingList.FirstOrDefault(bt => bt.ID == "传统中文");
            var btWindow = btSettingList.FirstOrDefault(bt => bt.ID == "窗口模式");
            var btFull = btSettingList.FirstOrDefault(bt => bt.ID == "全屏模式");

            btSimple.Selected = btTradition.Selected = false;
            if (Setting.Current.Language == "cn")
            {
                btSimple.Selected = true;
            }
            else
            {
                btTradition.Selected = true;
            }

            btWindow.Selected = btFull.Selected = false;
            if (Setting.Current.DisplayMode == "Window")
            {
                btWindow.Selected = true;
            }
            else
            {
                btFull.Selected = true;
            }

            var btOne = btSettingList.FirstOrDefault(bt => bt.ID == "CommonSound");
            btOne.Selected = Setting.Current.GlobalVariables.PlayNormalSound;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "BattleSound");
            btOne.Selected = Setting.Current.GlobalVariables.PlayBattleSound;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "MapSmoke");
            btOne.Selected = Setting.Current.GlobalVariables.DrawMapVeil;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "ArmyAnimation");
            btOne.Selected = Setting.Current.GlobalVariables.DrawTroopAnimation;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "AttackStop");
            btOne.Selected = Setting.Current.GlobalVariables.StopToControlOnAttack;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "ClickConfirm");
            btOne.Selected = Setting.Current.GlobalVariables.SingleSelectionOneClick;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "NoneNoticeSmall");
            btOne.Selected = Setting.Current.GlobalVariables.NoHintOnSmallFacility;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "NoticePopulationMove");
            btOne.Selected = Setting.Current.GlobalVariables.HintPopulation;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "NoticePopulationMove1000");
            btOne.Selected = Setting.Current.GlobalVariables.HintPopulationUnder1000;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "AutoSave");
            btOne.Selected = Setting.Current.GlobalVariables.doAutoSave;

            btOne = btSettingList.FirstOrDefault(bt => bt.ID == "OutFocus");
            btOne.Selected = Setting.Current.GlobalVariables.RunWhileNotFocused;

            nstAutoSaveTime.NowNumber = Setting.Current.GlobalVariables.AutoSaveFrequency;

            nstArmySpeed.NowNumber = Setting.Current.GlobalVariables.TroopMoveSpeed;

            nstDialogTime.NowNumber = Setting.Current.GlobalVariables.DialogShowTime;

            nstBattleSpeed.NowNumber = Setting.Current.GlobalVariables.FastBattleSpeed;

            //cbAIHardList.ForEach(cb => cb.Selected = false);
            //var cbAIHard = cbAIHardList.FirstOrDefault(cb => cb.ID == Setting.Current.Difficulty);
            //if (cbAIHard != null)
            //{
            //    cbAIHard.Selected = true;
            //}

        }

        public void Load()
        {

        }

        public void Update(GameTime gameTime)
        {
            float seconds = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            if (forward)
            {
                startElapsed = startElapsed + seconds;
            }
            else
            {
                startElapsed = startElapsed - seconds;
            }

            if (startElapsed <= 0 || startElapsed >= startCircle)
            {
                forward = !forward;

                if (startElapsed < 0)
                {
                    startElapsed = 0;
                }
                else if (startElapsed > startCircle)
                {
                    startElapsed = startCircle;
                }
            }

            var right = 2392f - 1280f;

            startPos = new Vector2(- startElapsed / startCircle * right, 0);

            if (isClosing)
            {
                menuTypeElapsed -= seconds;
                if (menuTypeElapsed <= 0)
                {
                    menuTypeElapsed = 0f;
                    MenuType = MenuType.None;
                    isClosing = false;
                }
            }
            else
            {
                menuTypeElapsed += seconds;
            }

            btPre.Visible = btNext.Visible = false;

            btPre1.Visible = btNext1.Visible = false;

            if (MenuType == MenuType.None)
            {
                textGameElapsed += seconds;

                if (textGameElapsed > 20f)
                {
                    textGameElapsed = 0f;
                }

                textLevel = Convert.ToInt32(textGameElapsed / 2f);

                btList.ForEach(bt => bt.Update());
            }
            else if (MenuType == MenuType.New)
            {
                btScenarioList.ForEach(bt => bt.Update());

                btPre.Position = new Vector2(40, 340);
                btNext.Position = new Vector2(40 + 125, 340);
                btPre.Visible = btNext.Visible = true;

                btPre1.Position = new Vector2(740, 340 + 208);
                btNext1.Position = new Vector2(740 + 125, 340 + 208);
                btPre1.Visible = btNext1.Visible = true;

                btScenarioSelectListPaged = GenericTools.GetPageList<ButtonTexture>(btScenarioSelectList, pageIndex.ToString(), 5, ref pageCount, ref page);

                for (int i = 0; i < btScenarioSelectListPaged.Count; i++)
                {
                    var btOne = btScenarioSelectListPaged[i];
                    btOne.Position = new Vector2(40, 80 + 55 * i);
                    btOne.Update();
                }

                btScenarioPlayersListPaged = GenericTools.GetPageList<ButtonTexture>(btScenarioPlayersList, pageIndex1.ToString(), 8, ref pageCount1, ref page1);

                for (int i = 0; i < btScenarioPlayersListPaged.Count; i++)
                {
                    var btOne = btScenarioPlayersListPaged[i];
                    btOne.Position = new Vector2(740, 80 + 55 * i);
                    btOne.Update();
                }

                if (CurrentScenario != null)
                {
                    CurrentScenario.Players = String.Join(",", btScenarioPlayersList.Where(bt => bt.Selected).Select(bt => bt.ID));
                }
            }
            else if (MenuType == MenuType.Config)
            {
                btScenarioList.ForEach(bt => bt.Update());

                btSettingList.ForEach(bt =>
                {
                    if (!String.IsNullOrEmpty(bt.ID))
                    {
                        bt.Visible = false;
                    }
                });

                if (CurrentSetting == "基本")
                {
                    btConfigList1.ForEach(bt => bt.Update());

                    var nsts = new NumericSetTextureF[] { nstViewDetail, nstGeneralBattleDead, nstGeneralYun, nstFeiZiYun, nstZhaoXian, nstSearchGen, nstZaiNan, nstDayInTurn };

                    foreach (var nst in nsts)
                    {
                        float nstValue = 0;
                        nst.Update(Vector2.Zero, ref nstValue);
                        if (nst.NowNumber <= nst.MinNumber)
                        {
                            nst.leftTexture.Enable = false;
                        }
                        else
                        {
                            nst.leftTexture.Enable = true;
                        }
                        if (nst.NowNumber >= nst.MaxNumber)
                        {
                            nst.rightTexture.Enable = false;
                        }
                        else
                        {
                            nst.rightTexture.Enable = true;
                        }
                    }

                    if (Session.globalVariablesTemp.TabListDetailLevel != (int)nstViewDetail.NowNumber)
                    {
                        Session.globalVariablesTemp.TabListDetailLevel = (int)nstViewDetail.NowNumber;
                    }

                    if (Session.globalVariablesTemp.OfficerDieInBattleRate != (int)nstGeneralBattleDead.NowNumber)
                    {
                        Session.globalVariablesTemp.OfficerDieInBattleRate = (int)nstGeneralBattleDead.NowNumber;
                    }

                    if (Session.globalVariablesTemp.getChildrenRate != (int)nstGeneralYun.NowNumber)
                    {
                        Session.globalVariablesTemp.getChildrenRate = (int)nstGeneralYun.NowNumber;
                    }

                    if (Session.globalVariablesTemp.hougongGetChildrenRate != (int)nstFeiZiYun.NowNumber)
                    {
                        Session.globalVariablesTemp.hougongGetChildrenRate = (int)nstFeiZiYun.NowNumber;
                    }

                    if (Session.globalVariablesTemp.ZhaoXianSuccessRate != (int)nstZhaoXian.NowNumber)
                    {
                        Session.globalVariablesTemp.ZhaoXianSuccessRate = (int)nstZhaoXian.NowNumber;
                    }

                    if (Session.globalVariablesTemp.CreateRandomOfficerChance != (int)nstSearchGen.NowNumber)
                    {
                        Session.globalVariablesTemp.CreateRandomOfficerChance = (int)nstSearchGen.NowNumber;
                    }

                    if (Session.globalVariablesTemp.zainanfashengjilv != (int)nstZaiNan.NowNumber)
                    {
                        Session.globalVariablesTemp.zainanfashengjilv = (int)nstZaiNan.NowNumber;
                    }

                    if (Session.parametersTemp.DayInTurn != (int)nstDayInTurn.NowNumber)
                    {
                        Session.parametersTemp.DayInTurn = (int)nstDayInTurn.NowNumber;
                    }

                }
                else if (CurrentSetting == "人物")
                {
                    btConfigList2.ForEach(bt => bt.Update());

                    var nsts = new NumericSetTextureF[] { nstMaxExperience, nstMaxSkill, nstPiLeiUp, nstPiLeiDown, nstExperienceUp, nstTreasureDiscover,
    nstFollowAttachPlus, nstFollowDefencePlus, nstSearchTime, nstGeneralChildsMax, nstGeneralChildsAppear, nstGeneralChildsSkill, nstChildsSkill, nstForbiddenDays };

                    foreach (var nst in nsts)
                    {
                        float nstValue = 0;
                        nst.Update(Vector2.Zero, ref nstValue);
                        if (nst.NowNumber <= nst.MinNumber)
                        {
                            nst.leftTexture.Enable = false;
                        }
                        else
                        {
                            nst.leftTexture.Enable = true;
                        }
                        if (nst.NowNumber >= nst.MaxNumber)
                        {
                            nst.rightTexture.Enable = false;
                        }
                        else
                        {
                            nst.rightTexture.Enable = true;
                        }
                    }

                    if (Session.globalVariablesTemp.maxExperience != (int)nstMaxExperience.NowNumber)
                    {
                        Session.globalVariablesTemp.maxExperience = (int)nstMaxExperience.NowNumber;
                    }

                    if (Session.globalVariablesTemp.MaxAbility != (int)nstMaxSkill.NowNumber)
                    {
                        Session.globalVariablesTemp.MaxAbility = (int)nstMaxSkill.NowNumber;
                    }

                    if (Session.globalVariablesTemp.TirednessIncrease != (int)nstPiLeiUp.NowNumber)
                    {
                        Session.globalVariablesTemp.TirednessIncrease = (int)nstPiLeiUp.NowNumber;
                    }

                    if (Session.globalVariablesTemp.TirednessDecrease != (int)nstPiLeiDown.NowNumber)
                    {
                        Session.globalVariablesTemp.TirednessDecrease = (int)nstPiLeiDown.NowNumber;
                    }

                    if (Session.parametersTemp.AbilityExperienceRate != (float)nstExperienceUp.NowNumber)
                    {
                        Session.parametersTemp.AbilityExperienceRate = (float)nstExperienceUp.NowNumber;
                    }

                    if (Session.parametersTemp.FindTreasureChance != (int)nstTreasureDiscover.NowNumber)
                    {
                        Session.parametersTemp.FindTreasureChance = (int)nstTreasureDiscover.NowNumber;
                    }

                    if (Session.parametersTemp.FollowedLeaderOffenceRateIncrement != (float)nstFollowAttachPlus.NowNumber)
                    {
                        Session.parametersTemp.FollowedLeaderOffenceRateIncrement = (float)nstFollowAttachPlus.NowNumber;
                    }

                    if (Session.parametersTemp.FollowedLeaderDefenceRateIncrement != (float)nstFollowDefencePlus.NowNumber)
                    {
                        Session.parametersTemp.FollowedLeaderDefenceRateIncrement = (float)nstFollowDefencePlus.NowNumber;
                    }

                    if (Session.parametersTemp.SearchDays != (int)nstSearchTime.NowNumber)
                    {
                        Session.parametersTemp.SearchDays = (int)nstSearchTime.NowNumber;
                    }

                    if (Session.globalVariablesTemp.OfficerChildrenLimit != (int)nstGeneralChildsMax.NowNumber)
                    {
                        Session.globalVariablesTemp.OfficerChildrenLimit = (int)nstGeneralChildsMax.NowNumber;
                        if (nstGeneralChildsMax.NowNumber >= 100)
                        {
                            Session.globalVariablesTemp.OfficerChildrenLimit = 100000;
                        }
                    }

                    if (Session.globalVariablesTemp.ChildrenAvailableAge != (int)nstGeneralChildsAppear.NowNumber)
                    {
                        Session.globalVariablesTemp.ChildrenAvailableAge = (int)nstGeneralChildsAppear.NowNumber;
                    }

                    if (Session.globalVariablesTemp.CreatedOfficerAbilityFactor != (float)nstGeneralChildsSkill.NowNumber)
                    {
                        Session.globalVariablesTemp.CreatedOfficerAbilityFactor = (float)nstGeneralChildsSkill.NowNumber;
                    }

                    if (Session.globalVariablesTemp.ChildrenAbilityFactor != (float)nstChildsSkill.NowNumber)
                    {
                        Session.globalVariablesTemp.ChildrenAbilityFactor = (float)nstChildsSkill.NowNumber;
                    }

                    if (Session.globalVariablesTemp.ProhibitFactionAgainstDestroyer != (float)nstForbiddenDays.NowNumber)
                    {
                        Session.globalVariablesTemp.ProhibitFactionAgainstDestroyer = (float)nstForbiddenDays.NowNumber;
                    }

                }
                else if (CurrentSetting == "参数")
                {
                    var nsts = new NumericSetTextureF[] { nstNeiZheng, nstXunLian, nstBuChong, nstZiJing, nstLiangCao, nstBuDui, nstJianZhu, nstRenKou, nstWeiCheng, nstHuoYan,
                          nstMaiLiangNongYe, nstMaiLiangShangYe, nstZiJingHuanLiang, nstLiangCaoHuanZiJing, nstJiqiaoDian, nstNeiZhengZiJing, nstBuChongZiJing, nstBuChongTongZhi, nstBuChongMinXin, nstQianDuZiJing,
                          nstShuoFuZiJing, nstBaoJiangZiJing, nstJieLaoZiJing, nstPoHuaiZiJing, nstShanDongZiJing, nstLiuYanZiJing, nstBingYiShangXian, nstBingYiZengLiang, nstBuDuiJingYan, nstTongShuaiGongJi };

                    foreach (var nst in nsts)
                    {
                        float nstValue = 0;
                        nst.Update(Vector2.Zero, ref nstValue);
                        if (nst.NowNumber <= nst.MinNumber)
                        {
                            nst.leftTexture.Enable = false;
                        }
                        else
                        {
                            nst.leftTexture.Enable = true;
                        }
                        if (nst.NowNumber >= nst.MaxNumber)
                        {
                            nst.rightTexture.Enable = false;
                        }
                        else
                        {
                            nst.rightTexture.Enable = true;
                        }
                    }

                    if (Session.parametersTemp.InternalRate != (float)nstNeiZheng.NowNumber)
                    {
                        Session.parametersTemp.InternalRate = (float)nstNeiZheng.NowNumber;
                    }

                    if (Session.parametersTemp.TrainingRate != (float)nstXunLian.NowNumber)
                    {
                        Session.parametersTemp.TrainingRate = (float)nstXunLian.NowNumber;
                    }

                    if (Session.parametersTemp.RecruitmentRate != (float)nstBuChong.NowNumber)
                    {
                        Session.parametersTemp.RecruitmentRate = (float)nstBuChong.NowNumber;
                    }

                    if (Session.parametersTemp.FundRate != (float)nstZiJing.NowNumber)
                    {
                        Session.parametersTemp.FundRate = (float)nstZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.FoodRate != (float)nstLiangCao.NowNumber)
                    {
                        Session.parametersTemp.FoodRate = (float)nstLiangCao.NowNumber;
                    }

                    if (Session.parametersTemp.TroopDamageRate != (float)nstBuDui.NowNumber)
                    {
                        Session.parametersTemp.TroopDamageRate = (float)nstBuDui.NowNumber;
                    }

                    if (Session.parametersTemp.ArchitectureDamageRate != (float)nstJianZhu.NowNumber)
                    {
                        Session.parametersTemp.ArchitectureDamageRate = (float)nstJianZhu.NowNumber;
                    }

                    if (Session.parametersTemp.DefaultPopulationDevelopingRate != (float)nstRenKou.NowNumber)
                    {
                        Session.parametersTemp.DefaultPopulationDevelopingRate = (float)nstRenKou.NowNumber;
                    }

                    if (Session.parametersTemp.SurroundArchitectureDominationUnit != (int)nstWeiCheng.NowNumber)
                    {
                        Session.parametersTemp.SurroundArchitectureDominationUnit = (int)nstWeiCheng.NowNumber;
                    }

                    if (Session.parametersTemp.FireDamageScale != (float)nstHuoYan.NowNumber)
                    {
                        Session.parametersTemp.FireDamageScale = (float)nstHuoYan.NowNumber;
                    }

                    if (Session.parametersTemp.BuyFoodAgriculture != (int)nstMaiLiangNongYe.NowNumber)
                    {
                        Session.parametersTemp.BuyFoodAgriculture = (int)nstMaiLiangNongYe.NowNumber;
                    }

                    if (Session.parametersTemp.SellFoodCommerce != (int)nstMaiLiangShangYe.NowNumber)
                    {
                        Session.parametersTemp.SellFoodCommerce = (int)nstMaiLiangShangYe.NowNumber;
                    }

                    if (Session.parametersTemp.FundToFoodMultiple != (int)nstZiJingHuanLiang.NowNumber)
                    {
                        Session.parametersTemp.FundToFoodMultiple = (int)nstZiJingHuanLiang.NowNumber;
                    }

                    if (Session.parametersTemp.FoodToFundDivisor != (int)nstLiangCaoHuanZiJing.NowNumber)
                    {
                        Session.parametersTemp.FoodToFundDivisor = (int)nstLiangCaoHuanZiJing.NowNumber;
                    }

                    if (Session.globalVariablesTemp.TechniquePointMultiple != (float)nstJiqiaoDian.NowNumber)
                    {
                        Session.globalVariablesTemp.TechniquePointMultiple = (float)nstJiqiaoDian.NowNumber;
                    }

                    if (Session.parametersTemp.InternalFundCost != (int)nstNeiZhengZiJing.NowNumber)
                    {
                        Session.parametersTemp.InternalFundCost = (int)nstNeiZhengZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.RecruitmentFundCost != (int)nstBuChongZiJing.NowNumber)
                    {
                        Session.parametersTemp.RecruitmentFundCost = (int)nstBuChongZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.RecruitmentDomination != (int)nstBuChongTongZhi.NowNumber)
                    {
                        Session.parametersTemp.RecruitmentDomination = (int)nstBuChongTongZhi.NowNumber;
                    }

                    if (Session.parametersTemp.RecruitmentMorale != (int)nstBuChongMinXin.NowNumber)
                    {
                        Session.parametersTemp.RecruitmentMorale = (int)nstBuChongMinXin.NowNumber;
                    }

                    if (Session.parametersTemp.ChangeCapitalCost != (int)nstQianDuZiJing.NowNumber)
                    {
                        Session.parametersTemp.ChangeCapitalCost = (int)nstQianDuZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.ConvincePersonCost != (int)nstShuoFuZiJing.NowNumber)
                    {
                        Session.parametersTemp.ConvincePersonCost = (int)nstShuoFuZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.RewardPersonCost != (int)nstBaoJiangZiJing.NowNumber)
                    {
                        Session.parametersTemp.RewardPersonCost = (int)nstBaoJiangZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.JailBreakArchitectureCost != (int)nstJieLaoZiJing.NowNumber)
                    {
                        Session.parametersTemp.JailBreakArchitectureCost = (int)nstJieLaoZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.DestroyArchitectureCost != (int)nstPoHuaiZiJing.NowNumber)
                    {
                        Session.parametersTemp.DestroyArchitectureCost = (int)nstPoHuaiZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.InstigateArchitectureCost != (int)nstShanDongZiJing.NowNumber)
                    {
                        Session.parametersTemp.InstigateArchitectureCost = (int)nstShanDongZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.GossipArchitectureCost != (int)nstLiuYanZiJing.NowNumber)
                    {
                        Session.parametersTemp.GossipArchitectureCost = (int)nstLiuYanZiJing.NowNumber;
                    }

                    if (Session.parametersTemp.MilitaryPopulationCap != (float)nstBingYiShangXian.NowNumber)
                    {
                        Session.parametersTemp.MilitaryPopulationCap = (float)nstBingYiShangXian.NowNumber;
                    }

                    if (Session.parametersTemp.MilitaryPopulationReloadQuantity != (float)nstBingYiZengLiang.NowNumber)
                    {
                        Session.parametersTemp.MilitaryPopulationReloadQuantity = (float)nstBingYiZengLiang.NowNumber;
                    }

                    if (Session.globalVariablesTemp.MaxMilitaryExperience != (int)nstBuDuiJingYan.NowNumber)
                    {
                        Session.globalVariablesTemp.MaxMilitaryExperience = (int)nstBuDuiJingYan.NowNumber;
                    }

                    if (Session.globalVariablesTemp.LeadershipOffenceRate != (float)nstTongShuaiGongJi.NowNumber)
                    {
                        Session.globalVariablesTemp.LeadershipOffenceRate = (float)nstTongShuaiGongJi.NowNumber;
                    }

                }
                else if (CurrentSetting == "电脑")
                {
                    btConfigList4.ForEach(bt => bt.Update());

                    var nsts = new NumericSetTextureF[] { nstDianNaoChuZhan, nstDianNaoShengTao, nstDianNaoYinWanJiaHeBing,
            nstDianNaoZiJing1, nstDianNaoZiJing2, nstDianNaoLiangCao1, nstDianNaoLiangCao2, nstDianNaoBuDuiGongJi1, nstDianNaoBuDuiGongJi2,
            nstDianNaoFangYu1, nstDianNaoFangYu2, nstDianNaoShangHai1, nstDianNaoShangHai2, nstDianNaoXunLian1, nstDianNaoXunLian2,
            nstDianNaoZhengBing1, nstDianNaoZhengBing2, nstDianNaoWuJiangJingYan1, nstDianNaoWuJiangJingYan2, nstDianNaoBuDuiJingYan1, nstDianNaoBuDuiJingYan2,
            nstDianNaoKangJi1, nstDianNaoKangJi2, nstDianNaoKangWei1, nstDianNaoKangWei2, nstDianNaoEWai1, nstDianNaoEWai2 };

                    foreach (var nst in nsts)
                    {
                        float nstValue = 0;

                        float origin = (float)nst.NowNumber;
                        nst.Update(Vector2.Zero, ref nstValue);
                        if (origin != nstValue)
                        {
                            setDifficultyToCustom(null, null);
                        }

                        if (nst.NowNumber <= nst.MinNumber)
                        {
                            nst.leftTexture.Enable = false;
                        }
                        else
                        {
                            nst.leftTexture.Enable = true;
                        }
                        if (nst.NowNumber >= nst.MaxNumber)
                        {
                            nst.rightTexture.Enable = false;
                        }
                        else
                        {
                            nst.rightTexture.Enable = true;
                        }
                    }

                    if (Session.parametersTemp.AIEncirclePlayerRate != (int)nstDianNaoShengTao.NowNumber)
                    {
                        Session.parametersTemp.AIEncirclePlayerRate = (int)nstDianNaoShengTao.NowNumber;
                    }

                    if (Session.globalVariablesTemp.AIMergeAgainstPlayer != (float)nstDianNaoYinWanJiaHeBing.NowNumber)
                    {
                        Session.globalVariablesTemp.AIMergeAgainstPlayer = (float)nstDianNaoYinWanJiaHeBing.NowNumber;
                    }

                    if (Session.globalVariablesTemp.AIExecutionRate != (int)nstDianNaoChuZhan.NowNumber)
                    {
                        Session.globalVariablesTemp.AIExecutionRate = (int)nstDianNaoChuZhan.NowNumber;
                    }

                    if (Session.parametersTemp.AIFundRate != (float)nstDianNaoZiJing1.NowNumber)
                    {
                        Session.parametersTemp.AIFundRate = (float)nstDianNaoZiJing1.NowNumber;
                        //setDifficultyToCustom(null, null);
                    }

                    if (Session.parametersTemp.AIFundYearIncreaseRate != (float)nstDianNaoZiJing2.NowNumber)
                    {
                        Session.parametersTemp.AIFundYearIncreaseRate = (float)nstDianNaoZiJing2.NowNumber;
                    }

                    if (Session.parametersTemp.AIFoodRate != (float)nstDianNaoLiangCao1.NowNumber)
                    {
                        Session.parametersTemp.AIFoodRate = (float)nstDianNaoLiangCao1.NowNumber;
                        //setDifficultyToCustom(null, null);
                    }

                    if (Session.parametersTemp.AIFoodYearIncreaseRate != (float)nstDianNaoLiangCao2.NowNumber)
                    {
                        Session.parametersTemp.AIFoodYearIncreaseRate = (float)nstDianNaoLiangCao2.NowNumber;
                    }

                    if (Session.parametersTemp.AITroopOffenceRate != (float)nstDianNaoBuDuiGongJi1.NowNumber)
                    {
                        Session.parametersTemp.AITroopOffenceRate = (float)nstDianNaoBuDuiGongJi1.NowNumber;
                        //setDifficultyToCustom(null, null);
                    }

                    if (Session.parametersTemp.AITroopOffenceYearIncreaseRate != (float)nstDianNaoBuDuiGongJi2.NowNumber)
                    {
                        Session.parametersTemp.AITroopOffenceYearIncreaseRate = (float)nstDianNaoBuDuiGongJi2.NowNumber;
                    }

                    if (Session.parametersTemp.AITroopDefenceRate != (float)nstDianNaoFangYu1.NowNumber)
                    {
                        Session.parametersTemp.AITroopDefenceRate = (float)nstDianNaoFangYu1.NowNumber;
                        //setDifficultyToCustom(null, null);
                    }

                    if (Session.parametersTemp.AITroopDefenceYearIncreaseRate != (float)nstDianNaoFangYu2.NowNumber)
                    {
                        Session.parametersTemp.AITroopDefenceYearIncreaseRate = (float)nstDianNaoFangYu2.NowNumber;
                    }

                    if (Session.parametersTemp.AIArchitectureDamageRate != (float)nstDianNaoShangHai1.NowNumber)
                    {
                        Session.parametersTemp.AIArchitectureDamageRate = (float)nstDianNaoShangHai1.NowNumber;
                        //setDifficultyToCustom(null, null);
                    }

                    if (Session.parametersTemp.AIArchitectureDamageYearIncreaseRate != (float)nstDianNaoShangHai2.NowNumber)
                    {
                        Session.parametersTemp.AIArchitectureDamageYearIncreaseRate = (float)nstDianNaoShangHai2.NowNumber;
                    }

                    if (Session.parametersTemp.AITrainingSpeedRate != (float)nstDianNaoXunLian1.NowNumber)
                    {
                        Session.parametersTemp.AITrainingSpeedRate = (float)nstDianNaoXunLian1.NowNumber;
                        //setDifficultyToCustom(null, null);
                    }

                    if (Session.parametersTemp.AITrainingSpeedYearIncreaseRate != (float)nstDianNaoXunLian2.NowNumber)
                    {
                        Session.parametersTemp.AITrainingSpeedYearIncreaseRate = (float)nstDianNaoXunLian2.NowNumber;
                    }

                    if (Session.parametersTemp.AIRecruitmentSpeedRate != (float)nstDianNaoZhengBing1.NowNumber)
                    {
                        Session.parametersTemp.AIRecruitmentSpeedRate = (float)nstDianNaoZhengBing1.NowNumber;
                        //setDifficultyToCustom(null, null);
                    }

                    if (Session.parametersTemp.AIRecruitmentSpeedYearIncreaseRate != (float)nstDianNaoZhengBing2.NowNumber)
                    {
                        Session.parametersTemp.AIRecruitmentSpeedYearIncreaseRate = (float)nstDianNaoZhengBing2.NowNumber;
                    }

                    if (Session.parametersTemp.AIOfficerExperienceRate != (float)nstDianNaoWuJiangJingYan1.NowNumber)
                    {
                        Session.parametersTemp.AIOfficerExperienceRate = (float)nstDianNaoWuJiangJingYan1.NowNumber;
                    }

                    if (Session.parametersTemp.AIOfficerExperienceYearIncreaseRate != (float)nstDianNaoWuJiangJingYan2.NowNumber)
                    {
                        Session.parametersTemp.AIOfficerExperienceYearIncreaseRate = (float)nstDianNaoWuJiangJingYan2.NowNumber;
                    }

                    if (Session.parametersTemp.AIArmyExperienceRate != (float)nstDianNaoBuDuiJingYan1.NowNumber)
                    {
                        Session.parametersTemp.AIArmyExperienceRate = (float)nstDianNaoBuDuiJingYan1.NowNumber;
                    }

                    if (Session.parametersTemp.AIArmyExperienceYearIncreaseRate != (float)nstDianNaoBuDuiJingYan2.NowNumber)
                    {
                        Session.parametersTemp.AIArmyExperienceYearIncreaseRate = (float)nstDianNaoBuDuiJingYan2.NowNumber;
                    }

                    if (Session.parametersTemp.AIAntiStratagem != (int)nstDianNaoKangJi1.NowNumber)
                    {
                        Session.parametersTemp.AIAntiStratagem = (int)nstDianNaoKangJi1.NowNumber;
                    }

                    if (Session.parametersTemp.AIAntiStratagemIncreaseRate != (float)nstDianNaoKangJi2.NowNumber)
                    {
                        Session.parametersTemp.AIAntiStratagemIncreaseRate = (float)nstDianNaoKangJi2.NowNumber;
                    }

                    if (Session.parametersTemp.AIAntiSurround != (int)nstDianNaoKangWei1.NowNumber)
                    {
                        Session.parametersTemp.AIAntiSurround = (int)nstDianNaoKangWei1.NowNumber;
                    }

                    if (Session.parametersTemp.AIAntiSurroundIncreaseRate != (float)nstDianNaoKangWei2.NowNumber)
                    {
                        Session.parametersTemp.AIAntiSurroundIncreaseRate = (float)nstDianNaoKangWei2.NowNumber;
                    }

                    if (Session.parametersTemp.AIExtraPerson != (float)nstDianNaoEWai1.NowNumber)
                    {
                        Session.parametersTemp.AIExtraPerson = (float)nstDianNaoEWai1.NowNumber;
                    }

                    if (Session.parametersTemp.AIExtraPersonIncreaseRate != (float)nstDianNaoEWai2.NowNumber)
                    {
                        Session.parametersTemp.AIExtraPersonIncreaseRate = (float)nstDianNaoEWai2.NowNumber;
                    }

                    cbAIHardList.ForEach(cb => cb.Update());
                }

                lbSettingList.ForEach(lb =>
                {
                    lb.Update(null, true);
                    if (lb.MouseOver || lb.Text == CurrentSetting)
                    {
                        lb.Color = PlatformColor.DarkRed;
                    }
                    else
                    {
                        lb.Color = Color.Blue;
                    }
                });

            }
            else if (MenuType == MenuType.Save)
            {
                btSaveList.ForEach(bt => bt.Update());

                btPre.Position = new Vector2(530, 355 + 268);
                btNext.Position = new Vector2(530 + 125, 355 + 268);
                btPre.Visible = btNext.Visible = true;

                btScenarioSelectListPaged = GenericTools.GetPageList<ButtonTexture>(btScenarioSelectList, pageIndex.ToString(), 10, ref pageCount, ref page);

                for (int i = 0; i < btScenarioSelectListPaged.Count; i++)
                {
                    var btOne = btScenarioSelectListPaged[i];
                    btOne.Position = new Vector2(40, 45 + 53 * i);
                    btOne.Update();
                }
            }
            else if (MenuType == MenuType.Setting)
            {
                //tbBattleSpeed.Update(seconds);
                //tbBattleSpeed.HandleInput(seconds);

                btSettingList.ForEach(bt =>
                {
                    bt.Visible = true;
                    bt.Update();
                });

                //btSettingList.Where(bt => buttons.Contains(bt.ID)).NullToEmptyList().ForEach(bt =>
                //{
                //    bt.Visible = true;
                //});

                //btSettingList.ForEach(bt =>
                //{
                //    bt.Update();
                //});

                //tbGamerName.Update(seconds);
                //tbGamerName.HandleInput(seconds);

                if (!Platform.IsMobilePlatForm)
                {
                    btnSmallResolution.Enable = btnLargeResolution.Enable = true;

                    if (AvaliableResolutions.First() == Setting.Current.Resolution)
                    {
                        btnSmallResolution.Enable = false;
                    }

                    if (AvaliableResolutions.Last() == Setting.Current.Resolution)
                    {
                        btnLargeResolution.Enable = false;
                    }

                    btnSmallResolution.Update();

                    btnLargeResolution.Update();
                }

                //btnTextureAlpha.Update();

                Setting.Current.GamerName = tbGamerName.Text.NullToStringTrim();

                var nsts = new NumericSetTextureF[] { nstMusic, nstSound, nstArmySpeed, nstDialogTime, nstBattleSpeed, nstAutoSaveTime };

                foreach (var nst in nsts)
                {
                    float nstValue = 0;
                    nst.Update(Vector2.Zero, ref nstValue);
                    if (nst.NowNumber <= nst.MinNumber)
                    {
                        nst.leftTexture.Enable = false;
                    }
                    else
                    {
                        nst.leftTexture.Enable = true;
                    }
                    if (nst.NowNumber >= nst.MaxNumber)
                    {
                        nst.rightTexture.Enable = false;
                    }
                    else
                    {
                        nst.rightTexture.Enable = true;
                    }
                }

                if (Setting.Current.GlobalVariables.AutoSaveFrequency != (int)nstAutoSaveTime.NowNumber)
                {
                    Setting.Current.GlobalVariables.AutoSaveFrequency = (int)nstAutoSaveTime.NowNumber;
                }

                if (Setting.Current.GlobalVariables.TroopMoveSpeed != (int)nstArmySpeed.NowNumber)
                {
                    Setting.Current.GlobalVariables.TroopMoveSpeed = (int)nstArmySpeed.NowNumber;
                }

                if (Setting.Current.GlobalVariables.DialogShowTime != (int)nstDialogTime.NowNumber)
                {
                    Setting.Current.GlobalVariables.DialogShowTime = (int)nstDialogTime.NowNumber;
                }

                if (Setting.Current.MusicVolume != (int)nstMusic.NowNumber)
                {
                    Setting.Current.MusicVolume = (int)nstMusic.NowNumber;
                    Platform.Current.SetMusicVolume((int)Setting.Current.MusicVolume);
                }
                if (Setting.Current.SoundVolume != (int)nstSound.NowNumber)
                {
                    Setting.Current.SoundVolume = (int)nstSound.NowNumber;
                    Platform.Current.PlayEffect(@"Content\Sound\Move");
                }

            }
            else if (MenuType == MenuType.About)
            {
                btAboutList.ForEach(bt => bt.Update());
            }
            else if (MenuType == MenuType.Start)
            {
                btAboutList.ForEach(bt => bt.Update());
            }

            btPre.Enable = pageIndex > 1;
            btNext.Enable = pageIndex < pageCount;
            btPre.Update();
            btNext.Update();

            btPre1.Enable = pageIndex1 > 1;
            btNext1.Enable = pageIndex1 < pageCount1;
            btPre1.Update();
            btNext1.Update();

        }

        public void Draw(GameTime gameTime)
        {
            float alpha = menuTypeElapsed <= 0.5f ? menuTypeElapsed * 2 : 1f;

            //CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Start.jpg", Vector2.Zero, Color.White, 1f);

            CacheManager.Draw(@"Content\Textures\Resources\Start\Start01.jpg", startPos, Color.White);
            //CacheManager.Draw(@"Content\Textures\Resources\Start\Start.jpg", new Rectangle(0, 0, , Color.White);

            CacheManager.Draw(@"Content\Textures\Resources\Start\Logo.png", new Vector2(380, 50), Color.White);

            CacheManager.Draw(@"Content\Textures\Resources\Start\Words.png", new Vector2(380, 260), Color.White);

            if (MenuType == MenuType.None)
            {
                btList.ForEach(bt => bt.Draw());

                for (int i = 0; i < texts.Length && i <= textLevel; i++)
                {
                    var text = String.Join(System.Environment.NewLine, texts[i].ToArray());
                    CacheManager.DrawString(Session.Current.Font, text, new Vector2(720 - 65 * i, 230), Color.White, 0f, Vector2.Zero, 1.5f, SpriteEffects.None, 0f);
                }
            }
            else if (MenuType == MenuType.New)
            {
                CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Scenario.jpg", Vector2.Zero, Color.White * alpha, 1f);

                CacheManager.DrawString(Session.Current.Font, "剧本选择", new Vector2(38, 27), PlatformColor.DarkRed * alpha);

                CacheManager.DrawString(Session.Current.Font, "剧本介绍", new Vector2(38, 408), PlatformColor.DarkRed * alpha);

                CacheManager.DrawString(Session.Current.Font, "势力选择", new Vector2(728, 27), PlatformColor.DarkRed * alpha);

                btScenarioList.ForEach(bt => bt.Draw(null, Color.White * alpha));

                btScenarioSelectListPaged.ForEach(bt =>
                {
                    int index = btScenarioSelectList.IndexOf(bt);
                    if (index >= 0)
                    {
                        var sce = ScenarioList[index];

                        if (sce != null && !String.IsNullOrEmpty(sce.Title))
                        {
                            var time = DateTime.Parse(sce.Time.Trim()).ToString("yyyy-MM-dd");

                            bt.Draw(null, Color.White * alpha);
                            CacheManager.DrawString(Session.Current.Font, sce.Title + " " + time, bt.Position + new Vector2(45, 2), Color.Black * alpha);
                        }
                    }
                });

                btScenarioPlayersListPaged.ForEach(bt =>
                {
                    int index = btScenarioPlayersList.IndexOf(bt);
                    if (index >= 0)
                    {
                        var play = CurrentScenario.Names.Split(',').NullToEmptyArray()[index];

                        bt.Draw(null, Color.White * alpha);

                        CacheManager.DrawString(Session.Current.Font, play, bt.Position + new Vector2(45, 2), Color.Black * alpha);
                    }
                });

                if (CurrentScenario != null)
                {
                    int resultRow = 0;
                    int resultWidth = 0;
                    CacheManager.DrawString(Session.Current.Font, CurrentScenario.Desc.SplitLineString(22, 7, ref resultRow, ref resultWidth), new Vector2(40, 460), Color.Black * alpha);
                }

                if (!String.IsNullOrEmpty(message))
                {
                    CacheManager.DrawString(Session.Current.Font, message, btNext.Position + new Vector2(70, 6), Color.Red * alpha);
                }
            }
            else if (MenuType == MenuType.Config)
            {
                CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Common.jpg", Vector2.Zero, Color.White * alpha, 1f);

                btScenarioList.ForEach(bt => bt.Draw(null, Color.White * alpha));

                lbSettingList.ForEach(lb => lb.Draw(alpha));

                int heightBase = 90;

                int height = 85;

                int left1 = 50 + 60;

                int left2 = 50 + 620 + 60;

                if (CurrentSetting == "基本")
                {
                    btConfigList1.ForEach(bt => bt.Draw());

                    var nsts = new NumericSetTextureF[] { nstViewDetail, nstGeneralBattleDead, nstGeneralYun, nstFeiZiYun, nstZhaoXian, nstSearchGen, nstZaiNan, nstDayInTurn };

                    foreach (var nst in nsts)
                    {
                        nst.Draw(alpha);
                    }

                    //CacheManager.DrawString(Session.Current.Font, "粮道系统", new Vector2(left1, heightBase), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "出仕相性考虑有效", new Vector2(left1, heightBase + height * 0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "部队速率有效", new Vector2(left1, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "武将可能在单挑中死亡", new Vector2(left1, heightBase + height * 1.0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "年龄有效", new Vector2(left1, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "年龄影响能力", new Vector2(left1, heightBase + height * 2.0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "武将有可能独立", new Vector2(left1, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "容许势力合并", new Vector2(left1, heightBase + height * 3.0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "人口小于兵力时禁止征兵", new Vector2(left1, heightBase + height * 3.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "默认开启天眼", new Vector2(left1, heightBase + height * 4.0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "只有异族能納妃", new Vector2(left1, heightBase + height * 4.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "开启作弊功能", new Vector2(left2, heightBase), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "硬核模式(禁止S/L)", new Vector2(left2, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "生成虚拟子嗣", new Vector2(left2, heightBase + height * 1f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "使用简易AI战斗算法", new Vector2(left2, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "资料显示详细度", new Vector2(left2 - 60, heightBase + height * 2f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "武将战死率", new Vector2(left2 - 60, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "武将怀孕机率", new Vector2(left2 - 60, heightBase + height * 3f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "妃子怀孕机率", new Vector2(left2 - 60, heightBase + height * 3.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "招贤成功率", new Vector2(left2 - 60, heightBase + height * 4f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "搜索武将成功率", new Vector2(left2 - 60, heightBase + height * 4.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "灾难发生几率(1/此数)", new Vector2(left2 - 60, heightBase + height * 5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "每回天数", new Vector2(left2 - 60, heightBase + height * 5.5f), Color.Black * alpha);
                }
                else if (CurrentSetting == "人物")
                {
                    btConfigList2.ForEach(bt => bt.Draw());

                    var nsts = new NumericSetTextureF[] { nstMaxExperience, nstMaxSkill, nstPiLeiUp, nstPiLeiDown, nstExperienceUp, nstTreasureDiscover,
    nstFollowAttachPlus, nstFollowDefencePlus, nstSearchTime, nstGeneralChildsMax, nstGeneralChildsAppear, nstGeneralChildsSkill, nstChildsSkill, nstForbiddenDays };

                    foreach (var nst in nsts)
                    {
                        nst.Draw(alpha);
                    }

                    left2 = 50 + 470;

                    CacheManager.DrawString(Session.Current.Font, "虚拟子嗣能力可超越上限", new Vector2(left1, heightBase + height * 0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "武将关系会随游戏调整", new Vector2(left1, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "最大经验", new Vector2(left1 - 60, heightBase + height * 1f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "最大能力", new Vector2(left1 - 60, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "疲累度增长", new Vector2(left1 - 60, heightBase + height * 2f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "疲累度下降", new Vector2(left1 - 60, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "经验增长率", new Vector2(left1 - 60, heightBase + height * 3.0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "宝物发现概率", new Vector2(left1 - 60, heightBase + height * 3.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "追随将领攻击力加成", new Vector2(left2, heightBase + height * 0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "追随将领防御力加成", new Vector2(left2, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "搜索时间", new Vector2(left2, heightBase + height * 1f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "武将子女上限", new Vector2(left2, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "子女登场年龄", new Vector2(left2, heightBase + height * 2f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "生成武将能力乘数", new Vector2(left2, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "生成子女能力乘数", new Vector2(left2, heightBase + height * 3f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "禁仕天数乘数", new Vector2(left2, heightBase + height * 3.5f), Color.Black * alpha);

                }
                else if (CurrentSetting == "参数")
                {
                    var nsts = new NumericSetTextureF[] { nstNeiZheng, nstXunLian, nstBuChong, nstZiJing, nstLiangCao, nstBuDui, nstJianZhu, nstRenKou, nstWeiCheng, nstHuoYan,
                          nstMaiLiangNongYe, nstMaiLiangShangYe, nstZiJingHuanLiang, nstLiangCaoHuanZiJing, nstJiqiaoDian, nstNeiZhengZiJing, nstBuChongZiJing, nstBuChongTongZhi, nstBuChongMinXin, nstQianDuZiJing,
                          nstShuoFuZiJing, nstBaoJiangZiJing, nstJieLaoZiJing, nstPoHuaiZiJing, nstShanDongZiJing, nstLiuYanZiJing, nstBingYiShangXian, nstBingYiZengLiang, nstBuDuiJingYan, nstTongShuaiGongJi };

                    foreach (var nst in nsts)
                    {
                        nst.Draw(alpha);
                    }

                    left1 = left1 - 60;

                    CacheManager.DrawString(Session.Current.Font, "内政速率", new Vector2(left1, heightBase), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "训练速率", new Vector2(left1, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "补充速率", new Vector2(left1, heightBase + height * 1f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "资金收入率", new Vector2(left1, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "粮草收入率", new Vector2(left1, heightBase + height * 2.0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "部队伤害率", new Vector2(left1, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "建筑伤害率", new Vector2(left1, heightBase + height * 3f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "人口默认增长", new Vector2(left1, heightBase + height * 3.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "围城统治单位", new Vector2(left1, heightBase + height * 4f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "火焰伤害率", new Vector2(left1, heightBase + height * 4.5f), Color.Black * alpha);


                    left2 = left1 + 400;

                    CacheManager.DrawString(Session.Current.Font, "买粮所需农业", new Vector2(left2, heightBase), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "卖粮所需商业", new Vector2(left2, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "资金换粮乘数", new Vector2(left2, heightBase + height * 1f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "粮草换金除数", new Vector2(left2, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "技巧点乘数", new Vector2(left2, heightBase + height * 2.0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "内政资金单位", new Vector2(left2, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "补充资金单位", new Vector2(left2, heightBase + height * 3f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "补充最小统治", new Vector2(left2, heightBase + height * 3.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "补充最小民心", new Vector2(left2, heightBase + height * 4f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "迁都资金单位", new Vector2(left2, heightBase + height * 4.5f), Color.Black * alpha);

                    int left3 = left2 + 400;

                    CacheManager.DrawString(Session.Current.Font, "说服所需资金", new Vector2(left3, heightBase), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "褒奖所需资金", new Vector2(left3, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "劫牢所需资金", new Vector2(left3, heightBase + height * 1f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "破坏所需资金", new Vector2(left3, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "煽动所需资金", new Vector2(left3, heightBase + height * 2.0f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "流言所需资金", new Vector2(left3, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "兵役上限", new Vector2(left3, heightBase + height * 3f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "兵役增量倍数", new Vector2(left3, heightBase + height * 3.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "部队经验上限", new Vector2(left3, heightBase + height * 4f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "统率攻击影响", new Vector2(left3, heightBase + height * 4.5f), Color.Black * alpha);

                }
                else if (CurrentSetting == "电脑")
                {
                    var nsts = new NumericSetTextureF[] { nstDianNaoChuZhan, nstDianNaoShengTao, nstDianNaoYinWanJiaHeBing,
            nstDianNaoZiJing1, nstDianNaoZiJing2, nstDianNaoLiangCao1, nstDianNaoLiangCao2, nstDianNaoBuDuiGongJi1, nstDianNaoBuDuiGongJi2,
            nstDianNaoFangYu1, nstDianNaoFangYu2, nstDianNaoShangHai1, nstDianNaoShangHai2, nstDianNaoXunLian1, nstDianNaoXunLian2,
            nstDianNaoZhengBing1, nstDianNaoZhengBing2, nstDianNaoWuJiangJingYan1, nstDianNaoWuJiangJingYan2, nstDianNaoBuDuiJingYan1, nstDianNaoBuDuiJingYan2,
            nstDianNaoKangJi1, nstDianNaoKangJi2, nstDianNaoKangWei1, nstDianNaoKangWei2, nstDianNaoEWai1, nstDianNaoEWai2 };

                    int i = 1;
                    foreach (var nst in nsts)
                    {
                        nst.Draw(alpha);
                        if (i > 3 && i % 2 == 0)
                        {
                            CacheManager.DrawString(Session.Current.Font, "基", nst.Position + new Vector2(-30, 0), Color.Black * alpha);

                            CacheManager.DrawString(Session.Current.Font, "增", nst.Position + new Vector2(220, 0), Color.Black * alpha);
                        }
                        i++;
                    }

                    btConfigList4.ForEach(bt => bt.Draw());

                    CacheManager.DrawString(Session.Current.Font, "难度", new Vector2(left2 - 240, 40), Color.Black * alpha);
                    int inHard = 0;
                    cbAIHardList.ForEach(cb =>
                    {
                        cb.Draw(null, Color.White * alpha);

                        CacheManager.DrawString(Session.Current.Font, hards2[inHard], new Vector2(cb.Position.X + 40 - 2, cb.Position.Y), Color.Black * alpha);

                        inHard++;
                    });

                    CacheManager.DrawString(Session.Current.Font, "电脑必成功说服没势力俘虏", new Vector2(left1, heightBase), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑必成功说服城中在野武将", new Vector2(left1, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑必成功说服玩家势力俘虏", new Vector2(left1, heightBase + height * 1f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑说服俘虏限忠诚不满100", new Vector2(left1, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑视玩家为最大敌人", new Vector2(left1, heightBase + height * 2f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "收入缩减率对玩家有效", new Vector2(left1, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "收入缩减率对电脑有效", new Vector2(left1, heightBase + height * 3f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "忽略电脑君主的战略倾向", new Vector2(left1, heightBase + height * 3.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑优先处斩能力高者", new Vector2(left1, heightBase + height * 4f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑处斩机率", new Vector2(left1 - 60, heightBase + height * 4.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑声讨玩家", new Vector2(left1 - 60, heightBase + height * 5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑因玩家合并", new Vector2(left1 - 60, heightBase + height * 5.5f), Color.Black * alpha);

                    left2 = left2 - 240;

                    CacheManager.DrawString(Session.Current.Font, "电脑资金收入率", new Vector2(left2, heightBase), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑粮草收入率", new Vector2(left2, heightBase + height * 0.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑部队攻击力乘数", new Vector2(left2, heightBase + height * 1f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑部队防御力乘数", new Vector2(left2, heightBase + height * 1.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑建筑伤害率乘数", new Vector2(left2, heightBase + height * 2f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑训练速度", new Vector2(left2, heightBase + height * 2.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑征兵速度", new Vector2(left2, heightBase + height * 3f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑武将经验获得率", new Vector2(left2, heightBase + height * 3.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑部队经验获得率", new Vector2(left2, heightBase + height * 4f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑部队抗计率", new Vector2(left2, heightBase + height * 4.5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑部队抗围率", new Vector2(left2, heightBase + height * 5f), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "电脑额外人才", new Vector2(left2, heightBase + height * 5.5f), Color.Black * alpha);

                }

            }
            else if (MenuType == MenuType.Save)
            {
                CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Common.jpg", Vector2.Zero, Color.White * alpha, 1f);

                btSaveList.ForEach(bt => bt.Draw(null, Color.White * alpha));

                btScenarioSelectListPaged.ForEach(bt =>
                {
                    int index = btScenarioSelectList.IndexOf(bt);
                    if (index >= 0)
                    {
                        CacheManager.DrawString(Session.Current.Font, index.ToString(), bt.Position + new Vector2(45, 2), Color.Black * alpha);

                        var sce = ScenarioList[index];
                        if (sce == null || String.IsNullOrEmpty(sce.Title))
                        {
                            bt.Enable = false;
                            CacheManager.DrawString(Session.Current.Font, "暂无进度", bt.Position + new Vector2(45 + 100, 2), Color.Black * alpha);
                        }
                        else
                        {
                            bt.Enable = true;
                            CacheManager.DrawString(Session.Current.Font, sce.Title.WordsSubString(25), bt.Position + new Vector2(45 + 100, 2), Color.Blue * alpha);
                            CacheManager.DrawString(Session.Current.Font, sce.Time.ToSeasonDate(), bt.Position + new Vector2(45 + 600, 2), Color.Blue * alpha);
                            CacheManager.DrawString(Session.Current.Font, sce.Info, bt.Position + new Vector2(45 + 800, 2), Color.Blue * alpha);

                            int playTime;
                            if (int.TryParse(sce.PlayTime, out playTime))
                            {
                                string viewTime = "(" + (playTime / 60 / 60) + ":" + (playTime / 60 % 60) + ")";

                                CacheManager.DrawString(Session.Current.Font, viewTime, bt.Position + new Vector2(45 + 900, 2), Color.Blue * alpha);
                            }
                        }

                        if (index == 0)
                        {
                            CacheManager.DrawString(Session.Current.Font, "自动", bt.Position + new Vector2(45 + 35, 2), PlatformColor.DarkRed * alpha);
                        }

                        bt.Draw(null, Color.White * alpha);
                    }
                });

                if (!String.IsNullOrEmpty(message))
                {
                    CacheManager.DrawString(Session.Current.Font, message, new Vector2(40, 560), Color.Red * alpha);
                }
            }
            else if (MenuType == MenuType.Setting)
            {
                CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Common.jpg", Vector2.Zero, Color.White * alpha, 1f);

                btSettingList.ForEach(bt => bt.Draw(null, Color.White * alpha));

                CacheManager.DrawString(Session.Current.Font, "语言", new Vector2(50, 120), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "简体中文", new Vector2(50 + 100 + 50, 120), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "传统中文", new Vector2(50 + 100 + 200 + 50, 120), Color.Black * alpha);

                //CacheManager.DrawString(Session.Current.Font, "战斗", new Vector2(50, 120 + 60 * 2), Color.Black * alpha);

                //CacheManager.DrawString(Session.Current.Font, "快速战斗的速度", new Vector2(50 + 300, 120 + 60 * 2), Color.Black * alpha);

                if (!Platform.IsMobilePlatForm)
                {
                    CacheManager.DrawString(Session.Current.Font, "分辨", new Vector2(50, 120 + 60 * 4), Color.Black * alpha);

                    btnSmallResolution.Alpha = alpha;
                    btnSmallResolution.Draw();

                    CacheManager.DrawString(Session.Current.Font, Setting.Current.Resolution, new Vector2(220, 120 + 60 * 4), Color.Black * alpha);

                    btnLargeResolution.Alpha = alpha;
                    btnLargeResolution.Draw();
                }

                //tbBattleSpeed.tranAlpha = alpha;
                //tbBattleSpeed.Draw();

                //tbGamerName.tranAlpha = alpha;
                //tbGamerName.Draw();

                CacheManager.DrawString(Session.Current.Font, "界面", new Vector2(50, 120 + 60), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "窗口模式", new Vector2(50 + 100 + 50, 120 + 60), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "全屏模式", new Vector2(50 + 100 + 200 + 50, 120 + 60), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "音乐", new Vector2(50, 120 + 60 * 2), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "声音", new Vector2(50, 120 + 60 * 3), Color.Black * alpha);

                nstMusic.Draw(alpha);
                nstSound.Draw(alpha);

                nstArmySpeed.Draw(alpha);
                nstDialogTime.Draw(alpha);
                nstBattleSpeed.Draw(alpha);
                nstAutoSaveTime.Draw(alpha);

                int height = 85;

                int left = 50 + 620 + 60;

                CacheManager.DrawString(Session.Current.Font, "播放一般音效", new Vector2(left, 120), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "播放战斗音效", new Vector2(left, 120 + height * 0.5f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "显示地图烟幕", new Vector2(left, 120 + height * 1), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "显示部队动画", new Vector2(left, 120 + height * 1.5f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "被攻击时暂停游戏", new Vector2(left, 120 + height * 2), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "从某列表中选择单一项时单击即确定", new Vector2(left, 120 + height * 2.5f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "不提示小型设施的建设完成", new Vector2(left, 120 + height * 3f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "提示人口的迁移", new Vector2(left, 120 + height * 3.5f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "提示1000人以下的人口迁移", new Vector2(left, 120 + height * 4f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "自动存档，密度（分钟）", new Vector2(left, 120 + height * 4.5f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "游戏窗体失去焦点时继续运行", new Vector2(left, 120 + height * 5f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "部队移动（越大越慢）", new Vector2(50, 120 + height * 3.5f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "对话窗显示时间", new Vector2(50, 120 + height * 4f), Color.Black * alpha);

                CacheManager.DrawString(Session.Current.Font, "战斗速度", new Vector2(50, 120 + height * 4.5f), Color.Black * alpha);

                //btnTextureAlpha.Alpha = alpha;
                //btnTextureAlpha.Draw();

                //CacheManager.DrawString(Session.Current.Font, "处理材质Alpha", new Vector2(50 + 100, 120 + height * 5f), Color.Black * alpha);

            }
            else if (MenuType == MenuType.About)
            {
                CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Common.jpg", Vector2.Zero, Color.White * alpha, 1f);

                CacheManager.DrawString(Session.Current.Font, String.Join(System.Environment.NewLine, aboutLines), new Vector2(40, 40), Color.Black * alpha, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

                btAboutList.ForEach(bt => bt.Draw(null, Color.White * alpha));
            }
            else if (MenuType == MenuType.Start)
            {
                CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Common.jpg", Vector2.Zero, Color.White * alpha, 1f);

                CacheManager.DrawString(Session.Current.Font, String.Join(System.Environment.NewLine, startLines), new Vector2(40, 40), Color.Black * alpha, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 0f);

                btAboutList.ForEach(bt => bt.Draw(null, Color.White * alpha));
            }

            btPre.Draw(null, Color.White * alpha);
            btNext.Draw(null, Color.White * alpha);

            btPre1.Draw(null, Color.White * alpha);
            btNext1.Draw(null, Color.White * alpha);

            if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop || Platform.PlatFormType == PlatFormType.UWP && !Platform.IsMobile)
            {
                CacheManager.DrawAvatar(@"Content\Textures\Resources\MouseArrow\Normal.png", InputManager.Position, Color.White * alpha, 1f);
            }

            //var str = @"Content/Textures/GameComponents/tupianwenzi/Data/tupian/TruceDiplomaticRelation.jpg";

            //CacheManager.DrawAvatar(str, Vector2.Zero, Color.White, 1f);

            //str = @"Content/Textures/GameComponents/tupianwenzi/Data/tupian/beiyue.jpg";

            //CacheManager.DrawAvatar(str, new Vector2(0, 300), Color.White, 1f);

            //str = @"Content/Textures/GameComponents/PersonPortrait/Images/Default/19207.jpg";

            //CacheManager.DrawAvatar(str, new Vector2(500, 200), Color.White, 1f);

        }

        public void ExitScreen()
        {

        }

    }
}
