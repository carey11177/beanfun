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
		public partial  class AddServiceAccount : Window, IComponentConnector
	{
				public AddServiceAccount()
		{
			this.InitializeComponent();
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			if (this.imagea.Text == null || this.imagea.Text == "")
			{
				MessageBox.Show("请输入使用者名稱！", "系统信息");
				return;
			}
			if (!this.imageb.IsChecked.Value)
			{
				MessageBox.Show("您必須先同意服務条款才可新增账号！", "系统信息");
				return;
			}
			base.Close();
			if (!App.MainWnd.AddServiceAccount(this.imagea.Text))
			{
				MessageBox.Show("新增游戏账号失败, 可能這個游戏无法创建账号。", "系统信息");
			}
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			base.Close();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			string serviceContract = App.MainWnd.GetServiceContract();
			if (serviceContract == "")
			{
				MessageBox.Show("發生未知错误", "系统信息");
				return;
			}
			new Contract(serviceContract).ShowDialog();
		}

		
	}
}
