using Microsoft.Xna.Framework;
using SanguoSeason;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;
using System;

namespace GameManager
{
    public class AnimatedTexture
    {
        //public Texture2D myTexture;
        public Rectangle[] Recs;
        private float TimePerFrame;
        public int Frame { get; set; }
        public float TotalElapsed;
        public bool Paused = false, Visible = true;

        public int CircleNumber = 0;
        public int CircleNumberMax { get; set; }

        //private bool MultiPicture = false;        
        //public float Rotation, Scale, Depth;
        //public Vector2 Origin;
        public bool MouseOver;
        public Vector2 Position;

        public float Depth = 0f;

        private int width, height;

        public string Asset;
        private string Name;

        public string Text;
        public int[] TextHigh;
        public Vector2 TextPos;

        public bool OrderAsc;

        public float Scale = 1f;

        //public bool IsPaused
        //{
        //    get { return Paused; }
        //}

        public int FrameCount;

        public int LimiteWidth = 0;

        public bool LimiteOrder = true;

        public AnimatedTexture(string asset, string name, string extension, bool orderAsc, int framesPerSec)
            : this(asset, name, extension, Session.TextureRecs[asset + "#" + name], orderAsc, framesPerSec)
        {
        }

        public AnimatedTexture(string asset, string name, string extension, TextureRecs textureRecs, bool orderAsc, int framesPerSec)
        {
            Asset = asset;
            Name = name;
            //MultiPicture = false;
            width = textureRecs.Recs[0].Width; height = textureRecs.Recs[0].Height;

            OrderAsc = orderAsc;
            //myTexture = CacheManager.Load(asset); //, extension, textureRecs.IsLive ? CacheLiveType.Live : CacheLiveType.Scene, false);

            if (OrderAsc)
            {
                Recs = textureRecs.Recs;
            }
            else
            {
                //Recs = textureRecs.Recs.Select(re => new Rectangle(myTexture.Width - re.X - re.Width, re.Y, re.Width, re.Height)).ToArray();
                Recs = textureRecs.Recs.Select(re => new Rectangle(textureRecs.Width - re.X - re.Width, re.Y, re.Width, re.Height)).ToArray();
            }
            FrameCount = Recs.Length;
            //width = (myTextures[0].Width * assets.Length) / frameCount;
            TimePerFrame = (float)1 / framesPerSec;
            Frame = 0;
            TotalElapsed = 0;
            Paused = false;
        }

        public void ChangeFrame(int framesPerSec)
        {
            TimePerFrame = (float)1 / framesPerSec;
        }

        public void ClearCache()
        {
            //FontCharacter.RemoveSceneCache(myTexture);
        }

        public bool IsInTexture(int poX, int poY)
        {
            MouseOver = this.Position.X <= poX && poX <= this.Position.X + width && this.Position.Y <= poY && poY <= this.Position.Y + height;
            return MouseOver;
        }

        public void UpdateFrame(float elapsed, int start, int end)
        {
            if (!Visible || Paused || CircleNumberMax != 0 && CircleNumber >= CircleNumberMax) return;
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                //if (OrderAsc)
                //{
                if (Frame < start || Frame > end) Frame = start;
                else if (Frame == end) Frame = start;
                else Frame++;
                //}
                //else
                //{
                //    if (Frame < start || Frame > end) Frame = end;
                //    else if (Frame == start) Frame = end;
                //    else Frame--;
                //}
                TotalElapsed -= TimePerFrame;
            }
        }

        public void UpdateFrame(float elapsed)
        {
            if (!Visible || Paused || CircleNumberMax != 0 && CircleNumber >= CircleNumberMax) return;
            TotalElapsed += elapsed;
            if (TotalElapsed > TimePerFrame)
            {
                Frame = NextFrame(Frame, true);
                //if (OrderAsc)
                //{
                //    if (Frame != FrameCount - 1) { Frame++; }
                //    else { Frame = 0; CircleNumber++; }
                //    //Frame++;
                //    // Keep the Frame between 0 and the total frames, minus one.
                //    //if (Frame == FrameCount) Frame = 0;
                //    //Frame = Frame % FrameCount;
                //    //if (Frame == 0) CircleNumber++;
                //    ////if (Frame == 0 && CircleNumber != null) CircleNumber++;
                //}
                //else
                //{
                //    if (Frame != 0) { Frame--; }
                //    else { Frame = FrameCount - 1; CircleNumber++; }
                //}
                TotalElapsed -= TimePerFrame;
            }
        }

