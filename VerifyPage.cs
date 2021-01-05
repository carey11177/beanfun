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
		public partial class VerifyPage : Page, IComponentConnector
	{
				public VerifyPage()
		{
			this.InitializeComponent();
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			if (App.MainWnd.loginPage.imagec.SelectedIndex == 2)
			{
				App.MainWnd.ddlAuthType_SelectionChanged(null, null);
			}
			App.MainWnd.o.Content = App.MainWnd.loginPage;
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			if (this.imagea.Text.Length <= 0)
			{
				MessageBox.Show("验证码不能为空。");
				return;
			}
			if (this.imaged.Text.Length <= 0)
			{
				MessageBox.Show("图形验证码不能为空。");
				return;
			}
			App.MainWnd.verifyWorker.RunWorkerAsync();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			this.imagee.Source = App.MainWnd.bfClient.getVerifyCaptcha(App.MainWnd.samplecaptcha);
			this.imaged.Text = "";
		}

			

			internal TextBox m_a;

				internal CheckBox m_b;

				internal Label m_c;

				internal TextBox m_d;

				internal Image m_e;

				private bool m_f;
	}
}
