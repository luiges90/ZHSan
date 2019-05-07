using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameFreeText;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using GameManager;


//using	System.Drawing;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers
{
    public class SelectingLayer
    {
        private bool allowToSelectOutsideArea = false;
        public GameArea Area;
        private SelectingUndoneWorkKind areaFrameKind;
        private PlatformTexture areaFrameTexture;
        public bool Canceled = false;
        public bool CanSelectArchitecture = true;
        private FreeText Conment;
        private PlatformTexture currentPositionTexture;
        private GameArea effectingArea;
        public bool EffectingAreaOblique;
        public int EffectingAreaRadius;
        private PlatformTexture EffectingAreaTexture;
        public GameArea FromArea;
        private bool isShowing = false;
        public Point SelectedPoint;
        public bool ShowComment = false;
        public bool SingleWay;

        public void Draw(Point viewportSize)
        {
            if (this.Area != null)
            {
                Rectangle? nullable;
                foreach (Point point in this.Area.Area)
                {
                    if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(point))
                    {
                        nullable = null;
                        CacheManager.Draw(this.areaFrameTexture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[point.X, point.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
                    }
                }
                if (this.allowToSelectOutsideArea || this.Area.HasPoint(this.SelectedPoint))
                {
                    int xpt = Math.Max(Math.Min(Session.Current.Scenario.ScenarioMap.MapDimensions.X - 1, this.SelectedPoint.X), 0);
                    int ypt = Math.Max(Math.Min(Session.Current.Scenario.ScenarioMap.MapDimensions.Y - 1, this.SelectedPoint.Y), 0);
                    nullable = null;
                    CacheManager.Draw(this.currentPositionTexture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[xpt, ypt].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4998f);
                    if (this.EffectingAreaRadius > 0)
                    {
                        foreach (Point point in this.EffectingArea.Area)
                        {
                            if (!Session.Current.Scenario.PositionOutOfRange(point) && Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(point))
                            {
                                nullable = null;
                                CacheManager.Draw(this.EffectingAreaTexture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[point.X, point.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4998f);
                            }
                        }
                    }
                    if (this.ShowComment)
                    {
                        this.Conment.Position = new Rectangle(0, 0, Session.MainGame.mainGameScreen.mainMapLayer.TileWidth, Session.MainGame.mainGameScreen.mainMapLayer.TileHeight);
                        this.Conment.DisplayOffset = new Point(Session.MainGame.mainGameScreen.mainMapLayer.Tiles[this.SelectedPoint.X, this.SelectedPoint.Y].Destination.X, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[this.SelectedPoint.X, this.SelectedPoint.Y].Destination.Y);
                        int returnDays = 0;
                        if (this.SingleWay)
                        {
                            returnDays = Math.Max(Session.Current.Scenario.GetReturnDays(this.SelectedPoint, this.FromArea) / 2, 1) * Session.Parameters.DayInTurn;
                        }
                        else
                        {
                            returnDays = Session.Current.Scenario.GetReturnDays(this.SelectedPoint, this.FromArea) * Session.Parameters.DayInTurn;
                        }
                        this.Conment.Text = returnDays.ToString() + "天";
                        this.Conment.Draw(0.5f);
                    }
                }
            }
        }

        public void Initialize(MainGameScreen screen)
        {
            //this.Conment = new FreeText(screen.GraphicsDevice, new System.Drawing.Font("宋体", 10f), Color.White);
            this.Conment = new FreeText(new Font("宋体", 10f, ""), Color.White);
            this.Conment.Align = TextAlign.Middle;

            this.currentPositionTexture = screen.Textures.TileFrameTextures[0];
            this.EffectingAreaTexture = screen.Textures.TileFrameTextures[4];
        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (((this.Area != null) && Session.MainGame.mainGameScreen.EnableSelecting) && ((Session.MainGame.mainGameScreen.ViewMoveDirection == ViewMove.Stop) && ((!Session.MainGame.mainGameScreen.PositionOutOfScreen(position) && (this.CanSelectArchitecture || (Session.Current.Scenario.GetArchitectureByPositionNoCheck(this.SelectedPoint) == null))) && (this.allowToSelectOutsideArea || this.Area.HasPoint(this.SelectedPoint)))))
            {
                this.AreaFrameKind = SelectingUndoneWorkKind.None;
                this.Area = null;
                this.EffectingAreaRadius = 0;
                this.EffectingAreaOblique = false;
                this.Canceled = false;
                this.allowToSelectOutsideArea = false;
                this.IsShowing = false;
            }
        }

        private void screen_OnMouseMove(Point position, bool leftDown)
        {
            if (Session.MainGame.mainGameScreen.EnableSelecting && (Session.MainGame.mainGameScreen.ViewMoveDirection == ViewMove.Stop))
            {
                Point point = Session.MainGame.mainGameScreen.mainMapLayer.TranslateCoordinateToTilePosition(position.X, position.Y);
                if (!Session.Current.Scenario.PositionOutOfRange(point))
                {
                    if (this.SelectedPoint != point)
                    {
                        this.effectingArea = null;
                    }
                    this.SelectedPoint = point;
                }
            }
        }

        private void screen_OnMouseRightUp(Point position)
        {
            if (Session.MainGame.mainGameScreen.EnableSelecting)
            {
                this.AreaFrameKind = SelectingUndoneWorkKind.None;
                this.Area = null;
                this.EffectingAreaRadius = 0;
                this.EffectingAreaOblique = false;
                this.Canceled = true;
                this.allowToSelectOutsideArea = false;
                this.IsShowing = false;
            }
        }

        public void TryToShow()
        {
            if (this.Area == null)
            {
                this.Canceled = true;
                this.IsShowing = false;
            }
            else if (!((this.Area.Count != 0) || this.allowToSelectOutsideArea))
            {
                this.Canceled = true;
                this.IsShowing = false;
            }
            else
            {
                this.Canceled = false;
                this.IsShowing = true;
            }
        }

        public SelectingUndoneWorkKind AreaFrameKind
        {
            get
            {
                return this.areaFrameKind;
            }
            set
            {
                this.areaFrameKind = value;
                switch (this.areaFrameKind)
                {
                    case SelectingUndoneWorkKind.ArchitectureAvailableContactArea:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[3];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.InformationPosition:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = true;
                        return;

                    case SelectingUndoneWorkKind.TroopDestination:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[3];

                        if (Platform.IsMobilePlatForm)
                        {
                            this.allowToSelectOutsideArea = false;
                        }
                        else
                        {
                            this.allowToSelectOutsideArea = true;
                        }

                        return;

                    case SelectingUndoneWorkKind.SelectorTroopsDestination:
                        this.allowToSelectOutsideArea = true;
                        return;

                    case SelectingUndoneWorkKind.TroopTarget:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[5];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.Trooprucheng:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[3];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.TroopInvestigatePosition:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.TroopSetFirePosition:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.ArchitectureRoutewayStartPoint:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[6];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.RoutewayPointShortestNormal:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = true;
                        return;

                    case SelectingUndoneWorkKind.RoutewayPointShortestNoWater:
                        this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = true;
                        return;
                }
                this.areaFrameTexture = Session.MainGame.mainGameScreen.Textures.TileFrameTextures[0];
                this.allowToSelectOutsideArea = false;
            }
        }

        private GameArea EffectingArea
        {
            get
            {
                if (this.effectingArea == null)
                {
                    this.effectingArea = GameArea.GetArea(this.SelectedPoint, this.EffectingAreaRadius, this.EffectingAreaOblique);
                }
                return this.effectingArea;
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
                if (value)
                {
                    Session.MainGame.mainGameScreen.OnMouseLeftUp += new Screen.MouseLeftUp(screen_OnMouseLeftUp);
                    Session.MainGame.mainGameScreen.OnMouseRightUp += new Screen.MouseRightUp(screen_OnMouseRightUp);
                    Session.MainGame.mainGameScreen.OnMouseMove += new Screen.MouseMove(screen_OnMouseMove);
                    this.SelectedPoint = Session.MainGame.mainGameScreen.GetPositionByPoint(Session.MainGame.mainGameScreen.MousePosition);
                }
                else
                {
                    Session.MainGame.mainGameScreen.OnMouseLeftUp -= new Screen.MouseLeftUp(screen_OnMouseLeftUp);
                    Session.MainGame.mainGameScreen.OnMouseRightUp -= new Screen.MouseRightUp(screen_OnMouseRightUp);
                    Session.MainGame.mainGameScreen.OnMouseMove -= new Screen.MouseMove(screen_OnMouseMove);
                    Session.MainGame.mainGameScreen.PopUndoneWork();
                    this.ShowComment = false;
                    this.CanSelectArchitecture = true;
                    this.allowToSelectOutsideArea = false;
                }
            }
        }
    }

 

}
