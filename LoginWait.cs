using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class LoginWait : Page, IComponentConnector
	{
				public LoginWait()
		{
			this.InitializeComponent();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			App.MainWnd.loginWorker.CancelAsync();
			App.MainWnd.bfAPPAutoLogin.IsEnabled = false;
			this.imagea.Content = "正在登录,请稍等...";
			if (App.MainWnd.loginPage.imagec.SelectedIndex == 2)
			{
				App.MainWnd.ddlAuthType_SelectionChanged(null, null);
			}
			App.MainWnd.o.Content = App.MainWnd.loginPage;
		}

		//	

				internal Label m_a;

				private bool b;
	}
}
