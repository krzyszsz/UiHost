using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using log4net.Config;

namespace SpellingMasterGame
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		/*
		[Import]
		private Lazy<ISynthesizer> _synthesizer;

		[Import]
		private ShellWindow _window;*/

		protected override void OnStartup(StartupEventArgs e)
		{
			base.OnStartup(e);
			XmlConfigurator.Configure();
			var bootstrapper = new Bootstrapper();
			bootstrapper.Run();
		}

		public App()
		{
			/*var catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
			var container = new CompositionContainer(catalog);
			container.SatisfyImportsOnce(this);

			//_synthesizer.Value.Say("A B C");
			_window.Show();*/
			//new Bootstrapper().Run();
		}
	}
}
