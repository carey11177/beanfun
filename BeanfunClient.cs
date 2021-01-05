using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Media.Imaging;
using FluorineFx;
using Newtonsoft.Json.Linq;

namespace Beanfun
{
    public class BeanfunClient : WebClient
    {
        public void GetAccounts(string service_code, string service_region, bool fatal = true)
        {
            if (this.m_d == null)
            {
                return;
            }
            _ = this.m_e;
            bool flag;
            string text;
            if ((service_code == "610153" && service_region == "TN") || (service_code == "610085" && service_region == "TC"))
            {
                flag = true;
                text = "https://tw.beanfun.com/TW/auth.aspx?channel=accounts_management&page_and_query=01.aspx%3FServiceCode%3D" + service_code + "%26ServiceRegion%3D" + service_region + "&web_token=" + this.m_d;
            }
            else
            {
                flag = false;
                text = "https://tw.beanfun.com/beanfun_block/auth.aspx?channel=game_zone&page_and_query=game_start.aspx%3Fservice_code_and_region%3D" + service_code + "_" + service_region + "&web_token=" + this.m_d;
            }
            if (this.m_e != null)
            {
                text = text + "&cardid=" + this.m_e;
            }
            string input = DownloadString(text);
            if (this.m_e != null)
            {
                Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
                if (!regex.IsMatch(input))
                {
                    errmsg = "LoginNoViewstate";
                    return;
                }
                string value = regex.Match(input).Groups[1].Value;
                regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
                if (!regex.IsMatch(input))
                {
                    errmsg = "LoginNoEventvalidation";
                    return;
                }
                string value2 = regex.Match(input).Groups[1].Value;
                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection.Add("__VIEWSTATE", value);
                nameValueCollection.Add("__EVENTVALIDATION", value2);
                nameValueCollection.Add("btnCheckPLASYSAFE", "Hidden+Button");
                input = UploadString(text, nameValueCollection);
            }
            if (flag)
            {
                Regex regex = new Regex("<span id=\"lblGameMaxAccount\" style=\"display:inline-block;\">(.*)</span>");
                if (regex.IsMatch(input))
                {
                    accountAmountLimitNotice = "最多新增账号数:" + regex.Match(input).Groups[1].Value;
                }
                else
                {
                    accountAmountLimitNotice = "";
                }
                input = DownloadString("https://tw.beanfun.com/TW/accounts_management/01Accounts.aspx");
                if (input.Contains("点我前往完成进阶认证"))
                {
                    accountAmountLimitNotice = "请完成进阶认证";
                }
                regex = new Regex("<td class=\"game\" align=\"left\"><font size=\"4\"><b>(.*)</b></font></td><td class=\"game\">");
                accountList.Clear();
                foreach (Match item in regex.Matches(input))
                {
                    accountList.Add(new ServiceAccount(isEnable: true, null, null, item.Groups[1].Value, null));
                }
            }
            else
            {
                if (service_code == "600035" && service_region == "T7")
                {
                    input = DownloadString("https://tw.beanfun.com/beanfun_block/game_zone/game_server_account_list.aspx?sc=" + service_code + "&sr=" + service_region + "&ss=" + service_code + "_" + service_region);
                }
                Regex regex = new Regex("onclick=\"([^\"]*)\"><div id=\"(\\w+)\" sn=\"(\\d+)\" name=\"([^\"]+)\"");
                accountList.Clear();
                foreach (Match item2 in regex.Matches(input))
                {
                    if (!(item2.Groups[2].Value == "") && !(item2.Groups[3].Value == "") && !(item2.Groups[4].Value == ""))
                    {
                        accountList.Add(new ServiceAccount(item2.Groups[1].Value != "", item2.Groups[2].Value, item2.Groups[3].Value, WebUtility.HtmlDecode(item2.Groups[4].Value), e(service_code, service_region, item2.Groups[3].Value)));
                    }
                }
                regex = new Regex("<div id=\"divServiceAccountAmountLimitNotice\" class=\"InnerContent\">(.*)</div>");
                if (regex.IsMatch(input))
                {
                    accountAmountLimitNotice = regex.Match(input).Groups[1].Value;
                    if (accountAmountLimitNotice.Contains("进阶认证"))
                    {
                        accountAmountLimitNotice = "请完成进阶认证";
                    }
                }
                else
                {
                    accountAmountLimitNotice = "";
                }
            }
            errmsg = null;
        }

        private string e(string A_0, string A_1, string A_2)
        {
            string result;
            try
            {
                string input = this.DownloadString(string.Concat(new string[]
                {
                    "https://tw.beanfun.com/beanfun_block/game_zone/game_start_step2.aspx?service_code=",
                    A_0,
                    "&service_region=",
                    A_1,
                    "&sotp=",
                    A_2,
                    "&dt=",
                    this.a(2)
                }));
                Regex regex = new Regex("ServiceAccountCreateTime: \"([^\"]+)\"");
                if (!regex.IsMatch(input))
                {
                    result = null;
                }
                else
                {
                    result = regex.Match(input).Groups[1].Value;
                }
            }
            catch
            {
                result = null;
            }
            return result;
        }

        public void GetAccounts_HK(string service_code, string service_region, bool fatal = true)
        {
            string input = this.DownloadString("http://hk.beanfun.com/beanfun_block/game_zone/game_server_account_list.aspx?service_code=" + service_code + "&service_region=" + service_region);
            IList serviceAccounts = this.gameServAccListApp.GetServiceAccounts(service_code, service_region);
            if (serviceAccounts == null)
            {
                this.errmsg = "LoginGetAccountErr";
                return;
            }
            this.accountList.Clear();
            try
            {
                foreach (object obj in serviceAccounts)
                {
                    ASObject asobject = (ASObject)obj;
                    if (!(asobject["service_code"].ToString() != service_code) && !(asobject["service_region"].ToString() != service_region))
                    {
                        this.accountList.Add(new BeanfunClient.ServiceAccount(!bool.Parse(asobject["lock_flag"].ToString()), asobject["visible"].ToString() == "1", bool.Parse(asobject["is_inherited"].ToString()), asobject["service_account_id"].ToString(), asobject["service_account_sn"].ToString(), asobject["service_account_display_name"].ToString(), asobject["create_time"].ToString(), asobject["last_used_time"].ToString(), asobject["auth_type"].ToString()));
                    }
                }
            }
            catch
            {
                this.errmsg = "LoginUpdateAccountListErr";
                return;
            }
            Regex regex = new Regex("<service_account_amount_limit>(.*)</service_account_amount_limit>");
            if (regex.IsMatch(input))
            {
                this.accountAmountLimitNotice = "账号上限：" + regex.Match(input).Groups[1].Value;
            }
            else
            {
                this.accountAmountLimitNotice = "";
            }
            this.errmsg = null;
        }

