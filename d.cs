using System;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;

internal class d : WebClient
{
		public d() : this(60000)
	{
	}

		public d(int A_0)
	{
		this.a(A_0);
	}

		[CompilerGenerated]
	public int a()
	{
		return this.m_a;
	}

		[CompilerGenerated]
	public void a(int A_0)
	{
		this.m_a = A_0;
	}

		protected override WebRequest GetWebRequest(Uri address)
	{
		WebRequest webRequest = base.GetWebRequest(address);
		webRequest.Timeout = this.a();
		return webRequest;
	}

		public MemoryStream a(string A_0)
	{
		return new MemoryStream(base.DownloadData(A_0));
	}

		public MemoryStream a(Uri A_0)
	{
		return new MemoryStream(base.DownloadData(A_0));
	}

		[CompilerGenerated]
	private int m_a;
}
