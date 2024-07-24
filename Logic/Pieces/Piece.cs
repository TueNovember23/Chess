using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls.Primitives;
using Chess.Logic.Moves;

namespace Chess.Logic.Pieces
{
	public abstract class Piece
	{
		public GameColor Color { get; }
		public abstract PieceType Type { get; }
		public bool HasMoved { get; set; } = false;

		protected abstract List<Vector> MoveDirections { get; }
		
		public Piece(GameColor color)
		{
			Color = color;
		}

		protected List<Position> GetMovePositionInDirection(Board board, Position piecePosition, Vector direction)
		{
			List<Position> positions = new();
			for(Position position = piecePosition + direction; Board.IsOnBoard(position); position += direction)
			{
				if (board.IsEmpty(position))
				{
					positions.Add(position);
				}
				else if (board[position]?.Color != Color)
				{
					positions.Add(position);
					break;
				}
				else
				{
					break;
				}
			}
			return positions;
		}
		
		protected List<Position> GetMovePositionInDirections(Board board, Position piecePosition)
		{
			List<Position> positions = new();
			foreach(Vector direction in MoveDirections)
			{
				positions.AddRange(GetMovePositionInDirection(board, piecePosition, direction));
			}
			return positions;
		}

		public virtual List<Move> GetMoves(Board board, Position piecePosition)
		{
			List<Move> moves = new();
			List<Position> positions = GetMovePositionInDirections(board, piecePosition);
			foreach(Position position in positions)
			{
				moves.Add(new NormalMove(piecePosition, position));
			}
			return moves;
		}

		public abstract Piece Copy();
	}

	public enum PieceType
	{
		Pawn,
		Knight,
		Bishop,
		Rook,
		Queen,
		King
	}
}
