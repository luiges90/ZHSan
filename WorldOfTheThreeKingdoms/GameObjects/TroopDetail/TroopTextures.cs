using GameManager;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Runtime.InteropServices;
using WorldOfTheThreeKingdoms;



namespace GameObjects.TroopDetail
{

    [StructLayout(LayoutKind.Sequential)]
    public struct TroopTextures
    {
        public string MoveTextureFileName;
        private PlatformTexture moveTexture;
        public string AttackTextureFileName;
        private PlatformTexture attackTexture;
        public string BeAttackedTextureFileName;
        private PlatformTexture beAttackedTexture;
        public string CastTextureFileName;
        private PlatformTexture castTexture;
        public string BeCastedTextureFileName;
        private PlatformTexture beCastedTexture;

        public int TextureWidth
        {
            get
            {
                return 1280;  // Platform.IsMobilePlatForm ? 600 : 1280;
            }
        }

        public int TextureHeight
        {
            get
            {
                return 1024;  // Platform.IsMobilePlatForm ? 480 : 1024;
            }
        }

        public PlatformTexture MoveTexture
        {
            get
            {
                //try
                //{
                    if (this.moveTexture == null)
                    {
                        this.moveTexture = CacheManager.GetTempTexture(this.MoveTextureFileName);
                        this.moveTexture.Width = TextureWidth;
                        this.moveTexture.Height = TextureHeight;
                    }
                //}
                //catch
                //{
                //    if (this.moveTexture == null)
                //    {
                //        this.moveTexture = new PlatformTexture(1, 1);
                //    }
                //}
                return this.moveTexture;
            }
        }
        public PlatformTexture AttackTexture
        {
            get
            {
                //try
                //{
                    if (this.attackTexture == null)
                    {
                        this.attackTexture = CacheManager.GetTempTexture(this.AttackTextureFileName);
                    this.attackTexture.Width = TextureWidth;
                    this.attackTexture.Height = TextureHeight;
                }
                //}
                //catch
                //{
                //    if (this.attackTexture == null)
                //    {
                //        this.attackTexture = new Texture2D(1, 1);
                //    }
                //}
                return this.attackTexture;
            }
        }
        public PlatformTexture BeAttackedTexture
        {
            get
            {
                //try
                //{
                    if (this.beAttackedTexture == null)
                    {
                        this.beAttackedTexture = CacheManager.GetTempTexture(this.BeAttackedTextureFileName);
                    this.beAttackedTexture.Width = TextureWidth;
                    this.beAttackedTexture.Height = TextureHeight;
                }
                //}
                //catch
                //{
                //    if (this.beAttackedTexture == null)
                //    {
                //        this.beAttackedTexture = new Texture2D(1, 1);
                //    }
                //}
                return this.beAttackedTexture;
            }
        }
        public PlatformTexture CastTexture
        {
            get
            {
                //try
                //{
                    if (this.castTexture == null)
                    {
                        if (this.CastTextureFileName == this.AttackTextureFileName)
                        {
                            this.castTexture = this.AttackTexture;
                        this.castTexture.Width = TextureWidth;
                        this.castTexture.Height = TextureHeight;
                    }
                        else
                        {
                            this.castTexture = CacheManager.GetTempTexture(this.CastTextureFileName);
                        this.castTexture.Width = TextureWidth;
                        this.castTexture.Height = TextureHeight;
                    }
                    }
                //}
                //catch
                //{
                //    if (this.castTexture == null)
                //    {
                //        this.castTexture = new Texture2D(1, 1);
                //    }
                //}
                return this.castTexture;
            }
        }
        public PlatformTexture BeCastedTexture
        {
            get
            {
                //try
                //{
                    if (this.beCastedTexture == null)
                    {
                        if (this.BeCastedTextureFileName == this.BeAttackedTextureFileName)
                        {
                            this.beCastedTexture = this.BeAttackedTexture;
                        }
                        else
                        {
                            this.beCastedTexture = CacheManager.GetTempTexture(this.BeCastedTextureFileName);
                        }
                    this.beCastedTexture.Width = TextureWidth;
                    this.beCastedTexture.Height = TextureHeight;
                }
                //}
                //catch
                //{
                //    if (this.beCastedTexture == null)
                //    {
                //        this.beCastedTexture = new PlatformTexture(1, 1);
                //    }
                //}
                return this.beCastedTexture;
            }
        }
        //public void Dispose()
        //{
        //    if (this.moveTexture != null)
        //    {
        //        this.moveTexture.Dispose();
        //        this.moveTexture = null;
        //    }
        //    if (this.attackTexture != null)
        //    {
        //        this.attackTexture.Dispose();
        //        this.attackTexture = null;
        //    }
        //    if (this.beAttackedTexture != null)
        //    {
        //        this.beAttackedTexture.Dispose();
        //        this.beAttackedTexture = null;
        //    }
        //    if (this.castTexture != null)
        //    {
        //        this.castTexture.Dispose();
        //        this.castTexture = null;
        //    }
        //    if (this.beCastedTexture != null)
        //    {
        //        this.beCastedTexture.Dispose();
        //        this.beCastedTexture = null;
        //    }
        //}
    }
}

