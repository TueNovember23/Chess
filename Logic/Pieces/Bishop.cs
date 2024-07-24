using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Logic.Moves;

namespace Chess.Logic.Pieces
{
	public class Bishop : Piece
	{
		public Bishop(GameColor color) : base(color)
		{
		}

		public override PieceType Type => PieceType.Bishop;

		protected override List<Vector> MoveDirections => new()
		{
			Vector.UpLeft, Vector.UpRight, Vector.DownLeft, Vector.DownRight
		};

		public override Piece Copy()
		{
			return new Bishop(Color) { HasMoved = this.HasMoved };
		}
	}
}
