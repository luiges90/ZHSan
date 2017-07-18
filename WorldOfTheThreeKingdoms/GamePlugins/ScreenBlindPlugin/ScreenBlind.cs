using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ScreenBlindPlugin
{

    public class ScreenBlind
    {
        internal Texture2D AutumnTexture;
        internal Rectangle BackgroundClient;
        internal Texture2D BackgroundTexture;
        internal Rectangle DateClient;
        internal FreeText DateText;
        internal Rectangle FactionClient;
        internal FreeText FactionText;
        private bool isShowing;
        private Screen screen;
        internal Rectangle SeasonClient;
        internal Texture2D SeasonTexture;
        internal Texture2D SpringTexture;
        internal Texture2D SummerTexture;
        internal Texture2D WinterTexture;

        internal void Draw(SpriteBatch spriteBatch)
        {
            Rectangle? sourceRectangle = null;
            spriteBatch.Draw(this.BackgroundTexture, this.BackgroundClient, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.43f);
            spriteBatch.Draw(this.SeasonTexture, this.SeasonClient, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.429f);

            this.DateText.Draw(spriteBatch, 0f, 0.4299f);
            this.FactionText.Draw(spriteBatch, 0.4299f);
        }

        internal void Initialize(Screen screen)
        {
            this.screen = screen;
            screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (StaticMethods.PointInRectangle(position, this.BackgroundClient))
            {
                this.IsShowing = false;
            }
            else
            {
                this.IsShowing = true;
            }
        }

        internal void Update()
        {
            if (this.screen.Scenario.Date.Season == GameSeason.春)
            {
                this.SeasonTexture = this.SpringTexture;
            }
            else if (this.screen.Scenario.Date.Season == GameSeason.夏 )
            {
                this.SeasonTexture = this.SummerTexture;
            }
            else if (this.screen.Scenario.Date.Season == GameSeason.秋 )
            {
                this.SeasonTexture = this.AutumnTexture;
            }
            else
            {
                this.SeasonTexture = this.WinterTexture;
            }
            this.DateText.Text = this.screen.Scenario.Date.ToDateString();
            if (this.screen.Scenario.CurrentFaction != null)
            {
                if ((this.screen.Scenario.CurrentFaction == this.screen.Scenario.CurrentPlayer) || GlobalVariables.SkyEye)
                {
                    this.FactionText.Text = string.Concat(new object[] { this.screen.Scenario.CurrentFaction.Name, " 【", this.screen.Scenario.CurrentFaction.TotalTechniquePoint, "】" });
                }
                else
                {
                    this.FactionText.Text = this.screen.Scenario.CurrentFaction.Name;
                }
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
                if (this.isShowing != value)
                {
                    this.isShowing = value;
                    if (value)
                    {
                    }
                }
            }
        }
    }
}

