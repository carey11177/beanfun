using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class LoginPage : Page, IComponentConnector
	{
				public LoginPage()
		{
			this.InitializeComponent();
			if (g.a("loginRegion", "TW") == "TW")
			{
				this.imagea.IsEnabled = false;
			}
			else
			{
				this.imagea.IsEnabled = true;
			}
			this.imageb.IsEnabled = !this.imagea.IsEnabled;
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			if (!this.imagea.IsEnabled)
			{
				return;
			}
			this.imagea.IsEnabled = !this.imagea.IsEnabled;
			this.imageb.IsEnabled = !this.imagea.IsEnabled;
			App.LoginRegion = "TW";
			g.b("loginRegion", App.LoginRegion);
			App.MainWnd.ddlAuthTypeItemsInit();
			App.MainWnd.reLoadGameInfo();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			if (!this.imageb.IsEnabled)
			{
				return;
			}
			this.imageb.IsEnabled = !this.imageb.IsEnabled;
			this.imagea.IsEnabled = !this.imageb.IsEnabled;
			App.LoginRegion = "HK";
			g.b("loginRegion", App.LoginRegion);
			App.MainWnd.ddlAuthTypeItemsInit();
			App.MainWnd.reLoadGameInfo();
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			new AccRecovery(App.MainWnd.accountManager).ShowDialog();
		}


		
		public List<string> item_TW = new List<string>
		{
			"账号密码",
			"PLAYSAFE",
			"QR Code便利登"
		};

				public List<string> item_HK = new List<string>
		{
			"账号密码"
		};

				public id_pass_form id_pass = new id_pass_form();

				public qr_form qr = new qr_form();

			
	}
}
