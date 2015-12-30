﻿using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Prism.Interactivity.InteractionRequest;
using Prism.Mef;
using Prism.Modularity;
using SpellingMasterCommon.Dialogs;
using SpellingMasterCommon.RegionAdapters;

namespace SpellingMasterGame
{
	public class Bootstrapper : MefBootstrapper
	{
		// TODO: Line below: direct reference to the module is not desired
		// TODO: Need to add msbuild task to copy all modules

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
			var q = new InteractionRequest<INotification>();
			var dialogService = new DialogService(q);
			Container.ComposeExportedValue(dialogService);
			var shell = new ShellWindow { ViewModel = new ShellWindowViewModel(dialogService, q) };
			Container.ComposeExportedValue(shell);
			return shell;
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
		}

		protected override Prism.Regions.RegionAdapterMappings ConfigureRegionAdapterMappings()
		{
			var regionAdapterMappings = base.ConfigureRegionAdapterMappings();
			regionAdapterMappings.RegisterMapping(typeof(TabControl), ServiceLocator.Current.GetInstance<TabControlRegionAdapter>());
			regionAdapterMappings.RegisterMapping(typeof(MenuItem), ServiceLocator.Current.GetInstance<MenuItemRegionAdapter>());
			return regionAdapterMappings;
		}

		//protected override ILoggerFacade CreateLogger()
		//{
		//	return new Log4NetLogger(); // todo
		//}
	}
}
