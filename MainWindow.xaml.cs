using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using Beanfun.Properties;
using IniParser;
using IniParser.Model;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;

namespace Beanfun
{
		public partial class MainWindow : Window
	{
		public LoginPage loginPage;

				public ManagerAccount manageAccPage;

				public LoginWait loginWaitPage = new LoginWait();

				public AccountList accountList;

				public VerifyPage verifyPage;

				public Settings settingPage;

				public About aboutPage;

				public BackgroundWorker getOtpWorker;

				public BackgroundWorker loginWorker;

				public BackgroundWorker pingWorker;

				public BackgroundWorker qrWorker;

				public BackgroundWorker verifyWorker;

				public DispatcherTimer qrCheckLogin;

				public DispatcherTimer checkPlayPage;

				public DispatcherTimer checkPatcher;

				public DispatcherTimer bfAPPAutoLogin;

				public AccountManager accountManager;

				public BeanfunClient bfClient;

				public BeanfunClient.QRCodeClass qrcodeClass;

				public string LastLoginAccountID = "";

				public string service_code = "610074";

				public string service_region = "T9";

				public string game_exe = "MapleStory.exe";

				public string dir_value_name = "Path";

				public string win_class_name = "MapleStoryClass";

				public short login_action_type = 1;

				public string game_commandLine = "tw.login.maplestory.gamania.com 8484 BeanFun %s %s";

				private string m_a;

				private BitmapImage m_b;

				private static readonly NotifyIcon m_c = new NotifyIcon
		{
			Icon = Beanfun.Properties.Resources.icon
		};

				private Version v_d = Assembly.GetExecutingAssembly().GetName().Version;

				public List<MainWindow.GameService> gameList = new List<MainWindow.GameService>();

				public MainWindow.GameService SelectedGame;

				public bool UnconnectedGame;

				public string viewstate;

				public string eventvalidation;

				public string samplecaptcha;

				public Page return_page;

				public IniData INIData;
				
		
		public class GameService
		{
					
			public string name { get; set; }
						
			public string service_code { get; set; }
					
			public string service_region { get; set; }
					
			public string website_url { get; set; }
						
			public string xlarge_image_name { get; set; }
					
			public string large_image_name { get; set; }

			public string small_image_name { get; set; }

					
			public string download_url { get; set; }

						public GameService(string name, string service_code, string service_region, string website_url, string xlarge_image_name, string large_image_name, string small_image_name, string download_url)
			{
				this.name = name;
				this.service_code = service_code;
				this.service_region = service_region;
				this.website_url = website_url;
				this.xlarge_image_name = xlarge_image_name;
				this.large_image_name = large_image_name;
				this.small_image_name = small_image_name;
				this.download_url = download_url;
			}

						[CompilerGenerated]
			private string v_a;

						[CompilerGenerated]
			private string v_b;

						[CompilerGenerated]
			private string v_c;

						[CompilerGenerated]
			private string v_d;

						[CompilerGenerated]
			private string v_e;

						[CompilerGenerated]
			private string v_f;

						[CompilerGenerated]
			private string v_g;

						[CompilerGenerated]
			private string v_h;
		}

				[CompilerGenerated]
		private sealed class A
		{
						internal void ab()
			{
				this.s_a = this.w_b.bfClient.verify(this.w_b.viewstate, this.w_b.eventvalidation, this.w_b.samplecaptcha, this.w_b.verifyPage.imagea.Text, this.w_b.verifyPage.imaged.Text);
			}

						public string s_a;

						public MainWindow w_b;
		}

				[CompilerGenerated]
		private sealed class B
		{
						internal void a()
			{
				this.s_a = this.Ab.w_b.a(this.Ab.s_a);
				this.Ab.w_b.verifyPage.imaged.Text = "";
			}

						public string s_a;

