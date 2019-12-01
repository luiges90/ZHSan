using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using		GameFreeText;
using		GameGlobal;
using		GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManager;
using Platforms;

namespace TroopTitlePlugin
{
    internal class TroopTitle
    {
        internal PlatformTexture ActionAutoDoneTexture;
        internal PlatformTexture ActionAutoTexture;
        internal PlatformTexture ActionDoneTexture;
        internal PlatformTexture ActionAttackedTexture;
        internal PlatformTexture ActionMovedTexture;
        internal Rectangle ActionIconPosition;
        internal PlatformTexture ActionUndoneTexture;
        public Point BackgroundSize;
        public PlatformTexture BackgroundTexture;
        internal PlatformTexture PictureNull;
        private Point displayOffset;
        public Rectangle FactionPosition;
        public PlatformTexture FactionTexture;
        internal Rectangle FoodIconPosition;
        internal PlatformTexture FoodNormalTexture;
        internal PlatformTexture FoodShortageTexture;
        private bool isShowing = true;
        public FreeText NameText;
        public FreeText binglitext;
        internal Rectangle NoControlIconPosition;
        internal PlatformTexture NoControlTexture;
        public Rectangle PortraitPosition;
        internal Rectangle StuntIconPosition;
        internal PlatformTexture StuntTexture;

        internal Rectangle shiqicaoweizhi;
        internal PlatformTexture shiqicaotupian;
        internal Rectangle shiqitiaoweizhi;
        internal PlatformTexture shiqitiaotupian;

       
        //////////以下新加
        internal string Switch1;//UI类型
        /*
        //以后再加
        internal string Switch11;//人物图片
        internal string Switch12;//人物头像
        internal string Switch21;//势力图片
        internal string Switch22;//势力名称
        internal string Switch23;//势力颜色
        internal string Switch31;//部队类型
        internal string Switch36;//粮食状态
        internal string Switch37;//特技状态
        internal string Switch38;//动作状态
        internal string Switch41;//士气条
        internal string Switch42;//战意条
        */
        private string UIKind;

        internal PlatformTexture TheBackground1;
        internal PlatformTexture TheMask11;
        internal PlatformTexture TheMask12;
        internal PlatformTexture TheMask13;
        internal PlatformTexture TheMask14;
        internal Rectangle TheBackground1Position;
        internal PlatformTexture TheBackground2;
        internal PlatformTexture TheMask21;
        internal PlatformTexture TheMask22;
        internal PlatformTexture TheMask23;
        internal PlatformTexture TheMask24;
        internal Rectangle TheBackground2Position;

        internal int PersonID;
        internal Rectangle ThePortrait1Position;
        internal Rectangle ThePortrait2Position;
/*
        internal bool HasPersonPicture
        {get
            {
                if (File.Exists(@"Content\Textures\GameComponents\TroopTitle\Data\PersonPicture\" + this.PersonID.ToString() + ".png"))
                { return true; }
                else
                { return false; }
            }
        }
        private Texture2D PersonPicture
        {get
            {
                if (HasPersonPicture == true)
                { return this.ThePersonPicture; }
                else
                { return this.PictureNull; }
            }
        }
        internal PlatformTexture ThePersonPicture;
        internal Rectangle PersonPicture1Position;
        internal Rectangle PersonPicture2Position;
        
        internal int FactionID;
        internal bool HasFactionPicture
        {get
            {
                if (File.Exists(@"Content\Textures\GameComponents\TroopTitle\Data\FactionPicture\" + this.FactionID.ToString() + ".png"))
                { return true; }
                else
                { return false; }
            }
        }
        private Texture2D FactionPicture
        {get
            {
                if (HasFactionPicture == true)
                { return this.TheFactionPicture; }
                else
                { return this.PictureNull; }
            }
        }
        internal PlatformTexture TheFactionPicture;
        internal Rectangle FactionPicture1Position;
        internal Rectangle FactionPicture2Position;
        */
        internal FreeText FactionName1Text;
        internal FreeText FactionName2Text;
        internal string FactionName1Kind;
        internal string FactionName2Kind;
        internal string ShowFactionName1Background;
        internal string ShowFactionName2Background;
        internal PlatformTexture FactionName1Background;
        internal PlatformTexture FactionName2Background;
        internal Rectangle FactionName1Position;
        internal Rectangle FactionName2Position;
        internal PlatformTexture FactionColor1Picture;
        internal PlatformTexture FactionColor1Background;
        internal PlatformTexture FactionColor2Picture;
        internal PlatformTexture FactionColor2Background;
        internal Rectangle FactionColor1Position;
        internal Rectangle FactionColor2Position;

