using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;

namespace Beanfun
{
    public partial class AccountList : Page, IComponentConnector
    {
        public AccountList()
        {
            this.InitializeComponent();
            this.imageab.IsChecked = new bool?(bool.Parse(global::g.a("autoPaste", "false")));
        }

        private void q(object A_0, RoutedEventArgs A_1)
        {
            if (App.MainWnd.bfClient != null)
            {
                App.MainWnd.bfClient.Logout();
            }
            if (App.MainWnd.loginPage.imagec.SelectedIndex == 2)
            {
                App.MainWnd.ddlAuthType_SelectionChanged(null, null);
            }
            App.MainWnd.o.Content = App.MainWnd.loginPage;
        }

        private void p(object A_0, RoutedEventArgs A_1)
        {
            BeanfunClient.ServiceAccount serviceAccount = (BeanfunClient.ServiceAccount)this.imageo.SelectedItem;
            if (serviceAccount == null)
            {
                return;
            }
            try
            {
                Clipboard.SetText(serviceAccount.sid);
            }
            catch
            {
            }
        }

        private void o(object A_0, RoutedEventArgs A_1)
        {
            if ((App.MainWnd.settingPage.imagef.IsChecked.Value && App.MainWnd.login_action_type == -1) || App.MainWnd.login_action_type == 0)
            {
                this.imagee.IsEnabled = false;
                App.MainWnd.runGame(null, null);
                this.imagee.IsEnabled = true;
                return;
            }
            this.btnGetOtp_Click(null, null);
        }

        private void n(object A_0, RoutedEventArgs A_1)
        {
            bool flag = bool.Parse(global::g.a("autoPaste", "false"));
            bool? isChecked = this.imageab.IsChecked;
            if (flag == isChecked.GetValueOrDefault() & isChecked != null)
            {
                return;
            }
            if (global::g.a("autoPaste", "") == "")
            {
                MessageBox.Show("自动输入需要满足以下条件才能正常使用:\r\n1.游戏需要在输入账密界面\r\n2.游戏沒有选中记住账号\r\n3.游戏账号密码输入栏为空\r\n4.输入栏激活狀态为账号栏位\r\n\r\n※ 自动输入功能可能会由于游戏限制出現偶尔无法正常进行的问题, 请斟酌使用");
            }
            global::g.b("autoPaste", Convert.ToString(this.imageab.IsChecked));
        }

        public void btnGetOtp_Click(object sender, RoutedEventArgs e)
        {
            if (App.MainWnd.pingWorker.IsBusy)
            {
                App.MainWnd.pingWorker.CancelAsync();
            }
            if (this.imageo.SelectedIndex < 0 || App.MainWnd.loginWorker.IsBusy)
            {
                MessageBox.Show("您还未选择需要启动游戏的账号。");
                return;
            }
            this.imageac.Text = "获取密码中...";
            this.imageo.IsEnabled = false;
            this.imageaa.IsEnabled = false;
            this.imagef.IsEnabled = false;
            this.imageb.IsEnabled = false;
            this.imaged.IsEnabled = false;
            this.imagee.IsEnabled = false;
            this.imagey.IsEnabled = false;
            this.imageh.IsEnabled = false;
            App.MainWnd.getOtpWorker.RunWorkerAsync(this.imageo.SelectedIndex);
        }

        private void a(object A_0, MouseButtonEventArgs A_1)
        {
            if (this.imageac.Text == "" || this.imageac.Text == "获取失败" || this.imageac.Text == "获取密码中...")
            {
                return;
            }
            try
            {
                Clipboard.SetText(this.imageac.Text);
            }
            catch
            {
            }
        }

        private void m(object A_0, RoutedEventArgs A_1)
        {
            if (!this.imagey.IsEnabled)
            {
                return;
            }
            if ((string)this.imagey.Content == "前往认证")
            {
                new WebBrowser("https://tw.beanfun.com/TW/member/verify_index.aspx").Show();
                return;
            }
            if ((App.MainWnd.service_code == "610153" && App.MainWnd.service_region == "TN") || (App.MainWnd.service_code == "610085" && App.MainWnd.service_region == "TC"))
            {
                new UnconnectedGame_AddAccount().ShowDialog();
                return;
            }
            new AddServiceAccount().ShowDialog();
        }

