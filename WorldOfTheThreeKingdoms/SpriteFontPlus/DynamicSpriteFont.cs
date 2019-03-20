using FontStashSharp;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteFontPlus
{
	public class DynamicSpriteFont
	{
		private static readonly string DefaultFontName = string.Empty;

		private readonly FontSystem _fontSystem;
		private readonly int _defaultFontId;
		public Texture2D Texture
		{
			get { return _fontSystem.Texture; }
		}

		public float Size
		{
			get
			{
				return _fontSystem.Size;
			}
			set
			{
				_fontSystem.Size = value;
			}
		}

		public float Spacing
		{
			get
			{
				return _fontSystem.Spacing;
			}
			set
			{
				_fontSystem.Spacing = value;
			}
		}

		public int FontId
		{
			get
			{
				return _fontSystem.FontId;
			}
			set
			{
				_fontSystem.FontId = value;
			}
		}

		public int DefaultFontId
		{
			get
			{
				return _defaultFontId;
			}
		}

		private DynamicSpriteFont(byte[] ttf, float defaultSize, int textureWidth, int textureHeight)
		{
			var fontParams = new FontSystemParams
			{
				Width = textureWidth,
				Height = textureHeight,
				Flags = FontSystem.FONS_ZERO_TOPLEFT
			};

			_fontSystem = new FontSystem(fontParams);
			_fontSystem.Alignment = FontSystem.FONS_ALIGN_TOP;

			_defaultFontId = _fontSystem.AddFontMem(DefaultFontName, ttf);
			Size = defaultSize;
		}

		public float DrawString(SpriteBatch batch, string text, Vector2 pos, Color color)
		{
			return DrawString(batch, text, pos, color, Vector2.One, 0f);
		}

		public float DrawString(SpriteBatch batch, string text, Vector2 pos, Color color, Vector2 scale, float depth)
		{
			_fontSystem.Color = color;
			_fontSystem.Scale = scale;

			var result = _fontSystem.DrawText(batch, pos.X, pos.Y, text, depth);

			_fontSystem.Scale = Vector2.One;

			return result;
		}

		public int AddTtf(string name, byte[] ttf)
		{
			return _fontSystem.AddFontMem(name, ttf);
		}

		public int? GetFontIdByName(string name)
		{
			return _fontSystem.GetFontByName(name);
		}

		public Vector2 MeasureString(string text)
		{
			Bounds bounds = new Bounds();
			_fontSystem.TextBounds(0, 0, text, ref bounds);

			return new Vector2(bounds.X2, bounds.Y2);
		}

		public Rectangle GetTextBounds(Vector2 position, string text)
		{
			Bounds bounds = new Bounds();
			_fontSystem.TextBounds(position.X, position.Y, text, ref bounds);

			return new Rectangle((int)bounds.X, (int)bounds.Y, (int)(bounds.X2 - bounds.X), (int)(bounds.Y2 - bounds.Y));
		}

		public static DynamicSpriteFont FromTtf(byte[] ttf, float defaultSize, int textureWidth = 1024, int textureHeight = 1024)
		{
			return new DynamicSpriteFont(ttf, defaultSize, textureWidth, textureHeight);
		}
	}
}