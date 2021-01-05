using System;
using System.ComponentModel;

namespace Beanfun
{
		public class WebBrowserExtendedNavigatingEventArgs : CancelEventArgs
	{
			
		public string Url
		{
			get
			{
				return this.a;
			}
		}

	
		public string Frame
		{
			get
			{
				return this.b;
			}
		}

				public WebBrowserExtendedNavigatingEventArgs(string url, string frame)
		{
			this.a = url;
			this.b = frame;
		}

				private string a;

				private string b;
	}
}