        public int NextFrame(int frame, bool circle)
        {
            if (OrderAsc)
            {
                if (frame != FrameCount - 1) { return frame + 1; }
                else
                {
                    if (circle) CircleNumber++;
                    return 0;
                }
                //Frame++;
                // Keep the Frame between 0 and the total frames, minus one.
                //if (Frame == FrameCount) Frame = 0;
                //Frame = Frame % FrameCount;
                //if (Frame == 0) CircleNumber++;
                ////if (Frame == 0 && CircleNumber != null) CircleNumber++;
            }
            else
            {
                if (frame != 0) { return frame - 1; }
                else
                {
                    if (circle) CircleNumber++;
                    return FrameCount - 1;
                }
            }
        }

        public void DrawFrame(int? frame)
        {
            DrawFrame(frame == null ? Frame : (int)frame, Position, Color.White);
        }

        public void DrawFrame(Vector2 screenPos)
        {
            DrawFrame(Frame, screenPos, Color.White);
        }

        public void DrawFrame(int? frame, Vector2 screenPos)
        {
            DrawFrame(frame == null ? Frame : (int)frame, screenPos, Color.White);
        }

        public void DrawFrame(Vector2 screenPos, Color alpha)
        {
            DrawFrame(Frame, screenPos, alpha);
        }

        public void DrawFrame(Color alpha)
        {
            DrawFrame(Frame, Position, alpha);
        }

        public void DrawFrame(int frame, Vector2 screenPos, Color alpha)
        {
            if (Visible && (CircleNumberMax == 0 || CircleNumberMax != 0 && CircleNumber < CircleNumberMax))
            {
                SpriteEffects effect = SpriteEffects.None;
                Vector2 pos = screenPos;
                if (!OrderAsc)
                {
                    effect = SpriteEffects.FlipHorizontally;
                    if (Recs.Length > frame)
                    {
                        pos.X = pos.X - (Recs[frame].Width - 83) + 34;
                    }
                }
                if (Recs.Length > frame)
                {
                    var rec = Recs[frame];

                    Vector2 viewVec = pos;

                    var viewRec = new Rectangle(rec.X, rec.Y, rec.Width, rec.Height);

                    if (LimiteWidth > 0)
                    {
                        if (LimiteOrder)
                        {
                            viewVec = pos + new Vector2(rec.Width - LimiteWidth, 0);

                            viewRec = new Rectangle(rec.X + (rec.Width - LimiteWidth), rec.Y, LimiteWidth, rec.Height);
                        }
                        else
                        {
                            viewRec = new Rectangle(rec.X, rec.Y, LimiteWidth, rec.Height);
                        }
                    }

                    CacheManager.Draw(Asset, viewVec, viewRec, alpha, effect, Scale, Depth);
                }
                if (!String.IsNullOrEmpty(Text))
                {
                    int framePlus = frame % FrameCount;
                    if (TextHigh != null && TextHigh.Length > 0 && TextHigh.Length > framePlus)
                    {
                        CacheManager.DrawString(null, Text, pos + TextPos * Scale + new Vector2(0, TextHigh[framePlus]), Color.White, 0f, Vector2.Zero, Scale, SpriteEffects.None, Depth);
                    }
                }
                //myTexture = CacheManager.Load(asset);
                //CoreGame.Current.spriteBatch.Draw(myTexture, pos, Recs[frame], alpha, 0f, Vector2.Zero, 1f, effect, 1f);
                //if (!MultiPicture)
                //{
                //    //int FrameWidth = myWidth / framecount;
                //    int num = width * frame / myTextures[0].Width;
                //    int plus = (width * frame % myTextures[0].Width) / width;
                //    Rectangle sourcerect = new Rectangle(width * plus, 0, width, myTextures[0].Height);
                //    batch.Draw(myTextures[num].Texture, sourcerect, null, screenPos, Color.White);
                //}
                //else if (myTextures != null)
                //{
                //    batch.Draw(myTextures[frame].Texture, null, screenPos, Color.White);
                //}
            }
        }
        public void Reset()
        {
            Frame = 0;
            TotalElapsed = 0f;
        }
        public void Stop()
        {
            Paused = true;
            Reset();
        }
        public void Play()
        {
            Paused = false;
        }
        //public void Pause()
        //{
        //    Paused = true;
        //}

    }
}
