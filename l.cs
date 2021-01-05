using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Xml;
using Beanfun;

internal class l
{
		internal static void a(Version A_0, bool A_1)
	{
		string a_ = l.m_a + "VersionInfo.xml";
		try
		{
			MemoryStream inStream = new d(10000).a(a_);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(inStream);
			l.a(xmlDocument, A_0, A_1);
		}
		catch (Exception)
		{
		}
	}

		private static void a(XmlDocument A_0, Version A_1, bool A_2)
	{
		string value = A_0.SelectSingleNode("/VersionInfo/Version/text()").Value;
		if (l.a(A_1, new Version(value)))
		{
			try
			{
				Version version = new Version(A_0.SelectSingleNode("/VersionInfo/Version/text()").Value);
				string value2 = A_0.SelectSingleNode("/VersionInfo/Date/text()").Value;
				string value3 = A_0.SelectSingleNode("/VersionInfo/Note/text()").Value;
				if (MessageBox.Show(string.Format("检测到新版本 {0}.{1}.{2}({3}) 当前: {4}.{5}.{6}({7})\r\n\r\n{8}\r\n", new object[]
				{
					version.Major,
					version.Minor,
					version.Build,
					version.Revision,
					A_1.Major,
					A_1.Minor,
					A_1.Build,
					A_1.Revision,
					value3
				}) + "\r\n是否更新(会重启软件)？", "更新检测", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
				{
					l.a(A_0);
				}
				return;
			}
			catch (Exception)
			{
				return;
			}
		}
		if (A_2)
		{
			MessageBox.Show("未检测到有更新。", "更新检测", MessageBoxButton.OK);
		}
	}

		private static void a(XmlDocument A_0)
	{
		string value = A_0.SelectSingleNode("/VersionInfo/Url/text()").Value;
		Version a_ = new Version(A_0.SelectSingleNode("/VersionInfo/UpdaterVersion/text()").Value);
		string text = Environment.CurrentDirectory + "\\";
		l.c.Clear();
		if (!File.Exists(text + "BFUpdater.exe") || l.a(l.a(text + "BFUpdater.exe"), a_))
		{
			l.a(l.c, l.m_a + "BFUpdater.exe", text);
		}
		l.a(l.c, value, text);
		l.b = new DownloadProgressBar(l.c, "正在下载更新...", text, true);
		l.b.Closing += l.a;
		l.b.ShowDialog();
	}

		private static void a(object A_0, CancelEventArgs A_1)
	{
		if (l.b.TaskFileNum > 0 && l.b.TaskFileNum == l.b.DownloadedFileNum)
		{
			Process.Start(Environment.CurrentDirectory + "\\BFUpdater.exe");
			return;
		}
		string str = Environment.CurrentDirectory + "\\";
		foreach (string text in l.c)
		{
			string str2 = text.Substring(text.LastIndexOf("/") + 1);
			string path = str + str2;
			if (File.Exists(path))
			{
				File.Delete(path);
			}
		}
	}

		private static void a(List<string> A_0, string A_1, string A_2)
	{
		A_2 += A_1.Substring(A_1.LastIndexOf("/") + 1);
		if (File.Exists(A_2))
		{
			File.Delete(A_2);
		}
		A_0.Add(A_1);
	}

		private static Version a(string A_0)
	{
		string text = null;
		try
		{
			text = FileVersionInfo.GetVersionInfo(A_0).FileVersion;
		}
		catch
		{
		}
		return new Version((text == null) ? "0.0.0.0" : text);
	}

		private static bool a(Version A_0, Version A_1)
	{
		return A_0 < A_1;
	}

		public l()
	{
	}

		
	static l()
	{
	}

		private static string m_a = "https://raw.githubusercontent.com/pungin/Beanfun/" + (g.a("updateChannel", "Stable").Equals("Stable") ? "master" : "beta") + "/";

		private static DownloadProgressBar b;

		private static List<string> c = new List<string>();
}
