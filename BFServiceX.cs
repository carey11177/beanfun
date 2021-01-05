using System;
using BFService;

namespace Beanfun
{
		public class BFServiceX
	{
				public BFServiceX()
		{
			this.a = new BFServiceXClass();
		}

				public uint Initialize2()
		{
			uint result;
			try
			{
				uint num = this.a.Initialize2("HK;Production", "", "", 0U, "");
				if (num != 0U)
				{
					result = this.Initialize2();
				}
				else
				{
					result = num;
				}
			}
			catch
			{
				result = this.Initialize2();
			}
			return result;
		}

			
		public string Token
		{
			get
			{
				string result;
				try
				{
					this.a.SaveData("Seed", "0");
					this.a.SaveData("Token", this.b);
					string text = this.a.LoadData("Token");
					if (text == "Failure")
					{
						this.Initialize2();
						result = this.Token;
					}
					else
					{
						result = text;
					}
				}
				catch
				{
					result = this.Token;
				}
				return result;
			}
			set
			{
				this.b = value;
			}
		}

				private BFServiceXClass a;

				private string b;
	}
}
