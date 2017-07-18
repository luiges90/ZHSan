using GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Platforms;
using System;
using System.Runtime.Serialization;
using WorldOfTheThreeKingdoms;



namespace GameObjects.Animations
{
    [DataContract]
    public class CombatNumberGenerator
    {
        public GraphicsDevice Device;
        [DataMember]
        public int DigitHeight = 20;
        [DataMember]
        public int DigitWidth = 12;
        private Texture2D texture;
        [DataMember]
        public string TextureFileName;

        public Rectangle GetCurrentArrowRectangle(CombatNumberKind kind, CombatNumberDirection direction)
        {
            return new Rectangle(this.DigitWidth * (10 + (int) direction), this.DigitHeight * (((int) kind *(int)  CombatNumberKind.战意) + (((int) direction))), this.DigitWidth, this.DigitHeight);
        }

        public Rectangle GetCurrentDigitRectangle(CombatNumberKind kind, CombatNumberDirection direction, int digit)
        {
            return new Rectangle(this.DigitWidth * digit, this.DigitHeight * (((int) kind *(int)  CombatNumberKind.战意) + ( ((int) direction))), this.DigitWidth, this.DigitHeight);
        }

        public Texture2D Texture
        {
            get
            {
                if (this.texture == null)
                {
                    this.texture = CacheManager.LoadTempTexture(this.TextureFileName);
                    this.DigitWidth = this.texture.Width / 12;
                    this.DigitHeight = (this.texture.Height / Enum.GetValues(typeof(CombatNumberKind)).Length) / 2;
                }
                return this.texture;
            }
        }
    }
}

