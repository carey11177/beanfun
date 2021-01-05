using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class Settings : Page, IComponentConnector
	{
				public Settings()
		{
			this.InitializeComponent();
			this.imagec.IsChecked = new bool?(bool.Parse(global::g.a("autoStartGame", "false")));
			this.imagea.IsChecked = new bool?(bool.Parse(global::g.a("ask_update", "true")));
			this.imaged.IsChecked = new bool?(bool.Parse(global::g.a("minimize_to_tray", "false")));
			this.imagef.IsChecked = new bool?(bool.Parse(global::g.a("tradLogin", "true")));
			this.imageh.IsChecked = new bool?(bool.Parse(global::g.a("skipPlayWnd", "true")));
			this.imageg.IsChecked = new bool?(bool.Parse(global::g.a("autoKillPatcher", "true")));
			this.imageb.SelectedIndex = (global::g.a("updateChannel", "Stable").Equals("Stable") ? 0 : 1);
		}

				private void g(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd == null || App.MainWnd.o == null)
			{
				return;
			}
			if (App.MainWnd.return_page == null)
			{
				App.MainWnd.return_page = App.MainWnd.loginPage;
			}
			App.MainWnd.o.Content = App.MainWnd.return_page;
			App.MainWnd.return_page = null;
		}

				private void f(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd != null && App.MainWnd.settingPage != null && App.MainWnd.checkPlayPage != null)
			{
				bool? isChecked = this.imageh.IsChecked;
				bool flag = bool.Parse(global::g.a("skipPlayWnd", "true"));
				if (!(isChecked.GetValueOrDefault() == flag & isChecked != null))
				{
					global::g.b("skipPlayWnd", Convert.ToString(this.imageh.IsChecked.Value));
					App.MainWnd.checkPlayPage.IsEnabled = this.imageh.IsChecked.Value;
					return;
				}
			}
		}

				private void e(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd != null && App.MainWnd.settingPage != null)
			{
				bool? isChecked = this.imageg.IsChecked;
				bool flag = bool.Parse(global::g.a("autoKillPatcher", "true"));
				if (!(isChecked.GetValueOrDefault() == flag & isChecked != null))
				{
					global::g.b("autoKillPatcher", Convert.ToString(this.imageg.IsChecked.Value));
					App.MainWnd.checkPatcher.IsEnabled = this.imageg.IsChecked.Value;
					return;
				}
			}
		}

				private void d(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd != null && App.MainWnd.settingPage != null)
			{
				bool? isChecked = this.imagec.IsChecked;
				bool flag = bool.Parse(global::g.a("autoStartGame", "false"));
				if (!(isChecked.GetValueOrDefault() == flag & isChecked != null))
				{
					global::g.b("autoStartGame", Convert.ToString(this.imagec.IsChecked.Value));
					return;
				}
			}
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd != null && App.MainWnd.settingPage != null)
			{
				bool? isChecked = this.imagea.IsChecked;
				bool flag = bool.Parse(global::g.a("ask_update", "true"));
				if (!(isChecked.GetValueOrDefault() == flag & isChecked != null))
				{
					global::g.b("ask_update", Convert.ToString(this.imagea.IsChecked.Value));
					return;
				}
			}
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd == null || App.MainWnd.accountList == null || App.MainWnd.accountList.imagez == null || App.MainWnd.accountList.imageab == null)
			{
				return;
			}
			if (this.imagef.IsChecked.Value)
			{
				App.MainWnd.accountList.imagez.Visibility = Visibility.Visible;
				if (App.MainWnd.win_class_name != null && App.MainWnd.win_class_name != "" && App.MainWnd.game_exe != "" && App.MainWnd.login_action_type != 1)
				{
					App.MainWnd.accountList.imageab.Visibility = Visibility.Visible;
				}
				else
				{
					App.MainWnd.accountList.imageab.Visibility = Visibility.Collapsed;
				}
			}
			else
			{
				App.MainWnd.accountList.imagez.Visibility = Visibility.Collapsed;
			}
			if (App.MainWnd.settingPage != null)
			{
				bool flag = bool.Parse(global::g.a("tradLogin", "true"));
				bool? isChecked = this.imagef.IsChecked;
				if (!(flag == isChecked.GetValueOrDefault() & isChecked != null))
				{
					global::g.b("tradLogin", Convert.ToString(this.imagef.IsChecked));
					return;
				}
			}
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd != null && App.MainWnd.settingPage != null)
			{
				bool? isChecked = this.imaged.IsChecked;
				bool flag = bool.Parse(global::g.a("minimize_to_tray", "false"));
				if (!(isChecked.GetValueOrDefault() == flag & isChecked != null))
				{
					global::g.b("minimize_to_tray", Convert.ToString(this.imaged.IsChecked.Value));
					return;
				}
			}
		}

				private void a(object A_0, SelectionChangedEventArgs A_1)
		{
			if (App.MainWnd == null || App.MainWnd.settingPage == null || this.imageb.SelectedIndex == (global::g.a("updateChannel", "Stable").Equals("Stable") ? 0 : 1))
			{
				return;
			}
			global::g.b("updateChannel", (this.imageb.SelectedIndex == 0) ? "Stable" : "Beta");
		}

				

				internal CheckBox m_a;

				internal ComboBox m_b;

				internal CheckBox m_c;

				internal CheckBox m_d;

				internal TextBox m_e;

				internal CheckBox m_f;

				internal CheckBox m_g;

				internal CheckBox m_h;

				private bool m_i;
	}
}
