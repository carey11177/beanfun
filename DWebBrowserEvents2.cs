using System;
using System.Runtime.InteropServices;

namespace Beanfun
{
		[ComImport]
	[Guid("34A715A0-6587-11D0-924A-0020AFC7AC4D")]
	[InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
	[TypeLibType(TypeLibTypeFlags.FHidden)]
	public interface DWebBrowserEvents2
	{
				[DispId(250)]
		void BeforeNavigate2([In] [MarshalAs(UnmanagedType.IDispatch)] object pDisp, [In] ref object URL, [In] ref object Flags, [In] ref object TargetFrameName, [In] ref object PostData, [In] ref object Headers, [In] [Out] ref bool Cancel);

				[DispId(273)]
		void NewWindow3([In] [MarshalAs(UnmanagedType.IDispatch)] [Out] ref object ppDisp, [In] [Out] ref bool Cancel, [In] ref object dwFlags, [In] ref object bstrUrlContext, [In] ref object bstrUrl);
	}
}
