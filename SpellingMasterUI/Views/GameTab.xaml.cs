using System.ComponentModel.Composition;
using System.Windows.Controls;
using SpellingMasterCommon.RegionAdapters;
using SpellingMasterUI.ViewModels;

namespace SpellingMasterUI.Views
{
	/// <summary>
	/// Interaction logic for GameTab.xaml
	/// </summary>
	[Export]
	public partial class GameTab : ITabItemView
	{
		public GameTab()
		{
			InitializeComponent();
		}

		public int Index { get; set; } = 0;

		public bool IsActive { get; set; } = true;

		[Import(typeof(GameViewModel))]
		public ITabViewModel TabViewModel
		{
			set { DataContext = value; }
		}
	}
}
