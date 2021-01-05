using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Windows;
using Microsoft.Win32;

namespace Beanfun
{
		public partial class App : Application
	{
				public App()
		{
			AppDomain.CurrentDomain.AssemblyResolve += this.a;
		}

				private Assembly a(object A_0, ResolveEventArgs A_1)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			AssemblyName assemblyName = new AssemblyName(A_1.Name);
			if (assemblyName.Name.EndsWith(".resources"))
			{
				return null;
			}
			string text = assemblyName.Name + ".dll";
			if (!assemblyName.CultureInfo.Equals(CultureInfo.InvariantCulture))
			{
				text = string.Format("{0}\\{1}", assemblyName.CultureInfo, text);
			}
			Assembly result;
			using (Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(text))
			{
				if (manifestResourceStream == null)
				{
					result = null;
				}
				else
				{
					byte[] array = new byte[manifestResourceStream.Length];
					manifestResourceStream.Read(array, 0, array.Length);
					result = Assembly.Load(array);
				}
			}
			return result;
		}

				private void a(object A_0, StartupEventArgs A_1)
		{
			ServicePointManager.SecurityProtocol = (SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls12);
			if (File.Exists(string.Format("{0}\\BFUpdater.exe", Environment.CurrentDirectory)))
			{
				try
				{
					File.Delete(string.Format("{0}\\BFUpdater.exe", Environment.CurrentDirectory));
				}
				catch
				{
				}
			}
			if (!App.a(378389) && MessageBox.Show("侦测到你所安裝的.Net Framework版本低于4.5，如果运行中出現程式中断的情況建议你安裝版本为4.5以上的.Net Framework。\r\n\r\n是否前往下载 .Net Framework 4.5.2 ？", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				Process.Start("https://dotnet.microsoft.com/download/thank-you/net452");
			}
			LoginRegionSelection loginRegionSelection = null;
			if (g.a("loginRegion") == string.Empty)
			{
				loginRegionSelection = new LoginRegionSelection();
				loginRegionSelection.ShowDialog();
				if (g.a("loginRegion") == string.Empty)
				{
					return;
				}
			}
			App.MainWnd = new MainWindow();
			App.MainWnd.Show();
			if (loginRegionSelection != null)
			{
				loginRegionSelection.Close();
			}
			if (App.MainWnd.loginPage.imagec.SelectedIndex != 0)
			{
				return;
			}
			if (App.MainWnd.loginPage.id_pass.imagef.IsChecked.Value)
			{
				App.MainWnd.do_Login();
			}
		}

				public bool compareFile(string path1, string path2)
		{
			HashAlgorithm hashAlgorithm = HashAlgorithm.Create();
			FileStream fileStream = File.OpenRead(path1);
			byte[] value = hashAlgorithm.ComputeHash(fileStream);
			fileStream.Close();
			FileStream fileStream2 = File.OpenRead(path2);
			byte[] value2 = hashAlgorithm.ComputeHash(fileStream2);
			fileStream2.Close();
			return BitConverter.ToString(value) == BitConverter.ToString(value2);
		}

				private static bool a(int A_0)
		{
			bool result;
			using (RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey("SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full\\"))
			{
				if (registryKey != null && registryKey.GetValue("Release") != null)
				{
					result = ((int)registryKey.GetValue("Release") >= A_0);
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

				private void a(object A_0, ExitEventArgs A_1)
		{
			if (App.MainWnd != null && App.MainWnd.bfClient != null)
			{
				try
				{
					App.MainWnd.bfClient.Logout();
				}
				catch
				{
				}
			}
			foreach (Process process in Process.GetProcessesByName("BFWidgetKernel"))
			{
				try
				{
					process.Kill();
				}
				catch
				{
				}
			}
		}

				public static MainWindow MainWnd;

				public static string LoginRegion = "TW";
	}
}
