using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldOfTheThreeKingdoms.GameScreens
{
    public class LoadingScreen
    {
        string background = "";

        string tishi = "";

        float elapsedTime = 0f;

        Vector2 backgroundScale = Microsoft.Xna.Framework.Vector2.One;  // new Vector2(1280f/1024f, 720/768f);

        float textPre = 0f;

        public bool IsLoading = false;
        public bool IsComplete = false;
        public event EventHandler LoadScreenEvent;

        public void ClearEvent()
        {
            LoadScreenEvent = null;
        }

        public LoadingScreen()
        {
            int ran = new Random().Next(1, 60);

            string ranStr = ran < 10 ? ("0" + ran) : ran.ToString();

            background = "Content/Textures/Resources/ScenarioLoading/" + ranStr + ".jpg";

            tishi = new TiShiText().getRandomText();

            var textLength = 22.4 * tishi.Trim().Length;

            textPre = Convert.ToSingle((845 - textLength) / 2);
        }

        public void Load()
        {

        }

        public void Update(GameTime gameTime)
        {
            float seconds = Convert.ToSingle(gameTime.ElapsedGameTime.TotalSeconds);

            elapsedTime += seconds;

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
                            Session.MainGame.loadingScreen = null;
                        }).Start();
                        
                    }
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            CacheManager.DrawAvatar(background, Vector2.Zero, Color.White * 1f, backgroundScale);

            CacheManager.DrawAvatar(@"Content/Textures/Resources/ScenarioLoading/jindulan.png", new Vector2(215, 650), Color.White * 1f, 1f);

            CacheManager.DrawString(Session.Current.Font, tishi.Trim(), new Vector2(215 + textPre, 650) + new Vector2(10, 10), Color.White, 0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0f);
        }

        public void ExitScreen()
        {

        }

    }
}
