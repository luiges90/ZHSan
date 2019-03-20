/* AngelCode bitmap font parsing using C#
 * http://www.cyotek.com/blog/angelcode-bitmap-font-parsing-using-csharp
 *
 * Copyright © 2012-2015 Cyotek Ltd.
 *
 * Licensed under the MIT License. See license.txt for the full text.
 */

// Some documentation derived from the BMFont file format specification
// http://www.angelcode.com/products/bmfont/doc/file_format.html

using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Cyotek.Drawing.BitmapFont
{
  /// <summary>
  ///     A bitmap font.
  /// </summary>
  /// <seealso cref="T:System.Collections.Generic.IEnumerable{Cyotek.Drawing.BitmapFont.Character}" />
  internal class BitmapFont : IEnumerable<Character>
    {
        #region Constants

        /// <summary>
        ///     When used with <see cref="MeasureFont(string,double)" />, specifies that no wrapping should occur.
        /// </summary>
        public const int NoMaxWidth = -1;

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets the alpha channel.
        /// </summary>
        /// <value>
        ///     The alpha channel.
        /// </value>
        /// <remarks>
        ///     Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the
        ///     outline, 3 if its set to zero, and 4 if its set to one.
        /// </remarks>
        public int AlphaChannel { get; set; }

        /// <summary>
        ///     Gets or sets the number of pixels from the absolute top of the line to the base of the characters.
        /// </summary>
        /// <value>
        ///     The number of pixels from the absolute top of the line to the base of the characters.
        /// </value>
        public int BaseHeight { get; set; }

        /// <summary>
        ///     Gets or sets the blue channel.
        /// </summary>
        /// <value>
        ///     The blue channel.
        /// </value>
        /// <remarks>
        ///     Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the
        ///     outline, 3 if its set to zero, and 4 if its set to one.
        /// </remarks>
        public int BlueChannel { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the font is bold.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is bold, otherwise <c>false</c>.
        /// </value>
        public bool Bold { get; set; }

        /// <summary>
        ///     Gets or sets the characters that comprise the font.
        /// </summary>
        /// <value>
        ///     The characters that comprise the font.
        /// </value>
        public IDictionary<char, Character> Characters { get; set; }

        /// <summary>
        ///     Gets or sets the name of the OEM charset used.
        /// </summary>
        /// <value>
        ///     The name of the OEM charset used (when not unicode).
        /// </value>
        public string Charset { get; set; }

        /// <summary>
        ///     Gets or sets the name of the true type font.
        /// </summary>
        /// <value>
        ///     The font family name.
        /// </value>
        public string FamilyName { get; set; }

        /// <summary>
        ///     Gets or sets the size of the font.
        /// </summary>
        /// <value>
        ///     The size of the font.
        /// </value>
        public int FontSize { get; set; }

        /// <summary>
        ///     Gets or sets the green channel.
        /// </summary>
        /// <value>
        ///     The green channel.
        /// </value>
        /// <remarks>
        ///     Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the
        ///     outline, 3 if its set to zero, and 4 if its set to one.
        /// </remarks>
        public int GreenChannel { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the font is italic.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is italic, otherwise <c>false</c>.
        /// </value>
        public bool Italic { get; set; }

        /// <summary>
        ///     Indexer to get items within this collection using array index syntax.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <returns>
        ///     The indexed item.
        /// </returns>
        public Character this[char character] => Characters[character];

        /// <summary>
        ///     Gets or sets the character kernings for the font.
        /// </summary>
        /// <value>
        ///     The character kernings for the font.
        /// </value>
        public IDictionary<Kerning, int> Kernings { get; set; }

        /// <summary>
        ///     Gets or sets the distance in pixels between each line of text.
        /// </summary>
        /// <value>
        ///     The distance in pixels between each line of text.
        /// </value>
        public int LineHeight { get; set; }

        /// <summary>
        ///     Gets or sets the outline thickness for the characters.
        /// </summary>
        /// <value>
        ///     The outline thickness for the characters.
        /// </value>
        public int OutlineSize { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the monochrome characters have been packed into each of the texture
        ///     channels.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the characters are packed, otherwise <c>false</c>.
        /// </value>
        /// <remarks>
        ///     When packed, the <see cref="AlphaChannel" /> property describes what is stored in each channel.
        /// </remarks>
        public bool Packed { get; set; }

        /// <summary>
        ///     Gets or sets the padding for each character.
        /// </summary>
        /// <value>
        ///     The padding for each character.
        /// </value>
        public Padding Padding { get; set; }

        /// <summary>
        ///     Gets or sets the texture pages for the font.
        /// </summary>
        /// <value>
        ///     The pages.
        /// </value>
        public Page[] Pages { get; set; }

        /// <summary>
        ///     Gets or sets the red channel.
        /// </summary>
        /// <value>
        ///     The red channel.
        /// </value>
        /// <remarks>
        ///     Set to 0 if the channel holds the glyph data, 1 if it holds the outline, 2 if it holds the glyph and the
        ///     outline, 3 if its set to zero, and 4 if its set to one.
        /// </remarks>
        public int RedChannel { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the font is smoothed.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is smoothed, otherwise <c>false</c>.
        /// </value>
        public bool Smoothed { get; set; }

        /// <summary>
        ///     Gets or sets the spacing for each character.
        /// </summary>
        /// <value>
        ///     The spacing for each character.
        /// </value>
        public Point Spacing { get; set; }

        /// <summary>
        ///     Gets or sets the font height stretch.
        /// </summary>
        /// <value>
        ///     The font height stretch.
        /// </value>
        /// <remarks>100% means no stretch.</remarks>
        public int StretchedHeight { get; set; }

        /// <summary>
        ///     Gets or sets the level of super sampling used by the font.
        /// </summary>
        /// <value>
        ///     The super sampling level of the font.
        /// </value>
        /// <remarks>A value of 1 indicates no super sampling is in use.</remarks>
        public int SuperSampling { get; set; }

        /// <summary>
        ///     Gets or sets the size of the texture images used by the font.
        /// </summary>
        /// <value>
        ///     The size of the texture.
        /// </value>
        public Size TextureSize { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the font is unicode.
        /// </summary>
        /// <value>
        ///     <c>true</c> if the font is unicode, otherwise <c>false</c>.
        /// </value>
        public bool Unicode { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Gets the kerning for the specified character combination.
        /// </summary>
        /// <param name="previous">The previous character.</param>
        /// <param name="current">The current character.</param>
        /// <returns>
        ///     The spacing between the specified characters.
        /// </returns>
        public int GetKerning(char previous, char current)
        {
            Kerning key;
            int result;

            key = new Kerning(previous, current, 0);

            if (!Kernings.TryGetValue(key, out result)) result = 0;

            return result;
        }

        /// <summary>
        ///     Load font information from the specified <see cref="Stream" />.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have unsupported or
        ///     illegal values.
        /// </exception>
        /// <exception cref="InvalidDataException">Thrown when an Invalid Data error condition occurs.</exception>
        /// <param name="stream">The stream to load.</param>
        public virtual void Load(Stream stream)
        {
            byte[] buffer;
            string header;

            if (stream == null) throw new ArgumentNullException("stream");

            if (!stream.CanSeek)
                throw new ArgumentException("Stream must be seekable in order to determine file format.", "stream");

            // read the first five bytes so we can try and work out what the format is
            // then reset the position so the format loaders can work
            buffer = new byte[5];
            stream.Read(buffer, 0, 5);
            stream.Seek(0, SeekOrigin.Begin);
            header = Encoding.ASCII.GetString(buffer);

            switch (header)
            {
                case "info ":
                    LoadText(stream);
                    break;
                case "<?xml":
                    LoadXml(stream);
                    break;
                default:
                    throw new InvalidDataException("Unknown file format.");
            }
        }

        /// <summary>
        ///     Load font information from the specified file.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <exception cref="FileNotFoundException">Thrown when the requested file is not present.</exception>
        /// <param name="fileName">The file name to load.</param>
        public void Load(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) throw new ArgumentNullException("fileName");

            if (!File.Exists(fileName))
                throw new FileNotFoundException(string.Format("Cannot find file '{0}'.", fileName), fileName);

            using (Stream stream = File.OpenRead(fileName))
            {
                Load(stream);
            }

            BitmapFontLoader.QualifyResourcePaths(this, Path.GetDirectoryName(fileName));
        }

        /// <summary>
        ///     Loads font information from the specified string.
        /// </summary>
        /// <param name="text">String containing the font to load.</param>
        /// <remarks>The source data must be in BMFont text format.</remarks>
        public void LoadText(string text)
        {
            using (var reader = new StringReader(text))
            {
                LoadText(reader);
            }
        }

        /// <summary>
        ///     Loads font information from the specified stream.
        /// </summary>
        /// <remarks>
        ///     The source data must be in BMFont text format.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="stream">The stream containing the font to load.</param>
        public void LoadText(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            using (TextReader reader = new StreamReader(stream))
            {
                LoadText(reader);
            }
        }

        /// <summary>
        ///     Loads font information from the specified <see cref="TextReader" />.
        /// </summary>
        /// <remarks>
        ///     The source data must be in BMFont text format.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="reader">The <strong>TextReader</strong> used to feed the data into the font.</param>
        public virtual void LoadText(TextReader reader)
        {
            IDictionary<int, Page> pageData;
            IDictionary<Kerning, int> kerningDictionary;
            IDictionary<char, Character> charDictionary;
            string line;

            if (reader == null) throw new ArgumentNullException("reader");

            pageData = new SortedDictionary<int, Page>();
            kerningDictionary = new Dictionary<Kerning, int>();
            charDictionary = new Dictionary<char, Character>();

            do
            {
                line = reader.ReadLine();

                if (line != null)
                {
                    string[] parts;

                    parts = BitmapFontLoader.Split(line, ' ');

                    if (parts.Length != 0)
                        switch (parts[0])
                        {
                            case "info":
                                FamilyName = BitmapFontLoader.GetNamedString(parts, "face");
                                FontSize = BitmapFontLoader.GetNamedInt(parts, "size");
                                Bold = BitmapFontLoader.GetNamedBool(parts, "bold");
                                Italic = BitmapFontLoader.GetNamedBool(parts, "italic");
                                Charset = BitmapFontLoader.GetNamedString(parts, "charset");
                                Unicode = BitmapFontLoader.GetNamedBool(parts, "unicode");
                                StretchedHeight = BitmapFontLoader.GetNamedInt(parts, "stretchH");
                                Smoothed = BitmapFontLoader.GetNamedBool(parts, "smooth");
                                SuperSampling = BitmapFontLoader.GetNamedInt(parts, "aa");
                                Padding = BitmapFontLoader.ParsePadding(
                                    BitmapFontLoader.GetNamedString(parts, "padding"));
                                Spacing = BitmapFontLoader.ParsePoint(
                                    BitmapFontLoader.GetNamedString(parts, "spacing"));
                                OutlineSize = BitmapFontLoader.GetNamedInt(parts, "outline");
                                break;
                            case "common":
                                LineHeight = BitmapFontLoader.GetNamedInt(parts, "lineHeight");
                                BaseHeight = BitmapFontLoader.GetNamedInt(parts, "base");
                                TextureSize = new Size(BitmapFontLoader.GetNamedInt(parts, "scaleW"),
                                    BitmapFontLoader.GetNamedInt(parts, "scaleH"));
                                Packed = BitmapFontLoader.GetNamedBool(parts, "packed");
                                AlphaChannel = BitmapFontLoader.GetNamedInt(parts, "alphaChnl");
                                RedChannel = BitmapFontLoader.GetNamedInt(parts, "redChnl");
                                GreenChannel = BitmapFontLoader.GetNamedInt(parts, "greenChnl");
                                BlueChannel = BitmapFontLoader.GetNamedInt(parts, "blueChnl");
                                break;
                            case "page":
                                int id;
                                string name;

                                id = BitmapFontLoader.GetNamedInt(parts, "id");
                                name = BitmapFontLoader.GetNamedString(parts, "file");

                                pageData.Add(id, new Page(id, name));
                                break;
                            case "char":
                                Character charData;

                                charData = new Character
                                {
                                    Char = (char) BitmapFontLoader.GetNamedInt(parts, "id"),
                                    Bounds =
                                        new Rectangle(BitmapFontLoader.GetNamedInt(parts, "x"),
                                            BitmapFontLoader.GetNamedInt(parts, "y"),
                                            BitmapFontLoader.GetNamedInt(parts, "width"),
                                            BitmapFontLoader.GetNamedInt(parts, "height")),
                                    Offset =
                                        new Point(BitmapFontLoader.GetNamedInt(parts, "xoffset"),
                                            BitmapFontLoader.GetNamedInt(parts, "yoffset")),
                                    XAdvance = BitmapFontLoader.GetNamedInt(parts, "xadvance"),
                                    TexturePage = BitmapFontLoader.GetNamedInt(parts, "page"),
                                    Channel = BitmapFontLoader.GetNamedInt(parts, "chnl")
                                };
                                charDictionary.Add(charData.Char, charData);
                                break;
                            case "kerning":
                                Kerning key;

                                key = new Kerning((char) BitmapFontLoader.GetNamedInt(parts, "first"),
                                    (char) BitmapFontLoader.GetNamedInt(parts, "second"),
                                    BitmapFontLoader.GetNamedInt(parts, "amount"));

                                if (!kerningDictionary.ContainsKey(key)) kerningDictionary.Add(key, key.Amount);
                                break;
                        }
                }
            } while (line != null);

            Pages = BitmapFontLoader.ToArray(pageData.Values);
            Characters = charDictionary;
            Kernings = kerningDictionary;
        }

        /// <summary>
        ///     Loads font information from the specified string.
        /// </summary>
        /// <param name="xml">String containing the font to load.</param>
        /// <remarks>The source data must be in BMFont XML format.</remarks>
        public void LoadXml(string xml)
        {
            using (var reader = new StringReader(xml))
            {
                LoadXml(reader);
            }
        }

        /// <summary>
        ///     Loads font information from the specified <see cref="TextReader" />.
        /// </summary>
        /// <remarks>
        ///     The source data must be in BMFont XML format.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="reader">The <strong>TextReader</strong> used to feed the data into the font.</param>
        public virtual void LoadXml(TextReader reader)
        {
            XmlDocument document;
            IDictionary<int, Page> pageData;
            IDictionary<Kerning, int> kerningDictionary;
            IDictionary<char, Character> charDictionary;
            XmlNode root;
            XmlNode properties;

            if (reader == null) throw new ArgumentNullException("reader");

            document = new XmlDocument();
            pageData = new SortedDictionary<int, Page>();
            kerningDictionary = new Dictionary<Kerning, int>();
            charDictionary = new Dictionary<char, Character>();

            document.Load(reader);
            root = document.DocumentElement;

            // load the basic attributes
            properties = root.SelectSingleNode("info");
            FamilyName = properties.Attributes["face"].Value;
            FontSize = Convert.ToInt32(properties.Attributes["size"].Value);
            Bold = Convert.ToInt32(properties.Attributes["bold"].Value) != 0;
            Italic = Convert.ToInt32(properties.Attributes["italic"].Value) != 0;
            Unicode = Convert.ToInt32(properties.Attributes["unicode"].Value) != 0;
            StretchedHeight = Convert.ToInt32(properties.Attributes["stretchH"].Value);
            Charset = properties.Attributes["charset"].Value;
            Smoothed = Convert.ToInt32(properties.Attributes["smooth"].Value) != 0;
            SuperSampling = Convert.ToInt32(properties.Attributes["aa"].Value);
            Padding = BitmapFontLoader.ParsePadding(properties.Attributes["padding"].Value);
            Spacing = BitmapFontLoader.ParsePoint(properties.Attributes["spacing"].Value);
            OutlineSize = Convert.ToInt32(properties.Attributes["outline"].Value);

            // common attributes
            properties = root.SelectSingleNode("common");
            BaseHeight = Convert.ToInt32(properties.Attributes["base"].Value);
            LineHeight = Convert.ToInt32(properties.Attributes["lineHeight"].Value);
            TextureSize = new Size(Convert.ToInt32(properties.Attributes["scaleW"].Value),
                Convert.ToInt32(properties.Attributes["scaleH"].Value));
            Packed = Convert.ToInt32(properties.Attributes["packed"].Value) != 0;
            AlphaChannel = Convert.ToInt32(properties.Attributes["alphaChnl"].Value);
            RedChannel = Convert.ToInt32(properties.Attributes["redChnl"].Value);
            GreenChannel = Convert.ToInt32(properties.Attributes["greenChnl"].Value);
            BlueChannel = Convert.ToInt32(properties.Attributes["blueChnl"].Value);

            // load texture information
            foreach (XmlNode node in root.SelectNodes("pages/page"))
            {
                Page page;

                page = new Page();
                page.Id = Convert.ToInt32(node.Attributes["id"].Value);
                page.FileName = node.Attributes["file"].Value;

                pageData.Add(page.Id, page);
            }

            Pages = BitmapFontLoader.ToArray(pageData.Values);

            // load character information
            foreach (XmlNode node in root.SelectNodes("chars/char"))
            {
                Character character;

                character = new Character();
                character.Char = (char) Convert.ToInt32(node.Attributes["id"].Value);
                character.Bounds = new Rectangle(Convert.ToInt32(node.Attributes["x"].Value),
                    Convert.ToInt32(node.Attributes["y"].Value),
                    Convert.ToInt32(node.Attributes["width"].Value),
                    Convert.ToInt32(node.Attributes["height"].Value));
                character.Offset = new Point(Convert.ToInt32(node.Attributes["xoffset"].Value),
                    Convert.ToInt32(node.Attributes["yoffset"].Value));
                character.XAdvance = Convert.ToInt32(node.Attributes["xadvance"].Value);
                character.TexturePage = Convert.ToInt32(node.Attributes["page"].Value);
                character.Channel = Convert.ToInt32(node.Attributes["chnl"].Value);

                charDictionary.Add(character.Char, character);
            }

            Characters = charDictionary;

            // loading kerning information
            foreach (XmlNode node in root.SelectNodes("kernings/kerning"))
            {
                Kerning key;

                key = new Kerning((char) Convert.ToInt32(node.Attributes["first"].Value),
                    (char) Convert.ToInt32(node.Attributes["second"].Value),
                    Convert.ToInt32(node.Attributes["amount"].Value));

                if (!kerningDictionary.ContainsKey(key)) kerningDictionary.Add(key, key.Amount);
            }

            Kernings = kerningDictionary;
        }

        /// <summary>
        ///     Loads font information from the specified stream.
        /// </summary>
        /// <remarks>
        ///     The source data must be in BMFont XML format.
        /// </remarks>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="stream">The stream containing the font to load.</param>
        public void LoadXml(Stream stream)
        {
            if (stream == null) throw new ArgumentNullException("stream");

            using (TextReader reader = new StreamReader(stream))
            {
                LoadXml(reader);
            }
        }

        /// <summary>
        ///     Provides the size, in pixels, of the specified text when drawn with this font.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <returns>
        ///     The <see cref="Size" />, in pixels, of <paramref name="text" /> drawn with this font.
        /// </returns>
        public Size MeasureFont(string text)
        {
            return MeasureFont(text, NoMaxWidth);
        }

        /// <summary>
        ///     Provides the size, in pixels, of the specified text when drawn with this font, automatically wrapping to keep
        ///     within the specified with.
        /// </summary>
        /// <param name="text">The text to measure.</param>
        /// <param name="maxWidth">The maximum width.</param>
        /// <returns>
        ///     The <see cref="Size" />, in pixels, of <paramref name="text" /> drawn with this font.
        /// </returns>
        /// <remarks>
        ///     The MeasureText method uses the <paramref name="maxWidth" /> parameter to automatically wrap when determining
        ///     text size.
        /// </remarks>
        public Size MeasureFont(string text, double maxWidth)
        {
            Size result;

            if (!string.IsNullOrEmpty(text))
            {
                char previousCharacter;
                int currentLineWidth;
                int currentLineHeight;
                int blockWidth;
                int blockHeight;
                int length;
                List<int> lineHeights;

                length = text.Length;
                previousCharacter = ' ';
                currentLineWidth = 0;
                currentLineHeight = LineHeight;
                blockWidth = 0;
                blockHeight = 0;
                lineHeights = new List<int>();

                for (var i = 0; i < length; i++)
                {
                    char character;

                    character = text[i];

                    if (character == '\n' || character == '\r')
                    {
                        if (character == '\n' || i + 1 == length || text[i + 1] != '\n')
                        {
                            lineHeights.Add(currentLineHeight);
                            blockWidth = Math.Max(blockWidth, currentLineWidth);
                            currentLineWidth = 0;
                            currentLineHeight = LineHeight;
                        }
                    }
                    else
                    {
                        Character data;
                        int width;

                        data = this[character];
                        width = data.XAdvance + GetKerning(previousCharacter, character);

                        if (maxWidth != NoMaxWidth && currentLineWidth + width >= maxWidth)
                        {
                            lineHeights.Add(currentLineHeight);
                            blockWidth = Math.Max(blockWidth, currentLineWidth);
                            currentLineWidth = 0;
                            currentLineHeight = LineHeight;
                        }

                        currentLineWidth += width;
                        currentLineHeight = Math.Max(currentLineHeight, data.Bounds.Height + data.Offset.Y);
                        previousCharacter = character;
                    }
                }

                // finish off the current line if required
                if (currentLineHeight != 0) lineHeights.Add(currentLineHeight);

                // reduce any lines other than the last back to the base
                for (var i = 0; i < lineHeights.Count - 1; i++) lineHeights[i] = LineHeight;

                // calculate the final block height
                foreach (var lineHeight in lineHeights) blockHeight += lineHeight;

                result = new Size(Math.Max(currentLineWidth, blockWidth), blockHeight);
            }
            else
            {
                result = Size.Empty;
            }

            return result;
        }

        #endregion

        #region IEnumerable<Character> Interface

        /// <summary>
        ///     Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through
        ///     the collection.
        /// </returns>
        /// <seealso cref="M:System.Collections.Generic.IEnumerable{Cyotek.Drawing.BitmapFont.Character}.GetEnumerator()" />
        public IEnumerator<Character> GetEnumerator()
        {
            foreach (var pair in Characters) yield return pair.Value;
        }

        /// <summary>
        ///     Gets the enumerator.
        /// </summary>
        /// <returns>
        ///     The enumerator.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}