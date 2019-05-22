using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PluginInterface;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
//using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using GameManager;

namespace tupianwenziPlugin
{

    public class tupianwenzilei
    {
        internal Point BackgroundSize;
        internal PlatformTexture BackgroundTexture;
        internal Queue<PlatformTexture> shijiantupianduilie = new Queue<PlatformTexture>();
        internal Queue<Rectangle> juxingduilie = new Queue<Rectangle>();
        internal Queue<string> shijianshengyinduilie = new Queue<string>();


        internal Rectangle shijiantupianjuxing;
        internal PlatformTexture shijiantupian;
        internal string shijianshengyin;


        internal FreeRichText BuildingRichText = new FreeRichText();
        internal Rectangle ClientPosition;
        private Keys currentKey;
        private Point DisplayOffset;
        private Queue<GameObjectAndBranchName> DisplayQueue = new Queue<GameObjectAndBranchName>();
        internal PlatformTexture FirstPageButtonDisabledTexture;
        private PlatformTexture FirstPageButtonDisplayTexture;
        internal Rectangle FirstPageButtonPosition;
        internal PlatformTexture FirstPageButtonSelectedTexture;
        internal PlatformTexture FirstPageButtonTexture;
        private bool firstShowing;
        internal bool HasConfirmationDialog = false;
        internal IConfirmationDialog iConfirmationDialog;
        internal IGameContextMenu iContextMenu;
        internal Itupianwenzi iPersonTextDialog;
        private bool isShowing;
        private bool diyigeshengyin=false;
        internal FreeText NameText;
        internal GameDelegates.VoidFunction NoFunction;
        internal Rectangle PortraitClient;
        internal FreeRichText RichText = new FreeRichText();
        
        internal Person SpeakingPerson;
        private DateTime startShowingTime;
        internal GameObjectTextTree TextTree = new GameObjectTextTree();
        internal GameDelegates.VoidFunction YesFunction;

        internal event GameDelegates.VoidFunction CloseFunction;
        internal string TryToShowString = "";

        internal void Close(Screen screen)
        {
            if (this.DequeueAndDisplay(screen))
            {
                this.IsShowing = false;
            }
        }


        private bool DequeueAndDisplay(Screen screen)
        {


            if (this.DisplayQueue.Count > 0)
            {
                this.startShowingTime = DateTime.Now;
                GameObjectAndBranchName name = this.DisplayQueue.Dequeue();
                this.shijiantupian = this.shijiantupianduilie.Dequeue();
                this.shijiantupianjuxing = this.juxingduilie.Dequeue();
                this.SetPosition(ShowPosition.Bottom, screen);
                this.shijianshengyin = this.shijianshengyinduilie.Dequeue();
                this.SpeakingPerson = name.person;
                this.NameText.Text = name.person.Name;
                if (name.TryToShowString != null && name.TryToShowString.Length > 1)
                {
                    this.TryToShowString = name.TryToShowString;
                }
                else this.TryToShowString = "";
                if(TryToShowString != null && TryToShowString.Length > 1)
                {
                    this.SetPosition(ShowPosition.BottomLeft, screen);
                }
                this.RichText.Clear();
                if (this.diyigeshengyin)
                {
                    screen.PlayNormalSound(this.shijianshengyin);
                    this.diyigeshengyin = false;
                }

                this.RichText.Texts = name.texts;
                this.RichText.ResortTexts();
                if (name.iConfirmationDialog != null)
                {
                    this.iConfirmationDialog = name.iConfirmationDialog;
                    this.iConfirmationDialog.SetPersonTextDialog(this.iPersonTextDialog);
                    this.iConfirmationDialog.AddYesFunction(name.YesFunction);
                    this.iConfirmationDialog.AddNoFunction(name.NoFunction);
                    this.iConfirmationDialog.IsShowing = true;
                }
                else
                {
                    this.iConfirmationDialog = null;
                    this.YesFunction = null;
                    this.NoFunction = null;
                }
                return false;
            }
            return true;
        }

