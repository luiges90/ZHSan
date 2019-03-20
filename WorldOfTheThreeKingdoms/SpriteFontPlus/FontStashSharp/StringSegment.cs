namespace FontStashSharp
{
	internal struct StringSegment
	{
		public static readonly StringSegment Null;

		public string String;
		public int Location;
		public int Length;

		public char this[int index]
		{
			get
			{
				return String[Location + index];
			}
		}

		public bool IsNullOrEmpty
		{
			get
			{
				if (String == null)
				{
					return true;
				}

				return Location >= String.Length;
			}
		}

		public StringSegment(StringSegment s, int location)
		{
			String = s.String;
			Length = s.Length;
			Location = location;
		}

		public StringSegment(StringSegment s, int location, int length)
		{
			String = s.String;
			Length = length;
			Location = location;
		}

		public static implicit operator StringSegment(string value)
		{
			return new StringSegment
			{
				String = value,
				Location = 0,
				Length = value != null ? value.Length : 0
			};
		}

		public static bool operator ==(StringSegment a, StringSegment b)
		{
			return object.ReferenceEquals(a.String, b.String) &&
				a.Location == b.Location &&
				a.Length == b.Length;
		}

		public static bool operator !=(StringSegment a, StringSegment b)
		{
			return !(a == b);
		}

		public static StringSegment operator +(StringSegment a, int loc)
		{
			return new StringSegment(a, a.Location + loc);
		}

		public void Reset()
		{
			String = null;
			Location = Length = 0;
		}
	}
}
