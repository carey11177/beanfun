using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class About : Page, IComponentConnector
	{

        public About()
        {
            this.InitializeComponent();
        }

                private void g(object A_0, RoutedEventArgs A_1)
		{
			new Donate().ShowDialog();
		}

				private void f(object A_0, RoutedEventArgs A_1)
		{
			if (App.MainWnd == null)
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

				private void e(object A_0, RoutedEventArgs A_1)
		{
			App.MainWnd.CheckUpdates(true);
		}

				private void d(object A_0, RoutedEventArgs A_1)
		{
			string text = "pungin@msn.com ";
			string text2 = "缤放 反馈/建议";
			string text3 = "软件版本: " + this.imagea.Text + "%0d反馈/建议信息:%0d";
			Process.Start(string.Concat(new string[]
			{
				"mailto:",
				text,
				"?subject=",
				text2,
				"&body=",
				text3
			}));
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			Process.Start("https://github.com/pungin/Beanfun/issues/new");
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			Process.Start("https://jq.qq.com/?_wv=1027&k=5GkGMMm");
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			Process.Start("https://t.me/tw_maplestory");
		}		

			
	}
}
