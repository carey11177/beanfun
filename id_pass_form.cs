using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class id_pass_form : Page, IComponentConnector, IStyleConnector
	{
				public id_pass_form()
		{
			this.InitializeComponent();
			base.Loaded += this.a;
		}

				private void j(object A_0, RoutedEventArgs A_1)
		{
			this.imagef.IsChecked = new bool?(false);
		}

				private void i(object A_0, RoutedEventArgs A_1)
		{
			this.imagee.IsChecked = new bool?(true);
		}

				private void h(object A_0, RoutedEventArgs A_1)
		{
			string uri;
			if (App.LoginRegion == "TW")
			{
				uri = "https://tw.beanfun.com/TW/signup/Join_beanfun_signup.aspx?service=999999_T0";
			}
			else
			{
				uri = "http://hk.beanfun.com/beanfun_web_ap/signup/preregistration.aspx?service=999999_T0";
			}
			new WebBrowser(uri).Show();
		}

				private void g(object A_0, RoutedEventArgs A_1)
		{
			string uri;
			if (App.LoginRegion == "TW")
			{
				uri = "https://tw.beanfun.com/member/forgot_pwd.aspx";
			}
			else
			{
				uri = "http://hk.beanfun.com/member/forgot_pwd.aspx";
			}
			new WebBrowser(uri).Show();
		}

				private void f(object A_0, RoutedEventArgs A_1)
		{
			App.MainWnd.o.Content = App.MainWnd.manageAccPage;
		}

				private void e(object A_0, RoutedEventArgs A_1)
		{
			if (this.imagec.Text == null || this.imagec.Text == "")
			{
				MessageBox.Show("请输入账号");
				return;
			}
			if (this.imaged.Password == null || this.imaged.Password == "")
			{
				MessageBox.Show("请输入密码");
				return;
			}
			App.MainWnd.do_Login();
		}

				private void d(object A_0, RoutedEventArgs A_1)
		{
			new GameList().ShowDialog();
		}

				private void b(object A_0, EventArgs A_1)
		{
			TextBox textBox = this.imagec.Template.FindName("PART_EditableTextBox", this.imagec) as TextBox;
			int caretIndex = 0;
			if (textBox != null)
			{
				caretIndex = textBox.CaretIndex;
			}
			string text = this.imagec.Text;
			List<string> list = new List<string>();
			List<string> list2 = new List<string>();
			string[] accountList = App.MainWnd.accountManager.getAccountList(App.LoginRegion);
			bool flag = false;
			foreach (string item in accountList)
			{
				if (item == text)
				{
					flag = true;
				}
				list2.Add(item);
			}
			list = list2.FindAll(new Predicate<string>(this.b));
			if (!flag && this.imagec.Text != "" && list.Count > 0)
			{
				this.imagec.IsDropDownOpen = true;
				this.imagec.ItemsSource = null;
				this.imagec.ItemsSource = list;
				this.imagec.SelectedIndex = -1;
				this.imagec.Text = text;
			}
			else
			{
				this.imagec.ItemsSource = null;
				this.imagec.ItemsSource = list2;
				if (!flag)
				{
					this.imagec.SelectedIndex = -1;
					this.imagec.Text = text;
				}
				this.imagec.IsDropDownOpen = false;
				if (flag)
				{
					if (list2.Count > 0)
					{
						this.imagec.SelectedItem = text;
					}
					this.imaged.Password = "";
					this.imagee.IsChecked = new bool?(false);
					int methodByAccount = App.MainWnd.accountManager.getMethodByAccount(App.LoginRegion, this.imagec.Text);
					if (methodByAccount > -1)
					{
						App.MainWnd.loginPage.imagec.SelectedIndex = methodByAccount;
					}
					App.MainWnd.ddlAuthType_SelectionChanged(null, null);
				}
			}
			if (textBox != null)
			{
				textBox.CaretIndex = caretIndex;
			}
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			TextBox textBox = this.imagec.Template.FindName("PART_EditableTextBox", this.imagec) as TextBox;
			if (textBox != null)
			{
				textBox.CaretIndex = textBox.Text.Length;
			}
		}

				private void a(object A_0, EventArgs A_1)
		{
			TextBox textBox = this.imagec.Template.FindName("PART_EditableTextBox", this.imagec) as TextBox;
			if (textBox != null)
			{
				if ((this.imagec.ItemsSource as List<string>).FindAll(new Predicate<string>(this.a)).Count <= 0)
				{
					textBox.SelectionLength = 0;
					return;
				}
				textBox.CaretIndex = textBox.Text.Length;
			}
		}

				private void b(object A_0, RoutedEventArgs A_1)
        {
            string text = this.imagec.Text;
            string text2 = (string)(A_0 as Button).Tag;
            if (MessageBox.Show("即將移除账号「" + text2 + "」，此操作不可恢復，是否确认要移除？", "移除账号", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                App.MainWnd.accountManager.removeAccount(App.LoginRegion, text2);
                App.MainWnd.ddlAuthTypeItemsInit();
                foreach (object obj in ((IEnumerable)this.imagec.Items))
                {
                    string selectedItem = (string)obj;
                    if (text == selectedItem)
                    {
                        this.imagec.SelectedItem = selectedItem;
                        break;
                    }
                }
            }
        }



        [CompilerGenerated]
		private void a(object A_0, RoutedEventArgs A_1)
		{
			TextBox textBox = this.imagec.Template.FindName("PART_EditableTextBox", this.imagec) as TextBox;
			if (textBox != null)
			{
				InputMethod.SetPreferredImeState(textBox, InputMethodState.Off);
			}
		}

				[CompilerGenerated]
		private bool b(string A_0)
		{
			return A_0.Contains(this.imagec.Text.Trim());
		}

				[CompilerGenerated]
		private bool a(string A_0)
		{
			return A_0.Equals(this.imagec.Text.Trim());
		}

        public void Connect(int connectionId, object target)
        {
            throw new NotImplementedException();
        }

                internal Image m_a;

				internal TextBlock m_b;

				internal ComboBox m_c;

				internal PasswordBox m_d;

				internal CheckBox m_e;

				internal CheckBox m_f;

				internal Button m_g;

				internal TextBlock m_h;

				private bool m_i;
	}
}
