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
		public partial class CopyBox : Window, IComponentConnector
	{
				public CopyBox(string title, string value)
		{
			this.InitializeComponent();
			base.Title = title;
			this.imagea.Text = value;
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			try
			{
				Clipboard.SetText(this.imagea.Text);
				MessageBox.Show("复制完成");
			}
			catch
			{
				MessageBox.Show("复制失败");
			}
		}


	}
}
