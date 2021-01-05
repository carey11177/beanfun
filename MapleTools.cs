using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class MapleTools : Window, IComponentConnector
	{
		public double price = 99.8;
		public MapleTools()
		{
			this.InitializeComponent();
		}
		private void CheckBox_Checked(object sender, RoutedEventArgs e)
		{

			zhifubao.IsChecked = false;
		}

		private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
		{
			weixin.IsChecked = false;


		}



		private void five_Checked(object sender, RoutedEventArgs e)
		{
			if (five.IsChecked == true)
			{
				imagee.Text = 1 * price + "元";
			}

		}

		private void ten_Checked(object sender, RoutedEventArgs e)
		{

			imagee.Text = 2 * price + "元";

		}
		private void twenty_Checked(object sender, RoutedEventArgs e)
		{

			imagee.Text = 4 * price + "元";


		}
		private void thirhy_Checked(object sender, RoutedEventArgs e)
		{


			imagee.Text = 6 * price + "元";


		}
		private void fifthy_Checked(object sender, RoutedEventArgs e)
		{


			imagee.Text = 10 * price + "元";


		}
		private void recharge_Checked(object sender, RoutedEventArgs e)
		{
			BeanfunClient bf = new BeanfunClient();
			if (zhifubao.IsChecked == false && weixin.IsChecked == false)
			{
				MessageBox.Show("请选择支付方式");
			}
			else if (fifthy.IsChecked == false && five.IsChecked == false && ten.IsChecked == false && twenty.IsChecked == false && thirhy.IsChecked == false)
			{
				MessageBox.Show("请选择充值金额");
			}
			
			else 
            {
				bf.Recharge();
            }
		}

     
 

        private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void f(object A_0, RoutedEventArgs A_1)
		{
			if (App.LoginRegion == "HK")
			{
				MessageBox.Show("「新枫之谷」即時举报功能需要登录台湾Beanfun账号，不支持香港Beanfun账号，您可以自行注册一個台湾Beanfun账号用來举报。");
			}
			new WebBrowser("https://event.beanfun.com/customerservice/PluginReporting/PlayerReport.aspx").Show();
		}

				private void e(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://tw.beanfun.com/maplestory/event/20100806pl/index.html").Show();
		}

				private void d(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://tw.beanfun.com/maplestory/Exchange/EventSelect.aspx").Show();
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			new EquipCalculator().Show();
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			new WebBrowser("https://www.8591.com.tw/mallList-list-859.html?gst=1").Show();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			if (MessageBox.Show("是否需要回收空間(更新游戏時请不要使用此功能)？", "", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
			{
				return;
			}
			DirectoryInfo directoryInfo = new DirectoryInfo(Path.GetDirectoryName(App.MainWnd.settingPage.imagee.Text));
			foreach (string str in new string[]
			{
				"blob_storage",
				"GPUCache",
				"swiftshader",
				"VideoDecodeStats",
				"XignCode"
			})
			{
				if (Directory.Exists(directoryInfo.FullName + "\\" + str))
				{
					try
					{
						Directory.Delete(directoryInfo.FullName + "\\" + str, true);
					}
					catch
					{
					}
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				try
				{
					if (directoryInfo2.Name.EndsWith(".$$$"))
					{
						directoryInfo2.Delete(true);
					}
				}
				catch
				{
				}
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				try
				{
					if (fileInfo.Name.ToLower().EndsWith(".dmp"))
					{
						fileInfo.Delete();
					}
				}
				catch
				{
				}
			}
			MessageBox.Show("枫之谷资料夹空間回收完成");
		}

				

			private bool m_a;
	}
}
