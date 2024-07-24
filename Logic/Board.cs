using Chess.Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Chess.Logic
{
	public class Board
	{
		private readonly Piece?[,] board = new Piece[8, 8];
		public Position? PawnSkipPos { get; set; }

		public Piece? this[int row, int column]
		{
			get => board[row, column];
			set => board[row, column] = value;
		}

		public Piece? this[Position position]
		{
			get => board[position.Row, position.Column];
			set => board[position.Row, position.Column] = value;
		}

		public Board() { Initialize(); }

		public static bool IsOnBoard(Position position)
		{
			return position.Row >= 0 && position.Row < 8 && position.Column >= 0 && position.Column < 8;
		}

		public bool IsEmpty(Position position)
		{
			return this[position] == null;
		}

		public Board Copy()
		{
			Board copy = new();
			for (int row = 0; row < 8; row++)
			{
				for (int column = 0; column < 8; column++)
				{
					copy[row, column] = this[row, column]?.Copy();
				}
			}
			return copy;
		}

		public Position GetKingPosition(GameColor color)
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					if (this[i, j] is King && this[i, j].Color == color)
					{
						return new(i, j);
					}
				}
			}
			throw new Exception($"The {color} King is not exist");
		}

		public bool IsUnderAttack(Position position, GameColor color)
		{
			Vector forward = (color == GameColor.White) ? Vector.Up : Vector.Down;
			Vector[] knights = new[]
			{
				Vector.Up*2 + Vector.Left,
				Vector.Up*2 + Vector.Right,
				Vector.Down*2 + Vector.Left,
				Vector.Down*2 + Vector.Right,
				Vector.Left*2 + Vector.Up,
				Vector.Left*2 + Vector.Down,
				Vector.Right*2 + Vector.Up,
				Vector.Right*2 + Vector.Down,
			};
			Vector[] diagonals = new[]
			{
				Vector.UpLeft, Vector.UpRight, Vector.DownLeft, Vector.DownRight
			};
			Vector[] vectors = new Vector[]
			{
				Vector.Up, Vector.Down, Vector.Left, Vector.Right
			};
			Vector[] kings = diagonals.Concat(vectors).ToArray();

			Position[] pawnPos = new[] { position + forward + Vector.Left, position + forward + Vector.Right };

			// Check knights
			foreach (Vector vector in knights)
			{
				Position knight = position + vector;
				if (IsOnBoard(knight) && this[knight] is Knight k && k.Color != color)
				{
					return true;
				}
			}

			// Check one square diagonals
			foreach (Position p in pawnPos)
			{
				if (IsOnBoard(p) && this[p] != null && this[p]?.Color != color)
				{
					if (this[p] is Pawn)
					{
						return true;
					}
				}
			}
			foreach (Vector v in kings)
			{
				Position p = position + v;
				if (IsOnBoard(p) && this[p] != null && this[p]?.Color != color)
				{
					if (this[p] is King)
					{
						return true;
					}
				}
			}

			// Check diagonals
			foreach (Vector vector in diagonals)
			{
				Position diagonal = position + vector;
				while (IsOnBoard(diagonal) && this[diagonal] == null)
				{
					diagonal += vector;
				}
				if (IsOnBoard(diagonal) && (this[diagonal]?.Color != color) && (this[diagonal] is Bishop || this[diagonal] is Queen))
				{
					return true;
				}
			}

			// Check vectors
			foreach (Vector vector in vectors)
			{
				Position p = position + vector;
				while (IsOnBoard(p) && this[p] == null)
				{
					p += vector;
				}
				if (IsOnBoard(p) && (this[p]?.Color != color) && (this[p] is Rook || this[p] is Queen))
				{
					return true;
				}
			}
			return false;
		}

		public bool IsInCheck(GameColor color)
		{
			return IsUnderAttack(GetKingPosition(color), color);
		}

		private void Initialize()
		{
			this[0, 0] = new Rook(GameColor.Black);
			this[0, 1] = new Knight(GameColor.Black);
			this[0, 2] = new Bishop(GameColor.Black);
			this[0, 3] = new Queen(GameColor.Black);
			this[0, 4] = new King(GameColor.Black);
			this[0, 5] = new Bishop(GameColor.Black);
			this[0, 6] = new Knight(GameColor.Black);
			this[0, 7] = new Rook(GameColor.Black);
			for (int i = 0; i < 8; i++)
			{
				this[1, i] = new Pawn(GameColor.Black);
			}

			for (int i = 0; i < 8; i++)
			{
				this[6, i] = new Pawn(GameColor.White);
			}
			this[7, 0] = new Rook(GameColor.White);
			this[7, 1] = new Knight(GameColor.White);
			this[7, 2] = new Bishop(GameColor.White);
			this[7, 3] = new Queen(GameColor.White);
			this[7, 4] = new King(GameColor.White);
			this[7, 5] = new Bishop(GameColor.White);
			this[7, 6] = new Knight(GameColor.White);
			this[7, 7] = new Rook(GameColor.White);
		}

		public List<Position> GetPiecePositionsFor(GameColor color)
		{
			List<Position> list = new();
			for(int i = 0; i < 8; i++)
			{
				for(int j = 0; j < 8; j++)
				{
					if (board[i, j]?.Color == color)
					{
						list.Add(new(i, j));
					}
				}
			}
			return list;
		}

		public Position? FindPiece(GameColor color, PieceType type)
		{
			return GetPiecePositionsFor(color).FirstOrDefault(_ => this[_]?.Type == type);
		}
	}
}
