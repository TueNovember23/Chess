using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace Chess.Logic.Moves
{
	public class CastlingKingSide : Move
	{
		public override MoveType Type => MoveType.CastlingKingside;

		private readonly Position rookFrom;
		private readonly Position rookTo;

		public CastlingKingSide(Position kingPos): base(kingPos, kingPos + Vector.Right * 2)
		{
			rookFrom = kingPos + Vector.Right * 3;
			rookTo = kingPos + Vector.Right;
		}

		public override void Execute(Board board)
		{
			base.Execute(board);
			new NormalMove(rookFrom, rookTo).Execute(board);
		}
	}

	public class CastlingQueenSide : Move
	{
		public override MoveType Type => MoveType.CastlingQueenside;

		private readonly Position rookFrom;
		private readonly Position rookTo;

		public CastlingQueenSide(Position kingPos) : base(kingPos, kingPos + Vector.Left * 2)
		{
			rookFrom = kingPos + Vector.Left * 4;
			rookTo = kingPos + Vector.Left;
		}

		public override void Execute(Board board)
		{
			base.Execute(board);
			new NormalMove(rookFrom, rookTo).Execute(board);
		}
	}
}
