using System;
using System.Security.Cryptography;
using System.Text;

internal class f
{
		public static string b(string A_0, string A_1)
	{
		string result;
		try
		{
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			descryptoServiceProvider.Mode = CipherMode.ECB;
			descryptoServiceProvider.Padding = PaddingMode.None;
			descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(A_1);
			byte[] bytes = Encoding.ASCII.GetBytes(A_0);
			result = BitConverter.ToString(descryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length)).Replace("-", "");
		}
		catch (Exception ex)
		{
			Console.WriteLine("EncryptDESError:" + ex.Message + "\n" + ex.StackTrace);
			result = null;
		}
		return result;
	}

		public static string a(string A_0, string A_1)
	{
		string result;
		try
		{
			DESCryptoServiceProvider descryptoServiceProvider = new DESCryptoServiceProvider();
			descryptoServiceProvider.Mode = CipherMode.ECB;
			descryptoServiceProvider.Padding = PaddingMode.None;
			descryptoServiceProvider.Key = Encoding.ASCII.GetBytes(A_1);
			byte[] array = new byte[A_0.Length / 2];
			for (int i = 0; i < A_0.Length; i += 2)
			{
				array[i / 2] = Convert.ToByte(A_0.Substring(i, 2), 16);
			}
			ICryptoTransform cryptoTransform = descryptoServiceProvider.CreateDecryptor();
			result = Encoding.ASCII.GetString(cryptoTransform.TransformFinalBlock(array, 0, array.Length));
		}
		catch (Exception ex)
		{
			Console.WriteLine("DecryptDESError:" + ex.Message + "\n" + ex.StackTrace);
			result = null;
		}
		return result;
	}

		public f()
	{
	}
}
