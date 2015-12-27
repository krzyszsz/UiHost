using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Mef;
using Prism.Modularity;
using SpellingMasterUI;

namespace SpellingMasterGame
{
	public class Bootstrapper : MefBootstrapper
	{
		private Prism.Regions.RegionAdapterMappings _regionAdapterMappings;

		//protected override void ConfigureAggregateCatalog()
		//{
		//	// TODO: Line below: direct reference to the module is not desired
		//	AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModuleSpellingMasterUi).Assembly));
		//	base.ConfigureAggregateCatalog();
		//}

		protected override void ConfigureAggregateCatalog()
		{
			AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
		}

		protected override IModuleCatalog CreateModuleCatalog()
		{
			var moduleCatalog = new DirectoryModuleCatalog();
			moduleCatalog.ModulePath = @".";

			/*var dir = Directory.GetCurrentDirectory();
			var module = Path.Combine(dir, "SpellingMasterUI.dll");
			var exists = File.Exists(module);
			moduleCatalog.ModulePath = dir;

			moduleCatalog.Load();
			var q = moduleCatalog.Modules.ToList();*/

			return moduleCatalog;
		}

		protected override DependencyObject CreateShell()
		{
			return new ShellWindow();
		}

		protected override void InitializeShell()
		{
			Application.Current.MainWindow = (ShellWindow)Shell;
			Application.Current.MainWindow.Show();
		}

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();
			//ServiceLocator.SetLocatorProvider(() => new MefServiceLocatorAdapter(Container));

			Container.ComposeExportedValue<IRegionManager>(new RegionManager());
			//var mappings = ConfigureRegionAdapterMappings();
			//Container.ComposeExportedValue<Prism.Regions.RegionAdapterMappings>(mappings);
			Container.ComposeExportedValue<IRegionViewRegistry>(new RegionViewRegistry(Container.GetExportedValue<IServiceLocator>()));
		}

		protected override Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
		{
			_regionAdapterMappings = _regionAdapterMappings ?? base.ConfigureRegionAdapterMappings();
			return _regionAdapterMappings;
		}

		//protected override ILoggerFacade CreateLogger()
		//{
		//	return new Log4NetLogger(); // todo
		//}
	}
}
