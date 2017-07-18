using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using GameManager;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers

{
    public class Tile
    {
        public Rectangle Destination;
        public Point Position;
        public float Scale = 1f;

        public PlatformTexture TileTexture { get; set; }
        
        //private Texture2D tileTexture;

        //public Texture2D TileTexture
        //{
        //    get
        //    {
        //        return this.tileTexture;
        //    }
        //    set
        //    {
        //        this.tileTexture = value;
        //    }
        //}
         
    }

 

}
