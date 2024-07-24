using System;
using System.Windows;
using System.Windows.Controls;
using Chess.Logic;

namespace Chess.Views
{
	/// <summary>
	/// Interaction logic for GameOverMenu.xaml
	/// </summary>
	public partial class GameOverMenu : UserControl
	{
		public event Action<Option>? OnOptionSelected;

		public GameOverMenu(GamePlay game)
		{
			InitializeComponent();
			Result result = game.Result ?? throw new Exception("Game is not over.");
			WinnerTxtBlock.Text = GetWinnerTxt(result.Winner);
			EndReasonTxtBlock.Text = GetReasonTxt(result.Reason, result.Winner);
		}

		private static string GetWinnerTxt(GameColor color)
		{
			return color switch
			{
				GameColor.Black => "Black Wins",
				GameColor.White => "White Wins",
				_ => "Draw"
			};
		}

		private static string GetReasonTxt(EndReason reason, GameColor winner)
		{
			return reason switch
			{
				EndReason.Checkmate => "Checkmate",
				EndReason.Stalemate => $"Stalemate",
				EndReason.InsufficientMaterial => "Insufficient Material",
				EndReason.ThreefoldRepetition => "Threefold Repetition",
				EndReason.FiftyMoveRule => "Fifty Move Rule",
				EndReason.Resignation => "Resignation",
				EndReason.DrawAgreement => "Draw Agreement",
				_ => ""
			};
		}

		private void NewGameBtn_Click(object sender, RoutedEventArgs e)
		{
			OnOptionSelected?.Invoke(Option.NewGame);
		}

		private void ExitBtn_Click(object sender, RoutedEventArgs e)
		{
			OnOptionSelected?.Invoke(Option.Exit);
		}
	}

	public enum Option
	{
		Exit,
		NewGame
	}
}
