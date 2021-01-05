using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;

namespace Beanfun
{
		public class AccountManager
	{
				public bool init()
		{
			return this.c();
		}

				private void d()
		{
			if (this.m_a == null)
			{
				this.m_a = new AccountRecords();
			}
			if (this.m_a.regionList == null)
			{
				this.m_a.regionList = new List<string>();
			}
			if (this.m_a.accountList == null)
			{
				this.m_a.accountList = new List<string>();
			}
			if (this.m_a.passwdList == null)
			{
				this.m_a.passwdList = new List<string>();
			}
			if (this.m_a.verifyList == null)
			{
				this.m_a.verifyList = new List<string>();
			}
			if (this.m_a.methodList == null)
			{
				this.m_a.methodList = new List<int>();
			}
			if (this.m_a.autoLoginList == null)
			{
				this.m_a.autoLoginList = new List<bool>();
			}
		}

				private bool c()
		{
			string text = this.a();
			if (text != null)
			{
				using (Stream stream = new MemoryStream(Convert.FromBase64String(text)))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					this.m_a = (AccountRecords)binaryFormatter.Deserialize(stream);
				}
			}
			this.d();
			return true;
		}

				private bool b()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, this.m_a);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, (int)memoryStream.Length);
				this.a(Convert.ToBase64String(array));
			}
			return true;
		}

				private string a()
		{
			string result;
			try
			{
				if (File.Exists(this.m_b))
				{
					try
					{
						byte[] encryptedData = File.ReadAllBytes(this.m_b);
						global::A A= new global::A();
						A.a(Registry.CurrentUser);
						A.a("Software\\Beanfun");
						string s = A.b("Entropy");
						byte[] bytes = ProtectedData.Unprotect(encryptedData, Encoding.UTF8.GetBytes(s), DataProtectionScope.CurrentUser);
						return Encoding.UTF8.GetString(bytes);
					}
					catch
					{
						File.Delete(this.m_b);
					}
				}
				result = null;
			}
			catch
			{
				result = null;
			}
			return result;
		}

				private void a(string A_0)
		{
			using (BinaryWriter binaryWriter = new BinaryWriter(File.Open(this.m_b, FileMode.Create)))
			{
				AccountManager.aB a = new AccountManager.aB();
				string element = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
				a.r_a = new Random();
				string a_ = new string(Enumerable.Repeat<string>(element, 8).Select(new Func<string, char>(a.a)).ToArray<char>());
				global::A a2 = new global::A();
				a2.a(Registry.CurrentUser);
				a2.a("Software\\Beanfun");
				a2.a("Entropy", a_);
				binaryWriter.Write(this.a(A_0, a_));
			}
		}

				private byte[] a(string A_0, string A_1)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(A_0);
			byte[] bytes2 = Encoding.UTF8.GetBytes(A_1);
			return ProtectedData.Protect(bytes, bytes2, DataProtectionScope.CurrentUser);
		}

				public bool addAccount(string region, string account, string password, string verify, int method, bool autoLogin)
		{
			return this.addAccount(-1, region, account, password, verify, method, autoLogin);
		}

				public bool addAccount(int index, string region, string account, string password, string verify, int method, bool autoLogin)
		{
			bool flag = false;
			List<int> list = new List<int>();
			for (int i = 0; i < this.m_a.accountList.Count; i++)
			{
				if (!(region != this.m_a.regionList[i]))
				{
					if (account == this.m_a.accountList[i])
					{
						if (index <= -1 || list.Count == index)
						{
							this.m_a.passwdList[i] = password;
							this.m_a.verifyList[i] = verify;
							this.m_a.methodList[i] = method;
							this.m_a.autoLoginList[i] = autoLogin;
							flag = true;
							break;
						}
						this.removeAccount(region, account);
						i--;
					}
					else
					{
						list.Add(i);
					}
				}
			}
			if (!flag)
			{
				if (index < 0 || list.Count <= index)
				{
					this.m_a.regionList.Add(region);
					this.m_a.accountList.Add(account);
					this.m_a.passwdList.Add(password);
					this.m_a.verifyList.Add(verify);
					this.m_a.methodList.Add(method);
					this.m_a.autoLoginList.Add(autoLogin);
				}
				else
				{
					index = list[index];
					this.m_a.regionList.Insert(index, region);
					this.m_a.accountList.Insert(index, account);
					this.m_a.passwdList.Insert(index, password);
					this.m_a.verifyList.Insert(index, verify);
					this.m_a.methodList.Insert(index, method);
					this.m_a.autoLoginList.Insert(index, autoLogin);
				}
			}
			this.b();
			return true;
		}

				public string getPasswordByAccount(string region, string account)
		{
			for (int i = 0; i < this.m_a.accountList.Count; i++)
			{
				if (account == this.m_a.accountList[i] && region == this.m_a.regionList[i])
				{
					return this.m_a.passwdList[i];
				}
			}
			return null;
		}

				public string getVerifyByAccount(string region, string account)
		{
			for (int i = 0; i < this.m_a.accountList.Count; i++)
			{
				if (account == this.m_a.accountList[i] && region == this.m_a.regionList[i])
				{
					return this.m_a.verifyList[i];
				}
			}
			return null;
		}

				public int getMethodByAccount(string region, string account)
		{
			for (int i = 0; i < this.m_a.accountList.Count; i++)
			{
				if (account == this.m_a.accountList[i] && region == this.m_a.regionList[i])
				{
					return this.m_a.methodList[i];
				}
			}
			return -1;
		}

				public bool getAutoLoginByAccount(string region, string account)
		{
			for (int i = 0; i < this.m_a.accountList.Count; i++)
			{
				if (account == this.m_a.accountList[i] && region == this.m_a.regionList[i])
				{
					return this.m_a.autoLoginList[i];
				}
			}
			return false;
		}

				public bool removeAccount(string region, string account)
		{
			for (int i = 0; i < this.m_a.accountList.Count; i++)
			{
				if (account == this.m_a.accountList[i] && region == this.m_a.regionList[i])
				{
					this.m_a.regionList.RemoveAt(i);
					this.m_a.accountList.RemoveAt(i);
					this.m_a.passwdList.RemoveAt(i);
					this.m_a.verifyList.RemoveAt(i);
					this.m_a.methodList.RemoveAt(i);
					this.m_a.autoLoginList.RemoveAt(i);
					this.b();
					return true;
				}
			}
			return false;
		}

				public string[] getAccountList()
		{
			return this.m_a.accountList.ToArray();
		}

				public string[] getAccountList(string region)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < this.m_a.accountList.Count; i++)
			{
				if (region == this.m_a.regionList[i])
				{
					list.Add(this.m_a.accountList[i]);
				}
			}
			return list.ToArray();
		}

				public bool importRecord(string raw)
		{
			try
			{
				using (Stream stream = new MemoryStream(Convert.FromBase64String(raw)))
				{
					BinaryFormatter binaryFormatter = new BinaryFormatter();
					this.m_a = (AccountRecords)binaryFormatter.Deserialize(stream);
				}
				this.d();
				this.b();
			}
			catch
			{
				return false;
			}
			return true;
		}

				public string exportRecord()
		{
			string result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				new BinaryFormatter().Serialize(memoryStream, this.m_a);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				byte[] array = new byte[memoryStream.Length];
				memoryStream.Read(array, 0, (int)memoryStream.Length);
				result = Convert.ToBase64String(array);
			}
			return result;
		}

				public AccountManager()
		{
		}

				private AccountRecords m_a;

				private string m_b = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\Beanfun\\Users.dat";

				[CompilerGenerated]
		private sealed class aB
		{
												
						internal char a(string A_0)
			{
				return A_0[this.r_a.Next(A_0.Length)];
			}

						public Random r_a;
		}
	}
}
