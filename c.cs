using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Amemiya.Extensions;
using Microsoft.Win32;

internal class c
{
	internal struct structa
	{
		internal uint unita;

		internal uint unitb;

		internal uint unitc;

		internal uint unitd;

		internal uint unite;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		internal byte[] f;

		internal c.structc g;
	}

	internal struct structb
	{
		internal IntPtr IntPtra;

		internal IntPtr IntPtrb;

		internal uint uintc;

		internal uint uintd;

		internal IntPtr e;
	}

	internal struct structc
	{
		internal int inta;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		internal byte[] byteb;

		internal structf c;

		internal int d;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 64)]
		internal byte[] e;

		internal structf f;

		internal int g;

		public override string ToString()
		{
			return "StandardName=" + Encoding.Unicode.GetString(this.byteb);
		}

		public string a()
		{
			return Encoding.Unicode.GetString(this.byteb).Replace("\0", "");
		}

		public void a(string A_0)
		{
			this.byteb = global::c.a(new byte[64], (IEnumerable<byte>)Encoding.Unicode.GetBytes(A_0));
		}

		public string b()
		{
			return Encoding.Unicode.GetString(e).Replace("\0", "");
		}

		public void b(string A_0)
		{
			e = global::c.a(new byte[64], (IEnumerable<byte>)Encoding.Unicode.GetBytes(A_0));
		}
	}

	private struct d
	{
		internal int inta;

		internal int b;

		internal int intc;

		internal structe structed;

		internal structe e;
	}

	internal struct structe
	{
		internal ushort ushorta;

		internal ushort b;

		internal ushort ushortc;

		internal ushort d;

		internal ushort e;

		internal ushort f;

		internal ushort g;

		internal ushort h;
	}

	internal struct structf
	{
		internal ushort a;

		internal ushort b;

		internal ushort ushortc;

		internal ushort d;

		internal ushort e;

		internal ushort f;

		internal ushort g;

		internal ushort h;
	}

	[CompilerGenerated]
	private class g
	{
		private string sealeda;

		internal bool a(TimeZoneInfo A_0)
		{
			return /*true*/A_0.Id == this.sealeda;
		}
	}

	private const uint m_a = 0u;

	private const uint m_b = 4u;

	private byte[] m_c = new byte[64];

	private structa m_d;

	private global::b m_e = new global::b(0);

	[CompilerGenerated]
	private string m_f;

	[CompilerGenerated]
	private string m_g;

	[CompilerGenerated]
	private string m_h;

	[CompilerGenerated]
	private bool m_i;

	internal c()
		: this(null, null, null)
	{
	}

	internal c(string A_0)
		: this(A_0, null, null)
	{
	}

	internal c(string A_0, string A_1, string A_2)
	{
		b(A_0);
		cc(A_1);
		cd(A_2);
		this.m_d = new structa
		{
			unita = 932u,
			unitb = 932u,
			unitc = 1041u,
			unitd = 128u,
			unite = 0u,
			f = new byte[64]
		};
		e("Tokyo Standard Time");
	}

	[CompilerGenerated]
	internal string a()
	{
		return this.m_f;
	}

	[CompilerGenerated]
	internal void b(string A_0)
	{
		this.m_f = A_0;
	}

	[CompilerGenerated]
	internal string b()
	{
		return this.m_g;
	}

	[CompilerGenerated]
	internal void cc(string A_0)
	{
		this.m_g = A_0;
	}

	[CompilerGenerated]
	internal string cc()
	{
		return this.m_h;
	}

	[CompilerGenerated]
	internal void cd(string A_0)
	{
		this.m_h = A_0;
	}

	[CompilerGenerated]
	internal bool cd()
	{
		return this.m_i;
	}

	[CompilerGenerated]
	internal void a(bool A_0)
	{
		this.m_i = A_0;
	}

	internal uint e()
	{
		return this.m_d.unita;
	}

	internal void a(uint A_0)
	{
		this.m_d.unita = A_0;
	}

	internal uint f()
	{
		return this.m_d.unitb;
	}

	internal void b(uint A_0)
	{
		this.m_d.unitb = A_0;
	}

	internal uint cg()
	{
		return this.m_d.unitc;
	}

	internal void cc(uint A_0)
	{
		this.m_d.unitc = A_0;
	}

	internal uint h()
	{
		return this.m_d.unitd;
	}

	internal void cd(uint A_0)
	{
		this.m_d.unitd = A_0;
	}

	internal uint i()
	{
		return this.m_d.unite;
	}

	internal void e(uint A_0)
	{
		this.m_d.unite = A_0;
	}

	internal string j()
	{
		return this.m_d.g.a();
	}

	internal void e(string A_0)
	{
		if (A_0.Length > 32)
		{
			throw new Exception("String too long.");
		}

		if (!TimeZoneInfo.GetSystemTimeZones().Any((TimeZoneInfo timeZoneInfo1) => timeZoneInfo1.Id == A_0))
		{
			throw new Exception("Timezone \"" + A_0 + "\" not found in your system.");
		}
		TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(A_0);
		string tiem = timeZoneInfo.StandardName;
		this.m_d.g.a(timeZoneInfo.StandardName);
		this.m_d.g.b(timeZoneInfo.StandardName);
		//structc structc=new structc();       
		d d = a(A_0);
		this.m_d.g.inta = d.inta;
		this.m_d.g.d = d.b;
		this.m_d.g.g = 0;
	}

	internal int k()
	{
		return this.m_e.A();
	}

	internal void a(int A_0)
	{
		this.m_e = new global::b(A_0);
	}

	internal bool a(string A_0, string A_1, string A_2, string A_3, string A_4)
	{
		return this.m_e.A(A_0, A_1, A_2, A_3, A_4);
	}

	internal uint l()
	{
		if (string.IsNullOrEmpty(a()))
		{
			throw new Exception("ApplicationName cannot null.");
		}
		byte[] source = ArrayExtensions.StructToBytes(this.m_d);
		source = source.CombineWith(this.m_e.Bb());
		IntPtr intPtr = Marshal.AllocHGlobal(source.Length);
		Marshal.Copy(source, 0, intPtr, source.Length);
		uint result = LeCreateProcess(intPtr, a(), b(), cc(), cd() ? 4u : 0u, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
		Marshal.FreeHGlobal(intPtr);
		return result;
	}

	private static byte[] a(byte[] A_0, IEnumerable<byte> A_1)
	{
		int num = 0;
		foreach (byte item in A_1)
		{
			byte b = (A_0[num] = item);
			num++;
		}
		return A_0;
	}

	public static a a<a>(byte[] A_0)
	{
		int num = Marshal.SizeOf(typeof(a));
		IntPtr intPtr = Marshal.AllocHGlobal(num);
		try
		{
			Marshal.Copy(A_0, 0, intPtr, num);
			return (a)Marshal.PtrToStructure(intPtr, typeof(a));
		}
		finally
		{
			Marshal.FreeHGlobal(intPtr);
		}
	}

	private structf a(structe A_0)
	{
		structf result = default(structf);
		result.a = A_0.ushorta;
		result.b = A_0.b;
		result.ushortc = A_0.d;
		result.d = A_0.e;
		result.e = A_0.f;
		result.f = A_0.g;
		result.g = A_0.h;
		result.h = A_0.ushortc;
		return result;
	}

	private d a(string A_0)
	{
		return a<d>((byte[])Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\" + A_0, "TZI", null));
	}

	[DllImport("LoaderDll.dll", CharSet = CharSet.Unicode)]
	public static extern uint LeCreateProcess(IntPtr A_0, [In][MarshalAs(UnmanagedType.LPWStr)] string A_1, [In][MarshalAs(UnmanagedType.LPWStr)] string A_2, [In][MarshalAs(UnmanagedType.LPWStr)] string A_3, uint A_4, IntPtr A_5, IntPtr A_6, IntPtr A_7, IntPtr A_8, IntPtr A_9, IntPtr A_10);
}
