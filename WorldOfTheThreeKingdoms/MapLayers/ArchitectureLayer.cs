using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using		GameGlobal;
using		GameObjects;
using GameFreeText;
using		Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;
using GameObjects.Animations;
using WorldOfTheThreeKingdoms.Resources;
using Platforms;
using GameManager;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers

{
    public class ArchitectureLayer
    {
        //private ArchitectureList Architectures;
        
        static Point currentFrame = new Point(0, 0);
        int timeSinceLastFrame = 0;
        int millisecondsPerFrame = 180;
        Point frameSize = new Point(100, 100);
        Point sheetSize = new Point(6, 1);

        public void Draw(Point viewportSize, GameTime gameTime)
        {
            foreach (Architecture architecture in Session.Current.Scenario.Architectures)
            {
                if (Session.MainGame.mainGameScreen.ShowArchitectureConnectedLine && Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(architecture.zhongxindian))
                {
                    this.drawArchitectureConnectedLine(architecture);
                }

                Color zainanyanse = new Color();
                if (architecture.youzainan)
                {
                    zainanyanse = Color.Red;
                }
                else
                {
                    zainanyanse = Color.White;
                }


                foreach (Point point in architecture.ArchitectureArea.Area)
                {
                    if (Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(point))
                    {
                        if ((point == architecture.zhongxindian || Session.Current.Scenario.ScenarioMap.UseSimpleArchImages))
                        {
                            var texture = this.huoqujianzhutupian(architecture);
                            if (texture != null)
                            {
                                CacheManager.Draw(texture, Session.MainGame.mainGameScreen.mainMapLayer.huoqujianzhujuxing(point, architecture), null, zainanyanse, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                            }
                        }

                        if (point == architecture.dingdian && point.Y > 0)
                        {
                            //////////////////////////////////////////////////////////
                            //architecture.jianzhubiaoti.Position = this.mainMapLayer.GetDestination(point);
                            Point pointXiaYige = new Point(point.X, point.Y + 1);
                            Rectangle jianzhubiaotiPosition = Session.MainGame.mainGameScreen.mainMapLayer.GetDestination(point);
                            //////////architecture.jianzhubiaoti.DisplayOffset = new Point(0, -this.mainMapLayer.TileWidth / 2);
                            //architecture.jianzhubiaoti.Draw(0.7999f);

                            Rectangle jianzhubiaotibeijingweizhi;
                            jianzhubiaotibeijingweizhi = new Rectangle(jianzhubiaotiPosition.X + Session.MainGame.mainGameScreen.mainMapLayer.TileWidth / 2 - architecture.CaptionTexture.Width / 2, jianzhubiaotiPosition.Y + Session.MainGame.mainGameScreen.mainMapLayer.TileHeight / 2 - architecture.CaptionTexture.Height / 2, architecture.CaptionTexture.Width, architecture.CaptionTexture.Height);
                            //CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.jianzhubiaotibeijing, jianzhubiaotibeijingweizhi, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.79996f);
                            CacheManager.Draw(architecture.CaptionTexture, jianzhubiaotibeijingweizhi, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.79996f);

                            if (architecture.BelongedFaction != null && Session.MainGame.mainGameScreen.mainMapLayer.TileInScreen(architecture.jianzhuqizi.qizipoint))      //不是空城的话绘制旗子
                            {
                                timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                                if (timeSinceLastFrame > millisecondsPerFrame)
                                {
                                    timeSinceLastFrame -= millisecondsPerFrame;
                                    ++currentFrame.X;
                                    if (currentFrame.X >= sheetSize.X)
                                    {
                                        currentFrame.X = 0;
                                        //++currentFrame.Y;
                                        //if (currentFrame.Y >= sheetSize.Y)
                                        //    currentFrame.Y = 0;
                                    }
                                }

                                var des = Session.MainGame.mainGameScreen.mainMapLayer.GetDestination(architecture.jianzhuqizi.qizipoint);

                                var rec = new Rectangle(currentFrame.X * frameSize.X,
                                      currentFrame.Y * frameSize.Y,
                                      frameSize.X,
                                      frameSize.Y);

                                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.qizitupian, des, rec,
                                architecture.BelongedFaction.FactionColor, 0, Vector2.Zero, SpriteEffects.None, 0.79998f);

                                //Session.MainGame.mainGameScreen.qizidezi.Text = architecture.BelongedFaction.ToString().Substring(0, 1);
                                //Session.MainGame.mainGameScreen.qizidezi.Position = this.mainMapLayer.huoquqizijuxing (architecture.jianzhuqizi.qizipoint);
                                //Session.MainGame.mainGameScreen.qizidezi.Draw(0.7999f, Session.MainGame.mainGameScreen.qizidezi.Position);

                                var scale = Convert.ToSingle(des.Width) / 30f;

                                var text = architecture.BelongedFaction.ToString().Substring(0, 1);
                                var pos = Session.MainGame.mainGameScreen.mainMapLayer.huoquqizijuxing(architecture.jianzhuqizi.qizipoint);
                                var color = Color.White;  // architecture.BelongedFaction.FactionColor;

                                //"方正北魏楷书繁体", 30f
                                //depth:  0.7999f
                                CacheManager.DrawString(Session.Current.Font, architecture.BelongedFaction.ToString().Substring(0, 1), new Vector2(pos.X, pos.Y), color, 0f, Vector2.Zero, 0.5f * scale, SpriteEffects.None, 0.7999f);

                                if (architecture.huangdisuozai)
                                {
                                    CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.huangditupian,
                                        Session.MainGame.mainGameScreen.mainMapLayer.GetDestination(new Point(architecture.jianzhuqizi.qizipoint.X + 1, architecture.jianzhuqizi.qizipoint.Y)),
                                        null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.8f);
                                }

                            }
                        } //end      if (point == architecture.ArchitectureArea.TopLeft && point.Y>0)

                        if ((Session.GlobalVariables.SkyEye || Session.Current.Scenario.NoCurrentPlayer) || Session.Current.Scenario.CurrentPlayer.IsArchitectureKnown(architecture))
                        {
                            if (!architecture.IncrementNumberList.IsEmpty)
                            {
                                architecture.IncrementNumberList.Draw(Session.Current.Scenario.GameCommonData.NumberGenerator, new GetDisplayRectangle(Session.MainGame.mainGameScreen.mainMapLayer.GetDestination), Session.MainGame.mainGameScreen.mainMapLayer.TileWidth, gameTime);
                            }
                            if (!architecture.DecrementNumberList.IsEmpty)
                            {
                                architecture.DecrementNumberList.Draw(Session.Current.Scenario.GameCommonData.NumberGenerator, new GetDisplayRectangle(Session.MainGame.mainGameScreen.mainMapLayer.GetDestination), Session.MainGame.mainGameScreen.mainMapLayer.TileWidth, gameTime);
                                //sourceRectangle = null;
                                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.TileFrameTextures[2], Session.MainGame.mainGameScreen.mainMapLayer.GetDestination(point), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.799f);
                            }
                        }
                        else
                        {
                            architecture.IncrementNumberList.Clear();
                            architecture.DecrementNumberList.Clear();
                        }
                    }
                    else
                    {
                        architecture.IncrementNumberList.Clear();
                        architecture.DecrementNumberList.Clear();
                    }
                }
            }
        }

        private void drawArchitectureConnectedLine(Architecture architecture)
        {
            int linkType;
            foreach (Architecture connectedArchitecture in architecture.AILandLinks)
            {
                if (connectedArchitecture.AILandLinks.GameObjects.Contains(architecture))
                {
                    linkType = 0;
                }
                else
                {
                    linkType = 2;
                }
                this.drawConnectedLineArchitectureToArchitecture(architecture, connectedArchitecture, linkType);

            }
            foreach (Architecture connectedArchitecture in architecture.AIWaterLinks)
            {
                if (connectedArchitecture.AIWaterLinks.GameObjects.Contains(architecture))
                {
                    linkType = 1;
                }
                else
                {
                    linkType = 2;
                }
                this.drawConnectedLineArchitectureToArchitecture(architecture, connectedArchitecture, linkType);
            }
        }

        private void drawConnectedLineArchitectureToArchitecture(Architecture architecture, Architecture connectedArchitecture, int linkType)
        {
            this.drawPointToPointLine(Session.MainGame.mainGameScreen.mainMapLayer.GetCenterCoordinate(architecture.zhongxindian), Session.MainGame.mainGameScreen.mainMapLayer.GetCenterCoordinate(connectedArchitecture.zhongxindian), linkType);
        }

        private void drawPointToPointLine(Point point1, Point point2, int linkType)
        {

            if (Math.Abs(point2.Y - point1.Y) <= Math.Abs(point2.X - point1.X))
            {
                int x, x1, x2;
                if (point1.X < point2.X)
                {
                    x1 = point1.X;
                    x2 = point2.X;
                }
                else
                {
                    x1 = point2.X;
                    x2 = point1.X;
                }
                for (x = x1; x < x2; x += 2)
                {
                    Rectangle rectangle = new Rectangle(x, (x - point1.X) * (point2.Y - point1.Y) / (point2.X - point1.X) + point1.Y, 3, 3);

                    this.drawLinkLinePoint(linkType, rectangle);

                }
            }
            else
            {
                int y, y1, y2;
                if (point1.Y  < point2.Y )
                {
                    y1 = point1.Y ;
                    y2 = point2.Y ;
                }
                else
                {
                    y1 = point2.Y ;
                    y2 = point1.Y ;
                }
                for (y = y1; y < y2; y += 2)
                {
                    Rectangle rectangle = new Rectangle((y - point1.Y) * (point2.X - point1.X) / (point2.Y - point1.Y) + point1.X , y, 3, 3);

                    this.drawLinkLinePoint(linkType, rectangle);
                }
            }
        }

        private void drawLinkLinePoint(int linkType, Rectangle rectangle)
        {
            if (linkType == 0)
            {
                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.LandConnect, rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.7999f);

            }
            else if (linkType == 1)
            {
                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.WaterConnect , rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.7999f);

            }
            else if (linkType == 2)
            {
                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.SingleConnect , rectangle, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.7999f);
            }
        }


        private PlatformTexture huoqujianzhutupian(Architecture architecture)
        {
            PlatformTexture tupian = architecture.Texture;
            if (Session.Current.Scenario.ScenarioMap.UseSimpleArchImages)
            {
                return tupian;
            }

            if (architecture.Kind.ID == 2)
            {
                if (architecture.JianzhuGuimo == 1)
                {
                    tupian = architecture.Texture;
                }
                else if (architecture.JianzhuGuimo == 3)//小关
                {
                    if (architecture.ArchitectureArea.Area[0].X == architecture.ArchitectureArea.Area[1].X) //竖关
                    {

                        tupian = Session.MainGame.mainGameScreen.Textures.guandetupian[1];
                    }
                    else //横关
                    {

                        tupian = Session.MainGame.mainGameScreen.Textures.guandetupian[0];
                    }
                }
                else if (architecture.JianzhuGuimo == 5)//大关
                {
                    if (architecture.ArchitectureArea.Area[0].X == architecture.ArchitectureArea.Area[1].X) //竖关
                    {
                        tupian = Session.MainGame.mainGameScreen.Textures.guandetupian[2];

                    }
                    else //横关
                    {
                        tupian = architecture.Texture;

                    }

                }

            }
            else
            {
                //try
                //{
                if (architecture.JianzhuGuimo == 13 && Session.MainGame.mainGameScreen.Textures.largeCityImg.ContainsKey(architecture.Kind.ID))
                {
                    tupian = Session.MainGame.mainGameScreen.Textures.largeCityImg[architecture.Kind.ID];
                }
                else if (architecture.JianzhuGuimo == 5 && Session.MainGame.mainGameScreen.Textures.mediumCityImg.ContainsKey(architecture.Kind.ID))
                {
                    tupian = Session.MainGame.mainGameScreen.Textures.mediumCityImg[architecture.Kind.ID];
                }
                else
                {
                    tupian = architecture.Texture;
                }
                //}
                //catch (KeyNotFoundException)
                //{
                //    tupian = architecture.Texture;
                //}
            }

            return tupian;
        }

        public void Initialize()
        {
        }
    } 

}
