using GameGlobal;
using GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Platforms;
using PluginServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Tools;
using WorldOfTheThreeKingdoms.GameScreens;

namespace WorldOfTheThreeKingdoms
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        //public static  ContentManager Content;   //原程序，没有new
        //public static new ContentManager Content;

        //public System.Windows.Forms.Form GameForm;

        //private GraphicsDeviceManager graphics;
        //private KeyboardState keyState;  //原程序，由于警告去掉

        public MainMenuScreen mainMenuScreen;

        public LoadingScreen loadingScreen;

        public MainGameScreen mainGameScreen;

        private int previousWindowHeight = 720;
        private int previousWindowWidth = 0x438;

        //public jiazaitishichuangkou jiazaitishi = new jiazaitishichuangkou();

        //public WindowsMediaPlayerClass Player = new WindowsMediaPlayerClass();

        //标识是否为全屏
        private bool IsFullScreen = false;

        public Matrix SpriteScale1, SpriteScale2;

        public bool disScale = false;

        public Rectangle fullScreenDestination;

        public SpriteBatch SpriteBatch;

        public bool? takePicture = null;
        RenderTarget2D screenshot;
        public string picture = "";

        public Vector2 errPos = Vector2.Zero;
        public string err = "";
        public string warn = "";
        public DateTime? lastWarnTime = null;
        public string view = "";

        public Texture2D tex = null;

        public Texture2D renderLast = null;

        public MainGame()
        {
            //第一步
          
            Platform.MainGame = this;

            //獲取設置數據
            Setting.Init();

            Session.Init();

            //原本的分辨率默认值为720*1080，在其他大小的屏幕上会变形
            //将当前屏幕的分辨率作为默认值……
            //this.previousWindowWidth = WinHelper.GetSystemMetrics(WinHelper.SM_CXSCREEN);
            //this.previousWindowHeight = WinHelper.GetSystemMetrics(WinHelper.SM_CYSCREEN);
            //Platform.SetGraphicsWidthHeight(this.previousWindowWidth, this.previousWindowHeight);
            //this.graphics.PreferredBackBufferWidth = this.previousWindowWidth;
            //this.graphics.PreferredBackBufferHeight = this.previousWindowHeight;                      

            if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop)  //Platform.PlatFormType == PlatFormType.UWP
            {
                DateTime buildDate = new FileInfo(Assembly.GetExecutingAssembly().Location).LastWriteTime;
                base.Window.Title = "中华三国志开发版(已命名修改版 v.33 - build-" + buildDate.Year + "-" + buildDate.Month + "-" + buildDate.Day + ")";
            }
            else
            {
                
            }

            Platform.Current.SetMouseVisible(false);
            Platform.Current.SetBarStyle();
            Platform.Current.SetTimerDisabled(true);
            Platform.Current.ApplicationViewChanged();

            //System.Windows.Forms.Control control = System.Windows.Forms.Control.FromHandle(base.Window.Handle);
            //this.GameForm = (System.Windows.Forms.Form)System.Windows.Forms.Form.FromHandle(this.Window.Handle);
            //this.GameForm.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            //this.GameForm = control as System.Windows.Forms.Form;
            //this.GameForm.KeyDown += new KeyEventHandler(this.GameForm_KeyDown);

            //int uFlags = 0x400;
            //IntPtr systemMenu = GetSystemMenu(base.Window.Handle, false);
            //int menuItemCount = GetMenuItemCount(systemMenu);
            //RemoveMenu(systemMenu, menuItemCount - 1, uFlags);
            //RemoveMenu(systemMenu, menuItemCount - 2, uFlags);

            //Plugin.Plugins.FindPlugins(AppDomain.CurrentDomain.BaseDirectory + "GameComponents");
            //Plugin.Plugins.FindPlugins(AppDomain.CurrentDomain.BaseDirectory + "GamePlugins");
            //this.mainGameScreen = new MainGameScreen(this);
            //base.Components.Add(this.mainGameScreen);
        }

        //private static bool AltComboPressed(KeyboardState state, Microsoft.Xna.Framework.Input.Keys key)
        //{
        //    return (state.IsKeyDown(key) && (state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftAlt) || state.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.RightAlt)));
        //}

        //private void GameForm_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Alt && (e.KeyCode == System.Windows.Forms.Keys.F4))
        //    {
        //        e.Handled = true;
        //    }
        //}

        protected override void Initialize()
        {
            //第二步

            Session.ChangeDisplay(true);

            //基本材質初始化
            Session.TextureRecs = TextureRecsManager.AllTextureRectangles();

            if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.Desktop)
            {
                Platform.Current.SetWindowAllowUserResizing(true);
            }

            //try
            //{
                this.mainMenuScreen = new MainMenuScreen(this);
            //}
            //catch (Exception ex)
            //{
            //    GameTools.SendErrMsg("MainMenuScreen", ex);
            //}

            //this.jiazaitishi.Close();
            //全屏的判断放到初始化代码中
            //if (GlobalVariables.FullScreen)
            //{
            //    this.ToggleFullScreen();
            //}

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //第三步

            SpriteBatch = new SpriteBatch(Platform.GraphicsDevice);

            Session.LoadContent(base.Content);

            Session.PlayMusic("Start");

            // Create a new SpriteBatch, which can be used to draw textures.
            //spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        public void ToggleFullScreen()
        {
            bool full = Setting.Current.DisplayMode == "Full";

            if (full)
            {
                Setting.Current.DisplayMode = "Window";
                Platform.Current.SetFullScreen(false);
            }
            else
            {
                Setting.Current.DisplayMode = "Full";
                Platform.Current.SetFullScreen(true);
            }

            Setting.Save();

            Platform.GraphicsApplyChanges();

            //原来的代码会和单挑程序冲突
            /*
            if (this.graphics.GraphicsDevice.PresentationParameters.IsFullScreen)
            {
                this.graphics.PreferredBackBufferWidth = this.previousWindowWidth;
                this.graphics.PreferredBackBufferHeight = this.previousWindowHeight;
            }
            else
            {
                this.previousWindowWidth = this.graphics.GraphicsDevice.Viewport.Width;
                this.previousWindowHeight = this.graphics.GraphicsDevice.Viewport.Height;
                GraphicsAdapter adapter = this.graphics.GraphicsDevice.CreationParameters.Adapter;
                FullScreenHelper.FullScreen();
                this.graphics.PreferredBackBufferWidth = adapter.CurrentDisplayMode.Width;
                this.graphics.PreferredBackBufferHeight = adapter.CurrentDisplayMode.Height;
            }
            this.graphics.ToggleFullScreen();
            GlobalVariables.FullScreen = this.graphics.GraphicsDevice.PresentationParameters.IsFullScreen;
             */
            //修改后的全屏代码
            //if (this.IsFullScreen)
            //{
            //    WinHelper.RestoreFullScreen(this.GameForm.Handle);//传入窗体句柄
            //    this.IsFullScreen = false;

            //}
            //else
            //{
            //    WinHelper.FullScreen(this.GameForm.Handle);
            //    this.IsFullScreen = true;
            //}
        }

        private void TryToExit()
        {
            this.mainGameScreen.TryToExit();
        }

        protected override void Update(GameTime gameTime)
        {
            //第四步

            base.Update(gameTime);
            if (base.IsActive)
            {
                if (Platform.Current.InputTextNow())
                {
                    return;
                }

                InputManager.Update(Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds));

                if (loadingScreen == null)
                {
                    if (mainGameScreen == null)
                    {
                        if (mainMenuScreen == null)
                        {

                        }
                        else
                        {
                            mainMenuScreen.Update(gameTime);
                        }
                    }
                    else
                    {
                        mainGameScreen.Update(gameTime);
                    }
                }
                else
                {
                    loadingScreen.Update(gameTime);
                }

                //if (AltComboPressed(this.mainGameScreen.KeyState, Microsoft.Xna.Framework.Input.Keys.F4))
                //{
                //    this.TryToExit();
                //}
                //if (AltComboPressed(this.mainGameScreen.KeyState, Microsoft.Xna.Framework.Input.Keys.Enter) && (this.mainGameScreen.PeekUndoneWork().Kind == UndoneWorkKind.None))
                //{
                //    this.mainGameScreen.ToggleFullScreen();
                //}
                //游戏设置中的全屏选项勾选后将多次调用这个方法，界面会闪来闪去
                //只在初始化的时候全屏一次就好啦……
                /*if ((GlobalVariables.FullScreen && !this.mainGameScreen.IsFullScreen) || (!GlobalVariables.FullScreen && this.mainGameScreen.IsFullScreen))
                {
                    this.mainGameScreen.ToggleFullScreen();
                }*/
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            //第五步

            if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.UWP || Platform.PlatFormType == PlatFormType.Android || Platform.PlatFormType == PlatFormType.Desktop)
            {
                if (takePicture == true)
                {
                    try
                    {
                        if (screenshot == null || screenshot.GraphicsDevice == null)
                        {
                            screenshot = new RenderTarget2D(Platform.GraphicsDevice, Platform.GraphicsDevice.Viewport.Width, Platform.GraphicsDevice.Viewport.Height, false, SurfaceFormat.Color, DepthFormat.None);
                        }
                        Platform.GraphicsDevice.SetRenderTarget(screenshot);
                    }
                    catch (Exception ex)
                    {
                        //Log...
                    }
                    //takePicture = false;
                }
            }

            var spriteMode = mainGameScreen == null ? SpriteSortMode.Deferred : SpriteSortMode.BackToFront;

            if (disScale) //Platform.PlatFormType == PlatForm.iOS && isRetina)
            {
                if (mainGameScreen == null)
                {
                    SpriteBatch.Begin(spriteMode, BlendState.AlphaBlend, null, null, null, null, SpriteScale1);
                }
                else
                {
                    SpriteBatch.Begin(spriteMode, BlendState.AlphaBlend, null, null, null, null, SpriteScale2);
                }
            }
            else
            {
                SpriteBatch.Begin(spriteMode, BlendState.AlphaBlend);
            }

            Platform.GraphicsDevice.Clear(Color.TransparentBlack);
            //this.graphics.GraphicsDevice.Clear(Color.TransparentBlack);

            if (loadingScreen == null)
            {
                if (mainGameScreen == null)
                {
                    if (mainMenuScreen == null)
                    {

                    }
                    else
                    {
                        mainMenuScreen.Draw(gameTime);
                    }
                }
                else
                {
                    mainGameScreen.Draw(gameTime);
                }
            }
            else
            {
                loadingScreen.Draw(gameTime);
            }

            view = Platform.Current.MemoryUsage;
            if (!String.IsNullOrEmpty(view))
            {
                CacheManager.DrawString(Session.Current.Font, "view:" + view.SplitLineString(100), errPos, Color.Red, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, -2f);
            }

            //if (!String.IsNullOrEmpty(warn) && lastWarnTime != null && (DateTime.Now - (DateTime)lastWarnTime).TotalSeconds < 20)
            //{
            //    CacheManager.DrawString(Session.Current.Font, "Warn:" + warn, errPos, Color.Red, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 1f);
            //}

            ////if (!String.IsNullOrEmpty(err))
            ////{
            ////CacheManager.DrawString(LightAncient, "Err:" + err.SplitLineString(100).ProcessStar(), errPos, Color.Red, 0f, Vector2.Zero, 0.5f, SpriteEffects.None, 1f);
            ////}

            if (renderLast != null)
            {
                SpriteBatch.Draw(renderLast, Vector2.Zero, Color.White);
            }

            SpriteBatch.End();

            if (takePicture == true && String.IsNullOrEmpty(err))
            {
                takePicture = false;
                try
                {
                    if (Platform.PlatFormType == PlatFormType.Win || Platform.PlatFormType == PlatFormType.UWP || Platform.PlatFormType == PlatFormType.Android || Platform.PlatFormType == PlatFormType.Desktop)  // || Platform.PlatFormType == PlatForm.iOS)
                    {
                        if (screenshot != null)
                        {
                            byte[] shot = Platform.Current.ScreenShot(Platform.GraphicsDevice, screenshot);
                            //Season.GraphicsDevice.SetRenderTarget(null);

                            if (shot != null && shot.Length > 0)
                            {
                                var task = new PlatformTask(() => { });
                                task.OnStartFinish += (result) =>
                                {
                                    if (task.ParamArrayResultBytes != null && task.ParamArrayResultBytes.Length > 0)
                                    {
                                        Platform.Current.SaveUserFile(picture, task.ParamArrayResultBytes, null);
                                    }
                                };
                                Platform.Current.ResizeImageFile(shot, 800, 480, false, task);
                            }
                        }
                        //Task.Run(async () => await Season.Current.SaveUserFile(picture, shot));
                        //var task = new SeasonTask(() =>
                        //{
                        //    screenshot = new RenderTarget2D(Season.GraphicsDevice, 800, 480, false, SurfaceFormat.Color, DepthFormat.None);
                        //});
                    }
                }
                catch (Exception ex)
                {
                    WebTools.TakeWarnMsg("游戏界面截屏失败:", "takePicture：", ex);
                }
            }

            base.Draw(gameTime);
        }

        public void SaveGameWhenCrash(String _savePath)
        {
            this.mainGameScreen.SaveGameWhenCrash(_savePath);
        }

        public List<int> InitializationFactionIDs
        {
            set
            {
                this.mainGameScreen.InitializationFactionIDs = value;
            }
        }

        public string InitializationFileName
        {
            set
            {
                this.mainGameScreen.InitializationFileName = value;
            }
        }

        public bool LoadScenarioInInitialization
        {
            set
            {
                this.mainGameScreen.LoadScenarioInInitialization = value;
            }
        }
        
        //public void PlayMusic()
        //{
            //Player.currentPlaylist.clear();
            //WMPLib.IWMPMedia media;

            //string[] filePaths = Directory.GetFiles("GameMusic/Start/", "*.mp3");
            //Random rd = new Random();
            //int index = rd.Next(0, filePaths.Length);
            //string path = filePaths[index];

            //foreach (String s in filePaths)
            //{
            //    media = Player.newMedia(s);
            //    Player.currentPlaylist.appendItem(media);
            //}
            //media = Player.newMedia(path);
            //Player.currentPlaylist.appendItem(media);
            //Player.currentItem = media;
            //Player.play();
            //Player.settings.setMode("loop", true);
        //}

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            //第六步
            // TODO: Unload any non ContentManager content here
        }

        protected override void Dispose(bool disposing)
        {
            Plugin.Plugins.ClosePlugins();
        }

        //[DllImport("user32.dll")]
        //public static extern int GetMenuItemCount(IntPtr hMenu);
        //[DllImport("user32.dll")]
        //public static extern IntPtr GetSystemMenu(IntPtr hwnd, bool bRevert);
        //[DllImport("user32.dll")]
        //public static extern int RemoveMenu(IntPtr hMenu, int uPosition, int uFlags);

        //public void Processing()
        //{
        //    //formMainMenu menu = new formMainMenu
        //    //{
        //    //    mainGame = this
        //    //};

        //    if (menu.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        mainGame.jiazaitishi.Show();
        //        mainGame.jiazaitishi.Refresh();
        //        mainGame.Run();
        //    }
        //}

    }
}
