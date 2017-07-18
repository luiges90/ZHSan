using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;




namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers

{
    public class RoutewayLayer
    {
        //public bool Editing;   //原程序，由于警告去掉
        private MainMapLayer mainMapLayer;
        private GameScenario scenario;

        public void Draw(SpriteBatch spriteBatch, Point viewportSize)
        {
            if (GlobalVariables.LiangdaoXitong == false) return;

            if (GlobalVariables.CurrentMapLayer == MapLayerKind.Routeway)
            {
                Rectangle? nullable;
                float num = 0f;
                Color white = new Color((byte)200, (byte)200, (byte)200, (byte)200);
            //Label_064C:
                foreach (Routeway routeway in this.scenario.Routeways.GetList())
                {
                    if ((routeway == null) || (routeway.BelongedFaction == null))
                    {
                        //goto Label_064C;
                        continue;
                    }
                    //num += -1E-06f;
                    num += -0.000001f;

                    if (this.scenario.IsCurrentPlayer(routeway.BelongedFaction))
                    {
                        if (routeway.Selected)
                        {
                            white = Color.White;
                        }
                        foreach (RoutePoint point in routeway.RoutePoints)
                        {
                            if (this.mainMapLayer.TileInScreen(point.Position))
                            {
                                if (point.Index <= routeway.LastActivePointIndex)
                                {
                                    if (routeway.InefficiencyDays > 0)
                                    {
                                        nullable = null;
                                        spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayTextures[2], this.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                    }
                                    else
                                    {
                                        nullable = null;
                                        spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayTextures[1], this.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                    }
                                }
                                else if (routeway.Building)
                                {
                                    nullable = null;
                                    spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayTextures[3], this.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                }
                                else if ((routeway.StartArchitecture != null) && ((!routeway.StartArchitecture.BelongedSection.AIDetail.AutoRun || routeway.Selected) || (routeway.DestinationArchitecture == null)))
                                {
                                    nullable = null;
                                    spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayTextures[0], this.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                }
                                if (routeway.Building || ((routeway.StartArchitecture != null) && ((!routeway.StartArchitecture.BelongedSection.AIDetail.AutoRun || routeway.Selected) || (routeway.DestinationArchitecture == null))))
                                {
                                    if (point.PreviousDirection != SimpleDirection.None)
                                    {
                                        nullable = null;
                                        spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayDirectionTailTextures[(int)point.PreviousDirection], this.mainMapLayer.GetDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.849f + num);
                                    }
                                    if (point.Direction != SimpleDirection.None)
                                    {
                                        nullable = null;
                                        spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayDirectionArrowTextures[(int)point.Direction], this.mainMapLayer.GetDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.849f + num);
                                    }
                                }
                            }
                        }
                        if (routeway.ShowArea)
                        {
                            foreach (Point point2 in routeway.RouteArea.Keys)
                            {
                                if (this.mainMapLayer.TileInScreen(point2))
                                {
                                    nullable = null;
                                    spriteBatch.Draw(this.mainMapLayer.screen.Textures.TileFrameTextures[0], this.mainMapLayer.GetThreeFourthsDestination(point2), nullable, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8498001f + num);
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
                            if (this.mainMapLayer.TileInScreen(point.Position))
                            {
                                if ((this.scenario.CurrentPlayer == null) || (this.scenario.CurrentPlayer.IsFriendly(routeway.BelongedFaction) && (GlobalVariables.SkyEye || this.scenario.CurrentPlayer.IsPositionKnown(point.Position))))
                                {
                                    nullable = null;
                                    spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayTextures[1], this.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                }
                                else if (GlobalVariables.SkyEye || (this.scenario.CurrentPlayer.GetKnownAreaData(point.Position) >= GlobalVariables.ScoutRoutewayInformationLevel))
                                {
                                    nullable = null;
                                    spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayTextures[5], this.mainMapLayer.GetThreeFourthsDestination(point.Position), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.85f + num);
                                }
                            }
                        }
                    //Label_064B: ;
                    }
                }//结束Foreach (Routeway routeway in this.scenario.Routeways.GetList())
                foreach (Point point2 in this.scenario.NoFoodDictionary.Positions.Keys)
                {
                    if (this.scenario.IsPositionDisplayable(point2))
                    {
                        nullable = null;
                        spriteBatch.Draw(this.mainMapLayer.screen.Textures.RoutewayTextures[4], this.mainMapLayer.GetThreeFourthsDestination(point2), nullable, white, 0f, Vector2.Zero, SpriteEffects.None, 0.849999f);
                    }
                }
            }//结束IF
        }//结束DRAW函数

        public void Initialize(MainMapLayer mainMapLayer, GameScenario scenario)
        {
            this.mainMapLayer = mainMapLayer;
            this.scenario = scenario;
        }
    }//结束RoutewayLayer类


}
