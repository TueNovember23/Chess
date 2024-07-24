using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Logic.Moves;
namespace Chess.Logic.Pieces
{
	public class Rook : Piece
	{
		public Rook(GameColor color) : base(color)
		{
		}

		public override PieceType Type => PieceType.Rook;

		protected override List<Vector> MoveDirections => new() { 
			Vector.Up, Vector.Down, Vector.Left, Vector.Right 
		};

		public override Piece Copy()
		{
			return new Rook(Color) { HasMoved = this.HasMoved };
		}
	}
}
