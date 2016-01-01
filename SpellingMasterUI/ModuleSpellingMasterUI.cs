using System.ComponentModel.Composition;
using System.Threading.Tasks;
using CommonLibUi;
using CommonLibUi.WaitingService;
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
		private readonly IWaitingService _waitingService;

		[ImportingConstructor]
		public ModuleSpellingMasterUi(IWaitingService waitingService)
		{
			_waitingService = waitingService;
		}

		public async void Initialize()
		{
			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			// Below: registering tabs
			regionManager.RegisterViewWithRegion(RegionNames.Tabs, typeof(GameTab));
			regionManager.RegisterViewWithRegion(RegionNames.Tabs, typeof(ScoresTab));
			// Below: registering menuItems
			regionManager.RegisterViewWithRegion(RegionNames.Menu, typeof(RegisteredMenuItems.ScoresMenuItem));
			regionManager.RegisterViewWithRegion(RegionNames.Menu, typeof(RegisteredMenuItems.GameMenuItem));

			await _waitingService.ExecuteLongTask(Task.Delay(500));
		}
	}
}