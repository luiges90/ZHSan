using GameManager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GameObjects.Animations
{
    [DataContract]
    public class CombatNumberItem
    {
        [DataMember]
        public CombatNumberKind Kind;
        [DataMember]
        public int Number;
        [DataMember]
        public Point Position;

        public void DrawLeft(CombatNumberGenerator generator, Point start, float scale)
        {
            int x = start.X;
            var rec = new Rectangle?(generator.GetCurrentArrowRectangle(this.Kind, CombatNumberDirection.上));
            CacheManager.Draw(generator.Texture, new Vector2((float) x, start.Y - (generator.DigitHeight * scale)), rec, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.649f);
            int number = this.Number;
            List<int> list = new List<int>();
            if (number == 0)
            {
                list.Add(0);
            }
            else
            {
                while (number > 0)
                {
                    list.Add(number % 10);
                    number /= 10;
                }
            }
            list.Reverse();
            foreach (int num3 in list)
            {
                x += (int) (generator.DigitWidth * scale);
                CacheManager.Draw(generator.Texture, new Vector2((float) x, start.Y - (generator.DigitHeight * scale)), new Rectangle?(generator.GetCurrentDigitRectangle(this.Kind, CombatNumberDirection.上, num3)), Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.649f);
            }
        }

        public void DrawRight(CombatNumberGenerator generator, Point start, float scale)
        {
            int num = start.X - generator.DigitWidth;
            var rec = new Rectangle?(generator.GetCurrentArrowRectangle(this.Kind, CombatNumberDirection.下));
            CacheManager.Draw(generator.Texture, new Vector2((float) num, (float) start.Y), rec, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.649f);
            int number = this.Number;
            int renshuYanseXuhao=0;
            float renshuFangdaBeishu=1f;
            if (this.Kind == CombatNumberKind.人数)
            {
                if (this.Number < 1000)
                {
                    renshuYanseXuhao = 0;
                    renshuFangdaBeishu = 1f;
                }
                else if (this.Number >= 1000 && this.Number < 3000)
                {
                    renshuYanseXuhao = 1;
                    renshuFangdaBeishu = 1.3f;
                }
                else if (this.Number >= 3000 && this.Number < 5000)
                {
                    renshuYanseXuhao = 2;
                    renshuFangdaBeishu = 1.5f;
                }
                else
                {
                    renshuYanseXuhao = 3;
                    renshuFangdaBeishu = 1.8f;
                }
            }
            do
            {
                
                
                if (this.Kind != CombatNumberKind.人数)
                {
                    num -= (int)(generator.DigitWidth * scale);
                    var rec0 = new Rectangle?(generator.GetCurrentDigitRectangle(this.Kind, CombatNumberDirection.下, number % 10));
                    CacheManager.Draw(generator.Texture, new Vector2((float)num , (float)start.Y), rec0, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0.649f);
                }
                else
                {
                    num -= (int)(generator.DigitWidth * scale * renshuFangdaBeishu);
                    var rec0 = new Rectangle ? (generator.GetCurrentDigitRectangle((CombatNumberKind)renshuYanseXuhao, CombatNumberDirection.下, number % 10));
                    CacheManager.Draw(generator.Texture, new Vector2((float)num , (float)start.Y), rec0, Color.White, 0f, Vector2.Zero, renshuFangdaBeishu, SpriteEffects.None, 0.449f);

                }
                number /= 10;
            }
            while (number > 0);
        }
    }
}

