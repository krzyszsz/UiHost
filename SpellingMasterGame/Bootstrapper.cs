using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.PubSubEvents;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;
using Microsoft.Practices.ServiceLocation;
using Prism.Logging;
using Prism.Mef;
using Prism.Modularity;
using SpellingMasterCommon.RegionAdapters;
using SpellingMasterUI;

namespace SpellingMasterGame
{
	public class Bootstrapper : MefBootstrapper
	{
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
			var moduleCatalog = new DirectoryModuleCatalog {ModulePath = @"."};
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
			Container.ComposeExportedValue(new TabControlRegionAdapter(Container.GetExportedValue<Prism.Regions.IRegionBehaviorFactory>()));
		}

		protected override Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
		{
			var regionAdapterMappings = base.ConfigureRegionAdapterMappings();
			regionAdapterMappings.RegisterMapping(typeof(TabControl), ServiceLocator.Current.GetInstance<TabControlRegionAdapter>());
			return regionAdapterMappings;
		}

		//protected override ILoggerFacade CreateLogger()
		//{
		//	return new Log4NetLogger(); // todo
		//}
	}
}
