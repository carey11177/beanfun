using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace Beanfun
{
		public partial class GameList : Window, IComponentConnector
	{
				public GameList()
		{
			this.InitializeComponent();
			string str = (App.LoginRegion == "TW") ? "https://tw.images.beanfun.com/uploaded_images/beanfun_tw/game_zone/" : "http://hk.images.beanfun.com/uploaded_images/beanfun/game_zone/";
			WebClient webClient = new WebClient();
			foreach (MainWindow.GameService gameService in App.MainWnd.gameList)
			{
				byte[] buffer = webClient.DownloadData(str + gameService.large_image_name);
				BitmapImage bitmapImage = new BitmapImage();
				bitmapImage.BeginInit();
				bitmapImage.StreamSource = new MemoryStream(buffer);
				bitmapImage.EndInit();
				this.imagea.Items.Add(new GameList.Game(bitmapImage, gameService.name, gameService.service_code, gameService.service_region));
			}
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			base.DragMove();
		}

				private void a(object A_0, SelectionChangedEventArgs A_1)
		{
			if (this.imagea.SelectedIndex < 0)
			{
				return;
			}
			App.MainWnd.service_code = ((GameList.Game)this.imagea.SelectedItem).service_code;
			App.MainWnd.service_region = ((GameList.Game)this.imagea.SelectedItem).service_region;
			App.MainWnd.selectedGameChanged();
			base.Close();
		}

			

				internal ListView m_a;

				private bool b;

				public class Game
		{
				
			public Image image
			{
				[CompilerGenerated]
				get
				{
					return this.m_a;
				}
				[CompilerGenerated]
				set
				{
					this.m_a = value;
				}
			}

		
			public string name
			{
				[CompilerGenerated]
				get
				{
					return this.m_b;
				}
				[CompilerGenerated]
				set
				{
					this.m_b = value;
				}
			}

					
			public string service_code
			{
				[CompilerGenerated]
				get
				{
					return this.m_c;
				}
				[CompilerGenerated]
				set
				{
					this.m_c = value;
				}
			}

					
			public string service_region
			{
				[CompilerGenerated]
				get
				{
					return this.m_d;
				}
				[CompilerGenerated]
				set
				{
					this.m_d = value;
				}
			}

						public Game(BitmapImage source, string name, string service_code, string service_region)
			{
				this.image = new Image();
				this.image.Source = source;
				this.name = name;
				this.service_code = service_code;
				this.service_region = service_region;
			}

						[CompilerGenerated]
			private Image m_a;

						[CompilerGenerated]
			private string m_b;

						[CompilerGenerated]
			private string m_c;

						[CompilerGenerated]
			private string m_d;
		}
	}
}
