using Chess.Logic.Pieces;
using Chess.Views;

namespace Chess.Logic.Moves
{
	public class Promotion: Move
	{
		public override MoveType Type => MoveType.Promotion;

		public Promotion(Position from, Position to): base(from, to) { }

		public override void Execute(Board board)
		{
			PromotionView promotionView = new(board[From].Color);
			base.Execute(board);
			promotionView.ShowDialog();
			Piece piece = promotionView.PiecePromotion;
			board[To] = piece;
		}

		private void ExecuteCheck (Board board)
		{
			base.Execute(board);
		}

		public override bool IsValidMove(Board board)
		{
			if (board[From] == null)
			{
				return false;
			}
			Board copy = board.Copy();
			this.ExecuteCheck(copy);
			Position kingPos = copy.GetKingPosition(board[From].Color);
			return !copy.IsUnderAttack(kingPos, board[From].Color);
		}
	}
}
