namespace Chess.Logic.Moves
{
	public class DoubleSquarePawn : Move
	{
		public override MoveType Type => MoveType.DoubleSquarePawn;
		private Position skipPosition;

		public DoubleSquarePawn(Position from, Position to) : base(from, to)
		{
			skipPosition = new Position((from.Row + to.Row) / 2, from.Column);
		}

		public override void Execute(Board board)
		{
			board.PawnSkipPos = skipPosition;
			base.Execute(board);
		}
	}
}
