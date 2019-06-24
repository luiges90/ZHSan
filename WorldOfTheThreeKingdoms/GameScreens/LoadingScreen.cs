using GameManager;
using GameObjects;
using GamePanels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace WorldOfTheThreeKingdoms.GameScreens
{
    public class LoadingScreen
    {
        public string Mode = "Start";  //End

        public string Scenario = "";

        string[] maps = null;

        string sound = "";

        string background = "";
        string tishi = "";
        float textPre = 0f;

        float elapsedTime = 0f;

        float pageTime = 0f;

        Vector2 backgroundScale = Microsoft.Xna.Framework.Vector2.One;  // new Vector2(1280f/1024f, 720/768f);

        int page = 1;

        bool pause = false;

        public bool IsLoading = false;
        public bool IsComplete = false;
        public event EventHandler LoadScreenEvent;

        ButtonTexture btPre, btPlay, btPause, btNext, btLoad, btStart;

        public void ClearEvent()
        {
            LoadScreenEvent = null;
        }

        public LoadingScreen(string mode, string scenario)
        {
            Mode = mode;

            Scenario = scenario.NullToString().Split('-')[0];
            
            if (Mode == "Start")
            {
                string baseDir = @"Content\Textures\Resources\ScenarioLoading\Maps\";

                var dirs = Platform.Current.GetMODDirectories(baseDir, true).NullToEmptyArray();

                var dir = dirs.FirstOrDefault(di => di.Contains(Scenario));

                if (dir == null)
                {
                    Mode = "";
                }
                else
                {
                    maps = Platform.Current.GetMODFiles(dir + "/", true).NullToEmptyArray().Where(fi => fi.EndsWith(".jpg")).NullToEmptyArray();  //.Select(fi => baseDir + Scenario + @"\" + fi).NullToEmptyArray();
                }

                var soundDir = @"Content\Sound\Scenario\";

                var soundFiles = Platform.Current.GetMODFiles(soundDir, true).NullToEmptyArray();

                sound = soundFiles.FirstOrDefault(fi => fi.Contains(Scenario));

                if (!String.IsNullOrEmpty(sound))
                {
                    Platform.Current.PlayEffect(sound);
                }
            }

            if (Mode == "Start")
            {
                btPre = new ButtonTexture(@"Content\Textures\Resources\Start\Play", "Pre", new Vector2(200 - 50, 640));
                btPre.OnButtonPress += (sender, e) =>
                {
                    if (page > 1)
                    {
                        page--;
                        pageTime = 0f;
                        if (Platform.IsMobilePlatForm)
                        {
                            InputManager.PoX = 0;
                            InputManager.PoY = 0;
                        }
                    }
                };

                btPlay = new ButtonTexture(@"Content\Textures\Resources\Start\Play", "Play", new Vector2(200 + 220, 640));
                btPlay.OnButtonPress += (sender, e) =>
                {
                    pause = false;
                };

                btPause = new ButtonTexture(@"Content\Textures\Resources\Start\Play", "Pause", new Vector2(200 + 220, 640));
                btPause.OnButtonPress += (sender, e) =>
                {
                    pause = true;
                };

                btNext = new ButtonTexture(@"Content\Textures\Resources\Start\Play", "Next", new Vector2(200 + 220 * 2, 640));
                btNext.OnButtonPress += (sender, e) =>
                {
                    if (page < maps.Length)
                    {
                        page++;
                        pageTime = 0f;
                        if (Platform.IsMobilePlatForm)
                        {
                            InputManager.PoX = 0;
                            InputManager.PoY = 0;
                        }
                    }
                };

                btLoad = new ButtonTexture(@"Content\Textures\Resources\Start\Play", "Load", new Vector2(200 + 220 * 3 + 100, 640));
                btLoad.Enable = false;

                btStart = new ButtonTexture(@"Content\Textures\Resources\Start\Play", "Start", new Vector2(200 + 220 * 3 + 100, 640));
                btStart.OnButtonPress += (sender, e) =>
                {
                    Session.MainGame.loadingScreen = null;
                };
            }
            else
            {
                var baseDir = @"Content\Textures\Resources\ScenarioLoading\";

                var pictures = Platform.Current.GetMODFiles(baseDir, true).NullToEmptyArray().Where(pi => pi.EndsWith(".jpg")).NullToEmptyArray();  //.Select(fi => baseDir + fi).NullToEmptyArray();

                if (pictures.Length > 0)
                {
                    int ran = new Random().Next(1, pictures.Length);

                    //string ranStr = ran < 10 ? ("0" + ran) : ran.ToString();

                    background = pictures[ran-1];  // "Content/Textures/Resources/ScenarioLoading/" + ranStr + ".jpg";

                    tishi = new TiShiText().getRandomText();

                    var textLength = 22.4 * tishi.Trim().Length;

                    textPre = Convert.ToSingle((845 - textLength) / 2);
                }

            }


        }

        public void Load()
        {

        }

        public void Update(GameTime gameTime)
        {
            float seconds = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            elapsedTime += seconds;

            if (Mode == "Start")
            {
                if (pause)
                {
                    btPlay.Visible = true;
                    btPause.Visible = false;
                }
                else
                {
                    btPlay.Visible = false;
                    btPause.Visible = true;

                    if (page < maps.Length)
                    {
                        pageTime += seconds;

                        if (pageTime >= Session.globalVariablesBasic.ScenarioMapPerTime)
                        {
                            page++;
                            pageTime -= Session.globalVariablesBasic.ScenarioMapPerTime;
                        }
                    }
                }

                if (IsComplete)
                {
                    btLoad.Visible = false;
                    btStart.Visible = true;
                }
                else
                {
                    btLoad.Visible = true;
                    btStart.Visible = false;
                }

                btPre.Enable = btNext.Enable = true;

                if (page <= 1)
                {
                    btPre.Enable = false;
                }

                if (page >= maps.Length)
                {
                    btNext.Enable = false;
                }

                btPre.Update();

                btPlay.Update();

                btPause.Update();

                btNext.Update();

                btLoad.Update();

                btStart.Update();
            }
            else
            {

            }

            if (elapsedTime >= 0.2f)
            {
                if (IsLoading)
                {

                }
                else
                {
                    if (LoadScreenEvent == null)
                    {

                    }
                    else
                    {
                        IsLoading = true;

                        if (Session.MainGame.mainGameScreen != null)
                        {
                            Session.MainGame.mainGameScreen.mainMapLayer.StopThreads();

                            Session.MainGame.mainGameScreen.DisposeMapTileMemory(false, true);

                            Session.MainGame.mainGameScreen.Dispose();

                            Session.MainGame.mainGameScreen = null;

                            GameScenario.ProcessCommonData(CommonData.Current);
                        }

                        Session.Current.Clear();

                        CacheManager.Clear(CacheType.Live);

                        GC.Collect();

#if DEBUG
                        try
                        {
                            LoadScreenEvent.Invoke(null, null);
                        }
                        catch (Exception e)
                        {
                            throw new Exception("加載出錯：" + e);
                            //Program.PrintError(e);
                            //Environment.Exit(1);
                        }
                        ClearEvent();
                        IsComplete = true;
                        if (Mode == "Start")
                        {

                        }
                        else
                        {
                            Session.MainGame.loadingScreen = null;
                        }
#else
                        new Platforms.PlatformTask(() =>
                        {
                            try
                            {
                                LoadScreenEvent.Invoke(null, null);
                            }
                            catch (Exception e)
                            {
                                throw new Exception("加載出錯：" + e);
                                //Program.PrintError(e);
                                //Environment.Exit(1);
                            }

                            ClearEvent();
                            IsComplete = true;
                            if (Mode == "Start")
                            {

                            }
                            else
                            {
                                Session.MainGame.loadingScreen = null;
                            }
                        }).Start();
#endif
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (Mode == "Start")
            {
                CacheManager.DrawAvatar(@"Content/Textures/Resources/Start/LoadingBack.jpg", Vector2.Zero, Color.White * 1f, 1f);

                if (maps.Length > 0)
                {
                    var map = maps[page - 1];

                    if (String.IsNullOrEmpty(map))
                    {

                    }
                    else
                    {
                        //@"Content/Textures/Resources/ScenarioLoading/Maps/" + Scenario
                        CacheManager.DrawAvatar(map, new Vector2(23 + 66, 5 + 44), Color.White * 1f, new Vector2(1106f / 2821f, 565f / 1587f));
                    }
                }


                CacheManager.DrawAvatar(@"Content/Textures/Resources/Start/LoadingBorder.png", new Vector2(23, 5), Color.White * 1f, 1f);

                btPre.Draw();

                btPlay.Draw();

                btPause.Draw();

                btNext.Draw();

                btLoad.Draw();

                btStart.Draw();

                if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop || Platform.PlatFormType == PlatFormType.UWP && !Platform.IsMobile)
                {
                    CacheManager.DrawAvatar(@"Content\Textures\Resources\MouseArrow\Normal.png", InputManager.Position, Color.White, 1f);
                }
            }
            else
            {
                CacheManager.DrawAvatar(background, Vector2.Zero, Color.White * 1f, backgroundScale);

                CacheManager.DrawAvatar(@"Content/Textures/Resources/ScenarioLoading/jindulan.png", new Vector2(215, 650), Color.White * 1f, 1f);

                CacheManager.DrawString(Session.Current.Font, tishi.Trim(), new Vector2(215 + textPre, 650) + new Vector2(10, 10), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
            }

        }

        public void ExitScreen()
        {

        }

    }
}
