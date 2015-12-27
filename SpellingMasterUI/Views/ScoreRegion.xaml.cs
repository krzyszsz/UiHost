using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace SpellingMasterUI.Views
{
	/// <summary>
	/// Interaction logic for ScoreRegion.xaml
	/// </summary>
	[Export]
	public partial class ScoreRegion : UserControl
	{
		public ScoreRegion()
		{
			InitializeComponent();
		}
	}
}
