using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameGlobal;
using GameObjects;
using Microsoft.Xna.Framework;
using WorldOfTheThreeKingdoms;
using Microsoft.Xna.Framework.Graphics;
using GameManager;

namespace WorldOfTheThreeKingdoms.GameScreens.ScreenLayers
{
    public class CloudLayer
    {
        //Vector2[] vecs = new Vector2[] {
        //    new Vector2(230, 150),
        //    new Vector2(90, 150),
        //    new Vector2(-90, 150),
        //    new Vector2(230, 60),
        //    new Vector2(90, 60),
        //    new Vector2(-90, 60),
        //    new Vector2(230, -90),
        //    new Vector2(90, -90),
        //    new Vector2(-90, -90) };

        Vector2[] vecs = new Vector2[] {
            new Vector2(230 + 350, 150 + 200),   //右下
            new Vector2(90 + 250, 150 + 250),    //中下
            new Vector2(-90 - 200, 150 + 250),   //左下
            new Vector2(230 + 350, 60 + 150),    //右中
            new Vector2(90 + 200, 60 + 150),     //中中
            new Vector2(-90 - 200, 60 + 150),    //左中
            new Vector2(230 + 350, -90 - 100),   //右上
            new Vector2(90 + 200, -90 - 100),    //中上
            new Vector2(-90 - 200, -90 - 100) }; //左上

        Vector2[] vecsReal = null;

        float cloudAlpha = 1f;

        float elapsedTime = 0f;

        float delayTime = 1f;

        Vector2 scale;

        public bool IsVisible = false;

        public bool IsStart = false;

        public bool Reverse = false;

        public CloudLayer()
        {
            scale = new Vector2(Convert.ToSingle(Session.ResolutionX) / 800f, Convert.ToSingle(Session.ResolutionY) / 480f);

            if (DantiaoLayer.Persons != null && DantiaoLayer.Persons.Count >= 2)
            {
                Reverse = true;
            }

        }

        public void Start()
        {
            cloudAlpha = 1f;
            elapsedTime = 0f;
            IsVisible = true;

            vecsReal = new Vector2[] {
            new Vector2(230 + 350, 150 + 200),   //右下
            new Vector2(90 + 250, 150 + 250),    //中下
            new Vector2(-90 - 200, 150 + 250),   //左下
            new Vector2(230 + 350, 60 + 150),    //右中
            new Vector2(90 + 200, 60 + 150),     //中中
            new Vector2(-90 - 200, 60 + 150),    //左中
            new Vector2(230 + 350, -90 - 100),   //右上
            new Vector2(90 + 200, -90 - 100),    //中上
            new Vector2(-90 - 200, -90 - 100) }; //左上
        }

        public void Update(float gameTime)
        {
            if (IsVisible && IsStart)
            {
                if (delayTime > 0)
                {
                    delayTime -= gameTime;

                    if (delayTime <= 0)
                    {
                        delayTime = 0;
                    }

                    return;
                }

                elapsedTime += gameTime;

                float elapsedTime2 = 0f;

                if (Reverse)
                {
                    elapsedTime2 = elapsedTime >= 1 ? 0 : 1 - elapsedTime;
                }
                else
                {
                    elapsedTime2 = elapsedTime < 1f ? elapsedTime : 1f;
                }

                cloudAlpha = 1 - elapsedTime2;

                for (int i = 0; i < vecsReal.Length; i++)
                {
                    var vec = vecs[i];

                    float ratio = 2f;

                    if (Platforms.Platform.PlatFormType == Platforms.PlatFormType.iOS)
                    {
                        ratio = 4f;
                    }

                    if (i <= 2)
                    {
                        vecsReal[i] = vec + new Vector2(0, 391f * elapsedTime2 / ratio);
                    }
                    else if (i == 3)
                    {
                        vecsReal[i] = vec + new Vector2(665f * elapsedTime2 / ratio, 0);
                    }
                    else if (i == 4)
                    {

                    }
                    else if (i == 5)
                    {
                        vecsReal[i] = vec + new Vector2(-665f * elapsedTime2 / ratio, 0);
                    }
                    else
                    {
                        vecsReal[i] = vec + new Vector2(0, -391f * elapsedTime2 / ratio);
                    }

                }

                if (Reverse && cloudAlpha >= 1)
                {
                    IsStart = false;

                    if (DantiaoLayer.Persons != null && DantiaoLayer.Persons.Count >= 2 && Session.MainGame.mainGameScreen.dantiaoLayer == null)
                    {
                        if (DantiaoLayer.Persons == null)
                        {

                        }
                        else
                        {
                            Session.MainGame.mainGameScreen.dantiaoLayer = new DantiaoLayer(DantiaoLayer.Persons[DantiaoLayer.Persons.Count - 2], DantiaoLayer.Persons[DantiaoLayer.Persons.Count - 1]);
                        }
                    }

                }
                else if (!Reverse && cloudAlpha <= 0)
                {
                    IsStart = false;
                    IsVisible = false;
                    Reverse = false;
                }
            }
        }

        public void Draw()
        {
            if (Reverse && delayTime <= 0 || !Reverse && IsVisible)
            {
                float depth = Reverse ? 0.1f - 0.005f : 0.798f;

                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[0], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[0], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[0], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[0], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[0], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[0], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);


                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[1], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[1], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[1], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[1], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[1], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[1], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);


                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[2], null, Color.White * cloudAlpha, SpriteEffects.None, Vector2.One, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[2], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[2], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[2], null, Color.White * cloudAlpha, SpriteEffects.None, Vector2.One, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[2], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[2], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);


                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[3], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[3], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[3], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[3], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[3], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[3], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[3], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[3], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);


                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[4], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[4], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[4], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[4], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[4], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[4], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);


                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[5], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[5], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[5], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[5], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[5], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[5], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[5], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);


                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[6], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[6], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[6], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[6], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[6], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[6], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[6], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);

                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[7], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[7], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[7], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[7], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[7], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[7], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);

                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud1", vecsReal[8], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[8], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud2", vecsReal[8], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud3", vecsReal[8], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud4", vecsReal[8], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud5", vecsReal[8], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[8], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
                CacheManager.Draw(@"Content\Textures\Resources\Start\Cloud6", vecsReal[8], null, Color.White * cloudAlpha, SpriteEffects.None, scale, depth);
            }
        }

    }

}
