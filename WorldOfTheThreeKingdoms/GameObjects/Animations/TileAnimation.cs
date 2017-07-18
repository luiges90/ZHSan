using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;



namespace GameObjects.Animations
{
    public class TileAnimation
    {
        public int currentFrameIndex;
        public int currentStayIndex;
        public bool Drawing;
        public TileAnimationKind Kind;
        public Animation LinkedAnimation;
        public bool Looping;
        public Point Position;

        public void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            if (this.Drawing)
            {
                try
                {
                    bool endLoop = false;
                    float layerDepth = 0.65f;
                    if (this.LinkedAnimation.Back)
                    {
                        layerDepth = 0.75f;
                    }
                    spriteBatch.Draw(this.LinkedAnimation.Texture, destination, new Rectangle?(this.LinkedAnimation.GetCurrentDisplayRectangle(ref this.currentFrameIndex, ref this.currentStayIndex, this.LinkedAnimation.Texture.Width / this.LinkedAnimation.FrameCount, 0, out endLoop, false)), Color.White, 0f, Vector2.Zero, SpriteEffects.None, layerDepth);
                    if (endLoop)
                    {
                        if (this.Looping)
                        {
                            this.currentFrameIndex = 0;
                            this.currentStayIndex = 0;
                        }
                        else
                        {
                            this.Drawing = false;
                        }
                    }
                }
                catch (Exception) { }
            }
        }

        public override int GetHashCode()
        {
            return ((this.Position.ToString().GetHashCode() ^ this.Kind.ToString().GetHashCode()) ^ this.Looping.GetHashCode());
        }
    }
}

