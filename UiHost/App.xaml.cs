using System;
using System.Windows;
using System.Windows.Threading;
using log4net.Config;

namespace UiHost
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
			DispatcherUnhandledException += UnhandledException;
			/*var catalog = new AssemblyCatalog(System.Reflection.Assembly.GetExecutingAssembly());
			var container = new CompositionContainer(catalog);
			container.SatisfyImportsOnce(this);

			//_synthesizer.Value.Say("A B C");
			_window.Show();*/
			//new Bootstrapper().Run();
		}

		private void UnhandledException(object sender, DispatcherUnhandledExceptionEventArgs dispatcherUnhandledExceptionEventArgs)
		{
			dispatcherUnhandledExceptionEventArgs.Handled = true;
			try
			{
				var logger = log4net.LogManager.GetLogger(typeof (App));
				logger.Error(dispatcherUnhandledExceptionEventArgs.Exception);
			}
			catch (Exception e)
			{
				// Log4net might be not initialized.
				Console.WriteLine(e);
				Console.WriteLine(dispatcherUnhandledExceptionEventArgs.Exception);
			}
			MessageBox.Show(dispatcherUnhandledExceptionEventArgs.Exception.ToString(), "Unhandled exception");
		}
	}
}
