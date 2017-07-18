using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PluginInterface;
using System;


namespace GameSystemPlugin
{

    public class GameSystem : Tool
    {
        private IOptionDialog OptionDialogPlugin;
        private Screen screen;
        public Texture2D SystemDisplayTexture;
        public Rectangle SystemPosition;
        public Texture2D SystemSelectedTexture;
        public Texture2D SystemTexture;

        private void BuildOptionDialog()
        {
            this.OptionDialogPlugin.SetStyle("Basic");
            this.OptionDialogPlugin.SetTitle("系统选项");
            this.OptionDialogPlugin.Clear();
            if (this.screen.Scenario.SaveAvail())
            {
                this.OptionDialogPlugin.AddOption("存储进度", null, new GameDelegates.VoidFunction(this.screen.SaveGame));
            }
            if (this.screen.Scenario.LoadAvail())
            {
                this.OptionDialogPlugin.AddOption("读取进度", null, new GameDelegates.VoidFunction(this.screen.LoadGame));
            }
            this.OptionDialogPlugin.AddOption("退出游戏", null, new GameDelegates.VoidFunction(this.screen.TryToExit));
            this.OptionDialogPlugin.AddOption("返回初始界面", null, new GameDelegates.VoidFunction(this.screen.返回初始菜单));
            this.OptionDialogPlugin.AddOption("继续游戏", null, null);
            this.OptionDialogPlugin.EndAddOptions();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.SystemDisplayTexture, this.SystemDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
        }

        public void Initialize(Screen screen)
        {
            this.screen = screen;
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
            //screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
            screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftDown);
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (base.Enabled && StaticMethods.PointInRectangle(position, this.SystemDisplayPosition))
            {
                this.ShowOptionDialog(ShowPosition.BottomRight);
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (base.Enabled)
            {
                if (StaticMethods.PointInRectangle(position, this.SystemDisplayPosition))
                {
                    this.SystemDisplayTexture = this.SystemSelectedTexture;
                }
                else
                {
                    this.SystemDisplayTexture = this.SystemTexture;
                }
            }
        }

        public void SetOptionDialog(IOptionDialog iOptionDialog)
        {
            this.OptionDialogPlugin = iOptionDialog;
        }

        public void ShowOptionDialog(ShowPosition showPosition)
        {
            this.BuildOptionDialog();
            this.OptionDialogPlugin.ShowOptionDialog(showPosition);
        }

        public override void Update()
        {
        }

        public Rectangle SystemDisplayPosition
        {
            get
            {
                return new Rectangle(this.SystemPosition.X + this.DisplayOffset.X, this.SystemPosition.Y + this.DisplayOffset.Y, this.SystemPosition.Width, this.SystemPosition.Height);
            }
        }
    }
}

