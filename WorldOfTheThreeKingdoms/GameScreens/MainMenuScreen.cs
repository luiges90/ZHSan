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
        Save,
        Setting,
        About
    }

    public class MainMenuScreen
    {
        public MenuType MenuType = MenuType.None;

        float textGameElapsed = 0f;

        float menuTypeElapsed = 0f;

        bool isClosing = false;

        List<ButtonTexture> btList = null;

        List<ButtonTexture> btScenarioList = null;

        List<ButtonTexture> btSaveList = null;

        List<ButtonTexture> btSettingList = null;

        List<LinkButton> lbSettingList = null;

        ButtonTexture btPre, btNext, btPre1, btNext1;

        int pageIndex = 1;
        int pageCount = 0;
#pragma warning disable CS0414 // The field 'MainMenuScreen.pageid' is assigned but its value is never used
        int pageid = 1;
#pragma warning restore CS0414 // The field 'MainMenuScreen.pageid' is assigned but its value is never used
        int page = 0;

        int pageIndex1 = 1;
        int pageCount1 = 0;
#pragma warning disable CS0414 // The field 'MainMenuScreen.pageid1' is assigned but its value is never used
        int pageid1 = 1;
#pragma warning restore CS0414 // The field 'MainMenuScreen.pageid1' is assigned but its value is never used
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

        NumericSetTexture nstMusic, nstSound;

        TextBox tbGamerName = null;
        TextBox tbBattleSpeed = null;

        List<ButtonTexture> cbAIHardList = null;

        List<ButtonTexture> btAboutList = null;

        int textLevel = 0;

        string[] texts = new string[]
        {
            "滚滚长江东逝水",
            "浪花淘尽英雄",
            "是非成败转头空",
            "青山依旧在",
            "几度夕阳红",
            "白发渔樵江渚上",
            "惯看秋月春风",
            "一壶浊酒喜相逢",
            "古今多少事",
            "都付笑谈中"
        };

        string[] aboutLines = new string[]
        {
            String.Format("中华三国志 {0} {1}", Platform.GameVersion, Platform.GameVersionType),
            "",
            "游戏的更新与讨论请关注官方论坛及百度贴吧：",
            "官方论坛 http://www.zhsan.com  百度贴吧 中华三国志吧",
            "",
            "原作者：Clip_on",
            "",
            "制作组：",
            "",
            "程序：45399735 耒戈氏 kpxp 月落乌江 majorcheng(外交) breamask(单挑) 我勒个去(其他) 最爱艾氏兄弟",
            "YPZhou(程序优化) 兔巴哥(《三国春秋传》作者)",
            "美工：caibao2009 月落乌江(地图) 酷热7(地图) sgb6700tt(界面) Ftian80(头像) 无限时段(粮道) PTMASTER酱(帅旗)",
            "剧本：一身是胆 qiaoabcde caibao2009 酷热7 粒粒橙 o哀之咏叹调o 髀里肉生o0 哈哈哈呵呵呵 helokero13(剧本) ",
            "yuanhouzi123(称号) 熊家依若(武将) 黑翼喵 郭文斌(兵种) asmz2002(影响文字) 枫舞影(对话文字)",
            "特别呜谢：人骚骚兮夜无眠",
            "",
            "该游戏为学习交流之用，游戏制作者对其所制作部分分别享有版权，游戏中图片、音乐等资源版权为原制作公司所有。",
            "",
            "未经许可禁止把该游戏用作商业用途。",
            "",
            "感谢广大网友的支持！"
        };

        string message = "";

        public MainMenuScreen()
        {
            //None

            btList = new List<ButtonTexture>() { };

            var btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "New", new Vector2(870, 120 + 90));
            btOne.OnButtonPress += (sender, e) =>
            {
                menuTypeElapsed = 0f;
                UnCheckTextBoxs();
                MenuType = MenuType.New;
                InitScenarioList();
            };
            btList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "Save", new Vector2(870, 120 + 90 * 2));
            btOne.OnButtonPress += (sender, e) =>
            {
                menuTypeElapsed = 0f;
                UnCheckTextBoxs();
                MenuType = MenuType.Save;
                InitScenarioSaveList();
            };
            btList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "Setting", new Vector2(870, 120 + 90 * 3));
            btOne.OnButtonPress += (sender, e) =>
            {
                menuTypeElapsed = 0f;
                UnCheckTextBoxs();
                MenuType = MenuType.Setting;

            };
            btList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "About", new Vector2(870, 120 + 90 * 4));
            btOne.OnButtonPress += (sender, e) =>
            {
                menuTypeElapsed = 0f;
                UnCheckTextBoxs();
                MenuType = MenuType.About;
            };
            btList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\Menu", "Exit", new Vector2(870, 120 + 90 * 5));
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
                    Session.StartScenario(CurrentScenario, false);
                }

            };
            btScenarioList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\ReadyButton", "Cancel", new Vector2(800, 615));
            btOne.OnButtonPress += (sender, e) =>
            {
                isClosing = true;
                menuTypeElapsed = 0.5f;
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
                Position = new Vector2(50, 50)
            };
            lbOne.OnButtonPress += (sender, e) =>
            {
                CurrentSetting = "基本";
                UnCheckTextBoxs();
            };
            lbSettingList.Add(lbOne);

            lbOne = new LinkButton("环境")
            {
                Position = new Vector2(50 + 100, 50)
            };
            lbOne.OnButtonPress += (sender, e) =>
            {
                CurrentSetting = "环境";
                UnCheckTextBoxs();
            };
            lbSettingList.Add(lbOne);

            lbOne = new LinkButton("人物")
            {
                Position = new Vector2(50 + 100 * 2, 50)
            };
            lbOne.OnButtonPress += (sender, e) =>
            {
                CurrentSetting = "人物";
                UnCheckTextBoxs();
            };
            lbSettingList.Add(lbOne);

            lbOne = new LinkButton("参数")
            {
                Position = new Vector2(50 + 100 * 3, 50)
            };
            lbOne.OnButtonPress += (sender, e) =>
            {
                CurrentSetting = "参数";
                UnCheckTextBoxs();
            };
            lbSettingList.Add(lbOne);

            lbOne = new LinkButton("电脑")
            {
                Position = new Vector2(50 + 100 * 4, 50)
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

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150, 115))
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

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150 + 200, 115))
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

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150, 115 + 60))
            {
                ID = "925*520"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                Session.Resolution = "925*520";
                Session.ChangeDisplay(true);
                InitSetting();
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150 + 200, 115 + 60))
            {
                ID = "1120*630"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                Session.Resolution = "1120*630";
                Session.ChangeDisplay(true);
                InitSetting();
            };
            btSettingList.Add(btOne);

            btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150 + 400, 115 + 60))
            {
                ID = "1024*768"
            };
            btOne.OnButtonPress += (sender, e) =>
            {
                Session.Resolution = "1024*768";
                Session.ChangeDisplay(true);
                InitSetting();
            };
            btSettingList.Add(btOne);

            nstMusic = new NumericSetTexture(0, 100, 100, null, new Vector2(150, 115 + 60 * 2), true)
            {
                DisNumber = false,
                NowNumber = Setting.Current.MusicVolume
            };
            nstSound = new NumericSetTexture(0, 100, 100, null, new Vector2(150, 115 + 60 * 3), true)
            {
                DisNumber = false,
                NowNumber = Setting.Current.SoundVolume
            };

            cbAIHardList = new List<ButtonTexture>();
            
            for (int i = 0; i < hards1.Length; i++)
            {
                var hard = hards1[i];

                btOne = new ButtonTexture(@"Content\Textures\Resources\Start\CheckBox", "CheckBox", new Vector2(150 + 100 * i, 115 + 60))
                {
                    ID = hard
                };
                btOne.OnButtonPress += (sender, e) =>
                {
                    Setting.Current.Difficulty = ((ButtonTexture)sender).ID;
                    InitSetting();
                };
                cbAIHardList.Add(btOne);
            }

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

            tbGamerName = new TextBox(TextBoxStyle.Small, "")
            {
                MaxLength = 20,
                Position = new Vector2(150, 120 + 60 * 3)
            };

            if (Platform.PlatFormType == PlatFormType.Win)
            {
                tbGamerName.TextBoxMode = TextBoxMode.Normal;
            }
            else
            {
                tbGamerName.TextBoxMode = TextBoxMode.Chinese;
            }

            tbGamerName.Text = Setting.Current.GamerName;
            tbGamerName.OnTextBoxSelected += (sender, e) =>
            {
                UnCheckTextBoxs();
                tbGamerName.Selected = true;
            };

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

        void InitSetting()
        {
            var btSimple = btSettingList.FirstOrDefault(bt => bt.ID == "简体中文");
            var btTradition = btSettingList.FirstOrDefault(bt => bt.ID == "传统中文");
            var btWindow = btSettingList.FirstOrDefault(bt => bt.ID == "窗口模式");
            var btFull = btSettingList.FirstOrDefault(bt => bt.ID == "全屏模式");
            var bt925520 = btSettingList.FirstOrDefault(bt => bt.ID == "925*520");
            var bt1120630 = btSettingList.FirstOrDefault(bt => bt.ID == "1120*630");
            var bt1024768 = btSettingList.FirstOrDefault(bt => bt.ID == "1024*768");

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

            bt925520.Selected = bt1120630.Selected = bt1024768.Selected = false;
            if (Session.Resolution == "925*520")
            {
                bt925520.Selected = true;
            }
            else if (Session.Resolution == "1120*630")
            {
                bt1120630.Selected = true;
            }
            else
            {
                bt1024768.Selected = true;
            }

            cbAIHardList.ForEach(cb => cb.Selected = false);
            var cbAIHard = cbAIHardList.FirstOrDefault(cb => cb.ID == Setting.Current.Difficulty);
            if (cbAIHard != null)
            {
                cbAIHard.Selected = true;
            }
            
        }

        public void Load()
        {

        }

        public void Update(GameTime gameTime)
        {
            float seconds = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

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
                btSettingList.ForEach(bt =>
                {
                    if (!String.IsNullOrEmpty(bt.ID))
                    {
                        bt.Visible = false;
                    }
                });

                string[] buttons = null;

                if (CurrentSetting == "基本")
                {
                    buttons = new string[] { "简体中文", "传统中文" };

                    //cbAIHardList.ForEach(cb => cb.Update());

                    //tbBattleSpeed.Update(seconds);
                    //tbBattleSpeed.HandleInput(seconds);

                    tbGamerName.Update(seconds);
                    tbGamerName.HandleInput(seconds);

                    //Setting.Current.BattleSpeed = tbBattleSpeed.Text.NullToStringTrim();
                    Setting.Current.GamerName = tbGamerName.Text.NullToStringTrim();
                }
                else if (CurrentSetting == "环境")
                {
                    buttons = new string[] { "窗口模式", "全屏模式" };  //, "925*520", "1120*630", "1024*768" };

                    int musicVolume = 0;
                    int soundVolume = 0;
                    nstMusic.Update(Vector2.Zero, ref musicVolume);
                    nstSound.Update(Vector2.Zero, ref soundVolume);

                    if (nstMusic.NowNumber <= nstMusic.MinNumber)
                    {
                        nstMusic.leftTexture.Enable = false;
                    }
                    else
                    {
                        nstMusic.leftTexture.Enable = true;
                    }
                    if (nstMusic.NowNumber >= nstMusic.MaxNumber)
                    {
                        nstMusic.rightTexture.Enable = false;
                    }
                    else
                    {
                        nstMusic.rightTexture.Enable = true;
                    }

                    if (nstSound.NowNumber <= nstSound.MinNumber)
                    {
                        nstSound.leftTexture.Enable = false;
                    }
                    else
                    {
                        nstSound.leftTexture.Enable = true;
                    }
                    if (nstSound.NowNumber >= nstSound.MaxNumber)
                    {
                        nstSound.rightTexture.Enable = false;
                    }
                    else
                    {
                        nstSound.rightTexture.Enable = true;
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
                else if (CurrentSetting == "人物")
                {
                    buttons = new string[] { };
                }
                else if (CurrentSetting == "参数")
                {
                    buttons = new string[] { };
                }
                else if (CurrentSetting == "电脑")
                {
                    buttons = new string[] { };
                }

                btSettingList.Where(bt => buttons.Contains(bt.ID)).NullToEmptyList().ForEach(bt =>
                {
                    bt.Visible = true;
                });

                btSettingList.ForEach(bt =>
                {
                    bt.Update();
                });

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
            else if (MenuType == MenuType.About)
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

            CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Start.jpg", Vector2.Zero, Color.White, 1f);

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
                    CacheManager.DrawString(Session.Current.Font, CurrentScenario.Desc.SplitLineString(23, 7, ref resultRow, ref resultWidth), new Vector2(40, 460), Color.Black * alpha);
                }

                if (!String.IsNullOrEmpty(message))
                {
                    CacheManager.DrawString(Session.Current.Font, message, btNext.Position + new Vector2(70, 6), Color.Red * alpha);
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

                lbSettingList.ForEach(lb => lb.Draw(alpha));

                if (CurrentSetting == "基本")
                {
                    CacheManager.DrawString(Session.Current.Font, "语言", new Vector2(50, 120), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "简体中文", new Vector2(50 + 100 + 50 - 12, 120), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "传统中文", new Vector2(50 + 100 + 200 + 50 - 12, 120), Color.Black * alpha);

                    //CacheManager.DrawString(Session.Current.Font, "难度", new Vector2(50, 120 + 60), Color.Black * alpha);

                    //CacheManager.DrawString(Session.Current.Font, "战斗", new Vector2(50, 120 + 60 * 2), Color.Black * alpha);

                    //CacheManager.DrawString(Session.Current.Font, "快速战斗的速度", new Vector2(50 + 300, 120 + 60 * 2), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "玩家", new Vector2(50, 120 + 60 * 3), Color.Black * alpha);

                    //int inHard = 0;
                    //cbAIHardList.ForEach(cb =>
                    //{
                    //    cb.Draw(null, Color.White * alpha);

                    //    CacheManager.DrawString(Session.Current.Font, hards2[inHard], new Vector2(cb.Position.X + 40 - 2, 120 + 60), Color.Black * alpha);

                    //    inHard++;
                    //});

                    //tbBattleSpeed.tranAlpha = alpha;
                    //tbBattleSpeed.Draw();

                    tbGamerName.tranAlpha = alpha;
                    tbGamerName.Draw();
                }
                else if (CurrentSetting == "环境")
                {
                    CacheManager.DrawString(Session.Current.Font, "界面", new Vector2(50, 120), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "窗口模式", new Vector2(50 + 100 + 50, 120), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "全屏模式", new Vector2(50 + 100 + 200 + 50, 120), Color.Black * alpha);

                    //CacheManager.DrawString(Session.Current.Font, "925*520", new Vector2(50 + 100 + 50, 120 + 60), Color.Black * alpha);

                    //CacheManager.DrawString(Session.Current.Font, "1120*630", new Vector2(50 + 100 + 200 + 50, 120 + 60), Color.Black * alpha);

                    //CacheManager.DrawString(Session.Current.Font, "1024*768", new Vector2(50 + 100 + 400 + 50, 120 + 60), Color.Black * alpha);

                    //CacheManager.DrawString(Session.Current.Font, "分辨", new Vector2(50, 120 + 60), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "音乐", new Vector2(50, 120 + 60 * 2), Color.Black * alpha);

                    CacheManager.DrawString(Session.Current.Font, "声音", new Vector2(50, 120 + 60 * 3), Color.Black * alpha);

                    nstMusic.Draw(alpha);
                    nstSound.Draw(alpha);

                }
                else if (CurrentSetting == "人物")
                {

                }
                else if (CurrentSetting == "参数")
                {

                }
                else if (CurrentSetting == "电脑")
                {

                }

            }
            else if (MenuType == MenuType.About)
            {
                CacheManager.DrawAvatar(@"Content\Textures\Resources\Start\Common.jpg", Vector2.Zero, Color.White * alpha, 1f);

                CacheManager.DrawString(Session.Current.Font, String.Join(System.Environment.NewLine, aboutLines), new Vector2(40, 40), Color.Black * alpha, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);

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
