using System;
using System.Configuration;
using System.IO;

internal class g
{
		public static void b(string A_0, string A_1)
	{
		try
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
			{
				ExeConfigFilename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Beanfun\\Config.xml"
			}, ConfigurationUserLevel.None);
			if (configuration.AppSettings.Settings[A_0] == null)
			{
				if (A_1 != null)
				{
					configuration.AppSettings.Settings.Add(A_0, A_1);
				}
			}
			else if (A_1 == null)
			{
				configuration.AppSettings.Settings.Remove(A_0);
			}
			else
			{
				configuration.AppSettings.Settings[A_0].Value = A_1;
			}
			configuration.Save(ConfigurationSaveMode.Modified);
			ConfigurationManager.RefreshSection("appSettings");
		}
		catch
		{
			try
			{
				foreach (FileSystemInfo fileSystemInfo in new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Beanfun").GetFileSystemInfos("Config.xml"))
				{
					if (fileSystemInfo is DirectoryInfo)
					{
						new DirectoryInfo(fileSystemInfo.FullName).Delete(true);
					}
					else
					{
						File.Delete(fileSystemInfo.FullName);
					}
				}
				g.b(A_0, A_1);
			}
			catch
			{
			}
		}
	}

		public static string a(string A_0)
	{
		return g.a(A_0, string.Empty);
	}

		public static string a(string A_0, string A_1)
	{
		string result;
		try
		{
			Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(new ExeConfigurationFileMap
			{
				ExeConfigFilename = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Beanfun\\Config.xml"
			}, ConfigurationUserLevel.None);
			result = ((configuration.AppSettings.Settings[A_0] == null) ? A_1 : configuration.AppSettings.Settings[A_0].Value);
		}
		catch
		{
			result = A_1;
		}
		return result;
	}

		public g()
	{
	}
}
