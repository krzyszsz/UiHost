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
			regionManager.RegisterViewWithRegion("GameRegion", typeof(GameRegion));
			regionManager.RegisterViewWithRegion("ScoreRegion", typeof(ScoreRegion));
		}
	}
}