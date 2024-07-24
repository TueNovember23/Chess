using Chess.Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Logic
{
	public class Counter
	{
		private Dictionary<PieceType, int> blackCounter = new();
		private Dictionary<PieceType, int> whiteCounter = new();

		public int TotalCount { get; private set; }

		public Counter()
		{
			foreach (PieceType pieceType in Enum.GetValues(typeof(PieceType)))
			{
				blackCounter.Add(pieceType, 0);
				whiteCounter.Add(pieceType, 0);
			}
		}

		public void Increment(GameColor color, PieceType pieceType)
		{
			if (color == GameColor.Black)
			{
				blackCounter[pieceType]++;
			}
			else
			{
				whiteCounter[pieceType]++;
			}
			TotalCount++;
		}
		
		public int Black(PieceType type)
		{
			return blackCounter[type];
		}

		public int White(PieceType type)
		{
			return whiteCounter[type];
		}
		
		public bool IsKingVsKing()
		{
			return TotalCount == 2;
		}

		public bool IsKingBishopVsKing()
		{
			return TotalCount == 3 && (White(PieceType.Bishop) == 1 || Black(PieceType.Bishop) == 1);
		}

		public bool IsKingKnightVsKing()
		{
			return TotalCount == 3 && (White(PieceType.Knight) == 1 || Black(PieceType.Knight) == 1);
		}

		public bool IsKingBishopVsKingBishop()
		{
			return TotalCount == 4 && White(PieceType.Bishop) == 1 && Black(PieceType.Bishop) == 1;
		}

	}
}
