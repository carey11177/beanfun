using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Navigation;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Win32;

namespace Beanfun
{
		public partial class WebBrowser : Window, IComponentConnector
	{
				public WebBrowser(string uri)
		{
			this.InitializeComponent();
			this.ChangeUserAgent();
			new WebBrowserHelper(this.m_c).BeforeNewWindow += this.a;
			if (App.MainWnd.bfClient != null)
			{
				foreach (object obj in App.MainWnd.bfClient.GetCookies())
				{
					Cookie cookie = (Cookie)obj;
					WebBrowser.InternetSetCookie("https://beanfun.com/", cookie.Name, cookie.Value);
				}
			}
			if (App.LoginRegion == "HK")
			{
				this.a();
			}
			this.imagec.Navigate(uri);
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				[DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool InternetSetCookie(string lpszUrlName, string lbszCookieName, string lpszCookieData);

				[DllImport("urlmon.dll", CharSet = CharSet.Ansi)]
		private static extern int UrlMkSetSessionOption(int A_0, string A_1, int A_2, int A_3);

				public void ChangeUserAgent()
		{
			new List<string>();
			string text = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko";
			WebBrowser.UrlMkSetSessionOption(268435457, text, text.Length, 0);
		}

				private void a(object A_0, NavigationEventArgs A_1)
		{
			if (WebBrowser.A.a == null)
			{
				WebBrowser.A.a = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "Document", typeof(WebBrowser), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			object arg = WebBrowser.A.a.Target(WebBrowser.A.a, A_0);
			TextBox textBox = this.imageb;
			if (WebBrowser.A.c == null)
			{
				WebBrowser.A.c = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(WebBrowser)));
			}
			Func<CallSite, object, string> target = WebBrowser.A.c.Target;
			CallSite arg2 = WebBrowser.A.c;
			if (WebBrowser.A.b == null)
			{
				WebBrowser.A.b = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "url", typeof(WebBrowser), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			textBox.Text = target(arg2, WebBrowser.A.b.Target(WebBrowser.A.b, arg));
			if (WebBrowser.A.e == null)
			{
				WebBrowser.A.e = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(WebBrowser)));
			}
			Func<CallSite, object, string> target2 = WebBrowser.A.e.Target;
			CallSite e = WebBrowser.A.e;
			if (WebBrowser.A.d == null)
			{
				WebBrowser.A.d = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "title", typeof(WebBrowser), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			base.Title = target2(e, WebBrowser.A.d.Target(WebBrowser.A.d, arg));
		}

				private void a(object A_0, WebBrowserExtendedNavigatingEventArgs A_1)
		{
			A_1.Cancel = true;
			if (WebBrowser.b.a == null)
			{
				WebBrowser.b.a = CallSite<Action<CallSite, object, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Navigate", null, typeof(WebBrowser), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			WebBrowser.b.a.Target(WebBrowser.b.a, A_0, A_1.Url);
		}

				private void a()
		{
			global::A a = new global::A();
			a.a(Registry.CurrentUser);
			a.a("Software\\Microsoft\\Windows\\CurrentVersion\\Ext\\Stats\\{8AFB38D0-67A4-49D3-8822-401755FC6573}\\iexplore\\AllowedDomains\\beanfun.com");
			a.d();
			a.a("Software\\Microsoft\\Windows\\CurrentVersion\\Ext\\Stats\\{8AFB38D0-67A4-49D3-8822-401755FC6573}\\iexplore");
			a.c("Blocked");
			a.c("Flags");
			a.a("Software\\Microsoft\\Windows\\CurrentVersion\\Ext\\Settings\\{8AFB38D0-67A4-49D3-8822-401755FC6573}");
			a.e();
			a.a("Software\\Policies\\Microsoft\\Internet Explorer\\BrowserEmulation\\PolicyList");
			a.a("beanfun.com", "beanfun.com");
		}

			
				private const int m_a = 268435457;

				internal TextBox m_b;

				internal WebBrowser m_c;

				private bool m_d;

				[CompilerGenerated]
		private static class A
		{
						public static CallSite<Func<CallSite, object, object>> a;

						public static CallSite<Func<CallSite, object, object>> b;

						public static CallSite<Func<CallSite, object, string>> c;

						public static CallSite<Func<CallSite, object, object>> d;

						public static CallSite<Func<CallSite, object, string>> e;
		}

				[CompilerGenerated]
		private static class b
		{
						public static CallSite<Action<CallSite, object, string>> a;
		}
	}
}
