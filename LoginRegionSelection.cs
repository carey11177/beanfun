using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class LoginRegionSelection : Window, IComponentConnector
	{
				public LoginRegionSelection()
		{
			this.InitializeComponent();
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			g.b("loginRegion", "TW");
			base.Hide();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			g.b("loginRegion", "HK");
			base.Hide();
		}

			
	}
}
