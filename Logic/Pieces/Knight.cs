using System;
using System.Collections.Generic;
using Chess.Logic.Moves;

namespace Chess.Logic.Pieces
{
	public class Knight : Piece
	{
		public Knight(GameColor color) : base(color)
		{
		}

		public override PieceType Type => PieceType.Knight;

		protected override List<Vector> MoveDirections => new() { 
			2*Vector.Up+Vector.Left,
			2*Vector.Up+Vector.Right,
			2*Vector.Down+Vector.Left,
			2*Vector.Down+Vector.Right,
			2*Vector.Left+Vector.Up,
			2*Vector.Left+Vector.Down,
			2*Vector.Right+Vector.Up,
			2*Vector.Right+Vector.Down
		};

		public override Piece Copy()
		{
			return new Knight(Color) { HasMoved = this.HasMoved };
		}

		public override List<Move> GetMoves(Board board, Position piecePosition)
		{
			List<Move> moves = new();
			foreach(Vector direction in MoveDirections)
			{
				Position position = piecePosition + direction;
				if (Board.IsOnBoard(position) && ((board[position] == null) || (board[position]?.Color != Color)))
				{
					moves.Add(new NormalMove(piecePosition, position));	
				}
			}
			return moves;
		}
	}
}