						public MainWindow.A Ab;
		}
				public MainWindow()
		{
			this.InitializeComponent();
			try
			{
				this.getOtpWorker = new BackgroundWorker();
				this.loginWorker = new BackgroundWorker();
				this.pingWorker = new BackgroundWorker();
				this.qrWorker = new BackgroundWorker();
				this.verifyWorker = new BackgroundWorker();
				this.qrCheckLogin = new DispatcherTimer();
				this.checkPlayPage = new DispatcherTimer();
				this.checkPatcher = new DispatcherTimer();
				this.bfAPPAutoLogin = new DispatcherTimer();
				this.getOtpWorker.WorkerReportsProgress = true;
				this.getOtpWorker.WorkerSupportsCancellation = true;
				this.getOtpWorker.DoWork += this.d;
				this.getOtpWorker.RunWorkerCompleted += this.d;
				this.loginWorker.WorkerReportsProgress = true;
				this.loginWorker.WorkerSupportsCancellation = true;
				this.loginWorker.DoWork += this.e;
				this.loginWorker.RunWorkerCompleted += this.ea;
				this.pingWorker.WorkerReportsProgress = true;
				this.pingWorker.WorkerSupportsCancellation = true;
				this.pingWorker.DoWork += this.c;
				this.pingWorker.RunWorkerCompleted += this.c;
				this.qrWorker.WorkerReportsProgress = true;
				this.qrWorker.WorkerSupportsCancellation = true;
				this.qrWorker.DoWork += this.b;
				this.qrWorker.RunWorkerCompleted += this.b;
				this.verifyWorker.WorkerReportsProgress = true;
				this.verifyWorker.WorkerSupportsCancellation = true;
				this.verifyWorker.DoWork += this.a;
				this.verifyWorker.RunWorkerCompleted += this.a;
				this.qrCheckLogin.Interval = TimeSpan.FromSeconds(2.0);
				this.qrCheckLogin.Tick += this.d;
				this.checkPlayPage.Interval = TimeSpan.FromMilliseconds(100.0);
				this.checkPlayPage.Tick += this.b;
				this.checkPatcher.Interval = TimeSpan.FromMilliseconds(100.0);
				this.checkPatcher.Tick += this.a;
				this.bfAPPAutoLogin.Interval = TimeSpan.FromSeconds(2.0);
				this.bfAPPAutoLogin.Tick += this.c;
				this.loginPage = new LoginPage();
				this.manageAccPage = new ManagerAccount();
				this.verifyPage = new VerifyPage();
				this.accountList = new AccountList();
				this.settingPage = new Settings();
				this.aboutPage = new About();
				App.LoginRegion = (this.loginPage.imagea.IsEnabled ? "HK" : "TW");
				if (this.settingPage.imagef != null && !this.settingPage.imagef.IsChecked.Value)
				{
                    this.accountList.imagez.Visibility = Visibility.Collapsed;
                }
                this.aboutPage.imagea.Text = string.Format("{0}.{1}.{2}({3})", new object[]
                {
                    this.v_d.Major,
                    this.v_d.Minor,
                    this.v_d.Build,
                    this.v_d.Revision
                });
                this.m_b = new BitmapImage();
				this.m_b.BeginInit();
				this.m_b.UriSource = new Uri("pack://application:,,,/Resources/refresh.png");
				this.m_b.EndInit();
				this.loginPage.qr.imaged.Source = this.m_b;
				string text = global::g.a("loginGame", "");
				if (text != "")
				{
					string[] array = text.Split(new char[]
					{
						'_'
					});
					if (array != null && array.Length > 1)
					{
						this.service_code = array[0];
						this.service_region = array[1];
					}
				}
				if (this.settingPage.imagea.IsChecked.Value)
				{
					this.CheckUpdates(false);
				}
				this.accountManager = new AccountManager();
				if (!this.accountManager.init())
				{
					this.errexit("账号记錄初始化失败，未知的错误。", 0, null);
				}
				this.loginPage.imagec.SelectionChanged += this.ddlAuthType_SelectionChanged;
				this.settingPage.imagee.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.a);
				this.LastLoginAccountID = global::g.a("AccountID", this.LastLoginAccountID);
				int num = this.accountManager.getMethodByAccount(App.LoginRegion, this.LastLoginAccountID);
				if (num < 0)
				{
					num = int.Parse(global::g.a("loginMethod", "0"));
				}
				this.ddlAuthTypeItemsInit();
				this.reLoadGameInfo();
				this.loginPage.imagec.SelectedIndex = num;
				this.o.Content = this.loginPage;
				MainWindow.m_c.MouseClick += this.a;
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message + "\r\n\r\n" + ex.StackTrace);
				System.Windows.Application.Current.Shutdown();
			}
		}

				public void selectedGameChanged()
		{
			string text = this.service_code + "_" + this.service_region;
			global::g.b("loginGame", text);
			if (this.INIData == null)
			{
				this.reLoadGameInfo();
				return;
			}
			string input = this.INIData[text]["exe"];
			Regex regex = new Regex("(.*).exe");
			if (regex.IsMatch(input))
			{
				this.game_exe = regex.Match(input).Groups[1].Value + ".exe";
			}
			else
			{
				this.game_exe = "";
			}
			regex = new Regex(".exe (.*)");
			if (regex.IsMatch(input))
			{
				this.game_commandLine = regex.Match(input).Groups[1].Value;
			}
			else
			{
				this.game_commandLine = "";
			}
			this.login_action_type = 8;
			string s = this.INIData[text]["login_action_type"];
			if (s != "")
			{
				this.login_action_type = short.Parse(s);
			}
			if (this.login_action_type == 1 && !(text == "300148_AF") && !(text == "600309_A2") && !(text == "610096_TE"))
			{
				this.login_action_type = -1;
			}
			if (this.login_action_type == -1)
			{
				this.settingPage.imagef.Visibility = Visibility.Visible;
				if (this.settingPage.imagef.IsChecked.Value)
				{
					this.accountList.imagez.Visibility = Visibility.Visible;
				}
				else
				{
                    this.accountList.imagez.Visibility = Visibility.Collapsed;
                }
			}
			else
			{
				this.settingPage.imagef.Visibility = Visibility.Collapsed;
				this.accountList.imagez.Visibility = Visibility.Collapsed;
			}
			this.win_class_name = this.INIData[text]["win_class_name"];
			if (this.win_class_name == "" || this.game_exe == "" || this.login_action_type == 1)
			{
				this.accountList.imageab.Visibility = Visibility.Collapsed;
			}
			else
			{
                this.accountList.imageab.Visibility = Visibility.Visible;
            }
			this.dir_value_name = this.INIData[text]["dir_value_name"];
			if (global::g.a(this.dir_value_name + "." + text, "") == "")
			{
				string text2 = this.INIData[text]["dir_reg"];
				if (!(text2 != ""))
				{
					goto IL_35D;
				}
				text2 = text2.Replace("HKEY_LOCAL_MACHINE\\", "");
				try
				{
					global::A a = new global::A();
					a.a(Registry.CurrentUser);
					a.a(text2);
					if (a.b(this.dir_value_name) != "")
					{
						global::g.b(this.dir_value_name + "." + text, a.b(this.dir_value_name));
						this.settingPage.imagee.Text = a.b(this.dir_value_name);
					}
					goto IL_35D;
				}
				catch
				{
					this.settingPage.imagee.Text = "";
					goto IL_35D;
				}
			}
			this.settingPage.imagee.Text = global::g.a(this.dir_value_name + "." + text);
			IL_35D:
			if (text == "610074_T9")
			{
				this.settingPage.imageh.Visibility = Visibility.Visible;
				if (this.settingPage.imageh.IsChecked.Value)
				{
					this.checkPlayPage.IsEnabled = true;
				}
				this.settingPage.imageg.Visibility = Visibility.Visible;
				if (this.settingPage.imageg.IsChecked.Value)
				{
					this.checkPatcher.IsEnabled = true;
				}
			}
			else
			{
				this.settingPage.imageh.Visibility = Visibility.Collapsed;
				this.checkPlayPage.IsEnabled = false;
				this.settingPage.imageg.Visibility = Visibility.Collapsed;
				this.checkPatcher.IsEnabled = false;
			}
			if (this.bfClient != null && !this.loginWorker.IsBusy && !this.getOtpWorker.IsBusy && (App.LoginRegion == "TW" || this.bfClient.gameServAccListApp != null))
			{
				if (App.LoginRegion == "TW")
				{
					this.bfClient.GetAccounts(this.service_code, this.service_region, true);
				}
				else
				{
					this.bfClient.GetAccounts_HK(this.service_code, this.service_region, true);
				}
				this.c();
				if (this.bfClient.errmsg != null)
				{
					this.errexit(this.bfClient.errmsg, 2, null);
					this.bfClient.errmsg = null;
				}
			}
			if (text == "610153_TN" || text == "610085_TC")
			{
				this.UnconnectedGame = true;
			}
			else
			{
				this.UnconnectedGame = false;
			}
			WebClient webClient = new WebClient();
			try
			{
				if (this.loginPage != null)
				{
					foreach (MainWindow.GameService gameService in this.gameList)
					{
						if (gameService.service_region == this.service_region && gameService.service_code == this.service_code)
						{
							BitmapImage bitmapImage;
							BitmapImage bitmapImage2;
							try
							{
								string str = (App.LoginRegion == "TW") ? "https://tw.images.beanfun.com/uploaded_images/beanfun_tw/game_zone/" : "http://hk.images.beanfun.com/uploaded_images/beanfun/game_zone/";
								byte[] buffer = webClient.DownloadData(str + gameService.large_image_name);
								bitmapImage = new BitmapImage();
								bitmapImage.BeginInit();
								bitmapImage.StreamSource = new MemoryStream(buffer);
								bitmapImage.EndInit();
								buffer = webClient.DownloadData(str + gameService.small_image_name);
								bitmapImage2 = new BitmapImage();
								bitmapImage2.BeginInit();
								bitmapImage2.StreamSource = new MemoryStream(buffer);
								bitmapImage2.EndInit();
							}
							catch (Exception)
							{
								bitmapImage = null;
								bitmapImage2 = null;
							}
							this.loginPage.id_pass.imagea.Source = bitmapImage;
							this.loginPage.qr.imagea.Content = gameService.name;
                            this.accountList.imagec.Source = bitmapImage2;
                            this.accountList.imaged.Content = gameService.name;
                            this.SelectedGame = gameService;
							break;
						}
					}
				}
			}
			catch
			{
			}
		}

				public void reLoadGameInfo()
		{
			WebClient webClient = new WebClient();
			string @string = Encoding.UTF8.GetString(webClient.DownloadData("https://" + App.LoginRegion.ToLower() + ".beanfun.com/beanfun_block/generic_handlers/get_service_ini.ashx"));
			StringIniParser stringIniParser = new StringIniParser();
			this.INIData = stringIniParser.ParseString(@string);
			@string = Encoding.UTF8.GetString(webClient.DownloadData("https://" + App.LoginRegion.ToLower() + ".beanfun.com/game_zone/"));
			Regex regex = new Regex("Services.ServiceList = (.*);");
			if (regex.IsMatch(@string))
			{
				this.gameList.Clear();
				foreach (JToken jtoken in JObject.Parse(regex.Match(@string).Groups[1].Value)["Rows"])
				{
					MainWindow.GameService gameService = new MainWindow.GameService((string)jtoken["ServiceFamilyName"], (string)jtoken["ServiceCode"], (string)jtoken["ServiceRegion"], (string)jtoken["ServiceWebsiteURL"], (string)jtoken["ServiceXLargeImageName"], (string)jtoken["ServiceLargeImageName"], (string)jtoken["ServiceSmallImageName"], (string)jtoken["ServiceDownloadURL"]);
					this.gameList.Add(gameService);
					if (gameService.service_code == this.service_code && gameService.service_region == this.service_region)
					{
						this.SelectedGame = gameService;
					}
				}
			}
			this.selectedGameChanged();
		}

				public void CheckUpdates(bool show)
		{
			global::l.a(this.v_d, show);
		}

				private string a(string A_0)
		{
			Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\"");
			if (!regex.IsMatch(A_0))
			{
				return "VerifyNoViewstate";
			}
			this.viewstate = regex.Match(A_0).Groups[1].Value;
			regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\"");
			if (!regex.IsMatch(A_0))
			{
				return "VerifyNoEventvalidation";
			}
			this.eventvalidation = regex.Match(A_0).Groups[1].Value;
			regex = new Regex("id=\"LBD_VCID_c_logincheck_advancecheck_samplecaptcha\" value=\"(.*)\"");
			if (!regex.IsMatch(A_0))
			{
				return "VerifyNoSamplecaptcha";
			}
			this.samplecaptcha = regex.Match(A_0).Groups[1].Value;
			regex = new Regex("\\<span id=\"lblAuthType\"\\>(.*)\\<\\/span\\>");
			if (!regex.IsMatch(A_0))
			{
				return "VerifyNoLblAuthType";
			}
			this.verifyPage.imagec.Content = regex.Match(A_0).Groups[1].Value;
			regex = new Regex("alert\\('(.*)'\\);");
			if (regex.IsMatch(A_0))
			{
				return regex.Match(A_0).Groups[1].Value;
			}
			this.verifyPage.imagee.Source = this.bfClient.getVerifyCaptcha(this.samplecaptcha);
			return null;
		}

				private void a(object A_0, MouseButtonEventArgs A_1)
		{
			try
			{
				base.DragMove();
			}
			catch
			{
			}
		}

				private void f(object A_0, EventArgs A_1)
		{
			this.g(null, null);
			this.eb(null, null);
			this.cb(null, null);
			this.ab(null, null);
			if (base.IsActive)
			{
				this.imagee.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF7EA05A"));
                //this.imagef.Color = (Color)ColorConverter.ConvertFromString("#E5B6DE8E");
                return;
            }
            this.imagee.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
            //this.imagef.Color = (Color)ColorConverter.ConvertFromString("LightGray");
        }

				private void e(object A_0, EventArgs A_1)
		{
			if (this.settingPage != null && this.settingPage.imaged != null && this.settingPage.imaged.IsChecked.Value && base.WindowState == WindowState.Minimized)
			{
				base.Visibility = Visibility.Hidden;
				MainWindow.m_c.Visible = true;
			}
		}

				private void h(object A_0, System.Windows.Input.MouseEventArgs A_1)
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
			//this.v_h.Stroke = solidColorBrush;
			//this.v_h.Fill = solidColorBrush;
		}

				private void g(object A_0, System.Windows.Input.MouseEventArgs A_1)
		{
			SolidColorBrush solidColorBrush;
			if (base.IsActive)
			{
				solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
			}
			else
			{
				solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
			}
            this.imageh.Stroke = solidColorBrush;
            this.imageh.Fill = solidColorBrush;
        }

				private void d(object A_0, DependencyPropertyChangedEventArgs A_1)
		{
			if (this.imageg.IsKeyboardFocused)
			{
				this.o.Focus();
			}
		}

				private void f(object A_0, System.Windows.Input.MouseEventArgs A_1)
		{
			SolidColorBrush solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
			this.j.Stroke = solidColorBrush;
			this.j.Fill = solidColorBrush;
		}

				private void eb(object A_0, System.Windows.Input.MouseEventArgs A_1)
		{
			SolidColorBrush solidColorBrush;
			if (base.IsActive)
			{
				solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
			}
			else
			{
				solidColorBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
			}
			this.j.Stroke = solidColorBrush;
			this.j.Fill = solidColorBrush;
		}

				private void c(object A_0, DependencyPropertyChangedEventArgs A_1)
		{
			if (this.imagei.IsKeyboardFocused)
			{
				this.o.Focus();
			}
		}

				private void d(object A_0, System.Windows.Input.MouseEventArgs A_1)
		{
			this.l.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
		}

				private void cb(object A_0, System.Windows.Input.MouseEventArgs A_1)
		{
			if (base.IsActive)
			{
				this.l.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
				return;
			}
			this.l.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
		}

				private void b(object A_0, DependencyPropertyChangedEventArgs A_1)
		{
			if (this.imagek.IsKeyboardFocused)
			{
				this.o.Focus();
			}
		}

				private void b(object A_0, System.Windows.Input.MouseEventArgs A_1)
		{
			this.imagen.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("White"));
		}

				private void ab(object A_0, System.Windows.Input.MouseEventArgs A_1)
		{
			if (base.IsActive)
			{
				this.imagen.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
				return;
			}
			this.imagen.Stroke = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Gray"));
		}

				private void a(object A_0, DependencyPropertyChangedEventArgs A_1)
		{
			if (this.imagem.IsKeyboardFocused)
			{
				this.o.Focus();
			}
		}

				private void e(object A_0, RoutedEventArgs A_1)
		{
			this.o.Content = this.aboutPage;
			if (this.return_page != null)
			{
				return;
			}
			this.return_page = (Page)this.o.Content;
		}

				private void d(object A_0, RoutedEventArgs A_1)
		{
			this.o.Content = this.settingPage;
			if (this.return_page != null)
			{
				return;
			}
			this.return_page = (Page)this.o.Content;
		}

				private void c(object A_0, RoutedEventArgs A_1)
		{
			base.WindowState = WindowState.Minimized;
		}

				private void b(object A_0, RoutedEventArgs A_1)
		{
			System.Windows.Application.Current.Shutdown();
		}

				private void a(object A_0, RoutedEventArgs A_1)
		{
			string str = this.service_code + "_" + this.service_region;
			Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
			openFileDialog.Filter = string.Concat(new object[]
			{
                this.accountList.imaged.Content,
                "主程式|",
                this.game_exe,
                "|All files (*.*)|*.*"
            });
			openFileDialog.Title = "请选择 " + this.game_exe + " 档案";
			bool? flag = openFileDialog.ShowDialog();
			bool flag2 = true;
			if (flag.GetValueOrDefault() == flag2 & flag != null)
			{
				string fileName = openFileDialog.FileName;
				global::g.b(this.dir_value_name + "." + str, fileName);
				this.settingPage.imagee.Text = fileName;
			}
		}

				public void ddlAuthType_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			this.qrCheckLogin.IsEnabled = false;
			if (App.LoginRegion == "TW")
			{
				int selectedIndex = this.loginPage.imagec.SelectedIndex;
				if (selectedIndex == 2)
				{
					this.loginPage.imagec.IsEnabled = false;
					this.loginPage.qr.imaged.Source = this.m_b;
					this.loginPage.d.Content = this.loginPage.qr;
					this.qrWorker.RunWorkerAsync(this.loginPage != null && this.loginPage.qr != null && this.loginPage.qr.imageb != null && !this.loginPage.qr.imageb.IsChecked.Value);
				}
				else
				{
					this.loginPage.d.Content = this.loginPage.id_pass;
					if (this.loginPage.imagec.SelectedIndex == 0)
					{
						this.loginPage.id_pass.imageb.Text = "密码";
					}
					else
					{
						this.loginPage.id_pass.imageb.Text = "PIN码";
					}
				}
			}
			else
			{
				this.loginPage.d.Content = this.loginPage.id_pass;
				this.loginPage.imagec.SelectedIndex = 0;
				this.loginPage.id_pass.imageb.Text = "密码";
			}
			if (this.loginPage.imagec.SelectedIndex == 0 && (this.loginPage.id_pass.imaged.Password == "" || this.loginPage.id_pass.imaged.Password == null))
			{
				string passwordByAccount = this.accountManager.getPasswordByAccount(App.LoginRegion, this.loginPage.id_pass.imagec.Text);
				if (passwordByAccount != null && passwordByAccount != "")
				{
					this.loginPage.id_pass.imaged.Password = passwordByAccount;
					this.loginPage.id_pass.imagee.IsChecked = new bool?(true);
					this.loginPage.id_pass.imagef.IsChecked = new bool?(this.accountManager.getAutoLoginByAccount(App.LoginRegion, this.loginPage.id_pass.imagec.Text));
				}
				string verifyByAccount = this.accountManager.getVerifyByAccount(App.LoginRegion, this.loginPage.id_pass.imagec.Text);
				if (verifyByAccount != null && verifyByAccount != "")
				{
					this.verifyPage.imagea.Text = verifyByAccount;
					this.verifyPage.imageb.IsChecked = new bool?(true);
				}
				else
				{
					this.verifyPage.imagea.Text = "";
					this.verifyPage.imageb.IsChecked = new bool?(false);
				}
			}
			if (this.bfClient != null)
			{
				this.bfClient.CardID = null;
			}
		}

				public void ddlAuthTypeItemsInit()
		{
			try
			{
				this.loginPage.imagec.Items.Clear();
				if (App.LoginRegion == "TW")
				{
					foreach (string newItem in this.loginPage.item_TW)
					{
						this.loginPage.imagec.Items.Add(newItem);
					}
					//this.accountList.imagel.Visibility = Visibility.Visible;
				}
				else
				{
					foreach (string newItem2 in this.loginPage.item_HK)
					{
						this.loginPage.imagec.Items.Add(newItem2);
					}
                    this.accountList.imagel.Visibility = Visibility.Collapsed;
                }
			}
			catch
			{
			}
			try
			{
				string account = this.LastLoginAccountID;
				int selectedIndex = -1;
				string[] array = this.accountManager.getAccountList(App.LoginRegion);
				List<string> list = new List<string>();
				int num = 0;
				foreach (string item in array)
				{
					if (item == account)
					{
						selectedIndex = num;
					}
					list.Add(item);
					num++;
				}
				this.loginPage.id_pass.imagec.ItemsSource = null;
				this.loginPage.id_pass.imagec.ItemsSource = list;
				if (this.accountManager.getAccountList().Length != 0)
				{
					this.loginPage.id_pass.imageh.Visibility = Visibility.Visible;
				}
				else
				{
					this.loginPage.id_pass.imageh.Visibility = Visibility.Collapsed;
				}
				int methodByAccount = this.accountManager.getMethodByAccount(App.LoginRegion, account);
				if (methodByAccount < 0)
				{
					if (array.Length != 0)
					{
						account = array[0];
						selectedIndex = 0;
					}
					methodByAccount = this.accountManager.getMethodByAccount(App.LoginRegion, account);
				}
				if (methodByAccount > -1)
				{
					this.loginPage.id_pass.imagec.SelectedIndex = selectedIndex;
					this.loginPage.imagec.SelectedIndex = methodByAccount;
					string text = this.accountManager.getPasswordByAccount(App.LoginRegion, account);
					if (methodByAccount != 0)
					{
						text = "";
					}
					if (text == null || text == "")
					{
						this.loginPage.id_pass.imaged.Password = "";
						this.loginPage.id_pass.imagee.IsChecked = new bool?(false);
						this.loginPage.id_pass.imagef.IsChecked = new bool?(false);
					}
				}
				else
				{
					this.loginPage.id_pass.imagec.Text = "";
					this.loginPage.id_pass.imaged.Password = "";
					this.loginPage.id_pass.imagee.IsChecked = new bool?(false);
					this.loginPage.id_pass.imagef.IsChecked = new bool?(false);
					this.loginPage.imagec.SelectedIndex = 0;
					this.verifyPage.imagea.Text = "";
					this.verifyPage.imageb.IsChecked = new bool?(false);
				}
			}
			catch
			{
			}
			this.manageAccPage.setupAccList(this);
		}

				public void do_Login()
		{
			this.o.Content = this.loginWaitPage;
			if (this.pingWorker.IsBusy)
			{
				this.pingWorker.CancelAsync();
			}
			this.loginWorker.RunWorkerAsync(this.loginPage.imagec.SelectedIndex);
		}

				public bool errexit(string msg, int method, string title = null)
		{
			uint num = global::m.a(msg);
			if (num <= 2152076470U)
			{
				if (num <= 850990848U)
				{
					if (num <= 202559517U)
					{
						if (num <= 94180174U)
						{
							if (num != 77648445U)
							{
								if (num != 94180174U)
								{
									goto IL_835;
								}
								if (!(msg == "LoginPlaySafeResultError"))
								{
									goto IL_835;
								}
								msg = "PlaySafe反馈信息出錯。";
								goto IL_835;
							}
							else if (!(msg == "LoginUpdateAccountListErr"))
							{
								goto IL_835;
							}
						}
						else if (num != 133404986U)
						{
							if (num != 190317005U)
							{
								if (num != 202559517U)
								{
									goto IL_835;
								}
								if (!(msg == "LoginNoOTP1"))
								{
									goto IL_835;
								}
								msg = "获取OTP1失败。";
								method = 0;
								goto IL_835;
							}
							else
							{
								if (!(msg == "MainAccount_Not_Exist"))
								{
									goto IL_835;
								}
								msg = "此Beanfun账号不存在，请确认您的Beanfun账号是否成功注册，或者确认账号所在區域为" + ((App.LoginRegion == "TW") ? "台湾" : "香港") + "的Beanfun账号。";
								goto IL_835;
							}
						}
						else
						{
							if (!(msg == "OTPNoMyAccountData"))
							{
								goto IL_835;
							}
							msg = "获取密码失败，未找到MyAccountData信息，请检查网络连接。";
							goto IL_835;
						}
					}
					else if (num <= 263842209U)
					{
						if (num != 239911073U)
						{
							if (num != 263842209U)
							{
								goto IL_835;
							}
							if (!(msg == "LoginNoReaderName"))
							{
								goto IL_835;
							}
							msg = "登录失败，找不到晶片卡或读卡器，请检查晶片卡是否插入读卡器，且读卡器運作正常。\n若还是發生此情形，请尝试重新登录。";
							goto IL_835;
						}
						else if (!(msg == "LoginNoAccountMatch"))
						{
							goto IL_835;
						}
					}
					else if (num != 543046154U)
					{
						if (num != 597255599U)
						{
							if (num != 850990848U)
							{
								goto IL_835;
							}
							if (!(msg == "LoginNoSeed"))
							{
								goto IL_835;
							}
							msg = "获取Seed失败。";
							method = 0;
							goto IL_835;
						}
						else
						{
							if (!(msg == "LoginNoSamplecaptcha"))
							{
								goto IL_835;
							}
							msg = "登录失败，未找到Samplecaptcha信息，请检查网络连接。";
							goto IL_835;
						}
					}
					else
					{
						if (!(msg == "LoginNoResponse"))
						{
							goto IL_835;
						}
						msg = "初始化失败，请检查網路连接。";
						method = 0;
						goto IL_835;
					}
				}
				else if (num <= 1437211371U)
				{
					if (num <= 994642352U)
					{
						if (num != 880043772U)
						{
							if (num != 994642352U)
							{
								goto IL_835;
							}
							if (!(msg == "LoginUnknown"))
							{
								goto IL_835;
							}
							msg = "登录失败，请稍後再試";
							method = 0;
							goto IL_835;
						}
						else
						{
							if (!(msg == "OTPNoSecretCode"))
							{
								goto IL_835;
							}
							msg = "获取密码失败，未找到SecretCode信息，请检查网络连接。";
							goto IL_835;
						}
					}
					else if (num != 1022638743U)
					{
						if (num != 1247149019U)
						{
							if (num != 1437211371U)
							{
								goto IL_835;
							}
							if (!(msg == "DecryptOTPError"))
							{
								goto IL_835;
							}
							msg = "解密密码失败。";
							goto IL_835;
						}
						else
						{
							if (!(msg == "LoginNoCardType"))
							{
								goto IL_835;
							}
							msg = "登录失败，晶片卡读取失败。";
							goto IL_835;
						}
					}
					else
					{
						if (!(msg == "LoginNoEncryptedData"))
						{
							goto IL_835;
						}
						msg = "登录失败，晶片卡读取失败。";
						goto IL_835;
					}
				}
				else if (num <= 1735783458U)
				{
					if (num != 1556141878U)
					{
						if (num != 1735783458U)
						{
							goto IL_835;
						}
						if (!(msg == "OTPNoResponse"))
						{
							goto IL_835;
						}
						msg = "获取密码時初始化失败，请检查网络连接。";
						goto IL_835;
					}
					else
					{
						if (!(msg == "LoginJsonParseFailed"))
						{
							goto IL_835;
						}
						msg = "侦测登录结果失败，未找到返回的Json信息。";
						goto IL_835;
					}
				}
				else if (num != 2001564402U)
				{
					if (num != 2089856110U)
					{
						if (num != 2152076470U)
						{
							goto IL_835;
						}
						if (!(msg == "LoginNoViewstateGenerator"))
						{
							goto IL_835;
						}
						msg = "登录失败，未找到ViewstateGenerator信息，请检查网络连接。";
						goto IL_835;
					}
					else
					{
						if (!(msg == "LoginNoCardId"))
						{
							goto IL_835;
						}
						msg = "登录失败，找不到读卡器。";
						goto IL_835;
					}
				}
				else
				{
					if (!(msg == "LoginNoWebtoken"))
					{
						goto IL_835;
					}
					msg = "登录失败，登录後无法取得bfWebToken Cookie。";
					goto IL_835;
				}
			}
			else if (num <= 3535827048U)
			{
				if (num <= 3156528516U)
				{
					if (num <= 2514855629U)
					{
						if (num != 2227711709U)
						{
							if (num != 2514855629U)
							{
								goto IL_835;
							}
							if (!(msg == "LoginNoSkey"))
							{
								goto IL_835;
							}
							msg = "获取Skey失败。";
							method = 0;
							goto IL_835;
						}
						else if (!(msg == "LoginNoProcessLoginV2JSON"))
						{
							goto IL_835;
						}
					}
					else if (num != 2725313151U)
					{
						if (num != 2998661127U)
						{
							if (num != 3156528516U)
							{
								goto IL_835;
							}
							if (!(msg == "LoginIntResultError"))
							{
								goto IL_835;
							}
							msg = "获取QRcode失败，返回的初始化信息不正确。";
							method = 0;
							goto IL_835;
						}
						else
						{
							if (!(msg == "LoginAuthErr"))
							{
								goto IL_835;
							}
							goto IL_6B2;
						}
					}
					else if (!(msg == "LoginNoAkey"))
					{
						goto IL_835;
					}
					msg = "登录失败，账号或密码错误。(" + msg + ")";
					goto IL_835;
				}
				if (num <= 3305567231U)
				{
					if (num != 3185375694U)
					{
						if (num != 3305567231U)
						{
							goto IL_835;
						}
						if (!(msg == "AKeyParseFailed"))
						{
							goto IL_835;
						}
						msg = "获取AKey失败。";
						method = 0;
						goto IL_835;
					}
					else
					{
						if (!(msg == "LoginNoOpInfo"))
						{
							goto IL_835;
						}
						msg = "登录失败，读卡器读取失败。";
						goto IL_835;
					}
				}
				else if (num != 3338178207U)
				{
					if (num != 3492429448U)
					{
						if (num != 3535827048U)
						{
							goto IL_835;
						}
						if (!(msg == "OTPUnknown"))
						{
							goto IL_835;
						}
						msg = "获取密码失败，请尝试重新登录。";
						goto IL_835;
					}
					else
					{
						if (!(msg == "authkeyParseFailed"))
						{
							goto IL_835;
						}
						msg = "获取authkey失败。";
						method = 0;
						goto IL_835;
					}
				}
				else
				{
					if (!(msg == "OTPNeedAuthAccount"))
					{
						goto IL_835;
					}
					msg = "您的账号无法启动此游戏，请与客服人員联系 (很抱歉，需先完成进阶认证，才可启动此款游戏)";
					goto IL_835;
				}
			}
			else if (num <= 3973711785U)
			{
				if (num <= 3725256740U)
				{
					if (num != 3665611404U)
					{
						if (num != 3725256740U)
						{
							goto IL_835;
						}
						if (!(msg == "LoginNoMethod"))
						{
							goto IL_835;
						}
						msg = "登录出錯，选择了不存在的登录方式。";
						goto IL_835;
					}
					else
					{
						if (!(msg == "LoginNoEventvalidation"))
						{
							goto IL_835;
						}
						msg = "登录失败，未找到Eventvalidation信息，请检查网络连接。";
						goto IL_835;
					}
				}
				else if (num != 3868894711U)
				{
					if (num != 3917986240U)
					{
						if (num != 3973711785U)
						{
							goto IL_835;
						}
						if (!(msg == "OTPNoLongPollingKey"))
						{
							goto IL_835;
						}
						if (this.accountManager.getMethodByAccount(App.LoginRegion, this.LastLoginAccountID) == 1)
						{
							msg = "密码获取失败，请检查晶片卡是否插入读卡器，且读卡器運作正常。\n若仍出現此信息，请尝试重新登录。";
							goto IL_835;
						}
						msg = "已从服务器断开，请重新登录。";
						method = 1;
						goto IL_835;
					}
					else
					{
						if (!(msg == "OTPNoCreateTime"))
						{
							goto IL_835;
						}
						msg = "获取账号创建时间失败。";
						goto IL_835;
					}
				}
				else
				{
					if (!(msg == "LoginNoSotp"))
					{
						goto IL_835;
					}
					msg = "获取Sotp失败。";
					method = 0;
					goto IL_835;
				}
			}
			else if (num <= 4005849880U)
			{
				if (num != 3998259309U)
				{
					if (num != 4005849880U)
					{
						goto IL_835;
					}
					if (!(msg == "LoginGetAccountErr"))
					{
						goto IL_835;
					}
				}
				else
				{
					if (!(msg == "LoginNoHash"))
					{
						goto IL_835;
					}
					msg = "获取QRcode失败。";
					method = 0;
					goto IL_835;
				}
			}
			else if (num != 4007013441U)
			{
				if (num != 4089830030U)
				{
					if (num != 4151426667U)
					{
						goto IL_835;
					}
					if (!(msg == "BFServiceXNotFound"))
					{
						goto IL_835;
					}
					if (System.Windows.MessageBox.Show("调用或初始化BFService元件失败，有如下可能：\n1.未安裝BFService元件\n2.BFService元件默认会安裝到「文档」资料夹，请确认真实路径是否为与当前语言的字元集支持的文字並且能正常访问\n\n是否前往下载元件？", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						Process.Start("http://hk.download.beanfun.com/beanfun20/beanfun_2_0_93_170_hk.exe");
					}
					this.o.Content = this.loginPage;
					return false;
				}
				else
				{
					if (!(msg == "LoginNoPSDriver"))
					{
						goto IL_835;
					}
					if (System.Windows.MessageBox.Show("PlaySafe驱动初始化失败。可能你未安裝PlaySafe元件，是否前往下载？", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
					{
						Process.Start("https://tw.playsafecard.gamania.com/Drivers/PLAYSAFECard.exe");
					}
					this.o.Content = this.loginPage;
					return false;
				}
			}
			else
			{
				if (!(msg == "LoginNoViewstate"))
				{
					goto IL_835;
				}
				msg = "登录失败，未找到Viewstate信息，请检查网络连接。";
				goto IL_835;
			}
			IL_6B2:
			msg = "登录失败，无法取得账号列表。(" + msg + ")";
			IL_835:
			System.Windows.MessageBox.Show(msg, title);
			if (method == 0)
			{
				System.Windows.Application.Current.Shutdown();
			}
			else if (method == 1)
			{
				this.ddlAuthType_SelectionChanged(null, null);
				this.accountList.imageac.Text = "";
				this.o.Content = this.loginPage;
			}
			return false;
		}

				private void e(object A_0, DoWorkEventArgs A_1)
		{
			while (pingWorker.IsBusy)
			{
				Thread.Sleep(137);
			}
			Thread.CurrentThread.Name = "Login Worker";
			A_1.Result = "";
			try
			{
				loginPage.Dispatcher.Invoke(delegate
				{
					if (loginPage.imagec.SelectedIndex != 2)
					{
						bfClient = new BeanfunClient();
					}
					bfClient.Login(loginPage.id_pass.imagec.Text, loginPage.id_pass.imaged.Password, loginPage.imagec.SelectedIndex, qrcodeClass, service_code, service_region);
				});
				if (bfClient.errmsg != null)
				{
					A_1.Result = bfClient.errmsg;
				}
				else
				{
					A_1.Result = null;
				}
			}
			catch (Exception ex)
			{
				A_1.Result = "登登录失败，未知错误。\n\n" + ex.Message + "\n" + ex.StackTrace;
			}
		}


				private void ea(object A_0, RunWorkerCompletedEventArgs A_1)
		{
			if (!this.pingWorker.IsBusy)
			{
				this.pingWorker.RunWorkerAsync();
			}
			if (A_1 != null && A_1.Error != null)
			{
				this.errexit(A_1.Error.Message, 1, null);
				this.o.Content = this.loginPage;
				return;
			}
			if (A_1 != null && (string)A_1.Result != null)
			{
				if ((string)A_1.Result == "LoginAdvanceCheck")
				{
					System.Windows.MessageBox.Show("为确保您的账号安全，需请您协助进行资料验证，以利保障您的权益。");
					this.o.Content = this.verifyPage;
					if (this.verifyPage.imageb.IsChecked.Value)
					{
						this.verifyPage.imaged.Focus();
					}
					else
					{
						this.verifyPage.imagea.Focus();
					}
					this.verifyPage.imagea.Text = this.accountManager.getVerifyByAccount(App.LoginRegion, this.loginPage.id_pass.imagec.Text);
					this.verifyPage.imageb.IsChecked = new bool?(this.verifyPage.imagea.Text != null && this.verifyPage.imagea.Text != "");
					this.verifyPage.imaged.Text = "";
					string verifyPageInfo = this.bfClient.getVerifyPageInfo();
					if (verifyPageInfo == null)
					{
						System.Windows.MessageBox.Show(this.bfClient.errmsg);
						this.o.Content = this.loginPage;
					}
					string text = this.a(verifyPageInfo);
					if (text != null)
					{
						System.Windows.MessageBox.Show(text);
						this.o.Content = this.loginPage;
						return;
					}
				}
				else if (((string)A_1.Result).StartsWith("bfAPPAutoLogin.ashx"))
				{
					string[] array = Regex.Split((string)A_1.Result, "\",\"");
					if (array.Length < 2)
					{
						this.errexit("LoginUnknown", 1, null);
						return;
					}
					this.loginWaitPage.imagea.Content = "此账号需透过beanfun! 游戏授权登录。\r\n请使用beanfun! 游戏授权登录。";
					this.bfAPPAutoLogin.IsEnabled = true;
					global::e.a(new string[]
					{
						array[1]
					});
					return;
				}
				else
				{
					this.errexit((string)A_1.Result, 1, null);
				}
				return;
			}
			global::g.b("loginMethod", this.loginPage.imagec.SelectedIndex.ToString());
			if (App.LoginRegion != "TW" || this.loginPage.imagec.SelectedIndex != 2)
			{
				this.LastLoginAccountID = this.loginPage.id_pass.imagec.Text;
				global::g.b("AccountID", this.LastLoginAccountID);
				this.accountManager.addAccount(App.LoginRegion, this.loginPage.id_pass.imagec.Text, (this.loginPage.id_pass.imagee.IsEnabled && this.loginPage.id_pass.imagee.IsChecked.Value) ? this.loginPage.id_pass.imaged.Password : "", this.verifyPage.imageb.IsChecked.Value ? this.verifyPage.imagea.Text : "", this.loginPage.imagec.SelectedIndex, this.loginPage.id_pass.imagef.IsChecked.Value);
				this.ddlAuthTypeItemsInit();
			}
			else
			{
				global::g.b("AccountID", null);
			}
			try
			{
				this.o.Content = this.accountList;
				this.c();
				this.updateRemainPoint(this.bfClient.remainPoint);
				this.accountList.imageo.Focus();
				if (this.settingPage.imagec.IsChecked.Value && this.bfClient.accountList.Count<BeanfunClient.ServiceAccount>() > 0)
				{
					if ((this.settingPage.imagef.IsChecked.Value && this.login_action_type == -1) || this.login_action_type == 0)
					{
						this.runGame(null, null);
					}
					if (this.pingWorker.IsBusy)
					{
						this.pingWorker.CancelAsync();
					}
					if (App.MainWnd.login_action_type != 0)
					{
						this.accountList.btnGetOtp_Click(null, null);
					}
				}
				if (!this.pingWorker.IsBusy)
				{
					this.pingWorker.RunWorkerAsync();
				}
			}
			catch
			{
				this.errexit("登录失败，无法取得账号列表。", 1, null);
			}
		}

				private void c()
		{
			if (this.bfClient.accountAmountLimitNotice != "")
			{
				this.accountList.imagex.Content = this.bfClient.accountAmountLimitNotice;
				this.accountList.w.Visibility = Visibility.Visible;
				int num;
				try
				{
					num = int.Parse(this.bfClient.accountAmountLimitNotice.Substring(this.bfClient.accountAmountLimitNotice.Length - 1, 1));
				}
				catch
				{
					num = -1;
				}
				if (num == -1)
				{
					this.accountList.imagey.Content = "前往认证";
					this.accountList.imagey.IsEnabled = true;
					this.accountList.imagey.Visibility = Visibility.Visible;
				}
				else
				{
					this.accountList.imagey.Content = "新增账号";
					this.accountList.imagey.IsEnabled = (this.bfClient.accountList.Count < num);
					this.accountList.imagey.Visibility = ((this.bfClient.accountList.Count < num) ? Visibility.Visible : Visibility.Hidden);
				}
			}
			else
			{
				this.accountList.imagex.Content = "";
				this.accountList.w.Visibility = Visibility.Collapsed;
			}
			this.accountList.imageo.ItemsSource = null;
			this.accountList.imageo.ItemsSource = this.bfClient.accountList;
			string text = this.service_code + "_" + this.service_region;
			Visibility visibility = (App.LoginRegion == "TW") ? Visibility.Visible : Visibility.Collapsed;
			if (this.accountList.imageo.Items.Count > 0)
			{
				this.accountList.imageo.SelectedIndex = 0;
				this.accountList.imageq.Visibility = Visibility.Visible;
				this.accountList.r.Visibility = ((!this.UnconnectedGame || App.LoginRegion != "TW") ? Visibility.Visible : Visibility.Collapsed);
				this.accountList.s.Visibility = (this.UnconnectedGame ? Visibility.Visible : Visibility.Collapsed);
				this.accountList.t.Visibility = ((!this.UnconnectedGame || App.LoginRegion != "TW") ? Visibility.Visible : Visibility.Collapsed);
				this.accountList.u.Visibility = Visibility.Visible;
			}
			else
			{
				this.accountList.imageq.Visibility = Visibility.Collapsed;
				this.accountList.r.Visibility = Visibility.Collapsed;
				this.accountList.s.Visibility = Visibility.Collapsed;
				this.accountList.t.Visibility = Visibility.Collapsed;
				this.accountList.u.Visibility = Visibility.Collapsed;
			}
			this.accountList.v.Visibility = visibility;
			if (text == "610074_T9" || text == "610096_TE")
			{
				this.accountList.imageg.Visibility = Visibility.Visible;
				return;
			}
			this.accountList.imageg.Visibility = Visibility.Collapsed;
		}

				public void updateRemainPoint(int remainPoint)
		{
			this.accountList.imagei.Header = string.Format("乐豆: {0}{1} 点", remainPoint, (App.LoginRegion == "TW" || remainPoint == 0) ? "" : string.Format(" (游戏內 {0})", Math.Floor((double)remainPoint / 2.5)));
		}

				public void runGame(string account = null, string password = null)
		{
			_=service_code + "_" + this.service_region;
			string text = this.settingPage.imagee.Text;
			if (text == "" || !File.Exists(text))
			{
				if (System.Windows.MessageBox.Show("无法正确侦测游戏安裝狀态。请按一下<是>來重新侦测。若未安裝游戏，请按一下<否>开始下载。", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes || this.SelectedGame == null)
				{
					this.a(null, null);
					return;
				}
				Process.Start(this.SelectedGame.download_url);
				return;
			}
			else
			{
				text = this.settingPage.imagee.Text;
				if (text == "" || !File.Exists(text))
				{
					return;
				}
				bool flag = false;
				Regex regex = new Regex("(.*).exe");
				string processName = "";
				if (regex.IsMatch(this.game_exe))
				{
					processName = regex.Match(this.game_exe).Groups[1].Value;
				}
				if (processName != "")
				{
					foreach (Process process in Process.GetProcessesByName(processName))
					{
						try
						{
							if (process.MainModule.FileName == text)
							{
								flag = true;
								break;
							}
						}
						catch
						{
						}
					}
				}
				if (flag && System.Windows.MessageBox.Show("游戏已经运行,可能是客户端问题导致未完全结束程序,是否要结束游戏?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					foreach (Process process2 in Process.GetProcessesByName(processName))
					{
						try
						{
							if (process2.MainModule.FileName == text)
							{
								process2.Kill();
							}
						}
						catch
						{
						}
					}
				}
				try
				{
					int num = int.Parse(global::g.a("startGameMode", "0"));
					if (num == 0)
					{
						string text2 = global::i.a();
						if (text2 == "zh-Hant" || text2 == "zh-CHT" || text2 == "zh-TW" || text2 == "zh-HK" || text2 == "zh-MO")
						{
							num = 1;
						}
						else
						{
							num = 2;
						}
					}
					if (num > 2)
					{
						num = 2;
					}
					string text3 = "";
					if (account != null && password != null && account != "" && password != "" && this.game_commandLine != "")
					{
						text3 = this.game_commandLine;
						Regex regex2 = new Regex("%s");
						text3 = regex2.Replace(text3, account, 1);
						text3 = regex2.Replace(text3, password, 1);
					}
					if (num != 1)
					{
						if (num == 2)
						{
							OperatingSystem osversion = Environment.OSVersion;
							if (osversion.Platform == PlatformID.Win32NT && osversion.Version.Major < 6 && num != 1)
							{
								this.errexit("以非繁体语言系统启动游戏的方式不支持Windows XP。", 2, null);
							}
							else
							{
								this.b(text, text3);
							}
						}
					}
					else
					{
						Process.Start(new ProcessStartInfo(text)
						{
							WorkingDirectory = System.IO.Path.GetDirectoryName(text),
							Arguments = text3
						});
					}
				}
				catch
				{
					this.errexit("启动失败，请尝试从桌面捷径直接启动游戏。若您系统为非繁体语言系统，可能是Locale Emulator元件不支持您的系统。", 2, null);
				}
				return;
			}
		}

				private void a(byte[] A_0, string A_1)
		{
			if (!File.Exists(A_1))
			{
				FileStream fileStream = new FileStream(A_1, FileMode.CreateNew);
				fileStream.Write(A_0, 0, A_0.Length);
				fileStream.Close();
			}
		}

				private void b(string A_0, string A_1)
		{
			if (!File.Exists(string.Format("{0}\\LocaleEmulator.dll", Environment.CurrentDirectory)) || !File.Exists(string.Format("{0}\\LoaderDll.dll", Environment.CurrentDirectory)))
			{
				this.a(Beanfun.Properties.Resources.LocaleEmulator, string.Format("{0}\\LocaleEmulator.dll", Environment.CurrentDirectory));
				this.a(Beanfun.Properties.Resources.LoaderDll, string.Format("{0}\\LoaderDll.dll", Environment.CurrentDirectory));
			}
			int a_ = 136;
			string text = string.Empty;
			text = (A_0.StartsWith("\"") ? (A_0 + " ") : ("\"" + A_0 + "\" "));
			text += A_1;
			TextInfo textInfo = CultureInfo.GetCultureInfo("zh-HK").TextInfo;
			c obj = new c();
			obj.b(A_0);
			obj.cc(text);
			obj.cd(System.IO.Path.GetDirectoryName(A_0));
			obj.a((uint)textInfo.ANSICodePage);
			obj.b((uint)textInfo.OEMCodePage);
			obj.cc((uint)textInfo.LCID);
			obj.cd((uint)a_);
			obj.e(0u);
			obj.e("China Standard Time");
			obj.a(0);
			obj.a(A_0: false);
			uint num = obj.l();
			if (num != 0)
				
			{
				this.errexit(string.Concat(new string[]
				{
					"非繁体语言系统启动游戏失败\r\n错误码: ",
					Convert.ToString((long)((ulong)num), 16).ToUpper(),
					"\r\n",
					string.Format(string.Format("{0} {1}", Environment.OSVersion, MainWindow.Is64BitOS() ? "x64" : "x86"), Environment.OSVersion, MainWindow.Is64BitOS() ? "x64" : "x86"),
					"\r\n",
					MainWindow.b(),
					"\r\n如果你有运行任何防毒软件, 请关闭後再次尝试。\r\n如果仍然显示此窗口, 请尝试以「安全模式」启动程式。如果你进行了以上的尝试仍然沒有一個有效，请隨時在後面的連结提交问题\r\nhttps://github.com/xupefei/Locale-Emulator/issues\r\n\r\n\r\n你可以按 CTRL+C 將此信息複製到你的剪貼板。\r\n"
				}), 2, null);
			}
		}

				public static bool Is64BitOS()
		{
			bool flag;
			return IntPtr.Size == 8 || (MainWindow.a("kernel32.dll", "IsWow64Process") && global::i.IsWow64Process(global::i.GetCurrentProcess(), out flag) && flag);
		}

				private static bool a(string A_0, string A_1)
		{
			IntPtr moduleHandle = global::i.GetModuleHandle(A_0);
			return !(moduleHandle == IntPtr.Zero) && global::i.GetProcAddress(moduleHandle, A_1) != IntPtr.Zero;
		}

				private static string b()
		{
			string[] array = new string[]
			{
				"NTDLL.DLL",
				"KERNELBASE.DLL",
				"KERNEL32.DLL",
				"USER32.DLL",
				"GDI32.DLL"
			};
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in array)
			{
				FileVersionInfo versionInfo = FileVersionInfo.GetVersionInfo(System.IO.Path.Combine(System.IO.Path.GetPathRoot(Environment.SystemDirectory), MainWindow.Is64BitOS() ? "Windows\\SysWOW64\\" : "Windows\\System32\\", text));
				stringBuilder.Append(text);
				stringBuilder.Append(": ");
				stringBuilder.Append(string.Format("{0}.{1}.{2}.{3}", new object[]
				{
					versionInfo.FileMajorPart,
					versionInfo.FileMinorPart,
					versionInfo.FileBuildPart,
					versionInfo.FilePrivatePart
				}));
				stringBuilder.Append("\r\n");
			}
			return stringBuilder.ToString();
		}

				public bool AddServiceAccount(string name)
		{
			if (this.bfClient == null)
			{
				return false;
			}
			if (name == null || name == "")
			{
				return false;
			}
			if (this.bfClient.AddServiceAccount(name, this.service_code, this.service_region))
			{
				if (App.LoginRegion == "TW")
				{
					this.bfClient.GetAccounts(this.service_code, this.service_region, true);
				}
				else
				{
					this.bfClient.GetAccounts_HK(this.service_code, this.service_region, true);
				}
				int selectedIndex = this.accountList.imageo.SelectedIndex;
				this.c();
				this.accountList.imageo.SelectedIndex = selectedIndex;
				return true;
			}
			return false;
		}

				public string UnconnectedGame_AddAccount(string name, string txtNewPwd, string txtNewPwd2, string txtServiceAccountDN, NameValueCollection payload)
		{
			if (this.bfClient == null)
			{
				return null;
			}
			if (name == null || name == "")
			{
				return null;
			}
			if (txtNewPwd == null || txtNewPwd == "")
			{
				return null;
			}
			if (txtNewPwd2 == null || txtNewPwd2 == "")
			{
				return null;
			}
			string result = this.bfClient.UnconnectedGame_AddAccount(this.service_code, this.service_region, name, txtNewPwd, txtNewPwd2, txtServiceAccountDN, payload);
			if (result == "")
			{
				if (App.LoginRegion == "TW")
				{
					this.bfClient.GetAccounts(this.service_code, this.service_region, true);
				}
				else
				{
					this.bfClient.GetAccounts_HK(this.service_code, this.service_region, true);
				}
				int selectedIndex = this.accountList.imageo.SelectedIndex;
				this.c();
				this.accountList.imageo.SelectedIndex = selectedIndex;
			}
			return result;
		}

				public string UnconnectedGame_ChangePassword(string txtEmail)
		{
			if (this.bfClient == null)
			{
				return null;
			}
			if (txtEmail == null)
			{
				return null;
			}
			return this.bfClient.UnconnectedGame_ChangePassword(this.service_code, this.service_region, this.accountList.imageo.SelectedIndex, txtEmail);
		}

				public NameValueCollection UnconnectedGame_AddAccountInit()
		{
			if (this.bfClient == null)
			{
				return null;
			}
			return this.bfClient.UnconnectedGame_InitAddAccountPayload(this.service_code, this.service_region);
		}

				public NameValueCollection UnconnectedGame_AddUnconnectedCheck(string name, string txtServiceAccountDN, NameValueCollection payload)
		{
			if (this.bfClient == null)
			{
				return null;
			}
			return this.bfClient.UnconnectedGame_AddAccountCheck(this.service_code, this.service_region, name, txtServiceAccountDN, payload);
		}

				public NameValueCollection UnconnectedGame_AddAccountCheckNickName(string txtServiceAccountDN, NameValueCollection payload)
		{
			if (this.bfClient == null)
			{
				return null;
			}
			return this.bfClient.UnconnectedGame_AddAccountCheckNickName(this.service_code, this.service_region, txtServiceAccountDN, payload);
		}

				public bool ChangeServiceAccountDisplayName(string newName)
		{
			if (this.bfClient == null)
			{
				return false;
			}
			BeanfunClient.ServiceAccount serviceAccount = (BeanfunClient.ServiceAccount)this.accountList.imageo.SelectedItem;
			if (newName == null || newName == "" || serviceAccount == null)
			{
				return false;
			}
			if (newName == serviceAccount.sname)
			{
				return true;
			}
			string gameCode = this.service_code + "_" + this.service_region;
			if (this.bfClient.ChangeServiceAccountDisplayName(newName, gameCode, serviceAccount))
			{
				serviceAccount.sname = newName;
				int selectedIndex = this.accountList.imageo.SelectedIndex;
				this.c();
				this.accountList.imageo.SelectedIndex = selectedIndex;
				return true;
			}
			return false;
		}

				public string GetServiceContract()
		{
			if (this.bfClient == null)
			{
				return "";
			}
			return this.bfClient.GetServiceContract(this.service_code, this.service_region);
		}

				private void d(object A_0, DoWorkEventArgs A_1)
		{
			while (this.pingWorker.IsBusy)
			{
				Thread.Sleep(133);
			}
			Thread.CurrentThread.Name = "GetOTP Worker";
			int num = (int)A_1.Argument;
			if (this.bfClient.accountList.Count <= num)
			{
				return;
			}
			this.m_a = this.bfClient.GetOTP(this.bfClient.accountList[num], this.service_code, this.service_region);
			if (this.m_a == null)
			{
				A_1.Result = -1;
				return;
			}
			A_1.Result = num;
		}

				private void d(object A_0, RunWorkerCompletedEventArgs A_1)
		{
			if (A_1.Error != null)
			{
				this.accountList.imageac.Text = "获取失败";
				this.errexit(A_1.Error.Message, 2, null);
			}
			else
			{
				int num = (int)A_1.Result;
				if (num == -1)
				{
					this.accountList.imageac.Text = "获取失败";
					this.errexit(this.bfClient.errmsg, 2, null);
				}
				else
				{
					int selectedIndex = this.accountList.imageo.SelectedIndex;
					string sid = this.bfClient.accountList[num].sid;
					this.accountList.imageac.Text = this.m_a;
					if ((!this.settingPage.imagef.IsChecked.Value && this.login_action_type == -1) || this.login_action_type == 1)
					{
						this.runGame(sid, this.accountList.imageac.Text);
					}
					else
					{
						IntPtr foregroundWindow;
						if (this.accountList.imageab.IsChecked.Value && this.accountList.imageab.Visibility == Visibility.Visible && (foregroundWindow = global::i.FindWindow(this.win_class_name, this.game_exe.Split(new char[]
						{
							'.'
						})[0])) != IntPtr.Zero)
						{
							try
							{
								object dataObject = System.Windows.Clipboard.GetDataObject();
								System.Windows.Clipboard.SetText(sid);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(17, 157, 1, 0);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(86, 158, 0, 0);
								Thread.Sleep(100);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(86, 158, 2, 0);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(17, 157, 3, 0);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(9, 0, 1, 0);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(9, 0, 2, 0);
								Thread.Sleep(250);
								System.Windows.Clipboard.SetText(this.accountList.imageac.Text);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(17, 157, 1, 0);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(86, 158, 0, 0);
								Thread.Sleep(100);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(86, 158, 2, 0);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(17, 157, 3, 0);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(13, 0, 0, 0);
								global::i.SetForegroundWindow(foregroundWindow);
								Thread.Sleep(10);
								global::i.keybd_event(13, 0, 2, 0);
								System.Windows.Clipboard.SetDataObject(dataObject);
								goto IL_2FD;
							}
							catch
							{
								goto IL_2FD;
							}
						}
						try
						{
							System.Windows.Clipboard.SetText(this.accountList.imageac.Text);
						}
						catch
						{
						}
					}
				}
			}
			IL_2FD:
			this.accountList.imageo.IsEnabled = true;
			this.accountList.imageaa.IsEnabled = true;
			this.accountList.imagef.IsEnabled = true;
			this.accountList.imageb.IsEnabled = true;
			this.accountList.imaged.IsEnabled = true;
			this.accountList.imagee.IsEnabled = true;
			this.accountList.imageh.IsEnabled = true;
			if (this.bfClient.accountAmountLimitNotice != "")
			{
				this.accountList.imagey.IsEnabled = (this.bfClient.accountList.Count < int.Parse(this.bfClient.accountAmountLimitNotice.Substring(this.bfClient.accountAmountLimitNotice.Length - 1, 1)));
			}
			if (!this.pingWorker.IsBusy)
			{
				this.pingWorker.RunWorkerAsync();
			}
		}

				private void c(object A_0, DoWorkEventArgs A_1)
		{
			Thread.CurrentThread.Name = "ping Worker";
			while (!this.pingWorker.CancellationPending)
			{
				if (this.getOtpWorker.IsBusy || this.loginWorker.IsBusy)
				{
					Thread.Sleep(1000);
				}
				else
				{
					if (this.bfClient != null)
					{
						this.bfClient.Ping();
					}
					for (int i = 0; i < 60; i++)
					{
						if (this.pingWorker.CancellationPending)
						{
							break;
						}
						Thread.Sleep(1000);
					}
				}
			}
		}

				private void c(object A_0, RunWorkerCompletedEventArgs A_1)
		{
		}

				private void b(object A_0, DoWorkEventArgs A_1)
		{
			this.bfClient = new BeanfunClient();
			string sessionkey = this.bfClient.GetSessionkey();
			this.qrcodeClass = this.bfClient.GetQRCodeValue(sessionkey, (bool)A_1.Argument);
		}

				private void b(object A_0, RunWorkerCompletedEventArgs A_1)
		{
			this.loginPage.imagec.IsEnabled = true;
			if (this.updateQRCodeImage())
			{
				this.qrCheckLogin.IsEnabled = true;
			}
		}

				private void d(object A_0, EventArgs A_1)
		{
			if (this.qrcodeClass == null)
			{
				System.Windows.MessageBox.Show("QRCode not get yet");
				return;
			}
			int num = this.bfClient.QRCodeCheckLoginStatus(this.qrcodeClass);
			if (num != 0)
			{
				this.qrCheckLogin.IsEnabled = false;
			}
			if (num == 1)
			{
				this.do_Login();
			}
			if (num == -2)
			{
				this.refreshQRCode();
			}
		}

				public void refreshQRCode()
		{
			this.qrWorker.RunWorkerAsync(this.loginPage != null && this.loginPage.qr != null && this.loginPage.qr.imageb != null && !this.loginPage.qr.imageb.IsChecked.Value);
		}

				public bool updateQRCodeImage()
		{
			this.loginPage.qr.imagec.IsEnabled = false;
			BitmapImage qrcodeImage;
			bool result;
			if (this.qrcodeClass == null || (qrcodeImage = this.bfClient.getQRCodeImage(this.qrcodeClass)) == null)
			{
				result = false;
				this.loginPage.qr.imaged.Source = this.m_b;
			}
			else
			{
				result = true;
				this.loginPage.qr.imaged.Source = qrcodeImage;
			}
			this.loginPage.qr.imagec.IsEnabled = true;
			return result;
		}

				private void c(object A_0, EventArgs A_1)
		{
			JObject jobject = this.bfClient.CheckIsRegisteDevice(this.service_code, this.service_region);
			if (jobject == null || jobject["IntResult"] == null)
			{
				return;
			}
			if ((string)jobject["IntResult"] != "1" && (string)jobject["IntResult"] != "0")
			{
				this.bfAPPAutoLogin.IsEnabled = false;
			}
			string text = (string)jobject["IntResult"];
			if (!(text == "-3"))
			{
				if (!(text == "-2"))
				{
					if (!(text == "-1"))
					{
						if (text == "0")
						{
							return;
						}
						if (text == "1")
						{
							return;
						}
						if (text == "2")
						{
							this.ea(null, null);
						}
					}
					else
					{
						this.errexit((string)jobject["StrReslut"], 1, null);
					}
				}
				else
				{
					this.o.Content = this.loginPage;
				}
			}
			else
			{
				this.errexit("您的登录要求已被beanfun! App拒絕。", 1, null);
			}
			this.loginWaitPage.imagea.Content = "正在登录,请稍等...";
		}

				private void b(object A_0, EventArgs A_1)
		{
			try
			{
				IntPtr a_;
				if ((a_ = global::i.FindWindow("StartUpDlgClass", "MapleStory")) != IntPtr.Zero)
				{
					global::i.PostMessage(a_, 16U, 0, 0);
				}
			}
			catch
			{
			}
		}

				private void a(object A_0, EventArgs A_1)
		{
			if (this.settingPage == null || this.settingPage.imagee == null)
			{
				return;
			}
			bool flag = false;
			try
			{
				string text = System.IO.Path.GetDirectoryName(this.settingPage.imagee.Text) + "\\Patcher.exe";
				foreach (Process process in Process.GetProcessesByName("Patcher"))
				{
					try
					{
						if (process.MainModule.FileName == text)
						{
							process.Kill();
							flag = true;
						}
					}
					catch
					{
					}
				}
			}
			catch
			{
			}
			if (flag)
			{
				System.Windows.MessageBox.Show("游戏自动更新有可能会造成游戏程式損毀，已被阻止。建议下载手動更新档來更新，如需要使用自动更新功能请到設定頁面取消阻止。");
			}
		}

				private void a(object A_0, DoWorkEventArgs A_1)
		{
			MainWindow.A a = new MainWindow.A();
			a.w_b = this;
			A_1.Result = null;
			a.s_a = "";
			this.verifyPage.Dispatcher.Invoke(new Action(a.ab));
			Regex regex = new Regex("alert\\('(.*)'\\);");
			string text = null;
			if (regex.IsMatch(a.s_a))
			{
				text = regex.Match(a.s_a).Groups[1].Value;
			}
			if (text == null)
			{
				if (a.s_a.Contains("圖形验证码输入错误"))
				{
					System.Windows.MessageBox.Show("圖形验证码输入错误");
				}
				else
				{
					System.Windows.MessageBox.Show("资料错误，请重新输入");
				}
			}
			else
			{
				System.Windows.MessageBox.Show(text.Replace("\\n", "\n").Replace("\\r", "\r"));
				if (text.Contains("资料已验证成功"))
				{
					A_1.Result = true;
				}
			}
			if (A_1.Result == null)
			{
				MainWindow.B b = new MainWindow.B();
				b.Ab = a;
				b.s_a = "Error Load Verify Page";
				this.verifyPage.Dispatcher.Invoke(new Action(b.a));
				if (b.s_a != null)
				{
					System.Windows.MessageBox.Show(b.s_a);
				}
			}
		}

				private void a(object A_0, RunWorkerCompletedEventArgs A_1)
		{
			if (A_1.Result != null)
			{
				this.do_Login();
			}
		}

				[CompilerGenerated]
		private void a(object A_0, System.Windows.Forms.MouseEventArgs A_1)
		{
			if (A_1.Button == MouseButtons.Left)
			{
				if (base.Visibility == Visibility.Visible)
				{
					base.Visibility = Visibility.Hidden;
				}
				else
				{
					base.Visibility = Visibility.Visible;
					base.Activate();
				}
				MainWindow.m_c.Visible = false;
			}
		}

				[CompilerGenerated]
		private void a()
		{
			if (this.loginPage.imagec.SelectedIndex != 2)
			{
				this.bfClient = new BeanfunClient();
			}
			this.bfClient.Login(this.loginPage.id_pass.imagec.Text, this.loginPage.id_pass.imaged.Password, this.loginPage.imagec.SelectedIndex, this.qrcodeClass, this.service_code, this.service_region);
		}

        private void a(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

      
    }
}