        private void l(object A_0, RoutedEventArgs A_1)
        {
            App.MainWnd.updateRemainPoint(App.MainWnd.bfClient.getRemainPoint());
        }

        private void k(object A_0, RoutedEventArgs A_1)
        {
            BeanfunClient.ServiceAccount serviceAccount = (BeanfunClient.ServiceAccount)this.imageo.SelectedItem;
            if (serviceAccount == null)
            {
                return;
            }
            new ChangeServiceAccountDisplayName(serviceAccount.sname).ShowDialog();
        }

        private void j(object A_0, RoutedEventArgs A_1)
        {
            string text;
            if (App.LoginRegion == "TW")
            {
                text = "https://tw.beanfun.com/TW/auth.aspx?channel=gash&page_and_query=default.aspx%3Fservice_code%3D999999%26service_region%3DT0&web_token=" + App.MainWnd.bfClient.WebToken;
                if (App.MainWnd.bfClient.CardID != null)
                {
                    text = text + "&cardid=" + App.MainWnd.bfClient.CardID;
                }
            }
            else
            {
                text = "https://hk.beanfun.com/beanfun_web_ap/auth.aspx?channel=gash&page_and_query=default.aspx%3fservice_code%3d999999%26service_region%3dT0&token=" + App.MainWnd.bfClient.BFServ.Token;
            }
            new WebBrowser(text).Show();
        }

        private void i(object A_0, RoutedEventArgs A_1)
        {
            string text;
            if (App.LoginRegion == "TW")
            {
                text = "https://tw.beanfun.com/TW/auth.aspx?channel=member&page_and_query=default.aspx%3Fservice_code%3D999999%26service_region%3DT0&web_token=" + App.MainWnd.bfClient.WebToken;
                if (App.MainWnd.bfClient.CardID != null)
                {
                    text = text + "&cardid=" + App.MainWnd.bfClient.CardID;
                }
            }
            else
            {
                text = "https://hk.beanfun.com/beanfun_web_ap/auth.aspx?channel=member&page_and_query=default.aspx%3fservice_code%3d999999%26service_region%3dT0&token=" + App.MainWnd.bfClient.BFServ.Token;
            }
            new WebBrowser(text).Show();
        }

        private void h(object A_0, RoutedEventArgs A_1)
        {
            string uri;
            if (App.LoginRegion == "TW")
            {
                uri = "https://tw.beanfun.com/customerservice/www/main.aspx";
            }
            else
            {
                uri = "http://hk.games.beanfun.com/faq/service.asp";
            }
            new WebBrowser(uri).Show();
        }

        private void g(object A_0, RoutedEventArgs A_1)
        {
            new CopyBox("认证信箱", App.MainWnd.bfClient.getEmail()).ShowDialog();
        }

        private void f(object A_0, RoutedEventArgs A_1)
        {
            BeanfunClient.ServiceAccount serviceAccount = (BeanfunClient.ServiceAccount)this.imageo.SelectedItem;
            if (serviceAccount == null)
            {
                return;
            }
            new ServiceAccountInfo(serviceAccount).ShowDialog();
        }

        private void e(object A_0, RoutedEventArgs A_1)
        {
            if (App.MainWnd != null && App.MainWnd.SelectedGame != null)
            {
                new WebBrowser(App.MainWnd.SelectedGame.website_url).Show();
            }
        }

        private void d(object A_0, RoutedEventArgs A_1)
        {
            new GameList().ShowDialog();
        }

        private void a(object A_0, DependencyPropertyChangedEventArgs A_1)
        {
            if (this.imagea.IsKeyboardFocused)
            {
                this.imageo.Focus();
            }
        }

        private void c(object A_0, RoutedEventArgs A_1)
        {
            new UnconnectedGame_ChangePassword().ShowDialog();
        }

        private void b(object A_0, RoutedEventArgs A_1)
        {
            string text = App.MainWnd.service_code + "_" + App.MainWnd.service_region;
            if (text == "610074_T9")
            {
                new MapleTools().Show();
                return;
            }
            if (!(text == "610096_TE"))
            {
                return;
            }
            new KartTools().Show();
        }

        private void a(object A_0, RoutedEventArgs A_1)
        {
            new WebBrowser("https://m.beanfun.com/Deposite").Show();
        }


    }
}
