using System;
using System.Windows.Forms;
using Microsoft.Win32;

internal class A
{
		public bool a()
	{
		return this.m_a;
	}

		public void a(bool A_0)
	{
		this.m_a = A_0;
	}

		public string b()
	{
		return this.m_b;
	}

		public void a(string A_0)
	{
		this.m_b = A_0;
	}

		public RegistryKey c()
	{
		return this.m_c;
	}

		public void a(RegistryKey A_0)
	{
		this.m_c = A_0;
	}

		public string b(string A_0)
	{
		RegistryKey registryKey = this.m_c.OpenSubKey(this.m_b);
		if (registryKey == null)
		{
			return null;
		}
		string result;
		try
		{
			result = (string)registryKey.GetValue(A_0.ToUpper());
		}
		catch (Exception a_)
		{
			this.a(a_, "Reading registry " + A_0.ToUpper());
			result = null;
		}
		return result;
	}

		public bool a(string A_0, object A_1)
	{
		bool result;
		try
		{
			this.m_c.CreateSubKey(this.m_b).SetValue(A_0.ToUpper(), A_1);
			result = true;
		}
		catch (Exception a_)
		{
			this.a(a_, "Writing registry " + A_0.ToUpper());
			result = false;
		}
		return result;
	}

		public bool c(string A_0)
	{
		bool result;
		try
		{
			RegistryKey registryKey = this.m_c.CreateSubKey(this.m_b);
			if (registryKey == null)
			{
				result = true;
			}
			else
			{
				registryKey.DeleteValue(A_0);
				result = true;
			}
		}
		catch (Exception a_)
		{
			this.a(a_, "Deleting SubKey " + this.m_b);
			result = false;
		}
		return result;
	}

		public void d()
	{
		RegistryKey registryKey = this.m_c;
		if (registryKey.OpenSubKey(this.m_b) == null)
		{
			registryKey.CreateSubKey(this.m_b);
		}
	}

		public bool e()
	{
		bool result;
		try
		{
			RegistryKey registryKey = this.m_c;
			if (registryKey.OpenSubKey(this.m_b) != null)
			{
				registryKey.DeleteSubKeyTree(this.m_b);
			}
			result = true;
		}
		catch (Exception a_)
		{
			this.a(a_, "Deleting SubKey " + this.m_b);
			result = false;
		}
		return result;
	}

		public int f()
	{
		int result;
		try
		{
			RegistryKey registryKey = this.m_c.OpenSubKey(this.m_b);
			if (registryKey != null)
			{
				result = registryKey.SubKeyCount;
			}
			else
			{
				result = 0;
			}
		}
		catch (Exception a_)
		{
			this.a(a_, "Retriving subkeys of " + this.m_b);
			result = 0;
		}
		return result;
	}

		public int g()
	{
		int result;
		try
		{
			RegistryKey registryKey = this.m_c.OpenSubKey(this.m_b);
			if (registryKey != null)
			{
				result = registryKey.ValueCount;
			}
			else
			{
				result = 0;
			}
		}
		catch (Exception a_)
		{
			this.a(a_, "Retriving keys of " + this.m_b);
			result = 0;
		}
		return result;
	}

		private void a(Exception A_0, string A_1)
	{
		if (this.m_a)
		{
			MessageBox.Show(A_0.Message, A_1, MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	

		private bool m_a;

		private string m_b = "SOFTWARE\\" + Application.ProductName.ToUpper();

		private RegistryKey m_c = Registry.LocalMachine;
}