        internal void Draw()
        {
            if (this.SpeakingPerson != null)
            {
                Rectangle? sourceRectangle = null;

                CacheManager.DrawZhsanAvatar(this.SpeakingPerson, "", this.PortraitDisplayPosition, Color.White, 0.201f);

                sourceRectangle = null;
                CacheManager.Draw(this.BackgroundTexture, this.BackgroundDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                if (this.shijiantupian != null)
                {
                    if (this.shijiantupianjuxing.Width == 240 && this.shijiantupianjuxing.Height == 240)//如果是人物死亡图片的话
                    {
                        CacheManager.Draw(this.shijiantupian, this.shijiantupianjuxing, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.202f);
                    }
                    else
                    {
                        CacheManager.Draw(this.shijiantupian, this.shijiantupianjuxing, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.202f);
                    }
                }
                
                this.NameText.Draw(0.1999f);
                CacheManager.Draw(this.FirstPageButtonDisplayTexture, this.FirstPageButtonDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.199f);
                if(TryToShowString!=null && TryToShowString.Length>1)
                {
                    CacheManager.Draw(@"Content\Textures\GameComponents\tupianwenzi\Data\Background2.png", new Rectangle(this.BackgroundDisplayPosition.X + this.BackgroundDisplayPosition.Width + 100, this.DisplayOffset.Y+115, 460, 165), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.2f);
                    CacheManager.DrawString(Session.Current.Font,TryToShowString.Replace(" ","\n"),new Vector2(this.BackgroundDisplayPosition.X + this.BackgroundDisplayPosition.Width + 120, this.DisplayOffset.Y + 135),Color.White,0f, Vector2.Zero, 0.8f, SpriteEffects.None, 0.1999f);
                }
                this.RichText.Draw(0.1999f);
            }
        }

        internal void Initialize()
        {
            
        }

        private void screen_OnMouseLeftDown(Point position)
        {

        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (StaticMethods.PointInRectangle(position, this.FirstPageButtonDisplayPosition))
            {
                //this.RichText.FirstPage();
                //this.FirstPageButtonDisplayTexture = this.FirstPageButtonDisabledTexture;
            }
            else if ((this.RichText.CurrentPageIndex >= (this.RichText.PageCount - 1)) && ((this.iConfirmationDialog == null) || !this.iConfirmationDialog.IsShowing))
            {
                if (!this.firstShowing)
                {
                    this.Close(Session.MainGame.mainGameScreen);
                }
                this.firstShowing = false;
            }
            else
            {
                this.RichText.NextPage();
                this.startShowingTime = DateTime.Now;
                if (this.RichText.CurrentPageIndex > 0)
                {
                    this.FirstPageButtonDisplayTexture = this.FirstPageButtonTexture;
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (this.RichText.CurrentPageIndex > 0)
            {
                if (StaticMethods.PointInRectangle(position, this.FirstPageButtonDisplayPosition))
                {
                    this.FirstPageButtonDisplayTexture = this.FirstPageButtonSelectedTexture;
                }
                else
                {
                    this.FirstPageButtonDisplayTexture = this.FirstPageButtonTexture;
                }
            }
        }

        internal void SetGameObjectBranch(GameObject gongfang, GameObject gameObject, string branchName,string TryToShowString = "")
        {
            this.BuildingRichText.Clear();
            if (gameObject != null)
            {
                GameObjectTextBranch a = this.TextTree.GetBranch(branchName);
                if (a != null)
                {
                    this.BuildingRichText.AddGameObjectTextBranch(gameObject, a);
                }
                else
                {
                    this.BuildingRichText.AddText(branchName);
                }
            }
            else
            {
                GameObjectTextBranch a = this.TextTree.GetBranch(branchName);
                if (a != null)
                {
                    this.BuildingRichText.AddGameObjectTextBranch(gongfang, a);
                }
                else
                {
                    this.BuildingRichText.AddText(branchName);
                }
            }
            if (this.HasConfirmationDialog)
            {
                this.DisplayQueue.Enqueue(new GameObjectAndBranchName(gongfang, this.BuildingRichText.Texts, branchName, this.iConfirmationDialog, this.YesFunction, this.NoFunction,TryToShowString));
                this.YesFunction = null;
                this.NoFunction = null;
            }
            else
            {
                this.DisplayQueue.Enqueue(new GameObjectAndBranchName(gongfang, this.BuildingRichText.Texts, branchName, null, null, null, TryToShowString));
            }
            this.HasConfirmationDialog = false;
        }

        internal void SetPosition(ShowPosition showPosition, Screen screen)
        {
            Rectangle rectDes = new Rectangle(0, 0, screen.viewportSize.X, screen.viewportSize.Y);
            Rectangle rect = new Rectangle(0, 0, this.BackgroundSize.X, this.BackgroundSize.Y);
            switch (showPosition)
            {
                case ShowPosition.Center:
                    rect = StaticMethods.GetCenterRectangle(rectDes, rect);
                    break;

                case ShowPosition.Top:
                    rect = StaticMethods.GetTopRectangle(rectDes, rect);
                    break;

                case ShowPosition.Left:
                    rect = StaticMethods.GetLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.Right:
                    rect = StaticMethods.GetRightRectangle(rectDes, rect);
                    break;

                case ShowPosition.Bottom:
                    rect = StaticMethods.GetBottomRectangle(rectDes, rect);
                    break;

                case ShowPosition.TopLeft:
                    rect = StaticMethods.GetTopLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.TopRight:
                    rect = StaticMethods.GetTopRightRectangle(rectDes, rect);
                    break;

                case ShowPosition.BottomLeft:
                    rect = StaticMethods.GetBottomLeftRectangle(rectDes, rect);
                    break;

                case ShowPosition.BottomRight:
                    rect = StaticMethods.GetBottomRightRectangle(rectDes, rect);
                    break;
            }
            this.DisplayOffset = new Point(rect.X, rect.Y);
            this.RichText.DisplayOffset = new Point(rect.X + this.ClientPosition.X, rect.Y + this.ClientPosition.Y);
            this.NameText.DisplayOffset = this.DisplayOffset;

            this.shijiantupianjuxing = StaticMethods.GetTopRectangle(rectDes, this.shijiantupianjuxing);
            this.shijiantupianjuxing.Y += 40;
        }

        internal void Update()
        {
            if ((this.iConfirmationDialog == null) || !this.iConfirmationDialog.IsShowing)
            {
                if (this.currentKey != Keys.None)
                {
                    //if (!Session.MainGame.mainGameScreen.KeyState.IsKeyUp(this.currentKey))
                    if (!InputManager.KeyBoardState.IsKeyUp(this.currentKey))
                    {
                        return;
                    }
                    this.currentKey = Keys.None;
                }
                //if (Session.MainGame.mainGameScreen.KeyState.IsKeyDown(Keys.Enter))
                if (InputManager.KeyBoardState.IsKeyDown(Keys.Enter))
                {
                    this.currentKey = Keys.Enter;
                    this.Close(Session.MainGame.mainGameScreen);
                }
                TimeSpan span = (TimeSpan) (DateTime.Now - this.startShowingTime);
                if (span.Milliseconds >= 300)
                {
                    this.firstShowing = false;
                }
                if (span.Seconds >= Setting.Current.GlobalVariables.DialogShowTime)
                {
                    this.Close(Session.MainGame.mainGameScreen);
                }
            }
        }

        private Rectangle BackgroundDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X, this.DisplayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y);
            }
        }

