using System.Collections.Generic;
using Chess.Logic.Moves;

namespace Chess.Logic.Pieces
{
	public class King : Piece
	{
		public King(GameColor color) : base(color)
		{
		}

		public override PieceType Type => PieceType.King;

		protected override List<Vector> MoveDirections => new()
		{
			Vector.Up, Vector.UpRight, Vector.Right, Vector.DownRight, Vector.Down, Vector.DownLeft, Vector.Left, Vector.UpLeft
		};

		public override Piece Copy()
		{
			return new King(Color) { HasMoved = this.HasMoved };
		}

		public override List<Move> GetMoves(Board board, Position piecePosition)
		{
			List<Move> moves = new();
			foreach(Vector direction in MoveDirections)
			{
				Position position = piecePosition + direction;
				if (Board.IsOnBoard(position) && (board[position] == null || board[position]?.Color != Color))
				{
					moves.Add(new NormalMove(piecePosition, position));
				}
			}

			if (!HasMoved && !board.IsInCheck(Color))
			{
				if (board[piecePosition + Vector.Right * 3] is Rook rook && !rook.HasMoved)
				{
					Position rightPos = piecePosition + Vector.Right;
					Position rightPos2 = piecePosition + Vector.Right * 2;
					if (board[rightPos] == null && board[rightPos2] == null && !board.IsUnderAttack(rightPos, Color))
					{
						moves.Add(new CastlingKingSide(piecePosition));
					}
				}
				
				if (board[piecePosition + Vector.Left * 4] is Rook rook2 && !rook2.HasMoved)
				{
					Position leftPos = piecePosition + Vector.Left;
					Position leftPos2 = piecePosition + Vector.Left * 2;
					Position leftPos3 = piecePosition + Vector.Left * 3;
					if (board[leftPos] == null && board[leftPos2] == null && board[leftPos3] == null && !board.IsUnderAttack(leftPos, Color))
					{
						moves.Add(new CastlingQueenSide(piecePosition));
					}
				}
			}

			return moves;
		}
	}
}
