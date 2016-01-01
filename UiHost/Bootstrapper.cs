using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Controls;
using CommonLib.Logging;
using CommonLibUi.Dialogs;
using CommonLibUi.RegionAdapters;
using CommonLibUi.WaitingService;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Prism.Interactivity.InteractionRequest;
using Prism.Logging;
using Prism.Mef;
using Prism.Modularity;

namespace UiHost
{
	public class Bootstrapper : MefBootstrapper
	{
		protected override void ConfigureAggregateCatalog()
		{
			AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(Bootstrapper).Assembly));
		}

		protected override IModuleCatalog CreateModuleCatalog()
		{
			base.CreateModuleCatalog();
			var moduleCatalog = new DirectoryModuleCatalog {ModulePath = @"."};
			return moduleCatalog;
		}

		protected override DependencyObject CreateShell()
		{
			var dialogService = Container.GetExportedValue<IDialogService>();
			var waitingService = Container.GetExportedValue<IWaitingService>();
			var interactionRequest = Container.GetExportedValue<InteractionRequest<INotification>>();
			var shell = new ShellWindow { ViewModel = new ShellWindowViewModel(dialogService, interactionRequest, waitingService) };
			Container.ComposeExportedValue(shell);
			return shell;
		}

		private void RegisterDialogService()
		{
			var interactionRequest = new InteractionRequest<INotification>();
			var dialogService = new DialogService(interactionRequest);
			Container.ComposeExportedValue(interactionRequest);
			Container.ComposeExportedValue<IDialogService>(dialogService);
		}

		private void RegisterWaitingService()
		{
			var waitingService = new WaitingService();
			Container.ComposeExportedValue<IWaitingService>(waitingService);
		}

		protected override void InitializeShell()
		{
			Application.Current.MainWindow = (ShellWindow)Shell;
			Application.Current.MainWindow.Show();
		}

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();
			Container.ComposeExportedValue<IRegionManager>(new RegionManager());
			Container.ComposeExportedValue<IRegionViewRegistry>(new RegionViewRegistry(Container.GetExportedValue<IServiceLocator>()));
			Container.ComposeExportedValue(new TabControlRegionAdapter(Container.GetExportedValue<Prism.Regions.IRegionBehaviorFactory>()));
			Container.ComposeExportedValue(new MenuItemRegionAdapter(Container.GetExportedValue<Prism.Regions.IRegionBehaviorFactory>()));
			RegisterDialogService();
			RegisterWaitingService();
		}

		protected override Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
		{
			var regionAdapterMappings = base.ConfigureRegionAdapterMappings();
			regionAdapterMappings.RegisterMapping(typeof(TabControl), ServiceLocator.Current.GetInstance<TabControlRegionAdapter>());
			regionAdapterMappings.RegisterMapping(typeof(MenuItem), ServiceLocator.Current.GetInstance<MenuItemRegionAdapter>());
			return regionAdapterMappings;
		}

		protected override ILoggerFacade CreateLogger()
		{
			return new Log4NetLogger();
		}
	}
}
