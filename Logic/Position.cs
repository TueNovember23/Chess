using System;

namespace Chess.Logic
{
	public class Position
	{
		public int Row { get; }
		public int Column { get; }

		public Position()
		{
			Row = 0;
			Column = 0;
		}

		public Position(int row, int column)
		{
			Row = row;
			Column = column;
		}

		public GameColor Color()
		{
			return (Row + Column) % 2 == 0 ? GameColor.White : GameColor.Black;
		}

		public override bool Equals(object? obj)
		{
			return obj is Position position &&
				   Row == position.Row &&
				   Column == position.Column;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Row, Column);
		}

		public static Position operator +(Position a, Vector b)
		{
			return new Position(a.Row + b.Vertical, a.Column + b.Horizontal);
		}
	}
}
