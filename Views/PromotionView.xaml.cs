using Chess.Logic;
using Chess.Logic.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Chess.Views
{
	/// <summary>
	/// Interaction logic for PromotionView.xaml
	/// </summary>
	public partial class PromotionView : Window
	{
		private readonly GameColor color;
		public Piece PiecePromotion { get; private set; }
		public PromotionView(GameColor color)
		{
			InitializeComponent();
			this.color = color;
			SetPieceColor();
			PiecePromotion = new Queen(color);
		}

		private void SetPieceColor()
		{
			if(this.color == GameColor.Black)
			{
				queenImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/QueenB.png"));
				rookImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/RookB.png"));
				knightImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/KnightB.png"));
				bishopImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/BishopB.png"));
			}
		}

		private void SelectQueen(object sender, RoutedEventArgs e)
		{ 
			MessageBoxResult result = MessageBox.Show("Choose Queen?", Name, MessageBoxButton.YesNo);
			if(result == MessageBoxResult.Yes)
			{
				this.Close();
			}
		}

		private void SelectBishop(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Choose Bishop?", Name, MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				PiecePromotion = new Bishop(color);
				this.Close();
			}
		}

		private void SelectRook(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Choose Rook?", Name, MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				PiecePromotion = new Rook(color);
				this.Close();
			}
		}

		private void SelectKnight(object sender, RoutedEventArgs e)
		{
			MessageBoxResult result = MessageBox.Show("Choose Knight?", Name, MessageBoxButton.YesNo);
			if (result == MessageBoxResult.Yes)
			{
				PiecePromotion = new Knight(color);
				this.Close();
			}
		}

		private void Window_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if(e.LeftButton == MouseButtonState.Pressed)
			{
				this.DragMove();
			}
        }
    }
}
