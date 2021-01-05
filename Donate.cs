using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class Donate : Window, IComponentConnector
	{
				public Donate()
		{
			this.InitializeComponent();
		}

				private void f(object A_0, MouseEventArgs A_1)
		{
			this.imageb.Visibility = Visibility.Visible;
		}

				private void e(object A_0, MouseEventArgs A_1)
		{
			this.imageb.Visibility = Visibility.Collapsed;
		}

				private void d(object A_0, MouseEventArgs A_1)
		{
			this.imagec.Visibility = Visibility.Visible;
		}

				private void c(object A_0, MouseEventArgs A_1)
		{
			this.imagec.Visibility = Visibility.Collapsed;
		}

				private void b(object A_0, MouseEventArgs A_1)
		{
			this.imaged.Visibility = Visibility.Visible;
		}

				private void a(object A_0, MouseEventArgs A_1)
		{
			this.imaged.Visibility = Visibility.Collapsed;
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			Process.Start("http://goo.gl/EbHIYK");
		}

	}
}