        internal FreeText TroopName1Text;
        internal FreeText TroopName2Text;
        

        internal int TheTroopKind;
        internal PlatformTexture TheTroopKindPicture;
        internal PlatformTexture TheTroopKind11Picture;
        internal PlatformTexture TheTroopKind12Picture;
        internal PlatformTexture TheTroopKind13Picture;
        internal PlatformTexture TheTroopKind14Picture;
        internal PlatformTexture TheTroopKind15Picture;
        internal Rectangle TheTroopKind1Position;
        internal PlatformTexture TheTroopKind21Picture;
        internal PlatformTexture TheTroopKind22Picture;
        internal PlatformTexture TheTroopKind23Picture;
        internal PlatformTexture TheTroopKind24Picture;
        internal PlatformTexture TheTroopKind25Picture;
        internal Rectangle TheTroopKind2Position;
        internal FreeText Thebingli1Text;
        internal FreeText Thebingli2Text;

        internal PlatformTexture TheActionAutoDone1Texture;
        internal PlatformTexture TheActionAuto1Texture;
        internal PlatformTexture TheActionDone1Texture;
        internal PlatformTexture TheActionUndone1Texture;
        internal PlatformTexture TheActionAttacked1Texture;
        internal PlatformTexture TheActionMoved1Texture;
        internal Rectangle TheActionIcon1Position;
        
        internal PlatformTexture TheActionAutoDone2Texture;
        internal PlatformTexture TheActionAuto2Texture;
        internal PlatformTexture TheActionDone2Texture;
        internal PlatformTexture TheActionUndone2Texture;
        internal PlatformTexture TheActionAttacked2Texture;
        internal PlatformTexture TheActionMoved2Texture;
        internal Rectangle TheActionIcon2Position;
        
        internal PlatformTexture TheFoodNormal1Texture;
        internal PlatformTexture TheFoodShortage1Texture;
        internal Rectangle TheFoodIcon1Position;

        internal PlatformTexture TheFoodNormal2Texture;
        internal PlatformTexture TheFoodShortage2Texture;
        internal Rectangle TheFoodIcon2Position;

        internal PlatformTexture TheNoControl1Texture;
        internal Rectangle TheNoControlIcon1Position;

        internal PlatformTexture TheNoControl2Texture;
        internal Rectangle TheNoControlIcon2Position;
        
        internal PlatformTexture TheStunt1Texture;
        internal Rectangle TheStuntIcon1Position;
        internal PlatformTexture TheStunt2Texture;
        internal Rectangle TheStuntIcon2Position;

        internal PlatformTexture Theshiqi1Texture;
        internal Rectangle Theshiqi1Position;
        internal PlatformTexture Theshiqi2Texture;
        internal Rectangle Theshiqi2Position;
        internal PlatformTexture Thezhanyi1Texture;
        internal Rectangle Thezhanyi1Position;
        internal PlatformTexture Thezhanyi2Texture;
        internal Rectangle Thezhanyi2Position;


