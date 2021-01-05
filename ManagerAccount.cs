using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class ManagerAccount : Page, IComponentConnector
	{
				public ManagerAccount()
		{
			this.InitializeComponent();
		}

				public void setupAccList(MainWindow MainWnd)
		{
			string region = (!this.imagea.IsEnabled) ? "TW" : "HK";
			List<string> list = (region == "TW") ? MainWnd.loginPage.item_TW : MainWnd.loginPage.item_HK;
			string[] accountList = MainWnd.accountManager.getAccountList(region);
			List<ManagerAccount.BeanfunAccount> list2 = new List<ManagerAccount.BeanfunAccount>();
			foreach (string account in accountList)
			{
				list2.Add(new ManagerAccount.BeanfunAccount(list[MainWnd.accountManager.getMethodByAccount(region, account)], account, (MainWnd.accountManager.getPasswordByAccount(region, account) != "") ? "是" : "否", MainWnd.accountManager.getAutoLoginByAccount(region, account) ? "是" : "否", (MainWnd.accountManager.getVerifyByAccount(region, account) != "") ? "是" : "否"));
			}
			this.imagec.ItemsSource = null;
			this.imagec.ItemsSource = list2;
			if (accountList.Length != 0)
			{
				this.imagec.SelectedIndex = 0;
				return;
			}
			this.imageg.IsEnabled = false;
		}

				private void g(object A_0, RoutedEventArgs A_1)
		{
			if (!this.imagea.IsEnabled)
			{
				return;
			}
			this.imagea.IsEnabled = false;
			this.imageb.IsEnabled = true;
			this.setupAccList(App.MainWnd);
		}

				private void f(object A_0, RoutedEventArgs A_1)
		{
			if (!this.imageb.IsEnabled)
			{
				return;
			}
			this.imagea.IsEnabled = true;
			this.imageb.IsEnabled = false;
			this.setupAccList(App.MainWnd);
		}

				private void e(object A_0, RoutedEventArgs A_1)
		{
			if (this.imagec.SelectedIndex <= 0)
			{
				return;
			}
			this.a(true);
		}

				private void d(object A_0, RoutedEventArgs A_1)
		{
			if (this.imagec.SelectedIndex + 1 >= this.imagec.Items.Count)
			{
				return;
			}
			this.a(false);
		}

				private void a(bool A_0)
		{
			string region = (!this.imagea.IsEnabled) ? "TW" : "HK";
			string account = ((ManagerAccount.BeanfunAccount)this.imagec.SelectedItem).account;
			string passwordByAccount = App.MainWnd.accountManager.getPasswordByAccount(region, account);
			string verifyByAccount = App.MainWnd.accountManager.getVerifyByAccount(region, account);
			int methodByAccount = App.MainWnd.accountManager.getMethodByAccount(region, account);
			bool autoLoginByAccount = App.MainWnd.accountManager.getAutoLoginByAccount(region, account);
			int num = this.imagec.SelectedIndex + (A_0 ? -1 : 1);
			App.MainWnd.accountManager.addAccount(num, region, account, passwordByAccount, verifyByAccount, methodByAccount, autoLoginByAccount);
			this.setupAccList(App.MainWnd);
			App.MainWnd.ddlAuthTypeItemsInit();
			this.imagec.SelectedIndex = num;
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			if (!this.imagef.IsEnabled)
			{
				return;
			}
			new AddAccount().ShowDialog();
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			if (!this.imageg.IsEnabled)
			{
				return;
			}
			if (this.imagec.SelectedItems.Count < 1)
			{
				return;
			}
			string str;
			if (this.imagec.SelectedItems.Count > 1)
			{
				str = string.Format(" {0} 個账号", this.imagec.SelectedItems.Count);
			}
			else
			{
				str = "账号「" + ((ManagerAccount.BeanfunAccount)this.imagec.SelectedItem).account + "」";
			}
			if (MessageBox.Show("即將移除" + str + "，此操作不可恢復，是否确认要移除？", "移除账号", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				string region = (!this.imagea.IsEnabled) ? "TW" : "HK";
				foreach (object obj in this.imagec.SelectedItems)
				{
					ManagerAccount.BeanfunAccount beanfunAccount = (ManagerAccount.BeanfunAccount)obj;
					App.MainWnd.accountManager.removeAccount(region, beanfunAccount.account);
				}
				this.setupAccList(App.MainWnd);
				App.MainWnd.ddlAuthTypeItemsInit();
			}
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd.accountManager.getAccountList("TW").Length + App.MainWnd.accountManager.getAccountList("HK").Length > 0)
			{
				App.MainWnd.loginPage.id_pass.imageh.Visibility = Visibility.Visible;
			}
			else
			{
				App.MainWnd.loginPage.id_pass.imageh.Visibility = Visibility.Collapsed;
			}
			if (App.MainWnd.loginPage.imagec.SelectedIndex == 2)
			{
				App.MainWnd.ddlAuthType_SelectionChanged(null, null);
			}
			App.MainWnd.o.Content = App.MainWnd.loginPage;
		}

				private void a(object A_0, SelectionChangedEventArgs A_1)
		{
			this.imaged.IsEnabled = false;
			this.imagee.IsEnabled = false;
			if (this.imagec.Items.Count <= 0)
			{
				this.imageg.IsEnabled = false;
				return;
			}
			if (this.imagec.SelectedIndex > 0)
			{
				this.imaged.IsEnabled = true;
			}
			if (this.imagec.SelectedIndex + 1 < this.imagec.Items.Count)
			{
				this.imagee.IsEnabled = true;
			}
			this.imageg.IsEnabled = true;
		}

			


				public class BeanfunAccount
		{
			public string method
			{
				[CompilerGenerated]
				get
				{
					return this.m_a;
				}
				[CompilerGenerated]
				set
				{
					this.m_a = value;
				}
			}

					
			public string account
			{
				[CompilerGenerated]
				get
				{
					return this.m_b;
				}
				[CompilerGenerated]
				set
				{
					this.m_b = value;
				}
			}

			public string isSavePwd
			{
				[CompilerGenerated]
				get
				{
					return this.m_c;
				}
				[CompilerGenerated]
				set
				{
					this.m_c = value;
				}
			}

			public string isAutoLogin
			{
				[CompilerGenerated]
				get
				{
					return this.m_d;
				}
				[CompilerGenerated]
				set
				{
					this.m_d = value;
				}
			}

		
			public string isSaveVerify
			{
				[CompilerGenerated]
				get
				{
					return this.m_e;
				}
				[CompilerGenerated]
				set
				{
					this.m_e = value;
				}
			}

						public BeanfunAccount()
			{
				this.method = null;
				this.account = null;
				this.isSavePwd = null;
				this.isAutoLogin = null;
				this.isSaveVerify = null;
			}

						public BeanfunAccount(string method, string account, string isSavePwd, string isAutoLogin, string isSaveVerify = null)
			{
				this.method = method;
				this.account = account;
				this.isSavePwd = isSavePwd;
				this.isAutoLogin = isAutoLogin;
				this.isSaveVerify = isSaveVerify;
			}

						[CompilerGenerated]
			private string m_a;

						[CompilerGenerated]
			private string m_b;

						[CompilerGenerated]
			private string m_c;

						[CompilerGenerated]
			private string m_d;

						[CompilerGenerated]
			private string m_e;
		}
	}
}
