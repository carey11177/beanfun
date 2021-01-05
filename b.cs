using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Amemiya.Extensions;

internal class b
{
	internal struct a
	{
		internal ulong ulongA;

		internal c b;

		internal c c;

		internal uint d;

		internal long e;

		internal ulong f;
	}

	internal struct bc
	{
		internal a a;

		internal a ab;
	}

	internal struct c
	{
		internal ushort a;

		internal ushort b;

		internal long bc;
	}

	private static readonly Dictionary<string, ulong> m_a = new Dictionary<string, ulong>
	{
		{
			"HKEY_CLASSES_ROOT",
			2147483648uL
		},
		{
			"HKEY_CURRENT_USER",
			2147483649uL
		},
		{
			"HKEY_LOCAL_MACHINE",
			2147483650uL
		},
		{
			"HKEY_USERS",
			2147483651uL
		},
		{
			"HKEY_CURRENT_CONFIG",
			2147483653uL
		}
	};

	private static readonly Dictionary<string, uint> m_b = new Dictionary<string, uint>
	{
		{
			"REG_SZ",
			1u
		},
		{
			"REG_EXPAND_SZ",
			2u
		},
		{
			"REG_BINARY",
			3u
		},
		{
			"REG_DWORD",
			4u
		},
		{
			"REG_MULTI_SZ",
			7u
		},
		{
			"REG_QWORD",
			11u
		}
	};

	private readonly List<byte> m_c = new List<byte>();

	private readonly List<bc> d;

	[CompilerGenerated]
	private int e;

	internal b(int A_0)
	{
		A(A_0);
		d = new List<bc>(A_0);
	}

	[CompilerGenerated]
	internal int A()
	{
		return e;
	}

	[CompilerGenerated]
	internal void A(int A_0)
	{
		e = A_0;
	}

	internal byte[] Bb()
	{
		byte[] bytes = BitConverter.GetBytes((ulong)A());
		byte[] array = new byte[A() * Marshal.SizeOf((object)default(bc))];
		array.FillWith((byte)0);
		for (int i = 0; i < d.Count; i++)
		{
			array.SetRange(ArrayExtensions.StructToBytes(d[i]), i * Marshal.SizeOf((object)d[i]));
		}
		bytes = bytes.CombineWith(array);
		return bytes.CombineWith(this.m_c.ToArray());
	}

	internal bool A(string A_0, string A_1, string A_2, string A_3, string A_4)
	{
		try
		{
			a a = default(a);
			a.ulongA = global::b.m_a[A_0];
			a.b = default(c);
			a.c = default(c);
			a.d = global::b.m_b[A_3];
			a.e = 0L;
			a.f = 0uL;
			a a2 = a;
			this.A(A_1, out a2.b.bc, out a2.b.a, out a2.b.b);
			this.A(A_2, out a2.c.bc, out a2.c.a, out a2.c.b);
			this.A(A_3, A_4, out a2.e, out a2.f);
			global::b.bc item = new global::b.bc
			{
				a = a2,
				ab = a2
			};

			d.Add(item);
			return true;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return false;
		}
	}

	private void A(string A_0, out long A_1, out ushort A_2, out ushort A_3)
	{
		A("REG_SZ", A_0, out A_1, out var A_4);
		A_2 = (ushort)A_4;
		A_3 = (ushort)A_4;
	}

	private void A(string A_0, string A_1, out long A_2, out ulong A_3)
	{
		A_2 = Marshal.SizeOf((object)default(global::c.structa)) + A() * Marshal.SizeOf((object)default(b.bc)) + this.m_c.Count + 8;
		switch (A_0)
		{
			case "REG_SZ":
			case "REG_EXPAND_SZ":
			case "REG_MULTI_SZ":
				A_3 = (ulong)Encoding.Unicode.GetBytes(A_1).Length;
				this.m_c.AddRange(Encoding.Unicode.GetBytes(A_1));
				break;
			case "REG_DWORD":
				A_3 = 4uL;
				this.m_c.AddRange(BitConverter.GetBytes(uint.Parse(A_1)));
				break;
			case "REG_QWORD":
				A_3 = 8uL;
				this.m_c.AddRange(BitConverter.GetBytes(ulong.Parse(A_1)));
				break;
			default:
				throw new Exception("Data type " + A_0 + " not supported yet.");
		}
		this.m_c.AddRange(new byte[2]);
	}
}
