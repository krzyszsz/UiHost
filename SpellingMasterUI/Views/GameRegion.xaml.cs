using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SpellingMasterUI.Views
{
	/// <summary>
	/// Interaction logic for GameRegion.xaml
	/// </summary>
	[Export]
	public partial class GameRegion : UserControl
	{
		public GameRegion()
		{
			InitializeComponent();
		}
	}
}
