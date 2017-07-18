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

        private GameScenario gameScenario;
        private int leftEdge = 0;
        public Map mainMap;
        public MainGameScreen screen;
        private GraphicsDevice device;
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
                        maptile.TileTexture = Platform.Current.LoadTexture("Content/Textures/Resources/ditu/" + mainMap.MapName + "/" + maptile.number + ".jpg", false);
                    //}
                    //catch (FileNotFoundException)
                    //{
                    //    maptile.TileTexture = CacheManager.LoadTempTexture("Content/Textures/Resources/ditu/" + mainMap.MapName + "/" + maptile.number + ".png");
                    //}
                }
                catch (Exception)
                {
                    maptile.TileTexture = new Texture2D(this.device, 1, 1);
                    //try
                    //{
                    //    this.freeTilesMemory(false);
                    //    try
                    //    {
                    //        maptile.TileTexture = CacheManager.LoadTempTexture("Content/Textures/Resources/ditu/" + mainMap.MapName + "/" + maptile.number + ".jpg");
                    //    }
                    //    catch (FileNotFoundException)
                    //    {
                    //        maptile.TileTexture = CacheManager.LoadTempTexture("Content/Textures/Resources/ditu/" + mainMap.MapName + "/" + maptile.number + ".png");
                    //    }
                    //}
                    //catch (Exception)
                    //{
                    //    maptile.TileTexture = new Texture2D(this.device, 1, 1);
                    //}
                }
            }
        }

        private void CheckTileTexture(Tile tile, out List<Texture2D> decorativeTextures)
        {
            decorativeTextures = null;
            return;
            TerrainDetail terrainDetailByPositionNoCheck = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(tile.Position);
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
                if (!this.screen.Scenario.PositionOutOfRange(position))
                {
                    leftDetail = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(position);
                    leftId = leftDetail.ID;
                    //(list = this.TerrainList)[num12 = this.mainMap.MapData[position.X, position.Y]] = list[num12] + 1;
                }
                Point point2 = new Point(tile.Position.X - 1, tile.Position.Y - 1);
                int topLeftId = 0;
                if (!this.screen.Scenario.PositionOutOfRange(point2))
                {
                    topLeftId = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(point2).ID;
                    //(list = this.TerrainList)[num12 = this.mainMap.MapData[point2.X, point2.Y]] = list[num12] + 1;
                }
                Point point3 = new Point(tile.Position.X, tile.Position.Y - 1);
                int topId = 0;
                TerrainDetail top = null;
                if (!this.screen.Scenario.PositionOutOfRange(point3))
                {
                    top = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(point3);
                    topId = top.ID;
                }
                Point point4 = new Point(tile.Position.X + 1, tile.Position.Y - 1);
                int topRightId = 0;
                if (!this.screen.Scenario.PositionOutOfRange(point4))
                {
                    topRightId = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(point4).ID;
                }
                Point point5 = new Point(tile.Position.X + 1, tile.Position.Y);
                int rightId = 0;
                TerrainDetail right = null;
                if (!this.screen.Scenario.PositionOutOfRange(point5))
                {
                    right = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(point5);
                    rightId = right.ID;
                }
                Point point6 = new Point(tile.Position.X + 1, tile.Position.Y + 1);
                int bottomRightId = 0;
                if (!this.screen.Scenario.PositionOutOfRange(point6))
                {
                    bottomRightId = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(point6).ID;
                }
                Point point7 = new Point(tile.Position.X, tile.Position.Y + 1);
                int bottomId = 0;
                TerrainDetail bottom = null;
                if (!this.screen.Scenario.PositionOutOfRange(point7))
                {
                    bottom = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(point7);
                    bottomId = bottom.ID;
                }
                Point point8 = new Point(tile.Position.X - 1, tile.Position.Y + 1);
                int bottomLeftId = 0;
                if (!this.screen.Scenario.PositionOutOfRange(point8))
                {
                    bottomLeftId = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(point8).ID;
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
                        (list = this.TerrainList)[num12 = this.mainMap.MapData[point3.X, point3.Y]] = list[num12] + 1;
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
                        (list = this.TerrainList)[num12 = this.mainMap.MapData[point4.X, point4.Y]] = list[num12] + 1;
                    }
                    if (rightId > 0)
                    {
                        (list = this.TerrainList)[num12 = this.mainMap.MapData[point5.X, point5.Y]] = list[num12] + 1;
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
                        (list = this.TerrainList)[num12 = this.mainMap.MapData[point6.X, point6.Y]] = list[num12] + 1;
                    }
                    if (bottomId > 0)
                    {
                        (list = this.TerrainList)[num12 = this.mainMap.MapData[point7.X, point7.Y]] = list[num12] + 1;
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
                        (list = this.TerrainList)[num12 = this.mainMap.MapData[point8.X, point8.Y]] = list[num12] + 1;
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
                    decorativeTextures = new List<Texture2D>();
                    switch (direction)
                    {
                        case TerrainDirection.Top:
                            decorativeTextures.Add(top.Textures.TopTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopTextures.Count]);
                            if ((((bottom != null) && (bottom.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomRightId != terrainDetailByPositionNoCheck.ID) && (bottomLeftId != terrainDetailByPositionNoCheck.ID))) && (bottom.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(bottom.Textures.BottomEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomEdgeTextures.Count]);
                            }
                            return;

                        case TerrainDirection.Left:
                            decorativeTextures.Add(leftDetail.Textures.LeftTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.LeftTextures.Count]);
                            if ((((right != null) && (right.ID != terrainDetailByPositionNoCheck.ID)) && ((topRightId != terrainDetailByPositionNoCheck.ID) && (bottomRightId != terrainDetailByPositionNoCheck.ID))) && (right.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(right.Textures.RightEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.RightEdgeTextures.Count]);
                            }
                            return;

                        case TerrainDirection.Right:
                            decorativeTextures.Add(right.Textures.RightTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.RightTextures.Count]);
                            if ((((leftDetail != null) && (leftDetail.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomLeftId != terrainDetailByPositionNoCheck.ID) && (topLeftId != terrainDetailByPositionNoCheck.ID))) && (leftDetail.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(leftDetail.Textures.LeftEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.LeftEdgeTextures.Count]);
                            }
                            return;

                        case TerrainDirection.Bottom:
                            decorativeTextures.Add(bottom.Textures.BottomTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomTextures.Count]);
                            if ((((top != null) && (top.ID != terrainDetailByPositionNoCheck.ID)) && ((topLeftId != terrainDetailByPositionNoCheck.ID) && (topRightId != terrainDetailByPositionNoCheck.ID))) && (top.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
                            {
                                decorativeTextures.Add(top.Textures.TopEdgeTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopEdgeTextures.Count]);
                            }
                            return;

                        case TerrainDirection.TopLeft:
                            decorativeTextures.Add(leftDetail.Textures.TopLeftCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.TopLeftCornerTextures.Count]);
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
                            decorativeTextures.Add(top.Textures.TopRightCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % top.Textures.TopRightCornerTextures.Count]);
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
                            decorativeTextures.Add(bottom.Textures.BottomLeftCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % bottom.Textures.BottomLeftCornerTextures.Count]);
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
                            decorativeTextures.Add(right.Textures.BottomRightCornerTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % right.Textures.BottomRightCornerTextures.Count]);
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
                            decorativeTextures.Add(leftDetail.Textures.CentreTextures[((tile.Position.X * 7) + (tile.Position.Y * 11)) % leftDetail.Textures.CentreTextures.Count]);
                            return;

                        case TerrainDirection.None:
                            if ((((leftDetail != null) && (leftDetail.ID != terrainDetailByPositionNoCheck.ID)) && ((bottomLeftId != terrainDetailByPositionNoCheck.ID) && (topLeftId != terrainDetailByPositionNoCheck.ID))) && (leftDetail.GraphicLayer < terrainDetailByPositionNoCheck.GraphicLayer))
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

        public void freeTilesMemory(bool gc = true)
        {
            if (this.MapTiles != null)
            {
                var nums = GetCurrentViewMapTileNumsAll();

                foreach (MapTile maptile in this.MapTiles)
                {
                    if (nums.Contains(int.Parse(maptile.number)))
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
            MapThread1.Abort();
            MapThread2.Abort();
        }

        Thread MapThread1;
        Thread MapThread2;

        private void ProcessMapTileTextureSync()
        {
            if (MapThread1 == null)
            {
                MapThread1 = new Thread(() =>
                {
                    while (true)
                    {
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

                            Thread.Sleep(30);
                        }
                        catch (ThreadAbortException)
                        {
                            break;
                        }
                        catch (Exception e)
                        {
                            DateTime dt = System.DateTime.Now;
                            String dateSuffix = "_" + dt.Year + "_" + dt.Month + "_" + dt.Day + "_" + dt.Hour + "h" + dt.Minute;
                            String logPath = "CrashLog" + dateSuffix + ".log";
                            StreamWriter sw = new StreamWriter(new FileStream(logPath, FileMode.Create));

                            sw.WriteLine("==================== Message ====================");
                            sw.WriteLine(e.Message);
                            sw.WriteLine("=================== StackTrace ==================");
                            sw.WriteLine(e.StackTrace);

                            sw.Close();

                            Thread.Sleep(1000);
                        }
                    }
                }
                );
                MapThread1.Start();
            }
            if (MapThread2 == null)
            {
                MapThread2 = new Thread(() =>
                {
                    while (true)
                    {
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
                                Thread.Sleep(80);
                                continue;
                            }

                            var tileNums2 = GetCurrentViewMapTileNums2();

                            var maps = tileNums2.Select(num => this.MapTiles[num % 30, num / 30]).Where(ma => ma != null && ma.TileTexture == null).ToArray();

                            if (maps != null && maps.Length > 0)
                            {
                                mapTile = maps.FirstOrDefault();
                                CheckMapTileTexture(mapTile);
                            }

                            Thread.Sleep(50);
                        }
                        catch (ThreadAbortException)
                        {
                            break;
                        }
                        catch (Exception e)
                        {
                            DateTime dt = System.DateTime.Now;
                            String dateSuffix = "_" + dt.Year + "_" + dt.Month + "_" + dt.Day + "_" + dt.Hour + "h" + dt.Minute;
                            String logPath = "CrashLog" + dateSuffix + ".log";
                            StreamWriter sw = new StreamWriter(new FileStream(logPath, FileMode.Create));

                            sw.WriteLine("==================== Message ====================");
                            sw.WriteLine(e.Message);
                            sw.WriteLine("=================== StackTrace ==================");
                            sw.WriteLine(e.StackTrace);

                            sw.Close();

                            Thread.Sleep(1000);
                        }
                    }
                });
                MapThread2.Start();
            }

        }


        public void Draw(SpriteBatch spriteBatch, Point viewportSize)
        {
            if (spriteBatch != null)
            {

                if (this.mainMap.MapName != null)
                {
                    ProcessMapTileTextureSync();

                    foreach (MapTile maptile in this.DisplayingMapTiles)
                    {
                        Rectangle? sourceRectangle = null;

                        List<Texture2D> decorativeTextures = null;
                        //this.CheckTileTexture(maptile, out decorativeTextures);

                        if (!drawBlackWhenNoneTexture)
                        {
                            this.CheckMapTileTexture(maptile);
                        }

                        if (maptile != null && maptile.TileTexture != null)
                        {
                            spriteBatch.Draw(maptile.TileTexture, maptile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.9f);
                        }

                        if (decorativeTextures != null)
                        {
                            foreach (Texture2D textured in decorativeTextures)
                            {
                                sourceRectangle = null;
                                spriteBatch.Draw(textured, maptile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8998f);
                            }
                        }
                    }

                    ResetDisplayingTiles();

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

                    //臨時材質到達一定數量，也予清理
                    if (CacheManager.TextureTempDics.Count >= 30)
                    {
                        CacheManager.Clear(CacheType.Page);
                    }

                    if (this.screen.editMode)
                    {
                        foreach (Tile tile in this.DisplayingTiles)
                        {
                            List<Texture2D> decorativeTextures = null;
                            this.CheckTileTexture(tile, out decorativeTextures);
                            //Rectangle? sourceRectangle = null;
                            if (this.xianshidituxiaokuai && this.mainMap.MapData[tile.Position.X, tile.Position.Y] != 0) //未知地形显示为透明，以方便地形编辑
                            {
                                //spriteBatch.Draw(tile.TileTexture, tile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8998f);
                                CacheManager.DrawAvatar(tile.TileTexture.Name, tile.Destination, Color.White, false, true, TextureShape.None, null, 0.8998f);
                            }

                        }
                    }

                }
                else
                {
                    foreach (Tile tile in this.DisplayingTiles)
                    {
                        List<Texture2D> decorativeTextures = null;
                        this.CheckTileTexture(tile, out decorativeTextures);
                        //Rectangle? sourceRectangle = null;

                        //spriteBatch.Draw(tile.TileTexture, tile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8998f);

                        CacheManager.DrawAvatar(tile.TileTexture.Name, tile.Destination, Color.White, false, true, TextureShape.None, null, 0.8998f);

                        /*if (decorativeTextures != null)
                        {
                            foreach (Texture2D textured in decorativeTextures)
                            {
                                sourceRectangle = null;
                                spriteBatch.Draw(textured, tile.Destination, sourceRectangle, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.8998f);
                            }
                        }*/
                    }

                }

                if (GlobalVariables.ShowGrid)
                {
                    foreach (Tile tile in this.DisplayingTiles)
                    {
                        if (this.screen.editMode)
                        {
                            spriteBatch.Draw(this.screen.Textures.EditModeGrid, tile.Destination, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.81f);
                        }
                        else
                        {
                            if (this.mainMap.MapData[tile.Position.X, tile.Position.Y] != 0 && this.mainMap.MapData[tile.Position.X, tile.Position.Y] != 4 && this.mainMap.MapData[tile.Position.X, tile.Position.Y] != 7)
                            {
                                spriteBatch.Draw(this.screen.Textures.wanggetupian, tile.Destination, null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0.81f);
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
                if (jianzhu.Scenario.ScenarioMap.UseSimpleArchImages)
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
                if (guimo == 1 || jianzhu.Scenario.ScenarioMap.UseSimpleArchImages)
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
            return this.gameScenario.GetTerrainNameByPosition(position);
        }

        public Rectangle GetThreeFourthsDestination(Point position)
        {
            return new Rectangle(this.Tiles[position.X, position.Y].Destination.X + (this.Tiles[position.X, position.Y].Destination.Width / 8), this.Tiles[position.X, position.Y].Destination.Y + (this.Tiles[position.X, position.Y].Destination.Height / 8), (this.Tiles[position.X, position.Y].Destination.Width * 3) / 4, (this.Tiles[position.X, position.Y].Destination.Height * 3) / 4);
        }

        public Point GetTopCenterPoint(Point position)
        {
            return new Point(((position.X * this.TileWidth) + (this.TileWidth / 2)) + this.leftEdge, (position.Y * this.TileHeight) + this.topEdge);
        }

        public void Initialize(GameScenario scenario, MainGameScreen screen, GraphicsDevice device)
        {
            this.gameScenario = scenario;
            this.mainMap = scenario.ScenarioMap;
            this.screen = screen;
            this.device = device;
            this.TerrainList.Clear();
            for (int i = 0; i < Enum.GetValues(typeof(TerrainKind)).Length; i++)
            {
                this.TerrainList.Add(0);
            }
        }

        public void PrepareMap()
        {
            this.Tiles = null;
            this.Tiles = new Tile[this.mainMap.MapDimensions.X, this.mainMap.MapDimensions.Y];
            for (int i = 0; i < this.mainMap.MapDimensions.X; i++)
            {
                for (int j = 0; j < this.mainMap.MapDimensions.Y; j++)
                {
                    this.Tiles[i, j] = new Tile();
                    this.Tiles[i, j].Position = new Point(i, j);
                    TerrainDetail terrainDetailByPositionNoCheck = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(this.Tiles[i, j].Position);

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
            this.MapTiles = new MapTile[this.mainMap.NumberOfTiles, this.mainMap.NumberOfTiles];
            for (int i = 0; i < this.mainMap.NumberOfTiles; i++)
            {
                for (int j = 0; j < this.mainMap.NumberOfTiles; j++)
                {
                    this.MapTiles[i, j] = new MapTile();
                    this.MapTiles[i, j].Position = new Point(i, j);
                    this.MapTiles[i, j].number = (i + j * this.mainMap.NumberOfTiles).ToString();
                    /*TerrainDetail terrainDetailByPositionNoCheck = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(this.Tiles[i, j].Position);
                    
                    if (terrainDetailByPositionNoCheck != null)
                    {
                        if (terrainDetailByPositionNoCheck.Textures.BasicTextures.Count > 0)
                        {
                            this.Tiles[i, j].TileTexture = terrainDetailByPositionNoCheck.Textures.BasicTextures[((i * 7) + (j * 11)) % terrainDetailByPositionNoCheck.Textures.BasicTextures.Count];
                        }
                        else
                        {
                            this.Tiles[i, j].TileTexture = this.screen.Textures.TerrainTextures[this.mainMap.MapData[i, j]];
                        }
                    }*/
                }
            }
        }

        public void ReCalculateTileDestination(GraphicsDevice device)
        {

            this.ResetDisplayingTiles();
            foreach (Tile tile in this.DisplayingTiles)
            {
                tile.Destination.X = this.leftEdge + (tile.Position.X * this.TileWidth);
                tile.Destination.Y = this.topEdge + (tile.Position.Y * this.TileHeight);
                tile.Destination.Width = this.TileWidth;
                tile.Destination.Height = this.TileHeight;
            }


            foreach (MapTile maptile in this.DisplayingMapTiles)
            {
                maptile.Destination.X = this.leftEdge + (maptile.Position.X * this.TileWidth * this.mainMap.NumberOfSquaresInEachTile);
                maptile.Destination.Y = this.topEdge + (maptile.Position.Y * this.TileHeight * this.mainMap.NumberOfSquaresInEachTile);
                maptile.Destination.Width = this.TileWidth * this.mainMap.NumberOfSquaresInEachTile;
                maptile.Destination.Height = this.TileHeight * this.mainMap.NumberOfSquaresInEachTile;
            }
        }

        public void ResetDisplayingTiles()
        {

            if (this.Tiles != null)
            {
                this.DisplayingTiles.Clear();
                for (int i = this.screen.TopLeftPosition.X; i <= this.screen.BottomRightPosition.X; i++)
                {
                    for (int j = this.screen.TopLeftPosition.Y; j <= this.screen.BottomRightPosition.Y; j++)
                    {
                        if ((((i >= 0) && (i < this.mainMap.MapDimensions.X)) && (j >= 0)) && (j < this.mainMap.MapDimensions.Y))
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
                for (int i = this.screen.TopLeftPosition.X; i <= this.screen.BottomRightPosition.X + this.mainMap.NumberOfSquaresInEachTile; i += this.mainMap.NumberOfSquaresInEachTile)
                {
                    for (int j = this.screen.TopLeftPosition.Y; j <= this.screen.BottomRightPosition.Y + this.mainMap.NumberOfSquaresInEachTile; j += this.mainMap.NumberOfSquaresInEachTile)
                    {
                        if ((((i >= 0) && (i < this.mainMap.MapDimensions.X)) && (j >= 0)) && (j < this.mainMap.MapDimensions.Y))
                        {
                            lock (this.DisplayingMapTiles)
                            {
                                this.DisplayingMapTiles.Add(this.MapTiles[i / this.mainMap.NumberOfSquaresInEachTile, j / this.mainMap.NumberOfSquaresInEachTile]);
                            }
                        }
                    }
                }
            }
        }

        public bool TileInScreen(Point tile)
        {
            return this.screen.TileInScreen(tile);
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
                return this.mainMap.TileHeight;
            }
        }

        public int TileWidth
        {
            get
            {
                return this.mainMap.TileWidth;
            }
            set
            {
                this.mainMap.TileWidth = value;
                if (this.mainMap.TileWidth < this.tileWidthMin)
                {
                    this.mainMap.TileWidth = this.tileWidthMin;
                }
                else if (this.mainMap.TileWidth > this.tileWidthMax)
                {
                    this.mainMap.TileWidth = this.tileWidthMax;
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
                return this.mainMap.TotalTileHeight;
            }
        }

        public int TotalTileWidth
        {
            get
            {
                return this.mainMap.TotalTileWidth;
            }
        }

        public void chongsheditukuaitupian(int i, int j)
        {
            this.Tiles[i, j] = new Tile();
            this.Tiles[i, j].Position = new Point(i, j);
            TerrainDetail terrainDetailByPositionNoCheck = this.screen.Scenario.GetTerrainDetailByPositionNoCheck(this.Tiles[i, j].Position);
            if (terrainDetailByPositionNoCheck != null)
            {
                if (terrainDetailByPositionNoCheck.Textures.BasicTextures.Count > 0)
                {
                    this.Tiles[i, j].TileTexture = terrainDetailByPositionNoCheck.Textures.BasicTextures[((i * 7) + (j * 11)) % terrainDetailByPositionNoCheck.Textures.BasicTextures.Count];
                }
                else
                {
                    this.Tiles[i, j].TileTexture = this.screen.Textures.TerrainTextures[this.mainMap.MapData[i, j]];
                }
            }
            this.ReCalculateTileDestination(device);
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
            //this.BackgroundMap = this.beijingtupian.huoqupingmutuxing(-this.LeftEdge, -this.TopEdge, this.screen.viewportSize.X, this.screen.viewportSize.Y,device );
            this.BackgroundMap = Texture2D.FromFile(device, "Content/Textures/Resources/ditu/" + this.gameScenario.ScenarioMap.dituwenjian);
            */
        }

    }
}