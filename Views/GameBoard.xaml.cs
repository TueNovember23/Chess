using Chess.Logic;
using Chess.Logic.Moves;
using Chess.Logic.Pieces;
using Chess.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
namespace Chess.Views
{
	/// <summary>
	/// Interaction logic for GameBoard.xaml
	/// </summary>
	public partial class GameBoard : Window
	{
		private GamePlay gamePlay = new();
		private readonly Rectangle[,]  highlights = new Rectangle[8, 8];
		private readonly Dictionary<Position, Move> moveCache = new();
		private Position? selectedPosition = null;

		public GameBoard()
		{
			InitializeComponent();
			InitializeHighlightSquares();
			DrawBoard();
			ChangeCursor();
		}

		private void PieceGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			if(IsMenuOpen)
			{
				return;
			}
			UniformGrid grid = (UniformGrid)sender;
			Point mousePosition = e.GetPosition(grid);

			int row = (int)(mousePosition.Y / (grid.ActualHeight / grid.Rows));
			int column = (int)(mousePosition.X / (grid.ActualWidth / grid.Columns));
			Position position = new(row, column);
			if (Board.IsOnBoard(position))
			{
				if (selectedPosition == null)
				{
					HandleFromClick(position);
				}
				else
				{
					HandleToClick(position);
				}
			}
		}

		private void HandleFromClick(Position position)
		{
			List<Move> moves = gamePlay.GetMovesForPiece(position);
			if(moves.Any())
			{
				CacheMoves(moves);
				ShowHighlights();
				selectedPosition = position;
				HighlightSelectedPosition();
			}
			else
			{
				selectedPosition = null;
			}
		}

		private void HandleToClick(Position position)
		{
			RemoveHighlightSelectedPosition();
			selectedPosition = null;
			HideHighlights();
			if (gamePlay.Board[position]?.Color == gamePlay.CurrentPlayer)
			{
				HandleFromClick(position);
			}
			else if (moveCache.TryGetValue(position, out Move? move))
			{
				HandleMove(move);
			}
		}

		private void HandleMove(Move move)
		{
			gamePlay.ExecuteMove(move);
			DrawBoard();
			ChangeCursor();
			if(gamePlay.IsGameOver())
			{
				ShowGameOverMenu();
			}
		}

		private void ShowHighlights()
		{
			foreach(Position position in moveCache.Keys)
			{
				highlights[position.Row, position.Column].Visibility = Visibility.Visible;
			}
		}

		private void HideHighlights()
		{
			foreach(Position position in moveCache.Keys)
			{
				highlights[position.Row, position.Column].Visibility = Visibility.Hidden;
			}
		}

		private void DrawBoard()
		{
			PieceGrid.Children.Clear();
			for (int i = 0; i < 8; i++)
			{
				for(int j = 0;j < 8; j++)
				{
					Position position = new(i, j);
					Piece? piece = gamePlay.Board[position];
					PieceGrid.Children.Add(PieceImage.GetImage(piece));
				}
			}
		}

		private void ChangeCursor()
		{
			string cursorName = gamePlay.CurrentPlayer == GameColor.White ? "CursorW" : "CursorB";
			Stream stream = Application.GetResourceStream(new Uri($"pack://application:,,,/Images/{cursorName}.cur")).Stream;
			Cursor = new Cursor(stream, true);
		}

		private void CacheMoves(List<Move> moves)
		{
			moveCache.Clear();
			foreach(Move move in moves)
			{
				moveCache.Add(move.To, move);
			}
		}

		private void HighlightSelectedPosition()
		{
			if(selectedPosition != null)
			{
				Rectangle rect = highlights[selectedPosition.Row, selectedPosition.Column];
				rect.Fill = Resources["SelectedSquare"] as SolidColorBrush;
				rect.Visibility = Visibility.Visible;
			}
		}

		private void RemoveHighlightSelectedPosition()
		{
			if (selectedPosition != null)
			{
				Rectangle rect = highlights[selectedPosition.Row, selectedPosition.Column];
				rect.Fill = Resources["HighlightSquare"] as SolidColorBrush;
				rect.Visibility = Visibility.Hidden;
			}
		}

		private void InitializeHighlightSquares()
		{
			for (int i = 0; i < 8; i++)
			{
				for (int j = 0; j < 8; j++)
				{
					Rectangle rect = new()
					{
						Fill = Resources["HighlightSquare"] as SolidColorBrush,
						Visibility = Visibility.Hidden
					};
					highlights[i, j] = rect;
					HighlightGrid.Children.Add(rect);
				}
			}
		}

		private bool IsMenuOpen => MenuContainer.Content != null;

		private void ShowGameOverMenu()
		{
			GameOverMenu gameOverMenu = new(gamePlay);
			MenuContainer.Content = gameOverMenu;
			gameOverMenu.OnOptionSelected += option =>
			{
				if (option == Option.NewGame)
				{
					MenuContainer.Content = null;
					RestartGame();
				}
				else
				{
					Application.Current.Shutdown();
				}
			};
		}

		private void RestartGame()
		{
			moveCache.Clear();
			RemoveHighlightSelectedPosition();
			HideHighlights();
			ChangeCursor();
			gamePlay = new();
			DrawBoard();
			WhiteDrawOfferButton.Background = BlackDrawOfferButton.Background = Brushes.Gray;
		}

		private void DrawOffer_Click(object sender, RoutedEventArgs e)
		{
			if(IsMenuOpen)
			{
				return;
			}
			Button button = (Button)sender;
			string? colorOffer = button.Tag.ToString();
			if(colorOffer == "WhiteDrawOffer")
			{
				gamePlay.DrawOffer(GameColor.White);
				if(gamePlay.WhiteDrawOffered)
				{
					button.Background = Brushes.LightGreen;
				} else
				{
					button.Background = Brushes.Gray;
				}
			} else
			{
				gamePlay.DrawOffer(GameColor.Black);
				if (gamePlay.BlackDrawOffered)
				{
					button.Background = Brushes.LightGreen;
				}
				else
				{
					button.Background = Brushes.Gray;
				}
			}
			if(gamePlay.IsDrawAgreement)
			{
				ShowGameOverMenu();
			}
		}

		private void Resign_Click(object sender, RoutedEventArgs e)
		{
			if (IsMenuOpen)
			{
				return;
			}
			MessageBoxResult result = MessageBox.Show("You will loose. Are you sure you want to resign?", "Resign", MessageBoxButton.YesNo);
			if(result == MessageBoxResult.Yes)
			{
				gamePlay.Resign();
				ShowGameOverMenu();
			}
		}
	}
}