        private Rectangle FirstPageButtonDisplayPosition
        {
            get
            {
                return new Rectangle(this.FirstPageButtonPosition.X + this.DisplayOffset.X, this.FirstPageButtonPosition.Y + this.DisplayOffset.Y, this.FirstPageButtonPosition.Width, this.FirstPageButtonPosition.Height);
            }
        }

        internal bool IsShowing
        {
            get
            {
                return this.isShowing;
            }
            set
            {
                SetIsShowing(Session.MainGame.mainGameScreen, value);
            }
        }

        public void SetIsShowing(Screen screen, bool value)
        {
            if (this.isShowing != value && Setting.Current.GlobalVariables.DialogShowTime > 0)
            {
                this.isShowing = value;
                if (value)
                {
                    if ((this.iContextMenu != null) && this.iContextMenu.IsShowing)
                    {
                        this.iContextMenu.IsShowing = false;
                    }
                    this.firstShowing = true;
                    screen.IsHolding = true;
                    this.diyigeshengyin = true;
                    screen.PushUndoneWork(new UndoneWorkItem(UndoneWorkKind.tupianwenzi, UndoneWorkSubKind.None));
                    screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    this.FirstPageButtonDisplayTexture = this.FirstPageButtonDisabledTexture;
                    screen.EnableLaterMouseEvent = false;
                    this.Close(screen);
                }
                else
                {
                    screen.IsHolding = false;
                    if (screen.PopUndoneWork().Kind != UndoneWorkKind.tupianwenzi)
                    {
                        //throw new Exception("The UndoneWork is not a tupianwenzi.");
                    }
                    screen.OnMouseLeftDown -= new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
                    screen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    screen.EnableLaterMouseEvent = true;

                    this.iConfirmationDialog = null;
                    if (this.CloseFunction != null)
                    {
                        this.CloseFunction();
                        this.CloseFunction = null;
                    }
                }
            }
        }

        private Rectangle PortraitDisplayPosition
        {
            get
            {
                return new Rectangle(this.PortraitClient.X + this.DisplayOffset.X, this.PortraitClient.Y + this.DisplayOffset.Y, this.PortraitClient.Width, this.PortraitClient.Height);
            }
        }


        //private Texture2D CaiseTupianZhuanchengHeibai(GraphicsDevice device, Texture2D YuanTupian)
        //{


        //    int Height = YuanTupian.Height;
        //    int Width = YuanTupian.Width;
        //    Texture2D XinTupian = new Texture2D(device, Width, Height);


        //    Color[] pixel = GetColorDataFromTexture(YuanTupian);
        //    int XiangsuGeshu = YuanTupian.Width * YuanTupian.Height;
        //    for (int i = 0; i < XiangsuGeshu; i++)

        //    {

        //        int r, g, b, Result = 0;
        //        r = pixel[i].R;
        //        g = pixel[i].G;
        //        b = pixel[i].B;
        //        //实例程序以加权平均值法产生黑白图像       
        //        int iType = 2;
        //        switch (iType)
        //        {
        //            case 0://平均值法           
        //                Result = ((r + g + b) / 3);
        //                break;
        //            case 1://最大值法                 
        //                Result = r > g ? r : g;
        //                Result = Result > b ? Result : b;
        //                break;
        //            case 2://加权平均值法             
        //                Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
        //                break;
        //        }
        //        pixel[i] = new Color(Result, Result, Result);

        //        XinTupian.SetData<Color>(pixel);
        //    }

        //    return XinTupian;
        //}

        private Color[] GetColorDataFromTexture(Texture2D texture)
        {
            Color[] colors = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(colors);
            return colors;
        }


    }
}

