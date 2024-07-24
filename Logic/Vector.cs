using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Logic
{
	public class Vector
	{
		public static Vector Up => new(-1, 0);
		public static Vector Down => new(1, 0);
		public static Vector Left => new(0, -1);
		public static Vector Right => new(0, 1);
		public static Vector UpLeft => Up + Left;
		public static Vector UpRight => Up + Right;
		public static Vector DownLeft => Down + Left;
		public static Vector DownRight => Down + Right;


		public int Vertical { get; set; }
		public int Horizontal { get; set; }

		public Vector(int vertical, int horizontal)
		{
			Vertical = vertical;
			Horizontal = horizontal;
		}

		public Vector()
		{
			Vertical = Horizontal = 0;
		}

		public static Vector operator +(Vector a, Vector b)
		{
			return new Vector(a.Vertical + b.Vertical, a.Horizontal + b.Horizontal);
		}

		public static Vector operator *(int b, Vector a)
		{
			return new Vector(a.Vertical * b, a.Horizontal * b);
		}

		public static Vector operator *(Vector a, int b)
		{
			return b * a;
		}

		public static bool operator ==(Vector a, Vector b)
		{
			return a.Vertical == b.Vertical && a.Horizontal == b.Horizontal;
		}

		public static bool operator !=(Vector a, Vector b)
		{
			return !(a == b);
		}

		public override bool Equals(object? obj)
		{
			return obj is Vector vector &&
				   Vertical == vector.Vertical &&
				   Horizontal == vector.Horizontal;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(Vertical, Horizontal);
		}
	}
}
