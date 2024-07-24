using Chess.Logic.Pieces;
using Chess.Logic;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Chess.UI
{
	public static class PieceImage
	{
		private static Dictionary<PieceType, string> pieceImagePaths = new() {
						{ PieceType.Pawn, "Pawn" },
						{ PieceType.Rook, "Rook" },
						{ PieceType.Knight, "Knight" },
						{ PieceType.Queen, "Queen" },
						{ PieceType.King, "King" },
						{ PieceType.Bishop, "Bishop" },
					};
		public static Image GetImage(Piece? piece)
		{
			if(piece == null)
			{
				return new Image();
			}
			string colorPrefix = (piece.Color == GameColor.White) ? "W" : "B";
			Image image = new();
			image.Stretch = Stretch.UniformToFill;
			if (pieceImagePaths.TryGetValue(piece.Type, out string? pieceTypePath))
			{
				image.Source = new BitmapImage(new Uri($"pack://application:,,,/Images/{pieceTypePath}{colorPrefix}.png"));
				RenderOptions.SetBitmapScalingMode(image, BitmapScalingMode.HighQuality);
			}
			return image;
		}
	}
}
