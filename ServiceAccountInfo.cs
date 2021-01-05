using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace Beanfun
{
		public partial class ServiceAccountInfo : Window, IComponentConnector
	{
				public ServiceAccountInfo(BeanfunClient.ServiceAccount account)
		{
			this.InitializeComponent();
			this.imageb.Text = account.ssn;
			this.imagec.Text = account.sname;
			this.imagea.Text = account.sid;
			this.imagef.Content = (account.isEnable ? "正常" : "锁定");
			this.imagef.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(account.isEnable ? "Green" : "Red"));
			if (account.sauthtype == null)
			{
				this.imaged.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.imagee.Text = account.sauthtype;
			}
			if (account.screatetime == null)
			{
				this.imageg.Visibility = Visibility.Collapsed;
			}
			else
			{
				this.imagei.Content = "于 " + account.screatetime + " 建立";
				this.imageh.Content = this.a(account.screatetime);
			}
			if (account.slastusedtime == null)
			{
				this.imagej.Visibility = Visibility.Collapsed;
				return;
			}
			this.imagek.Content = "上次于 " + account.slastusedtime + " 登录";
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private string a(string A_0)
		{
			DateTime value = Convert.ToDateTime(A_0);
			return Convert.ToString(Convert.ToDateTime(DateTime.Now).Subtract(value).Days);
		}

			
				internal TextBox m_a;

				internal TextBox m_b;

				internal TextBox m_c;

				internal DockPanel m_d;

				internal TextBox m_e;

				internal Label m_f;

				internal StackPanel gm_;

				internal Label m_h;

				internal Label m_i;

				internal StackPanel m_j;

				internal Label m_k;

				private bool m_l;
	}
}