        internal void DrawTroop(Troop troop, bool playerControlling)
        {
            if (Session.Current.Scenario.ScenarioMap.TileWidth >= 50)
            {
                Color white = Color.White;
                this.DisplayOffset = Session.MainGame.mainGameScreen.GetPointByPosition(troop.Position);
                this.DisplayOffset = new Point(this.DisplayOffset.X, this.DisplayOffset.Y - 13);
                if (troop.BelongedFaction != null)
                {
                    white = troop.BelongedFaction.FactionColor;
                }
                TheTroopKind = troop.TheMilitaryType;
                PersonID = troop.Leader.PictureIndex;
                //FactionID = troop.BelongedFaction.ID;
                UIKind = "Old";
                if (Switch1 == "1")
                {
                    UIKind = "New1";
                }
                else if (Switch1 == "2")
                {
                    UIKind = "New2";
                }
                else if (Switch1 == "3")
                {
                    //UIKind = "New1";
                    string path1 = @"Content\Textures\GameComponents\PersonPortrait\Images\Default\" + this.PersonID.ToString() + "t.jpg";
                    //string path2 = @"Content\Textures\GameComponents\PersonPortrait\Images\Player\" + this.PersonID.ToString() + "t.jpg";

                    //if (Platform.Current.FileExists(path1))
                    //{
                        UIKind = "New2";
                    //}
                    //else if (Platform.Current.FileExists(path2))
                    //{
                    //    UIKind = "New2";
                    //}
                }
                if (UIKind == "Old")
                {                    
                    Rectangle? sourceRectangle = null;
                    CacheManager.Draw(this.BackgroundTexture, new Rectangle(this.displayOffset.X, this.displayOffset.Y, this.BackgroundSize.X, this.BackgroundSize.Y), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.47f);
                    CacheManager.Draw(this.shiqicaotupian, new Rectangle(this.displayOffset.X + this.shiqicaoweizhi.X, this.displayOffset.Y + this.shiqicaoweizhi.Y, this.shiqicaoweizhi.Width, this.shiqicaoweizhi.Height), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.469f);
                    CacheManager.Draw(this.shiqitiaotupian, new Rectangle(this.displayOffset.X + this.shiqitiaoweizhi.X, this.displayOffset.Y + this.shiqitiaoweizhi.Y, shiqitiaokuandu(troop), this.shiqitiaoweizhi.Height), sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.469f);
                    sourceRectangle = null;
                    CacheManager.Draw(this.FactionTexture, new Rectangle(this.displayOffset.X + this.FactionPosition.X, this.displayOffset.Y + this.FactionPosition.Y, this.FactionPosition.Width, this.FactionPosition.Height), sourceRectangle, white, 0f, Vector2.Zero, SpriteEffects.None, 0.469f);
                    this.NameText.Text = troop.Leader.Name;
                    this.binglitext.Text = troop.Army.Quantity.ToString();
                    this.NameText.Draw(0.47f);
                    this.binglitext.Draw(0.47f);
                    sourceRectangle = null;
                    
                    //try
                    //{
                    //CacheManager.Draw(troop.Leader.TroopPortrait, new Rectangle(this.displayOffset.X + this.PortraitPosition.X, this.displayOffset.Y + this.PortraitPosition.Y, this.PortraitPosition.Width, this.PortraitPosition.Height), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4687f);

                    CacheManager.DrawZhsanAvatar(troop.Leader, "s", new Rectangle(this.displayOffset.X + this.PortraitPosition.X, this.displayOffset.Y + this.PortraitPosition.Y, this.PortraitPosition.Width, this.PortraitPosition.Height), Color.White, 0.4687f);

                    //}
                    //catch { }

                    if (playerControlling && (Session.GlobalVariables.SkyEye || Session.Current.Scenario.IsCurrentPlayer(troop.BelongedFaction)))
                    {
                        if (!troop.ControlAvail())
                        {
                            sourceRectangle = null;
                            CacheManager.Draw(this.NoControlTexture, this.NoControlIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                        }
                        else
                        {
                            switch (troop.ControlState)
                            {
                                case TroopControlState.Undone:
                                    sourceRectangle = null;
                                    CacheManager.Draw(this.ActionUndoneTexture, this.ActionIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                                    break;

                                case TroopControlState.Done:
                                    sourceRectangle = null;
                                    CacheManager.Draw(this.ActionDoneTexture, this.ActionIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                                    break;

                                case TroopControlState.Auto:
                                    sourceRectangle = null;
                                    CacheManager.Draw(this.ActionAutoTexture, this.ActionIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                                    break;

                                case TroopControlState.AutoDone:
                                    sourceRectangle = null;
                                    CacheManager.Draw(this.ActionAutoDoneTexture, this.ActionIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                                    break;

                                case TroopControlState.Attacked:
                                    sourceRectangle = null;
                                    CacheManager.Draw(this.ActionAttackedTexture, this.ActionIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                                    break;

                                case TroopControlState.Moved:
                                    sourceRectangle = null;
                                    CacheManager.Draw(this.ActionMovedTexture, this.ActionIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                                    break;
                            }
                        }
                        if (troop.Food < troop.FoodCostPerDay)
                        {
                            sourceRectangle = null;
                            CacheManager.Draw(this.FoodShortageTexture, this.FoodIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                        }
                        /*
                        else
                        {
                            sourceRectangle = null;
                            CacheManager.Draw(this.FoodNormalTexture, this.FoodIconDisplayPosition, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                        }
                        */

                    }
                    if (troop.CurrentStunt != null)
                    {
                        CacheManager.Draw(this.StuntTexture, this.StuntIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4688f);
                    }
                }
                if (UIKind == "New1")
                {

                    TheTroopKindPicture = PictureNull;
                    if (TheTroopKind == 0) { TheTroopKindPicture = TheTroopKind11Picture; }
                    else if (TheTroopKind == 1) { TheTroopKindPicture = TheTroopKind12Picture; }
                    else if (TheTroopKind == 2) { TheTroopKindPicture = TheTroopKind13Picture; }
                    else if (TheTroopKind == 3) { TheTroopKindPicture = TheTroopKind14Picture; }
                    else if (TheTroopKind == 4) { TheTroopKindPicture = TheTroopKind15Picture; }
                    try
                    {
                        CacheManager.Draw(this.TheMask11, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.461f);
                        CacheManager.Draw(this.TheMask12, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.464f);
                        CacheManager.Draw(this.TheMask13, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.466f);
                        CacheManager.Draw(this.TheMask14, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.468f);
                        CacheManager.Draw(this.TheBackground1, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.47f);
                    }
                    catch { }
                    //try
                    //{ 
                        //CacheManager.Draw(troop.Leader.TroopPortrait, this.ThePortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.469f);

                        CacheManager.DrawZhsanAvatar(troop.Leader, "s", this.ThePortraitDisplayPosition, Color.White, 0.469f);

                        if (ShowFactionName1Background == "on")
                        {
                            CacheManager.Draw(this.FactionName1Background, this.FactionNameDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.462f);
                        }
                        CacheManager.Draw(this.FactionColor1Picture, this.FactionColorDisplayPosition, null, white, 0f, Vector2.Zero, SpriteEffects.None, 0.463f);
                        CacheManager.Draw(this.FactionColor1Background, this.FactionColorDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                    //}
                    //catch { }
                    try
                    { 
                        CacheManager.Draw(this.TheTroopKindPicture, this.TheTroopKindDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                    }
                    catch { }
                    try
                    {
                        CacheManager.Draw(this.Theshiqi1Texture, this.TheshiqiDisplayPosition(troop), this.TheshiqiPosition(troop), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.467f);
                        CacheManager.Draw(this.Thezhanyi1Texture, this.ThezhanyiDisplayPosition(troop), this.ThezhanyiPosition(troop), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.467f);

                    }                    
                    catch { }
                    if (playerControlling && (Session.GlobalVariables.SkyEye || Session.Current.Scenario.IsCurrentPlayer(troop.BelongedFaction)))
                    {
                        if (!troop.ControlAvail())
                        {
                            CacheManager.Draw(this.TheNoControl1Texture, this.TheNoControlIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                        }
                        else
                        {
                            switch (troop.ControlState)
                            {
                                case TroopControlState.Undone:
                                    CacheManager.Draw(this.TheActionUndone1Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.Done:
                                    CacheManager.Draw(this.TheActionDone1Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.Auto:
                                    CacheManager.Draw(this.TheActionAuto1Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.AutoDone:
                                    CacheManager.Draw(this.TheActionAutoDone1Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.Attacked:
                                    CacheManager.Draw(this.TheActionAttacked1Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.Moved:
                                    CacheManager.Draw(this.TheActionMoved1Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;
                            }
                        }
                        if (troop.Food < troop.FoodCostPerDay)
                        {
                            CacheManager.Draw(this.TheFoodShortage1Texture, this.TheFoodIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.463f);
                        }
                    }
                    if (troop.CurrentStunt != null)
                    {
                        CacheManager.Draw(this.TheStunt1Texture, this.TheStuntIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.463f);
                    }
                    if (FactionName1Kind == "1")
                    {
                        this.FactionName1Text.Text = troop.Leader.SurName;
                    }
                    else if (FactionName1Kind == "2")
                    {
                        this.FactionName1Text.Text = troop.Leader.BelongedFaction.Leader.SurName;
                    }
                    else if (FactionName1Kind == "3")
                    {
                        if (troop.Leader.BelongedFaction.Leader.Name == troop.Leader.BelongedFaction.Name)
                        {
                            this.FactionName1Text.Text = troop.Leader.BelongedFaction.Leader.SurName;
                        }
                        this.FactionName1Text.Text = troop.Leader.BelongedFaction.Name;
                    }
                    this.TroopName1Text.Text = troop.Leader.Name;
                    this.Thebingli1Text.Text = troop.Army.Quantity.ToString();
                    this.FactionName1Text.Draw(0.462f);
                    this.TroopName1Text.Draw(0.462f);
                    this.Thebingli1Text.Draw(0.462f);
                }
                
                if (UIKind == "New2")
                {
                    TheTroopKindPicture = PictureNull;
                    if (TheTroopKind == 0) { TheTroopKindPicture = TheTroopKind21Picture; }
                    else if (TheTroopKind == 1) { TheTroopKindPicture = TheTroopKind22Picture; }
                    else if (TheTroopKind == 2) { TheTroopKindPicture = TheTroopKind23Picture; }
                    else if (TheTroopKind == 3) { TheTroopKindPicture = TheTroopKind24Picture; }
                    else if (TheTroopKind == 4) { TheTroopKindPicture = TheTroopKind25Picture; }
                    //try
                    //{
                    if (this.TheMask21 != null)
                    {
                        CacheManager.Draw(this.TheMask21, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.461f);
                    }
                    if (this.TheMask22 != null)
                    {
                        CacheManager.Draw(this.TheMask22, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.464f);
                    }
                    if (this.TheMask23 != null)
                    {
                        CacheManager.Draw(this.TheMask23, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.466f);
                    }
                    if (this.TheMask24 != null)
                    {
                        CacheManager.Draw(this.TheMask24, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.468f);
                    }
                    if (this.TheBackground2 != null)
                    {
                        CacheManager.Draw(this.TheBackground2, this.TheBackgroundDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.47f);
                    }
                    //}
                    //catch { }
                    //try
                    //{
                    CacheManager.DrawZhsanAvatar(troop.Leader, "s", this.ThePortraitDisplayPosition, Color.White, 0.469f);
                    //CacheManager.Draw(troop.Leader.TroopPortrait, this.ThePortraitDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.469f);
                        if (ShowFactionName2Background == "on" && this.FactionName2Background != null)
                        {
                            CacheManager.Draw(this.FactionName2Background, this.FactionNameDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.462f);
                        }
                    if (this.FactionColor2Picture != null)
                    {
                        CacheManager.Draw(this.FactionColor2Picture, this.FactionColorDisplayPosition, null, white, 0f, Vector2.Zero, SpriteEffects.None, 0.463f);
                    }
                    if (this.FactionColor2Background != null)
                    {
                        CacheManager.Draw(this.FactionColor2Background, this.FactionColorDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                    }
                    //}
                    //catch { }
                    //try
                    //{
                    if (this.TheTroopKindPicture != null)
                    {
                        CacheManager.Draw(this.TheTroopKindPicture, this.TheTroopKindDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                    }
                    //}
                    //catch { }
                    //try
                    //{
                    if (this.Theshiqi2Texture != null)
                    {
                        CacheManager.Draw(this.Theshiqi2Texture, this.TheshiqiDisplayPosition(troop), this.TheshiqiPosition(troop), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.467f);
                    }
                    if (this.Thezhanyi2Texture != null)
                    {
                        CacheManager.Draw(this.Thezhanyi2Texture, this.ThezhanyiDisplayPosition(troop), this.ThezhanyiPosition(troop), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.467f);
                    }
                    //}
                    //catch { }
                    if (playerControlling && (Session.GlobalVariables.SkyEye || Session.Current.Scenario.IsCurrentPlayer(troop.BelongedFaction)))
                    {
                        if (!troop.ControlAvail())
                        {
                            CacheManager.Draw(this.TheNoControl2Texture, this.TheNoControlIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                        }
                        else
                        {
                            switch (troop.ControlState)
                            {
                                case TroopControlState.Undone:
                                    CacheManager.Draw(this.TheActionUndone2Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.Done:
                                    CacheManager.Draw(this.TheActionDone2Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.Auto:
                                    CacheManager.Draw(this.TheActionAuto2Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.AutoDone:
                                    CacheManager.Draw(this.TheActionAutoDone2Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.Attacked:
                                    CacheManager.Draw(this.TheActionAttacked2Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;

                                case TroopControlState.Moved:
                                    CacheManager.Draw(this.TheActionMoved2Texture, this.TheActionIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.465f);
                                    break;
                            }
                        }
                        if (troop.Food < troop.FoodCostPerDay)
                        {
                            CacheManager.Draw(this.TheFoodShortage2Texture, this.TheFoodIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.463f);
                        }
                    }
                    if (troop.CurrentStunt != null)
                    {
                        CacheManager.Draw(this.TheStunt2Texture, this.TheStuntIconDisplayPosition, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.463f);
                    }
                    if (FactionName2Kind == "1")
                    {
                        this.FactionName2Text.Text = troop.Leader.SurName;
                    }
                    else if (FactionName2Kind == "2")
                    {
                        this.FactionName2Text.Text = troop.Leader.BelongedFaction.Leader.SurName;
                    }
                    else if (FactionName2Kind == "3")
                    {
                        if(troop.Leader.BelongedFaction.Leader.Name==troop.Leader.BelongedFaction.Name)
                        {
                            this.FactionName2Text.Text = troop.Leader.BelongedFaction.Leader.SurName;
                        }
                        this.FactionName2Text.Text = troop.Leader.BelongedFaction.Name;
                    }
                    this.TroopName2Text.Text = troop.Leader.Name;
                    this.Thebingli2Text.Text = troop.Army.Quantity.ToString();
                    this.FactionName2Text.Draw(0.462f);
                    this.TroopName2Text.Draw(0.462f);
                    this.Thebingli2Text.Draw(0.462f);
                }
                
            }
        }

        private void ResetTextsPosition()
        {
            this.NameText.DisplayOffset = this.displayOffset;
            this.binglitext.DisplayOffset = this.displayOffset;
            //////////以下添加
            this.FactionName1Text.DisplayOffset = this.displayOffset;
            this.FactionName2Text.DisplayOffset = this.displayOffset;
            this.TroopName1Text.DisplayOffset = this.displayOffset;
            this.TroopName2Text.DisplayOffset = this.displayOffset;
            this.Thebingli1Text.DisplayOffset = this.displayOffset;
            this.Thebingli2Text.DisplayOffset = this.displayOffset;


            //////////
        }

        private Rectangle ActionIconDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.ActionIconPosition.X, this.DisplayOffset.Y + this.ActionIconPosition.Y, this.ActionIconPosition.Width, this.ActionIconPosition.Height);
            }
        }

        public Point DisplayOffset
        {
            get
            {
                return this.displayOffset;
            }
            set
            {
                this.displayOffset = value;
                this.ResetTextsPosition();
            }
        }

        private Rectangle FoodIconDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.FoodIconPosition.X, this.DisplayOffset.Y + this.FoodIconPosition.Y, this.FoodIconPosition.Width, this.FoodIconPosition.Height);
            }
        }

        public bool IsShowing
        {
            get
            {
                return this.isShowing;
            }
            set
            {
                this.isShowing = value;
            }
        }

        private Rectangle NoControlIconDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.NoControlIconPosition.X, this.DisplayOffset.Y + this.NoControlIconPosition.Y, this.NoControlIconPosition.Width, this.NoControlIconPosition.Height);
            }
        }

        private Rectangle StuntIconDisplayPosition
        {
            get
            {
                return new Rectangle(this.DisplayOffset.X + this.StuntIconPosition.X, this.DisplayOffset.Y + this.StuntIconPosition.Y, this.StuntIconPosition.Width, this.StuntIconPosition.Height);
            }
        }

        private int shiqitiaokuandu(Troop troop)
        {
            return troop.Army.Morale * this.shiqitiaoweizhi.Width  / troop.Army.MoraleCeiling;
        }
        //////////以下添加
        private Rectangle TheBackgroundDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheBackground2Position.X, this.DisplayOffset.Y + this.TheBackground2Position.Y, this.TheBackground2Position.Width, this.TheBackground2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheBackground1Position.X, this.DisplayOffset.Y + this.TheBackground1Position.Y, this.TheBackground1Position.Width, this.TheBackground1Position.Height);              
                }
            }
        }
        private Rectangle ThePortraitDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.ThePortrait2Position.X, this.DisplayOffset.Y + this.ThePortrait2Position.Y, this.ThePortrait2Position.Width, this.ThePortrait2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.ThePortrait1Position.X, this.DisplayOffset.Y + this.ThePortrait1Position.Y, this.ThePortrait1Position.Width, this.ThePortrait1Position.Height);
                }
            }
        }
        /*
        private Rectangle PersonPictureDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.PersonPicture2Position.X, this.DisplayOffset.Y + this.PersonPicture2Position.Y, this.PersonPicture2Position.Width, this.PersonPicture2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.PersonPicture1Position.X, this.DisplayOffset.Y + this.PersonPicture1Position.Y, this.PersonPicture1Position.Width, this.PersonPicture1Position.Height);
                }
            }
        }
        private Rectangle FactionPictureDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.FactionPicture2Position.X, this.DisplayOffset.Y + this.FactionPicture2Position.Y, this.FactionPicture2Position.Width, this.FactionPicture2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.FactionPicture1Position.X, this.DisplayOffset.Y + this.FactionPicture1Position.Y, this.FactionPicture1Position.Width, this.FactionPicture1Position.Height);
                }
            }
        }
         */ 
        private Rectangle FactionNameDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.FactionName2Position.X, this.DisplayOffset.Y + this.FactionName2Position.Y, this.FactionName2Position.Width, this.FactionName2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.FactionName1Position.X, this.DisplayOffset.Y + this.FactionName1Position.Y, this.FactionName1Position.Width, this.FactionName1Position.Height);
                }
            }
        }
        private Rectangle FactionColorDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.FactionColor2Position.X, this.DisplayOffset.Y + this.FactionColor2Position.Y, this.FactionColor2Position.Width, this.FactionColor2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.FactionColor1Position.X, this.DisplayOffset.Y + this.FactionColor1Position.Y, this.FactionColor1Position.Width, this.FactionColor1Position.Height);
                }
            }
        }
        private Rectangle TheTroopKindDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheTroopKind2Position.X, this.DisplayOffset.Y + this.TheTroopKind2Position.Y, this.TheTroopKind2Position.Width, this.TheTroopKind2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheTroopKind1Position.X, this.DisplayOffset.Y + this.TheTroopKind1Position.Y, this.TheTroopKind1Position.Width, this.TheTroopKind1Position.Height);
                }
            }
        }
        private Rectangle TheshiqiDisplayPosition(Troop troop)
        {
            if (UIKind == "New2")
            {
                return new Rectangle(this.DisplayOffset.X + this.Theshiqi2Position.X, this.DisplayOffset.Y + this.Theshiqi2Position.Y, troop.Army.Morale * this.Theshiqi2Position.Width / troop.Army.MoraleCeiling, this.Theshiqi2Position.Height);
            }
            else
            {
                return new Rectangle(this.DisplayOffset.X + this.Theshiqi1Position.X, this.DisplayOffset.Y + this.Theshiqi1Position.Y+this.Theshiqi1Position.Height - (troop.Army.Morale * this.Theshiqi1Position.Height / troop.Army.MoraleCeiling), this.Theshiqi1Position.Width, troop.Army.Morale * this.Theshiqi1Position.Height / troop.Army.MoraleCeiling);
            }       
        }
        private Rectangle ThezhanyiDisplayPosition(Troop troop)
        {
            if (UIKind == "New2")
            {
                return new Rectangle(this.DisplayOffset.X + this.Thezhanyi2Position.X, this.DisplayOffset.Y + this.Thezhanyi2Position.Y, troop.Army.Combativity * this.Thezhanyi2Position.Width / troop.Army.CombativityCeiling, this.Thezhanyi2Position.Height);
            }
            else
            {
                return new Rectangle(this.DisplayOffset.X + this.Thezhanyi1Position.X, this.DisplayOffset.Y + this.Thezhanyi1Position.Y + this.Thezhanyi1Position.Height - (troop.Army.Combativity * this.Thezhanyi1Position.Height / troop.Army.CombativityCeiling), this.Thezhanyi1Position.Width, troop.Army.Combativity * this.Thezhanyi1Position.Height / troop.Army.CombativityCeiling);
            }  
        }
        private Rectangle TheshiqiPosition(Troop troop)
        {
            if (UIKind == "New2")
            {
                return new Rectangle(0, 0, troop.Army.Morale * this.Theshiqi2Position.Width / troop.Army.MoraleCeiling, this.Theshiqi2Position.Height);
            }
            else
            {
                return new Rectangle(0, this.Theshiqi1Position.Height - (troop.Army.Morale * this.Theshiqi1Position.Height / troop.Army.MoraleCeiling), this.Theshiqi1Position.Width, troop.Army.Morale * this.Theshiqi1Position.Height / troop.Army.MoraleCeiling);
            }                
           
        }
        private Rectangle ThezhanyiPosition(Troop troop)
        {
            if (UIKind == "New2")
            {
                return new Rectangle(0, 0, troop.Army.Combativity * this.Thezhanyi2Position.Width / troop.Army.CombativityCeiling, this.Thezhanyi2Position.Height);
            }
            else
            {
                return new Rectangle(0,this.Thezhanyi1Position.Height-(troop.Army.Combativity * this.Thezhanyi1Position.Height / troop.Army.CombativityCeiling), this.Thezhanyi1Position.Width, troop.Army.Combativity * this.Thezhanyi1Position.Height / troop.Army.CombativityCeiling);
            }  
            
        }
        private Rectangle TheActionIconDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheActionIcon2Position.X, this.DisplayOffset.Y + this.TheActionIcon2Position.Y, this.TheActionIcon2Position.Width, this.TheActionIcon2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheActionIcon1Position.X, this.DisplayOffset.Y + this.TheActionIcon1Position.Y, this.TheActionIcon1Position.Width, this.TheActionIcon1Position.Height);
                }
            }
        }
        private Rectangle TheFoodIconDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheFoodIcon2Position.X, this.DisplayOffset.Y + this.TheFoodIcon2Position.Y, this.TheFoodIcon2Position.Width, this.TheFoodIcon2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheFoodIcon1Position.X, this.DisplayOffset.Y + this.TheFoodIcon1Position.Y, this.TheFoodIcon1Position.Width, this.TheFoodIcon1Position.Height);
                }
            }
        }
        private Rectangle TheNoControlIconDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheNoControlIcon2Position.X, this.DisplayOffset.Y + this.TheNoControlIcon2Position.Y, this.TheNoControlIcon2Position.Width, this.TheNoControlIcon2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheNoControlIcon1Position.X, this.DisplayOffset.Y + this.TheNoControlIcon1Position.Y, this.TheNoControlIcon1Position.Width, this.TheNoControlIcon1Position.Height);
                }
            }
        }
        private Rectangle TheStuntIconDisplayPosition
        {
            get
            {
                if (UIKind == "New2")
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheStuntIcon2Position.X, this.DisplayOffset.Y + this.TheStuntIcon2Position.Y, this.TheStuntIcon2Position.Width, this.TheStuntIcon2Position.Height);
                }
                else
                {
                    return new Rectangle(this.DisplayOffset.X + this.TheStuntIcon1Position.X, this.DisplayOffset.Y + this.TheStuntIcon1Position.Y, this.TheStuntIcon1Position.Width, this.TheStuntIcon1Position.Height);
                }
            }
        }
        //////////////
    }

}
