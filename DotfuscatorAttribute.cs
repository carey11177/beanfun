using System;
using System.Runtime.InteropServices;

[AttributeUsage(AttributeTargets.Assembly)]
[ComVisible(false)]
public sealed class DotfuscatorAttribute : Attribute
{
		public DotfuscatorAttribute(string a, int c)
	{
		this.a = a;
		this.c = c;
	}

	
	public string A
	{
		get
		{
			return this.a;
		}
	}

	
	public int C
	{
		get
		{
			return this.c;
		}
	}

		private string a;

		private int c;
}
