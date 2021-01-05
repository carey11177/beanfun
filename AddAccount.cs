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
		public partial class AddAccount : Window, IComponentConnector
	{
				public AddAccount()
		{
			this.InitializeComponent();
			this.a();
		}

				private void a()
		{
			string text = (this.imagea.SelectedIndex == 0) ? "TW" : "HK";
			this.imageb.Items.Clear();
			if (text == "TW")
			{
				foreach (string newItem in App.MainWnd.loginPage.item_TW)
				{
					if (!(newItem == "QR Code便利登"))
					{
						this.imageb.Items.Add(newItem);
					}
				}
				this.imagee.Visibility = Visibility.Visible;
			}
			else
			{
				foreach (string newItem2 in App.MainWnd.loginPage.item_HK)
				{
					this.imageb.Items.Add(newItem2);
				}
				this.imagee.Visibility = Visibility.Collapsed;
				this.imagee.Text = "";
			}
			this.imageb.SelectedIndex = 0;
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void b(object A_0, SelectionChangedEventArgs A_1)
		{
			if (this.imageb != null)
			{
				this.a();
			}
		}

				private void a(object A_0, SelectionChangedEventArgs A_1)
		{
			if (((this.imagea.SelectedIndex == 0) ? "TW" : "HK") == "HK" && this.imageb.SelectedIndex > 0)
			{
				this.imageb.SelectedIndex = 0;
			}
			this.imaged.Text = "";
			this.imagef.IsChecked = new bool?(false);
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			if (this.imagec.Text == null || this.imagec.Text == "")
			{
				MessageBox.Show("请输入账号");
				return;
			}
			App.MainWnd.accountManager.addAccount((this.imagea.SelectedIndex == 0) ? "TW" : "HK", this.imagec.Text, this.imaged.Text, this.imagee.Text, this.imageb.SelectedIndex, !(this.imaged.Text == "") && this.imagef.IsChecked.Value);
			App.MainWnd.ddlAuthTypeItemsInit();
			base.Close();
		}		

		
	}
}
