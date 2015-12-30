using System.ComponentModel.Composition;
using System.Windows.Controls;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef.Modularity;
using Prism.Modularity;
using Prism.Regions;
using SpellingMasterUI.Views;

namespace SpellingMasterUI
{
	[ModuleExport("ModuleSpellingMasterUi", typeof(ModuleSpellingMasterUi))]
	public class ModuleSpellingMasterUi : IModule
	{
		public void Initialize()
		{
			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			// Below: registering tabs
			regionManager.RegisterViewWithRegion("TabsRegion", typeof(GameTab));
			regionManager.RegisterViewWithRegion("TabsRegion", typeof(ScoresTab));
			// Below: registering menuItems
			regionManager.RegisterViewWithRegion("MenuRegion", typeof(ScoresMenuItem));
			regionManager.RegisterViewWithRegion("MenuRegion", typeof(GameMenuItem));
		}

		[Export]
		public class ScoresMenuItem : MenuItem
		{
			public ScoresMenuItem()
			{
				Header = "_Scores";
			}
		}

		[Export]
		public class GameMenuItem : MenuItem
		{
			public GameMenuItem()
			{
				Header = "_Game";
			}
		}
	}
}