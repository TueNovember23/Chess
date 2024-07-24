using Chess.Logic.Moves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chess.Logic.Pieces
{
	public class Pawn : Piece
	{
		public Pawn(GameColor color) : base(color)
		{
		}

		public override PieceType Type => PieceType.Pawn;

		protected override List<Vector> MoveDirections => new() { Forward };

		private Vector Forward => Color == GameColor.White ? Vector.Up : Vector.Down;

		private bool CanCaptureAt(Board board, Position position)
		{
			return !board.IsEmpty(position) && board[position]?.Color != Color;
		}	

		private bool CanMoveTo(Board board, Position position)
		{
			return board.IsEmpty(position);
		}

		private bool CanPromotion(Position position)
		{
			return position.Row == (Color == GameColor.White ? 0 : 7);
		}

		private bool CanEnPassent(Board board, Position nextToPos)
		{
			return board[nextToPos] is Pawn && board[nextToPos].Color != Color;
		}

		public override List<Move> GetMoves(Board board, Position piecePosition)
		{
			Position forward = piecePosition + Forward;
			Position leftForwardPos = forward + Vector.Left;
			Position rightForwardPos = forward + Vector.Right;
			List<Move> moves = new();
			if(Board.IsOnBoard(forward) && CanMoveTo(board, forward))
			{
				if (CanPromotion(forward))
				{
					moves.Add(new Promotion(piecePosition, forward));
				}
				else
				{
					moves.Add(new NormalMove(piecePosition, forward));
				}
				if (!HasMoved)
				{
					Position doubleForward = forward + Forward;
					if (Board.IsOnBoard(doubleForward) && CanMoveTo(board, doubleForward))
					{
						moves.Add(new DoubleSquarePawn(piecePosition, doubleForward));
					}
				}
			}
			if(Board.IsOnBoard(leftForwardPos) && CanCaptureAt(board, leftForwardPos))
			{
				if (CanPromotion(leftForwardPos))
				{
					moves.Add(new Promotion(piecePosition, leftForwardPos));
				}
				else
				{
					moves.Add(new NormalMove(piecePosition, leftForwardPos));
				}
			}
			if (Board.IsOnBoard(rightForwardPos) && CanCaptureAt(board, rightForwardPos))
			{
				if(CanPromotion(rightForwardPos))
				{
					moves.Add(new Promotion(piecePosition, rightForwardPos));
				}
				else
				{
					moves.Add(new NormalMove(piecePosition, rightForwardPos));
				}
			}
			Position leftPos = piecePosition + Vector.Left;
			Position rightPos = piecePosition + Vector.Right;
			if (Board.IsOnBoard(leftPos) && CanEnPassent(board, leftPos) && Board.IsOnBoard(leftForwardPos))
			{
				moves.Add(new EnPassent(piecePosition, leftForwardPos));
			}
			if (Board.IsOnBoard(rightPos) && CanEnPassent(board, rightPos) && Board.IsOnBoard(rightForwardPos))
			{
				moves.Add(new EnPassent(piecePosition, rightForwardPos));
			}
			return moves;
		}

		public override Piece Copy()
		{
			return new Pawn(Color) { HasMoved = this.HasMoved };
		}
	}
}
