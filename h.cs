using System.Collections;
using System.Linq;
using FSFISCATLLib;
using FSP11CRYPTATLLib;

internal class h
{
	public string stringA/*a*/;

	public string stringB/*b*/;

	public string stringC/*c*/;

	private FSFISCClass FSFISCClassD/*d*/;

	private KENP11CryptClass kENP11CryptClassE/*e*/;

	public h()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Expected O, but got Unknown
		FSFISCClassD = new FSFISCClass();
		this.stringA = null;
		this.stringB = null;
		this.stringC = null;
	}

	public string a()
	{
		string result = "";
		object obj;
		try
		{
			obj = FSFISCClassD.FSFISC_GetReaderNames(0);
		}
		catch
		{
			return null;
		}
		if (obj == null)
		{
			return null;
		}
		string[] array = (from object A_0 in (IEnumerable)obj
						  select A_0.ToString()).ToArray();
		foreach (string text in array)
		{
			int num = FSFISCClassD.FSFISC_GetCardType2(text);
			if (FSFISCClassD.FSFISC_GetErrorCode() != 0)
			{
				num = -1;
				continue;
			}
			switch (num)
			{
				case 0:
					result = text;
					this.stringA = "F";
					break;
				case 1:
					result = text;
					this.stringA = "G";
					break;
			}
		}
		if (result != "")
		{
			this.stringB = result;
			return result;
		}
		return null;
	}

	public string c(string A_0)
	{
		if (A_0 == "" || A_0 == null)
		{
			return null;
		}
		string result = FSFISCClassD.FSFISC_GetPublicCN(A_0, 0);
		if (FSFISCClassD.FSFISC_GetErrorCode() != 0)
		{
			return null;
		}
		return result;
	}

	public string a(string A_0, string A_1)
	{
		if (A_0 == "" || A_0 == null)
		{
			return null;
		}
		string result = FSFISCClassD.FSFISC_GetOPInfo(A_0, A_1, 0);
		if (FSFISCClassD.FSFISC_GetErrorCode() != 0)
		{
			return null;
		}
		return result;
	}

	public string a(string A_0, string A_1, string A_2)
	{
		if (A_0 == "" || A_0 == null)
		{
			return null;
		}
		string result = FSFISCClassD.FSFISC_GetTAC(A_0, A_1, A_2, 0, 0);
		if (FSFISCClassD.FSFISC_GetErrorCode() != 0)
		{
			return null;
		}
		return result;
	}

	public string b(string A_0, string A_1)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		kENP11CryptClassE = new KENP11CryptClass();
		if (kENP11CryptClassE.FSXP11Init("gclib.dll") != 0)
		{
			int errorCode = kENP11CryptClassE.GetErrorCode();
			switch (errorCode)
			{
				case 9110: return "憑證卡讀取失敗( 晶片卡驅動程式未安裝 )";
				case 9056: return "憑證卡讀取失敗( 請插入晶片卡 )";
				default: return "憑證卡讀取失敗(" + errorCode + ")";
			};
		}
		if (kENP11CryptClassE.FSXP11SessionOpen() != 0)
		{
			kENP11CryptClassE.FSXP11Final();
			return "憑證卡開啟失敗(" + kENP11CryptClassE.GetErrorCode() + ")";
		}
		string text = kENP11CryptClassE.FSP11_GetSerialNumber();
		int errorCode2 = kENP11CryptClassE.GetErrorCode();
		if (errorCode2 != 0)
		{
			kENP11CryptClassE.FSXP11SessionClose();
			kENP11CryptClassE.FSXP11Final();
			return "read card serial number fail(" + errorCode2 + ")";
		}
		text = (this.stringC = text.Substring(0, 16));
		if (kENP11CryptClassE.FSXP11Login(A_0) != 0)
		{
			int errorCode3 = kENP11CryptClassE.GetErrorCode();
			int num = kENP11CryptClassE.FSP11_GetRetryCounter(131072);
			kENP11CryptClassE.FSXP11SessionClose();
			kENP11CryptClassE.FSXP11Final();
			switch (errorCode3)

			{
				case 9039: return "密碼驗證失敗:(還有" + num + "次機會)";
				case 9043: return "密碼輸入錯誤已達八次，請用購買認證序號解鎖!";
				default: return "憑證卡登入失敗" + errorCode3 + ")";
			};
		}
		return b(A_1);
	}

	private string b(string A_0)
	{
		int num = kENP11CryptClassE.FSXP11GetObjectList(0);
		if (kENP11CryptClassE.GetErrorCode() != 0)
		{
			kENP11CryptClassE.FSXP11Logout();
			kENP11CryptClassE.FSXP11SessionClose();
			kENP11CryptClassE.FSXP11Final();
			return "Error on funtcion FSXP11GetObjectList, error code=" + kENP11CryptClassE.GetErrorCode();
		}
		string text = "";
		for (int i = 0; i < num; i++)
		{
			if (kENP11CryptClassE.FSXP11GetObjectListObjectType(i) == 17)
			{
				text = kENP11CryptClassE.FSXP11GetObjectListLabel(i);
				if (text == "PlaySAFE")
				{
					break;
				}
				text = "";
			}
			if (kENP11CryptClassE.GetErrorCode() != 0)
			{
				kENP11CryptClassE.FSXP11Logout();
				kENP11CryptClassE.FSXP11SessionClose();
				kENP11CryptClassE.FSXP11Final();
				return "凭证存取失败,请重新启动后再尝试(" + kENP11CryptClassE.GetErrorCode() + ")";
			}
		}
		if (text != "PlaySAFE")
		{
			kENP11CryptClassE.FSXP11Logout();
			kENP11CryptClassE.FSXP11SessionClose();
			kENP11CryptClassE.FSXP11Final();
			return "找不到指定物件 Label[PlaySAFE]";
		}
		return a(A_0);
	}

	private string a(string A_0)
	{
		string result = kENP11CryptClassE.FSP11Sign("PlaySAFE", 0, A_0, 0);
		int errorCode = kENP11CryptClassE.GetErrorCode();
		kENP11CryptClassE.FSXP11Logout();
		kENP11CryptClassE.FSXP11SessionClose();
		kENP11CryptClassE.FSXP11Final();
		if (errorCode != 0)
		{
			return "签章失败(" + errorCode + ")";
		}
		return result;
	}
}
