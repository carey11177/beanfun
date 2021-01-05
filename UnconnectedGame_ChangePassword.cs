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
		public partial class UnconnectedGame_ChangePassword : Window, IComponentConnector
	{
				public UnconnectedGame_ChangePassword()
		{
			this.InitializeComponent();
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			string text = App.MainWnd.UnconnectedGame_ChangePassword(this.imagea.Text);
			if (text == null)
			{
				MessageBox.Show("未知错误。", "系统信息");
				return;
			}
			if (text.StartsWith("verify_code"))
			{
				MessageBox.Show("请至您已认证的e - mail信箱中收取密码設定信喲！\r\n确认码: " + text.Replace("verify_code", "") + "\r\n为保障安全!请您在收到信後，\r\n点选連结前先确认信中的确认码是否相同正确喔！", "资料已寄出！");
				base.Close();
				return;
			}
			this.b.Visibility = Visibility.Visible;
			this.b.Content = text;
		}

			

				internal TextBox m_a;

				internal Label m_b;

				private bool m_c;
	}
}
