using System.ComponentModel.Composition;
using SpellingMasterCommon.RegionAdapters;

namespace SpellingMasterUI.ViewModels
{
	[Export]
	public class ScoresViewModel : ITabViewModel
	{
		public string HeaderText { get; } = "Scores";
	}
}