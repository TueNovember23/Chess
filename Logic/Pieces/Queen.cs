using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Logic.Pieces
{
	public class Queen : Piece
	{
		public Queen(GameColor color) : base(color)
		{
		}

		public override PieceType Type => PieceType.Queen;

		protected override List<Vector> MoveDirections => new()
		{
			Vector.Up, Vector.Down, Vector.Left, Vector.Right,
			Vector.UpLeft, Vector.UpRight, Vector.DownLeft, Vector.DownRight
		};

		public override Piece Copy()
		{
			return new Queen(Color) { HasMoved = this.HasMoved};
		}
	}
}
