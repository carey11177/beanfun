using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Beanfun;
using Newtonsoft.Json;

internal class e
{
		public static void a(string[] A_0)
	{
		try
		{
			if (A_0.Length == 0)
			{
				Console.WriteLine("No Parameter");
			}
			else
			{
				string[] array = Encoding.UTF8.GetString(Convert.FromBase64String(A_0[0].Substring(0, A_0[0].Length - 1).Replace("bfap://", ""))).Split(new char[]
				{
					'&'
				});
				Console.WriteLine("Encoded Parameter");
				Console.WriteLine("type:" + array[0] + ",token:" + array[1]);
				string text = string.Empty;
				foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
				{
					if (networkInterface.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
					{
						text = networkInterface.GetPhysicalAddress().ToString();
						break;
					}
				}
				Console.WriteLine("Get Computer MAC:" + text + ",DeviceName:" + Environment.MachineName);
				string text2 = array[0];
				if (!(text2 == "0"))
				{
					if (!(text2 == "1"))
					{
						if (!(text2 == "2"))
						{
							if (!(text2 == "3"))
							{
								if (!(text2 == "4"))
								{
									if (text2 == "5")
									{
										Console.WriteLine("Request Login");
										SKIV skiv = JsonConvert.DeserializeObject<SKIV>(e.a(APIUrl.GetSk, "=" + array[1], "2"));
										Console.WriteLine("Geting Key");
										if (skiv.ReturnValue.Equals("1"))
										{
											Console.WriteLine("Geted Key");
											Console.WriteLine(e.a(APIUrl.CreLl, string.Format("key={0}&data={1}", skiv.Skey, e.a(string.Concat(new string[]
											{
												"{bfAPPuid:\"",
												skiv.bfAPPuid,
												"\",APPuid:\"",
												skiv.APPuid,
												"\",LoginToken:\"",
												array[1],
												"\",DeviceName:\"",
												Environment.MachineName,
												" - 缤放(V",
												e.m_a,
												")\",MAC:\"",
												text,
												"\"}"
											}), skiv.Skey, skiv.IV)), "2"));
										}
									}
								}
								else
								{
									Console.WriteLine("Register Computer");
									SKIV skiv2 = JsonConvert.DeserializeObject<SKIV>(e.a(APIUrl.GetSk, "=" + array[1], "2"));
									Console.WriteLine("Geting Key");
									if (skiv2.ReturnValue.Equals("1"))
									{
										Console.WriteLine("Geted Key");
										Console.WriteLine("Post CheckRegistration");
										Response response = JsonConvert.DeserializeObject<Response>(e.a(APIUrl.GetRc, string.Format("key={0}&data={1}", skiv2.Skey, e.a(string.Concat(new string[]
										{
											"{bfAPPuid:\"",
											skiv2.bfAPPuid,
											"\",APPuid:\"",
											skiv2.APPuid,
											"\",DeviceName:\"",
											Environment.MachineName,
											" - 缤放(V",
											e.m_a,
											")\",MAC:\"",
											text,
											"\"}"
										}), skiv2.Skey, skiv2.IV)), "2"));
										if (response.Result == null)
										{
											if (!response.ReturnValue.Equals("1"))
											{
												Console.WriteLine("Registration Device");
												Console.WriteLine(e.a(APIUrl.CreRl, string.Format("key={0}&data={1}", skiv2.Skey, e.a("{" + string.Format("bfAPPuid:\"{0}\",APPuid:\"{1}\",DeviceName:\"{2} - 缤放(V{3})\",MAC:\"{4}\",ClientID:\"{5}\"", new object[]
												{
													skiv2.bfAPPuid,
													skiv2.APPuid,
													Environment.MachineName,
													e.m_a,
													text,
													skiv2.ClientID
												}) + "}", skiv2.Skey, skiv2.IV)), "2"));
											}
											else
											{
												Console.WriteLine("This device is registered");
											}
										}
										else
										{
											Console.WriteLine("Cannot connection to check registered");
										}
									}
								}
							}
							else
							{
								Console.WriteLine("Request Login");
								SKIV skiv3 = JsonConvert.DeserializeObject<SKIV>(e.a(APIUrl.GetSk, "=" + array[1], "1"));
								Console.WriteLine("Geting Key");
								if (skiv3.ReturnValue.Equals("1"))
								{
									Console.WriteLine("Geted Key");
									Console.WriteLine(e.a(APIUrl.CreLl, string.Format("key={0}&data={1}", skiv3.Skey, e.a(string.Concat(new string[]
									{
										"{bfAPPuid:\"",
										skiv3.bfAPPuid,
										"\",APPuid:\"",
										skiv3.APPuid,
										"\",LoginToken:\"",
										array[1],
										"\",DeviceName:\"",
										Environment.MachineName,
										" - 缤放(V",
										e.m_a,
										")\",MAC:\"",
										text,
										"\"}"
									}), skiv3.Skey, skiv3.IV)), "1"));
								}
							}
						}
						else
						{
							Console.WriteLine("Register Computer");
							SKIV skiv4 = JsonConvert.DeserializeObject<SKIV>(e.a(APIUrl.GetSk, "=" + array[1], "1"));
							Console.WriteLine("Geting Key");
							if (skiv4.ReturnValue.Equals("1"))
							{
								Console.WriteLine("Geted Key");
								Console.WriteLine("Post CheckRegistration");
								Response response2 = JsonConvert.DeserializeObject<Response>(e.a(APIUrl.GetRc, string.Format("key={0}&data={1}", skiv4.Skey, e.a(string.Concat(new string[]
								{
									"{bfAPPuid:\"",
									skiv4.bfAPPuid,
									"\",APPuid:\"",
									skiv4.APPuid,
									"\",DeviceName:\"",
									Environment.MachineName,
									" - 缤放(V",
									e.m_a,
									")\",MAC:\"",
									text,
									"\"}"
								}), skiv4.Skey, skiv4.IV)), "1"));
								if (response2.Result == null)
								{
									if (!response2.ReturnValue.Equals("1"))
									{
										Console.WriteLine("Registration Device");
										Console.WriteLine(e.a(APIUrl.CreRl, string.Format("key={0}&data={1}", skiv4.Skey, e.a("{" + string.Format("bfAPPuid:\"{0}\",APPuid:\"{1}\",DeviceName:\"{2} - 缤放(V{3})\",MAC:\"{4}\",ClientID:\"{5}\"", new object[]
										{
											skiv4.bfAPPuid,
											skiv4.APPuid,
											Environment.MachineName,
											e.m_a,
											text,
											skiv4.ClientID
										}) + "}", skiv4.Skey, skiv4.IV)), "1"));
									}
									else
									{
										Console.WriteLine("This device is registered");
									}
								}
								else
								{
									Console.WriteLine("Cannot connection to check registered");
								}
							}
						}
					}
					else
					{
						Console.WriteLine("Request Login");
						SKIV skiv5 = JsonConvert.DeserializeObject<SKIV>(e.a(APIUrl.GetSk, "=" + array[1], "0"));
						Console.WriteLine("Geting Key");
						if (skiv5.ReturnValue.Equals("1"))
						{
							Console.WriteLine("Geted Key");
							Console.WriteLine(e.a(APIUrl.CreLl, string.Format("key={0}&data={1}", skiv5.Skey, e.a(string.Concat(new string[]
							{
								"{bfAPPuid:\"",
								skiv5.bfAPPuid,
								"\",APPuid:\"",
								skiv5.APPuid,
								"\",LoginToken:\"",
								array[1],
								"\",DeviceName:\"",
								Environment.MachineName,
								" - 缤放(V",
								e.m_a,
								")\",MAC:\"",
								text,
								"\"}"
							}), skiv5.Skey, skiv5.IV)), "0"));
						}
					}
				}
				else
				{
					Console.WriteLine("Register Computer");
					SKIV skiv6 = JsonConvert.DeserializeObject<SKIV>(e.a(APIUrl.GetSk, "=" + array[1], "0"));
					Console.WriteLine("Geting Key");
					if (skiv6.ReturnValue.Equals("1"))
					{
						Console.WriteLine("Geted Key");
						Console.WriteLine("Post CheckRegistration");
						Response response3 = JsonConvert.DeserializeObject<Response>(e.a(APIUrl.GetRc, string.Format("key={0}&data={1}", skiv6.Skey, e.a(string.Concat(new string[]
						{
							"{bfAPPuid:\"",
							skiv6.bfAPPuid,
							"\",APPuid:\"",
							skiv6.APPuid,
							"\",DeviceName:\"",
							Environment.MachineName,
							" - 缤放(V",
							e.m_a,
							")\",MAC:\"",
							text,
							"\"}"
						}), skiv6.Skey, skiv6.IV)), "0"));
						if (response3.Result == null)
						{
							if (!response3.ReturnValue.Equals("1"))
							{
								Console.WriteLine("Registration Device");
								Console.WriteLine(e.a(APIUrl.CreRl, string.Format("key={0}&data={1}", skiv6.Skey, e.a("{" + string.Format("bfAPPuid:\"{0}\",APPuid:\"{1}\",DeviceName:\"{2} - 缤放(V{3})\",MAC:\"{4}\",ClientID:\"{5}\"", new object[]
								{
									skiv6.bfAPPuid,
									skiv6.APPuid,
									Environment.MachineName,
									e.m_a,
									text,
									skiv6.ClientID
								}) + "}", skiv6.Skey, skiv6.IV)), "0"));
							}
							else
							{
								Console.WriteLine("This device is registered");
							}
						}
						else
						{
							Console.WriteLine("Cannot connection to check registered");
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine(ex.Message);
		}
	}

		public static string a(string A_0, string A_1, string A_2)
	{
		if (string.IsNullOrEmpty(A_0) || string.IsNullOrEmpty(A_1) || string.IsNullOrEmpty(A_2))
		{
			return string.Empty;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(A_0);
		return HttpUtility.UrlEncode(Convert.ToBase64String(new RijndaelManaged
		{
			Key = Encoding.UTF8.GetBytes(A_1),
			IV = Encoding.UTF8.GetBytes(A_2),
			Mode = CipherMode.CBC,
			Padding = PaddingMode.PKCS7
		}.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length)));
	}

		public static string a(APIUrl A_0, string A_1, string A_2)
	{
		string text = string.Empty;
		if (!(A_2 == "0"))
		{
			if (!(A_2 == "1"))
			{
				if (A_2 == "2")
				{
					text = "http://localhost:18402/";
				}
			}
			else
			{
				text = "https://alpha-bfapp.beanfun.com/";
			}
		}
		else
		{
			text = "https://tw.bfapp.beanfun.com/";
		}
		switch (A_0)
		{
		case APIUrl.GetSk:
			text += "api/check/Archaeopteryx0010";
			break;
		case APIUrl.GetRc:
			text += "api/check/Archaeopteryx0006";
			break;
		case APIUrl.CreRl:
			text += "api/check/Archaeopteryx0007";
			break;
		case APIUrl.CreLl:
			text += "api/check/Archaeopteryx0009";
			break;
		}
		string result;
		try
		{
			HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(text);
			httpWebRequest.Method = "POST";
			httpWebRequest.ContentType = "application/x-www-form-urlencoded";
			byte[] bytes = Encoding.ASCII.GetBytes(A_1);
			httpWebRequest.ContentLength = (long)bytes.Length;
			using (Stream requestStream = httpWebRequest.GetRequestStream())
			{
				requestStream.Write(bytes, 0, bytes.Length);
				requestStream.Close();
			}
			using (HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse())
			{
				result = new StreamReader(httpWebResponse.GetResponseStream(), Encoding.UTF8).ReadToEnd();
			}
		}
		catch (Exception ex)
		{
			throw ex;
		}
		return result;
	}

		public e()
	{
	}

	static e()
	{
	}

		private static string m_a = Assembly.GetExecutingAssembly().GetName().Version.ToString();
}
