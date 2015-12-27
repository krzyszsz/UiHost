using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Mvvm;
using Microsoft.Practices.Prism.ViewModel;

namespace SpellingMasterUI.ViewModels
{
	[Export]
	public class GameViewModel : BindableBase, ITabViewModel
	{
		public string HeaderText { get; } = "Game";
	}
}