        private NameValueCollection a(string A_0, string A_1)
        {
            string text = this.m_e;
            string input;
            if (App.LoginRegion == "TW")
            {
                string text2 = string.Concat(new string[]
                {
                    "https://tw.beanfun.com/TW/auth.aspx?channel=accounts_management&page_and_query=01.aspx%3FServiceCode%3D",
                    A_0,
                    "%26ServiceRegion%3D",
                    A_1,
                    "&web_token=",
                    this.m_d
                });
                if (this.m_e != null)
                {
                    text2 = text2 + "&cardid=" + this.m_e;
                }
                input = this.DownloadString(text2);
            }
            else
            {
                string text2 = string.Concat(new string[]
                {
                    "http://hk.beanfun.com/beanfun_web_ap/auth.aspx?channel=accounts_management&page_and_query=01.aspx%3FServiceCode%3D",
                    A_0,
                    "%26ServiceRegion%3D",
                    A_1,
                    "&token=",
                    this.m_a.Token
                });
                input = this.DownloadString(text2);
            }
            Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstate";
                return null;
            }
            string value = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__VIEWSTATEGENERATOR\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstategenerator";
                return null;
            }
            string value2 = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__PREVIOUSPAGE\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoPreviouspage";
                return null;
            }
            string value3 = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoEventvalidation";
                return null;
            }
            string value4 = regex.Match(input).Groups[1].Value;
            return new NameValueCollection
            {
                {
                    "__VIEWSTATE",
                    value
                },
                {
                    "__VIEWSTATEGENERATOR",
                    value2
                },
                {
                    "__PREVIOUSPAGE",
                    value3
                },
                {
                    "__EVENTVALIDATION",
                    value4
                }
            };
        }

        public NameValueCollection UnconnectedGame_InitAddAccountPayload(string service_code, string service_region)
        {
            NameValueCollection nameValueCollection = this.a(service_code, service_region);
            nameValueCollection.Add("__EVENTTARGET", "");
            nameValueCollection.Add("__EVENTARGUMENT", "");
            nameValueCollection.Add("imgbtn_AddAccount.x", "0");
            nameValueCollection.Add("imgbtn_AddAccount.y", "0");
            string text;
            if (App.LoginRegion == "TW")
            {
                text = this.UploadString("https://tw.beanfun.com/TW/accounts_management/02.aspx", nameValueCollection);
            }
            else
            {
                text = this.UploadString("http://hk.beanfun.com/beanfun_web_ap/accounts_management/02.aspx", nameValueCollection);
            }
            Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
            if (!regex.IsMatch(text))
            {
                this.errmsg = "LoginNoViewstate";
                return null;
            }
            string value = regex.Match(text).Groups[1].Value;
            regex = new Regex("id=\"__VIEWSTATEGENERATOR\" value=\"(.*)\" />");
            if (!regex.IsMatch(text))
            {
                this.errmsg = "LoginNoViewstategenerator";
                return null;
            }
            string value2 = regex.Match(text).Groups[1].Value;
            regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
            if (!regex.IsMatch(text))
            {
                this.errmsg = "LoginNoEventvalidation";
                return null;
            }
            string value3 = regex.Match(text).Groups[1].Value;
            nameValueCollection.Clear();
            nameValueCollection.Add("__VIEWSTATE", value);
            nameValueCollection.Add("__VIEWSTATEGENERATOR", value2);
            nameValueCollection.Add("__EVENTVALIDATION", value3);
            regex = new Regex("<span id=\"lblGameName\">(.*)</span>");
            if (!regex.IsMatch(text))
            {
                this.errmsg = "LoginNoGameName";
                return null;
            }
            nameValueCollection.Add("GameName", regex.Match(text).Groups[1].Value);
            regex = new Regex("<span id=\"lblAccountLen\">(.*)</span>");
            if (!regex.IsMatch(text))
            {
                this.errmsg = "LoginNoAccountLen";
                return null;
            }
            nameValueCollection.Add("AccountLen", regex.Match(text).Groups[1].Value);
            nameValueCollection.Add("CheckNickName", text.Contains("<a id=\"lbtnCheckNickName\"") ? "1" : "");
            return nameValueCollection;
        }

        public NameValueCollection UnconnectedGame_AddAccountCheck(string service_code, string service_region, string name, string txtServiceAccountDN, NameValueCollection payload)
        {
            if (payload == null)
            {
                return null;
            }
            payload.Add("__EVENTTARGET", "lbtnCheckAccount");
            payload.Add("__EVENTARGUMENT", "");
            payload.Add("txtServiceAccountID", name);
            if (txtServiceAccountDN != null)
            {
                if (App.LoginRegion == "TW")
                {
                    payload.Add("t1", txtServiceAccountDN);
                }
                else
                {
                    payload.Add("txtServiceAccountDN", txtServiceAccountDN);
                }
            }
            payload.Add("txtNewPwd", "");
            payload.Add("txtNewPwd2", "");
            string input;
            if (App.LoginRegion == "TW")
            {
                input = this.UploadString("https://tw.beanfun.com/TW/accounts_management/02.aspx", payload);
            }
            else
            {
                input = this.UploadString("http://hk.beanfun.com/beanfun_web_ap/accounts_management/02.aspx", payload);
            }
            Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstate";
                return null;
            }
            string value = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__VIEWSTATEGENERATOR\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstategenerator";
                return null;
            }
            string value2 = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoEventvalidation";
                return null;
            }
            string value3 = regex.Match(input).Groups[1].Value;
            payload.Clear();
            payload.Add("__VIEWSTATE", value);
            payload.Add("__VIEWSTATEGENERATOR", value2);
            payload.Add("__EVENTVALIDATION", value3);
            regex = new Regex("<span id=\"lblErrorMessage\" style=\"color:Red;\">(.*)</span>");
            payload.Add("lblErrorMessage", regex.IsMatch(input) ? regex.Match(input).Groups[1].Value : "");
            return payload;
        }

        public NameValueCollection UnconnectedGame_AddAccountCheckNickName(string service_code, string service_region, string txtServiceAccountDN, NameValueCollection payload)
        {
            if (payload == null)
            {
                return null;
            }
            payload.Add("__EVENTTARGET", "lbtnCheckNickName");
            payload.Add("__EVENTARGUMENT", "");
            payload.Add("txtServiceAccountID", "");
            if (txtServiceAccountDN != null)
            {
                if (App.LoginRegion == "TW")
                {
                    payload.Add("t1", txtServiceAccountDN);
                }
                else
                {
                    payload.Add("txtServiceAccountDN", txtServiceAccountDN);
                }
            }
            payload.Add("txtNewPwd", "");
            payload.Add("txtNewPwd2", "");
            string input;
            if (App.LoginRegion == "TW")
            {
                input = this.UploadString("https://tw.beanfun.com/TW/accounts_management/02.aspx", payload);
            }
            else
            {
                input = this.UploadString("http://hk.beanfun.com/beanfun_web_ap/accounts_management/02.aspx", payload);
            }
            Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstate";
                return null;
            }
            string value = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__VIEWSTATEGENERATOR\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstategenerator";
                return null;
            }
            string value2 = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoEventvalidation";
                return null;
            }
            string value3 = regex.Match(input).Groups[1].Value;
            payload.Clear();
            payload.Add("__VIEWSTATE", value);
            payload.Add("__VIEWSTATEGENERATOR", value2);
            payload.Add("__EVENTVALIDATION", value3);
            regex = new Regex("<span id=\"lblErrorMessage\" style=\"color:Red;\">(.*)</span>");
            payload.Add("lblErrorMessage", regex.IsMatch(input) ? regex.Match(input).Groups[1].Value : "");
            return payload;
        }

        public string UnconnectedGame_AddAccount(string service_code, string service_region, string name, string txtNewPwd, string txtNewPwd2, string txtServiceAccountDN, NameValueCollection payload)
        {
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
            if (App.LoginRegion == "TW")
            {
                if (payload == null)
                {
                    return null;
                }
                payload.Add("__EVENTTARGET", "");
                payload.Add("__EVENTARGUMENT", "");
                payload.Add("txtServiceAccountID", name);
                if (txtServiceAccountDN != null)
                {
                    if (App.LoginRegion == "TW")
                    {
                        payload.Add("t1", txtServiceAccountDN);
                    }
                    else
                    {
                        payload.Add("txtServiceAccountDN", txtServiceAccountDN);
                    }
                }
                payload.Add("txtNewPwd", txtNewPwd);
                payload.Add("txtNewPwd2", txtNewPwd2);
                payload.Add("chkBox1", "on");
                payload.Add("imgbtn_Submit.x", "0");
                payload.Add("imgbtn_Submit.y", "0");
                string input;
                if (App.LoginRegion == "TW")
                {
                    input = this.UploadString("https://tw.beanfun.com/TW/accounts_management/02.aspx", payload);
                }
                else
                {
                    input = this.UploadString("http://hk.beanfun.com/beanfun_web_ap/accounts_management/02.aspx", payload);
                }
                Regex regex = new Regex("<span id=\"lblErrorMessage\" style=\"color:Red;\">(.*)</span>");
                if (!regex.IsMatch(input))
                {
                    return "";
                }
                return regex.Match(input).Groups[1].Value;
            }
            else
            {
                ASObject asobject = this.gameServAccListApp.AddServiceAccount(null, null, service_code, service_region, null, name);
                if (asobject == null || asobject["result"] == null || (string)asobject["result"] != "1")
                {
                    return null;
                }
                return "";
            }
        }

        public string UnconnectedGame_ChangePassword(string service_code, string service_region, int num, string txtEmail)
        {
            this.a(service_code, service_region);
            string input;
            if (App.LoginRegion == "TW")
            {
                input = this.DownloadString("https://tw.beanfun.com/TW/accounts_management/01Accounts.aspx");
            }
            else
            {
                input = this.DownloadString("http://hk.beanfun.com/beanfun_web_ap/accounts_management/01Accounts.aspx");
            }
            Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstate";
                return null;
            }
            string value = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__VIEWSTATEGENERATOR\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstategenerator";
                return null;
            }
            string value2 = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoEventvalidation";
                return null;
            }
            string value3 = regex.Match(input).Groups[1].Value;
            NameValueCollection nameValueCollection = new NameValueCollection();
            nameValueCollection.Add("__VIEWSTATE", value);
            nameValueCollection.Add("__VIEWSTATEGENERATOR", value2);
            nameValueCollection.Add("__EVENTVALIDATION", value3);
            nameValueCollection.Add("__EVENTTARGET", "gvServiceAccountList");
            nameValueCollection.Add("__EVENTARGUMENT", "ChangePassword$" + num);
            nameValueCollection.Add("x", "0");
            nameValueCollection.Add("y", "0");
            if (App.LoginRegion == "TW")
            {
                input = this.UploadString("https://tw.beanfun.com/TW/accounts_management/01Accounts.aspx", nameValueCollection);
                input = this.DownloadString("https://tw.beanfun.com/TW/accounts_management/03.aspx");
            }
            else
            {
                input = this.UploadString("http://hk.beanfun.com/beanfun_web_ap/accounts_management/01Accounts.aspx", nameValueCollection);
                input = this.DownloadString("http://hk.beanfun.com/beanfun_web_ap/accounts_management/03.aspx");
            }
            regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstate";
                return null;
            }
            value = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__VIEWSTATEGENERATOR\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstategenerator";
                return null;
            }
            value2 = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoEventvalidation";
                return null;
            }
            value3 = regex.Match(input).Groups[1].Value;
            nameValueCollection.Clear();
            nameValueCollection.Add("__VIEWSTATE", value);
            nameValueCollection.Add("__VIEWSTATEGENERATOR", value2);
            nameValueCollection.Add("__EVENTVALIDATION", value3);
            nameValueCollection.Add("txtEmail", txtEmail);
            nameValueCollection.Add("imgbtn_Submit.x", "0");
            nameValueCollection.Add("imgbtn_Submit.y", "0");
            regex = new Regex("<span id=\"lblErrorMessage\" style=\"color:Red;\">(.*)</span>");
            if (App.LoginRegion == "TW")
            {
                input = this.UploadString("https://tw.beanfun.com/TW/accounts_management/03.aspx", nameValueCollection);
                string result = regex.IsMatch(input) ? regex.Match(input).Groups[1].Value : "";
                if (result != "")
                {
                    return result;
                }
                regex = new Regex("verify_code=(.*)");
                if (!regex.IsMatch(this.m_c.ToString()))
                {
                    return null;
                }
                return "verify_code" + regex.Match(this.m_c.ToString()).Groups[1].Value;
            }
            else
            {
                input = this.UploadString("http://hk.beanfun.com/beanfun_web_ap/accounts_management/03.aspx", nameValueCollection);
                string text = regex.IsMatch(input) ? regex.Match(input).Groups[1].Value : null;
                if (text == null || text == "")
                {
                    return null;
                }
                regex = new Regex("确认码：(.*)");
                if (!regex.IsMatch(text))
                {
                    return text;
                }
                return "verify_code" + regex.Match(text).Groups[1].Value;
            }
        }

        public bool AddServiceAccount(string name, string service_code, string service_region)
        {
            if (name == null || name == "")
            {
                return false;
            }
            if (App.LoginRegion == "TW")
            {
                JObject jobject = JObject.Parse(this.UploadString("https://tw.beanfun.com/generic_handlers/gamezone.ashx", new NameValueCollection
                {
                    {
                        "strFunction",
                        "AddServiceAccount"
                    },
                    {
                        "npsc",
                        ""
                    },
                    {
                        "npsr",
                        ""
                    },
                    {
                        "sc",
                        service_code
                    },
                    {
                        "sr",
                        service_region
                    },
                    {
                        "sadn",
                        name
                    },
                    {
                        "sag",
                        ""
                    }
                }));
                return jobject["intResult"] != null && (int)jobject["intResult"] == 1;
            }
            ASObject asobject = this.gameServAccListApp.AddServiceAccount(null, null, service_code, service_region, null, name);
            return asobject != null && asobject["result"] != null && !((string)asobject["result"] != "1");
        }

        public bool ChangeServiceAccountDisplayName(string newName, string gameCode, BeanfunClient.ServiceAccount account)
        {
            if (newName == null || newName == "" || account == null || newName == account.sname)
            {
                return false;
            }
            if (App.LoginRegion == "TW")
            {
                JObject jobject = JObject.Parse(this.UploadString("https://tw.beanfun.com/generic_handlers/gamezone.ashx", new NameValueCollection
                {
                    {
                        "strFunction",
                        "ChangeServiceAccountDisplayName"
                    },
                    {
                        "sl",
                        gameCode
                    },
                    {
                        "said",
                        account.sid
                    },
                    {
                        "nsadn",
                        newName
                    }
                }));
                return jobject["intResult"] != null && (int)jobject["intResult"] == 1;
            }
            return this.gameServAccListApp.ChangeServiceAccountDisplayName(gameCode, account.sid, newName) == "";
        }

        public string GetServiceContract(string service_code, string service_region)
        {
            if (App.LoginRegion == "TW")
            {
                JObject jobject = JObject.Parse(this.UploadStringGZip("https://tw.beanfun.com/generic_handlers/gamezone.ashx", new NameValueCollection
                {
                    {
                        "strFunction",
                        "GetServiceContract"
                    },
                    {
                        "sc",
                        service_code
                    },
                    {
                        "sr",
                        service_region
                    }
                }));
                if (jobject["intResult"] == null || (int)jobject["intResult"] != 1)
                {
                    return "";
                }
                return (string)jobject["strResult"];
            }
            else
            {
                string serviceContract = this.gameServAccListApp.GetServiceContract(service_code, service_region);
                if (serviceContract != null)
                {
                    return serviceContract;
                }
                return "";
            }
        }

        // (get) Token: 0x0600016C RID: 364 RVA: 0x0000A635 File Offset: 0x00008835
        public string WebToken
        {
            get
            {
                return this.m_d;
            }
        }

        // (get) Token: 0x0600016D RID: 365 RVA: 0x0000A63D File Offset: 0x0000883D
        // (set) Token: 0x0600016E RID: 366 RVA: 0x0000A645 File Offset: 0x00008845
        public string CardID
        {
            get
            {
                return this.m_e;
            }
            set
            {
                this.m_e = value;
            }
        }

        // (get) Token: 0x0600016F RID: 367 RVA: 0x0000A64E File Offset: 0x0000884E
        public BFServiceX BFServ
        {
            get
            {
                return this.m_a;
            }
        }

        public BeanfunClient()
        {
            this.m_f = true;
            this.m_b = new CookieContainer();
            base.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            base.Headers.Set("Accept-Encoding", "identity");
            base.Encoding = Encoding.UTF8;
            this.m_c = null;
            this.errmsg = null;
            this.m_d = null;
            this.m_e = null;
            this.accountList = new List<BeanfunClient.ServiceAccount>();
            this.accountAmountLimitNotice = "";
        }

        public string DownloadString(string Uri, Encoding Encoding)
        {
            base.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            base.Headers.Set("Accept-Encoding", "identity");
            return Encoding.GetString(base.DownloadData(Uri));
        }

        public new string DownloadString(string Uri)
        {
            base.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            base.Headers.Set("Accept-Encoding", "identity");
            return base.DownloadString(Uri);
        }

        public string UploadString(string skey, NameValueCollection payload)
        {
            base.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            base.Headers.Set("Accept-Encoding", "identity");
            return Encoding.UTF8.GetString(base.UploadValues(skey, payload));
        }

        public string UploadStringGZip(string skey, NameValueCollection payload)
        {
            base.Headers.Set("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
            base.Headers.Set("Accept-Encoding", "gzip, deflate, br");
            byte[] array = base.UploadValues(skey, payload);
            if (array[0] == 31 && array[1] == 139)
            {
                Stream stream = new MemoryStream(array);
                MemoryStream memoryStream = new MemoryStream();
                GZipStream gzipStream = new GZipStream(stream, CompressionMode.Decompress);
                byte[] array2 = new byte[1000];
                int count;
                while ((count = gzipStream.Read(array2, 0, array2.Length)) > 0)
                {
                    memoryStream.Write(array2, 0, count);
                }
                array = memoryStream.ToArray();
            }
            return Encoding.UTF8.GetString(array);
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest httpWebRequest = base.GetWebRequest(address) as HttpWebRequest;
            if (httpWebRequest != null)
            {
                httpWebRequest.CookieContainer = this.m_b;
                httpWebRequest.AllowAutoRedirect = this.m_f;
            }
            return httpWebRequest;
        }

        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse webResponse = base.GetWebResponse(request);
            this.m_c = webResponse.ResponseUri;
            return webResponse;
        }

        public CookieCollection GetCookies()
        {
            return this.m_b.GetCookies(new Uri("https://" + App.LoginRegion.ToLower() + ".beanfun.com/"));
        }

        private string a(string A_0)
        {
            foreach (object obj in this.GetCookies())
            {
                Cookie cookie = (Cookie)obj;
                if (cookie.Name == A_0)
                {
                    return cookie.Value;
                }
            }
            return null;
        }

        private string a(int A_0 = 0)
        {
            DateTime now = DateTime.Now;
            if (A_0 == 1)
            {
                return (now.Year - 1900).ToString() + (now.Month - 1).ToString() + now.ToString("ddHHmmssfff");
            }
            if (A_0 != 2)
            {
                return now.ToString("yyyyMMddHHmmss.fff");
            }
            return now.Year.ToString() + (now.Month - 1).ToString() + now.ToString("ddHHmmssfff");
        }

        public void Ping()
        {
            try
            {
                if (App.LoginRegion == "TW")
                {
                    base.Encoding.GetString(base.DownloadData("https://tw.beanfun.com/beanfun_block/generic_handlers/echo_token.ashx?webtoken=1"));
                }
                else if (this.m_a != null)
                {
                    this.DownloadString("http://hk.beanfun.com/beanfun_block/generic_handlers/echo_token.ashx?token=" + this.m_a.Token);
                }
            }
            catch
            {
            }
        }

        public string Recharge()
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
            return text;
        }
        public int getRemainPoint()
        {
            string uri = "https://tw.beanfun.com/beanfun_block/generic_handlers/get_remain_point.ashx?webtoken=1";
            if (App.LoginRegion == "HK")
            {
                uri = "http://hk.beanfun.com/beanfun_block/generic_handlers/get_remain_point.ashx?token=" + this.m_a.Token;
            }
            string input = this.DownloadString(uri);
            int result;
            try
            {
                Regex regex = new Regex("\"RemainPoint\" : \"(.*)\" }");
                if (regex.IsMatch(input))
                {
                    result = int.Parse(regex.Match(input).Groups[1].Value);
                }
                else
                {
                    result = 0;
                }
            }
            catch
            {
                result = 0;
            }
            return result;
        }

        public string getEmail()
        {
            if (App.LoginRegion == "HK")
            {
                return "";
            }
            base.Headers.Set("Referer", "https://tw.beanfun.com/");
            string input = this.DownloadString("https://tw.beanfun.com/beanfun_block/loader.ashx?service_code=999999&service_region=T0");
            Regex regex = new Regex("BeanFunBlock.LoggedInUserData.Email = \"(.*)\";BeanFunBlock.LoggedInUserData.MessageCount");
            if (regex.IsMatch(input))
            {
                return regex.Match(input).Groups[1].Value;
            }
            return "";
        }

        private string d(string A_0, string A_1, string A_2)
        {
            string result;
            try
            {
                string input = this.DownloadString("http://hk.beanfun.com/beanfun_block/login/id-pass_form.aspx?otp1=" + A_2 + "&seed=0");
                Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
                if (!regex.IsMatch(input))
                {
                    this.errmsg = "LoginNoViewstate";
                    result = null;
                }
                else
                {
                    string value = regex.Match(input).Groups[1].Value;
                    regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
                    if (!regex.IsMatch(input))
                    {
                        this.errmsg = "LoginNoEventvalidation";
                        result = null;
                    }
                    else
                    {
                        string value2 = regex.Match(input).Groups[1].Value;
                        regex = new Regex("id=\"__VIEWSTATEGENERATOR\" value=\"(.*)\" />");
                        if (!regex.IsMatch(input))
                        {
                            this.errmsg = "LoginNoViewstateGenerator";
                            result = null;
                        }
                        else
                        {
                            string value3 = regex.Match(input).Groups[1].Value;
                            NameValueCollection nameValueCollection = new NameValueCollection();
                            nameValueCollection.Add("__EVENTTARGET", "");
                            nameValueCollection.Add("__EVENTARGUMENT", "");
                            nameValueCollection.Add("__VIEWSTATE", value);
                            nameValueCollection.Add("__VIEWSTATEGENERATOR", value3);
                            nameValueCollection.Add("__EVENTVALIDATION", value2);
                            nameValueCollection.Add("t_AccountID", A_0);
                            nameValueCollection.Add("t_Password", A_1);
                            nameValueCollection.Add("btn_login.x", "0");
                            nameValueCollection.Add("btn_login.y", "0");
                            nameValueCollection.Add("recaptcha_response_field", "manual_challenge");
                            input = this.UploadString("http://hk.beanfun.com/beanfun_block/login/id-pass_form.aspx?otp1=" + A_2 + "&seed=0", nameValueCollection);
                            regex = new Regex("ProcessLoginV2\\((.*)\\);\\\"");
                            if (!regex.IsMatch(input))
                            {
                                this.errmsg = "LoginNoProcessLoginV2JSON";
                                result = null;
                            }
                            else
                            {
                                string text = regex.Match(input).Groups[1].Value.Replace("\\", "");
                                this.m_a.Token = (string)JObject.Parse(text)["token"];
                                result = "true";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errmsg = "LoginUnknown\n\n" + ex.Message + "\n" + ex.StackTrace;
                result = null;
            }
            return result;
        }

        private string c(string A_0, string A_1, string A_2)
        {
            string result;
            try
            {
                string text = this.DownloadString("https://tw.newlogin.beanfun.com/login/id-pass_form.aspx?skey=" + A_2);
                Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
                if (!regex.IsMatch(text))
                {
                    this.errmsg = "LoginNoViewstate";
                    result = null;
                }
                else
                {
                    string value = regex.Match(text).Groups[1].Value;
                    regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
                    if (!regex.IsMatch(text))
                    {
                        this.errmsg = "LoginNoEventvalidation";
                        result = null;
                    }
                    else
                    {
                        string value2 = regex.Match(text).Groups[1].Value;
                        regex = new Regex("id=\"__VIEWSTATEGENERATOR\" value=\"(.*)\" />");
                        if (!regex.IsMatch(text))
                        {
                            this.errmsg = "LoginNoViewstateGenerator";
                            result = null;
                        }
                        else
                        {
                            string value3 = regex.Match(text).Groups[1].Value;
                            regex = new Regex("id=\"LBD_VCID_c_login_idpass_form_samplecaptcha\" value=\"(.*)\" />");
                            if (!regex.IsMatch(text))
                            {
                                this.errmsg = "LoginNoSamplecaptcha";
                                result = null;
                            }
                            else
                            {
                                string value4 = regex.Match(text).Groups[1].Value;
                                NameValueCollection nameValueCollection = new NameValueCollection();
                                nameValueCollection.Add("__EVENTTARGET", "");
                                nameValueCollection.Add("__EVENTARGUMENT", "");
                                nameValueCollection.Add("__VIEWSTATE", value);
                                nameValueCollection.Add("__VIEWSTATEGENERATOR", value3);
                                nameValueCollection.Add("__EVENTVALIDATION", value2);
                                nameValueCollection.Add("t_AccountID", A_0);
                                nameValueCollection.Add("t_Password", A_1);
                                nameValueCollection.Add("CodeTextBox", "");
                                nameValueCollection.Add("btn_login", "登录");
                                nameValueCollection.Add("LBD_VCID_c_login_idpass_form_samplecaptcha", value4);
                                text = this.UploadString("https://tw.newlogin.beanfun.com/login/id-pass_form.aspx?skey=" + A_2, nameValueCollection);
                                if (text.Contains("RELOAD_CAPTCHA_CODE") && text.Contains("alert"))
                                {
                                    this.errmsg = "LoginAdvanceCheck";
                                    result = null;
                                }
                                else
                                {
                                    regex = new Regex("akey=(.*)");
                                    if (!regex.IsMatch(this.m_c.ToString()))
                                    {
                                        this.errmsg = "LoginNoAkey";
                                        regex = new Regex("<script type=\"text/javascript\">\\$\\(function\\(\\){MsgBox.Show\\('(.*)'\\);}\\);</script>");
                                        if (regex.IsMatch(text))
                                        {
                                            this.errmsg = regex.Match(text).Groups[1].Value;
                                        }
                                        else
                                        {
                                            regex = new Regex("pollRequest\\(\"([^\"]*)\",\"(\\w+)\",\"([^\"]+)\"\\);");
                                            if (regex.IsMatch(text))
                                            {
                                                this.errmsg = regex.Match(text).Groups[1].Value + "\",\"" + regex.Match(text).Groups[3].Value;
                                                this.m_h = regex.Match(text).Groups[2].Value;
                                            }
                                        }
                                        result = null;
                                    }
                                    else
                                    {
                                        result = regex.Match(this.m_c.ToString()).Groups[1].Value;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errmsg = "LoginUnknown\n\n" + ex.Message + "\n" + ex.StackTrace;
                result = null;
            }
            return result;
        }

        private string b(string A_0, string A_1, string A_2)
        {
            string result;
            try
            {
                string input = this.DownloadString("https://tw.newlogin.beanfun.com/login/playsafe_form.aspx?skey=" + A_2);
                Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
                if (!regex.IsMatch(input))
                {
                    this.errmsg = "LoginNoViewstate";
                    result = null;
                }
                else
                {
                    string value = regex.Match(input).Groups[1].Value;
                    regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
                    if (!regex.IsMatch(input))
                    {
                        this.errmsg = "LoginNoEventvalidation";
                        result = null;
                    }
                    else
                    {
                        string value2 = regex.Match(input).Groups[1].Value;
                        input = this.DownloadString("https://tw.newlogin.beanfun.com/generic_handlers/get_security_otp.ashx?d=" + this.a(1));
                        regex = new Regex("<playsafe_otp>(\\w+)</playsafe_otp>");
                        if (!regex.IsMatch(input))
                        {
                            this.errmsg = "LoginNoSotp";
                            result = null;
                        }
                        else
                        {
                            string value3 = regex.Match(input).Groups[1].Value;
                            h h = null;
                            try
                            {
                                h = new h();
                            }
                            catch
                            {
                                this.errmsg = "LoginNoPSDriver";
                                return null;
                            }
                            string text = h.a();
                            if (text == null)
                            {
                                this.errmsg = "LoginNoReaderName";
                                result = null;
                            }
                            else if (h.stringA == null)
                            {
                                this.errmsg = "LoginNoCardType";
                                result = null;
                            }
                            else
                            {
                                string text2 = null;
                                string text3 = null;
                                if (h.stringA == "F")
                                {
                                    h.stringC = h.c(text);
                                    if (h.stringC == null)
                                    {
                                        this.errmsg = "LoginNoCardId";
                                        return null;
                                    }
                                    string text4 = h.a(text, A_1);
                                    if (text4 == null)
                                    {
                                        this.errmsg = "LoginNoOpInfo";
                                        return null;
                                    }
                                    text2 = string.Concat(new string[]
                                    {
                                            h.stringA,
                                            "~",
                                            value3,
                                            "~",
                                            A_0,
                                            "~",
                                            text4
                                    });
                                    text3 = h.a(text, A_1, text2);
                                    if (text3 == null)
                                    {
                                        this.errmsg = "LoginNoEncryptedData";
                                        return null;
                                    }
                                }
                                else if (h.stringA == "G")
                                {
                                    text2 = string.Concat(new string[]
                                    {
                                            h.stringA,
                                            "~",
                                            value3,
                                            "~",
                                            A_0,
                                            "~"
                                    });
                                    text3 = h.b(A_1, text2);
                                }
                                NameValueCollection nameValueCollection = new NameValueCollection();
                                nameValueCollection.Add("__EVENTTARGET", "");
                                nameValueCollection.Add("__EVENTARGUMENT", "");
                                nameValueCollection.Add("__VIEWSTATE", value);
                                nameValueCollection.Add("__EVENTVALIDATION", value2);
                                nameValueCollection.Add("card_check_id", h.stringC);
                                nameValueCollection.Add("original", text2);
                                nameValueCollection.Add("signature", text3);
                                nameValueCollection.Add("serverotp", value3);
                                nameValueCollection.Add("t_AccountID", A_0);
                                nameValueCollection.Add("t_Password", A_1);
                                nameValueCollection.Add("btn_login", "Login");
                                input = this.UploadString("https://tw.newlogin.beanfun.com/login/playsafe_form.aspx?skey=" + A_2, nameValueCollection);
                                regex = new Regex("akey=(.*)");
                                //if (!regex.IsMatch(this.c.ToString()))
                                //{
                                //    this.errmsg = "LoginNoAkey";
                                //    result = null;
                                //}
                                //else
                                {
                                    result = h.stringC + " " + regex.Match(this.m_c.ToString()).Groups[1].Value;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.errmsg = "LoginUnknown\n\n" + ex.Message + "\n" + ex.StackTrace;
                result = null;
            }
            return null; ;
        }

        public BeanfunClient.QRCodeClass GetQRCodeValue(string skey, bool oldAppQRCode)
        {
            string input = this.DownloadString("https://tw.newlogin.beanfun.com/login/qr_form.aspx?skey=" + skey);
            Regex regex = new Regex("id=\"__VIEWSTATE\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoViewstate";
                return null;
            }
            string value = regex.Match(input).Groups[1].Value;
            regex = new Regex("id=\"__EVENTVALIDATION\" value=\"(.*)\" />");
            if (!regex.IsMatch(input))
            {
                this.errmsg = "LoginNoEventvalidation";
                return null;
            }
            string value2 = regex.Match(input).Groups[1].Value;
            string value3;
            string text;
            if (oldAppQRCode)
            {
                string input2 = this.DownloadString("https://tw.newlogin.beanfun.com/generic_handlers/get_qrcodeData.ashx?skey=" + skey + "&startGame=");
                regex = new Regex("\"strEncryptData\": \"(.*)\"}");
                if (!regex.IsMatch(input2))
                {
                    this.errmsg = "LoginNoQrcodedata";
                    return null;
                }
                value3 = regex.Match(input2).Groups[1].Value;
                text = Uri.UnescapeDataString(value3);
            }
            else
            {
                regex = new Regex("\\$\\(\"#theQrCodeImg\"\\).attr\\(\"src\", \"../(.*)\" \\+ obj.strEncryptData\\);");
                if (!regex.IsMatch(input))
                {
                    this.errmsg = "LoginNoHash";
                    return null;
                }
                value3 = regex.Match(input).Groups[1].Value;
                text = this.getQRCodeStrEncryptData(skey);
                if (text == null || text == "")
                {
                    this.errmsg = "LoginIntResultError";
                    return null;
                }
            }
            return new BeanfunClient.QRCodeClass
            {
                skey = skey,
                viewstate = value,
                eventvalidation = value2,
                value = text,
                bitmapUrl = (oldAppQRCode ? ("http://tw.newlogin.beanfun.com/qrhandler.ashx?u=" + value3) : ("https://tw.newlogin.beanfun.com/" + value3)),
                oldAppQRCode = oldAppQRCode
            };
        }

        public string getQRCodeStrEncryptData(string skey)
        {
            JObject jobject = JObject.Parse(this.DownloadString("https://tw.newlogin.beanfun.com/generic_handlers/get_qrcodeData.ashx?skey=" + skey));
            if (jobject["intResult"] == null || (int)jobject["intResult"] != 1)
            {
                this.errmsg = "LoginIntResultError";
                return null;
            }
            return (string)jobject["strEncryptData"];
        }

        public BitmapImage getQRCodeImage(BeanfunClient.QRCodeClass qrcodeclass)
        {
            BitmapImage bitmapImage;
            try
            {
                byte[] buffer = base.DownloadData(qrcodeclass.bitmapUrl + (qrcodeclass.oldAppQRCode ? "" : qrcodeclass.value));
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(buffer);
                bitmapImage.EndInit();
            }
            catch (Exception)
            {
                bitmapImage = null;
            }
            return bitmapImage;
        }

        private string a(BeanfunClient.QRCodeClass A_0)
        {
            string result;
            try
            {
                string skey = A_0.skey;
                base.Headers.Set("Referer", "https://tw.newlogin.beanfun.com/login/qr_form.aspx?skey=" + skey);
                this.m_f = false;
                byte[] bytes = base.DownloadData("https://tw.newlogin.beanfun.com/login/qr_step2.aspx?skey=" + skey);
                this.m_f = true;
                string @string = Encoding.UTF8.GetString(bytes);
                Regex regex = new Regex("akey=(.*)&authkey");
                if (!regex.IsMatch(@string))
                {
                    this.errmsg = "AKeyParseFailed";
                    result = null;
                }
                else
                {
                    string value = regex.Match(@string).Groups[1].Value;
                    regex = new Regex("authkey=(.*)&");
                    if (!regex.IsMatch(@string))
                    {
                        this.errmsg = "authkeyParseFailed";
                        result = null;
                    }
                    else
                    {
                        string value2 = regex.Match(@string).Groups[1].Value;
                        this.DownloadString(string.Concat(new string[]
                        {
                            "https://tw.newlogin.beanfun.com/login/final_step.aspx?akey=",
                            value,
                            "&authkey=",
                            value2,
                            "&bfapp=1"
                        }));
                        result = value;
                    }
                }
            }
            catch (Exception ex)
            {
                this.errmsg = "LoginUnknown\n\n" + ex.Message + "\n" + ex.StackTrace;
                result = null;
            }
            return result;
        }

        public int QRCodeCheckLoginStatus(BeanfunClient.QRCodeClass qrcodeclass)
        {
            try
            {
                string skey = qrcodeclass.skey;
                base.Headers.Set("Referer", "https://tw.newlogin.beanfun.com/login/qr_form.aspx?skey=" + skey);
                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection.Add(qrcodeclass.oldAppQRCode ? "data" : "status", qrcodeclass.value);
                string text = this.UploadString(qrcodeclass.oldAppQRCode ? "https://tw.bfapp.beanfun.com/api/Check/CheckLoginStatus" : "https://tw.newlogin.beanfun.com/generic_handlers/CheckLoginStatus.ashx", nameValueCollection);
                JObject jobject;
                try
                {
                    jobject = JObject.Parse(text);
                }
                catch
                {
                    this.errmsg = "LoginJsonParseFailed";
                    return -1;
                }
                string text2 = (string)jobject["ResultMessage"];
                if (text2 == "Failed")
                {
                    return 0;
                }
                if (text2 == "Token Expired")
                {
                    return -2;
                }
                if (text2 == "Success")
                {
                    return 1;
                }
                this.errmsg = text;
                return -1;
            }
            catch (Exception ex)
            {
                this.errmsg = "Network Error on QRCode checking login status\n\n" + ex.Message + "\n" + ex.StackTrace;
            }
            return -1;
        }

        public JObject CheckIsRegisteDevice(string service_code = "610074", string service_region = "T9")
        {
            JObject jobject = JObject.Parse(this.UploadString("https://tw.newlogin.beanfun.com/login/bfAPPAutoLogin.ashx", new NameValueCollection
            {
                {
                    "LT",
                    this.m_h
                }
            }));
            if (jobject == null || jobject["IntResult"] == null || jobject["StrReslut"] == null)
            {
                return null;
            }
            if ((string)jobject["IntResult"] == "2")
            {
                this.DownloadString("https://tw.newlogin.beanfun.com/login/" + (string)jobject["StrReslut"]);
                Regex regex = new Regex("akey=(.*)");
                if (!regex.IsMatch((string)jobject["StrReslut"]))
                {
                    this.errmsg = "AKeyParseFailed";
                    return null;
                }
                string value = regex.Match((string)jobject["StrReslut"]).Groups[1].Value;
                this.a(value, service_code, service_region);
            }
            return jobject;
        }

        public string GetSessionkey()
        {
            if (App.LoginRegion == "TW")
            {
                string text = this.DownloadString("https://tw.beanfun.com/beanfun_block/bflogin/default.aspx?service=999999_T0");
                text = this.m_c.ToString();
                if (text == null)
                {
                    this.errmsg = "LoginNoResponse";
                    return null;
                }
                Regex regex = new Regex("skey=(.*)&display");
                if (!regex.IsMatch(text))
                {
                    this.errmsg = "LoginNoSkey";
                    return null;
                }
                return regex.Match(text).Groups[1].Value;
            }
            else
            {
                string text2 = this.DownloadString("http://hk.beanfun.com/beanfun_block/login/index.aspx?service=999999_T0");
                if (text2 == null)
                {
                    this.errmsg = "LoginNoResponse";
                    return null;
                }
                Regex regex2 = new Regex("otp1 = \"(.*)\";");
                if (!regex2.IsMatch(text2))
                {
                    this.errmsg = "LoginNoOTP1";
                    return null;
                }
                return regex2.Match(text2).Groups[1].Value;
            }
        }

        public void Login(string id, string pass, int loginMethod, BeanfunClient.QRCodeClass qrcodeClass = null, string service_code = "610074", string service_region = "T9")
        {
            this.m_e = null;
            this.m_d = null;
            this.m_i = null;
            try
            {
                if (loginMethod == 2)
                {
                    this.m_i = qrcodeClass.skey;
                }
                else
                {
                    if (App.LoginRegion == "HK")
                    {
                        if (this.m_a == null)
                        {
                            try
                            {
                                this.m_a = new BFServiceX();
                            }
                            catch
                            {
                                this.errmsg = "BFServiceXNotFound";
                                return;
                            }
                        }
                        this.m_a.Initialize2();
                        if (this.gameServAccListApp == null)
                        {
                            this.gameServAccListApp = new GameServerAccountListApp();
                        }
                    }
                    this.m_i = this.GetSessionkey();
                }
                string a_;
                switch (loginMethod)
                {
                    case 0:
                        if (App.LoginRegion == "TW")
                        {
                            a_ = this.c(id, pass, this.m_i);
                        }
                        else
                        {
                            a_ = this.d(id, pass, this.m_i);
                        }
                        break;
                    case 1:
                        {
                            string text = this.b(id, pass, this.m_i);
                            if (text == null)
                            {
                                return;
                            }
                            string[] array = text.Split(new char[]
                            {
                        ' '
                            });
                            if (array.Count<string>() != 2)
                            {
                                this.errmsg = "LoginPlaySafeResultError";
                                return;
                            }
                            string text2 = array[0];
                            a_ = array[1];
                            this.m_e = text2;
                            break;
                        }
                    case 2:
                        a_ = this.a(qrcodeClass);
                        break;
                    default:
                        this.errmsg = "LoginNoMethod";
                        return;
                }
                this.a(a_, service_code, service_region);
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    this.errmsg = "網路连接错误，请检查官方網站连接是否正常。" + ex.Message;
                }
                else
                {
                    this.errmsg = "LoginUnknown\n\n" + ex.Message + "\n" + ex.StackTrace;
                }
            }
        }

        private void a(string A_0, string A_1 = "610074", string A_2 = "T9")
        {
            if (this.m_i == null || A_0 == null)
            {
                return;
            }
            if (App.LoginRegion == "TW")
            {
                this.UploadString("https://tw.beanfun.com/beanfun_block/bflogin/return.aspx", new NameValueCollection
                {
                    {
                        "SessionKey",
                        this.m_i
                    },
                    {
                        "AuthKey",
                        A_0
                    },
                    {
                        "ServiceCode",
                        ""
                    },
                    {
                        "ServiceRegion",
                        ""
                    },
                    {
                        "ServiceAccountSN",
                        "0"
                    }
                });
                this.DownloadString("https://tw.beanfun.com/" + base.ResponseHeaders["Location"]);
                this.m_d = this.a("bfWebToken");
                if (this.m_d == "")
                {
                    this.errmsg = "LoginNoWebtoken";
                    return;
                }
                this.GetAccounts(A_1, A_2, false);
            }
            else
            {
                if (!this.DownloadString(string.Concat(new string[]
                {
                    "http://hk.beanfun.com/beanfun_block/auth.aspx?channel=game_zone&page_and_query=game_start.aspx%3Fservice_code_and_region%3D",
                    A_1,
                    "_",
                    A_2,
                    "&token=",
                    this.m_a.Token
                })).Contains("document.location = \"http://hk.beanfun.com/beanfun_block/game_zone/game_server_account_list.aspx"))
                {
                    this.errmsg = "LoginAuthErr";
                    return;
                }
                this.GetAccounts_HK(A_1, A_2, false);
            }
            if (this.errmsg != null)
            {
                return;
            }
            this.remainPoint = this.getRemainPoint();
            this.errmsg = null;
        }

        public void Logout()
        {
            if (App.LoginRegion == "TW")
            {
                this.DownloadString("https://tw.beanfun.com/generic_handlers/remove_bflogin_session.ashx");
                this.UploadString("https://tw.newlogin.beanfun.com/generic_handlers/erase_token.ashx", new NameValueCollection
                {
                    {
                        "web_token",
                        "1"
                    }
                });
                return;
            }
            this.DownloadString("http://hk.beanfun.com/beanfun_block/generic_handlers/remove_login_session.ashx");
            this.DownloadString("http://hk.beanfun.com/beanfun_web_ap/remove_login_session.ashx");
            if (this.m_a != null)
            {
                this.DownloadString("http://hk.beanfun.com/beanfun_block/generic_handlers/erase_token.ashx?token=" + this.m_a.Token);
            }
        }

        public string GetOTP(BeanfunClient.ServiceAccount acc, string service_code = "610074", string service_region = "T9")
        {
            string result;
            try
            {
                string text;
                if (App.LoginRegion == "TW")
                {
                    text = this.DownloadString(string.Concat(new string[]
                    {
                        "https://tw.beanfun.com/beanfun_block/game_zone/game_start_step2.aspx?service_code=",
                        service_code,
                        "&service_region=",
                        service_region,
                        "&sotp=",
                        acc.ssn,
                        "&dt=",
                        this.a(2)
                    }));
                    if (text == "")
                    {
                        this.errmsg = "OTPNoResponse";
                        return null;
                    }
                    if (text.Contains("很抱歉，需先完成进阶认证"))
                    {
                        this.errmsg = "OTPNeedAuthAccount";
                        return null;
                    }
                    Regex regex = new Regex("GetResultByLongPolling&key=(.*)\"");
                    if (!regex.IsMatch(text))
                    {
                        this.errmsg = "OTPNoLongPollingKey";
                        return null;
                    }
                    string value = regex.Match(text).Groups[1].Value;
                    if (acc.screatetime == null)
                    {
                        regex = new Regex("ServiceAccountCreateTime: \"([^\"]+)\"");
                        if (!regex.IsMatch(text))
                        {
                            this.errmsg = "OTPNoCreateTime";
                            return null;
                        }
                        acc.screatetime = regex.Match(text).Groups[1].Value;
                    }
                    text = this.DownloadString("https://tw.newlogin.beanfun.com/generic_handlers/get_cookies.ashx");
                    regex = new Regex("var m_strSecretCode = '(.*)';");
                    if (!regex.IsMatch(text))
                    {
                        this.errmsg = "OTPNoSecretCode";
                        return null;
                    }
                    string value2 = regex.Match(text).Groups[1].Value;
                    NameValueCollection nameValueCollection = new NameValueCollection();
                    nameValueCollection.Add("service_code", service_code);
                    nameValueCollection.Add("service_region", service_region);
                    nameValueCollection.Add("service_account_id", acc.sid);
                    nameValueCollection.Add("sotp", acc.ssn);
                    nameValueCollection.Add("service_account_display_name", acc.sname);
                    nameValueCollection.Add("service_account_create_time", acc.screatetime);
                    ServicePointManager.Expect100Continue = false;
                    this.UploadString("https://tw.beanfun.com/beanfun_block/generic_handlers/record_service_start.ashx", nameValueCollection);
                    text = this.DownloadString("https://tw.beanfun.com/generic_handlers/get_result.ashx?meth=GetResultByLongPolling&key=" + value + "&_=" + this.a(0));
                    text = this.DownloadString(string.Concat(new object[]
                    {
                        "https://tw.beanfun.com/beanfun_block/generic_handlers/get_webstart_otp.ashx?SN=",
                        value,
                        "&WebToken=",
                        this.m_d,
                        "&SecretCode=",
                        value2,
                        "&ppppp=1F552AEAFF976018F942B13690C990F60ED01510DDF89165F1658CCE7BC21DBA&ServiceCode=",
                        service_code,
                        "&ServiceRegion=",
                        service_region,
                        "&ServiceAccount=",
                        acc.sid,
                        "&CreateTime=",
                        acc.screatetime.Replace(" ", "%20"),
                        "&d=",
                        Environment.TickCount
                    }));
                }
                else
                {
                    text = this.DownloadString(string.Concat(new string[]
                    {
                        "http://hk.beanfun.com/beanfun_block/auth.aspx?channel=game_zone&page_and_query=game_start_step2.aspx%3Fservice_code%3D",
                        service_code,
                        "%26service_region%3D",
                        service_region,
                        "%26service_account_sn%3D",
                        acc.ssn,
                        "&token=",
                        this.m_a.Token
                    }));
                    if (text == "")
                    {
                        this.errmsg = "OTPNoResponse";
                        return null;
                    }
                    Regex regex2 = new Regex("var MyAccountData = (.*);");
                    if (!regex2.IsMatch(text))
                    {
                        this.errmsg = "OTPNoMyAccountData";
                        return null;
                    }
                    JObject jobject = JObject.Parse(regex2.Match(text).Groups[1].Value);
                    acc.sname = (string)jobject["ServiceAccountDisplayName"];
                    acc.screatetime = (string)jobject["ServiceAccountCreateTime"];
                    NameValueCollection nameValueCollection2 = new NameValueCollection();
                    nameValueCollection2.Add("service_code", service_code);
                    nameValueCollection2.Add("service_region", service_region);
                    nameValueCollection2.Add("service_account_id", acc.sid);
                    nameValueCollection2.Add("service_sotp", acc.ssn);
                    nameValueCollection2.Add("service_display_name", acc.sname);
                    nameValueCollection2.Add("service_create_time", acc.screatetime);
                    ServicePointManager.Expect100Continue = false;
                    this.UploadString("http://hk.beanfun.com/beanfun_block/generic_handlers/record_service_start.ashx", nameValueCollection2);
                    text = this.DownloadString(string.Concat(new object[]
                    {
                        "http://hk.beanfun.com/beanfun_block/generic_handlers/get_otp.ashx?ppppp=&token=",
                        this.m_a.Token,
                        "&account_service_code=",
                        service_code,
                        "&account_service_region=",
                        service_region,
                        "&service_account_id=",
                        acc.sid,
                        "&create_time=",
                        acc.screatetime.Replace(" ", "%20"),
                        "&d=",
                        Environment.TickCount
                    }));
                }
                text = text.Substring(2);
                string a_ = text.Substring(0, 8);
                string text2 = global::f.a(text.Substring(8), a_);
                if (text2 != null)
                {
                    text2 = text2.Trim(new char[1]);
                    this.errmsg = null;
                }
                else
                {
                    this.errmsg = "DecryptOTPError";
                }
                result = text2;
            }
            catch (Exception ex)
            {
                this.errmsg = "获取密码失败，请尝试重新登录。\n\n" + ex.Message + "\n" + ex.StackTrace;
                result = null;
            }
            return result;
        }

        public string getVerifyPageInfo()
        {
            string result;
            try
            {
                result = this.DownloadString("https://tw.newlogin.beanfun.com/LoginCheck/AdvanceCheck.aspx");
            }
            catch (Exception ex)
            {
                this.errmsg = "VerifyUnknown\n\n" + ex.Message + "\n" + ex.StackTrace;
                result = null;
            }
            return result;
        }

        public BitmapImage getVerifyCaptcha(string samplecaptcha)
        {
            BitmapImage bitmapImage;
            try
            {
                byte[] buffer = base.DownloadData("https://tw.newlogin.beanfun.com/LoginCheck/BotDetectCaptcha.ashx?get=image&c=c_logincheck_advancecheck_samplecaptcha&t=" + samplecaptcha);
                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(buffer);
                bitmapImage.EndInit();
            }
            catch (Exception)
            {
                bitmapImage = null;
            }
            return bitmapImage;
        }

        public string verify(string viewstate, string eventvalidation, string samplecaptcha, string verifyCode, string captchaCode)
        {
            string result;
            try
            {
                result = this.UploadString("https://tw.newlogin.beanfun.com/LoginCheck/AdvanceCheck.aspx", new NameValueCollection
                {
                    {
                        "__VIEWSTATE",
                        viewstate
                    },
                    {
                        "__EVENTVALIDATION",
                        eventvalidation
                    },
                    {
                        "txtVerify",
                        verifyCode
                    },
                    {
                        "CodeTextBox",
                        captchaCode
                    },
                    {
                        "imgbtnSubmit.x",
                        "19"
                    },
                    {
                        "imgbtnSubmit.y",
                        "23"
                    },
                    {
                        "LBD_VCID_c_logincheck_advancecheck_samplecaptcha",
                        samplecaptcha
                    }
                });
            }
            catch (Exception ex)
            {
                this.errmsg = "VerifyUnknown\n\n" + ex.Message + "\n" + ex.StackTrace;
                result = null;
            }
            return result;
        }

        public GameServerAccountListApp gameServAccListApp;

        private BFServiceX m_a;

        private CookieContainer m_b;

        private Uri m_c;

        public string errmsg;

        private string m_d;

        private string m_e;

        public List<BeanfunClient.ServiceAccount> accountList;

        public int remainPoint;

        public string accountAmountLimitNotice;

        private bool m_f;

        private const string g = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36";

        private string m_h;

        private string m_i;

        public class ServiceAccount
        {
            // (get) Token: 0x0600018E RID: 398 RVA: 0x0000C600 File Offset: 0x0000A800
            // (set) Token: 0x0600018F RID: 399 RVA: 0x0000C608 File Offset: 0x0000A808
            public bool isEnable
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


            public bool visible
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


            public bool isinherited
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


            public string sid
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

            public string ssn
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


            public string sname
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


            public string screatetime
            {
                [CompilerGenerated]
                get
                {
                    return this.g;
                }
                [CompilerGenerated]
                set
                {
                    this.g = value;
                }
            }


            public string slastusedtime
            {
                [CompilerGenerated]
                get
                {
                    return this.h;
                }
                [CompilerGenerated]
                set
                {
                    this.h = value;
                }
            }

            public string sauthtype
            {
                [CompilerGenerated]
                get
                {
                    return this.i;
                }
                [CompilerGenerated]
                set
                {
                    this.i = value;
                }
            }

            public ServiceAccount(bool isEnable, string sid, string ssn, string sname, string screatetime)
            {
                this.isEnable = isEnable;
                this.visible = true;
                this.isinherited = false;
                this.sid = sid;
                this.ssn = ssn;
                this.sname = sname;
                this.screatetime = screatetime;
                this.slastusedtime = null;
                this.sauthtype = null;
            }

            public ServiceAccount(bool isEnable, bool visible, bool isinherited, string sid, string ssn, string sname, string screatetime, string slastusedtime, string sauthtype)
            {
                this.isEnable = isEnable;
                this.visible = visible;
                this.isinherited = isinherited;
                this.sid = sid;
                this.ssn = ssn;
                this.sname = sname;
                this.screatetime = screatetime;
                this.slastusedtime = slastusedtime;
                this.sauthtype = sauthtype;
            }

            [CompilerGenerated]
            private bool a;

            [CompilerGenerated]
            private bool b;

            [CompilerGenerated]
            private bool c;

            [CompilerGenerated]
            private string d;

            [CompilerGenerated]
            private string e;

            [CompilerGenerated]
            private string f;

            [CompilerGenerated]
            private string g;

            [CompilerGenerated]
            private string h;

            [CompilerGenerated]
            private string i;
        }

        public class QRCodeClass
        {
            public QRCodeClass()
            {
            }

            public string skey;

            public string value;

            public string viewstate;

            public string eventvalidation;

            public string bitmapUrl;

            public bool oldAppQRCode;
        }
    }
}
