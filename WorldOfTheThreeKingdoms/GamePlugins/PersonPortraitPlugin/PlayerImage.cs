using System;
//using System.Drawing;


namespace PersonPortraitPlugin
{
    public class Image
    {
        public string Picture { get; set; }

        public Image(string image)
        {
            Picture = image;
        }

        public void Save(string image)
        {
            Picture = image;
        }

        public static Image FromFile(string image)
        {
            return new Image(image);
        }
    }

    public class PlayerImage
    {
        public Image Portrait;
        public Image FullPortrait;
        public Image SmallPortrait;
        public Image TroopPortrait;        
    }
}

