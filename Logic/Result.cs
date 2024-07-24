namespace Chess.Logic
{
	public class Result
	{
		public GameColor Winner { get; }
		public EndReason Reason { get; }

		public Result(GameColor winner, EndReason reason)
		{
			Winner = winner;
			Reason = reason;
		}

		public static Result Draw(EndReason reason)
		{
			return new Result(GameColor.None, reason);
		}

		public static Result Win(GameColor winner, EndReason reason)
		{
			return new Result(winner, reason);
		}
	}
}
