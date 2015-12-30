using System.ComponentModel.Composition;
using SpellingMasterCommon.RegionAdapters;
using SpellingMasterUI.ViewModels;

namespace SpellingMasterUI.Views
{
	/// <summary>
	/// Interaction logic for ScoresTab.xaml
	/// </summary>
	[Export]
	public partial class ScoresTab : ITabItemView
	{
		public ScoresTab()
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
