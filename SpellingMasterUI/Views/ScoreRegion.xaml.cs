using System.ComponentModel.Composition;
using SpellingMasterCommon.RegionAdapters;
using SpellingMasterUI.ViewModels;

namespace SpellingMasterUI.Views
{
	/// <summary>
	/// Interaction logic for ScoreRegion.xaml
	/// </summary>
	[Export]
	public partial class ScoreRegion : ITabItemView
	{
		public ScoreRegion()
		{
			InitializeComponent();
		}

		public int Index { get; set; } = 1;

		public bool IsActive { get; set; } = false;

		[Import(typeof(ScoresViewModel))]
		public ITabViewModel TabViewModel
		{
			set { DataContext = value; }
		}
	}
}
