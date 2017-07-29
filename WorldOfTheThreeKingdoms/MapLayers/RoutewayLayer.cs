using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;
using GameManager;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers

{
    public class RoutewayLayer
    {
        //public bool Editing;   //原程序，由于警告去掉

        public void Draw(Point viewportSize)
        {
            if (Session.GlobalVariables.LiangdaoXitong == false) return;

            if (Session.GlobalVariables.CurrentMapLayer == MapLayerKind.Routeway)
            {
                Rectangle? nullable;
                float num = 0f;
                Color white = new Color((byte)200, (byte)200, (byte)200, (byte)200);
            //Label_064C:
                foreach (Routeway routeway in Session.Current.Scenario.Routeways.GetList())
                {
                    if ((routeway == null) || (routeway.BelongedFaction == null))
                    {
                        //goto Label_064C;
                        continue;
                    }
                    //num += -1E-06f;
                    num += -0.000001f;

                    if (Session.Current.Scenario.IsCurrentPlayer(routeway.BelongedFaction))
                    {
                        if (routeway.Selected)
                        {
                            white = Color.White;
                        }
                        foreach (RoutePoint point in routeway.RoutePoints)
                        {
                            if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(point.Position))
                            {
                                if (point.Index <= routeway.LastActivePointIndex)
                                {
                                    if (routeway.InefficiencyDays > 0)
                                    {
                                        nullable = null;
                                        CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayTextures[2], Session.MainGame.mainGameScreen.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                    }
                                    else
                                    {
                                        nullable = null;
                                        CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayTextures[1], Session.MainGame.mainGameScreen.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                    }
                                }
                                else if (routeway.Building)
                                {
                                    nullable = null;
                                    CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayTextures[3], Session.MainGame.mainGameScreen.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                }
                                else if ((routeway.StartArchitecture != null) && ((!routeway.StartArchitecture.BelongedSection.AIDetail.AutoRun || routeway.Selected) || (routeway.DestinationArchitecture == null)))
                                {
                                    nullable = null;
                                    CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayTextures[0], Session.MainGame.mainGameScreen.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                }
                                if (routeway.Building || ((routeway.StartArchitecture != null) && ((!routeway.StartArchitecture.BelongedSection.AIDetail.AutoRun || routeway.Selected) || (routeway.DestinationArchitecture == null))))
                                {
                                    if (point.PreviousDirection != SimpleDirection.None)
                                    {
                                        nullable = null;
                                        CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayDirectionTailTextures[(int)point.PreviousDirection], Session.MainGame.mainGameScreen.mainMapLayer.GetDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.849f + num);
                                    }
                                    if (point.Direction != SimpleDirection.None)
                                    {
                                        nullable = null;
                                        CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayDirectionArrowTextures[(int)point.Direction], Session.MainGame.mainGameScreen.mainMapLayer.GetDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.849f + num);
                                    }
                                }
                            }
                        }
                        if (routeway.ShowArea)
                        {
                            foreach (Point point2 in routeway.RouteArea.Keys)
                            {
                                if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(point2))
                                {
                                    nullable = null;
                                    CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[0], Session.MainGame.mainGameScreen.mainMapLayer.GetThreeFourthsDestination(point2), nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8498001f + num);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (routeway.LastActivePointIndex < 0)
                        {
                            //goto Label_064C;
                            continue;
                        }
                        foreach (RoutePoint point in routeway.RoutePoints)
                        {
                            if (point.Index > routeway.LastActivePointIndex)
                            {
                                //goto Label_064B;
                                break;
                            }
                            if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(point.Position))
                            {
                                if ((Session.Current.Scenario.CurrentPlayer == null) || (Session.Current.Scenario.CurrentPlayer.IsFriendly(routeway.BelongedFaction) && (Session.GlobalVariables.SkyEye || Session.Current.Scenario.CurrentPlayer.IsPositionKnown(point.Position))))
                                {
                                    nullable = null;
                                    CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayTextures[1], Session.MainGame.mainGameScreen.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                }
                                else if (Session.GlobalVariables.SkyEye || (Session.Current.Scenario.CurrentPlayer.GetKnownAreaData(point.Position) >= Session.GlobalVariables.ScoutRoutewayInformationLevel))
                                {
                                    nullable = null;
                                    CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayTextures[5], Session.MainGame.mainGameScreen.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                }
                            }
                        }
                    //Label_064B: ;
                    }
                }//结束Foreach (Routeway routeway in Session.Current.Scenario.Routeways.GetList())
                foreach (Point point2 in Session.Current.Scenario.NoFoodDictionary.Positions.Keys)
                {
                    if (Session.Current.Scenario.IsPositionDisplayable(point2))
                    {
                        nullable = null;
                        CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.RoutewayTextures[4], Session.MainGame.mainGameScreen.mainMapLayer.GetThreeFourthsDestination(point2), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.849999f);
                    }
                }
            }//结束IF
        }//结束DRAW函数

        public void Initialize()
        {
        }
    }//结束RoutewayLayer类


}
