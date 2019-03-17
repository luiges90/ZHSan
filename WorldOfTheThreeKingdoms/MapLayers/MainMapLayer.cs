using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;
using GameObjects.MapDetail;
using GameFreeText;
using System.Threading;
using System.IO;
using Platforms;
using GameManager;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers
{
    public class MainMapLayer
    {
        //private Texture2D BackgroundMap;
        //private beijingtupian beijingtupian = new beijingtupian();
        //private Texture2D BackgroundMap1;
        //private Texture2D BackgroundMap2;
        //private Texture2D BackgroundMap3;
        //private Texture2D BackgroundMap4;

        bool drawBlackWhenNoneTexture = true;

        public List<Tile> DisplayingTiles = new List<Tile>();

        public List<MapTile> DisplayingMapTiles = new List<MapTile>();
        
        private int leftEdge = 0;

        private List<int> TerrainList = new List<int>();
        public Tile[,] Tiles;
        public MapTile[,] MapTiles;
        public int tileWidthMax = 100;
        public int tileWidthMin = 30;
        private int topEdge = 0;
        private Rectangle jianzhujuxing = new Rectangle();
        private Rectangle qizijuxing = new Rectangle();
        internal bool xianshidituxiaokuai = true;
        
        private void CheckMapTileTexture(MapTile maptile)
        {
            if (maptile.TileTexture == null)
            {
                try
                {
                    //try
                    //{
                        maptile.TileTexture = Platform.Current.LoadTexture("Content/Textures/Resources/ditu/" + Session.Current.Scenario.ScenarioMap.MapName + "/" + maptile.number + ".jpg", false);
                    //}
                    //catch (FileNotFoundException)
                    //{
                    //    maptile.TileTexture = CacheManager.GetTempTexture("Content/Textures/Resources/ditu/" + Session.Current.Scenario.ScenarioMap.MapName + "/" + maptile.number + ".png");
                    //}
                }
                catch (Exception)
                {
                    maptile.TileTexture = new Texture2D(Platform.GraphicsDevice, 1, 1);
                    //try
                    //{
                    //    this.freeTilesMemory(false);
                    //    try
                    //    {
                    //        maptile.TileTexture = CacheManager.GetTempTexture("Content/Textures/Resources/ditu/" + Session.Current.Scenario.ScenarioMap.MapName + "/" + maptile.number + ".jpg");
                    //    }
                    //    catch (FileNotFoundException)
                    //    {
                    //        maptile.TileTexture = CacheManager.GetTempTexture("Content/Textures/Resources/ditu/" + Session.Current.Scenario.ScenarioMap.MapName + "/" + maptile.number + ".png");
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    maptile.TileTexture = new Texture2D(this.device, 1, 1);
                    //}
                }
            }
        }

        private void CheckTileTexture(Tile tile, out List<PlatformTexture> decorativeTextures)
        {
            decorativeTextures = null;
            return;
#pragma warning disable CS0162 // Unreachable code detected
            TerrainDetail terrainDetailByPositionNoCheck = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(tile.Position);
#pragma warning restore CS0162 // Unreachable code detected
            if (terrainDetailByPositionNoCheck.Textures != null && terrainDetailByPositionNoCheck.Textures.BasicTextures.Count != 0)
            {
                int i;
                List<int> list;
                int num12;
                for (i = 0; i < this.TerrainList.Count; i++)
                {
                    this.TerrainList[i] = 0;
                }
                TerrainDirection direction = TerrainDirection.None;
                int num2 = 0;

                Point position = new Point(tile.Position.X - 1, tile.Position.Y);
                int leftId = 0;
                TerrainDetail leftDetail = null;
                if (!Session.Current.Scenario.PositionOutOfRange(position))
                {
                    leftDetail = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(position);
                    leftId = leftDetail.ID;
                    //(list = this.TerrainList)[num12 = Session.Current.Scenario.ScenarioMap.MapData[position.X, position.Y]] = list[num12] + 1;
                }
                Point point2 = new Point(tile.Position.X - 1, tile.Position.Y - 1);
                int topLeftId = 0;
                if (!Session.Current.Scenario.PositionOutOfRange(point2))
                {
                    topLeftId = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(point2).ID;
                    //(list = this.TerrainList)[num12 = Session.Current.Scenario.ScenarioMap.MapData[point2.X, point2.Y]] = list[num12] + 1;
                }
                Point point3 = new Point(tile.Position.X, tile.Position.Y - 1);
                int topId = 0;
                TerrainDetail top = null;
                if (!Session.Current.Scenario.PositionOutOfRange(point3))
                {
                    top = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(point3);
                    topId = top.ID;
                }
                Point point4 = new Point(tile.Position.X + 1, tile.Position.Y - 1);
                int topRightId = 0;
                if (!Session.Current.Scenario.PositionOutOfRange(point4))
                {
                    topRightId = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(point4).ID;
                }
                Point point5 = new Point(tile.Position.X + 1, tile.Position.Y);
                int rightId = 0;
                TerrainDetail right = null;
                if (!Session.Current.Scenario.PositionOutOfRange(point5))
                {
                    right = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(point5);
                    rightId = right.ID;
                }
                Point point6 = new Point(tile.Position.X + 1, tile.Position.Y + 1);
                int bottomRightId = 0;
                if (!Session.Current.Scenario.PositionOutOfRange(point6))
                {
                    bottomRightId = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(point6).ID;
                }
                Point point7 = new Point(tile.Position.X, tile.Position.Y + 1);
                int bottomId = 0;
                TerrainDetail bottom = null;
                if (!Session.Current.Scenario.PositionOutOfRange(point7))
                {
                    bottom = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(point7);
                    bottomId = bottom.ID;
                }
                Point point8 = new Point(tile.Position.X - 1, tile.Position.Y + 1);
                int bottomLeftId = 0;
                if (!Session.Current.Scenario.PositionOutOfRange(point8))
                {
                    bottomLeftId = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(point8).ID;
                }

                int equalTerrainCnt = 0;
                if ((leftId > 0) && (leftId == terrainDetailByPositionNoCheck.ID))
                {
                    equalTerrainCnt++;
                }
                if ((topId > 0) && (topId == terrainDetailByPositionNoCheck.ID))
                {
                    equalTerrainCnt++;
                }
                if ((rightId > 0) && (rightId == terrainDetailByPositionNoCheck.ID))
                {
                    equalTerrainCnt++;
                }
                if ((bottomId > 0) && (bottomId == terrainDetailByPositionNoCheck.ID))
                {
                    equalTerrainCnt++;
                }

                if (equalTerrainCnt < 4)
                {
                    if (top != null)
                    {
                        (list = this.TerrainList)[num12 = Session.Current.Scenario.ScenarioMap.MapData[point3.X, point3.Y]] = list[num12] + 1;
                        for (i = 0; i < this.TerrainList.Count; i++)
                        {
                            if ((i != terrainDetailByPositionNoCheck.ID) && ((((this.TerrainList[i] >= 2) && (leftId == i)) && (topLeftId != terrainDetailByPositionNoCheck.ID)) && (topId == i)))
                            {
                                direction = TerrainDirection.TopLeft;
                                num2 = i;
                                break;
                            }
                        }
                    }
                    if (topRightId > 0)
                    {
                        (list = this.TerrainList)[num12 = Session.Current.Scenario.ScenarioMap.MapData[point4.X, point4.Y]] = list[num12] + 1;
                    }
                    if (rightId > 0)
                    {
                        (list = this.TerrainList)[num12 = Session.Current.Scenario.ScenarioMap.MapData[point5.X, point5.Y]] = list[num12] + 1;
                        for (i = 0; i < this.TerrainList.Count; i++)
                        {
                            if (i != terrainDetailByPositionNoCheck.ID)
                            {
                                if (((direction == TerrainDirection.None) && (this.TerrainList[i] >= 2)) && (((topId == i) && (topRightId != terrainDetailByPositionNoCheck.ID)) && (rightId == i)))
                                {
                                    direction = TerrainDirection.TopRight;
                                    num2 = i;
                                }
                                if (this.TerrainList[i] >= 3)
                                {
                                    if ((((leftId == i) && (topId == i)) && ((rightId == i) && (topRightId != terrainDetailByPositionNoCheck.ID))) && (topLeftId != terrainDetailByPositionNoCheck.ID))
                                    {
                                        direction = TerrainDirection.Top;
                                        num2 = i;
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    if (bottomRightId > 0)
                    {
                        (list = this.TerrainList)[num12 = Session.Current.Scenario.ScenarioMap.MapData[point6.X, point6.Y]] = list[num12] + 1;
                    }
                    if (bottomId > 0)
                    {
                        (list = this.TerrainList)[num12 = Session.Current.Scenario.ScenarioMap.MapData[point7.X, point7.Y]] = list[num12] + 1;
                        for (i = 0; i < this.TerrainList.Count; i++)
                        {
                            if (i != terrainDetailByPositionNoCheck.ID)
                            {
                                if (((direction == TerrainDirection.None) && (this.TerrainList[i] >= 2)) && (((rightId == i) && (bottomRightId != terrainDetailByPositionNoCheck.ID)) && (bottomId == i)))
                                {
                                    direction = TerrainDirection.BottomRight;
                                    num2 = i;
                                }
                                if ((this.TerrainList[i] >= 3) && ((((topId == i) && (rightId == i)) && ((bottomId == i) && (topRightId != terrainDetailByPositionNoCheck.ID))) && (bottomRightId != terrainDetailByPositionNoCheck.ID)))
                                {
                                    direction = TerrainDirection.Right;
                                    num2 = i;
                                }
                            }
                        }
                    }
                    if (bottomLeftId > 0)
                    {
                        (list = this.TerrainList)[num12 = Session.Current.Scenario.ScenarioMap.MapData[point8.X, point8.Y]] = list[num12] + 1;
                    }
                    if (leftId > 0)
                    {
                        for (i = 0; i < this.TerrainList.Count; i++)
                        {
                            if (i != terrainDetailByPositionNoCheck.ID)
                            {
                                if (((direction == TerrainDirection.None) && (this.TerrainList[i] >= 2)) && (((bottomId == i) && (bottomLeftId != terrainDetailByPositionNoCheck.ID)) && (leftId == i)))
                                {
                                    direction = TerrainDirection.BottomLeft;
                                    num2 = i;
                                }
                                if ((this.TerrainList[i] >= 3) && ((((rightId == i) && (bottomId == i)) && ((leftId == i) && (bottomRightId != terrainDetailByPositionNoCheck.ID))) && (bottomLeftId != terrainDetailByPositionNoCheck.ID)))
                                {
                                    direction = TerrainDirection.Bottom;
                                    num2 = i;
                                }
                                if (((((this.TerrainList[i] >= 4) && (leftId == i)) && ((topId == i) && (rightId == i))) && (bottomId == i)) && ((((topLeftId != terrainDetailByPositionNoCheck.ID) && (topRightId != terrainDetailByPositionNoCheck.ID)) && ((bottomRightId != terrainDetailByPositionNoCheck.ID) && (bottomLeftId != terrainDetailByPositionNoCheck.ID))) || (this.TerrainList[i] >= 7)))
                                {
                                    direction = TerrainDirection.Centre;
                                    num2 = i;
                                    break;
                                }
                            }
                        }
                    }
                    if ((((direction != TerrainDirection.Centre) && (direction != TerrainDirection.Top)) && ((direction != TerrainDirection.Right) && (direction != TerrainDirection.Bottom))) && (topId > 0))
                    {
                        for (i = 0; i < this.TerrainList.Count; i++)
                        {
                            if (((i != terrainDetailByPositionNoCheck.ID) && (this.TerrainList[i] >= 3)) && ((((bottomId == i) && (leftId == i)) && ((topId == i) && (bottomLeftId != terrainDetailByPositionNoCheck.ID))) && (topLeftId != terrainDetailByPositionNoCheck.ID)))
                            {
                                direction = TerrainDirection.Left;
                                num2 = i;
                            }
                        }
                    }
                    decorativeTextures = new List<PlatformTexture>();
                    switch (direction)
                    {
                        case TerrainDirection.Top:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(top.Textures.TopTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            if ((((bottom != null) && (bottom.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomRightId != terrainDetailByPositionNoCheck.ID) && (bottomLeftId != terrainDetailByPositionNoCheck.ID))) && (bottom.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(bottom.Textures.BottomEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomEdgeTextures.Count]);
                            }
                            return;

                        case TerrainDirection.Left:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(leftDetail.Textures.LeftTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.LeftTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            if ((((right != null) && (right.ID != terrainDetailByPositionNoCheck.ID)) && ((topRightId != terrainDetailByPositionNoCheck.ID) && (bottomRightId != terrainDetailByPositionNoCheck.ID))) && (right.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(right.Textures.RightEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.RightEdgeTextures.Count]);
                            }
                            return;

                        case TerrainDirection.Right:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(right.Textures.RightTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.RightTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            if ((((leftDetail != null) && (leftDetail.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomLeftId != terrainDetailByPositionNoCheck.ID) && (topLeftId != terrainDetailByPositionNoCheck.ID))) && (leftDetail.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(leftDetail.Textures.LeftEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.LeftEdgeTextures.Count]);
                            }
                            return;

                        case TerrainDirection.Bottom:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(bottom.Textures.BottomTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            if ((((top != null) && (top.ID != terrainDetailByPositionNoCheck.ID)) && ((topLeftId != terrainDetailByPositionNoCheck.ID) && (topRightId != terrainDetailByPositionNoCheck.ID))) && (top.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(top.Textures.TopEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopEdgeTextures.Count]);
                            }
                            return;

                        case TerrainDirection.TopLeft:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(leftDetail.Textures.TopLeftCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.TopLeftCornerTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            if ((((right == null) || (terrainDetailByPositionNoCheck.ID == bottomRightId)) || (rightId != bottomId)) || (terrainDetailByPositionNoCheck.ID == rightId))
                            {
                                if ((((right != null) && (right.ID != terrainDetailByPositionNoCheck.ID)) && ((topRightId != terrainDetailByPositionNoCheck.ID) && (bottomRightId != terrainDetailByPositionNoCheck.ID))) && (right.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                                {
                                    decorativeTextures.Add(right.Textures.RightEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.RightEdgeTextures.Count]);
                                }
                                if ((((bottom != null) && (bottom.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomRightId != terrainDetailByPositionNoCheck.ID) && (bottomLeftId != terrainDetailByPositionNoCheck.ID))) && (bottom.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                                {
                                    decorativeTextures.Add(bottom.Textures.BottomEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomEdgeTextures.Count]);
                                }
                                return;
                            }
                            decorativeTextures.Add(right.Textures.BottomRightCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.BottomRightCornerTextures.Count]);
                            return;

                        case TerrainDirection.TopRight:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(top.Textures.TopRightCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopRightCornerTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            if ((((bottom == null) || (terrainDetailByPositionNoCheck.ID == bottomLeftId)) || (bottomId != leftId)) || (terrainDetailByPositionNoCheck.ID == bottomId))
                            {
                                if ((((leftDetail != null) && (leftDetail.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomLeftId != terrainDetailByPositionNoCheck.ID) && (topLeftId != terrainDetailByPositionNoCheck.ID))) && (leftDetail.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                                {
                                    decorativeTextures.Add(leftDetail.Textures.LeftEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.LeftEdgeTextures.Count]);
                                }
                                if ((((bottom != null) && (bottom.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomRightId != terrainDetailByPositionNoCheck.ID) && (bottomLeftId != terrainDetailByPositionNoCheck.ID))) && (bottom.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                                {
                                    decorativeTextures.Add(bottom.Textures.BottomEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomEdgeTextures.Count]);
                                }
                                return;
                            }
                            decorativeTextures.Add(bottom.Textures.BottomLeftCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomLeftCornerTextures.Count]);
                            return;

                        case TerrainDirection.BottomLeft:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(bottom.Textures.BottomLeftCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomLeftCornerTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            if ((((top == null) || (terrainDetailByPositionNoCheck.ID == topRightId)) || (topId != rightId)) || (terrainDetailByPositionNoCheck.ID == topId))
                            {
                                if ((((top != null) && (top.ID != terrainDetailByPositionNoCheck.ID)) && ((topLeftId != terrainDetailByPositionNoCheck.ID) && (topRightId != terrainDetailByPositionNoCheck.ID))) && (top.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                                {
                                    decorativeTextures.Add(top.Textures.TopEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopEdgeTextures.Count]);
                                }
                                if ((((right != null) && (right.ID != terrainDetailByPositionNoCheck.ID)) && ((topRightId != terrainDetailByPositionNoCheck.ID) && (bottomRightId != terrainDetailByPositionNoCheck.ID))) && (right.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                                {
                                    decorativeTextures.Add(right.Textures.RightEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.RightEdgeTextures.Count]);
                                }
                                return;
                            }
                            decorativeTextures.Add(top.Textures.TopRightCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopRightCornerTextures.Count]);
                            return;

                        case TerrainDirection.BottomRight:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(right.Textures.BottomRightCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.BottomRightCornerTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            if ((((leftDetail == null) || (terrainDetailByPositionNoCheck.ID == topLeftId)) || (leftId != topId)) || (terrainDetailByPositionNoCheck.ID == leftId))
                            {
                                if ((((leftDetail != null) && (leftDetail.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomLeftId != terrainDetailByPositionNoCheck.ID) && (topLeftId != terrainDetailByPositionNoCheck.ID))) && (leftDetail.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                                {
                                    decorativeTextures.Add(leftDetail.Textures.LeftEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.LeftEdgeTextures.Count]);
                                }
                                if ((((top != null) && (top.ID != terrainDetailByPositionNoCheck.ID)) && ((topLeftId != terrainDetailByPositionNoCheck.ID) && (topRightId != terrainDetailByPositionNoCheck.ID))) && (top.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                                {
                                    decorativeTextures.Add(top.Textures.TopEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopEdgeTextures.Count]);
                                }
                                return;
                            }
                            decorativeTextures.Add(leftDetail.Textures.TopLeftCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.TopLeftCornerTextures.Count]);
                            return;

                        case TerrainDirection.Centre:
#pragma warning disable CS0162 // Unreachable code detected
                            decorativeTextures.Add(leftDetail.Textures.CentreTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.CentreTextures.Count]);
#pragma warning restore CS0162 // Unreachable code detected
                            return;

                        case TerrainDirection.None:
#pragma warning disable CS0162 // Unreachable code detected
                            if ((((leftDetail != null) && (leftDetail.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomLeftId != terrainDetailByPositionNoCheck.ID) && (topLeftId != terrainDetailByPositionNoCheck.ID))) && (leftDetail.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
#pragma warning restore CS0162 // Unreachable code detected
                            {
                                decorativeTextures.Add(leftDetail.Textures.LeftEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.LeftEdgeTextures.Count]);
                            }
                            if ((((top != null) && (top.ID != terrainDetailByPositionNoCheck.ID)) && ((topLeftId != terrainDetailByPositionNoCheck.ID) && (topRightId != terrainDetailByPositionNoCheck.ID))) && (top.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(top.Textures.TopEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopEdgeTextures.Count]);
                            }
                            if ((((right != null) && (right.ID != terrainDetailByPositionNoCheck.ID)) && ((topRightId != terrainDetailByPositionNoCheck.ID) && (bottomRightId != terrainDetailByPositionNoCheck.ID))) && (right.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(right.Textures.RightEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.RightEdgeTextures.Count]);
                            }
                            if ((((bottom != null) && (bottom.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomRightId != terrainDetailByPositionNoCheck.ID) && (bottomLeftId != terrainDetailByPositionNoCheck.ID))) && (bottom.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(bottom.Textures.BottomEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomEdgeTextures.Count]);
                            }
                            return;
                    }
                }
            }
        }

        public void freeTilesMemory(bool gc = true, bool clearAll = false)
        {
            if (this.MapTiles != null)
            {
                var nums = GetCurrentViewMapTileNumsAll();

                foreach (MapTile maptile in this.MapTiles)
                {
                    if (nums.Contains(int.Parse(maptile.number)) && !clearAll)
                    {
                        continue;
                    }
                    //try
                    //{
                    if (maptile.TileTexture != null)
                    {
                        maptile.TileTexture.Dispose();
                        maptile.TileTexture = null;
                    }
                    //}
                    //catch (Exception ex)
                    //{
                    //}                    
                }
            }

            //臨時材質到達一定數量，也予清理
            //if (CacheManager.TextureTempDics.Count >= 30)
            //{
            CacheManager.Clear(CacheType.Page);
            //}

            if (gc)
            {
                GC.Collect();
            }
        }

        private int[] GetCurrentViewMapTileNums1()
        {
            lock (this.DisplayingMapTiles)
            {
                var disNums = this.DisplayingMapTiles.Select(di => int.Parse(di.number)).ToArray();
                return disNums;
            }
        }

        private int[] GetCurrentViewMapTileNums2()
        {
            var disNums = GetCurrentViewMapTileNums1();
            var nums = new int[] { -1, +1, -30, +30, -31, -29, +29, +31 };
            var expandNums = disNums.SelectMany(nu => nums.Select(num => num + nu)).Where(num => 0 <= num && num <= 899 && !disNums.Contains(num)).ToArray();
            return expandNums.Distinct().ToArray();
        }

        private int[] GetCurrentViewMapTileNumsAll()
        {
            var disNums = GetCurrentViewMapTileNums1();
            var nums = new int[] { -1, +1, -30, +30, -31, -29, +29, +31 };
            var exceptNums = disNums.SelectMany(nu => nums.Select(num => num + nu)).Where(num => 0 <= num && num <= 899).ToArray();
            return disNums.Union(exceptNums).Distinct().ToArray();
        }

        public void StopThreads()
        {
            freeTilesMemory();
            if (MapThread1 != null)
            {
                MapThread1.Abort();
                MapThread1 = null;
            }
            if (MapThread2 != null)
            {
                MapThread2.Abort();
                MapThread2 = null;
            }            
        }

        PlatformTask MapThread1;
        PlatformTask MapThread2;

        private void ProcessMapTileTextureSync()
        {
            if (MapThread1 == null)
            {
                MapThread1 = new PlatformTask(() =>
                {
                    while (true)
                    {
                        if (MapThread1 == null || MapThread1.IsStop)
                        {
                            break;
                        }

                        try
                        {
                            MapTile mapTile = null;

                            //var tileNums1 = GetCurrentViewMapTileNums1();                            

                            lock (this.DisplayingMapTiles)
                            {
                                var maps = this.DisplayingMapTiles.Where(ma => ma != null && ma.TileTexture == null).ToArray();  // tileNums1.Select(num => this.MapTiles[num % 30, num / 30]).Where(ma => ma.TileTexture == null).ToArray();                            
                                if (maps != null && maps.Length > 0)
                                {
                                    mapTile = maps.FirstOrDefault();                                    
                                }
                            }

                            if (mapTile != null)
                            {
                                CheckMapTileTexture(mapTile);
                            }

                            Platform.Sleep(30);
                        }
                        //catch (ThreadAbortException)
                        //{
                        //    break;
                        //}
                        catch (Exception e)
                        {
                            //DateTime dt = System.DateTime.Now;
                            //String dateSuffix = "_" + dt.Year + "_" + dt.Month + "_" + dt.Day + "_" + dt.Hour + "h" + dt.Minute;
                            //String logPath = "CrashLog" + dateSuffix + ".log";
                            //StreamWriter sw = new StreamWriter(new FileStream(logPath, FileMode.Create));

                            //sw.WriteLine("==================== Message ====================");
                            //sw.WriteLine(e.Message);
                            //sw.WriteLine("=================== StackTrace ==================");
                            //sw.WriteLine(e.StackTrace);

                            //sw.Close();

                            Platform.Sleep(1000);
                        }
                    }
                }
                );
                MapThread1.Start();
            }
            if (MapThread2 == null)
            {
                MapThread2 = new PlatformTask(() =>
                {
                    while (true)
                    {
                        if (MapThread2 == null || MapThread2.IsStop)
                        {
                            break;
                        }

                        try
                        {
                            MapTile mapTile = null;

                            lock (this.DisplayingMapTiles)
                            {
                                var maps0 = this.DisplayingMapTiles.Where(ma => ma != null && ma.TileTexture == null).ToArray();

                                //如果当前地图未绘制完，暂停绘制周边
                                if (maps0 != null && maps0.Length > 0)
                                {
                                    mapTile = maps0.FirstOrDefault();
                                }
                            }

                            if (mapTile != null)
                            {
                                //CheckMapTileTexture(mapTile);
                                Platform.Sleep(80);
                                continue;
                            }

                            var tileNums2 = GetCurrentViewMapTileNums2();

                            var maps = tileNums2.Select(num => this.MapTiles[num % 30, num / 30]).Where(ma => ma != null && ma.TileTexture == null).ToArray();

                            if (maps != null && maps.Length > 0)
                            {
                                mapTile = maps.FirstOrDefault();
                                CheckMapTileTexture(mapTile);
                            }

                            Platform.Sleep(50);
                        }
                        catch   (Exception e)
                        {
                            //DateTime dt = System.DateTime.Now;
                            //String dateSuffix = "_" + dt.Year + "_" + dt.Month + "_" + dt.Day + "_" + dt.Hour + "h" + dt.Minute;
                            //String logPath = "CrashLog" + dateSuffix + ".log";
                            //StreamWriter sw = new StreamWriter(new FileStream(logPath, FileMode.Create));

                            //sw.WriteLine("==================== Message ====================");
                            //sw.WriteLine(e.Message);
                            //sw.WriteLine("=================== StackTrace ==================");
                            //sw.WriteLine(e.StackTrace);

                            //sw.Close();

                            Platform.Sleep(1000);
                        }
                        //catch (ThreadAbortException)
                        //{
                        //    break;
                        //}
                    }
                });
                MapThread2.Start();
            }

        }


        public void Draw(Point viewportSize)
        {
            var spriteBatch = Session.Current.SpriteBatch;
            if (spriteBatch != null)
            {

                if (Session.Current.Scenario.ScenarioMap.MapName != null)
                {
                    ProcessMapTileTextureSync();

                    foreach (MapTile maptile in this.DisplayingMapTiles)
                    {
                        Rectangle? sourceRectangle = null;

                        if (!drawBlackWhenNoneTexture)
                        {
                            this.CheckMapTileTexture(maptile);
                        }

                        if (maptile != null && maptile.TileTexture != null)
                        {
                            spriteBatch.Draw(maptile.TileTexture, maptile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                        }

                        List<Texture2D> decorativeTextures = null;
                        //this.CheckTileTexture(maptile, out decorativeTextures);
                        if (decorativeTextures != null)
                        {
                            foreach (Texture2D textured in decorativeTextures)
                            {
                                sourceRectangle = null;
                                spriteBatch.Draw(textured, maptile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8998f);
                            }
                        }
                    }

                    ResetDisplayingTiles(Session.MainGame.mainGameScreen);

                    //數量到達一定之後，清理緩存材質
                    int mapNum = 0;
                    foreach (var map in MapTiles)
                    {
                        if (map.TileTexture != null)
                        {
                            mapNum++;
                        }
                    }
                    if (mapNum >= 100)
                    {
                        freeTilesMemory(false);
                    }

                    if (Session.MainGame.mainGameScreen.editMode)
                    {
                        foreach (Tile tile in this.DisplayingTiles)
                        {
                            List<PlatformTexture> decorativeTextures = null;
                            this.CheckTileTexture(tile, out decorativeTextures);
                            //Rectangle? sourceRectangle = null;
                            if (this.xianshidituxiaokuai && Session.Current.Scenario.ScenarioMap.MapData[tile.Position.X, tile.Position.Y] != 0) //未知地形显示为透明，以方便地形编辑
                            {
                                //CacheManager.Draw(tile.TileTexture, tile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8998f);
                                CacheManager.DrawAvatar(tile.TileTexture.Name, tile.Destination, Color.White, false, true, TextureShape.None, null, 0.8998f);
                            }

                        }
                    }

                }
                else
                {
                    foreach (Tile tile in this.DisplayingTiles)
                    {
                        List<PlatformTexture> decorativeTextures = null;
                        this.CheckTileTexture(tile, out decorativeTextures);
                        //Rectangle? sourceRectangle = null;

                        //CacheManager.Draw(tile.TileTexture, tile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8998f);

                        CacheManager.DrawAvatar(tile.TileTexture.Name, tile.Destination, Color.White, false, true, TextureShape.None, null, 0.8998f);

                        /*if (decorativeTextures != null)
                        {
                            foreach (Texture2D textured in decorativeTextures)
                            {
                                sourceRectangle = null;
                                CacheManager.Draw(textured, tile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8998f);
                            }
                        }*/
                    }

                }

                if (Session.GlobalVariables.ShowGrid)
                {
                    foreach (Tile tile in this.DisplayingTiles)
                    {
                        if (Session.MainGame.mainGameScreen.editMode)
                        {
                            CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.EditModeGrid, tile.Destination, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.81f);
                        }
                        else
                        {
                            if (Session.Current.Scenario.ScenarioMap.MapData[tile.Position.X, tile.Position.Y] != 0 && Session.Current.Scenario.ScenarioMap.MapData[tile.Position.X, tile.Position.Y] != 4 && Session.Current.Scenario.ScenarioMap.MapData[tile.Position.X, tile.Position.Y] != 7)
                            {
                                CacheManager.Draw(Session.MainGame.mainGameScreen.Textures.wanggetupian, tile.Destination, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.81f);
                            }
                        }
                    }
                }

            }
        }

        public Point GetCurrentScreenCenter(Point viewportSize)
        {
            return new Point(((viewportSize.X / 2) - this.LeftEdge) / this.TileWidth, ((viewportSize.Y / 2) - this.TopEdge) / this.TileHeight);
        }

        public Rectangle GetDestination(Point position)
        {
            return this.Tiles[position.X, position.Y].Destination;
        }
        public Rectangle huoqujianzhujuxing(Point position, Architecture jianzhu)
        {
            jianzhujuxing = this.Tiles[position.X, position.Y].Destination;//此句是?了防止出?。

            int guimo = jianzhu.JianzhuGuimo;

            if (jianzhu.Kind.ID != 2 && jianzhu.Kind.ID != 3)
            {
                if (Session.Current.Scenario.ScenarioMap.UseSimpleArchImages)
                {
                    jianzhujuxing.X = (position.X) * this.TileWidth + this.LeftEdge;
                    jianzhujuxing.Y = (position.Y) * this.TileHeight + this.TopEdge;

                    jianzhujuxing.Width = this.TileWidth;
                    jianzhujuxing.Height = this.TileHeight;
                }
                else
                {
                    if (jianzhu.Kind.ID != 1 && guimo == 1)
                    {
                        jianzhujuxing.X = position.X * this.TileWidth + this.LeftEdge;
                        jianzhujuxing.Y = position.Y * this.TileHeight + this.TopEdge;

                        jianzhujuxing.Width = this.TileWidth;
                        jianzhujuxing.Height = this.TileHeight;
                    }
                    else if (guimo == 5 || (jianzhu.Kind.ID == 1 && guimo == 1))
                    {
                        jianzhujuxing.X = (position.X - 1) * this.TileWidth + this.LeftEdge;
                        jianzhujuxing.Y = (position.Y - 1) * this.TileHeight + this.TopEdge;

                        jianzhujuxing.Width = this.TileWidth * 3;
                        jianzhujuxing.Height = this.TileHeight * 3;
                    }
                    else if (guimo == 13)
                    {
                        jianzhujuxing.X = (position.X - 2) * this.TileWidth + this.LeftEdge;
                        jianzhujuxing.Y = (position.Y - 2) * this.TileHeight + this.TopEdge;

                        jianzhujuxing.Width = this.TileWidth * 5;
                        jianzhujuxing.Height = this.TileHeight * 5;
                    }
                }
            }
            else if (jianzhu.Kind.ID == 2)
            {
                if (guimo == 1 || Session.Current.Scenario.ScenarioMap.UseSimpleArchImages)
                {
                    return jianzhujuxing;
                }
                else if (guimo == 5)
                {
                    if (jianzhu.ArchitectureArea.Area[0].X == jianzhu.ArchitectureArea.Area[1].X) //??
                    {
                        jianzhujuxing.X = (position.X - 2) * this.TileWidth + this.LeftEdge;
                        jianzhujuxing.Y = (position.Y - 3) * this.TileHeight + this.TopEdge;

                        jianzhujuxing.Width = this.TileWidth * 5;
                        jianzhujuxing.Height = this.TileHeight * 7;

                    }
                    else
                    {
                        jianzhujuxing.X = (position.X - 4) * this.TileWidth + this.LeftEdge;
                        jianzhujuxing.Y = (position.Y - 1) * this.TileHeight + this.TopEdge;

                        jianzhujuxing.Width = this.TileWidth * 9;
                        jianzhujuxing.Height = this.TileHeight * 3;
                    }
                }
                else if (guimo == 3)
                {
                    if (jianzhu.ArchitectureArea.Area[0].X == jianzhu.ArchitectureArea.Area[1].X) //??
                    {
                        jianzhujuxing.X = (position.X - 2) * this.TileWidth + this.LeftEdge;
                        jianzhujuxing.Y = (position.Y - 2) * this.TileHeight + this.TopEdge;

                        jianzhujuxing.Width = this.TileWidth * 5;
                        jianzhujuxing.Height = this.TileHeight * 5;
                    }
                    else
                    {
                        jianzhujuxing.X = (position.X - 2) * this.TileWidth + this.LeftEdge;
                        jianzhujuxing.Y = (position.Y - 1) * this.TileHeight + this.TopEdge;

                        jianzhujuxing.Width = this.TileWidth * 5;
                        jianzhujuxing.Height = this.TileHeight * 3;
                    }
                }
            }
            return jianzhujuxing;
        }
        public Rectangle huoquqizijuxing(Point position)
        {
            qizijuxing = this.Tiles[position.X, position.Y].Destination;
            qizijuxing.X += (int)(qizijuxing.Width * 0.2);
            qizijuxing.Y += (int)(qizijuxing.Height * 0.54);
            qizijuxing.Width = (int)(qizijuxing.Width * 0.5);
            qizijuxing.Height = (int)(qizijuxing.Height * 0.4);
            return qizijuxing;
        }

        internal Point GetCenterCoordinate(Point point)
        {
            Point p = new Point();
            p.X = this.leftEdge + (point.X * this.TileWidth) + this.TileWidth / 2;
            p.Y = this.topEdge + (point.Y * this.TileHeight) + this.TileHeight / 2;
            return p;
        }

        public Rectangle GetHalfDestination(Point position)
        {
            return new Rectangle(this.Tiles[position.X, position.Y].Destination.X + (this.Tiles[position.X, position.Y].Destination.Width / 4), this.Tiles[position.X, position.Y].Destination.Y + (this.Tiles[position.X, position.Y].Destination.Height / 4), this.Tiles[position.X, position.Y].Destination.Width / 2, this.Tiles[position.X, position.Y].Destination.Height / 2);
        }

        public string GetTerrainNameByPosition(Point position)
        {
            return Session.Current.Scenario.GetTerrainNameByPosition(position);
        }

        public Rectangle GetThreeFourthsDestination(Point position)
        {
            return new Rectangle(this.Tiles[position.X, position.Y].Destination.X + (this.Tiles[position.X, position.Y].Destination.Width / 8), this.Tiles[position.X, position.Y].Destination.Y + (this.Tiles[position.X, position.Y].Destination.Height / 8), (this.Tiles[position.X, position.Y].Destination.Width * 3) / 4, (this.Tiles[position.X, position.Y].Destination.Height * 3) / 4);
        }

        public Point GetTopCenterPoint(Point position)
        {
            return new Point(((position.X * this.TileWidth) + (this.TileWidth / 2)) + this.leftEdge, (position.Y * this.TileHeight) + this.topEdge);
        }

        public void Initialize()
        {
            this.TerrainList.Clear();
            for (int i = 0; i < Enum.GetValues(typeof(TerrainKind)).Length; i++)
            {
                this.TerrainList.Add(0);
            }
        }

        public void PrepareMap()
        {
            this.Tiles = null;
            this.Tiles = new Tile[Session.Current.Scenario.ScenarioMap.MapDimensions.X, Session.Current.Scenario.ScenarioMap.MapDimensions.Y];
            for (int i = 0; i < Session.Current.Scenario.ScenarioMap.MapDimensions.X; i++)
            {
                for (int j = 0; j < Session.Current.Scenario.ScenarioMap.MapDimensions.Y; j++)
                {
                    this.Tiles[i, j] = new Tile();
                    this.Tiles[i, j].Position = new Point(i, j);
                    TerrainDetail terrainDetailByPositionNoCheck = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(this.Tiles[i, j].Position);

                    if (terrainDetailByPositionNoCheck != null)
                    {
                        if (terrainDetailByPositionNoCheck.Textures.BasicTextures.Count > 0)
                        {
                            this.Tiles[i, j].TileTexture = terrainDetailByPositionNoCheck.Textures.BasicTextures[((i * 7) + (j * 11)) % terrainDetailByPositionNoCheck.Textures.BasicTextures.Count];
                        }
                    }
                }
            }

            this.MapTiles = null;
            this.MapTiles = new MapTile[Session.Current.Scenario.ScenarioMap.NumberOfTiles, Session.Current.Scenario.ScenarioMap.NumberOfTiles];
            for (int i = 0; i < Session.Current.Scenario.ScenarioMap.NumberOfTiles; i++)
            {
                for (int j = 0; j < Session.Current.Scenario.ScenarioMap.NumberOfTiles; j++)
                {
                    this.MapTiles[i, j] = new MapTile();
                    this.MapTiles[i, j].Position = new Point(i, j);
                    this.MapTiles[i, j].number = (i + j * Session.Current.Scenario.ScenarioMap.NumberOfTiles).ToString();
                    /*TerrainDetail terrainDetailByPositionNoCheck = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(this.Tiles[i, j].Position);
                    
                    if (terrainDetailByPositionNoCheck != null)
                    {
                        if (terrainDetailByPositionNoCheck.Textures.BasicTextures.Count > 0)
                        {
                            this.Tiles[i, j].TileTexture = terrainDetailByPositionNoCheck.Textures.BasicTextures[((i * 7) + (j * 11)) % terrainDetailByPositionNoCheck.Textures.BasicTextures.Count];
                        }
                        else
                        {
                            this.Tiles[i, j].TileTexture = Session.MainGame.mainGameScreen.Textures.TerrainTextures[Session.Current.Scenario.ScenarioMap.MapData[i, j]];
                        }
                    }*/
                }
            }
        }

        public void ReCalculateTileDestination(MainGameScreen screen)
        {
            this.ResetDisplayingTiles(screen);

            foreach (Tile tile in this.DisplayingTiles)
            {
                tile.Destination.X = this.leftEdge + (tile.Position.X * this.TileWidth);
                tile.Destination.Y = this.topEdge + (tile.Position.Y * this.TileHeight);
                tile.Destination.Width = this.TileWidth;
                tile.Destination.Height = this.TileHeight;
            }


            foreach (MapTile maptile in this.DisplayingMapTiles)
            {
                maptile.Destination.X = this.leftEdge + (maptile.Position.X * this.TileWidth * Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile);
                maptile.Destination.Y = this.topEdge + (maptile.Position.Y * this.TileHeight * Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile);
                maptile.Destination.Width = this.TileWidth * Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile;
                maptile.Destination.Height = this.TileHeight * Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile;
            }
        }

        public void ResetDisplayingTiles(MainGameScreen screen)
        {

            if (this.Tiles != null)
            {
                this.DisplayingTiles.Clear();
                for (int i = screen.TopLeftPosition.X; i <= screen.BottomRightPosition.X; i++)
                {
                    for (int j = screen.TopLeftPosition.Y; j <= screen.BottomRightPosition.Y; j++)
                    {
                        if ((((i >= 0) && (i < Session.Current.Scenario.ScenarioMap.MapDimensions.X)) && (j >= 0)) && (j < Session.Current.Scenario.ScenarioMap.MapDimensions.Y))
                        {
                            this.DisplayingTiles.Add(this.Tiles[i, j]);
                        }
                    }
                }
            }


            if (this.MapTiles != null)
            {
                lock (this.DisplayingMapTiles)
                {
                    this.DisplayingMapTiles.Clear();
                }
                for (int i = screen.TopLeftPosition.X; i <= screen.BottomRightPosition.X + Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile; i += Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile)
                {
                    for (int j = screen.TopLeftPosition.Y; j <= screen.BottomRightPosition.Y + Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile; j += Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile)
                    {
                        if ((((i >= 0) && (i < Session.Current.Scenario.ScenarioMap.MapDimensions.X)) && (j >= 0)) && (j < Session.Current.Scenario.ScenarioMap.MapDimensions.Y))
                        {
                            lock (this.DisplayingMapTiles)
                            {
                                this.DisplayingMapTiles.Add(this.MapTiles[i / Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile, j / Session.Current.Scenario.ScenarioMap.NumberOfSquaresInEachTile]);
                            }
                        }
                    }
                }
            }
        }

        public bool TileInScreen(Point tile)
        {
            return Session.MainGame.mainGameScreen.TileInScreen(tile);
        }

        public Point TranslateCoordinateToTilePosition(int coordinateX, int coordinateY)
        {
            int num = coordinateX - this.leftEdge;
            int num2 = coordinateY - this.topEdge;
            return new Point(num / this.TileWidth, num2 / this.TileHeight);
        }

        public int LeftEdge
        {
            get
            {
                return this.leftEdge;
            }
            set
            {
                this.leftEdge = value;
            }
        }

        public int TileHeight
        {
            get
            {
                return Session.Current.Scenario.ScenarioMap.TileHeight;
            }
            set
            {
                Session.Current.Scenario.ScenarioMap.TileHeight = value;
                if (Session.Current.Scenario.ScenarioMap.TileHeight < Session.Current.Scenario.ScenarioMap.TileWidthMin)
                {
                    Session.Current.Scenario.ScenarioMap.TileHeight = Session.Current.Scenario.ScenarioMap.TileWidthMin;
                }
                else if (Session.Current.Scenario.ScenarioMap.TileHeight > Session.Current.Scenario.ScenarioMap.TileWidthMax)
                {
                    Session.Current.Scenario.ScenarioMap.TileHeight = Session.Current.Scenario.ScenarioMap.TileWidthMax;
                }
            }
        }

        public int TileWidth
        {
            get
            {
                return Session.Current.Scenario.ScenarioMap.TileWidth;
            }
            set
            {
                Session.Current.Scenario.ScenarioMap.TileWidth = value;
                if (Session.Current.Scenario.ScenarioMap.TileWidth < Session.Current.Scenario.ScenarioMap.TileWidthMin)
                {
                    Session.Current.Scenario.ScenarioMap.TileWidth = Session.Current.Scenario.ScenarioMap.TileWidthMin;
                }
                else if (Session.Current.Scenario.ScenarioMap.TileWidth > Session.Current.Scenario.ScenarioMap.TileWidthMax)
                {
                    Session.Current.Scenario.ScenarioMap.TileWidth = Session.Current.Scenario.ScenarioMap.TileWidthMax;
                }

            }
        }

        public int TopEdge
        {
            get
            {
                return this.topEdge;
            }
            set
            {
                this.topEdge = value;
            }
        }

        public Point TotalMapSize
        {
            get
            {
                return new Point(this.TotalTileWidth, this.TotalTileHeight);
            }
        }

        public int TotalTileHeight
        {
            get
            {
                return Session.Current.Scenario.ScenarioMap.TotalTileHeight;
            }
        }

        public int TotalTileWidth
        {
            get
            {
                return Session.Current.Scenario.ScenarioMap.TotalTileWidth;
            }
        }

        public void chongsheditukuaitupian(int i, int j)
        {
            this.Tiles[i, j] = new Tile();
            this.Tiles[i, j].Position = new Point(i, j);
            TerrainDetail terrainDetailByPositionNoCheck = Session.Current.Scenario.GetTerrainDetailByPositionNoCheck(this.Tiles[i, j].Position);
            if (terrainDetailByPositionNoCheck != null)
            {
                if (terrainDetailByPositionNoCheck.Textures.BasicTextures.Count > 0)
                {
                    this.Tiles[i, j].TileTexture = terrainDetailByPositionNoCheck.Textures.BasicTextures[((i * 7) + (j * 11)) % terrainDetailByPositionNoCheck.Textures.BasicTextures.Count];
                }
                else
                {
                    this.Tiles[i, j].TileTexture = Session.MainGame.mainGameScreen.Textures.TerrainTextures[Session.Current.Scenario.ScenarioMap.MapData[i, j]];
                }
            }
            this.ReCalculateTileDestination(Session.MainGame.mainGameScreen);
        }

        internal void jiazaibeijingtupian()
        {
            /*
            if (this.BackgroundMap != null)
            {
                return;
                //this.BackgroundMap.Dispose();
                
            }
            //s=this.beijingtupian.BitmapToMemoryStream(device, bm);
            //bm.Dispose();
            //this.BackgroundMap = this.beijingtupian.huoqupingmutuxing(-this.LeftEdge, -this.TopEdge, Session.MainGame.mainGameScreen.viewportSize.X, Session.MainGame.mainGameScreen.viewportSize.Y,device );
            this.BackgroundMap = Texture2D.FromFile(device, "Content/Textures/Resources/ditu/" + Session.Current.Scenario.ScenarioMap.dituwenjian);
            */
        }

    }
}