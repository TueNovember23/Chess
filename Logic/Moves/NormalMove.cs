using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chess.Logic.Pieces;

namespace Chess.Logic.Moves
{
	public class NormalMove : Move
	{
		public NormalMove(Position from, Position to) : base(from, to)
		{
		}

		public override MoveType Type => MoveType.Normal;
	}
}
