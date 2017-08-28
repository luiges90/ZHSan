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
using GameManager;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers
{
    public class TroopLayer
    {
        public void Draw(Point viewportSize, GameTime gameTime)
        {
            bool playerControlling = Session.Current.Scenario.IsPlayerControlling();
            if (Setting.Current.GlobalVariables.DrawTroopAnimation)
            {
                bool hold = false;
                
            //Label_097B:
                foreach (Troop troop in Session.Current.Scenario.Troops.GetList())
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
                    if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(troop.Position) && (((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsFriendly(troop.BelongedFaction)) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)))
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
                        if (!(((Session.GlobalVariables.SkyEye || (Session.Current.Scenario.CurrentPlayer == null)) || (troop.Status != TroopStatus.埋伏)) || troop.IsFriendly(Session.Current.Scenario.CurrentPlayer)))
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
                                troop.IncrementNumberList.Draw(Session.Current.Scenario.GameCommonData.NumberGenerator, new GetDisplayRectangle(Session.MainGame.mainGameScreen.mainMapLayer.GetDestination), Session.MainGame.mainGameScreen.mainMapLayer.TileWidth, gameTime);
                            }
                            if (!troop.DecrementNumberList.IsEmpty)
                            {
                                troop.DecrementNumberList.Draw(Session.Current.Scenario.GameCommonData.NumberGenerator, new GetDisplayRectangle(Session.MainGame.mainGameScreen.mainMapLayer.GetDestination), Session.MainGame.mainGameScreen.mainMapLayer.TileWidth, gameTime);
                            }
                            if (troop.PreAction != TroopPreAction.无)
                            {
                                CacheManager.Draw(troop.TileAnimation.Texture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentPreTroopActionRectangle(troop.TileAnimation.Texture.Width / troop.TileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                            }
                            this.DrawStoppedTroop( viewportSize, troop);
                            if (troop.IncrementNumberList.IsEmpty && troop.DecrementNumberList.IsEmpty)
                            {
                                troop.ShowNumber = false;
                            }
                        }
                        else if ((troop.Action == TroopAction.Stop) && (troop.PreAction != TroopPreAction.无))
                        {
                            CacheManager.Draw(troop.TileAnimation.Texture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentPreTroopActionRectangle(troop.TileAnimation.Texture.Width / troop.TileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                            this.DrawStoppedTroop( viewportSize, troop);
                        }
                        else if (troop.Action == TroopAction.Move)
                        {
                            Rectangle currentDirectionAnimationRectangle = troop.GetCurrentDirectionAnimationRectangle(Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination);
                            CacheManager.Draw(troop.TroopTexture, currentDirectionAnimationRectangle, new Rectangle?(troop.GetCurrentStopDisplayRectangle(troop.TroopTexture.Width / troop.CurrentAnimation.FrameCount)), white, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                            if (troop.CurrentStunt != null)
                            {
                                CacheManager.Draw(troop.StuntTileAnimation.Texture, currentDirectionAnimationRectangle, new Rectangle?(troop.GetStuntTroopTileAnimationRectangle(troop.StuntTileAnimation.Texture.Width / troop.StuntTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                            }
                        }
                        else
                        {
                            Rectangle? nullable;
                            if ((troop.Action == TroopAction.Attack) || (troop.Action == TroopAction.Cast))
                            {
                                if (troop.PreAction != TroopPreAction.无)
                                {
                                    CacheManager.Draw(troop.TileAnimation.Texture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentPreTroopActionRectangle(troop.TileAnimation.Texture.Width / troop.TileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                                    hold = true;
                                }
                                troop.PlayCriticalAttackSound();
                                CacheManager.Draw(troop.TroopTexture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentDisplayRectangle(troop.TroopTexture.Width / troop.CurrentAnimation.FrameCount, hold)), white, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                                nullable = null;
                                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[4], Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.699f);
                                if (troop.CurrentStunt != null)
                                {
                                    CacheManager.Draw(troop.StuntTileAnimation.Texture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetStuntTroopTileAnimationRectangle(troop.StuntTileAnimation.Texture.Width / troop.StuntTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
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
                                    CacheManager.Draw(troop.EffectTileAnimation.Texture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetEffectTroopTileAnimationRectangle(troop.EffectTileAnimation.Texture.Width / troop.EffectTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
                                }
                                CacheManager.Draw(troop.TroopTexture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentDisplayRectangle(troop.TroopTexture.Width / troop.CurrentAnimation.FrameCount, hold)), white, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                                nullable = null;
                                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[2], Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.699f);
                                if (troop.CurrentStunt != null)
                                {
                                    CacheManager.Draw(troop.StuntTileAnimation.Texture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetStuntTroopTileAnimationRectangle(troop.StuntTileAnimation.Texture.Width / troop.StuntTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
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
                                this.DrawStoppedTroop(viewportSize, troop);
                            }
                        }
                        Session.MainGame.mainGameScreen.Plugins.TroopTitlePlugin.DrawTroop( troop, playerControlling);
                        this.DrawTroopTarget( troop, viewportSize, gameTime);
                    }
                    else
                    {
                        troop.SetNotShowing();
                    }
                }
            }
            else
            {
                foreach (Troop troop in Session.Current.Scenario.Troops.GetList())
                {
                    if ((troop != null) && !troop.Destroyed)
                    {
                        troop.SetNotShowing();
                        if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(troop.Position) && (((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsFriendly(troop.BelongedFaction)) || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(troop.Position)))
                        {
                            this.DrawStoppedTroop( viewportSize, troop);
                            Session.MainGame.mainGameScreen.Plugins.TroopTitlePlugin.DrawTroop( troop, playerControlling);
                            this.DrawTroopTarget( troop, viewportSize, gameTime);
                        }
                    }
                }
            }
        }

        private void DrawStoppedTroop(Point viewportSize, Troop troop)
        {
            Rectangle? nullable;
            Color white = Color.White;
            if ((Session.MainGame.mainGameScreen.DrawingSelector && (troop.Status == TroopStatus.一般)) && 
                Session.Current.Scenario.IsCurrentPlayer(troop.BelongedFaction) && !troop.Operated)
            {
                Point positionByPoint = Session.MainGame.mainGameScreen.GetPositionByPoint(Session.MainGame.mainGameScreen.SelectorStartPosition);
                Point point2 = Session.MainGame.mainGameScreen.GetPositionByPoint(Session.MainGame.mainGameScreen.MousePosition);
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
            CacheManager.Draw(troop.TroopTexture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetCurrentStopDisplayRectangle(troop.TroopTexture.Width / troop.CurrentAnimation.FrameCount)), white, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
            if (Session.MainGame.mainGameScreen.SelectorTroops.HasGameObject(troop.ID))
            {
                nullable = null;
                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[4], Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.699f);
            }
            if (troop.Surrounding)
            {
                nullable = null;
                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[4], Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.699f);
            }
            if (troop.Status != TroopStatus.一般)
            {
                CacheManager.Draw(troop.StatusTileAnimation.Texture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetStatusTroopTileAnimationRectangle(troop.StatusTileAnimation.Texture.Width / troop.StatusTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
            }
            if (troop.CurrentStunt != null)
            {
                CacheManager.Draw(troop.StuntTileAnimation.Texture, Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.Position.X, troop.Position.Y].Destination, new Rectangle?(troop.GetStuntTroopTileAnimationRectangle(troop.StuntTileAnimation.Texture.Width / troop.StuntTileAnimation.FrameCount)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.6998f);
            }
            if ((Session.MainGame.mainGameScreen.CurrentTroop == troop) && (Session.MainGame.mainGameScreen.UndoneWorks.Peek().Kind == UndoneWorkKind.ContextMenu))
            {
                foreach (Point point3 in troop.OffenceArea.Area)
                {
                    if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(point3))
                    {
                        nullable = null;
                        CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[2], Session.MainGame.mainGameScreen.mainMapLayer.Tiles[point3.X, point3.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.7f);
                    }
                }
            }
        }

        private void DrawTroopPath(Troop troop, Point viewportSize, GameTime gameTime)
        {
            if (((Session.MainGame.mainGameScreen.CurrentTroop == troop) && (Session.MainGame.mainGameScreen.UndoneWorks.Peek().Kind == UndoneWorkKind.Selecting)) && (((SelectingUndoneWorkKind)Session.MainGame.mainGameScreen.UndoneWorks.Peek().SubKind) == SelectingUndoneWorkKind.TroopDestination))
            {
                Rectangle rectangle;
                Rectangle? nullable;
                if (troop.FirstTierPath != null)
                {
                    foreach (Point point in troop.UnfinishedFirstTierPath)
                    {
                        if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(point))
                        {
                            nullable = null;
                            CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[0], Session.MainGame.mainGameScreen.mainMapLayer.Tiles[point.X, point.Y].Destination, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
                        }
                    }
                }
                if (troop.SecondTierPath != null)
                {
                    rectangle.Width = Session.MainGame.mainGameScreen.mainMapLayer.TileWidth * GameObjectConsts.SecondTierSquareSize;
                    rectangle.Height = Session.MainGame.mainGameScreen.mainMapLayer.TileHeight * GameObjectConsts.SecondTierSquareSize;
                    foreach (Point point in troop.UnfinishedSecondTierPath)
                    {
                        rectangle.X = Session.MainGame.mainGameScreen.mainMapLayer.LeftEdge + (point.X * rectangle.Width);
                        rectangle.Y = Session.MainGame.mainGameScreen.mainMapLayer.TopEdge + (point.Y * rectangle.Height);
                        if (StaticMethods.RectangleInViewport(rectangle, viewportSize))
                        {
                            nullable = null;
                            CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[5], rectangle, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
                        }
                    }
                }
                if (troop.ThirdTierPath != null)
                {
                    rectangle.Width = Session.MainGame.mainGameScreen.mainMapLayer.TileWidth * GameObjectConsts.ThirdTierSquareSize;
                    rectangle.Height = Session.MainGame.mainGameScreen.mainMapLayer.TileHeight * GameObjectConsts.ThirdTierSquareSize;
                    foreach (Point point in troop.UnfinishedThirdTierPath)
                    {
                        rectangle.X = Session.MainGame.mainGameScreen.mainMapLayer.LeftEdge + (point.X * rectangle.Width);
                        rectangle.Y = Session.MainGame.mainGameScreen.mainMapLayer.TopEdge + (point.Y * rectangle.Height);
                        if (StaticMethods.RectangleInViewport(rectangle, viewportSize))
                        {
                            nullable = null;
                            CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[5], rectangle, nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
                        }
                    }
                }
                if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(troop.RealDestination))
                {
                    CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[6], Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.RealDestination.X, troop.RealDestination.Y].Destination, null, new Color(255f, (float)(gameTime.TotalGameTime.Milliseconds / 4), 255f), 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
                }
            }
        }

        private void DrawTroopTarget(Troop troop, Point viewportSize, GameTime gameTime)
        {
            if ((((Session.MainGame.mainGameScreen.CurrentTroop == troop) && (Session.MainGame.mainGameScreen.UndoneWorks.Peek().Kind == UndoneWorkKind.Selecting)) && (((SelectingUndoneWorkKind)Session.MainGame.mainGameScreen.UndoneWorks.Peek().SubKind) == SelectingUndoneWorkKind.TroopTarget)) && ((troop.TargetTroop != null) && Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(troop.TargetTroop.Position)))
            {
                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[6], Session.MainGame.mainGameScreen.mainMapLayer.Tiles[troop.TargetTroop.Position.X, troop.TargetTroop.Position.Y].Destination, null, new Color(255f, (float)(gameTime.TotalGameTime.Milliseconds / 4), 255f), 0f, Vector2.Zero, SpriteEffects.None, 0.499f);
            }
        }

        public void Initialize()
        {
        }
    }

 

}
