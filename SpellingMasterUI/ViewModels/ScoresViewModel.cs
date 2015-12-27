using System.ComponentModel.Composition;

namespace SpellingMasterUI.ViewModels
{
	[Export]
	public class ScoresViewModel : ITabViewModel
	{
		public string HeaderText { get; } = "Scores";
	}
}