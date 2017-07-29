using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Collections.Generic;
//using System.Drawing;
using System.IO;
using System.Resources;
using WorldOfTheThreeKingdoms;


namespace PersonPortraitPlugin
{

    public class PersonPortrait
    {
        public Dictionary<float, PlayerImage> PlayerImages = new Dictionary<float, PlayerImage>();
        //private Dictionary<float, PortraitItem> portraits = new Dictionary<float, PortraitItem>();
        public string TempImageFileName;

#pragma warning disable CS0169 // The field 'PersonPortrait.defaultImage' is never used
        private PlayerImage defaultImage;
#pragma warning restore CS0169 // The field 'PersonPortrait.defaultImage' is never used

        public PersonPortrait()
        {
            //defaultImage = LoadImage(9999);
        }

        //public bool HasPortrait(float id) 
        //{
        //    return GetImage(id) != defaultImage.Portrait;
        //}

        //public Image GetImage(float id)
        //{
        //    Image portrait = null;
        //    PlayerImage image = null;
            
        //    if (!this.PlayerImages.TryGetValue(id, out image))
        //    {
        //        image = this.LoadImage(id);
        //        if (image == null)
        //        {
        //            image = defaultImage;
                    
        //        }
        //        this.PlayerImages.Add(id, image);
        //    }

        //    portrait = image.Portrait;

        //    return portrait;
        //}

        //private PortraitItem GetItem(float id)
        //{
        //    PortraitItem item;
        //    this.portraits.TryGetValue(id, out item);
        //    if (item == null)
        //    {
        //        PlayerImage image = null;
        //        this.PlayerImages.TryGetValue(id, out image);
        //        if (image == null)
        //        {
        //            image = this.LoadImage(id);
        //            this.PlayerImages.Add(id, image);
        //            if (image == null)
        //            {
        //                image = this.LoadImage((int)id);
        //                if (image == null)
        //                {
        //                    image = defaultImage;
        //                }
        //            }
        //        }
        //        item = new PortraitItem();

        //        image.Portrait.Save(this.TempImageFileName);
        //        //item.PortraitTexture = CacheManager.GetTempTexture(this.TempImageFileName);
        //        image.SmallPortrait.Save(this.TempImageFileName);
        //        //item.SmallPortraitTexture = CacheManager.GetTempTexture(this.TempImageFileName);
        //        image.TroopPortrait.Save(this.TempImageFileName);
        //        //item.TroopPortraitTexture = CacheManager.GetTempTexture(this.TempImageFileName);
        //        image.FullPortrait.Save(this.TempImageFileName);
        //        //item.FullPortraitTexture = CacheManager.GetTempTexture(this.TempImageFileName);

        //        this.portraits.Add(id, item);
        //    }
        //    return item;
        //}

        //public Texture2D GetPortrait(float id)
        //{
        //    PortraitItem item = this.GetItem(id);
        //    if (item != null)
        //    {
        //        return item.PortraitTexture;
        //    }
        //    return null;
        //}

        //public Texture2D GetSmallPortrait(float id)
        //{
        //    PortraitItem item = this.GetItem(id);
        //    if (item != null)
        //    {
        //        return item.SmallPortraitTexture;
        //    }
        //    return null;
        //}

        //public Texture2D GetTroopPortrait(float id)
        //{
        //    PortraitItem item = this.GetItem(id);
        //    if (item != null)
        //    {
        //        return item.TroopPortraitTexture;
        //    }
        //    return null;
        //}
        //public Texture2D GetFullPortrait(float id)
        //{
        //    PortraitItem item = this.GetItem(id);
        //    if (item != null)
        //    {
        //        return item.FullPortraitTexture;
        //    }
        //    return null;
        //}
        //private PlayerImage LoadImage(string path, float id)
        //{
        //    if (Platform.Current.FileExists(path + @"\" + id + "f.jpg"))
        //    {
        //        if (Platform.Current.FileExists(path + @"\" + id + "s.jpg"))
        //        {
        //            if (Platform.Current.FileExists(path + @"\" + id + "t.jpg"))
        //            {
        //                PlayerImage image = new PlayerImage
        //                {
        //                    Portrait = Image.FromFile(path + @"\" + id + ".jpg"),
        //                    SmallPortrait = Image.FromFile(path + @"\" + id + "s.jpg"),
        //                    TroopPortrait = Image.FromFile(path + @"\" + id + "t.jpg"),
        //                    FullPortrait = Image.FromFile(path + @"\" + id + "f.jpg")
        //                };
        //                return image;
        //            }
        //            else
        //            {
        //                PlayerImage image = new PlayerImage
        //                {
        //                    Portrait = Image.FromFile(path + @"\" + id + ".jpg"),
        //                    SmallPortrait = Image.FromFile(path + @"\" + id + "s.jpg"),
        //                    TroopPortrait = Image.FromFile(path + @"\" + id + "s.jpg"),
        //                    FullPortrait = Image.FromFile(path + @"\" + id + "f.jpg")
        //                };
        //                return image;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (Platform.Current.FileExists(path + @"\" + id + "s.jpg"))
        //        {
        //            if (Platform.Current.FileExists(path + @"\" + id + "t.jpg"))
        //            {
        //                PlayerImage image = new PlayerImage
        //                {
        //                    Portrait = Image.FromFile(path + @"\" + id + ".jpg"),
        //                    SmallPortrait = Image.FromFile(path + @"\" + id + "s.jpg"),
        //                    TroopPortrait = Image.FromFile(path + @"\" + id + "t.jpg"),
        //                    FullPortrait = Image.FromFile(path + @"\9999f.jpg")
        //                };
        //                return image;
        //            }
        //            else
        //            {
        //                PlayerImage image = new PlayerImage
        //                {
        //                    Portrait = Image.FromFile(path + @"\" + id + ".jpg"),
        //                    SmallPortrait = Image.FromFile(path + @"\" + id + "s.jpg"),
        //                    TroopPortrait = Image.FromFile(path + @"\" + id + "s.jpg"),
        //                    FullPortrait = Image.FromFile(path + @"\9999f.jpg")
        //                };
        //                return image;
        //            }
        //        }

        //    }
        //    return null;
        //}

        //private PlayerImage LoadImage(float id)
        //{
        //    return null;
        //    PlayerImage result = this.LoadImage(@"Content\Textures\GameComponents\PersonPortrait\Images\Player", id);
        //    if (result == null)
        //    {
        //        result = this.LoadImage(@"Content\Textures\GameComponents\PersonPortrait\Images\Default", id);
        //    }
        //    return result;
        //}

    }
}

