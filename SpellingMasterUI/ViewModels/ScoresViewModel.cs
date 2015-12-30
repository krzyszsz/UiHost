using System.ComponentModel.Composition;
using CommonLibUi.RegionAdapters;

namespace SpellingMasterUI.ViewModels
{
	[Export]
	public class ScoresViewModel : ITabViewModel
	{
		public string HeaderText { get; } = "Scores";
	}
}