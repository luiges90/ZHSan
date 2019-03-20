/* AngelCode bitmap font parsing using C#
 * http://www.cyotek.com/blog/angelcode-bitmap-font-parsing-using-csharp
 *
 * Copyright © 2012-2015 Cyotek Ltd.
 *
 * Licensed under the MIT License. See license.txt for the full text.
 */

using System;

namespace Cyotek.Drawing.BitmapFont
{
  /// <summary>
  ///     Represents the font kerning between two characters.
  /// </summary>
  internal struct Kerning : IEquatable<Kerning>
    {
        #region Constructors

        /// <summary>
        ///     Constructor.
        /// </summary>
        /// <param name="firstCharacter">The first character.</param>
        /// <param name="secondCharacter">The second character.</param>
        /// <param name="amount">
        ///     How much the x position should be adjusted when drawing the second
        ///     character immediately following the first.
        /// </param>
        public Kerning(char firstCharacter, char secondCharacter, int amount)
            : this()
        {
            FirstCharacter = firstCharacter;
            SecondCharacter = secondCharacter;
            Amount = amount;
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets or sets how much the x position should be adjusted when drawing the second character immediately following the
        ///     first.
        /// </summary>
        /// <value>
        ///     How much the x position should be adjusted when drawing the second character immediately following the first.
        /// </value>
        public int Amount { get; set; }

        /// <summary>
        ///     Gets or sets the first character.
        /// </summary>
        /// <value>
        ///     The first character.
        /// </value>
        public char FirstCharacter { get; set; }

        /// <summary>
        ///     Gets or sets the second character.
        /// </summary>
        /// <value>
        ///     The second character.
        /// </value>
        public char SecondCharacter { get; set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Returns the fully qualified type name of this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> containing a fully qualified type name.
        /// </returns>
        /// <seealso cref="M:System.ValueType.ToString()" />
        public override string ToString()
        {
            return string.Format("{0} to {1} = {2}", FirstCharacter, SecondCharacter, Amount);
        }

        /// <summary>
        ///     Check if the object represents kerning between the same two characters.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns>
        ///     Whether or not the object represents kerning between the same two characters.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Kerning)) return false;

            return Equals((Kerning) obj);
        }

        /// <summary>
        ///     Check if the other kerning is between the same two characters.
        /// </summary>
        /// <param name="other"></param>
        /// <returns>
        ///     Whether or not the other kerning is between the same two characters.
        /// </returns>
        public bool Equals(Kerning other)
        {
            return FirstCharacter == other.FirstCharacter && SecondCharacter == other.SecondCharacter;
        }

        /// <summary>
        ///     Return the hash code of the kerning between the two characters.
        /// </summary>
        /// <returns>
        ///     A unique hash code of the kerning between the two characters.
        /// </returns>
        public override int GetHashCode()
        {
            return (FirstCharacter << 16) | SecondCharacter;
        }

        #endregion
    }
}