using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class KartTools : Window, IComponentConnector
	{
				public KartTools()
		{
			this.InitializeComponent();
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void f(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://tw.beanfun.com/KartRider/guild/maneger_data.aspx").Show();
		}

				private void e(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://tw.beanfun.com/kartrider/guild/rank.aspx").Show();
		}

				private void d(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://tw.beanfun.com/KartRider/guild/create.aspx").Show();
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://tw.beanfun.com/KartRider/guild/rank_team_in.aspx").Show();
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://tw.beanfun.com/KartRider/guild/search_member.aspx").Show();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://tw.beanfun.com/KartRider/guild/leave_guild_Member.aspx").Show();
		}

	
	}
}
