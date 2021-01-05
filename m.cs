using System.Runtime.CompilerServices;

[CompilerGenerated]
internal sealed class m
{
	internal static uint a(string A_0)
	{
		uint num = default(uint);
		if (A_0 != null)
		{
			num = 2166136261u;
			for (int i = 0; i < A_0.Length; i++)
			{
				num = (A_0[i] ^ num) * 16777619;
			}
		}
		return num;
	}
}
