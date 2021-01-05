using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Forms;

namespace Beanfun
{
		public class WebBrowserHelper
	{
			
		public event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNavigate
		{
			[CompilerGenerated]
			add
			{
				EventHandler<WebBrowserExtendedNavigatingEventArgs> eventHandler = this.m_c;
				EventHandler<WebBrowserExtendedNavigatingEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<WebBrowserExtendedNavigatingEventArgs> value2 = (EventHandler<WebBrowserExtendedNavigatingEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<WebBrowserExtendedNavigatingEventArgs>>(ref this.m_c, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<WebBrowserExtendedNavigatingEventArgs> eventHandler = this.m_c;
				EventHandler<WebBrowserExtendedNavigatingEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<WebBrowserExtendedNavigatingEventArgs> value2 = (EventHandler<WebBrowserExtendedNavigatingEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<WebBrowserExtendedNavigatingEventArgs>>(ref this.m_c, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

				
		public event EventHandler<WebBrowserExtendedNavigatingEventArgs> BeforeNewWindow
		{
			[CompilerGenerated]
			add
			{
				EventHandler<WebBrowserExtendedNavigatingEventArgs> eventHandler = this.m_d;
				EventHandler<WebBrowserExtendedNavigatingEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<WebBrowserExtendedNavigatingEventArgs> value2 = (EventHandler<WebBrowserExtendedNavigatingEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<WebBrowserExtendedNavigatingEventArgs>>(ref this.m_d, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<WebBrowserExtendedNavigatingEventArgs> eventHandler = this.m_d;
				EventHandler<WebBrowserExtendedNavigatingEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<WebBrowserExtendedNavigatingEventArgs> value2 = (EventHandler<WebBrowserExtendedNavigatingEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<WebBrowserExtendedNavigatingEventArgs>>(ref this.m_d, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

				public WebBrowserHelper(WebBrowser webBrowser)
		{
			//if (webBrowser == null)
			//{
			//	throw new ArgumentNullException("webBrowser");
			//}
			//this.m_a = webBrowser;
			//this.m_a.Dispatcher.BeginInvoke(new Action(this.c), DispatcherPriority.Loaded, new object[0]);
			//this.m_a.Navigated += this.b;
			//if (WebBrowserHelper.a())
			//{
			//	this.m_a.Navigated += WebBrowserHelper.a;
			//}
		}

				private void b(object A_0, NavigationEventArgs A_1)
		{
			WebBrowserHelper.SetSilent(this.m_a, true);
		}

				public void Disconnect()
		{
			if (this.m_b != null)
			{
				this.m_b.ReflectInvokeMethod("Disconnect", new Type[0], null);
				this.m_b = null;
			}
		}

				private void c()
		{
			object obj = this.m_a.ReflectGetProperty("AxIWebBrowser2");
			WebBrowserHelper.B b = new WebBrowserHelper.B(this);
			Type type = typeof(WebBrowser).Assembly.GetType("MS.Internal.Controls.ConnectionPointCookie");
			this.m_b = Activator.CreateInstance(type, ReflectionService.BindingFlags, null, new object[]
			{
				obj,
				b,
				typeof(DWebBrowserEvents2)
			}, CultureInfo.CurrentUICulture);
		}

				private void a(string A_0, string A_1, out bool A_2)
		{
			WebBrowserExtendedNavigatingEventArgs webBrowserExtendedNavigatingEventArgs = new WebBrowserExtendedNavigatingEventArgs(A_0, A_1);
			if (this.m_c != null)
			{
				this.m_c(this.m_a, webBrowserExtendedNavigatingEventArgs);
			}
			A_2 = webBrowserExtendedNavigatingEventArgs.Cancel;
		}

				private void a(string A_0, out bool A_1)
		{
			WebBrowserExtendedNavigatingEventArgs webBrowserExtendedNavigatingEventArgs = new WebBrowserExtendedNavigatingEventArgs(A_0, null);
			if (this.m_d != null)
			{
				this.m_d(this.m_a, webBrowserExtendedNavigatingEventArgs);
			}
			A_1 = webBrowserExtendedNavigatingEventArgs.Cancel;
		}

				public static void SetSilent(WebBrowser browser, bool silent)
		{
			if (browser == null)
			{
				throw new ArgumentNullException("browser");
			}
		
		}

				[DllImport("user32.dll")]
		private static extern IntPtr GetDC(IntPtr A_0);

				[DllImport("user32.dll")]
		public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDc);

				[DllImport("gdi32.dll")]
		public static extern IntPtr CreateDC(string lpszDriver, string lpszDevice, string lpszOutput, long lpInitData);

				[DllImport("gdi32.dll")]
		public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

				[DllImport("user32.dll")]
		public static extern bool SetProcessDPIAware();

				private static PointF b()
		{
			PointF result = new PointF(1f, 1f);
			try
			{
				WebBrowserHelper.SetProcessDPIAware();
				IntPtr dc = WebBrowserHelper.GetDC(IntPtr.Zero);
				int deviceCaps = WebBrowserHelper.GetDeviceCaps(dc, 88);
				int deviceCaps2 = WebBrowserHelper.GetDeviceCaps(dc, 90);
				result.X = (float)deviceCaps / 96f;
				result.Y = (float)deviceCaps2 / 96f;
				WebBrowserHelper.ReleaseDC(IntPtr.Zero, dc);
				return result;
			}
			catch (Exception)
			{
			}
			return result;
		}

				private static void a(WebBrowser A_0, int A_1)
		{
			try
			{
				if (A_0 != null)
				{
					FieldInfo field = A_0.GetType().GetField("_axIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
					if (null != field)
					{
						object value = field.GetValue(A_0);
						if (value != null)
						{
							object[] args = new object[]
							{
								WebBrowserHelper.h,
								WebBrowserHelper.g,
								A_1,
								IntPtr.Zero
							};
							value.GetType().InvokeMember("ExecWB", BindingFlags.InvokeMethod, null, value, args);
						}
					}
				}
			}
			catch (Exception)
			{
			}
		}

				private static void a(object A_0, NavigationEventArgs A_1)
		{
			WebBrowser webBrowser = A_0 as WebBrowser;
			if (webBrowser != null)
			{
			
				PointF pointF = WebBrowserHelper.b();
				if (100 != (int)(pointF.X * 100f))
				{
					WebBrowserHelper.a(webBrowser, (int)(pointF.X * pointF.Y * 100f));
				}
			}
		}

				private static bool a()
		{
			return 100 != (int)(WebBrowserHelper.b().X * 100f);
		}

			
		static WebBrowserHelper()
		{
		}

				private WebBrowser m_a;

				private object m_b;

				[CompilerGenerated]
		private EventHandler<WebBrowserExtendedNavigatingEventArgs> m_c;

				[CompilerGenerated]
		private EventHandler<WebBrowserExtendedNavigatingEventArgs> m_d;

				private const int e = 88;

				private const int f = 90;

				private static readonly int g = 0;

				private static readonly int h = 63;

				[ComImport]
		[Guid("6D5140C1-7436-11CE-8034-00AA006009FA")]
		[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
		private interface A
		{
						[PreserveSig]
			int a([In] ref Guid A_0, [In] ref Guid A_1, [MarshalAs(UnmanagedType.IDispatch)] out object A_2);
		}

				private class B : StandardOleMarshalObject, DWebBrowserEvents2
		{
						public B(WebBrowserHelper A_0)
			{
				this.n_a = A_0;
			}

						public void BeforeNavigate2(object pDisp, ref object URL, ref object Flags, ref object TargetFrameName, ref object PostData, ref object Headers, ref bool Cancel)
			{
				this.n_a.a((string)URL, (string)TargetFrameName, out Cancel);
			}

						public void NewWindow3(ref object ppDisp, ref bool Cancel, ref object dwFlags, ref object bstrUrlContext, ref object bstrUrl)
			{
				this.n_a.a((string)bstrUrl, out Cancel);
			}

						private WebBrowserHelper n_a;
		}
	}
}
