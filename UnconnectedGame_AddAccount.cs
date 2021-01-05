using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class UnconnectedGame_AddAccount : Window, IComponentConnector
	{
				public UnconnectedGame_AddAccount()
		{
			this.n_a = App.MainWnd.UnconnectedGame_AddAccountInit();
			if (this.n_a == null)
			{
				MessageBox.Show("發生未知错误", "系统信息");
				base.Close();
				return;
			}
			this.InitializeComponent();
			string text = this.n_a.Get("GameName");
			string text2 = this.n_a.Get("AccountLen");
			this.n_a.Remove("GameName");
			this.n_a.Remove("AccountLen");
			if (this.n_a.Get("CheckNickName") == "")
			{
				this.imageg.Visibility = Visibility.Collapsed;
				this.imagen.Visibility = Visibility.Collapsed;
			}
			this.n_a.Remove("CheckNickName");
			this.imaged.Text = text2;
			this.imageb.Text = text;
			this.imagec.Text = text;
			this.imagee.Text = text;
			this.imagej.Text = text;
			this.imagel.Text = text;
			this.imageh.Text = text;
			this.imageq.Text = text;
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void d(object A_0, RoutedEventArgs A_1)
		{
			this.n_a = App.MainWnd.UnconnectedGame_AddUnconnectedCheck(this.imagef.Text, (this.imageg.Visibility == Visibility.Visible) ? "" : null, this.n_a);
			if (this.n_a == null || this.n_a.Get("lblErrorMessage") == "")
			{
				this.n_a = null;
				MessageBox.Show("發生未知错误", "系统信息");
				return;
			}
			//this.imageo.Visibility = Visibility.Visible;
			//this.imageo.Content = this.n_a.Get("lblErrorMessage");
			this.n_a.Remove("lblErrorMessage");
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			if (this.imagen.Visibility != Visibility.Visible)
			{
				return;
			}
			this.n_a = App.MainWnd.UnconnectedGame_AddAccountCheckNickName(this.imagei.Text, this.n_a);
			if (this.n_a == null || this.n_a.Get("lblErrorMessage") == "")
			{
				this.n_a = null;
				MessageBox.Show("發生未知错误", "系统信息");
				return;
			}
			//this.imageo.Visibility = Visibility.Visible;
			//this.imageo.Content = this.imagea.Get("lblErrorMessage");
			this.n_a.Remove("lblErrorMessage");
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			string serviceContract = App.MainWnd.GetServiceContract();
			if (serviceContract == "")
			{
				MessageBox.Show("發生未知错误", "系统信息");
				return;
			}
			new Contract(serviceContract).ShowDialog();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			string text = this.imaged.Text;
			if (text == null || text == "" || !text.Contains(" - "))
			{
				MessageBox.Show("發生未知错误！", "系统信息");
				return;
			}
			string[] array = text.Split(new string[]
			{
				" - "
			}, StringSplitOptions.None);
			byte b = byte.Parse(array[0]);
			byte b2 = byte.Parse(array[1]);
			if (this.imagef.Text == null || this.imagef.Text == "")
			{
				MessageBox.Show("请输入账号！", "系统信息");
				return;
			}
			if (this.imagef.Text.Length < (int)b || this.imagef.Text.Length > (int)b2)
			{
				MessageBox.Show("账号位數不正确！", "系统信息");
				return;
			}
			if (this.imagek.Password == null || this.imagek.Password == "")
			{
				MessageBox.Show("请输入密码！", "系统信息");
				return;
			}
			if (this.imagek.Password.Length < (int)b || this.imagek.Password.Length > (int)b2)
			{
				MessageBox.Show("密码位數不正确！", "系统信息");
				return;
			}
			if (this.imagem.Password == null || this.imagem.Password == "")
			{
				MessageBox.Show("请输入确认密码！", "系统信息");
				return;
			}
			if (this.imagem.Password.Length < (int)b || this.imagem.Password.Length > (int)b2)
			{
				MessageBox.Show("确认密码位數不正确！", "系统信息");
				return;
			}
			if (this.imageg.Visibility == Visibility.Visible)
			{
				if (this.imagei.Text == null || this.imagei.Text == "")
				{
					MessageBox.Show("请输入昵称！", "系统信息");
					return;
				}
				if (this.imagei.Text.Length < 2 || this.imagei.Text.Length > 6)
				{
					MessageBox.Show("昵称位数不正确！", "系统信息");
					return;
				}
			}
			if (!this.imagep.IsChecked.Value)
			{
				MessageBox.Show("您必須先同意服務条款才可新增账号！", "系统信息");
				return;
			}
			string text2 = App.MainWnd.UnconnectedGame_AddAccount(this.imagef.Text, this.imagek.Password, this.imagem.Password, (this.imageg.Visibility == Visibility.Visible) ? this.imagei.Text : null, this.n_a);
			if (text2 == "")
			{
				base.Close();
				return;
			}
			if (text2 == null)
			{
				MessageBox.Show("新增游戏账号失败, 可能这個游戏无法创建账号。", "系统信息");
				return;
			}
			//this.imageo.Visibility = Visibility.Visible;
			//this.imageo.Content = text2;
		}

	

				private NameValueCollection n_a;

				internal Run m_b;

				internal Run m_c;

				internal Run m_d;

				internal Run m_e;

				internal TextBox m_f;

				internal StackPanel m_g;

				internal Run m_h;

				internal TextBox m_i;

				internal Run m_j;

				internal PasswordBox m_k;

				internal Run m_l;

				internal PasswordBox m_m;

				internal TextBlock m_n;

				internal Label m_o;

				internal CheckBox m_p;

				internal Run m_q;

				private bool m_r;
	}
}
