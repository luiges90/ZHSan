using GameFreeText;
using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using WorldOfTheThreeKingdoms.GameScreens;

namespace ScreenBlindPlugin
{

    public class ScreenBlind
    {
        internal PlatformTexture AutumnTexture;
        internal Rectangle BackgroundClient;
        internal PlatformTexture BackgroundTexture;
#pragma warning disable CS0649 // Field 'ScreenBlind.DateClient' is never assigned to, and will always have its default value
        internal Rectangle DateClient;
#pragma warning restore CS0649 // Field 'ScreenBlind.DateClient' is never assigned to, and will always have its default value
        internal FreeText DateText;
        internal Rectangle FactionClient;
        internal FreeText FactionText;
        private bool isShowing;
        
        internal Rectangle SeasonClient;
        internal PlatformTexture SeasonTexture;
        internal PlatformTexture SpringTexture;
        internal PlatformTexture SummerTexture;
        internal PlatformTexture WinterTexture;

        internal void Draw()
        {
            Rectangle? sourceRectangle = null;
            CacheManager.Draw(this.BackgroundTexture, this.BackgroundClient, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.43f);
            CacheManager.Draw(this.SeasonTexture, this.SeasonClient, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.429f);

            this.DateText.Draw(0f, 0.4299f);
            this.FactionText.Draw(0.4299f);
        }

        internal void Initialize(MainGameScreen screen)
        {
            
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
            if (Session.Current.Scenario.Date.Season == GameSeason.春)
            {
                this.SeasonTexture = this.SpringTexture;
            }
            else if (Session.Current.Scenario.Date.Season == GameSeason.夏 )
            {
                this.SeasonTexture = this.SummerTexture;
            }
            else if (Session.Current.Scenario.Date.Season == GameSeason.秋 )
            {
                this.SeasonTexture = this.AutumnTexture;
            }
            else
            {
                this.SeasonTexture = this.WinterTexture;
            }
            this.DateText.Text = Session.Current.Scenario.Date.ToDateString();
            if (Session.Current.Scenario.CurrentFaction != null)
            {
                if ((Session.Current.Scenario.CurrentFaction == Session.Current.Scenario.CurrentPlayer) || Session.GlobalVariables.SkyEye)
                {
                    this.FactionText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.Name, " 【", Session.Current.Scenario.CurrentFaction.TotalTechniquePoint, "】" });
                }
                else
                {
                    this.FactionText.Text = Session.Current.Scenario.CurrentFaction.Name;
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

