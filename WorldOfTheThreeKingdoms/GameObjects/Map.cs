using Microsoft.Xna.Framework;
using System;
using System.Runtime.Serialization;
using System.Text;


namespace GameObjects
{
    [DataContract]
    public class Map
    {
        [DataMember]
        public Point JumpPosition;

        private int[,] mapData;
        private Point mapDimensions;
        private int tileWidth = 100;
        private int tileHeight = 100;
        private string dituwenjian;
        private int kuaishu = 20;
        private int meikuaidexiaokuaishu = 10;
        private bool useSimpleArchImages = false;

        [DataMember]
        public int TileWidthMin = 30;
        [DataMember]
        public int TileWidthMax = 100;

        [DataMember]
        public string MapName
        {
            get
            {
                if (String.IsNullOrEmpty(dituwenjian))
                {
                    return null;
                } 
                else 
                {
                    return dituwenjian;
                }
            }
            set
            {
                if (value.EndsWith(".jpg"))
                {
                    dituwenjian = value.Substring(0, value.Length - 4);
                }
                else
                {
                    dituwenjian = value;
                }
            }
        }

        public void Init()
        {
            if (TileWidthMin == 0)
            {
                TileWidthMin = 30;
            }
            if (TileWidthMax == 0)
            {
                TileWidthMax = 100;
            }
        }

        public void Clear()
        {
            this.mapDimensions = Point.Zero;
            this.mapData = null;
        }

        public bool LoadMapData(string[] mapDataValueString, int X, int Y)
        {
            this.mapDimensions = new Point(X, Y);
            if (mapDataValueString.Length != this.MapTileCount)
            {
                throw new Exception("The map data count does not match the MapTileCount");
            }
            this.mapData = new int[X, Y];
            for (int i = 0; i < this.MapTileCount; i++)
            {
                try
                {
                    this.mapData[i % X, i / X] = int.Parse(mapDataValueString[i]);
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.ToString());
                }
            }
            return true;
        }

        public bool LoadMapData(string mapdata, int X, int Y)
        {
            this.mapDimensions.X = X;
            this.mapDimensions.Y = Y;
            char[] separator = new char[] { ' ', '\n', '\r', '\t' };
            string[] strArray = mapdata.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length != this.MapTileCount)
            {
                throw new Exception("The map data count does not match the MapTileCount");
            }
            this.mapData = new int[X, Y];
            for (int i = 0; i < this.MapTileCount; i++)
            {
                try
                {
                    this.mapData[i % X, i / X] = int.Parse(strArray[i]);
                }
                catch (Exception exception)
                {
                    throw new Exception(exception.ToString());
                }
            }
            return true;
        }

        public bool LoadMapDataFromDataBase(string connectionString)
        {
            return true;
        }

        public bool PositionOutOfRange(Point mapPosition)
        {
            return ((((mapPosition.X < 0) || (mapPosition.Y < 0)) || (mapPosition.X >= this.mapDimensions.X)) || (mapPosition.Y >= this.mapDimensions.Y));
        }

        public void Replace(int terrainID1, int terrainID2)
        {
            for (int i = 0; i < this.mapDimensions.Y; i++)
            {
                for (int j = 0; j < this.mapDimensions.X; j++)
                {
                    if (this.mapData[j, i] == terrainID1)
                    {
                        this.mapData[j, i] = terrainID2;
                    }
                }
            }
        }

        public string SaveToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < this.mapDimensions.Y; i++)
            {
                for (int j = 0; j < this.mapDimensions.X; j++)
                {
                    builder.Append(this.mapData[j, i].ToString() + " ");
                }
            }
            return builder.ToString();
        }
        
        public int[,] MapData
        {
            get
            {
                return this.mapData;
            }
            set
            {
                this.mapData = value;
            }
        }

        [DataMember]
        public string MapDataString { get; set; }

        [DataMember]
        public Point MapDimensions
        {
            get
            {
                return this.mapDimensions;
            }
            set
            {
                this.mapDimensions = value;
            }
        }

        public int MapTileCount
        {
            get
            {
                return (this.mapDimensions.X * this.mapDimensions.Y);
            }
        }
        [DataMember]
        public int TileHeight
        {
            get
            {
                return this.tileHeight;
            }
            set
            {
                this.tileHeight = value;
            }
        }
        [DataMember]
        public int TileWidth
        {
            get
            {
                return this.tileWidth;
            }
            set
            {
                this.tileWidth = value;
            }
        }

        public int TotalTileHeight
        {
            get
            {
                return (this.TileHeight * this.mapDimensions.Y);
            }
        }

        public int TotalTileWidth
        {
            get
            {
                return (this.TileWidth * this.mapDimensions.X);
            }
        }
        [DataMember]
        public int NumberOfTiles
        {
            get
            {
                return kuaishu;
            }
            set
            {
                kuaishu = value;
            }
        }
        [DataMember]
        public int NumberOfSquaresInEachTile
        {
            get
            {
                return meikuaidexiaokuaishu;
            }
            set
            {
                meikuaidexiaokuaishu = value;
            }
        }
        [DataMember]
        public bool UseSimpleArchImages
        {
            get
            {
                return useSimpleArchImages;
            }
            set
            {
                useSimpleArchImages = value;
            }
        }
    }
}

