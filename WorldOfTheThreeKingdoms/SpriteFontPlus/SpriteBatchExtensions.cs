using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteFontPlus
{
	public static class SpriteBatchExtensions
	{
		public static float DrawString(this SpriteBatch batch, DynamicSpriteFont font,
			string _string_, Vector2 pos, Color color)
		{
			return font.DrawString(batch, _string_, pos, color);
		}

		public static float DrawString(this SpriteBatch batch, DynamicSpriteFont font,
			string _string_, Vector2 pos, Color color, Vector2 scale)
		{
			return font.DrawString(batch, _string_, pos, color, scale, 0f);
		}
	}
}