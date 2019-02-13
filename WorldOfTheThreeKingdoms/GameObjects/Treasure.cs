using GameManager;
using GameObjects.Influences;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Runtime.Serialization;
using WorldOfTheThreeKingdoms;



namespace GameObjects
{
    [DataContract]
    public class Treasure : GameObject
    {
        private int appearYear;
        private bool available;

        [DataMember]
        public int OwnershipIDString { get; set; }

        public Person Ownership;
        private string description;

        [DataMember]
        public int HiddenPlaceIDString { get; set; }

        public Architecture HiddenPlace;

        [DataMember]
        public string EffectsString { get; set; }

        public InfluenceTable Influences = new InfluenceTable();
        private int pic;
        private PlatformTexture picture;
        private int worth;

        public void Init()
        {
            Influences = new InfluenceTable();
        }

        [DataMember]
        public int TreasureGroup
        {
            get;
            set;
        }

        [DataMember]
        public int AppearYear
        {
            get
            {
                return this.appearYear;
            }
            set
            {
                this.appearYear = value;
            }
        }

        [DataMember]
        public bool Available
        {
            get
            {
                return this.available;
            }
            set
            {
                this.available = value;
            }
        }

        public string BelongedPersonString
        {
            get
            {
                return ((this.Ownership != null) ? this.Ownership.Name : "----");
            }
        }

        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public string HidePlaceString
        {
            get
            {
                return ((this.HiddenPlace != null) ? this.HiddenPlace.Name : "----");
            }
        }

        public string InfluenceString
        {
            get
            {
                string str = "";
                foreach (Influence influence in this.Influences.Influences.Values)
                {
                    str = str + "•" + influence.Description;
                }
                return str;
            }
        }

        [DataMember]
        public int TreasureImage
        {
            get
            {
                return this.pic;
            }
            set
            {
                this.pic = value;
            }
        }

        //public void disposeTexture()
        //{
        //    if (this.picture != null)
        //    {
        //        this.picture.Dispose();
        //        this.picture = null;
        //    }
        //}

        public PlatformTexture Picture
        {
            get
            {
                if (this.picture == null)
                {
                    //try
                    //{
                        this.picture = CacheManager.GetTempTexture("Content/Textures/Resources/Treasure/" + this.TreasureImage.ToString() + ".jpg");
                    //}
                    //catch
                    //{
                    //    this.picture = new Texture2D(Platform.GraphicsDevice, 0, 0);
                    //}
                }
                return this.picture;
            }
        }

        [DataMember]
        public int Worth
        {
            get
            {
                return this.worth;
            }
            set
            {
                this.worth = value;
            }
        }
    }
}

