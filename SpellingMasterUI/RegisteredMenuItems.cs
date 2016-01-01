using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using CommonLibUi;
using Prism.Commands;
using Prism.Regions;
using SpellingMasterUI.Views;

namespace SpellingMasterUI
{
	public class RegisteredMenuItems
	{
		[Export]
		public class ScoresMenuItem : MenuItem
		{
			private readonly IRegionManager _regionManager;

			[ImportingConstructor]
			public ScoresMenuItem(IRegionManager regionManager)
			{
				_regionManager = regionManager;
				Header = "_Scores";
				Command = new DelegateCommand(Navigate);
			}

			private void Navigate()
			{
				_regionManager.RequestNavigate(RegionNames.Tabs, new Uri(typeof (ScoresTab).Name, UriKind.Relative));
			}
		}

		[Export]
		public class GameMenuItem : MenuItem
		{
			private readonly IRegionManager _regionManager;

			[ImportingConstructor]
			public GameMenuItem(IRegionManager regionManager)
			{
				_regionManager = regionManager;
				Header = "_Game";
				Command = new DelegateCommand(Navigate);
			}

			private void Navigate()
			{
				_regionManager.RequestNavigate(RegionNames.Tabs, new Uri(typeof (GameTab).Name, UriKind.Relative));
			}
		}
	}
}