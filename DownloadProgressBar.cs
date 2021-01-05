using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
		public partial class DownloadProgressBar : Window, IComponentConnector
	{
				public DownloadProgressBar(List<string> taskFiles, string title = "正在下载...", string dir = null, bool isShowFileName = true)
		{
			this.InitializeComponent();
			this.m_a = new WebClient();
			this.m_d = title;
			if (dir != null)
			{
				this.m_e = dir;
			}
			this.m_c = isShowFileName;
			this.m_a.DownloadProgressChanged += this.a;
			this.m_a.DownloadFileCompleted += this.a;
			this.b = taskFiles;
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			this.a();
		}

				private void a(object A_0, CancelEventArgs A_1)
		{
			this.m_a.CancelAsync();
		}

				private void a(object A_0, DownloadProgressChangedEventArgs A_1)
		{
			DownloadProgressBar.A A = new DownloadProgressBar.A();
			A.dlpa = this;
			A.dlpb = A_1;
			base.Dispatcher.Invoke(new Action(A.a));
		}

				private void a(object A_0, AsyncCompletedEventArgs A_1)
		{
			if (A_1.Cancelled)
			{
				return;
			}
			this.DownloadedFileNum++;
			if (this.DownloadedFileNum == this.TaskFileNum)
			{
				base.Close();
				return;
			}
			if (this.m_a != null && this.m_a.IsBusy)
			{
				this.m_a.CancelAsync();
			}
			if (this.DownloadedFileNum < this.TaskFileNum)
			{
				this.a(this.b[this.DownloadedFileNum]);
			}
		}

				private void a()
		{
			if (this.b != null)
			{
				this.TaskFileNum = this.b.Count;
				if (this.TaskFileNum > 0)
				{
					string a_ = this.b[0];
					this.a(a_);
				}
			}
		}

				private void a(string A_0)
		{
			if (!Directory.Exists(this.m_e))
			{
				Directory.CreateDirectory(this.m_e);
			}
			string text = A_0.Substring(A_0.LastIndexOf("/") + 1);
			string text2 = this.m_e + text;
			if (File.Exists(text2))
			{
				File.Delete(text2);
			}
			if (this.m_c)
			{
				this.f.Visibility = Visibility.Visible;
				this.f.Content = text;
			}
			else
			{
				this.f.Visibility = Visibility.Collapsed;
			}
			base.Title = this.m_d + ((this.TaskFileNum > 1) ? string.Format("({0}/{1})", this.DownloadedFileNum + 1, this.TaskFileNum) : "");
			this.m_a.DownloadFileAsync(new Uri(A_0), text2);
		}

				

				private WebClient m_a;

				private List<string> b = new List<string>();

				public int TaskFileNum;

				public int DownloadedFileNum;

				private bool m_c;

				private string m_d;

				private string m_e = Environment.CurrentDirectory + "\\";

				internal Label m_f;

				internal Label m_g;

				internal ProgressBar m_h;

	

				[CompilerGenerated]
		private sealed class A
		{
						public A()
			{
			}

						internal void a()
			{
				this.dlpa.m_h.Value = (double)this.dlpb.ProgressPercentage;
				this.dlpa.g.Content = this.dlpb.ProgressPercentage.ToString() + " %";
			}

						public DownloadProgressBar dlpa;

						public DownloadProgressChangedEventArgs dlpb;
		}
	}
}
