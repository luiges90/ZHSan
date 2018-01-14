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

        //阿柒:新增的势力信息
        internal FreeText FactionTechText;
        internal FreeText LeaderNameText;
        internal FreeText PrinceNameText;
        internal FreeText CounsellorNameText;
        internal FreeText PersonCountText;
        internal FreeText PopulationText;
        internal FreeText ArmyText;
        internal FreeText FundText;
        internal FreeText FoodText;
        internal FreeText CapitalNameText;
        internal FreeText guanjuezifuchuanText;
        internal FreeText chaotinggongxianduText;
        internal FreeText CityCountText;

        internal FreeText FiveTigerText;
        internal FreeText GovernorNameText;

        internal void Draw()
        {
            Rectangle? sourceRectangle = null;
            CacheManager.Draw(this.BackgroundTexture, this.BackgroundClient, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.43f);
            CacheManager.Draw(this.SeasonTexture, this.SeasonClient, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.429f);

            this.DateText.Draw(0f, 0.4299f);
            this.FactionText.Draw(0.4299f);

            //阿柒:新增势力信息显示相关
            this.FactionTechText.Draw(0.4299f);
            this.LeaderNameText.Draw(0.4299f);
            this.PrinceNameText.Draw(0.4299f);
            this.CounsellorNameText.Draw(0.4299f);
            this.PersonCountText.Draw(0.4299f);
            this.PopulationText.Draw(0.4299f);
            this.ArmyText.Draw(0.4299f);
            this.FundText.Draw(0.4299f);
            this.FoodText.Draw(0.4299f);
            this.CapitalNameText.Draw(0.4299f);
            this.guanjuezifuchuanText.Draw(0.4299f);
            this.chaotinggongxianduText.Draw(0.4299f);
            this.CityCountText.Draw(0.4299f);
            this.FiveTigerText.Draw(0.4299f);
            this.GovernorNameText.Draw(0.4299f);
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
                    String counsellor = Session.Current.Scenario.CurrentFaction.Counsellor;
                    String governor = Session.Current.Scenario.CurrentFaction.Governor;
                    String fiveTigers = Session.Current.Scenario.CurrentFaction.FiveTigers;
                    if (counsellor != null && governor != null && fiveTigers != null)
                    {
                        this.CounsellorNameText.Text = counsellor;
                        this.GovernorNameText.Text = governor;
                        this.FiveTigerText.Text = fiveTigers;
                    }

                    this.FactionText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.Name, " 【", Session.Current.Scenario.CurrentFaction.TotalTechniquePoint, "】" });

                    //阿柒:新增势力信息显示
                    this.LeaderNameText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.LeaderName });
                    this.PrinceNameText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.PrinceName });
                    this.PersonCountText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.PersonCount });
                    this.PopulationText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.Population });
                    this.ArmyText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.Army });
                    this.FundText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.Fund });

                    if (Session.Current.Scenario.CurrentFaction.Food > 10000)
                    {
                        this.FoodText.Text = string.Concat(new object[] { (int)(Session.Current.Scenario.CurrentFaction.Food / 10000), "万" });
                    }
                    else
                    {
                        this.FoodText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.Food });
                    }

                    this.CapitalNameText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.CapitalName });
                    this.guanjuezifuchuanText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.guanjuezifuchuan, "（势力声望：", Session.Current.Scenario.CurrentFaction.Reputation, "）" });
                    this.chaotinggongxianduText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.chaotinggongxiandu, " •升官所需", Session.Current.Scenario.CurrentFaction.shengguanxuyaogongxiandu });
                    this.CityCountText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.CityCount, " •升官所需", Session.Current.Scenario.CurrentFaction.shengguanxuyaochengchi });

                    this.FactionText.Text = string.Concat(new object[] { Session.Current.Scenario.CurrentFaction.Name, " • ", Session.Current.Scenario.CurrentFaction.TotalTechniquePoint, });

                    this.FactionTechText.Text = string.Concat(new object[] { Session.Current.Scenario.GameCommonData.AllTechniques.GetTechnique(Session.Current.Scenario.CurrentFaction.UpgradingTechnique), "•", "余", Session.Current.Scenario.CurrentFaction.UpgradingDaysLeft, "天" });
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

