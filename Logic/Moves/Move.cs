using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Logic.Pieces;

namespace Chess.Logic.Moves
{
	public abstract class Move
	{
		public Position From { get; } = new Position();
		public Position To { get; } = new Position();

		public abstract MoveType Type { get; }

		public Move(Position from, Position to)
		{
			From = from;
			To = to;
		}

		public virtual void Execute(Board board)
		{
			if (board[From] is Piece piece)
			{
				piece.HasMoved = true;
				board[To] = piece;
				board[From] = null;
			}
		}

		public virtual bool IsValidMove(Board board)
		{
			if (board[From] == null)
			{
				return false;
			}
			Board copy = board.Copy();
			this.Execute(copy);
			Position kingPos = copy.GetKingPosition(board[From].Color);
			return !copy.IsUnderAttack(kingPos, board[From].Color);
		}
	}

	public enum MoveType
	{
		Normal,
		DoubleSquarePawn,
		Promotion,
		EnPassant,
		CastlingQueenside,
		CastlingKingside
	}
}
