using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using PluginInterface;
using Microsoft.Xna.Framework.Graphics;
using GameObjects.Animations;




namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers
{
    public class TroopLayer
    {
        private GameScenario gameScenario;
        private MainMapLayer mainMapLayer;
        private MainGameScreen screen;

        public void Draw(SpriteBatch spriteBatch, Point viewportSize, GameTime gameTime)
        {
            bool playerControlling = this.screen.Scenario.IsPlayerControlling();
            if (GlobalVariables.DrawTroopAnimation)
            {
                bool hold = false;
                
            //Label_097B:
                foreach (Troop troop in this.screen.Scenario.Troops.GetList())
                {
                    
                    if (troop.Destroyed)
                    {
                        continue;
                    }
                    if (!troop.DrawAnimation)
                    {
                        troop.SetNotShowing();
                        continue;
                    }
                    if (this.mainMapLayer.TileInScreen(troop.Position) && (((GlobalVariables.SkyEye || this.gameScenario.NoCurrentPlayer) || this.gameScenario.CurrentPlayer.IsFriendly(troop.BelongedFaction)) || this.gameScenario.CurrentPlayer.IsPositionKnown(troop.Position)))
                    {
                        Color white = Color.White;
                        if (troop.CurrentOutburstKind == OutburstKind.愤怒)
                        {
                            white = Color.Red;
                        }
                        else if (troop.CurrentOutburstKind == OutburstKind.沉静)
                        {
                            white = Color.Green;
                        }
                        if (!(((GlobalVariables.SkyEye || (this.gameScenario.CurrentPlayer == null)) || (troop.Status != TroopStatus.埋伏)) || troop.IsFriendly(this.gameScenario.CurrentPlayer)))
                        {
                            troop.SetNotShowing();
                            continue;
                        }
                        if (troop.TileAnimation.FrameCount == 0)
                        {
                            troop.TileAnimation.FrameCount = 1;
                        }

                        if ((troop.Action == TroopAction.Stop) && troop.ShowNumber)
                        {
                            if (!troop.IncrementNumberList.IsEmpty)
                            {
                                troop.IncrementNumberList.Draw(this.screen, spriteBatch, this.mainMapLayer.screen.Scenario.GameCommonData.NumberGenerator, new GetDisplayRectangle(this.mainMapLayer.GetDestination), this.mainMapLayer.TileWidth, gameTime);
                            }
                            if (!troop.DecrementNumberList.IsEmpty)
                            {
                                troop.DecrementNumberList.Draw(this.screen, spriteBatch, this.mainMapLayer.screen.Scenario.GameCommonData.NumberGenerator, new GetDisplayRectangle(this.mainMapLayer.GetDestination), this.mainMapLayer.TileWidth, gameTime);
                            }
                            if (troop.PreAction != TroopPreAction.无)
                            {
                                spriteBatch.Draw(troop.TileAnimation.Texture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentPreTroopActionRectangle(troop.TileAnimation.Texture.Width / troop.TileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                            }
                            this.DrawStoppedTroop(spriteBatch, viewportSize, troop);
                            if (troop.IncrementNumberList.IsEmpty && troop.DecrementNumberList.IsEmpty)
                            {
                                troop.ShowNumber = false;
                            }
                        }
                        else if ((troop.Action == TroopAction.Stop) && (troop.PreAction != TroopPreAction.无))
                        {
                            spriteBatch.Draw(troop.TileAnimation.Texture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentPreTroopActionRectangle(troop.TileAnimation.Texture.Width / troop.TileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                            this.DrawStoppedTroop(spriteBatch, viewportSize, troop);
                        }
                        else if (troop.Action == TroopAction.Move)
                        {
                            Rectangle currentDirectionAnimationRectangle = troop.GetCurrentDirectionAnimationRectangle(this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination);
                            spriteBatch.Draw(troop.TroopTexture, currentDirectionAnimationRectangle, new Rectangle?(troop.GetCurrentStopDisplayRectangle(troop.TroopTexture.Width / troop.CurrentAnimation.FrameCount)), white, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                            if (troop.CurrentStunt != null)
                            {
                                spriteBatch.Draw(troop.StuntTileAnimation.Texture, currentDirectionAnimationRectangle, new Rectangle?(troop.GetStuntTroopTileAnimationRectangle(troop.StuntTileAnimation.Texture.Width / troop.StuntTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                            }
                        }
                        else
                        {
                            Rectangle? nullable;
                            if ((troop.Action == TroopAction.Attack) || (troop.Action == TroopAction.Cast))
                            {
                                if (troop.PreAction != TroopPreAction.无)
                                {
                                    spriteBatch.Draw(troop.TileAnimation.Texture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentPreTroopActionRectangle(troop.TileAnimation.Texture.Width / troop.TileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                                    hold = true;
                                }
                                troop.PlayCriticalAttackSound();
                                spriteBatch.Draw(troop.TroopTexture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentDisplayRectangle(troop.TroopTexture.Width / troop.CurrentAnimation.FrameCount, hold)), white, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                                nullable = null;
                                spriteBatch.Draw(this.mainMapLayer.screen.Textures.TileFrameTextures[4], this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.699f);
                                if (troop.CurrentStunt != null)
                                {
                                    spriteBatch.Draw(troop.StuntTileAnimation.Texture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetStuntTroopTileAnimationRectangle(troop.StuntTileAnimation.Texture.Width / troop.StuntTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                                }
                            }
                            else if ((troop.Action == TroopAction.BeAttacked) || (troop.Action == TroopAction.BeCasted))
                            {
                                if (troop.OrientationTroop != null)
                                {
                                    hold = troop.OrientationTroop.PreAction != TroopPreAction.无;
                                }
                                if (troop.Effect != TroopEffect.无)
                                {
                                    spriteBatch.Draw(troop.EffectTileAnimation.Texture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetEffectTroopTileAnimationRectangle(troop.EffectTileAnimation.Texture.Width / troop.EffectTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                                }
                                spriteBatch.Draw(troop.TroopTexture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentDisplayRectangle(troop.TroopTexture.Width / troop.CurrentAnimation.FrameCount, hold)), white, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                                nullable = null;
                                spriteBatch.Draw(this.mainMapLayer.screen.Textures.TileFrameTextures[2], this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.699f);
                                if (troop.CurrentStunt != null)
                                {
                                    spriteBatch.Draw(troop.StuntTileAnimation.Texture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetStuntTroopTileAnimationRectangle(troop.StuntTileAnimation.Texture.Width / troop.StuntTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                                }
                            }
                            else
                            {
                                if (troop.WaitForDeepChaosFrameCount > 0)
                                {
                                    if ((troop.OrientationTroop == null) || troop.OrientationTroop.Destroyed)
                                    {
                                        troop.WaitForDeepChaos = false;
                                        troop.WaitForDeepChaosFrameCount = 0;
                                    }
                                    else
                                    {
                                        troop.WaitForDeepChaosFrameCount--;
                                    }
                                }
                                this.DrawStoppedTroop(spriteBatch, viewportSize, troop);
                            }
                        }
                        this.screen.Plugins.TroopTitlePlugin.DrawTroop(spriteBatch, troop, playerControlling);
                        this.DrawTroopTarget(spriteBatch, troop, viewportSize, gameTime);
                    }
                    else
                    {
                        troop.SetNotShowing();
                    }
                }
            }
            else
            {
                foreach (Troop troop in this.screen.Scenario.Troops.GetList())
                {
                    if ((troop != null) && !troop.Destroyed)
                    {
                        troop.SetNotShowing();
                        if (this.mainMapLayer.TileInScreen(troop.Position) && (((GlobalVariables.SkyEye || this.gameScenario.NoCurrentPlayer) || this.gameScenario.CurrentPlayer.IsFriendly(troop.BelongedFaction)) || this.gameScenario.CurrentPlayer.IsPositionKnown(troop.Position)))
                        {
                            this.DrawStoppedTroop(spriteBatch, viewportSize, troop);
                            this.screen.Plugins.TroopTitlePlugin.DrawTroop(spriteBatch, troop, playerControlling);
                            this.DrawTroopTarget(spriteBatch, troop, viewportSize, gameTime);
                        }
                    }
                }
            }
        }

        private void DrawStoppedTroop(SpriteBatch spriteBatch, Point viewportSize, Troop troop)
        {
            Rectangle? nullable;
            Color white = Color.White;
            if ((this.screen.DrawingSelector && (troop.Status == TroopStatus.一般)) && 
                this.screen.Scenario.IsCurrentPlayer(troop.BelongedFaction) && !troop.Operated)
            {
                Point positionByPoint = this.screen.GetPositionByPoint(this.screen.SelectorStartPosition);
                Point point2 = this.screen.GetPositionByPoint(this.screen.MousePosition);
                Rectangle r = new Rectangle(
                    Math.Min(point2.X, positionByPoint.X), Math.Min(point2.Y, positionByPoint.Y), 
                    Math.Abs(point2.X - positionByPoint.X), Math.Abs(point2.Y - positionByPoint.Y));
                if (r.Contains(troop.Position))
                {
                    white = Color.Blue;
                }
            }
            if (troop.CurrentOutburstKind == OutburstKind.愤怒)
            {
                white = Color.Red;
            }
            else if (troop.CurrentOutburstKind == OutburstKind.沉静)
            {
                white = Color.Green;
            }
            spriteBatch.Draw(troop.TroopTexture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentStopDisplayRectangle(troop.TroopTexture.Width / troop.CurrentAnimation.FrameCount)), white, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
            if (this.screen.SelectorTroops.HasGameObject(troop.ID))
            {
                nullable = null;
                spriteBatch.Draw(this.screen.Textures.TileFrameTextures[4], this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.699f);
            }
            if (troop.Surrounding)
            {
                nullable = null;
                spriteBatch.Draw(this.screen.Textures.TileFrameTextures[4], this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.699f);
            }
            if (troop.Status != TroopStatus.一般)
            {
                spriteBatch.Draw(troop.StatusTileAnimation.Texture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetStatusTroopTileAnimationRectangle(troop.StatusTileAnimation.Texture.Width / troop.StatusTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
            }
            if (troop.CurrentStunt != null)
            {
                spriteBatch.Draw(troop.StuntTileAnimation.Texture, this.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetStuntTroopTileAnimationRectangle(troop.StuntTileAnimation.Texture.Width / troop.StuntTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
            }
            if ((this.screen.CurrentTroop == troop) && (this.screen.UndoneWorks.Peek().Kind == UndoneWorkKind.ContextMenu))
            {
                foreach (Point point3 in troop.OffenceArea.Area)
                {
                    if (this.mainMapLayer.TileInScreen(point3))
                    {
                        nullable = null;
                        spriteBatch.Draw(this.screen.Textures.TileFrameTextures[2], this.mainMapLayer.Tiles[point3.X, point3.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                    }
                }
            }
        }

        private void DrawTroopPath(SpriteBatch spriteBatch, Troop troop, Point viewportSize, GameTime gameTime)
        {
            if (((this.screen.CurrentTroop == troop) && (this.screen.UndoneWorks.Peek().Kind == UndoneWorkKind.Selecting)) && (((SelectingUndoneWorkKind)this.screen.UndoneWorks.Peek().SubKind) == SelectingUndoneWorkKind.TroopDestination))
            {
                Rectangle rectangle;
                Rectangle? nullable;
                if (troop.FirstTierPath != null)
                {
                    foreach (Point point in troop.UnfinishedFirstTierPath)
                    {
                        if (this.mainMapLayer.TileInScreen(point))
                        {
                            nullable = null;
                            spriteBatch.Draw(this.mainMapLayer.screen.Textures.TileFrameTextures[0], this.mainMapLayer.Tiles[point.X, point.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
                        }
                    }
                }
                if (troop.SecondTierPath != null)
                {
                    rectangle.Width = this.mainMapLayer.TileWidth * GameObjectConsts.SecondTierSquareSize;
                    rectangle.Height = this.mainMapLayer.TileHeight * GameObjectConsts.SecondTierSquareSize;
                    foreach (Point point in troop.UnfinishedSecondTierPath)
                    {
                        rectangle.X = this.mainMapLayer.LeftEdge + (point.X * rectangle.Width);
                        rectangle.Y = this.mainMapLayer.TopEdge + (point.Y * rectangle.Height);
                        if (StaticMethods.RectangleInViewport(rectangle, viewportSize))
                        {
                            nullable = null;
                            spriteBatch.Draw(this.mainMapLayer.screen.Textures.TileFrameTextures[5], rectangle, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
                        }
                    }
                }
                if (troop.ThirdTierPath != null)
                {
                    rectangle.Width = this.mainMapLayer.TileWidth * GameObjectConsts.ThirdTierSquareSize;
                    rectangle.Height = this.mainMapLayer.TileHeight * GameObjectConsts.ThirdTierSquareSize;
                    foreach (Point point in troop.UnfinishedThirdTierPath)
                    {
                        rectangle.X = this.mainMapLayer.LeftEdge + (point.X * rectangle.Width);
                        rectangle.Y = this.mainMapLayer.TopEdge + (point.Y * rectangle.Height);
                        if (StaticMethods.RectangleInViewport(rectangle, viewportSize))
                        {
                            nullable = null;
                            spriteBatch.Draw(this.mainMapLayer.screen.Textures.TileFrameTextures[5], rectangle, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
                        }
                    }
                }
                if (this.mainMapLayer.TileInScreen(troop.RealDestination))
                {
                    spriteBatch.Draw(this.mainMapLayer.screen.Textures.TileFrameTextures[6], this.mainMapLayer.Tiles[troop.RealDestination.X, troop.RealDestination.Y].Destination, null, new Color(255f, (float)(gameTime.TotalGameTime.Milliseconds / 4), 255f), 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
                }
            }
        }

        private void DrawTroopTarget(SpriteBatch spriteBatch, Troop troop, Point viewportSize, GameTime gameTime)
        {
            if ((((this.screen.CurrentTroop == troop) && (this.screen.UndoneWorks.Peek().Kind == UndoneWorkKind.Selecting)) && (((SelectingUndoneWorkKind)this.screen.UndoneWorks.Peek().SubKind) == SelectingUndoneWorkKind.TroopTarget)) && ((troop.TargetTroop != null) && this.mainMapLayer.TileInScreen(troop.TargetTroop.Position)))
            {
                spriteBatch.Draw(this.mainMapLayer.screen.Textures.TileFrameTextures[6], this.mainMapLayer.Tiles[troop.TargetTroop.Position.X, troop.TargetTroop.Position.Y].Destination, null, new Color(255f, (float)(gameTime.TotalGameTime.Milliseconds / 4), 255f), 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
            }
        }

        public void Initialize(MainMapLayer mainMapLayer, GameScenario scenario, MainGameScreen screen)
        {
            this.mainMapLayer = mainMapLayer;
            this.gameScenario = scenario;
            this.screen = screen;
        }
    }

 

}
