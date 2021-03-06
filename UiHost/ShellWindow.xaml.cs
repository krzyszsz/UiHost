﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UiHost
{
	// TODO: Extract interface of this ViewModel & View for loose-coupling
	/// <summary>
	/// Interaction logic for ShellWindow.xaml
	/// </summary>
	[Export]
	public partial class ShellWindow : Window
	{
		public ShellWindow()
		{
			InitializeComponent();
		}

		public ShellWindowViewModel ViewModel
		{
			set
			{
				DataContext = value;
				value.Shell = this;
			}
		}
	}
}
