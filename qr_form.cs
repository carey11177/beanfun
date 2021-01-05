using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class qr_form : Page, IComponentConnector
	{
				public qr_form()
		{
			this.InitializeComponent();
			this.imageb.IsChecked = new bool?(bool.Parse(g.a("useNewQRCode", "true")));
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			new GameList().ShowDialog();
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			App.MainWnd.refreshQRCode();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd != null && App.MainWnd.loginPage != null && App.MainWnd.loginPage.qr != null)
			{
				bool? isChecked = this.imageb.IsChecked;
				bool flag = bool.Parse(g.a("useNewQRCode", "true"));
				if (!(isChecked.GetValueOrDefault() == flag & isChecked != null))
				{
					g.b("useNewQRCode", Convert.ToString(this.imageb.IsChecked.Value));
					App.MainWnd.refreshQRCode();
					return;
				}
			}
		}

	
				internal Button m_a;

				internal CheckBox m_b;

				internal Button m_c;

				internal Image m_d;

				private bool m_e;
	}
}
