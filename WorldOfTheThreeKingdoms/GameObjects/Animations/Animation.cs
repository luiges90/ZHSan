using GameGlobal;
using GameManager;
using GameObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using WorldOfTheThreeKingdoms;



namespace GameObjects.Animations
{
    [DataContract]
    public class Animation : GameObject
    {
        private bool back;
        private int frameCount;

        private int stayCount;
        private PlatformTexture texture;
        [DataMember]
        public string TextureFileName;
        [DataMember]
        public int TextureWidth;
        [DataMember]
        public int TextureHeight;

        public Rectangle GetCurrentDisplayRectangle(ref int frameIndex, ref int stayIndex, int width, int row, out bool EndLoop, bool hold)
        {
            EndLoop = false;
            if (!hold)
            {
                stayIndex++;
                if (stayIndex >= this.StayCount * Setting.Current.GlobalVariables.TroopMoveSpeed / 4)
                {
                    stayIndex = 0;
                    frameIndex++;
                    if (frameIndex >= (this.FrameCount - 1))
                    {
                        EndLoop = true;
                    }
                }
            }
            return new Rectangle(width * frameIndex, width * row, width, width);
        }
        [DataMember]
        public bool Back
        {
            get
            {
                return this.back;
            }
            set
            {
                this.back = value;
            }
        }
        [DataMember]
        public int FrameCount
        {
            get
            {
                return this.frameCount;
            }
            set
            {
                this.frameCount = value;
            }
        }
        [DataMember]
        public int StayCount
        {
            get
            {
                return this.stayCount;
            }
            set
            {
                this.stayCount = value;
            }
        }

        //public void disposeTexture()
        //{
        //    if (this.texture != null)
        //    {
        //        this.texture.Dispose();
        //        this.texture = null;
        //    }
        //}

        public PlatformTexture Texture
        {
            get
            {
                if (this.texture == null)
                {
                    //try
                    //{
                    this.texture = CacheManager.GetTempTexture(this.TextureFileName);
                    this.texture.Width = this.TextureWidth;
                    this.texture.Height = this.TextureHeight;
                    //}
                    //catch (OutOfMemoryException)
                    //{
                    //    this.texture = new Texture2D(1, 1);
                    //}
                }
                return this.texture;
            }
        }
    }
}

