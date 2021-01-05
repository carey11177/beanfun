using System;
using System.Runtime.CompilerServices;

namespace Beanfun
{
		public class Response
	{
		
		public string Result
		{
			[CompilerGenerated]
			get
			{
				return this.a;
			}
			[CompilerGenerated]
			set
			{
				this.a = value;
			}
		}

			
		public string ResultMessage
		{
			[CompilerGenerated]
			get
			{
				return this.b;
			}
			[CompilerGenerated]
			set
			{
				this.b = value;
			}
		}

			
		public object ResultData
		{
			[CompilerGenerated]
			get
			{
				return this.c;
			}
			[CompilerGenerated]
			set
			{
				this.c = value;
			}
		}

	
		public string ReturnValue
		{
			[CompilerGenerated]
			get
			{
				return this.d;
			}
			[CompilerGenerated]
			set
			{
				this.d = value;
			}
		}

				
		public string ReturnMessage
		{
			[CompilerGenerated]
			get
			{
				return this.e;
			}
			[CompilerGenerated]
			set
			{
				this.e = value;
			}
		}

		
		public object ReturnData
		{
			[CompilerGenerated]
			get
			{
				return this.f;
			}
			[CompilerGenerated]
			set
			{
				this.f = value;
			}
		}

				public Response()
		{
		}

				[CompilerGenerated]
		private string a;

				[CompilerGenerated]
		private string b;

				[CompilerGenerated]
		private object c;

				[CompilerGenerated]
		private string d;

				[CompilerGenerated]
		private string e;

				[CompilerGenerated]
		private object f;
	}
}
