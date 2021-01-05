using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{

		public partial class AccRecovery : Window, IComponentConnector
	{

		private AccountManager m_a;
		public AccRecovery(AccountManager a)
		{
			this.InitializeComponent();
			this.m_a = a;
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			string s = this.m_a.exportRecord();
			byte[] bytes = Encoding.UTF8.GetBytes(s);
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] rgbKey = md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes(this.imageb.Text));
			byte[] inArray = new RijndaelManaged().CreateEncryptor(rgbKey, md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes("pungin"))).TransformFinalBlock(bytes, 0, bytes.Length);
			this.imagec.Text = Convert.ToBase64String(inArray);
			MessageBox.Show("匯出完成");
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(this.imageb.Text);
			MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] rgbKey = md5CryptoServiceProvider.ComputeHash(bytes);
			ICryptoTransform cryptoTransform = new RijndaelManaged().CreateDecryptor(rgbKey, md5CryptoServiceProvider.ComputeHash(Encoding.UTF8.GetBytes("pungin")));
			byte[] array = Convert.FromBase64String(this.imagec.Text);
			try
			{
				byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
				string @string = Encoding.UTF8.GetString(bytes2);
				if (!this.m_a.importRecord(@string))
				{
					MessageBox.Show("匯入失败");
				}
				else
				{
					MessageBox.Show("匯入成功");
					App.MainWnd.ddlAuthTypeItemsInit();
				}
			}
			catch
			{
				MessageBox.Show("密码或资料错误，解密失败");
			}
		}

	


							
	}
}
