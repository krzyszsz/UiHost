using CommonLibUi;
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
			regionManager.RegisterViewWithRegion(RegionNames.Tabs, typeof(GameTab));
			regionManager.RegisterViewWithRegion(RegionNames.Tabs, typeof(ScoresTab));
			// Below: registering menuItems
			regionManager.RegisterViewWithRegion(RegionNames.Menu, typeof(RegisteredMenuItems.ScoresMenuItem));
			regionManager.RegisterViewWithRegion(RegionNames.Menu, typeof(RegisteredMenuItems.GameMenuItem));
		}
	}
}