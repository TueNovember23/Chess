using Chess.Logic.Pieces;

namespace Chess.Logic.Moves
{
	public class EnPassent : Move
	{
		public override MoveType Type => MoveType.EnPassant;
		private readonly Position enemyPawnPos;

		public EnPassent(Position from, Position to) : base(from, to)
		{
			this.enemyPawnPos = new Position(from.Row, to.Column);
		}

		public override void Execute(Board board)
		{
			base.Execute(board);
			board[enemyPawnPos] = null;
		}

		public override bool IsValidMove(Board board)
		{
			if (board[From] == null)
			{
				return false;
			}
			if (board[enemyPawnPos] is not Pawn || board.PawnSkipPos is null || To.Row != board.PawnSkipPos.Row || To.Column != board.PawnSkipPos.Column)
			{
				return false;
			}
			Board copy = board.Copy();
			this.Execute(copy);
			Position kingPos = copy.GetKingPosition(board[From].Color);
			return !copy.IsUnderAttack(kingPos, board[From].Color);
		}
	}
}
