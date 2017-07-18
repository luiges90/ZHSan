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


//using	System.Drawing;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers
{
    public class SelectingLayer
    {
        private bool allowToSelectOutsideArea = false;
        public GameArea Area;
        private SelectingUndoneWorkKind areaFrameKind;
        private Texture2D areaFrameTexture;
        public bool Canceled = false;
        public bool CanSelectArchitecture = true;
        private FreeText Conment;
        private Texture2D currentPositionTexture;
        private GameArea effectingArea;
        public bool EffectingAreaOblique;
        public int EffectingAreaRadius;
        private Texture2D EffectingAreaTexture;
        public GameArea FromArea;
        private bool isShowing = false;
        private MainMapLayer mainMapLayer;
        private MainGameScreen screen;
        public Point SelectedPoint;
        public bool ShowComment = false;
        public bool SingleWay;

        public void Draw(SpriteBatch spriteBatch, Point viewportSize)
        {
            if (this.Area != null)
            {
                Rectangle? nullable;
                foreach (Point point in this.Area.Area)
                {
                    if (this.mainMapLayer.TileInScreen(point))
                    {
                        nullable = null;
                        spriteBatch.Draw(this.areaFrameTexture, this.mainMapLayer.Tiles[point.X, point.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.5f);
                    }
                }
                if (this.allowToSelectOutsideArea || this.Area.HasPoint(this.SelectedPoint))
                {
                    int xpt = Math.Max(Math.Min(this.mainMapLayer.mainMap.MapDimensions.X - 1, this.SelectedPoint.X), 0);
                    int ypt = Math.Max(Math.Min(this.mainMapLayer.mainMap.MapDimensions.Y - 1, this.SelectedPoint.Y), 0);
                    nullable = null;
                    spriteBatch.Draw(this.currentPositionTexture, this.mainMapLayer.Tiles[xpt, ypt].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4998f);
                    if (this.EffectingAreaRadius > 0)
                    {
                        foreach (Point point in this.EffectingArea.Area)
                        {
                            if (!this.screen.Scenario.PositionOutOfRange(point) && this.mainMapLayer.TileInScreen(point))
                            {
                                nullable = null;
                                spriteBatch.Draw(this.EffectingAreaTexture, this.mainMapLayer.Tiles[point.X, point.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.4998f);
                            }
                        }
                    }
                    if (this.ShowComment)
                    {
                        this.Conment.Position = new Rectangle(0, 0, this.mainMapLayer.TileWidth, this.mainMapLayer.TileHeight);
                        this.Conment.DisplayOffset = new Point(this.mainMapLayer.Tiles[this.SelectedPoint.X, this.SelectedPoint.Y].Destination.X, this.mainMapLayer.Tiles[this.SelectedPoint.X, this.SelectedPoint.Y].Destination.Y);
                        int returnDays = 0;
                        if (this.SingleWay)
                        {
                            returnDays = Math.Max(this.screen.Scenario.GetReturnDays(this.SelectedPoint, this.FromArea) / 2, 1);
                        }
                        else
                        {
                            returnDays = this.screen.Scenario.GetReturnDays(this.SelectedPoint, this.FromArea);
                        }
                        this.Conment.Text = returnDays.ToString() + "天";
                        this.Conment.Draw(spriteBatch, 0.5f);
                    }
                }
            }
        }

        public void Initialize(MainMapLayer mainMapLayer, MainGameScreen screen)
        {
            this.mainMapLayer = mainMapLayer;
            this.screen = screen;
            //this.Conment = new FreeText(screen.GraphicsDevice, new System.Drawing.Font("宋体", 10f), Color.White);
            this.Conment = new FreeText(Platform.GraphicsDevice, new Font("宋体", 10f, ""), Color.White);
            this.Conment.Align = TextAlign.Middle;

            this.currentPositionTexture = screen.Textures.TileFrameTextures[0];
            this.EffectingAreaTexture = screen.Textures.TileFrameTextures[4];
        }

        private void screen_OnMouseLeftUp(Point position)
        {
            if (((this.Area != null) && this.screen.EnableSelecting) && ((this.screen.ViewMoveDirection == ViewMove.Stop) && ((!this.screen.PositionOutOfScreen(position) && (this.CanSelectArchitecture || (this.screen.Scenario.GetArchitectureByPositionNoCheck(this.SelectedPoint) == null))) && (this.allowToSelectOutsideArea || this.Area.HasPoint(this.SelectedPoint)))))
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
            if (this.screen.EnableSelecting && (this.screen.ViewMoveDirection == ViewMove.Stop))
            {
                Point point = this.mainMapLayer.TranslateCoordinateToTilePosition(position.X, position.Y);
                if (!this.screen.Scenario.PositionOutOfRange(point))
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
            if (this.screen.EnableSelecting)
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
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[3];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.InformationPosition:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = true;
                        return;

                    case SelectingUndoneWorkKind.TroopDestination:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[3];
                        this.allowToSelectOutsideArea = true;
                        return;

                    case SelectingUndoneWorkKind.SelectorTroopsDestination:
                        this.allowToSelectOutsideArea = true;
                        return;

                    case SelectingUndoneWorkKind.TroopTarget:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[5];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.Trooprucheng:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[3];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.TroopInvestigatePosition:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.TroopSetFirePosition:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.ArchitectureRoutewayStartPoint:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[6];
                        this.allowToSelectOutsideArea = false;
                        return;

                    case SelectingUndoneWorkKind.RoutewayPointShortestNormal:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = true;
                        return;

                    case SelectingUndoneWorkKind.RoutewayPointShortestNoWater:
                        this.areaFrameTexture = this.screen.Textures.TileFrameTextures[0];
                        this.allowToSelectOutsideArea = true;
                        return;
                }
                this.areaFrameTexture = this.screen.Textures.TileFrameTextures[0];
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
                    this.screen.OnMouseLeftUp += new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.screen.OnMouseRightUp += new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.screen.OnMouseMove += new Screen.MouseMove(this.screen_OnMouseMove);
                    this.SelectedPoint = this.screen.GetPositionByPoint(this.screen.MousePosition);
                }
                else
                {
                    this.screen.OnMouseLeftUp -= new Screen.MouseLeftUp(this.screen_OnMouseLeftUp);
                    this.screen.OnMouseRightUp -= new Screen.MouseRightUp(this.screen_OnMouseRightUp);
                    this.screen.OnMouseMove -= new Screen.MouseMove(this.screen_OnMouseMove);
                    this.screen.PopUndoneWork();
                    this.ShowComment = false;
                    this.CanSelectArchitecture = true;
                    this.allowToSelectOutsideArea = false;
                }
            }
        }
    }

 

}
