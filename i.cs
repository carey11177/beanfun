using System;
using System.Runtime.InteropServices;
using System.Text;

internal static class i
{
		[DllImport("user32.dll", SetLastError = true)]
	public static extern IntPtr FindWindow(string A_0, string A_1);

		[DllImport("user32.dll", SetLastError = true)]
	public static extern uint GetWindowThreadProcessId(IntPtr A_0, out int A_1);

		[DllImport("user32.dll")]
	public static extern int SendMessage(IntPtr A_0, uint A_1, IntPtr A_2, IntPtr A_3);

		[DllImport("USER32.DLL")]
	public static extern bool SetForegroundWindow(IntPtr A_0);

		[DllImport("User32.dll")]
	public static extern int PostMessage(IntPtr A_0, uint A_1, int A_2, int A_3);

		[DllImport("User32.dll")]
	public static extern int PostMessage(IntPtr A_0, uint A_1, int A_2, IntPtr A_3);

		[DllImport("user32.dll")]
	public static extern void keybd_event(byte A_0, byte A_1, int A_2, int A_3);

		[DllImport("Kernel32.dll", CharSet = CharSet.Auto)]
	private static extern int GetSystemDefaultLocaleName([Out] StringBuilder A_0, int A_1);

		public static string a()
	{
		StringBuilder stringBuilder = new StringBuilder(85);
		if (i.GetSystemDefaultLocaleName(stringBuilder, 85) > 0)
		{
			return stringBuilder.ToString();
		}
		return null;
	}

		[DllImport("kernel32.dll")]
	public static extern IntPtr GetCurrentProcess();

		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern IntPtr GetModuleHandle(string A_0);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr GetProcAddress(IntPtr A_0, [MarshalAs(UnmanagedType.LPStr)] string A_1);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool IsWow64Process(IntPtr A_0, out bool A_1);

        public const int m_a = 85;

}
