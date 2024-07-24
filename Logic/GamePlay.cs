using System.Collections.Generic;
using System.Linq;
using Chess.Logic.Pieces;
using Chess.Logic.Moves;
using System;

namespace Chess.Logic
{
	public class GamePlay
	{
		public Board Board { get; } = new Board();
		public GameColor CurrentPlayer { get; set; } = GameColor.White;
		public Result? Result { get; private set; } = null;
		public Move? PreviousMove { get; set; } = null;
		
		public GameColor Opponent { get => GameColor.White == CurrentPlayer ? GameColor.Black : GameColor.White; }

		public bool WhiteDrawOffered { get; private set; } = false;
		public bool BlackDrawOffered { get; private set; } = false;

		private int countFiftyMoveRule = 0;

		public GamePlay()
		{
			Board = new();
			CurrentPlayer = GameColor.White;
			Result = null;
			PreviousMove = null;
		}

		public List<Move> GetMovesForPiece(Position position)
		{
			Piece? piece = Board[position];
			if (piece?.Color != CurrentPlayer)
			{
				return new();
			}
			return piece.GetMoves(Board, position).Where(_ => _.IsValidMove(Board)).ToList();
		}

		public void ExecuteMove(Move move)
		{
			Board.PawnSkipPos = null;
			move.Execute(Board);
			if (Board[move.From] is Pawn || Board[move.To] != null)
			{
				countFiftyMoveRule = 0;
			}
			else
			{
				countFiftyMoveRule++;
			}
			
			PreviousMove = move;
			CurrentPlayer = Opponent;
			CheckForGameOver();
		}

		public List<Move> AllValidMoveForPlayer(GameColor color)
		{
			return Board.GetPiecePositionsFor(color).SelectMany(GetMovesForPiece).ToList();
		}

		public void CheckForGameOver()
		{
			if(!AllValidMoveForPlayer(CurrentPlayer).Any())
			{
				if(Board.IsInCheck(CurrentPlayer))
				{
					Result = Result.Win(Opponent, EndReason.Checkmate);
				}
				else
				{
					Result = Result.Draw(EndReason.Stalemate);
				}
			} else if(IsInsufficientMaterial())
			{
				Result = Result.Draw(EndReason.InsufficientMaterial);
			} else if(countFiftyMoveRule >= 20)
			{
				Result = Result.Draw(EndReason.FiftyMoveRule);
			}
		}

		public bool IsGameOver()
		{
			return Result != null;
		}

		public void Resign()
		{
			Result = Result.Win(Opponent, EndReason.Resignation);
		}

		public void DrawOffer(GameColor color)
		{
			if(color == GameColor.White)
			{
				WhiteDrawOffered = !WhiteDrawOffered;
			} else
			{
				BlackDrawOffered = !BlackDrawOffered;
			}
			if(IsDrawAgreement)
			{
				Result = Result.Draw(EndReason.DrawAgreement);
			}
		}

		public bool IsDrawAgreement => WhiteDrawOffered && BlackDrawOffered;

		private Counter CountPiece()
		{
			Counter counter = new();
			List<Position> piecePositions = Board.GetPiecePositionsFor(GameColor.Black).Concat(Board.GetPiecePositionsFor(GameColor.White)).ToList();
			foreach (Position position in piecePositions)
			{
				Piece? piece = Board[position];
				counter.Increment(piece.Color, piece.Type);
			}
			return counter;
		}

		public bool IsInsufficientMaterial()
		{
			Counter counter = CountPiece();
			if (counter.IsKingVsKing() || counter.IsKingBishopVsKing() || counter.IsKingKnightVsKing())
			{
				return true;
			}
			if (counter.IsKingBishopVsKingBishop())
			{
				Position? whiteBishop = Board.FindPiece(GameColor.White, PieceType.Bishop);
				Position? blackBishop = Board.FindPiece(GameColor.Black, PieceType.Bishop);
				if (whiteBishop.Color == blackBishop.Color)
				{
					return true;
				}
			}
			return false;
		}
	}
}
