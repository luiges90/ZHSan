using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DateRunnerPlugin
{

    internal class DateRunner : Tool
    {
        internal Texture2D BackgroundTexture;
        internal GameDate Date;
        private int daysLeftBackUp;

        public Font DaysLeftTextBuilder = new Font();
        //internal FreeTextBuilder DaysLeftTextBuilder = new FreeTextBuilder();

        internal Color DaysLeftTextColor;
        internal Rectangle DaysLeftTextPosition;
        //private Texture2D DaysLeftTextTexture;
        internal int DaysToGo = 1;
        internal Rectangle DaysToGoFirstDigitTextPosition;
        //private Texture2D DaysToGoFirstDigitTextTexture;
        internal Rectangle DaysToGoSecondDigitTextPosition;
        //private Texture2D DaysToGoSecondDigitTextTexture;

        public Font DaysToGoTextBuilder = new Font();
        //internal FreeTextBuilder DaysToGoTextBuilder = new FreeTextBuilder();

        internal Color DaysToGoTextColor;
        private Texture2D FirstDigitLowerArrowDisplayTexture;
        internal Rectangle FirstDigitLowerArrowPosition;
        private Texture2D FirstDigitUpperArrowDisplayTexture;
        internal Rectangle FirstDigitUpperArrowPosition;
        internal Texture2D LowerArrowSelectedTexture;
        internal Texture2D LowerArrowTexture;
        private const int MaxDay = 0x63;
        internal Texture2D PauseSelectedTexture;
        internal Texture2D PauseTexture;
        private Texture2D PlayDisplayTexture;
        private bool playing = false;
        internal Rectangle PlayPosition;
        internal Texture2D PlaySelectedTexture;
        internal Texture2D PlayTexture;
        private bool runLastDay = false;
        private Screen screen;
        private Texture2D SecondDigitLowerArrowDisplayTexture;
        internal Rectangle SecondDigitLowerArrowPosition;
        private Texture2D SecondDigitUpperArrowDisplayTexture;
        internal Rectangle SecondDigitUpperArrowPosition;
        private Texture2D StopDisplayTexture;
        internal Rectangle StopPosition;
        internal Texture2D StopSelectedTexture;
        internal Texture2D StopTexture;
        internal bool Updated = false;
        internal Texture2D UpperArrowSelectedTexture;
        internal Texture2D UpperArrowTexture;
        internal bool yizhiyunxing = false;

        private const int MAX_DAY = 99;

        internal void DateGo()
        {
            if (this.playing && (this.DaysLeft > 0))
            {
                this.DateStartRunning();
            }
        }

        private void DateStartRunning()
        {
            if (this.Date.StartRunning())
            {
            }
        }

        internal void DateStop()
        {
            if (this.Date.EndRunning() && !GlobalVariables.EnableResposiveThreading)
            {
                if (this.yizhiyunxing)
                {
                    this.DaysLeft = MAX_DAY;
                }
                this.Date.Go();
                if (this.DaysLeft == 0)
                {
                    this.playing = false;
                    if (this.PlayDisplayTexture == this.PauseSelectedTexture)
                    {
                        this.PlayDisplayTexture = this.PlaySelectedTexture;
                    }
                    else
                    {
                        this.PlayDisplayTexture = this.PlayTexture;
                    }
                }
                this.Updated = false;
            }
        }

        private string DaysLeftString()
        {
            int daysLeftBackUp = this.daysLeftBackUp;
            if (daysLeftBackUp == 0)
            {
                daysLeftBackUp = this.DaysLeft;
            }
            if (daysLeftBackUp > 9)
            {
                return daysLeftBackUp.ToString();
            }
            return ("0" + daysLeftBackUp.ToString());
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = null;
            spriteBatch.Draw(this.FirstDigitUpperArrowDisplayTexture, this.FirstDigitUpperArrowDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            sourceRectangle = null;
            spriteBatch.Draw(this.FirstDigitLowerArrowDisplayTexture, this.FirstDigitLowerArrowDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            sourceRectangle = null;
            spriteBatch.Draw(this.SecondDigitUpperArrowDisplayTexture, this.SecondDigitUpperArrowDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            sourceRectangle = null;
            spriteBatch.Draw(this.SecondDigitLowerArrowDisplayTexture, this.SecondDigitLowerArrowDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            sourceRectangle = null;

            var first = ((this.DaysToGo / 10) % 10).ToString();
            var pos = new Vector2(this.DaysToGoFirstDigitTextDisplayPosition.X, this.DaysToGoFirstDigitTextDisplayPosition.Y);

            var scale = 1f; //DaysToGoTextBuilder.Scale

            CacheManager.DrawString(Session.Current.Font, first, pos, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.0999f);
            
            //spriteBatch.Draw(this.DaysToGoFirstDigitTextTexture, this.DaysToGoFirstDigitTextDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0999f);

            sourceRectangle = null;

            int num = this.DaysToGo % 10;
            //num.ToString()
            pos = new Vector2(this.DaysToGoSecondDigitTextDisplayPosition.X, this.DaysToGoSecondDigitTextDisplayPosition.Y);

            scale = 1f; //DaysToGoTextBuilder.Scale

            CacheManager.DrawString(Session.Current.Font, num.ToString(), pos, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.0999f);
            //spriteBatch.Draw(this.DaysToGoSecondDigitTextTexture, this.DaysToGoSecondDigitTextDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0999f);

            sourceRectangle = null;
            spriteBatch.Draw(this.PlayDisplayTexture, this.PlayDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);
            sourceRectangle = null;
            spriteBatch.Draw(this.StopDisplayTexture, this.StopDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.099f);

            var left = this.DaysLeftString();
            pos = new Vector2(this.DaysLeftTextDisplayPosition.X, this.DaysLeftTextDisplayPosition.Y);

            scale = 1f; //DaysLeftTextBuilder.Scale

            CacheManager.DrawString(Session.Current.Font, left, pos, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.0999f);
            //spriteBatch.Draw(this.DaysLeftTextTexture, this.DaysLeftTextDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.0999f);

        }

        public override void DrawBackground(SpriteBatch spriteBatch, Rectangle Position)
        {
            spriteBatch.Draw(this.BackgroundTexture, Position, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.09999f);
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
            screen.OnMouseLeftDown += new Screen.MouseLeftDown(this.screen_OnMouseLeftDown);
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
        }

        public void Pause()
        {
            if (((this.DaysLeft > 1) && this.playing) && (this.daysLeftBackUp == 0))
            {
                this.daysLeftBackUp = this.DaysLeft - 1;
                this.DaysLeft = 1;
                this.PlayDisplayTexture = this.PlaySelectedTexture;
                this.Updated = false;
            }
        }

        public void Reset()
        {
            this.playing = false;
            this.DaysLeft = 0;
            this.daysLeftBackUp = 0;
            this.PlayDisplayTexture = this.PlayTexture;
            this.Updated = false;
        }

        internal void ResetDisplayTextures()
        {
            this.FirstDigitUpperArrowDisplayTexture = this.UpperArrowTexture;
            this.FirstDigitLowerArrowDisplayTexture = this.LowerArrowTexture;
            this.SecondDigitUpperArrowDisplayTexture = this.UpperArrowTexture;
            this.SecondDigitLowerArrowDisplayTexture = this.LowerArrowTexture;
            this.ResetPlayDisplayTexture();
            this.StopDisplayTexture = this.StopTexture;
        }

        private void ResetPlayDisplayTexture()
        {
            if (this.playing)
            {
                this.PlayDisplayTexture = this.PauseTexture;
            }
            else
            {
                this.PlayDisplayTexture = this.PlayTexture;
            }
        }

        public void Run()
        {
            if ((((this.DaysLeft == 0) && (this.daysLeftBackUp == 0)) && (this.DaysToGo > 0)) && !this.playing)
            {
                if ((this.screen.Scenario.CurrentFaction == this.screen.Scenario.CurrentPlayer) && this.screen.Scenario.CurrentPlayer.Controlling)
                {
                    if (this.screen.Scenario.CurrentPlayer != null)
                    {
                        this.screen.Scenario.CurrentPlayer.Passed = true;
                        this.screen.Scenario.CurrentPlayer.Controlling = false;
                    }
                    if (this.screen.Scenario.IsLastPlayer(this.screen.Scenario.CurrentPlayer))
                    {
                        this.RunDays(this.DaysToGo);
                    }
                }
            }
            else if (((this.DaysLeft > 1) && this.playing) && (this.daysLeftBackUp == 0)) //这里是暂停
            {
                this.yizhiyunxing = false;

                this.daysLeftBackUp = this.DaysLeft - 1;
                this.DaysLeft = 1;
                this.PlayDisplayTexture = this.PlaySelectedTexture;
                this.Updated = false;
            }
            else if ((((this.daysLeftBackUp > 0) && !this.playing) && (this.DaysLeft == 0)) && ((this.screen.Scenario.CurrentFaction == this.screen.Scenario.CurrentPlayer) && this.screen.Scenario.CurrentPlayer.Controlling))
            {
                if (this.screen.Scenario.CurrentPlayer != null)
                {
                    this.screen.Scenario.CurrentPlayer.Passed = true;
                    this.screen.Scenario.CurrentPlayer.Controlling = false;
                }
                if (this.screen.Scenario.IsLastPlayer(this.screen.Scenario.CurrentPlayer))
                {
                    this.RunDays(0);
                }
            }
        }

        internal void RunDays(int Days)
        {
            if (Days == -999)
            {
                this.yizhiyunxing = true;
            }
            this.playing = true;
            this.DaysLeft += Days + this.daysLeftBackUp;
            this.daysLeftBackUp = 0;
            if (this.DaysLeft > MAX_DAY || this.yizhiyunxing)
            {
                this.DaysLeft = MAX_DAY;
            }
            this.Updated = false;
            if (this.PlayDisplayTexture == this.PlayTexture)
            {
                this.PlayDisplayTexture = this.PauseTexture;
            }
            else if (this.PlayDisplayTexture == this.PlaySelectedTexture)
            {
                this.PlayDisplayTexture = this.PauseSelectedTexture;
            }
        }

        private void screen_OnMouseLeftDown(Point position)
        {
            if (base.Enabled)
            {
                if (StaticMethods.PointInRectangle(position, this.PlayDisplayPosition))
                {
                    this.Run();
                }
                else if (StaticMethods.PointInRectangle(position, this.StopDisplayPosition))
                {
                    this.yizhiyunxing = false;

                    this.Stop();
                }
                else if (StaticMethods.PointInRectangle(position, this.FirstDigitUpperArrowDisplayPosition))
                {
                    if (this.DaysToGo <= 0x63)
                    {
                        this.DaysToGo += 10;
                        if (this.DaysToGo > 0x63)
                        {
                            this.DaysToGo = 0x63;
                        }
                        this.Updated = false;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.FirstDigitLowerArrowDisplayPosition))
                {
                    if (this.DaysToGo > 1)
                    {
                        this.DaysToGo -= 10;
                        if (this.DaysToGo < 1)
                        {
                            this.DaysToGo = 1;
                        }
                        this.Updated = false;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.SecondDigitUpperArrowDisplayPosition))
                {
                    if (this.DaysToGo < 0x63)
                    {
                        this.DaysToGo++;
                        this.Updated = false;
                    }
                }
                else if (StaticMethods.PointInRectangle(position, this.SecondDigitLowerArrowDisplayPosition) && (this.DaysToGo > 1))
                {
                    this.DaysToGo--;
                    this.Updated = false;
                }
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (base.Enabled)
            {
                if (StaticMethods.PointInRectangle(position, this.PlayDisplayPosition))
                {
                    if (this.PlayDisplayTexture == this.PlayTexture)
                    {
                        this.PlayDisplayTexture = this.PlaySelectedTexture;
                    }
                    else if (this.PlayDisplayTexture == this.PauseTexture)
                    {
                        this.PlayDisplayTexture = this.PauseSelectedTexture;
                    }
                    this.StopDisplayTexture = this.StopTexture;
                }
                else if (StaticMethods.PointInRectangle(position, this.StopDisplayPosition))
                {
                    this.StopDisplayTexture = this.StopSelectedTexture;
                    this.ResetPlayDisplayTexture();
                }
                else if (StaticMethods.PointInRectangle(position, this.FirstDigitUpperArrowDisplayPosition))
                {
                    this.FirstDigitUpperArrowDisplayTexture = this.UpperArrowSelectedTexture;
                }
                else if (StaticMethods.PointInRectangle(position, this.FirstDigitLowerArrowDisplayPosition))
                {
                    this.FirstDigitLowerArrowDisplayTexture = this.LowerArrowSelectedTexture;
                }
                else if (StaticMethods.PointInRectangle(position, this.SecondDigitUpperArrowDisplayPosition))
                {
                    this.SecondDigitUpperArrowDisplayTexture = this.UpperArrowSelectedTexture;
                }
                else if (StaticMethods.PointInRectangle(position, this.SecondDigitLowerArrowDisplayPosition))
                {
                    this.SecondDigitLowerArrowDisplayTexture = this.LowerArrowSelectedTexture;
                }
                else
                {
                    this.ResetDisplayTextures();
                }
            }
        }

        public void Stop()
        {
            if ((this.playing && (this.DaysLeft > 1)) && (this.daysLeftBackUp == 0))
            {
                this.DaysLeft = 1;
                this.PlayDisplayTexture = this.PlayTexture;
                this.Updated = false;
            }
            else if (((!this.playing && (this.daysLeftBackUp > 0)) && (this.DaysLeft == 0)) && ((this.screen.Scenario.CurrentFaction == this.screen.Scenario.CurrentPlayer) && this.screen.Scenario.CurrentPlayer.Controlling))
            {
                if (this.screen.Scenario.CurrentPlayer.Passed)
                {
                    this.screen.Scenario.CurrentPlayer.Passed = false;
                }
                this.daysLeftBackUp = 0;

                this.PlayDisplayTexture = this.PlayTexture;
                this.Updated = false;
            }
        }

        public override void Update()
        {
            if (!this.Updated)
            {
                //int num = this.DaysToGo % 10;
                //this.DaysToGoSecondDigitTextTexture = this.DaysToGoTextBuilder.CreateTextTexture(num.ToString());
                //this.DaysToGoFirstDigitTextTexture = this.DaysToGoTextBuilder.CreateTextTexture(((this.DaysToGo / 10) % 10).ToString());
                //this.DaysLeftTextTexture = this.DaysLeftTextBuilder.CreateTextTexture(this.DaysLeftString());
                this.Updated = true;
            }
        }

        internal int DaysLeft
        {
            get
            {
                return this.Date.DaysLeft;
            }
            set
            {
                this.Date.DaysLeft = value;
            }
        }

        private Rectangle DaysLeftTextDisplayPosition
        {
            get
            {
                return new Rectangle(this.DaysLeftTextPosition.X + this.DisplayOffset.X, this.DaysLeftTextPosition.Y + this.DisplayOffset.Y, this.DaysLeftTextPosition.Width, this.DaysLeftTextPosition.Height);
            }
        }

        private Rectangle DaysToGoFirstDigitTextDisplayPosition
        {
            get
            {
                return new Rectangle(this.DaysToGoFirstDigitTextPosition.X + this.DisplayOffset.X, this.DaysToGoFirstDigitTextPosition.Y + this.DisplayOffset.Y, this.DaysToGoFirstDigitTextPosition.Width, this.DaysToGoFirstDigitTextPosition.Height);
            }
        }

        private Rectangle DaysToGoSecondDigitTextDisplayPosition
        {
            get
            {
                return new Rectangle(this.DaysToGoSecondDigitTextPosition.X + this.DisplayOffset.X, this.DaysToGoSecondDigitTextPosition.Y + this.DisplayOffset.Y, this.DaysToGoSecondDigitTextPosition.Width, this.DaysToGoSecondDigitTextPosition.Height);
            }
        }

        private Rectangle FirstDigitLowerArrowDisplayPosition
        {
            get
            {
                return new Rectangle(this.FirstDigitLowerArrowPosition.X + this.DisplayOffset.X, this.FirstDigitLowerArrowPosition.Y + this.DisplayOffset.Y, this.FirstDigitLowerArrowPosition.Width, this.FirstDigitLowerArrowPosition.Height);
            }
        }

        private Rectangle FirstDigitUpperArrowDisplayPosition
        {
            get
            {
                return new Rectangle(this.FirstDigitUpperArrowPosition.X + this.DisplayOffset.X, this.FirstDigitUpperArrowPosition.Y + this.DisplayOffset.Y, this.FirstDigitUpperArrowPosition.Width, this.FirstDigitUpperArrowPosition.Height);
            }
        }

        private Rectangle PlayDisplayPosition
        {
            get
            {
                return new Rectangle(this.PlayPosition.X + this.DisplayOffset.X, this.PlayPosition.Y + this.DisplayOffset.Y, this.PlayPosition.Width, this.PlayPosition.Height);
            }
        }

        private Rectangle SecondDigitLowerArrowDisplayPosition
        {
            get
            {
                return new Rectangle(this.SecondDigitLowerArrowPosition.X + this.DisplayOffset.X, this.SecondDigitLowerArrowPosition.Y + this.DisplayOffset.Y, this.SecondDigitLowerArrowPosition.Width, this.SecondDigitLowerArrowPosition.Height);
            }
        }

        private Rectangle SecondDigitUpperArrowDisplayPosition
        {
            get
            {
                return new Rectangle(this.SecondDigitUpperArrowPosition.X + this.DisplayOffset.X, this.SecondDigitUpperArrowPosition.Y + this.DisplayOffset.Y, this.SecondDigitUpperArrowPosition.Width, this.SecondDigitUpperArrowPosition.Height);
            }
        }

        private Rectangle StopDisplayPosition
        {
            get
            {
                return new Rectangle(this.StopPosition.X + this.DisplayOffset.X, this.StopPosition.Y + this.DisplayOffset.Y, this.StopPosition.Width, this.StopPosition.Height);
            }
        }
    }
}

