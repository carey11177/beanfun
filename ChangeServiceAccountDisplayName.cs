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
		public partial class ChangeServiceAccountDisplayName : Window, IComponentConnector
	{
				public ChangeServiceAccountDisplayName(string name)
		{
			this.InitializeComponent();
			this.imagea.Text = name;
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			base.Close();
			if (!App.MainWnd.ChangeServiceAccountDisplayName(this.imagea.Text))
			{
				MessageBox.Show("未知错误, 变更游戏账号名失败。", "系统信息");
			}
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			base.Close();
		}

			

				internal TextBox m_a;

				private bool m_b;
	}
}
