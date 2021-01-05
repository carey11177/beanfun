using System;
using System.Collections;
using System.Threading;
using FluorineFx;
using FluorineFx.AMF3;
using FluorineFx.Messaging.Api.Service;
using FluorineFx.Net;

namespace Beanfun
{
		public class GameServerAccountListApp
	{
				public void Connect()
		{
			this.n_a = new NetConnection();
			this.n_a.ObjectEncoding = (ObjectEncoding)3;
			this.n_a.NetStatus += new NetStatusHandler(this.a);
			this.n_a.CookieContainer.Add(new Uri("http://hk.beanfun.com/"), App.MainWnd.bfClient.GetCookies());
			this.n_a.Connect("http://hk.beanfun.com/Gateway.aspx", new object[0]);
		}

				private void a(object A_0, NetStatusEventArgs A_1)
		{
			string text = A_1.Info["level"] as string;
			if (text == "error")
			{
				Console.WriteLine("Error: " + A_1.Info["code"]);
			}
			if (text == "status")
			{
				Console.WriteLine("Status: " + A_1.Info["code"]);
			}
		}

				public IList GetServiceAccounts(string service_code, string service_region)
		{
			if (this.n_a == null || !this.n_a.Connected)
			{
				this.Connect();
			}
			GameServerAccountListApp.A a = new GameServerAccountListApp.A(service_code, service_region);
			this.n_a.Call("BeanFunBlock.GameZone.GetServiceAccounts", a, new object[]
			{
				service_code,
				service_region
			});
			int num = 0;
			while (num < 60 && a.m_c == null)
			{
				Thread.Sleep(1000);
				num++;
			}
			return a.m_c;
		}

				public string GetServiceContract(string service_code, string service_region)
		{
			if (this.n_a == null || !this.n_a.Connected)
			{
				this.Connect();
			}
			GameServerAccountListApp.b b = new GameServerAccountListApp.b();
			this.n_a.Call("BeanFunBlock.GameZone.GetServiceContract", b, new object[]
			{
				service_code,
				service_region
			});
			int num = 0;
			while (num < 60 && b.a == null)
			{
				Thread.Sleep(1000);
				num++;
			}
			return b.a;
		}

				public ASObject AddServiceAccount(string parent_service_code, string parent_service_region, string service_code, string service_region, string service_account_id, string service_account_display_name)
		{
			if (this.n_a == null || !this.n_a.Connected)
			{
				this.Connect();
			}
			GameServerAccountListApp.c c = new GameServerAccountListApp.c();
			this.n_a.Call("BeanFunBlock.GameZone.AddServiceAccount", c, new object[]
			{
				parent_service_code,
				parent_service_region,
				service_code,
				service_region,
				service_account_id,
				service_account_display_name,
				""
			});
			int num = 0;
			while (num < 60 && c.a == null)
			{
				Thread.Sleep(1000);
				num++;
			}
			return c.a;
		}

				public string ChangeServiceAccountDisplayName(string gameCode, string service_account_id, string service_account_display_name)
		{
			if (this.n_a == null || !this.n_a.Connected)
			{
				this.Connect();
			}
			GameServerAccountListApp.d d = new GameServerAccountListApp.d();
			this.n_a.Call("BeanFunBlock.GameZone.ChangeServiceAccountDisplayName", d, new object[]
			{
				gameCode,
				service_account_id,
				service_account_display_name
			});
			int num = 0;
			while (num < 60 && d.a == null)
			{
				Thread.Sleep(1000);
				num++;
			}
			return d.a;
		}

				public void UpdateServiceAccount(string service_code, string service_region, BeanfunClient.ServiceAccount account)
		{
			if (this.n_a == null || !this.n_a.Connected)
			{
				this.Connect();
			}
			this.n_a.Call("BeanFunBlock.GameZone.UpdateServiceAccount", new GameServerAccountListApp.e(), new object[]
			{
				service_code
			});
		}

				public GameServerAccountListApp()
		{
		}

				private NetConnection n_a;

				private class A : IPendingServiceCallback
		{
						public A(string A_0, string A_1)
			{
				this.m_a = A_0;
				this.m_b = A_1;
			}

						public void ResultReceived(IPendingServiceCall call)
			{
				ArrayCollection arrayCollection = (ArrayCollection)call.Result;
				this.m_c = arrayCollection.List;
			}

						private string m_a;

						private string m_b;

						public IList m_c;
		}

				private class b : IPendingServiceCallback
		{
						public void ResultReceived(IPendingServiceCall call)
			{
				this.a = (string)call.Result;
			}

						public b()
			{
			}

						public string a;
		}

				private class c : IPendingServiceCallback
		{
						public void ResultReceived(IPendingServiceCall call)
			{
				this.a = (ASObject)call.Result;
			}

						public c()
			{
			}

						public ASObject a;
		}

				private class d : IPendingServiceCallback
		{
						public void ResultReceived(IPendingServiceCall call)
			{
				this.a = (string)call.Result;
			}

						public d()
			{
			}

						public string a;
		}

				private class e : IPendingServiceCallback
		{
						public void ResultReceived(IPendingServiceCall call)
			{
				object result = call.Result;
			}

						public e()
			{
			}
		}
	}
}
